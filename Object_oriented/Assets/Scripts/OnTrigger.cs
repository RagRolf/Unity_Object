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
        Debug.Log("Enter " + other.gameObject.name);
        if (other.CompareTag("Mother"))
            allIndices.Add(other.gameObject.name[0] - ' ');
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Exit " + other.gameObject.name);
        if (other.CompareTag("Mother"))
            allIndices.Remove(other.gameObject.name[0] - ' ');
    }
    private IEnumerator Die()
    {
        yield return new WaitForSeconds(3f);
        for(int i = 0; i <  allIndices.Count; i++)
        {
            attack[allIndices[i]].IsHit();
            allIndices = new List<int>(5);
        }
        transform.parent.gameObject.SetActive(false);
    }
}
