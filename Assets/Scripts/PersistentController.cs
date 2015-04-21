using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class PersistentController : MonoBehaviour {
	
	public static Dictionary<int, int> progress = new Dictionary<int, int>();

	AudioSource fxSound;

	private GameObject Canvas;
	private GameObject InputCanvas;
	private GameObject InputField;
	private GameObject levels;
	private GameObject Session;
	
	private string filename;
	private string session = "";
	private string time;
	private string levelLog = "";

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}
	
	// Use this for initialization
	void Start () {

		fxSound = GetComponent<AudioSource> ();
		fxSound.Play ();

		InputCanvas = GameObject.Find("InputCanvas");
		Transform getInputField = InputCanvas.transform.FindChild("InputField");
		InputField = getInputField.gameObject;
		progress.Clear();
		progress.Add(1, 0);
		progress.Add(2, 0);
		progress.Add(3, 0);
		progress.Add(4, 0);
		progress.Add(5, 0);
		progress.Add(6, 0);
        progress.Add(7, 0);
        progress.Add(8, 0);
        progress.Add(9, 0);
        progress.Add(10, 0);
        progress.Add(11, 0);
        progress.Add(12, 0);
		progress.Add(13, 0);
		progress.Add(14, 0);
		progress.Add(15, 0);
		progress.Add(16, 0);
		progress.Add(17, 0);
		progress.Add(18, 0);

	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (Application.persistentDataPath);
		
		if (Application.loadedLevel == 0) {
			session = InputField.GetComponent<UnityEngine.UI.InputField>().text;

			filename = Application.persistentDataPath +"/"+ session.ToUpper() + ".txt";
		} else {
			time = Time.timeSinceLevelLoad.ToString ("00.00");
		}
		
		if (Application.loadedLevel == 1) {
			Canvas = GameObject.Find ("Canvas Select");
			levels = GameObject.Find ("Levels");
			foreach (KeyValuePair<int, int> pair in progress) {
				Transform getChild = Canvas.transform.FindChild ("Progress_Bar" + pair.Key);
				GameObject child = getChild.gameObject;
				child.SetActive (false);
				Transform getLevelChild = levels.transform.FindChild (pair.Key + "/Background");
				GameObject childLevel = getLevelChild.gameObject;
				if (pair.Value == 3) {
					childLevel.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/won");
				} else if (pair.Value != 0) {
					childLevel.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/notWon");
					child.SetActive (true);
					child.GetComponent<Scrollbar> ().size = pair.Value / 3f;
				}
			}
		} 
	}
	
	public void UpdateScore(int level, int score)
	{
		level = level - 2;
		progress[level] = score;
	}
	
	public string getFileName(){
		return filename;
	}
	
	public string getSessionName(){
		return session.ToUpper ();
	}
	
	public string getTime(){
		return ("[" + time + "]");
	}
	
	public void createFile(){
		//if (!File.Exists (filename))
		//	File.Create (filename);
		
		StartCoroutine(writeFileName());
	}

	IEnumerator writeFileName()
	{
		yield return new WaitForSeconds(0.5f);
		Debug.Log("FILE CREATED AT "+Application.persistentDataPath);
        //File.AppendAllText(getFileName(),"session started " + getSessionName() + " " + SystemInfo.deviceType.ToString() + " " + SystemInfo.operatingSystem);
		postHTML("session started " + getSessionName() + " " + SystemInfo.deviceType.ToString() + " " + SystemInfo.operatingSystem+ " ");
	}

	public void AddLevelLog(string log){
		levelLog = levelLog + log;
	}

	public string returnLevelLog(){
		string log = levelLog;
		levelLog = "";
		return log;  
	}

	public void postHTML(string value){
		string url = "http://nil.cs.uno.edu/studies/readingrocket/log.php";

		WWWForm form = new WWWForm();
		form.AddField("log", value);
		form.AddField("id", getSessionName());

		WWW www = new WWW (url, form);
		StartCoroutine(WaitForRequest(www));
	}

	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;
		if(www.error == null){
			Debug.Log("WWW OK!: " + www.text);
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}    
	}  

	public bool checkIfComplete(int level){
		if (progress [level] == 3)
			return true;
		else
			return false;
	}

}