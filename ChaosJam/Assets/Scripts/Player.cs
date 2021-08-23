using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Transform HealthBar;
    Transform healthBarTransform;
    HealthSystem healthSystem;
    // Start is called before the first frame update
    void Start()
    {
        healthSystem = new HealthSystem(100);

        healthBarTransform = Instantiate(HealthBar, new Vector2(transform.position.x, transform.position.y + 0.75f), Quaternion.identity);
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
