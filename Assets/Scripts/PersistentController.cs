using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class PersistentController : MonoBehaviour {

    public static Dictionary<int, int> progress = new Dictionary<int, int>();
	private GameObject Canvas;
	private GameObject InputCanvas;
    private GameObject levels;
	private GameObject Session;

	private string filename;
	private string session;

    void Awake()
    {
       // DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	void Start () {

		InputCanvas = GameObject.Find("InputCanvas");
		Transform getInputField = InputCanvas.transform.FindChild("InputField");
		GameObject inputField = getInputField.gameObject;
		session = inputField.GetComponent<UnityEngine.UI.InputField>().text;

		filename = "Assets/" + session + ".txt";
 
		if (!File.Exists (filename))
			File.Create (filename);

		progress.Add(1, 0);
		progress.Add(2, 0);
		progress.Add(3, 0);
		progress.Add(4, 0);
		progress.Add(5, 0);
		progress.Add(6, 0);

	}
	
	// Update is called once per frame
	void Update () {



        if (Application.loadedLevel == 1)
        {
            Canvas = GameObject.Find("Canvas Select");
            levels = GameObject.Find("Levels");
            foreach (KeyValuePair<int, int> pair in progress)
            {
                Transform getChild = Canvas.transform.FindChild("Progress_Bar" + pair.Key);
                GameObject child = getChild.gameObject;
                child.SetActive(false);
                Transform getLevelChild = levels.transform.FindChild(pair.Key + "/Background");
                GameObject childLevel = getLevelChild.gameObject;
                if (pair.Value == 3)
                {
                    childLevel.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/won");
                }
                else if (pair.Value != 0)
                {
                    childLevel.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/notWon");
                    child.SetActive(true);
                    child.GetComponent<Scrollbar>().size = pair.Value / 3f;
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
}
