using UnityEngine;
using System.Collections;

public class BartenderScript : MonoBehaviour
{
    public GameObject[] drinkPrefabs;

    private Vector3 velocity = Vector3.zero;
    private float smoothness = 0.2f;
    private float posX;

    private void Start() {
        posX = transform.position.x;
    }

    public void ServeDrink() {
        posX = Random.Range(3.0f, 15.0f);
        transform.position = new Vector3(posX, -6.0f, transform.position.z);
        //Start the coroutine we define below named ExampleCoroutine.
        StartCoroutine(GenerateDrink());
    }

    void Update() {
        transform.position = Vector3.SmoothDamp(
            transform.position,
            new Vector3(posX, -2.0f, transform.position.z),
            ref velocity,
            smoothness
        );
    }

    IEnumerator GenerateDrink()
    {
        yield return new WaitForSeconds(0.4f);
    
        GameObject newDrink = (GameObject)Instantiate(drinkPrefabs[Random.Range(0, drinkPrefabs.Length)], transform.position, Quaternion.identity);
        newDrink.GetComponent<Rigidbody2D>().velocity += new Vector2(Random.Range(-1.0f, 1.0f), 10.0f);
    }
}
