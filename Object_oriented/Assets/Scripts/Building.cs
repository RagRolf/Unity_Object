using UnityEngine;

public class Building : Destructable, IInterfaceA //inheritance 
{
    private void Start()
    {
        InitializeProjectiles();
    }
    public void IsHit()
    {
        Debug.Log(name + "got hit!");
    }

    public void HealthAid()
    {
        Debug.Log(name + "got hit!");
    }

    public override void Destruction()
    {
        base.Destruction();
    }
    public override void Upgrade()
    {

    }
}
