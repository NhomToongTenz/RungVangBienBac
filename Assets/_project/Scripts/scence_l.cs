using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scence_l : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        // Check if the object entering the trigger is the player (identified by tag "Player")
        if (other.CompareTag("Player"))
        {
            // Get the current scene index and calculate the next scene index
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;

            // Check if the next scene index is within the range of scenes in the Build Settings
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                Debug.Log("No more scenes to load!");
            }
        }
    }
}
