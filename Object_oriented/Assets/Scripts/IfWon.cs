using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IfWon : MonoBehaviour
{
    [SerializeField] private GameObject winUI;
    [HideInInspector] public int StoreBuildings, AmountOfBuildings;
    public bool playerWon { get; private set; }

    public void StartChangeScene()
    {
        StartCoroutine(ChangeScene());
    }
    private IEnumerator ChangeScene()
    {
        playerWon = true;
        winUI.SetActive(true);
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene(0);
    }
}
