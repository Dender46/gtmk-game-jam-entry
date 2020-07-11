using UnityEngine;

public class DrinkScript : MonoBehaviour
{
    public float alcoholPower;
    public float alcoholGravity;

    void Awake() {
        Physics2D.IgnoreCollision(GameObject.Find("Player").GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    public void Use() {
        Destroy(gameObject);
    }
}
