using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundZoomer : MonoBehaviour
{

    private int waves;

    Vector3 pos;

    Camera main;

    protected float Timer = 0;

    public int DelayAmount = 25;

    // Start is called before the first frame update
    void Start()
    {
        waves = 0;
        pos = transform.position;
        main = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        Timer += Time.deltaTime;

        if(Timer >= DelayAmount)
        {
            Timer = 0;
            waves++;
        }

        switch (waves)
        {
            case 0:
                Debug.Log("Wave 1");
                main.orthographicSize = 6;
                break;
            case 1:
                main.orthographicSize = Mathf.Lerp(main.orthographicSize, 8, 0.01f);
                Debug.Log("Wave 2");
                break;
            case 2:
                Debug.Log("Wave 3");
                main.orthographicSize = Mathf.Lerp(main.orthographicSize, 10, 0.01f);
                break;
            case 3:
                Debug.Log("Wave 4");
                main.orthographicSize = Mathf.Lerp(main.orthographicSize, 12, 0.01f);
                break;
            case 4:
                Debug.Log("Wave 5");
                break;
            case 5:
                Debug.Log("Wave 6");
                break;
            case 6:
                Debug.Log("Wave 7");
                break;
            case 7:
                Debug.Log("Wave 8");
                break;
            case 8:
                Debug.Log("Wave 9");
                break;
            case 9:
                Debug.Log("Wave 10");
                break;
        }
    }
}
