using UnityEngine;
using System.Linq;
using System;
using System.Collections.Generic;

public class AppearedLevels : MonoBehaviour {
    private GameObject[] levels;
    private PersistentController persistent;
    Dictionary<int,GameObject> numbers;

	// Use this for initialization
	void Start () {
        GameObject persistentObject = GameObject.FindGameObjectWithTag("Persistent");
        if(persistentObject != null){
            persistent = persistentObject.GetComponent<PersistentController>();
        }
        if(persistent == null){
            Debug.Log("Cannot find 'Persistent Controller' script");
        }

        levels = GameObject.FindGameObjectsWithTag("Level");
        initDictionary();
        showLevels(persistent.getNumberOfLevels());
	}

    void initDictionary(){
        numbers = new Dictionary<int,GameObject>();
        for(int i=0; i<levels.Length;i++){
            Transform getText = levels[i].transform.FindChild("label");
            GameObject text = getText.gameObject;
            TextMesh label = text.GetComponent<TextMesh>();
            numbers.Add(int.Parse(label.text.Substring(6)),levels[i]);
        }
    }

    void showLevels(int levels){
        foreach(KeyValuePair<int, GameObject> pair in numbers){
            pair.Value.SetActive(false);
        }
        for(int i=1; i<=levels;i++){
            numbers[i].SetActive(true);
        }
    }
}
