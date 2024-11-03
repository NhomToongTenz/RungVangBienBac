using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public GameObject endGameUI; // UI sẽ hiển thị khi kết thúc game

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boat")) // Kiểm tra xem đối tượng chạm có phải là thuyền không
        {
            EndGame(); // Gọi hàm kết thúc game
        }
    }

    private void EndGame()
    {
        // Hiển thị UI kết thúc game
        endGameUI.SetActive(true);

        // Tạm dừng thuyền hoặc dừng game (nếu cần)
        Time.timeScale = 0f; // Dừng mọi chuyển động trong game
    }
}
