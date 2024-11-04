using UnityEngine;

public class Wave : MonoBehaviour
{
    private float speed;

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime); // Move wave from left to right
    }

    public void SetSpeed(float waveSpeed)
    {
        speed = waveSpeed;
    }

 
}
