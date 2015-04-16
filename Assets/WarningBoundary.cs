using UnityEngine;
using System.Collections;

public class WarningBoundary : MonoBehaviour {

	private GameObject warning;

	void Start(){
		warning = GameObject.Find ("Warning");
		warning.SetActive(false);
	}

	void OnTriggerEnter(Collider other){
		if(other.tag.Equals("Boundary"))
			warning.SetActive (true);
	}

	void OnTriggerExit(Collider other){
		warning.SetActive (false);
	}
}
