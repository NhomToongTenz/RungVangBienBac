using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceBoat : MonoBehaviour
{
    public float speed = 5f; // Tốc độ di chuyển sang phải

    void Update()
    {
        // Di chuyển đối tượng sang phải với tốc độ cố định
        transform.position += Vector3.right * speed * Time.deltaTime;
    }
}
