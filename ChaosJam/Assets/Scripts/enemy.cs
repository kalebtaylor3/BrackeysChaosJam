using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class enemy : MonoBehaviour
{

    public Transform HealthBar;
    Transform healthBarTransform;
    HealthSystem healthSystem;
    // Start is called before the first frame update
    void Start()
    {
        healthSystem = new HealthSystem(100);

        healthBarTransform = Instantiate(HealthBar, new Vector2(transform.position.x, transform.position.y + 0.75f), Quaternion.identity);
        healthBarTransform.SetParent(this.transform);
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
            Destroy(this.gameObject);
        }

    }

    void TakeDamage()
    {
        healthSystem.Damage(20);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            healthSystem.Damage(0.55f);
        }
    }
}
