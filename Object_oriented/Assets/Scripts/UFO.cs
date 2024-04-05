using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UFO : MonoBehaviour
{
    private GameObject[] allBombs;
    [SerializeField] private Transform targetParent;
    [SerializeField] private GameObject bomb;
    [SerializeField] private InputAction action;
    private static int amountOfBombs = 6;
    private int index;
    private float counter;

    private void OnEnable()
    {
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
    }
    void Start()
    {
        int index = 0;
        while (index < amountOfBombs)
        {
            Array.Resize(ref allBombs, ++index);
            allBombs[index - 1] = Instantiate(bomb, transform);
        }
        List<GameObject> transformList = new List<GameObject>();
        for(int i = 0; i < transform.childCount; i++)
        {
            GameObject obj = transform.GetChild(i).gameObject;
            OnTrigger trigger = obj.transform.GetChild(0).GetComponent<OnTrigger>();
            trigger.targetParent = targetParent;
            trigger.canBeAttacked = targetParent.GetComponentsInChildren<CanBeAttacked>();
            transformList.Add(obj);
        }
        allBombs = transformList.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (action.IsPressed())
        {
            counter += Time.deltaTime;
            if (counter > 1f)
            {
                counter = 0f;
                allBombs[index].transform.position = transform.position - new Vector3(0, 0.8f);
                allBombs[index++].SetActive(true);
                if (index == amountOfBombs)
                    index = 0;
            }
        }
    }
}
