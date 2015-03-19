using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour {
	
	public int level;
	
	void OnMouseDown() {
			Application.LoadLevel (level);
	}

}
