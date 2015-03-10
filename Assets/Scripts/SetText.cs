using UnityEngine;
using System.Collections;

public class SetText : MonoBehaviour {

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
			Debug.Log ("Cannot find 'Level Editor' script");
		}
	}
	
	void Update () {
		gameObject.GetComponent<TextMesh>().text = gamecontroller.details[gamecontroller.actualLevel];
	}
}
