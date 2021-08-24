using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class enemy : MonoBehaviour
{

    public Transform HealthBar;
    Transform healthBarTransform;
    HealthSystem healthSystem;
    public Animator animations;
    public Transform holder;
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
        Bullet.OnBullet += TakeDamage;
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
            StartCoroutine(Die());
        }

    }

    void TakeDamage()
    {
        healthSystem.Damage(20);
    }


    IEnumerator Die()
    {
        yield return new WaitForSeconds(2);
        Destroy(holder.gameObject);
    }    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            healthSystem.Damage(0.55f);
            animations.SetBool("Attacking", true);
        }

        if (collision.gameObject.tag == "Player")
        {
            animations.SetBool("Attacking", true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        animations.SetBool("Attacking", false);
    }
}
