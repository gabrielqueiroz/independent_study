using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Linq;      
using System.Xml.Linq;

public class LevelEditor : MonoBehaviour {
    
    public GameObject itemPicture;
    public GameObject itemPicture_bad;
    public GameObject itemText;
    public GameObject itemText_bad;
    public GameObject itemWord;
    public GameObject itemWord_bad;
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
    
    void Start(){

        Player = GameObject.FindWithTag("Player");
        Canvas = GameObject.Find("Canvas");
        Details = GameObject.Find("Details");
        Notification = GameObject.Find("Notification");
        Notification.SetActive(false);
        winSound = Resources.Load<AudioClip>("Sounds/level-win");

        GameObject persistentObject = GameObject.FindGameObjectWithTag("Persistent");
        if(persistentObject != null){
            persistent = persistentObject.GetComponent<PersistentController>();
        }
        if(persistent == null){
            Debug.Log("Cannot find 'Persistent Controller' script");
        }
        
        if(Application.loadedLevel == 3){
            persistent.setTime();
            persistent.AddLevelLog("\r\n" + persistent.getTime() + " start level 1");
            LoadLevel1();  
        }
        
        if(Application.loadedLevel == 4){
            persistent.setTime();
            persistent.AddLevelLog("\r\n" + persistent.getTime() + " start level 2");
            LoadLevel2();
        }
        
        if(Application.loadedLevel == 5){
            persistent.setTime();
            persistent.AddLevelLog("\r\n" + persistent.getTime() + " start level 3");
            LoadLevel3();
        }
        
        if(Application.loadedLevel == 6){
            persistent.setTime();
            persistent.AddLevelLog("\r\n" + persistent.getTime() + " start level 4");
            LoadLevel4();
        }
        
        if(Application.loadedLevel == 7){
            persistent.setTime();
            persistent.AddLevelLog("\r\n" + persistent.getTime() + " start level 5");
            LoadLevel5();
        }

        if(Application.loadedLevel == 8){
            persistent.setTime();
            persistent.AddLevelLog("\r\n" + persistent.getTime() + " start level 6");
            LoadLevel6();
        }

        if(Application.loadedLevel == 9){
            persistent.setTime();
            persistent.AddLevelLog("\r\n" + persistent.getTime() + " start level 7");
            LoadLevel7();
        }
        
        if(Application.loadedLevel == 10){
            persistent.setTime();
            persistent.AddLevelLog("\r\n" + persistent.getTime() + " start level 8");
            LoadLevel8();
        }
        
        if(Application.loadedLevel == 11){
            persistent.setTime();
            persistent.AddLevelLog("\r\n" + persistent.getTime() + " start level 9");
            LoadLevel9();
        }
        
        if(Application.loadedLevel == 12){
            persistent.setTime();
            persistent.AddLevelLog("\r\n" + persistent.getTime() + " start level 10");
            LoadLevel10();
        }
        
        if(Application.loadedLevel == 13){
            persistent.setTime();
            persistent.AddLevelLog("\r\n" + persistent.getTime() + " start level 11");
            LoadLevel11();
        }

        if(Application.loadedLevel == 14){
            persistent.setTime();
            persistent.AddLevelLog("\r\n" + persistent.getTime() + " start level 12");
            LoadLevel12();
        }

        if(Application.loadedLevel == 15){
            persistent.setTime();
            persistent.AddLevelLog("\r\n" + persistent.getTime() + " start level 13");
            LoadLevel13();
        }

        if(Application.loadedLevel == 16){
            persistent.setTime();
            persistent.AddLevelLog("\r\n" + persistent.getTime() + " start level 14");
            LoadLevel14();
        }

        if(Application.loadedLevel == 17){
            persistent.setTime();
            persistent.AddLevelLog("\r\n" + persistent.getTime() + " start level 15");
            LoadLevel15();
        }

        if(Application.loadedLevel == 18){
            persistent.setTime();
            persistent.AddLevelLog("\r\n" + persistent.getTime() + " start level 16");
            LoadLevel16();
        }

        if(Application.loadedLevel == 19){
            persistent.setTime();
            persistent.AddLevelLog("\r\n" + persistent.getTime() + " start level 17");
            LoadLevel17();
        }

        if(Application.loadedLevel == 20){
            persistent.setTime();
            persistent.AddLevelLog("\r\n" + persistent.getTime() + " start level 18");
            LoadLevel18();
        }

    }
    
    void Update(){

        if(Player.activeSelf)
            HelpUpdate();

        if(life == 0){
            persistent.UpdateScore(Application.loadedLevel, score);
            persistent.AddLevelLog("\r\n" + persistent.getTime() + " lose level " + (Application.loadedLevel - 2) + " score " + score);
            persistent.postHTML(persistent.returnLevelLog());
            Application.LoadLevel(2);
        }
        
        if(score == 3){
            persistent.UpdateScore(Application.loadedLevel, score);
            StartCoroutine(LevelComplete());
        }
           
        if(Input.GetKeyDown(KeyCode.Escape))
            QuitLevel();
    }

    public int getScore(){
        return score;
    }

    public void AddScore(){
        Transform getChild = Canvas.transform.FindChild("Progress_Bar");
        GameObject child = getChild.gameObject;
        score = score + 1;
        child.GetComponent<Scrollbar>().size = score / 3f;
    }

    public void DecScore(){
        if(score <= 3){
            Transform getChild = Canvas.transform.FindChild("Heart_" + life);
            GameObject child = getChild.gameObject;
            child.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load <Sprite>("Sprites/DeadHeart");
            life--;
        }
    }

    public void HelpUpdate(){
        Details.transform.position = Player.transform.position + new Vector3(0, 1.0f, 8.6f);
    }

    IEnumerator LevelComplete(){
        if(!Notification.activeSelf){
            AudioSource.PlayClipAtPoint(winSound, transform.position);
            Instantiate(explosion, Player.transform.position + new Vector3(0, 0, 3f), Player.transform.rotation);
            Instantiate(explosion, Player.transform.position + new Vector3(-3f, 0, 3f), Player.transform.rotation);
            Instantiate(explosion, Player.transform.position + new Vector3(3f, 0, 3f), Player.transform.rotation);
        }

        Notification.SetActive(true);

        while(true){
            yield return new WaitForSeconds(3.0f);
            persistent.AddLevelLog("\r\n" + persistent.getTime() + " win level " + (Application.loadedLevel - 2));
            persistent.postHTML(persistent.returnLevelLog());
            Application.LoadLevel(1);
        }
    }

    public void QuitLevel(){
        Player.SetActive(false);
        Canvas.SetActive(false);
        persistent.UpdateScore(Application.loadedLevel, score);
        persistent.AddLevelLog("\r\n" + persistent.getTime() + " quit level " + (Application.loadedLevel - 2) + " score " + score);
        persistent.postHTML(persistent.returnLevelLog());
        Application.LoadLevel(1);
    }

    /*********************************************START LEVELS EDIT**************************************************/

    private void LoadLevel1(){

        Stack<Vector3> levelPositions = randomPosition(33);
        Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
        List<string> wrongObjects = randomWrong(30, "cat");
        Dictionary<string, Vector3> levelObject_wrong = new Dictionary<string, Vector3>();
        XDocument xdoc = XDocument.Load(Application.dataPath + "/Resources/LevelsXML/Level1.xml");

        var levels = from lvl in xdoc.Descendants("level")
        select new {
            objects = lvl.Descendants("objects")
        };
        
        foreach(var lvl in levels){                   
            var objects = from x in lvl.objects.Descendants("object")
            select new {
                name        =  x.Element("name").Value,
                type        =  System.Convert.ToInt32(x.Element("type").Value),
                goodCollect =  System.Convert.ToInt32(x.Element("goodcollect").Value) != 0,
            };
            int goodObjects = 1;
            foreach(var obj in objects){
                if(obj.goodCollect && goodObjects <= 3){
                    levelObject.Add(obj.name, levelPositions.Pop());
                    goodObjects++;
                }
            }

            foreach(string wrong in wrongObjects){
                foreach(var obj in objects){
                    if(obj.name.Equals(wrong))
                        levelObject_wrong.Add(obj.name, levelPositions.Pop());
                }
            }

        }

        foreach(KeyValuePair<string, Vector3> pair in levelObject){           
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemPicture.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            child.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite>("Images/" + pair.Key);  
            Instantiate(itemPicture, position, rotation);
        }

        foreach(KeyValuePair<string, Vector3> pair in levelObject_wrong){         
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemPicture_bad.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            child.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite>("Images/" + pair.Key);         
            Instantiate(itemPicture_bad, position, rotation);
        }
    }

    private void LoadLevel2(){
        Stack<Vector3> levelPositions = randomPosition(33);
        Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
        List<string> wrongObjects = randomWrong(27, "car");
        Dictionary<string, Vector3> levelObject_wrong = new Dictionary<string, Vector3>();
        XDocument xdoc = XDocument.Load(Application.dataPath + "/Resources/LevelsXML/Level2.xml");
        
        var levels = from lvl in xdoc.Descendants("level")
        select new {
            objects = lvl.Descendants("objects")
        };
        
        foreach(var lvl in levels){                   
            var objects = from x in lvl.objects.Descendants("object")
            select new {
                name        =  x.Element("name").Value,
                type        =  System.Convert.ToInt32(x.Element("type").Value),
                goodCollect =  System.Convert.ToInt32(x.Element("goodcollect").Value) != 0,
            };
            int goodObjects = 1;
            foreach(var obj in objects){
                if(obj.goodCollect && goodObjects <= 6){
                    levelObject.Add(obj.name, levelPositions.Pop());
                    goodObjects++;
                }
            }
            
            foreach(string wrong in wrongObjects){
                foreach(var obj in objects){
                    if(obj.name.Equals(wrong))
                        levelObject_wrong.Add(obj.name, levelPositions.Pop());
                }
            }
            
        }
        
        foreach(KeyValuePair<string, Vector3> pair in levelObject){           
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemPicture.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            child.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite>("Images/" + pair.Key);  
            Instantiate(itemPicture, position, rotation);
        }
                
        foreach(KeyValuePair<string, Vector3> pair in levelObject_wrong){         
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemPicture_bad.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            child.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite>("Images/" + pair.Key);         
            Instantiate(itemPicture_bad, position, rotation);
        }
    }
    
    private void LoadLevel3(){
        Stack<Vector3> levelPositions = randomPosition(33);
        Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
        List<string> wrongObjects = randomWrong(30, "pencil");
        Dictionary<string, Vector3> levelObject_wrong = new Dictionary<string, Vector3>();
        XDocument xdoc = XDocument.Load(Application.dataPath + "/Resources/LevelsXML/Level3.xml");
        
        var levels = from lvl in xdoc.Descendants("level")
        select new {
            objects = lvl.Descendants("objects")
        };
        
        foreach(var lvl in levels){                   
            var objects = from x in lvl.objects.Descendants("object")
            select new {
                name        =  x.Element("name").Value,
                type        =  System.Convert.ToInt32(x.Element("type").Value),
                goodCollect =  System.Convert.ToInt32(x.Element("goodcollect").Value) != 0,
            };
            int goodObjects = 1;
            foreach(var obj in objects){
                if(obj.goodCollect && goodObjects <= 3){
                    levelObject.Add(obj.name, levelPositions.Pop());
                    goodObjects++;
                }
            }
            
            foreach(string wrong in wrongObjects){
                foreach(var obj in objects){
                    if(obj.name.Equals(wrong))
                        levelObject_wrong.Add(obj.name, levelPositions.Pop());
                }
            }
            
        }
        
        foreach(KeyValuePair<string, Vector3> pair in levelObject){           
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemPicture.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            child.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite>("Images/" + pair.Key);  
            Instantiate(itemPicture, position, rotation);
        }
                
        foreach(KeyValuePair<string, Vector3> pair in levelObject_wrong){         
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemPicture_bad.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            child.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite>("Images/" + pair.Key);         
            Instantiate(itemPicture_bad, position, rotation);
        }
    }

    private void LoadLevel4(){
        Stack<Vector3> levelPositions = randomPosition(33);
        Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
        List<string> wrongObjects = randomWrong(27, "frog");
        Dictionary<string, Vector3> levelObject_wrong = new Dictionary<string, Vector3>();
        XDocument xdoc = XDocument.Load(Application.dataPath + "/Resources/LevelsXML/Level4.xml");
        
        var levels = from lvl in xdoc.Descendants("level")
        select new {
            objects = lvl.Descendants("objects")
        };
        
        foreach(var lvl in levels){                   
            var objects = from x in lvl.objects.Descendants("object")
            select new {
                name        =  x.Element("name").Value,
                type        =  System.Convert.ToInt32(x.Element("type").Value),
                goodCollect =  System.Convert.ToInt32(x.Element("goodcollect").Value) != 0,
            };
            int goodObjects = 1;
            foreach(var obj in objects){
                if(obj.goodCollect && goodObjects <= 6){
                    levelObject.Add(obj.name, levelPositions.Pop());
                    goodObjects++;
                }
            }
            
            foreach(string wrong in wrongObjects){
                foreach(var obj in objects){
                    if(obj.name.Equals(wrong))
                        levelObject_wrong.Add(obj.name, levelPositions.Pop());
                }
            }
            
        }
        
        foreach(KeyValuePair<string, Vector3> pair in levelObject){           
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemPicture.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            child.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite>("Images/" + pair.Key);  
            Instantiate(itemPicture, position, rotation);
        }
                
        foreach(KeyValuePair<string, Vector3> pair in levelObject_wrong){         
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemPicture_bad.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            child.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite>("Images/" + pair.Key);         
            Instantiate(itemPicture_bad, position, rotation);
        }
    }

    private void LoadLevel5(){

        Stack<Vector3> levelPositions = randomPosition(33);
        Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
        Dictionary<string, Vector3> levelObject_wrong = new Dictionary<string, Vector3>();
        XDocument xdoc = XDocument.Load(Application.dataPath + "/Resources/LevelsXML/Level5.xml");
        
        var levels = from lvl in xdoc.Descendants("level")
        select new {
            objects = lvl.Descendants("objects")
        };
        
        foreach(var lvl in levels){                   
            var objects = from x in lvl.objects.Descendants("object")
            select new {
                name        =  x.Element("name").Value,
                type        =  System.Convert.ToInt32(x.Element("type").Value),
                goodCollect =  System.Convert.ToInt32(x.Element("goodcollect").Value) != 0,
            };
            int badObjects = 1;
            foreach(var obj in objects){
                if(obj.goodCollect){
                    levelObject.Add(obj.name, levelPositions.Pop());
                } else{
                    if(badObjects <= 30){
                        levelObject_wrong.Add(obj.name, levelPositions.Pop());
                        badObjects++;
                    }
                }
            }
        }

        foreach(KeyValuePair<string, Vector3> pair in levelObject){
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemWord.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            createImageFromWord(getChild, child, pair.Key);
            Instantiate(itemWord, position, rotation);
        }

        
        foreach(KeyValuePair<string, Vector3> pair in levelObject_wrong){
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemWord_bad.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            createImageFromWord(getChild, child, pair.Key);
            Instantiate(itemWord_bad, position, rotation);
        }
    }

    private void LoadLevel6(){
        Stack<Vector3> levelPositions = randomPosition(33);
        Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
        Dictionary<string, Vector3> levelObject_wrong = new Dictionary<string, Vector3>();
        XDocument xdoc = XDocument.Load(Application.dataPath + "/Resources/LevelsXML/Level6.xml");
        
        var levels = from lvl in xdoc.Descendants("level")
        select new {
            objects = lvl.Descendants("objects")
        };
        
        foreach(var lvl in levels){                   
            var objects = from x in lvl.objects.Descendants("object")
            select new {
                name        =  x.Element("name").Value,
                type        =  System.Convert.ToInt32(x.Element("type").Value),
                goodCollect =  System.Convert.ToInt32(x.Element("goodcollect").Value) != 0,
            };
            int badObjects = 1;
            foreach(var obj in objects){
                if(obj.goodCollect){
                    levelObject.Add(obj.name, levelPositions.Pop());
                } else{
                    if(badObjects <= 30){
                        levelObject_wrong.Add(obj.name, levelPositions.Pop());
                        badObjects++;
                    }
                }
            }
        }
        
        foreach(KeyValuePair<string, Vector3> pair in levelObject){
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemWord.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            createImageFromWord(getChild, child, pair.Key);
            Instantiate(itemWord, position, rotation);
        }
                
                
        foreach(KeyValuePair<string, Vector3> pair in levelObject_wrong){
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemWord_bad.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            createImageFromWord(getChild, child, pair.Key);
            Instantiate(itemWord_bad, position, rotation);
        }
    }

    private void LoadLevel7(){
        Stack<Vector3> levelPositions = randomPosition(29);
        Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
        Dictionary<string, Vector3> levelObject_wrong = new Dictionary<string, Vector3>();
        XDocument xdoc = XDocument.Load(Application.dataPath + "/Resources/LevelsXML/Level7.xml");
        
        var levels = from lvl in xdoc.Descendants("level")
        select new {
            objects = lvl.Descendants("objects")
        };
        
        foreach(var lvl in levels){                   
            var objects = from x in lvl.objects.Descendants("object")
            select new {
                name        =  x.Element("name").Value,
                type        =  System.Convert.ToInt32(x.Element("type").Value),
                goodCollect =  System.Convert.ToInt32(x.Element("goodcollect").Value) != 0,
            };
            int badObjects = 1;
            foreach(var obj in objects){
                if(obj.goodCollect){
                    levelObject.Add(obj.name, levelPositions.Pop());
                } else{
                    if(badObjects <= 13){
                        levelObject_wrong.Add(obj.name, new Vector3());
                        badObjects++;
                    }
                }
            }
        }
        
        foreach(KeyValuePair<string, Vector3> pair in levelObject){
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemWord.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            createImageFromWord(getChild, child, pair.Key);
            Instantiate(itemWord, position, rotation);
        }

                
        for(int i=0; i<2; i++){
            foreach(KeyValuePair<string, Vector3> pair in levelObject_wrong){
                Vector3 position = levelPositions.Pop();
                Quaternion rotation = Quaternion.identity;
                Transform getChild = itemWord_bad.transform.FindChild("Sprite");
                GameObject child = getChild.gameObject;
                createImageFromWord(getChild, child, pair.Key);
                Instantiate(itemWord_bad, position, rotation);
            }
        }
    }

    private void LoadLevel8(){
        Stack<Vector3> levelPositions = randomPosition(33);
        Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
        Dictionary<string, Vector3> levelObject_wrong = new Dictionary<string, Vector3>();
        XDocument xdoc = XDocument.Load(Application.dataPath + "/Resources/LevelsXML/Level8.xml");
        
        var levels = from lvl in xdoc.Descendants("level")
        select new {
            objects = lvl.Descendants("objects")
        };
        
        foreach(var lvl in levels){                   
            var objects = from x in lvl.objects.Descendants("object")
            select new {
                name        =  x.Element("name").Value,
                type        =  System.Convert.ToInt32(x.Element("type").Value),
                goodCollect =  System.Convert.ToInt32(x.Element("goodcollect").Value) != 0,
            };
            int badObjects = 1;
            foreach(var obj in objects){
                if(obj.goodCollect){
                    levelObject.Add(obj.name, levelPositions.Pop());
                } else{
                    if(badObjects <= 27){
                        levelObject_wrong.Add(obj.name, levelPositions.Pop());
                        badObjects++;
                    }
                }
            }
        }
        
        foreach(KeyValuePair<string, Vector3> pair in levelObject){
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemWord.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            createImageFromWord(getChild, child, pair.Key);
            Instantiate(itemWord, position, rotation);
        }
        
                
        foreach(KeyValuePair<string, Vector3> pair in levelObject_wrong){
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemWord_bad.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            createImageFromWord(getChild, child, pair.Key);
            Instantiate(itemWord_bad, position, rotation);
        }
    }
        
    private void LoadLevel9(){
        Stack<Vector3> levelPositions = randomPosition(33);
        Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
        Dictionary<string, Vector3> levelObject_wrong = new Dictionary<string, Vector3>();
        XDocument xdoc = XDocument.Load(Application.dataPath + "/Resources/LevelsXML/Level9.xml");
        
        var levels = from lvl in xdoc.Descendants("level")
        select new {
            objects = lvl.Descendants("objects")
        };
        
        foreach(var lvl in levels){                   
            var objects = from x in lvl.objects.Descendants("object")
            select new {
                name        =  x.Element("name").Value,
                type        =  System.Convert.ToInt32(x.Element("type").Value),
                goodCollect =  System.Convert.ToInt32(x.Element("goodcollect").Value) != 0,
            };
            int badObjects = 1;
            foreach(var obj in objects){
                if(obj.goodCollect){
                    levelObject.Add(obj.name, levelPositions.Pop());
                } else{
                    if(badObjects <= 29){
                        levelObject_wrong.Add(obj.name, levelPositions.Pop());
                        badObjects++;
                    }
                }
            }
        }
        
        foreach(KeyValuePair<string, Vector3> pair in levelObject){
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemWord.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            createImageFromWord(getChild, child, pair.Key);
            Instantiate(itemWord, position, rotation);
        }
                
                
        foreach(KeyValuePair<string, Vector3> pair in levelObject_wrong){
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemWord_bad.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            createImageFromWord(getChild, child, pair.Key);
            Instantiate(itemWord_bad, position, rotation);
        }
    }
        
    private void LoadLevel10(){
        Stack<Vector3> levelPositions = randomPosition(33);
        Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
        Dictionary<string, Vector3> levelObject_wrong = new Dictionary<string, Vector3>();
        XDocument xdoc = XDocument.Load(Application.dataPath + "/Resources/LevelsXML/Level10.xml");
        
        var levels = from lvl in xdoc.Descendants("level")
        select new {
            objects = lvl.Descendants("objects")
        };
        
        foreach(var lvl in levels){                   
            var objects = from x in lvl.objects.Descendants("object")
            select new {
                name        =  x.Element("name").Value,
                type        =  System.Convert.ToInt32(x.Element("type").Value),
                goodCollect =  System.Convert.ToInt32(x.Element("goodcollect").Value) != 0,
            };
            int badObjects = 1;
            foreach(var obj in objects){
                if(obj.goodCollect){
                    levelObject.Add(obj.name, levelPositions.Pop());
                } else{
                    if(badObjects <= 30){
                        levelObject_wrong.Add(obj.name, levelPositions.Pop());
                        badObjects++;
                    }
                }
            }
        }
        
        foreach(KeyValuePair<string, Vector3> pair in levelObject){
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemWord.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            createImageFromWord(getChild, child, pair.Key);
            Instantiate(itemWord, position, rotation);
        }
        
                
        foreach(KeyValuePair<string, Vector3> pair in levelObject_wrong){
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemWord_bad.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            createImageFromWord(getChild, child, pair.Key);
            Instantiate(itemWord_bad, position, rotation);
        }
    }
        
    private void LoadLevel11(){
        Stack<Vector3> levelPositions = randomPosition(33);
        Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
        Dictionary<string, Vector3> levelObject_wrong = new Dictionary<string, Vector3>();
        XDocument xdoc = XDocument.Load(Application.dataPath + "/Resources/LevelsXML/Level11.xml");
        
        var levels = from lvl in xdoc.Descendants("level")
        select new {
            objects = lvl.Descendants("objects")
        };
        
        foreach(var lvl in levels){                   
            var objects = from x in lvl.objects.Descendants("object")
            select new {
                name        =  x.Element("name").Value,
                type        =  System.Convert.ToInt32(x.Element("type").Value),
                goodCollect =  System.Convert.ToInt32(x.Element("goodcollect").Value) != 0,
            };
           
            foreach(var obj in objects){
                if(obj.goodCollect)
                    levelObject.Add(obj.name, levelPositions.Pop());
                else
                    levelObject_wrong.Add(obj.name, new Vector3());
            }
        }

        
        foreach(KeyValuePair<string, Vector3> pair in levelObject){
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemWord.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            createImageFromWord(getChild, child, pair.Key);
            Instantiate(itemWord, position, rotation);
        }

                
        for(int i=0; i<5; i++){
            foreach(KeyValuePair<string, Vector3> pair in levelObject_wrong){
                Vector3 position = levelPositions.Pop();
                Quaternion rotation = Quaternion.identity;
                Transform getChild = itemWord_bad.transform.FindChild("Sprite");
                GameObject child = getChild.gameObject;
                createImageFromWord(getChild, child, pair.Key);
                Instantiate(itemWord_bad, position, rotation);
            }
        }
    }

    private void LoadLevel12(){
        Stack<Vector3> levelPositions = randomPosition(33);
        Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
        Dictionary<string, Vector3> levelObject_wrong = new Dictionary<string, Vector3>();
        XDocument xdoc = XDocument.Load(Application.dataPath + "/Resources/LevelsXML/Level12.xml");
        
        var levels = from lvl in xdoc.Descendants("level")
        select new {
            objects = lvl.Descendants("objects")
        };
        
        foreach(var lvl in levels){                   
            var objects = from x in lvl.objects.Descendants("object")
            select new {
                name        =  x.Element("name").Value,
                type        =  System.Convert.ToInt32(x.Element("type").Value),
                goodCollect =  System.Convert.ToInt32(x.Element("goodcollect").Value) != 0,
            };
            int goodObjects = 1;
            int badObjects = 1;
            foreach(var obj in objects){
                if(obj.goodCollect && goodObjects <= 5){
                    levelObject.Add(obj.name, levelPositions.Pop());
                    goodObjects++;
                } else{
                    if(badObjects <= 28){
                        levelObject_wrong.Add(obj.name, levelPositions.Pop());
                        badObjects++;
                    }
                }
            }
        }
        
        foreach(KeyValuePair<string, Vector3> pair in levelObject){           
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemPicture.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            child.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite>("Images/" + pair.Key);  
            Instantiate(itemPicture, position, rotation);
        }
        
        foreach(KeyValuePair<string, Vector3> pair in levelObject_wrong){         
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemPicture_bad.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            child.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite>("Images/" + pair.Key);         
            Instantiate(itemPicture_bad, position, rotation);
        }
    }

    private void LoadLevel13(){
        Stack<Vector3> levelPositions = randomPosition(33);
        Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
        Dictionary<string, Vector3> levelObject_wrong = new Dictionary<string, Vector3>();
        XDocument xdoc = XDocument.Load(Application.dataPath + "/Resources/LevelsXML/Level13.xml");
        
        var levels = from lvl in xdoc.Descendants("level")
        select new {
            objects = lvl.Descendants("objects")
        };
        
        foreach(var lvl in levels){                   
            var objects = from x in lvl.objects.Descendants("object")
            select new {
                name        =  x.Element("name").Value,
                type        =  System.Convert.ToInt32(x.Element("type").Value),
                goodCollect =  System.Convert.ToInt32(x.Element("goodcollect").Value) != 0,
            };
            int badObjects = 1;
            foreach(var obj in objects){
                if(obj.goodCollect){
                    levelObject.Add(obj.name, levelPositions.Pop());
                } else{
                    if(badObjects <= 27){
                        levelObject_wrong.Add(obj.name, levelPositions.Pop());
                        badObjects++;
                    }
                }
            }
        }
        
        foreach(KeyValuePair<string, Vector3> pair in levelObject){           
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemPicture.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            child.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite>("Images/" + pair.Key);  
            Instantiate(itemPicture, position, rotation);
        }
        
        foreach(KeyValuePair<string, Vector3> pair in levelObject_wrong){         
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemPicture_bad.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            child.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite>("Images/" + pair.Key);         
            Instantiate(itemPicture_bad, position, rotation);
        }
    }

    private void LoadLevel14(){
        Stack<Vector3> levelPositions = randomPosition(33);
        Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
        Dictionary<string, Vector3> levelObject_wrong = new Dictionary<string, Vector3>();
        XDocument xdoc = XDocument.Load(Application.dataPath + "/Resources/LevelsXML/Level14.xml");
        
        var levels = from lvl in xdoc.Descendants("level")
        select new {
            objects = lvl.Descendants("objects")
        };
        
        foreach(var lvl in levels){                   
            var objects = from x in lvl.objects.Descendants("object")
            select new {
                name        =  x.Element("name").Value,
                type        =  System.Convert.ToInt32(x.Element("type").Value),
                goodCollect =  System.Convert.ToInt32(x.Element("goodcollect").Value) != 0,
            };
            int badObjects = 1;
            foreach(var obj in objects){
                if(obj.goodCollect){
                    levelObject.Add(obj.name, levelPositions.Pop());
                } else{
                    if(badObjects <= 26){
                        levelObject_wrong.Add(obj.name, levelPositions.Pop());
                        badObjects++;
                    }
                }
            }
        }
        
        foreach(KeyValuePair<string, Vector3> pair in levelObject){           
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemPicture.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            child.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite>("Images/" + pair.Key);  
            Instantiate(itemPicture, position, rotation);
        }
        
        foreach(KeyValuePair<string, Vector3> pair in levelObject_wrong){         
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemPicture_bad.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            child.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite>("Images/" + pair.Key);         
            Instantiate(itemPicture_bad, position, rotation);
        }
    }

    private void LoadLevel15(){
        Stack<Vector3> levelPositions = randomPosition(33);
        Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
        Dictionary<string, Vector3> levelObject_wrong = new Dictionary<string, Vector3>();
        XDocument xdoc = XDocument.Load(Application.dataPath + "/Resources/LevelsXML/Level15.xml");
        
        var levels = from lvl in xdoc.Descendants("level")
        select new {
            objects = lvl.Descendants("objects")
        };
        
        foreach(var lvl in levels){                   
            var objects = from x in lvl.objects.Descendants("object")
            select new {
                name        =  x.Element("name").Value,
                type        =  System.Convert.ToInt32(x.Element("type").Value),
                goodCollect =  System.Convert.ToInt32(x.Element("goodcollect").Value) != 0,
            };
            int badObjects = 1;
            foreach(var obj in objects){
                if(obj.goodCollect){
                    levelObject.Add(obj.name, levelPositions.Pop());
                } else{
                    if(badObjects <= 26){
                        levelObject_wrong.Add(obj.name, levelPositions.Pop());
                        badObjects++;
                    }
                }
            }
        }
        
        foreach(KeyValuePair<string, Vector3> pair in levelObject){           
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemPicture.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            child.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite>("Images/" + pair.Key);  
            Instantiate(itemPicture, position, rotation);
        }
        
        foreach(KeyValuePair<string, Vector3> pair in levelObject_wrong){         
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemPicture_bad.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            child.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite>("Images/" + pair.Key);         
            Instantiate(itemPicture_bad, position, rotation);
        }
    }

    private void LoadLevel16(){
        Stack<Vector3> levelPositions = randomPosition(18);
        Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
        Dictionary<string, Vector3> levelObject_wrong = new Dictionary<string, Vector3>();
        XDocument xdoc = XDocument.Load(Application.dataPath + "/Resources/LevelsXML/Level16.xml");
        
        var levels = from lvl in xdoc.Descendants("level")
        select new {
            objects = lvl.Descendants("objects")
        };
        
        foreach(var lvl in levels){                   
            var objects = from x in lvl.objects.Descendants("object")
            select new {
                name        =  x.Element("name").Value,
                type        =  System.Convert.ToInt32(x.Element("type").Value),
                goodCollect =  System.Convert.ToInt32(x.Element("goodcollect").Value) != 0,
            };

            foreach(var obj in objects){
                if(obj.goodCollect){
                    levelObject.Add(obj.name, levelPositions.Pop());
                } else{

                    levelObject_wrong.Add(obj.name, new Vector3());
                   
                }
            }
        }

        
        foreach(KeyValuePair<string, Vector3> pair in levelObject){
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemText.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            createImageFromText(getChild, child, pair.Key);
            Instantiate(itemText, position, rotation);
        }
                
        for(int i=0; i<3; i++){
            foreach(KeyValuePair<string, Vector3> pair in levelObject_wrong){
                Vector3 position = levelPositions.Pop();
                Quaternion rotation = Quaternion.identity;
                Transform getChild = itemText_bad.transform.FindChild("Sprite");
                GameObject child = getChild.gameObject;
                createImageFromText(getChild, child, pair.Key);
                Instantiate(itemText_bad, position, rotation);
            }
        }
    }
        
    private void LoadLevel17(){
        Stack<Vector3> levelPositions = randomPosition(18);
        Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
        Dictionary<string, Vector3> levelObject_wrong = new Dictionary<string, Vector3>();
        XDocument xdoc = XDocument.Load(Application.dataPath + "/Resources/LevelsXML/Level17.xml");
        
        var levels = from lvl in xdoc.Descendants("level")
        select new {
            objects = lvl.Descendants("objects")
        };
        
        foreach(var lvl in levels){                   
            var objects = from x in lvl.objects.Descendants("object")
            select new {
                name        =  x.Element("name").Value,
                type        =  System.Convert.ToInt32(x.Element("type").Value),
                goodCollect =  System.Convert.ToInt32(x.Element("goodcollect").Value) != 0,
            };
            
            foreach(var obj in objects){
                if(obj.goodCollect){
                    levelObject.Add(obj.name, levelPositions.Pop());
                } else{
                    
                    levelObject_wrong.Add(obj.name, new Vector3());
                    
                }
            }
        }
        
        
        foreach(KeyValuePair<string, Vector3> pair in levelObject){
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemText.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            createImageFromText(getChild, child, pair.Key);
            Instantiate(itemText, position, rotation);
        }
        
        for(int i=0; i<3; i++){
            foreach(KeyValuePair<string, Vector3> pair in levelObject_wrong){
                Vector3 position = levelPositions.Pop();
                Quaternion rotation = Quaternion.identity;
                Transform getChild = itemText_bad.transform.FindChild("Sprite");
                GameObject child = getChild.gameObject;
                createImageFromText(getChild, child, pair.Key);
                Instantiate(itemText_bad, position, rotation);
            }
        }
    }

    private void LoadLevel18(){
        Stack<Vector3> levelPositions = randomPosition(21);
        Dictionary<string, Vector3> levelObject = new Dictionary<string, Vector3>();
        Dictionary<string, Vector3> levelObject_wrong = new Dictionary<string, Vector3>();
        XDocument xdoc = XDocument.Load(Application.dataPath + "/Resources/LevelsXML/Level18.xml");
        
        var levels = from lvl in xdoc.Descendants("level")
        select new {
            objects = lvl.Descendants("objects")
        };
        
        foreach(var lvl in levels){                   
            var objects = from x in lvl.objects.Descendants("object")
            select new {
                name        =  x.Element("name").Value,
                type        =  System.Convert.ToInt32(x.Element("type").Value),
                goodCollect =  System.Convert.ToInt32(x.Element("goodcollect").Value) != 0,
            };
            
            foreach(var obj in objects){
                if(obj.goodCollect){
                    levelObject.Add(obj.name, levelPositions.Pop());
                } else{
                    
                    levelObject_wrong.Add(obj.name, new Vector3());
                    
                }
            }
        }
        
        
        foreach(KeyValuePair<string, Vector3> pair in levelObject){
            Vector3 position = pair.Value;
            Quaternion rotation = Quaternion.identity;
            Transform getChild = itemText.transform.FindChild("Sprite");
            GameObject child = getChild.gameObject;
            createImageFromText(getChild, child, pair.Key);
            Instantiate(itemText, position, rotation);
        }
        
        for(int i=0; i<3; i++){
            foreach(KeyValuePair<string, Vector3> pair in levelObject_wrong){
                Vector3 position = levelPositions.Pop();
                Quaternion rotation = Quaternion.identity;
                Transform getChild = itemText_bad.transform.FindChild("Sprite");
                GameObject child = getChild.gameObject;
                createImageFromText(getChild, child, pair.Key);
                Instantiate(itemText_bad, position, rotation);
            }
        }
    }

    /*********************************************END LEVEL EDIT**************************************************/

    private List<string> randomWrong(int length, string levelWord){
        List<string> randomWrong = new List<string>();
        List<string> allObjects = levelContent();
        
        for(int i = 0; i < length; i++){
            string current = allObjects[Random.Range(0, allObjects.Count)];
            
            while(current.Contains(levelWord) || randomWrong.Contains(current))
                current = allObjects[Random.Range(0, allObjects.Count)];
            
            randomWrong.Add(current);
        }

        return randomWrong;
    }

    private Dictionary<string, Vector3> randomWordsWrong(int length, string levelWord, Stack<Vector3> positions){
        Dictionary<string, Vector3> randomWrong = new Dictionary<string, Vector3>();
        List<string> allObjects = levelContentWord();

        for(int i = 0; i < length; i++){
            string current = allObjects[Random.Range(0, allObjects.Count)];

            while(current.Contains(levelWord) || randomWrong.ContainsKey(current))
                current = allObjects[Random.Range(0, allObjects.Count)];

            randomWrong.Add(current, positions.Pop());
        }

        return randomWrong;
    }

    List<string> levelContent(){
        List<string> levelContent = new List<string>();
        Object[] sprites = Resources.LoadAll("Images");

        foreach(Object o in sprites) 
            levelContent.Add(o.name);
        
        return levelContent;
    }

    List<string> levelContentWord(){
        List<string> levelContent = new List<string>();
        Object[] sprites = Resources.LoadAll("Images");
        
        foreach(Object o in sprites) 
            levelContent.Add(o.name);

        return levelContent;
    }
    
    Stack<Vector3> randomPosition(int qnt){
        List<Vector3> positions = new List<Vector3>();
        Stack<Vector3> stack = new Stack<Vector3>();

        Vector3 spaceShip = new Vector3(0, 0, 0);
        Vector3 testPosition;
        bool validPosition;

        positions.Add(new Vector3(Random.Range(-35, 35), 0, Random.Range(-35, 35)));

        while(qnt > 0){
            testPosition = new Vector3(Random.Range(-35, 35), 0, Random.Range(-35, 35)); 
            validPosition = true;

            foreach(Vector3 pos in positions){
                if((Vector3.Distance(pos, testPosition) <= 10.0f) || (Vector3.Distance(spaceShip, testPosition) <= 10.0f))
                    validPosition = false;              
            }

            if(validPosition){ 
                positions.Add(testPosition); 
                qnt = qnt - 1;
            }
                
        }

        foreach(Vector3 pos in positions){
            stack.Push(pos);
        }
        
        return stack;
    }

    void createImageFromWord(Transform getChild, GameObject child, string word){
        string spriteWord = word.Remove(word.Length - 2);
        float size = spriteWord.Length / 3f;
        Transform text = getChild.transform.FindChild("Text");
        GameObject textObject = text.gameObject;
        child.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite>("Images/Word");
        textObject.GetComponent<TextMesh>().fontSize = Mathf.RoundToInt(100 / size);
        textObject.GetComponent<TextMesh>().text = spriteWord.ToUpper();
    }
    
    void createImageFromText(Transform getChild, GameObject child, string phrase){
        Transform text = getChild.transform.FindChild("Text");
        GameObject textObject = text.gameObject;
        child.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite>("Images/Text");
        textObject.GetComponent<TextMesh>().fontSize = 35;
        textObject.GetComponent<TextMesh>().text = phrase;
    }
}
