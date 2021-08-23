using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{

    public Transform HealthBar;
    Transform healthBarTransform;
    // Start is called before the first frame update
    void Start()
    {
        HealthSystem healthSystem = new HealthSystem(100);

        healthBarTransform = Instantiate(HealthBar, new Vector2(transform.position.x, transform.position.y + 1), Quaternion.identity);
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);
    }

    // Update is called once per frame
    void Update()
    {
        healthBarTransform.position = new Vector2(transform.position.x, transform.position.y + 1.5f);
    }
}
