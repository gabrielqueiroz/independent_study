using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour {

	private PersistentController persistent;
	public int level;

	void Start(){		
		GameObject persistentObject = GameObject.FindGameObjectWithTag ("Persistent");
		if (persistentObject != null) {
			persistent = persistentObject.GetComponent<PersistentController> ();
		}
		if (persistent == null) {
			Debug.Log ("Cannot find 'Persistent Controller' script");
		}
	}

	void OnMouseDown() {
		if (!persistent.checkIfComplete(level-2))
			Application.LoadLevel (level);
		else
			Application.LoadLevel (1);
	}

}
