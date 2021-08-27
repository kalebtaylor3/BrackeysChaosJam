using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boss : MonoBehaviour
{
    public Transform HealthBar;
    Transform healthBarTransform;
    public HealthSystem healthSystem;
    public Animator animations;
    public Transform holder;
    public static event Action OnDeath;
    public GameObject _bloodDeath;
    public GameObject _bloodChip;
    bool hasHappened = false;
    public Transform bosspos;
    public GameObject _bosschip;
    public AudioSource hittingWall;
    bool isHappening = false;
    bool willhappen = false;
    bool happen = false;
    public AudioSource shooting;
    public AudioSource beingHit;
    // Start is called before the first frame update
    void Start()
    {
        healthSystem = new HealthSystem(500);

        healthBarTransform = Instantiate(HealthBar, bosspos.position, Quaternion.identity);
        healthBarTransform.SetParent(bosspos);
        healthBarTransform.position = bosspos.position;
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);
    }
    private void OnEnable()
    {
        //Bullet.OnBullet += TakeDamage;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            healthSystem.Damage(10);
        }

        if (healthSystem.GetHealth() == 0)
        {
            Debug.Log("Dead Af");
            if (!hasHappened)
            {
                OnDeath?.Invoke();
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

        if (!happen)
        {
            beingHit.Play();
            happen = true;
        }

        if (collision.gameObject.tag == "Wall")
        {
            if (!isHappening)
            {
                hittingWall.Play();
                isHappening = true;
            }
            healthSystem.Damage(0.55f);
            animations.SetBool("Attacking", true);
        }

        if (collision.gameObject.tag == "Turret")
        {
            if (!willhappen)
            {
                shooting.Play();
                willhappen = true;
            }
            Instantiate(_bloodChip, new Vector2(transform.position.x, transform.position.y + 0.2f), transform.rotation);
            animations.SetBool("Attacking", true);
        }

        if (collision.gameObject.tag == "Player")
        {
            healthSystem.Damage(0.25f);
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

            Instantiate(_bosschip, new Vector2(transform.position.x, transform.position.y + 0.2f), transform.rotation);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        animations.SetBool("Attacking", false);
        hittingWall.Stop();
        shooting.Stop();
        beingHit.Stop();
        isHappening = false;
        willhappen = false;
        happen = false;
    }
}
