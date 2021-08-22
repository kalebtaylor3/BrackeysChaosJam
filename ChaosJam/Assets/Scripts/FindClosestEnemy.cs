using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosestEnemy : MonoBehaviour
{

    public float whereToStop;
    public float speed;

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
                currentEnemy.transform.LookAt(closestEnemy.transform);
            }
        }

        if (Vector2.Distance(transform.position, closestEnemy.transform.position) > whereToStop)
        {
            
            transform.position = Vector2.MoveTowards(transform.position, closestEnemy.transform.position, speed * Time.deltaTime);
        }

        Debug.DrawLine(this.transform.position, closestEnemy.transform.position);
    }
}
