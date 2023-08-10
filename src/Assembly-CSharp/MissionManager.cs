using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
	private string xmlName = "Missions.xml";

	public static int totalMissions;

	public static MissionTemplate[] missions;

	public static bool activeBaboonsMission;

	public static bool activeFly_BaboonsMission;

	public static bool activeBoomerang_BaboonsMission;

	public static bool activeGorillaMission;

	public static bool activeFly_GorillaMission;

	public static bool activeKoplje_GorillaMission;

	public static bool activeDiamondsMission;

	public static bool activeCoinsMission;

	public static bool activeDistanceMission;

	public static bool activeBarrelsMission;

	public static bool activeRedDiamondsMission;

	public static bool activeBlueDiamondsMission;

	public static bool activeGreenDiamondsMission;

	public static int NumberOfQuests;

	private TextMesh missionDescription;

	private static MissionManager instance;

	private static int currentLevel;

	private static TextMesh baboonsText;

	private static TextMesh fly_baboonsText;

	private static TextMesh boomerang_baboonsText;

	private static TextMesh gorillaText;

	private static TextMesh fly_gorillaText;

	private static TextMesh koplje_gorillaText;

	private static TextMesh diamondsText;

	private static TextMesh coinsText;

	private static TextMesh distanceText;

	private static TextMesh barrelsText;

	private static TextMesh redDiamondsText;

	private static TextMesh blueDiamondsText;

	private static TextMesh greenDiamondsText;

	private static TextMeshEffects baboonsTextEffects;

	private static TextMeshEffects fly_baboonsTextEffects;

	private static TextMeshEffects boomerang_baboonsTextEffects;

	private static TextMeshEffects gorillaTextEffects;

	private static TextMeshEffects fly_gorillaTextEffects;

	private static TextMeshEffects koplje_gorillaTextEffects;

	private static TextMeshEffects diamondsTextEffects;

	private static TextMeshEffects coinsTextEffects;

	private static TextMeshEffects distanceTextEffects;

	private static TextMeshEffects barrelsTextEffects;

	private static TextMeshEffects redDiamondsTextEffects;

	private static TextMeshEffects blueDiamondsTextEffects;

	private static TextMeshEffects greenDiamondsTextEffects;

	public static bool missionsComplete;

	private static bool postavioFinish;

	private static Renderer aktivnaIkonicaMisija1;

	private static Renderer aktivnaIkonicaMisija2;

	private static Renderer aktivnaIkonicaMisija3;

	private static float previousDistance;

	public static int points3Stars;

	public static MissionManager Instance
	{
		get
		{
			if ((Object)(object)instance == (Object)null)
			{
				instance = Object.FindObjectOfType(typeof(MissionManager)) as MissionManager;
			}
			return instance;
		}
	}

	private void Awake()
	{
		((Object)((Component)this).transform).name = "MissionManager";
		instance = this;
		Object.DontDestroyOnLoad((Object)(object)((Component)this).gameObject);
		((MonoBehaviour)this).StartCoroutine(LoadMissions());
	}

	private IEnumerator LoadMissions()
	{
		_ = string.Empty;
		string result = string.Empty;
		string text = Path.Combine(Application.streamingAssetsPath, xmlName);
		Debug.Log((object)("path: " + text));
		if (text.Contains("://"))
		{
			WWW www = new WWW(text);
			yield return www;
			if (string.IsNullOrEmpty(www.error))
			{
				result = www.text;
			}
			else
			{
				Debug.LogError((object)("Error reading file from path! " + www.error));
			}
		}
		else
		{
			result = File.ReadAllText(text);
		}
		IEnumerable<XElement> source = ((XContainer)XElement.Parse(result)).Elements();
		totalMissions = source.Count();
		missions = new MissionTemplate[totalMissions];
		for (int i = 0; i < totalMissions; i++)
		{
			IEnumerable<XElement> source2 = ((XContainer)source.ElementAt(i)).Elements();
			missions[i] = new MissionTemplate();
			missions[i].level = source.ElementAt(i).Attribute(XName.op_Implicit("level")).Value;
			missions[i].baboons = int.Parse(source2.ElementAt(0).Value);
			missions[i].fly_baboons = int.Parse(source2.ElementAt(1).Value);
			missions[i].boomerang_baboons = int.Parse(source2.ElementAt(2).Value);
			missions[i].gorilla = int.Parse(source2.ElementAt(3).Value);
			missions[i].fly_gorilla = int.Parse(source2.ElementAt(4).Value);
			missions[i].koplje_gorilla = int.Parse(source2.ElementAt(5).Value);
			missions[i].diamonds = int.Parse(source2.ElementAt(6).Value);
			missions[i].coins = int.Parse(source2.ElementAt(7).Value);
			missions[i].distance = int.Parse(source2.ElementAt(8).Value);
			missions[i].barrels = int.Parse(source2.ElementAt(9).Value);
			missions[i].red_diamonds = int.Parse(source2.ElementAt(10).Value);
			missions[i].blue_diamonds = int.Parse(source2.ElementAt(11).Value);
			missions[i].green_diamonds = int.Parse(source2.ElementAt(12).Value);
			missions[i].points = int.Parse(source2.ElementAt(13).Value);
			missions[i].description_en = source2.ElementAt(14).Value;
			missions[i].description_us = source2.ElementAt(15).Value;
			missions[i].description_es = source2.ElementAt(16).Value;
			missions[i].description_ru = source2.ElementAt(17).Value;
			missions[i].description_pt = source2.ElementAt(18).Value;
			missions[i].description_pt_br = source2.ElementAt(19).Value;
			missions[i].description_fr = source2.ElementAt(20).Value;
			missions[i].description_tha = source2.ElementAt(21).Value;
			missions[i].description_zh = source2.ElementAt(22).Value;
			missions[i].description_tzh = source2.ElementAt(23).Value;
			missions[i].description_ger = source2.ElementAt(24).Value;
			missions[i].description_it = source2.ElementAt(25).Value;
			missions[i].description_srb = source2.ElementAt(26).Value;
			missions[i].description_tur = source2.ElementAt(27).Value;
			missions[i].description_kor = source2.ElementAt(28).Value;
		}
		StagesParser.stagesLoaded = true;
	}

	public static void OdrediJezik()
	{
	}

	public static void OdrediMisiju(int level, bool mapa)
	{
		postavioFinish = false;
		NumberOfQuests = 0;
		currentLevel = level;
		if (missions[level].baboons > 0)
		{
			activeBaboonsMission = true;
			NumberOfQuests++;
		}
		else
		{
			activeBaboonsMission = false;
		}
		if (missions[level].fly_baboons > 0)
		{
			activeFly_BaboonsMission = true;
			NumberOfQuests++;
		}
		else
		{
			activeFly_BaboonsMission = false;
		}
		if (missions[level].boomerang_baboons > 0)
		{
			activeBoomerang_BaboonsMission = true;
			NumberOfQuests++;
		}
		else
		{
			activeBoomerang_BaboonsMission = false;
		}
		if (missions[level].gorilla > 0)
		{
			activeGorillaMission = true;
			NumberOfQuests++;
		}
		else
		{
			activeGorillaMission = false;
		}
		if (missions[level].fly_gorilla > 0)
		{
			activeFly_GorillaMission = true;
			NumberOfQuests++;
		}
		else
		{
			activeFly_GorillaMission = false;
		}
		if (missions[level].koplje_gorilla > 0)
		{
			activeKoplje_GorillaMission = true;
			NumberOfQuests++;
		}
		else
		{
			activeKoplje_GorillaMission = false;
		}
		if (missions[level].diamonds > 0)
		{
			activeDiamondsMission = true;
			NumberOfQuests++;
		}
		else
		{
			activeDiamondsMission = false;
		}
		if (missions[level].coins > 0)
		{
			activeCoinsMission = true;
			NumberOfQuests++;
		}
		else
		{
			activeCoinsMission = false;
		}
		if (missions[level].distance > 0)
		{
			activeDistanceMission = true;
			NumberOfQuests++;
		}
		else
		{
			activeDistanceMission = false;
		}
		if (missions[level].barrels > 0)
		{
			activeBarrelsMission = true;
			NumberOfQuests++;
		}
		else
		{
			activeBarrelsMission = false;
		}
		if (missions[level].red_diamonds > 0)
		{
			activeRedDiamondsMission = true;
			NumberOfQuests++;
		}
		else
		{
			activeRedDiamondsMission = false;
		}
		if (missions[level].blue_diamonds > 0)
		{
			activeBlueDiamondsMission = true;
			NumberOfQuests++;
		}
		else
		{
			activeBlueDiamondsMission = false;
		}
		if (missions[level].green_diamonds > 0)
		{
			activeGreenDiamondsMission = true;
			NumberOfQuests++;
		}
		else
		{
			activeGreenDiamondsMission = false;
		}
		missionsComplete = false;
		previousDistance = 0f;
		points3Stars = missions[level].points;
		if (missions[level].IspisiDescriptionNaIspravnomJeziku().Contains("BOSS STAGE"))
		{
			StagesParser.bossStage = true;
		}
		if (mapa)
		{
			OdrediIkoniceNaMapi(level);
		}
		else
		{
			OdrediIkonice(level);
		}
	}

	public static void OdrediIkonice(int level)
	{
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_0662: Unknown result type (might be due to invalid IL or missing references)
		//IL_07df: Unknown result type (might be due to invalid IL or missing references)
		//IL_095c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c18: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d92: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed4: Unknown result type (might be due to invalid IL or missing references)
		//IL_129d: Unknown result type (might be due to invalid IL or missing references)
		//IL_12c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_12e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1051: Unknown result type (might be due to invalid IL or missing references)
		//IL_1312: Unknown result type (might be due to invalid IL or missing references)
		//IL_1336: Unknown result type (might be due to invalid IL or missing references)
		//IL_135a: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_1387: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_13cf: Unknown result type (might be due to invalid IL or missing references)
		Transform transform = GameObject.Find("_GameManager/Gameplay Scena Interface/_TopMissions").transform;
		int num = 1;
		Transform val = null;
		if (activeBaboonsMission)
		{
			((Component)transform.Find("MissionIcons/Babun")).gameObject.SetActive(true);
			switch (num)
			{
			case 1:
			{
				val = transform.Find("TextField Mission1");
				Transform obj3 = val;
				Transform parent = (transform.Find("MissionIcons/Babun").parent = transform.Find("Mission1"));
				obj3.parent = parent;
				break;
			}
			case 2:
			{
				val = transform.Find("TextField Mission2");
				Transform obj2 = val;
				Transform parent = (transform.Find("MissionIcons/Babun").parent = transform.Find("Mission2"));
				obj2.parent = parent;
				break;
			}
			case 3:
			{
				val = transform.Find("TextField Mission3");
				Transform obj = val;
				Transform parent = (transform.Find("MissionIcons/Babun").parent = transform.Find("Mission3"));
				obj.parent = parent;
				break;
			}
			}
			val.localPosition = new Vector3(0f, 0f, -0.1f);
			((Component)val).gameObject.SetActive(true);
			baboonsText = ((Component)val.GetChild(0).GetChild(0)).GetComponent<TextMesh>();
			baboonsText.text = "0/" + missions[level].baboons;
			baboonsTextEffects = ((Component)baboonsText).GetComponent<TextMeshEffects>();
			baboonsTextEffects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			num++;
		}
		if (activeFly_BaboonsMission)
		{
			((Component)transform.Find("MissionIcons/Babun Leteci")).gameObject.SetActive(true);
			switch (num)
			{
			case 1:
			{
				val = transform.Find("TextField Mission1");
				Transform obj6 = val;
				Transform parent = (transform.Find("MissionIcons/Babun Leteci").parent = transform.Find("Mission1"));
				obj6.parent = parent;
				break;
			}
			case 2:
			{
				val = transform.Find("TextField Mission2");
				Transform obj5 = val;
				Transform parent = (transform.Find("MissionIcons/Babun Leteci").parent = transform.Find("Mission2"));
				obj5.parent = parent;
				break;
			}
			case 3:
			{
				val = transform.Find("TextField Mission3");
				Transform obj4 = val;
				Transform parent = (transform.Find("MissionIcons/Babun Leteci").parent = transform.Find("Mission3"));
				obj4.parent = parent;
				break;
			}
			}
			val.localPosition = new Vector3(0f, 0f, -0.1f);
			((Component)val).gameObject.SetActive(true);
			fly_baboonsText = ((Component)val.GetChild(0).GetChild(0)).GetComponent<TextMesh>();
			fly_baboonsText.text = "0/" + missions[level].fly_baboons;
			fly_baboonsTextEffects = ((Component)fly_baboonsText).GetComponent<TextMeshEffects>();
			fly_baboonsTextEffects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			num++;
			if (missions[level].fly_baboons > 0)
			{
				LevelFactory.instance.leteciBabuni_Kvota = (LevelFactory.instance.leteciBabuni_Kvota_locked = 7f / (float)missions[level].fly_baboons);
			}
		}
		if (activeBoomerang_BaboonsMission)
		{
			((Component)transform.Find("MissionIcons/Babun Boomerang")).gameObject.SetActive(true);
			switch (num)
			{
			case 1:
			{
				val = transform.Find("TextField Mission1");
				Transform obj9 = val;
				Transform parent = (transform.Find("MissionIcons/Babun Boomerang").parent = transform.Find("Mission1"));
				obj9.parent = parent;
				break;
			}
			case 2:
			{
				val = transform.Find("TextField Mission2");
				Transform obj8 = val;
				Transform parent = (transform.Find("MissionIcons/Babun Boomerang").parent = transform.Find("Mission2"));
				obj8.parent = parent;
				break;
			}
			case 3:
			{
				val = transform.Find("TextField Mission3");
				Transform obj7 = val;
				Transform parent = (transform.Find("MissionIcons/Babun Boomerang").parent = transform.Find("Mission3"));
				obj7.parent = parent;
				break;
			}
			}
			val.localPosition = new Vector3(0f, 0f, -0.1f);
			((Component)val).gameObject.SetActive(true);
			boomerang_baboonsText = ((Component)val.GetChild(0).GetChild(0)).GetComponent<TextMesh>();
			boomerang_baboonsText.text = "0/" + missions[level].boomerang_baboons;
			boomerang_baboonsTextEffects = ((Component)boomerang_baboonsText).GetComponent<TextMeshEffects>();
			boomerang_baboonsTextEffects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			num++;
			if (missions[level].boomerang_baboons > 0)
			{
				LevelFactory.instance.boomerangBabuni_Kvota = (LevelFactory.instance.boomerangBabuni_Kvota_locked = 7f / (float)missions[level].boomerang_baboons);
			}
		}
		if (activeGorillaMission)
		{
			((Component)transform.Find("MissionIcons/Gorila")).gameObject.SetActive(true);
			switch (num)
			{
			case 1:
			{
				val = transform.Find("TextField Mission1");
				Transform obj12 = val;
				Transform parent = (transform.Find("MissionIcons/Gorila").parent = transform.Find("Mission1"));
				obj12.parent = parent;
				break;
			}
			case 2:
			{
				val = transform.Find("TextField Mission2");
				Transform obj11 = val;
				Transform parent = (transform.Find("MissionIcons/Gorila").parent = transform.Find("Mission2"));
				obj11.parent = parent;
				break;
			}
			case 3:
			{
				val = transform.Find("TextField Mission3");
				Transform obj10 = val;
				Transform parent = (transform.Find("MissionIcons/Gorila").parent = transform.Find("Mission3"));
				obj10.parent = parent;
				break;
			}
			}
			val.localPosition = new Vector3(0f, 0f, -0.1f);
			((Component)val).gameObject.SetActive(true);
			gorillaText = ((Component)val.GetChild(0).GetChild(0)).GetComponent<TextMesh>();
			gorillaText.text = "0/" + missions[level].gorilla;
			gorillaTextEffects = ((Component)gorillaText).GetComponent<TextMeshEffects>();
			gorillaTextEffects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			num++;
		}
		if (activeFly_GorillaMission)
		{
			((Component)transform.Find("MissionIcons/Gorila Leteca")).gameObject.SetActive(true);
			switch (num)
			{
			case 1:
			{
				val = transform.Find("TextField Mission1");
				Transform obj15 = val;
				Transform parent = (transform.Find("MissionIcons/Gorila Leteca").parent = transform.Find("Mission1"));
				obj15.parent = parent;
				break;
			}
			case 2:
			{
				val = transform.Find("TextField Mission2");
				Transform obj14 = val;
				Transform parent = (transform.Find("MissionIcons/Gorila Leteca").parent = transform.Find("Mission2"));
				obj14.parent = parent;
				break;
			}
			case 3:
			{
				val = transform.Find("TextField Mission3");
				Transform obj13 = val;
				Transform parent = (transform.Find("MissionIcons/Gorila Leteca").parent = transform.Find("Mission3"));
				obj13.parent = parent;
				break;
			}
			}
			val.localPosition = new Vector3(0f, 0f, -0.1f);
			((Component)val).gameObject.SetActive(true);
			fly_gorillaText = ((Component)val.GetChild(0).GetChild(0)).GetComponent<TextMesh>();
			fly_gorillaText.text = "0/" + missions[level].fly_gorilla;
			fly_gorillaTextEffects = ((Component)fly_gorillaText).GetComponent<TextMeshEffects>();
			fly_gorillaTextEffects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			num++;
			if (missions[level].fly_gorilla > 0)
			{
				LevelFactory.instance.leteceGorile_Kvota = (LevelFactory.instance.leteceGorile_Kvota_locked = 7f / (float)missions[level].fly_gorilla);
			}
		}
		if (activeKoplje_GorillaMission)
		{
			((Component)transform.Find("MissionIcons/Gorila Sa Kopljem")).gameObject.SetActive(true);
			switch (num)
			{
			case 1:
			{
				val = transform.Find("TextField Mission1");
				Transform obj18 = val;
				Transform parent = (transform.Find("MissionIcons/Gorila Sa Kopljem").parent = transform.Find("Mission1"));
				obj18.parent = parent;
				break;
			}
			case 2:
			{
				val = transform.Find("TextField Mission2");
				Transform obj17 = val;
				Transform parent = (transform.Find("MissionIcons/Gorila Sa Kopljem").parent = transform.Find("Mission2"));
				obj17.parent = parent;
				break;
			}
			case 3:
			{
				val = transform.Find("TextField Mission3");
				Transform obj16 = val;
				Transform parent = (transform.Find("MissionIcons/Gorila Sa Kopljem").parent = transform.Find("Mission3"));
				obj16.parent = parent;
				break;
			}
			}
			val.localPosition = new Vector3(0f, 0f, -0.1f);
			((Component)val).gameObject.SetActive(true);
			koplje_gorillaText = ((Component)val.GetChild(0).GetChild(0)).GetComponent<TextMesh>();
			koplje_gorillaText.text = "0/" + missions[level].koplje_gorilla;
			koplje_gorillaTextEffects = ((Component)koplje_gorillaText).GetComponent<TextMeshEffects>();
			koplje_gorillaTextEffects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			num++;
			if (missions[level].koplje_gorilla > 0)
			{
				LevelFactory.instance.kopljeGorile_Kvota = (LevelFactory.instance.kopljeGorile_Kvota_locked = 7f / (float)missions[level].koplje_gorilla);
			}
		}
		if (activeDiamondsMission)
		{
			((Component)transform.Find("MissionIcons/Svi Dijamanti")).gameObject.SetActive(true);
			switch (num)
			{
			case 1:
			{
				val = transform.Find("TextField Mission1");
				Transform obj21 = val;
				Transform parent = (transform.Find("MissionIcons/Svi Dijamanti").parent = transform.Find("Mission1"));
				obj21.parent = parent;
				break;
			}
			case 2:
			{
				val = transform.Find("TextField Mission2");
				Transform obj20 = val;
				Transform parent = (transform.Find("MissionIcons/Svi Dijamanti").parent = transform.Find("Mission2"));
				obj20.parent = parent;
				break;
			}
			case 3:
			{
				val = transform.Find("TextField Mission3");
				Transform obj19 = val;
				Transform parent = (transform.Find("MissionIcons/Svi Dijamanti").parent = transform.Find("Mission3"));
				obj19.parent = parent;
				break;
			}
			}
			val.localPosition = new Vector3(0f, 0f, -0.1f);
			((Component)val).gameObject.SetActive(true);
			diamondsText = ((Component)val.GetChild(0).GetChild(0)).GetComponent<TextMesh>();
			diamondsText.text = "0/" + missions[level].diamonds;
			diamondsTextEffects = ((Component)diamondsText).GetComponent<TextMeshEffects>();
			diamondsTextEffects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			num++;
		}
		if (activeCoinsMission)
		{
			((Component)transform.Find("MissionIcons/Coin")).gameObject.SetActive(true);
			switch (num)
			{
			case 1:
			{
				val = transform.Find("TextFieldSplit Mission1");
				Transform obj24 = val;
				Transform parent = (transform.Find("MissionIcons/Coin").parent = transform.Find("Mission1"));
				obj24.parent = parent;
				break;
			}
			case 2:
			{
				val = transform.Find("TextFieldSplit Mission2");
				Transform obj23 = val;
				Transform parent = (transform.Find("MissionIcons/Coin").parent = transform.Find("Mission2"));
				obj23.parent = parent;
				break;
			}
			case 3:
			{
				val = transform.Find("TextFieldSplit Mission3");
				Transform obj22 = val;
				Transform parent = (transform.Find("MissionIcons/Coin").parent = transform.Find("Mission3"));
				obj22.parent = parent;
				break;
			}
			}
			val.localPosition = new Vector3(0f, 0f, -0.1f);
			((Component)val).gameObject.SetActive(true);
			((Component)val.GetChild(0).Find("Target Number")).GetComponent<TextMesh>().text = missions[level].coins.ToString();
			((Component)val.GetChild(0).Find("Target Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			coinsText = ((Component)val.GetChild(0).Find("Current Number")).GetComponent<TextMesh>();
			coinsText.text = "0";
			coinsTextEffects = ((Component)coinsText).GetComponent<TextMeshEffects>();
			coinsTextEffects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			num++;
		}
		if (activeDistanceMission)
		{
			((Component)transform.Find("MissionIcons/Distance")).gameObject.SetActive(true);
			switch (num)
			{
			case 1:
			{
				val = transform.Find("TextFieldSplit Mission1");
				Transform obj27 = val;
				Transform parent = (transform.Find("MissionIcons/Distance").parent = transform.Find("Mission1"));
				obj27.parent = parent;
				break;
			}
			case 2:
			{
				val = transform.Find("TextFieldSplit Mission2");
				Transform obj26 = val;
				Transform parent = (transform.Find("MissionIcons/Distance").parent = transform.Find("Mission2"));
				obj26.parent = parent;
				break;
			}
			case 3:
			{
				val = transform.Find("TextFieldSplit Mission3");
				Transform obj25 = val;
				Transform parent = (transform.Find("MissionIcons/Distance").parent = transform.Find("Mission3"));
				obj25.parent = parent;
				break;
			}
			}
			val.localPosition = new Vector3(0f, 0f, -0.1f);
			((Component)val).gameObject.SetActive(true);
			((Component)val.GetChild(0).Find("Target Number")).GetComponent<TextMesh>().text = missions[level].distance.ToString();
			((Component)val.GetChild(0).Find("Target Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			distanceText = ((Component)val.GetChild(0).Find("Current Number")).GetComponent<TextMesh>();
			distanceText.text = "0";
			distanceTextEffects = ((Component)distanceText).GetComponent<TextMeshEffects>();
			distanceTextEffects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			num++;
		}
		if (activeBarrelsMission)
		{
			((Component)transform.Find("MissionIcons/Bure")).gameObject.SetActive(true);
			switch (num)
			{
			case 1:
			{
				val = transform.Find("TextField Mission1");
				Transform obj30 = val;
				Transform parent = (transform.Find("MissionIcons/Bure").parent = transform.Find("Mission1"));
				obj30.parent = parent;
				break;
			}
			case 2:
			{
				val = transform.Find("TextField Mission2");
				Transform obj29 = val;
				Transform parent = (transform.Find("MissionIcons/Bure").parent = transform.Find("Mission2"));
				obj29.parent = parent;
				break;
			}
			case 3:
			{
				val = transform.Find("TextField Mission3");
				Transform obj28 = val;
				Transform parent = (transform.Find("MissionIcons/Bure").parent = transform.Find("Mission3"));
				obj28.parent = parent;
				break;
			}
			}
			val.localPosition = new Vector3(0f, 0f, -0.1f);
			((Component)val).gameObject.SetActive(true);
			barrelsText = ((Component)val.GetChild(0).GetChild(0)).GetComponent<TextMesh>();
			barrelsText.text = "0/" + missions[level].barrels;
			barrelsTextEffects = ((Component)barrelsText).GetComponent<TextMeshEffects>();
			barrelsTextEffects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			num++;
		}
		if (activeRedDiamondsMission)
		{
			((Component)transform.Find("MissionIcons/Crveni Dijamant")).gameObject.SetActive(true);
			switch (num)
			{
			case 1:
			{
				val = transform.Find("TextField Mission1");
				Transform obj33 = val;
				Transform parent = (transform.Find("MissionIcons/Crveni Dijamant").parent = transform.Find("Mission1"));
				obj33.parent = parent;
				break;
			}
			case 2:
			{
				val = transform.Find("TextField Mission2");
				Transform obj32 = val;
				Transform parent = (transform.Find("MissionIcons/Crveni Dijamant").parent = transform.Find("Mission2"));
				obj32.parent = parent;
				break;
			}
			case 3:
			{
				val = transform.Find("TextField Mission3");
				Transform obj31 = val;
				Transform parent = (transform.Find("MissionIcons/Crveni Dijamant").parent = transform.Find("Mission3"));
				obj31.parent = parent;
				break;
			}
			}
			val.localPosition = new Vector3(0f, 0f, -0.1f);
			((Component)val).gameObject.SetActive(true);
			redDiamondsText = ((Component)val.GetChild(0).GetChild(0)).GetComponent<TextMesh>();
			redDiamondsText.text = "0/" + missions[level].red_diamonds;
			redDiamondsTextEffects = ((Component)redDiamondsText).GetComponent<TextMeshEffects>();
			redDiamondsTextEffects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			num++;
			if (missions[level].red_diamonds > 0)
			{
				LevelFactory.instance.crveniDijamant_Kvota = (LevelFactory.instance.crveniDijamant_Kvota_locked = 7f / (float)missions[level].red_diamonds);
			}
		}
		if (activeBlueDiamondsMission)
		{
			((Component)transform.Find("MissionIcons/Plavi Dijamant")).gameObject.SetActive(true);
			switch (num)
			{
			case 1:
			{
				val = transform.Find("TextField Mission1");
				Transform obj36 = val;
				Transform parent = (transform.Find("MissionIcons/Plavi Dijamant").parent = transform.Find("Mission1"));
				obj36.parent = parent;
				break;
			}
			case 2:
			{
				val = transform.Find("TextField Mission2");
				Transform obj35 = val;
				Transform parent = (transform.Find("MissionIcons/Plavi Dijamant").parent = transform.Find("Mission2"));
				obj35.parent = parent;
				break;
			}
			case 3:
			{
				val = transform.Find("TextField Mission3");
				Transform obj34 = val;
				Transform parent = (transform.Find("MissionIcons/Plavi Dijamant").parent = transform.Find("Mission3"));
				obj34.parent = parent;
				break;
			}
			}
			val.localPosition = new Vector3(0f, 0f, -0.1f);
			((Component)val).gameObject.SetActive(true);
			blueDiamondsText = ((Component)val.GetChild(0).GetChild(0)).GetComponent<TextMesh>();
			blueDiamondsText.text = "0/" + missions[level].blue_diamonds;
			blueDiamondsTextEffects = ((Component)blueDiamondsText).GetComponent<TextMeshEffects>();
			blueDiamondsTextEffects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			num++;
			if (missions[level].blue_diamonds > 0)
			{
				LevelFactory.instance.plaviDijamant_Kvota = (LevelFactory.instance.plaviDijamant_Kvota_locked = 7f / (float)missions[level].blue_diamonds);
			}
		}
		if (activeGreenDiamondsMission)
		{
			((Component)transform.Find("MissionIcons/Zeleni Dijamant")).gameObject.SetActive(true);
			switch (num)
			{
			case 1:
			{
				val = transform.Find("TextField Mission1");
				Transform obj39 = val;
				Transform parent = (transform.Find("MissionIcons/Zeleni Dijamant").parent = transform.Find("Mission1"));
				obj39.parent = parent;
				break;
			}
			case 2:
			{
				val = transform.Find("TextField Mission2");
				Transform obj38 = val;
				Transform parent = (transform.Find("MissionIcons/Zeleni Dijamant").parent = transform.Find("Mission2"));
				obj38.parent = parent;
				break;
			}
			case 3:
			{
				val = transform.Find("TextField Mission3");
				Transform obj37 = val;
				Transform parent = (transform.Find("MissionIcons/Zeleni Dijamant").parent = transform.Find("Mission3"));
				obj37.parent = parent;
				break;
			}
			}
			val.localPosition = new Vector3(0f, 0f, -0.1f);
			((Component)val).gameObject.SetActive(true);
			greenDiamondsText = ((Component)val.GetChild(0).GetChild(0)).GetComponent<TextMesh>();
			greenDiamondsText.text = "0/" + missions[level].green_diamonds;
			greenDiamondsTextEffects = ((Component)greenDiamondsText).GetComponent<TextMeshEffects>();
			greenDiamondsTextEffects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			num++;
			if (missions[level].green_diamonds > 0)
			{
				LevelFactory.instance.zeleniDijamant_Kvota = (LevelFactory.instance.zeleniDijamant_Kvota_locked = 7f / (float)missions[level].green_diamonds);
			}
		}
		if (NumberOfQuests == 1)
		{
			transform.Find("Mission1").localPosition = new Vector3(7f, 0f, 0f);
			transform.Find("Mission2").localPosition = new Vector3(0f, 7f, 0f);
			transform.Find("Mission3").localPosition = new Vector3(0f, 7f, 0f);
		}
		else if (NumberOfQuests == 2)
		{
			transform.Find("Mission1").localPosition = new Vector3(4.5f, 0f, 0f);
			transform.Find("Mission2").localPosition = new Vector3(10.5f, 0f, 0f);
			transform.Find("Mission3").localPosition = new Vector3(0f, 7f, 0f);
		}
		else if (NumberOfQuests == 3)
		{
			transform.Find("Mission1").localPosition = new Vector3(1f, 0f, 0f);
			transform.Find("Mission2").localPosition = new Vector3(7f, 0f, 0f);
			transform.Find("Mission3").localPosition = new Vector3(13f, 0f, 0f);
		}
	}

	public static void OdrediIkoniceNaMapi(int level)
	{
		Transform transform = GameObject.FindGameObjectWithTag("Mission").transform;
		int num = 1;
		int num2 = level;
		num2++;
		num2 %= 20;
		if (num2 == 0)
		{
			num2 = 20;
		}
		((Component)transform.Find("Text/Level No")).GetComponent<TextMesh>().text = LanguageManager.Level + " " + num2;
		((Component)transform.Find("Text/Level No")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		if (activeBaboonsMission)
		{
			switch (num)
			{
			case 1:
			{
				if ((Object)(object)aktivnaIkonicaMisija1 != (Object)null)
				{
					aktivnaIkonicaMisija1.enabled = false;
				}
				aktivnaIkonicaMisija1 = ((Component)transform.Find("Mission Icons/Mission 1/Babun")).GetComponent<Renderer>();
				aktivnaIkonicaMisija1.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj2 = transform.Find("Text/Mission 1");
				((Component)obj2).GetComponent<TextMesh>().text = array[0];
				((Component)obj2).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			case 2:
			{
				if ((Object)(object)aktivnaIkonicaMisija2 != (Object)null)
				{
					aktivnaIkonicaMisija2.enabled = false;
				}
				aktivnaIkonicaMisija2 = ((Component)transform.Find("Mission Icons/Mission 2/Babun")).GetComponent<Renderer>();
				aktivnaIkonicaMisija2.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj3 = transform.Find("Text/Mission 2");
				((Component)obj3).GetComponent<TextMesh>().text = array[1];
				((Component)obj3).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			case 3:
			{
				if ((Object)(object)aktivnaIkonicaMisija3 != (Object)null)
				{
					aktivnaIkonicaMisija3.enabled = false;
				}
				aktivnaIkonicaMisija3 = ((Component)transform.Find("Mission Icons/Mission 3/Babun")).GetComponent<Renderer>();
				aktivnaIkonicaMisija3.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj = transform.Find("Text/Mission 3");
				((Component)obj).GetComponent<TextMesh>().text = array[2];
				((Component)obj).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			}
			num++;
		}
		if (activeFly_BaboonsMission)
		{
			switch (num)
			{
			case 1:
			{
				if ((Object)(object)aktivnaIkonicaMisija1 != (Object)null)
				{
					aktivnaIkonicaMisija1.enabled = false;
				}
				aktivnaIkonicaMisija1 = ((Component)transform.Find("Mission Icons/Mission 1/Babun Leteci")).GetComponent<Renderer>();
				aktivnaIkonicaMisija1.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj5 = transform.Find("Text/Mission 1");
				((Component)obj5).GetComponent<TextMesh>().text = array[0];
				((Component)obj5).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			case 2:
			{
				if ((Object)(object)aktivnaIkonicaMisija2 != (Object)null)
				{
					aktivnaIkonicaMisija2.enabled = false;
				}
				aktivnaIkonicaMisija2 = ((Component)transform.Find("Mission Icons/Mission 2/Babun Leteci")).GetComponent<Renderer>();
				aktivnaIkonicaMisija2.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj6 = transform.Find("Text/Mission 2");
				((Component)obj6).GetComponent<TextMesh>().text = array[1];
				((Component)obj6).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			case 3:
			{
				if ((Object)(object)aktivnaIkonicaMisija3 != (Object)null)
				{
					aktivnaIkonicaMisija3.enabled = false;
				}
				aktivnaIkonicaMisija3 = ((Component)transform.Find("Mission Icons/Mission 3/Babun Leteci")).GetComponent<Renderer>();
				aktivnaIkonicaMisija3.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj4 = transform.Find("Text/Mission 3");
				((Component)obj4).GetComponent<TextMesh>().text = array[2];
				((Component)obj4).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			}
			num++;
		}
		if (activeBoomerang_BaboonsMission)
		{
			switch (num)
			{
			case 1:
			{
				if ((Object)(object)aktivnaIkonicaMisija1 != (Object)null)
				{
					aktivnaIkonicaMisija1.enabled = false;
				}
				aktivnaIkonicaMisija1 = ((Component)transform.Find("Mission Icons/Mission 1/Babun Bumerang")).GetComponent<Renderer>();
				aktivnaIkonicaMisija1.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj8 = transform.Find("Text/Mission 1");
				((Component)obj8).GetComponent<TextMesh>().text = array[0];
				((Component)obj8).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			case 2:
			{
				if ((Object)(object)aktivnaIkonicaMisija2 != (Object)null)
				{
					aktivnaIkonicaMisija2.enabled = false;
				}
				aktivnaIkonicaMisija2 = ((Component)transform.Find("Mission Icons/Mission 2/Babun Bumerang")).GetComponent<Renderer>();
				aktivnaIkonicaMisija2.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj9 = transform.Find("Text/Mission 2");
				((Component)obj9).GetComponent<TextMesh>().text = array[1];
				((Component)obj9).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			case 3:
			{
				if ((Object)(object)aktivnaIkonicaMisija3 != (Object)null)
				{
					aktivnaIkonicaMisija3.enabled = false;
				}
				aktivnaIkonicaMisija3 = ((Component)transform.Find("Mission Icons/Mission 3/Babun Bumerang")).GetComponent<Renderer>();
				aktivnaIkonicaMisija3.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj7 = transform.Find("Text/Mission 3");
				((Component)obj7).GetComponent<TextMesh>().text = array[2];
				((Component)obj7).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			}
			num++;
		}
		if (activeGorillaMission)
		{
			switch (num)
			{
			case 1:
			{
				if ((Object)(object)aktivnaIkonicaMisija1 != (Object)null)
				{
					aktivnaIkonicaMisija1.enabled = false;
				}
				aktivnaIkonicaMisija1 = ((Component)transform.Find("Mission Icons/Mission 1/Gorila")).GetComponent<Renderer>();
				aktivnaIkonicaMisija1.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj11 = transform.Find("Text/Mission 1");
				((Component)obj11).GetComponent<TextMesh>().text = array[0];
				((Component)obj11).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			case 2:
			{
				if ((Object)(object)aktivnaIkonicaMisija2 != (Object)null)
				{
					aktivnaIkonicaMisija2.enabled = false;
				}
				aktivnaIkonicaMisija2 = ((Component)transform.Find("Mission Icons/Mission 2/Gorila")).GetComponent<Renderer>();
				aktivnaIkonicaMisija2.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj12 = transform.Find("Text/Mission 2");
				((Component)obj12).GetComponent<TextMesh>().text = array[1];
				((Component)obj12).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			case 3:
			{
				if ((Object)(object)aktivnaIkonicaMisija3 != (Object)null)
				{
					aktivnaIkonicaMisija3.enabled = false;
				}
				aktivnaIkonicaMisija3 = ((Component)transform.Find("Mission Icons/Mission 3/Gorila")).GetComponent<Renderer>();
				aktivnaIkonicaMisija3.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj10 = transform.Find("Text/Mission 3");
				((Component)obj10).GetComponent<TextMesh>().text = array[2];
				((Component)obj10).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			}
			num++;
		}
		if (activeFly_GorillaMission)
		{
			switch (num)
			{
			case 1:
			{
				if ((Object)(object)aktivnaIkonicaMisija1 != (Object)null)
				{
					aktivnaIkonicaMisija1.enabled = false;
				}
				aktivnaIkonicaMisija1 = ((Component)transform.Find("Mission Icons/Mission 1/Gorila Leteca")).GetComponent<Renderer>();
				aktivnaIkonicaMisija1.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj14 = transform.Find("Text/Mission 1");
				((Component)obj14).GetComponent<TextMesh>().text = array[0];
				((Component)obj14).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			case 2:
			{
				if ((Object)(object)aktivnaIkonicaMisija2 != (Object)null)
				{
					aktivnaIkonicaMisija2.enabled = false;
				}
				aktivnaIkonicaMisija2 = ((Component)transform.Find("Mission Icons/Mission 2/Gorila Leteca")).GetComponent<Renderer>();
				aktivnaIkonicaMisija2.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj15 = transform.Find("Text/Mission 2");
				((Component)obj15).GetComponent<TextMesh>().text = array[1];
				((Component)obj15).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			case 3:
			{
				if ((Object)(object)aktivnaIkonicaMisija3 != (Object)null)
				{
					aktivnaIkonicaMisija3.enabled = false;
				}
				aktivnaIkonicaMisija3 = ((Component)transform.Find("Mission Icons/Mission 3/Gorila Leteca")).GetComponent<Renderer>();
				aktivnaIkonicaMisija3.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj13 = transform.Find("Text/Mission 3");
				((Component)obj13).GetComponent<TextMesh>().text = array[2];
				((Component)obj13).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			}
			num++;
		}
		if (activeKoplje_GorillaMission)
		{
			switch (num)
			{
			case 1:
			{
				if ((Object)(object)aktivnaIkonicaMisija1 != (Object)null)
				{
					aktivnaIkonicaMisija1.enabled = false;
				}
				aktivnaIkonicaMisija1 = ((Component)transform.Find("Mission Icons/Mission 1/Gorila Sa Kopljem")).GetComponent<Renderer>();
				aktivnaIkonicaMisija1.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj17 = transform.Find("Text/Mission 1");
				((Component)obj17).GetComponent<TextMesh>().text = array[0];
				((Component)obj17).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			case 2:
			{
				if ((Object)(object)aktivnaIkonicaMisija2 != (Object)null)
				{
					aktivnaIkonicaMisija2.enabled = false;
				}
				aktivnaIkonicaMisija2 = ((Component)transform.Find("Mission Icons/Mission 2/Gorila Sa Kopljem")).GetComponent<Renderer>();
				aktivnaIkonicaMisija2.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj18 = transform.Find("Text/Mission 2");
				((Component)obj18).GetComponent<TextMesh>().text = array[1];
				((Component)obj18).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			case 3:
			{
				if ((Object)(object)aktivnaIkonicaMisija3 != (Object)null)
				{
					aktivnaIkonicaMisija3.enabled = false;
				}
				aktivnaIkonicaMisija3 = ((Component)transform.Find("Mission Icons/Mission 3/Gorila Sa Kopljem")).GetComponent<Renderer>();
				aktivnaIkonicaMisija3.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj16 = transform.Find("Text/Mission 3");
				((Component)obj16).GetComponent<TextMesh>().text = array[2];
				((Component)obj16).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			}
			num++;
		}
		if (activeDiamondsMission)
		{
			switch (num)
			{
			case 1:
			{
				if ((Object)(object)aktivnaIkonicaMisija1 != (Object)null)
				{
					aktivnaIkonicaMisija1.enabled = false;
				}
				aktivnaIkonicaMisija1 = ((Component)transform.Find("Mission Icons/Mission 1/3 Dijamanta")).GetComponent<Renderer>();
				aktivnaIkonicaMisija1.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj20 = transform.Find("Text/Mission 1");
				((Component)obj20).GetComponent<TextMesh>().text = array[0];
				((Component)obj20).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			case 2:
			{
				if ((Object)(object)aktivnaIkonicaMisija2 != (Object)null)
				{
					aktivnaIkonicaMisija2.enabled = false;
				}
				aktivnaIkonicaMisija2 = ((Component)transform.Find("Mission Icons/Mission 2/3 Dijamanta")).GetComponent<Renderer>();
				aktivnaIkonicaMisija2.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj21 = transform.Find("Text/Mission 2");
				((Component)obj21).GetComponent<TextMesh>().text = array[1];
				((Component)obj21).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			case 3:
			{
				if ((Object)(object)aktivnaIkonicaMisija3 != (Object)null)
				{
					aktivnaIkonicaMisija3.enabled = false;
				}
				aktivnaIkonicaMisija3 = ((Component)transform.Find("Mission Icons/Mission 3/3 Dijamanta")).GetComponent<Renderer>();
				aktivnaIkonicaMisija3.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj19 = transform.Find("Text/Mission 3");
				((Component)obj19).GetComponent<TextMesh>().text = array[2];
				((Component)obj19).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			}
			num++;
		}
		if (activeCoinsMission)
		{
			switch (num)
			{
			case 1:
			{
				if ((Object)(object)aktivnaIkonicaMisija1 != (Object)null)
				{
					aktivnaIkonicaMisija1.enabled = false;
				}
				aktivnaIkonicaMisija1 = ((Component)transform.Find("Mission Icons/Mission 1/Coin")).GetComponent<Renderer>();
				aktivnaIkonicaMisija1.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj23 = transform.Find("Text/Mission 1");
				((Component)obj23).GetComponent<TextMesh>().text = array[0];
				((Component)obj23).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			case 2:
			{
				if ((Object)(object)aktivnaIkonicaMisija2 != (Object)null)
				{
					aktivnaIkonicaMisija2.enabled = false;
				}
				aktivnaIkonicaMisija2 = ((Component)transform.Find("Mission Icons/Mission 2/Coin")).GetComponent<Renderer>();
				aktivnaIkonicaMisija2.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj24 = transform.Find("Text/Mission 2");
				((Component)obj24).GetComponent<TextMesh>().text = array[1];
				((Component)obj24).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			case 3:
			{
				if ((Object)(object)aktivnaIkonicaMisija3 != (Object)null)
				{
					aktivnaIkonicaMisija3.enabled = false;
				}
				aktivnaIkonicaMisija3 = ((Component)transform.Find("Mission Icons/Mission 3/Coin")).GetComponent<Renderer>();
				aktivnaIkonicaMisija3.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj22 = transform.Find("Text/Mission 3");
				((Component)obj22).GetComponent<TextMesh>().text = array[2];
				((Component)obj22).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			}
			num++;
		}
		if (activeDistanceMission)
		{
			switch (num)
			{
			case 1:
			{
				if ((Object)(object)aktivnaIkonicaMisija1 != (Object)null)
				{
					aktivnaIkonicaMisija1.enabled = false;
				}
				aktivnaIkonicaMisija1 = ((Component)transform.Find("Mission Icons/Mission 1/Distance")).GetComponent<Renderer>();
				aktivnaIkonicaMisija1.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj26 = transform.Find("Text/Mission 1");
				((Component)obj26).GetComponent<TextMesh>().text = array[0];
				((Component)obj26).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			case 2:
			{
				if ((Object)(object)aktivnaIkonicaMisija2 != (Object)null)
				{
					aktivnaIkonicaMisija2.enabled = false;
				}
				aktivnaIkonicaMisija2 = ((Component)transform.Find("Mission Icons/Mission 2/Distance")).GetComponent<Renderer>();
				aktivnaIkonicaMisija2.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj27 = transform.Find("Text/Mission 2");
				((Component)obj27).GetComponent<TextMesh>().text = array[1];
				((Component)obj27).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			case 3:
			{
				if ((Object)(object)aktivnaIkonicaMisija3 != (Object)null)
				{
					aktivnaIkonicaMisija3.enabled = false;
				}
				aktivnaIkonicaMisija3 = ((Component)transform.Find("Mission Icons/Mission 3/Distance")).GetComponent<Renderer>();
				aktivnaIkonicaMisija3.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj25 = transform.Find("Text/Mission 3");
				((Component)obj25).GetComponent<TextMesh>().text = array[2];
				((Component)obj25).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			}
			num++;
		}
		if (activeBarrelsMission)
		{
			switch (num)
			{
			case 1:
			{
				if ((Object)(object)aktivnaIkonicaMisija1 != (Object)null)
				{
					aktivnaIkonicaMisija1.enabled = false;
				}
				aktivnaIkonicaMisija1 = ((Component)transform.Find("Mission Icons/Mission 1/Bure")).GetComponent<Renderer>();
				aktivnaIkonicaMisija1.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj29 = transform.Find("Text/Mission 1");
				((Component)obj29).GetComponent<TextMesh>().text = array[0];
				((Component)obj29).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			case 2:
			{
				if ((Object)(object)aktivnaIkonicaMisija2 != (Object)null)
				{
					aktivnaIkonicaMisija2.enabled = false;
				}
				aktivnaIkonicaMisija2 = ((Component)transform.Find("Mission Icons/Mission 2/Bure")).GetComponent<Renderer>();
				aktivnaIkonicaMisija2.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj30 = transform.Find("Text/Mission 2");
				((Component)obj30).GetComponent<TextMesh>().text = array[1];
				((Component)obj30).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			case 3:
			{
				if ((Object)(object)aktivnaIkonicaMisija3 != (Object)null)
				{
					aktivnaIkonicaMisija3.enabled = false;
				}
				aktivnaIkonicaMisija3 = ((Component)transform.Find("Mission Icons/Mission 3/Bure")).GetComponent<Renderer>();
				aktivnaIkonicaMisija3.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj28 = transform.Find("Text/Mission 3");
				((Component)obj28).GetComponent<TextMesh>().text = array[2];
				((Component)obj28).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			}
			num++;
		}
		if (activeRedDiamondsMission)
		{
			switch (num)
			{
			case 1:
			{
				if ((Object)(object)aktivnaIkonicaMisija1 != (Object)null)
				{
					aktivnaIkonicaMisija1.enabled = false;
				}
				aktivnaIkonicaMisija1 = ((Component)transform.Find("Mission Icons/Mission 1/Crveni Dijamant")).GetComponent<Renderer>();
				aktivnaIkonicaMisija1.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj32 = transform.Find("Text/Mission 1");
				((Component)obj32).GetComponent<TextMesh>().text = array[0];
				((Component)obj32).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			case 2:
			{
				if ((Object)(object)aktivnaIkonicaMisija2 != (Object)null)
				{
					aktivnaIkonicaMisija2.enabled = false;
				}
				aktivnaIkonicaMisija2 = ((Component)transform.Find("Mission Icons/Mission 2/Crveni Dijamant")).GetComponent<Renderer>();
				aktivnaIkonicaMisija2.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj33 = transform.Find("Text/Mission 2");
				((Component)obj33).GetComponent<TextMesh>().text = array[1];
				((Component)obj33).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			case 3:
			{
				if ((Object)(object)aktivnaIkonicaMisija3 != (Object)null)
				{
					aktivnaIkonicaMisija3.enabled = false;
				}
				aktivnaIkonicaMisija3 = ((Component)transform.Find("Mission Icons/Mission 3/Crveni Dijamant")).GetComponent<Renderer>();
				aktivnaIkonicaMisija3.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj31 = transform.Find("Text/Mission 3");
				((Component)obj31).GetComponent<TextMesh>().text = array[2];
				((Component)obj31).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			}
			num++;
		}
		if (activeBlueDiamondsMission)
		{
			switch (num)
			{
			case 1:
			{
				if ((Object)(object)aktivnaIkonicaMisija1 != (Object)null)
				{
					aktivnaIkonicaMisija1.enabled = false;
				}
				aktivnaIkonicaMisija1 = ((Component)transform.Find("Mission Icons/Mission 1/Plavi Dijamant")).GetComponent<Renderer>();
				aktivnaIkonicaMisija1.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj35 = transform.Find("Text/Mission 1");
				((Component)obj35).GetComponent<TextMesh>().text = array[0];
				((Component)obj35).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			case 2:
			{
				if ((Object)(object)aktivnaIkonicaMisija2 != (Object)null)
				{
					aktivnaIkonicaMisija2.enabled = false;
				}
				aktivnaIkonicaMisija2 = ((Component)transform.Find("Mission Icons/Mission 2/Plavi Dijamant")).GetComponent<Renderer>();
				aktivnaIkonicaMisija2.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj36 = transform.Find("Text/Mission 2");
				((Component)obj36).GetComponent<TextMesh>().text = array[1];
				((Component)obj36).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			case 3:
			{
				if ((Object)(object)aktivnaIkonicaMisija3 != (Object)null)
				{
					aktivnaIkonicaMisija3.enabled = false;
				}
				aktivnaIkonicaMisija3 = ((Component)transform.Find("Mission Icons/Mission 3/Plavi Dijamant")).GetComponent<Renderer>();
				aktivnaIkonicaMisija3.enabled = true;
				string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
				Transform obj34 = transform.Find("Text/Mission 3");
				((Component)obj34).GetComponent<TextMesh>().text = array[2];
				((Component)obj34).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				break;
			}
			}
			num++;
		}
		if (!activeGreenDiamondsMission)
		{
			return;
		}
		switch (num)
		{
		case 1:
		{
			if ((Object)(object)aktivnaIkonicaMisija1 != (Object)null)
			{
				aktivnaIkonicaMisija1.enabled = false;
			}
			aktivnaIkonicaMisija1 = ((Component)transform.Find("Mission Icons/Mission 1/Zeleni Dijamant")).GetComponent<Renderer>();
			aktivnaIkonicaMisija1.enabled = true;
			string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
			Transform obj38 = transform.Find("Text/Mission 1");
			((Component)obj38).GetComponent<TextMesh>().text = array[0];
			((Component)obj38).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
			break;
		}
		case 2:
		{
			if ((Object)(object)aktivnaIkonicaMisija2 != (Object)null)
			{
				aktivnaIkonicaMisija2.enabled = false;
			}
			aktivnaIkonicaMisija2 = ((Component)transform.Find("Mission Icons/Mission 2/Zeleni Dijamant")).GetComponent<Renderer>();
			aktivnaIkonicaMisija2.enabled = true;
			string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
			Transform obj39 = transform.Find("Text/Mission 2");
			((Component)obj39).GetComponent<TextMesh>().text = array[1];
			((Component)obj39).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
			break;
		}
		case 3:
		{
			if ((Object)(object)aktivnaIkonicaMisija3 != (Object)null)
			{
				aktivnaIkonicaMisija3.enabled = false;
			}
			aktivnaIkonicaMisija3 = ((Component)transform.Find("Mission Icons/Mission 3/Zeleni Dijamant")).GetComponent<Renderer>();
			aktivnaIkonicaMisija3.enabled = true;
			string[] array = missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[1] { "\n" }, StringSplitOptions.None);
			Transform obj37 = transform.Find("Text/Mission 3");
			((Component)obj37).GetComponent<TextMesh>().text = array[2];
			((Component)obj37).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
			break;
		}
		}
		num++;
	}

	public void BaboonEvent(int currentBaboons)
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		if (missions[currentLevel].baboons > 0)
		{
			baboonsText.text = currentBaboons + "/" + missions[currentLevel].baboons;
			baboonsTextEffects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			if (currentBaboons >= missions[currentLevel].baboons && baboonsText.color != Color.green)
			{
				NumberOfQuests--;
				baboonsText.color = Color.green;
			}
			if (NumberOfQuests <= 0)
			{
				MissionComplete();
			}
		}
	}

	public void Fly_BaboonEvent(int currentFly_Baboons)
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		if (missions[currentLevel].fly_baboons > 0)
		{
			fly_baboonsText.text = currentFly_Baboons + "/" + missions[currentLevel].fly_baboons;
			fly_baboonsTextEffects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			if (currentFly_Baboons >= missions[currentLevel].fly_baboons && fly_baboonsText.color != Color.green)
			{
				NumberOfQuests--;
				fly_baboonsText.color = Color.green;
			}
			if (NumberOfQuests <= 0)
			{
				MissionComplete();
			}
		}
	}

	public void Boomerang_BaboonEvent(int currentBoomerang_Baboons)
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		if (missions[currentLevel].boomerang_baboons > 0)
		{
			boomerang_baboonsText.text = currentBoomerang_Baboons + "/" + missions[currentLevel].boomerang_baboons;
			boomerang_baboonsTextEffects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			if (currentBoomerang_Baboons >= missions[currentLevel].boomerang_baboons && boomerang_baboonsText.color != Color.green)
			{
				NumberOfQuests--;
				boomerang_baboonsText.color = Color.green;
			}
			if (NumberOfQuests <= 0)
			{
				MissionComplete();
			}
		}
	}

	public void GorillaEvent(int currentGorillas)
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		if (missions[currentLevel].gorilla > 0)
		{
			gorillaText.text = currentGorillas + "/" + missions[currentLevel].gorilla;
			gorillaTextEffects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			if (currentGorillas >= missions[currentLevel].gorilla && gorillaText.color != Color.green)
			{
				NumberOfQuests--;
				gorillaText.color = Color.green;
			}
			if (NumberOfQuests <= 0)
			{
				MissionComplete();
			}
		}
	}

	public void Fly_GorillaEvent(int currentFly_Gorillas)
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		if (missions[currentLevel].fly_gorilla > 0)
		{
			fly_gorillaText.text = currentFly_Gorillas + "/" + missions[currentLevel].fly_gorilla;
			fly_gorillaTextEffects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			if (currentFly_Gorillas >= missions[currentLevel].fly_gorilla && fly_gorillaText.color != Color.green)
			{
				NumberOfQuests--;
				fly_gorillaText.color = Color.green;
				LevelFactory.instance.leteceGorile = 0;
			}
			if (NumberOfQuests <= 0)
			{
				MissionComplete();
			}
		}
	}

	public void Koplje_GorillaEvent(int currentKoplje_Gorillas)
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		if (missions[currentLevel].koplje_gorilla > 0)
		{
			koplje_gorillaText.text = currentKoplje_Gorillas + "/" + missions[currentLevel].koplje_gorilla;
			koplje_gorillaTextEffects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			if (currentKoplje_Gorillas >= missions[currentLevel].koplje_gorilla && koplje_gorillaText.color != Color.green)
			{
				NumberOfQuests--;
				koplje_gorillaText.color = Color.green;
				LevelFactory.instance.kopljeGorile = 0;
			}
			if (NumberOfQuests <= 0)
			{
				MissionComplete();
			}
		}
	}

	public void DiamondEvent(int currentDiamonds)
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		if (missions[currentLevel].diamonds > 0)
		{
			diamondsText.text = currentDiamonds + "/" + missions[currentLevel].diamonds;
			diamondsTextEffects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			if (currentDiamonds >= missions[currentLevel].diamonds && diamondsText.color != Color.green)
			{
				NumberOfQuests--;
				diamondsText.color = Color.green;
			}
			if (NumberOfQuests <= 0)
			{
				MissionComplete();
			}
		}
	}

	public void CoinEvent(int currentCoins)
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		if (missions[currentLevel].coins > 0)
		{
			coinsText.text = currentCoins.ToString();
			coinsTextEffects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			if (currentCoins >= missions[currentLevel].coins && coinsText.color != Color.green)
			{
				NumberOfQuests--;
				coinsText.color = Color.green;
				((Component)((Component)coinsText).transform.parent.Find("Target Number")).GetComponent<TextMesh>().color = Color.green;
			}
			if (NumberOfQuests <= 0)
			{
				MissionComplete();
			}
		}
	}

	public void DistanceEvent(float currentDistance)
	{
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		distanceText.text = currentDistance.ToString();
		distanceTextEffects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		if (currentDistance % 10f == 0f && currentDistance != previousDistance)
		{
			Manage.points += 10;
			Manage.pointsText.text = Manage.points.ToString();
			Manage.pointsEffects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			previousDistance = currentDistance;
		}
		if (currentDistance >= (float)missions[currentLevel].distance && distanceText.color != Color.green)
		{
			NumberOfQuests--;
			distanceText.color = Color.green;
			((Component)((Component)distanceText).transform.parent.Find("Target Number")).GetComponent<TextMesh>().color = Color.green;
		}
		if (NumberOfQuests <= 0)
		{
			MissionComplete();
		}
	}

	public void BarrelEvent(int currentBarrels)
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		if (missions[currentLevel].barrels > 0)
		{
			barrelsText.text = currentBarrels + "/" + missions[currentLevel].barrels;
			barrelsTextEffects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			if (currentBarrels >= missions[currentLevel].barrels && barrelsText.color != Color.green)
			{
				NumberOfQuests--;
				barrelsText.color = Color.green;
			}
			if (NumberOfQuests <= 0)
			{
				MissionComplete();
			}
		}
	}

	public void RedDiamondEvent(int currentRedDiamonds)
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		if (missions[currentLevel].red_diamonds > 0)
		{
			redDiamondsText.text = currentRedDiamonds + "/" + missions[currentLevel].red_diamonds;
			redDiamondsTextEffects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			if (currentRedDiamonds >= missions[currentLevel].red_diamonds && redDiamondsText.color != Color.green)
			{
				NumberOfQuests--;
				redDiamondsText.color = Color.green;
				LevelFactory.instance.crveniDijamant = 0;
			}
			if (NumberOfQuests <= 0)
			{
				MissionComplete();
			}
		}
	}

	public void BlueDiamondEvent(int currentBlueDiamonds)
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		if (missions[currentLevel].blue_diamonds > 0)
		{
			blueDiamondsText.text = currentBlueDiamonds + "/" + missions[currentLevel].blue_diamonds;
			blueDiamondsTextEffects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			if (currentBlueDiamonds >= missions[currentLevel].blue_diamonds && blueDiamondsText.color != Color.green)
			{
				NumberOfQuests--;
				blueDiamondsText.color = Color.green;
				LevelFactory.instance.plaviDijamant = 0;
			}
			if (NumberOfQuests <= 0)
			{
				MissionComplete();
			}
		}
	}

	public void GreenDiamondEvent(int currentGreenDiamonds)
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		if (missions[currentLevel].green_diamonds > 0)
		{
			greenDiamondsText.text = currentGreenDiamonds + "/" + missions[currentLevel].green_diamonds;
			greenDiamondsTextEffects.RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			if (currentGreenDiamonds >= missions[currentLevel].green_diamonds && greenDiamondsText.color != Color.green)
			{
				NumberOfQuests--;
				greenDiamondsText.color = Color.green;
				LevelFactory.instance.zeleniDijamant = 0;
			}
			if (NumberOfQuests <= 0)
			{
				MissionComplete();
			}
		}
	}

	private void MissionComplete()
	{
		if (!postavioFinish)
		{
			MonkeyController2D.Instance.invincible = true;
			Manage.pauseEnabled = false;
			Manage.Instance.ApplyPowerUp(-1);
			Manage.Instance.ApplyPowerUp(-2);
			Manage.Instance.ApplyPowerUp(-3);
			postavioFinish = true;
			missionsComplete = true;
			MonkeyController2D.Instance.Finish();
		}
	}

	private void NotifyFinish()
	{
		MonkeyController2D.Instance.Finish();
	}
}
