using UnityEngine;
using System.Collections;

public class AppearedInScreen : MonoBehaviour {

	private PersistentController persistent;

	void Start(){		
		GameObject persistentObject = GameObject.FindGameObjectWithTag ("Persistent");
		if (persistentObject != null) {
			persistent = persistentObject.GetComponent<PersistentController> ();
		}
		if (persistent == null) {
			Debug.Log ("Cannot find 'Persistent Controller' script");
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.name.Equals ("ItemPicture(Clone)") || other.name.Equals ("ItemPicture Bad(Clone)")) {
			Transform getChild = other.transform.FindChild("Sprite");
			GameObject child = getChild.gameObject;
			Debug.Log (persistent.getTime()+ " appear "+ child.GetComponent<SpriteRenderer>().sprite.name);
			persistent.AddLevelLog("\r\n"+persistent.getTime()+" appear "+child.GetComponent<SpriteRenderer>().sprite.name);
		}
	}

	void OnTriggerExit(Collider other){
		if (other.name.Equals ("ItemPicture(Clone)") || other.name.Equals ("ItemPicture Bad(Clone)")) {
			Transform getChild = other.transform.FindChild("Sprite");
			GameObject child = getChild.gameObject;
			Debug.Log (persistent.getTime()+" disappear "+child.GetComponent<SpriteRenderer>().sprite.name);
			persistent.AddLevelLog("\r\n"+persistent.getTime()+" disappear "+child.GetComponent<SpriteRenderer>().sprite.name);
		}
	}
}
