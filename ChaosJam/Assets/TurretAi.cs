using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CodeMonkey.Utils;

public class TurretAi : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float scanRadius = 3f;
    [SerializeField] private LayerMask layers;
    [SerializeField] private GameObject fireball;
    [SerializeField] private Transform firepoint;
    [SerializeField] private float fireDelay = 1f;

    public Transform HealthBar;
    Transform healthBarTransform;
    HealthSystem healthSystem;
    private Collider2D target;
    public static event Action onShoot; 
    bool looking = false;
    bool deleteMode = false;
    public Animator animations;
    public GameObject _delete;
    public static event Action<int> GetResources;

    public GameObject _shooting;
    public Transform shootingPoint;
    public Transform bigman;
    public GameObject _bloodChip;
    public GameObject _bloodDeath;
    public AudioSource shooting;
    public AudioSource delete;
    bool happenOnce = false;

    enemy activeEnemy;

    private void OnEnable()
    {
        PlacementManager.delMode += DeleteMode;
        BuildingTypeSelectUI.DisableDelete += NotDeleteMode;
    }

    private void Start()
    {
        healthSystem = new HealthSystem(100);

        healthBarTransform = Instantiate(HealthBar, new Vector2(transform.position.x, transform.position.y + 0.75f), Quaternion.identity);
        healthBarTransform.SetParent(this.transform);
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);
        happenOnce = false;
    }

    private void OnDisable()
    {
        PlacementManager.delMode -= DeleteMode;
        BuildingTypeSelectUI.DisableDelete -= NotDeleteMode;
    }
    // Update is called once per frame
    void Update()
    {


        if (healthSystem.GetHealth() == 0)
        {
            StartCoroutine(Die());
            Instantiate(_bloodDeath, new Vector2(transform.position.x, transform.position.y + 0.2f), transform.rotation);
        }

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 5f;

        Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Collider2D[] colliders = Physics2D.OverlapPointAll(worldMousePosition);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Turret") && deleteMode && Input.GetMouseButtonDown(0))
            {
                collider.GetComponent<TurretAi>().DestroyTurret();
                Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
                //Instantiate(_delete, mouseWorldPosition, collider.GetComponent<Transform>().rotation);
                GetResources?.Invoke(20);
            }
        }

        CheckEnviorment();
        LookAtTarget();

        if (looking && activeEnemy != null)
        {
            animations.SetBool("shoot", true);
            if (activeEnemy != null)
            {
                activeEnemy.healthSystem.Damage(0.6f);
            }
            onShoot?.Invoke();
            GameObject particle = Instantiate(_shooting, shootingPoint.position, bigman.rotation);
            particle.transform.SetParent(bigman);

            if (!happenOnce)
            {
                shooting.Play();
                happenOnce = true;
            }
        }
        else
        {
            animations.SetBool("shoot", false);
            shooting.Stop();
            happenOnce = false;
        }

        if(activeEnemy == null)
        {
            animations.SetBool("shoot", false);
            shooting.Stop();
            happenOnce = false;
        }

        Debug.Log(activeEnemy);
    }


    void DestroyTurret()
    {
        Destroy(this.gameObject);
    }

    void NotDeleteMode()
    {
        deleteMode = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Instantiate(_bloodChip, new Vector2(transform.position.x, transform.position.y + 0.2f), transform.rotation);
            healthSystem.Damage(0.75f);
        }

        if (collision.gameObject.tag == "Boss")
        {
            Instantiate(_bloodChip, new Vector2(transform.position.x, transform.position.y + 0.2f), transform.rotation);
            healthSystem.Damage(1.50f);
        }
    }


    private void CheckEnviorment()
    {
        target = Physics2D.OverlapCircle(transform.position, scanRadius, layers);

        if (target != null)
            activeEnemy = target.GetComponent<enemy>();
        else
            activeEnemy = null;
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }

    private void LookAtTarget()
    {
        if (target != null)
        {
            if (target.TryGetComponent(out enemy closestEnemy))
            {
                float distanceToClosestEnemy = Mathf.Infinity;
                enemy[] allEnemies = FindObjectsOfType<enemy>();

                foreach (enemy currentEnemy in allEnemies)
                {
                    float distancetoEnemy = (currentEnemy.transform.position - transform.position).sqrMagnitude;
                    if (distancetoEnemy < distanceToClosestEnemy)
                    {
                        distanceToClosestEnemy = distancetoEnemy;
                        closestEnemy = currentEnemy;
                    }
                }

                Vector2 Dir = closestEnemy.transform.position - transform.position;
                float angle = Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                looking = true;
            }
            else if (target.TryGetComponent(out Boss boss))
            {
                float distanceToClosestEnemy = Mathf.Infinity;
                Boss[] allBosses = FindObjectsOfType<Boss>();

                foreach (Boss currentBoss in allBosses)
                {
                    float distancetoBoss = (currentBoss.transform.position - transform.position).sqrMagnitude;
                    if (distancetoBoss < distanceToClosestEnemy)
                    {
                        distanceToClosestEnemy = distancetoBoss;
                        boss = currentBoss;
                    }
                }

                GameObject particle = Instantiate(_shooting, shootingPoint.position, bigman.rotation);
                particle.transform.SetParent(bigman);
                boss.healthSystem.Damage(0.1f);
                Vector2 Dir = boss.transform.position - transform.position;
                float angle = Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                looking = true;
            }
            else
            {
                looking = false;
                activeEnemy = null;
            }
        }
        else
        {
            looking = false;
            activeEnemy = null;
        }
    }

    void DeleteMode()
    {

        deleteMode = true;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, scanRadius);
    }
}
