using UnityEngine;

public class songdichuyen : MonoBehaviour
{
    public GameObject notificationPanel; // Tham chiếu đến UI thông báo

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boat")) // Kiểm tra nếu đối tượng chạm vào là Boat
        {
             // Dừng chuyển động của thuyền
            ShowNotification(); // Gọi hàm hiển thị thông báo
        }
    }

    private void ShowNotification()
    {
        notificationPanel.SetActive(true); // Hiện thông báo
        Invoke("HideNotification", 1f); // Gọi hàm ẩn thông báo sau 1 giây
    }

    private void HideNotification()
    {
        notificationPanel.SetActive(false); // Ẩn thông báo
    }
}
