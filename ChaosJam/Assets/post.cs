using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class post : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(Gone());
    }

    IEnumerator Gone()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
    }
}
