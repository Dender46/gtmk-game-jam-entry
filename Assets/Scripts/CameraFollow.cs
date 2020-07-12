using UnityEngine;

public class CameraFollow : MonoBehaviour {

	private Transform target;
	private Vector3 velocity = Vector3.zero;
	private GameData gameData;

	public Vector3 offset;
	public float smoothSpeed = 0.6f;
	public float drunkSwayness;

	private void Start() {
		target = GameObject.Find("Player").transform;
		gameData = GameObject.Find("GameData").GetComponent<GameData>();

		// Fixes jittery movement of player
		GameObject.Find("Player").GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.Interpolate;
	}

	void Update () {
		float drunkLevel = gameData.playerAlcoholLevel * drunkSwayness;
		float randomX = Random.Range(-drunkLevel, drunkLevel);
		float randomY = Random.Range(-drunkLevel, drunkLevel);
		float randomZ = Random.Range(-drunkLevel, drunkLevel);
		Vector3 drunkOffset = new Vector3(randomX, randomY, randomZ);
		Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, target.position + offset + drunkOffset, ref velocity, smoothSpeed);
		transform.position = smoothedPosition;
	}
}
