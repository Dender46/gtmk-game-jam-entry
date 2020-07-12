using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour {

	CharacterController2D movement;
	PlayerInputActions playerActions;
	Animator animator;
	[Range(0.0f, 5.0f)] [SerializeField] float colliderRadius = 0.5f;

	public float speed = 130f;

	float horizontalMove;
	bool jump	= false;
	bool canMove = true;
	bool canDrink = true;

	void Awake() {
		movement = GetComponent<CharacterController2D>();
		playerActions = new PlayerInputActions();
		animator = transform.Find("Texture").GetComponent<Animator>();

		playerActions.Player.Move.performed += ctx => horizontalMove = ctx.ReadValue<Vector2>().x;
		playerActions.Player.Jump.performed += ctx => jump = true;
		playerActions.Player.Use.performed += ctx => StartCoroutine(Use());
	}
	
	void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, colliderRadius);
    }

	IEnumerator Use() {
		if (!canDrink)
			yield break;

		Collider2D hitCollider = Physics2D.OverlapCircle(transform.position, colliderRadius, LayerMask.GetMask("Drinks"), -0.5f, 0.5f);
		if (hitCollider) {
			DrinkScript script = hitCollider.gameObject.GetComponent<DrinkScript>();
			script.Use();
			
			canMove = false;
			canDrink = false;
			animator.SetBool("Drinking", true);
			
			yield return new WaitForSeconds(1.0f);
			
			canMove = true;
			canDrink = true;
			animator.SetBool("Drinking", false);
		}
	}

	void FixedUpdate() {
		if (!canMove) {
			movement.Move (0.0f, false, jump);
			return;
		}

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
