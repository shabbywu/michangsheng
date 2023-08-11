using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class StagesParser : MonoBehaviour
{
	private string xmlName = "StarsAndStages.xml";

	public static int totalSets = 0;

	public static Set[] SetsInGame;

	public static bool stagesLoaded = false;

	public static bool saving = false;

	public static int totalStars = 0;

	public static int currentStars = 0;

	public static int currSetIndex = 0;

	public static int currStageIndex = 0;

	public static int[] currWorldGridIndex = new int[5];

	public static bool[] unlockedWorlds = new bool[6];

	public static bool[] openedButNotPlayed = new bool[6];

	public static bool isJustOpened = false;

	public static bool NemaRequiredStars_VratiULevele = false;

	public static bool nemojDaAnimirasZvezdice = false;

	public static int starsGained = 0;

	public static int animate = 0;

	public static int currentMoney = 0;

	public static int currentBananas = 0;

	public static int bananaCost = 2000;

	public static int currentPoints = 0;

	public static int powerup_magnets = 0;

	public static int powerup_doublecoins = 0;

	public static int powerup_shields = 0;

	public static int cost_magnet = 100;

	public static int cost_doublecoins = 250;

	public static int cost_shield = 500;

	public static int numberGotKilled = 0;

	public static int lastOpenedNotPlayedYet = 1;

	public static int lastUnlockedWorldIndex = 0;

	private bool prefs;

	public static int worldToFocus = 0;

	public static int levelToFocus = 0;

	public static int currentWorld = 1;

	public static int currentLevel = 1;

	public static int totalWorlds = 6;

	public static int currentStarsNEW = 0;

	private int tour;

	public static int maxLevel = 1;

	public static bool maska = false;

	public static int[] trenutniNivoNaOstrvu;

	public static int nivoZaUcitavanje;

	public static int zadnjiOtkljucanNivo = 0;

	public static Vector3 pozicijaMajmuncetaNaMapi = Vector3.zero;

	public static bool bonusLevel = false;

	public static string bonusName;

	public static int bonusID;

	public static bool dodatnaProveraIzasaoIzBonusa = false;

	public static bool bossStage = false;

	public static bool vratioSeNaSvaOstrva = false;

	public static List<string> LoadingPoruke = new List<string>();

	public static int odgledaoTutorial = 0;

	public static int loadingTip = -1;

	public static int odgledanihTipova = 0;

	public static int otvaraoShopNekad = 0;

	public static bool imaUsi = true;

	public static bool imaKosu = true;

	public static int majica = -1;

	public static int glava = -1;

	public static int ledja = -1;

	public static Color bojaMajice = Color.white;

	public static string svekupovineGlava = string.Empty;

	public static string svekupovineMajica = string.Empty;

	public static string svekupovineLedja = string.Empty;

	public static int[] PointsPoNivoima;

	public static int[] StarsPoNivoima;

	public static int[] maxLevelNaOstrvu;

	public static List<int> RedniBrojSlike = new List<int>();

	public static int ServerUpdate = 0;

	public static string[] allLevels;

	public static string bonusLevels;

	public static int LoginReward = 1000;

	public static int InviteReward = 100;

	public static int ShareReward = 100;

	public static int likePageReward = 1000;

	public static int watchVideoReward = 1000;

	public static bool internetOn = false;

	public static string lastLoggedUser = string.Empty;

	public static int brojIgranja = 0;

	public static bool obucenSeLogovaoNaDrugojSceni = false;

	public static bool ucitaoMainScenuPrviPut = false;

	public static int jezikPromenjen = 0;

	public static int sceneID = 0;

	public static string languageBefore = "";

	[Header("Rate Link Set Up:")]
	public string rateLink;

	[Header("Advertisement Set Up:")]
	public string AdMobInterstitialID;

	public string UnityAdsVideoGameID;

	private static StagesParser instance;

	public static StagesParser Instance
	{
		get
		{
			if ((Object)(object)instance == (Object)null)
			{
				instance = Object.FindObjectOfType(typeof(StagesParser)) as StagesParser;
			}
			return instance;
		}
	}

	private void Awake()
	{
		instance = this;
		((Object)((Component)this).transform).name = "StagesParserManager";
		Object.DontDestroyOnLoad((Object)(object)((Component)this).gameObject);
		if (!PlayerPrefs.HasKey("TotalMoney"))
		{
			PlayerPrefs.SetInt("TotalMoney", currentMoney);
			PlayerPrefs.Save();
		}
		else
		{
			currentMoney = PlayerPrefs.GetInt("TotalMoney");
		}
		if (!PlayerPrefs.HasKey("TotalBananas"))
		{
			PlayerPrefs.SetInt("TotalBananas", currentBananas);
			PlayerPrefs.Save();
		}
		else
		{
			currentBananas = PlayerPrefs.GetInt("TotalBananas");
		}
		if (!PlayerPrefs.HasKey("TotalPoints"))
		{
			PlayerPrefs.SetInt("TotalPoints", currentPoints);
			PlayerPrefs.Save();
		}
		else
		{
			currentPoints = PlayerPrefs.GetInt("TotalPoints");
		}
		if (PlayerPrefs.HasKey("PowerUps"))
		{
			string[] array = PlayerPrefs.GetString("PowerUps").Split(new char[1] { '#' });
			powerup_doublecoins = int.Parse(array[0]);
			powerup_magnets = int.Parse(array[1]);
			powerup_shields = int.Parse(array[2]);
		}
		else
		{
			PlayerPrefs.SetString("PowerUps", powerup_doublecoins + "#" + powerup_magnets + "#" + powerup_shields);
			PlayerPrefs.Save();
		}
		if (PlayerPrefs.HasKey("OdgledaoTutorial"))
		{
			string[] array2 = PlayerPrefs.GetString("OdgledaoTutorial").Split(new char[1] { '#' });
			odgledaoTutorial = int.Parse(array2[0]);
			otvaraoShopNekad = int.Parse(array2[1]);
		}
		if (PlayerPrefs.HasKey("LastLoggedUser"))
		{
			lastLoggedUser = PlayerPrefs.GetString("LastLoggedUser");
		}
		if (PlayerPrefs.HasKey("JezikPromenjen"))
		{
			jezikPromenjen = PlayerPrefs.GetInt("JezikPromenjen");
		}
		PointsPoNivoima = new int[120];
		StarsPoNivoima = new int[120];
		for (int i = 0; i < PointsPoNivoima.Length; i++)
		{
			PointsPoNivoima[i] = 0;
			StarsPoNivoima[i] = -1;
		}
		if (PlayerPrefs.HasKey("UserSveKupovineHats"))
		{
			svekupovineGlava = PlayerPrefs.GetString("UserSveKupovineHats");
		}
		if (PlayerPrefs.HasKey("UserSveKupovineShirts"))
		{
			svekupovineMajica = PlayerPrefs.GetString("UserSveKupovineShirts");
		}
		if (PlayerPrefs.HasKey("UserSveKupovineBackPacks"))
		{
			svekupovineLedja = PlayerPrefs.GetString("UserSveKupovineBackPacks");
		}
		allLevels = new string[120];
		if (!PlayerPrefs.HasKey("AllLevels"))
		{
			string text = "1#0#0";
			for (int j = 1; j < allLevels.Length; j++)
			{
				text = text + "_" + (j + 1) + "#-1#0";
			}
			allLevels = text.Split(new char[1] { '_' });
			PlayerPrefs.SetString("AllLevels", text);
			PlayerPrefs.Save();
		}
		else
		{
			string[] array3 = new string[allLevels.Length];
			array3 = PlayerPrefs.GetString("AllLevels").Split(new char[1] { '_' });
			if (array3.Length != allLevels.Length)
			{
				for (int k = 0; k < array3.Length; k++)
				{
					allLevels[k] = array3[k];
				}
				for (int l = array3.Length; l < allLevels.Length; l++)
				{
					allLevels[l] = l + 1 + "#-1#0";
				}
				allLevels[array3.Length] = array3.Length + 1 + "#0#0";
				string text2 = string.Empty;
				for (int m = 0; m < allLevels.Length; m++)
				{
					text2 += allLevels[m];
					text2 += "_";
				}
				text2 = text2.Remove(text2.Length - 1);
				PlayerPrefs.SetString("AllLevels", text2);
				PlayerPrefs.Save();
			}
			else
			{
				allLevels = PlayerPrefs.GetString("AllLevels").Split(new char[1] { '_' });
			}
		}
		totalSets = 6;
		maxLevelNaOstrvu = new int[totalSets];
		SetsInGame = new Set[totalSets];
		trenutniNivoNaOstrvu = new int[totalSets];
		for (int n = 0; n < totalSets; n++)
		{
			int num = 20;
			SetsInGame[n] = new Set(num);
			SetsInGame[n].StagesOnSet = num;
			SetsInGame[n].SetID = (n + 1).ToString();
			SetsInGame[n].TotalStarsInStage += 3 * num;
			if (PlayerPrefs.HasKey("TrenutniNivoNaOstrvu" + n))
			{
				trenutniNivoNaOstrvu[n] = PlayerPrefs.GetInt("TrenutniNivoNaOstrvu" + n);
			}
			else
			{
				trenutniNivoNaOstrvu[n] = 1;
			}
		}
		if (!PlayerPrefs.HasKey("BonusLevel"))
		{
			string text3 = string.Empty;
			for (int num2 = 0; num2 < totalSets; num2++)
			{
				text3 += "-1#-1#-1#-1_";
			}
			text3 = text3.Remove(text3.Length - 1);
			PlayerPrefs.SetString("BonusLevel", text3);
			PlayerPrefs.Save();
			bonusLevels = text3;
		}
		else
		{
			bonusLevels = PlayerPrefs.GetString("BonusLevel");
			string[] array4 = bonusLevels.Split(new char[1] { '_' });
			if (array4.Length < totalSets)
			{
				for (int num3 = array4.Length; num3 < totalSets; num3++)
				{
					bonusLevels += "_-1#-1#-1#-1";
				}
				PlayerPrefs.SetString("BonusLevel", bonusLevels);
				PlayerPrefs.Save();
			}
		}
		totalStars = 0;
		currentStars = 0;
		for (int num4 = 0; num4 < totalSets; num4++)
		{
			totalStars += 3 * SetsInGame[num4].StagesOnSet;
			for (int num5 = 0; num5 < SetsInGame[num4].StagesOnSet; num5++)
			{
				currentStars += ((SetsInGame[num4].GetStarOnStage(num5) >= 0) ? SetsInGame[num4].GetStarOnStage(num5) : 0);
			}
		}
		if (PlayerPrefs.HasKey("CurrentStars"))
		{
			currentStarsNEW = PlayerPrefs.GetInt("CurrentStars");
		}
		stagesLoaded = true;
		prefs = false;
		if (!PlayerPrefs.HasKey("Tour"))
		{
			tour = 1;
		}
		else
		{
			tour = PlayerPrefs.GetInt("Tour");
		}
		switch (tour)
		{
		case 1:
			SetsInGame[0].StarRequirement = 0;
			SetsInGame[1].StarRequirement = 40;
			SetsInGame[2].StarRequirement = 85;
			SetsInGame[3].StarRequirement = 135;
			SetsInGame[4].StarRequirement = 185;
			SetsInGame[5].StarRequirement = 235;
			break;
		case 2:
			SetsInGame[0].StarRequirement = 0;
			SetsInGame[1].StarRequirement = 50;
			SetsInGame[2].StarRequirement = 100;
			SetsInGame[3].StarRequirement = 150;
			SetsInGame[4].StarRequirement = 200;
			SetsInGame[5].StarRequirement = 260;
			break;
		case 3:
			SetsInGame[0].StarRequirement = 0;
			SetsInGame[1].StarRequirement = 55;
			SetsInGame[2].StarRequirement = 110;
			SetsInGame[3].StarRequirement = 165;
			SetsInGame[4].StarRequirement = 220;
			SetsInGame[5].StarRequirement = 280;
			break;
		}
		RecountTotalStars();
		for (int num6 = 0; num6 < totalSets; num6++)
		{
			if (currentStarsNEW >= SetsInGame[num6].StarRequirement && num6 > 0 && int.Parse(allLevels[(num6 - 1) * 20 + 19].Split(new char[1] { '#' })[1]) > 0)
			{
				unlockedWorlds[num6] = true;
			}
		}
		unlockedWorlds[0] = true;
		for (int num7 = 0; num7 < totalSets; num7++)
		{
			if (PlayerPrefs.HasKey("WatchVideoWorld" + (num7 + 1)))
			{
				PlayerPrefs.DeleteKey("WatchVideoWorld" + (num7 + 1));
			}
		}
		((MonoBehaviour)this).StartCoroutine(checkInternetConnection());
	}

	public void ObrisiProgresNaLogOut()
	{
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
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
		for (int i = 1; i < allLevels.Length; i++)
		{
			text3 = text3 + "_" + (i + 1) + "#-1#0";
		}
		allLevels = text3.Split(new char[1] { '_' });
		PlayerPrefs.SetString("AllLevels", text3);
		text3 = string.Empty;
		for (int j = 0; j < totalSets; j++)
		{
			text3 += "-1#-1#-1#-1_";
		}
		text3 = text3.Remove(text3.Length - 1);
		PlayerPrefs.SetString("BonusLevel", text3);
		bonusLevels = text3;
		for (int k = 0; k < totalSets; k++)
		{
			trenutniNivoNaOstrvu[k] = 1;
			PlayerPrefs.SetInt("TrenutniNivoNaOstrvu" + k, trenutniNivoNaOstrvu[k]);
		}
		for (int l = 0; l < totalSets; l++)
		{
			unlockedWorlds[l] = false;
		}
		RecountTotalStars();
		for (int m = 0; m < totalSets; m++)
		{
			if (currentStarsNEW >= SetsInGame[m].StarRequirement && m > 0 && int.Parse(allLevels[(m - 1) * 20 + 19].Split(new char[1] { '#' })[1]) > 0)
			{
				unlockedWorlds[m] = true;
			}
		}
		unlockedWorlds[0] = true;
		lastUnlockedWorldIndex = 0;
		imaUsi = true;
		imaKosu = true;
		majica = -1;
		glava = -1;
		ledja = -1;
		ShopManagerFull.AktivanSesir = -1;
		ShopManagerFull.AktivnaMajica = -1;
		ShopManagerFull.AktivanRanac = -1;
		bojaMajice = Color.white;
		svekupovineGlava = string.Empty;
		svekupovineMajica = string.Empty;
		svekupovineLedja = string.Empty;
		currentMoney = 0;
		currentBananas = 0;
		bananaCost = 2000;
		currentPoints = 0;
		powerup_magnets = 0;
		powerup_doublecoins = 0;
		powerup_shields = 0;
		cost_magnet = 150;
		cost_doublecoins = 300;
		cost_shield = 600;
		((Component)((Component)ShopManagerFull.ShopObject).transform.Find("3 Customize/Customize Tabovi/1Hats")).gameObject.SetActive(true);
		((Component)((Component)ShopManagerFull.ShopObject).transform.Find("3 Customize/Customize Tabovi/2Shirts")).gameObject.SetActive(true);
		((Component)((Component)ShopManagerFull.ShopObject).transform.Find("3 Customize/Customize Tabovi/3BackPack")).gameObject.SetActive(true);
		LanguageManager.chosenLanguage = "_en";
		((Component)Camera.main).SendMessage("PromeniZastavuNaOsnovuImena", (SendMessageOptions)1);
		Object obj = Resources.Load("Zastave/0");
		Texture val = (Texture)(object)((obj is Texture) ? obj : null);
		GameObject.FindGameObjectWithTag("Zastava").GetComponent<Renderer>().material.SetTexture("_MainTex", val);
		ShopManagerFull.ShopObject.SviItemiInvetory();
		ShopManagerFull.ShopObject.PobrisiSveOtkljucanoIzShopa();
		ShopManagerFull.ShopObject.RefresujImenaItema();
		FacebookManager.Ulogovan = false;
		PlayerPrefs.Save();
		UgasiLoading();
	}

	public static void RecountTotalStars()
	{
		currentStarsNEW = 0;
		for (int i = 0; i < totalSets; i++)
		{
			maxLevelNaOstrvu[i] = 0;
			SetsInGame[i].CurrentStarsInStageNEW = 0;
			for (int j = 0; j < SetsInGame[i].StagesOnSet; j++)
			{
				string[] array = allLevels[i * 20 + j].Split(new char[1] { '#' });
				SetsInGame[i].SetStarOnStage(j, int.Parse(array[1]));
				if (int.Parse(array[1]) > -1)
				{
					SetsInGame[i].CurrentStarsInStageNEW += int.Parse(array[1]);
					maxLevel = i * 20 + j + 1;
				}
				if (int.Parse(array[1]) > 0)
				{
					maxLevelNaOstrvu[i] = j + 1;
				}
				PointsPoNivoima[i * 20 + j] = int.Parse(array[2]);
				StarsPoNivoima[i * 20 + j] = int.Parse(array[1]);
			}
			currentStarsNEW += SetsInGame[i].CurrentStarsInStageNEW;
		}
	}

	public IEnumerator checkInternetConnection()
	{
		WWW www = new WWW("https://www.google.com");
		yield return www;
		if (!string.IsNullOrEmpty(www.error))
		{
			internetOn = false;
		}
		else
		{
			internetOn = true;
		}
	}

	public void CallLoad()
	{
		((MonoBehaviour)this).StartCoroutine("LoadStages");
	}

	public void CallSave()
	{
		((MonoBehaviour)this).StartCoroutine("SaveStages");
	}

	public IEnumerator LoadStages()
	{
		if (PlayerPrefs.HasKey("firstSave18819"))
		{
			Debug.Log((object)"Ima key firstSave18819");
		}
		else
		{
			Debug.Log((object)"NEMA key firstSave18819");
		}
		stagesLoaded = false;
		string filePath = string.Empty;
		string result = string.Empty;
		Debug.Log((object)("Streaming assets: " + Path.Combine(Application.streamingAssetsPath, xmlName)));
		Debug.Log((object)("Persistent data: " + Path.Combine(Application.persistentDataPath, xmlName)));
		if (PlayerPrefs.HasKey("firstSave18819") && File.Exists(Path.Combine(Application.persistentDataPath, xmlName)))
		{
			filePath = Path.Combine(Application.persistentDataPath, xmlName);
			Debug.Log((object)("usao i pokusao: " + filePath));
		}
		else if (PlayerPrefs.HasKey("starsandstages"))
		{
			prefs = true;
			result = PlayerPrefs.GetString("starsandstages");
			Debug.Log((object)("result je (iz load stages): " + result));
		}
		else
		{
			filePath = Path.Combine(Application.streamingAssetsPath, xmlName);
		}
		if (filePath.Contains("://"))
		{
			Debug.Log((object)"koliko puta ovde");
			WWW www = new WWW(filePath);
			yield return www;
			if (!string.IsNullOrEmpty(www.error))
			{
				Debug.Log((object)("Error se desio: " + www.error + ", " + filePath));
			}
			else
			{
				result = www.text;
				Debug.Log((object)("result: " + result));
			}
		}
		else if (!prefs)
		{
			Debug.Log((object)"Usao u persistent data");
			if (filePath == string.Empty)
			{
				Debug.Log((object)"Uleteo ovde, prazna staza, brisi key");
				PlayerPrefs.DeleteKey("firstSave18819");
				((MonoBehaviour)this).StartCoroutine("LoadStages");
				yield break;
			}
			result = File.ReadAllText(filePath);
		}
		IEnumerable<XElement> source = ((XContainer)XElement.Parse(result)).Elements();
		totalSets = source.Count();
		SetsInGame = new Set[totalSets];
		for (int i = 0; i < totalSets; i++)
		{
			IEnumerable<XElement> source2 = ((XContainer)source.ElementAt(i)).Elements();
			int num = source2.Count();
			SetsInGame[i] = new Set(num);
			SetsInGame[i].StagesOnSet = num;
			SetsInGame[i].StarRequirement = int.Parse(source.ElementAt(i).Attribute(XName.op_Implicit("req")).Value);
			SetsInGame[i].SetID = source.ElementAt(i).Attribute(XName.op_Implicit("id")).Value;
			SetsInGame[i].TotalStarsInStage += 3 * num;
			for (int j = 0; j < num; j++)
			{
				SetsInGame[i].SetStarOnStage(j, int.Parse(source2.ElementAt(j).Value));
			}
		}
		totalStars = 0;
		currentStars = 0;
		for (int k = 0; k < totalSets; k++)
		{
			totalStars += 3 * SetsInGame[k].StagesOnSet;
			for (int l = 0; l < SetsInGame[k].StagesOnSet; l++)
			{
				currentStars += ((SetsInGame[k].GetStarOnStage(l) >= 0) ? SetsInGame[k].GetStarOnStage(l) : 0);
			}
		}
		stagesLoaded = true;
		prefs = false;
	}

	public IEnumerator SaveStages()
	{
		string text = string.Empty;
		string text2 = string.Empty;
		if (PlayerPrefs.HasKey("firstSave18819") && File.Exists(Path.Combine(Application.persistentDataPath, xmlName)))
		{
			text = Path.Combine(Application.persistentDataPath, xmlName);
		}
		else if (PlayerPrefs.HasKey("starsandstages"))
		{
			prefs = true;
			text2 = PlayerPrefs.GetString("starsandstages");
		}
		else
		{
			text = Path.Combine(Application.streamingAssetsPath, xmlName);
		}
		if (text.Contains("://"))
		{
			WWW www = new WWW(text);
			yield return www;
			text2 = www.text;
		}
		else if (!prefs)
		{
			text2 = File.ReadAllText(text);
		}
		XElement val = XElement.Parse(text2);
		IEnumerable<XElement> source = ((XContainer)val).Elements();
		for (int i = 0; i < SetsInGame.Length; i++)
		{
			IEnumerable<XElement> source2 = ((XContainer)source.ElementAt(i)).Elements();
			for (int j = 0; j < SetsInGame[i].StagesOnSet; j++)
			{
				source2.ElementAt(j).Value = SetsInGame[i].GetStarOnStage(j).ToString();
			}
		}
		if (PlayerPrefs.HasKey("firstSave18819"))
		{
			Debug.Log((object)"brisem iz persistent data xml");
			PlayerPrefs.DeleteKey("firstSave18819");
		}
		string text3 = ((object)val).ToString();
		PlayerPrefs.SetString("starsandstages", text3);
		PlayerPrefs.Save();
		saving = true;
		prefs = false;
	}

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
				((Component)moneyText).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			}
			yield return (object)new WaitForSeconds(0.07f);
		}
		moneyText.text = currentMoney.ToString();
		((Component)moneyText).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
	}

	public void UcitajLoadingPoruke()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		IEnumerable<XElement> source = ((XContainer)XElement.Parse(((object)(TextAsset)Resources.Load("LoadingBackground/Loading" + LanguageManager.chosenLanguage)).ToString())).Elements();
		int num = source.Count();
		for (int i = 0; i < num; i++)
		{
			RedniBrojSlike.Add(int.Parse(source.ElementAt(i).Attribute(XName.op_Implicit("redniBroj")).Value));
		}
		LoadingPoruke.Add(source.ElementAt(0).Value);
		LoadingPoruke.Add(source.ElementAt(1).Value);
		LoadingPoruke.Add(source.ElementAt(2).Value);
		LoadingPoruke.Add(source.ElementAt(3).Value);
		LoadingPoruke.Add(source.ElementAt(4).Value);
		LoadingPoruke.Add(source.ElementAt(5).Value);
		LoadingPoruke.Add(source.ElementAt(6).Value);
		LoadingPoruke.Add(source.ElementAt(7).Value);
		LoadingPoruke.Add(source.ElementAt(8).Value);
		LoadingPoruke.Add(source.ElementAt(9).Value);
		LoadingPoruke.Add(source.ElementAt(10).Value);
		LoadingPoruke.Add(source.ElementAt(11).Value);
		LoadingPoruke.Add(source.ElementAt(12).Value);
		LoadingPoruke.Add(source.ElementAt(13).Value);
		LoadingPoruke.Add(source.ElementAt(14).Value);
		LoadingPoruke.Add(source.ElementAt(15).Value);
		LoadingPoruke.Add(source.ElementAt(16).Value);
		LoadingPoruke.Add(source.ElementAt(17).Value);
		LoadingPoruke.Add(source.ElementAt(18).Value);
		LoadingPoruke.Add(source.ElementAt(19).Value);
		LoadingPoruke.Add(source.ElementAt(20).Value);
		LoadingPoruke.Add(source.ElementAt(21).Value);
	}

	public void CompareScores()
	{
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_0557: Unknown result type (might be due to invalid IL or missing references)
		Debug.Log((object)("COMPARE SCORES - FacebookManager.User: " + FacebookManager.User + ", LastLoggedUser: " + lastLoggedUser + ", LogoutPrefs: " + PlayerPrefs.GetInt("Logout")));
		if (!FacebookManager.User.Equals(lastLoggedUser) || lastLoggedUser.Equals(string.Empty) || PlayerPrefs.GetInt("Logout") == 1)
		{
			Debug.Log((object)"USAO U PRVI USLOV");
			currentMoney = FacebookManager.UserCoins;
			currentPoints = FacebookManager.UserScore;
			LanguageManager.chosenLanguage = FacebookManager.UserLanguage;
			currentBananas = FacebookManager.UserBanana;
			powerup_magnets = FacebookManager.UserPowerMagnet;
			powerup_shields = FacebookManager.UserPowerShield;
			powerup_doublecoins = FacebookManager.UserPowerDoubleCoins;
			glava = FacebookManager.GlavaItem;
			majica = FacebookManager.TeloItem;
			if (majica >= 0)
			{
				bojaMajice = ShopManagerFull.ShopObject.TShirtColors[majica];
			}
			ledja = FacebookManager.LedjaItem;
			imaUsi = FacebookManager.Usi;
			imaKosu = FacebookManager.Kosa;
			svekupovineGlava = FacebookManager.UserSveKupovineHats;
			svekupovineMajica = FacebookManager.UserSveKupovineShirts;
			svekupovineLedja = FacebookManager.UserSveKupovineBackPacks;
			bonusLevels = FacebookManager.bonusLevels;
			LanguageManager.RefreshTexts();
			for (int i = 0; i < totalSets; i++)
			{
				trenutniNivoNaOstrvu[i] = 1;
				PlayerPrefs.SetInt("TrenutniNivoNaOstrvu" + i, trenutniNivoNaOstrvu[i]);
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
						PointsPoNivoima[k] = strukturaPrijatelja.scores[k];
						StarsPoNivoima[k] = strukturaPrijatelja.stars[k];
					}
					maxLevel = strukturaPrijatelja.MaxLevel;
				}
			}
			string text = string.Empty;
			for (int l = 0; l < PointsPoNivoima.Length; l++)
			{
				text = text + (l + 1) + "#" + StarsPoNivoima[l] + "#" + PointsPoNivoima[l] + "_";
			}
			text = text.Remove(text.Length - 1);
			string[] array = text.Split(new char[1] { '_' });
			if (allLevels.Length > array.Length)
			{
				for (int m = 0; m < array.Length; m++)
				{
					allLevels[m] = array[m];
				}
				for (int n = array.Length; n < allLevels.Length; n++)
				{
					allLevels[n] = n + 1 + "#-1#0";
				}
				allLevels[array.Length] = array.Length + "#0#0";
				text = string.Empty;
				for (int num = 0; num < allLevels.Length; num++)
				{
					text += allLevels[num];
					text += "_";
				}
				text = text.Remove(text.Length - 1);
			}
			else
			{
				allLevels = text.Split(new char[1] { '_' });
			}
			string[] array2 = bonusLevels.Split(new char[1] { '_' });
			if (array2.Length < totalSets)
			{
				for (int num2 = array2.Length; num2 < totalSets; num2++)
				{
					bonusLevels += "_-1#-1#-1#-1";
				}
				PlayerPrefs.SetString("BonusLevel", bonusLevels);
				PlayerPrefs.Save();
			}
			PlayerPrefs.SetString("AllLevels", text);
			RecountTotalStars();
			for (int num3 = 0; num3 < totalSets; num3++)
			{
				if (currentStarsNEW >= SetsInGame[num3].StarRequirement && num3 > 0 && int.Parse(allLevels[(num3 - 1) * 20 + 19].Split(new char[1] { '#' })[1]) > 0)
				{
					unlockedWorlds[num3] = true;
					lastUnlockedWorldIndex = num3;
				}
			}
			unlockedWorlds[0] = true;
			Debug.Log((object)"stigao do pred Shop");
			if (FacebookManager.MestoPozivanjaLogina != 2)
			{
				Debug.Log((object)"login pozvan u 1. ili 3. sceni");
				ShopManagerFull.ShopObject.OcistiMajmuna();
				((Component)((Component)ShopManagerFull.ShopObject).transform.Find("3 Customize/Customize Tabovi/1Hats")).gameObject.SetActive(true);
				((Component)((Component)ShopManagerFull.ShopObject).transform.Find("3 Customize/Customize Tabovi/2Shirts")).gameObject.SetActive(true);
				((Component)((Component)ShopManagerFull.ShopObject).transform.Find("3 Customize/Customize Tabovi/3BackPack")).gameObject.SetActive(true);
				svekupovineGlava = FacebookManager.UserSveKupovineHats;
				svekupovineMajica = FacebookManager.UserSveKupovineShirts;
				svekupovineLedja = FacebookManager.UserSveKupovineBackPacks;
				glava = FacebookManager.GlavaItem;
				majica = FacebookManager.TeloItem;
				if (majica >= 0)
				{
					bojaMajice = ShopManagerFull.ShopObject.TShirtColors[majica];
				}
				ledja = FacebookManager.LedjaItem;
				imaUsi = FacebookManager.Usi;
				imaKosu = FacebookManager.Kosa;
				ShopManagerFull.AktivanSesir = glava;
				ShopManagerFull.AktivnaMajica = majica;
				ShopManagerFull.AktivanRanac = ledja;
				string text2 = ShopManagerFull.AktivanSesir + "#" + ShopManagerFull.AktivnaMajica + "#" + ShopManagerFull.AktivanRanac;
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
				obucenSeLogovaoNaDrugojSceni = true;
				Debug.Log((object)"logovao se na 2. sceni");
			}
			PlayerPrefs.SetInt("TotalMoney", currentMoney);
			PlayerPrefs.SetInt("TotalPoints", currentPoints);
			PlayerPrefs.SetString("choosenLanguage", LanguageManager.chosenLanguage);
			PlayerPrefs.SetInt("TotalBananas", currentBananas);
			PlayerPrefs.SetString("PowerUps", powerup_doublecoins + "#" + powerup_magnets + "#" + powerup_shields);
			PlayerPrefs.SetString("UserSveKupovineHats", svekupovineGlava);
			PlayerPrefs.SetString("UserSveKupovineShirts", svekupovineMajica);
			PlayerPrefs.SetString("UserSveKupovineBackPacks", svekupovineLedja);
			PlayerPrefs.SetString("BonusLevel", bonusLevels);
			PlayerPrefs.SetInt("Logout", 0);
			lastLoggedUser = FacebookManager.User;
			PlayerPrefs.SetString("LastLoggedUser", lastLoggedUser);
			PlayerPrefs.Save();
			ServerUpdate = 0;
			Debug.Log((object)"ZAVRSIO USLOV");
		}
		else
		{
			Debug.Log((object)"NIJE UPAO U USLOV");
		}
		if (FacebookManager.MestoPozivanjaLogina == 2)
		{
			((Component)Camera.main).SendMessage("RefreshScene", (SendMessageOptions)1);
			if (FacebookManager.FacebookObject.zavrsioUcitavanje)
			{
				Debug.Log((object)"^^^^^^ GOTOVO ZA FACEBOOK, SKLONI DUGME!!!!! LOKACIJA CS2");
				((MonoBehaviour)this).Invoke("UgasiLoading", 0.5f);
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
				Debug.Log((object)"##### ZAVRSIO UCITAVANJE, LOKACIJA CS2");
			}
			return;
		}
		if (FacebookManager.MestoPozivanjaLogina == 3)
		{
			((Component)Camera.main).SendMessage("RefreshScene", (SendMessageOptions)1);
			if (FacebookManager.FacebookObject.zavrsioUcitavanje)
			{
				Debug.Log((object)"^^^^^^ GOTOVO ZA FACEBOOK, SKLONI DUGME!!!!! LOKACIJA CS3");
				((MonoBehaviour)this).Invoke("UgasiLoading", 0.5f);
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
				Debug.Log((object)"##### ZAVRSIO UCITAVANJE, LOKACIJA CS3");
			}
			return;
		}
		if (FacebookManager.FacebookObject.zavrsioUcitavanje)
		{
			Debug.Log((object)"^^^^^^ GOTOVO ZA FACEBOOK, SKLONI DUGME!!!!! LOKACIJA CS1");
			Transform transform = GameObject.Find("Loading Buffer HOLDER").transform;
			transform.position = new Vector3(0f, -70f, transform.position.z);
			((Component)transform.GetChild(0)).GetComponent<Animator>().StopPlayback();
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
			Debug.Log((object)"##### ZAVRSIO UCITAVANJE, LOKACIJA CS1");
		}
		((Component)Camera.main).SendMessage("PromeniZastavuNaOsnovuImena", (SendMessageOptions)1);
	}

	public void UgasiLoading()
	{
		((Component)GameObject.Find("Loading Buffer HOLDER").transform.GetChild(0)).gameObject.SetActive(false);
	}

	public void ShopDeoIzCompareScores()
	{
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		ShopManagerFull.ShopObject.OcistiMajmuna();
		((Component)((Component)ShopManagerFull.ShopObject).transform.Find("3 Customize/Customize Tabovi/1Hats")).gameObject.SetActive(true);
		((Component)((Component)ShopManagerFull.ShopObject).transform.Find("3 Customize/Customize Tabovi/2Shirts")).gameObject.SetActive(true);
		((Component)((Component)ShopManagerFull.ShopObject).transform.Find("3 Customize/Customize Tabovi/3BackPack")).gameObject.SetActive(true);
		svekupovineGlava = FacebookManager.UserSveKupovineHats;
		svekupovineMajica = FacebookManager.UserSveKupovineShirts;
		svekupovineLedja = FacebookManager.UserSveKupovineBackPacks;
		glava = FacebookManager.GlavaItem;
		majica = FacebookManager.TeloItem;
		if (majica >= 0)
		{
			bojaMajice = ShopManagerFull.ShopObject.TShirtColors[majica];
		}
		ledja = FacebookManager.LedjaItem;
		imaUsi = FacebookManager.Usi;
		imaKosu = FacebookManager.Kosa;
		ShopManagerFull.AktivanSesir = glava;
		ShopManagerFull.AktivnaMajica = majica;
		ShopManagerFull.AktivanRanac = ledja;
		string text = ShopManagerFull.AktivanSesir + "#" + ShopManagerFull.AktivnaMajica + "#" + ShopManagerFull.AktivanRanac;
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
}
