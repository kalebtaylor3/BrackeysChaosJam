using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretIAi : MonoBehaviour
{
    public float range;

    public Transform target;

    bool detected = false;

    Vector2 direction;

    public GameObject gun;

    public GameObject bullet;

    public float fireRate;

    private float nextTimeToShoot = 0;

    public Transform ShootPoint;

    public float force;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetPos = target.position;

        direction = targetPos - (Vector2)transform.position;

        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, direction, range);

        if(rayInfo)
        {
            if (rayInfo.collider.gameObject.tag == "Enemy")
            {
                if (detected == false)
                {
                    detected = true;
                   
                }
            }

            else
            {
                if (detected == true)
                {
                    detected = false;
                    
                }
            }
        }

        if(detected)
        {
            gun.transform.up = direction;
            if(Time.time > nextTimeToShoot)
            {
                nextTimeToShoot = Time.time + 1 / fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        GameObject BulletIns = Instantiate(bullet, ShootPoint.position, Quaternion.identity);
        BulletIns.GetComponent<Rigidbody2D>().AddForce(direction * force);

    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
