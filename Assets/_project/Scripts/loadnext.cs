using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadNextScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextScene()
    {
        int curScene = SceneManager.GetActiveScene().buildIndex;
        int nextScecne = curScene + 1;

        if (nextScecne < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextScecne);
        else
            Debug.Log("nah load");
    }
}
