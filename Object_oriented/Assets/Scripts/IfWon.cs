using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IfWon : MonoBehaviour
{
    [SerializeField] private GameObject winUI;
    private AudioSource winSource;
    public static IfWon won { get; private set; }
    public bool playerWon { get; private set; }

    private void Start()
    {
        won = this;
        winSource = winUI.GetComponent<AudioSource>();
    }

    public void StartChangeScene()
    {
        StartCoroutine(ChangeScene());
    }
    private IEnumerator ChangeScene()
    {
        playerWon = true;
        winUI.SetActive(true);
        winSource.Play();
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene(0);
    }
}
