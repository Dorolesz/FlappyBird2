using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength;  // �ll�that� ugr�si er�
    public LogicScript logic;
    public bool birdIsAlive = true;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();

        if (myRigidbody == null)  // Ha nincs hozz�rendelve, pr�b�ljuk megkeresni
        {
            myRigidbody = GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
        if (!birdIsAlive) return;

        // Ha a space billenty�t lenyomj�k, akkor a mad�r felugrik
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody.velocity = Vector2.up * flapStrength;
        }

        // Ha a mad�r kimegy a k�perny� tetej�n vagy alj�n
        if (transform.position.y > Camera.main.orthographicSize || transform.position.y < -Camera.main.orthographicSize)
        {
            GameOver();
        }
    }

    // Haszn�ljunk OnCollisionEnter2D-t az �tk�z�sek kezel�s�re
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("�tk�z�s t�rt�nt: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Pipe"))
        {
            GameOver();
        }
    }



    // J�t�k v�ge, mad�r halott
    private void GameOver()
    {
        logic.gameOver();
        birdIsAlive = false;
        myRigidbody.velocity = Vector2.zero;  // Meg�ll�tja a mad�r mozg�s�t
    }
}
