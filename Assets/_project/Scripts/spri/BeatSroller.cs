using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float beatTempo;
    public float startDelay = 5f; // Thời gian đếm ngược trước khi bắt đầu di chuyển
    private bool hasStarted = false; // Biến kiểm tra trạng thái bắt đầu

    // Use this for initialization
    void Start()
    {
        beatTempo = beatTempo / 60f;
    }

    // Update is called once per frame
    void Update()
    {
        // Nếu chưa bắt đầu di chuyển, thực hiện đếm ngược
        if (!hasStarted)
        {
            if (startDelay > 0)
            {
                startDelay -= Time.deltaTime; // Giảm thời gian đếm ngược theo thời gian thực
            }
            else
            {
                hasStarted = true; // Khi hết thời gian đếm ngược, bắt đầu di chuyển
            }
        }
        else
        {
            // Di chuyển mũi tên sang phải khi đã hết thời gian đếm ngược
            transform.position += new Vector3(beatTempo * Time.deltaTime, 0f, 0f);
        }
    }
}
