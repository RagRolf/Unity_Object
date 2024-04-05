using System;
using UnityEngine;

public abstract class Destructable : MonoBehaviour
{
    [SerializeField] protected GameObject projectile;
    protected GameObject[] projectiles;
    [SerializeField] protected int projectileAmount;
    [SerializeField] protected int lives;
    public abstract void Upgrade();

    protected void InitializeProjectiles()
    {
        int amount = 0;
        while (amount < projectileAmount)
        {
            Array.Resize(ref projectiles, ++amount);
            projectiles[amount] = Instantiate(projectile);
        }
    }
    public virtual void Destruction()
    {
        for(int i = 0; i < projectiles.Length; i++)
        {
            projectiles[i].SetActive(true);
            Destroy(gameObject);
        }
    }
}
