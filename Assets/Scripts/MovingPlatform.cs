using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

    private int movingRight = 1;
    private float speed = 2f;
    private float distanceToMove = 8;
    private float distanceMoved = 0;

    void Update() {
        transform.Translate(new Vector3(Time.deltaTime * speed * movingRight, 0, 0));
        distanceMoved += Time.deltaTime * speed;
        if (distanceMoved >= distanceToMove) {
            movingRight *= -1;
            distanceMoved = 0;
        }
    }

    void OnTriggerEnter(Collider other) {
		Debug.Log("On Platform");
		other.SendMessage("OnPlatform", transform, SendMessageOptions.DontRequireReceiver);
    }

    void OnTriggerExit(Collider other) {
		Debug.Log ("Off Platform");
		other.SendMessage("OffPlatform", transform, SendMessageOptions.DontRequireReceiver);
    }
}
