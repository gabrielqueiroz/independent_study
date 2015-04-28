using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class StartAnimation : MonoBehaviour {

	private GameObject warnLabel;
	private GameObject cam;
	private GameObject startButton;
	private PersistentController persistent;
	private Vector3 goalPosition = new Vector3(0.0f,15.0f,-4.0f);
	private bool clicked = false;
	private bool shake = false;
	private List<string> codes;
	
	// Use this for initialization
	void Start () {
		codes = new List<string>();
		// Test File
		codes.Add("TEST");
		// Students Codes
		codes.Add("11158");
		codes.Add("11165");
		codes.Add("11179");
		codes.Add("11230");
		codes.Add("11249");
		codes.Add("11267");
		codes.Add("11310");
		codes.Add("11405");
		codes.Add("11466");
		codes.Add("11484");
		codes.Add("11544");
		codes.Add("11551");
		codes.Add("11555");
		codes.Add("11562");
		codes.Add("11568");
		codes.Add("11585");
		codes.Add("11643");
		codes.Add("11650");
		codes.Add("11652");
		codes.Add("11658");
		codes.Add("11659");
		codes.Add("11674");
		codes.Add("11722");
		codes.Add("11745");
		codes.Add("11896");
		codes.Add("12182");
		codes.Add("12213");
		codes.Add("12228");
		codes.Add("12301");
		codes.Add("12379");
		codes.Add("12381");
		codes.Add("12455");
		codes.Add("12626");
		codes.Add("12664");
		codes.Add("12693");
		codes.Add("12782");
		codes.Add("12789");
		codes.Add("12916");
		codes.Add("13162");
		codes.Add("13235");
		codes.Add("13253");
		codes.Add("13304");
		codes.Add("13362");
		codes.Add("13379");
		codes.Add("13515");
		codes.Add("13527");
		codes.Add("13588");
		codes.Add("13642");
		codes.Add("13687");
		codes.Add("13740");
		codes.Add("13765");
		codes.Add("13810");
		codes.Add("13885");
		codes.Add("14058");
		codes.Add("14152");
		codes.Add("14203");
		codes.Add("14212");
		codes.Add("14215");
		codes.Add("14293");
		codes.Add("14314");
		codes.Add("14350");
		codes.Add("14358");
		codes.Add("14417");
		codes.Add("14483");
		codes.Add("14504");
		codes.Add("14505");
		codes.Add("14591");
		codes.Add("14610");
		codes.Add("14610");
		codes.Add("14656");
		codes.Add("14717");
		codes.Add("14748");
		codes.Add("14767");
		codes.Add("14768");
		codes.Add("14928");
		codes.Add("14965");
		codes.Add("14999");
		codes.Add("15027");
		codes.Add("15082");
		codes.Add("15106");
		codes.Add("15140");
		codes.Add("15213");
		codes.Add("15266");
		codes.Add("15464");
		codes.Add("15497");
		codes.Add("15515");
		codes.Add("15529");
		codes.Add("15553");
		codes.Add("15671");
		codes.Add("15673");
		codes.Add("15785");
		codes.Add("15833");
		codes.Add("15866");
		codes.Add("15894");
		codes.Add("15947");
		codes.Add("16021");
		codes.Add("16274");
		codes.Add("16291");
		codes.Add("16298");
		codes.Add("16303");
		codes.Add("16307");
		codes.Add("16318");
		codes.Add("16344");
		codes.Add("16423");
		codes.Add("16491");
		codes.Add("16594");
		codes.Add("16686");
		codes.Add("16706");
		codes.Add("16716");
		codes.Add("16765");
		codes.Add("16797");
		codes.Add("16852");
		codes.Add("16907");
		codes.Add("16917");
		codes.Add("16928");
		codes.Add("16997");
		codes.Add("17032");
		codes.Add("17039");
		codes.Add("17079");
		codes.Add("17108");
		codes.Add("17113");
		codes.Add("17174");
		codes.Add("17199");
		codes.Add("17237");
		codes.Add("17283");
		codes.Add("17289");
		codes.Add("17299");
		codes.Add("17318");
		codes.Add("17332");
		codes.Add("17373");
		codes.Add("17496");
		codes.Add("17520");
		codes.Add("17563");
		codes.Add("17565");
		codes.Add("17643");
		codes.Add("17649");
		codes.Add("17747");
		codes.Add("17758");
		codes.Add("17822");
		codes.Add("17937");
		codes.Add("17944");
		codes.Add("18008");
		codes.Add("18110");
		codes.Add("18141");
		codes.Add("18225");
		codes.Add("18257");
		codes.Add("18414");
		codes.Add("18417");
		codes.Add("18432");
		codes.Add("18454");
		codes.Add("18538");
		codes.Add("18739");
		codes.Add("18808");
		codes.Add("18822");
		codes.Add("18837");
		codes.Add("18844");
		codes.Add("19000");
		codes.Add("19004");
		codes.Add("19062");
		codes.Add("19098");
		codes.Add("19171");
		codes.Add("19238");
		codes.Add("19306");
		codes.Add("19337");
		codes.Add("19421");
		codes.Add("19473");
		codes.Add("19483");
		codes.Add("19555");
		codes.Add("19621");
		codes.Add("19647");
		codes.Add("19705");
		codes.Add("19720");
		codes.Add("19819");
		codes.Add("19842");
		codes.Add("19887");
		codes.Add("19936");
		codes.Add("19951");
		codes.Add("20051");
		codes.Add("20095");
		codes.Add("20097");
		codes.Add("20104");
		codes.Add("20129");
		codes.Add("20149");
		codes.Add("20207");
		codes.Add("20218");
		codes.Add("20301");
		codes.Add("20316");
		codes.Add("20396");
		codes.Add("20440");
		codes.Add("20560");
		codes.Add("20606");
		codes.Add("20619");
		codes.Add("20626");
		codes.Add("20683");
		codes.Add("20710");
		codes.Add("20746");
		codes.Add("20769");
		codes.Add("20891");
		codes.Add("20936");
		codes.Add("21023");
		codes.Add("21025");
		codes.Add("21089");
		codes.Add("21095");
		codes.Add("21115");
		codes.Add("21125");
		codes.Add("21163");
		codes.Add("21179");
		codes.Add("21195");
		codes.Add("21196");
		codes.Add("21273");
		codes.Add("21291");
		codes.Add("21303");
		codes.Add("21397");
		codes.Add("21457");
		codes.Add("21591");
		codes.Add("21624");
		codes.Add("21806");
		codes.Add("21902");
		codes.Add("21903");
		codes.Add("21965");
		codes.Add("22065");
		codes.Add("22081");
		codes.Add("22094");
		codes.Add("22139");
		codes.Add("22288");
		codes.Add("22292");
		codes.Add("22310");
		codes.Add("22337");
		codes.Add("22486");
		codes.Add("22493");
		codes.Add("22511");
		codes.Add("22521");
		codes.Add("22543");
		codes.Add("22595");
		codes.Add("22660");
		codes.Add("22754");
		codes.Add("22860");
		codes.Add("22873");
		codes.Add("22889");
		codes.Add("22901");
		codes.Add("22943");
		codes.Add("22965");
		codes.Add("22973");
		codes.Add("22976");
		codes.Add("22994");
		codes.Add("23082");
		codes.Add("23130");
		codes.Add("23140");
		codes.Add("23232");
		codes.Add("23245");
		codes.Add("23277");
		codes.Add("23287");
		codes.Add("23316");
		codes.Add("23339");
		codes.Add("23367");
		codes.Add("23456");
		codes.Add("23475");
		codes.Add("23477");
		codes.Add("23484");
		codes.Add("23623");
		codes.Add("23734");
		codes.Add("23905");
		codes.Add("23996");
		codes.Add("23997");
		codes.Add("24029");
		codes.Add("24034");
		codes.Add("24105");
		codes.Add("24139");
		codes.Add("24155");
		codes.Add("24169");
		codes.Add("24265");
		codes.Add("24313");
		codes.Add("24376");
		codes.Add("24416");
		codes.Add("24421");
		codes.Add("24512");
		codes.Add("24544");
		codes.Add("24557");
		codes.Add("24607");
		codes.Add("24650");
		codes.Add("24677");
		codes.Add("24701");
		codes.Add("24727");
		codes.Add("24743");
		codes.Add("24762");
		codes.Add("24868");
		codes.Add("24923");
		codes.Add("25008");
		codes.Add("25091");
		codes.Add("25256");
		codes.Add("25266");
		codes.Add("25266");
		codes.Add("25267");
		codes.Add("25328");
		codes.Add("25386");
		codes.Add("25539");
		codes.Add("25675");
		codes.Add("25754");
		codes.Add("25779");
		codes.Add("26007");
		codes.Add("26036");
		codes.Add("26157");
		codes.Add("26250");
		codes.Add("26529");
		codes.Add("26588");
		codes.Add("26610");
		codes.Add("26652");
		codes.Add("26668");
		codes.Add("26674");
		codes.Add("26702");
		codes.Add("26748");
		codes.Add("26781");
		codes.Add("26790");
		codes.Add("26820");
		codes.Add("26822");
		codes.Add("26865");
		codes.Add("26941");
		codes.Add("26988");
		codes.Add("27078");
		codes.Add("27126");
		codes.Add("27149");
		codes.Add("27216");
		codes.Add("27219");
		codes.Add("27235");
		codes.Add("27242");
		codes.Add("27262");
		codes.Add("27282");
		codes.Add("27363");
		codes.Add("27464");
		codes.Add("27573");
		codes.Add("27575");
		codes.Add("27578");
		codes.Add("27650");
		codes.Add("27666");
		codes.Add("27695");
		codes.Add("27750");
		codes.Add("27893");
		codes.Add("27902");
		codes.Add("27902");
		codes.Add("27932");
		codes.Add("28110");
		codes.Add("28121");
		codes.Add("28130");
		codes.Add("28210");
		codes.Add("28260");
		codes.Add("28365");
		codes.Add("28372");
		codes.Add("28381");
		codes.Add("28498");
		codes.Add("28513");
		codes.Add("28521");
		codes.Add("28582");
		codes.Add("28597");
		codes.Add("28664");
		codes.Add("28685");
		codes.Add("28688");
		codes.Add("28729");
		codes.Add("28730");
		codes.Add("28756");
		codes.Add("28875");
		codes.Add("28901");
		codes.Add("28965");
		codes.Add("28970");
		codes.Add("28971");
		codes.Add("28993");
		codes.Add("29008");
		codes.Add("29100");
		codes.Add("29236");
		codes.Add("29262");
		codes.Add("29263");
		codes.Add("29284");
		codes.Add("29311");
		codes.Add("29368");
		codes.Add("29579");
		codes.Add("29631");
		codes.Add("29735");
		codes.Add("29747");
		codes.Add("29769");
		codes.Add("29844");
		codes.Add("29889");
		codes.Add("29954");
		codes.Add("29985");
		codes.Add("29987");
		codes.Add("30003");
		codes.Add("30061");
		codes.Add("30227");
		codes.Add("30256");
		codes.Add("30306");
		codes.Add("30330");
		codes.Add("30458");
		codes.Add("30510");
		codes.Add("30532");
		codes.Add("30603");
		codes.Add("30821");
		codes.Add("30900");
		codes.Add("30963");
		codes.Add("30989");
		codes.Add("30992");
		codes.Add("31114");
		codes.Add("31174");
		codes.Add("31174");
		codes.Add("31193");
		codes.Add("31475");
		codes.Add("31481");
		codes.Add("31523");
		codes.Add("31571");
		codes.Add("31591");
		codes.Add("31659");
		codes.Add("31776");
		codes.Add("31861");
		codes.Add("32031");
		codes.Add("32040");
		codes.Add("32129");
		codes.Add("32244");
		codes.Add("32304");
		codes.Add("32338");
		codes.Add("32359");
		codes.Add("32375");
		codes.Add("32417");
		codes.Add("32469");
		codes.Add("32478");
		codes.Add("32523");
		codes.Add("32531");
		codes.Add("32568");
		codes.Add("32603");
		codes.Add("32631");
		codes.Add("32684");
		codes.Add("32686");
		codes.Add("32789");
		codes.Add("32799");
		codes.Add("32840");
		codes.Add("32868");
		codes.Add("32970");
		codes.Add("33165");
		codes.Add("33172");
		codes.Add("33177");
		codes.Add("33196");
		codes.Add("33210");
		codes.Add("33292");
		codes.Add("33317");

		cam = GameObject.Find ("Main Camera");
		startButton = GameObject.Find ("Start Button");
		warnLabel = GameObject.Find ("InputCanvas").transform.FindChild ("WarnLabel").gameObject;
		
		GameObject persistentObject = GameObject.FindGameObjectWithTag("Persistent");
		if (persistentObject != null)
		{
			persistent = persistentObject.GetComponent<PersistentController>();
		}
		if (persistent == null)
		{
			Debug.Log("Cannot find 'Persistent Controller' script");
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		if (clicked)
			AnimationDown ();
		if (shake)
			shakeWarning ();
		if (Input.GetKeyDown ("enter")) {
	
			if (cam.transform.position == goalPosition) {
				if( !codes.Contains(persistent.getSessionName())){
					warnLabel.SetActive(true);
					shake = true;
					StartCoroutine(shakeFalse());			
				} else {
					persistent.createFile();
					Application.LoadLevel(1);
				}
			} else {
				clicked = true;
				Transform getChild = startButton.transform.FindChild ("label");
				GameObject child = getChild.gameObject;
				child.GetComponent<TextMesh> ().text = "Submit Code";
				TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, true, true);
			}

		}
	}

	
	void OnMouseDown() {
		if (cam.transform.position == goalPosition) {
			if( !codes.Contains(persistent.getSessionName())){
				warnLabel.SetActive(true);
				shake = true;
				StartCoroutine(shakeFalse());			
			} else {
				persistent.createFile();
				Application.LoadLevel(1);
			}
		} else {
			clicked = true;
			Transform getChild = startButton.transform.FindChild ("label");
			GameObject child = getChild.gameObject;
			child.GetComponent<TextMesh> ().text = "Submit Code";
			TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, true, true);
		}
	}

	IEnumerator shakeFalse()
	{
		yield return new WaitForSeconds(1.0f);
		warnLabel.transform.position = new Vector3(0,-45f,-29f);
		shake = false;
	}

	void shakeWarning(){
		warnLabel.transform.position = new Vector3(Mathf.Sin(Time.time * 25),-45f,-29f);
	}

	void AnimationDown(){	
		cam.transform.position = Vector3.Lerp (cam.transform.position, goalPosition, (Time.deltaTime * 5));
	}
}