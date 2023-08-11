using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using KBEngine;
using UnityEngine;
using UnityEngine.Advertisements;

public class MainScene : MonoBehaviour
{
	public Transform[] FriendsObjects = (Transform[])(object)new Transform[0];

	public Transform[] SettingsObjects = (Transform[])(object)new Transform[0];

	public Transform[] LanguagesObjects = (Transform[])(object)new Transform[0];

	public static GameObject LeaderBoardInvite;

	public static GameObject FacebookLogIn;

	private bool SettingsOtvoren;

	private bool LeaderboardOtvoren;

	private int SettingState = 1;

	private string releasedItem;

	private string clickedItem;

	private Vector3 originalScale;

	private GameObject temp;

	private DateTimeFormatInfo format;

	private int selectedLanguage;

	private int dailyReward;

	private string jezikPreUlaskaUPromenuJezika;

	private bool logoutKliknut;

	private void Start()
	{
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		if (Advertisement.isSupported)
		{
			Advertisement.Initialize(StagesParser.Instance.UnityAdsVideoGameID);
		}
		else
		{
			Debug.Log((object)"UNITYADS Platform not supported");
		}
		((MonoBehaviour)this).StartCoroutine(checkConnectionForAutologin());
		if ((Object)(object)Loading.Instance != (Object)null)
		{
			((MonoBehaviour)this).StartCoroutine(Loading.Instance.UcitanaScena(Camera.main, 4, 0.25f));
		}
		LeaderBoardInvite = GameObject.Find("Leaderboard Scena/FB Invite");
		((Component)LeaderBoardInvite.transform.Find("Text/Number")).GetComponent<TextMesh>().text = "+" + StagesParser.InviteReward;
		((Component)LeaderBoardInvite.transform.Find("Text/Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: false);
		((Component)((Component)ShopManagerFull.ShopObject).transform.Find("1 Free Coins/Free Coins Tabovi/ShopFCWatchVideo/Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMesh>().text = StagesParser.watchVideoReward.ToString();
		((Component)((Component)ShopManagerFull.ShopObject).transform.Find("1 Free Coins/Free Coins Tabovi/ShopFCWatchVideo/Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: false);
		FacebookLogIn = GameObject.Find("FB HOLDER LogIn");
		GameObject.Find("Gore Levo HOLDER Buttons").transform.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.zero).x, Camera.main.ViewportToWorldPoint(Vector3.one).y, 0f);
		GameObject.Find("Dole Desno HOLDER Buttons").transform.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.one).x, Camera.main.ViewportToWorldPoint(Vector3.zero).y, 0f);
		GameObject.Find("Dole Levo HOLDER Buttons").transform.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.zero).x, Camera.main.ViewportToWorldPoint(Vector3.zero).y, 0f);
		if (PlayerPrefs.HasKey("JezikPromenjen"))
		{
			switch (LanguageManager.chosenLanguage)
			{
			case "_en":
				PromeniZastavu(1);
				break;
			case "_us":
				PromeniZastavu(2);
				break;
			case "_es":
				PromeniZastavu(3);
				break;
			case "_ru":
				PromeniZastavu(4);
				break;
			case "_pt":
				PromeniZastavu(5);
				break;
			case "_br":
				PromeniZastavu(6);
				break;
			case "_fr":
				PromeniZastavu(7);
				break;
			case "_th":
				PromeniZastavu(8);
				break;
			case "_ch":
				PromeniZastavu(9);
				break;
			case "_tch":
				PromeniZastavu(10);
				break;
			case "_de":
				PromeniZastavu(11);
				break;
			case "_it":
				PromeniZastavu(12);
				break;
			case "_srb":
				PromeniZastavu(13);
				break;
			case "_tr":
				PromeniZastavu(14);
				break;
			case "_ko":
				PromeniZastavu(15);
				break;
			default:
				PromeniZastavu(0);
				break;
			}
		}
		if (PlaySounds.soundOn)
		{
			((Renderer)((Component)SettingsObjects[2].Find("Shop Tab Element Selected")).GetComponent<SpriteRenderer>()).enabled = false;
		}
		else
		{
			((Renderer)((Component)SettingsObjects[2].Find("Shop Tab Element Selected")).GetComponent<SpriteRenderer>()).enabled = true;
		}
		if (PlaySounds.musicOn)
		{
			((Renderer)((Component)SettingsObjects[1].Find("Shop Tab Element Selected")).GetComponent<SpriteRenderer>()).enabled = false;
			PlaySounds.Play_BackgroundMusic_Menu();
		}
		else
		{
			((Renderer)((Component)SettingsObjects[1].Find("Shop Tab Element Selected")).GetComponent<SpriteRenderer>()).enabled = true;
		}
		ShopManagerFull.AktivanTab = 0;
		if (FB.IsLoggedIn)
		{
			GameObject.Find("FB HOLDER LogIn").SetActive(false);
			LeaderBoardInvite.SetActive(true);
		}
		else
		{
			LeaderBoardInvite.SetActive(false);
			GameObject.Find("FB HOLDER LogIn").SetActive(true);
			for (int i = 0; i < 10; i++)
			{
				if (i == 1)
				{
					((Component)FriendsObjects[i].Find("FB Invite")).gameObject.SetActive(true);
					((Component)FriendsObjects[i].Find("Friend")).gameObject.SetActive(false);
					((Component)FriendsObjects[i].Find("FB Invite/Coin Shop")).gameObject.SetActive(false);
					((Component)FriendsObjects[i].Find("FB Invite/Invite")).GetComponent<TextMesh>().text = LanguageManager.LogIn;
					((Component)FriendsObjects[i].Find("FB Invite/Invite")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
					((Component)FriendsObjects[i].Find("FB Invite/Coin Number")).GetComponent<TextMesh>().text = "+" + StagesParser.LoginReward;
					((Component)FriendsObjects[i].Find("FB Invite/Coin Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: false);
				}
				else
				{
					((Component)FriendsObjects[i]).gameObject.SetActive(false);
				}
			}
		}
		if (!PlayerPrefs.HasKey("VecPokrenuto"))
		{
			GameObject.Find("FB HOLDER LogIn").SetActive(false);
			GameObject.Find("Dole Desno HOLDER Buttons").SetActive(false);
			GameObject.Find("Dole Levo HOLDER Buttons").SetActive(false);
			GameObject.Find("Zastava").GetComponent<Collider>().enabled = false;
		}
		if (StagesParser.obucenSeLogovaoNaDrugojSceni)
		{
			StagesParser.obucenSeLogovaoNaDrugojSceni = false;
			StagesParser.Instance.ShopDeoIzCompareScores();
		}
	}

	private IEnumerator checkConnectionForAutologin()
	{
		((MonoBehaviour)this).StartCoroutine(CheckInternetConnection.Instance.checkInternetConnection());
		while (!CheckInternetConnection.Instance.checkDone)
		{
			yield return null;
		}
		if (!CheckInternetConnection.Instance.internetOK || !PlayerPrefs.HasKey("Logovan"))
		{
			yield break;
		}
		if (PlayerPrefs.GetInt("Logovan") == 1)
		{
			if (!FacebookManager.Ulogovan)
			{
				FacebookManager.FacebookObject.FacebookLogin();
				yield break;
			}
			FacebookManager.MestoPozivanjaLogina = 1;
			FacebookManager.FacebookObject.BrojPrijatelja = 0;
			FacebookManager.FacebookObject.Korisnici.Clear();
			FacebookManager.FacebookObject.Scorovi.Clear();
			FacebookManager.FacebookObject.Imena.Clear();
			FacebookManager.ProfileSlikePrijatelja.Clear();
			FacebookManager.ListaStructPrijatelja.Clear();
			FacebookManager.FacebookObject.GetFacebookFriendScores();
		}
		else if (FB.IsLoggedIn)
		{
			if (!FacebookManager.Ulogovan)
			{
				FacebookManager.FacebookObject.FacebookLogin();
				yield break;
			}
			FacebookManager.MestoPozivanjaLogina = 1;
			FacebookManager.FacebookObject.BrojPrijatelja = 0;
			FacebookManager.FacebookObject.Korisnici.Clear();
			FacebookManager.FacebookObject.Scorovi.Clear();
			FacebookManager.FacebookObject.Imena.Clear();
			FacebookManager.ProfileSlikePrijatelja.Clear();
			FacebookManager.ListaStructPrijatelja.Clear();
			FacebookManager.FacebookObject.GetFacebookFriendScores();
		}
	}

	private IEnumerator checkConnectionForLoginButton()
	{
		((MonoBehaviour)this).StartCoroutine(CheckInternetConnection.Instance.checkInternetConnection());
		while (!CheckInternetConnection.Instance.checkDone)
		{
			yield return null;
		}
		if (CheckInternetConnection.Instance.internetOK)
		{
			if (!FB.IsLoggedIn)
			{
				FacebookManager.MestoPozivanjaLogina = 1;
				FacebookManager.FacebookObject.FacebookLogin();
			}
		}
		else
		{
			CheckInternetConnection.Instance.openPopup();
		}
	}

	private IEnumerator checkConnectionForLeaderboardLogin()
	{
		((MonoBehaviour)this).StartCoroutine(CheckInternetConnection.Instance.checkInternetConnection());
		while (!CheckInternetConnection.Instance.checkDone)
		{
			yield return null;
		}
		if (CheckInternetConnection.Instance.internetOK)
		{
			FacebookManager.FacebookObject.FaceInvite();
		}
		else
		{
			CheckInternetConnection.Instance.openPopup();
		}
	}

	private IEnumerator checkConnectionForPageLike(string url, string key)
	{
		((MonoBehaviour)this).StartCoroutine(CheckInternetConnection.Instance.checkInternetConnection());
		while (!CheckInternetConnection.Instance.checkDone)
		{
			yield return null;
		}
		if (!CheckInternetConnection.Instance.internetOK)
		{
			CheckInternetConnection.Instance.openPopup();
		}
	}

	private IEnumerator checkConnectionForWatchVideo()
	{
		((MonoBehaviour)this).StartCoroutine(CheckInternetConnection.Instance.checkInternetConnection());
		while (!CheckInternetConnection.Instance.checkDone)
		{
			yield return null;
		}
		if (CheckInternetConnection.Instance.internetOK)
		{
			StagesParser.sceneID = 0;
			if (Advertisement.IsReady())
			{
				Advertisement.Show((string)null, new ShowOptions
				{
					resultCallback = delegate(ShowResult result)
					{
						Debug.Log((object)((object)(ShowResult)(ref result)).ToString());
						if (((object)(ShowResult)(ref result)).ToString().Equals("Finished"))
						{
							if (StagesParser.sceneID == 0)
							{
								Debug.Log((object)"ovde li sam");
								StagesParser.currentMoney += StagesParser.watchVideoReward;
								PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
								PlayerPrefs.Save();
								((MonoBehaviour)this).StartCoroutine(StagesParser.Instance.moneyCounter(StagesParser.watchVideoReward, ((Component)((Component)ShopManagerFull.ShopObject).transform.Find("Shop Interface/Coins/Coins Number")).GetComponent<TextMesh>(), hasOutline: true));
							}
							else if (StagesParser.sceneID == 1)
							{
								((Component)Camera.main).SendMessage("WatchVideoCallback", (object)1, (SendMessageOptions)1);
							}
							else if (StagesParser.sceneID == 2)
							{
								GameObject.Find("_GameManager").SendMessage("WatchVideoCallback", (SendMessageOptions)1);
							}
							StagesParser.ServerUpdate = 1;
						}
					}
				});
			}
			else
			{
				CheckInternetConnection.Instance.NoVideosAvailable_OpenPopup();
			}
		}
		else
		{
			CheckInternetConnection.Instance.openPopup();
		}
	}

	private IEnumerator checkConnectionForLogout()
	{
		((MonoBehaviour)this).StartCoroutine(CheckInternetConnection.Instance.checkInternetConnection());
		while (!CheckInternetConnection.Instance.checkDone)
		{
			yield return null;
		}
		if (CheckInternetConnection.Instance.internetOK)
		{
			logoutKliknut = true;
			((Component)SettingsObjects[5]).GetComponent<Collider>().enabled = false;
			((Renderer)((Component)SettingsObjects[5].Find("Shop Tab Element Selected")).GetComponent<SpriteRenderer>()).enabled = true;
			if (FB.IsLoggedIn)
			{
				FacebookManager.FacebookObject.scoreToSet = StagesParser.currentPoints;
				FacebookManager.FacebookObject.proveraPublish_ActionPermisije();
				StagesParser.ServerUpdate = 3;
				FacebookManager.FacebookObject.SacuvajScoreNaNivoima(StagesParser.PointsPoNivoima, StagesParser.StarsPoNivoima, StagesParser.maxLevel, StagesParser.bonusLevels);
				FacebookManager.FacebookObject.UpdateujPodatkeKorisnika(StagesParser.currentMoney, StagesParser.currentPoints, LanguageManager.chosenLanguage, StagesParser.currentBananas, StagesParser.powerup_magnets, StagesParser.powerup_shields, StagesParser.powerup_doublecoins, StagesParser.svekupovineGlava, StagesParser.svekupovineMajica, StagesParser.svekupovineLedja, StagesParser.ledja, StagesParser.glava, StagesParser.majica, StagesParser.imaUsi, StagesParser.imaKosu, FacebookManager.NumberOfFriends);
			}
			Transform transform = GameObject.Find("Loading Buffer HOLDER").transform;
			Transform transform2 = ((Component)Camera.main).transform;
			transform.position = new Vector3(transform2.position.x, transform2.position.y, transform.position.z);
			((Component)transform.GetChild(0)).gameObject.SetActive(true);
			((Component)transform.GetChild(0)).GetComponent<Animator>().Play("LoadingBufferUlazAnimation");
			OcistiLeaderboard();
			DeaktivirajLeaderboard();
			Transform val = LeaderBoardInvite.transform.parent.Find("Friends Tabs/Friend No 2");
			val.localPosition = new Vector3(val.localPosition.x, -1.85f, val.localPosition.z);
			((MonoBehaviour)this).StartCoroutine(DoLogout());
			FacebookLogIn.SetActive(true);
			LeaderBoardInvite.SetActive(false);
			for (int i = 0; i < 10; i++)
			{
				if (i == 1)
				{
					((Component)FriendsObjects[i].Find("FB Invite")).gameObject.SetActive(true);
					((Component)FriendsObjects[i].Find("Friend")).gameObject.SetActive(false);
					((Component)FriendsObjects[i].Find("FB Invite/Coin Shop")).gameObject.SetActive(false);
					((Component)FriendsObjects[i].Find("FB Invite/Invite")).GetComponent<TextMesh>().text = LanguageManager.LogIn;
					((Component)FriendsObjects[i].Find("FB Invite/Invite")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				}
				else
				{
					((Component)FriendsObjects[i]).gameObject.SetActive(false);
				}
			}
		}
		else
		{
			CheckInternetConnection.Instance.openPopup();
		}
	}

	private IEnumerator checkConnectionForResetProgress()
	{
		((MonoBehaviour)this).StartCoroutine(CheckInternetConnection.Instance.checkInternetConnection());
		while (!CheckInternetConnection.Instance.checkDone)
		{
			yield return null;
		}
		if (CheckInternetConnection.Instance.internetOK)
		{
			StagesParser.ServerUpdate = 1;
			FacebookManager.FacebookObject.resetovanScoreNaNulu = 2;
			FacebookManager.FacebookObject.scoreToSet = 0;
			FacebookManager.FacebookObject.proveraPublish_ActionPermisije();
			((MonoBehaviour)this).StartCoroutine(SacekajDaSePostaviScoreNaNulu());
		}
		else
		{
			CheckInternetConnection.Instance.openPopup();
		}
	}

	private void Update()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		Input.GetKeyUp((KeyCode)27);
		if (Input.GetMouseButtonDown(0))
		{
			clickedItem = RaycastFunction(Input.mousePosition);
			if (clickedItem.Equals("NekoDugme") || clickedItem.Equals("ButtonSettings") || clickedItem.Equals("ButtonLeaderboard") || clickedItem.Equals("Custumization") || clickedItem.Equals("FreeCoins") || clickedItem.Equals("4 Reset Progres") || clickedItem.Equals("5 Reset Tutorials") || clickedItem.Equals("6 Log Out") || clickedItem.Equals("Button_CheckOK"))
			{
				temp = GameObject.Find(clickedItem);
				originalScale = temp.transform.localScale;
				temp.transform.localScale = originalScale * 1.2f;
			}
			else if (clickedItem.Equals("ClearAll"))
			{
				((Renderer)((Component)((Component)ShopManagerFull.ShopObject).transform.Find("3 Customize/Costumization BG/ClearAll/ClearAll_Selected")).GetComponent<SpriteRenderer>()).enabled = true;
			}
			else if (clickedItem != string.Empty)
			{
				temp = GameObject.Find(clickedItem);
				originalScale = temp.transform.localScale;
			}
		}
		if (!Input.GetMouseButtonUp(0))
		{
			return;
		}
		releasedItem = RaycastFunction(Input.mousePosition);
		if (clickedItem.Equals(string.Empty))
		{
			return;
		}
		if ((Object)(object)temp != (Object)null)
		{
			temp.transform.localScale = originalScale;
		}
		if (((Renderer)((Component)((Component)ShopManagerFull.ShopObject).transform.Find("3 Customize/Costumization BG/ClearAll/ClearAll_Selected")).GetComponent<SpriteRenderer>()).enabled)
		{
			((Renderer)((Component)((Component)ShopManagerFull.ShopObject).transform.Find("3 Customize/Costumization BG/ClearAll/ClearAll_Selected")).GetComponent<SpriteRenderer>()).enabled = false;
		}
		if (clickedItem == releasedItem && releasedItem == "NekoDugme" && PlaySounds.soundOn)
		{
			PlaySounds.Play_Button_OpenLevel();
		}
		if (clickedItem == releasedItem && releasedItem == "Play")
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
			format = CultureInfo.CurrentCulture.DateTimeFormat;
			string text = DateTime.Now.ToString(format.FullDateTimePattern);
			PlayerPrefs.SetString("VremeQuit", text);
			PlayerPrefs.SetFloat("VremeBrojaca", TimeReward.VremeBrojaca);
			PlayerPrefs.Save();
			GameObject.Find("Loading Animation Vrata").GetComponent<Animator>().Play("Loading Zidovi Dolazak");
			((MonoBehaviour)this).StartCoroutine(otvoriSledeciNivo());
		}
		if (clickedItem == releasedItem && releasedItem == "ButtonSettings")
		{
			GameObject.Find("Settings i Language Scena").GetComponent<Animation>().Play("SettingsDolazak");
			PrikaziSettings();
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem == "ButtonFacebook")
		{
			FacebookManager.KorisnikoviPodaciSpremni = false;
			ShopManagerFull.ShopInicijalizovan = false;
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
			((MonoBehaviour)this).StartCoroutine(checkConnectionForLoginButton());
		}
		else if (clickedItem == releasedItem && releasedItem == "ButtonMusic")
		{
			if (!PlaySounds.musicOn)
			{
				PlaySounds.musicOn = true;
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_Button_MusicOn();
				}
				PlaySounds.Play_BackgroundMusic_Menu();
				PlayerPrefs.SetInt("musicOn", 1);
				PlayerPrefs.Save();
			}
			else
			{
				PlaySounds.musicOn = false;
				PlaySounds.Stop_BackgroundMusic_Menu();
				PlayerPrefs.SetInt("musicOn", 0);
				PlayerPrefs.Save();
			}
		}
		else if (clickedItem == releasedItem && releasedItem == "ButtonSound")
		{
			if (!PlaySounds.soundOn)
			{
				PlaySounds.soundOn = true;
				PlaySounds.Play_Button_SoundOn();
				PlaySounds.Play_Button_SoundOn();
				PlayerPrefs.SetInt("soundOn", 1);
				PlayerPrefs.Save();
			}
			else
			{
				PlaySounds.soundOn = false;
				PlayerPrefs.SetInt("soundOn", 0);
				PlayerPrefs.Save();
			}
		}
		else if (clickedItem == releasedItem && releasedItem == "Zastava")
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
			GameObject.Find("Settings i Language Scena").GetComponent<Animation>().Play("SettingsDolazak");
			PrikaziJezike();
			SettingsOtvoren = true;
			SettingState = 3;
		}
		else if (clickedItem == releasedItem && releasedItem == "ButtonLeaderboard")
		{
			GameObject.Find("Leaderboard Scena").GetComponent<Animation>().Play("MeniDolazak");
			LeaderboardOtvoren = true;
			if (FB.IsLoggedIn)
			{
				((MonoBehaviour)this).Invoke("AktivirajLeaderboard", 1f);
			}
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem == "FreeCoins")
		{
			GameObject.Find("Shop").GetComponent<Animation>().Play("MeniDolazak");
			if (ShopManagerFull.AktivanRanac == 0)
			{
				((Component)((Component)GameObject.Find("MonkeyHolder").transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + ShopManagerFull.AktivanRanac)).transform.GetChild(0)).GetComponent<MeshFilter>().mesh = GameObject.Find("RefZaShop_AndjeoskaKrila").GetComponent<MeshFilter>().mesh;
			}
			else if (ShopManagerFull.AktivanRanac == 5)
			{
				((Component)((Component)GameObject.Find("MonkeyHolder").transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + ShopManagerFull.AktivanRanac)).transform.GetChild(0)).GetComponent<MeshFilter>().mesh = GameObject.Find("RefZaShop_SlepiMisKrila").GetComponent<MeshFilter>().mesh;
			}
			ShopManagerFull.ShopObject.PozoviTab(1);
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem == "Custumization")
		{
			GameObject.Find("Shop").GetComponent<Animation>().Play("MeniDolazak");
			if (ShopManagerFull.AktivanCustomizationTab == 1)
			{
				ShopManagerFull.AktivanItemSesir++;
			}
			else if (ShopManagerFull.AktivanCustomizationTab == 2)
			{
				ShopManagerFull.AktivanItemMajica++;
			}
			else if (ShopManagerFull.AktivanCustomizationTab == 3)
			{
				ShopManagerFull.AktivanItemRanac++;
			}
			if (ShopManagerFull.AktivanRanac == 0)
			{
				((Component)((Component)GameObject.Find("MonkeyHolder").transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + ShopManagerFull.AktivanRanac)).transform.GetChild(0)).GetComponent<MeshFilter>().mesh = GameObject.Find("RefZaShop_AndjeoskaKrila").GetComponent<MeshFilter>().mesh;
			}
			else if (ShopManagerFull.AktivanRanac == 5)
			{
				((Component)((Component)GameObject.Find("MonkeyHolder").transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + ShopManagerFull.AktivanRanac)).transform.GetChild(0)).GetComponent<MeshFilter>().mesh = GameObject.Find("RefZaShop_SlepiMisKrila").GetComponent<MeshFilter>().mesh;
			}
			ShopManagerFull.ShopObject.PozoviTab(3);
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem == "ButtonFreeCoins")
		{
			ShopManagerFull.ShopObject.PozoviTab(1);
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem == "ButtonShop")
		{
			ShopManagerFull.ShopObject.PozoviTab(2);
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem == "ButtonCustomize")
		{
			ShopManagerFull.ShopObject.PozoviTab(3);
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem == "ButtonPowerUps")
		{
			ShopManagerFull.ShopObject.PozoviTab(4);
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem == "ButtonBackShop")
		{
			ShopManagerFull.ShopObject.SkloniShop();
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem == "BackButtonLeaderboard")
		{
			LeaderboardOtvoren = false;
			GameObject.Find("Leaderboard Scena").GetComponent<Animation>().Play("MeniOdlazak");
			((MonoBehaviour)this).Invoke("DeaktivirajLeaderboard", 1f);
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem == "BackButtonSettings")
		{
			if (SettingState == 1)
			{
				GameObject.Find("Settings i Language Scena").GetComponent<Animation>().Play("SettingsOdlazak");
				SettingsOtvoren = false;
				((MonoBehaviour)this).Invoke("DeaktivirajSettings", 1f);
				ProveraZaLogoutZbogDugmica();
			}
			else if (SettingState == 2)
			{
				SettingState = 1;
				GameObject.Find("Settings i Language Scena/Settings Tabs").GetComponent<Animation>().Play("TabSettingsDolazak");
				GameObject.Find("Settings i Language Scena/Language Tabs").GetComponent<Animation>().Play("TabSettingsOdlazak");
				AktivirajSettings();
				if (LanguageManager.chosenLanguage != jezikPreUlaskaUPromenuJezika)
				{
					jezikPreUlaskaUPromenuJezika = LanguageManager.chosenLanguage;
					ShopManagerFull.ShopObject.RefresujImenaItema();
				}
			}
			else if (SettingState == 3)
			{
				SettingState = 1;
				SettingsOtvoren = false;
				GameObject.Find("Settings i Language Scena").GetComponent<Animation>().Play("SettingsOdlazak");
				GameObject.Find("Settings i Language Scena/Language Tabs").GetComponent<Animation>().Play("TabSettingsOdlazak");
				if (LanguageManager.chosenLanguage != jezikPreUlaskaUPromenuJezika)
				{
					jezikPreUlaskaUPromenuJezika = LanguageManager.chosenLanguage;
					ShopManagerFull.ShopObject.RefresujImenaItema();
				}
			}
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem == "ButtonCollect")
		{
			PlayerPrefs.SetInt("ProveriVreme", 1);
			PlayerPrefs.Save();
			switch (DailyRewards.LevelReward)
			{
			case 1:
			{
				GameObject obj5 = GameObject.Find("CoinsReward");
				((Component)obj5.transform.Find("Coins Number")).GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
				((Component)obj5.transform.Find("Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
				StagesParser.currentMoney += DailyRewards.DailyRewardAmount[0];
				PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
				PlayerPrefs.Save();
				dailyReward = DailyRewards.DailyRewardAmount[0];
				((Component)GameObject.Find("Day 1").transform.GetChild(0).Find("CollectCoinsDailyRewardParticles")).GetComponent<ParticleSystem>().Play();
				GameObject.Find("Day 1").GetComponent<Collider>().enabled = false;
				GameObject.Find("ButtonCollect").GetComponent<Collider>().enabled = false;
				obj5.GetComponent<Animation>().Play("CoinsRewardDolazak");
				((MonoBehaviour)this).Invoke("DelayZaOdbrojavanje", 1.15f);
				break;
			}
			case 2:
			{
				GameObject obj4 = GameObject.Find("CoinsReward");
				((Component)obj4.transform.Find("Coins Number")).GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
				((Component)obj4.transform.Find("Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
				StagesParser.currentMoney += DailyRewards.DailyRewardAmount[1];
				PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
				PlayerPrefs.Save();
				dailyReward = DailyRewards.DailyRewardAmount[1];
				((Component)GameObject.Find("Day 2").transform.GetChild(0).Find("CollectCoinsDailyRewardParticles")).GetComponent<ParticleSystem>().Play();
				GameObject.Find("Day 2").GetComponent<Collider>().enabled = false;
				GameObject.Find("ButtonCollect").GetComponent<Collider>().enabled = false;
				obj4.GetComponent<Animation>().Play("CoinsRewardDolazak");
				((MonoBehaviour)this).Invoke("DelayZaOdbrojavanje", 1.15f);
				break;
			}
			case 3:
			{
				GameObject obj3 = GameObject.Find("CoinsReward");
				((Component)obj3.transform.Find("Coins Number")).GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
				((Component)obj3.transform.Find("Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
				StagesParser.currentMoney += DailyRewards.DailyRewardAmount[2];
				PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
				PlayerPrefs.Save();
				dailyReward = DailyRewards.DailyRewardAmount[2];
				((Component)GameObject.Find("Day 3").transform.GetChild(0).Find("CollectCoinsDailyRewardParticles")).GetComponent<ParticleSystem>().Play();
				GameObject.Find("Day 3").GetComponent<Collider>().enabled = false;
				GameObject.Find("ButtonCollect").GetComponent<Collider>().enabled = false;
				obj3.GetComponent<Animation>().Play("CoinsRewardDolazak");
				((MonoBehaviour)this).Invoke("DelayZaOdbrojavanje", 1.15f);
				break;
			}
			case 4:
			{
				GameObject obj2 = GameObject.Find("CoinsReward");
				((Component)obj2.transform.Find("Coins Number")).GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
				((Component)obj2.transform.Find("Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
				StagesParser.currentMoney += DailyRewards.DailyRewardAmount[3];
				PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
				PlayerPrefs.Save();
				dailyReward = DailyRewards.DailyRewardAmount[3];
				((Component)GameObject.Find("Day 4").transform.GetChild(0).Find("CollectCoinsDailyRewardParticles")).GetComponent<ParticleSystem>().Play();
				GameObject.Find("Day 4").GetComponent<Collider>().enabled = false;
				GameObject.Find("ButtonCollect").GetComponent<Collider>().enabled = false;
				obj2.GetComponent<Animation>().Play("CoinsRewardDolazak");
				((MonoBehaviour)this).Invoke("DelayZaOdbrojavanje", 1.15f);
				break;
			}
			case 5:
			{
				GameObject obj = GameObject.Find("CoinsReward");
				((Component)obj.transform.Find("Coins Number")).GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
				((Component)obj.transform.Find("Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
				StagesParser.currentMoney += DailyRewards.DailyRewardAmount[4];
				PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
				PlayerPrefs.Save();
				dailyReward = DailyRewards.DailyRewardAmount[4];
				((Component)GameObject.Find("Day 5").transform.GetChild(0).Find("CollectCoinsDailyRewardParticles")).GetComponent<ParticleSystem>().Play();
				GameObject.Find("Day 5").GetComponent<Collider>().enabled = false;
				GameObject.Find("ButtonCollect").GetComponent<Collider>().enabled = false;
				obj.GetComponent<Animation>().Play("CoinsRewardDolazak");
				((MonoBehaviour)this).Invoke("DelayZaOdbrojavanje", 1.15f);
				break;
			}
			case 6:
				GameObject.Find("Day 6 - Magic Box").GetComponent<Collider>().enabled = false;
				GameObject.Find("ButtonCollect").GetComponent<Collider>().enabled = false;
				MysteryBox();
				break;
			}
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
			StagesParser.ServerUpdate = 1;
		}
		else if (clickedItem == releasedItem && releasedItem == "Day 1")
		{
			if (DailyRewards.LevelReward == 1)
			{
				GameObject obj6 = GameObject.Find("CoinsReward");
				((Component)obj6.transform.Find("Coins Number")).GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
				((Component)obj6.transform.Find("Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
				StagesParser.currentMoney += DailyRewards.DailyRewardAmount[0];
				PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
				PlayerPrefs.Save();
				dailyReward = DailyRewards.DailyRewardAmount[0];
				((Component)GameObject.Find("Day 1").transform.GetChild(0).Find("CollectCoinsDailyRewardParticles")).GetComponent<ParticleSystem>().Play();
				GameObject.Find("Day 1").GetComponent<Collider>().enabled = false;
				GameObject.Find("ButtonCollect").GetComponent<Collider>().enabled = false;
				obj6.GetComponent<Animation>().Play("CoinsRewardDolazak");
				((MonoBehaviour)this).Invoke("DelayZaOdbrojavanje", 1.15f);
			}
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem == "Day 2")
		{
			if (DailyRewards.LevelReward == 2)
			{
				GameObject obj7 = GameObject.Find("CoinsReward");
				((Component)obj7.transform.Find("Coins Number")).GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
				((Component)obj7.transform.Find("Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
				StagesParser.currentMoney += DailyRewards.DailyRewardAmount[1];
				PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
				PlayerPrefs.Save();
				dailyReward = DailyRewards.DailyRewardAmount[1];
				((Component)GameObject.Find("Day 2").transform.GetChild(0).Find("CollectCoinsDailyRewardParticles")).GetComponent<ParticleSystem>().Play();
				GameObject.Find("Day 2").GetComponent<Collider>().enabled = false;
				GameObject.Find("ButtonCollect").GetComponent<Collider>().enabled = false;
				obj7.GetComponent<Animation>().Play("CoinsRewardDolazak");
				((MonoBehaviour)this).Invoke("DelayZaOdbrojavanje", 1.15f);
			}
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem == "Day 3")
		{
			if (DailyRewards.LevelReward == 3)
			{
				GameObject obj8 = GameObject.Find("CoinsReward");
				((Component)obj8.transform.Find("Coins Number")).GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
				((Component)obj8.transform.Find("Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
				StagesParser.currentMoney += DailyRewards.DailyRewardAmount[2];
				PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
				PlayerPrefs.Save();
				dailyReward = DailyRewards.DailyRewardAmount[2];
				((Component)GameObject.Find("Day 3").transform.GetChild(0).Find("CollectCoinsDailyRewardParticles")).GetComponent<ParticleSystem>().Play();
				GameObject.Find("Day 3").GetComponent<Collider>().enabled = false;
				GameObject.Find("ButtonCollect").GetComponent<Collider>().enabled = false;
				obj8.GetComponent<Animation>().Play("CoinsRewardDolazak");
				((MonoBehaviour)this).Invoke("DelayZaOdbrojavanje", 1.15f);
			}
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem == "Day 4")
		{
			if (DailyRewards.LevelReward == 4)
			{
				GameObject obj9 = GameObject.Find("CoinsReward");
				((Component)obj9.transform.Find("Coins Number")).GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
				((Component)obj9.transform.Find("Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
				StagesParser.currentMoney += DailyRewards.DailyRewardAmount[3];
				PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
				PlayerPrefs.Save();
				dailyReward = DailyRewards.DailyRewardAmount[3];
				((Component)GameObject.Find("Day 4").transform.GetChild(0).Find("CollectCoinsDailyRewardParticles")).GetComponent<ParticleSystem>().Play();
				GameObject.Find("Day 4").GetComponent<Collider>().enabled = false;
				GameObject.Find("ButtonCollect").GetComponent<Collider>().enabled = false;
				obj9.GetComponent<Animation>().Play("CoinsRewardDolazak");
				((MonoBehaviour)this).Invoke("DelayZaOdbrojavanje", 1.15f);
			}
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem == "Day 5")
		{
			if (DailyRewards.LevelReward == 5)
			{
				GameObject obj10 = GameObject.Find("CoinsReward");
				((Component)obj10.transform.Find("Coins Number")).GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
				((Component)obj10.transform.Find("Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
				StagesParser.currentMoney += DailyRewards.DailyRewardAmount[4];
				PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
				PlayerPrefs.Save();
				dailyReward = DailyRewards.DailyRewardAmount[4];
				((Component)GameObject.Find("Day 5").transform.GetChild(0).Find("CollectCoinsDailyRewardParticles")).GetComponent<ParticleSystem>().Play();
				GameObject.Find("Day 5").GetComponent<Collider>().enabled = false;
				GameObject.Find("ButtonCollect").GetComponent<Collider>().enabled = false;
				obj10.GetComponent<Animation>().Play("CoinsRewardDolazak");
				((MonoBehaviour)this).Invoke("DelayZaOdbrojavanje", 1.15f);
			}
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem == "Day 6 - Magic Box")
		{
			if (DailyRewards.LevelReward == 6)
			{
				GameObject.Find("Day 6 - Magic Box").GetComponent<Collider>().enabled = false;
				GameObject.Find("ButtonCollect").GetComponent<Collider>().enabled = false;
				MysteryBox();
			}
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem == "1HatsShopTab")
		{
			ShopManagerFull.ShopObject.DeaktivirajCustomization();
			ShopManagerFull.AktivanItemSesir++;
			ShopManagerFull.ShopObject.PozoviCustomizationTab(1);
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem == "2TShirtsShopTab")
		{
			ShopManagerFull.ShopObject.DeaktivirajCustomization();
			ShopManagerFull.AktivanItemMajica++;
			ShopManagerFull.ShopObject.PozoviCustomizationTab(2);
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem == "3BackPackShopTab")
		{
			ShopManagerFull.ShopObject.DeaktivirajCustomization();
			ShopManagerFull.AktivanItemRanac++;
			ShopManagerFull.ShopObject.PozoviCustomizationTab(3);
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem.StartsWith("Hats"))
		{
			for (int i = 0; i < ShopManagerFull.ShopObject.HatsObjects.Length; i++)
			{
				if (releasedItem.StartsWith("Hats " + (i + 1)))
				{
					ObjCustomizationHats.swipeCtrl.currentValue = ShopManagerFull.ShopObject.HatsObjects.Length - i - 1;
				}
			}
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem.StartsWith("Shirts"))
		{
			for (int j = 0; j < ShopManagerFull.ShopObject.ShirtsObjects.Length; j++)
			{
				if (releasedItem.StartsWith("Shirts " + (j + 1)))
				{
					ObjCustomizationShirts.swipeCtrl.currentValue = ShopManagerFull.ShopObject.ShirtsObjects.Length - j - 1;
				}
			}
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem.StartsWith("BackPacks"))
		{
			for (int k = 0; k < ShopManagerFull.ShopObject.BackPacksObjects.Length; k++)
			{
				if (releasedItem.StartsWith("BackPacks " + (k + 1)))
				{
					ObjCustomizationBackPacks.swipeCtrl.currentValue = ShopManagerFull.ShopObject.BackPacksObjects.Length - k - 1;
				}
			}
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem == "ClearAll")
		{
			ShopManagerFull.ShopObject.OcistiMajmuna();
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem == "Kovceg")
		{
			GameObject obj11 = GameObject.Find("Kovceg");
			obj11.GetComponent<Collider>().enabled = false;
			GameObject val = GameObject.Find("CoinsReward");
			((Component)val.transform.Find("Coins Number")).GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
			((Component)val.transform.Find("Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			obj11.GetComponent<TimeReward>().PokupiNagradu();
			obj11.GetComponent<Animator>().Play("Kovceg Collect Animation Click");
			((Component)obj11.transform.Find("PARTIKLI za Kada Se Klikne Collect/CFXM3 Spikes")).GetComponent<ParticleSystem>().Play();
			((Component)obj11.transform.Find("PARTIKLI za Kada Se Klikne Collect/CollectCoinsParticles")).GetComponent<ParticleSystem>().Play();
			val.GetComponent<Animation>().Play("CoinsRewardDolazak");
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem == "Shop Banana")
		{
			ShopManagerFull.ShopObject.KupiBananu();
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem.StartsWith("ShopInApp"))
		{
			_ = releasedItem;
			switch (releasedItem)
			{
			case "ShopInAppPackSmall":
				break;
			case "ShopInAppPackMedium":
				break;
			case "ShopInAppPackLarge":
				break;
			case "ShopInAppPackGiant":
				break;
			case "ShopInAppPackMonster":
				break;
			case "ShopInAppBananaSmall":
				break;
			case "ShopInAppBananaMedium":
				break;
			case "ShopInAppBananaLarge":
				break;
			case "ShopInAppStarter":
				break;
			case "ShopInAppBobosChoice":
				break;
			case "ShopInAppRemoveAds":
				break;
			case "ShopInAppRestore":
				break;
			}
		}
		else if (clickedItem == releasedItem && releasedItem == "Shop POWERUP Double Coins")
		{
			ShopManagerFull.ShopObject.KupiDoubleCoins();
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem == "Shop POWERUP Magnet")
		{
			ShopManagerFull.ShopObject.KupiMagnet();
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem == "Shop POWERUP Shield")
		{
			ShopManagerFull.ShopObject.KupiShield();
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem == "FB Invite")
		{
			((MonoBehaviour)this).StartCoroutine(checkConnectionForLeaderboardLogin());
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem == "Preview Button")
		{
			if (ShopManagerFull.PreviewState)
			{
				ShopManagerFull.ShopObject.PreviewItem();
			}
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem == "Buy Button")
		{
			ShopManagerFull.ShopObject.KupiItem();
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem == "1 Language")
		{
			GameObject.Find("Settings i Language Scena/Settings Tabs").GetComponent<Animation>().Play("TabSettingsOdlazak");
			PrikaziJezike();
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem == "2 Music")
		{
			if (PlaySounds.musicOn)
			{
				((Renderer)((Component)SettingsObjects[1].Find("Shop Tab Element Selected")).GetComponent<SpriteRenderer>()).enabled = true;
				PlaySounds.musicOn = false;
				PlayerPrefs.SetInt("musicOn", 0);
				PlayerPrefs.Save();
				PlaySounds.Stop_BackgroundMusic_Menu();
			}
			else
			{
				((Renderer)((Component)SettingsObjects[1].Find("Shop Tab Element Selected")).GetComponent<SpriteRenderer>()).enabled = false;
				PlaySounds.musicOn = true;
				PlayerPrefs.SetInt("musicOn", 1);
				PlayerPrefs.Save();
				PlaySounds.Play_BackgroundMusic_Menu();
			}
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem == "3 Sound")
		{
			if (PlaySounds.soundOn)
			{
				((Renderer)((Component)SettingsObjects[2].Find("Shop Tab Element Selected")).GetComponent<SpriteRenderer>()).enabled = true;
				PlaySounds.soundOn = false;
				PlayerPrefs.SetInt("soundOn", 0);
				PlayerPrefs.Save();
				return;
			}
			((Renderer)((Component)SettingsObjects[2].Find("Shop Tab Element Selected")).GetComponent<SpriteRenderer>()).enabled = false;
			PlaySounds.soundOn = true;
			PlayerPrefs.SetInt("soundOn", 1);
			PlayerPrefs.Save();
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem == "4 Reset Progres")
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
			((Component)SettingsObjects[3]).GetComponent<Collider>().enabled = false;
			((Renderer)((Component)SettingsObjects[3].Find("Shop Tab Element Selected")).GetComponent<SpriteRenderer>()).enabled = true;
			ResetProgress();
		}
		else if (clickedItem == releasedItem && releasedItem == "5 Reset Tutorials")
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
			((Component)SettingsObjects[4]).GetComponent<Collider>().enabled = false;
			((Renderer)((Component)SettingsObjects[4].Find("Shop Tab Element Selected")).GetComponent<SpriteRenderer>()).enabled = true;
			ResetTutorials();
		}
		else if (clickedItem == releasedItem && releasedItem == "6 Log Out")
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
			((MonoBehaviour)this).StartCoroutine(checkConnectionForLogout());
		}
		else if (clickedItem == releasedItem && releasedItem == "1")
		{
			if (!PlayerPrefs.HasKey("JezikPromenjen"))
			{
				PlayerPrefs.SetInt("JezikPromenjen", 1);
				PlayerPrefs.Save();
			}
			PromeniZastavu(1);
			StagesParser.ServerUpdate = 1;
		}
		else if (clickedItem == releasedItem && releasedItem == "2")
		{
			if (!PlayerPrefs.HasKey("JezikPromenjen"))
			{
				PlayerPrefs.SetInt("JezikPromenjen", 1);
				PlayerPrefs.Save();
			}
			PromeniZastavu(2);
			StagesParser.ServerUpdate = 1;
		}
		else if (clickedItem == releasedItem && releasedItem == "3")
		{
			if (!PlayerPrefs.HasKey("JezikPromenjen"))
			{
				PlayerPrefs.SetInt("JezikPromenjen", 1);
				PlayerPrefs.Save();
			}
			PromeniZastavu(3);
			StagesParser.ServerUpdate = 1;
		}
		else if (clickedItem == releasedItem && releasedItem == "4")
		{
			if (!PlayerPrefs.HasKey("JezikPromenjen"))
			{
				PlayerPrefs.SetInt("JezikPromenjen", 1);
				PlayerPrefs.Save();
			}
			PromeniZastavu(4);
			StagesParser.ServerUpdate = 1;
		}
		else if (clickedItem == releasedItem && releasedItem == "5")
		{
			if (!PlayerPrefs.HasKey("JezikPromenjen"))
			{
				PlayerPrefs.SetInt("JezikPromenjen", 1);
				PlayerPrefs.Save();
			}
			PromeniZastavu(5);
			StagesParser.ServerUpdate = 1;
		}
		else if (clickedItem == releasedItem && releasedItem == "6")
		{
			if (!PlayerPrefs.HasKey("JezikPromenjen"))
			{
				PlayerPrefs.SetInt("JezikPromenjen", 1);
				PlayerPrefs.Save();
			}
			PromeniZastavu(6);
			StagesParser.ServerUpdate = 1;
		}
		else if (clickedItem == releasedItem && releasedItem == "7")
		{
			if (!PlayerPrefs.HasKey("JezikPromenjen"))
			{
				PlayerPrefs.SetInt("JezikPromenjen", 1);
				PlayerPrefs.Save();
			}
			PromeniZastavu(7);
			StagesParser.ServerUpdate = 1;
		}
		else if (clickedItem == releasedItem && releasedItem == "8")
		{
			if (!PlayerPrefs.HasKey("JezikPromenjen"))
			{
				PlayerPrefs.SetInt("JezikPromenjen", 1);
				PlayerPrefs.Save();
			}
			PromeniZastavu(8);
			StagesParser.ServerUpdate = 1;
		}
		else if (clickedItem == releasedItem && releasedItem == "9")
		{
			if (!PlayerPrefs.HasKey("JezikPromenjen"))
			{
				PlayerPrefs.SetInt("JezikPromenjen", 1);
				PlayerPrefs.Save();
			}
			PromeniZastavu(9);
			StagesParser.ServerUpdate = 1;
		}
		else if (clickedItem == releasedItem && releasedItem == "10")
		{
			if (!PlayerPrefs.HasKey("JezikPromenjen"))
			{
				PlayerPrefs.SetInt("JezikPromenjen", 1);
				PlayerPrefs.Save();
			}
			PromeniZastavu(10);
			StagesParser.ServerUpdate = 1;
		}
		else if (clickedItem == releasedItem && releasedItem == "11")
		{
			if (!PlayerPrefs.HasKey("JezikPromenjen"))
			{
				PlayerPrefs.SetInt("JezikPromenjen", 1);
				PlayerPrefs.Save();
			}
			PromeniZastavu(11);
			StagesParser.ServerUpdate = 1;
		}
		else if (clickedItem == releasedItem && releasedItem == "12")
		{
			if (!PlayerPrefs.HasKey("JezikPromenjen"))
			{
				PlayerPrefs.SetInt("JezikPromenjen", 1);
				PlayerPrefs.Save();
			}
			PromeniZastavu(12);
			StagesParser.ServerUpdate = 1;
		}
		else if (clickedItem == releasedItem && releasedItem == "13")
		{
			if (!PlayerPrefs.HasKey("JezikPromenjen"))
			{
				PlayerPrefs.SetInt("JezikPromenjen", 1);
				PlayerPrefs.Save();
			}
			PromeniZastavu(13);
			StagesParser.ServerUpdate = 1;
		}
		else if (clickedItem == releasedItem && releasedItem == "14")
		{
			if (!PlayerPrefs.HasKey("JezikPromenjen"))
			{
				PlayerPrefs.SetInt("JezikPromenjen", 1);
				PlayerPrefs.Save();
			}
			PromeniZastavu(14);
			StagesParser.ServerUpdate = 1;
		}
		else if (clickedItem == releasedItem && releasedItem == "15")
		{
			if (!PlayerPrefs.HasKey("JezikPromenjen"))
			{
				PlayerPrefs.SetInt("JezikPromenjen", 1);
				PlayerPrefs.Save();
			}
			PromeniZastavu(15);
			StagesParser.ServerUpdate = 1;
		}
		else if (clickedItem == releasedItem && releasedItem == "Exit Button")
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
			if (StagesParser.ServerUpdate == 1 && FB.IsLoggedIn)
			{
				FacebookManager.FacebookObject.scoreToSet = StagesParser.currentPoints;
				FacebookManager.FacebookObject.proveraPublish_ActionPermisije();
				FacebookManager.FacebookObject.SacuvajScoreNaNivoima(StagesParser.PointsPoNivoima, StagesParser.StarsPoNivoima, StagesParser.maxLevel, StagesParser.bonusLevels);
				FacebookManager.FacebookObject.UpdateujPodatkeKorisnika(StagesParser.currentMoney, StagesParser.currentPoints, LanguageManager.chosenLanguage, StagesParser.currentBananas, StagesParser.powerup_magnets, StagesParser.powerup_shields, StagesParser.powerup_doublecoins, StagesParser.svekupovineGlava, StagesParser.svekupovineMajica, StagesParser.svekupovineLedja, StagesParser.ledja, StagesParser.glava, StagesParser.majica, StagesParser.imaUsi, StagesParser.imaKosu, FacebookManager.NumberOfFriends);
			}
			Application.Quit();
		}
		else if (clickedItem == releasedItem && releasedItem == "Button_CheckOK")
		{
			((MonoBehaviour)this).StartCoroutine(CheckInternetConnection.Instance.ClosePopup());
		}
		else if (clickedItem == releasedItem && releasedItem == "ShopFCBILikePage")
		{
			((MonoBehaviour)this).StartCoroutine(checkConnectionForPageLike("https://www.facebook.com/pages/Banana-Island/636650059721490", "BananaIsland"));
		}
		else if (clickedItem == releasedItem && releasedItem == "ShopFCWatchVideo")
		{
			((MonoBehaviour)this).StartCoroutine(checkConnectionForWatchVideo());
		}
		else if (clickedItem == releasedItem && releasedItem == "ShopFCWLLikePage")
		{
			((MonoBehaviour)this).StartCoroutine(checkConnectionForPageLike("https://www.facebook.com/WebelinxGamesApps", "Webelinx"));
		}
	}

	private string RaycastFunction(Vector3 vector)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		RaycastHit val = default(RaycastHit);
		if (Physics.Raycast(Camera.main.ScreenPointToRay(vector), ref val))
		{
			return ((Object)((RaycastHit)(ref val)).collider).name;
		}
		return "";
	}

	public void AktivirajLeaderboard()
	{
		ObjLeaderboard.Leaderboard = true;
		SwipeControlLeaderboard.controlEnabled = true;
	}

	public void DeaktivirajLeaderboard()
	{
		ObjLeaderboard.Leaderboard = false;
		SwipeControlLeaderboard.controlEnabled = false;
	}

	public void OcistiLeaderboard()
	{
		Transform val = LeaderBoardInvite.transform.parent.Find("Friends Tabs");
		for (int i = 0; i < val.childCount; i++)
		{
			((Renderer)((Component)val.GetChild(i).Find("Friend/LeaderboardYou")).GetComponent<SpriteRenderer>()).enabled = false;
		}
		FacebookManager.FacebookObject.BrojPrijatelja = 0;
		FacebookManager.FacebookObject.Korisnici.Clear();
		FacebookManager.FacebookObject.Scorovi.Clear();
		FacebookManager.FacebookObject.Imena.Clear();
		FacebookManager.ProfileSlikePrijatelja.Clear();
		FacebookManager.ListaStructPrijatelja.Clear();
	}

	public void AktivirajSettings()
	{
		ObjSettingsTabs.SettingsTabs = true;
		SwipeControlSettingsTabs.controlEnabled = true;
	}

	public void DeaktivirajSettings()
	{
		ObjSettingsTabs.SettingsTabs = false;
		SwipeControlSettingsTabs.controlEnabled = false;
	}

	public void AktivirajLanguages()
	{
		ObjLanguages.Languages = true;
		SwipeControlLanguages.controlEnabled = true;
	}

	public void DeaktivirajLanguages()
	{
		ObjLanguages.Languages = false;
		SwipeControlLanguages.controlEnabled = false;
	}

	public void PrikaziJezike()
	{
		SettingState = 2;
		DeaktivirajSettings();
		((MonoBehaviour)this).Invoke("AktivirajLanguages", 1f);
		jezikPreUlaskaUPromenuJezika = LanguageManager.chosenLanguage;
		GameObject.Find("Settings i Language Scena/Language Tabs").GetComponent<Animation>().Play("TabSettingsDolazak");
	}

	public void PrikaziSettings()
	{
		if (FB.IsLoggedIn)
		{
			((Component)SettingsObjects[5]).GetComponent<Collider>().enabled = true;
			((Renderer)((Component)SettingsObjects[5].Find("Shop Tab Element Selected")).GetComponent<SpriteRenderer>()).enabled = false;
		}
		else
		{
			((Component)SettingsObjects[5]).GetComponent<Collider>().enabled = false;
			((Renderer)((Component)SettingsObjects[5].Find("Shop Tab Element Selected")).GetComponent<SpriteRenderer>()).enabled = true;
		}
		SettingState = 1;
		SettingsOtvoren = true;
		DeaktivirajLanguages();
		((MonoBehaviour)this).Invoke("AktivirajSettings", 1f);
		GameObject.Find("Settings i Language Scena/Settings Tabs").GetComponent<Animation>().Play("TabSettingsDolazak");
	}

	public void PromeniZastavuNaOsnovuImena()
	{
		if (!StagesParser.languageBefore.Equals(LanguageManager.chosenLanguage))
		{
			StagesParser.jezikPromenjen = 1;
			PlayerPrefs.SetInt("JezikPromenjen", 1);
			PlayerPrefs.Save();
		}
		int num = 0;
		if (StagesParser.jezikPromenjen != 0 || (((Object)GameObject.FindGameObjectWithTag("Zastava").GetComponent<Renderer>().material.mainTexture).name.Equals("0") && !LanguageManager.chosenLanguage.Equals("_en")))
		{
			switch (LanguageManager.chosenLanguage)
			{
			case "_en":
				num = 1;
				break;
			case "_us":
				num = 2;
				break;
			case "_es":
				num = 3;
				break;
			case "_ru":
				num = 4;
				break;
			case "_pt":
				num = 5;
				break;
			case "_br":
				num = 6;
				break;
			case "_fr":
				num = 7;
				break;
			case "_th":
				num = 8;
				break;
			case "_ch":
				num = 9;
				break;
			case "_tch":
				num = 10;
				break;
			case "_de":
				num = 11;
				break;
			case "_it":
				num = 12;
				break;
			case "_srb":
				num = 13;
				break;
			case "_tr":
				num = 14;
				break;
			case "_ko":
				num = 15;
				break;
			}
			Object obj = Resources.Load("Zastave/" + num);
			Texture val = (Texture)(object)((obj is Texture) ? obj : null);
			GameObject.FindGameObjectWithTag("Zastava").GetComponent<Renderer>().material.SetTexture("_MainTex", val);
		}
		LanguageManager.RefreshTexts();
		PrevediTekstove();
		CheckInternetConnection.Instance.refreshText();
		StagesParser.LoadingPoruke.Clear();
		StagesParser.RedniBrojSlike.Clear();
		StagesParser.Instance.UcitajLoadingPoruke();
	}

	public void PromeniZastavu(int BrojZastave)
	{
		Object obj = Resources.Load("Zastave/" + BrojZastave);
		Texture val = (Texture)(object)((obj is Texture) ? obj : null);
		GameObject.FindGameObjectWithTag("Zastava").GetComponent<Renderer>().material.SetTexture("_MainTex", val);
		if (PlayerPrefs.HasKey("JezikPromenjen"))
		{
			((Component)GameObject.Find("Settings i Language Scena").transform.Find("Language Tabs/" + BrojZastave + "/Shop Tab Element Selected")).GetComponent<Renderer>().enabled = true;
		}
		if (selectedLanguage != 0 && selectedLanguage != BrojZastave)
		{
			((Component)GameObject.Find("Settings i Language Scena").transform.Find("Language Tabs/" + selectedLanguage + "/Shop Tab Element Selected")).GetComponent<Renderer>().enabled = false;
		}
		selectedLanguage = BrojZastave;
		switch (BrojZastave)
		{
		case 1:
			LanguageManager.chosenLanguage = "_en";
			break;
		case 2:
			LanguageManager.chosenLanguage = "_us";
			break;
		case 3:
			LanguageManager.chosenLanguage = "_es";
			break;
		case 4:
			LanguageManager.chosenLanguage = "_ru";
			break;
		case 5:
			LanguageManager.chosenLanguage = "_pt";
			break;
		case 6:
			LanguageManager.chosenLanguage = "_br";
			break;
		case 7:
			LanguageManager.chosenLanguage = "_fr";
			break;
		case 8:
			LanguageManager.chosenLanguage = "_th";
			break;
		case 9:
			LanguageManager.chosenLanguage = "_ch";
			break;
		case 10:
			LanguageManager.chosenLanguage = "_tch";
			break;
		case 11:
			LanguageManager.chosenLanguage = "_de";
			break;
		case 12:
			LanguageManager.chosenLanguage = "_it";
			break;
		case 13:
			LanguageManager.chosenLanguage = "_srb";
			break;
		case 14:
			LanguageManager.chosenLanguage = "_tr";
			break;
		case 15:
			LanguageManager.chosenLanguage = "_ko";
			break;
		}
		LanguageManager.RefreshTexts();
		PlayerPrefs.SetString("choosenLanguage", LanguageManager.chosenLanguage);
		PlayerPrefs.Save();
		PrevediTekstove();
		CheckInternetConnection.Instance.refreshText();
		StagesParser.LoadingPoruke.Clear();
		StagesParser.RedniBrojSlike.Clear();
		StagesParser.Instance.UcitajLoadingPoruke();
	}

	private void PrevediTekstove()
	{
		GameObject.Find("Kovceg/Text/Collect").GetComponent<TextMesh>().text = LanguageManager.Collect;
		GameObject.Find("ButtonCollect/Text").GetComponent<TextMesh>().text = LanguageManager.Collect;
		GameObject.Find("ButtonCollect/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)GameObject.Find("Home Scena Interface").transform.Find("FB HOLDER LogIn/ButtonFacebook/Log in")).GetComponent<TextMesh>().text = LanguageManager.LogIn;
		((Component)GameObject.Find("Home Scena Interface").transform.Find("FB HOLDER LogIn/ButtonFacebook/Log in")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Zid Header Shop/Text").GetComponent<TextMesh>().text = LanguageManager.DailyReward;
		GameObject.Find("Zid Header Shop/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("ButtonFreeCoins/Text").GetComponent<TextMesh>().text = LanguageManager.FreeCoins;
		GameObject.Find("ButtonFreeCoins/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("ButtonCustomize/Text").GetComponent<TextMesh>().text = LanguageManager.Customize;
		GameObject.Find("ButtonCustomize/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("ButtonPowerUps/Text").GetComponent<TextMesh>().text = LanguageManager.PowerUps;
		GameObject.Find("ButtonPowerUps/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("ButtonShop/Text").GetComponent<TextMesh>().text = LanguageManager.Shop;
		GameObject.Find("ButtonShop/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Shop Banana/Text/Banana").GetComponent<TextMesh>().text = LanguageManager.Banana;
		GameObject.Find("Shop Banana/Text/Banana").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("ShopFCWatchVideo/Text/Watch Video").GetComponent<TextMesh>().text = LanguageManager.WatchVideo;
		GameObject.Find("ShopFCWatchVideo/Text/Watch Video").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("ShopFCBILikePage/Text/FollowUsOnFacebook").GetComponent<TextMesh>().text = LanguageManager.FollowUsOnFacebook;
		GameObject.Find("ShopFCBILikePage/Text/FollowUsOnFacebook").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("ShopFCWLLikePage/Text/FollowUsOnFacebook").GetComponent<TextMesh>().text = LanguageManager.FollowUsOnFacebook;
		GameObject.Find("ShopFCWLLikePage/Text/FollowUsOnFacebook").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("ButtonBuy").GetComponent<TextMesh>().text = LanguageManager.Buy;
		GameObject.Find("ButtonBuy").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Shop POWERUP Double Coins/Text/ime").GetComponent<TextMesh>().text = LanguageManager.DoubleCoins;
		GameObject.Find("Shop POWERUP Double Coins/Text/ime").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Shop POWERUP Magnet/Text/ime").GetComponent<TextMesh>().text = LanguageManager.CoinsMagnet;
		GameObject.Find("Shop POWERUP Magnet/Text/ime").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Shop POWERUP Shield/Text/ime").GetComponent<TextMesh>().text = LanguageManager.Shield;
		GameObject.Find("Shop POWERUP Shield/Text/ime").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("TextLanguage").GetComponent<TextMesh>().text = LanguageManager.Language;
		GameObject.Find("TextLanguage").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("TextSettings").GetComponent<TextMesh>().text = LanguageManager.Settings;
		GameObject.Find("TextSettings").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("TextLeaderboard").GetComponent<TextMesh>().text = LanguageManager.Leaderboard;
		GameObject.Find("TextLeaderboard").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)GameObject.Find("Leaderboard Scena").transform.Find("FB Invite/Text/Invite And Earn")).GetComponent<TextMesh>().text = LanguageManager.InviteAndEarn;
		((Component)GameObject.Find("Leaderboard Scena").transform.Find("FB Invite/Text/Invite And Earn")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Day 1/DailyRewordDayAnimationHolder/Text/Day").GetComponent<TextMesh>().text = LanguageManager.Day + " 1";
		GameObject.Find("Day 1/DailyRewordDayAnimationHolder/Text/Day").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Day 2/DailyRewordDayAnimationHolder/Text/Day").GetComponent<TextMesh>().text = LanguageManager.Day + " 2";
		GameObject.Find("Day 2/DailyRewordDayAnimationHolder/Text/Day").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Day 3/DailyRewordDayAnimationHolder/Text/Day").GetComponent<TextMesh>().text = LanguageManager.Day + " 3";
		GameObject.Find("Day 3/DailyRewordDayAnimationHolder/Text/Day").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Day 4/DailyRewordDayAnimationHolder/Text/Day").GetComponent<TextMesh>().text = LanguageManager.Day + " 4";
		GameObject.Find("Day 4/DailyRewordDayAnimationHolder/Text/Day").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Day 5/DailyRewordDayAnimationHolder/Text/Day").GetComponent<TextMesh>().text = LanguageManager.Day + " 5";
		GameObject.Find("Day 5/DailyRewordDayAnimationHolder/Text/Day").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Day 6 - Magic Box/DailyRewordDayAnimationHolder/Text/Day").GetComponent<TextMesh>().text = LanguageManager.Day + " 6";
		GameObject.Find("Day 6 - Magic Box/DailyRewordDayAnimationHolder/Text/Day").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		Transform transform = GameObject.Find("Settings Tabs").transform;
		((Component)transform.Find("1 Language/Text/Text")).GetComponent<TextMesh>().text = LanguageManager.Language;
		((Component)transform.Find("1 Language/Text/Text")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)transform.Find("2 Music/Text/Text")).GetComponent<TextMesh>().text = LanguageManager.Music;
		((Component)transform.Find("2 Music/Text/Text")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)transform.Find("3 Sound/Text/Text")).GetComponent<TextMesh>().text = LanguageManager.Sound;
		((Component)transform.Find("3 Sound/Text/Text")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)transform.Find("4 Reset Progres/Text/Text")).GetComponent<TextMesh>().text = LanguageManager.ResetProgress;
		((Component)transform.Find("4 Reset Progres/Text/Text")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)transform.Find("5 Reset Tutorials/Text/Text")).GetComponent<TextMesh>().text = LanguageManager.ResetTutorials;
		((Component)transform.Find("5 Reset Tutorials/Text/Text")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)transform.Find("6 Log Out/Text/Text")).GetComponent<TextMesh>().text = LanguageManager.LogOut;
		((Component)transform.Find("6 Log Out/Text/Text")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		transform = LeaderBoardInvite.transform.parent.Find("Friends Tabs");
		for (int i = 0; i < 10; i++)
		{
			if (((Component)transform.GetChild(i).Find("FB Invite")).gameObject.activeSelf)
			{
				((Component)transform.GetChild(i).Find("FB Invite/Invite")).GetComponent<TextMesh>().text = LanguageManager.InviteFriendsAndEarn;
				((Component)transform.GetChild(i).Find("FB Invite/Invite")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
				continue;
			}
			((Component)transform.GetChild(i).Find("FB Invite")).gameObject.SetActive(true);
			((Component)transform.GetChild(i).Find("FB Invite/Invite")).GetComponent<TextMesh>().text = LanguageManager.InviteFriendsAndEarn;
			((Component)transform.GetChild(i).Find("FB Invite/Invite")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
			((Component)transform.GetChild(i).Find("FB Invite")).gameObject.SetActive(false);
		}
	}

	public void addAvatar()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		creatAvatar(10, 51, 100, new Vector3(-5f, 0f, 0f), new Vector3(0f, 0f, 80f));
		KBEngineApp.app.entity_id = 10;
		new Dictionary<int, int>();
	}

	public void creatAvatar(int avaterID, int roleType, int HP_Max, Vector3 position, Vector3 direction)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		KBEngineApp.app.Client_onCreatedProxies((ulong)avaterID, avaterID, "Avatar");
		Avatar obj = (Avatar)KBEngineApp.app.entities[avaterID];
		obj.roleTypeCell = (uint)roleType;
		obj.position = position;
		obj.direction = direction;
		obj.HP_Max = HP_Max;
		obj.HP = HP_Max;
		obj.ZiZhi = 24;
		obj.LingGeng.Add(0);
		obj.LingGeng.Add(1);
		obj.LingGeng.Add(2);
	}

	private IEnumerator otvoriSledeciNivo()
	{
		yield return (object)new WaitForSeconds(1.1f);
		if (StagesParser.odgledaoTutorial == 0)
		{
			StagesParser.loadingTip = 1;
			addAvatar();
			Application.LoadLevel("AllMaps");
		}
		else
		{
			StagesParser.vratioSeNaSvaOstrva = true;
			addAvatar();
			Application.LoadLevel("AllMaps");
		}
	}

	private void SkloniCoinsReward()
	{
		GameObject.Find("CoinsReward").GetComponent<Animation>().Play("CoinsRewardOdlazak");
		GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyOdlazak");
	}

	private void DelayZaOdbrojavanje()
	{
		((MonoBehaviour)this).StartCoroutine(StagesParser.Instance.moneyCounter(dailyReward, GameObject.Find("CoinsReward/Coins Number").GetComponent<TextMesh>(), hasOutline: true));
		((MonoBehaviour)this).Invoke("SkloniCoinsReward", 1.2f);
	}

	private void MysteryBox()
	{
		GameObject val = GameObject.Find("Day 6 - Magic Box");
		((Component)val.transform.GetChild(0)).GetComponent<Animator>().Play("CollectDailyRewardMagicBox");
		Sprite val2 = null;
		if (StagesParser.powerup_magnets <= StagesParser.powerup_shields)
		{
			if (StagesParser.powerup_magnets <= StagesParser.powerup_doublecoins)
			{
				val2 = ((Component)((Component)ShopManagerFull.ShopObject).transform.Find("4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Magnet/Plavi Bedz/Magnet Icon")).GetComponent<SpriteRenderer>().sprite;
				if (StagesParser.powerup_magnets <= 10)
				{
					StagesParser.powerup_magnets += 3;
					((Component)val.transform.GetChild(0).Find("Magic Box Reward HOLDER/Magic Box Reward/Kolicina")).GetComponent<TextMesh>().text = "x 3";
					((Component)val.transform.GetChild(0).Find("Magic Box Reward HOLDER/Magic Box Reward/Kolicina")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
				}
				else
				{
					StagesParser.powerup_magnets += 2;
					((Component)val.transform.GetChild(0).Find("Magic Box Reward HOLDER/Magic Box Reward/Kolicina")).GetComponent<TextMesh>().text = "x 2";
					((Component)val.transform.GetChild(0).Find("Magic Box Reward HOLDER/Magic Box Reward/Kolicina")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
				}
			}
			else
			{
				val2 = ((Component)((Component)ShopManagerFull.ShopObject).transform.Find("4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Double Coins/Plavi Bedz/Double Coins Icon")).GetComponent<SpriteRenderer>().sprite;
				if (StagesParser.powerup_doublecoins <= 10)
				{
					StagesParser.powerup_doublecoins += 3;
					((Component)val.transform.GetChild(0).Find("Magic Box Reward HOLDER/Magic Box Reward/Kolicina")).GetComponent<TextMesh>().text = "x 3";
					((Component)val.transform.GetChild(0).Find("Magic Box Reward HOLDER/Magic Box Reward/Kolicina")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
				}
				else
				{
					StagesParser.powerup_doublecoins += 2;
					((Component)val.transform.GetChild(0).Find("Magic Box Reward HOLDER/Magic Box Reward/Kolicina")).GetComponent<TextMesh>().text = "x 2";
					((Component)val.transform.GetChild(0).Find("Magic Box Reward HOLDER/Magic Box Reward/Kolicina")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
				}
			}
		}
		else if (StagesParser.powerup_shields <= StagesParser.powerup_doublecoins)
		{
			val2 = ((Component)((Component)ShopManagerFull.ShopObject).transform.Find("4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Shield/Plavi Bedz/Shield Icon")).GetComponent<SpriteRenderer>().sprite;
			if (StagesParser.powerup_shields <= 10)
			{
				StagesParser.powerup_shields += 3;
				((Component)val.transform.GetChild(0).Find("Magic Box Reward HOLDER/Magic Box Reward/Kolicina")).GetComponent<TextMesh>().text = "x 3";
				((Component)val.transform.GetChild(0).Find("Magic Box Reward HOLDER/Magic Box Reward/Kolicina")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			}
			else
			{
				StagesParser.powerup_shields += 2;
				((Component)val.transform.GetChild(0).Find("Magic Box Reward HOLDER/Magic Box Reward/Kolicina")).GetComponent<TextMesh>().text = "x 2";
				((Component)val.transform.GetChild(0).Find("Magic Box Reward HOLDER/Magic Box Reward/Kolicina")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			}
		}
		else
		{
			val2 = ((Component)((Component)ShopManagerFull.ShopObject).transform.Find("4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Double Coins/Plavi Bedz/Double Coins Icon")).GetComponent<SpriteRenderer>().sprite;
			if (StagesParser.powerup_doublecoins <= 10)
			{
				StagesParser.powerup_doublecoins += 3;
				((Component)val.transform.GetChild(0).Find("Magic Box Reward HOLDER/Magic Box Reward/Kolicina")).GetComponent<TextMesh>().text = "x 3";
				((Component)val.transform.GetChild(0).Find("Magic Box Reward HOLDER/Magic Box Reward/Kolicina")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			}
			else
			{
				StagesParser.powerup_doublecoins += 2;
				((Component)val.transform.GetChild(0).Find("Magic Box Reward HOLDER/Magic Box Reward/Kolicina")).GetComponent<TextMesh>().text = "x 2";
				((Component)val.transform.GetChild(0).Find("Magic Box Reward HOLDER/Magic Box Reward/Kolicina")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			}
		}
		StagesParser.currentBananas++;
		PlayerPrefs.SetInt("TotalBananas", StagesParser.currentBananas);
		PlayerPrefs.SetString("PowerUps", StagesParser.powerup_doublecoins + "#" + StagesParser.powerup_magnets + "#" + StagesParser.powerup_shields);
		PlayerPrefs.Save();
		GameObject.Find("Double Coins Number/Number").GetComponent<TextMesh>().text = StagesParser.powerup_doublecoins.ToString();
		GameObject.Find("Double Coins Number/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Magnet Number/Number").GetComponent<TextMesh>().text = StagesParser.powerup_magnets.ToString();
		GameObject.Find("Magnet Number/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Shield Number/Number").GetComponent<TextMesh>().text = StagesParser.powerup_shields.ToString();
		GameObject.Find("Shield Number/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)val.transform.GetChild(0).Find("Magic Box Reward HOLDER/Magic Box Reward")).GetComponent<SpriteRenderer>().sprite = val2;
		((MonoBehaviour)this).Invoke("SkloniDailyRewardsPosleMysteryBox", 4.5f);
	}

	private void SkloniDailyRewardsPosleMysteryBox()
	{
		GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyOdlazak");
		((MonoBehaviour)this).Invoke("UgasiMysteryBox", 2f);
	}

	private void UgasiMysteryBox()
	{
		GameObject.Find("Day 6 - Magic Box").SetActive(false);
	}

	private void ResetProgress()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		Transform transform = GameObject.Find("Loading Buffer HOLDER").transform;
		Transform transform2 = ((Component)Camera.main).transform;
		transform.position = new Vector3(transform2.position.x, transform2.position.y, transform.position.z);
		((Component)transform.GetChild(0)).gameObject.SetActive(true);
		((Component)transform.GetChild(0)).GetComponent<Animator>().Play("LoadingBufferUlazAnimation");
		string text = "1#0#0";
		for (int i = 1; i < StagesParser.allLevels.Length; i++)
		{
			text = text + "_" + (i + 1) + "#-1#0";
		}
		StagesParser.allLevels = text.Split(new char[1] { '_' });
		PlayerPrefs.SetString("AllLevels", text);
		StagesParser.currentPoints = 0;
		PlayerPrefs.SetInt("TotalPoints", StagesParser.currentPoints);
		StagesParser.lastUnlockedWorldIndex = 0;
		for (int j = 0; j < StagesParser.totalSets; j++)
		{
			StagesParser.unlockedWorlds[j] = false;
		}
		StagesParser.unlockedWorlds[0] = true;
		if (FB.IsLoggedIn)
		{
			((MonoBehaviour)this).StartCoroutine(checkConnectionForResetProgress());
		}
		for (int k = 0; k < StagesParser.totalSets; k++)
		{
			StagesParser.trenutniNivoNaOstrvu[k] = 1;
			PlayerPrefs.SetInt("TrenutniNivoNaOstrvu" + k, StagesParser.trenutniNivoNaOstrvu[k]);
		}
		PlayerPrefs.Save();
		StagesParser.RecountTotalStars();
		if (!FB.IsLoggedIn)
		{
			StagesParser.Instance.UgasiLoading();
		}
	}

	private IEnumerator SacekajDaSePostaviScoreNaNulu()
	{
		while (FacebookManager.FacebookObject.resetovanScoreNaNulu == 2)
		{
			yield return null;
		}
		FacebookManager.MestoPozivanjaLogina = 1;
		OcistiLeaderboard();
		FacebookManager.FacebookObject.GetFacebookFriendScores();
	}

	private void ResetTutorials()
	{
		StagesParser.odgledaoTutorial = 0;
		StagesParser.currStageIndex = 0;
		StagesParser.currSetIndex = 0;
		PlayerPrefs.SetString("OdgledaoTutorial", StagesParser.odgledaoTutorial + "#" + StagesParser.otvaraoShopNekad);
		PlayerPrefs.Save();
	}

	private IEnumerator DoLogout()
	{
		while (!FacebookManager.FacebookObject.OKzaLogout)
		{
			yield return null;
		}
		FacebookManager.FacebookLogout();
		FacebookManager.FacebookObject.OKzaLogout = false;
	}

	private void ProveraZaLogoutZbogDugmica()
	{
		if (!logoutKliknut)
		{
			return;
		}
		logoutKliknut = false;
		if (FB.IsLoggedIn)
		{
			return;
		}
		if (PlayerPrefs.GetInt("Logovan") == 1)
		{
			PlayerPrefs.SetInt("Logovan", 0);
		}
		FacebookLogIn.SetActive(true);
		LeaderBoardInvite.SetActive(false);
		for (int i = 0; i < 10; i++)
		{
			if (i == 1)
			{
				((Component)FriendsObjects[i].Find("FB Invite")).gameObject.SetActive(true);
				((Component)FriendsObjects[i].Find("Friend")).gameObject.SetActive(false);
				((Component)FriendsObjects[i].Find("FB Invite/Coin Shop")).gameObject.SetActive(false);
				((Component)FriendsObjects[i].Find("FB Invite/Invite")).GetComponent<TextMesh>().text = LanguageManager.LogIn;
				((Component)FriendsObjects[i].Find("FB Invite/Invite")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
			}
			else
			{
				((Component)FriendsObjects[i]).gameObject.SetActive(false);
			}
		}
		PlayerPrefs.DeleteKey("JezikPromenjen");
		PlayerPrefs.Save();
	}
}
