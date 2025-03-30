using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{
    public float moveSpeed = 0f;  // Indul�skor nulla
    public float maxSpeed = 5f;   // V�gs� sebess�g
    public float acceleration = 2f; // Gyorsul�s m�rt�ke
    public float deadZone = -45;

    void Update()
    {
        if (moveSpeed < maxSpeed)
        {
            moveSpeed += acceleration * Time.deltaTime;
        }

        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }


}
