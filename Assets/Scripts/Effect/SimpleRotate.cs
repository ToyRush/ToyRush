using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveAxisState{

	X,
	Y,
	Z

}

public class SimpleRotate : MonoBehaviour {
	public float RotateSpeed = 0.0f;
	public MoveAxisState Axis;
	// Update is called once per frame

	void Update () {
		if (Axis == MoveAxisState.X) {
			transform.Rotate(Vector3.right * Time.deltaTime * RotateSpeed);
		}

		if (Axis == MoveAxisState.Y) {
			transform.Rotate(Vector3.up * Time.deltaTime * RotateSpeed);
		}

		if (Axis == MoveAxisState.Z) {
			transform.Rotate (Vector3.forward * Time.deltaTime * RotateSpeed);
		} 
	}
}