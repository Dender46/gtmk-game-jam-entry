using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour {

	PlayerInputActions playerActions;
	[Range(0.0f, 5.0f)] [SerializeField] private float colliderRadius = 0.5f;

	public CharacterController2D movement;
	public float speed = 130f;

	float horizontalMove;
	bool jump	= false;

	void Awake() {
		playerActions = new PlayerInputActions();
		playerActions.Player.Move.performed += ctx => horizontalMove = ctx.ReadValue<Vector2>().x;
		playerActions.Player.Jump.performed += ctx => jump = true;
		playerActions.Player.Use.performed += ctx => Use();
	}
	
	void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, colliderRadius);
    }

	void Use() {
		Collider2D hitCollider = Physics2D.OverlapCircle(transform.position, colliderRadius);
		Debug.Log("Found: " + hitCollider);
	}

	void FixedUpdate() {
		movement.Move (horizontalMove * speed * Time.fixedDeltaTime, false, jump);
		jump = false;
	}

	// Important for some reason
	private void OnEnable() {
		playerActions.Enable();
	}

	private void OnDisable() {
		playerActions.Disable();
	}
}
