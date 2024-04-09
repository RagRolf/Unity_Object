using System.Collections;
using UnityEngine;

public class MoveProjectileDeath : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DestroySelf());
    }
    void Update()
    {
        transform.Translate(3f * Time.deltaTime * Vector3.up);
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
