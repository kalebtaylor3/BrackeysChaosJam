using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followMouse : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 mousePosition;
    private SpriteRenderer image;

    private void OnEnable()
    {
        wall.OnHover += Visable;
        wall.OffHover += NotVisable;
    }

    void Start()
    {

        image = GetComponent<SpriteRenderer>();

        Color c = image.color;
        c.a = 0;
        image.color = c;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = Vector2.Lerp(transform.position, mousePosition, 1);
    }

    void Visable()
    {
        Color c = image.color;
        c.a = 1;
        image.color = c;
    }

    void NotVisable()
    {
        Color c = image.color;
        c.a = 0;
        image.color = c;
    }
}
