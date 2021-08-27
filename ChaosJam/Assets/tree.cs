using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tree : MonoBehaviour
{

    public Transform HealthBar;
    Transform healthBarTransform;
    HealthSystem healthSystem;

    bool isHappening = false;
    public AudioSource hittingWall;
    public GameObject _wallchip;
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
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            healthSystem.Damage(0.75f);
            if (!isHappening)
            {
                hittingWall.Play();
                isHappening = true;
            }

            Instantiate(_wallchip, new Vector2(transform.position.x, transform.position.y + 0.2f), transform.rotation);
        }

        if (collision.gameObject.tag == "Boss")
        {
            healthSystem.Damage(1.50f);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        hittingWall.Stop();
        isHappening = false;
    }
}
