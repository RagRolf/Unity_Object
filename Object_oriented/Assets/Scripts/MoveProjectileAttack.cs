using UnityEngine;

public class MoveProjectileAttack : MonoBehaviour
{
    void Update()
    {
        transform.Translate(2.5f * Time.deltaTime * Vector3.up);
    }
}
