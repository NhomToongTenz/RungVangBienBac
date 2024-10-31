using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void OpenLevel(int levelId)
    {
        string levelName = "Level " + levelId;
        SceneManager.LoadScene(levelName);
    }
}
