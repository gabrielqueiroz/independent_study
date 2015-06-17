using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Level {
    public string text;
    public string sprite;
    public int elements;
    public int type;
    public Dictionary<string, Vector3> goodObjects;
    public Dictionary<string, Vector3> badObjects;
    public int id;

    public Level(){
        sprite = null;
        goodObjects = new Dictionary<string, Vector3>();
        badObjects = new Dictionary<string, Vector3>();
    }

}
