using UnityEngine;

public class DrinkScript : MonoBehaviour
{
    public float alcoholPower;
    public float alcoholGravity;

    private GameData gameData;

    void Awake() {
        Physics2D.IgnoreCollision(GameObject.Find("Player").GetComponent<Collider2D>(), GetComponent<Collider2D>());
        gameData = GameObject.Find("GameData").GetComponent<GameData>();
    }

    public void Use() {
        Destroy(gameObject);
        gameData.playerAlcoholLevel += alcoholPower;
    }
}
