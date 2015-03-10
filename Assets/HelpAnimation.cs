using UnityEngine;
using System.Collections;

public class HelpAnimation : MonoBehaviour {

	public GameObject Details;
	public float speed;

	private GameObject Player;
	private bool helpDown = false;
	private bool helpUp = false;
	private bool clicked = false;
	private Vector3 PlayerPosition;
	private Vector3 OriginalPosition;
	private Vector3 Offset = new Vector3 (0, 0, 9.5f);

	void Start(){		
		Player = GameObject.FindWithTag ("Player");
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

	}

	void OnMouseDown() {
		if (!clicked) {
				Transform getChild = Details.transform.FindChild("Button");
				getChild = getChild.transform.FindChild("Text");
				GameObject child = getChild.gameObject;
				child.GetComponent<TextMesh>().text = "Back";
			Player.SetActive (false);
			helpDown = true;
			helpUp = false;
			clicked = true;
		} else {
				Transform getChild = Details.transform.FindChild("Button");
				getChild = getChild.transform.FindChild("Text");
				GameObject child = getChild.gameObject;
				child.GetComponent<TextMesh>().text = "Help";
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

	IEnumerator activePlayer()
	{
		yield return new WaitForSeconds(1.0f);
		Player.SetActive(true);
	}

}
