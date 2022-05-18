using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks2;
using Facebook;
using Facebook.MiniJSON;
using Parse;
using UnityEngine;

// Token: 0x0200068C RID: 1676
public class FacebookManager : MonoBehaviour
{
	// Token: 0x060029DB RID: 10715 RVA: 0x00020886 File Offset: 0x0001EA86
	private void Awake()
	{
		FacebookManager.FacebookObject = this;
		Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x060029DC RID: 10716 RVA: 0x00144044 File Offset: 0x00142244
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

	// Token: 0x060029DD RID: 10717 RVA: 0x00020899 File Offset: 0x0001EA99
	public static void FacebookLogout()
	{
		if (FB.IsLoggedIn)
		{
			FB.Logout();
			FacebookManager.Ulogovan = false;
			StagesParser.Instance.ObrisiProgresNaLogOut();
		}
	}

	// Token: 0x060029DE RID: 10718 RVA: 0x000208B7 File Offset: 0x0001EAB7
	private void OnInitComplete()
	{
		this.isInit = true;
	}

	// Token: 0x060029DF RID: 10719 RVA: 0x0000FC3C File Offset: 0x0000DE3C
	private void OnHideUnity(bool isGameShown)
	{
		Debug.Log("Is game showing? " + isGameShown.ToString());
	}

	// Token: 0x060029E0 RID: 10720 RVA: 0x000208C0 File Offset: 0x0001EAC0
	public void FacebookLogin()
	{
		FB.Login(this.permissions, new FacebookDelegate(this.LoginCallback));
	}

	// Token: 0x060029E1 RID: 10721 RVA: 0x00144130 File Offset: 0x00142330
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

	// Token: 0x060029E2 RID: 10722 RVA: 0x000208D9 File Offset: 0x0001EAD9
	public void RefreshujScenu1PosleLogina()
	{
		MainScene.FacebookLogIn.SetActive(false);
		MainScene.LeaderBoardInvite.SetActive(true);
	}

	// Token: 0x060029E3 RID: 10723 RVA: 0x000208F1 File Offset: 0x0001EAF1
	public void RefreshujScenu2PosleLogina()
	{
		GameObject.Find("FB Login").SetActive(false);
		AllMapsManageFull.makniPopup = 0;
	}

	// Token: 0x060029E4 RID: 10724 RVA: 0x00020909 File Offset: 0x0001EB09
	public void RefreshujScenu3PosleLogina()
	{
		GameObject.Find("FB Login").SetActive(false);
		KameraMovement.makniPopup = 0;
	}

	// Token: 0x060029E5 RID: 10725 RVA: 0x00144318 File Offset: 0x00142518
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

	// Token: 0x060029E6 RID: 10726 RVA: 0x001443E0 File Offset: 0x001425E0
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

	// Token: 0x060029E7 RID: 10727 RVA: 0x00020921 File Offset: 0x0001EB21
	public void GetProfilePicture()
	{
		FB.API("/" + FacebookManager.User + "/picture?redirect=true&height=64&type=normal&width=64", HttpMethod.GET, new FacebookDelegate(this.MyPictureCallback), null);
	}

	// Token: 0x060029E8 RID: 10728 RVA: 0x0002094E File Offset: 0x0001EB4E
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

	// Token: 0x060029E9 RID: 10729 RVA: 0x0002098C File Offset: 0x0001EB8C
	public void GetFacebookName()
	{
		FB.API("me?fields=id,name", HttpMethod.GET, new FacebookDelegate(this.GetFacebookNameCallback), null);
	}

	// Token: 0x060029EA RID: 10730 RVA: 0x000209AA File Offset: 0x0001EBAA
	public void GetFacebookNameCallback(FBResult result)
	{
		if (result.Error != null)
		{
			StagesParser.Instance.UgasiLoading();
			return;
		}
		FacebookManager.UserName = (Json.Deserialize(result.Text) as Dictionary<string, object>)["name"].ToString();
	}

	// Token: 0x060029EB RID: 10731 RVA: 0x001448AC File Offset: 0x00142AAC
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

	// Token: 0x060029EC RID: 10732 RVA: 0x00144968 File Offset: 0x00142B68
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

	// Token: 0x060029ED RID: 10733 RVA: 0x00144A08 File Offset: 0x00142C08
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

	// Token: 0x060029EE RID: 10734 RVA: 0x000042DD File Offset: 0x000024DD
	private void FeedCallback(FBResult result)
	{
	}

	// Token: 0x060029EF RID: 10735 RVA: 0x000209E3 File Offset: 0x0001EBE3
	public void ProveriPermisije()
	{
		FB.API("/me/permissions", HttpMethod.GET, new FacebookDelegate(this.MyPermissionsCallback), null);
	}

	// Token: 0x060029F0 RID: 10736 RVA: 0x00144AA0 File Offset: 0x00142CA0
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

	// Token: 0x060029F1 RID: 10737 RVA: 0x00020A01 File Offset: 0x0001EC01
	public void proveraPublish_ActionPermisije()
	{
		FB.API("/me/permissions", HttpMethod.GET, new FacebookDelegate(this.Publish_ActionsCallback), null);
	}

	// Token: 0x060029F2 RID: 10738 RVA: 0x00144C50 File Offset: 0x00142E50
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

	// Token: 0x060029F3 RID: 10739 RVA: 0x00020A1F File Offset: 0x0001EC1F
	public void GetRodjendan()
	{
		FB.API("/me?fields=birthday", HttpMethod.GET, new FacebookDelegate(this.MyBirthdayCallback), null);
	}

	// Token: 0x060029F4 RID: 10740 RVA: 0x00144D58 File Offset: 0x00142F58
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

	// Token: 0x060029F5 RID: 10741 RVA: 0x00144DA0 File Offset: 0x00142FA0
	public void SetFacebookHighScore(int trenutniScore)
	{
		if (FB.IsLoggedIn)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["score"] = trenutniScore.ToString();
			FB.API("/me/scores?", HttpMethod.POST, new FacebookDelegate(this.SetFacebookScoreCallback), dictionary);
		}
	}

	// Token: 0x060029F6 RID: 10742 RVA: 0x00020A3D File Offset: 0x0001EC3D
	public void SetFacebookScoreCallback(FBResult result)
	{
		if (result.Error == null && this.resetovanScoreNaNulu == 2)
		{
			this.resetovanScoreNaNulu = 1;
		}
	}

	// Token: 0x060029F7 RID: 10743 RVA: 0x00020A57 File Offset: 0x0001EC57
	public void GetFacebookHighScore()
	{
		FB.API("me?fields=scores", HttpMethod.GET, new FacebookDelegate(this.GetFacebookHighScoreCallback), null);
	}

	// Token: 0x060029F8 RID: 10744 RVA: 0x00144DE8 File Offset: 0x00142FE8
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

	// Token: 0x060029F9 RID: 10745 RVA: 0x00020A75 File Offset: 0x0001EC75
	public void GetFacebookFriendScores()
	{
		FB.API("1609658469261083/scores", HttpMethod.GET, new FacebookDelegate(this.GetFacebookFriendScoresCallback), null);
	}

	// Token: 0x060029FA RID: 10746 RVA: 0x00144E58 File Offset: 0x00143058
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

	// Token: 0x060029FB RID: 10747 RVA: 0x00020A93 File Offset: 0x0001EC93
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

	// Token: 0x060029FC RID: 10748 RVA: 0x00020AA2 File Offset: 0x0001ECA2
	public void SpisakSvihFacebookPrijatelja()
	{
		FB.API("/fql?q=SELECT+uid,name,pic_square+FROM+user+WHERE+is_app_user=1+AND+uid+IN+(SELECT+uid2+FROM+friend+WHERE+uid1=me())", HttpMethod.GET, new FacebookDelegate(this.SviPrijateljiFacebookCallback), null);
	}

	// Token: 0x060029FD RID: 10749 RVA: 0x00145220 File Offset: 0x00143420
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

	// Token: 0x060029FE RID: 10750 RVA: 0x00020AC0 File Offset: 0x0001ECC0
	public void GetFacebookFriendPicture(string PrijateljevID)
	{
		if (FacebookManager.Ulogovan)
		{
			this.prijateljevIDzaSliku = PrijateljevID;
			FB.API(PrijateljevID + "/picture?redirect=true&height=64&type=normal&width=64", HttpMethod.GET, new FacebookDelegate(this.FacebookFriendPictureCallback), null);
		}
	}

	// Token: 0x060029FF RID: 10751 RVA: 0x001452D4 File Offset: 0x001434D4
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

	// Token: 0x06002A00 RID: 10752 RVA: 0x00020AF2 File Offset: 0x0001ECF2
	public void GetFacebookGameAchievements()
	{
		FB.API(FB.AppId + "/achievements", HttpMethod.GET, new FacebookDelegate(this.GetFacebookGameAchievementsCallback), null);
	}

	// Token: 0x06002A01 RID: 10753 RVA: 0x001455B4 File Offset: 0x001437B4
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

	// Token: 0x06002A02 RID: 10754 RVA: 0x00145620 File Offset: 0x00143820
	public void DodajFacebookAchievement(string URLAchivmenta)
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary["achievement"] = URLAchivmenta;
		FB.API("me/achievements", HttpMethod.POST, new FacebookDelegate(this.DodajFacebookAchievementCallback), dictionary);
	}

	// Token: 0x06002A03 RID: 10755 RVA: 0x00020B1A File Offset: 0x0001ED1A
	public void DodajFacebookAchievementCallback(FBResult result)
	{
		string error = result.Error;
	}

	// Token: 0x06002A04 RID: 10756 RVA: 0x00020B23 File Offset: 0x0001ED23
	public void ObrisiFacebookAchievement(string UrlACH)
	{
		FB.API("me/achievements?achievement=" + UrlACH, HttpMethod.DELETE, new FacebookDelegate(this.ObrisiFacebookAchievementCallback), null);
	}

	// Token: 0x06002A05 RID: 10757 RVA: 0x00020B1A File Offset: 0x0001ED1A
	public void ObrisiFacebookAchievementCallback(FBResult result)
	{
		string error = result.Error;
	}

	// Token: 0x06002A06 RID: 10758 RVA: 0x00020B47 File Offset: 0x0001ED47
	public void ProveraFacebookAchievmenta()
	{
		FB.API("me/achievements", HttpMethod.GET, new FacebookDelegate(this.ProveraAchievmentaCallback), null);
	}

	// Token: 0x06002A07 RID: 10759 RVA: 0x00020B1A File Offset: 0x0001ED1A
	public void ProveraAchievmentaCallback(FBResult result)
	{
		string error = result.Error;
	}

	// Token: 0x06002A08 RID: 10760 RVA: 0x0014565C File Offset: 0x0014385C
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

	// Token: 0x06002A09 RID: 10761 RVA: 0x00145828 File Offset: 0x00143A28
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

	// Token: 0x06002A0A RID: 10762 RVA: 0x00020B65 File Offset: 0x0001ED65
	public void ProveriKorisnika()
	{
		FacebookManager.ListaStructPrijatelja.Clear();
		base.StartCoroutine("DaLiPostojiKorisnik");
	}

	// Token: 0x06002A0B RID: 10763 RVA: 0x00020B7D File Offset: 0x0001ED7D
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

	// Token: 0x06002A0C RID: 10764 RVA: 0x00145928 File Offset: 0x00143B28
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

	// Token: 0x06002A0D RID: 10765 RVA: 0x00020B8C File Offset: 0x0001ED8C
	private void SkloniCoinsReward()
	{
		GameObject.Find("CoinsReward").GetComponent<Animation>().Play("CoinsRewardOdlazak");
	}

	// Token: 0x06002A0E RID: 10766 RVA: 0x001459F8 File Offset: 0x00143BF8
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

	// Token: 0x06002A0F RID: 10767 RVA: 0x00145A4C File Offset: 0x00143C4C
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

	// Token: 0x06002A10 RID: 10768 RVA: 0x00145AA8 File Offset: 0x00143CA8
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

	// Token: 0x06002A11 RID: 10769 RVA: 0x00145AFC File Offset: 0x00143CFC
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

	// Token: 0x06002A12 RID: 10770 RVA: 0x00145B58 File Offset: 0x00143D58
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

	// Token: 0x06002A13 RID: 10771 RVA: 0x00145BAC File Offset: 0x00143DAC
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

	// Token: 0x06002A14 RID: 10772 RVA: 0x00145C08 File Offset: 0x00143E08
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

	// Token: 0x06002A15 RID: 10773 RVA: 0x00145C5C File Offset: 0x00143E5C
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

	// Token: 0x06002A16 RID: 10774 RVA: 0x00145CB8 File Offset: 0x00143EB8
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

	// Token: 0x06002A17 RID: 10775 RVA: 0x00145D0C File Offset: 0x00143F0C
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

	// Token: 0x06002A18 RID: 10776 RVA: 0x00145D68 File Offset: 0x00143F68
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

	// Token: 0x06002A19 RID: 10777 RVA: 0x00145DBC File Offset: 0x00143FBC
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

	// Token: 0x06002A1A RID: 10778 RVA: 0x00145E18 File Offset: 0x00144018
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

	// Token: 0x06002A1B RID: 10779 RVA: 0x00145E6C File Offset: 0x0014406C
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

	// Token: 0x06002A1C RID: 10780 RVA: 0x00145EC8 File Offset: 0x001440C8
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

	// Token: 0x06002A1D RID: 10781 RVA: 0x00145F2C File Offset: 0x0014412C
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

	// Token: 0x06002A1E RID: 10782 RVA: 0x00145F88 File Offset: 0x00144188
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

	// Token: 0x06002A1F RID: 10783 RVA: 0x00145FEC File Offset: 0x001441EC
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

	// Token: 0x06002A20 RID: 10784 RVA: 0x00146048 File Offset: 0x00144248
	public void InicijalizujScoreNaNivoima(int[] NizBrojZvezdaPoNivou, int[] NizScorovaPoNivoima, int MaxNivo, string BonusLevels)
	{
		this.LevelScore["NumOfStars"] = NizBrojZvezdaPoNivou;
		this.LevelScore["LevelScore"] = NizScorovaPoNivoima;
		this.LevelScore["MaxLevel"] = MaxNivo;
		this.LevelScore["BonusLevels"] = BonusLevels;
		this.LevelScore["UserID"] = FacebookManager.User;
	}

	// Token: 0x06002A21 RID: 10785 RVA: 0x001460B4 File Offset: 0x001442B4
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

	// Token: 0x06002A22 RID: 10786 RVA: 0x00020BA8 File Offset: 0x0001EDA8
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

	// Token: 0x06002A23 RID: 10787 RVA: 0x0014614C File Offset: 0x0014434C
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

	// Token: 0x06002A24 RID: 10788 RVA: 0x00020BE1 File Offset: 0x0001EDE1
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

	// Token: 0x04002386 RID: 9094
	public static string UserSveKupovineHats;

	// Token: 0x04002387 RID: 9095
	public static string UserSveKupovineShirts;

	// Token: 0x04002388 RID: 9096
	public static string UserSveKupovineBackPacks;

	// Token: 0x04002389 RID: 9097
	public static int UserCoins;

	// Token: 0x0400238A RID: 9098
	public static string bonusLevels;

	// Token: 0x0400238B RID: 9099
	public static int UserScore;

	// Token: 0x0400238C RID: 9100
	public static string UserLanguage;

	// Token: 0x0400238D RID: 9101
	public static int UserBanana;

	// Token: 0x0400238E RID: 9102
	public static int UserPowerMagnet;

	// Token: 0x0400238F RID: 9103
	public static int UserPowerShield;

	// Token: 0x04002390 RID: 9104
	public static int UserPowerDoubleCoins;

	// Token: 0x04002391 RID: 9105
	public static int GlavaItem;

	// Token: 0x04002392 RID: 9106
	public static int TeloItem;

	// Token: 0x04002393 RID: 9107
	public static int LedjaItem;

	// Token: 0x04002394 RID: 9108
	public static bool Usi;

	// Token: 0x04002395 RID: 9109
	public static bool Kosa;

	// Token: 0x04002396 RID: 9110
	public static int indexUListaStructPrijatelja;

	// Token: 0x04002397 RID: 9111
	public static int MestoPozivanjaLogina = 1;

	// Token: 0x04002398 RID: 9112
	public static bool KorisnikoviPodaciSpremni = false;

	// Token: 0x04002399 RID: 9113
	private bool isInit;

	// Token: 0x0400239A RID: 9114
	public static FacebookManager FacebookObject;

	// Token: 0x0400239B RID: 9115
	private Texture2D lastResponseTexture;

	// Token: 0x0400239C RID: 9116
	private string ApiQuery = "";

	// Token: 0x0400239D RID: 9117
	public static bool Ulogovan;

	// Token: 0x0400239E RID: 9118
	public static string stranica = string.Empty;

	// Token: 0x0400239F RID: 9119
	private static bool otisaoDaLajkuje = false;

	// Token: 0x040023A0 RID: 9120
	public static string IDstranice = string.Empty;

	// Token: 0x040023A1 RID: 9121
	private static bool lajkovao = false;

	// Token: 0x040023A2 RID: 9122
	private static int nagrada = 0;

	// Token: 0x040023A3 RID: 9123
	private DateTime timeToShowNextElement;

	// Token: 0x040023A4 RID: 9124
	private bool leftApp;

	// Token: 0x040023A5 RID: 9125
	public static string lokacijaProvere = "Shop";

	// Token: 0x040023A6 RID: 9126
	public static string User;

	// Token: 0x040023A7 RID: 9127
	private string UserRodjendan;

	// Token: 0x040023A8 RID: 9128
	private List<string> Prijatelji;

	// Token: 0x040023A9 RID: 9129
	public static List<FacebookManager.IDiSlika> ProfileSlikePrijatelja = new List<FacebookManager.IDiSlika>();

	// Token: 0x040023AA RID: 9130
	public List<string> Korisnici;

	// Token: 0x040023AB RID: 9131
	public List<string> Scorovi;

	// Token: 0x040023AC RID: 9132
	public List<string> Imena;

	// Token: 0x040023AD RID: 9133
	public static int NumberOfFriends;

	// Token: 0x040023AE RID: 9134
	public bool odobrioPublishActions;

	// Token: 0x040023AF RID: 9135
	public int scoreToSet;

	// Token: 0x040023B0 RID: 9136
	public bool nePostojiKorisnik = true;

	// Token: 0x040023B1 RID: 9137
	public bool zavrsioUcitavanje;

	// Token: 0x040023B2 RID: 9138
	public static string[] permisija;

	// Token: 0x040023B3 RID: 9139
	public static string[] statusPermisije;

	// Token: 0x040023B4 RID: 9140
	public static List<FacebookManager.StrukturaPrijatelja> ListaStructPrijatelja = new List<FacebookManager.StrukturaPrijatelja>();

	// Token: 0x040023B5 RID: 9141
	private int TrenutniNivoIgraca;

	// Token: 0x040023B6 RID: 9142
	private int[] scorePoNivouPrijatelja = new int[120];

	// Token: 0x040023B7 RID: 9143
	private int[] ScorePoNivoimaNiz = new int[120];

	// Token: 0x040023B8 RID: 9144
	private int[] BrojZvezdaPoNivouNiz = new int[120];

	// Token: 0x040023B9 RID: 9145
	private int[] testNiz = new int[120];

	// Token: 0x040023BA RID: 9146
	private bool WaitForFacebook;

	// Token: 0x040023BB RID: 9147
	private bool WaitForFacebookFriend;

	// Token: 0x040023BC RID: 9148
	public static string UserName;

	// Token: 0x040023BD RID: 9149
	public static Texture ProfilePicture;

	// Token: 0x040023BE RID: 9150
	public static Texture FriendPic1;

	// Token: 0x040023BF RID: 9151
	public static Texture FriendPic2;

	// Token: 0x040023C0 RID: 9152
	public static Texture FriendPic3;

	// Token: 0x040023C1 RID: 9153
	public static Texture FriendPic4;

	// Token: 0x040023C2 RID: 9154
	public static Texture FriendPic5;

	// Token: 0x040023C3 RID: 9155
	private string permissions = "user_friends,publish_actions";

	// Token: 0x040023C4 RID: 9156
	private string Code;

	// Token: 0x040023C5 RID: 9157
	private string TipNagrade;

	// Token: 0x040023C6 RID: 9158
	private int IznosNagrade;

	// Token: 0x040023C7 RID: 9159
	private ParseObject Korisnik = new ParseObject("User");

	// Token: 0x040023C8 RID: 9160
	private ParseObject LevelScore = new ParseObject("LevelScore");

	// Token: 0x040023C9 RID: 9161
	private string JezikServer;

	// Token: 0x040023CA RID: 9162
	private int NivoServer;

	// Token: 0x040023CB RID: 9163
	private bool updatedSuccessfullyScoreNaNivoima;

	// Token: 0x040023CC RID: 9164
	private bool updatedSuccessfullyPodaciKorisnika;

	// Token: 0x040023CD RID: 9165
	[HideInInspector]
	public bool OKzaLogout;

	// Token: 0x040023CE RID: 9166
	[HideInInspector]
	public int resetovanScoreNaNulu;

	// Token: 0x040023CF RID: 9167
	private string FriendSelectorTitle = "Banana Island - Bobo's Epic Tale";

	// Token: 0x040023D0 RID: 9168
	private string FriendSelectorMessage = LanguageManager.Play + " \"Banana Island - Bobo's Epic Tale\"";

	// Token: 0x040023D1 RID: 9169
	private string FriendSelectorFilters = "[\"all\",\"app_users\",\"app_non_users\"]";

	// Token: 0x040023D2 RID: 9170
	private string FriendSelectorData = "{}";

	// Token: 0x040023D3 RID: 9171
	private string FriendSelectorExcludeIds = "";

	// Token: 0x040023D4 RID: 9172
	private string FriendSelectorMax = "";

	// Token: 0x040023D5 RID: 9173
	private string lastResponse = "";

	// Token: 0x040023D6 RID: 9174
	private string FeedLinkKratakOpis = " ";

	// Token: 0x040023D7 RID: 9175
	private string LinkSlike = "https://trello-attachments.s3.amazonaws.com/52fb9e010aaea80557f83f1f/1024x500/d64a4cc319932dde0630258aee32d7d4/Feature-Graphic.jpg";

	// Token: 0x040023D8 RID: 9176
	private string LinkVideailiZvuka = "";

	// Token: 0x040023D9 RID: 9177
	private string FeedActionName = "";

	// Token: 0x040023DA RID: 9178
	private string FeedActionLink = "";

	// Token: 0x040023DB RID: 9179
	private string FeedReference = "";

	// Token: 0x040023DC RID: 9180
	private bool IncludeFeedProperties;

	// Token: 0x040023DD RID: 9181
	private Dictionary<string, string[]> FeedProperties = new Dictionary<string, string[]>();

	// Token: 0x040023DE RID: 9182
	private int nesto;

	// Token: 0x040023DF RID: 9183
	public int BrojPrijatelja;

	// Token: 0x040023E0 RID: 9184
	private string URL;

	// Token: 0x040023E1 RID: 9185
	private string prijateljevIDzaSliku;

	// Token: 0x0200068D RID: 1677
	public struct IDiSlika
	{
		// Token: 0x040023E2 RID: 9186
		public string PrijateljID;

		// Token: 0x040023E3 RID: 9187
		public Texture profilePicture;
	}

	// Token: 0x0200068E RID: 1678
	public struct StrukturaPrijatelja
	{
		// Token: 0x040023E4 RID: 9188
		public string PrijateljID;

		// Token: 0x040023E5 RID: 9189
		public IList<int> scores;

		// Token: 0x040023E6 RID: 9190
		public IList<int> stars;

		// Token: 0x040023E7 RID: 9191
		public int MaxLevel;

		// Token: 0x040023E8 RID: 9192
		public Texture profilePicture;
	}
}
