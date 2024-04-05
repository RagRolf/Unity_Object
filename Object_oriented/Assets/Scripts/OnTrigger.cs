using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrigger : MonoBehaviour
{
    [HideInInspector] public CanBeAttacked[] canBeAttacked;
    [HideInInspector] public Transform targetParent;
    List<int> allIndices = new List<int>(5);
    private void OnEnable()
    {
        StartCoroutine(Die());
    }

    private void OnTriggerEnter(Collider other)
    {
        allIndices.Add(other.gameObject.name[0] - ' ');
    }

    private void OnTriggerExit(Collider other)
    {
        allIndices.Remove(other.gameObject.name[0] - ' ');
    }
    private IEnumerator Die()
    {
        yield return new WaitForSeconds(3f);
        for(int i = 0; i <  allIndices.Count; i++)
        {
            canBeAttacked[allIndices[i]].IsHit();
            allIndices = new List<int>(5);
        }
        transform.parent.gameObject.SetActive(false);
    }
}
