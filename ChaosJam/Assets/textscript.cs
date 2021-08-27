using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textscript : MonoBehaviour
{

    public GameObject nextWave;
    public int secondsLeft = 5;
    public bool takingAway = false;
    // Start is called before the first frame update
    void Start()
    {
        nextWave.GetComponent<Text>().text = "wave starting in 00:0" + secondsLeft;
    }

    // Update is called once per frame
    void Update()
    {

            nextWave.SetActive(true);
            if (takingAway == false && secondsLeft > 0)
            {
                StartCoroutine(TimerTake());
            }

        if (secondsLeft <= 0)
        {
            Destroy(this.gameObject);
        }

    }

    IEnumerator TimerTake()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        nextWave.GetComponent<Text>().text = "wave starting in 00:0" + secondsLeft;
        takingAway = false;
    }
}
