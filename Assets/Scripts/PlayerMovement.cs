using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    /*
     * TO DO:
     * Movement
     *      Should use the CharacterController which is already attached to this GameObject
     *      Be able to move left and right
     *      Collide with/be stopped by walls
     *      Not move too quickly or slowly
     *          Remember that movement happens every frame
     * Jumping/Falling
     *      Fall to the ground, and not through it
     *      Able to jump
     *      Can reach platforms to the right, but not the one on the left
     *      Only able to jump while standing on the ground
     * Input
     *      Ideally, use the KeyboardInput script which is already attached to this GameObject
     *      A & D for left and right movement
     *      Space for jumping
     * Moving Platform
     *      When standing on the platform, the character should stay on it/move relative to the moving platform
     *      When not standing on the platform, revert to normal behavior
     * Enemy
     *      If the character touches the enemy, he should "die"
     *      
     * 
     * 
     * 
     * Variables you might want:
     *      References to the CharacterController and Keyboard input classes
     *      Speed values for moving, falling, and jumping
     *      A value to keep track of the player's movement speed and direction
     *      You will probably need to use the Update function as well as create functions for moving platforms and enemies
     */

	public float speed = 5f;
	public float gravity = 25f;
	public float jumpSpeed = 15f;
	//public GameObject enemy;

	private float movement;
	private Vector3 vertDirection = Vector3.zero;
	private bool onPlatform = false;
	private Vector3 diff;
	private Vector3 platPosition;
	//private CapsuleCollider playerCollider = this.GetComponent<CapsuleCollider>();


	void Update(){
		// Left/Right Movement
		KeyboardInput keyboardInput = GetComponent<KeyboardInput> ();
		movement = keyboardInput.XAxis;
		transform.Translate(new Vector3(Time.deltaTime * speed * movement, 0, 0));

		//Up/Down Movement
		CharacterController controller = GetComponent<CharacterController> ();
		if (controller.isGrounded) {
			vertDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			if (keyboardInput.JumpButtonPressed)
				vertDirection.y = jumpSpeed;
		}
		vertDirection.y -= gravity * Time.deltaTime;
		controller.Move(vertDirection * Time.deltaTime);

		//*******NOTE*********
		//I was unable to get Moving platform to fully work, but I hope you can follow my logic of what I was going for


		/*if (onPlatform) {
			transform.Translate(platPosition + diff);
		}*/
	}

	//Collision With Enemy
	void OnControllerColliderHit (ControllerColliderHit hit){
		//Debug.Log ("Collision");
		if (hit.gameObject.name == "Enemy") {
			Application.LoadLevel(0);
			Debug.Log ("Collision with enemy");
		}
	}

	void OnPlatform(Transform platTransform){
		onPlatform = true;
		Debug.Log("On Platform is True!");
		platPosition = platTransform.transform.position;
		diff = transform.position - platPosition;
	}

	void OffPlatform(Transform platTransform){
		onPlatform = false;
		Debug.Log ("On Platform is False!");
	}

}