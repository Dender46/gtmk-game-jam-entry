using UnityEngine;

public class CameraFollow : MonoBehaviour {

	private Transform target;
	private Vector3 velocity = Vector3.zero;

	public Vector3 offset;
	public float smoothSpeed = 0.3f;

	private void Start() {
		target = GameObject.Find("Player").transform;
		// Fixes jittery movement of player
		GameObject.Find("Player").GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.Interpolate;
	}

	void Update () {
		Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, target.position + offset, ref velocity, smoothSpeed);
		transform.position = smoothedPosition;
	}
}
