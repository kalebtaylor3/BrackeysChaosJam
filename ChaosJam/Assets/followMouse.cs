﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class followMouse : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 mousePosition;
    private SpriteRenderer image;
    private void OnEnable()
    {
        WaveController.OnWave += CheckWave;
    }

    private void OnDisable()
    {
        WaveController.OnWave -= CheckWave;
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

    void CheckWave(int wave)
    {
        switch (wave)
        {
            case 0:
                this.transform.localScale = new Vector2(0.40363f, 0.40363f);
                break;
            case 1:
                this.transform.localScale = new Vector2(0.54f, 0.54f);
                break;
            case 2:

                this.transform.localScale = new Vector2(0.64f, 0.64f);
                break;
            case 3:

                this.transform.localScale = new Vector2(0.74f, 0.74f);
                break;
            case 4:

                this.transform.localScale = new Vector2(0.84f, 0.84f);
                break;
            case 5:
                this.transform.localScale = new Vector2(1f, 1f);
                break;
        }
    }
}
