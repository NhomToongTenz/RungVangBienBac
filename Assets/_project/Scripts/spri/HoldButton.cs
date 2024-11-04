using UnityEngine;
using UnityEngine.UI;

public class HoldButton : MonoBehaviour
{
    public GameObject holdButtonUI; // Đối tượng UI của nút giữ
    public float holdTime = 3f; // Thời gian cần giữ (3 giây)
    private float holdCounter = 0f; // Bộ đếm thời gian giữ
    private bool isHolding = false; // Kiểm tra xem người chơi đang giữ nút hay không

    public void ShowHoldButton()
    {
        holdButtonUI.SetActive(true); // Hiển thị nút giữ
        isHolding = true;
        holdCounter = 0f; // Đặt lại bộ đếm giữ nút
    }

    void Update()
    {
        // Kiểm tra xem người chơi có đang giữ nút không
        if (isHolding && Input.GetButton("Fire1")) // "Fire1" là nút mặc định cho chuột trái hoặc chạm
        {
            holdCounter += Time.deltaTime; // Tăng bộ đếm thời gian giữ

            // Nếu người chơi giữ đủ thời gian, gọi hàm kết thúc giữ
            if (holdCounter >= holdTime)
            {
                ReleaseHold();
            }
        }
        else if (isHolding && !Input.GetButton("Fire1"))
        {
            holdCounter = 0f; // Đặt lại bộ đếm nếu người chơi ngừng giữ
        }
    }

    private void ReleaseHold()
    {
        holdButtonUI.SetActive(false); // Ẩn nút giữ
        isHolding = false;

        // Tìm Boat và kích hoạt lại di chuyển
        GameObject boat = GameObject.FindGameObjectWithTag("Boat");
        if (boat != null)
        {
            boat.GetComponent<BoatController>().ResumeBoat(); // Tiếp tục di chuyển thuyền
        }
    }
}
