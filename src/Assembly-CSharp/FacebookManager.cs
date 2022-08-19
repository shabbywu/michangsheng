using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks2;
using Facebook;
using Facebook.MiniJSON;
using Parse;
using UnityEngine;

// Token: 0x020004AC RID: 1196
public class FacebookManager : MonoBehaviour
{
	// Token: 0x0600259F RID: 9631 RVA: 0x00104549 File Offset: 0x00102749
	private void Awake()
	{
		FacebookManager.FacebookObject = this;
		Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x060025A0 RID: 9632 RVA: 0x0010455C File Offset: 0x0010275C
	private void Start()
	{
		for (int i = 0; i < 100; i++)
		{
			this.testNiz[i] = i + 100;
		}
		FB.Init(new InitDelegate(this.OnInitComplete), new HideUnityDelegate(this.OnHideUnity), null);
		if (PlayerPrefs.HasKey("UserSveKupovineHats"))
		{
			FacebookManager.UserSveKupovineHats = PlayerPrefs.GetString("UserSveKupovineHats");
		}
		else
		{
			FacebookManager.UserSveKupovineHats = "0#0#";
		}
		if (PlayerPrefs.HasKey("UserSveKupovineShirts"))
		{
			FacebookManager.UserSveKupovineShirts = PlayerPrefs.GetString("UserSveKupovineShirts");
		}
		else
		{
			FacebookManager.UserSveKupovineShirts = "0#0#0#0#0#0#0#0#";
		}
		if (PlayerPrefs.HasKey("UserSveKupovineBackPacks"))
		{
			FacebookManager.UserSveKupovineBackPacks = PlayerPrefs.GetString("UserSveKupovineBackPacks");
		}
		else
		{
			FacebookManager.UserSveKupovineBackPacks = "0#0#0#0#0#0#";
		}
		StagesParser.svekupovineGlava = FacebookManager.UserSveKupovineHats;
		StagesParser.svekupovineMajica = FacebookManager.UserSveKupovineShirts;
		StagesParser.svekupovineLedja = FacebookManager.UserSveKupovineBackPacks;
		if (FB.IsLoggedIn)
		{
			FacebookManager.Ulogovan = true;
			return;
		}
		FacebookManager.Ulogovan = false;
	}

	// Token: 0x060025A1 RID: 9633 RVA: 0x00104647 File Offset: 0x00102847
	public static void FacebookLogout()
	{
		if (FB.IsLoggedIn)
		{
			FB.Logout();
			FacebookManager.Ulogovan = false;
			StagesParser.Instance.ObrisiProgresNaLogOut();
		}
	}

	// Token: 0x060025A2 RID: 9634 RVA: 0x00104665 File Offset: 0x00102865
	private void OnInitComplete()
	{
		this.isInit = true;
	}

	// Token: 0x060025A3 RID: 9635 RVA: 0x0010466E File Offset: 0x0010286E
	private void OnHideUnity(bool isGameShown)
	{
		Debug.Log("Is game showing? " + isGameShown.ToString());
	}

	// Token: 0x060025A4 RID: 9636 RVA: 0x00104686 File Offset: 0x00102886
	public void FacebookLogin()
	{
		FB.Login(this.permissions, new FacebookDelegate(this.LoginCallback));
	}

	// Token: 0x060025A5 RID: 9637 RVA: 0x001046A0 File Offset: 0x001028A0
	public void LoginCallback(FBResult result)
	{
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
		FacebookManager.Ulogovan = true;
		FacebookManager.User = FB.UserId;
		PlayerPrefs.SetInt("Logovan", 0);
		PlayerPrefs.Save();
		this.GetFacebookName();
		if (FacebookManager.MestoPozivanjaLogina == 1)
		{
			Transform transform = GameObject.Find("Loading Buffer HOLDER").transform;
			Transform transform2 = Camera.main.transform;
			transform.position = new Vector3(transform2.position.x, transform2.position.y, transform.position.z);
			transform.GetChild(0).gameObject.SetActive(true);
			transform.GetChild(0).GetComponent<Animator>().Play("LoadingBufferUlazAnimation");
			this.ProveriKorisnika();
			return;
		}
		if (FacebookManager.MestoPozivanjaLogina == 2)
		{
			Transform transform3 = GameObject.Find("Loading Buffer HOLDER").transform;
			Transform transform4 = GameObject.Find("GUICamera").transform;
			transform3.position = new Vector3(transform4.position.x, transform4.position.y, transform3.position.z);
			transform3.GetChild(0).gameObject.SetActive(true);
			transform3.GetChild(0).GetComponent<Animator>().Play("LoadingBufferUlazAnimation");
			this.ProveriKorisnika();
			return;
		}
		if (FacebookManager.MestoPozivanjaLogina == 3)
		{
			Transform transform5 = GameObject.Find("Loading Buffer HOLDER").transform;
			Transform transform6 = GameObject.Find("GUICamera").transform;
			transform5.position = new Vector3(transform6.position.x, transform6.position.y, transform5.position.z);
			transform5.GetChild(0).gameObject.SetActive(true);
			transform5.GetChild(0).GetComponent<Animator>().Play("LoadingBufferUlazAnimation");
			this.ProveriKorisnika();
		}
	}

	// Token: 0x060025A6 RID: 9638 RVA: 0x00104886 File Offset: 0x00102A86
	public void RefreshujScenu1PosleLogina()
	{
		MainScene.FacebookLogIn.SetActive(false);
		MainScene.LeaderBoardInvite.SetActive(true);
	}

	// Token: 0x060025A7 RID: 9639 RVA: 0x0010489E File Offset: 0x00102A9E
	public void RefreshujScenu2PosleLogina()
	{
		GameObject.Find("FB Login").SetActive(false);
		AllMapsManageFull.makniPopup = 0;
	}

	// Token: 0x060025A8 RID: 9640 RVA: 0x001048B6 File Offset: 0x00102AB6
	public void RefreshujScenu3PosleLogina()
	{
		GameObject.Find("FB Login").SetActive(false);
		KameraMovement.makniPopup = 0;
	}

	// Token: 0x060025A9 RID: 9641 RVA: 0x001048D0 File Offset: 0x00102AD0
	private void OpenPage()
	{
		if (FacebookManager.stranica == "BananaIsland")
		{
			FacebookManager.IDstranice = "636650059721490";
			FacebookManager.nagrada = 1000;
			FacebookManager.otisaoDaLajkuje = true;
			PlayerPrefs.SetInt("otisaoDaLajkuje", FacebookManager.nagrada);
			PlayerPrefs.SetString("IDstranice", FacebookManager.IDstranice);
			PlayerPrefs.SetString("stranica", FacebookManager.stranica);
			PlayerPrefs.Save();
			return;
		}
		if (FacebookManager.stranica == "Webelinx")
		{
			FacebookManager.IDstranice = "437444296353647";
			FacebookManager.nagrada = 1000;
			FacebookManager.otisaoDaLajkuje = true;
			PlayerPrefs.SetInt("otisaoDaLajkuje", FacebookManager.nagrada);
			PlayerPrefs.SetString("IDstranice", FacebookManager.IDstranice);
			PlayerPrefs.SetString("stranica", FacebookManager.stranica);
			PlayerPrefs.Save();
		}
	}

	// Token: 0x060025AA RID: 9642 RVA: 0x00104998 File Offset: 0x00102B98
	private void OnApplicationPause(bool pauseStatus)
	{
		if (!pauseStatus)
		{
			if (FacebookManager.otisaoDaLajkuje)
			{
				FacebookManager.otisaoDaLajkuje = false;
				StagesParser.Instance.UgasiLoading();
				if (FacebookManager.stranica == "BananaIsland")
				{
					ShopManagerFull.ShopObject.transform.Find("1 Free Coins/Free Coins Tabovi/ShopFCBILikePage").GetComponent<Collider>().enabled = false;
					ShopManagerFull.ShopObject.transform.Find("1 Free Coins/Free Coins Tabovi/ShopFCBILikePage").GetComponent<Renderer>().material.color = new Color(0.58f, 0.58f, 0.58f);
					ShopManagerFull.ShopObject.transform.Find("1 Free Coins/Free Coins Tabovi/ShopFCBILikePage/Like BananaIsland FC").GetComponent<Renderer>().material.color = new Color(0.58f, 0.58f, 0.58f);
					StagesParser.currentMoney += StagesParser.likePageReward;
					PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
					PlayerPrefs.Save();
					base.StartCoroutine(StagesParser.Instance.moneyCounter(StagesParser.likePageReward, ShopManagerFull.ShopObject.transform.Find("Shop Interface/Coins/Coins Number").GetComponent<TextMesh>(), true));
				}
				if (FacebookManager.stranica == "Webelinx")
				{
					ShopManagerFull.ShopObject.transform.Find("1 Free Coins/Free Coins Tabovi/ShopFCWLLikePage").GetComponent<Collider>().enabled = false;
					ShopManagerFull.ShopObject.transform.Find("1 Free Coins/Free Coins Tabovi/ShopFCWLLikePage").GetComponent<Renderer>().material.color = new Color(0.58f, 0.58f, 0.58f);
					ShopManagerFull.ShopObject.transform.Find("1 Free Coins/Free Coins Tabovi/ShopFCWLLikePage/Like Webelinx FC").GetComponent<Renderer>().material.color = new Color(0.58f, 0.58f, 0.58f);
					StagesParser.currentMoney += StagesParser.likePageReward;
					PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
					PlayerPrefs.Save();
					base.StartCoroutine(StagesParser.Instance.moneyCounter(StagesParser.likePageReward, ShopManagerFull.ShopObject.transform.Find("Shop Interface/Coins/Coins Number").GetComponent<TextMesh>(), true));
				}
				StagesParser.ServerUpdate = 1;
			}
			if (PlayerPrefs.HasKey("Logovan") && PlayerPrefs.GetInt("Logovan") == 0 && FB.IsLoggedIn)
			{
				FB.Logout();
				FacebookManager.Ulogovan = false;
				this.zavrsioUcitavanje = false;
				this.BrojPrijatelja = 0;
				if (FacebookManager.MestoPozivanjaLogina == 1)
				{
					Transform transform = MainScene.LeaderBoardInvite.transform.parent.Find("Friends Tabs");
					Transform[] array = new Transform[transform.childCount];
					for (int i = 0; i < transform.childCount; i++)
					{
						transform.GetChild(i).Find("Friend/LeaderboardYou").GetComponent<SpriteRenderer>().enabled = false;
						array[i] = transform.GetChild(i);
					}
					Transform transform2 = MainScene.LeaderBoardInvite.transform.parent.Find("Friends Tabs/Friend No 2");
					transform2.localPosition = new Vector3(transform2.localPosition.x, -1.85f, transform2.localPosition.z);
					MainScene.FacebookLogIn.SetActive(true);
					MainScene.LeaderBoardInvite.SetActive(false);
					for (int j = 0; j < 10; j++)
					{
						if (j == 1)
						{
							array[j].Find("FB Invite").gameObject.SetActive(true);
							array[j].Find("Friend").gameObject.SetActive(false);
							array[j].Find("FB Invite/Coin Shop").gameObject.SetActive(false);
							array[j].Find("FB Invite/Invite").GetComponent<TextMesh>().text = LanguageManager.LogIn;
							array[j].Find("FB Invite/Invite").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
						}
						else
						{
							array[j].gameObject.SetActive(false);
						}
					}
				}
				FacebookManager.FacebookObject.BrojPrijatelja = 0;
				FacebookManager.FacebookObject.Korisnici.Clear();
				FacebookManager.FacebookObject.Scorovi.Clear();
				FacebookManager.FacebookObject.Imena.Clear();
				FacebookManager.ProfileSlikePrijatelja.Clear();
				FacebookManager.ListaStructPrijatelja.Clear();
				return;
			}
		}
		else
		{
			this.leftApp = true;
			if (StagesParser.ServerUpdate == 1 && FB.IsLoggedIn)
			{
				FacebookManager.FacebookObject.scoreToSet = StagesParser.currentPoints;
				FacebookManager.FacebookObject.proveraPublish_ActionPermisije();
				FacebookManager.FacebookObject.SacuvajScoreNaNivoima(StagesParser.PointsPoNivoima, StagesParser.StarsPoNivoima, StagesParser.maxLevel, StagesParser.bonusLevels);
				FacebookManager.FacebookObject.UpdateujPodatkeKorisnika(StagesParser.currentMoney, StagesParser.currentPoints, LanguageManager.chosenLanguage, StagesParser.currentBananas, StagesParser.powerup_magnets, StagesParser.powerup_shields, StagesParser.powerup_doublecoins, StagesParser.svekupovineGlava, StagesParser.svekupovineMajica, StagesParser.svekupovineLedja, StagesParser.ledja, StagesParser.glava, StagesParser.majica, StagesParser.imaUsi, StagesParser.imaKosu, FacebookManager.NumberOfFriends);
			}
		}
	}

	// Token: 0x060025AB RID: 9643 RVA: 0x00104E61 File Offset: 0x00103061
	public void GetProfilePicture()
	{
		FB.API("/" + FacebookManager.User + "/picture?redirect=true&height=64&type=normal&width=64", HttpMethod.GET, new FacebookDelegate(this.MyPictureCallback), null);
	}

	// Token: 0x060025AC RID: 9644 RVA: 0x00104E8E File Offset: 0x0010308E
	public void MyPictureCallback(FBResult result)
	{
		if (result.Error != null)
		{
			StagesParser.Instance.UgasiLoading();
			return;
		}
		FacebookManager.ProfilePicture = result.Texture;
		GameObject.Find("FaceButton").GetComponent<Renderer>().material.mainTexture = FacebookManager.ProfilePicture;
	}

	// Token: 0x060025AD RID: 9645 RVA: 0x00104ECC File Offset: 0x001030CC
	public void GetFacebookName()
	{
		FB.API("me?fields=id,name", HttpMethod.GET, new FacebookDelegate(this.GetFacebookNameCallback), null);
	}

	// Token: 0x060025AE RID: 9646 RVA: 0x00104EEA File Offset: 0x001030EA
	public void GetFacebookNameCallback(FBResult result)
	{
		if (result.Error != null)
		{
			StagesParser.Instance.UgasiLoading();
			return;
		}
		FacebookManager.UserName = (Json.Deserialize(result.Text) as Dictionary<string, object>)["name"].ToString();
	}

	// Token: 0x060025AF RID: 9647 RVA: 0x00104F24 File Offset: 0x00103124
	public void FaceInvite()
	{
		if (FB.IsLoggedIn)
		{
			int? maxRecipients = null;
			if (this.FriendSelectorMax != "")
			{
				try
				{
					maxRecipients = new int?(int.Parse(this.FriendSelectorMax));
				}
				catch
				{
				}
			}
			string[] excludeIds = (this.FriendSelectorExcludeIds == "") ? null : this.FriendSelectorExcludeIds.Split(new char[]
			{
				','
			});
			FB.AppRequest(this.FriendSelectorMessage, null, this.FriendSelectorFilters, excludeIds, maxRecipients, this.FriendSelectorData, this.FriendSelectorTitle, new FacebookDelegate(this.InviteCallback));
			return;
		}
		FacebookManager.FacebookObject.FacebookLogin();
	}

	// Token: 0x060025B0 RID: 9648 RVA: 0x00104FE0 File Offset: 0x001031E0
	private void InviteCallback(FBResult result)
	{
		List<object> list = (List<object>)(Json.Deserialize(result.Text) as Dictionary<string, object>)["to"];
		StagesParser.currentMoney += list.Count * StagesParser.InviteReward;
		PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
		PlayerPrefs.Save();
		if (Application.loadedLevelName.Contains("Mapa"))
		{
			GameObject.Find("INTERFACE HOLDER/_TopLeft/Coins/Coins Number").GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
			GameObject.Find("INTERFACE HOLDER/_TopLeft/Coins/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		}
	}

	// Token: 0x060025B1 RID: 9649 RVA: 0x00105080 File Offset: 0x00103280
	public void CallFBFeed(string ImeOstrva, int BrojNivoa)
	{
		Dictionary<string, string[]> properties = null;
		if (this.IncludeFeedProperties)
		{
			properties = this.FeedProperties;
		}
		string link = "https://www.facebook.com/pages/Banana-Island/636650059721490";
		FB.Feed("", link, LanguageManager.LevelCompleted, this.FeedLinkKratakOpis, string.Concat(new object[]
		{
			ImeOstrva,
			" - ",
			LanguageManager.Level,
			" ",
			BrojNivoa
		}), this.LinkSlike, this.LinkVideailiZvuka, this.FeedActionName, this.FeedActionLink, this.FeedReference, properties, new FacebookDelegate(this.FeedCallback));
	}

	// Token: 0x060025B2 RID: 9650 RVA: 0x00004095 File Offset: 0x00002295
	private void FeedCallback(FBResult result)
	{
	}

	// Token: 0x060025B3 RID: 9651 RVA: 0x00105115 File Offset: 0x00103315
	public void ProveriPermisije()
	{
		FB.API("/me/permissions", HttpMethod.GET, new FacebookDelegate(this.MyPermissionsCallback), null);
	}

	// Token: 0x060025B4 RID: 9652 RVA: 0x00105134 File Offset: 0x00103334
	public void MyPermissionsCallback(FBResult result)
	{
		if (result.Error != null)
		{
			StagesParser.Instance.UgasiLoading();
			return;
		}
		Dictionary<string, object> dictionary = Json.Deserialize(result.Text) as Dictionary<string, object>;
		object obj = dictionary["data"];
		object obj2 = dictionary["data"];
		List<object> list = new List<object>();
		list = (List<object>)obj2;
		int count = list.Count;
		FacebookManager.permisija = new string[count];
		FacebookManager.statusPermisije = new string[count];
		for (int i = 0; i < count; i++)
		{
			Dictionary<string, object> dictionary2 = list[i] as Dictionary<string, object>;
			FacebookManager.permisija[i] = (string)dictionary2["permission"];
			FacebookManager.statusPermisije[i] = (string)dictionary2["status"];
		}
		bool flag = false;
		for (int j = 0; j < count; j++)
		{
			if (FacebookManager.permisija[j] == "publish_actions")
			{
				flag = true;
				if (FacebookManager.statusPermisije[j] == "granted")
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
					this.CallFBFeed(imeOstrva, StagesParser.currStageIndex + 1);
				}
				else
				{
					FacebookManager.MestoPozivanjaLogina = 3;
					FB.Login(this.permissions, new FacebookDelegate(this.LoginCallback));
				}
			}
		}
		if (!flag)
		{
			FacebookManager.MestoPozivanjaLogina = 3;
			FB.Login(this.permissions, new FacebookDelegate(this.LoginCallback));
		}
	}

	// Token: 0x060025B5 RID: 9653 RVA: 0x001052E3 File Offset: 0x001034E3
	public void proveraPublish_ActionPermisije()
	{
		FB.API("/me/permissions", HttpMethod.GET, new FacebookDelegate(this.Publish_ActionsCallback), null);
	}

	// Token: 0x060025B6 RID: 9654 RVA: 0x00105304 File Offset: 0x00103504
	public void Publish_ActionsCallback(FBResult result)
	{
		if (result.Error != null)
		{
			StagesParser.Instance.UgasiLoading();
			return;
		}
		Dictionary<string, object> dictionary = Json.Deserialize(result.Text) as Dictionary<string, object>;
		object obj = dictionary["data"];
		object obj2 = dictionary["data"];
		List<object> list = new List<object>();
		list = (List<object>)obj2;
		int count = list.Count;
		FacebookManager.permisija = new string[count];
		FacebookManager.statusPermisije = new string[count];
		for (int i = 0; i < count; i++)
		{
			Dictionary<string, object> dictionary2 = list[i] as Dictionary<string, object>;
			FacebookManager.permisija[i] = (string)dictionary2["permission"];
			FacebookManager.statusPermisije[i] = (string)dictionary2["status"];
		}
		this.odobrioPublishActions = false;
		for (int j = 0; j < count; j++)
		{
			if (FacebookManager.permisija[j] == "publish_actions" && FacebookManager.statusPermisije[j] == "granted")
			{
				this.odobrioPublishActions = true;
				this.SetFacebookHighScore(this.scoreToSet);
			}
		}
	}

	// Token: 0x060025B7 RID: 9655 RVA: 0x0010540C File Offset: 0x0010360C
	public void GetRodjendan()
	{
		FB.API("/me?fields=birthday", HttpMethod.GET, new FacebookDelegate(this.MyBirthdayCallback), null);
	}

	// Token: 0x060025B8 RID: 9656 RVA: 0x0010542C File Offset: 0x0010362C
	public void MyBirthdayCallback(FBResult result)
	{
		if (result.Error != null)
		{
			StagesParser.Instance.UgasiLoading();
			return;
		}
		Dictionary<string, object> dictionary = Json.Deserialize(result.Text) as Dictionary<string, object>;
		this.UserRodjendan = dictionary["birthday"].ToString();
	}

	// Token: 0x060025B9 RID: 9657 RVA: 0x00105474 File Offset: 0x00103674
	public void SetFacebookHighScore(int trenutniScore)
	{
		if (FB.IsLoggedIn)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["score"] = trenutniScore.ToString();
			FB.API("/me/scores?", HttpMethod.POST, new FacebookDelegate(this.SetFacebookScoreCallback), dictionary);
		}
	}

	// Token: 0x060025BA RID: 9658 RVA: 0x001054BC File Offset: 0x001036BC
	public void SetFacebookScoreCallback(FBResult result)
	{
		if (result.Error == null && this.resetovanScoreNaNulu == 2)
		{
			this.resetovanScoreNaNulu = 1;
		}
	}

	// Token: 0x060025BB RID: 9659 RVA: 0x001054D6 File Offset: 0x001036D6
	public void GetFacebookHighScore()
	{
		FB.API("me?fields=scores", HttpMethod.GET, new FacebookDelegate(this.GetFacebookHighScoreCallback), null);
	}

	// Token: 0x060025BC RID: 9660 RVA: 0x001054F4 File Offset: 0x001036F4
	public void GetFacebookHighScoreCallback(FBResult result)
	{
		if (result.Error == null)
		{
			Dictionary<string, object> dictionary = Json.Deserialize(result.Text) as Dictionary<string, object>;
			object obj = dictionary["scores"];
			new List<object>();
			object obj2;
			if (dictionary.TryGetValue("scores", out obj2))
			{
				object obj3 = ((Dictionary<string, object>)((List<object>)((Dictionary<string, object>)obj2)["data"])[0])["score"];
			}
		}
	}

	// Token: 0x060025BD RID: 9661 RVA: 0x00105564 File Offset: 0x00103764
	public void GetFacebookFriendScores()
	{
		FB.API("1609658469261083/scores", HttpMethod.GET, new FacebookDelegate(this.GetFacebookFriendScoresCallback), null);
	}

	// Token: 0x060025BE RID: 9662 RVA: 0x00105584 File Offset: 0x00103784
	public void GetFacebookFriendScoresCallback(FBResult result)
	{
		if (result.Error != null)
		{
			StagesParser.Instance.UgasiLoading();
			return;
		}
		Dictionary<string, object> dictionary = Json.Deserialize(result.Text) as Dictionary<string, object>;
		object obj = dictionary["data"];
		object obj2 = dictionary["data"];
		List<object> list = new List<object>();
		list = (List<object>)obj2;
		int count = list.Count;
		this.Korisnici = new List<string>();
		this.Scorovi = new List<string>();
		this.Imena = new List<string>();
		FacebookManager.NumberOfFriends = list.Count;
		for (int i = 0; i < list.Count; i++)
		{
			Dictionary<string, object> dictionary2 = list[i] as Dictionary<string, object>;
			object obj3;
			if (dictionary2.TryGetValue("user", out obj3))
			{
				string item = (string)((Dictionary<string, object>)obj3)["name"];
				string item2 = (string)((Dictionary<string, object>)obj3)["id"];
				this.Korisnici.Add(item2);
				this.Scorovi.Add(dictionary2["score"].ToString());
				this.Imena.Add(item);
			}
		}
		if (FacebookManager.MestoPozivanjaLogina == 1)
		{
			for (int j = 0; j < 10; j++)
			{
				Camera.main.GetComponent<MainScene>().FriendsObjects[j].gameObject.SetActive(true);
				Camera.main.GetComponent<MainScene>().FriendsObjects[j].Find("FB Invite").gameObject.SetActive(false);
				Camera.main.GetComponent<MainScene>().FriendsObjects[j].Find("Friend").gameObject.SetActive(true);
			}
			for (int k = 0; k < this.Scorovi.Count; k++)
			{
				int num = 1 + k;
				if (k < 10)
				{
					Camera.main.GetComponent<MainScene>().FriendsObjects[k].gameObject.SetActive(true);
					GameObject.Find("Pts Number " + num.ToString()).GetComponent<TextMesh>().text = this.Scorovi[k];
					GameObject.Find("Pts Number " + num.ToString()).GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
					GameObject.Find("Name " + num.ToString()).GetComponent<TextMesh>().text = this.Imena[k];
					GameObject.Find("Name " + num.ToString()).GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
					if (this.Korisnici[k] == FB.UserId)
					{
						Camera.main.GetComponent<MainScene>().FriendsObjects[k].Find("Friend/LeaderboardYou").GetComponent<SpriteRenderer>().enabled = true;
					}
				}
			}
			for (int l = this.Imena.Count; l < 10; l++)
			{
				Camera.main.GetComponent<MainScene>().FriendsObjects[l].Find("FB Invite").gameObject.SetActive(true);
				Camera.main.GetComponent<MainScene>().FriendsObjects[l].Find("FB Invite/Coin Number").GetComponent<TextMesh>().text = "+" + StagesParser.InviteReward;
				Camera.main.GetComponent<MainScene>().FriendsObjects[l].Find("FB Invite/Coin Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, false, true);
				Camera.main.GetComponent<MainScene>().FriendsObjects[l].Find("Friend").gameObject.SetActive(false);
			}
			ObjLeaderboard.Leaderboard = true;
			SwipeControlLeaderboard.controlEnabled = true;
		}
		base.StartCoroutine("GetFriendPictures");
		base.StartCoroutine("TrenutniNivoSvihPrijatelja");
	}

	// Token: 0x060025BF RID: 9663 RVA: 0x0010594C File Offset: 0x00103B4C
	private IEnumerator GetFriendPictures()
	{
		int i = 0;
		if (FacebookManager.Ulogovan)
		{
			while (i < this.Korisnici.Count)
			{
				this.WaitForFacebook = false;
				this.GetFacebookFriendPicture(this.Korisnici[i]);
				while (!this.WaitForFacebook && FacebookManager.Ulogovan)
				{
					yield return null;
				}
				int num = i;
				i = num + 1;
			}
			this.BrojPrijatelja = 0;
			yield return null;
			if (this.zavrsioUcitavanje)
			{
				StagesParser.Instance.UgasiLoading();
				PlayerPrefs.SetInt("Logovan", 1);
				if (FacebookManager.MestoPozivanjaLogina == 1)
				{
					this.RefreshujScenu1PosleLogina();
				}
				else if (FacebookManager.MestoPozivanjaLogina == 2)
				{
					this.RefreshujScenu2PosleLogina();
				}
				else if (FacebookManager.MestoPozivanjaLogina == 3)
				{
					this.RefreshujScenu3PosleLogina();
				}
				this.zavrsioUcitavanje = false;
			}
			else
			{
				this.zavrsioUcitavanje = true;
			}
		}
		yield return null;
		yield break;
	}

	// Token: 0x060025C0 RID: 9664 RVA: 0x0010595B File Offset: 0x00103B5B
	public void SpisakSvihFacebookPrijatelja()
	{
		FB.API("/fql?q=SELECT+uid,name,pic_square+FROM+user+WHERE+is_app_user=1+AND+uid+IN+(SELECT+uid2+FROM+friend+WHERE+uid1=me())", HttpMethod.GET, new FacebookDelegate(this.SviPrijateljiFacebookCallback), null);
	}

	// Token: 0x060025C1 RID: 9665 RVA: 0x0010597C File Offset: 0x00103B7C
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
		int count = list.Count;
		this.Prijatelji = new List<string>();
		for (int i = 0; i < list.Count; i++)
		{
			Dictionary<string, object> dictionary = list[i] as Dictionary<string, object>;
			object obj2;
			if (dictionary.TryGetValue("name", out obj2))
			{
				this.nesto++;
				string item = dictionary["uid"].ToString();
				this.Prijatelji.Add(item);
			}
		}
	}

	// Token: 0x060025C2 RID: 9666 RVA: 0x00105A2E File Offset: 0x00103C2E
	public void GetFacebookFriendPicture(string PrijateljevID)
	{
		if (FacebookManager.Ulogovan)
		{
			this.prijateljevIDzaSliku = PrijateljevID;
			FB.API(PrijateljevID + "/picture?redirect=true&height=64&type=normal&width=64", HttpMethod.GET, new FacebookDelegate(this.FacebookFriendPictureCallback), null);
		}
	}

	// Token: 0x060025C3 RID: 9667 RVA: 0x00105A60 File Offset: 0x00103C60
	public void FacebookFriendPictureCallback(FBResult result)
	{
		if (result.Error != null)
		{
			StagesParser.Instance.UgasiLoading();
			return;
		}
		this.BrojPrijatelja++;
		if (this.BrojPrijatelja >= this.Korisnici.Count)
		{
			this.WaitForFacebook = true;
		}
		FacebookManager.IDiSlika item = default(FacebookManager.IDiSlika);
		item.PrijateljID = this.prijateljevIDzaSliku;
		item.profilePicture = result.Texture;
		FacebookManager.ProfileSlikePrijatelja.Add(item);
		if (FacebookManager.MestoPozivanjaLogina == 1)
		{
			switch (this.BrojPrijatelja)
			{
			case 1:
				GameObject.Find("Friends Level Win Picture 1").GetComponent<Renderer>().material.mainTexture = FacebookManager.ProfileSlikePrijatelja[0].profilePicture;
				this.WaitForFacebook = true;
				break;
			case 2:
				GameObject.Find("Friends Level Win Picture 2").GetComponent<Renderer>().material.mainTexture = FacebookManager.ProfileSlikePrijatelja[1].profilePicture;
				this.WaitForFacebook = true;
				break;
			case 3:
				GameObject.Find("Friends Level Win Picture 3").GetComponent<Renderer>().material.mainTexture = FacebookManager.ProfileSlikePrijatelja[2].profilePicture;
				this.WaitForFacebook = true;
				break;
			case 4:
				GameObject.Find("Friends Level Win Picture 4").GetComponent<Renderer>().material.mainTexture = FacebookManager.ProfileSlikePrijatelja[3].profilePicture;
				this.WaitForFacebook = true;
				break;
			case 5:
				GameObject.Find("Friends Level Win Picture 5").GetComponent<Renderer>().material.mainTexture = FacebookManager.ProfileSlikePrijatelja[4].profilePicture;
				this.WaitForFacebook = true;
				break;
			case 6:
				GameObject.Find("Friends Level Win Picture 6").GetComponent<Renderer>().material.mainTexture = FacebookManager.ProfileSlikePrijatelja[5].profilePicture;
				this.WaitForFacebook = true;
				break;
			case 7:
				GameObject.Find("Friends Level Win Picture 7").GetComponent<Renderer>().material.mainTexture = FacebookManager.ProfileSlikePrijatelja[6].profilePicture;
				this.WaitForFacebook = true;
				break;
			case 8:
				GameObject.Find("Friends Level Win Picture 8").GetComponent<Renderer>().material.mainTexture = FacebookManager.ProfileSlikePrijatelja[7].profilePicture;
				this.WaitForFacebook = true;
				break;
			case 9:
				GameObject.Find("Friends Level Win Picture 9").GetComponent<Renderer>().material.mainTexture = FacebookManager.ProfileSlikePrijatelja[8].profilePicture;
				this.WaitForFacebook = true;
				break;
			case 10:
				GameObject.Find("Friends Level Win Picture 10").GetComponent<Renderer>().material.mainTexture = FacebookManager.ProfileSlikePrijatelja[9].profilePicture;
				this.WaitForFacebook = true;
				break;
			}
			if (this.BrojPrijatelja > 10)
			{
				this.WaitForFacebook = true;
				return;
			}
		}
		else
		{
			this.WaitForFacebook = true;
		}
	}

	// Token: 0x060025C4 RID: 9668 RVA: 0x00105D3D File Offset: 0x00103F3D
	public void GetFacebookGameAchievements()
	{
		FB.API(FB.AppId + "/achievements", HttpMethod.GET, new FacebookDelegate(this.GetFacebookGameAchievementsCallback), null);
	}

	// Token: 0x060025C5 RID: 9669 RVA: 0x00105D68 File Offset: 0x00103F68
	public void GetFacebookGameAchievementsCallback(FBResult result)
	{
		if (result.Error == null)
		{
			object obj = (Json.Deserialize(result.Text) as Dictionary<string, object>)["data"];
			List<object> list = new List<object>();
			list = (List<object>)obj;
			int count = list.Count;
			for (int i = 0; i < list.Count; i++)
			{
				object obj2;
				(list[i] as Dictionary<string, object>).TryGetValue("title", out obj2);
			}
		}
	}

	// Token: 0x060025C6 RID: 9670 RVA: 0x00105DD4 File Offset: 0x00103FD4
	public void DodajFacebookAchievement(string URLAchivmenta)
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary["achievement"] = URLAchivmenta;
		FB.API("me/achievements", HttpMethod.POST, new FacebookDelegate(this.DodajFacebookAchievementCallback), dictionary);
	}

	// Token: 0x060025C7 RID: 9671 RVA: 0x00105E0F File Offset: 0x0010400F
	public void DodajFacebookAchievementCallback(FBResult result)
	{
		string error = result.Error;
	}

	// Token: 0x060025C8 RID: 9672 RVA: 0x00105E18 File Offset: 0x00104018
	public void ObrisiFacebookAchievement(string UrlACH)
	{
		FB.API("me/achievements?achievement=" + UrlACH, HttpMethod.DELETE, new FacebookDelegate(this.ObrisiFacebookAchievementCallback), null);
	}

	// Token: 0x060025C9 RID: 9673 RVA: 0x00105E0F File Offset: 0x0010400F
	public void ObrisiFacebookAchievementCallback(FBResult result)
	{
		string error = result.Error;
	}

	// Token: 0x060025CA RID: 9674 RVA: 0x00105E3C File Offset: 0x0010403C
	public void ProveraFacebookAchievmenta()
	{
		FB.API("me/achievements", HttpMethod.GET, new FacebookDelegate(this.ProveraAchievmentaCallback), null);
	}

	// Token: 0x060025CB RID: 9675 RVA: 0x00105E0F File Offset: 0x0010400F
	public void ProveraAchievmentaCallback(FBResult result)
	{
		string error = result.Error;
	}

	// Token: 0x060025CC RID: 9676 RVA: 0x00105E5C File Offset: 0x0010405C
	public void InicijalizujKorisnika(string KorisnikovID, int numCoins, int Score, string Jezik, int Banana, int Shield, int Magnet, int DoubleCoins, string UserSveKupovineHats, string UserSveKupovineShirts, string UserSveKupovineBackPacks)
	{
		if (FB.IsLoggedIn)
		{
			this.Korisnik["UserID"] = KorisnikovID;
			this.Korisnik["Name"] = FacebookManager.UserName;
			this.Korisnik["Coins"] = numCoins;
			this.Korisnik["Score"] = Score;
			this.Korisnik["Language"] = Jezik;
			this.Korisnik["Banana"] = Banana;
			this.Korisnik["PowerShield"] = Shield;
			this.Korisnik["PowerMagnet"] = Magnet;
			this.Korisnik["PowerDoubleCoins"] = DoubleCoins;
			this.Korisnik["UserSveKupovineHats"] = UserSveKupovineHats;
			this.Korisnik["UserSveKupovineShirts"] = UserSveKupovineShirts;
			this.Korisnik["UserSveKupovineBackPacks"] = UserSveKupovineBackPacks;
			this.Korisnik["GlavaItems"] = 0;
			this.Korisnik["TeloItems"] = 0;
			this.Korisnik["LedjaItems"] = 0;
			this.Korisnik["Usi"] = true;
			this.Korisnik["Kosa"] = true;
			this.Korisnik["NumberOfFriends"] = FacebookManager.NumberOfFriends;
			this.Korisnik["OdgledaoShopTutorial"] = StagesParser.otvaraoShopNekad;
			this.Korisnik["JezikPromenjen"] = StagesParser.jezikPromenjen;
		}
	}

	// Token: 0x060025CD RID: 9677 RVA: 0x00106028 File Offset: 0x00104228
	public void ProcitajPodatkeKorisnika()
	{
		StagesParser.languageBefore = LanguageManager.chosenLanguage;
		if (FB.IsLoggedIn)
		{
			ParseObject.GetQuery("User").WhereEqualTo("UserID", FacebookManager.User).FirstAsync().ContinueWith(delegate(Task<ParseObject> t)
			{
				if (t.IsCompleted)
				{
					ParseObject result = t.Result;
					FacebookManager.UserCoins = result.Get<int>("Coins");
					FacebookManager.UserScore = result.Get<int>("Score");
					FacebookManager.UserLanguage = result.Get<string>("Language");
					FacebookManager.UserBanana = result.Get<int>("Banana");
					FacebookManager.UserPowerMagnet = result.Get<int>("PowerMagnet");
					FacebookManager.UserPowerShield = result.Get<int>("PowerShield");
					FacebookManager.UserPowerDoubleCoins = result.Get<int>("PowerDoubleCoins");
					FacebookManager.UserSveKupovineHats = result.Get<string>("UserSveKupovineHats");
					FacebookManager.UserSveKupovineShirts = result.Get<string>("UserSveKupovineShirts");
					FacebookManager.UserSveKupovineBackPacks = result.Get<string>("UserSveKupovineBackPacks");
					StagesParser.otvaraoShopNekad = result.Get<int>("OdgledaoShopTutorial");
					StagesParser.jezikPromenjen = result.Get<int>("JezikPromenjen");
					FacebookManager.GlavaItem = result.Get<int>("GlavaItems");
					FacebookManager.TeloItem = result.Get<int>("TeloItems");
					FacebookManager.LedjaItem = result.Get<int>("LedjaItems");
					FacebookManager.Usi = result.Get<bool>("Usi");
					FacebookManager.Kosa = result.Get<bool>("Kosa");
					FacebookManager.KorisnikoviPodaciSpremni = true;
					return;
				}
				if (t.IsFaulted || t.IsCanceled)
				{
					FacebookManager.UserCoins = StagesParser.currentMoney;
					FacebookManager.UserScore = StagesParser.currentPoints;
					FacebookManager.UserLanguage = LanguageManager.chosenLanguage;
					FacebookManager.UserBanana = StagesParser.currentBananas;
					FacebookManager.UserPowerMagnet = StagesParser.powerup_magnets;
					FacebookManager.UserPowerShield = StagesParser.powerup_shields;
					FacebookManager.UserPowerDoubleCoins = StagesParser.powerup_doublecoins;
					FacebookManager.UserSveKupovineHats = StagesParser.svekupovineGlava;
					FacebookManager.UserSveKupovineShirts = StagesParser.svekupovineMajica;
					FacebookManager.UserSveKupovineBackPacks = StagesParser.svekupovineLedja;
					FacebookManager.GlavaItem = StagesParser.glava;
					FacebookManager.TeloItem = StagesParser.majica;
					FacebookManager.LedjaItem = StagesParser.ledja;
					FacebookManager.Usi = StagesParser.imaUsi;
					FacebookManager.Kosa = StagesParser.imaKosu;
					FacebookManager.KorisnikoviPodaciSpremni = true;
				}
			});
			return;
		}
		FacebookManager.UserCoins = StagesParser.currentMoney;
		FacebookManager.UserScore = StagesParser.currentPoints;
		FacebookManager.UserLanguage = LanguageManager.chosenLanguage;
		FacebookManager.UserBanana = StagesParser.currentBananas;
		FacebookManager.UserPowerMagnet = StagesParser.powerup_magnets;
		FacebookManager.UserPowerShield = StagesParser.powerup_shields;
		FacebookManager.UserPowerDoubleCoins = StagesParser.powerup_doublecoins;
		FacebookManager.UserSveKupovineHats = StagesParser.svekupovineGlava;
		FacebookManager.UserSveKupovineShirts = StagesParser.svekupovineMajica;
		FacebookManager.UserSveKupovineBackPacks = StagesParser.svekupovineLedja;
		FacebookManager.GlavaItem = StagesParser.glava;
		FacebookManager.TeloItem = StagesParser.majica;
		FacebookManager.LedjaItem = StagesParser.ledja;
		FacebookManager.Usi = StagesParser.imaUsi;
		FacebookManager.Kosa = StagesParser.imaKosu;
		FacebookManager.KorisnikoviPodaciSpremni = true;
	}

	// Token: 0x060025CE RID: 9678 RVA: 0x00106126 File Offset: 0x00104326
	public void ProveriKorisnika()
	{
		FacebookManager.ListaStructPrijatelja.Clear();
		base.StartCoroutine("DaLiPostojiKorisnik");
	}

	// Token: 0x060025CF RID: 9679 RVA: 0x0010613E File Offset: 0x0010433E
	private IEnumerator DaLiPostojiKorisnik()
	{
		if (FB.IsLoggedIn)
		{
			ParseQuery<ParseObject> parseQuery = ParseObject.GetQuery("User").WhereEqualTo("UserID", FacebookManager.User).Limit(1);
			Task<IEnumerable<ParseObject>> queryTask = parseQuery.FindAsync();
			while (!queryTask.IsCompleted)
			{
				yield return null;
			}
			if (((ReadOnlyCollection<ParseObject>)queryTask.Result).Count > 0)
			{
				this.ProcitajPodatkeKorisnika();
				this.GetFacebookFriendScores();
				this.nePostojiKorisnik = false;
			}
			else
			{
				this.nePostojiKorisnik = true;
				StagesParser.currentMoney += StagesParser.LoginReward;
				PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
				PlayerPrefs.Save();
				if (FacebookManager.MestoPozivanjaLogina == 1)
				{
					GameObject gameObject = GameObject.Find("CoinsReward");
					gameObject.GetComponent<Animation>().Play("CoinsRewardDolazak");
					base.StartCoroutine(StagesParser.Instance.moneyCounter(2000, gameObject.transform.Find("Coins Number").GetComponent<TextMesh>(), true));
					base.Invoke("SkloniCoinsReward", 1.2f);
				}
				else
				{
					base.StartCoroutine(StagesParser.Instance.moneyCounter(StagesParser.LoginReward, GameObject.Find("_GUI").transform.Find("INTERFACE HOLDER/_TopLeft/Coins/Coins Number").GetComponent<TextMesh>(), true));
				}
				StagesParser.ServerUpdate = 1;
				this.scoreToSet = StagesParser.currentPoints;
				this.proveraPublish_ActionPermisije();
				this.InicijalizujKorisnika(FacebookManager.User, StagesParser.currentMoney, StagesParser.currentPoints, LanguageManager.chosenLanguage, StagesParser.currentBananas, StagesParser.powerup_shields, StagesParser.powerup_magnets, StagesParser.powerup_doublecoins, StagesParser.svekupovineGlava, StagesParser.svekupovineMajica, StagesParser.svekupovineLedja);
				this.InicijalizujScoreNaNivoima(StagesParser.StarsPoNivoima, StagesParser.PointsPoNivoima, StagesParser.maxLevel, StagesParser.bonusLevels);
				this.GetFacebookFriendScores();
			}
			queryTask = null;
		}
		else
		{
			this.ProcitajPodatkeKorisnika();
		}
		yield break;
	}

	// Token: 0x060025D0 RID: 9680 RVA: 0x00106150 File Offset: 0x00104350
	public void UpdateujPodatkeKorisnika(int BrojCoina, int Score, string Jezik, int Banana, int PowerMagnet, int PowerShield, int PowerDoubleCoins, string UserSveKupovineHats, string UserSveKupovineShirts, string UserSveKupovineBackPacks, int Ledja, int Glava, int Telo, bool Usi, bool Kosa, int NumberOfFriends)
	{
		if (FB.IsLoggedIn)
		{
			ParseObject.GetQuery("User").WhereEqualTo("UserID", FacebookManager.User).FirstAsync().ContinueWith(delegate(Task<ParseObject> t)
			{
				if (t.IsCompleted)
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
					this.updatedSuccessfullyPodaciKorisnika = true;
					if (StagesParser.ServerUpdate == 1 && this.updatedSuccessfullyScoreNaNivoima)
					{
						StagesParser.ServerUpdate = 2;
						PlayerPrefs.SetInt("ServerUpdate", StagesParser.ServerUpdate);
						PlayerPrefs.Save();
						return;
					}
				}
				else if (!t.IsFaulted)
				{
					bool isCanceled = t.IsCanceled;
				}
			});
		}
	}

	// Token: 0x060025D1 RID: 9681 RVA: 0x0010621E File Offset: 0x0010441E
	private void SkloniCoinsReward()
	{
		GameObject.Find("CoinsReward").GetComponent<Animation>().Play("CoinsRewardOdlazak");
	}

	// Token: 0x060025D2 RID: 9682 RVA: 0x0010623C File Offset: 0x0010443C
	public void SacuvajBrojNovcica(int numCoins)
	{
		if (FB.IsLoggedIn)
		{
			ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
			query.WhereEqualTo("UserID", FacebookManager.User);
			query.FirstAsync().ContinueWith(delegate(Task<ParseObject> t)
			{
				if (t.IsCompleted)
				{
					ParseObject result = t.Result;
					result["Coins"] = numCoins;
					result.SaveAsync();
					return;
				}
				if (!t.IsFaulted)
				{
					bool isCanceled = t.IsCanceled;
				}
			});
		}
	}

	// Token: 0x060025D3 RID: 9683 RVA: 0x00106290 File Offset: 0x00104490
	public void ProcitajBrojNovcica()
	{
		if (FB.IsLoggedIn)
		{
			ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
			query.WhereEqualTo("UserID", FacebookManager.User);
			query.FirstAsync().ContinueWith(delegate(Task<ParseObject> t)
			{
				if (t.IsCompleted)
				{
					t.Result.Get<int>("Coins");
					return;
				}
				if (!t.IsFaulted)
				{
					bool isCanceled = t.IsCanceled;
				}
			});
		}
	}

	// Token: 0x060025D4 RID: 9684 RVA: 0x001062EC File Offset: 0x001044EC
	public void SacuvajBrojBanana(int BrojBanana)
	{
		if (FB.IsLoggedIn)
		{
			ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
			query.WhereEqualTo("UserID", FacebookManager.User);
			query.FirstAsync().ContinueWith(delegate(Task<ParseObject> t)
			{
				if (t.IsCompleted)
				{
					ParseObject result = t.Result;
					result["Banana"] = BrojBanana;
					result.SaveAsync();
					return;
				}
				if (!t.IsFaulted)
				{
					bool isCanceled = t.IsCanceled;
				}
			});
		}
	}

	// Token: 0x060025D5 RID: 9685 RVA: 0x00106340 File Offset: 0x00104540
	public void ProcitajBrojBanana()
	{
		if (FB.IsLoggedIn)
		{
			ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
			query.WhereEqualTo("UserID", FacebookManager.User);
			query.FirstAsync().ContinueWith(delegate(Task<ParseObject> t)
			{
				if (t.IsCompleted)
				{
					t.Result.Get<int>("Banana");
					return;
				}
				if (!t.IsFaulted)
				{
					bool isCanceled = t.IsCanceled;
				}
			});
		}
	}

	// Token: 0x060025D6 RID: 9686 RVA: 0x0010639C File Offset: 0x0010459C
	public void SacuvajScore(int GlobalScore)
	{
		if (FB.IsLoggedIn)
		{
			ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
			query.WhereEqualTo("UserID", FacebookManager.User);
			query.FirstAsync().ContinueWith(delegate(Task<ParseObject> t)
			{
				if (t.IsCompleted)
				{
					ParseObject result = t.Result;
					result["Score"] = GlobalScore;
					result.SaveAsync();
					return;
				}
				if (!t.IsFaulted)
				{
					bool isCanceled = t.IsCanceled;
				}
			});
		}
	}

	// Token: 0x060025D7 RID: 9687 RVA: 0x001063F0 File Offset: 0x001045F0
	public void ProcitajScore()
	{
		if (FB.IsLoggedIn)
		{
			ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
			query.WhereEqualTo("UserID", FacebookManager.User);
			query.FirstAsync().ContinueWith(delegate(Task<ParseObject> t)
			{
				if (t.IsCompleted)
				{
					t.Result.Get<int>("Score");
					return;
				}
				if (!t.IsFaulted)
				{
					bool isCanceled = t.IsCanceled;
				}
			});
		}
	}

	// Token: 0x060025D8 RID: 9688 RVA: 0x0010644C File Offset: 0x0010464C
	public void SacuvajLanguage(string NoviJezik)
	{
		if (FB.IsLoggedIn)
		{
			ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
			query.WhereEqualTo("UserID", FacebookManager.User);
			query.FirstAsync().ContinueWith(delegate(Task<ParseObject> t)
			{
				if (t.IsCompleted)
				{
					ParseObject result = t.Result;
					result["Language"] = NoviJezik;
					result.SaveAsync();
					return;
				}
				if (!t.IsFaulted)
				{
					bool isCanceled = t.IsCanceled;
				}
			});
		}
	}

	// Token: 0x060025D9 RID: 9689 RVA: 0x001064A0 File Offset: 0x001046A0
	public void ProcitajLanguage()
	{
		if (FB.IsLoggedIn)
		{
			ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
			query.WhereEqualTo("UserID", FacebookManager.User);
			query.FirstAsync().ContinueWith(delegate(Task<ParseObject> t)
			{
				if (t.IsCompleted)
				{
					t.Result.Get<string>("Language");
					return;
				}
				if (!t.IsFaulted)
				{
					bool isCanceled = t.IsCanceled;
				}
			});
		}
	}

	// Token: 0x060025DA RID: 9690 RVA: 0x001064FC File Offset: 0x001046FC
	public void SacuvajPowerShield(int BrojPowerShield)
	{
		if (FB.IsLoggedIn)
		{
			ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
			query.WhereEqualTo("UserID", FacebookManager.User);
			query.FirstAsync().ContinueWith(delegate(Task<ParseObject> t)
			{
				if (t.IsCompleted)
				{
					ParseObject result = t.Result;
					result["PowerShield"] = BrojPowerShield;
					result.SaveAsync();
					return;
				}
				if (!t.IsFaulted)
				{
					bool isCanceled = t.IsCanceled;
				}
			});
		}
	}

	// Token: 0x060025DB RID: 9691 RVA: 0x00106550 File Offset: 0x00104750
	public void ProcitajPowerShield()
	{
		if (FB.IsLoggedIn)
		{
			ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
			query.WhereEqualTo("UserID", FacebookManager.User);
			query.FirstAsync().ContinueWith(delegate(Task<ParseObject> t)
			{
				if (t.IsCompleted)
				{
					t.Result.Get<int>("PowerShield");
					return;
				}
				if (!t.IsFaulted)
				{
					bool isCanceled = t.IsCanceled;
				}
			});
		}
	}

	// Token: 0x060025DC RID: 9692 RVA: 0x001065AC File Offset: 0x001047AC
	public void SacuvajPowerMagnet(int BrojPowerMagnet)
	{
		if (FB.IsLoggedIn)
		{
			ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
			query.WhereEqualTo("UserID", FacebookManager.User);
			query.FirstAsync().ContinueWith(delegate(Task<ParseObject> t)
			{
				if (t.IsCompleted)
				{
					ParseObject result = t.Result;
					result["PowerMagnet"] = BrojPowerMagnet;
					result.SaveAsync();
					return;
				}
				if (!t.IsFaulted)
				{
					bool isCanceled = t.IsCanceled;
				}
			});
		}
	}

	// Token: 0x060025DD RID: 9693 RVA: 0x00106600 File Offset: 0x00104800
	public void ProcitajPowerMagnet()
	{
		if (FB.IsLoggedIn)
		{
			ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
			query.WhereEqualTo("UserID", FacebookManager.User);
			query.FirstAsync().ContinueWith(delegate(Task<ParseObject> t)
			{
				if (t.IsCompleted)
				{
					t.Result.Get<int>("PowerMagnet");
					return;
				}
				if (!t.IsFaulted)
				{
					bool isCanceled = t.IsCanceled;
				}
			});
		}
	}

	// Token: 0x060025DE RID: 9694 RVA: 0x0010665C File Offset: 0x0010485C
	public void SacuvajPowerDoubleCoins(int BrojPowerDoubleCoins)
	{
		if (FB.IsLoggedIn)
		{
			ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
			query.WhereEqualTo("UserID", FacebookManager.User);
			query.FirstAsync().ContinueWith(delegate(Task<ParseObject> t)
			{
				if (t.IsCompleted)
				{
					ParseObject result = t.Result;
					result["PowerDoubleCoins"] = BrojPowerDoubleCoins;
					result.SaveAsync();
					return;
				}
				if (!t.IsFaulted)
				{
					bool isCanceled = t.IsCanceled;
				}
			});
		}
	}

	// Token: 0x060025DF RID: 9695 RVA: 0x001066B0 File Offset: 0x001048B0
	public void ProcitajPowerDoubleCoins()
	{
		if (FB.IsLoggedIn)
		{
			ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
			query.WhereEqualTo("UserID", FacebookManager.User);
			query.FirstAsync().ContinueWith(delegate(Task<ParseObject> t)
			{
				if (t.IsCompleted)
				{
					t.Result.Get<int>("PowerDoubleCoins");
					return;
				}
				if (!t.IsFaulted)
				{
					bool isCanceled = t.IsCanceled;
				}
			});
		}
	}

	// Token: 0x060025E0 RID: 9696 RVA: 0x0010670C File Offset: 0x0010490C
	public void SacuvajSveMoci(int BrojPowerShield, int BrojPowerMagnet, int BrojPowerDoubleCoins)
	{
		if (FB.IsLoggedIn)
		{
			ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
			query.WhereEqualTo("UserID", FacebookManager.User);
			query.FirstAsync().ContinueWith(delegate(Task<ParseObject> t)
			{
				if (t.IsCompleted)
				{
					ParseObject result = t.Result;
					result["PowerShield"] = BrojPowerShield;
					result["PowerMagnet"] = BrojPowerMagnet;
					result["PowerDoubleCoins"] = BrojPowerDoubleCoins;
					result.SaveAsync();
					return;
				}
				if (!t.IsFaulted)
				{
					bool isCanceled = t.IsCanceled;
				}
			});
		}
	}

	// Token: 0x060025E1 RID: 9697 RVA: 0x00106770 File Offset: 0x00104970
	public void ProcitajSveMoci()
	{
		if (FB.IsLoggedIn)
		{
			ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
			query.WhereEqualTo("UserID", FacebookManager.User);
			query.FirstAsync().ContinueWith(delegate(Task<ParseObject> t)
			{
				if (t.IsCompleted)
				{
					ParseObject result = t.Result;
					result.Get<int>("PowerShield");
					result.Get<int>("PowerMagnet");
					result.Get<int>("PowerDoubleCoins");
					return;
				}
				if (!t.IsFaulted)
				{
					bool isCanceled = t.IsCanceled;
				}
			});
		}
	}

	// Token: 0x060025E2 RID: 9698 RVA: 0x001067CC File Offset: 0x001049CC
	public void SacuvajKupljeneStvari(string UserSveKupovineHats, string UserSveKupovineShirts, string UserSveKupovineBackPacks)
	{
		if (FB.IsLoggedIn)
		{
			ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
			query.WhereEqualTo("UserID", FacebookManager.User);
			query.FirstAsync().ContinueWith(delegate(Task<ParseObject> t)
			{
				if (t.IsCompleted)
				{
					ParseObject result = t.Result;
					result["UserSveKupovineHats"] = UserSveKupovineHats;
					result["UserSveKupovineShirts"] = UserSveKupovineShirts;
					result["UserSveKupovineBackPacks"] = UserSveKupovineBackPacks;
					result.SaveAsync();
					return;
				}
				if (!t.IsFaulted)
				{
					bool isCanceled = t.IsCanceled;
				}
			});
		}
	}

	// Token: 0x060025E3 RID: 9699 RVA: 0x00106830 File Offset: 0x00104A30
	public void ProcitajKupljeneStvari()
	{
		if (FB.IsLoggedIn)
		{
			ParseQuery<ParseObject> query = ParseObject.GetQuery("User");
			query.WhereEqualTo("UserID", FacebookManager.User);
			query.FirstAsync().ContinueWith(delegate(Task<ParseObject> t)
			{
				if (t.IsCompleted)
				{
					ParseObject result = t.Result;
					FacebookManager.UserSveKupovineHats = result.Get<string>("UserSveKupovineHats");
					FacebookManager.UserSveKupovineShirts = result.Get<string>("UserSveKupovineShirts");
					FacebookManager.UserSveKupovineHats = result.Get<string>("UserSveKupovineHats");
					return;
				}
				if (!t.IsFaulted)
				{
					bool isCanceled = t.IsCanceled;
				}
			});
		}
	}

	// Token: 0x060025E4 RID: 9700 RVA: 0x0010688C File Offset: 0x00104A8C
	public void InicijalizujScoreNaNivoima(int[] NizBrojZvezdaPoNivou, int[] NizScorovaPoNivoima, int MaxNivo, string BonusLevels)
	{
		this.LevelScore["NumOfStars"] = NizBrojZvezdaPoNivou;
		this.LevelScore["LevelScore"] = NizScorovaPoNivoima;
		this.LevelScore["MaxLevel"] = MaxNivo;
		this.LevelScore["BonusLevels"] = BonusLevels;
		this.LevelScore["UserID"] = FacebookManager.User;
	}

	// Token: 0x060025E5 RID: 9701 RVA: 0x001068F8 File Offset: 0x00104AF8
	public void SacuvajScoreNaNivoima(int[] ScorePoNivoima, int[] BrojZvezdaPoNivoima, int TrenutniNivoIgraca, string BonusLevels)
	{
		if (FB.IsLoggedIn)
		{
			ParseObject.GetQuery("LevelScore").WhereEqualTo("UserID", FacebookManager.User).FirstAsync().ContinueWith(delegate(Task<ParseObject> t)
			{
				if (t.IsCompleted)
				{
					ParseObject result = t.Result;
					result["LevelScore"] = ScorePoNivoima;
					result["NumOfStars"] = BrojZvezdaPoNivoima;
					result["MaxLevel"] = TrenutniNivoIgraca;
					result["BonusLevels"] = BonusLevels;
					result.SaveAsync();
					this.updatedSuccessfullyScoreNaNivoima = true;
					if (StagesParser.ServerUpdate == 3)
					{
						StagesParser.ServerUpdate = 2;
						this.OKzaLogout = true;
						return;
					}
				}
				else if (!t.IsFaulted)
				{
					bool isCanceled = t.IsCanceled;
				}
			});
			if (StagesParser.ServerUpdate == 1 && this.updatedSuccessfullyPodaciKorisnika)
			{
				StagesParser.ServerUpdate = 2;
				PlayerPrefs.SetInt("ServerUpdate", StagesParser.ServerUpdate);
				PlayerPrefs.Save();
			}
		}
	}

	// Token: 0x060025E6 RID: 9702 RVA: 0x00106990 File Offset: 0x00104B90
	public void ProcitajScoreNaNivoima()
	{
		if (FB.IsLoggedIn)
		{
			ParseObject.GetQuery("LevelScore").WhereEqualTo("UserID", FacebookManager.User).FirstAsync().ContinueWith(delegate(Task<ParseObject> t)
			{
				if (t.IsCompleted)
				{
					ParseObject result = t.Result;
					result.Get<IList<int>>("LevelScore");
					result.Get<IList<int>>("NumOfStars");
					this.TrenutniNivoIgraca = result.Get<int>("MaxLevel");
					return;
				}
				if (!t.IsFaulted)
				{
					bool isCanceled = t.IsCanceled;
				}
			});
		}
	}

	// Token: 0x060025E7 RID: 9703 RVA: 0x001069CC File Offset: 0x00104BCC
	public void ProcitajScorovePrijatelja(string FriendID)
	{
		if (FB.IsLoggedIn)
		{
			ParseObject.GetQuery("LevelScore").WhereEqualTo("UserID", FriendID).FirstAsync().ContinueWith(delegate(Task<ParseObject> t)
			{
				if (t.IsCompleted)
				{
					ParseObject result = t.Result;
					IList<int> scores = result.Get<IList<int>>("LevelScore");
					IList<int> stars = result.Get<IList<int>>("NumOfStars");
					int maxLevel = result.Get<int>("MaxLevel");
					FacebookManager.bonusLevels = result.Get<string>("BonusLevels");
					FacebookManager.StrukturaPrijatelja item = new FacebookManager.StrukturaPrijatelja
					{
						PrijateljID = FriendID
					};
					item.MaxLevel = maxLevel;
					item.scores = scores;
					item.stars = stars;
					FacebookManager.ListaStructPrijatelja.Add(item);
					this.WaitForFacebookFriend = true;
					return;
				}
				if (t.IsFaulted || t.IsCanceled)
				{
					StagesParser.Instance.UgasiLoading();
				}
			});
		}
	}

	// Token: 0x060025E8 RID: 9704 RVA: 0x00106A25 File Offset: 0x00104C25
	private IEnumerator TrenutniNivoSvihPrijatelja()
	{
		int i = 0;
		float timer = 0f;
		while (i < this.Korisnici.Count)
		{
			this.WaitForFacebookFriend = false;
			this.ProcitajScorovePrijatelja(this.Korisnici[i]);
			while (!this.WaitForFacebookFriend && FacebookManager.Ulogovan)
			{
				if (timer > 20f)
				{
					this.WaitForFacebookFriend = true;
				}
				timer += Time.deltaTime;
				yield return null;
			}
			int num = i;
			i = num + 1;
		}
		if (this.resetovanScoreNaNulu == 1)
		{
			for (int j = 0; j < FacebookManager.ListaStructPrijatelja.Count; j++)
			{
				if (FacebookManager.ListaStructPrijatelja[j].PrijateljID == FB.UserId)
				{
					for (int k = 0; k < this.scorePoNivouPrijatelja.Length; k++)
					{
						FacebookManager.ListaStructPrijatelja[j].scores[k] = 0;
					}
				}
			}
			this.resetovanScoreNaNulu = 0;
			StagesParser.Instance.UgasiLoading();
		}
		for (int l = 0; l < FacebookManager.ListaStructPrijatelja.Count; l++)
		{
			if (FacebookManager.ListaStructPrijatelja[l].PrijateljID == FacebookManager.User)
			{
				FacebookManager.indexUListaStructPrijatelja = l;
			}
		}
		if (FacebookManager.Ulogovan)
		{
			if (!this.nePostojiKorisnik)
			{
				StagesParser.Instance.CompareScores();
			}
			else if (this.zavrsioUcitavanje)
			{
				StagesParser.Instance.UgasiLoading();
				PlayerPrefs.SetInt("Logovan", 1);
				if (FacebookManager.MestoPozivanjaLogina == 1)
				{
					this.RefreshujScenu1PosleLogina();
				}
				else if (FacebookManager.MestoPozivanjaLogina == 2)
				{
					this.RefreshujScenu2PosleLogina();
				}
				else if (FacebookManager.MestoPozivanjaLogina == 3)
				{
					this.RefreshujScenu3PosleLogina();
				}
				this.zavrsioUcitavanje = false;
			}
			else
			{
				this.zavrsioUcitavanje = true;
			}
		}
		else
		{
			StagesParser.Instance.UgasiLoading();
		}
		yield break;
	}

	// Token: 0x04001E60 RID: 7776
	public static string UserSveKupovineHats;

	// Token: 0x04001E61 RID: 7777
	public static string UserSveKupovineShirts;

	// Token: 0x04001E62 RID: 7778
	public static string UserSveKupovineBackPacks;

	// Token: 0x04001E63 RID: 7779
	public static int UserCoins;

	// Token: 0x04001E64 RID: 7780
	public static string bonusLevels;

	// Token: 0x04001E65 RID: 7781
	public static int UserScore;

	// Token: 0x04001E66 RID: 7782
	public static string UserLanguage;

	// Token: 0x04001E67 RID: 7783
	public static int UserBanana;

	// Token: 0x04001E68 RID: 7784
	public static int UserPowerMagnet;

	// Token: 0x04001E69 RID: 7785
	public static int UserPowerShield;

	// Token: 0x04001E6A RID: 7786
	public static int UserPowerDoubleCoins;

	// Token: 0x04001E6B RID: 7787
	public static int GlavaItem;

	// Token: 0x04001E6C RID: 7788
	public static int TeloItem;

	// Token: 0x04001E6D RID: 7789
	public static int LedjaItem;

	// Token: 0x04001E6E RID: 7790
	public static bool Usi;

	// Token: 0x04001E6F RID: 7791
	public static bool Kosa;

	// Token: 0x04001E70 RID: 7792
	public static int indexUListaStructPrijatelja;

	// Token: 0x04001E71 RID: 7793
	public static int MestoPozivanjaLogina = 1;

	// Token: 0x04001E72 RID: 7794
	public static bool KorisnikoviPodaciSpremni = false;

	// Token: 0x04001E73 RID: 7795
	private bool isInit;

	// Token: 0x04001E74 RID: 7796
	public static FacebookManager FacebookObject;

	// Token: 0x04001E75 RID: 7797
	private Texture2D lastResponseTexture;

	// Token: 0x04001E76 RID: 7798
	private string ApiQuery = "";

	// Token: 0x04001E77 RID: 7799
	public static bool Ulogovan;

	// Token: 0x04001E78 RID: 7800
	public static string stranica = string.Empty;

	// Token: 0x04001E79 RID: 7801
	private static bool otisaoDaLajkuje = false;

	// Token: 0x04001E7A RID: 7802
	public static string IDstranice = string.Empty;

	// Token: 0x04001E7B RID: 7803
	private static bool lajkovao = false;

	// Token: 0x04001E7C RID: 7804
	private static int nagrada = 0;

	// Token: 0x04001E7D RID: 7805
	private DateTime timeToShowNextElement;

	// Token: 0x04001E7E RID: 7806
	private bool leftApp;

	// Token: 0x04001E7F RID: 7807
	public static string lokacijaProvere = "Shop";

	// Token: 0x04001E80 RID: 7808
	public static string User;

	// Token: 0x04001E81 RID: 7809
	private string UserRodjendan;

	// Token: 0x04001E82 RID: 7810
	private List<string> Prijatelji;

	// Token: 0x04001E83 RID: 7811
	public static List<FacebookManager.IDiSlika> ProfileSlikePrijatelja = new List<FacebookManager.IDiSlika>();

	// Token: 0x04001E84 RID: 7812
	public List<string> Korisnici;

	// Token: 0x04001E85 RID: 7813
	public List<string> Scorovi;

	// Token: 0x04001E86 RID: 7814
	public List<string> Imena;

	// Token: 0x04001E87 RID: 7815
	public static int NumberOfFriends;

	// Token: 0x04001E88 RID: 7816
	public bool odobrioPublishActions;

	// Token: 0x04001E89 RID: 7817
	public int scoreToSet;

	// Token: 0x04001E8A RID: 7818
	public bool nePostojiKorisnik = true;

	// Token: 0x04001E8B RID: 7819
	public bool zavrsioUcitavanje;

	// Token: 0x04001E8C RID: 7820
	public static string[] permisija;

	// Token: 0x04001E8D RID: 7821
	public static string[] statusPermisije;

	// Token: 0x04001E8E RID: 7822
	public static List<FacebookManager.StrukturaPrijatelja> ListaStructPrijatelja = new List<FacebookManager.StrukturaPrijatelja>();

	// Token: 0x04001E8F RID: 7823
	private int TrenutniNivoIgraca;

	// Token: 0x04001E90 RID: 7824
	private int[] scorePoNivouPrijatelja = new int[120];

	// Token: 0x04001E91 RID: 7825
	private int[] ScorePoNivoimaNiz = new int[120];

	// Token: 0x04001E92 RID: 7826
	private int[] BrojZvezdaPoNivouNiz = new int[120];

	// Token: 0x04001E93 RID: 7827
	private int[] testNiz = new int[120];

	// Token: 0x04001E94 RID: 7828
	private bool WaitForFacebook;

	// Token: 0x04001E95 RID: 7829
	private bool WaitForFacebookFriend;

	// Token: 0x04001E96 RID: 7830
	public static string UserName;

	// Token: 0x04001E97 RID: 7831
	public static Texture ProfilePicture;

	// Token: 0x04001E98 RID: 7832
	public static Texture FriendPic1;

	// Token: 0x04001E99 RID: 7833
	public static Texture FriendPic2;

	// Token: 0x04001E9A RID: 7834
	public static Texture FriendPic3;

	// Token: 0x04001E9B RID: 7835
	public static Texture FriendPic4;

	// Token: 0x04001E9C RID: 7836
	public static Texture FriendPic5;

	// Token: 0x04001E9D RID: 7837
	private string permissions = "user_friends,publish_actions";

	// Token: 0x04001E9E RID: 7838
	private string Code;

	// Token: 0x04001E9F RID: 7839
	private string TipNagrade;

	// Token: 0x04001EA0 RID: 7840
	private int IznosNagrade;

	// Token: 0x04001EA1 RID: 7841
	private ParseObject Korisnik = new ParseObject("User");

	// Token: 0x04001EA2 RID: 7842
	private ParseObject LevelScore = new ParseObject("LevelScore");

	// Token: 0x04001EA3 RID: 7843
	private string JezikServer;

	// Token: 0x04001EA4 RID: 7844
	private int NivoServer;

	// Token: 0x04001EA5 RID: 7845
	private bool updatedSuccessfullyScoreNaNivoima;

	// Token: 0x04001EA6 RID: 7846
	private bool updatedSuccessfullyPodaciKorisnika;

	// Token: 0x04001EA7 RID: 7847
	[HideInInspector]
	public bool OKzaLogout;

	// Token: 0x04001EA8 RID: 7848
	[HideInInspector]
	public int resetovanScoreNaNulu;

	// Token: 0x04001EA9 RID: 7849
	private string FriendSelectorTitle = "Banana Island - Bobo's Epic Tale";

	// Token: 0x04001EAA RID: 7850
	private string FriendSelectorMessage = LanguageManager.Play + " \"Banana Island - Bobo's Epic Tale\"";

	// Token: 0x04001EAB RID: 7851
	private string FriendSelectorFilters = "[\"all\",\"app_users\",\"app_non_users\"]";

	// Token: 0x04001EAC RID: 7852
	private string FriendSelectorData = "{}";

	// Token: 0x04001EAD RID: 7853
	private string FriendSelectorExcludeIds = "";

	// Token: 0x04001EAE RID: 7854
	private string FriendSelectorMax = "";

	// Token: 0x04001EAF RID: 7855
	private string lastResponse = "";

	// Token: 0x04001EB0 RID: 7856
	private string FeedLinkKratakOpis = " ";

	// Token: 0x04001EB1 RID: 7857
	private string LinkSlike = "https://trello-attachments.s3.amazonaws.com/52fb9e010aaea80557f83f1f/1024x500/d64a4cc319932dde0630258aee32d7d4/Feature-Graphic.jpg";

	// Token: 0x04001EB2 RID: 7858
	private string LinkVideailiZvuka = "";

	// Token: 0x04001EB3 RID: 7859
	private string FeedActionName = "";

	// Token: 0x04001EB4 RID: 7860
	private string FeedActionLink = "";

	// Token: 0x04001EB5 RID: 7861
	private string FeedReference = "";

	// Token: 0x04001EB6 RID: 7862
	private bool IncludeFeedProperties;

	// Token: 0x04001EB7 RID: 7863
	private Dictionary<string, string[]> FeedProperties = new Dictionary<string, string[]>();

	// Token: 0x04001EB8 RID: 7864
	private int nesto;

	// Token: 0x04001EB9 RID: 7865
	public int BrojPrijatelja;

	// Token: 0x04001EBA RID: 7866
	private string URL;

	// Token: 0x04001EBB RID: 7867
	private string prijateljevIDzaSliku;

	// Token: 0x020013CA RID: 5066
	public struct IDiSlika
	{
		// Token: 0x04006968 RID: 26984
		public string PrijateljID;

		// Token: 0x04006969 RID: 26985
		public Texture profilePicture;
	}

	// Token: 0x020013CB RID: 5067
	public struct StrukturaPrijatelja
	{
		// Token: 0x0400696A RID: 26986
		public string PrijateljID;

		// Token: 0x0400696B RID: 26987
		public IList<int> scores;

		// Token: 0x0400696C RID: 26988
		public IList<int> stars;

		// Token: 0x0400696D RID: 26989
		public int MaxLevel;

		// Token: 0x0400696E RID: 26990
		public Texture profilePicture;
	}
}
