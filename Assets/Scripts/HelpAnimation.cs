using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class HelpAnimation : MonoBehaviour {
	
	public GameObject Details;
	public float speed;
	
	private PersistentController persistent;
	private LevelEditor leveleditor;
	private GameObject Canvas;
	private List<GameObject> Pictures = new List<GameObject>();
	private GameObject Player;
	private GameObject ButtonText;
	private GameObject ButtonQuit;
	private Transform getChild;
	private bool helpDown = false;
	private bool helpUp = false;
	private bool clicked = true;
	private Vector3 PlayerPosition;
	private Vector3 Offset = new Vector3 (0, 1.0f, 8.6f);
	
	void Start(){		
		GameObject persistentObject = GameObject.FindGameObjectWithTag("Persistent");
		if (persistentObject != null)
		{
			persistent = persistentObject.GetComponent<PersistentController>();
		}
		if (persistent == null)
		{
			Debug.Log("Cannot find 'Persistent Controller' script");
		}

		GameObject editorOnlyObject = GameObject.FindGameObjectWithTag ("LevelEditor");
		if (editorOnlyObject != null){
			leveleditor = editorOnlyObject.GetComponent <LevelEditor>();
		}
		
		if (leveleditor == null){
			Debug.Log ("Cannot find 'Level Editor' script");
		}
		
		Player = GameObject.FindWithTag ("Player");
		Canvas = GameObject.Find ("Canvas");
		
		foreach (GameObject picture in GameObject.FindGameObjectsWithTag("ItemPicture")) {
			Pictures.Add(picture);
		}
		
		getChild = Details.transform.FindChild("Button");
		getChild = getChild.transform.FindChild("Text");
		ButtonText = getChild.gameObject;

		getChild = Details.transform.FindChild("Quit");
		ButtonQuit = getChild.gameObject;

		DeactivateAll();
		Details.transform.position = PlayerPosition;	
		
		Debug.Log(persistent.getTime()+" help open");
		//File.AppendAllText (persistent.getFileName(), "\r\n"+persistent.getTime()+" help open");
		persistent.AddLevelLog("\r\n"+persistent.getTime()+" help open");
	}
	
	void Update(){
		if (Player.activeSelf)
			PlayerPosition = Player.transform.position;
		
		if (helpDown) 
			AnimationDown ();
		
		if (helpUp)
			AnimationUp ();

		if(leveleditor.getScore() == 3)
			LevelComplete();

		/**
		foreach (GameObject picture in Pictures) {
			if(!picture.activeSelf)
				break;
			getChild = picture.transform.FindChild("Sprite");
			SpriteRenderer sprite = getChild.gameObject.GetComponent<SpriteRenderer>();
			if(sprite.isVisible)
				Debug.Log("SPRITE VISIBLE "+sprite.sprite.name);
		}
		*/
	}
	
	void OnMouseDown() {
		if (!clicked) {		
			Debug.Log(persistent.getTime()+" help open");
			//File.AppendAllText (persistent.getFileName(),"\r\n"+persistent.getTime()+" help open");
			persistent.AddLevelLog("\r\n"+persistent.getTime()+" help open");

			ButtonText.GetComponent<TextMesh>().text = "Back";
			DeactivateAll();
			helpDown = true;
			helpUp = false;
			clicked = true;
		} else {				
			Debug.Log(persistent.getTime()+"help close");
			//File.AppendAllText (persistent.getFileName(), "\r\n"+persistent.getTime()+" help close");
			persistent.AddLevelLog("\r\n"+persistent.getTime()+" help close");

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
	
	IEnumerator activeAll()
	{
		yield return new WaitForSeconds(1.0f);
		Player.SetActive (true);
		Canvas.SetActive (true);
		ButtonQuit.SetActive (true);
		foreach (GameObject picture in Pictures) {
			if(picture != null)
				picture.SetActive(true);
		}
	}
	
	void DeactivateAll(){
		Player.SetActive (false);
		Canvas.SetActive (false);
		ButtonQuit.SetActive (false);
		foreach (GameObject picture in Pictures) {
			if(picture != null)
				picture.SetActive(false);
		}
	}

	void LevelComplete(){
		Canvas.SetActive (false);
		Details.SetActive (false);
		ButtonQuit.SetActive (false);
		foreach (GameObject picture in Pictures) {
			if(picture != null)
				picture.SetActive(false);
		}
	}
	
}