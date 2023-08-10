using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks2;
using Facebook;
using Facebook.MiniJSON;
using Parse;
using UnityEngine;

public class FacebookManager : MonoBehaviour
{
	public struct IDiSlika
	{
		public string PrijateljID;

		public Texture profilePicture;
	}

	public struct StrukturaPrijatelja
	{
		public string PrijateljID;

		public IList<int> scores;

		public IList<int> stars;

		public int MaxLevel;

		public Texture profilePicture;
	}

	public static string UserSveKupovineHats;

	public static string UserSveKupovineShirts;

	public static string UserSveKupovineBackPacks;

	public static int UserCoins;

	public static string bonusLevels;

	public static int UserScore;

	public static string UserLanguage;

	public static int UserBanana;

	public static int UserPowerMagnet;

	public static int UserPowerShield;

	public static int UserPowerDoubleCoins;

	public static int GlavaItem;

	public static int TeloItem;

	public static int LedjaItem;

	public static bool Usi;

	public static bool Kosa;

	public static int indexUListaStructPrijatelja;

	public static int MestoPozivanjaLogina = 1;

	public static bool KorisnikoviPodaciSpremni = false;

	private bool isInit;

	public static FacebookManager FacebookObject;

	private Texture2D lastResponseTexture;

	private string ApiQuery = "";

	public static bool Ulogovan;

	public static string stranica = string.Empty;

	private static bool otisaoDaLajkuje = false;

	public static string IDstranice = string.Empty;

	private static bool lajkovao = false;

	private static int nagrada = 0;

	private DateTime timeToShowNextElement;

	private bool leftApp;

	public static string lokacijaProvere = "Shop";

	public static string User;

	private string UserRodjendan;

	private List<string> Prijatelji;

	public static List<IDiSlika> ProfileSlikePrijatelja = new List<IDiSlika>();

	public List<string> Korisnici;

	public List<string> Scorovi;

	public List<string> Imena;

	public static int NumberOfFriends;

	public bool odobrioPublishActions;

	public int scoreToSet;

	public bool nePostojiKorisnik = true;

	public bool zavrsioUcitavanje;

	public static string[] permisija;

	public static string[] statusPermisije;

	public static List<StrukturaPrijatelja> ListaStructPrijatelja = new List<StrukturaPrijatelja>();

	private int TrenutniNivoIgraca;

	private int[] scorePoNivouPrijatelja = new int[120];

	private int[] ScorePoNivoimaNiz = new int[120];

	private int[] BrojZvezdaPoNivouNiz = new int[120];

	private int[] testNiz = new int[120];

	private bool WaitForFacebook;

	private bool WaitForFacebookFriend;

	public static string UserName;

	public static Texture ProfilePicture;

	public static Texture FriendPic1;

	public static Texture FriendPic2;

	public static Texture FriendPic3;

	public static Texture FriendPic4;

	public static Texture FriendPic5;

	private string permissions = "user_friends,publish_actions";

	private string Code;

	private string TipNagrade;

	private int IznosNagrade;

	private ParseObject Korisnik = new ParseObject("User");

	private ParseObject LevelScore = new ParseObject("LevelScore");

	private string JezikServer;

	private int NivoServer;

	private bool updatedSuccessfullyScoreNaNivoima;

	private bool updatedSuccessfullyPodaciKorisnika;

	[HideInInspector]
	public bool OKzaLogout;

	[HideInInspector]
	public int resetovanScoreNaNulu;

	private string FriendSelectorTitle = "Banana Island - Bobo's Epic Tale";

	private string FriendSelectorMessage = LanguageManager.Play + " \"Banana Island - Bobo's Epic Tale\"";

	private string FriendSelectorFilters = "[\"all\",\"app_users\",\"app_non_users\"]";

	private string FriendSelectorData = "{}";

	private string FriendSelectorExcludeIds = "";

	private string FriendSelectorMax = "";

	private string lastResponse = "";

	private string FeedLinkKratakOpis = " ";

	private string LinkSlike = "https://trello-attachments.s3.amazonaws.com/52fb9e010aaea80557f83f1f/1024x500/d64a4cc319932dde0630258aee32d7d4/Feature-Graphic.jpg";

	private string LinkVideailiZvuka = "";

	private string FeedActionName = "";

	private string FeedActionLink = "";

	private string FeedReference = "";

	private bool IncludeFeedProperties;

	private Dictionary<string, string[]> FeedProperties = new Dictionary<string, string[]>();

	private int nesto;

	public int BrojPrijatelja;

	private string URL;

	private string prijateljevIDzaSliku;

	private void Awake()
	{
		FacebookObject = this;
		Object.DontDestroyOnLoad((Object)(object)((Component)this).gameObject);
	}

	private void Start()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected O, but got Unknown
		//IL_0037: Expected O, but got Unknown
		for (int i = 0; i < 100; i++)
		{
			testNiz[i] = i + 100;
		}
		FB.Init(new InitDelegate(OnInitComplete), new HideUnityDelegate(OnHideUnity));
		if (PlayerPrefs.HasKey("UserSveKupovineHats"))
		{
			UserSveKupovineHats = PlayerPrefs.GetString("UserSveKupovineHats");
		}
		else
		{
			UserSveKupovineHats = "0#0#";
		}
		if (PlayerPrefs.HasKey("UserSveKupovineShirts"))
		{
			UserSveKupovineShirts = PlayerPrefs.GetString("UserSveKupovineShirts");
		}
		else
		{
			UserSveKupovineShirts = "0#0#0#0#0#0#0#0#";
		}
		if (PlayerPrefs.HasKey("UserSveKupovineBackPacks"))
		{
			UserSveKupovineBackPacks = PlayerPrefs.GetString("UserSveKupovineBackPacks");
		}
		else
		{
			UserSveKupovineBackPacks = "0#0#0#0#0#0#";
		}
		StagesParser.svekupovineGlava = UserSveKupovineHats;
		StagesParser.svekupovineMajica = UserSveKupovineShirts;
		StagesParser.svekupovineLedja = UserSveKupovineBackPacks;
		if (FB.IsLoggedIn)
		{
			Ulogovan = true;
		}
		else
		{
			Ulogovan = false;
		}
	}

	public static void FacebookLogout()
	{
		if (FB.IsLoggedIn)
		{
			FB.Logout();
			Ulogovan = false;
			StagesParser.Instance.ObrisiProgresNaLogOut();
		}
	}

	private void OnInitComplete()
	{
		isInit = true;
	}

	private void OnHideUnity(bool isGameShown)
	{
		Debug.Log((object)("Is game showing? " + isGameShown));
	}

	public void FacebookLogin()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		FB.Login(permissions, new FacebookDelegate(LoginCallback));
	}

	public void LoginCallback(FBResult result)
	{
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		if (result.Error != null)
		{
			StagesParser.Instance.UgasiLoading();
			return;
		}
		if (!FB.IsLoggedIn)
		{
			StagesParser.Instance.UgasiLoading();
			return;
		}
		Ulogovan = true;
		User = FB.UserId;
		PlayerPrefs.SetInt("Logovan", 0);
		PlayerPrefs.Save();
		GetFacebookName();
		if (MestoPozivanjaLogina == 1)
		{
			Transform transform = GameObject.Find("Loading Buffer HOLDER").transform;
			Transform transform2 = ((Component)Camera.main).transform;
			transform.position = new Vector3(transform2.position.x, transform2.position.y, transform.position.z);
			((Component)transform.GetChild(0)).gameObject.SetActive(true);
			((Component)transform.GetChild(0)).GetComponent<Animator>().Play("LoadingBufferUlazAnimation");
			ProveriKorisnika();
		}
		else if (MestoPozivanjaLogina == 2)
		{
			Transform transform3 = GameObject.Find("Loading Buffer HOLDER").transform;
			Transform transform4 = GameObject.Find("GUICamera").transform;
			transform3.position = new Vector3(transform4.position.x, transform4.position.y, transform3.position.z);
			((Component)transform3.GetChild(0)).gameObject.SetActive(true);
			((Component)transform3.GetChild(0)).GetComponent<Animator>().Play("LoadingBufferUlazAnimation");
			ProveriKorisnika();
		}
		else if (MestoPozivanjaLogina == 3)
		{
			Transform transform5 = GameObject.Find("Loading Buffer HOLDER").transform;
			Transform transform6 = GameObject.Find("GUICamera").transform;
			transform5.position = new Vector3(transform6.position.x, transform6.position.y, transform5.position.z);
			((Component)transform5.GetChild(0)).gameObject.SetActive(true);
			((Component)transform5.GetChild(0)).GetComponent<Animator>().Play("LoadingBufferUlazAnimation");
			ProveriKorisnika();
		}
	}

	public void RefreshujScenu1PosleLogina()
	{
		MainScene.FacebookLogIn.SetActive(false);
		MainScene.LeaderBoardInvite.SetActive(true);
	}

	public void RefreshujScenu2PosleLogina()
	{
		GameObject.Find("FB Login").SetActive(false);
		AllMapsManageFull.makniPopup = 0;
	}

	public void RefreshujScenu3PosleLogina()
	{
		GameObject.Find("FB Login").SetActive(false);
		KameraMovement.makniPopup = 0;
	}

	private void OpenPage()
	{
		if (stranica == "BananaIsland")
		{
			IDstranice = "636650059721490";
			nagrada = 1000;
			otisaoDaLajkuje = true;
			PlayerPrefs.SetInt("otisaoDaLajkuje", nagrada);
			PlayerPrefs.SetString("IDstranice", IDstranice);
			PlayerPrefs.SetString("stranica", stranica);
			PlayerPrefs.Save();
		}
		else if (stranica == "Webelinx")
		{
			IDstranice = "437444296353647";
			nagrada = 1000;
			otisaoDaLajkuje = true;
			PlayerPrefs.SetInt("otisaoDaLajkuje", nagrada);
			PlayerPrefs.SetString("IDstranice", IDstranice);
			PlayerPrefs.SetString("stranica", stranica);
			PlayerPrefs.Save();
		}
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		if (!pauseStatus)
		{
			if (otisaoDaLajkuje)
			{
				otisaoDaLajkuje = false;
				StagesParser.Instance.UgasiLoading();
				if (stranica == "BananaIsland")
				{
					((Component)((Component)ShopManagerFull.ShopObject).transform.Find("1 Free Coins/Free Coins Tabovi/ShopFCBILikePage")).GetComponent<Collider>().enabled = false;
					((Component)((Component)ShopManagerFull.ShopObject).transform.Find("1 Free Coins/Free Coins Tabovi/ShopFCBILikePage")).GetComponent<Renderer>().material.color = new Color(0.58f, 0.58f, 0.58f);
					((Component)((Component)ShopManagerFull.ShopObject).transform.Find("1 Free Coins/Free Coins Tabovi/ShopFCBILikePage/Like BananaIsland FC")).GetComponent<Renderer>().material.color = new Color(0.58f, 0.58f, 0.58f);
					StagesParser.currentMoney += StagesParser.likePageReward;
					PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
					PlayerPrefs.Save();
					((MonoBehaviour)this).StartCoroutine(StagesParser.Instance.moneyCounter(StagesParser.likePageReward, ((Component)((Component)ShopManagerFull.ShopObject).transform.Find("Shop Interface/Coins/Coins Number")).GetComponent<TextMesh>(), hasOutline: true));
				}
				if (stranica == "Webelinx")
				{
					((Component)((Component)ShopManagerFull.ShopObject).transform.Find("1 Free Coins/Free Coins Tabovi/ShopFCWLLikePage")).GetComponent<Collider>().enabled = false;
					((Component)((Component)ShopManagerFull.ShopObject).transform.Find("1 Free Coins/Free Coins Tabovi/ShopFCWLLikePage")).GetComponent<Renderer>().material.color = new Color(0.58f, 0.58f, 0.58f);
					((Component)((Component)ShopManagerFull.ShopObject).transform.Find("1 Free Coins/Free Coins Tabovi/ShopFCWLLikePage/Like Webelinx FC")).GetComponent<Renderer>().material.color = new Color(0.58f, 0.58f, 0.58f);
					StagesParser.currentMoney += StagesParser.likePageReward;
					PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
					PlayerPrefs.Save();
					((MonoBehaviour)this).StartCoroutine(StagesParser.Instance.moneyCounter(StagesParser.likePageReward, ((Component)((Component)ShopManagerFull.ShopObject).transform.Find("Shop Interface/Coins/Coins Number")).GetComponent<TextMesh>(), hasOutline: true));
				}
				StagesParser.ServerUpdate = 1;
			}
			if (!PlayerPrefs.HasKey("Logovan") || PlayerPrefs.GetInt("Logovan") != 0 || !FB.IsLoggedIn)
			{
				return;
			}
			FB.Logout();
			Ulogovan = false;
			zavrsioUcitavanje = false;
			BrojPrijatelja = 0;
			if (MestoPozivanjaLogina == 1)
			{
				Transform val = MainScene.LeaderBoardInvite.transform.parent.Find("Friends Tabs");
				Transform[] array = (Transform[])(object)new Transform[val.childCount];
				for (int i = 0; i < val.childCount; i++)
				{
					((Renderer)((Component)val.GetChild(i).Find("Friend/LeaderboardYou")).GetComponent<SpriteRenderer>()).enabled = false;
					array[i] = val.GetChild(i);
				}
				Transform val2 = MainScene.LeaderBoardInvite.transform.parent.Find("Friends Tabs/Friend No 2");
				val2.localPosition = new Vector3(val2.localPosition.x, -1.85f, val2.localPosition.z);
				MainScene.FacebookLogIn.SetActive(true);
				MainScene.LeaderBoardInvite.SetActive(false);
				for (int j = 0; j < 10; j++)
				{
					if (j == 1)
					{
						((Component)array[j].Find("FB Invite")).gameObject.SetActive(true);
						((Component)array[j].Find("Friend")).gameObject.SetActive(false);
						((Component)array[j].Find("FB Invite/Coin Shop")).gameObject.SetActive(false);
						((Component)array[j].Find("FB Invite/Invite")).GetComponent<TextMesh>().text = LanguageManager.LogIn;
						((Component)array[j].Find("FB Invite/Invite")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
					}
					else
					{
						((Component)array[j]).gameObject.SetActive(false);
					}
				}
			}
			FacebookObject.BrojPrijatelja = 0;
			FacebookObject.Korisnici.Clear();
			FacebookObject.Scorovi.Clear();
			FacebookObject.Imena.Clear();
			ProfileSlikePrijatelja.Clear();
			ListaStructPrijatelja.Clear();
		}
		else
		{
			leftApp = true;
			if (StagesParser.ServerUpdate == 1 && FB.IsLoggedIn)
			{
				FacebookObject.scoreToSet = StagesParser.currentPoints;
				FacebookObject.proveraPublish_ActionPermisije();
				FacebookObject.SacuvajScoreNaNivoima(StagesParser.PointsPoNivoima, StagesParser.StarsPoNivoima, StagesParser.maxLevel, StagesParser.bonusLevels);
				FacebookObject.UpdateujPodatkeKorisnika(StagesParser.currentMoney, StagesParser.currentPoints, LanguageManager.chosenLanguage, StagesParser.currentBananas, StagesParser.powerup_magnets, StagesParser.powerup_shields, StagesParser.powerup_doublecoins, StagesParser.svekupovineGlava, StagesParser.svekupovineMajica, StagesParser.svekupovineLedja, StagesParser.ledja, StagesParser.glava, StagesParser.majica, StagesParser.imaUsi, StagesParser.imaKosu, NumberOfFriends);
			}
		}
	}

	public void GetProfilePicture()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		FB.API("/" + User + "/picture?redirect=true&height=64&type=normal&width=64", HttpMethod.GET, new FacebookDelegate(MyPictureCallback));
	}

	public void MyPictureCallback(FBResult result)
	{
		if (result.Error != null)
		{
			StagesParser.Instance.UgasiLoading();
			return;
		}
		ProfilePicture = (Texture)(object)result.Texture;
		GameObject.Find("FaceButton").GetComponent<Renderer>().material.mainTexture = ProfilePicture;
	}

	public void GetFacebookName()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		FB.API("me?fields=id,name", HttpMethod.GET, new FacebookDelegate(GetFacebookNameCallback));
	}

	public void GetFacebookNameCallback(FBResult result)
	{
		if (result.Error != null)
		{
			StagesParser.Instance.UgasiLoading();
		}
		else
		{
			UserName = (Json.Deserialize(result.Text) as Dictionary<string, object>)["name"].ToString();
		}
	}

	public void FaceInvite()
	{
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Expected O, but got Unknown
		if (FB.IsLoggedIn)
		{
			int? maxRecipients = null;
			if (FriendSelectorMax != "")
			{
				try
				{
					maxRecipients = int.Parse(FriendSelectorMax);
				}
				catch
				{
				}
			}
			string[] excludeIds = ((FriendSelectorExcludeIds == "") ? null : FriendSelectorExcludeIds.Split(new char[1] { ',' }));
			FB.AppRequest(FriendSelectorMessage, null, FriendSelectorFilters, excludeIds, maxRecipients, FriendSelectorData, FriendSelectorTitle, new FacebookDelegate(InviteCallback));
		}
		else
		{
			FacebookObject.FacebookLogin();
		}
	}

	private void InviteCallback(FBResult result)
	{
		List<object> list = (List<object>)(Json.Deserialize(result.Text) as Dictionary<string, object>)["to"];
		StagesParser.currentMoney += list.Count * StagesParser.InviteReward;
		PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
		PlayerPrefs.Save();
		if (Application.loadedLevelName.Contains("Mapa"))
		{
			GameObject.Find("INTERFACE HOLDER/_TopLeft/Coins/Coins Number").GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
			GameObject.Find("INTERFACE HOLDER/_TopLeft/Coins/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		}
	}

	public void CallFBFeed(string ImeOstrva, int BrojNivoa)
	{
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Expected O, but got Unknown
		Dictionary<string, string[]> properties = null;
		if (IncludeFeedProperties)
		{
			properties = FeedProperties;
		}
		string link = "https://www.facebook.com/pages/Banana-Island/636650059721490";
		FB.Feed("", link, LanguageManager.LevelCompleted, FeedLinkKratakOpis, ImeOstrva + " - " + LanguageManager.Level + " " + BrojNivoa, LinkSlike, LinkVideailiZvuka, FeedActionName, FeedActionLink, FeedReference, properties, new FacebookDelegate(FeedCallback));
	}

	private void FeedCallback(FBResult result)
	{
	}

	public void ProveriPermisije()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		FB.API("/me/permissions", HttpMethod.GET, new FacebookDelegate(MyPermissionsCallback));
	}

	public void MyPermissionsCallback(FBResult result)
	{
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Expected O, but got Unknown
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Expected O, but got Unknown
		if (result.Error != null)
		{
			StagesParser.Instance.UgasiLoading();
			return;
		}
		Dictionary<string, object> obj = Json.Deserialize(result.Text) as Dictionary<string, object>;
		_ = obj["data"];
		object obj2 = obj["data"];
		List<object> list = new List<object>();
		list = (List<object>)obj2;
		int count = list.Count;
		permisija = new string[count];
		statusPermisije = new string[count];
		for (int i = 0; i < count; i++)
		{
			Dictionary<string, object> dictionary = list[i] as Dictionary<string, object>;
			permisija[i] = (string)dictionary["permission"];
			statusPermisije[i] = (string)dictionary["status"];
		}
		bool flag = false;
		for (int j = 0; j < count; j++)
		{
			if (!(permisija[j] == "publish_actions"))
			{
				continue;
			}
			flag = true;
			if (statusPermisije[j] == "granted")
			{
				string imeOstrva = "";
				switch (StagesParser.currSetIndex)
				{
				case 0:
					imeOstrva = LanguageManager.BananaIsland;
					break;
				case 1:
					imeOstrva = LanguageManager.SavannaIsland;
					break;
				case 2:
					imeOstrva = LanguageManager.JungleIsland;
					break;
				case 3:
					imeOstrva = LanguageManager.TempleIsland;
					break;
				case 4:
					imeOstrva = LanguageManager.VolcanoIsland;
					break;
				case 5:
					imeOstrva = LanguageManager.FrozenIsland;
					break;
				}
				CallFBFeed(imeOstrva, StagesParser.currStageIndex + 1);
			}
			else
			{
				MestoPozivanjaLogina = 3;
				FB.Login(permissions, new FacebookDelegate(LoginCallback));
			}
		}
		if (!flag)
		{
			MestoPozivanjaLogina = 3;
			FB.Login(permissions, new FacebookDelegate(LoginCallback));
		}
	}

	public void proveraPublish_ActionPermisije()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		FB.API("/me/permissions", HttpMethod.GET, new FacebookDelegate(Publish_ActionsCallback));
	}

	public void Publish_ActionsCallback(FBResult result)
	{
		if (result.Error != null)
		{
			StagesParser.Instance.UgasiLoading();
			return;
		}
		Dictionary<string, object> obj = Json.Deserialize(result.Text) as Dictionary<string, object>;
		_ = obj["data"];
		object obj2 = obj["data"];
		List<object> list = new List<object>();
		list = (List<object>)obj2;
		int count = list.Count;
		permisija = new string[count];
		statusPermisije = new string[count];
		for (int i = 0; i < count; i++)
		{
			Dictionary<string, object> dictionary = list[i] as Dictionary<string, object>;
			permisija[i] = (string)dictionary["permission"];
			statusPermisije[i] = (string)dictionary["status"];
		}
		odobrioPublishActions = false;
		for (int j = 0; j < count; j++)
		{
			if (permisija[j] == "publish_actions" && statusPermisije[j] == "granted")
			{
				odobrioPublishActions = true;
				SetFacebookHighScore(scoreToSet);
			}
		}
	}

	public void GetRodjendan()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		FB.API("/me?fields=birthday", HttpMethod.GET, new FacebookDelegate(MyBirthdayCallback));
	}

	public void MyBirthdayCallback(FBResult result)
	{
		if (result.Error != null)
		{
			StagesParser.Instance.UgasiLoading();
			return;
		}
		Dictionary<string, object> dictionary = Json.Deserialize(result.Text) as Dictionary<string, object>;
		UserRodjendan = dictionary["birthday"].ToString();
	}

	public void SetFacebookHighScore(int trenutniScore)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Expected O, but got Unknown
		if (FB.IsLoggedIn)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["score"] = trenutniScore.ToString();
			FB.API("/me/scores?", HttpMethod.POST, new FacebookDelegate(SetFacebookScoreCallback), dictionary);
		}
	}

	public void SetFacebookScoreCallback(FBResult result)
	{
		if (result.Error == null && resetovanScoreNaNulu == 2)
		{
			resetovanScoreNaNulu = 1;
		}
	}

	public void GetFacebookHighScore()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		FB.API("me?fields=scores", HttpMethod.GET, new FacebookDelegate(GetFacebookHighScoreCallback));
	}

	public void GetFacebookHighScoreCallback(FBResult result)
	{
		if (result.Error == null)
		{
			Dictionary<string, object> obj = Json.Deserialize(result.Text) as Dictionary<string, object>;
			_ = obj["scores"];
			new List<object>();
			if (obj.TryGetValue("scores", out var value))
			{
				_ = ((Dictionary<string, object>)((List<object>)((Dictionary<string, object>)value)["data"])[0])["score"];
			}
		}
	}

	public void GetFacebookFriendScores()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		FB.API("1609658469261083/scores", HttpMethod.GET, new FacebookDelegate(GetFacebookFriendScoresCallback));
	}

	public void GetFacebookFriendScoresCallback(FBResult result)
	{
		if (result.Error != null)
		{
			StagesParser.Instance.UgasiLoading();
			return;
		}
		Dictionary<string, object> obj = Json.Deserialize(result.Text) as Dictionary<string, object>;
		_ = obj["data"];
		object obj2 = obj["data"];
		List<object> list = new List<object>();
		list = (List<object>)obj2;
		_ = list.Count;
		Korisnici = new List<string>();
		Scorovi = new List<string>();
		Imena = new List<string>();
		NumberOfFriends = list.Count;
		for (int i = 0; i < list.Count; i++)
		{
			Dictionary<string, object> dictionary = list[i] as Dictionary<string, object>;
			if (dictionary.TryGetValue("user", out var value))
			{
				string item = (string)((Dictionary<string, object>)value)["name"];
				string item2 = (string)((Dictionary<string, object>)value)["id"];
				Korisnici.Add(item2);
				Scorovi.Add(dictionary["score"].ToString());
				Imena.Add(item);
			}
		}
		if (MestoPozivanjaLogina == 1)
		{
			for (int j = 0; j < 10; j++)
			{
				((Component)((Component)Camera.main).GetComponent<MainScene>().FriendsObjects[j]).gameObject.SetActive(true);
				((Component)((Component)Camera.main).GetComponent<MainScene>().FriendsObjects[j].Find("FB Invite")).gameObject.SetActive(false);
				((Component)((Component)Camera.main).GetComponent<MainScene>().FriendsObjects[j].Find("Friend")).gameObject.SetActive(true);
			}
			for (int k = 0; k < Scorovi.Count; k++)
			{
				int num = 1 + k;
				if (k < 10)
				{
					((Component)((Component)Camera.main).GetComponent<MainScene>().FriendsObjects[k]).gameObject.SetActive(true);
					GameObject.Find("Pts Number " + num).GetComponent<TextMesh>().text = Scorovi[k];
					GameObject.Find("Pts Number " + num).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
					GameObject.Find("Name " + num).GetComponent<TextMesh>().text = Imena[k];
					GameObject.Find("Name " + num).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
					if (Korisnici[k] == FB.UserId)
					{
						((Renderer)((Component)((Component)Camera.main).GetComponent<MainScene>().FriendsObjects[k].Find("Friend/LeaderboardYou")).GetComponent<SpriteRenderer>()).enabled = true;
					}
				}
			}
			for (int l = Imena.Count; l < 10; l++)
			{
				((Component)((Component)Camera.main).GetComponent<MainScene>().FriendsObjects[l].Find("FB Invite")).gameObject.SetActive(true);
				((Component)((Component)Camera.main).GetComponent<MainScene>().FriendsObjects[l].Find("FB Invite/Coin Number")).GetComponent<TextMesh>().text = "+" + StagesParser.InviteReward;
				((Component)((Component)Camera.main).GetComponent<MainScene>().FriendsObjects[l].Find("FB Invite/Coin Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: false);
				((Component)((Component)Camera.main).GetComponent<MainScene>().FriendsObjects[l].Find("Friend")).gameObject.SetActive(false);
			}
			ObjLeaderboard.Leaderboard = true;
			SwipeControlLeaderboard.controlEnabled = true;
		}
		((MonoBehaviour)this).StartCoroutine("GetFriendPictures");
		((MonoBehaviour)this).StartCoroutine("TrenutniNivoSvihPrijatelja");
	}

	private IEnumerator GetFriendPictures()
	{
		int i = 0;
		if (Ulogovan)
		{
			for (; i < Korisnici.Count; i++)
			{
				WaitForFacebook = false;
				GetFacebookFriendPicture(Korisnici[i]);
				while (!WaitForFacebook && Ulogovan)
				{
					yield return null;
				}
			}
			BrojPrijatelja = 0;
			yield return null;
			if (zavrsioUcitavanje)
			{
				StagesParser.Instance.UgasiLoading();
				PlayerPrefs.SetInt("Logovan", 1);
				if (MestoPozivanjaLogina == 1)
				{
					RefreshujScenu1PosleLogina();
				}
				else if (MestoPozivanjaLogina == 2)
				{
					RefreshujScenu2PosleLogina();
				}
				else if (MestoPozivanjaLogina == 3)
				{
					RefreshujScenu3PosleLogina();
				}
				zavrsioUcitavanje = false;
			}
			else
			{
				zavrsioUcitavanje = true;
			}
		}
		yield return null;
	}

	public void SpisakSvihFacebookPrijatelja()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		FB.API("/fql?q=SELECT+uid,name,pic_square+FROM+user+WHERE+is_app_user=1+AND+uid+IN+(SELECT+uid2+FROM+friend+WHERE+uid1=me())", HttpMethod.GET, new FacebookDelegate(SviPrijateljiFacebookCallback));
	}

	public void SviPrijateljiFacebookCallback(FBResult result)
	{
		if (result.Error != null)
		{
			StagesParser.Instance.UgasiLoading();
			return;
		}
		object obj = (Json.Deserialize(result.Text) as Dictionary<string, object>)["data"];
		List<object> list = new List<object>();
		list = (List<object>)obj;
		_ = list.Count;
		Prijatelji = new List<string>();
		for (int i = 0; i < list.Count; i++)
		{
			Dictionary<string, object> dictionary = list[i] as Dictionary<string, object>;
			if (dictionary.TryGetValue("name", out var _))
			{
				nesto++;
				string item = dictionary["uid"].ToString();
				Prijatelji.Add(item);
			}
		}
	}

	public void GetFacebookFriendPicture(string PrijateljevID)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		if (Ulogovan)
		{
			prijateljevIDzaSliku = PrijateljevID;
			FB.API(PrijateljevID + "/picture?redirect=true&height=64&type=normal&width=64", HttpMethod.GET, new FacebookDelegate(FacebookFriendPictureCallback));
		}
	}

	public void FacebookFriendPictureCallback(FBResult result)
	{
		if (result.Error != null)
		{
			StagesParser.Instance.UgasiLoading();
			return;
		}
		BrojPrijatelja++;
		if (BrojPrijatelja >= Korisnici.Count)
		{
			WaitForFacebook = true;
		}
		IDiSlika item = default(IDiSlika);
		item.PrijateljID = prijateljevIDzaSliku;
		item.profilePicture = (Texture)(object)result.Texture;
		ProfileSlikePrijatelja.Add(item);
		if (MestoPozivanjaLogina == 1)
		{
			switch (BrojPrijatelja)
			{
			case 1:
				GameObject.Find("Friends Level Win Picture 1").GetComponent<Renderer>().material.mainTexture = ProfileSlikePrijatelja[0].profilePicture;
				WaitForFacebook = true;
				break;
			case 2:
				GameObject.Find("Friends Level Win Picture 2").GetComponent<Renderer>().material.mainTexture = ProfileSlikePrijatelja[1].profilePicture;
				WaitForFacebook = true;
				break;
			case 3:
				GameObject.Find("Friends Level Win Picture 3").GetComponent<Renderer>().material.mainTexture = ProfileSlikePrijatelja[2].profilePicture;
				WaitForFacebook = true;
				break;
			case 4:
				GameObject.Find("Friends Level Win Picture 4").GetComponent<Renderer>().material.mainTexture = ProfileSlikePrijatelja[3].profilePicture;
				WaitForFacebook = true;
				break;
			case 5:
				GameObject.Find("Friends Level Win Picture 5").GetComponent<Renderer>().material.mainTexture = ProfileSlikePrijatelja[4].profilePicture;
				WaitForFacebook = true;
				break;
			case 6:
				GameObject.Find("Friends Level Win Picture 6").GetComponent<Renderer>().material.mainTexture = ProfileSlikePrijatelja[5].profilePicture;
				WaitForFacebook = true;
				break;
			case 7:
				GameObject.Find("Friends Level Win Picture 7").GetComponent<Renderer>().material.mainTexture = ProfileSlikePrijatelja[6].profilePicture;
				WaitForFacebook = true;
				break;
			case 8:
				GameObject.Find("Friends Level Win Picture 8").GetComponent<Renderer>().material.mainTexture = ProfileSlikePrijatelja[7].profilePicture;
				WaitForFacebook = true;
				break;
			case 9:
				GameObject.Find("Friends Level Win Picture 9").GetComponent<Renderer>().material.mainTexture = ProfileSlikePrijatelja[8].profilePicture;
				WaitForFacebook = true;
				break;
			case 10:
				GameObject.Find("Friends Level Win Picture 10").GetComponent<Renderer>().material.mainTexture = ProfileSlikePrijatelja[9].profilePicture;
				WaitForFacebook = true;
				break;
			}
			if (BrojPrijatelja > 10)
			{
				WaitForFacebook = true;
			}
		}
		else
		{
			WaitForFacebook = true;
		}
	}

	public void GetFacebookGameAchievements()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		FB.API(FB.AppId + "/achievements", HttpMethod.GET, new FacebookDelegate(GetFacebookGameAchievementsCallback));
	}

	public void GetFacebookGameAchievementsCallback(FBResult result)
	{
		if (result.Error == null)
		{
			object obj = (Json.Deserialize(result.Text) as Dictionary<string, object>)["data"];
			List<object> list = new List<object>();
			list = (List<object>)obj;
			_ = list.Count;
			for (int i = 0; i < list.Count; i++)
			{
				(list[i] as Dictionary<string, object>).TryGetValue("title", out var _);
			}
		}
	}

	public void DodajFacebookAchievement(string URLAchivmenta)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary["achievement"] = URLAchivmenta;
		FB.API("me/achievements", HttpMethod.POST, new FacebookDelegate(DodajFacebookAchievementCallback), dictionary);
	}

	public void DodajFacebookAchievementCallback(FBResult result)
	{
		_ = result.Error;
	}

	public void ObrisiFacebookAchievement(string UrlACH)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		FB.API("me/achievements?achievement=" + UrlACH, HttpMethod.DELETE, new FacebookDelegate(ObrisiFacebookAchievementCallback));
	}

	public void ObrisiFacebookAchievementCallback(FBResult result)
	{
		_ = result.Error;
	}

	public void ProveraFacebookAchievmenta()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		FB.API("me/achievements", HttpMethod.GET, new FacebookDelegate(ProveraAchievmentaCallback));
	}

	public void ProveraAchievmentaCallback(FBResult result)
	{
		_ = result.Error;
	}

	public void InicijalizujKorisnika(string KorisnikovID, int numCoins, int Score, string Jezik, int Banana, int Shield, int Magnet, int DoubleCoins, string UserSveKupovineHats, string UserSveKupovineShirts, string UserSveKupovineBackPacks)
	{
		if (FB.IsLoggedIn)
		{
			Korisnik["UserID"] = KorisnikovID;
			Korisnik["Name"] = UserName;
			Korisnik["Coins"] = numCoins;
			Korisnik["Score"] = Score;
			Korisnik["Language"] = Jezik;
			Korisnik["Banana"] = Banana;
			Korisnik["PowerShield"] = Shield;
			Korisnik["PowerMagnet"] = Magnet;
			Korisnik["PowerDoubleCoins"] = DoubleCoins;
			Korisnik["UserSveKupovineHats"] = UserSveKupovineHats;
			Korisnik["UserSveKupovineShirts"] = UserSveKupovineShirts;
			Korisnik["UserSveKupovineBackPacks"] = UserSveKupovineBackPacks;
			Korisnik["GlavaItems"] = 0;
			Korisnik["TeloItems"] = 0;
			Korisnik["LedjaItems"] = 0;
			Korisnik["Usi"] = true;
			Korisnik["Kosa"] = true;
			Korisnik["NumberOfFriends"] = NumberOfFriends;
			Korisnik["OdgledaoShopTutorial"] = StagesParser.otvaraoShopNekad;
			Korisnik["JezikPromenjen"] = StagesParser.jezikPromenjen;
		}
	}

	public void ProcitajPodatkeKorisnika()
	{
		StagesParser.languageBefore = LanguageManager.chosenLanguage;
		if (FB.IsLoggedIn)
		{
			ParseObject.GetQuery("User").WhereEqualTo("UserID", (object)User).FirstAsync()
				.ContinueWith((Action<Task<ParseObject>>)delegate(Task<ParseObject> t)
				{
					if (((Task)t).IsCompleted)
					{
						ParseObject result = t.Result;
						UserCoins = result.Get<int>("Coins");
						UserScore = result.Get<int>("Score");
						UserLanguage = result.Get<string>("Language");
						UserBanana = result.Get<int>("Banana");
						UserPowerMagnet = result.Get<int>("PowerMagnet");
						UserPowerShield = result.Get<int>("PowerShield");
						UserPowerDoubleCoins = result.Get<int>("PowerDoubleCoins");
						UserSveKupovineHats = result.Get<string>("UserSveKupovineHats");
						UserSveKupovineShirts = result.Get<string>("UserSveKupovineShirts");
						UserSveKupovineBackPacks = result.Get<string>("UserSveKupovineBackPacks");
						StagesParser.otvaraoShopNekad = result.Get<int>("OdgledaoShopTutorial");
						StagesParser.jezikPromenjen = result.Get<int>("JezikPromenjen");
						GlavaItem = result.Get<int>("GlavaItems");
						TeloItem = result.Get<int>("TeloItems");
						LedjaItem = result.Get<int>("LedjaItems");
						Usi = result.Get<bool>("Usi");
						Kosa = result.Get<bool>("Kosa");
						KorisnikoviPodaciSpremni = true;
					}
					else if (((Task)t).IsFaulted || ((Task)t).IsCanceled)
					{
						UserCoins = StagesParser.currentMoney;
						UserScore = StagesParser.currentPoints;
						UserLanguage = LanguageManager.chosenLanguage;
						UserBanana = StagesParser.currentBananas;
						UserPowerMagnet = StagesParser.powerup_magnets;
						UserPowerShield = StagesParser.powerup_shields;
						UserPowerDoubleCoins = StagesParser.powerup_doublecoins;
						UserSveKupovineHats = StagesParser.svekupovineGlava;
						UserSveKupovineShirts = StagesParser.svekupovineMajica;
						UserSveKupovineBackPacks = StagesParser.svekupovineLedja;
						GlavaItem = StagesParser.glava;
						TeloItem = StagesParser.majica;
						LedjaItem = StagesParser.ledja;
						Usi = StagesParser.imaUsi;
						Kosa = StagesParser.imaKosu;
						KorisnikoviPodaciSpremni = true;
					}
				});
		}
		else
		{
			UserCoins = StagesParser.currentMoney;
			UserScore = StagesParser.currentPoints;
			UserLanguage = LanguageManager.chosenLanguage;
			UserBanana = StagesParser.currentBananas;
			UserPowerMagnet = StagesParser.powerup_magnets;
			UserPowerShield = StagesParser.powerup_shields;
			UserPowerDoubleCoins = StagesParser.powerup_doublecoins;
			UserSveKupovineHats = StagesParser.svekupovineGlava;
			UserSveKupovineShirts = StagesParser.svekupovineMajica;
			UserSveKupovineBackPacks = StagesParser.svekupovineLedja;
			GlavaItem = StagesParser.glava;
			TeloItem = StagesParser.majica;
			LedjaItem = StagesParser.ledja;
			Usi = StagesParser.imaUsi;
			Kosa = StagesParser.imaKosu;
			KorisnikoviPodaciSpremni = true;
		}
	}

	public void ProveriKorisnika()
	{
		ListaStructPrijatelja.Clear();
		((MonoBehaviour)this).StartCoroutine("DaLiPostojiKorisnik");
	}

	private IEnumerator DaLiPostojiKorisnik()
	{
		if (FB.IsLoggedIn)
		{
			ParseQuery<ParseObject> val = ParseObject.GetQuery("User").WhereEqualTo("UserID", (object)User).Limit(1);
			Task<IEnumerable<ParseObject>> queryTask = val.FindAsync();
			while (!((Task)queryTask).IsCompleted)
			{
				yield return null;
			}
			if (((ReadOnlyCollection<ParseObject>)queryTask.Result).Count > 0)
			{
				ProcitajPodatkeKorisnika();
				GetFacebookFriendScores();
				nePostojiKorisnik = false;
				yield break;
			}
			nePostojiKorisnik = true;
			StagesParser.currentMoney += StagesParser.LoginReward;
			PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
			PlayerPrefs.Save();
			if (MestoPozivanjaLogina == 1)
			{
				GameObject val2 = GameObject.Find("CoinsReward");
				val2.GetComponent<Animation>().Play("CoinsRewardDolazak");
				((MonoBehaviour)this).StartCoroutine(StagesParser.Instance.moneyCounter(2000, ((Component)val2.transform.Find("Coins Number")).GetComponent<TextMesh>(), hasOutline: true));
				((MonoBehaviour)this).Invoke("SkloniCoinsReward", 1.2f);
			}
			else
			{
				((MonoBehaviour)this).StartCoroutine(StagesParser.Instance.moneyCounter(StagesParser.LoginReward, ((Component)GameObject.Find("_GUI").transform.Find("INTERFACE HOLDER/_TopLeft/Coins/Coins Number")).GetComponent<TextMesh>(), hasOutline: true));
			}
			StagesParser.ServerUpdate = 1;
			scoreToSet = StagesParser.currentPoints;
			proveraPublish_ActionPermisije();
			InicijalizujKorisnika(User, StagesParser.currentMoney, StagesParser.currentPoints, LanguageManager.chosenLanguage, StagesParser.currentBananas, StagesParser.powerup_shields, StagesParser.powerup_magnets, StagesParser.powerup_doublecoins, StagesParser.svekupovineGlava, StagesParser.svekupovineMajica, StagesParser.svekupovineLedja);
			InicijalizujScoreNaNivoima(StagesParser.StarsPoNivoima, StagesParser.PointsPoNivoima, StagesParser.maxLevel, StagesParser.bonusLevels);
			GetFacebookFriendScores();
		}
		else
		{
			ProcitajPodatkeKorisnika();
		}
	}

	public void UpdateujPodatkeKorisnika(int BrojCoina, int Score, string Jezik, int Banana, int PowerMagnet, int PowerShield, int PowerDoubleCoins, string UserSveKupovineHats, string UserSveKupovineShirts, string UserSveKupovineBackPacks, int Ledja, int Glava, int Telo, bool Usi, bool Kosa, int NumberOfFriends)
	{
		if (!FB.IsLoggedIn)
		{
			return;
		}
		ParseObject.GetQuery("User").WhereEqualTo("UserID", (object)User).FirstAsync()
			.ContinueWith((Action<Task<ParseObject>>)delegate(Task<ParseObject> t)
			{
				if (((Task)t).IsCompleted)
				{
					ParseObject result = t.Result;
					result["Coins"] = BrojCoina;
					result["Score"] = Score;
					result["Language"] = Jezik;
					result["Banana"] = Banana;
					result["PowerMagnet"] = PowerMagnet;
					result["PowerShield"] = PowerShield;
					result["PowerDoubleCoins"] = PowerDoubleCoins;
					result["UserSveKupovineHats"] = UserSveKupovineHats;
					result["UserSveKupovineShirts"] = UserSveKupovineShirts;
					result["UserSveKupovineBackPacks"] = UserSveKupovineBackPacks;
					result["GlavaItems"] = Glava;
					result["TeloItems"] = Telo;
					result["LedjaItems"] = Ledja;
					result["Usi"] = Usi;
					result["Kosa"] = Kosa;
					result["NumberOfFriends"] = NumberOfFriends;
					result["OdgledaoShopTutorial"] = StagesParser.otvaraoShopNekad;
					result["JezikPromenjen"] = StagesParser.jezikPromenjen;
					result.SaveAsync();
					updatedSuccessfullyPodaciKorisnika = true;
					if (StagesParser.ServerUpdate == 1 && updatedSuccessfullyScoreNaNivoima)
					{
						StagesParser.ServerUpdate = 2;
						PlayerPrefs.SetInt("ServerUpdate", StagesParser.ServerUpdate);
						PlayerPrefs.Save();
					}
				}
				else if (!((Task)t).IsFaulted)
				{
					_ = ((Task)t).IsCanceled;
				}
			});
	}

	private void SkloniCoinsReward()
	{
		GameObject.Find("CoinsReward").GetComponent<Animation>().Play("CoinsRewardOdlazak");
	}

	public void SacuvajBrojNovcica(int numCoins)
	{
		if (!FB.IsLoggedIn)
		{
			return;
		}
		ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
		query.WhereEqualTo("UserID", (object)User);
		query.FirstAsync().ContinueWith((Action<Task<ParseObject>>)delegate(Task<ParseObject> t)
		{
			if (((Task)t).IsCompleted)
			{
				ParseObject result = t.Result;
				result["Coins"] = numCoins;
				result.SaveAsync();
			}
			else if (!((Task)t).IsFaulted)
			{
				_ = ((Task)t).IsCanceled;
			}
		});
	}

	public void ProcitajBrojNovcica()
	{
		if (!FB.IsLoggedIn)
		{
			return;
		}
		ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
		query.WhereEqualTo("UserID", (object)User);
		query.FirstAsync().ContinueWith((Action<Task<ParseObject>>)delegate(Task<ParseObject> t)
		{
			if (((Task)t).IsCompleted)
			{
				t.Result.Get<int>("Coins");
			}
			else if (!((Task)t).IsFaulted)
			{
				_ = ((Task)t).IsCanceled;
			}
		});
	}

	public void SacuvajBrojBanana(int BrojBanana)
	{
		if (!FB.IsLoggedIn)
		{
			return;
		}
		ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
		query.WhereEqualTo("UserID", (object)User);
		query.FirstAsync().ContinueWith((Action<Task<ParseObject>>)delegate(Task<ParseObject> t)
		{
			if (((Task)t).IsCompleted)
			{
				ParseObject result = t.Result;
				result["Banana"] = BrojBanana;
				result.SaveAsync();
			}
			else if (!((Task)t).IsFaulted)
			{
				_ = ((Task)t).IsCanceled;
			}
		});
	}

	public void ProcitajBrojBanana()
	{
		if (!FB.IsLoggedIn)
		{
			return;
		}
		ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
		query.WhereEqualTo("UserID", (object)User);
		query.FirstAsync().ContinueWith((Action<Task<ParseObject>>)delegate(Task<ParseObject> t)
		{
			if (((Task)t).IsCompleted)
			{
				t.Result.Get<int>("Banana");
			}
			else if (!((Task)t).IsFaulted)
			{
				_ = ((Task)t).IsCanceled;
			}
		});
	}

	public void SacuvajScore(int GlobalScore)
	{
		if (!FB.IsLoggedIn)
		{
			return;
		}
		ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
		query.WhereEqualTo("UserID", (object)User);
		query.FirstAsync().ContinueWith((Action<Task<ParseObject>>)delegate(Task<ParseObject> t)
		{
			if (((Task)t).IsCompleted)
			{
				ParseObject result = t.Result;
				result["Score"] = GlobalScore;
				result.SaveAsync();
			}
			else if (!((Task)t).IsFaulted)
			{
				_ = ((Task)t).IsCanceled;
			}
		});
	}

	public void ProcitajScore()
	{
		if (!FB.IsLoggedIn)
		{
			return;
		}
		ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
		query.WhereEqualTo("UserID", (object)User);
		query.FirstAsync().ContinueWith((Action<Task<ParseObject>>)delegate(Task<ParseObject> t)
		{
			if (((Task)t).IsCompleted)
			{
				t.Result.Get<int>("Score");
			}
			else if (!((Task)t).IsFaulted)
			{
				_ = ((Task)t).IsCanceled;
			}
		});
	}

	public void SacuvajLanguage(string NoviJezik)
	{
		if (!FB.IsLoggedIn)
		{
			return;
		}
		ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
		query.WhereEqualTo("UserID", (object)User);
		query.FirstAsync().ContinueWith((Action<Task<ParseObject>>)delegate(Task<ParseObject> t)
		{
			if (((Task)t).IsCompleted)
			{
				ParseObject result = t.Result;
				result["Language"] = NoviJezik;
				result.SaveAsync();
			}
			else if (!((Task)t).IsFaulted)
			{
				_ = ((Task)t).IsCanceled;
			}
		});
	}

	public void ProcitajLanguage()
	{
		if (!FB.IsLoggedIn)
		{
			return;
		}
		ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
		query.WhereEqualTo("UserID", (object)User);
		query.FirstAsync().ContinueWith((Action<Task<ParseObject>>)delegate(Task<ParseObject> t)
		{
			if (((Task)t).IsCompleted)
			{
				t.Result.Get<string>("Language");
			}
			else if (!((Task)t).IsFaulted)
			{
				_ = ((Task)t).IsCanceled;
			}
		});
	}

	public void SacuvajPowerShield(int BrojPowerShield)
	{
		if (!FB.IsLoggedIn)
		{
			return;
		}
		ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
		query.WhereEqualTo("UserID", (object)User);
		query.FirstAsync().ContinueWith((Action<Task<ParseObject>>)delegate(Task<ParseObject> t)
		{
			if (((Task)t).IsCompleted)
			{
				ParseObject result = t.Result;
				result["PowerShield"] = BrojPowerShield;
				result.SaveAsync();
			}
			else if (!((Task)t).IsFaulted)
			{
				_ = ((Task)t).IsCanceled;
			}
		});
	}

	public void ProcitajPowerShield()
	{
		if (!FB.IsLoggedIn)
		{
			return;
		}
		ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
		query.WhereEqualTo("UserID", (object)User);
		query.FirstAsync().ContinueWith((Action<Task<ParseObject>>)delegate(Task<ParseObject> t)
		{
			if (((Task)t).IsCompleted)
			{
				t.Result.Get<int>("PowerShield");
			}
			else if (!((Task)t).IsFaulted)
			{
				_ = ((Task)t).IsCanceled;
			}
		});
	}

	public void SacuvajPowerMagnet(int BrojPowerMagnet)
	{
		if (!FB.IsLoggedIn)
		{
			return;
		}
		ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
		query.WhereEqualTo("UserID", (object)User);
		query.FirstAsync().ContinueWith((Action<Task<ParseObject>>)delegate(Task<ParseObject> t)
		{
			if (((Task)t).IsCompleted)
			{
				ParseObject result = t.Result;
				result["PowerMagnet"] = BrojPowerMagnet;
				result.SaveAsync();
			}
			else if (!((Task)t).IsFaulted)
			{
				_ = ((Task)t).IsCanceled;
			}
		});
	}

	public void ProcitajPowerMagnet()
	{
		if (!FB.IsLoggedIn)
		{
			return;
		}
		ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
		query.WhereEqualTo("UserID", (object)User);
		query.FirstAsync().ContinueWith((Action<Task<ParseObject>>)delegate(Task<ParseObject> t)
		{
			if (((Task)t).IsCompleted)
			{
				t.Result.Get<int>("PowerMagnet");
			}
			else if (!((Task)t).IsFaulted)
			{
				_ = ((Task)t).IsCanceled;
			}
		});
	}

	public void SacuvajPowerDoubleCoins(int BrojPowerDoubleCoins)
	{
		if (!FB.IsLoggedIn)
		{
			return;
		}
		ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
		query.WhereEqualTo("UserID", (object)User);
		query.FirstAsync().ContinueWith((Action<Task<ParseObject>>)delegate(Task<ParseObject> t)
		{
			if (((Task)t).IsCompleted)
			{
				ParseObject result = t.Result;
				result["PowerDoubleCoins"] = BrojPowerDoubleCoins;
				result.SaveAsync();
			}
			else if (!((Task)t).IsFaulted)
			{
				_ = ((Task)t).IsCanceled;
			}
		});
	}

	public void ProcitajPowerDoubleCoins()
	{
		if (!FB.IsLoggedIn)
		{
			return;
		}
		ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
		query.WhereEqualTo("UserID", (object)User);
		query.FirstAsync().ContinueWith((Action<Task<ParseObject>>)delegate(Task<ParseObject> t)
		{
			if (((Task)t).IsCompleted)
			{
				t.Result.Get<int>("PowerDoubleCoins");
			}
			else if (!((Task)t).IsFaulted)
			{
				_ = ((Task)t).IsCanceled;
			}
		});
	}

	public void SacuvajSveMoci(int BrojPowerShield, int BrojPowerMagnet, int BrojPowerDoubleCoins)
	{
		if (!FB.IsLoggedIn)
		{
			return;
		}
		ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
		query.WhereEqualTo("UserID", (object)User);
		query.FirstAsync().ContinueWith((Action<Task<ParseObject>>)delegate(Task<ParseObject> t)
		{
			if (((Task)t).IsCompleted)
			{
				ParseObject result = t.Result;
				result["PowerShield"] = BrojPowerShield;
				result["PowerMagnet"] = BrojPowerMagnet;
				result["PowerDoubleCoins"] = BrojPowerDoubleCoins;
				result.SaveAsync();
			}
			else if (!((Task)t).IsFaulted)
			{
				_ = ((Task)t).IsCanceled;
			}
		});
	}

	public void ProcitajSveMoci()
	{
		if (!FB.IsLoggedIn)
		{
			return;
		}
		ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
		query.WhereEqualTo("UserID", (object)User);
		query.FirstAsync().ContinueWith((Action<Task<ParseObject>>)delegate(Task<ParseObject> t)
		{
			if (((Task)t).IsCompleted)
			{
				ParseObject result = t.Result;
				result.Get<int>("PowerShield");
				result.Get<int>("PowerMagnet");
				result.Get<int>("PowerDoubleCoins");
			}
			else if (!((Task)t).IsFaulted)
			{
				_ = ((Task)t).IsCanceled;
			}
		});
	}

	public void SacuvajKupljeneStvari(string UserSveKupovineHats, string UserSveKupovineShirts, string UserSveKupovineBackPacks)
	{
		if (!FB.IsLoggedIn)
		{
			return;
		}
		ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
		query.WhereEqualTo("UserID", (object)User);
		query.FirstAsync().ContinueWith((Action<Task<ParseObject>>)delegate(Task<ParseObject> t)
		{
			if (((Task)t).IsCompleted)
			{
				ParseObject result = t.Result;
				result["UserSveKupovineHats"] = UserSveKupovineHats;
				result["UserSveKupovineShirts"] = UserSveKupovineShirts;
				result["UserSveKupovineBackPacks"] = UserSveKupovineBackPacks;
				result.SaveAsync();
			}
			else if (!((Task)t).IsFaulted)
			{
				_ = ((Task)t).IsCanceled;
			}
		});
	}

	public void ProcitajKupljeneStvari()
	{
		if (!FB.IsLoggedIn)
		{
			return;
		}
		ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
		query.WhereEqualTo("UserID", (object)User);
		query.FirstAsync().ContinueWith((Action<Task<ParseObject>>)delegate(Task<ParseObject> t)
		{
			if (((Task)t).IsCompleted)
			{
				ParseObject result = t.Result;
				UserSveKupovineHats = result.Get<string>("UserSveKupovineHats");
				UserSveKupovineShirts = result.Get<string>("UserSveKupovineShirts");
				UserSveKupovineHats = result.Get<string>("UserSveKupovineHats");
			}
			else if (!((Task)t).IsFaulted)
			{
				_ = ((Task)t).IsCanceled;
			}
		});
	}

	public void InicijalizujScoreNaNivoima(int[] NizBrojZvezdaPoNivou, int[] NizScorovaPoNivoima, int MaxNivo, string BonusLevels)
	{
		LevelScore["NumOfStars"] = NizBrojZvezdaPoNivou;
		LevelScore["LevelScore"] = NizScorovaPoNivoima;
		LevelScore["MaxLevel"] = MaxNivo;
		LevelScore["BonusLevels"] = BonusLevels;
		LevelScore["UserID"] = User;
	}

	public void SacuvajScoreNaNivoima(int[] ScorePoNivoima, int[] BrojZvezdaPoNivoima, int TrenutniNivoIgraca, string BonusLevels)
	{
		if (!FB.IsLoggedIn)
		{
			return;
		}
		ParseObject.GetQuery("LevelScore").WhereEqualTo("UserID", (object)User).FirstAsync()
			.ContinueWith((Action<Task<ParseObject>>)delegate(Task<ParseObject> t)
			{
				if (((Task)t).IsCompleted)
				{
					ParseObject result = t.Result;
					result["LevelScore"] = ScorePoNivoima;
					result["NumOfStars"] = BrojZvezdaPoNivoima;
					result["MaxLevel"] = TrenutniNivoIgraca;
					result["BonusLevels"] = BonusLevels;
					result.SaveAsync();
					updatedSuccessfullyScoreNaNivoima = true;
					if (StagesParser.ServerUpdate == 3)
					{
						StagesParser.ServerUpdate = 2;
						OKzaLogout = true;
					}
				}
				else if (!((Task)t).IsFaulted)
				{
					_ = ((Task)t).IsCanceled;
				}
			});
		if (StagesParser.ServerUpdate == 1 && updatedSuccessfullyPodaciKorisnika)
		{
			StagesParser.ServerUpdate = 2;
			PlayerPrefs.SetInt("ServerUpdate", StagesParser.ServerUpdate);
			PlayerPrefs.Save();
		}
	}

	public void ProcitajScoreNaNivoima()
	{
		if (!FB.IsLoggedIn)
		{
			return;
		}
		ParseObject.GetQuery("LevelScore").WhereEqualTo("UserID", (object)User).FirstAsync()
			.ContinueWith((Action<Task<ParseObject>>)delegate(Task<ParseObject> t)
			{
				if (((Task)t).IsCompleted)
				{
					ParseObject result = t.Result;
					result.Get<IList<int>>("LevelScore");
					result.Get<IList<int>>("NumOfStars");
					TrenutniNivoIgraca = result.Get<int>("MaxLevel");
				}
				else if (!((Task)t).IsFaulted)
				{
					_ = ((Task)t).IsCanceled;
				}
			});
	}

	public void ProcitajScorovePrijatelja(string FriendID)
	{
		if (!FB.IsLoggedIn)
		{
			return;
		}
		ParseObject.GetQuery("LevelScore").WhereEqualTo("UserID", (object)FriendID).FirstAsync()
			.ContinueWith((Action<Task<ParseObject>>)delegate(Task<ParseObject> t)
			{
				if (((Task)t).IsCompleted)
				{
					ParseObject result = t.Result;
					IList<int> scores = result.Get<IList<int>>("LevelScore");
					IList<int> stars = result.Get<IList<int>>("NumOfStars");
					int maxLevel = result.Get<int>("MaxLevel");
					bonusLevels = result.Get<string>("BonusLevels");
					StrukturaPrijatelja strukturaPrijatelja = default(StrukturaPrijatelja);
					strukturaPrijatelja.PrijateljID = FriendID;
					StrukturaPrijatelja item = strukturaPrijatelja;
					item.MaxLevel = maxLevel;
					item.scores = scores;
					item.stars = stars;
					ListaStructPrijatelja.Add(item);
					WaitForFacebookFriend = true;
				}
				else if (((Task)t).IsFaulted || ((Task)t).IsCanceled)
				{
					StagesParser.Instance.UgasiLoading();
				}
			});
	}

	private IEnumerator TrenutniNivoSvihPrijatelja()
	{
		int i = 0;
		float timer = 0f;
		for (; i < Korisnici.Count; i++)
		{
			WaitForFacebookFriend = false;
			ProcitajScorovePrijatelja(Korisnici[i]);
			while (!WaitForFacebookFriend && Ulogovan)
			{
				if (timer > 20f)
				{
					WaitForFacebookFriend = true;
				}
				timer += Time.deltaTime;
				yield return null;
			}
		}
		if (resetovanScoreNaNulu == 1)
		{
			for (int j = 0; j < ListaStructPrijatelja.Count; j++)
			{
				if (ListaStructPrijatelja[j].PrijateljID == FB.UserId)
				{
					for (int k = 0; k < scorePoNivouPrijatelja.Length; k++)
					{
						ListaStructPrijatelja[j].scores[k] = 0;
					}
				}
			}
			resetovanScoreNaNulu = 0;
			StagesParser.Instance.UgasiLoading();
		}
		for (int l = 0; l < ListaStructPrijatelja.Count; l++)
		{
			if (ListaStructPrijatelja[l].PrijateljID == User)
			{
				indexUListaStructPrijatelja = l;
			}
		}
		if (Ulogovan)
		{
			if (!nePostojiKorisnik)
			{
				StagesParser.Instance.CompareScores();
			}
			else if (zavrsioUcitavanje)
			{
				StagesParser.Instance.UgasiLoading();
				PlayerPrefs.SetInt("Logovan", 1);
				if (MestoPozivanjaLogina == 1)
				{
					RefreshujScenu1PosleLogina();
				}
				else if (MestoPozivanjaLogina == 2)
				{
					RefreshujScenu2PosleLogina();
				}
				else if (MestoPozivanjaLogina == 3)
				{
					RefreshujScenu3PosleLogina();
				}
				zavrsioUcitavanje = false;
			}
			else
			{
				zavrsioUcitavanje = true;
			}
		}
		else
		{
			StagesParser.Instance.UgasiLoading();
		}
	}
}
