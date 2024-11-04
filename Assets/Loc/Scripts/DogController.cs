using UnityEngine;

public class DogController : MonoBehaviour
{
    public float speed = 2.0f; // Tốc độ di chuyển của con chó
    public float waitTime = 2.0f; // Thời gian chờ giữa các trạng thái
    public Transform[] movePoints; // Các điểm để di chuyển qua lại

    private int currentPointIndex = 0;
    private Animator animator;
    private bool isMoving = false;
    private float waitCounter;

    void Start()
    {
        animator = GetComponent<Animator>();
        waitCounter = waitTime;
    }

    void Update()
    {
        if (isMoving)
        {
            // Di chuyển về phía điểm hiện tại
            transform.position = Vector2.MoveTowards(transform.position, movePoints[currentPointIndex].position, speed * Time.deltaTime);

            // Xoay mặt con chó theo hướng di chuyển
            if (transform.position.x < movePoints[currentPointIndex].position.x)
            {
                transform.localScale = new Vector3(1, 1, 1); // Hướng về bên phải
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1); // Hướng về bên trái
            }

            // Nếu đến gần điểm, thì chờ và thực hiện hành vi khác
            if (Vector2.Distance(transform.position, movePoints[currentPointIndex].position) < 0.1f)
            {
                isMoving = false;
                animator.SetTrigger("Idle");
                waitCounter = waitTime;
                currentPointIndex = (currentPointIndex + 1) % movePoints.Length;
            }
        }
        else
        {
            // Đếm ngược thời gian chờ
            waitCounter -= Time.deltaTime;
            if (waitCounter <= 0)
            {
                // Ngẫu nhiên thực hiện hành vi "ngửi" hoặc bắt đầu chạy
                if (Random.value < 0.5f)
                {
                    animator.SetTrigger("Sniff");
                    waitCounter = waitTime; // Chờ thêm trước khi di chuyển
                }
                else
                {
                    isMoving = true;
                    animator.SetTrigger("Run");
                }
            }
        }
    }
}
