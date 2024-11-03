using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public HoldButton holdButton; // Tham chiếu đến HoldButton

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boat")) // Kiểm tra nếu đối tượng chạm vào là Boat
        {
            holdButton.ShowHoldButton(); // Hiển thị nút giữ
            other.GetComponent<BoatController>().StopBoat(); // Dừng chuyển động của thuyền
        }
    }
}
