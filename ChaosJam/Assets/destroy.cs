using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(Gone());
    }

    IEnumerator Gone()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
