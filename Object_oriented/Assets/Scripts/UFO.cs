using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class UFO : MonoBehaviour
{
    private GameObject[] allBombs;
    [SerializeField] private Transform targetParent;
    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject healthDrop;
    [SerializeField] private InputAction dropBomb;
    [SerializeField] private InputAction move;
    private static float border = 11.5f, offset = 0.95f;
    private static int amountOfBombs = 5, ufoSpeed = 3;
    private int index;
    private float counter;

    private void OnEnable()
    {
        dropBomb.Enable();
        move.Enable();
    }

    private void OnDisable()
    {
        dropBomb.Disable();
        move.Disable();
    }
    void Start()
    {
        StartCoroutine(DropIt());
        healthDrop = Instantiate(healthDrop);
        healthDrop.transform.GetChild(0).GetComponent<HealthDrop>().attack = targetParent.GetComponentsInChildren<IInterfaceA>();
        int index = 0;
        dropBomb.canceled += DropBomb_canceled;
        while (index < amountOfBombs)
        {
            Array.Resize(ref allBombs, ++index);
            GameObject obj = Instantiate(bomb);
            OnTrigger trigger = obj.transform.GetChild(0).GetComponent<OnTrigger>();
            trigger.attack = targetParent.GetComponentsInChildren<IInterfaceA>();
            allBombs[index - 1] = obj;
        }
        for(int i = 0; i < targetParent.childCount; i++)
        {
            targetParent.GetChild(i).name = (char)(' ' + i) + targetParent.GetChild(i).name;
        }
    }

    private void DropBomb_canceled(InputAction.CallbackContext obj)
    {
        counter = 0;
    }

    void Update()
    {
        float movement = move.ReadValue<float>();
        if (transform.position.x > border || transform.position.x < -border)
            transform.position = new Vector3(-transform.position.x * offset, transform.position.y, 0);
        transform.Translate(new Vector3(movement * Time.deltaTime * ufoSpeed, 0, 0));
        if (dropBomb.IsPressed())
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

    private IEnumerator DropIt()
    {
        while(true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(10, 15));
            healthDrop.SetActive(true);
            healthDrop.transform.position = transform.position - new Vector3(0, 0.8f);
        }
    }
}
