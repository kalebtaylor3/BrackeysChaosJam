using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class wall : MonoBehaviour
{

    public Transform HealthBar;
    Transform healthBarTransform;
    HealthSystem healthSystem;
    bool repairMode = false;
    public static event Action<int, HealthSystem> UseResources;
    public GameObject _woodChip;
    public GameObject _bloodChip;
    // Start is called before the first frame update
    void Start()
    {
        healthSystem = new HealthSystem(100);

        healthBarTransform = Instantiate(HealthBar, new Vector2(transform.position.x, transform.position.y + 0.50f), Quaternion.identity);
        healthBarTransform.SetParent(this.transform);
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);
    }

    private void OnEnable()
    {
        PlacementManager.makeVisable += RepairMode;
        BuildingTypeSelectUI.DisableBuilder += NotRepairMode;
    }

    private void OnDisable()
    {
        PlacementManager.makeVisable -= RepairMode;
        BuildingTypeSelectUI.DisableBuilder -= NotRepairMode;
    }



    // Update is called once per frame
    void Update()
    {
        if (healthSystem.GetHealth() == 0)
        {
            GameObject holder = GameObject.Find("VerticalWallHolder");
            Destroy(this.gameObject);

            if(holder != null)
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

                        c.GetComponent<wall>().Repair();
                    }

                }

            }

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            healthSystem.Damage(0.25f);
            Instantiate(_woodChip, new Vector2(collision.transform.position.x, collision.transform.position.y + 0.2f), transform.rotation);
            Instantiate(_bloodChip, new Vector2(collision.transform.position.x, collision.transform.position.y + 0.2f), transform.rotation);

        }
    }

    void RepairMode()
    {
        repairMode = true;
    }

    void NotRepairMode()
    {
        repairMode = false;
    }

    public void Repair()
    {
        UseResources?.Invoke(5, this.healthSystem);
    }

    //private void OnMouseDown()
    //{
        //if (repairMode)
        //{
            //UseResources?.Invoke(5, healthSystem);
        //}
    //}

}