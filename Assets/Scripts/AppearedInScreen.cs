using UnityEngine;
using System.Collections;

public class AppearedInScreen : MonoBehaviour {
	
	void OnTriggerEnter(Collider other) {
		Debug.Log ("Trigger enter " + other.name);
		if (other.name.Equals ("ItemPicture(Clone)") || other.name.Equals ("ItemPicture Bad(Clone)")) {
			Transform getChild = other.transform.FindChild("Sprite");
			GameObject child = getChild.gameObject;
			Debug.Log ("Object "+child.GetComponent<SpriteRenderer>().sprite.name+" appeared.");
		}
	}
}
