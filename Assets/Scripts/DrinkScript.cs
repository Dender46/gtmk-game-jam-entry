using UnityEngine;

public class DrinkScript : MonoBehaviour
{
    public float alcoholPower;
    public float alcoholGravity;

    public void Use() {
        Destroy(gameObject);
    }
}
