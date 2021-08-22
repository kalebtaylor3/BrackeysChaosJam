using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosestEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FindClosest();
    }

    void FindClosest()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        Player closestEnemy = null;
        Player[] allEnemies = GameObject.FindObjectsOfType<Player>();

        foreach (Player currentEnemy in allEnemies)
        {
            float distancetoEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if (distancetoEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distancetoEnemy;
                closestEnemy = currentEnemy;
            }
        }

        Debug.DrawLine(this.transform.position, closestEnemy.transform.position);
    }
}
