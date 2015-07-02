using UnityEngine;
using System.Collections;

public class WarriorAnimationDemo : MonoBehaviour {

	public Animator animator;

	float rotationSpeed = 30;
	Vector3 inputVec;
	bool isMoving;

	private DrawPool DPool;

	//Warrior types
	public enum Warrior{Karate, Ninja, Brute, Sorceress};

	public Warrior warrior;

	void Start () {
		animator = GetComponent<Animator> ();
		DPool = GetComponent<DrawPool> ();
	}

	void Update()
	{
		if (!DPool.isStunned) {
			//Get input from controls
			float x = -(Input.GetAxisRaw ("Horizontal"));
			float z = -(Input.GetAxisRaw ("Vertical"));
			inputVec = new Vector3 (x, 0, z);

			//Apply inputs to animator
			animator.SetFloat ("Input X", z);
			animator.SetFloat ("Input Z", -(x));

			if (x != 0 || z != 0) {  //if there is some input
				//set that character is moving
				animator.SetBool ("Moving", true);
				isMoving = true;
				animator.SetBool ("Running", true);
			} else {
				//character is not moving
				animator.SetBool ("Moving", false);
				animator.SetBool ("Running", false);
				isMoving = false;
			}
			UpdateMovement ();  //update character position and facing
		}
	}
		
	void RotateTowardsMovementDir()  //face character along input direction
	{
		if (inputVec != Vector3.zero)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(inputVec), Time.deltaTime * rotationSpeed);
		}
	}

	float UpdateMovement()
	{
		Vector3 motion = inputVec;  //get movement input from controls

		//reduce input for diagonal movement
		motion *= (Mathf.Abs(inputVec.x) == 1 && Mathf.Abs(inputVec.z) == 1)?.7f:1;
		
		RotateTowardsMovementDir();  //if not strafing, face character along input direction

		return inputVec.magnitude;  //return a movement value for the animator, not currently used
	}

}