using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public void ChangeLevel()
    {
        DataPersistance.own.SetName();
        SceneManager.LoadScene(1);
    }
}
