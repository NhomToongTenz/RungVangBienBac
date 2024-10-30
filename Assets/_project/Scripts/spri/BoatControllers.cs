using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float speed = 1f; // Tốc độ ban đầu của BoatController

    // Hàm này để tăng tốc độ
    public void IncreaseSpeed(float amount)
    {
        speed += amount;
    }

    // Ví dụ di chuyển sang phải dựa trên tốc độ
    void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }
}
