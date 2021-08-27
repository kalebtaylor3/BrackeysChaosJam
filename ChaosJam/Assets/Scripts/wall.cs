using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CodeMonkey.Utils;

public class wall : MonoBehaviour
{

    public Transform HealthBar;
    Transform healthBarTransform;
    public HealthSystem healthSystem;
    bool repairMode = false;
    bool deleteMode = false;
    public static event Action<int, HealthSystem, wall> UseResources;
    public static event Action<int> GetResources;
    public GameObject _woodChip;
    public GameObject _bloodChip;
    public GameObject _bossblood;
    public GameObject _delete;
    public GameObject _bosschip;
    //public AudioSource hittingWall;

    bool happenOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        healthSystem = new HealthSystem(100);

        healthBarTransform = Instantiate(HealthBar, new Vector2(transform.position.x, transform.position.y + 0.50f), Quaternion.identity);
        healthBarTransform.SetParent(this.transform);
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);

        Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
        Instantiate(_delete, mouseWorldPosition, this.GetComponent<Transform>().rotation);
    }

    private void OnEnable()
    {
        PlacementManager.makeVisable += RepairMode;
        BuildingTypeSelectUI.DisableBuilder += NotRepairMode;

        PlacementManager.delMode += DeleteMode;
        BuildingTypeSelectUI.DisableDelete += NotDeleteMode;

        PlacementManager.OnParticle += SpawnParticle;
    }

    private void OnDisable()
    {
        PlacementManager.makeVisable -= RepairMode;
        BuildingTypeSelectUI.DisableBuilder -= NotRepairMode;

        PlacementManager.delMode -= DeleteMode;
        BuildingTypeSelectUI.DisableDelete -= NotDeleteMode;
    }



    // Update is called once per frame
    void Update()
    {
        if (healthSystem.GetHealth() == 0)
        {
            GameObject holder = GameObject.Find("VerticalWallHolder");
            Destroy(this.gameObject);
            if (holder != null)
            {
                Destroy(holder);
            }

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

                if(c.gameObject.tag == "Wall")
                {
                    if(repairMode == true && Input.GetMouseButtonDown(0))
                    {
                        //play repair sound efFECT
                        c.GetComponent<wall>().Repair();
                    }

                    if (deleteMode == true && Input.GetMouseButtonDown(0))
                    {
                        c.GetComponent<wall>().DestroyWall();
                        Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
                        Instantiate(_delete, mouseWorldPosition, c.GetComponent<Transform>().rotation);
                        GetResources?.Invoke(20);
                    }

                }

            }

        }
    }

    void SpawnParticle()
    {

    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (!happenOnce)
        {
            //hittingWall.Play();
            happenOnce = true;
        }

        if (collision.gameObject.tag == "Enemy")
        {
            healthSystem.Damage(0.25f);
            Instantiate(_woodChip, new Vector2(collision.transform.position.x, collision.transform.position.y + 0.2f), transform.rotation);
            Instantiate(_bloodChip, new Vector2(collision.transform.position.x, collision.transform.position.y + 0.2f), transform.rotation);

        }

        if (collision.gameObject.tag == "Boss")
        {
            healthSystem.Damage(1f);
            Instantiate(_bosschip, new Vector2(collision.transform.position.x, collision.transform.position.y + 0.9f), transform.rotation);
            Instantiate(_bossblood, new Vector2(collision.transform.position.x, collision.transform.position.y + 0.2f), transform.rotation);

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //hittingWall.Stop();
    }

    void RepairMode()
    {
        repairMode = true;
        deleteMode = false;
    }

    void NotRepairMode()
    {
        repairMode = false;
    }

    void DeleteMode()
    {
        deleteMode = true;
        repairMode = false;
    }

    void NotDeleteMode()
    {
        deleteMode = false;
    }

    public void DestroyWall()
    {
        GameObject holder = GameObject.Find("VerticalWallHolder");
        Destroy(this.gameObject);

        if (holder != null)
        {
            Destroy(holder);
        }
    }

    public void Repair()
    {
        UseResources?.Invoke(5, this.healthSystem, this);
    }

    //private void OnMouseDown()
    //{
        //if (repairMode)
        //{
            //UseResources?.Invoke(5, healthSystem);
        //}
    //}

}