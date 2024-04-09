using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewScene : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ChangeScene);
    }
    private void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }
}
