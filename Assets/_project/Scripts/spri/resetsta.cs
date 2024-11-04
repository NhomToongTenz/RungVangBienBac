using UnityEngine;
using UnityEngine.SceneManagement;

public class resetsta : MonoBehaviour
{
    // Hàm này sẽ được gọi khi button được nhấn
    public void ResetCurrentScene()
    {
        // Lấy tên của scene hiện tại
        string sceneName = SceneManager.GetActiveScene().name;
        // Khởi động lại scene bằng cách tải lại nó
        SceneManager.LoadScene(sceneName);
    }
}
