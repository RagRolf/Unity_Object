using System;
using UnityEngine;

public class Attack : Destructable, IInterfaceA //inheritance 
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private int bombAmount = 5;
    protected GameObject[] projectiles;
    private void Start()
    {
        int amount = 0;
        while (amount < bombAmount)
        {
            Array.Resize(ref projectiles, ++amount);
        }
    }
    public void IsHit()
    {
        Debug.Log(name + "got hit!");
    }

    public void HealthAid()
    {
        Debug.Log(name + "got hit!");
    }
    public override void MakeStronger()
    {

    }
}
