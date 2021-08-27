using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurretAi : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float scanRadius = 3f;
    [SerializeField] private LayerMask layers;
    [SerializeField] private GameObject fireball;
    [SerializeField] private Transform firepoint;
    [SerializeField] private float fireDelay = 1f;
    private Collider2D target;
    public static event Action onShoot; 
    bool looking = false;

    public Animator animations;

    // Update is called once per frame
    void Update()
    {
        CheckEnviorment();
        LookAtTarget();

        if(looking)
        {
            animations.SetBool("shoot", true);
            target.GetComponent<enemy>().healthSystem.Damage(0.2f);
            onShoot?.Invoke();

        }
        else if(!looking)
        {
            animations.SetBool("shoot", false);
        }
    }

    private void CheckEnviorment()
    {
        target = Physics2D.OverlapCircle(transform.position, scanRadius, layers);
    }

    private void LookAtTarget()
    {
        if(target != null)
        {
            if(target.GetComponent<enemy>() != null)
            {
                Vector2 Dir = target.transform.position - transform.position;
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
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, scanRadius);
    }
}
