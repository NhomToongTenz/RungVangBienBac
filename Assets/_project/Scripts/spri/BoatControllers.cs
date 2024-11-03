using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float speed = 1f; // Tốc độ ban đầu của BoatController
    private bool isStopped = false; // Trạng thái dừng của thuyền

    // Hàm này để tăng tốc độ
    public void IncreaseSpeed(float amount)
    {
        speed += amount;
    }

    // Hàm này để dừng thuyền
    public void StopBoat()
    {
        isStopped = true;
    }

    // Hàm này để tiếp tục di chuyển thuyền
    public void ResumeBoat()
    {
        isStopped = false;
    }

    void Update()
    {
        // Di chuyển thuyền nếu không dừng
        if (!isStopped)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }
}
