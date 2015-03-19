using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HelpAnimation : MonoBehaviour {

	public GameObject Details;
	public float speed;

	private GameObject Canvas;
	private List<GameObject> Pictures = new List<GameObject>();
	private GameObject Player;
	private GameObject ButtonText;
	private GameObject Counter;
	private Transform getChild;
	private bool helpDown = false;
	private bool helpUp = false;
	private bool clicked = true;
	private Vector3 PlayerPosition;
	private Vector3 Offset = new Vector3 (0, 1.0f, 8.6f);

	void Start(){		
		Player = GameObject.FindWithTag ("Player");
		Canvas = GameObject.Find ("Canvas");

		foreach (GameObject picture in GameObject.FindGameObjectsWithTag("ItemPicture")) {
			Pictures.Add(picture);
		}

		getChild = Details.transform.FindChild("Button");
		getChild = getChild.transform.FindChild("Text");
		ButtonText = getChild.gameObject;

		getChild = Details.transform.FindChild("Counter");
		Counter = getChild.gameObject;

		DeactivateAll();
		Details.transform.position = PlayerPosition;	
	}

	void Update(){
		if (Player.activeSelf)
			PlayerPosition = Player.transform.position;
	
		if (helpDown)
			AnimationDown ();

		if (helpUp)
			AnimationUp ();

		//if (clicked)
		//	StartCoroutine (countPerSecond());
	}

	void OnMouseDown() {
		if (!clicked) {				
			ButtonText.GetComponent<TextMesh>().text = "Back";
			DeactivateAll();
			helpDown = true;
			helpUp = false;
			clicked = true;
		} else {				
			ButtonText.GetComponent<TextMesh>().text = "Help";
			helpUp = true;
			helpDown = false;
			clicked = false;
			StartCoroutine(activeAll());
		}
	}

	void AnimationDown(){	
		Details.transform.position = Vector3.Lerp (Details.transform.position, PlayerPosition, (Time.deltaTime * speed));
	}

	void AnimationUp(){
		Details.transform.position = Vector3.Lerp (Details.transform.position, PlayerPosition + Offset, (Time.deltaTime * speed));
	}

	IEnumerator countPerSecond(){
		yield return new WaitForSeconds (5.0f);
		Counter.GetComponent<TextMesh> ().text = Time.time.ToString();
	}

	IEnumerator activeAll()
	{
		yield return new WaitForSeconds(1.0f);
		Player.SetActive (true);
		Canvas.SetActive (true);
		foreach (GameObject picture in Pictures) {
			if(picture != null)
				picture.SetActive(true);
		}
	}

	void DeactivateAll(){
		Player.SetActive (false);
		Canvas.SetActive (false);
		foreach (GameObject picture in Pictures) {
			if(picture != null)
				picture.SetActive(false);
		}
	}
	
}
