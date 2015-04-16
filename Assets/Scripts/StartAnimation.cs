using UnityEngine;
using System.Collections;
using System.IO;

public class StartAnimation : MonoBehaviour {

	private GameObject warnLabel;
	private GameObject cam;
	private GameObject startButton;
	private PersistentController persistent;
	private Vector3 goalPosition = new Vector3(0.0f,15.0f,-4.0f);
	private bool clicked = false;
	private bool shake = false;
	
	// Use this for initialization
	void Start () {
		cam = GameObject.Find ("Main Camera");
		startButton = GameObject.Find ("Start Button");
		warnLabel = GameObject.Find ("InputCanvas").transform.FindChild ("WarnLabel").gameObject;
		
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
		if (shake)
			shakeWarning ();
		if (Input.GetKeyDown ("enter")) {

			if (cam.transform.position == goalPosition) {
				if(persistent.getSessionName().Length < 3){
					warnLabel.SetActive(true);
					shake = true;
					StartCoroutine(shakeFalse());			
				} else {
					persistent.createFile();
					Application.LoadLevel(1);
				}
			} else {
				clicked = true;
				Transform getChild = startButton.transform.FindChild ("label");
				GameObject child = getChild.gameObject;
				child.GetComponent<TextMesh> ().text = "Submit Code";
				TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, true, true);
			}

		}
	}

	
	void OnMouseDown() {
		if (cam.transform.position == goalPosition) {
			if(persistent.getSessionName().Length < 3){
				warnLabel.SetActive(true);
				shake = true;
				StartCoroutine(shakeFalse());			
			} else {
				persistent.createFile();
				Application.LoadLevel(1);
			}
		} else {
			clicked = true;
			Transform getChild = startButton.transform.FindChild ("label");
			GameObject child = getChild.gameObject;
			child.GetComponent<TextMesh> ().text = "Submit Code";
			TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, true, true);
		}
	}

	IEnumerator shakeFalse()
	{
		yield return new WaitForSeconds(1.0f);
		warnLabel.transform.position = new Vector3(0,-45f,-29f);
		shake = false;
	}

	void shakeWarning(){
		warnLabel.transform.position = new Vector3(Mathf.Sin(Time.time * 25),-45f,-29f);
	}

	void AnimationDown(){	
		cam.transform.position = Vector3.Lerp (cam.transform.position, goalPosition, (Time.deltaTime * 5));
	}
}