using UnityEngine;
using System.Collections;

public class HelpAnimation : MonoBehaviour {

	public GameObject Details;
	public float speed;
	
	private GameObject Player;
	private GameObject ButtonText;
	private GameObject Counter;
	private Transform getChild;
	private bool helpDown = false;
	private bool helpUp = false;
	private bool clicked = false;
	private Vector3 PlayerPosition;
	private Vector3 OriginalPosition;
	private Vector3 Offset = new Vector3 (0, 1.0f, 9.0f);

	void Start(){		
		Player = GameObject.FindWithTag ("Player");

		getChild = Details.transform.FindChild("Button");
		getChild = getChild.transform.FindChild("Text");
		ButtonText = getChild.gameObject;

		getChild = Details.transform.FindChild("Counter");
		Counter = getChild.gameObject;
	}

	void Update(){
		if (Player.activeSelf) {
			PlayerPosition = Player.transform.position;
			OriginalPosition = Details.transform.position;
		} 

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
			Player.SetActive (false);
			helpDown = true;
			helpUp = false;
			clicked = true;
		} else {				
			ButtonText.GetComponent<TextMesh>().text = "Help";
			helpUp = true;
			helpDown = false;
			clicked = false;
			StartCoroutine(activePlayer());
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

	IEnumerator activePlayer()
	{
		yield return new WaitForSeconds(1.0f);
		Player.SetActive(true);
	}

}
