using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour {

	public int speed;

	// Update is called once per frame
	void Update () {
		if(gameObject.name.Equals ("Player"))
			transform.Rotate (new Vector3 (0, 90, 0) * Time.deltaTime * speed);
		else
			transform.Rotate (new Vector3 (0, 0, 90) * Time.deltaTime * speed);
	}
}
