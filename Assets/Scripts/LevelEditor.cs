using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class LevelEditor : MonoBehaviour {
	
	public GameObject itemPicture;
	public GameObject itemPicture_bad;

	private PersistentController persistent;
	private DestroyByContact destroyByContact;
	private GameObject Player;
	private GameObject Canvas;
	private GameObject Details;
	private GameObject Notification;
	private AudioClip winSound;
	
	public int score = 0;
	public int life = 3;
	
	void Start (){

		Player = GameObject.FindWithTag ("Player");
		Canvas = GameObject.Find ("Canvas");
		Details = GameObject.Find ("Details");
		Notification = GameObject.Find ("Notification");
		Notification.SetActive (false);
		winSound = Resources.Load<AudioClip>("Sounds/level-win");

		GameObject persistentObject = GameObject.FindGameObjectWithTag("Persistent");
		if (persistentObject != null)
        {
			persistent = persistentObject.GetComponent<PersistentController>();
		}
		if (persistent == null)
        {
            Debug.Log("Cannot find 'Persistent Controller' script");
        }
		
		if (Application.loadedLevel == 3) {
			//File.AppendAllText (persistent.getFileName(), "\r\n"+persistent.getTime()+" start level 1");
			persistent.postHTML("\r\n"+persistent.getTime()+" start level 1");
			LoadLevel1 ();	
		}
		
		if (Application.loadedLevel == 4) {
			//File.AppendAllText (persistent.getFileName(), "\r\n"+persistent.getTime()+" start level 2");
			persistent.postHTML("\r\n"+persistent.getTime()+" start level 2");
			LoadLevel2 ();
		}
		
		if (Application.loadedLevel == 5) {
			//File.AppendAllText (persistent.getFileName(), "\r\n"+persistent.getTime()+" start level 3");
			persistent.postHTML("\r\n"+persistent.getTime()+" start level 3");
			LoadLevel3 ();
		}
		
		if (Application.loadedLevel == 6) {
			//File.AppendAllText (persistent.getFileName(), "\r\n"+persistent.getTime()+" start level 4");
			persistent.postHTML("\r\n"+persistent.getTime()+" start level 4");
			LoadLevel4();
		}
		
		if (Application.loadedLevel == 7) {
			//File.AppendAllText (persistent.getFileName(), "\r\n"+persistent.getTime()+" start level 5");
			persistent.postHTML("\r\n"+persistent.getTime()+" start level 5");
			LoadLevel5();
		}

		if (Application.loadedLevel == 8) {
			//File.AppendAllText (persistent.getFileName(), "\r\n"+persistent.getTime()+" start level 6");
			persistent.postHTML("\r\n"+persistent.getTime()+" start level 6");
			LoadLevel6 ();
		}

		if (Application.loadedLevel == 9) {
			//File.AppendAllText (persistent.getFileName(), "\r\n"+persistent.getTime()+" start level 7");
			persistent.postHTML("\r\n"+persistent.getTime()+" start level 7");
			LoadLevel7 ();
		}
	}
	
	void Update(){

		if(Player.activeSelf)
			HelpUpdate ();

		if (life == 0)
		{
			persistent.UpdateScore(Application.loadedLevel, score);
			//File.AppendAllText(persistent.getFileName(), "\r\n"+persistent.getTime()+" lose level " +(Application.loadedLevel-2)+ " score " +score);
			persistent.postHTML( persistent.returnLevelLog() );
			persistent.postHTML("\r\n"+persistent.getTime()+" lose level " +(Application.loadedLevel-2)+ " score " +score);
			Application.LoadLevel(2);
		}
		
		if (score == 3)
		{
			persistent.UpdateScore(Application.loadedLevel, score);
			AudioSource.PlayClipAtPoint(winSound,transform.position);
			StartCoroutine(LevelComplete());
		}
			

		if (Input.GetKeyDown(KeyCode.Escape))
			Application.LoadLevel(5);
	}

	public int getScore(){
		return score;
	}
	

	public void AddScore(){
		Transform getChild = Canvas.transform.FindChild ("Progress_Bar");
		GameObject child = getChild.gameObject;
		score = score + 1;
		child.GetComponent<Scrollbar>().size = score / 3f;
	}

	public void DecScore(){
		if (score <= 3) {
			Transform getChild = Canvas.transform.FindChild("Heart_"+life);
			GameObject child = getChild.gameObject;
			child.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load <Sprite> ("Sprites/DeadHeart");
			life--;
		}
	}

	public void HelpUpdate(){
			Details.transform.position = Player.transform.position + new Vector3 (0, 1.0f, 8.6f);
	}
	

	IEnumerator LevelComplete()
	{
		while (true) {
			Notification.SetActive(true);
			yield return new WaitForSeconds(3.0f);
			//File.AppendAllText (persistent.getFileName(), "\r\n"+persistent.getTime()+" win level " + (Application.loadedLevel-2));
			persistent.postHTML(persistent.returnLevelLog());
			persistent.postHTML("\r\n"+persistent.getTime()+" win level " + (Application.loadedLevel-2));
			Application.LoadLevel(1);
		}
	}

	public void QuitLevel(){
		persistent.UpdateScore(Application.loadedLevel, score);
		persistent.postHTML(persistent.returnLevelLog());
		persistent.postHTML("\r\n"+persistent.getTime()+" quit level " +(Application.loadedLevel-2)+ " score " +score);
		Application.LoadLevel (1);
	}
	
	private void LoadLevel1(){

		Stack<Vector3> levelPositions = randomPosition (33);

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

		Dictionary<string, Vector3> levelObject_wrong = randomWrong (30,"cat",levelPositions);

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

		Stack<Vector3> levelPositions = randomPosition (33);
		
		Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
		levelObject.Add ("car_1", levelPositions.Pop() );
		levelObject.Add ("car_2", levelPositions.Pop() );
		levelObject.Add ("car_3", levelPositions.Pop() );
		levelObject.Add ("car_4", levelPositions.Pop() );
		levelObject.Add ("car_5", levelPositions.Pop() );
		levelObject.Add ("car_6", levelPositions.Pop() );
		
		foreach (KeyValuePair<string, Vector3> pair in levelObject){			
			Vector3 position = pair.Value;
			Quaternion rotation = Quaternion.identity;
			Transform getChild = itemPicture.transform.FindChild("Sprite");
			GameObject child = getChild.gameObject;
			child.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("LevelContentUpdates/"+pair.Key);			
			Instantiate (itemPicture, position, rotation);
		}
		
		Dictionary<string, Vector3> levelObject_wrong = randomWrong (27, "car", levelPositions);
		
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
        Stack<Vector3> levelPositions = randomPosition(33);

        Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
        levelObject.Add("pencil_1", levelPositions.Pop());
        levelObject.Add("pencil_2", levelPositions.Pop());
        levelObject.Add("pencil_3", levelPositions.Pop());
	
        foreach (KeyValuePair<string, Vector3> pair in levelObject)
        {
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemPicture.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            child.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LevelContentUpdates/" + pair.Key);
            Instantiate(itemPicture, position, rotation);
        }

        Dictionary<string, Vector3> levelObject_wrong = randomWrong(30, "pencil", levelPositions);

        foreach (KeyValuePair<string, Vector3> pair in levelObject_wrong)
        {
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemPicture_bad.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            child.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LevelContentUpdates/" + pair.Key);
            Instantiate(itemPicture_bad, position, rotation);
        }
	}

    private void LoadLevel4()
    {
        Stack<Vector3> levelPositions = randomPosition(33);

        Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
        levelObject.Add ("frog_1", levelPositions.Pop());
        levelObject.Add ("frog_2", levelPositions.Pop());
        levelObject.Add ("frog_3", levelPositions.Pop());
        levelObject.Add ("frog_4", levelPositions.Pop());
        levelObject.Add ("frog_5", levelPositions.Pop());
		levelObject.Add ("frog_6", levelPositions.Pop());

        foreach (KeyValuePair<string, Vector3> pair in levelObject)
        {
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemPicture.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            child.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LevelContentUpdates/" + pair.Key);
            Instantiate(itemPicture, position, rotation);
        }

        Dictionary<string, Vector3> levelObject_wrong = randomWrong(27, "frog", levelPositions);

        foreach (KeyValuePair<string, Vector3> pair in levelObject_wrong)
        {
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemPicture_bad.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            child.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LevelContentUpdates/" + pair.Key);
            Instantiate(itemPicture_bad, position, rotation);
        }
    }

    private void LoadLevel5()
    {
        Stack<Vector3> levelPositions = randomPosition(33);

        Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
        levelObject.Add("horse_1", levelPositions.Pop());
        levelObject.Add("horse_2", levelPositions.Pop());
        levelObject.Add("horse_3", levelPositions.Pop());

        foreach (KeyValuePair<string, Vector3> pair in levelObject)
        {
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemPicture.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            child.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LevelContentWords/" + pair.Key);
            Instantiate(itemPicture, position, rotation);
        }

        Dictionary<string, Vector3> levelObject_wrong = randomWordsWrong(30, "horse", levelPositions);

        foreach (KeyValuePair<string, Vector3> pair in levelObject_wrong)
        {
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemPicture_bad.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            child.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LevelContentWords/" + pair.Key);
            Instantiate(itemPicture_bad, position, rotation);
        }
    }

    private void LoadLevel6()
    {
        Stack<Vector3> levelPositions = randomPosition(14);

        Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
        levelObject.Add("phone_1", levelPositions.Pop());
        levelObject.Add("phone_2", levelPositions.Pop());
        levelObject.Add("phone_3", levelPositions.Pop());

        foreach (KeyValuePair<string, Vector3> pair in levelObject)
        {
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemPicture.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            child.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LevelContentWords/" + pair.Key);
            Instantiate(itemPicture, position, rotation);
        }

        Dictionary<string, Vector3> levelObject_wrong = new Dictionary<string, Vector3>();
        levelObject_wrong.Add("nature_1", levelPositions.Pop());
        levelObject_wrong.Add("animal_1", levelPositions.Pop());
        levelObject_wrong.Add("celebrate_1", levelPositions.Pop());
        levelObject_wrong.Add("phony_1", levelPositions.Pop());
        levelObject_wrong.Add("edible_1", levelPositions.Pop());
        levelObject_wrong.Add("photography_1", levelPositions.Pop());
        levelObject_wrong.Add("pickle_1", levelPositions.Pop());
        levelObject_wrong.Add("phantom_1", levelPositions.Pop());
        levelObject_wrong.Add("philosophy_1", levelPositions.Pop());
        levelObject_wrong.Add("fossil_1", levelPositions.Pop());
        levelObject_wrong.Add("fees_1", levelPositions.Pop());

        foreach (KeyValuePair<string, Vector3> pair in levelObject_wrong)
        {
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemPicture_bad.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            child.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LevelContentWords/" + pair.Key);
            Instantiate(itemPicture_bad, position, rotation);
        }
    }

    private void LoadLevel7()
    {
        Stack<Vector3> levelPositions = randomPosition(14);

        Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
        levelObject.Add("cow_1", levelPositions.Pop());
        levelObject.Add("cow_2", levelPositions.Pop());
        levelObject.Add("cow_3", levelPositions.Pop());

        foreach (KeyValuePair<string, Vector3> pair in levelObject)
        {
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemPicture.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            child.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LevelContentWords/" + pair.Key);
            Instantiate(itemPicture, position, rotation);
        }

        Dictionary<string, Vector3> levelObject_wrong = new Dictionary<string, Vector3>();
        levelObject_wrong.Add("cat_1", levelPositions.Pop());
        levelObject_wrong.Add("can_1", levelPositions.Pop());
        levelObject_wrong.Add("cap_1", levelPositions.Pop());
        levelObject_wrong.Add("cup_1", levelPositions.Pop());
        levelObject_wrong.Add("car_1", levelPositions.Pop());
        levelObject_wrong.Add("cot_1", levelPositions.Pop());
        levelObject_wrong.Add("cud_1", levelPositions.Pop());
        levelObject_wrong.Add("cab_1", levelPositions.Pop());
        levelObject_wrong.Add("cam_1", levelPositions.Pop());
        levelObject_wrong.Add("camp_1", levelPositions.Pop());
        levelObject_wrong.Add("cast_1", levelPositions.Pop());

        foreach (KeyValuePair<string, Vector3> pair in levelObject_wrong)
        {
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemPicture_bad.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            child.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LevelContentWords/" + pair.Key);
            Instantiate(itemPicture_bad, position, rotation);
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

    private Dictionary<string, Vector3> randomWordsWrong(int length, string levelWord, Stack<Vector3> positions)
    {
        Dictionary<string, Vector3> randomWrong = new Dictionary<string, Vector3>();
        List<string> allObjects = levelContentWord();

        for (int i = 0; i < length; i++)
        {
            string current = allObjects[Random.Range(0, allObjects.Count)];

            while (current.Contains(levelWord) || randomWrong.ContainsKey(current))
                current = allObjects[Random.Range(0, allObjects.Count)];

            randomWrong.Add(current, positions.Pop());
        }

        return randomWrong;
    }

	List<string> levelContent(){
		List<string> levelContent = new List<string>();
		Object[] sprites = Resources.LoadAll ("LevelContentUpdates");

		foreach (Object o in sprites) 
			levelContent.Add(o.name);
		
		return levelContent;
	}

    List<string> levelContentWord()
    {
        List<string> levelContent = new List<string>();
		Object[] sprites = Resources.LoadAll ("LevelContentWords");
		
		foreach (Object o in sprites) 
			levelContent.Add(o.name);

        return levelContent;
    }
	
	Stack<Vector3> randomPosition(int qnt){
		List<Vector3> positions = new List<Vector3>();
		Stack<Vector3> stack = new Stack<Vector3>();

		Vector3 spaceShip = new Vector3 (0, 0, 0);
		Vector3 testPosition;
		bool validPosition;

		positions.Add (new Vector3(Random.Range (-72, 72), 0 ,Random.Range (-72, 72)));

		while (qnt > 0) {
			testPosition = new Vector3(Random.Range (-72, 72), 0 ,Random.Range (-72, 72)); 
			validPosition = true;

			foreach(Vector3 pos in positions){
				if((Vector3.Distance(pos,testPosition) <= 5.0f) || (Vector3.Distance(spaceShip,testPosition) <= 5.0f))
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
