using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using KBEngine;
using UnityEngine;
using UnityEngine.Advertisements;

// Token: 0x020006EA RID: 1770
public class MainScene : MonoBehaviour
{
	// Token: 0x06002C7B RID: 11387 RVA: 0x0015BF84 File Offset: 0x0015A184
	private void Start()
	{
		if (Advertisement.isSupported)
		{
			Advertisement.Initialize(StagesParser.Instance.UnityAdsVideoGameID);
		}
		else
		{
			Debug.Log("UNITYADS Platform not supported");
		}
		base.StartCoroutine(this.checkConnectionForAutologin());
		if (Loading.Instance != null)
		{
			base.StartCoroutine(Loading.Instance.UcitanaScena(Camera.main, 4, 0.25f));
		}
		MainScene.LeaderBoardInvite = GameObject.Find("Leaderboard Scena/FB Invite");
		MainScene.LeaderBoardInvite.transform.Find("Text/Number").GetComponent<TextMesh>().text = "+" + StagesParser.InviteReward;
		MainScene.LeaderBoardInvite.transform.Find("Text/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, false, true);
		ShopManagerFull.ShopObject.transform.Find("1 Free Coins/Free Coins Tabovi/ShopFCWatchVideo/Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMesh>().text = StagesParser.watchVideoReward.ToString();
		ShopManagerFull.ShopObject.transform.Find("1 Free Coins/Free Coins Tabovi/ShopFCWatchVideo/Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, false, true);
		MainScene.FacebookLogIn = GameObject.Find("FB HOLDER LogIn");
		GameObject.Find("Gore Levo HOLDER Buttons").transform.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.zero).x, Camera.main.ViewportToWorldPoint(Vector3.one).y, 0f);
		GameObject.Find("Dole Desno HOLDER Buttons").transform.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.one).x, Camera.main.ViewportToWorldPoint(Vector3.zero).y, 0f);
		GameObject.Find("Dole Levo HOLDER Buttons").transform.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.zero).x, Camera.main.ViewportToWorldPoint(Vector3.zero).y, 0f);
		if (PlayerPrefs.HasKey("JezikPromenjen"))
		{
			string chosenLanguage = LanguageManager.chosenLanguage;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(chosenLanguage);
			if (num <= 3673310179U)
			{
				if (num <= 3623565702U)
				{
					if (num != 1904450637U)
					{
						if (num != 2677697495U)
						{
							if (num == 3623565702U)
							{
								if (chosenLanguage == "_es")
								{
									this.PromeniZastavu(3);
									goto IL_4CB;
								}
							}
						}
						else if (chosenLanguage == "_srb")
						{
							this.PromeniZastavu(13);
							goto IL_4CB;
						}
					}
					else if (chosenLanguage == "_tch")
					{
						this.PromeniZastavu(10);
						goto IL_4CB;
					}
				}
				else if (num <= 3642756082U)
				{
					if (num != 3626228726U)
					{
						if (num == 3642756082U)
						{
							if (chosenLanguage == "_th")
							{
								this.PromeniZastavu(8);
								goto IL_4CB;
							}
						}
					}
					else if (chosenLanguage == "_us")
					{
						this.PromeniZastavu(2);
						goto IL_4CB;
					}
				}
				else if (num != 3659931059U)
				{
					if (num == 3673310179U)
					{
						if (chosenLanguage == "_it")
						{
							this.PromeniZastavu(12);
							goto IL_4CB;
						}
					}
				}
				else if (chosenLanguage == "_ru")
				{
					this.PromeniZastavu(4);
					goto IL_4CB;
				}
			}
			else if (num <= 4011863700U)
			{
				if (num <= 3874788702U)
				{
					if (num != 3741156130U)
					{
						if (num == 3874788702U)
						{
							if (chosenLanguage == "_fr")
							{
								this.PromeniZastavu(7);
								goto IL_4CB;
							}
						}
					}
					else if (chosenLanguage == "_br")
					{
						this.PromeniZastavu(6);
						goto IL_4CB;
					}
				}
				else if (num != 3979000010U)
				{
					if (num == 4011863700U)
					{
						if (chosenLanguage == "_tr")
						{
							this.PromeniZastavu(14);
							goto IL_4CB;
						}
					}
				}
				else if (chosenLanguage == "_pt")
				{
					this.PromeniZastavu(5);
					goto IL_4CB;
				}
			}
			else if (num <= 4110116653U)
			{
				if (num != 4092353296U)
				{
					if (num == 4110116653U)
					{
						if (chosenLanguage == "_en")
						{
							this.PromeniZastavu(1);
							goto IL_4CB;
						}
					}
				}
				else if (chosenLanguage == "_ko")
				{
					this.PromeniZastavu(15);
					goto IL_4CB;
				}
			}
			else if (num != 4211076557U)
			{
				if (num == 4260968129U)
				{
					if (chosenLanguage == "_de")
					{
						this.PromeniZastavu(11);
						goto IL_4CB;
					}
				}
			}
			else if (chosenLanguage == "_ch")
			{
				this.PromeniZastavu(9);
				goto IL_4CB;
			}
			this.PromeniZastavu(0);
		}
		IL_4CB:
		if (PlaySounds.soundOn)
		{
			this.SettingsObjects[2].Find("Shop Tab Element Selected").GetComponent<SpriteRenderer>().enabled = false;
		}
		else
		{
			this.SettingsObjects[2].Find("Shop Tab Element Selected").GetComponent<SpriteRenderer>().enabled = true;
		}
		if (PlaySounds.musicOn)
		{
			this.SettingsObjects[1].Find("Shop Tab Element Selected").GetComponent<SpriteRenderer>().enabled = false;
			PlaySounds.Play_BackgroundMusic_Menu();
		}
		else
		{
			this.SettingsObjects[1].Find("Shop Tab Element Selected").GetComponent<SpriteRenderer>().enabled = true;
		}
		ShopManagerFull.AktivanTab = 0;
		if (FB.IsLoggedIn)
		{
			GameObject.Find("FB HOLDER LogIn").SetActive(false);
			MainScene.LeaderBoardInvite.SetActive(true);
		}
		else
		{
			MainScene.LeaderBoardInvite.SetActive(false);
			GameObject.Find("FB HOLDER LogIn").SetActive(true);
			for (int i = 0; i < 10; i++)
			{
				if (i == 1)
				{
					this.FriendsObjects[i].Find("FB Invite").gameObject.SetActive(true);
					this.FriendsObjects[i].Find("Friend").gameObject.SetActive(false);
					this.FriendsObjects[i].Find("FB Invite/Coin Shop").gameObject.SetActive(false);
					this.FriendsObjects[i].Find("FB Invite/Invite").GetComponent<TextMesh>().text = LanguageManager.LogIn;
					this.FriendsObjects[i].Find("FB Invite/Invite").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
					this.FriendsObjects[i].Find("FB Invite/Coin Number").GetComponent<TextMesh>().text = "+" + StagesParser.LoginReward;
					this.FriendsObjects[i].Find("FB Invite/Coin Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, false, true);
				}
				else
				{
					this.FriendsObjects[i].gameObject.SetActive(false);
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

	// Token: 0x06002C7C RID: 11388 RVA: 0x00021CA8 File Offset: 0x0001FEA8
	private IEnumerator checkConnectionForAutologin()
	{
		base.StartCoroutine(CheckInternetConnection.Instance.checkInternetConnection());
		while (!CheckInternetConnection.Instance.checkDone)
		{
			yield return null;
		}
		if (CheckInternetConnection.Instance.internetOK && PlayerPrefs.HasKey("Logovan"))
		{
			if (PlayerPrefs.GetInt("Logovan") == 1)
			{
				if (!FacebookManager.Ulogovan)
				{
					FacebookManager.FacebookObject.FacebookLogin();
				}
				else
				{
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
			else if (FB.IsLoggedIn)
			{
				if (!FacebookManager.Ulogovan)
				{
					FacebookManager.FacebookObject.FacebookLogin();
				}
				else
				{
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
		}
		yield break;
	}

	// Token: 0x06002C7D RID: 11389 RVA: 0x00021CB7 File Offset: 0x0001FEB7
	private IEnumerator checkConnectionForLoginButton()
	{
		base.StartCoroutine(CheckInternetConnection.Instance.checkInternetConnection());
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
		yield break;
	}

	// Token: 0x06002C7E RID: 11390 RVA: 0x00021CC6 File Offset: 0x0001FEC6
	private IEnumerator checkConnectionForLeaderboardLogin()
	{
		base.StartCoroutine(CheckInternetConnection.Instance.checkInternetConnection());
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
		yield break;
	}

	// Token: 0x06002C7F RID: 11391 RVA: 0x00021CD5 File Offset: 0x0001FED5
	private IEnumerator checkConnectionForPageLike(string url, string key)
	{
		base.StartCoroutine(CheckInternetConnection.Instance.checkInternetConnection());
		while (!CheckInternetConnection.Instance.checkDone)
		{
			yield return null;
		}
		if (!CheckInternetConnection.Instance.internetOK)
		{
			CheckInternetConnection.Instance.openPopup();
		}
		yield break;
	}

	// Token: 0x06002C80 RID: 11392 RVA: 0x00021CE4 File Offset: 0x0001FEE4
	private IEnumerator checkConnectionForWatchVideo()
	{
		base.StartCoroutine(CheckInternetConnection.Instance.checkInternetConnection());
		while (!CheckInternetConnection.Instance.checkDone)
		{
			yield return null;
		}
		if (CheckInternetConnection.Instance.internetOK)
		{
			StagesParser.sceneID = 0;
			if (Advertisement.IsReady())
			{
				Advertisement.Show(null, new ShowOptions
				{
					resultCallback = delegate(ShowResult result)
					{
						Debug.Log(result.ToString());
						if (result.ToString().Equals("Finished"))
						{
							if (StagesParser.sceneID == 0)
							{
								Debug.Log("ovde li sam");
								StagesParser.currentMoney += StagesParser.watchVideoReward;
								PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
								PlayerPrefs.Save();
								base.StartCoroutine(StagesParser.Instance.moneyCounter(StagesParser.watchVideoReward, ShopManagerFull.ShopObject.transform.Find("Shop Interface/Coins/Coins Number").GetComponent<TextMesh>(), true));
							}
							else if (StagesParser.sceneID == 1)
							{
								Camera.main.SendMessage("WatchVideoCallback", 1, 1);
							}
							else if (StagesParser.sceneID == 2)
							{
								GameObject.Find("_GameManager").SendMessage("WatchVideoCallback", 1);
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
		yield break;
	}

	// Token: 0x06002C81 RID: 11393 RVA: 0x00021CF3 File Offset: 0x0001FEF3
	private IEnumerator checkConnectionForLogout()
	{
		base.StartCoroutine(CheckInternetConnection.Instance.checkInternetConnection());
		while (!CheckInternetConnection.Instance.checkDone)
		{
			yield return null;
		}
		if (CheckInternetConnection.Instance.internetOK)
		{
			this.logoutKliknut = true;
			this.SettingsObjects[5].GetComponent<Collider>().enabled = false;
			this.SettingsObjects[5].Find("Shop Tab Element Selected").GetComponent<SpriteRenderer>().enabled = true;
			if (FB.IsLoggedIn)
			{
				FacebookManager.FacebookObject.scoreToSet = StagesParser.currentPoints;
				FacebookManager.FacebookObject.proveraPublish_ActionPermisije();
				StagesParser.ServerUpdate = 3;
				FacebookManager.FacebookObject.SacuvajScoreNaNivoima(StagesParser.PointsPoNivoima, StagesParser.StarsPoNivoima, StagesParser.maxLevel, StagesParser.bonusLevels);
				FacebookManager.FacebookObject.UpdateujPodatkeKorisnika(StagesParser.currentMoney, StagesParser.currentPoints, LanguageManager.chosenLanguage, StagesParser.currentBananas, StagesParser.powerup_magnets, StagesParser.powerup_shields, StagesParser.powerup_doublecoins, StagesParser.svekupovineGlava, StagesParser.svekupovineMajica, StagesParser.svekupovineLedja, StagesParser.ledja, StagesParser.glava, StagesParser.majica, StagesParser.imaUsi, StagesParser.imaKosu, FacebookManager.NumberOfFriends);
			}
			Transform transform = GameObject.Find("Loading Buffer HOLDER").transform;
			Transform transform2 = Camera.main.transform;
			transform.position = new Vector3(transform2.position.x, transform2.position.y, transform.position.z);
			transform.GetChild(0).gameObject.SetActive(true);
			transform.GetChild(0).GetComponent<Animator>().Play("LoadingBufferUlazAnimation");
			this.OcistiLeaderboard();
			this.DeaktivirajLeaderboard();
			Transform transform3 = MainScene.LeaderBoardInvite.transform.parent.Find("Friends Tabs/Friend No 2");
			transform3.localPosition = new Vector3(transform3.localPosition.x, -1.85f, transform3.localPosition.z);
			base.StartCoroutine(this.DoLogout());
			MainScene.FacebookLogIn.SetActive(true);
			MainScene.LeaderBoardInvite.SetActive(false);
			for (int i = 0; i < 10; i++)
			{
				if (i == 1)
				{
					this.FriendsObjects[i].Find("FB Invite").gameObject.SetActive(true);
					this.FriendsObjects[i].Find("Friend").gameObject.SetActive(false);
					this.FriendsObjects[i].Find("FB Invite/Coin Shop").gameObject.SetActive(false);
					this.FriendsObjects[i].Find("FB Invite/Invite").GetComponent<TextMesh>().text = LanguageManager.LogIn;
					this.FriendsObjects[i].Find("FB Invite/Invite").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
				}
				else
				{
					this.FriendsObjects[i].gameObject.SetActive(false);
				}
			}
		}
		else
		{
			CheckInternetConnection.Instance.openPopup();
		}
		yield break;
	}

	// Token: 0x06002C82 RID: 11394 RVA: 0x00021D02 File Offset: 0x0001FF02
	private IEnumerator checkConnectionForResetProgress()
	{
		base.StartCoroutine(CheckInternetConnection.Instance.checkInternetConnection());
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
			base.StartCoroutine(this.SacekajDaSePostaviScoreNaNulu());
		}
		else
		{
			CheckInternetConnection.Instance.openPopup();
		}
		yield break;
	}

	// Token: 0x06002C83 RID: 11395 RVA: 0x0015C6AC File Offset: 0x0015A8AC
	private void Update()
	{
		Input.GetKeyUp(27);
		if (Input.GetMouseButtonDown(0))
		{
			this.clickedItem = this.RaycastFunction(Input.mousePosition);
			if (this.clickedItem.Equals("NekoDugme") || this.clickedItem.Equals("ButtonSettings") || this.clickedItem.Equals("ButtonLeaderboard") || this.clickedItem.Equals("Custumization") || this.clickedItem.Equals("FreeCoins") || this.clickedItem.Equals("4 Reset Progres") || this.clickedItem.Equals("5 Reset Tutorials") || this.clickedItem.Equals("6 Log Out") || this.clickedItem.Equals("Button_CheckOK"))
			{
				this.temp = GameObject.Find(this.clickedItem);
				this.originalScale = this.temp.transform.localScale;
				this.temp.transform.localScale = this.originalScale * 1.2f;
			}
			else if (this.clickedItem.Equals("ClearAll"))
			{
				ShopManagerFull.ShopObject.transform.Find("3 Customize/Costumization BG/ClearAll/ClearAll_Selected").GetComponent<SpriteRenderer>().enabled = true;
			}
			else if (this.clickedItem != string.Empty)
			{
				this.temp = GameObject.Find(this.clickedItem);
				this.originalScale = this.temp.transform.localScale;
			}
		}
		if (Input.GetMouseButtonUp(0))
		{
			this.releasedItem = this.RaycastFunction(Input.mousePosition);
			if (!this.clickedItem.Equals(string.Empty))
			{
				if (this.temp != null)
				{
					this.temp.transform.localScale = this.originalScale;
				}
				if (ShopManagerFull.ShopObject.transform.Find("3 Customize/Costumization BG/ClearAll/ClearAll_Selected").GetComponent<SpriteRenderer>().enabled)
				{
					ShopManagerFull.ShopObject.transform.Find("3 Customize/Costumization BG/ClearAll/ClearAll_Selected").GetComponent<SpriteRenderer>().enabled = false;
				}
				if (this.clickedItem == this.releasedItem && this.releasedItem == "NekoDugme" && PlaySounds.soundOn)
				{
					PlaySounds.Play_Button_OpenLevel();
				}
				if (this.clickedItem == this.releasedItem && this.releasedItem == "Play")
				{
					if (PlaySounds.soundOn)
					{
						PlaySounds.Play_Button_OpenLevel();
					}
					this.format = CultureInfo.CurrentCulture.DateTimeFormat;
					string text = DateTime.Now.ToString(this.format.FullDateTimePattern);
					PlayerPrefs.SetString("VremeQuit", text);
					PlayerPrefs.SetFloat("VremeBrojaca", TimeReward.VremeBrojaca);
					PlayerPrefs.Save();
					GameObject.Find("Loading Animation Vrata").GetComponent<Animator>().Play("Loading Zidovi Dolazak");
					base.StartCoroutine(this.otvoriSledeciNivo());
				}
				if (this.clickedItem == this.releasedItem && this.releasedItem == "ButtonSettings")
				{
					GameObject.Find("Settings i Language Scena").GetComponent<Animation>().Play("SettingsDolazak");
					this.PrikaziSettings();
					if (PlaySounds.soundOn)
					{
						PlaySounds.Play_Button_OpenLevel();
						return;
					}
				}
				else
				{
					if (this.clickedItem == this.releasedItem && this.releasedItem == "ButtonFacebook")
					{
						FacebookManager.KorisnikoviPodaciSpremni = false;
						ShopManagerFull.ShopInicijalizovan = false;
						if (PlaySounds.soundOn)
						{
							PlaySounds.Play_Button_OpenLevel();
						}
						base.StartCoroutine(this.checkConnectionForLoginButton());
						return;
					}
					if (this.clickedItem == this.releasedItem && this.releasedItem == "ButtonMusic")
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
							return;
						}
						PlaySounds.musicOn = false;
						PlaySounds.Stop_BackgroundMusic_Menu();
						PlayerPrefs.SetInt("musicOn", 0);
						PlayerPrefs.Save();
						return;
					}
					else if (this.clickedItem == this.releasedItem && this.releasedItem == "ButtonSound")
					{
						if (!PlaySounds.soundOn)
						{
							PlaySounds.soundOn = true;
							PlaySounds.Play_Button_SoundOn();
							PlaySounds.Play_Button_SoundOn();
							PlayerPrefs.SetInt("soundOn", 1);
							PlayerPrefs.Save();
							return;
						}
						PlaySounds.soundOn = false;
						PlayerPrefs.SetInt("soundOn", 0);
						PlayerPrefs.Save();
						return;
					}
					else
					{
						if (this.clickedItem == this.releasedItem && this.releasedItem == "Zastava")
						{
							if (PlaySounds.soundOn)
							{
								PlaySounds.Play_Button_OpenLevel();
							}
							GameObject.Find("Settings i Language Scena").GetComponent<Animation>().Play("SettingsDolazak");
							this.PrikaziJezike();
							this.SettingsOtvoren = true;
							this.SettingState = 3;
							return;
						}
						if (this.clickedItem == this.releasedItem && this.releasedItem == "ButtonLeaderboard")
						{
							GameObject.Find("Leaderboard Scena").GetComponent<Animation>().Play("MeniDolazak");
							this.LeaderboardOtvoren = true;
							if (FB.IsLoggedIn)
							{
								base.Invoke("AktivirajLeaderboard", 1f);
							}
							if (PlaySounds.soundOn)
							{
								PlaySounds.Play_Button_OpenLevel();
								return;
							}
						}
						else if (this.clickedItem == this.releasedItem && this.releasedItem == "FreeCoins")
						{
							GameObject.Find("Shop").GetComponent<Animation>().Play("MeniDolazak");
							if (ShopManagerFull.AktivanRanac == 0)
							{
								GameObject.Find("MonkeyHolder").transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + ShopManagerFull.AktivanRanac).transform.GetChild(0).GetComponent<MeshFilter>().mesh = GameObject.Find("RefZaShop_AndjeoskaKrila").GetComponent<MeshFilter>().mesh;
							}
							else if (ShopManagerFull.AktivanRanac == 5)
							{
								GameObject.Find("MonkeyHolder").transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + ShopManagerFull.AktivanRanac).transform.GetChild(0).GetComponent<MeshFilter>().mesh = GameObject.Find("RefZaShop_SlepiMisKrila").GetComponent<MeshFilter>().mesh;
							}
							ShopManagerFull.ShopObject.PozoviTab(1);
							if (PlaySounds.soundOn)
							{
								PlaySounds.Play_Button_OpenLevel();
								return;
							}
						}
						else if (this.clickedItem == this.releasedItem && this.releasedItem == "Custumization")
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
								GameObject.Find("MonkeyHolder").transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + ShopManagerFull.AktivanRanac).transform.GetChild(0).GetComponent<MeshFilter>().mesh = GameObject.Find("RefZaShop_AndjeoskaKrila").GetComponent<MeshFilter>().mesh;
							}
							else if (ShopManagerFull.AktivanRanac == 5)
							{
								GameObject.Find("MonkeyHolder").transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + ShopManagerFull.AktivanRanac).transform.GetChild(0).GetComponent<MeshFilter>().mesh = GameObject.Find("RefZaShop_SlepiMisKrila").GetComponent<MeshFilter>().mesh;
							}
							ShopManagerFull.ShopObject.PozoviTab(3);
							if (PlaySounds.soundOn)
							{
								PlaySounds.Play_Button_OpenLevel();
								return;
							}
						}
						else if (this.clickedItem == this.releasedItem && this.releasedItem == "ButtonFreeCoins")
						{
							ShopManagerFull.ShopObject.PozoviTab(1);
							if (PlaySounds.soundOn)
							{
								PlaySounds.Play_Button_OpenLevel();
								return;
							}
						}
						else if (this.clickedItem == this.releasedItem && this.releasedItem == "ButtonShop")
						{
							ShopManagerFull.ShopObject.PozoviTab(2);
							if (PlaySounds.soundOn)
							{
								PlaySounds.Play_Button_OpenLevel();
								return;
							}
						}
						else if (this.clickedItem == this.releasedItem && this.releasedItem == "ButtonCustomize")
						{
							ShopManagerFull.ShopObject.PozoviTab(3);
							if (PlaySounds.soundOn)
							{
								PlaySounds.Play_Button_OpenLevel();
								return;
							}
						}
						else if (this.clickedItem == this.releasedItem && this.releasedItem == "ButtonPowerUps")
						{
							ShopManagerFull.ShopObject.PozoviTab(4);
							if (PlaySounds.soundOn)
							{
								PlaySounds.Play_Button_OpenLevel();
								return;
							}
						}
						else if (this.clickedItem == this.releasedItem && this.releasedItem == "ButtonBackShop")
						{
							ShopManagerFull.ShopObject.SkloniShop();
							if (PlaySounds.soundOn)
							{
								PlaySounds.Play_Button_OpenLevel();
								return;
							}
						}
						else if (this.clickedItem == this.releasedItem && this.releasedItem == "BackButtonLeaderboard")
						{
							this.LeaderboardOtvoren = false;
							GameObject.Find("Leaderboard Scena").GetComponent<Animation>().Play("MeniOdlazak");
							base.Invoke("DeaktivirajLeaderboard", 1f);
							if (PlaySounds.soundOn)
							{
								PlaySounds.Play_Button_OpenLevel();
								return;
							}
						}
						else if (this.clickedItem == this.releasedItem && this.releasedItem == "BackButtonSettings")
						{
							if (this.SettingState == 1)
							{
								GameObject.Find("Settings i Language Scena").GetComponent<Animation>().Play("SettingsOdlazak");
								this.SettingsOtvoren = false;
								base.Invoke("DeaktivirajSettings", 1f);
								this.ProveraZaLogoutZbogDugmica();
							}
							else if (this.SettingState == 2)
							{
								this.SettingState = 1;
								GameObject.Find("Settings i Language Scena/Settings Tabs").GetComponent<Animation>().Play("TabSettingsDolazak");
								GameObject.Find("Settings i Language Scena/Language Tabs").GetComponent<Animation>().Play("TabSettingsOdlazak");
								this.AktivirajSettings();
								if (LanguageManager.chosenLanguage != this.jezikPreUlaskaUPromenuJezika)
								{
									this.jezikPreUlaskaUPromenuJezika = LanguageManager.chosenLanguage;
									ShopManagerFull.ShopObject.RefresujImenaItema();
								}
							}
							else if (this.SettingState == 3)
							{
								this.SettingState = 1;
								this.SettingsOtvoren = false;
								GameObject.Find("Settings i Language Scena").GetComponent<Animation>().Play("SettingsOdlazak");
								GameObject.Find("Settings i Language Scena/Language Tabs").GetComponent<Animation>().Play("TabSettingsOdlazak");
								if (LanguageManager.chosenLanguage != this.jezikPreUlaskaUPromenuJezika)
								{
									this.jezikPreUlaskaUPromenuJezika = LanguageManager.chosenLanguage;
									ShopManagerFull.ShopObject.RefresujImenaItema();
								}
							}
							if (PlaySounds.soundOn)
							{
								PlaySounds.Play_Button_OpenLevel();
								return;
							}
						}
						else
						{
							if (this.clickedItem == this.releasedItem && this.releasedItem == "ButtonCollect")
							{
								PlayerPrefs.SetInt("ProveriVreme", 1);
								PlayerPrefs.Save();
								switch (DailyRewards.LevelReward)
								{
								case 1:
								{
									GameObject gameObject = GameObject.Find("CoinsReward");
									gameObject.transform.Find("Coins Number").GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
									gameObject.transform.Find("Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
									StagesParser.currentMoney += DailyRewards.DailyRewardAmount[0];
									PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
									PlayerPrefs.Save();
									this.dailyReward = DailyRewards.DailyRewardAmount[0];
									GameObject.Find("Day 1").transform.GetChild(0).Find("CollectCoinsDailyRewardParticles").GetComponent<ParticleSystem>().Play();
									GameObject.Find("Day 1").GetComponent<Collider>().enabled = false;
									GameObject.Find("ButtonCollect").GetComponent<Collider>().enabled = false;
									gameObject.GetComponent<Animation>().Play("CoinsRewardDolazak");
									base.Invoke("DelayZaOdbrojavanje", 1.15f);
									break;
								}
								case 2:
								{
									GameObject gameObject2 = GameObject.Find("CoinsReward");
									gameObject2.transform.Find("Coins Number").GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
									gameObject2.transform.Find("Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
									StagesParser.currentMoney += DailyRewards.DailyRewardAmount[1];
									PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
									PlayerPrefs.Save();
									this.dailyReward = DailyRewards.DailyRewardAmount[1];
									GameObject.Find("Day 2").transform.GetChild(0).Find("CollectCoinsDailyRewardParticles").GetComponent<ParticleSystem>().Play();
									GameObject.Find("Day 2").GetComponent<Collider>().enabled = false;
									GameObject.Find("ButtonCollect").GetComponent<Collider>().enabled = false;
									gameObject2.GetComponent<Animation>().Play("CoinsRewardDolazak");
									base.Invoke("DelayZaOdbrojavanje", 1.15f);
									break;
								}
								case 3:
								{
									GameObject gameObject3 = GameObject.Find("CoinsReward");
									gameObject3.transform.Find("Coins Number").GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
									gameObject3.transform.Find("Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
									StagesParser.currentMoney += DailyRewards.DailyRewardAmount[2];
									PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
									PlayerPrefs.Save();
									this.dailyReward = DailyRewards.DailyRewardAmount[2];
									GameObject.Find("Day 3").transform.GetChild(0).Find("CollectCoinsDailyRewardParticles").GetComponent<ParticleSystem>().Play();
									GameObject.Find("Day 3").GetComponent<Collider>().enabled = false;
									GameObject.Find("ButtonCollect").GetComponent<Collider>().enabled = false;
									gameObject3.GetComponent<Animation>().Play("CoinsRewardDolazak");
									base.Invoke("DelayZaOdbrojavanje", 1.15f);
									break;
								}
								case 4:
								{
									GameObject gameObject4 = GameObject.Find("CoinsReward");
									gameObject4.transform.Find("Coins Number").GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
									gameObject4.transform.Find("Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
									StagesParser.currentMoney += DailyRewards.DailyRewardAmount[3];
									PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
									PlayerPrefs.Save();
									this.dailyReward = DailyRewards.DailyRewardAmount[3];
									GameObject.Find("Day 4").transform.GetChild(0).Find("CollectCoinsDailyRewardParticles").GetComponent<ParticleSystem>().Play();
									GameObject.Find("Day 4").GetComponent<Collider>().enabled = false;
									GameObject.Find("ButtonCollect").GetComponent<Collider>().enabled = false;
									gameObject4.GetComponent<Animation>().Play("CoinsRewardDolazak");
									base.Invoke("DelayZaOdbrojavanje", 1.15f);
									break;
								}
								case 5:
								{
									GameObject gameObject5 = GameObject.Find("CoinsReward");
									gameObject5.transform.Find("Coins Number").GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
									gameObject5.transform.Find("Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
									StagesParser.currentMoney += DailyRewards.DailyRewardAmount[4];
									PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
									PlayerPrefs.Save();
									this.dailyReward = DailyRewards.DailyRewardAmount[4];
									GameObject.Find("Day 5").transform.GetChild(0).Find("CollectCoinsDailyRewardParticles").GetComponent<ParticleSystem>().Play();
									GameObject.Find("Day 5").GetComponent<Collider>().enabled = false;
									GameObject.Find("ButtonCollect").GetComponent<Collider>().enabled = false;
									gameObject5.GetComponent<Animation>().Play("CoinsRewardDolazak");
									base.Invoke("DelayZaOdbrojavanje", 1.15f);
									break;
								}
								case 6:
									GameObject.Find("Day 6 - Magic Box").GetComponent<Collider>().enabled = false;
									GameObject.Find("ButtonCollect").GetComponent<Collider>().enabled = false;
									this.MysteryBox();
									break;
								}
								if (PlaySounds.soundOn)
								{
									PlaySounds.Play_Button_OpenLevel();
								}
								StagesParser.ServerUpdate = 1;
								return;
							}
							if (this.clickedItem == this.releasedItem && this.releasedItem == "Day 1")
							{
								if (DailyRewards.LevelReward == 1)
								{
									GameObject gameObject6 = GameObject.Find("CoinsReward");
									gameObject6.transform.Find("Coins Number").GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
									gameObject6.transform.Find("Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
									StagesParser.currentMoney += DailyRewards.DailyRewardAmount[0];
									PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
									PlayerPrefs.Save();
									this.dailyReward = DailyRewards.DailyRewardAmount[0];
									GameObject.Find("Day 1").transform.GetChild(0).Find("CollectCoinsDailyRewardParticles").GetComponent<ParticleSystem>().Play();
									GameObject.Find("Day 1").GetComponent<Collider>().enabled = false;
									GameObject.Find("ButtonCollect").GetComponent<Collider>().enabled = false;
									gameObject6.GetComponent<Animation>().Play("CoinsRewardDolazak");
									base.Invoke("DelayZaOdbrojavanje", 1.15f);
								}
								if (PlaySounds.soundOn)
								{
									PlaySounds.Play_Button_OpenLevel();
									return;
								}
							}
							else if (this.clickedItem == this.releasedItem && this.releasedItem == "Day 2")
							{
								if (DailyRewards.LevelReward == 2)
								{
									GameObject gameObject7 = GameObject.Find("CoinsReward");
									gameObject7.transform.Find("Coins Number").GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
									gameObject7.transform.Find("Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
									StagesParser.currentMoney += DailyRewards.DailyRewardAmount[1];
									PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
									PlayerPrefs.Save();
									this.dailyReward = DailyRewards.DailyRewardAmount[1];
									GameObject.Find("Day 2").transform.GetChild(0).Find("CollectCoinsDailyRewardParticles").GetComponent<ParticleSystem>().Play();
									GameObject.Find("Day 2").GetComponent<Collider>().enabled = false;
									GameObject.Find("ButtonCollect").GetComponent<Collider>().enabled = false;
									gameObject7.GetComponent<Animation>().Play("CoinsRewardDolazak");
									base.Invoke("DelayZaOdbrojavanje", 1.15f);
								}
								if (PlaySounds.soundOn)
								{
									PlaySounds.Play_Button_OpenLevel();
									return;
								}
							}
							else if (this.clickedItem == this.releasedItem && this.releasedItem == "Day 3")
							{
								if (DailyRewards.LevelReward == 3)
								{
									GameObject gameObject8 = GameObject.Find("CoinsReward");
									gameObject8.transform.Find("Coins Number").GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
									gameObject8.transform.Find("Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
									StagesParser.currentMoney += DailyRewards.DailyRewardAmount[2];
									PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
									PlayerPrefs.Save();
									this.dailyReward = DailyRewards.DailyRewardAmount[2];
									GameObject.Find("Day 3").transform.GetChild(0).Find("CollectCoinsDailyRewardParticles").GetComponent<ParticleSystem>().Play();
									GameObject.Find("Day 3").GetComponent<Collider>().enabled = false;
									GameObject.Find("ButtonCollect").GetComponent<Collider>().enabled = false;
									gameObject8.GetComponent<Animation>().Play("CoinsRewardDolazak");
									base.Invoke("DelayZaOdbrojavanje", 1.15f);
								}
								if (PlaySounds.soundOn)
								{
									PlaySounds.Play_Button_OpenLevel();
									return;
								}
							}
							else if (this.clickedItem == this.releasedItem && this.releasedItem == "Day 4")
							{
								if (DailyRewards.LevelReward == 4)
								{
									GameObject gameObject9 = GameObject.Find("CoinsReward");
									gameObject9.transform.Find("Coins Number").GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
									gameObject9.transform.Find("Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
									StagesParser.currentMoney += DailyRewards.DailyRewardAmount[3];
									PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
									PlayerPrefs.Save();
									this.dailyReward = DailyRewards.DailyRewardAmount[3];
									GameObject.Find("Day 4").transform.GetChild(0).Find("CollectCoinsDailyRewardParticles").GetComponent<ParticleSystem>().Play();
									GameObject.Find("Day 4").GetComponent<Collider>().enabled = false;
									GameObject.Find("ButtonCollect").GetComponent<Collider>().enabled = false;
									gameObject9.GetComponent<Animation>().Play("CoinsRewardDolazak");
									base.Invoke("DelayZaOdbrojavanje", 1.15f);
								}
								if (PlaySounds.soundOn)
								{
									PlaySounds.Play_Button_OpenLevel();
									return;
								}
							}
							else if (this.clickedItem == this.releasedItem && this.releasedItem == "Day 5")
							{
								if (DailyRewards.LevelReward == 5)
								{
									GameObject gameObject10 = GameObject.Find("CoinsReward");
									gameObject10.transform.Find("Coins Number").GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
									gameObject10.transform.Find("Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
									StagesParser.currentMoney += DailyRewards.DailyRewardAmount[4];
									PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
									PlayerPrefs.Save();
									this.dailyReward = DailyRewards.DailyRewardAmount[4];
									GameObject.Find("Day 5").transform.GetChild(0).Find("CollectCoinsDailyRewardParticles").GetComponent<ParticleSystem>().Play();
									GameObject.Find("Day 5").GetComponent<Collider>().enabled = false;
									GameObject.Find("ButtonCollect").GetComponent<Collider>().enabled = false;
									gameObject10.GetComponent<Animation>().Play("CoinsRewardDolazak");
									base.Invoke("DelayZaOdbrojavanje", 1.15f);
								}
								if (PlaySounds.soundOn)
								{
									PlaySounds.Play_Button_OpenLevel();
									return;
								}
							}
							else if (this.clickedItem == this.releasedItem && this.releasedItem == "Day 6 - Magic Box")
							{
								if (DailyRewards.LevelReward == 6)
								{
									GameObject.Find("Day 6 - Magic Box").GetComponent<Collider>().enabled = false;
									GameObject.Find("ButtonCollect").GetComponent<Collider>().enabled = false;
									this.MysteryBox();
								}
								if (PlaySounds.soundOn)
								{
									PlaySounds.Play_Button_OpenLevel();
									return;
								}
							}
							else if (this.clickedItem == this.releasedItem && this.releasedItem == "1HatsShopTab")
							{
								ShopManagerFull.ShopObject.DeaktivirajCustomization();
								ShopManagerFull.AktivanItemSesir++;
								ShopManagerFull.ShopObject.PozoviCustomizationTab(1);
								if (PlaySounds.soundOn)
								{
									PlaySounds.Play_Button_OpenLevel();
									return;
								}
							}
							else if (this.clickedItem == this.releasedItem && this.releasedItem == "2TShirtsShopTab")
							{
								ShopManagerFull.ShopObject.DeaktivirajCustomization();
								ShopManagerFull.AktivanItemMajica++;
								ShopManagerFull.ShopObject.PozoviCustomizationTab(2);
								if (PlaySounds.soundOn)
								{
									PlaySounds.Play_Button_OpenLevel();
									return;
								}
							}
							else if (this.clickedItem == this.releasedItem && this.releasedItem == "3BackPackShopTab")
							{
								ShopManagerFull.ShopObject.DeaktivirajCustomization();
								ShopManagerFull.AktivanItemRanac++;
								ShopManagerFull.ShopObject.PozoviCustomizationTab(3);
								if (PlaySounds.soundOn)
								{
									PlaySounds.Play_Button_OpenLevel();
									return;
								}
							}
							else if (this.clickedItem == this.releasedItem && this.releasedItem.StartsWith("Hats"))
							{
								for (int i = 0; i < ShopManagerFull.ShopObject.HatsObjects.Length; i++)
								{
									if (this.releasedItem.StartsWith("Hats " + (i + 1)))
									{
										ObjCustomizationHats.swipeCtrl.currentValue = ShopManagerFull.ShopObject.HatsObjects.Length - i - 1;
									}
								}
								if (PlaySounds.soundOn)
								{
									PlaySounds.Play_Button_OpenLevel();
									return;
								}
							}
							else if (this.clickedItem == this.releasedItem && this.releasedItem.StartsWith("Shirts"))
							{
								for (int j = 0; j < ShopManagerFull.ShopObject.ShirtsObjects.Length; j++)
								{
									if (this.releasedItem.StartsWith("Shirts " + (j + 1)))
									{
										ObjCustomizationShirts.swipeCtrl.currentValue = ShopManagerFull.ShopObject.ShirtsObjects.Length - j - 1;
									}
								}
								if (PlaySounds.soundOn)
								{
									PlaySounds.Play_Button_OpenLevel();
									return;
								}
							}
							else if (this.clickedItem == this.releasedItem && this.releasedItem.StartsWith("BackPacks"))
							{
								for (int k = 0; k < ShopManagerFull.ShopObject.BackPacksObjects.Length; k++)
								{
									if (this.releasedItem.StartsWith("BackPacks " + (k + 1)))
									{
										ObjCustomizationBackPacks.swipeCtrl.currentValue = ShopManagerFull.ShopObject.BackPacksObjects.Length - k - 1;
									}
								}
								if (PlaySounds.soundOn)
								{
									PlaySounds.Play_Button_OpenLevel();
									return;
								}
							}
							else if (this.clickedItem == this.releasedItem && this.releasedItem == "ClearAll")
							{
								ShopManagerFull.ShopObject.OcistiMajmuna();
								if (PlaySounds.soundOn)
								{
									PlaySounds.Play_Button_OpenLevel();
									return;
								}
							}
							else if (this.clickedItem == this.releasedItem && this.releasedItem == "Kovceg")
							{
								GameObject gameObject11 = GameObject.Find("Kovceg");
								gameObject11.GetComponent<Collider>().enabled = false;
								GameObject gameObject12 = GameObject.Find("CoinsReward");
								gameObject12.transform.Find("Coins Number").GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
								gameObject12.transform.Find("Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
								gameObject11.GetComponent<TimeReward>().PokupiNagradu();
								gameObject11.GetComponent<Animator>().Play("Kovceg Collect Animation Click");
								gameObject11.transform.Find("PARTIKLI za Kada Se Klikne Collect/CFXM3 Spikes").GetComponent<ParticleSystem>().Play();
								gameObject11.transform.Find("PARTIKLI za Kada Se Klikne Collect/CollectCoinsParticles").GetComponent<ParticleSystem>().Play();
								gameObject12.GetComponent<Animation>().Play("CoinsRewardDolazak");
								if (PlaySounds.soundOn)
								{
									PlaySounds.Play_Button_OpenLevel();
									return;
								}
							}
							else if (this.clickedItem == this.releasedItem && this.releasedItem == "Shop Banana")
							{
								ShopManagerFull.ShopObject.KupiBananu();
								if (PlaySounds.soundOn)
								{
									PlaySounds.Play_Button_OpenLevel();
									return;
								}
							}
							else if (this.clickedItem == this.releasedItem && this.releasedItem.StartsWith("ShopInApp"))
							{
								string text2 = this.releasedItem;
								string text3 = this.releasedItem;
								uint num = <PrivateImplementationDetails>.ComputeStringHash(text3);
								if (num <= 1820370873U)
								{
									if (num <= 1346063021U)
									{
										if (num == 1900171U)
										{
											text3 == "ShopInAppPackMedium";
											return;
										}
										if (num == 77184888U)
										{
											text3 == "ShopInAppStarter";
											return;
										}
										if (num != 1346063021U)
										{
											return;
										}
										text3 == "ShopInAppRestore";
										return;
									}
									else
									{
										if (num == 1424005466U)
										{
											text3 == "ShopInAppPackMonster";
											return;
										}
										if (num == 1452445829U)
										{
											text3 == "ShopInAppRemoveAds";
											return;
										}
										if (num != 1820370873U)
										{
											return;
										}
										text3 == "ShopInAppPackGiant";
										return;
									}
								}
								else if (num <= 3405301491U)
								{
									if (num == 3078620075U)
									{
										text3 == "ShopInAppBananaMedium";
										return;
									}
									if (num == 3184985931U)
									{
										text3 == "ShopInAppPackSmall";
										return;
									}
									if (num != 3405301491U)
									{
										return;
									}
									text3 == "ShopInAppPackLarge";
									return;
								}
								else
								{
									if (num == 4017012715U)
									{
										text3 == "ShopInAppBananaSmall";
										return;
									}
									if (num == 4195751571U)
									{
										text3 == "ShopInAppBananaLarge";
										return;
									}
									if (num != 4207975553U)
									{
										return;
									}
									text3 == "ShopInAppBobosChoice";
									return;
								}
							}
							else if (this.clickedItem == this.releasedItem && this.releasedItem == "Shop POWERUP Double Coins")
							{
								ShopManagerFull.ShopObject.KupiDoubleCoins();
								if (PlaySounds.soundOn)
								{
									PlaySounds.Play_Button_OpenLevel();
									return;
								}
							}
							else if (this.clickedItem == this.releasedItem && this.releasedItem == "Shop POWERUP Magnet")
							{
								ShopManagerFull.ShopObject.KupiMagnet();
								if (PlaySounds.soundOn)
								{
									PlaySounds.Play_Button_OpenLevel();
									return;
								}
							}
							else if (this.clickedItem == this.releasedItem && this.releasedItem == "Shop POWERUP Shield")
							{
								ShopManagerFull.ShopObject.KupiShield();
								if (PlaySounds.soundOn)
								{
									PlaySounds.Play_Button_OpenLevel();
									return;
								}
							}
							else if (this.clickedItem == this.releasedItem && this.releasedItem == "FB Invite")
							{
								base.StartCoroutine(this.checkConnectionForLeaderboardLogin());
								if (PlaySounds.soundOn)
								{
									PlaySounds.Play_Button_OpenLevel();
									return;
								}
							}
							else if (this.clickedItem == this.releasedItem && this.releasedItem == "Preview Button")
							{
								if (ShopManagerFull.PreviewState)
								{
									ShopManagerFull.ShopObject.PreviewItem();
								}
								if (PlaySounds.soundOn)
								{
									PlaySounds.Play_Button_OpenLevel();
									return;
								}
							}
							else if (this.clickedItem == this.releasedItem && this.releasedItem == "Buy Button")
							{
								ShopManagerFull.ShopObject.KupiItem();
								if (PlaySounds.soundOn)
								{
									PlaySounds.Play_Button_OpenLevel();
									return;
								}
							}
							else if (this.clickedItem == this.releasedItem && this.releasedItem == "1 Language")
							{
								GameObject.Find("Settings i Language Scena/Settings Tabs").GetComponent<Animation>().Play("TabSettingsOdlazak");
								this.PrikaziJezike();
								if (PlaySounds.soundOn)
								{
									PlaySounds.Play_Button_OpenLevel();
									return;
								}
							}
							else if (this.clickedItem == this.releasedItem && this.releasedItem == "2 Music")
							{
								if (PlaySounds.musicOn)
								{
									this.SettingsObjects[1].Find("Shop Tab Element Selected").GetComponent<SpriteRenderer>().enabled = true;
									PlaySounds.musicOn = false;
									PlayerPrefs.SetInt("musicOn", 0);
									PlayerPrefs.Save();
									PlaySounds.Stop_BackgroundMusic_Menu();
								}
								else
								{
									this.SettingsObjects[1].Find("Shop Tab Element Selected").GetComponent<SpriteRenderer>().enabled = false;
									PlaySounds.musicOn = true;
									PlayerPrefs.SetInt("musicOn", 1);
									PlayerPrefs.Save();
									PlaySounds.Play_BackgroundMusic_Menu();
								}
								if (PlaySounds.soundOn)
								{
									PlaySounds.Play_Button_OpenLevel();
									return;
								}
							}
							else if (this.clickedItem == this.releasedItem && this.releasedItem == "3 Sound")
							{
								if (PlaySounds.soundOn)
								{
									this.SettingsObjects[2].Find("Shop Tab Element Selected").GetComponent<SpriteRenderer>().enabled = true;
									PlaySounds.soundOn = false;
									PlayerPrefs.SetInt("soundOn", 0);
									PlayerPrefs.Save();
									return;
								}
								this.SettingsObjects[2].Find("Shop Tab Element Selected").GetComponent<SpriteRenderer>().enabled = false;
								PlaySounds.soundOn = true;
								PlayerPrefs.SetInt("soundOn", 1);
								PlayerPrefs.Save();
								if (PlaySounds.soundOn)
								{
									PlaySounds.Play_Button_OpenLevel();
									return;
								}
							}
							else
							{
								if (this.clickedItem == this.releasedItem && this.releasedItem == "4 Reset Progres")
								{
									if (PlaySounds.soundOn)
									{
										PlaySounds.Play_Button_OpenLevel();
									}
									this.SettingsObjects[3].GetComponent<Collider>().enabled = false;
									this.SettingsObjects[3].Find("Shop Tab Element Selected").GetComponent<SpriteRenderer>().enabled = true;
									this.ResetProgress();
									return;
								}
								if (this.clickedItem == this.releasedItem && this.releasedItem == "5 Reset Tutorials")
								{
									if (PlaySounds.soundOn)
									{
										PlaySounds.Play_Button_OpenLevel();
									}
									this.SettingsObjects[4].GetComponent<Collider>().enabled = false;
									this.SettingsObjects[4].Find("Shop Tab Element Selected").GetComponent<SpriteRenderer>().enabled = true;
									this.ResetTutorials();
									return;
								}
								if (this.clickedItem == this.releasedItem && this.releasedItem == "6 Log Out")
								{
									if (PlaySounds.soundOn)
									{
										PlaySounds.Play_Button_OpenLevel();
									}
									base.StartCoroutine(this.checkConnectionForLogout());
									return;
								}
								if (this.clickedItem == this.releasedItem && this.releasedItem == "1")
								{
									if (!PlayerPrefs.HasKey("JezikPromenjen"))
									{
										PlayerPrefs.SetInt("JezikPromenjen", 1);
										PlayerPrefs.Save();
									}
									this.PromeniZastavu(1);
									StagesParser.ServerUpdate = 1;
									return;
								}
								if (this.clickedItem == this.releasedItem && this.releasedItem == "2")
								{
									if (!PlayerPrefs.HasKey("JezikPromenjen"))
									{
										PlayerPrefs.SetInt("JezikPromenjen", 1);
										PlayerPrefs.Save();
									}
									this.PromeniZastavu(2);
									StagesParser.ServerUpdate = 1;
									return;
								}
								if (this.clickedItem == this.releasedItem && this.releasedItem == "3")
								{
									if (!PlayerPrefs.HasKey("JezikPromenjen"))
									{
										PlayerPrefs.SetInt("JezikPromenjen", 1);
										PlayerPrefs.Save();
									}
									this.PromeniZastavu(3);
									StagesParser.ServerUpdate = 1;
									return;
								}
								if (this.clickedItem == this.releasedItem && this.releasedItem == "4")
								{
									if (!PlayerPrefs.HasKey("JezikPromenjen"))
									{
										PlayerPrefs.SetInt("JezikPromenjen", 1);
										PlayerPrefs.Save();
									}
									this.PromeniZastavu(4);
									StagesParser.ServerUpdate = 1;
									return;
								}
								if (this.clickedItem == this.releasedItem && this.releasedItem == "5")
								{
									if (!PlayerPrefs.HasKey("JezikPromenjen"))
									{
										PlayerPrefs.SetInt("JezikPromenjen", 1);
										PlayerPrefs.Save();
									}
									this.PromeniZastavu(5);
									StagesParser.ServerUpdate = 1;
									return;
								}
								if (this.clickedItem == this.releasedItem && this.releasedItem == "6")
								{
									if (!PlayerPrefs.HasKey("JezikPromenjen"))
									{
										PlayerPrefs.SetInt("JezikPromenjen", 1);
										PlayerPrefs.Save();
									}
									this.PromeniZastavu(6);
									StagesParser.ServerUpdate = 1;
									return;
								}
								if (this.clickedItem == this.releasedItem && this.releasedItem == "7")
								{
									if (!PlayerPrefs.HasKey("JezikPromenjen"))
									{
										PlayerPrefs.SetInt("JezikPromenjen", 1);
										PlayerPrefs.Save();
									}
									this.PromeniZastavu(7);
									StagesParser.ServerUpdate = 1;
									return;
								}
								if (this.clickedItem == this.releasedItem && this.releasedItem == "8")
								{
									if (!PlayerPrefs.HasKey("JezikPromenjen"))
									{
										PlayerPrefs.SetInt("JezikPromenjen", 1);
										PlayerPrefs.Save();
									}
									this.PromeniZastavu(8);
									StagesParser.ServerUpdate = 1;
									return;
								}
								if (this.clickedItem == this.releasedItem && this.releasedItem == "9")
								{
									if (!PlayerPrefs.HasKey("JezikPromenjen"))
									{
										PlayerPrefs.SetInt("JezikPromenjen", 1);
										PlayerPrefs.Save();
									}
									this.PromeniZastavu(9);
									StagesParser.ServerUpdate = 1;
									return;
								}
								if (this.clickedItem == this.releasedItem && this.releasedItem == "10")
								{
									if (!PlayerPrefs.HasKey("JezikPromenjen"))
									{
										PlayerPrefs.SetInt("JezikPromenjen", 1);
										PlayerPrefs.Save();
									}
									this.PromeniZastavu(10);
									StagesParser.ServerUpdate = 1;
									return;
								}
								if (this.clickedItem == this.releasedItem && this.releasedItem == "11")
								{
									if (!PlayerPrefs.HasKey("JezikPromenjen"))
									{
										PlayerPrefs.SetInt("JezikPromenjen", 1);
										PlayerPrefs.Save();
									}
									this.PromeniZastavu(11);
									StagesParser.ServerUpdate = 1;
									return;
								}
								if (this.clickedItem == this.releasedItem && this.releasedItem == "12")
								{
									if (!PlayerPrefs.HasKey("JezikPromenjen"))
									{
										PlayerPrefs.SetInt("JezikPromenjen", 1);
										PlayerPrefs.Save();
									}
									this.PromeniZastavu(12);
									StagesParser.ServerUpdate = 1;
									return;
								}
								if (this.clickedItem == this.releasedItem && this.releasedItem == "13")
								{
									if (!PlayerPrefs.HasKey("JezikPromenjen"))
									{
										PlayerPrefs.SetInt("JezikPromenjen", 1);
										PlayerPrefs.Save();
									}
									this.PromeniZastavu(13);
									StagesParser.ServerUpdate = 1;
									return;
								}
								if (this.clickedItem == this.releasedItem && this.releasedItem == "14")
								{
									if (!PlayerPrefs.HasKey("JezikPromenjen"))
									{
										PlayerPrefs.SetInt("JezikPromenjen", 1);
										PlayerPrefs.Save();
									}
									this.PromeniZastavu(14);
									StagesParser.ServerUpdate = 1;
									return;
								}
								if (this.clickedItem == this.releasedItem && this.releasedItem == "15")
								{
									if (!PlayerPrefs.HasKey("JezikPromenjen"))
									{
										PlayerPrefs.SetInt("JezikPromenjen", 1);
										PlayerPrefs.Save();
									}
									this.PromeniZastavu(15);
									StagesParser.ServerUpdate = 1;
									return;
								}
								if (this.clickedItem == this.releasedItem && this.releasedItem == "Exit Button")
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
									return;
								}
								if (this.clickedItem == this.releasedItem && this.releasedItem == "Button_CheckOK")
								{
									base.StartCoroutine(CheckInternetConnection.Instance.ClosePopup());
									return;
								}
								if (this.clickedItem == this.releasedItem && this.releasedItem == "ShopFCBILikePage")
								{
									base.StartCoroutine(this.checkConnectionForPageLike("https://www.facebook.com/pages/Banana-Island/636650059721490", "BananaIsland"));
									return;
								}
								if (this.clickedItem == this.releasedItem && this.releasedItem == "ShopFCWatchVideo")
								{
									base.StartCoroutine(this.checkConnectionForWatchVideo());
									return;
								}
								if (this.clickedItem == this.releasedItem && this.releasedItem == "ShopFCWLLikePage")
								{
									base.StartCoroutine(this.checkConnectionForPageLike("https://www.facebook.com/WebelinxGamesApps", "Webelinx"));
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06002C84 RID: 11396 RVA: 0x00149A14 File Offset: 0x00147C14
	private string RaycastFunction(Vector3 vector)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(vector), ref raycastHit))
		{
			return raycastHit.collider.name;
		}
		return "";
	}

	// Token: 0x06002C85 RID: 11397 RVA: 0x00021D11 File Offset: 0x0001FF11
	public void AktivirajLeaderboard()
	{
		ObjLeaderboard.Leaderboard = true;
		SwipeControlLeaderboard.controlEnabled = true;
	}

	// Token: 0x06002C86 RID: 11398 RVA: 0x00021D1F File Offset: 0x0001FF1F
	public void DeaktivirajLeaderboard()
	{
		ObjLeaderboard.Leaderboard = false;
		SwipeControlLeaderboard.controlEnabled = false;
	}

	// Token: 0x06002C87 RID: 11399 RVA: 0x0015ED9C File Offset: 0x0015CF9C
	public void OcistiLeaderboard()
	{
		Transform transform = MainScene.LeaderBoardInvite.transform.parent.Find("Friends Tabs");
		for (int i = 0; i < transform.childCount; i++)
		{
			transform.GetChild(i).Find("Friend/LeaderboardYou").GetComponent<SpriteRenderer>().enabled = false;
		}
		FacebookManager.FacebookObject.BrojPrijatelja = 0;
		FacebookManager.FacebookObject.Korisnici.Clear();
		FacebookManager.FacebookObject.Scorovi.Clear();
		FacebookManager.FacebookObject.Imena.Clear();
		FacebookManager.ProfileSlikePrijatelja.Clear();
		FacebookManager.ListaStructPrijatelja.Clear();
	}

	// Token: 0x06002C88 RID: 11400 RVA: 0x00021D2D File Offset: 0x0001FF2D
	public void AktivirajSettings()
	{
		ObjSettingsTabs.SettingsTabs = true;
		SwipeControlSettingsTabs.controlEnabled = true;
	}

	// Token: 0x06002C89 RID: 11401 RVA: 0x00021D3B File Offset: 0x0001FF3B
	public void DeaktivirajSettings()
	{
		ObjSettingsTabs.SettingsTabs = false;
		SwipeControlSettingsTabs.controlEnabled = false;
	}

	// Token: 0x06002C8A RID: 11402 RVA: 0x00021D49 File Offset: 0x0001FF49
	public void AktivirajLanguages()
	{
		ObjLanguages.Languages = true;
		SwipeControlLanguages.controlEnabled = true;
	}

	// Token: 0x06002C8B RID: 11403 RVA: 0x00021D57 File Offset: 0x0001FF57
	public void DeaktivirajLanguages()
	{
		ObjLanguages.Languages = false;
		SwipeControlLanguages.controlEnabled = false;
	}

	// Token: 0x06002C8C RID: 11404 RVA: 0x0015EE3C File Offset: 0x0015D03C
	public void PrikaziJezike()
	{
		this.SettingState = 2;
		this.DeaktivirajSettings();
		base.Invoke("AktivirajLanguages", 1f);
		this.jezikPreUlaskaUPromenuJezika = LanguageManager.chosenLanguage;
		GameObject.Find("Settings i Language Scena/Language Tabs").GetComponent<Animation>().Play("TabSettingsDolazak");
	}

	// Token: 0x06002C8D RID: 11405 RVA: 0x0015EE8C File Offset: 0x0015D08C
	public void PrikaziSettings()
	{
		if (FB.IsLoggedIn)
		{
			this.SettingsObjects[5].GetComponent<Collider>().enabled = true;
			this.SettingsObjects[5].Find("Shop Tab Element Selected").GetComponent<SpriteRenderer>().enabled = false;
		}
		else
		{
			this.SettingsObjects[5].GetComponent<Collider>().enabled = false;
			this.SettingsObjects[5].Find("Shop Tab Element Selected").GetComponent<SpriteRenderer>().enabled = true;
		}
		this.SettingState = 1;
		this.SettingsOtvoren = true;
		this.DeaktivirajLanguages();
		base.Invoke("AktivirajSettings", 1f);
		GameObject.Find("Settings i Language Scena/Settings Tabs").GetComponent<Animation>().Play("TabSettingsDolazak");
	}

	// Token: 0x06002C8E RID: 11406 RVA: 0x0015EF40 File Offset: 0x0015D140
	public void PromeniZastavuNaOsnovuImena()
	{
		if (!StagesParser.languageBefore.Equals(LanguageManager.chosenLanguage))
		{
			StagesParser.jezikPromenjen = 1;
			PlayerPrefs.SetInt("JezikPromenjen", 1);
			PlayerPrefs.Save();
		}
		int num = 0;
		if (StagesParser.jezikPromenjen != 0 || (GameObject.FindGameObjectWithTag("Zastava").GetComponent<Renderer>().material.mainTexture.name.Equals("0") && !LanguageManager.chosenLanguage.Equals("_en")))
		{
			string chosenLanguage = LanguageManager.chosenLanguage;
			uint num2 = <PrivateImplementationDetails>.ComputeStringHash(chosenLanguage);
			if (num2 <= 3673310179U)
			{
				if (num2 <= 3623565702U)
				{
					if (num2 != 1904450637U)
					{
						if (num2 != 2677697495U)
						{
							if (num2 == 3623565702U)
							{
								if (chosenLanguage == "_es")
								{
									num = 3;
								}
							}
						}
						else if (chosenLanguage == "_srb")
						{
							num = 13;
						}
					}
					else if (chosenLanguage == "_tch")
					{
						num = 10;
					}
				}
				else if (num2 <= 3642756082U)
				{
					if (num2 != 3626228726U)
					{
						if (num2 == 3642756082U)
						{
							if (chosenLanguage == "_th")
							{
								num = 8;
							}
						}
					}
					else if (chosenLanguage == "_us")
					{
						num = 2;
					}
				}
				else if (num2 != 3659931059U)
				{
					if (num2 == 3673310179U)
					{
						if (chosenLanguage == "_it")
						{
							num = 12;
						}
					}
				}
				else if (chosenLanguage == "_ru")
				{
					num = 4;
				}
			}
			else if (num2 <= 4011863700U)
			{
				if (num2 <= 3874788702U)
				{
					if (num2 != 3741156130U)
					{
						if (num2 == 3874788702U)
						{
							if (chosenLanguage == "_fr")
							{
								num = 7;
							}
						}
					}
					else if (chosenLanguage == "_br")
					{
						num = 6;
					}
				}
				else if (num2 != 3979000010U)
				{
					if (num2 == 4011863700U)
					{
						if (chosenLanguage == "_tr")
						{
							num = 14;
						}
					}
				}
				else if (chosenLanguage == "_pt")
				{
					num = 5;
				}
			}
			else if (num2 <= 4110116653U)
			{
				if (num2 != 4092353296U)
				{
					if (num2 == 4110116653U)
					{
						if (chosenLanguage == "_en")
						{
							num = 1;
						}
					}
				}
				else if (chosenLanguage == "_ko")
				{
					num = 15;
				}
			}
			else if (num2 != 4211076557U)
			{
				if (num2 == 4260968129U)
				{
					if (chosenLanguage == "_de")
					{
						num = 11;
					}
				}
			}
			else if (chosenLanguage == "_ch")
			{
				num = 9;
			}
			Texture texture = Resources.Load("Zastave/" + num) as Texture;
			GameObject.FindGameObjectWithTag("Zastava").GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
		}
		LanguageManager.RefreshTexts();
		this.PrevediTekstove();
		CheckInternetConnection.Instance.refreshText();
		StagesParser.LoadingPoruke.Clear();
		StagesParser.RedniBrojSlike.Clear();
		StagesParser.Instance.UcitajLoadingPoruke();
	}

	// Token: 0x06002C8F RID: 11407 RVA: 0x0015F288 File Offset: 0x0015D488
	public void PromeniZastavu(int BrojZastave)
	{
		Texture texture = Resources.Load("Zastave/" + BrojZastave) as Texture;
		GameObject.FindGameObjectWithTag("Zastava").GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
		if (PlayerPrefs.HasKey("JezikPromenjen"))
		{
			GameObject.Find("Settings i Language Scena").transform.Find("Language Tabs/" + BrojZastave.ToString() + "/Shop Tab Element Selected").GetComponent<Renderer>().enabled = true;
		}
		if (this.selectedLanguage != 0 && this.selectedLanguage != BrojZastave)
		{
			GameObject.Find("Settings i Language Scena").transform.Find("Language Tabs/" + this.selectedLanguage.ToString() + "/Shop Tab Element Selected").GetComponent<Renderer>().enabled = false;
		}
		this.selectedLanguage = BrojZastave;
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
		this.PrevediTekstove();
		CheckInternetConnection.Instance.refreshText();
		StagesParser.LoadingPoruke.Clear();
		StagesParser.RedniBrojSlike.Clear();
		StagesParser.Instance.UcitajLoadingPoruke();
	}

	// Token: 0x06002C90 RID: 11408 RVA: 0x0015F4B0 File Offset: 0x0015D6B0
	private void PrevediTekstove()
	{
		GameObject.Find("Kovceg/Text/Collect").GetComponent<TextMesh>().text = LanguageManager.Collect;
		GameObject.Find("ButtonCollect/Text").GetComponent<TextMesh>().text = LanguageManager.Collect;
		GameObject.Find("ButtonCollect/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Home Scena Interface").transform.Find("FB HOLDER LogIn/ButtonFacebook/Log in").GetComponent<TextMesh>().text = LanguageManager.LogIn;
		GameObject.Find("Home Scena Interface").transform.Find("FB HOLDER LogIn/ButtonFacebook/Log in").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Zid Header Shop/Text").GetComponent<TextMesh>().text = LanguageManager.DailyReward;
		GameObject.Find("Zid Header Shop/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("ButtonFreeCoins/Text").GetComponent<TextMesh>().text = LanguageManager.FreeCoins;
		GameObject.Find("ButtonFreeCoins/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("ButtonCustomize/Text").GetComponent<TextMesh>().text = LanguageManager.Customize;
		GameObject.Find("ButtonCustomize/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("ButtonPowerUps/Text").GetComponent<TextMesh>().text = LanguageManager.PowerUps;
		GameObject.Find("ButtonPowerUps/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("ButtonShop/Text").GetComponent<TextMesh>().text = LanguageManager.Shop;
		GameObject.Find("ButtonShop/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Shop Banana/Text/Banana").GetComponent<TextMesh>().text = LanguageManager.Banana;
		GameObject.Find("Shop Banana/Text/Banana").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("ShopFCWatchVideo/Text/Watch Video").GetComponent<TextMesh>().text = LanguageManager.WatchVideo;
		GameObject.Find("ShopFCWatchVideo/Text/Watch Video").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("ShopFCBILikePage/Text/FollowUsOnFacebook").GetComponent<TextMesh>().text = LanguageManager.FollowUsOnFacebook;
		GameObject.Find("ShopFCBILikePage/Text/FollowUsOnFacebook").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("ShopFCWLLikePage/Text/FollowUsOnFacebook").GetComponent<TextMesh>().text = LanguageManager.FollowUsOnFacebook;
		GameObject.Find("ShopFCWLLikePage/Text/FollowUsOnFacebook").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("ButtonBuy").GetComponent<TextMesh>().text = LanguageManager.Buy;
		GameObject.Find("ButtonBuy").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Shop POWERUP Double Coins/Text/ime").GetComponent<TextMesh>().text = LanguageManager.DoubleCoins;
		GameObject.Find("Shop POWERUP Double Coins/Text/ime").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Shop POWERUP Magnet/Text/ime").GetComponent<TextMesh>().text = LanguageManager.CoinsMagnet;
		GameObject.Find("Shop POWERUP Magnet/Text/ime").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Shop POWERUP Shield/Text/ime").GetComponent<TextMesh>().text = LanguageManager.Shield;
		GameObject.Find("Shop POWERUP Shield/Text/ime").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("TextLanguage").GetComponent<TextMesh>().text = LanguageManager.Language;
		GameObject.Find("TextLanguage").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("TextSettings").GetComponent<TextMesh>().text = LanguageManager.Settings;
		GameObject.Find("TextSettings").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("TextLeaderboard").GetComponent<TextMesh>().text = LanguageManager.Leaderboard;
		GameObject.Find("TextLeaderboard").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Leaderboard Scena").transform.Find("FB Invite/Text/Invite And Earn").GetComponent<TextMesh>().text = LanguageManager.InviteAndEarn;
		GameObject.Find("Leaderboard Scena").transform.Find("FB Invite/Text/Invite And Earn").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Day 1/DailyRewordDayAnimationHolder/Text/Day").GetComponent<TextMesh>().text = LanguageManager.Day + " 1";
		GameObject.Find("Day 1/DailyRewordDayAnimationHolder/Text/Day").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Day 2/DailyRewordDayAnimationHolder/Text/Day").GetComponent<TextMesh>().text = LanguageManager.Day + " 2";
		GameObject.Find("Day 2/DailyRewordDayAnimationHolder/Text/Day").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Day 3/DailyRewordDayAnimationHolder/Text/Day").GetComponent<TextMesh>().text = LanguageManager.Day + " 3";
		GameObject.Find("Day 3/DailyRewordDayAnimationHolder/Text/Day").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Day 4/DailyRewordDayAnimationHolder/Text/Day").GetComponent<TextMesh>().text = LanguageManager.Day + " 4";
		GameObject.Find("Day 4/DailyRewordDayAnimationHolder/Text/Day").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Day 5/DailyRewordDayAnimationHolder/Text/Day").GetComponent<TextMesh>().text = LanguageManager.Day + " 5";
		GameObject.Find("Day 5/DailyRewordDayAnimationHolder/Text/Day").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Day 6 - Magic Box/DailyRewordDayAnimationHolder/Text/Day").GetComponent<TextMesh>().text = LanguageManager.Day + " 6";
		GameObject.Find("Day 6 - Magic Box/DailyRewordDayAnimationHolder/Text/Day").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		Transform transform = GameObject.Find("Settings Tabs").transform;
		transform.Find("1 Language/Text/Text").GetComponent<TextMesh>().text = LanguageManager.Language;
		transform.Find("1 Language/Text/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		transform.Find("2 Music/Text/Text").GetComponent<TextMesh>().text = LanguageManager.Music;
		transform.Find("2 Music/Text/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		transform.Find("3 Sound/Text/Text").GetComponent<TextMesh>().text = LanguageManager.Sound;
		transform.Find("3 Sound/Text/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		transform.Find("4 Reset Progres/Text/Text").GetComponent<TextMesh>().text = LanguageManager.ResetProgress;
		transform.Find("4 Reset Progres/Text/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		transform.Find("5 Reset Tutorials/Text/Text").GetComponent<TextMesh>().text = LanguageManager.ResetTutorials;
		transform.Find("5 Reset Tutorials/Text/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		transform.Find("6 Log Out/Text/Text").GetComponent<TextMesh>().text = LanguageManager.LogOut;
		transform.Find("6 Log Out/Text/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		transform = MainScene.LeaderBoardInvite.transform.parent.Find("Friends Tabs");
		for (int i = 0; i < 10; i++)
		{
			if (transform.GetChild(i).Find("FB Invite").gameObject.activeSelf)
			{
				transform.GetChild(i).Find("FB Invite/Invite").GetComponent<TextMesh>().text = LanguageManager.InviteFriendsAndEarn;
				transform.GetChild(i).Find("FB Invite/Invite").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			else
			{
				transform.GetChild(i).Find("FB Invite").gameObject.SetActive(true);
				transform.GetChild(i).Find("FB Invite/Invite").GetComponent<TextMesh>().text = LanguageManager.InviteFriendsAndEarn;
				transform.GetChild(i).Find("FB Invite/Invite").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
				transform.GetChild(i).Find("FB Invite").gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06002C91 RID: 11409 RVA: 0x0015FC3C File Offset: 0x0015DE3C
	public void addAvatar()
	{
		this.creatAvatar(10, 51, 100, new Vector3(-5f, 0f, 0f), new Vector3(0f, 0f, 80f));
		KBEngineApp.app.entity_id = 10;
		new Dictionary<int, int>();
	}

	// Token: 0x06002C92 RID: 11410 RVA: 0x0015FC90 File Offset: 0x0015DE90
	public void creatAvatar(int avaterID, int roleType, int HP_Max, Vector3 position, Vector3 direction)
	{
		KBEngineApp.app.Client_onCreatedProxies((ulong)((long)avaterID), avaterID, "Avatar");
		Avatar avatar = (Avatar)KBEngineApp.app.entities[avaterID];
		avatar.roleTypeCell = (uint)roleType;
		avatar.position = position;
		avatar.direction = direction;
		avatar.HP_Max = HP_Max;
		avatar.HP = HP_Max;
		avatar.ZiZhi = 24;
		avatar.LingGeng.Add(0);
		avatar.LingGeng.Add(1);
		avatar.LingGeng.Add(2);
	}

	// Token: 0x06002C93 RID: 11411 RVA: 0x00021D65 File Offset: 0x0001FF65
	private IEnumerator otvoriSledeciNivo()
	{
		yield return new WaitForSeconds(1.1f);
		if (StagesParser.odgledaoTutorial == 0)
		{
			StagesParser.loadingTip = 1;
			this.addAvatar();
			Application.LoadLevel("AllMaps");
		}
		else
		{
			StagesParser.vratioSeNaSvaOstrva = true;
			this.addAvatar();
			Application.LoadLevel("AllMaps");
		}
		yield break;
	}

	// Token: 0x06002C94 RID: 11412 RVA: 0x00021D74 File Offset: 0x0001FF74
	private void SkloniCoinsReward()
	{
		GameObject.Find("CoinsReward").GetComponent<Animation>().Play("CoinsRewardOdlazak");
		GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyOdlazak");
	}

	// Token: 0x06002C95 RID: 11413 RVA: 0x00021DAA File Offset: 0x0001FFAA
	private void DelayZaOdbrojavanje()
	{
		base.StartCoroutine(StagesParser.Instance.moneyCounter(this.dailyReward, GameObject.Find("CoinsReward/Coins Number").GetComponent<TextMesh>(), true));
		base.Invoke("SkloniCoinsReward", 1.2f);
	}

	// Token: 0x06002C96 RID: 11414 RVA: 0x0015FD14 File Offset: 0x0015DF14
	private void MysteryBox()
	{
		GameObject gameObject = GameObject.Find("Day 6 - Magic Box");
		gameObject.transform.GetChild(0).GetComponent<Animator>().Play("CollectDailyRewardMagicBox");
		Sprite sprite;
		if (StagesParser.powerup_magnets <= StagesParser.powerup_shields)
		{
			if (StagesParser.powerup_magnets <= StagesParser.powerup_doublecoins)
			{
				sprite = ShopManagerFull.ShopObject.transform.Find("4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Magnet/Plavi Bedz/Magnet Icon").GetComponent<SpriteRenderer>().sprite;
				if (StagesParser.powerup_magnets <= 10)
				{
					StagesParser.powerup_magnets += 3;
					gameObject.transform.GetChild(0).Find("Magic Box Reward HOLDER/Magic Box Reward/Kolicina").GetComponent<TextMesh>().text = "x 3";
					gameObject.transform.GetChild(0).Find("Magic Box Reward HOLDER/Magic Box Reward/Kolicina").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
				}
				else
				{
					StagesParser.powerup_magnets += 2;
					gameObject.transform.GetChild(0).Find("Magic Box Reward HOLDER/Magic Box Reward/Kolicina").GetComponent<TextMesh>().text = "x 2";
					gameObject.transform.GetChild(0).Find("Magic Box Reward HOLDER/Magic Box Reward/Kolicina").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
				}
			}
			else
			{
				sprite = ShopManagerFull.ShopObject.transform.Find("4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Double Coins/Plavi Bedz/Double Coins Icon").GetComponent<SpriteRenderer>().sprite;
				if (StagesParser.powerup_doublecoins <= 10)
				{
					StagesParser.powerup_doublecoins += 3;
					gameObject.transform.GetChild(0).Find("Magic Box Reward HOLDER/Magic Box Reward/Kolicina").GetComponent<TextMesh>().text = "x 3";
					gameObject.transform.GetChild(0).Find("Magic Box Reward HOLDER/Magic Box Reward/Kolicina").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
				}
				else
				{
					StagesParser.powerup_doublecoins += 2;
					gameObject.transform.GetChild(0).Find("Magic Box Reward HOLDER/Magic Box Reward/Kolicina").GetComponent<TextMesh>().text = "x 2";
					gameObject.transform.GetChild(0).Find("Magic Box Reward HOLDER/Magic Box Reward/Kolicina").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
				}
			}
		}
		else if (StagesParser.powerup_shields <= StagesParser.powerup_doublecoins)
		{
			sprite = ShopManagerFull.ShopObject.transform.Find("4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Shield/Plavi Bedz/Shield Icon").GetComponent<SpriteRenderer>().sprite;
			if (StagesParser.powerup_shields <= 10)
			{
				StagesParser.powerup_shields += 3;
				gameObject.transform.GetChild(0).Find("Magic Box Reward HOLDER/Magic Box Reward/Kolicina").GetComponent<TextMesh>().text = "x 3";
				gameObject.transform.GetChild(0).Find("Magic Box Reward HOLDER/Magic Box Reward/Kolicina").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
			}
			else
			{
				StagesParser.powerup_shields += 2;
				gameObject.transform.GetChild(0).Find("Magic Box Reward HOLDER/Magic Box Reward/Kolicina").GetComponent<TextMesh>().text = "x 2";
				gameObject.transform.GetChild(0).Find("Magic Box Reward HOLDER/Magic Box Reward/Kolicina").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
			}
		}
		else
		{
			sprite = ShopManagerFull.ShopObject.transform.Find("4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Double Coins/Plavi Bedz/Double Coins Icon").GetComponent<SpriteRenderer>().sprite;
			if (StagesParser.powerup_doublecoins <= 10)
			{
				StagesParser.powerup_doublecoins += 3;
				gameObject.transform.GetChild(0).Find("Magic Box Reward HOLDER/Magic Box Reward/Kolicina").GetComponent<TextMesh>().text = "x 3";
				gameObject.transform.GetChild(0).Find("Magic Box Reward HOLDER/Magic Box Reward/Kolicina").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
			}
			else
			{
				StagesParser.powerup_doublecoins += 2;
				gameObject.transform.GetChild(0).Find("Magic Box Reward HOLDER/Magic Box Reward/Kolicina").GetComponent<TextMesh>().text = "x 2";
				gameObject.transform.GetChild(0).Find("Magic Box Reward HOLDER/Magic Box Reward/Kolicina").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
			}
		}
		StagesParser.currentBananas++;
		PlayerPrefs.SetInt("TotalBananas", StagesParser.currentBananas);
		PlayerPrefs.SetString("PowerUps", string.Concat(new object[]
		{
			StagesParser.powerup_doublecoins,
			"#",
			StagesParser.powerup_magnets,
			"#",
			StagesParser.powerup_shields
		}));
		PlayerPrefs.Save();
		GameObject.Find("Double Coins Number/Number").GetComponent<TextMesh>().text = StagesParser.powerup_doublecoins.ToString();
		GameObject.Find("Double Coins Number/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Magnet Number/Number").GetComponent<TextMesh>().text = StagesParser.powerup_magnets.ToString();
		GameObject.Find("Magnet Number/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Shield Number/Number").GetComponent<TextMesh>().text = StagesParser.powerup_shields.ToString();
		GameObject.Find("Shield Number/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		gameObject.transform.GetChild(0).Find("Magic Box Reward HOLDER/Magic Box Reward").GetComponent<SpriteRenderer>().sprite = sprite;
		base.Invoke("SkloniDailyRewardsPosleMysteryBox", 4.5f);
	}

	// Token: 0x06002C97 RID: 11415 RVA: 0x00021DE3 File Offset: 0x0001FFE3
	private void SkloniDailyRewardsPosleMysteryBox()
	{
		GameObject.Find("DailyReward").GetComponent<Animation>().Play("DailyOdlazak");
		base.Invoke("UgasiMysteryBox", 2f);
	}

	// Token: 0x06002C98 RID: 11416 RVA: 0x00021E0F File Offset: 0x0002000F
	private void UgasiMysteryBox()
	{
		GameObject.Find("Day 6 - Magic Box").SetActive(false);
	}

	// Token: 0x06002C99 RID: 11417 RVA: 0x00160214 File Offset: 0x0015E414
	private void ResetProgress()
	{
		Transform transform = GameObject.Find("Loading Buffer HOLDER").transform;
		Transform transform2 = Camera.main.transform;
		transform.position = new Vector3(transform2.position.x, transform2.position.y, transform.position.z);
		transform.GetChild(0).gameObject.SetActive(true);
		transform.GetChild(0).GetComponent<Animator>().Play("LoadingBufferUlazAnimation");
		string text = "1#0#0";
		for (int i = 1; i < StagesParser.allLevels.Length; i++)
		{
			text = text + "_" + (i + 1).ToString() + "#-1#0";
		}
		StagesParser.allLevels = text.Split(new char[]
		{
			'_'
		});
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
			base.StartCoroutine(this.checkConnectionForResetProgress());
		}
		for (int k = 0; k < StagesParser.totalSets; k++)
		{
			StagesParser.trenutniNivoNaOstrvu[k] = 1;
			PlayerPrefs.SetInt("TrenutniNivoNaOstrvu" + k.ToString(), StagesParser.trenutniNivoNaOstrvu[k]);
		}
		PlayerPrefs.Save();
		StagesParser.RecountTotalStars();
		if (!FB.IsLoggedIn)
		{
			StagesParser.Instance.UgasiLoading();
		}
	}

	// Token: 0x06002C9A RID: 11418 RVA: 0x00021E21 File Offset: 0x00020021
	private IEnumerator SacekajDaSePostaviScoreNaNulu()
	{
		while (FacebookManager.FacebookObject.resetovanScoreNaNulu == 2)
		{
			yield return null;
		}
		FacebookManager.MestoPozivanjaLogina = 1;
		this.OcistiLeaderboard();
		FacebookManager.FacebookObject.GetFacebookFriendScores();
		yield break;
	}

	// Token: 0x06002C9B RID: 11419 RVA: 0x00160390 File Offset: 0x0015E590
	private void ResetTutorials()
	{
		StagesParser.odgledaoTutorial = 0;
		StagesParser.currStageIndex = 0;
		StagesParser.currSetIndex = 0;
		PlayerPrefs.SetString("OdgledaoTutorial", StagesParser.odgledaoTutorial.ToString() + "#" + StagesParser.otvaraoShopNekad.ToString());
		PlayerPrefs.Save();
	}

	// Token: 0x06002C9C RID: 11420 RVA: 0x00021E30 File Offset: 0x00020030
	private IEnumerator DoLogout()
	{
		while (!FacebookManager.FacebookObject.OKzaLogout)
		{
			yield return null;
		}
		FacebookManager.FacebookLogout();
		FacebookManager.FacebookObject.OKzaLogout = false;
		yield break;
	}

	// Token: 0x06002C9D RID: 11421 RVA: 0x001603DC File Offset: 0x0015E5DC
	private void ProveraZaLogoutZbogDugmica()
	{
		if (this.logoutKliknut)
		{
			this.logoutKliknut = false;
			if (!FB.IsLoggedIn)
			{
				if (PlayerPrefs.GetInt("Logovan") == 1)
				{
					PlayerPrefs.SetInt("Logovan", 0);
				}
				MainScene.FacebookLogIn.SetActive(true);
				MainScene.LeaderBoardInvite.SetActive(false);
				for (int i = 0; i < 10; i++)
				{
					if (i == 1)
					{
						this.FriendsObjects[i].Find("FB Invite").gameObject.SetActive(true);
						this.FriendsObjects[i].Find("Friend").gameObject.SetActive(false);
						this.FriendsObjects[i].Find("FB Invite/Coin Shop").gameObject.SetActive(false);
						this.FriendsObjects[i].Find("FB Invite/Invite").GetComponent<TextMesh>().text = LanguageManager.LogIn;
						this.FriendsObjects[i].Find("FB Invite/Invite").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
					}
					else
					{
						this.FriendsObjects[i].gameObject.SetActive(false);
					}
				}
				PlayerPrefs.DeleteKey("JezikPromenjen");
				PlayerPrefs.Save();
			}
		}
	}

	// Token: 0x040026EF RID: 9967
	public Transform[] FriendsObjects = new Transform[0];

	// Token: 0x040026F0 RID: 9968
	public Transform[] SettingsObjects = new Transform[0];

	// Token: 0x040026F1 RID: 9969
	public Transform[] LanguagesObjects = new Transform[0];

	// Token: 0x040026F2 RID: 9970
	public static GameObject LeaderBoardInvite;

	// Token: 0x040026F3 RID: 9971
	public static GameObject FacebookLogIn;

	// Token: 0x040026F4 RID: 9972
	private bool SettingsOtvoren;

	// Token: 0x040026F5 RID: 9973
	private bool LeaderboardOtvoren;

	// Token: 0x040026F6 RID: 9974
	private int SettingState = 1;

	// Token: 0x040026F7 RID: 9975
	private string releasedItem;

	// Token: 0x040026F8 RID: 9976
	private string clickedItem;

	// Token: 0x040026F9 RID: 9977
	private Vector3 originalScale;

	// Token: 0x040026FA RID: 9978
	private GameObject temp;

	// Token: 0x040026FB RID: 9979
	private DateTimeFormatInfo format;

	// Token: 0x040026FC RID: 9980
	private int selectedLanguage;

	// Token: 0x040026FD RID: 9981
	private int dailyReward;

	// Token: 0x040026FE RID: 9982
	private string jezikPreUlaskaUPromenuJezika;

	// Token: 0x040026FF RID: 9983
	private bool logoutKliknut;
}
