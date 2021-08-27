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

        Vector2 v = Camera.main.ScreenToWorldPoint(mousePosition);

        Collider2D[] col = Physics2D.OverlapPointAll(v);

        if (col.Length > 0)
        {
            foreach (Collider2D c in col)
            {
                //Debug.Log("Collided with: " + c.collider2D.gameObject.name);
                //targetPos = c.collider2D.gameObject.transform.position;

                if (c.gameObject.tag == "Turret")
                {
                    if (deleteMode == true && Input.GetMouseButtonDown(0))
                    {
                        c.GetComponent<TurretAi>().DestroyTurret();
                        Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
                        //Instantiate(_delete, mouseWorldPosition, c.GetComponent<Transform>().rotation);
                        GetResources?.Invoke(20);
                    }

                }

            }

        }

        CheckEnviorment();
        LookAtTarget();

        if(looking)
        {
            animations.SetBool("shoot", true);
            target.GetComponent<enemy>().healthSystem.Damage(0.6f);
            onShoot?.Invoke();
            GameObject particle = Instantiate(_shooting, shootingPoint.position, bigman.rotation);
            particle.transform.SetParent(bigman);
                if(!happenOnce)
                {
                    shooting.Play();
                    happenOnce = true;
                }

        }
        else if(!looking)
        {
            animations.SetBool("shoot", false);
            shooting.Stop();
            happenOnce = false;
        }
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
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }

    private void LookAtTarget()
    {
        if(target != null)
        {
            if(target.GetComponent<enemy>() != null)
            {
                float distanceToClosestEnemy = Mathf.Infinity;
                enemy closestEnemy = null;
                enemy[] allEnemies = GameObject.FindObjectsOfType<enemy>();

                foreach (enemy currentEnemy in allEnemies)
                {
                    float distancetoEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
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
            }else if (target.GetComponent<Boss>() != null)
            {

                float distanceToClosestEnemy = Mathf.Infinity;
                Boss closestEnemy = null;
                Boss[] allEnemies = GameObject.FindObjectsOfType<Boss>();

                foreach (Boss currentEnemy in allEnemies)
                {
                    float distancetoEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
                    if (distancetoEnemy < distanceToClosestEnemy)
                    {
                        distanceToClosestEnemy = distancetoEnemy;
                        closestEnemy = currentEnemy;
                    }
                }

                GameObject particle = Instantiate(_shooting, shootingPoint.position, bigman.rotation);
                particle.transform.SetParent(bigman);
                target.GetComponent<Boss>().healthSystem.Damage(0.1f);
                Vector2 Dir = closestEnemy.transform.position - transform.position;
                float angle = Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                looking = true;
            }
        }
        else
        {
            looking = false;
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
