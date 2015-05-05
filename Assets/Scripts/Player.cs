using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public float moveSpeed = 5f;
	public float rotationSpeed = 720f; //360f;

	public int charaDirection = 0;

	CharacterController characterController;
	Animator animator;

	void Start ()
	{
		characterController = GetComponent<CharacterController>();
		animator = GetComponentInChildren<Animator>();
	}

	void Update ()
	{
		float vertical = Input.GetAxis("Vertical");
		float horizontal = Input.GetAxis("Horizontal");
		Vector3 keyDirection = new Vector3(0, 0, vertical);

		// 向きを変える
		if (horizontal != 0) {
			Vector3 north = new Vector3(0, 0, 1.0f);
			Vector3 east = new Vector3(1.0f, 0, 0);
			Vector3 south = new Vector3(0, 0, -1.0f);
			Vector3 west = new Vector3(-1.0f, 0, 0);
			Vector3 direction;

			if (charaDirection >= 0 && charaDirection < 90) {
				direction = new	Vector3(
				  charaDirection / 90.0f,
					0,
					1.0f - (charaDirection / 90.0f)
				);
			} else if (charaDirection >= 90 && charaDirection < 180) {
				int tempDirection = charaDirection - 90;
				direction = new Vector3(
				  1.0f - (tempDirection / 90.0f),
					0,
					(-1) * (tempDirection / 90.0f)
				);
			} else if (charaDirection >= 180 && charaDirection < 270) {
				int tempDirection = charaDirection - 180;
				direction = new Vector3(
				  (-1) * (tempDirection / 90.0f),
					0,
					-1.0f + (tempDirection / 90.0f)
				);
			} else {
				int tempDirection = charaDirection - 270;
				direction = new Vector3(
				  -1.0f + (tempDirection / 90.0f),
					0,
					tempDirection / 90.0f
				);
			}

			Vector3 forward = Vector3.Slerp(
				transform.forward,
				direction,
				rotationSpeed * Time.deltaTime / Vector3.Angle(transform.forward, direction)
			);
			transform.LookAt(transform.position + forward);

			if (horizontal > 0) {
				charaDirection++;
			} else if (horizontal < 0) {
				charaDirection--;
			}

			if (charaDirection > 360) {
				charaDirection = 0;
			} else if (charaDirection < 0) {
				charaDirection = 360;
			}
		}

		// 移動
		characterController.Move((vertical * transform.forward) * moveSpeed * Time.deltaTime);

		animator.SetFloat("Speed", characterController.velocity.magnitude);

		// TODO: Win
	}

	void OnTriggerEnter (Collider other)
	{
		// TODO: Lose
	}
}
