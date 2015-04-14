using UnityEngine;
using System.Collections;

public class QuitOnClick : MonoBehaviour {

	private LevelEditor leveleditor;
	
	void Start(){
		GameObject editorOnlyObject = GameObject.FindGameObjectWithTag ("LevelEditor");
		if (editorOnlyObject != null){
			leveleditor = editorOnlyObject.GetComponent <LevelEditor>();
		}
		
		if (leveleditor == null){
			Debug.Log ("Cannot find 'Level Editor' script");
		}
	}

	void OnMouseDown() {
		leveleditor.QuitLevel ();
	}
}
