using UnityEngine;

public abstract class Destructable : MonoBehaviour
{
    public abstract void MakeStronger();
    public virtual void SelfDestruction()
    {
        
    }
}
