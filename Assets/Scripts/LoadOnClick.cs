using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour {
	
	public int level;
	private GameController gamecontroller;
	
	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gamecontroller = gameControllerObject.GetComponent <GameController>();
		}
		if (gamecontroller == null)
		{
			Debug.Log ("Cannot find 'gameController' script");
		}
	}
	
	void OnMouseDown() {
		if (Application.loadedLevel == 0)
			gamecontroller.setActualLevel (level);
		else 
			Application.LoadLevel (gamecontroller.actualLevel + 2);
	}

}
