using System.Collections;
using UnityEngine;

public class PipeSpawnerScript : MonoBehaviour
{
    public GameObject pipe; // A pipe prefab
    public float spawnRate = 2f; // A spawn idõköz
    public float timer = 0f; // Az idõmérõ
    public float heightOffset = 10f; // Függõleges távolság a csövek között

    void Start()
    {
        spawnPipe(); // Elindítjuk az elsõ csövet
    }

    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime; // Növeli az idõmérõt
        }
        else
        {
            spawnPipe(); // Új csõ létrehozása
            timer = 0f; // Idõmérõ újraindítása
        }
    }

    void spawnPipe()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        GameObject newPipe = Instantiate(pipe, new Vector3(transform.position.x + 5, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);

        // Debug log, hogy megnézd, milyen csövet generálunk
        Debug.Log("Új Pipe generálva: " + newPipe.name);

        // Elindítjuk a coroutine-t, hogy a csõ beússzon a képernyõre
        StartCoroutine(MovePipeIn(newPipe));
    }


    IEnumerator MovePipeIn(GameObject pipe)
    {
        // A csõ cél X pozíciója a képernyõ bal szélén kívül
        float targetX = -Camera.main.aspect * Camera.main.orthographicSize - 5f; // A csõ balra mozduljon el
        float startX = Camera.main.aspect * Camera.main.orthographicSize + 5f;  // A csõ indulási helye a jobb oldalon kívülrõl
        float duration = 5f; // Beúszási idõ
        float elapsedTime = 0f;

        // Az interpolációval mozgathatjuk a csövet az indulási helyrõl a célhelyre
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newX = Mathf.Lerp(startX, targetX, elapsedTime / duration);
            pipe.transform.position = new Vector3(newX, pipe.transform.position.y, 0);
            yield return null; // Várakozás a következõ frissítésig
        }

        // Miután a csõ elhagyta a képernyõt, eltûnik
        Destroy(pipe, 5f); // A csõ 5 másodperc múlva eltûnik a memóriából
    }
}
