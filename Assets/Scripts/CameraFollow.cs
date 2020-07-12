	using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	private Transform target;
	private Vector3 velocity = Vector3.zero;
	private Vector3 rvelocity = Vector3.zero;
	private GameData gameData;

	public Vector3 offset;
	public float smoothSpeed = 0.6f;
	
	public float drunkSwayness;
	public float drunkOffsetUpdate = 1.0f;

	private Vector3 drunkOffset = Vector3.zero;
	private Vector3 drunkRotation = Vector3.zero;

	private void Start() {
		target = GameObject.Find("Player").transform;
		gameData = GameObject.Find("GameData").GetComponent<GameData>();

		// Fixes jittery movement of player
		GameObject.Find("Player").GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.Interpolate;
		StartCoroutine(UpdateDrunkOffset());
	}

	void Update () {
		Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, target.position + offset + drunkOffset, ref velocity, smoothSpeed);
		transform.position = smoothedPosition;

		Vector3 smoothedRotation = Vector3.SmoothDamp(transform.eulerAngles, transform.eulerAngles + drunkRotation, ref rvelocity, smoothSpeed);
		transform.eulerAngles = smoothedRotation;
	}

	IEnumerator UpdateDrunkOffset() {
		yield return new WaitForSeconds(drunkOffsetUpdate);

		float drunkLevel = gameData.playerAlcoholLevel * drunkSwayness;
		float randomX = Random.Range(-drunkLevel, drunkLevel);
		float randomY = Random.Range(-drunkLevel, drunkLevel);
		float randomZ = Random.Range(-drunkLevel, drunkLevel);
		drunkOffset = new Vector3(randomX, randomY, randomZ);

		drunkRotation = new Vector3(0.0f, 0.0f, Random.Range(-drunkLevel, drunkLevel));

		StartCoroutine(UpdateDrunkOffset());
	}
}
