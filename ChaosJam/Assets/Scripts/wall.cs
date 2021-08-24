using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class wall : MonoBehaviour
{

    public Transform HealthBar;
    Transform healthBarTransform;
    HealthSystem healthSystem;
    bool repairMode = true;
    public static event Action<int, HealthSystem> UseResources;
    public static event Action OnHover;
    public static event Action OffHover;
    // Start is called before the first frame update
    void Start()
    {
        healthSystem = new HealthSystem(100);

        healthBarTransform = Instantiate(HealthBar, new Vector2(transform.position.x, transform.position.y + 0.45f), Quaternion.identity);
        healthBarTransform.SetParent(this.transform);
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);
    }

    // Update is called once per frame
    void Update()
    {
        if (healthSystem.GetHealth() == 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            healthSystem.Damage(0.25f);
        }
    }

    private void OnMouseDown()
    {
        if (repairMode)
        {
            UseResources?.Invoke(5, healthSystem);
        }
    }

    private void OnMouseEnter()
    {
        if (repairMode)
        {
            OnHover?.Invoke();
        }
    }

    private void OnMouseExit()
    {
        OffHover?.Invoke();
    }

    void RepairMode()
    {
        repairMode = true;
    }

    void otherModes()
    {
        repairMode = false;
    }
}