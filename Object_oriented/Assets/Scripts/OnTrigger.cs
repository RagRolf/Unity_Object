using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrigger : MonoBehaviour
{
    public IInterfaceA[] attack;
    List<int> allIndices = new List<int>(5);
    private void OnEnable()
    {
        StartCoroutine(Die());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
         allIndices.Add(other.gameObject.name[0] - ' ');
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        allIndices.Remove(other.gameObject.name[0] - ' ');
    }
    private IEnumerator Die()
    {
        yield return new WaitForSeconds(3f);
        for(int i = 0; i <  allIndices.Count; i++)
        {
            attack[allIndices[i]].IsHit();
        }
        allIndices.Clear();
        transform.parent.gameObject.SetActive(false);
    }
}
