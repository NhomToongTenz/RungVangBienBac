using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float scrollSpeed = 2f; // Tốc độ cuộn
    public float backgroundWidth;  // Chiều rộng của background

    private Vector3 startPosition;

    void Start()
    {
        // Ghi lại vị trí ban đầu của background
        startPosition = transform.position;
    }

    void Update()
    {
        // Tính toán vị trí mới dựa trên thời gian và tốc độ cuộn
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, backgroundWidth);
        transform.position = startPosition + Vector3.left * newPosition;
    }
}
