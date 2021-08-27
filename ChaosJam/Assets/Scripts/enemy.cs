using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class enemy : MonoBehaviour
{

    public Transform HealthBar;
    Transform healthBarTransform;
    public HealthSystem healthSystem;
    public Animator animations;
    public Transform holder;
    public static event Action<int, enemy> OnDeath;
    public GameObject _bloodDeath;
    public GameObject _bloodChip;
    public GameObject _wallchip;
    bool hasHappened = false;
    public AudioSource destroyWall;
    public AudioSource hittingWall;
    public AudioSource shooting;

    bool happenOnce = false;

    bool isHappening = false;
    // Start is called before the first frame update
    void Start()
    {
        healthSystem = new HealthSystem(100);

        healthBarTransform = Instantiate(HealthBar, new Vector2(transform.position.x, transform.position.y + 0.75f), Quaternion.identity);
        healthBarTransform.SetParent(holder);
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);
    }
    private void OnEnable()
    {
        //Bullet.OnBullet += TakeDamage;
        TurretAi.onShoot += spawnBlood;
    }

    private void OnDisable()
    {
        TurretAi.onShoot -= spawnBlood;
    }

    // Update is called once per frame
    void Update()
    {
        healthBarTransform.position = new Vector2(transform.position.x, transform.position.y + 1);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            healthSystem.Damage(10);
        }

        if(healthSystem.GetHealth() == 0)
        {
            Debug.Log("Dead Af");
            if (!hasHappened)
            {
                OnDeath?.Invoke(10, this);
                hasHappened = true;
            }
            StartCoroutine(Die());
            Instantiate(_bloodDeath, new Vector2(transform.position.x, transform.position.y + 0.2f), transform.rotation);
        }

    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(holder.gameObject);
    }

    void TakeDamage()
    {
        healthSystem.Damage(20);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        animations.SetBool("Attacking", true);

        if (collision.gameObject.tag == "Wall")
        {
            if(!isHappening)
            {
                hittingWall.Play();
                isHappening = true;
            }
            healthSystem.Damage(0.55f);
            animations.SetBool("Attacking", true);
        }

        if(collision.gameObject.GetComponent<wall>().healthSystem.GetHealth() <= 0)
        {
            Debug.Log("wall destyoryted");
            destroyWall.Play();
        }

        if (collision.gameObject.tag == "Player")
        {
            healthSystem.Damage(0.85f);
            animations.SetBool("Attacking", true);

            Instantiate(_bloodChip, new Vector2(transform.position.x, transform.position.y + 0.2f), transform.rotation);
        }

        if (collision.gameObject.tag == "Turret")
        {
            if (!happenOnce)
            {
                shooting.Play();
            }
            //healthSystem.Damage(0.5f);
            animations.SetBool("Attacking", true);

            Instantiate(_bloodChip, new Vector2(transform.position.x, transform.position.y + 0.2f), transform.rotation);
        }

        if (collision.gameObject.tag == "Boss")
        {
            healthSystem.Damage(1f);
            animations.SetBool("Attacking", true);

            Instantiate(_bloodChip, new Vector2(transform.position.x, transform.position.y + 0.2f), transform.rotation);
        }

        if (collision.gameObject.tag == "Tree")
        {
            if (!isHappening)
            {
                hittingWall.Play();
                isHappening = true;
            }
            healthSystem.Damage(0.15f);
            animations.SetBool("Attacking", true);

            Instantiate(_wallchip, new Vector2(transform.position.x, transform.position.y + 0.2f), transform.rotation);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        animations.SetBool("Attacking", false);
        hasHappened = false;
        isHappening = false;
        hittingWall.Stop();
    }

    void spawnBlood()
    {
        Instantiate(_bloodChip, new Vector2(transform.position.x, transform.position.y + 0.2f), transform.rotation);
    }
}
