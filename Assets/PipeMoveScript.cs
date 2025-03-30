using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{
    public float moveSpeed = 0f;  // Induláskor nulla
    public float maxSpeed = 5f;   // Végsõ sebesség
    public float acceleration = 2f; // Gyorsulás mértéke
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
