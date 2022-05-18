using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

// Token: 0x020006E7 RID: 1767
public class MissionManager : MonoBehaviour
{
	// Token: 0x1700037D RID: 893
	// (get) Token: 0x06002C5B RID: 11355 RVA: 0x00021C06 File Offset: 0x0001FE06
	public static MissionManager Instance
	{
		get
		{
			if (MissionManager.instance == null)
			{
				MissionManager.instance = (Object.FindObjectOfType(typeof(MissionManager)) as MissionManager);
			}
			return MissionManager.instance;
		}
	}

	// Token: 0x06002C5C RID: 11356 RVA: 0x00021C33 File Offset: 0x0001FE33
	private void Awake()
	{
		base.transform.name = "MissionManager";
		MissionManager.instance = this;
		Object.DontDestroyOnLoad(base.gameObject);
		base.StartCoroutine(this.LoadMissions());
	}

	// Token: 0x06002C5D RID: 11357 RVA: 0x00021C63 File Offset: 0x0001FE63
	private IEnumerator LoadMissions()
	{
		string text = string.Empty;
		string result = string.Empty;
		text = Path.Combine(Application.streamingAssetsPath, this.xmlName);
		Debug.Log("path: " + text);
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
				Debug.LogError("Error reading file from path! " + www.error);
			}
			www = null;
		}
		else
		{
			result = File.ReadAllText(text);
		}
		IEnumerable<XElement> source = XElement.Parse(result).Elements();
		MissionManager.totalMissions = source.Count<XElement>();
		MissionManager.missions = new MissionTemplate[MissionManager.totalMissions];
		for (int i = 0; i < MissionManager.totalMissions; i++)
		{
			IEnumerable<XElement> source2 = source.ElementAt(i).Elements();
			MissionManager.missions[i] = new MissionTemplate();
			MissionManager.missions[i].level = source.ElementAt(i).Attribute("level").Value;
			MissionManager.missions[i].baboons = int.Parse(source2.ElementAt(0).Value);
			MissionManager.missions[i].fly_baboons = int.Parse(source2.ElementAt(1).Value);
			MissionManager.missions[i].boomerang_baboons = int.Parse(source2.ElementAt(2).Value);
			MissionManager.missions[i].gorilla = int.Parse(source2.ElementAt(3).Value);
			MissionManager.missions[i].fly_gorilla = int.Parse(source2.ElementAt(4).Value);
			MissionManager.missions[i].koplje_gorilla = int.Parse(source2.ElementAt(5).Value);
			MissionManager.missions[i].diamonds = int.Parse(source2.ElementAt(6).Value);
			MissionManager.missions[i].coins = int.Parse(source2.ElementAt(7).Value);
			MissionManager.missions[i].distance = int.Parse(source2.ElementAt(8).Value);
			MissionManager.missions[i].barrels = int.Parse(source2.ElementAt(9).Value);
			MissionManager.missions[i].red_diamonds = int.Parse(source2.ElementAt(10).Value);
			MissionManager.missions[i].blue_diamonds = int.Parse(source2.ElementAt(11).Value);
			MissionManager.missions[i].green_diamonds = int.Parse(source2.ElementAt(12).Value);
			MissionManager.missions[i].points = int.Parse(source2.ElementAt(13).Value);
			MissionManager.missions[i].description_en = source2.ElementAt(14).Value;
			MissionManager.missions[i].description_us = source2.ElementAt(15).Value;
			MissionManager.missions[i].description_es = source2.ElementAt(16).Value;
			MissionManager.missions[i].description_ru = source2.ElementAt(17).Value;
			MissionManager.missions[i].description_pt = source2.ElementAt(18).Value;
			MissionManager.missions[i].description_pt_br = source2.ElementAt(19).Value;
			MissionManager.missions[i].description_fr = source2.ElementAt(20).Value;
			MissionManager.missions[i].description_tha = source2.ElementAt(21).Value;
			MissionManager.missions[i].description_zh = source2.ElementAt(22).Value;
			MissionManager.missions[i].description_tzh = source2.ElementAt(23).Value;
			MissionManager.missions[i].description_ger = source2.ElementAt(24).Value;
			MissionManager.missions[i].description_it = source2.ElementAt(25).Value;
			MissionManager.missions[i].description_srb = source2.ElementAt(26).Value;
			MissionManager.missions[i].description_tur = source2.ElementAt(27).Value;
			MissionManager.missions[i].description_kor = source2.ElementAt(28).Value;
		}
		StagesParser.stagesLoaded = true;
		yield break;
	}

	// Token: 0x06002C5E RID: 11358 RVA: 0x000042DD File Offset: 0x000024DD
	public static void OdrediJezik()
	{
	}

	// Token: 0x06002C5F RID: 11359 RVA: 0x00158120 File Offset: 0x00156320
	public static void OdrediMisiju(int level, bool mapa)
	{
		MissionManager.postavioFinish = false;
		MissionManager.NumberOfQuests = 0;
		MissionManager.currentLevel = level;
		if (MissionManager.missions[level].baboons > 0)
		{
			MissionManager.activeBaboonsMission = true;
			MissionManager.NumberOfQuests++;
		}
		else
		{
			MissionManager.activeBaboonsMission = false;
		}
		if (MissionManager.missions[level].fly_baboons > 0)
		{
			MissionManager.activeFly_BaboonsMission = true;
			MissionManager.NumberOfQuests++;
		}
		else
		{
			MissionManager.activeFly_BaboonsMission = false;
		}
		if (MissionManager.missions[level].boomerang_baboons > 0)
		{
			MissionManager.activeBoomerang_BaboonsMission = true;
			MissionManager.NumberOfQuests++;
		}
		else
		{
			MissionManager.activeBoomerang_BaboonsMission = false;
		}
		if (MissionManager.missions[level].gorilla > 0)
		{
			MissionManager.activeGorillaMission = true;
			MissionManager.NumberOfQuests++;
		}
		else
		{
			MissionManager.activeGorillaMission = false;
		}
		if (MissionManager.missions[level].fly_gorilla > 0)
		{
			MissionManager.activeFly_GorillaMission = true;
			MissionManager.NumberOfQuests++;
		}
		else
		{
			MissionManager.activeFly_GorillaMission = false;
		}
		if (MissionManager.missions[level].koplje_gorilla > 0)
		{
			MissionManager.activeKoplje_GorillaMission = true;
			MissionManager.NumberOfQuests++;
		}
		else
		{
			MissionManager.activeKoplje_GorillaMission = false;
		}
		if (MissionManager.missions[level].diamonds > 0)
		{
			MissionManager.activeDiamondsMission = true;
			MissionManager.NumberOfQuests++;
		}
		else
		{
			MissionManager.activeDiamondsMission = false;
		}
		if (MissionManager.missions[level].coins > 0)
		{
			MissionManager.activeCoinsMission = true;
			MissionManager.NumberOfQuests++;
		}
		else
		{
			MissionManager.activeCoinsMission = false;
		}
		if (MissionManager.missions[level].distance > 0)
		{
			MissionManager.activeDistanceMission = true;
			MissionManager.NumberOfQuests++;
		}
		else
		{
			MissionManager.activeDistanceMission = false;
		}
		if (MissionManager.missions[level].barrels > 0)
		{
			MissionManager.activeBarrelsMission = true;
			MissionManager.NumberOfQuests++;
		}
		else
		{
			MissionManager.activeBarrelsMission = false;
		}
		if (MissionManager.missions[level].red_diamonds > 0)
		{
			MissionManager.activeRedDiamondsMission = true;
			MissionManager.NumberOfQuests++;
		}
		else
		{
			MissionManager.activeRedDiamondsMission = false;
		}
		if (MissionManager.missions[level].blue_diamonds > 0)
		{
			MissionManager.activeBlueDiamondsMission = true;
			MissionManager.NumberOfQuests++;
		}
		else
		{
			MissionManager.activeBlueDiamondsMission = false;
		}
		if (MissionManager.missions[level].green_diamonds > 0)
		{
			MissionManager.activeGreenDiamondsMission = true;
			MissionManager.NumberOfQuests++;
		}
		else
		{
			MissionManager.activeGreenDiamondsMission = false;
		}
		MissionManager.missionsComplete = false;
		MissionManager.previousDistance = 0f;
		MissionManager.points3Stars = MissionManager.missions[level].points;
		if (MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Contains("BOSS STAGE"))
		{
			StagesParser.bossStage = true;
		}
		if (mapa)
		{
			MissionManager.OdrediIkoniceNaMapi(level);
			return;
		}
		MissionManager.OdrediIkonice(level);
	}

	// Token: 0x06002C60 RID: 11360 RVA: 0x001583A4 File Offset: 0x001565A4
	public static void OdrediIkonice(int level)
	{
		Transform transform = GameObject.Find("_GameManager/Gameplay Scena Interface/_TopMissions").transform;
		int num = 1;
		Transform transform2 = null;
		if (MissionManager.activeBaboonsMission)
		{
			transform.Find("MissionIcons/Babun").gameObject.SetActive(true);
			if (num == 1)
			{
				transform2 = transform.Find("TextField Mission1");
				transform2.parent = (transform.Find("MissionIcons/Babun").parent = transform.Find("Mission1"));
			}
			else if (num == 2)
			{
				transform2 = transform.Find("TextField Mission2");
				transform2.parent = (transform.Find("MissionIcons/Babun").parent = transform.Find("Mission2"));
			}
			else if (num == 3)
			{
				transform2 = transform.Find("TextField Mission3");
				transform2.parent = (transform.Find("MissionIcons/Babun").parent = transform.Find("Mission3"));
			}
			transform2.localPosition = new Vector3(0f, 0f, -0.1f);
			transform2.gameObject.SetActive(true);
			MissionManager.baboonsText = transform2.GetChild(0).GetChild(0).GetComponent<TextMesh>();
			MissionManager.baboonsText.text = "0/" + MissionManager.missions[level].baboons.ToString();
			MissionManager.baboonsTextEffects = MissionManager.baboonsText.GetComponent<TextMeshEffects>();
			MissionManager.baboonsTextEffects.RefreshTextOutline(false, true, true);
			num++;
		}
		if (MissionManager.activeFly_BaboonsMission)
		{
			transform.Find("MissionIcons/Babun Leteci").gameObject.SetActive(true);
			if (num == 1)
			{
				transform2 = transform.Find("TextField Mission1");
				transform2.parent = (transform.Find("MissionIcons/Babun Leteci").parent = transform.Find("Mission1"));
			}
			else if (num == 2)
			{
				transform2 = transform.Find("TextField Mission2");
				transform2.parent = (transform.Find("MissionIcons/Babun Leteci").parent = transform.Find("Mission2"));
			}
			else if (num == 3)
			{
				transform2 = transform.Find("TextField Mission3");
				transform2.parent = (transform.Find("MissionIcons/Babun Leteci").parent = transform.Find("Mission3"));
			}
			transform2.localPosition = new Vector3(0f, 0f, -0.1f);
			transform2.gameObject.SetActive(true);
			MissionManager.fly_baboonsText = transform2.GetChild(0).GetChild(0).GetComponent<TextMesh>();
			MissionManager.fly_baboonsText.text = "0/" + MissionManager.missions[level].fly_baboons.ToString();
			MissionManager.fly_baboonsTextEffects = MissionManager.fly_baboonsText.GetComponent<TextMeshEffects>();
			MissionManager.fly_baboonsTextEffects.RefreshTextOutline(false, true, true);
			num++;
			if (MissionManager.missions[level].fly_baboons > 0)
			{
				LevelFactory.instance.leteciBabuni_Kvota = (LevelFactory.instance.leteciBabuni_Kvota_locked = 7f / (float)MissionManager.missions[level].fly_baboons);
			}
		}
		if (MissionManager.activeBoomerang_BaboonsMission)
		{
			transform.Find("MissionIcons/Babun Boomerang").gameObject.SetActive(true);
			if (num == 1)
			{
				transform2 = transform.Find("TextField Mission1");
				transform2.parent = (transform.Find("MissionIcons/Babun Boomerang").parent = transform.Find("Mission1"));
			}
			else if (num == 2)
			{
				transform2 = transform.Find("TextField Mission2");
				transform2.parent = (transform.Find("MissionIcons/Babun Boomerang").parent = transform.Find("Mission2"));
			}
			else if (num == 3)
			{
				transform2 = transform.Find("TextField Mission3");
				transform2.parent = (transform.Find("MissionIcons/Babun Boomerang").parent = transform.Find("Mission3"));
			}
			transform2.localPosition = new Vector3(0f, 0f, -0.1f);
			transform2.gameObject.SetActive(true);
			MissionManager.boomerang_baboonsText = transform2.GetChild(0).GetChild(0).GetComponent<TextMesh>();
			MissionManager.boomerang_baboonsText.text = "0/" + MissionManager.missions[level].boomerang_baboons.ToString();
			MissionManager.boomerang_baboonsTextEffects = MissionManager.boomerang_baboonsText.GetComponent<TextMeshEffects>();
			MissionManager.boomerang_baboonsTextEffects.RefreshTextOutline(false, true, true);
			num++;
			if (MissionManager.missions[level].boomerang_baboons > 0)
			{
				LevelFactory.instance.boomerangBabuni_Kvota = (LevelFactory.instance.boomerangBabuni_Kvota_locked = 7f / (float)MissionManager.missions[level].boomerang_baboons);
			}
		}
		if (MissionManager.activeGorillaMission)
		{
			transform.Find("MissionIcons/Gorila").gameObject.SetActive(true);
			if (num == 1)
			{
				transform2 = transform.Find("TextField Mission1");
				transform2.parent = (transform.Find("MissionIcons/Gorila").parent = transform.Find("Mission1"));
			}
			else if (num == 2)
			{
				transform2 = transform.Find("TextField Mission2");
				transform2.parent = (transform.Find("MissionIcons/Gorila").parent = transform.Find("Mission2"));
			}
			else if (num == 3)
			{
				transform2 = transform.Find("TextField Mission3");
				transform2.parent = (transform.Find("MissionIcons/Gorila").parent = transform.Find("Mission3"));
			}
			transform2.localPosition = new Vector3(0f, 0f, -0.1f);
			transform2.gameObject.SetActive(true);
			MissionManager.gorillaText = transform2.GetChild(0).GetChild(0).GetComponent<TextMesh>();
			MissionManager.gorillaText.text = "0/" + MissionManager.missions[level].gorilla.ToString();
			MissionManager.gorillaTextEffects = MissionManager.gorillaText.GetComponent<TextMeshEffects>();
			MissionManager.gorillaTextEffects.RefreshTextOutline(false, true, true);
			num++;
		}
		if (MissionManager.activeFly_GorillaMission)
		{
			transform.Find("MissionIcons/Gorila Leteca").gameObject.SetActive(true);
			if (num == 1)
			{
				transform2 = transform.Find("TextField Mission1");
				transform2.parent = (transform.Find("MissionIcons/Gorila Leteca").parent = transform.Find("Mission1"));
			}
			else if (num == 2)
			{
				transform2 = transform.Find("TextField Mission2");
				transform2.parent = (transform.Find("MissionIcons/Gorila Leteca").parent = transform.Find("Mission2"));
			}
			else if (num == 3)
			{
				transform2 = transform.Find("TextField Mission3");
				transform2.parent = (transform.Find("MissionIcons/Gorila Leteca").parent = transform.Find("Mission3"));
			}
			transform2.localPosition = new Vector3(0f, 0f, -0.1f);
			transform2.gameObject.SetActive(true);
			MissionManager.fly_gorillaText = transform2.GetChild(0).GetChild(0).GetComponent<TextMesh>();
			MissionManager.fly_gorillaText.text = "0/" + MissionManager.missions[level].fly_gorilla.ToString();
			MissionManager.fly_gorillaTextEffects = MissionManager.fly_gorillaText.GetComponent<TextMeshEffects>();
			MissionManager.fly_gorillaTextEffects.RefreshTextOutline(false, true, true);
			num++;
			if (MissionManager.missions[level].fly_gorilla > 0)
			{
				LevelFactory.instance.leteceGorile_Kvota = (LevelFactory.instance.leteceGorile_Kvota_locked = 7f / (float)MissionManager.missions[level].fly_gorilla);
			}
		}
		if (MissionManager.activeKoplje_GorillaMission)
		{
			transform.Find("MissionIcons/Gorila Sa Kopljem").gameObject.SetActive(true);
			if (num == 1)
			{
				transform2 = transform.Find("TextField Mission1");
				transform2.parent = (transform.Find("MissionIcons/Gorila Sa Kopljem").parent = transform.Find("Mission1"));
			}
			else if (num == 2)
			{
				transform2 = transform.Find("TextField Mission2");
				transform2.parent = (transform.Find("MissionIcons/Gorila Sa Kopljem").parent = transform.Find("Mission2"));
			}
			else if (num == 3)
			{
				transform2 = transform.Find("TextField Mission3");
				transform2.parent = (transform.Find("MissionIcons/Gorila Sa Kopljem").parent = transform.Find("Mission3"));
			}
			transform2.localPosition = new Vector3(0f, 0f, -0.1f);
			transform2.gameObject.SetActive(true);
			MissionManager.koplje_gorillaText = transform2.GetChild(0).GetChild(0).GetComponent<TextMesh>();
			MissionManager.koplje_gorillaText.text = "0/" + MissionManager.missions[level].koplje_gorilla.ToString();
			MissionManager.koplje_gorillaTextEffects = MissionManager.koplje_gorillaText.GetComponent<TextMeshEffects>();
			MissionManager.koplje_gorillaTextEffects.RefreshTextOutline(false, true, true);
			num++;
			if (MissionManager.missions[level].koplje_gorilla > 0)
			{
				LevelFactory.instance.kopljeGorile_Kvota = (LevelFactory.instance.kopljeGorile_Kvota_locked = 7f / (float)MissionManager.missions[level].koplje_gorilla);
			}
		}
		if (MissionManager.activeDiamondsMission)
		{
			transform.Find("MissionIcons/Svi Dijamanti").gameObject.SetActive(true);
			if (num == 1)
			{
				transform2 = transform.Find("TextField Mission1");
				transform2.parent = (transform.Find("MissionIcons/Svi Dijamanti").parent = transform.Find("Mission1"));
			}
			else if (num == 2)
			{
				transform2 = transform.Find("TextField Mission2");
				transform2.parent = (transform.Find("MissionIcons/Svi Dijamanti").parent = transform.Find("Mission2"));
			}
			else if (num == 3)
			{
				transform2 = transform.Find("TextField Mission3");
				transform2.parent = (transform.Find("MissionIcons/Svi Dijamanti").parent = transform.Find("Mission3"));
			}
			transform2.localPosition = new Vector3(0f, 0f, -0.1f);
			transform2.gameObject.SetActive(true);
			MissionManager.diamondsText = transform2.GetChild(0).GetChild(0).GetComponent<TextMesh>();
			MissionManager.diamondsText.text = "0/" + MissionManager.missions[level].diamonds.ToString();
			MissionManager.diamondsTextEffects = MissionManager.diamondsText.GetComponent<TextMeshEffects>();
			MissionManager.diamondsTextEffects.RefreshTextOutline(false, true, true);
			num++;
		}
		if (MissionManager.activeCoinsMission)
		{
			transform.Find("MissionIcons/Coin").gameObject.SetActive(true);
			if (num == 1)
			{
				transform2 = transform.Find("TextFieldSplit Mission1");
				transform2.parent = (transform.Find("MissionIcons/Coin").parent = transform.Find("Mission1"));
			}
			else if (num == 2)
			{
				transform2 = transform.Find("TextFieldSplit Mission2");
				transform2.parent = (transform.Find("MissionIcons/Coin").parent = transform.Find("Mission2"));
			}
			else if (num == 3)
			{
				transform2 = transform.Find("TextFieldSplit Mission3");
				transform2.parent = (transform.Find("MissionIcons/Coin").parent = transform.Find("Mission3"));
			}
			transform2.localPosition = new Vector3(0f, 0f, -0.1f);
			transform2.gameObject.SetActive(true);
			transform2.GetChild(0).Find("Target Number").GetComponent<TextMesh>().text = MissionManager.missions[level].coins.ToString();
			transform2.GetChild(0).Find("Target Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
			MissionManager.coinsText = transform2.GetChild(0).Find("Current Number").GetComponent<TextMesh>();
			MissionManager.coinsText.text = "0";
			MissionManager.coinsTextEffects = MissionManager.coinsText.GetComponent<TextMeshEffects>();
			MissionManager.coinsTextEffects.RefreshTextOutline(false, true, true);
			num++;
		}
		if (MissionManager.activeDistanceMission)
		{
			transform.Find("MissionIcons/Distance").gameObject.SetActive(true);
			if (num == 1)
			{
				transform2 = transform.Find("TextFieldSplit Mission1");
				transform2.parent = (transform.Find("MissionIcons/Distance").parent = transform.Find("Mission1"));
			}
			else if (num == 2)
			{
				transform2 = transform.Find("TextFieldSplit Mission2");
				transform2.parent = (transform.Find("MissionIcons/Distance").parent = transform.Find("Mission2"));
			}
			else if (num == 3)
			{
				transform2 = transform.Find("TextFieldSplit Mission3");
				transform2.parent = (transform.Find("MissionIcons/Distance").parent = transform.Find("Mission3"));
			}
			transform2.localPosition = new Vector3(0f, 0f, -0.1f);
			transform2.gameObject.SetActive(true);
			transform2.GetChild(0).Find("Target Number").GetComponent<TextMesh>().text = MissionManager.missions[level].distance.ToString();
			transform2.GetChild(0).Find("Target Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
			MissionManager.distanceText = transform2.GetChild(0).Find("Current Number").GetComponent<TextMesh>();
			MissionManager.distanceText.text = "0";
			MissionManager.distanceTextEffects = MissionManager.distanceText.GetComponent<TextMeshEffects>();
			MissionManager.distanceTextEffects.RefreshTextOutline(false, true, true);
			num++;
		}
		if (MissionManager.activeBarrelsMission)
		{
			transform.Find("MissionIcons/Bure").gameObject.SetActive(true);
			if (num == 1)
			{
				transform2 = transform.Find("TextField Mission1");
				transform2.parent = (transform.Find("MissionIcons/Bure").parent = transform.Find("Mission1"));
			}
			else if (num == 2)
			{
				transform2 = transform.Find("TextField Mission2");
				transform2.parent = (transform.Find("MissionIcons/Bure").parent = transform.Find("Mission2"));
			}
			else if (num == 3)
			{
				transform2 = transform.Find("TextField Mission3");
				transform2.parent = (transform.Find("MissionIcons/Bure").parent = transform.Find("Mission3"));
			}
			transform2.localPosition = new Vector3(0f, 0f, -0.1f);
			transform2.gameObject.SetActive(true);
			MissionManager.barrelsText = transform2.GetChild(0).GetChild(0).GetComponent<TextMesh>();
			MissionManager.barrelsText.text = "0/" + MissionManager.missions[level].barrels.ToString();
			MissionManager.barrelsTextEffects = MissionManager.barrelsText.GetComponent<TextMeshEffects>();
			MissionManager.barrelsTextEffects.RefreshTextOutline(false, true, true);
			num++;
		}
		if (MissionManager.activeRedDiamondsMission)
		{
			transform.Find("MissionIcons/Crveni Dijamant").gameObject.SetActive(true);
			if (num == 1)
			{
				transform2 = transform.Find("TextField Mission1");
				transform2.parent = (transform.Find("MissionIcons/Crveni Dijamant").parent = transform.Find("Mission1"));
			}
			else if (num == 2)
			{
				transform2 = transform.Find("TextField Mission2");
				transform2.parent = (transform.Find("MissionIcons/Crveni Dijamant").parent = transform.Find("Mission2"));
			}
			else if (num == 3)
			{
				transform2 = transform.Find("TextField Mission3");
				transform2.parent = (transform.Find("MissionIcons/Crveni Dijamant").parent = transform.Find("Mission3"));
			}
			transform2.localPosition = new Vector3(0f, 0f, -0.1f);
			transform2.gameObject.SetActive(true);
			MissionManager.redDiamondsText = transform2.GetChild(0).GetChild(0).GetComponent<TextMesh>();
			MissionManager.redDiamondsText.text = "0/" + MissionManager.missions[level].red_diamonds.ToString();
			MissionManager.redDiamondsTextEffects = MissionManager.redDiamondsText.GetComponent<TextMeshEffects>();
			MissionManager.redDiamondsTextEffects.RefreshTextOutline(false, true, true);
			num++;
			if (MissionManager.missions[level].red_diamonds > 0)
			{
				LevelFactory.instance.crveniDijamant_Kvota = (LevelFactory.instance.crveniDijamant_Kvota_locked = 7f / (float)MissionManager.missions[level].red_diamonds);
			}
		}
		if (MissionManager.activeBlueDiamondsMission)
		{
			transform.Find("MissionIcons/Plavi Dijamant").gameObject.SetActive(true);
			if (num == 1)
			{
				transform2 = transform.Find("TextField Mission1");
				transform2.parent = (transform.Find("MissionIcons/Plavi Dijamant").parent = transform.Find("Mission1"));
			}
			else if (num == 2)
			{
				transform2 = transform.Find("TextField Mission2");
				transform2.parent = (transform.Find("MissionIcons/Plavi Dijamant").parent = transform.Find("Mission2"));
			}
			else if (num == 3)
			{
				transform2 = transform.Find("TextField Mission3");
				transform2.parent = (transform.Find("MissionIcons/Plavi Dijamant").parent = transform.Find("Mission3"));
			}
			transform2.localPosition = new Vector3(0f, 0f, -0.1f);
			transform2.gameObject.SetActive(true);
			MissionManager.blueDiamondsText = transform2.GetChild(0).GetChild(0).GetComponent<TextMesh>();
			MissionManager.blueDiamondsText.text = "0/" + MissionManager.missions[level].blue_diamonds.ToString();
			MissionManager.blueDiamondsTextEffects = MissionManager.blueDiamondsText.GetComponent<TextMeshEffects>();
			MissionManager.blueDiamondsTextEffects.RefreshTextOutline(false, true, true);
			num++;
			if (MissionManager.missions[level].blue_diamonds > 0)
			{
				LevelFactory.instance.plaviDijamant_Kvota = (LevelFactory.instance.plaviDijamant_Kvota_locked = 7f / (float)MissionManager.missions[level].blue_diamonds);
			}
		}
		if (MissionManager.activeGreenDiamondsMission)
		{
			transform.Find("MissionIcons/Zeleni Dijamant").gameObject.SetActive(true);
			if (num == 1)
			{
				transform2 = transform.Find("TextField Mission1");
				transform2.parent = (transform.Find("MissionIcons/Zeleni Dijamant").parent = transform.Find("Mission1"));
			}
			else if (num == 2)
			{
				transform2 = transform.Find("TextField Mission2");
				transform2.parent = (transform.Find("MissionIcons/Zeleni Dijamant").parent = transform.Find("Mission2"));
			}
			else if (num == 3)
			{
				transform2 = transform.Find("TextField Mission3");
				transform2.parent = (transform.Find("MissionIcons/Zeleni Dijamant").parent = transform.Find("Mission3"));
			}
			transform2.localPosition = new Vector3(0f, 0f, -0.1f);
			transform2.gameObject.SetActive(true);
			MissionManager.greenDiamondsText = transform2.GetChild(0).GetChild(0).GetComponent<TextMesh>();
			MissionManager.greenDiamondsText.text = "0/" + MissionManager.missions[level].green_diamonds.ToString();
			MissionManager.greenDiamondsTextEffects = MissionManager.greenDiamondsText.GetComponent<TextMeshEffects>();
			MissionManager.greenDiamondsTextEffects.RefreshTextOutline(false, true, true);
			num++;
			if (MissionManager.missions[level].green_diamonds > 0)
			{
				LevelFactory.instance.zeleniDijamant_Kvota = (LevelFactory.instance.zeleniDijamant_Kvota_locked = 7f / (float)MissionManager.missions[level].green_diamonds);
			}
		}
		if (MissionManager.NumberOfQuests == 1)
		{
			transform.Find("Mission1").localPosition = new Vector3(7f, 0f, 0f);
			transform.Find("Mission2").localPosition = new Vector3(0f, 7f, 0f);
			transform.Find("Mission3").localPosition = new Vector3(0f, 7f, 0f);
			return;
		}
		if (MissionManager.NumberOfQuests == 2)
		{
			transform.Find("Mission1").localPosition = new Vector3(4.5f, 0f, 0f);
			transform.Find("Mission2").localPosition = new Vector3(10.5f, 0f, 0f);
			transform.Find("Mission3").localPosition = new Vector3(0f, 7f, 0f);
			return;
		}
		if (MissionManager.NumberOfQuests == 3)
		{
			transform.Find("Mission1").localPosition = new Vector3(1f, 0f, 0f);
			transform.Find("Mission2").localPosition = new Vector3(7f, 0f, 0f);
			transform.Find("Mission3").localPosition = new Vector3(13f, 0f, 0f);
		}
	}

	// Token: 0x06002C61 RID: 11361 RVA: 0x0015978C File Offset: 0x0015798C
	public static void OdrediIkoniceNaMapi(int level)
	{
		Transform transform = GameObject.FindGameObjectWithTag("Mission").transform;
		int num = 1;
		int num2 = level + 1;
		num2 %= 20;
		if (num2 == 0)
		{
			num2 = 20;
		}
		transform.Find("Text/Level No").GetComponent<TextMesh>().text = LanguageManager.Level + " " + num2;
		transform.Find("Text/Level No").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		if (MissionManager.activeBaboonsMission)
		{
			if (num == 1)
			{
				if (MissionManager.aktivnaIkonicaMisija1 != null)
				{
					MissionManager.aktivnaIkonicaMisija1.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija1 = transform.Find("Mission Icons/Mission 1/Babun").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija1.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform2 = transform.Find("Text/Mission 1");
				transform2.GetComponent<TextMesh>().text = array[0];
				transform2.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			else if (num == 2)
			{
				if (MissionManager.aktivnaIkonicaMisija2 != null)
				{
					MissionManager.aktivnaIkonicaMisija2.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija2 = transform.Find("Mission Icons/Mission 2/Babun").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija2.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform3 = transform.Find("Text/Mission 2");
				transform3.GetComponent<TextMesh>().text = array[1];
				transform3.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			else if (num == 3)
			{
				if (MissionManager.aktivnaIkonicaMisija3 != null)
				{
					MissionManager.aktivnaIkonicaMisija3.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija3 = transform.Find("Mission Icons/Mission 3/Babun").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija3.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform4 = transform.Find("Text/Mission 3");
				transform4.GetComponent<TextMesh>().text = array[2];
				transform4.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			num++;
		}
		if (MissionManager.activeFly_BaboonsMission)
		{
			if (num == 1)
			{
				if (MissionManager.aktivnaIkonicaMisija1 != null)
				{
					MissionManager.aktivnaIkonicaMisija1.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija1 = transform.Find("Mission Icons/Mission 1/Babun Leteci").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija1.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform5 = transform.Find("Text/Mission 1");
				transform5.GetComponent<TextMesh>().text = array[0];
				transform5.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			else if (num == 2)
			{
				if (MissionManager.aktivnaIkonicaMisija2 != null)
				{
					MissionManager.aktivnaIkonicaMisija2.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija2 = transform.Find("Mission Icons/Mission 2/Babun Leteci").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija2.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform6 = transform.Find("Text/Mission 2");
				transform6.GetComponent<TextMesh>().text = array[1];
				transform6.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			else if (num == 3)
			{
				if (MissionManager.aktivnaIkonicaMisija3 != null)
				{
					MissionManager.aktivnaIkonicaMisija3.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija3 = transform.Find("Mission Icons/Mission 3/Babun Leteci").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija3.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform7 = transform.Find("Text/Mission 3");
				transform7.GetComponent<TextMesh>().text = array[2];
				transform7.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			num++;
		}
		if (MissionManager.activeBoomerang_BaboonsMission)
		{
			if (num == 1)
			{
				if (MissionManager.aktivnaIkonicaMisija1 != null)
				{
					MissionManager.aktivnaIkonicaMisija1.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija1 = transform.Find("Mission Icons/Mission 1/Babun Bumerang").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija1.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform8 = transform.Find("Text/Mission 1");
				transform8.GetComponent<TextMesh>().text = array[0];
				transform8.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			else if (num == 2)
			{
				if (MissionManager.aktivnaIkonicaMisija2 != null)
				{
					MissionManager.aktivnaIkonicaMisija2.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija2 = transform.Find("Mission Icons/Mission 2/Babun Bumerang").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija2.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform9 = transform.Find("Text/Mission 2");
				transform9.GetComponent<TextMesh>().text = array[1];
				transform9.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			else if (num == 3)
			{
				if (MissionManager.aktivnaIkonicaMisija3 != null)
				{
					MissionManager.aktivnaIkonicaMisija3.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija3 = transform.Find("Mission Icons/Mission 3/Babun Bumerang").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija3.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform10 = transform.Find("Text/Mission 3");
				transform10.GetComponent<TextMesh>().text = array[2];
				transform10.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			num++;
		}
		if (MissionManager.activeGorillaMission)
		{
			if (num == 1)
			{
				if (MissionManager.aktivnaIkonicaMisija1 != null)
				{
					MissionManager.aktivnaIkonicaMisija1.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija1 = transform.Find("Mission Icons/Mission 1/Gorila").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija1.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform11 = transform.Find("Text/Mission 1");
				transform11.GetComponent<TextMesh>().text = array[0];
				transform11.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			else if (num == 2)
			{
				if (MissionManager.aktivnaIkonicaMisija2 != null)
				{
					MissionManager.aktivnaIkonicaMisija2.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija2 = transform.Find("Mission Icons/Mission 2/Gorila").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija2.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform12 = transform.Find("Text/Mission 2");
				transform12.GetComponent<TextMesh>().text = array[1];
				transform12.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			else if (num == 3)
			{
				if (MissionManager.aktivnaIkonicaMisija3 != null)
				{
					MissionManager.aktivnaIkonicaMisija3.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija3 = transform.Find("Mission Icons/Mission 3/Gorila").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija3.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform13 = transform.Find("Text/Mission 3");
				transform13.GetComponent<TextMesh>().text = array[2];
				transform13.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			num++;
		}
		if (MissionManager.activeFly_GorillaMission)
		{
			if (num == 1)
			{
				if (MissionManager.aktivnaIkonicaMisija1 != null)
				{
					MissionManager.aktivnaIkonicaMisija1.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija1 = transform.Find("Mission Icons/Mission 1/Gorila Leteca").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija1.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform14 = transform.Find("Text/Mission 1");
				transform14.GetComponent<TextMesh>().text = array[0];
				transform14.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			else if (num == 2)
			{
				if (MissionManager.aktivnaIkonicaMisija2 != null)
				{
					MissionManager.aktivnaIkonicaMisija2.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija2 = transform.Find("Mission Icons/Mission 2/Gorila Leteca").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija2.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform15 = transform.Find("Text/Mission 2");
				transform15.GetComponent<TextMesh>().text = array[1];
				transform15.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			else if (num == 3)
			{
				if (MissionManager.aktivnaIkonicaMisija3 != null)
				{
					MissionManager.aktivnaIkonicaMisija3.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija3 = transform.Find("Mission Icons/Mission 3/Gorila Leteca").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija3.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform16 = transform.Find("Text/Mission 3");
				transform16.GetComponent<TextMesh>().text = array[2];
				transform16.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			num++;
		}
		if (MissionManager.activeKoplje_GorillaMission)
		{
			if (num == 1)
			{
				if (MissionManager.aktivnaIkonicaMisija1 != null)
				{
					MissionManager.aktivnaIkonicaMisija1.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija1 = transform.Find("Mission Icons/Mission 1/Gorila Sa Kopljem").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija1.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform17 = transform.Find("Text/Mission 1");
				transform17.GetComponent<TextMesh>().text = array[0];
				transform17.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			else if (num == 2)
			{
				if (MissionManager.aktivnaIkonicaMisija2 != null)
				{
					MissionManager.aktivnaIkonicaMisija2.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija2 = transform.Find("Mission Icons/Mission 2/Gorila Sa Kopljem").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija2.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform18 = transform.Find("Text/Mission 2");
				transform18.GetComponent<TextMesh>().text = array[1];
				transform18.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			else if (num == 3)
			{
				if (MissionManager.aktivnaIkonicaMisija3 != null)
				{
					MissionManager.aktivnaIkonicaMisija3.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija3 = transform.Find("Mission Icons/Mission 3/Gorila Sa Kopljem").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija3.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform19 = transform.Find("Text/Mission 3");
				transform19.GetComponent<TextMesh>().text = array[2];
				transform19.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			num++;
		}
		if (MissionManager.activeDiamondsMission)
		{
			if (num == 1)
			{
				if (MissionManager.aktivnaIkonicaMisija1 != null)
				{
					MissionManager.aktivnaIkonicaMisija1.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija1 = transform.Find("Mission Icons/Mission 1/3 Dijamanta").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija1.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform20 = transform.Find("Text/Mission 1");
				transform20.GetComponent<TextMesh>().text = array[0];
				transform20.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			else if (num == 2)
			{
				if (MissionManager.aktivnaIkonicaMisija2 != null)
				{
					MissionManager.aktivnaIkonicaMisija2.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija2 = transform.Find("Mission Icons/Mission 2/3 Dijamanta").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija2.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform21 = transform.Find("Text/Mission 2");
				transform21.GetComponent<TextMesh>().text = array[1];
				transform21.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			else if (num == 3)
			{
				if (MissionManager.aktivnaIkonicaMisija3 != null)
				{
					MissionManager.aktivnaIkonicaMisija3.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija3 = transform.Find("Mission Icons/Mission 3/3 Dijamanta").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija3.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform22 = transform.Find("Text/Mission 3");
				transform22.GetComponent<TextMesh>().text = array[2];
				transform22.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			num++;
		}
		if (MissionManager.activeCoinsMission)
		{
			if (num == 1)
			{
				if (MissionManager.aktivnaIkonicaMisija1 != null)
				{
					MissionManager.aktivnaIkonicaMisija1.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija1 = transform.Find("Mission Icons/Mission 1/Coin").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija1.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform23 = transform.Find("Text/Mission 1");
				transform23.GetComponent<TextMesh>().text = array[0];
				transform23.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			else if (num == 2)
			{
				if (MissionManager.aktivnaIkonicaMisija2 != null)
				{
					MissionManager.aktivnaIkonicaMisija2.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija2 = transform.Find("Mission Icons/Mission 2/Coin").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija2.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform24 = transform.Find("Text/Mission 2");
				transform24.GetComponent<TextMesh>().text = array[1];
				transform24.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			else if (num == 3)
			{
				if (MissionManager.aktivnaIkonicaMisija3 != null)
				{
					MissionManager.aktivnaIkonicaMisija3.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija3 = transform.Find("Mission Icons/Mission 3/Coin").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija3.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform25 = transform.Find("Text/Mission 3");
				transform25.GetComponent<TextMesh>().text = array[2];
				transform25.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			num++;
		}
		if (MissionManager.activeDistanceMission)
		{
			if (num == 1)
			{
				if (MissionManager.aktivnaIkonicaMisija1 != null)
				{
					MissionManager.aktivnaIkonicaMisija1.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija1 = transform.Find("Mission Icons/Mission 1/Distance").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija1.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform26 = transform.Find("Text/Mission 1");
				transform26.GetComponent<TextMesh>().text = array[0];
				transform26.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			else if (num == 2)
			{
				if (MissionManager.aktivnaIkonicaMisija2 != null)
				{
					MissionManager.aktivnaIkonicaMisija2.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija2 = transform.Find("Mission Icons/Mission 2/Distance").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija2.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform27 = transform.Find("Text/Mission 2");
				transform27.GetComponent<TextMesh>().text = array[1];
				transform27.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			else if (num == 3)
			{
				if (MissionManager.aktivnaIkonicaMisija3 != null)
				{
					MissionManager.aktivnaIkonicaMisija3.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija3 = transform.Find("Mission Icons/Mission 3/Distance").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija3.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform28 = transform.Find("Text/Mission 3");
				transform28.GetComponent<TextMesh>().text = array[2];
				transform28.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			num++;
		}
		if (MissionManager.activeBarrelsMission)
		{
			if (num == 1)
			{
				if (MissionManager.aktivnaIkonicaMisija1 != null)
				{
					MissionManager.aktivnaIkonicaMisija1.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija1 = transform.Find("Mission Icons/Mission 1/Bure").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija1.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform29 = transform.Find("Text/Mission 1");
				transform29.GetComponent<TextMesh>().text = array[0];
				transform29.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			else if (num == 2)
			{
				if (MissionManager.aktivnaIkonicaMisija2 != null)
				{
					MissionManager.aktivnaIkonicaMisija2.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija2 = transform.Find("Mission Icons/Mission 2/Bure").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija2.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform30 = transform.Find("Text/Mission 2");
				transform30.GetComponent<TextMesh>().text = array[1];
				transform30.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			else if (num == 3)
			{
				if (MissionManager.aktivnaIkonicaMisija3 != null)
				{
					MissionManager.aktivnaIkonicaMisija3.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija3 = transform.Find("Mission Icons/Mission 3/Bure").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija3.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform31 = transform.Find("Text/Mission 3");
				transform31.GetComponent<TextMesh>().text = array[2];
				transform31.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			num++;
		}
		if (MissionManager.activeRedDiamondsMission)
		{
			if (num == 1)
			{
				if (MissionManager.aktivnaIkonicaMisija1 != null)
				{
					MissionManager.aktivnaIkonicaMisija1.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija1 = transform.Find("Mission Icons/Mission 1/Crveni Dijamant").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija1.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform32 = transform.Find("Text/Mission 1");
				transform32.GetComponent<TextMesh>().text = array[0];
				transform32.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			else if (num == 2)
			{
				if (MissionManager.aktivnaIkonicaMisija2 != null)
				{
					MissionManager.aktivnaIkonicaMisija2.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija2 = transform.Find("Mission Icons/Mission 2/Crveni Dijamant").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija2.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform33 = transform.Find("Text/Mission 2");
				transform33.GetComponent<TextMesh>().text = array[1];
				transform33.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			else if (num == 3)
			{
				if (MissionManager.aktivnaIkonicaMisija3 != null)
				{
					MissionManager.aktivnaIkonicaMisija3.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija3 = transform.Find("Mission Icons/Mission 3/Crveni Dijamant").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija3.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform34 = transform.Find("Text/Mission 3");
				transform34.GetComponent<TextMesh>().text = array[2];
				transform34.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			num++;
		}
		if (MissionManager.activeBlueDiamondsMission)
		{
			if (num == 1)
			{
				if (MissionManager.aktivnaIkonicaMisija1 != null)
				{
					MissionManager.aktivnaIkonicaMisija1.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija1 = transform.Find("Mission Icons/Mission 1/Plavi Dijamant").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija1.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform35 = transform.Find("Text/Mission 1");
				transform35.GetComponent<TextMesh>().text = array[0];
				transform35.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			else if (num == 2)
			{
				if (MissionManager.aktivnaIkonicaMisija2 != null)
				{
					MissionManager.aktivnaIkonicaMisija2.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija2 = transform.Find("Mission Icons/Mission 2/Plavi Dijamant").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija2.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform36 = transform.Find("Text/Mission 2");
				transform36.GetComponent<TextMesh>().text = array[1];
				transform36.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			else if (num == 3)
			{
				if (MissionManager.aktivnaIkonicaMisija3 != null)
				{
					MissionManager.aktivnaIkonicaMisija3.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija3 = transform.Find("Mission Icons/Mission 3/Plavi Dijamant").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija3.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform37 = transform.Find("Text/Mission 3");
				transform37.GetComponent<TextMesh>().text = array[2];
				transform37.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			num++;
		}
		if (MissionManager.activeGreenDiamondsMission)
		{
			if (num == 1)
			{
				if (MissionManager.aktivnaIkonicaMisija1 != null)
				{
					MissionManager.aktivnaIkonicaMisija1.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija1 = transform.Find("Mission Icons/Mission 1/Zeleni Dijamant").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija1.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform38 = transform.Find("Text/Mission 1");
				transform38.GetComponent<TextMesh>().text = array[0];
				transform38.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			else if (num == 2)
			{
				if (MissionManager.aktivnaIkonicaMisija2 != null)
				{
					MissionManager.aktivnaIkonicaMisija2.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija2 = transform.Find("Mission Icons/Mission 2/Zeleni Dijamant").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija2.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform39 = transform.Find("Text/Mission 2");
				transform39.GetComponent<TextMesh>().text = array[1];
				transform39.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			else if (num == 3)
			{
				if (MissionManager.aktivnaIkonicaMisija3 != null)
				{
					MissionManager.aktivnaIkonicaMisija3.enabled = false;
				}
				MissionManager.aktivnaIkonicaMisija3 = transform.Find("Mission Icons/Mission 3/Zeleni Dijamant").GetComponent<Renderer>();
				MissionManager.aktivnaIkonicaMisija3.enabled = true;
				string[] array = MissionManager.missions[level].IspisiDescriptionNaIspravnomJeziku().Split(new string[]
				{
					"\n"
				}, StringSplitOptions.None);
				Transform transform40 = transform.Find("Text/Mission 3");
				transform40.GetComponent<TextMesh>().text = array[2];
				transform40.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			num++;
		}
	}

	// Token: 0x06002C62 RID: 11362 RVA: 0x0015AD7C File Offset: 0x00158F7C
	public void BaboonEvent(int currentBaboons)
	{
		if (MissionManager.missions[MissionManager.currentLevel].baboons > 0)
		{
			MissionManager.baboonsText.text = currentBaboons + "/" + MissionManager.missions[MissionManager.currentLevel].baboons;
			MissionManager.baboonsTextEffects.RefreshTextOutline(false, true, true);
			if (currentBaboons >= MissionManager.missions[MissionManager.currentLevel].baboons && MissionManager.baboonsText.color != Color.green)
			{
				MissionManager.NumberOfQuests--;
				MissionManager.baboonsText.color = Color.green;
			}
			if (MissionManager.NumberOfQuests <= 0)
			{
				this.MissionComplete();
			}
		}
	}

	// Token: 0x06002C63 RID: 11363 RVA: 0x0015AE30 File Offset: 0x00159030
	public void Fly_BaboonEvent(int currentFly_Baboons)
	{
		if (MissionManager.missions[MissionManager.currentLevel].fly_baboons > 0)
		{
			MissionManager.fly_baboonsText.text = currentFly_Baboons + "/" + MissionManager.missions[MissionManager.currentLevel].fly_baboons;
			MissionManager.fly_baboonsTextEffects.RefreshTextOutline(false, true, true);
			if (currentFly_Baboons >= MissionManager.missions[MissionManager.currentLevel].fly_baboons && MissionManager.fly_baboonsText.color != Color.green)
			{
				MissionManager.NumberOfQuests--;
				MissionManager.fly_baboonsText.color = Color.green;
			}
			if (MissionManager.NumberOfQuests <= 0)
			{
				this.MissionComplete();
			}
		}
	}

	// Token: 0x06002C64 RID: 11364 RVA: 0x0015AEE4 File Offset: 0x001590E4
	public void Boomerang_BaboonEvent(int currentBoomerang_Baboons)
	{
		if (MissionManager.missions[MissionManager.currentLevel].boomerang_baboons > 0)
		{
			MissionManager.boomerang_baboonsText.text = currentBoomerang_Baboons + "/" + MissionManager.missions[MissionManager.currentLevel].boomerang_baboons;
			MissionManager.boomerang_baboonsTextEffects.RefreshTextOutline(false, true, true);
			if (currentBoomerang_Baboons >= MissionManager.missions[MissionManager.currentLevel].boomerang_baboons && MissionManager.boomerang_baboonsText.color != Color.green)
			{
				MissionManager.NumberOfQuests--;
				MissionManager.boomerang_baboonsText.color = Color.green;
			}
			if (MissionManager.NumberOfQuests <= 0)
			{
				this.MissionComplete();
			}
		}
	}

	// Token: 0x06002C65 RID: 11365 RVA: 0x0015AF98 File Offset: 0x00159198
	public void GorillaEvent(int currentGorillas)
	{
		if (MissionManager.missions[MissionManager.currentLevel].gorilla > 0)
		{
			MissionManager.gorillaText.text = currentGorillas + "/" + MissionManager.missions[MissionManager.currentLevel].gorilla;
			MissionManager.gorillaTextEffects.RefreshTextOutline(false, true, true);
			if (currentGorillas >= MissionManager.missions[MissionManager.currentLevel].gorilla && MissionManager.gorillaText.color != Color.green)
			{
				MissionManager.NumberOfQuests--;
				MissionManager.gorillaText.color = Color.green;
			}
			if (MissionManager.NumberOfQuests <= 0)
			{
				this.MissionComplete();
			}
		}
	}

	// Token: 0x06002C66 RID: 11366 RVA: 0x0015B04C File Offset: 0x0015924C
	public void Fly_GorillaEvent(int currentFly_Gorillas)
	{
		if (MissionManager.missions[MissionManager.currentLevel].fly_gorilla > 0)
		{
			MissionManager.fly_gorillaText.text = currentFly_Gorillas + "/" + MissionManager.missions[MissionManager.currentLevel].fly_gorilla;
			MissionManager.fly_gorillaTextEffects.RefreshTextOutline(false, true, true);
			if (currentFly_Gorillas >= MissionManager.missions[MissionManager.currentLevel].fly_gorilla && MissionManager.fly_gorillaText.color != Color.green)
			{
				MissionManager.NumberOfQuests--;
				MissionManager.fly_gorillaText.color = Color.green;
				LevelFactory.instance.leteceGorile = 0;
			}
			if (MissionManager.NumberOfQuests <= 0)
			{
				this.MissionComplete();
			}
		}
	}

	// Token: 0x06002C67 RID: 11367 RVA: 0x0015B108 File Offset: 0x00159308
	public void Koplje_GorillaEvent(int currentKoplje_Gorillas)
	{
		if (MissionManager.missions[MissionManager.currentLevel].koplje_gorilla > 0)
		{
			MissionManager.koplje_gorillaText.text = currentKoplje_Gorillas + "/" + MissionManager.missions[MissionManager.currentLevel].koplje_gorilla;
			MissionManager.koplje_gorillaTextEffects.RefreshTextOutline(false, true, true);
			if (currentKoplje_Gorillas >= MissionManager.missions[MissionManager.currentLevel].koplje_gorilla && MissionManager.koplje_gorillaText.color != Color.green)
			{
				MissionManager.NumberOfQuests--;
				MissionManager.koplje_gorillaText.color = Color.green;
				LevelFactory.instance.kopljeGorile = 0;
			}
			if (MissionManager.NumberOfQuests <= 0)
			{
				this.MissionComplete();
			}
		}
	}

	// Token: 0x06002C68 RID: 11368 RVA: 0x0015B1C4 File Offset: 0x001593C4
	public void DiamondEvent(int currentDiamonds)
	{
		if (MissionManager.missions[MissionManager.currentLevel].diamonds > 0)
		{
			MissionManager.diamondsText.text = currentDiamonds + "/" + MissionManager.missions[MissionManager.currentLevel].diamonds;
			MissionManager.diamondsTextEffects.RefreshTextOutline(false, true, true);
			if (currentDiamonds >= MissionManager.missions[MissionManager.currentLevel].diamonds && MissionManager.diamondsText.color != Color.green)
			{
				MissionManager.NumberOfQuests--;
				MissionManager.diamondsText.color = Color.green;
			}
			if (MissionManager.NumberOfQuests <= 0)
			{
				this.MissionComplete();
			}
		}
	}

	// Token: 0x06002C69 RID: 11369 RVA: 0x0015B278 File Offset: 0x00159478
	public void CoinEvent(int currentCoins)
	{
		if (MissionManager.missions[MissionManager.currentLevel].coins > 0)
		{
			MissionManager.coinsText.text = currentCoins.ToString();
			MissionManager.coinsTextEffects.RefreshTextOutline(false, true, true);
			if (currentCoins >= MissionManager.missions[MissionManager.currentLevel].coins && MissionManager.coinsText.color != Color.green)
			{
				MissionManager.NumberOfQuests--;
				MissionManager.coinsText.color = Color.green;
				MissionManager.coinsText.transform.parent.Find("Target Number").GetComponent<TextMesh>().color = Color.green;
			}
			if (MissionManager.NumberOfQuests <= 0)
			{
				this.MissionComplete();
			}
		}
	}

	// Token: 0x06002C6A RID: 11370 RVA: 0x0015B334 File Offset: 0x00159534
	public void DistanceEvent(float currentDistance)
	{
		MissionManager.distanceText.text = currentDistance.ToString();
		MissionManager.distanceTextEffects.RefreshTextOutline(false, true, true);
		if (currentDistance % 10f == 0f && currentDistance != MissionManager.previousDistance)
		{
			Manage.points += 10;
			Manage.pointsText.text = Manage.points.ToString();
			Manage.pointsEffects.RefreshTextOutline(false, true, true);
			MissionManager.previousDistance = currentDistance;
		}
		if (currentDistance >= (float)MissionManager.missions[MissionManager.currentLevel].distance && MissionManager.distanceText.color != Color.green)
		{
			MissionManager.NumberOfQuests--;
			MissionManager.distanceText.color = Color.green;
			MissionManager.distanceText.transform.parent.Find("Target Number").GetComponent<TextMesh>().color = Color.green;
		}
		if (MissionManager.NumberOfQuests <= 0)
		{
			this.MissionComplete();
		}
	}

	// Token: 0x06002C6B RID: 11371 RVA: 0x0015B424 File Offset: 0x00159624
	public void BarrelEvent(int currentBarrels)
	{
		if (MissionManager.missions[MissionManager.currentLevel].barrels > 0)
		{
			MissionManager.barrelsText.text = currentBarrels + "/" + MissionManager.missions[MissionManager.currentLevel].barrels;
			MissionManager.barrelsTextEffects.RefreshTextOutline(false, true, true);
			if (currentBarrels >= MissionManager.missions[MissionManager.currentLevel].barrels && MissionManager.barrelsText.color != Color.green)
			{
				MissionManager.NumberOfQuests--;
				MissionManager.barrelsText.color = Color.green;
			}
			if (MissionManager.NumberOfQuests <= 0)
			{
				this.MissionComplete();
			}
		}
	}

	// Token: 0x06002C6C RID: 11372 RVA: 0x0015B4D8 File Offset: 0x001596D8
	public void RedDiamondEvent(int currentRedDiamonds)
	{
		if (MissionManager.missions[MissionManager.currentLevel].red_diamonds > 0)
		{
			MissionManager.redDiamondsText.text = currentRedDiamonds + "/" + MissionManager.missions[MissionManager.currentLevel].red_diamonds;
			MissionManager.redDiamondsTextEffects.RefreshTextOutline(false, true, true);
			if (currentRedDiamonds >= MissionManager.missions[MissionManager.currentLevel].red_diamonds && MissionManager.redDiamondsText.color != Color.green)
			{
				MissionManager.NumberOfQuests--;
				MissionManager.redDiamondsText.color = Color.green;
				LevelFactory.instance.crveniDijamant = 0;
			}
			if (MissionManager.NumberOfQuests <= 0)
			{
				this.MissionComplete();
			}
		}
	}

	// Token: 0x06002C6D RID: 11373 RVA: 0x0015B594 File Offset: 0x00159794
	public void BlueDiamondEvent(int currentBlueDiamonds)
	{
		if (MissionManager.missions[MissionManager.currentLevel].blue_diamonds > 0)
		{
			MissionManager.blueDiamondsText.text = currentBlueDiamonds + "/" + MissionManager.missions[MissionManager.currentLevel].blue_diamonds;
			MissionManager.blueDiamondsTextEffects.RefreshTextOutline(false, true, true);
			if (currentBlueDiamonds >= MissionManager.missions[MissionManager.currentLevel].blue_diamonds && MissionManager.blueDiamondsText.color != Color.green)
			{
				MissionManager.NumberOfQuests--;
				MissionManager.blueDiamondsText.color = Color.green;
				LevelFactory.instance.plaviDijamant = 0;
			}
			if (MissionManager.NumberOfQuests <= 0)
			{
				this.MissionComplete();
			}
		}
	}

	// Token: 0x06002C6E RID: 11374 RVA: 0x0015B650 File Offset: 0x00159850
	public void GreenDiamondEvent(int currentGreenDiamonds)
	{
		if (MissionManager.missions[MissionManager.currentLevel].green_diamonds > 0)
		{
			MissionManager.greenDiamondsText.text = currentGreenDiamonds + "/" + MissionManager.missions[MissionManager.currentLevel].green_diamonds;
			MissionManager.greenDiamondsTextEffects.RefreshTextOutline(false, true, true);
			if (currentGreenDiamonds >= MissionManager.missions[MissionManager.currentLevel].green_diamonds && MissionManager.greenDiamondsText.color != Color.green)
			{
				MissionManager.NumberOfQuests--;
				MissionManager.greenDiamondsText.color = Color.green;
				LevelFactory.instance.zeleniDijamant = 0;
			}
			if (MissionManager.NumberOfQuests <= 0)
			{
				this.MissionComplete();
			}
		}
	}

	// Token: 0x06002C6F RID: 11375 RVA: 0x0015B70C File Offset: 0x0015990C
	private void MissionComplete()
	{
		if (!MissionManager.postavioFinish)
		{
			MonkeyController2D.Instance.invincible = true;
			Manage.pauseEnabled = false;
			Manage.Instance.ApplyPowerUp(-1);
			Manage.Instance.ApplyPowerUp(-2);
			Manage.Instance.ApplyPowerUp(-3);
			MissionManager.postavioFinish = true;
			MissionManager.missionsComplete = true;
			MonkeyController2D.Instance.Finish();
		}
	}

	// Token: 0x06002C70 RID: 11376 RVA: 0x00021C72 File Offset: 0x0001FE72
	private void NotifyFinish()
	{
		MonkeyController2D.Instance.Finish();
	}

	// Token: 0x04002697 RID: 9879
	private string xmlName = "Missions.xml";

	// Token: 0x04002698 RID: 9880
	public static int totalMissions;

	// Token: 0x04002699 RID: 9881
	public static MissionTemplate[] missions;

	// Token: 0x0400269A RID: 9882
	public static bool activeBaboonsMission;

	// Token: 0x0400269B RID: 9883
	public static bool activeFly_BaboonsMission;

	// Token: 0x0400269C RID: 9884
	public static bool activeBoomerang_BaboonsMission;

	// Token: 0x0400269D RID: 9885
	public static bool activeGorillaMission;

	// Token: 0x0400269E RID: 9886
	public static bool activeFly_GorillaMission;

	// Token: 0x0400269F RID: 9887
	public static bool activeKoplje_GorillaMission;

	// Token: 0x040026A0 RID: 9888
	public static bool activeDiamondsMission;

	// Token: 0x040026A1 RID: 9889
	public static bool activeCoinsMission;

	// Token: 0x040026A2 RID: 9890
	public static bool activeDistanceMission;

	// Token: 0x040026A3 RID: 9891
	public static bool activeBarrelsMission;

	// Token: 0x040026A4 RID: 9892
	public static bool activeRedDiamondsMission;

	// Token: 0x040026A5 RID: 9893
	public static bool activeBlueDiamondsMission;

	// Token: 0x040026A6 RID: 9894
	public static bool activeGreenDiamondsMission;

	// Token: 0x040026A7 RID: 9895
	public static int NumberOfQuests;

	// Token: 0x040026A8 RID: 9896
	private TextMesh missionDescription;

	// Token: 0x040026A9 RID: 9897
	private static MissionManager instance;

	// Token: 0x040026AA RID: 9898
	private static int currentLevel;

	// Token: 0x040026AB RID: 9899
	private static TextMesh baboonsText;

	// Token: 0x040026AC RID: 9900
	private static TextMesh fly_baboonsText;

	// Token: 0x040026AD RID: 9901
	private static TextMesh boomerang_baboonsText;

	// Token: 0x040026AE RID: 9902
	private static TextMesh gorillaText;

	// Token: 0x040026AF RID: 9903
	private static TextMesh fly_gorillaText;

	// Token: 0x040026B0 RID: 9904
	private static TextMesh koplje_gorillaText;

	// Token: 0x040026B1 RID: 9905
	private static TextMesh diamondsText;

	// Token: 0x040026B2 RID: 9906
	private static TextMesh coinsText;

	// Token: 0x040026B3 RID: 9907
	private static TextMesh distanceText;

	// Token: 0x040026B4 RID: 9908
	private static TextMesh barrelsText;

	// Token: 0x040026B5 RID: 9909
	private static TextMesh redDiamondsText;

	// Token: 0x040026B6 RID: 9910
	private static TextMesh blueDiamondsText;

	// Token: 0x040026B7 RID: 9911
	private static TextMesh greenDiamondsText;

	// Token: 0x040026B8 RID: 9912
	private static TextMeshEffects baboonsTextEffects;

	// Token: 0x040026B9 RID: 9913
	private static TextMeshEffects fly_baboonsTextEffects;

	// Token: 0x040026BA RID: 9914
	private static TextMeshEffects boomerang_baboonsTextEffects;

	// Token: 0x040026BB RID: 9915
	private static TextMeshEffects gorillaTextEffects;

	// Token: 0x040026BC RID: 9916
	private static TextMeshEffects fly_gorillaTextEffects;

	// Token: 0x040026BD RID: 9917
	private static TextMeshEffects koplje_gorillaTextEffects;

	// Token: 0x040026BE RID: 9918
	private static TextMeshEffects diamondsTextEffects;

	// Token: 0x040026BF RID: 9919
	private static TextMeshEffects coinsTextEffects;

	// Token: 0x040026C0 RID: 9920
	private static TextMeshEffects distanceTextEffects;

	// Token: 0x040026C1 RID: 9921
	private static TextMeshEffects barrelsTextEffects;

	// Token: 0x040026C2 RID: 9922
	private static TextMeshEffects redDiamondsTextEffects;

	// Token: 0x040026C3 RID: 9923
	private static TextMeshEffects blueDiamondsTextEffects;

	// Token: 0x040026C4 RID: 9924
	private static TextMeshEffects greenDiamondsTextEffects;

	// Token: 0x040026C5 RID: 9925
	public static bool missionsComplete;

	// Token: 0x040026C6 RID: 9926
	private static bool postavioFinish;

	// Token: 0x040026C7 RID: 9927
	private static Renderer aktivnaIkonicaMisija1;

	// Token: 0x040026C8 RID: 9928
	private static Renderer aktivnaIkonicaMisija2;

	// Token: 0x040026C9 RID: 9929
	private static Renderer aktivnaIkonicaMisija3;

	// Token: 0x040026CA RID: 9930
	private static float previousDistance;

	// Token: 0x040026CB RID: 9931
	public static int points3Stars;
}
