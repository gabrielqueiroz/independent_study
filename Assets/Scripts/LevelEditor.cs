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
	public GameObject Player;

	public Scrollbar progressBar;
	public int score =0;
	public int life;
	
	void Start (){
		if (Application.loadedLevel == 3)
			LoadLevel1 ();	
		if (Application.loadedLevel == 4)
			LoadLevel2 ();	
	}
	
	void Update(){

		if(Player.activeSelf)
			HelpUpdate ();

		if (life == 0)
			Application.LoadLevel(2);

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
		Details.transform.position = Player.transform.position + new Vector3 (0, 0, 9.5f);
	}

	IEnumerator LevelComplete()
	{
		while (true) {
			Details.SetActive(true);
			Transform getChild = Details.transform.FindChild("Text");
			GameObject child = getChild.gameObject;
			child.GetComponent<UnityEngine.UI.Text>().text = "Congratulations!";
			yield return new WaitForSeconds(3.0f);
			Application.LoadLevel(0);
		}
	}
	
	void LoadLevel1(){

		Dictionary<string, Vector3> level1 = new Dictionary<string, Vector3>();
		level1.Add ("Cat_1", new Vector3 (Random.Range(-15,15), 0, Random.Range(-15,15)));
		level1.Add ("Cat_2", new Vector3 (Random.Range(-15,15), 0, Random.Range(-15,15)));
		level1.Add ("Cat_3", new Vector3 (Random.Range(-15,15), 0, Random.Range(-15,15)));

		foreach (KeyValuePair<string, Vector3> pair in level1){			
			Vector3 position = pair.Value;
			Quaternion rotation = Quaternion.identity;
			Transform getChild = itemPicture.transform.FindChild("Sprite");
			GameObject child = getChild.gameObject;
			child.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("LevelContent/"+pair.Key);			
			Instantiate (itemPicture, position, rotation);
		}

		Dictionary<string, Vector3> level1_wrong = randomWrong (3, "cat");

		foreach (KeyValuePair<string, Vector3> pair in level1_wrong){			
			Vector3 position = pair.Value;
			Quaternion rotation = Quaternion.identity;
			Transform getChild = itemPicture_bad.transform.FindChild("Sprite");
			GameObject child = getChild.gameObject;
			child.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("LevelContent/"+pair.Key);			
			Instantiate (itemPicture_bad, position, rotation);
		}
	}

	void LoadLevel2(){
		
		Dictionary<string, Vector3> level1 = new Dictionary<string, Vector3>();
		level1.Add ("frog_1", new Vector3 (Random.Range(-15,15), 0, Random.Range(-15,15)));
		level1.Add ("frog_2", new Vector3 (Random.Range(-15,15), 0, Random.Range(-15,15)));
		level1.Add ("frog_3", new Vector3 (Random.Range(-15,15), 0, Random.Range(-15,15)));
		level1.Add ("frog_4", new Vector3 (Random.Range(-15,15), 0, Random.Range(-15,15)));
		level1.Add ("frog_5", new Vector3 (Random.Range(-15,15), 0, Random.Range(-15,15)));
		level1.Add ("frog_6", new Vector3 (Random.Range(-15,15), 0, Random.Range(-15,15)));
		
		foreach (KeyValuePair<string, Vector3> pair in level1){			
			Vector3 position = pair.Value;
			Quaternion rotation = Quaternion.identity;
			Transform getChild = itemPicture.transform.FindChild("Sprite");
			GameObject child = getChild.gameObject;
			child.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("LevelContent/"+pair.Key);			
			Instantiate (itemPicture, position, rotation);
		}
		
		Dictionary<string, Vector3> level1_wrong = randomWrong (6, "frog");
		
		foreach (KeyValuePair<string, Vector3> pair in level1_wrong){			
			Vector3 position = pair.Value;
			Quaternion rotation = Quaternion.identity;
			Transform getChild = itemPicture_bad.transform.FindChild("Sprite");
			GameObject child = getChild.gameObject;
			child.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("LevelContent/"+pair.Key);			
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

			randomWrong.Add ( current , new Vector3 (Random.Range(-15,15), 0, Random.Range(-15,15)));
		}
			
		return randomWrong;
	}

	List<string> levelContent(){
		List<string> levelContent = new List<string>();
		DirectoryInfo dir = new DirectoryInfo("Assets/Resources/LevelContent");
		FileInfo[] info = dir.GetFiles ("*.png");
		foreach (FileInfo f in info) {
			string temp = f.Name.Remove(f.Name.Length - 4);
			levelContent.Add(temp);
		}
		return levelContent;
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
