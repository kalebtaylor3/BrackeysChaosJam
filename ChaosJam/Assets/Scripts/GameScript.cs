using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{

    public Transform HealthBar;

    // Start is called before the first frame update
    void Start()
    {
        HealthSystem healthSystem = new HealthSystem(100);

        Transform healthBarTransform = Instantiate(HealthBar, new Vector3(0, 10), Quaternion.identity);
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);

        Debug.Log("Health: " +healthSystem.GetHealth());
        healthSystem.Damage(10);
        Debug.Log("Health: " + healthSystem.GetHealth());
        healthSystem.Damage(20);
        Debug.Log("Health: " + healthSystem.GetHealth());
        Debug.Log("Health: " + healthSystem.GetHealthPercent());
        healthSystem.Damage(20);
        Debug.Log("Health: " + healthSystem.GetHealthPercent());



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
