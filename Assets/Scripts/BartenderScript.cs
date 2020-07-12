using UnityEngine;
using System.Collections;

public class BartenderScript : MonoBehaviour
{
    public GameObject[] drinkPrefabs;

    private Vector3 velocity = Vector3.zero;
    private float smoothness = 0.2f;
    private float posX;
    private GameData gameData;

    private bool bartenderStartedServing = false;

    void Start() {
        posX = transform.position.x;
        gameData = GameObject.Find("GameData").GetComponent<GameData>();

        if (gameData.gameHasStarted)
            StartCoroutine(ServeDrink());
    }

    private IEnumerator ServeDrink() {
        float waitFor = Mathf.Clamp(2.0f - gameData.playerAlcoholLevel * 0.0015f, 0.5f, 2.0f);
        Debug.Log("Waiting " + waitFor);
        yield return new WaitForSeconds(waitFor);
        
        posX = Random.Range(5.0f, 15.0f);
        transform.position = new Vector3(posX, -6.0f, transform.position.z);
        //Start the coroutine we define below named ExampleCoroutine.
        StartCoroutine(GenerateDrink());
    }

    void Update() {
        // Waiting for a player to drink first drink
        if (gameData.gameHasStarted && !bartenderStartedServing) {
            StartCoroutine(ServeDrink());
            bartenderStartedServing = true;
        }

        transform.position = Vector3.SmoothDamp(
            transform.position,
            new Vector3(posX, -2.0f, transform.position.z),
            ref velocity,
            smoothness
        );
    }

    private IEnumerator GenerateDrink()
    {
        yield return new WaitForSeconds(0.4f);
    
        GameObject newDrink = (GameObject)Instantiate(drinkPrefabs[Random.Range(0, drinkPrefabs.Length)], transform.position, Quaternion.identity);
        newDrink.GetComponent<Rigidbody2D>().velocity += new Vector2(Random.Range(-4.0f, 4.0f), 10.0f);
        StartCoroutine(ServeDrink());
    }
}
