using System.Collections;
using UnityEngine;

public class PipeSpawnerScript : MonoBehaviour
{
    public GameObject pipe; // A pipe prefab
    public float spawnRate = 2f; // A spawn id�k�z
    public float timer = 0f; // Az id�m�r�
    public float heightOffset = 10f; // F�gg�leges t�vols�g a cs�vek k�z�tt

    void Start()
    {
        spawnPipe(); // Elind�tjuk az els� cs�vet
    }

    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime; // N�veli az id�m�r�t
        }
        else
        {
            spawnPipe(); // �j cs� l�trehoz�sa
            timer = 0f; // Id�m�r� �jraind�t�sa
        }
    }

    void spawnPipe()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        GameObject newPipe = Instantiate(pipe, new Vector3(transform.position.x + 5, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);

        // Debug log, hogy megn�zd, milyen cs�vet gener�lunk
        Debug.Log("�j Pipe gener�lva: " + newPipe.name);

        // Elind�tjuk a coroutine-t, hogy a cs� be�sszon a k�perny�re
        StartCoroutine(MovePipeIn(newPipe));
    }


    IEnumerator MovePipeIn(GameObject pipe)
    {
        // A cs� c�l X poz�ci�ja a k�perny� bal sz�l�n k�v�l
        float targetX = -Camera.main.aspect * Camera.main.orthographicSize - 5f; // A cs� balra mozduljon el
        float startX = Camera.main.aspect * Camera.main.orthographicSize + 5f;  // A cs� indul�si helye a jobb oldalon k�v�lr�l
        float duration = 5f; // Be�sz�si id�
        float elapsedTime = 0f;

        // Az interpol�ci�val mozgathatjuk a cs�vet az indul�si helyr�l a c�lhelyre
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newX = Mathf.Lerp(startX, targetX, elapsedTime / duration);
            pipe.transform.position = new Vector3(newX, pipe.transform.position.y, 0);
            yield return null; // V�rakoz�s a k�vetkez� friss�t�sig
        }

        // Miut�n a cs� elhagyta a k�perny�t, elt�nik
        Destroy(pipe, 5f); // A cs� 5 m�sodperc m�lva elt�nik a mem�ri�b�l
    }
}
