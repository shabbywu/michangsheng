using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

// Token: 0x020004F6 RID: 1270
public class StagesParser : MonoBehaviour
{
	// Token: 0x170002B9 RID: 697
	// (get) Token: 0x06002921 RID: 10529 RVA: 0x00138324 File Offset: 0x00136524
	public static StagesParser Instance
	{
		get
		{
			if (StagesParser.instance == null)
			{
				StagesParser.instance = (Object.FindObjectOfType(typeof(StagesParser)) as StagesParser);
			}
			return StagesParser.instance;
		}
	}

	// Token: 0x06002922 RID: 10530 RVA: 0x00138354 File Offset: 0x00136554
	private void Awake()
	{
		StagesParser.instance = this;
		base.transform.name = "StagesParserManager";
		Object.DontDestroyOnLoad(base.gameObject);
		if (!PlayerPrefs.HasKey("TotalMoney"))
		{
			PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
			PlayerPrefs.Save();
		}
		else
		{
			StagesParser.currentMoney = PlayerPrefs.GetInt("TotalMoney");
		}
		if (!PlayerPrefs.HasKey("TotalBananas"))
		{
			PlayerPrefs.SetInt("TotalBananas", StagesParser.currentBananas);
			PlayerPrefs.Save();
		}
		else
		{
			StagesParser.currentBananas = PlayerPrefs.GetInt("TotalBananas");
		}
		if (!PlayerPrefs.HasKey("TotalPoints"))
		{
			PlayerPrefs.SetInt("TotalPoints", StagesParser.currentPoints);
			PlayerPrefs.Save();
		}
		else
		{
			StagesParser.currentPoints = PlayerPrefs.GetInt("TotalPoints");
		}
		if (PlayerPrefs.HasKey("PowerUps"))
		{
			string[] array = PlayerPrefs.GetString("PowerUps").Split(new char[]
			{
				'#'
			});
			StagesParser.powerup_doublecoins = int.Parse(array[0]);
			StagesParser.powerup_magnets = int.Parse(array[1]);
			StagesParser.powerup_shields = int.Parse(array[2]);
		}
		else
		{
			PlayerPrefs.SetString("PowerUps", string.Concat(new object[]
			{
				StagesParser.powerup_doublecoins,
				"#",
				StagesParser.powerup_magnets,
				"#",
				StagesParser.powerup_shields
			}));
			PlayerPrefs.Save();
		}
		if (PlayerPrefs.HasKey("OdgledaoTutorial"))
		{
			string[] array2 = PlayerPrefs.GetString("OdgledaoTutorial").Split(new char[]
			{
				'#'
			});
			StagesParser.odgledaoTutorial = int.Parse(array2[0]);
			StagesParser.otvaraoShopNekad = int.Parse(array2[1]);
		}
		if (PlayerPrefs.HasKey("LastLoggedUser"))
		{
			StagesParser.lastLoggedUser = PlayerPrefs.GetString("LastLoggedUser");
		}
		if (PlayerPrefs.HasKey("JezikPromenjen"))
		{
			StagesParser.jezikPromenjen = PlayerPrefs.GetInt("JezikPromenjen");
		}
		StagesParser.PointsPoNivoima = new int[120];
		StagesParser.StarsPoNivoima = new int[120];
		for (int i = 0; i < StagesParser.PointsPoNivoima.Length; i++)
		{
			StagesParser.PointsPoNivoima[i] = 0;
			StagesParser.StarsPoNivoima[i] = -1;
		}
		if (PlayerPrefs.HasKey("UserSveKupovineHats"))
		{
			StagesParser.svekupovineGlava = PlayerPrefs.GetString("UserSveKupovineHats");
		}
		if (PlayerPrefs.HasKey("UserSveKupovineShirts"))
		{
			StagesParser.svekupovineMajica = PlayerPrefs.GetString("UserSveKupovineShirts");
		}
		if (PlayerPrefs.HasKey("UserSveKupovineBackPacks"))
		{
			StagesParser.svekupovineLedja = PlayerPrefs.GetString("UserSveKupovineBackPacks");
		}
		StagesParser.allLevels = new string[120];
		if (!PlayerPrefs.HasKey("AllLevels"))
		{
			string text = "1#0#0";
			for (int j = 1; j < StagesParser.allLevels.Length; j++)
			{
				text = text + "_" + (j + 1).ToString() + "#-1#0";
			}
			StagesParser.allLevels = text.Split(new char[]
			{
				'_'
			});
			PlayerPrefs.SetString("AllLevels", text);
			PlayerPrefs.Save();
		}
		else
		{
			string[] array3 = new string[StagesParser.allLevels.Length];
			array3 = PlayerPrefs.GetString("AllLevels").Split(new char[]
			{
				'_'
			});
			if (array3.Length != StagesParser.allLevels.Length)
			{
				for (int k = 0; k < array3.Length; k++)
				{
					StagesParser.allLevels[k] = array3[k];
				}
				for (int l = array3.Length; l < StagesParser.allLevels.Length; l++)
				{
					StagesParser.allLevels[l] = (l + 1).ToString() + "#-1#0";
				}
				StagesParser.allLevels[array3.Length] = (array3.Length + 1).ToString() + "#0#0";
				string text2 = string.Empty;
				for (int m = 0; m < StagesParser.allLevels.Length; m++)
				{
					text2 += StagesParser.allLevels[m];
					text2 += "_";
				}
				text2 = text2.Remove(text2.Length - 1);
				PlayerPrefs.SetString("AllLevels", text2);
				PlayerPrefs.Save();
			}
			else
			{
				StagesParser.allLevels = PlayerPrefs.GetString("AllLevels").Split(new char[]
				{
					'_'
				});
			}
		}
		StagesParser.totalSets = 6;
		StagesParser.maxLevelNaOstrvu = new int[StagesParser.totalSets];
		StagesParser.SetsInGame = new Set[StagesParser.totalSets];
		StagesParser.trenutniNivoNaOstrvu = new int[StagesParser.totalSets];
		for (int n = 0; n < StagesParser.totalSets; n++)
		{
			int num = 20;
			StagesParser.SetsInGame[n] = new Set(num);
			StagesParser.SetsInGame[n].StagesOnSet = num;
			StagesParser.SetsInGame[n].SetID = (n + 1).ToString();
			StagesParser.SetsInGame[n].TotalStarsInStage += 3 * num;
			if (PlayerPrefs.HasKey("TrenutniNivoNaOstrvu" + n.ToString()))
			{
				StagesParser.trenutniNivoNaOstrvu[n] = PlayerPrefs.GetInt("TrenutniNivoNaOstrvu" + n.ToString());
			}
			else
			{
				StagesParser.trenutniNivoNaOstrvu[n] = 1;
			}
		}
		if (!PlayerPrefs.HasKey("BonusLevel"))
		{
			string text3 = string.Empty;
			for (int num2 = 0; num2 < StagesParser.totalSets; num2++)
			{
				text3 += "-1#-1#-1#-1_";
			}
			text3 = text3.Remove(text3.Length - 1);
			PlayerPrefs.SetString("BonusLevel", text3);
			PlayerPrefs.Save();
			StagesParser.bonusLevels = text3;
		}
		else
		{
			StagesParser.bonusLevels = PlayerPrefs.GetString("BonusLevel");
			string[] array4 = StagesParser.bonusLevels.Split(new char[]
			{
				'_'
			});
			if (array4.Length < StagesParser.totalSets)
			{
				for (int num3 = array4.Length; num3 < StagesParser.totalSets; num3++)
				{
					StagesParser.bonusLevels += "_-1#-1#-1#-1";
				}
				PlayerPrefs.SetString("BonusLevel", StagesParser.bonusLevels);
				PlayerPrefs.Save();
			}
		}
		StagesParser.totalStars = 0;
		StagesParser.currentStars = 0;
		for (int num4 = 0; num4 < StagesParser.totalSets; num4++)
		{
			StagesParser.totalStars += 3 * StagesParser.SetsInGame[num4].StagesOnSet;
			for (int num5 = 0; num5 < StagesParser.SetsInGame[num4].StagesOnSet; num5++)
			{
				StagesParser.currentStars += ((StagesParser.SetsInGame[num4].GetStarOnStage(num5) < 0) ? 0 : StagesParser.SetsInGame[num4].GetStarOnStage(num5));
			}
		}
		if (PlayerPrefs.HasKey("CurrentStars"))
		{
			StagesParser.currentStarsNEW = PlayerPrefs.GetInt("CurrentStars");
		}
		StagesParser.stagesLoaded = true;
		this.prefs = false;
		if (!PlayerPrefs.HasKey("Tour"))
		{
			this.tour = 1;
		}
		else
		{
			this.tour = PlayerPrefs.GetInt("Tour");
		}
		switch (this.tour)
		{
		case 1:
			StagesParser.SetsInGame[0].StarRequirement = 0;
			StagesParser.SetsInGame[1].StarRequirement = 40;
			StagesParser.SetsInGame[2].StarRequirement = 85;
			StagesParser.SetsInGame[3].StarRequirement = 135;
			StagesParser.SetsInGame[4].StarRequirement = 185;
			StagesParser.SetsInGame[5].StarRequirement = 235;
			break;
		case 2:
			StagesParser.SetsInGame[0].StarRequirement = 0;
			StagesParser.SetsInGame[1].StarRequirement = 50;
			StagesParser.SetsInGame[2].StarRequirement = 100;
			StagesParser.SetsInGame[3].StarRequirement = 150;
			StagesParser.SetsInGame[4].StarRequirement = 200;
			StagesParser.SetsInGame[5].StarRequirement = 260;
			break;
		case 3:
			StagesParser.SetsInGame[0].StarRequirement = 0;
			StagesParser.SetsInGame[1].StarRequirement = 55;
			StagesParser.SetsInGame[2].StarRequirement = 110;
			StagesParser.SetsInGame[3].StarRequirement = 165;
			StagesParser.SetsInGame[4].StarRequirement = 220;
			StagesParser.SetsInGame[5].StarRequirement = 280;
			break;
		}
		StagesParser.RecountTotalStars();
		for (int num6 = 0; num6 < StagesParser.totalSets; num6++)
		{
			if (StagesParser.currentStarsNEW >= StagesParser.SetsInGame[num6].StarRequirement && num6 > 0 && int.Parse(StagesParser.allLevels[(num6 - 1) * 20 + 19].Split(new char[]
			{
				'#'
			})[1]) > 0)
			{
				StagesParser.unlockedWorlds[num6] = true;
			}
		}
		StagesParser.unlockedWorlds[0] = true;
		for (int num7 = 0; num7 < StagesParser.totalSets; num7++)
		{
			if (PlayerPrefs.HasKey("WatchVideoWorld" + (num7 + 1)))
			{
				PlayerPrefs.DeleteKey("WatchVideoWorld" + (num7 + 1));
			}
		}
		base.StartCoroutine(this.checkInternetConnection());
	}

	// Token: 0x06002923 RID: 10531 RVA: 0x00138BE0 File Offset: 0x00136DE0
	public void ObrisiProgresNaLogOut()
	{
		int num = 0;
		int num2 = 0;
		float num3 = 0f;
		string text = string.Empty;
		string text2 = string.Empty;
		ShopManagerFull.ShopObject.OcistiMajmuna();
		if (PlayerPrefs.HasKey("LevelReward"))
		{
			num = PlayerPrefs.GetInt("LevelReward");
		}
		if (PlayerPrefs.HasKey("ProveriVreme"))
		{
			num2 = PlayerPrefs.GetInt("ProveriVreme");
		}
		if (PlayerPrefs.HasKey("VremeBrojaca"))
		{
			num3 = PlayerPrefs.GetFloat("VremeBrojaca");
		}
		if (PlayerPrefs.HasKey("VremeQuit"))
		{
			text = PlayerPrefs.GetString("VremeQuit");
		}
		if (PlayerPrefs.HasKey("OdgledaoTutorial"))
		{
			text2 = PlayerPrefs.GetString("OdgledaoTutorial");
		}
		PlayerPrefs.DeleteAll();
		PlayerPrefs.SetInt("Logovan", 0);
		PlayerPrefs.SetInt("Logout", 1);
		PlayerPrefs.SetInt("LevelReward", num);
		PlayerPrefs.SetInt("ProveriVreme", num2);
		PlayerPrefs.SetFloat("VremeBrojaca", num3);
		PlayerPrefs.SetString("VremeQuit", text);
		PlayerPrefs.SetString("OdgledaoTutorial", text2);
		PlayerPrefs.SetInt("VecPokrenuto", 1);
		string text3 = "1#0#0";
		for (int i = 1; i < StagesParser.allLevels.Length; i++)
		{
			text3 = text3 + "_" + (i + 1).ToString() + "#-1#0";
		}
		StagesParser.allLevels = text3.Split(new char[]
		{
			'_'
		});
		PlayerPrefs.SetString("AllLevels", text3);
		text3 = string.Empty;
		for (int j = 0; j < StagesParser.totalSets; j++)
		{
			text3 += "-1#-1#-1#-1_";
		}
		text3 = text3.Remove(text3.Length - 1);
		PlayerPrefs.SetString("BonusLevel", text3);
		StagesParser.bonusLevels = text3;
		for (int k = 0; k < StagesParser.totalSets; k++)
		{
			StagesParser.trenutniNivoNaOstrvu[k] = 1;
			PlayerPrefs.SetInt("TrenutniNivoNaOstrvu" + k.ToString(), StagesParser.trenutniNivoNaOstrvu[k]);
		}
		for (int l = 0; l < StagesParser.totalSets; l++)
		{
			StagesParser.unlockedWorlds[l] = false;
		}
		StagesParser.RecountTotalStars();
		for (int m = 0; m < StagesParser.totalSets; m++)
		{
			if (StagesParser.currentStarsNEW >= StagesParser.SetsInGame[m].StarRequirement && m > 0 && int.Parse(StagesParser.allLevels[(m - 1) * 20 + 19].Split(new char[]
			{
				'#'
			})[1]) > 0)
			{
				StagesParser.unlockedWorlds[m] = true;
			}
		}
		StagesParser.unlockedWorlds[0] = true;
		StagesParser.lastUnlockedWorldIndex = 0;
		StagesParser.imaUsi = true;
		StagesParser.imaKosu = true;
		StagesParser.majica = -1;
		StagesParser.glava = -1;
		StagesParser.ledja = -1;
		ShopManagerFull.AktivanSesir = -1;
		ShopManagerFull.AktivnaMajica = -1;
		ShopManagerFull.AktivanRanac = -1;
		StagesParser.bojaMajice = Color.white;
		StagesParser.svekupovineGlava = string.Empty;
		StagesParser.svekupovineMajica = string.Empty;
		StagesParser.svekupovineLedja = string.Empty;
		StagesParser.currentMoney = 0;
		StagesParser.currentBananas = 0;
		StagesParser.bananaCost = 2000;
		StagesParser.currentPoints = 0;
		StagesParser.powerup_magnets = 0;
		StagesParser.powerup_doublecoins = 0;
		StagesParser.powerup_shields = 0;
		StagesParser.cost_magnet = 150;
		StagesParser.cost_doublecoins = 300;
		StagesParser.cost_shield = 600;
		ShopManagerFull.ShopObject.transform.Find("3 Customize/Customize Tabovi/1Hats").gameObject.SetActive(true);
		ShopManagerFull.ShopObject.transform.Find("3 Customize/Customize Tabovi/2Shirts").gameObject.SetActive(true);
		ShopManagerFull.ShopObject.transform.Find("3 Customize/Customize Tabovi/3BackPack").gameObject.SetActive(true);
		LanguageManager.chosenLanguage = "_en";
		Camera.main.SendMessage("PromeniZastavuNaOsnovuImena", 1);
		Texture texture = Resources.Load("Zastave/0") as Texture;
		GameObject.FindGameObjectWithTag("Zastava").GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
		ShopManagerFull.ShopObject.SviItemiInvetory();
		ShopManagerFull.ShopObject.PobrisiSveOtkljucanoIzShopa();
		ShopManagerFull.ShopObject.RefresujImenaItema();
		FacebookManager.Ulogovan = false;
		PlayerPrefs.Save();
		this.UgasiLoading();
	}

	// Token: 0x06002924 RID: 10532 RVA: 0x00138FD4 File Offset: 0x001371D4
	public static void RecountTotalStars()
	{
		StagesParser.currentStarsNEW = 0;
		for (int i = 0; i < StagesParser.totalSets; i++)
		{
			StagesParser.maxLevelNaOstrvu[i] = 0;
			StagesParser.SetsInGame[i].CurrentStarsInStageNEW = 0;
			for (int j = 0; j < StagesParser.SetsInGame[i].StagesOnSet; j++)
			{
				string[] array = StagesParser.allLevels[i * 20 + j].Split(new char[]
				{
					'#'
				});
				StagesParser.SetsInGame[i].SetStarOnStage(j, int.Parse(array[1]));
				if (int.Parse(array[1]) > -1)
				{
					StagesParser.SetsInGame[i].CurrentStarsInStageNEW += int.Parse(array[1]);
					StagesParser.maxLevel = i * 20 + j + 1;
				}
				if (int.Parse(array[1]) > 0)
				{
					StagesParser.maxLevelNaOstrvu[i] = j + 1;
				}
				StagesParser.PointsPoNivoima[i * 20 + j] = int.Parse(array[2]);
				StagesParser.StarsPoNivoima[i * 20 + j] = int.Parse(array[1]);
			}
			StagesParser.currentStarsNEW += StagesParser.SetsInGame[i].CurrentStarsInStageNEW;
		}
	}

	// Token: 0x06002925 RID: 10533 RVA: 0x001390E8 File Offset: 0x001372E8
	public IEnumerator checkInternetConnection()
	{
		WWW www = new WWW("https://www.google.com");
		yield return www;
		if (!string.IsNullOrEmpty(www.error))
		{
			StagesParser.internetOn = false;
		}
		else
		{
			StagesParser.internetOn = true;
		}
		yield break;
	}

	// Token: 0x06002926 RID: 10534 RVA: 0x001390F0 File Offset: 0x001372F0
	public void CallLoad()
	{
		base.StartCoroutine("LoadStages");
	}

	// Token: 0x06002927 RID: 10535 RVA: 0x001390FE File Offset: 0x001372FE
	public void CallSave()
	{
		base.StartCoroutine("SaveStages");
	}

	// Token: 0x06002928 RID: 10536 RVA: 0x0013910C File Offset: 0x0013730C
	public IEnumerator LoadStages()
	{
		if (PlayerPrefs.HasKey("firstSave18819"))
		{
			Debug.Log("Ima key firstSave18819");
		}
		else
		{
			Debug.Log("NEMA key firstSave18819");
		}
		StagesParser.stagesLoaded = false;
		string filePath = string.Empty;
		string result = string.Empty;
		Debug.Log("Streaming assets: " + Path.Combine(Application.streamingAssetsPath, this.xmlName));
		Debug.Log("Persistent data: " + Path.Combine(Application.persistentDataPath, this.xmlName));
		if (PlayerPrefs.HasKey("firstSave18819") && File.Exists(Path.Combine(Application.persistentDataPath, this.xmlName)))
		{
			filePath = Path.Combine(Application.persistentDataPath, this.xmlName);
			Debug.Log("usao i pokusao: " + filePath);
		}
		else if (PlayerPrefs.HasKey("starsandstages"))
		{
			this.prefs = true;
			result = PlayerPrefs.GetString("starsandstages");
			Debug.Log("result je (iz load stages): " + result);
		}
		else
		{
			filePath = Path.Combine(Application.streamingAssetsPath, this.xmlName);
		}
		if (filePath.Contains("://"))
		{
			Debug.Log("koliko puta ovde");
			WWW www = new WWW(filePath);
			yield return www;
			if (!string.IsNullOrEmpty(www.error))
			{
				Debug.Log("Error se desio: " + www.error + ", " + filePath);
			}
			else
			{
				result = www.text;
				Debug.Log("result: " + result);
			}
			www = null;
		}
		else if (!this.prefs)
		{
			Debug.Log("Usao u persistent data");
			if (filePath == string.Empty)
			{
				Debug.Log("Uleteo ovde, prazna staza, brisi key");
				PlayerPrefs.DeleteKey("firstSave18819");
				base.StartCoroutine("LoadStages");
				yield break;
			}
			result = File.ReadAllText(filePath);
		}
		IEnumerable<XElement> source = XElement.Parse(result).Elements();
		StagesParser.totalSets = source.Count<XElement>();
		StagesParser.SetsInGame = new Set[StagesParser.totalSets];
		for (int i = 0; i < StagesParser.totalSets; i++)
		{
			IEnumerable<XElement> source2 = source.ElementAt(i).Elements();
			int num = source2.Count<XElement>();
			StagesParser.SetsInGame[i] = new Set(num);
			StagesParser.SetsInGame[i].StagesOnSet = num;
			StagesParser.SetsInGame[i].StarRequirement = int.Parse(source.ElementAt(i).Attribute("req").Value);
			StagesParser.SetsInGame[i].SetID = source.ElementAt(i).Attribute("id").Value;
			StagesParser.SetsInGame[i].TotalStarsInStage += 3 * num;
			for (int j = 0; j < num; j++)
			{
				StagesParser.SetsInGame[i].SetStarOnStage(j, int.Parse(source2.ElementAt(j).Value));
			}
		}
		StagesParser.totalStars = 0;
		StagesParser.currentStars = 0;
		for (int k = 0; k < StagesParser.totalSets; k++)
		{
			StagesParser.totalStars += 3 * StagesParser.SetsInGame[k].StagesOnSet;
			for (int l = 0; l < StagesParser.SetsInGame[k].StagesOnSet; l++)
			{
				StagesParser.currentStars += ((StagesParser.SetsInGame[k].GetStarOnStage(l) < 0) ? 0 : StagesParser.SetsInGame[k].GetStarOnStage(l));
			}
		}
		StagesParser.stagesLoaded = true;
		this.prefs = false;
		yield break;
	}

	// Token: 0x06002929 RID: 10537 RVA: 0x0013911B File Offset: 0x0013731B
	public IEnumerator SaveStages()
	{
		string text = string.Empty;
		string text2 = string.Empty;
		if (PlayerPrefs.HasKey("firstSave18819") && File.Exists(Path.Combine(Application.persistentDataPath, this.xmlName)))
		{
			text = Path.Combine(Application.persistentDataPath, this.xmlName);
		}
		else if (PlayerPrefs.HasKey("starsandstages"))
		{
			this.prefs = true;
			text2 = PlayerPrefs.GetString("starsandstages");
		}
		else
		{
			text = Path.Combine(Application.streamingAssetsPath, this.xmlName);
		}
		if (text.Contains("://"))
		{
			WWW www = new WWW(text);
			yield return www;
			text2 = www.text;
			www = null;
		}
		else if (!this.prefs)
		{
			text2 = File.ReadAllText(text);
		}
		XElement xelement = XElement.Parse(text2);
		IEnumerable<XElement> source = xelement.Elements();
		for (int i = 0; i < StagesParser.SetsInGame.Length; i++)
		{
			IEnumerable<XElement> source2 = source.ElementAt(i).Elements();
			for (int j = 0; j < StagesParser.SetsInGame[i].StagesOnSet; j++)
			{
				source2.ElementAt(j).Value = StagesParser.SetsInGame[i].GetStarOnStage(j).ToString();
			}
		}
		if (PlayerPrefs.HasKey("firstSave18819"))
		{
			Debug.Log("brisem iz persistent data xml");
			PlayerPrefs.DeleteKey("firstSave18819");
		}
		string text3 = xelement.ToString();
		PlayerPrefs.SetString("starsandstages", text3);
		PlayerPrefs.Save();
		StagesParser.saving = true;
		this.prefs = false;
		yield break;
	}

	// Token: 0x0600292A RID: 10538 RVA: 0x0013912A File Offset: 0x0013732A
	public IEnumerator moneyCounter(int kolicina, TextMesh moneyText, bool hasOutline)
	{
		if (kolicina > 0 && PlaySounds.soundOn)
		{
			PlaySounds.Play_CoinsSpent();
		}
		int current = int.Parse(moneyText.text);
		int suma = current + kolicina;
		int korak = (suma - current) / 10;
		while (current != suma)
		{
			current += korak;
			moneyText.text = current.ToString();
			if (hasOutline)
			{
				moneyText.GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
			}
			yield return new WaitForSeconds(0.07f);
		}
		moneyText.text = StagesParser.currentMoney.ToString();
		moneyText.GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		yield break;
	}

	// Token: 0x0600292B RID: 10539 RVA: 0x00139148 File Offset: 0x00137348
	public void UcitajLoadingPoruke()
	{
		IEnumerable<XElement> source = XElement.Parse(((TextAsset)Resources.Load("LoadingBackground/Loading" + LanguageManager.chosenLanguage)).ToString()).Elements();
		int num = source.Count<XElement>();
		for (int i = 0; i < num; i++)
		{
			StagesParser.RedniBrojSlike.Add(int.Parse(source.ElementAt(i).Attribute("redniBroj").Value));
		}
		StagesParser.LoadingPoruke.Add(source.ElementAt(0).Value);
		StagesParser.LoadingPoruke.Add(source.ElementAt(1).Value);
		StagesParser.LoadingPoruke.Add(source.ElementAt(2).Value);
		StagesParser.LoadingPoruke.Add(source.ElementAt(3).Value);
		StagesParser.LoadingPoruke.Add(source.ElementAt(4).Value);
		StagesParser.LoadingPoruke.Add(source.ElementAt(5).Value);
		StagesParser.LoadingPoruke.Add(source.ElementAt(6).Value);
		StagesParser.LoadingPoruke.Add(source.ElementAt(7).Value);
		StagesParser.LoadingPoruke.Add(source.ElementAt(8).Value);
		StagesParser.LoadingPoruke.Add(source.ElementAt(9).Value);
		StagesParser.LoadingPoruke.Add(source.ElementAt(10).Value);
		StagesParser.LoadingPoruke.Add(source.ElementAt(11).Value);
		StagesParser.LoadingPoruke.Add(source.ElementAt(12).Value);
		StagesParser.LoadingPoruke.Add(source.ElementAt(13).Value);
		StagesParser.LoadingPoruke.Add(source.ElementAt(14).Value);
		StagesParser.LoadingPoruke.Add(source.ElementAt(15).Value);
		StagesParser.LoadingPoruke.Add(source.ElementAt(16).Value);
		StagesParser.LoadingPoruke.Add(source.ElementAt(17).Value);
		StagesParser.LoadingPoruke.Add(source.ElementAt(18).Value);
		StagesParser.LoadingPoruke.Add(source.ElementAt(19).Value);
		StagesParser.LoadingPoruke.Add(source.ElementAt(20).Value);
		StagesParser.LoadingPoruke.Add(source.ElementAt(21).Value);
	}

	// Token: 0x0600292C RID: 10540 RVA: 0x001393AC File Offset: 0x001375AC
	public void CompareScores()
	{
		Debug.Log(string.Concat(new object[]
		{
			"COMPARE SCORES - FacebookManager.User: ",
			FacebookManager.User,
			", LastLoggedUser: ",
			StagesParser.lastLoggedUser,
			", LogoutPrefs: ",
			PlayerPrefs.GetInt("Logout")
		}));
		if (!FacebookManager.User.Equals(StagesParser.lastLoggedUser) || StagesParser.lastLoggedUser.Equals(string.Empty) || PlayerPrefs.GetInt("Logout") == 1)
		{
			Debug.Log("USAO U PRVI USLOV");
			StagesParser.currentMoney = FacebookManager.UserCoins;
			StagesParser.currentPoints = FacebookManager.UserScore;
			LanguageManager.chosenLanguage = FacebookManager.UserLanguage;
			StagesParser.currentBananas = FacebookManager.UserBanana;
			StagesParser.powerup_magnets = FacebookManager.UserPowerMagnet;
			StagesParser.powerup_shields = FacebookManager.UserPowerShield;
			StagesParser.powerup_doublecoins = FacebookManager.UserPowerDoubleCoins;
			StagesParser.glava = FacebookManager.GlavaItem;
			StagesParser.majica = FacebookManager.TeloItem;
			if (StagesParser.majica >= 0)
			{
				StagesParser.bojaMajice = ShopManagerFull.ShopObject.TShirtColors[StagesParser.majica];
			}
			StagesParser.ledja = FacebookManager.LedjaItem;
			StagesParser.imaUsi = FacebookManager.Usi;
			StagesParser.imaKosu = FacebookManager.Kosa;
			StagesParser.svekupovineGlava = FacebookManager.UserSveKupovineHats;
			StagesParser.svekupovineMajica = FacebookManager.UserSveKupovineShirts;
			StagesParser.svekupovineLedja = FacebookManager.UserSveKupovineBackPacks;
			StagesParser.bonusLevels = FacebookManager.bonusLevels;
			LanguageManager.RefreshTexts();
			for (int i = 0; i < StagesParser.totalSets; i++)
			{
				StagesParser.trenutniNivoNaOstrvu[i] = 1;
				PlayerPrefs.SetInt("TrenutniNivoNaOstrvu" + i.ToString(), StagesParser.trenutniNivoNaOstrvu[i]);
			}
			bool flag = false;
			for (int j = 0; j < FacebookManager.ListaStructPrijatelja.Count; j++)
			{
				FacebookManager.StrukturaPrijatelja strukturaPrijatelja = FacebookManager.ListaStructPrijatelja[j];
				if (strukturaPrijatelja.PrijateljID == FacebookManager.User && !flag)
				{
					flag = true;
					FacebookManager.indexUListaStructPrijatelja = j;
					for (int k = 0; k < strukturaPrijatelja.scores.Count; k++)
					{
						StagesParser.PointsPoNivoima[k] = strukturaPrijatelja.scores[k];
						StagesParser.StarsPoNivoima[k] = strukturaPrijatelja.stars[k];
					}
					StagesParser.maxLevel = strukturaPrijatelja.MaxLevel;
				}
			}
			string text = string.Empty;
			for (int l = 0; l < StagesParser.PointsPoNivoima.Length; l++)
			{
				text = string.Concat(new string[]
				{
					text,
					(l + 1).ToString(),
					"#",
					StagesParser.StarsPoNivoima[l].ToString(),
					"#",
					StagesParser.PointsPoNivoima[l].ToString(),
					"_"
				});
			}
			text = text.Remove(text.Length - 1);
			string[] array = text.Split(new char[]
			{
				'_'
			});
			if (StagesParser.allLevels.Length > array.Length)
			{
				for (int m = 0; m < array.Length; m++)
				{
					StagesParser.allLevels[m] = array[m];
				}
				for (int n = array.Length; n < StagesParser.allLevels.Length; n++)
				{
					StagesParser.allLevels[n] = (n + 1).ToString() + "#-1#0";
				}
				StagesParser.allLevels[array.Length] = array.Length.ToString() + "#0#0";
				text = string.Empty;
				for (int num = 0; num < StagesParser.allLevels.Length; num++)
				{
					text += StagesParser.allLevels[num];
					text += "_";
				}
				text = text.Remove(text.Length - 1);
			}
			else
			{
				StagesParser.allLevels = text.Split(new char[]
				{
					'_'
				});
			}
			string[] array2 = StagesParser.bonusLevels.Split(new char[]
			{
				'_'
			});
			if (array2.Length < StagesParser.totalSets)
			{
				for (int num2 = array2.Length; num2 < StagesParser.totalSets; num2++)
				{
					StagesParser.bonusLevels += "_-1#-1#-1#-1";
				}
				PlayerPrefs.SetString("BonusLevel", StagesParser.bonusLevels);
				PlayerPrefs.Save();
			}
			PlayerPrefs.SetString("AllLevels", text);
			StagesParser.RecountTotalStars();
			for (int num3 = 0; num3 < StagesParser.totalSets; num3++)
			{
				if (StagesParser.currentStarsNEW >= StagesParser.SetsInGame[num3].StarRequirement && num3 > 0 && int.Parse(StagesParser.allLevels[(num3 - 1) * 20 + 19].Split(new char[]
				{
					'#'
				})[1]) > 0)
				{
					StagesParser.unlockedWorlds[num3] = true;
					StagesParser.lastUnlockedWorldIndex = num3;
				}
			}
			StagesParser.unlockedWorlds[0] = true;
			Debug.Log("stigao do pred Shop");
			if (FacebookManager.MestoPozivanjaLogina != 2)
			{
				Debug.Log("login pozvan u 1. ili 3. sceni");
				ShopManagerFull.ShopObject.OcistiMajmuna();
				ShopManagerFull.ShopObject.transform.Find("3 Customize/Customize Tabovi/1Hats").gameObject.SetActive(true);
				ShopManagerFull.ShopObject.transform.Find("3 Customize/Customize Tabovi/2Shirts").gameObject.SetActive(true);
				ShopManagerFull.ShopObject.transform.Find("3 Customize/Customize Tabovi/3BackPack").gameObject.SetActive(true);
				StagesParser.svekupovineGlava = FacebookManager.UserSveKupovineHats;
				StagesParser.svekupovineMajica = FacebookManager.UserSveKupovineShirts;
				StagesParser.svekupovineLedja = FacebookManager.UserSveKupovineBackPacks;
				StagesParser.glava = FacebookManager.GlavaItem;
				StagesParser.majica = FacebookManager.TeloItem;
				if (StagesParser.majica >= 0)
				{
					StagesParser.bojaMajice = ShopManagerFull.ShopObject.TShirtColors[StagesParser.majica];
				}
				StagesParser.ledja = FacebookManager.LedjaItem;
				StagesParser.imaUsi = FacebookManager.Usi;
				StagesParser.imaKosu = FacebookManager.Kosa;
				ShopManagerFull.AktivanSesir = StagesParser.glava;
				ShopManagerFull.AktivnaMajica = StagesParser.majica;
				ShopManagerFull.AktivanRanac = StagesParser.ledja;
				string text2 = string.Concat(new string[]
				{
					ShopManagerFull.AktivanSesir.ToString(),
					"#",
					ShopManagerFull.AktivnaMajica.ToString(),
					"#",
					ShopManagerFull.AktivanRanac.ToString()
				});
				PlayerPrefs.SetString("AktivniItemi", text2);
				PlayerPrefs.Save();
				ShopManagerFull.ShopObject.SviItemiInvetory();
				ShopManagerFull.ShopObject.PobrisiSveOtkljucanoIzShopa();
				ShopManagerFull.ShopObject.RefresujImenaItema();
				if (FacebookManager.MestoPozivanjaLogina == 1)
				{
					ShopManagerFull.ShopObject.ObuciMajmunaNaStartu();
				}
			}
			else
			{
				StagesParser.obucenSeLogovaoNaDrugojSceni = true;
				Debug.Log("logovao se na 2. sceni");
			}
			PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
			PlayerPrefs.SetInt("TotalPoints", StagesParser.currentPoints);
			PlayerPrefs.SetString("choosenLanguage", LanguageManager.chosenLanguage);
			PlayerPrefs.SetInt("TotalBananas", StagesParser.currentBananas);
			PlayerPrefs.SetString("PowerUps", string.Concat(new object[]
			{
				StagesParser.powerup_doublecoins,
				"#",
				StagesParser.powerup_magnets,
				"#",
				StagesParser.powerup_shields
			}));
			PlayerPrefs.SetString("UserSveKupovineHats", StagesParser.svekupovineGlava);
			PlayerPrefs.SetString("UserSveKupovineShirts", StagesParser.svekupovineMajica);
			PlayerPrefs.SetString("UserSveKupovineBackPacks", StagesParser.svekupovineLedja);
			PlayerPrefs.SetString("BonusLevel", StagesParser.bonusLevels);
			PlayerPrefs.SetInt("Logout", 0);
			StagesParser.lastLoggedUser = FacebookManager.User;
			PlayerPrefs.SetString("LastLoggedUser", StagesParser.lastLoggedUser);
			PlayerPrefs.Save();
			StagesParser.ServerUpdate = 0;
			Debug.Log("ZAVRSIO USLOV");
		}
		else
		{
			Debug.Log("NIJE UPAO U USLOV");
		}
		if (FacebookManager.MestoPozivanjaLogina == 2)
		{
			Camera.main.SendMessage("RefreshScene", 1);
			if (FacebookManager.FacebookObject.zavrsioUcitavanje)
			{
				Debug.Log("^^^^^^ GOTOVO ZA FACEBOOK, SKLONI DUGME!!!!! LOKACIJA CS2");
				base.Invoke("UgasiLoading", 0.5f);
				PlayerPrefs.SetInt("Logovan", 1);
				if (FacebookManager.MestoPozivanjaLogina == 1)
				{
					FacebookManager.FacebookObject.RefreshujScenu1PosleLogina();
				}
				else if (FacebookManager.MestoPozivanjaLogina == 2)
				{
					FacebookManager.FacebookObject.RefreshujScenu2PosleLogina();
				}
				else if (FacebookManager.MestoPozivanjaLogina == 3)
				{
					FacebookManager.FacebookObject.RefreshujScenu3PosleLogina();
				}
				FacebookManager.FacebookObject.zavrsioUcitavanje = false;
				return;
			}
			FacebookManager.FacebookObject.zavrsioUcitavanje = true;
			Debug.Log("##### ZAVRSIO UCITAVANJE, LOKACIJA CS2");
			return;
		}
		else
		{
			if (FacebookManager.MestoPozivanjaLogina != 3)
			{
				if (FacebookManager.FacebookObject.zavrsioUcitavanje)
				{
					Debug.Log("^^^^^^ GOTOVO ZA FACEBOOK, SKLONI DUGME!!!!! LOKACIJA CS1");
					Transform transform = GameObject.Find("Loading Buffer HOLDER").transform;
					transform.position = new Vector3(0f, -70f, transform.position.z);
					transform.GetChild(0).GetComponent<Animator>().StopPlayback();
					PlayerPrefs.SetInt("Logovan", 1);
					if (FacebookManager.MestoPozivanjaLogina == 1)
					{
						FacebookManager.FacebookObject.RefreshujScenu1PosleLogina();
					}
					else if (FacebookManager.MestoPozivanjaLogina == 2)
					{
						FacebookManager.FacebookObject.RefreshujScenu2PosleLogina();
					}
					else if (FacebookManager.MestoPozivanjaLogina == 3)
					{
						FacebookManager.FacebookObject.RefreshujScenu3PosleLogina();
					}
					FacebookManager.FacebookObject.zavrsioUcitavanje = false;
				}
				else
				{
					FacebookManager.FacebookObject.zavrsioUcitavanje = true;
					Debug.Log("##### ZAVRSIO UCITAVANJE, LOKACIJA CS1");
				}
				Camera.main.SendMessage("PromeniZastavuNaOsnovuImena", 1);
				return;
			}
			Camera.main.SendMessage("RefreshScene", 1);
			if (FacebookManager.FacebookObject.zavrsioUcitavanje)
			{
				Debug.Log("^^^^^^ GOTOVO ZA FACEBOOK, SKLONI DUGME!!!!! LOKACIJA CS3");
				base.Invoke("UgasiLoading", 0.5f);
				PlayerPrefs.SetInt("Logovan", 1);
				if (FacebookManager.MestoPozivanjaLogina == 1)
				{
					FacebookManager.FacebookObject.RefreshujScenu1PosleLogina();
				}
				else if (FacebookManager.MestoPozivanjaLogina == 2)
				{
					FacebookManager.FacebookObject.RefreshujScenu2PosleLogina();
				}
				else if (FacebookManager.MestoPozivanjaLogina == 3)
				{
					FacebookManager.FacebookObject.RefreshujScenu3PosleLogina();
				}
				FacebookManager.FacebookObject.zavrsioUcitavanje = false;
				return;
			}
			FacebookManager.FacebookObject.zavrsioUcitavanje = true;
			Debug.Log("##### ZAVRSIO UCITAVANJE, LOKACIJA CS3");
			return;
		}
	}

	// Token: 0x0600292D RID: 10541 RVA: 0x00139D16 File Offset: 0x00137F16
	public void UgasiLoading()
	{
		GameObject.Find("Loading Buffer HOLDER").transform.GetChild(0).gameObject.SetActive(false);
	}

	// Token: 0x0600292E RID: 10542 RVA: 0x00139D38 File Offset: 0x00137F38
	public void ShopDeoIzCompareScores()
	{
		ShopManagerFull.ShopObject.OcistiMajmuna();
		ShopManagerFull.ShopObject.transform.Find("3 Customize/Customize Tabovi/1Hats").gameObject.SetActive(true);
		ShopManagerFull.ShopObject.transform.Find("3 Customize/Customize Tabovi/2Shirts").gameObject.SetActive(true);
		ShopManagerFull.ShopObject.transform.Find("3 Customize/Customize Tabovi/3BackPack").gameObject.SetActive(true);
		StagesParser.svekupovineGlava = FacebookManager.UserSveKupovineHats;
		StagesParser.svekupovineMajica = FacebookManager.UserSveKupovineShirts;
		StagesParser.svekupovineLedja = FacebookManager.UserSveKupovineBackPacks;
		StagesParser.glava = FacebookManager.GlavaItem;
		StagesParser.majica = FacebookManager.TeloItem;
		if (StagesParser.majica >= 0)
		{
			StagesParser.bojaMajice = ShopManagerFull.ShopObject.TShirtColors[StagesParser.majica];
		}
		StagesParser.ledja = FacebookManager.LedjaItem;
		StagesParser.imaUsi = FacebookManager.Usi;
		StagesParser.imaKosu = FacebookManager.Kosa;
		ShopManagerFull.AktivanSesir = StagesParser.glava;
		ShopManagerFull.AktivnaMajica = StagesParser.majica;
		ShopManagerFull.AktivanRanac = StagesParser.ledja;
		string text = string.Concat(new string[]
		{
			ShopManagerFull.AktivanSesir.ToString(),
			"#",
			ShopManagerFull.AktivnaMajica.ToString(),
			"#",
			ShopManagerFull.AktivanRanac.ToString()
		});
		PlayerPrefs.SetString("AktivniItemi", text);
		PlayerPrefs.Save();
		ShopManagerFull.ShopObject.SviItemiInvetory();
		ShopManagerFull.ShopObject.PobrisiSveOtkljucanoIzShopa();
		ShopManagerFull.ShopObject.RefresujImenaItema();
		if (Application.loadedLevel == 1)
		{
			ShopManagerFull.ShopObject.ObuciMajmunaNaStartu();
		}
	}

	// Token: 0x0400250E RID: 9486
	private string xmlName = "StarsAndStages.xml";

	// Token: 0x0400250F RID: 9487
	public static int totalSets = 0;

	// Token: 0x04002510 RID: 9488
	public static Set[] SetsInGame;

	// Token: 0x04002511 RID: 9489
	public static bool stagesLoaded = false;

	// Token: 0x04002512 RID: 9490
	public static bool saving = false;

	// Token: 0x04002513 RID: 9491
	public static int totalStars = 0;

	// Token: 0x04002514 RID: 9492
	public static int currentStars = 0;

	// Token: 0x04002515 RID: 9493
	public static int currSetIndex = 0;

	// Token: 0x04002516 RID: 9494
	public static int currStageIndex = 0;

	// Token: 0x04002517 RID: 9495
	public static int[] currWorldGridIndex = new int[5];

	// Token: 0x04002518 RID: 9496
	public static bool[] unlockedWorlds = new bool[6];

	// Token: 0x04002519 RID: 9497
	public static bool[] openedButNotPlayed = new bool[6];

	// Token: 0x0400251A RID: 9498
	public static bool isJustOpened = false;

	// Token: 0x0400251B RID: 9499
	public static bool NemaRequiredStars_VratiULevele = false;

	// Token: 0x0400251C RID: 9500
	public static bool nemojDaAnimirasZvezdice = false;

	// Token: 0x0400251D RID: 9501
	public static int starsGained = 0;

	// Token: 0x0400251E RID: 9502
	public static int animate = 0;

	// Token: 0x0400251F RID: 9503
	public static int currentMoney = 0;

	// Token: 0x04002520 RID: 9504
	public static int currentBananas = 0;

	// Token: 0x04002521 RID: 9505
	public static int bananaCost = 2000;

	// Token: 0x04002522 RID: 9506
	public static int currentPoints = 0;

	// Token: 0x04002523 RID: 9507
	public static int powerup_magnets = 0;

	// Token: 0x04002524 RID: 9508
	public static int powerup_doublecoins = 0;

	// Token: 0x04002525 RID: 9509
	public static int powerup_shields = 0;

	// Token: 0x04002526 RID: 9510
	public static int cost_magnet = 100;

	// Token: 0x04002527 RID: 9511
	public static int cost_doublecoins = 250;

	// Token: 0x04002528 RID: 9512
	public static int cost_shield = 500;

	// Token: 0x04002529 RID: 9513
	public static int numberGotKilled = 0;

	// Token: 0x0400252A RID: 9514
	public static int lastOpenedNotPlayedYet = 1;

	// Token: 0x0400252B RID: 9515
	public static int lastUnlockedWorldIndex = 0;

	// Token: 0x0400252C RID: 9516
	private bool prefs;

	// Token: 0x0400252D RID: 9517
	public static int worldToFocus = 0;

	// Token: 0x0400252E RID: 9518
	public static int levelToFocus = 0;

	// Token: 0x0400252F RID: 9519
	public static int currentWorld = 1;

	// Token: 0x04002530 RID: 9520
	public static int currentLevel = 1;

	// Token: 0x04002531 RID: 9521
	public static int totalWorlds = 6;

	// Token: 0x04002532 RID: 9522
	public static int currentStarsNEW = 0;

	// Token: 0x04002533 RID: 9523
	private int tour;

	// Token: 0x04002534 RID: 9524
	public static int maxLevel = 1;

	// Token: 0x04002535 RID: 9525
	public static bool maska = false;

	// Token: 0x04002536 RID: 9526
	public static int[] trenutniNivoNaOstrvu;

	// Token: 0x04002537 RID: 9527
	public static int nivoZaUcitavanje;

	// Token: 0x04002538 RID: 9528
	public static int zadnjiOtkljucanNivo = 0;

	// Token: 0x04002539 RID: 9529
	public static Vector3 pozicijaMajmuncetaNaMapi = Vector3.zero;

	// Token: 0x0400253A RID: 9530
	public static bool bonusLevel = false;

	// Token: 0x0400253B RID: 9531
	public static string bonusName;

	// Token: 0x0400253C RID: 9532
	public static int bonusID;

	// Token: 0x0400253D RID: 9533
	public static bool dodatnaProveraIzasaoIzBonusa = false;

	// Token: 0x0400253E RID: 9534
	public static bool bossStage = false;

	// Token: 0x0400253F RID: 9535
	public static bool vratioSeNaSvaOstrva = false;

	// Token: 0x04002540 RID: 9536
	public static List<string> LoadingPoruke = new List<string>();

	// Token: 0x04002541 RID: 9537
	public static int odgledaoTutorial = 0;

	// Token: 0x04002542 RID: 9538
	public static int loadingTip = -1;

	// Token: 0x04002543 RID: 9539
	public static int odgledanihTipova = 0;

	// Token: 0x04002544 RID: 9540
	public static int otvaraoShopNekad = 0;

	// Token: 0x04002545 RID: 9541
	public static bool imaUsi = true;

	// Token: 0x04002546 RID: 9542
	public static bool imaKosu = true;

	// Token: 0x04002547 RID: 9543
	public static int majica = -1;

	// Token: 0x04002548 RID: 9544
	public static int glava = -1;

	// Token: 0x04002549 RID: 9545
	public static int ledja = -1;

	// Token: 0x0400254A RID: 9546
	public static Color bojaMajice = Color.white;

	// Token: 0x0400254B RID: 9547
	public static string svekupovineGlava = string.Empty;

	// Token: 0x0400254C RID: 9548
	public static string svekupovineMajica = string.Empty;

	// Token: 0x0400254D RID: 9549
	public static string svekupovineLedja = string.Empty;

	// Token: 0x0400254E RID: 9550
	public static int[] PointsPoNivoima;

	// Token: 0x0400254F RID: 9551
	public static int[] StarsPoNivoima;

	// Token: 0x04002550 RID: 9552
	public static int[] maxLevelNaOstrvu;

	// Token: 0x04002551 RID: 9553
	public static List<int> RedniBrojSlike = new List<int>();

	// Token: 0x04002552 RID: 9554
	public static int ServerUpdate = 0;

	// Token: 0x04002553 RID: 9555
	public static string[] allLevels;

	// Token: 0x04002554 RID: 9556
	public static string bonusLevels;

	// Token: 0x04002555 RID: 9557
	public static int LoginReward = 1000;

	// Token: 0x04002556 RID: 9558
	public static int InviteReward = 100;

	// Token: 0x04002557 RID: 9559
	public static int ShareReward = 100;

	// Token: 0x04002558 RID: 9560
	public static int likePageReward = 1000;

	// Token: 0x04002559 RID: 9561
	public static int watchVideoReward = 1000;

	// Token: 0x0400255A RID: 9562
	public static bool internetOn = false;

	// Token: 0x0400255B RID: 9563
	public static string lastLoggedUser = string.Empty;

	// Token: 0x0400255C RID: 9564
	public static int brojIgranja = 0;

	// Token: 0x0400255D RID: 9565
	public static bool obucenSeLogovaoNaDrugojSceni = false;

	// Token: 0x0400255E RID: 9566
	public static bool ucitaoMainScenuPrviPut = false;

	// Token: 0x0400255F RID: 9567
	public static int jezikPromenjen = 0;

	// Token: 0x04002560 RID: 9568
	public static int sceneID = 0;

	// Token: 0x04002561 RID: 9569
	public static string languageBefore = "";

	// Token: 0x04002562 RID: 9570
	[Header("Rate Link Set Up:")]
	public string rateLink;

	// Token: 0x04002563 RID: 9571
	[Header("Advertisement Set Up:")]
	public string AdMobInterstitialID;

	// Token: 0x04002564 RID: 9572
	public string UnityAdsVideoGameID;

	// Token: 0x04002565 RID: 9573
	private static StagesParser instance;
}
