using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoundZoomer : MonoBehaviour
{
    Camera main;

    // Start is called before the first frame update
    void Start()
    {
        main = GetComponent<Camera>();
        main.orthographicSize = 5;
    }

    private void OnEnable()
    {
        WaveController.OnWave += CheckWave;
    }
    private void OnDisable()
    {
        WaveController.OnWave -= CheckWave;
    }

    void CheckWave(int wave)
    {
        switch (wave)
        {
            case 0:
                Debug.Log("Wave 1");
                main.orthographicSize = 5;
                break;
            case 1:
                main.orthographicSize = Mathf.Lerp(main.orthographicSize, 7, 0.008f);
                Debug.Log("Wave 2");
                break;
            case 2:
                Debug.Log("Wave 3");
                main.orthographicSize = Mathf.Lerp(main.orthographicSize, 9, 0.008f);
                break;
            case 3:
                Debug.Log("Wave 4");
                main.orthographicSize = Mathf.Lerp(main.orthographicSize, 12, 0.008f);
                break;
            case 4:
                Debug.Log("Wave 5");
                main.orthographicSize = Mathf.Lerp(main.orthographicSize, 14, 0.008f);
                break;
            case 5:
                Debug.Log("Wave 6");
                main.orthographicSize = Mathf.Lerp(main.orthographicSize, 15, 0.008f);
                break;
            case 6:
                Debug.Log("Wave 7");
                main.orthographicSize = Mathf.Lerp(main.orthographicSize, 16, 0.008f);
                break;
            case 7:
                Debug.Log("Wave 8");
                main.orthographicSize = Mathf.Lerp(main.orthographicSize, 17, 0.008f);
                break;
            case 8:
                Debug.Log("Wave 9");
                main.orthographicSize = Mathf.Lerp(main.orthographicSize, 18, 0.008f);
                break;
            case 9:
                Debug.Log("Wave 10");
                break;
        }
    }
}
