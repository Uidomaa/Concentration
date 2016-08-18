using UnityEngine;
using System.Collections;

public class RotatorScript : MonoBehaviour {
	
	// Rotate the light because lol
	void Update () {
        transform.Rotate(Vector3.up * Time.deltaTime * 10f, Space.World);
	}
}
