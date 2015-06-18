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
    public GameObject details;
    private PersistentController persistent;
    private DestroyByContact destroyByContact;
    private GameObject Player;
    private GameObject Canvas;
    private GameObject Details;
    private GameObject Notification;
    private AudioClip winSound;
    private List<Level> levels;
    public int score = 0;
    public int life = 3;

    public LevelEditor(){
    }
    
    void Start(){

        Player = GameObject.FindWithTag("Player");
        Canvas = GameObject.Find("Canvas");
        Details = GameObject.Find("Details");
        Notification = GameObject.Find("Notification");
        Notification.SetActive(false);
        winSound = Resources.Load<AudioClip>("Sounds/level-win");
        levels = new List<Level>();

        GameObject persistentObject = GameObject.FindGameObjectWithTag("Persistent");
        if(persistentObject != null){
            persistent = persistentObject.GetComponent<PersistentController>();
        }
        if(persistent == null){
            Debug.Log("Cannot find 'Persistent Controller' script");
        }

        LoadLevels();
        persistent.setTime();
        persistent.AddLevelLog("\r\n" + persistent.getTime() + " start level " + persistent.getCurrentLevel());
        LoadLevel(persistent.getCurrentLevel());  


    }
    
    void Update(){

        if(Player.activeSelf)
            HelpUpdate();

        if(life == 0){
            persistent.UpdateScore(persistent.getCurrentLevel(), score);
            persistent.AddLevelLog("\r\n" + persistent.getTime() + " lose level " + persistent.getCurrentLevel() + " score " + score);
            persistent.postHTML(persistent.returnLevelLog());
            Application.LoadLevel(2);
        }
        
        if(score == 3){
            persistent.UpdateScore(persistent.getCurrentLevel(), score);
            StartCoroutine(LevelComplete());
        }
           
        if(Input.GetKeyDown(KeyCode.Escape))
            QuitLevel();
    }

    public void LoadLevels(){
        Object[] xmlFiles = Resources.LoadAll("LevelsXML");
        //DirectoryInfo d = new DirectoryInfo(xmlFilesPath);
        //FileInfo[] files = d.GetFiles("*.xml");
        
        foreach(Object xml in xmlFiles){
            this.LoadFile(xml.name);
        }
        this.levels = this.levels.OrderBy(x => x.id).ToList();

    }

    private void LoadFile(string xml){
        Level objLevel;
        TextAsset textXML = (TextAsset)  Resources.Load("LevelsXML/"+xml);
        XDocument xdoc = XDocument.Parse(textXML.text);
        //XDocument xdoc = XDocument.Load(this.xmlFilesPath + _filename);
        var lvls = from lvl in xdoc.Descendants("level")
        select new {
            id      = System.Convert.ToInt32(lvl.Element("id").Value),
            task    = lvl.Descendants("task"),
            objects = lvl.Descendants("objects")
        };


        foreach(var lvl in lvls){
            objLevel = new Level();
            objLevel.id = lvl.id;
            var tasks = from t in lvl.task
            select new {
                text = t.Element("text").Value,
                sprite = t.Element("image").Value,
                elements = System.Convert.ToInt32(t.Element("elements").Value)
            };

            foreach(var task in tasks){
                objLevel.text = task.text;
                objLevel.sprite = task.sprite;
                objLevel.elements = task.elements;
            }
            
            var objects = from x in lvl.objects.Descendants("object")
            select new {
                name        =  x.Element("name").Value,
                type        =  System.Convert.ToInt32(x.Element("type").Value),
                goodCollect =  System.Convert.ToInt32(x.Element("goodcollect").Value) != 0
            };
            
            foreach(var obj in objects){
                objLevel.type = obj.type;
                if(obj.goodCollect) 
                    objLevel.goodObjects.Add(obj.name, new Vector3());
                else 
                    objLevel.badObjects.Add(obj.name, new Vector3());
            }
            this.levels.Add(objLevel);
        }

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
            child.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load <Sprite>("GameSprites/DeadHeart");
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
            persistent.AddLevelLog("\r\n" + persistent.getTime() + " win level " + persistent.getCurrentLevel());
            persistent.postHTML(persistent.returnLevelLog());
            Application.LoadLevel(1);
        }
    }

    public void QuitLevel(){
        Player.SetActive(false);
        Canvas.SetActive(false);
        persistent.UpdateScore(persistent.getCurrentLevel(), score);
        persistent.AddLevelLog("\r\n" + persistent.getTime() + " quit level " + persistent.getCurrentLevel() + " score " + score);
        persistent.postHTML(persistent.returnLevelLog());
        Application.LoadLevel(1);
    }

    /*********************************************START LEVELS EDIT**************************************************/

    public void LoadLevel(int lvl){
        Level level = levels.ElementAt(lvl - 1);
        Stack<Vector3> levelPositions = randomPosition(level.elements);
        GameObject itemGood;
        GameObject itemBad;
        string description = "";
        Transform getText = details.transform.FindChild("Text");
        GameObject text = getText.gameObject;
        Transform getDescription = details.transform.FindChild("Text Description");
        GameObject textDescription = getDescription.gameObject;
        if(level.text.Contains("Read this story")){
            string temp = level.text.Substring(0,68);
            description=level.text.Remove(0,69);
            level.text = temp;
            text.GetComponent<TextMesh>().fontSize = 70;
            textDescription.GetComponent<TextMesh>().fontSize = 50;
        }
        text.GetComponent<TextMesh>().text = level.text;
        textDescription.GetComponent<TextMesh>().text = description;
        if(level.sprite != null){
            Transform getSprite = details.transform.FindChild("Sprite");
            GameObject sprite = getSprite.gameObject;
            text.GetComponent<TextMesh>().text = level.text;
            sprite.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite>("Images/" + level.sprite);
        }
        if(level.type == 0){
            itemGood = itemPicture;
            itemBad = itemPicture_bad;
        } else{
            if(level.getLenght() < 15){
                itemGood = itemWord;
                itemBad = itemWord_bad;
            } else{
                itemGood = itemText;
                itemBad = itemText_bad;
            }
        }
        int qtdGoodObjects = level.goodObjects.Count;
        if(qtdGoodObjects>3){
            while(level.goodObjects.Count>3){
                int r = Random.Range(0,level.goodObjects.Count);
                string key = level.goodObjects.Keys.ElementAt(r);
                level.goodObjects.Remove(key);
           }
        }
        int qtdBadObjects = level.elements - 3;
        int badObjectsIndex = 0;
        if(qtdBadObjects > level.badObjects.Count){
            badObjectsIndex = qtdBadObjects / level.badObjects.Count;
        }
        foreach(KeyValuePair<string, Vector3> pair in level.goodObjects){
                Vector3 position = levelPositions.Pop();
                Quaternion rotation = Quaternion.identity;
                Transform getChild = itemGood.transform.FindChild("Sprite");
                GameObject child = getChild.gameObject;
                if(itemGood.Equals(itemWord))
                    createImageFromWord(getChild, child, pair.Key);
                else if(itemGood.Equals(itemText))
                    createImageFromText(getChild, child, pair.Key);
                else
                    child.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite>("Images/" + pair.Key);  
                Instantiate(itemGood, position, rotation);
        }
        int bad = 0;
        for(int i = 0; i<= badObjectsIndex;i++){
        foreach(KeyValuePair<string, Vector3> pair in level.badObjects){  
            if(bad < qtdBadObjects){
                Vector3 position = levelPositions.Pop();
                Quaternion rotation = Quaternion.identity;
                Transform getChild = itemBad.transform.FindChild("Sprite");
                GameObject child = getChild.gameObject;
                if(itemBad.Equals(itemWord_bad))
                    createImageFromWord(getChild, child, pair.Key);
                else if(itemBad.Equals(itemText_bad))
                    createImageFromText(getChild, child, pair.Key);
                else
                    child.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite>("Images/" + pair.Key);  
                Instantiate(itemBad, position, rotation);
                bad++;
            }
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
