﻿using UnityEngine;
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
			LoadLevel3 ();
        if (Application.loadedLevel == 6)
            LoadLevel4();
        if (Application.loadedLevel == 7)
            LoadLevel5();
        if (Application.loadedLevel == 8)
            LoadLevel6();
        if (Application.loadedLevel == 9)
            LoadLevel7();
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
		levelObject.Add ("car_1", levelPositions.Pop() );
		levelObject.Add ("car_2", levelPositions.Pop() );
		levelObject.Add ("car_3", levelPositions.Pop() );
		levelObject.Add ("car_4", levelPositions.Pop() );
		levelObject.Add ("car_5", levelPositions.Pop() );
		levelObject.Add ("car_6", levelPositions.Pop() );
        levelObject.Add("car_7", levelPositions.Pop());
        levelObject.Add("car_8", levelPositions.Pop()); 
		
		foreach (KeyValuePair<string, Vector3> pair in levelObject){			
			Vector3 position = pair.Value;
			Quaternion rotation = Quaternion.identity;
			Transform getChild = itemPicture.transform.FindChild("Sprite");
			GameObject child = getChild.gameObject;
			child.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("LevelContentUpdates/"+pair.Key);			
			Instantiate (itemPicture, position, rotation);
		}
		
		Dictionary<string, Vector3> levelObject_wrong = randomWrong (8, "car", levelPositions);
		
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
        Stack<Vector3> levelPositions = randomPosition(12);

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

        Dictionary<string, Vector3> levelObject_wrong = randomWrong(3, "pencil", levelPositions);

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
        Stack<Vector3> levelPositions = randomPosition(12);

        Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
        levelObject.Add("frog_1", levelPositions.Pop());
        levelObject.Add("frog_2", levelPositions.Pop());
        levelObject.Add("frog_3", levelPositions.Pop());
        levelObject.Add("frog_4", levelPositions.Pop());
        levelObject.Add("frog_5", levelPositions.Pop());
        levelObject.Add("frog_6", levelPositions.Pop());


        foreach (KeyValuePair<string, Vector3> pair in levelObject)
        {
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemPicture.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            child.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LevelContentUpdates/" + pair.Key);
            Instantiate(itemPicture, position, rotation);
        }

        Dictionary<string, Vector3> levelObject_wrong = randomWrong(6, "frog", levelPositions);

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
        Stack<Vector3> levelPositions = randomPosition(9);

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

        Dictionary<string, Vector3> levelObject_wrong = randomWordsWrong(6, "horse", levelPositions);

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
		DirectoryInfo dir = new DirectoryInfo("Assets/Resources/LevelContentUpdates");
		FileInfo[] info = dir.GetFiles ("*.png");
		foreach (FileInfo f in info) {
			string temp = f.Name.Remove(f.Name.Length - 4);
			levelContent.Add(temp);
		}
		return levelContent;
	}

    List<string> levelContentWord()
    {
        List<string> levelContent = new List<string>();
        DirectoryInfo dir = new DirectoryInfo("Assets/Resources/LevelContentWords");
        FileInfo[] info = dir.GetFiles("*.png");
        foreach (FileInfo f in info)
        {
            string temp = f.Name.Remove(f.Name.Length - 4);
            levelContent.Add(temp);
        }
        return levelContent;
    }
	
	Stack<Vector3> randomPosition(int qnt){
		List<Vector3> positions = new List<Vector3>();
		Stack<Vector3> stack = new Stack<Vector3>();

		Vector3 spaceShip = new Vector3 (0, 0, 0);
		Vector3 testPosition;
		bool validPosition;

		positions.Add (new Vector3(Random.Range (-12, 12), 0 ,Random.Range (-12, 12)));

		while (qnt > 0) {
			testPosition = new Vector3(Random.Range (-12, 12), 0 ,Random.Range (-12, 12)); 
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
