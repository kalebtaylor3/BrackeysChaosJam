﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{

    public Transform HealthBar;
    Transform healthBarTransform;
    HealthSystem healthSystem;
    public Animator animations;
    public static event Action OnDeath;
    public GameObject _bloodDeath;
    public GameObject _bloodChip;
    bool isDone = false;
    public AudioSource death;
    bool once = false;
    public AudioSource hit;
    // Start is called before the first frame update
    void Start()
    {
        healthSystem = new HealthSystem(100);

        healthBarTransform = Instantiate(HealthBar, new Vector2(transform.position.x, transform.position.y + 0.75f), Quaternion.identity);
        healthBarTransform.SetParent(this.transform);
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);
    }

    private void Update()
    {
        if (healthSystem.GetHealth() == 0)
        {
            Debug.Log("Dead Af");
            if (!isDone)
            {
                OnDeath?.Invoke();
                isDone = true;
            }
            Instantiate(_bloodDeath, new Vector2(transform.position.x, transform.position.y + 0.2f), transform.rotation);
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        death.Play();
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Instantiate(_bloodChip, new Vector2(transform.position.x, transform.position.y + 0.2f), transform.rotation);
            healthSystem.Damage(0.75f);
            animations.SetBool("Attacking", true);

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

            transform.rotation = closestEnemy.transform.rotation;

            closestEnemy.healthSystem.Damage(0.95f);
            if(!once)
            {
                hit.Play();
                death.Play();
                once = true;
            }
        }

        if (collision.gameObject.tag == "Boss")
        {
            Instantiate(_bloodChip, new Vector2(transform.position.x, transform.position.y + 0.2f), transform.rotation);
            healthSystem.Damage(1.50f);
            animations.SetBool("Attacking", true);

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

            transform.rotation = closestEnemy.transform.rotation;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        animations.SetBool("Attacking", false);
        once = false;
        death.Stop();
        hit.Stop();
    }
}
