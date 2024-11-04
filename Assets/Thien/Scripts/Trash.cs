using UnityEngine;

public class Trash : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Notify TrashManager
            FindObjectOfType<TrashManager>().CollectTrash();
            Destroy(gameObject);
        }
    }
}
