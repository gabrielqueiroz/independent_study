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

    private PersistentController progressBar;
	private GameObject Player;
	private GameObject Canvas;
	private GameObject Details;
	private GameObject Notification;
	
	public int score =0;
	public int life;
	
	void Start (){

		Player = GameObject.FindWithTag ("Player");
		Canvas = GameObject.Find ("Canvas");
		Details = GameObject.Find ("Details");
		Notification = GameObject.Find ("Notification");
		Notification.SetActive (false);

        GameObject progressObject = GameObject.FindGameObjectWithTag("Persistent");
        if (progressObject != null)
        {
			progressBar = progressObject.GetComponent<PersistentController>();
		}
		if (progressBar == null)
        {
            Debug.Log("Cannot find 'Persistent Controller' script");
        }
		
		if (Application.loadedLevel == 3)
			LoadLevel1 ();	
		if (Application.loadedLevel == 4)
			LoadLevel2 ();	
		if (Application.loadedLevel == 5)
			LoadLevel1 ();
	}
	
	void Update(){

		if(Player.activeSelf)
			HelpUpdate ();

        if (life == 0)
        {
			if(progressBar != null)
            	progressBar.UpdateScore(Application.loadedLevel, score);
            Application.LoadLevel(2);

        }

        if (score == 3)
        {
			if(progressBar != null)
            	progressBar.UpdateScore(Application.loadedLevel, score);
            StartCoroutine(LevelComplete());
        }
			

		if (Input.GetKeyDown(KeyCode.Escape))
			Application.LoadLevel(5);
	}

	public void AddScore(){
		Transform getChild = Canvas.transform.FindChild ("Progress_Bar");
		GameObject child = getChild.gameObject;
		score = score + 1;
		child.GetComponent<Scrollbar>().size = score / 3f;
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
			Application.LoadLevel(1);
		}
	}
	
	private void LoadLevel1(){

		Stack<Vector3> levelPositions = randomPosition (6);

		Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
		levelObject.Add ("Cat_1", levelPositions.Pop() );
		levelObject.Add ("Cat_2", levelPositions.Pop() );
		levelObject.Add ("Cat_3", levelPositions.Pop() );

		foreach (KeyValuePair<string, Vector3> pair in levelObject){			
			Vector3 position = pair.Value;
			Quaternion rotation = Quaternion.identity;
			Transform getChild = itemPicture.transform.FindChild("Sprite");
			GameObject child = getChild.gameObject;
			child.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("LevelContentUpdates/"+pair.Key);			
			Instantiate (itemPicture, position, rotation);
		}

		Dictionary<string, Vector3> levelObject_wrong = randomWrong (3, "cat",levelPositions);

		foreach (KeyValuePair<string, Vector3> pair in levelObject_wrong){			
			Vector3 position = pair.Value;
			Quaternion rotation = Quaternion.identity;
			Transform getChild = itemPicture_bad.transform.FindChild("Sprite");
			GameObject child = getChild.gameObject;
			child.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("LevelContentUpdates/"+pair.Key);			
			Instantiate (itemPicture_bad, position, rotation);
		}
	}

	private void LoadLevel2(){

		Stack<Vector3> levelPositions = randomPosition (12);
		
		Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
		levelObject.Add ("frog_1", levelPositions.Pop() );
		levelObject.Add ("frog_2", levelPositions.Pop() );
		levelObject.Add ("frog_3", levelPositions.Pop() );
		levelObject.Add ("frog_4", levelPositions.Pop() );
		levelObject.Add ("frog_5", levelPositions.Pop() );
		levelObject.Add ("frog_6", levelPositions.Pop() ); 
		
		foreach (KeyValuePair<string, Vector3> pair in levelObject){			
			Vector3 position = pair.Value;
			Quaternion rotation = Quaternion.identity;
			Transform getChild = itemPicture.transform.FindChild("Sprite");
			GameObject child = getChild.gameObject;
			child.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("LevelContentUpdates/"+pair.Key);			
			Instantiate (itemPicture, position, rotation);
		}
		
		Dictionary<string, Vector3> levelObject_wrong = randomWrong (6, "frog", levelPositions);
		
		foreach (KeyValuePair<string, Vector3> pair in levelObject_wrong){			
			Vector3 position = pair.Value;
			Quaternion rotation = Quaternion.identity;
			Transform getChild = itemPicture_bad.transform.FindChild("Sprite");
			GameObject child = getChild.gameObject;
			child.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("LevelContentUpdates/"+pair.Key);			
			Instantiate (itemPicture_bad, position, rotation);
		}
	}

	
	private void LoadLevel3(){
		Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
		
		//Load Good Words
		levelObject.Add ("Word1", new Vector3 (0,0,5));
		levelObject.Add ("Word3", new Vector3 (5,0,10));
		levelObject.Add ("Word5", new Vector3 (-5,0,10));
		
		foreach (KeyValuePair<string, Vector3> pair in levelObject){
			
			Vector3 position = pair.Value;
			Quaternion rotation = Quaternion.identity;
			Transform getChild = itemWord.transform.FindChild("Word");
			GameObject child = getChild.gameObject;
			child.GetComponent<TextMesh>().text = pair.Key;
			Instantiate (itemWord, position, rotation);
		}
		
		//Load Bad Words
		levelObject.Add ("Word2", new Vector3 (-5,0,5));
		levelObject.Add ("Word4", new Vector3 (0,0,10));
		levelObject.Add ("Word6", new Vector3 (5,0,5));
		
		foreach (KeyValuePair<string, Vector3> pair in levelObject){
			
			Vector3 position = pair.Value;
			Quaternion rotation = Quaternion.identity;
			Transform getChild = itemWord_bad.transform.FindChild("Word");
			GameObject child = getChild.gameObject;
			child.GetComponent<TextMesh>().text = pair.Key;
			Instantiate (itemWord_bad, position, rotation);
		}
	}

	private Dictionary<string, Vector3> randomWrong(int length, string levelWord, Stack<Vector3> positions){
		Dictionary<string, Vector3> randomWrong = new Dictionary<string, Vector3>();
		List<string> allObjects = levelContent ();

		for (int i = 0; i < length; i++) {
			string current = allObjects [Random.Range (0,allObjects.Count)];

			while (current.Contains(levelWord) || randomWrong.ContainsKey(current))
				current = allObjects [Random.Range (0, allObjects.Count)];

			randomWrong.Add ( current , positions.Pop() );
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
	
	Stack<Vector3> randomPosition(int qnt){
		List<Vector3> positions = new List<Vector3>();
		Stack<Vector3> stack = new Stack<Vector3>();

		Vector3 testPosition;
		bool validPosition;

		positions.Add (new Vector3(Random.Range (-14, 5), 0 ,Random.Range (5,14)));

		while (qnt > 0) {
			testPosition = new Vector3(Random.Range (-14, 5), 0 ,Random.Range (5,14)); 
			validPosition = true;

			foreach(Vector3 pos in positions){
				if(Vector3.Distance(pos,testPosition) <= 3.0f)
					validPosition=false;				
			}

			if(validPosition){ 
				positions.Add (testPosition); 
				qnt = qnt - 1;
			}
				
		}

		foreach(Vector3 pos in positions){
			stack.Push (pos);
		}
		
		return stack;
	}

}
