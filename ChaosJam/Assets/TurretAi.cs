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
    private Collider2D target;
    public static event Action onShoot; 
    bool looking = false;
    bool deleteMode = false;
    public Animator animations;
    public GameObject _delete;
    public static event Action<int> GetResources;

    private void OnEnable()
    {
        PlacementManager.delMode += DeleteMode;
    }
    // Update is called once per frame
    void Update()
    {

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
            target.GetComponent<enemy>().healthSystem.Damage(0.2f);
            onShoot?.Invoke();

        }
        else if(!looking)
        {
            animations.SetBool("shoot", false);
        }
    }


    void DestroyTurret()
    {
        Destroy(this.gameObject);
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

    void DeleteMode()
    {

        deleteMode = true;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, scanRadius);
    }
}
