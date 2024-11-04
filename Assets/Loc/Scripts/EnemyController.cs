using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2.0f; // Tốc độ di chuyển của nhân vật
    public Transform[] movePoints; // Các điểm để di chuyển qua lại

    private int currentPointIndex = 0;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (animator != null)
        {
            animator.SetBool("IsMoving", true); // Bật animation khi bắt đầu
        }
    }


    void Update()
    {
        // Di chuyển về phía điểm hiện tại
        transform.position = Vector2.MoveTowards(transform.position, movePoints[currentPointIndex].position, speed * Time.deltaTime);

        // Xoay mặt nhân vật theo hướng di chuyển
        if (transform.position.x < movePoints[currentPointIndex].position.x)
        {
            transform.localScale = new Vector3(1, 1, 1); // Hướng về bên phải
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1); // Hướng về bên trái
        }

        // Nếu đến gần điểm, thì chuyển sang điểm tiếp theo
        if (Vector2.Distance(transform.position, movePoints[currentPointIndex].position) < 0.1f)
        {
            currentPointIndex = (currentPointIndex + 1) % movePoints.Length;
        }
    }
}
