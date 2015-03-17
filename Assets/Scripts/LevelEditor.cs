using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class LevelEditor : MonoBehaviour {
	
	public GameObject itemWord;
	public GameObject itemWord_bad;
	public GameObject itemPicture;
	public GameObject itemPicture_bad;
	public GameObject Canvas;
	public GameObject Details;
	public GameObject Notification;

	private GameObject Player;

	public Scrollbar progressBar;
	public int score =0;
	public int life;
	
	void Start (){
		Player = GameObject.FindWithTag ("Player");

		if (Application.loadedLevel == 2)
			LoadLevel1 ();	
		if (Application.loadedLevel == 3)
			LoadLevel2 ();	

		//Level Test
		if (Application.loadedLevel == 4)
			LoadLevel1 ();
	}
	
	void Update(){

		if(Player.activeSelf)
			HelpUpdate ();

		if (life == 0)
			Application.LoadLevel(1);

		if (score == 3)
			StartCoroutine (LevelComplete());

		if (Input.GetKeyDown(KeyCode.Escape))
			Application.LoadLevel(0);
	}

	public void AddScore(){
		score = score + 1;
		progressBar.size = score / 3f;
	}

	public void DecScore(){
		Transform getChild = Canvas.transform.FindChild("Heart_"+life);
		GameObject child = getChild.gameObject;
		child.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load <Sprite> ("Sprites/DeadHeart");
		life--;
	}

	public void HelpUpdate(){
			Details.transform.position = Player.transform.position + new Vector3 (0, 1.0f, 8.6f);
	}

	IEnumerator LevelComplete()
	{
		while (true) {
			Notification.SetActive(true);
			yield return new WaitForSeconds(3.0f);
			Application.LoadLevel(0);
		}
	}
	
	void LoadLevel1(){

		Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
		levelObject.Add ("Cat_1", randomPosition());
		levelObject.Add ("Cat_2", randomPosition());
		levelObject.Add ("Cat_3", randomPosition());

		foreach (KeyValuePair<string, Vector3> pair in levelObject){			
			Vector3 position = pair.Value;
			Quaternion rotation = Quaternion.identity;
			Transform getChild = itemPicture.transform.FindChild("Sprite");
			GameObject child = getChild.gameObject;
			child.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("LevelContentUpdates/"+pair.Key);			
			Instantiate (itemPicture, position, rotation);
		}

		Dictionary<string, Vector3> levelObject_wrong = randomWrong (3, "cat");

		foreach (KeyValuePair<string, Vector3> pair in levelObject_wrong){			
			Vector3 position = pair.Value;
			Quaternion rotation = Quaternion.identity;
			Transform getChild = itemPicture_bad.transform.FindChild("Sprite");
			GameObject child = getChild.gameObject;
			child.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("LevelContentUpdates/"+pair.Key);			
			Instantiate (itemPicture_bad, position, rotation);
		}
	}

	void LoadLevel2(){
		
		Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
		levelObject.Add ("frog_1", randomPosition());
		levelObject.Add ("frog_2", randomPosition());
		levelObject.Add ("frog_3", randomPosition());
		levelObject.Add ("frog_4", randomPosition());
		levelObject.Add ("frog_5", randomPosition());
		levelObject.Add ("frog_6", randomPosition());
		
		foreach (KeyValuePair<string, Vector3> pair in levelObject){			
			Vector3 position = pair.Value;
			Quaternion rotation = Quaternion.identity;
			Transform getChild = itemPicture.transform.FindChild("Sprite");
			GameObject child = getChild.gameObject;
			child.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("LevelContentUpdates/"+pair.Key);			
			Instantiate (itemPicture, position, rotation);
		}
		
		Dictionary<string, Vector3> levelObject_wrong = randomWrong (6, "frog");
		
		foreach (KeyValuePair<string, Vector3> pair in levelObject_wrong){			
			Vector3 position = pair.Value;
			Quaternion rotation = Quaternion.identity;
			Transform getChild = itemPicture_bad.transform.FindChild("Sprite");
			GameObject child = getChild.gameObject;
			child.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("LevelContentUpdates/"+pair.Key);			
			Instantiate (itemPicture_bad, position, rotation);
		}
	}

	private Dictionary<string, Vector3> randomWrong(int length, string levelWord){
		Dictionary<string, Vector3> randomWrong = new Dictionary<string, Vector3>();
		List<string> allObjects = levelContent ();

		for (int i = 0; i < length; i++) {
			string current = allObjects [Random.Range (0,allObjects.Count)];

			while (current.Contains(levelWord) || randomWrong.ContainsKey(current))
				current = allObjects [Random.Range (0, allObjects.Count)];

			randomWrong.Add ( current , randomPosition());
		}
			
		return randomWrong;
	}

	List<string> levelContent(){
		List<string> levelContent = new List<string>();
		DirectoryInfo dir = new DirectoryInfo("Assets/Resources/LevelContentUpdates");
		FileInfo[] info = dir.GetFiles ("*.png");
		foreach (FileInfo f in info) {
			string temp = f.Name.Remove(f.Name.Length - 4);
			levelContent.Add(temp);
		}
		return levelContent;
	}

	Vector3 randomPosition(){
		float x = Random.Range (-15, 1);
		float z = Random.Range (1, 15);

		return new Vector3 (x, 0.0f, z);
	}

	/**
	void LoadWords(){
		//Load Good Words
		words.Add ("Word1", new Vector3 (0,0,5));
		words.Add ("Word3", new Vector3 (5,0,10));
		words.Add ("Word5", new Vector3 (-5,0,10));
		
		foreach (KeyValuePair<string, Vector3> pair in words){
			
			Vector3 position = pair.Value;
			Quaternion rotation = Quaternion.identity;
			Transform getChild = itemWord.transform.FindChild("Word");
			GameObject child = getChild.gameObject;
			child.GetComponent<TextMesh>().text = pair.Key;
			Instantiate (itemWord, position, rotation);
		}
		
		//Load Bad Words
		words_bad.Add ("Word2", new Vector3 (-5,0,5));
		words_bad.Add ("Word4", new Vector3 (0,0,10));
		words_bad.Add ("Word6", new Vector3 (5,0,5));
		
		foreach (KeyValuePair<string, Vector3> pair in words_bad){
			
			Vector3 position = pair.Value;
			Quaternion rotation = Quaternion.identity;
			Transform getChild = itemWord_bad.transform.FindChild("Word");
			GameObject child = getChild.gameObject;
			child.GetComponent<TextMesh>().text = pair.Key;
			Instantiate (itemWord_bad, position, rotation);
		}
	}
	*/
}
