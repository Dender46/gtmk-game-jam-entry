using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour {

	CharacterController2D movement;
	PlayerInputActions playerActions;
	Animator animator;
	GameData gameData;
	private float colliderRadius = 0.5f;

	public float speed = 130f;

	float horizontalMove;
	bool jump	= false;
	bool canMove = true;
	bool canDrink = true;

	void Awake() {
		movement = GetComponent<CharacterController2D>();
		playerActions = new PlayerInputActions();
		animator = transform.Find("Texture").GetComponent<Animator>();
		gameData = GameObject.Find("GameData").GetComponent<GameData>();

		playerActions.Player.Move.performed += ctx => horizontalMove = ctx.ReadValue<Vector2>().x;
		playerActions.Player.Jump.performed += ctx => jump = true;
		playerActions.Player.Use.performed += ctx => StartCoroutine(Use());
	}

	IEnumerator Use() {
		if (!canDrink)
			yield break;

		Collider2D hitCollider = Physics2D.OverlapCircle(transform.position, colliderRadius, LayerMask.GetMask("Drinks"), -0.5f, 0.5f);
		if (hitCollider) {
			SoundManagerScript.PlaySound("gulp");
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
			movement.Move (0.0f, false, jump); // disable sliding on floor
			return;
		}

		movement.Move(horizontalMove * Random.Range(Mathf.Clamp(speed - gameData.playerAlcoholLevel * 0.3f, 0.0f, speed), speed) * Time.fixedDeltaTime, false, jump);
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
