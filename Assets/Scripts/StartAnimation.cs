using UnityEngine;
using System.Collections;
using System.IO;

public class StartAnimation : MonoBehaviour {

	public int speed;

	private GameObject camera;
	private GameObject startButton;
	private PersistentController persistent;
	private Vector3 goalPosition = new Vector3(0.0f,15.0f,-12.0f);
	private bool clicked = false;

	// Use this for initialization
	void Start () {
		camera = GameObject.Find ("Main Camera");
		startButton = GameObject.Find ("Start Button");

		GameObject persistentObject = GameObject.FindGameObjectWithTag("Persistent");
		if (persistentObject != null)
		{
			persistent = persistentObject.GetComponent<PersistentController>();
		}
		if (persistent == null)
		{
			Debug.Log("Cannot find 'Persistent Controller' script");
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (clicked)
			AnimationDown ();
	}

	void OnMouseDown() {
		if (camera.transform.position == goalPosition) {
			persistent.createFile();
			Application.LoadLevel(1);
		} else {
			clicked = true;
			Transform getChild = startButton.transform.FindChild ("label");
			GameObject child = getChild.gameObject;
			child.GetComponent<TextMesh> ().text = "Submit Code";
		}
	}

	void AnimationDown(){	
		camera.transform.position = Vector3.Lerp (camera.transform.position, goalPosition, (Time.deltaTime * speed));
	}
}
