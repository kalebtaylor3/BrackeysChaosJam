using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    private float health;
    private float healthMax;

    public HealthSystem(int healthMax)
    {
        this.healthMax = healthMax;
        health = healthMax;
        
    }
    
    public float GetHealth()
    {
        return health;
    }

    public float GetHealthPercent()
    {
        return (float)health / healthMax;
    }

    public void Damage(float damageAmount)
    {
        health -= damageAmount;
        if (health < 0) health = 0;
    }

    public void RepairHealth()
    {
        health = healthMax;
    }

    
}
