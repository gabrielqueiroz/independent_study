using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class LevelEditor : MonoBehaviour {
	
	public GameObject itemPicture;
	public GameObject itemPicture_bad;
	public GameObject itemText;
	public GameObject itemText_bad;
	public GameObject explosion;

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
			persistent.postHTML("\r\n"+persistent.getTime()+" start level 1");
			LoadLevel1 ();	
		}
		
		if (Application.loadedLevel == 4) {
			persistent.postHTML("\r\n"+persistent.getTime()+" start level 2");
			LoadLevel2 ();
		}
		
		if (Application.loadedLevel == 5) {
			persistent.postHTML("\r\n"+persistent.getTime()+" start level 3");
			LoadLevel3 ();
		}
		
		if (Application.loadedLevel == 6) {
			persistent.postHTML("\r\n"+persistent.getTime()+" start level 4");
			LoadLevel4();
		}
		
		if (Application.loadedLevel == 7) {
			persistent.postHTML("\r\n"+persistent.getTime()+" start level 5");
			LoadLevel5();
		}

		if (Application.loadedLevel == 8) {
			persistent.postHTML("\r\n"+persistent.getTime()+" start level 6");
			LoadLevel6 ();
		}

		if (Application.loadedLevel == 9) {
			persistent.postHTML("\r\n"+persistent.getTime()+" start level 7");
			LoadLevel7 ();
		}
		
		if (Application.loadedLevel == 10) {
			persistent.postHTML("\r\n"+persistent.getTime()+" start level 8");
			LoadLevel8 ();
		}
		
		if (Application.loadedLevel == 11) {
			persistent.postHTML("\r\n"+persistent.getTime()+" start level 9");
			LoadLevel9 ();
		}
		
		if (Application.loadedLevel == 12) {
			persistent.postHTML("\r\n"+persistent.getTime()+" start level 10");
			LoadLevel10 ();
		}
		
		if (Application.loadedLevel == 13) {
			persistent.postHTML("\r\n"+persistent.getTime()+" start level 11");
			LoadLevel11 ();
		}

		if (Application.loadedLevel == 14) {
			persistent.postHTML("\r\n"+persistent.getTime()+" start level 12");
			LoadLevel12 ();
		}

		if (Application.loadedLevel == 15) {
			persistent.postHTML("\r\n"+persistent.getTime()+" start level 13");
			LoadLevel13 ();
		}

		if (Application.loadedLevel == 16) {
			persistent.postHTML("\r\n"+persistent.getTime()+" start level 14");
			LoadLevel14 ();
		}

		if (Application.loadedLevel == 17) {
			persistent.postHTML("\r\n"+persistent.getTime()+" start level 15");
			LoadLevel15 ();
		}

		if (Application.loadedLevel == 18) {
			persistent.postHTML("\r\n"+persistent.getTime()+" start level 16");
			LoadLevel16 ();
		}

		if (Application.loadedLevel == 19) {
			persistent.postHTML("\r\n"+persistent.getTime()+" start level 17");
			LoadLevel17 ();
		}

		if (Application.loadedLevel == 20) {
			persistent.postHTML("\r\n"+persistent.getTime()+" start level 18");
			LoadLevel18 ();
		}
	}
	
	void Update(){

		if(Player.activeSelf)
			HelpUpdate ();

		if (life == 0)
		{
			persistent.UpdateScore(Application.loadedLevel, score);
			persistent.postHTML( persistent.returnLevelLog() );
			persistent.postHTML("\r\n"+persistent.getTime()+" lose level " +(Application.loadedLevel-2)+ " score " +score);
			Application.LoadLevel(2);
		}
		
		if (score == 3)
		{
			persistent.UpdateScore(Application.loadedLevel, score);
			StartCoroutine(LevelComplete());
		}
			

		if (Input.GetKeyDown (KeyCode.Escape))
			QuitLevel ();
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
		if (!Notification.activeSelf) {
			AudioSource.PlayClipAtPoint(winSound,transform.position);
			Instantiate(explosion, Player.transform.position + new Vector3(0,0,3f), Player.transform.rotation);
			Instantiate(explosion, Player.transform.position + new Vector3(-3f,0,3f), Player.transform.rotation);
			Instantiate(explosion, Player.transform.position + new Vector3(3f,0,3f), Player.transform.rotation);
		}

		Notification.SetActive(true);

		while (true) {
			yield return new WaitForSeconds(3.0f);
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

	/*********************************************START LEVELS EDIT**************************************************/
	/*********************************************START LEVELS EDIT**************************************************/
	
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
        Stack<Vector3> levelPositions = randomPosition(33);

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

		Dictionary<string, Vector3> levelObject_wrong = randomWordsWrong(30, "phone", levelPositions);

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
        Stack<Vector3> levelPositions = randomPosition(33);

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

		Dictionary<string, Vector3> levelObject_wrong = randomWordsWrong(30, "cow", levelPositions);
	
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

	private void LoadLevel8()
	{
		Stack<Vector3> levelPositions = randomPosition(33);
		
		Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
		levelObject.Add("animal_1", levelPositions.Pop());
		levelObject.Add("animal_2", levelPositions.Pop());
		levelObject.Add("animal_3", levelPositions.Pop());
		levelObject.Add("brown_1", levelPositions.Pop());
		levelObject.Add("monkey_1", levelPositions.Pop());

		levelObject.Add("smiling_1", levelPositions.Pop());
		
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
		levelObject_wrong.Add("dog_1", levelPositions.Pop());
		levelObject_wrong.Add("angry_1", levelPositions.Pop());
		levelObject_wrong.Add("bird_1", levelPositions.Pop());
		levelObject_wrong.Add("band_1", levelPositions.Pop());
		levelObject_wrong.Add("yellow_1", levelPositions.Pop());

		levelObject_wrong.Add("horse_1", levelPositions.Pop());
		levelObject_wrong.Add("baby_1", levelPositions.Pop());
		levelObject_wrong.Add("ketchup_1", levelPositions.Pop());
		levelObject_wrong.Add("cat_1", levelPositions.Pop());
		levelObject_wrong.Add("can_1", levelPositions.Pop());

		levelObject_wrong.Add("cap_1", levelPositions.Pop());
		levelObject_wrong.Add("cup_1", levelPositions.Pop());
		levelObject_wrong.Add("car_1", levelPositions.Pop());
		levelObject_wrong.Add("plant_1", levelPositions.Pop());
		levelObject_wrong.Add("red_1", levelPositions.Pop());

		levelObject_wrong.Add("cot_1", levelPositions.Pop());
		levelObject_wrong.Add("cud_1", levelPositions.Pop());
		levelObject_wrong.Add("cab_1", levelPositions.Pop());
		levelObject_wrong.Add("cam_1", levelPositions.Pop());
		levelObject_wrong.Add("camp_1", levelPositions.Pop());

		levelObject_wrong.Add("cast_1", levelPositions.Pop());
		levelObject_wrong.Add("horse_2", levelPositions.Pop());
		levelObject_wrong.Add("trafficlight_1", levelPositions.Pop() );
		levelObject_wrong.Add("trafficlight_2", levelPositions.Pop() );
		levelObject_wrong.Add("pickle_1", levelPositions.Pop() );
	
		levelObject_wrong.Add("van_1", levelPositions.Pop() );
		levelObject_wrong.Add("sun_1", levelPositions.Pop() );

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

	private void LoadLevel9()
	{
		Stack<Vector3> levelPositions = randomPosition(33);
		
		Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
		levelObject.Add("tree_1", levelPositions.Pop());
		levelObject.Add("nature_1", levelPositions.Pop());
		levelObject.Add("green_1", levelPositions.Pop());
		levelObject.Add("plant_1", levelPositions.Pop());

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
		levelObject_wrong.Add("bird_1", levelPositions.Pop());
		levelObject_wrong.Add("band_1", levelPositions.Pop());
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
		levelObject_wrong.Add("dog_1", levelPositions.Pop());
		levelObject_wrong.Add("angry_1", levelPositions.Pop());

		levelObject_wrong.Add("yellow_1", levelPositions.Pop());
		levelObject_wrong.Add("horse_1", levelPositions.Pop());
		levelObject_wrong.Add("baby_1", levelPositions.Pop());
		levelObject_wrong.Add("ketchup_1", levelPositions.Pop());
		levelObject_wrong.Add("fees_1", levelPositions.Pop());

		levelObject_wrong.Add("trafficlight_1", levelPositions.Pop());
		levelObject_wrong.Add("galloping_1", levelPositions.Pop());
		levelObject_wrong.Add("pickle_1", levelPositions.Pop());
		levelObject_wrong.Add("celebrate_1", levelPositions.Pop());
		levelObject_wrong.Add("sun_1", levelPositions.Pop());

		levelObject_wrong.Add("celebrate_2", levelPositions.Pop());
		levelObject_wrong.Add("celebrate_3", levelPositions.Pop());
		levelObject_wrong.Add("horse_2", levelPositions.Pop());
		levelObject_wrong.Add("horse_3", levelPositions.Pop());

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

	private void LoadLevel10()
	{
		Stack<Vector3> levelPositions = randomPosition(33);
		
		Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
		levelObject.Add("streetcar_1", levelPositions.Pop());
		levelObject.Add("red_1", levelPositions.Pop());
		levelObject.Add("transportation_1", levelPositions.Pop());
		
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
		levelObject_wrong.Add("bird_1", levelPositions.Pop());
		levelObject_wrong.Add("band_1", levelPositions.Pop());
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
		levelObject_wrong.Add("dog_1", levelPositions.Pop());
		levelObject_wrong.Add("angry_1", levelPositions.Pop());

		levelObject_wrong.Add("celebrate_2", levelPositions.Pop());
		levelObject_wrong.Add("celebrate_3", levelPositions.Pop());
		levelObject_wrong.Add("horse_2", levelPositions.Pop());
		levelObject_wrong.Add("horse_3", levelPositions.Pop());
		levelObject_wrong.Add("galloping_1", levelPositions.Pop());

		levelObject_wrong.Add("pickle_1", levelPositions.Pop());
		levelObject_wrong.Add("celebrate_1", levelPositions.Pop());
		levelObject_wrong.Add("frog_1", levelPositions.Pop());
		levelObject_wrong.Add("frog_2", levelPositions.Pop());
		levelObject_wrong.Add("frog_3", levelPositions.Pop());

		levelObject_wrong.Add("yellow_1", levelPositions.Pop());
		levelObject_wrong.Add("horse_1", levelPositions.Pop());
		levelObject_wrong.Add("baby_1", levelPositions.Pop());
		levelObject_wrong.Add("ketchup_1", levelPositions.Pop());
		levelObject_wrong.Add("nature_1", levelPositions.Pop());

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

	private void LoadLevel11()
	{
		Stack<Vector3> levelPositions = randomPosition(33);
		
		Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3> ();
		levelObject.Add("airport_1", levelPositions.Pop());
		levelObject.Add("hamburger_1", levelPositions.Pop());
		levelObject.Add("lightning_1", levelPositions.Pop());
		
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
		levelObject_wrong.Add("sun_1", new Vector3());
		levelObject_wrong.Add("pink_1", new Vector3());
		levelObject_wrong.Add("horse_1", new Vector3());

		levelObject_wrong.Add("animal_1", new Vector3());
		levelObject_wrong.Add("yellow_1", new Vector3());
		levelObject_wrong.Add("galloping_1", new Vector3());

		for (int i=0; i<5; i++) {
			foreach (KeyValuePair<string, Vector3> pair in levelObject_wrong)
			{
				Vector3 position = levelPositions.Pop();
				Quaternion rotation = Quaternion.identity;
				Transform getChild = itemPicture_bad.transform.FindChild("Sprite");
				GameObject child = getChild.gameObject;
				child.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LevelContentWords/" + pair.Key);
				Instantiate(itemPicture_bad, position, rotation);
			}
		}
	}

	private void LoadLevel12()
	{
		Stack<Vector3> levelPositions = randomPosition(33);
		
		Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
		levelObject.Add ("book_1", levelPositions.Pop());
		levelObject.Add ("book_2", levelPositions.Pop());
		levelObject.Add ("book_3", levelPositions.Pop());
		levelObject.Add ("desk_1", levelPositions.Pop());
		levelObject.Add ("pencil_1", levelPositions.Pop());
		
		foreach (KeyValuePair<string, Vector3> pair in levelObject)
		{
			Vector3 position = pair.Value;
			Quaternion rotation = Quaternion.identity;
			Transform getChild = itemPicture.transform.FindChild("Sprite");
			GameObject child = getChild.gameObject;
			child.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LevelContentUpdates/" + pair.Key);
			Instantiate(itemPicture, position, rotation);
		}
		
		Dictionary<string, Vector3> levelObject_wrong = new Dictionary<string, Vector3>();
		levelObject_wrong.Add ("bed_1", levelPositions.Pop() );
		levelObject_wrong.Add ("bed_2", levelPositions.Pop() );
		levelObject_wrong.Add ("bed_3", levelPositions.Pop() );
		levelObject_wrong.Add ("trafficlight_1", levelPositions.Pop() );
		levelObject_wrong.Add ("trafficlight_2", levelPositions.Pop() );
		levelObject_wrong.Add ("van_1", levelPositions.Pop() );
		levelObject_wrong.Add ("car_1", levelPositions.Pop() );
		levelObject_wrong.Add ("car_2", levelPositions.Pop() );
		levelObject_wrong.Add ("car_3", levelPositions.Pop() );
		levelObject_wrong.Add ("car_4", levelPositions.Pop() );
		levelObject_wrong.Add ("frog_1", levelPositions.Pop() );
		levelObject_wrong.Add ("frog_2", levelPositions.Pop() );
		levelObject_wrong.Add ("frog_3", levelPositions.Pop() );
		levelObject_wrong.Add ("cat_1", levelPositions.Pop() );
		levelObject_wrong.Add ("cat_2", levelPositions.Pop() );
		levelObject_wrong.Add ("cat_3", levelPositions.Pop() );
		levelObject_wrong.Add ("cow_1", levelPositions.Pop() );
		levelObject_wrong.Add ("cow_2", levelPositions.Pop() );
		levelObject_wrong.Add ("cow_3", levelPositions.Pop() );
		levelObject_wrong.Add ("dog_1", levelPositions.Pop() );
		levelObject_wrong.Add ("dog_2", levelPositions.Pop() );
		levelObject_wrong.Add ("dog_3", levelPositions.Pop() );
		levelObject_wrong.Add ("horse_1", levelPositions.Pop() );
		levelObject_wrong.Add ("horse_2", levelPositions.Pop() );
		levelObject_wrong.Add ("horse_3", levelPositions.Pop() );
		levelObject_wrong.Add ("horse_4", levelPositions.Pop() );
		levelObject_wrong.Add ("plane_1", levelPositions.Pop() );
		levelObject_wrong.Add ("plane_2", levelPositions.Pop() );


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

	private void LoadLevel13()
	{
		Stack<Vector3> levelPositions = randomPosition(33);
		
		Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
		levelObject.Add ("eggs_1", levelPositions.Pop());
		levelObject.Add ("pancakes_1", levelPositions.Pop());
		levelObject.Add ("pasta_1", levelPositions.Pop());
		levelObject.Add ("tacos_1", levelPositions.Pop());
		levelObject.Add ("waffle_1", levelPositions.Pop());
		levelObject.Add ("waffle_2", levelPositions.Pop());
		
		foreach (KeyValuePair<string, Vector3> pair in levelObject)
		{
			Vector3 position = pair.Value;
			Quaternion rotation = Quaternion.identity;
			Transform getChild = itemPicture.transform.FindChild("Sprite");
			GameObject child = getChild.gameObject;
			child.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LevelContentUpdates/" + pair.Key);
			Instantiate(itemPicture, position, rotation);
		}
		
		Dictionary<string, Vector3> levelObject_wrong = new Dictionary<string, Vector3>();
		levelObject_wrong.Add ("bed_1", levelPositions.Pop() );
		levelObject_wrong.Add ("bed_2", levelPositions.Pop() );
		levelObject_wrong.Add ("bed_3", levelPositions.Pop() );
		levelObject_wrong.Add ("trafficlight_1", levelPositions.Pop() );
		levelObject_wrong.Add ("trafficlight_2", levelPositions.Pop() );
		levelObject_wrong.Add ("van_1", levelPositions.Pop() );
		levelObject_wrong.Add ("car_1", levelPositions.Pop() );
		levelObject_wrong.Add ("car_2", levelPositions.Pop() );
		levelObject_wrong.Add ("car_3", levelPositions.Pop() );
		levelObject_wrong.Add ("car_4", levelPositions.Pop() );
		levelObject_wrong.Add ("frog_1", levelPositions.Pop() );
		levelObject_wrong.Add ("frog_2", levelPositions.Pop() );
		levelObject_wrong.Add ("frog_3", levelPositions.Pop() );
		levelObject_wrong.Add ("cat_1", levelPositions.Pop() );
		levelObject_wrong.Add ("cat_2", levelPositions.Pop() );
		levelObject_wrong.Add ("cat_3", levelPositions.Pop() );
		levelObject_wrong.Add ("cow_1", levelPositions.Pop() );
		levelObject_wrong.Add ("cow_2", levelPositions.Pop() );
		levelObject_wrong.Add ("cow_3", levelPositions.Pop() );
		levelObject_wrong.Add ("dog_1", levelPositions.Pop() );
		levelObject_wrong.Add ("dog_2", levelPositions.Pop() );
		levelObject_wrong.Add ("dog_3", levelPositions.Pop() );
		levelObject_wrong.Add ("horse_1", levelPositions.Pop() );
		levelObject_wrong.Add ("horse_2", levelPositions.Pop() );
		levelObject_wrong.Add ("horse_3", levelPositions.Pop() );

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

	private void LoadLevel14()
	{
		Stack<Vector3> levelPositions = randomPosition(33);
		
		Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
		levelObject.Add ("pen_1", levelPositions.Pop());
		levelObject.Add ("pen_2", levelPositions.Pop());
		levelObject.Add ("pen_3", levelPositions.Pop());
		levelObject.Add ("pencil_1", levelPositions.Pop());
		levelObject.Add ("pencil_2", levelPositions.Pop());
		levelObject.Add ("pencil_3", levelPositions.Pop());
		
		foreach (KeyValuePair<string, Vector3> pair in levelObject)
		{
			Vector3 position = pair.Value;
			Quaternion rotation = Quaternion.identity;
			Transform getChild = itemPicture.transform.FindChild("Sprite");
			GameObject child = getChild.gameObject;
			child.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LevelContentUpdates/" + pair.Key);
			Instantiate(itemPicture, position, rotation);
		}
		
		Dictionary<string, Vector3> levelObject_wrong = new Dictionary<string, Vector3>();
		levelObject_wrong.Add ("bed_1", levelPositions.Pop() );
		levelObject_wrong.Add ("bed_2", levelPositions.Pop() );
		levelObject_wrong.Add ("bed_3", levelPositions.Pop() );
		levelObject_wrong.Add ("trafficlight_1", levelPositions.Pop() );
		levelObject_wrong.Add ("trafficlight_2", levelPositions.Pop() );
		levelObject_wrong.Add ("van_1", levelPositions.Pop() );
		levelObject_wrong.Add ("car_1", levelPositions.Pop() );
		levelObject_wrong.Add ("car_2", levelPositions.Pop() );
		levelObject_wrong.Add ("car_3", levelPositions.Pop() );
		levelObject_wrong.Add ("car_4", levelPositions.Pop() );
		levelObject_wrong.Add ("frog_1", levelPositions.Pop() );
		levelObject_wrong.Add ("frog_2", levelPositions.Pop() );
		levelObject_wrong.Add ("frog_3", levelPositions.Pop() );
		levelObject_wrong.Add ("cat_1", levelPositions.Pop() );
		levelObject_wrong.Add ("cat_2", levelPositions.Pop() );
		levelObject_wrong.Add ("cat_3", levelPositions.Pop() );
		levelObject_wrong.Add ("cow_1", levelPositions.Pop() );
		levelObject_wrong.Add ("cow_2", levelPositions.Pop() );
		levelObject_wrong.Add ("cow_3", levelPositions.Pop() );
		levelObject_wrong.Add ("dog_1", levelPositions.Pop() );
		levelObject_wrong.Add ("dog_2", levelPositions.Pop() );
		levelObject_wrong.Add ("dog_3", levelPositions.Pop() );
		levelObject_wrong.Add ("horse_1", levelPositions.Pop() );
		levelObject_wrong.Add ("horse_2", levelPositions.Pop() );
		levelObject_wrong.Add ("horse_3", levelPositions.Pop() );

		
		
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

	private void LoadLevel15()
	{
		Stack<Vector3> levelPositions = randomPosition(33);
		
		Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
		levelObject.Add ("train_1", levelPositions.Pop());
		levelObject.Add ("train_2", levelPositions.Pop());
		levelObject.Add ("plane_1", levelPositions.Pop());
		levelObject.Add ("plane_2", levelPositions.Pop());
		levelObject.Add ("plane_3", levelPositions.Pop());
		levelObject.Add ("rocketship_1", levelPositions.Pop());
		
		foreach (KeyValuePair<string, Vector3> pair in levelObject)
		{
			Vector3 position = pair.Value;
			Quaternion rotation = Quaternion.identity;
			Transform getChild = itemPicture.transform.FindChild("Sprite");
			GameObject child = getChild.gameObject;
			child.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LevelContentUpdates/" + pair.Key);
			Instantiate(itemPicture, position, rotation);
		}
		
		Dictionary<string, Vector3> levelObject_wrong = new Dictionary<string, Vector3>();
		levelObject_wrong.Add ("bed_1", levelPositions.Pop() );
		levelObject_wrong.Add ("bed_2", levelPositions.Pop() );
		levelObject_wrong.Add ("bed_3", levelPositions.Pop() );
		levelObject_wrong.Add ("trafficlight_1", levelPositions.Pop() );
		levelObject_wrong.Add ("trafficlight_2", levelPositions.Pop() );
		levelObject_wrong.Add ("van_1", levelPositions.Pop() );
		levelObject_wrong.Add ("car_1", levelPositions.Pop() );
		levelObject_wrong.Add ("car_2", levelPositions.Pop() );
		levelObject_wrong.Add ("car_3", levelPositions.Pop() );
		levelObject_wrong.Add ("car_4", levelPositions.Pop() );
		levelObject_wrong.Add ("frog_1", levelPositions.Pop() );
		levelObject_wrong.Add ("frog_2", levelPositions.Pop() );
		levelObject_wrong.Add ("frog_3", levelPositions.Pop() );
		levelObject_wrong.Add ("cat_1", levelPositions.Pop() );
		levelObject_wrong.Add ("cat_2", levelPositions.Pop() );
		levelObject_wrong.Add ("cat_3", levelPositions.Pop() );
		levelObject_wrong.Add ("cow_1", levelPositions.Pop() );
		levelObject_wrong.Add ("cow_2", levelPositions.Pop() );
		levelObject_wrong.Add ("cow_3", levelPositions.Pop() );
		levelObject_wrong.Add ("dog_1", levelPositions.Pop() );
		levelObject_wrong.Add ("dog_2", levelPositions.Pop() );
		levelObject_wrong.Add ("dog_3", levelPositions.Pop() );
		levelObject_wrong.Add ("horse_1", levelPositions.Pop() );
		levelObject_wrong.Add ("horse_2", levelPositions.Pop() );
		levelObject_wrong.Add ("horse_3", levelPositions.Pop() );
		
		
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

	private void LoadLevel16()
	{
		Stack<Vector3> levelPositions = randomPosition(18);
		
		Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
		levelObject.Add ("lv16_1", levelPositions.Pop());
		levelObject.Add ("lv16_2", levelPositions.Pop());
		levelObject.Add ("lv16_3", levelPositions.Pop());
		
		foreach (KeyValuePair<string, Vector3> pair in levelObject)
		{
			Vector3 position = pair.Value;
			Quaternion rotation = Quaternion.identity;
			Transform getChild = itemText.transform.FindChild("Sprite");
			GameObject child = getChild.gameObject;
			child.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LevelContentText/" + pair.Key);
			Instantiate(itemText, position, rotation);
		}
		
		Dictionary<string, Vector3> levelObject_wrong = new Dictionary<string, Vector3>();
		levelObject_wrong.Add ("lv16_4", new Vector3());
		levelObject_wrong.Add ("lv16_5", new Vector3());
		levelObject_wrong.Add ("lv16_6", new Vector3());
		levelObject_wrong.Add ("lv16_7", new Vector3());
		levelObject_wrong.Add ("lv16_8", new Vector3());
				
		for (int i=0; i<3; i++) {
			foreach (KeyValuePair<string, Vector3> pair in levelObject_wrong)
			{
				Vector3 position = levelPositions.Pop();
				Quaternion rotation = Quaternion.identity;
				Transform getChild = itemText_bad.transform.FindChild("Sprite");
				GameObject child = getChild.gameObject;
				child.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LevelContentText/" + pair.Key);
				Instantiate(itemText_bad, position, rotation);
			}
		}
	}

	private void LoadLevel17()
	{
		Stack<Vector3> levelPositions = randomPosition(18);
		
		Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
		levelObject.Add ("lv17_1", levelPositions.Pop());
		levelObject.Add ("lv17_2", levelPositions.Pop());
		levelObject.Add ("lv17_3", levelPositions.Pop());
		
		foreach (KeyValuePair<string, Vector3> pair in levelObject)
		{
			Vector3 position = pair.Value;
			Quaternion rotation = Quaternion.identity;
			Transform getChild = itemText.transform.FindChild("Sprite");
			GameObject child = getChild.gameObject;
			child.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LevelContentText/" + pair.Key);
			Instantiate(itemText, position, rotation);
		}
		
		Dictionary<string, Vector3> levelObject_wrong = new Dictionary<string, Vector3>();
		levelObject_wrong.Add ("lv17_4", new Vector3());
		levelObject_wrong.Add ("lv17_5", new Vector3());
		levelObject_wrong.Add ("lv17_6", new Vector3());
		levelObject_wrong.Add ("lv17_7", new Vector3());
		levelObject_wrong.Add ("lv17_8", new Vector3());

		for (int i=0; i<3; i++) {
			foreach (KeyValuePair<string, Vector3> pair in levelObject_wrong)
			{
				Vector3 position = levelPositions.Pop();
				Quaternion rotation = Quaternion.identity;
				Transform getChild = itemText_bad.transform.FindChild("Sprite");
				GameObject child = getChild.gameObject;
				child.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LevelContentText/" + pair.Key);
				Instantiate(itemText_bad, position, rotation);
			}
		}
	}

	private void LoadLevel18()
	{
		Stack<Vector3> levelPositions = randomPosition(15);
		
		Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
		levelObject.Add ("lv18_1", levelPositions.Pop());
		levelObject.Add ("lv18_2", levelPositions.Pop());
		levelObject.Add ("lv18_3", levelPositions.Pop());
		
		foreach (KeyValuePair<string, Vector3> pair in levelObject)
		{
			Vector3 position = pair.Value;
			Quaternion rotation = Quaternion.identity;
			Transform getChild = itemText.transform.FindChild("Sprite");
			GameObject child = getChild.gameObject;
			child.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LevelContentText/" + pair.Key);
			Instantiate(itemText, position, rotation);
		}
		
		Dictionary<string, Vector3> levelObject_wrong = new Dictionary<string, Vector3>();
		levelObject_wrong.Add ("lv18_4", new Vector3());
		levelObject_wrong.Add ("lv18_5", new Vector3());
		levelObject_wrong.Add ("lv18_6", new Vector3());
		levelObject_wrong.Add ("lv18_7", new Vector3());
		levelObject_wrong.Add ("lv18_8", new Vector3());

		levelObject_wrong.Add ("lv18_9", new Vector3());

		for (int i=0; i<2; i++) {
			foreach (KeyValuePair<string, Vector3> pair in levelObject_wrong) {
				Vector3 position = levelPositions.Pop();
				Quaternion rotation = Quaternion.identity;
				Transform getChild = itemText_bad.transform.FindChild ("Sprite");
				GameObject child = getChild.gameObject;
				child.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("LevelContentText/" + pair.Key);
				Instantiate (itemText_bad, position, rotation);
			}
		}
	}

	/*********************************************END LEVELS EDIT**************************************************/
	/*********************************************END LEVELS EDIT**************************************************/

	private Dictionary<string, Vector3> randomWrong(int length, string levelWord, Stack<Vector3> positions)
	{
		Dictionary<string, Vector3> randomWrong = new Dictionary<string, Vector3>();
		List<string> allObjects = levelContent();
		
		for (int i = 0; i < length; i++)
		{
			string current = allObjects[Random.Range(0, allObjects.Count)];
			
			while (current.Contains(levelWord) || randomWrong.ContainsKey(current))
				current = allObjects[Random.Range(0, allObjects.Count)];
			
			randomWrong.Add(current, positions.Pop());
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

		positions.Add (new Vector3(Random.Range (-40, 40), 0 ,Random.Range (-40, 40)));

		while (qnt > 0) {
			testPosition = new Vector3(Random.Range (-40, 40), 0 ,Random.Range (-40, 40)); 
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
