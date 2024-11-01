using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu; // UI tạm dừng
    [SerializeField] GameObject trapMenu; // UI khi chạm bẫy
    [SerializeField] GameObject successMenu; // UI khi hoàn thành màn chơi

    void Start()
    {
        pauseMenu.SetActive(false);
        trapMenu.SetActive(false);
        successMenu.SetActive(false);
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; // Tạm dừng game
    }

    public void Home()
    {
        Time.timeScale = 1f; // Khôi phục thời gian
        SceneManager.LoadScene("Level_UI"); // Quay về scene chọn cấp độ
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; // Tiếp tục game
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Khởi động lại màn hiện tại
    }

    // Phương thức hiển thị UI chạm bẫy
    public void ShowTrapMenu()
    {
        trapMenu.SetActive(true);
        Time.timeScale = 0f; // Tạm dừng game
    }

    // Phương thức hiển thị UI qua màn
    public void ShowSuccessMenu()
    {
        successMenu.SetActive(true);
        Time.timeScale = 0f; // Tạm dừng game
    }


    // Nút tiếp tục tới màn tiếp theo từ success menu
    public void OnNextLevelButtonClicked()
    {
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextLevelIndex); // Tải scene màn tiếp theo
    }
}
