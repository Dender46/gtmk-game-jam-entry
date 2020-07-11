﻿using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour {

	CharacterController2D movement;
	PlayerInputActions playerActions;
	[Range(0.0f, 5.0f)] [SerializeField] float colliderRadius = 0.5f;

	public float speed = 130f;

	float horizontalMove;
	bool jump	= false;

	void Awake() {
		movement = GetComponent<CharacterController2D>();
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
		if (hitCollider.name == "Drink") {
			DrinkScript script = hitCollider.gameObject.GetComponent<DrinkScript>();
			script.Use();
		}
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
