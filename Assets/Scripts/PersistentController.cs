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
    private int currentLevel;

    void Awake(){
        DontDestroyOnLoad(gameObject);
    }
    
    // Use this for initialization
    void Start(){

        fxSound = GetComponent<AudioSource>();
        fxSound.Play();

        InputCanvas = GameObject.Find("InputCanvas");
        Transform getInputField = InputCanvas.transform.FindChild("InputField");
        InputField = getInputField.gameObject;
        progress.Clear();
        for(int i=1; i<=getNumberOfLevels(); i++){
            progress.Add(i, 0);
        }
    }
    
    // Update is called once per frame
    void Update(){
        //Debug.Log (Application.persistentDataPath);
        
        if(Application.loadedLevel == 0){
            session = InputField.GetComponent<UnityEngine.UI.InputField>().text;
            filename = Application.persistentDataPath + "/" + session.ToUpper() + ".txt";
        } else{
            setTime();
        }
        if(Application.loadedLevel == 1){
            Canvas = GameObject.Find("Canvas Select");
            levels = GameObject.Find("Levels");
            foreach(KeyValuePair<int, int> pair in progress){
                Transform getChild = Canvas.transform.FindChild("Progress_Bar" + pair.Key);
                GameObject child = getChild.gameObject;
                child.SetActive(false);
                Transform getLevelChild = levels.transform.FindChild(pair.Key + "/Background");
                GameObject childLevel = getLevelChild.gameObject;
                if(pair.Value == 3){
                    childLevel.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("GameSprites/won");
                } else if(pair.Value != 0){
                    childLevel.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("GameSprites/notWon");
                    child.SetActive(true);
                    child.GetComponent<Scrollbar>().size = pair.Value / 3f;
                }
            }
        } 
    }
    
    public void UpdateScore(int level, int score){
        if(score > progress[level])
            progress[level] = score;
    }
    
    public string getFileName(){
        return filename;
    }
    
    public string getSessionName(){
        return session.ToUpper();
    }
    
    public string getTime(){
        return ("[" + time + "]");
    }

    public void setTime(){
        time = Time.timeSinceLevelLoad.ToString("00.00");
    }

    public void createFile(){
        StartCoroutine(writeFileName());
    }

    public void setCurrentLevel(int level){
        currentLevel = level;
    }

    public int getCurrentLevel(){
        return currentLevel;
    }

    public int getNumberOfLevels(){
        Object[] xmlFiles = Resources.LoadAll("LevelsXML");
        return xmlFiles.Length;
    }

    IEnumerator writeFileName(){
        yield return new WaitForSeconds(0.5f);
        Debug.Log("FILE CREATED AT " + Application.persistentDataPath);
        postHTML("\r\nsession started " + getSessionName() + " " + SystemInfo.deviceType.ToString() + " " + SystemInfo.operatingSystem + " ");
    }

    public void AddLevelLog(string log){
        levelLog += log;
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

        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www));
    }

    IEnumerator WaitForRequest(WWW www){
        yield return www;
        if(www.error == null){
            Debug.Log("WWW OK!: " + www.text);
        } else{
            Debug.Log("WWW Error: " + www.error);
        }    
    }

    public bool checkIfComplete(int level){
        if(progress[level] == 3)
            return true;
        else
            return false;
    }

}