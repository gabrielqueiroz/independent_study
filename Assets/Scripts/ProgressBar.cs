using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ProgressBar : MonoBehaviour {

    public static Dictionary<int, int> progress = new Dictionary<int, int>();
    private GameObject Canvas;
    private GameObject Progress;

    void Awake()
    {
        Debug.Log("awake");
       // DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	void Start () {
        Debug.Log("Entrei");
        progress.Add(1, 0);
        progress.Add(2, 0);
        progress.Add(3, 0);
        progress.Add(4, 0);
        progress.Add(5, 0);
        progress.Add(6, 0);
        Progress = GameObject.FindGameObjectWithTag("Progress");

	}
	
	// Update is called once per frame
	void Update () {
        Canvas = GameObject.Find("Canvas Select");

        if (Application.loadedLevel==5)
        {
            Transform getChild = Canvas.transform.FindChild("Progress_Bar1");
            GameObject child = getChild.gameObject;
            child.GetComponent<Scrollbar>().size = progress[1] / 3f;
        }
	}

    public void UpdateScore(int level, int score)
    {
        level--;
        progress[level] = score;
    }

}
