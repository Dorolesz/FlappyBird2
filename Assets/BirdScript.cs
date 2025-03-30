using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength;  // Állítható ugrási erõ
    public LogicScript logic;
    public bool birdIsAlive = true;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();

        if (myRigidbody == null)  // Ha nincs hozzárendelve, próbáljuk megkeresni
        {
            myRigidbody = GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
        if (!birdIsAlive) return;

        // Ha a space billentyût lenyomják, akkor a madár felugrik
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody.velocity = Vector2.up * flapStrength;
        }

        // Ha a madár kimegy a képernyõ tetején vagy alján
        if (transform.position.y > Camera.main.orthographicSize || transform.position.y < -Camera.main.orthographicSize)
        {
            GameOver();
        }
    }

    // Használjunk OnCollisionEnter2D-t az ütközések kezelésére
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Ütközés történt: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Pipe"))
        {
            GameOver();
        }
    }



    // Játék vége, madár halott
    private void GameOver()
    {
        logic.gameOver();
        birdIsAlive = false;
        myRigidbody.velocity = Vector2.zero;  // Megállítja a madár mozgását
    }
}
