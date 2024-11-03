using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private bool unlocked;
    public Image unlockImage;
    public int levelNumber;

    private void Start()
    {
        UpdateLevelStatus();
        UpdateLevelImage();
    }

    private void UpdateLevelStatus()
    {
        if (levelNumber == 1)
        {
            unlocked = true;
        }
        else
        {
            int previousLevel = levelNumber - 1;
            unlocked = PlayerPrefs.GetInt("Level_" + previousLevel) == 1;
        }
    }

    private void UpdateLevelImage()
    {
        unlockImage.gameObject.SetActive(!unlocked);
    }

    public void PressSelection(string levelName)
    {
        if (unlocked)
        {
            SceneManager.LoadScene(levelName);
        }
    }
}
