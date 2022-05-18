using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

// Token: 0x020006C6 RID: 1734
public class Manage : MonoBehaviour
{
	// Token: 0x17000342 RID: 834
	// (get) Token: 0x06002B54 RID: 11092 RVA: 0x000215B4 File Offset: 0x0001F7B4
	public static Manage Instance
	{
		get
		{
			if (Manage.instance == null)
			{
				Manage.instance = (Object.FindObjectOfType(typeof(Manage)) as Manage);
			}
			return Manage.instance;
		}
	}

	// Token: 0x06002B55 RID: 11093 RVA: 0x00150C7C File Offset: 0x0014EE7C
	private void Awake()
	{
		try
		{
			this.interstitial = new InterstitialAd(StagesParser.Instance.AdMobInterstitialID);
			this.request = new AdRequest.Builder().Build();
			this.interstitial.LoadAd(this.request);
		}
		catch
		{
			Debug.Log("AD NOT INITIALIZED");
		}
		Manage.instance = this;
		this.goScreen = base.transform.Find("GO screen").GetChild(0).gameObject;
		this.guiCamera = GameObject.Find("GUICamera").GetComponent<Camera>();
		this.starManager = base.GetComponent<SetRandomStarsManager>();
		this.player = GameObject.FindGameObjectWithTag("Monkey");
		this.playerController = this.player.GetComponent<MonkeyController2D>();
		this.coinsCollectedText = base.transform.Find("Gameplay Scena Interface/_TopLeft/Coins/CoinsGamePlayText").GetComponent<TextMesh>();
		this.pauseButton = base.transform.Find("Gameplay Scena Interface/_TopRight/Pause Button");
		Manage.pointsText = base.transform.Find("Gameplay Scena Interface/_TopLeft/PTS/PTS Number").GetComponent<TextMesh>();
		Manage.pointsEffects = Manage.pointsText.GetComponent<TextMeshEffects>();
		this.pauseScreenHolder = base.transform.Find("PAUSE HOLDER");
		this.FailedScreenHolder = base.transform.Find("Level Win_Lose SCENA/Popup za LOSE HOLDER");
		if (StagesParser.bonusLevel)
		{
			this.Win_CompletedScreenHolder = base.transform.Find("Level Win_Lose SCENA/Popup za WIN HOLDER_BONUS");
		}
		else
		{
			this.Win_CompletedScreenHolder = base.transform.Find("Level Win_Lose SCENA/Popup za WIN HOLDER");
		}
		this.star1 = GameObject.Find("Stars Polja 1");
		this.star2 = GameObject.Find("Stars Polja 2");
		this.star3 = GameObject.Find("Stars Polja 3");
		this.coinMagnet = this.player.transform.Find("CoinMagnet").gameObject;
		this.shield = GameObject.Find("Shield");
		this.shield.SetActive(false);
		this.rateHolder = base.transform.Find("RATE HOLDER/AnimationHolderGlavni/AnimationHolder/RATE Popup").transform;
		this.PickPowers = GameObject.Find("POWERS HOLDER").transform;
		this.powerCard_CoinX2 = GameObject.Find("Power_Double Coins Interface").transform;
		this.powerCard_Magnet = GameObject.Find("Power_Magnet Interface").transform;
		this.powerCard_Shield = GameObject.Find("Power_Shield Interface").transform;
		if (Camera.main.aspect < 1.5f)
		{
			Camera.main.orthographicSize = 18f;
		}
		else if (Camera.main.aspect > 1.5f)
		{
			Camera.main.orthographicSize = 15f;
		}
		else
		{
			Camera.main.orthographicSize = 16.5f;
		}
		if (PlaySounds.musicOn)
		{
			PlaySounds.Play_BackgroundMusic_Gameplay();
			if (PlaySounds.Level_Failed_Popup.isPlaying)
			{
				PlaySounds.Stop_Level_Failed_Popup();
			}
		}
		Manage.shouldRaycast = false;
		Manage.coinsCollected = 0;
		Manage.baboonsKilled = 0;
		Manage.fly_BaboonsKilled = 0;
		Manage.boomerang_BaboonsKilled = 0;
		Manage.gorillasKilled = 0;
		Manage.fly_GorillasKilled = 0;
		Manage.koplje_GorillasKilled = 0;
		Manage.points = 0;
		Manage.barrelsSmashed = 0;
		Manage.redDiamonds = 0;
		Manage.blueDiamonds = 0;
		Manage.greenDiamonds = 0;
		Manage.bananas = 0;
	}

	// Token: 0x06002B56 RID: 11094 RVA: 0x00150FA4 File Offset: 0x0014F1A4
	private void Start()
	{
		this.refreshText();
		if (Loading.Instance != null)
		{
			base.StartCoroutine(Loading.Instance.UcitanaScena(this.guiCamera, 1, 0.5f));
		}
		Manage.pauseEnabled = false;
		if (StagesParser.bonusLevel)
		{
			TextMesh component = this.goScreen.GetComponent<TextMesh>();
			component.text = component.text + "\n" + LanguageManager.BonusLevel;
		}
		else
		{
			TextMesh component2 = this.goScreen.GetComponent<TextMesh>();
			component2.text = string.Concat(new object[]
			{
				component2.text,
				"\n",
				LanguageManager.Level,
				" ",
				StagesParser.currStageIndex + 1
			});
		}
		this.goScreen.GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, false);
		this.powerCard_CoinX2.parent.Find("CoinsHolder/Coins/Coins Number").GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
		this.powerCard_CoinX2.parent.Find("CoinsHolder/Coins/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		this.powerCard_CoinX2.Find("Number").GetComponent<TextMesh>().text = StagesParser.powerup_doublecoins.ToString();
		this.powerCard_Magnet.Find("Number").GetComponent<TextMesh>().text = StagesParser.powerup_magnets.ToString();
		this.powerCard_Shield.Find("Number").GetComponent<TextMesh>().text = StagesParser.powerup_shields.ToString();
		this.powerCard_CoinX2.Find("Text/Cost Number").GetComponent<TextMesh>().text = StagesParser.cost_doublecoins.ToString();
		this.powerCard_CoinX2.Find("Text/Cost Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		this.powerCard_Magnet.Find("Text/Cost Number").GetComponent<TextMesh>().text = StagesParser.cost_magnet.ToString();
		this.powerCard_Magnet.Find("Text/Cost Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		this.powerCard_Shield.Find("Text/Cost Number").GetComponent<TextMesh>().text = StagesParser.cost_shield.ToString();
		this.powerCard_Shield.Find("Text/Cost Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		this.Win_CompletedScreenHolder.Find("FB Invite Large/Text/Number").GetComponent<TextMesh>().text = "+" + StagesParser.InviteReward;
		this.Win_CompletedScreenHolder.Find("FB Invite Large/Text/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, false, true);
		if (!StagesParser.bonusLevel)
		{
			this.Win_CompletedScreenHolder.Find("FB Share/Text/Number").GetComponent<TextMesh>().text = "+" + StagesParser.ShareReward;
			this.Win_CompletedScreenHolder.Find("FB Share/Text/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, false, true);
		}
		this.FailedScreenHolder.gameObject.SetActive(false);
		this.Win_CompletedScreenHolder.gameObject.SetActive(false);
		if (this.guiCamera.aspect <= 1.4f && MissionManager.NumberOfQuests == 3)
		{
			base.transform.Find("Gameplay Scena Interface").localScale = Vector3.one * 0.91f;
			base.transform.Find("Gameplay Scena Interface").localPosition = new Vector3(-1.2f, 13.5f, 5f);
			base.transform.Find("Gameplay Scena Interface/_TopMissions").localPosition = new Vector3(1.6f, 0f, 0f);
		}
		base.transform.Find("Gameplay Scena Interface/_TopLeft").position = new Vector3(this.guiCamera.ViewportToWorldPoint(Vector3.zero).x, base.transform.Find("Gameplay Scena Interface/_TopLeft").position.y, base.transform.Find("Gameplay Scena Interface/_TopLeft").position.z);
		base.transform.Find("Gameplay Scena Interface/_TopRight").position = new Vector3(this.guiCamera.ViewportToWorldPoint(Vector3.one).x, base.transform.Find("Gameplay Scena Interface/_TopRight").position.y, base.transform.Find("Gameplay Scena Interface/_TopRight").position.z);
		if (Application.loadedLevelName.Equals("_Tutorial Level") && PlayerPrefs.HasKey("VecPokrenuto"))
		{
			GameObject.Find("Banana_collect_Holder").SetActive(false);
		}
	}

	// Token: 0x06002B57 RID: 11095 RVA: 0x00151434 File Offset: 0x0014F634
	private void Update()
	{
		if (this.pocniVreme)
		{
			if (this.measureTime)
			{
				this.aktivnoVreme += 1f * Time.deltaTime;
			}
			if (this.aktivnoVreme >= 12f)
			{
				if (StagesParser.odgledaoTutorial == 0 && this.aktivnoVreme >= 35f && !this.postavljenFinish)
				{
					this.postavljenFinish = true;
					this.playerController.Finish();
				}
				if (this.aktivnoVreme >= 55f && this.povecanaTezina == 0)
				{
					this.povecanaTezina = 1;
					LevelFactory.instance.overallDifficulty = 11;
				}
				if (this.aktivnoVreme >= 70f && this.povecanaTezina == 1)
				{
					this.povecanaTezina = 2;
					LevelFactory.instance.overallDifficulty = 16;
				}
			}
			if (this.aktivnoVreme - this.timeElapsed >= 20f && this.stepenBrzine <= 7)
			{
				this.playerController.startSpeedX += 1f;
				this.playerController.maxSpeedX += 1f;
				this.playerController.majmun.GetComponent<Animator>().speed += 0.075f;
				this.timeElapsed = this.aktivnoVreme;
				this.stepenBrzine++;
				if (StagesParser.bossStage)
				{
					BossScript.Instance.maxSpeedX = this.playerController.maxSpeedX;
				}
			}
		}
		if (Input.GetKeyDown(32) && this.goScreen.activeSelf)
		{
			Time.timeScale = 1f;
			this.goScreen.transform.parent = base.transform;
			this.goScreen.SetActive(false);
			this.playerController.state = MonkeyController2D.State.running;
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Run();
			}
			this.pocniVreme = true;
			Manage.goTrenutak = Time.time;
		}
		else if (Input.GetKeyUp(27))
		{
			if (Manage.pauseEnabled && this.makniPopup == 0)
			{
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_Button_Pause();
				}
				PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
				PlayerPrefs.Save();
				if (Time.timeScale == 1f)
				{
					if (!this.neDozvoliPauzu)
					{
						Time.timeScale = 0f;
						base.StopAllCoroutines();
						this.pauseScreenHolder.GetChild(0).localPosition += new Vector3(0f, 35f, 0f);
						this.pauseScreenHolder.GetChild(0).GetComponent<Animator>().Play("OpenPopup");
						this.pauseButton.GetComponent<Collider>().enabled = false;
						this.neDozvoliPauzu = true;
					}
				}
				else
				{
					this.popupZaSpustanje = this.pauseScreenHolder.GetChild(0);
					base.Invoke("spustiPopup", 0.5f);
					this.pauseScreenHolder.GetChild(0).GetComponent<Animator>().Play("ClosePopup");
					Time.timeScale = 1f;
				}
			}
			else if (this.makniPopup == 1)
			{
				this.popupZaSpustanje = base.transform.Find("RATE HOLDER").GetChild(0);
				base.Invoke("spustiPopup", 0.5f);
				base.transform.Find("RATE HOLDER").GetChild(0).GetComponent<Animator>().Play("ClosePopup");
				this.kliknuoYes = 0;
				this.makniPopup = 0;
			}
			else if (this.makniPopup == 2)
			{
				this.popupZaSpustanje = base.transform.Find("WATCH VIDEO HOLDER");
				base.Invoke("spustiPopup", 0.5f);
				base.transform.Find("WATCH VIDEO HOLDER").GetChild(0).GetComponent<Animator>().Play("ClosePopup");
				this.makniPopup = 0;
			}
			else if (this.makniPopup == 3)
			{
				this.showFailedScreen();
				this.makniPopup = 0;
			}
			else if (this.makniPopup == 4)
			{
				this.makniPopup = 0;
				base.StartCoroutine(CheckInternetConnection.Instance.ClosePopup());
			}
			else if (this.makniPopup == 5)
			{
				this.makniPopup = 2;
				base.StartCoroutine(CheckInternetConnection.Instance.ClosePopup());
			}
		}
		if (Input.GetMouseButtonDown(0))
		{
			this.clickedItem = this.RaycastFunction(Input.mousePosition);
			if (this.clickedItem == "RateButtonNO" || this.clickedItem == "RateButtonYES" || this.clickedItem == "WatchButtonNO" || this.clickedItem == "WatchButtonYES" || this.clickedItem == "Button Buy Banana" || this.clickedItem == "Button Cancel" || this.clickedItem == "Button Play_Revive" || this.clickedItem == "Button Mapa_Win" || this.clickedItem == "Button Next_Win" || this.clickedItem == "Button Restart_Win" || this.clickedItem == "Button Home_Failed" || this.clickedItem == "Button Mapa_Failed" || this.clickedItem == "Button Restart_Failed" || this.clickedItem == "Menu Button_Pause" || this.clickedItem == "Play Button_Pause" || this.clickedItem == "Restart Button_Pause")
			{
				this.temp = GameObject.Find(this.clickedItem);
				this.originalScale = this.temp.transform.localScale;
				this.temp.transform.localScale = this.originalScale * 0.8f;
			}
			else if (this.clickedItem != string.Empty && this.clickedItem != "Buy Button")
			{
				this.temp = GameObject.Find(this.clickedItem);
				this.originalScale = this.temp.transform.localScale;
			}
		}
		if (Input.GetMouseButtonUp(0))
		{
			this.releasedItem = this.RaycastFunction(Input.mousePosition);
			if (this.clickedItem != string.Empty && this.clickedItem != "Buy Button" && this.temp != null)
			{
				this.temp.transform.localScale = this.originalScale;
			}
			if (this.clickedItem == this.releasedItem)
			{
				if (this.releasedItem == "GO screen")
				{
					if (StagesParser.bossStage)
					{
						BossScript.Instance.comeIntoTheWorld();
						this.goScreen.transform.parent.gameObject.SetActive(false);
						Manage.pauseEnabled = true;
						this.pocniVreme = true;
						Manage.goTrenutak = Time.time;
						return;
					}
					Time.timeScale = 1f;
					this.goScreen.transform.parent.gameObject.SetActive(false);
					this.playerController.state = MonkeyController2D.State.running;
					this.playerController.majmun.GetComponent<Animator>().SetBool("Run", true);
					if (PlaySounds.soundOn)
					{
						PlaySounds.Play_Run();
					}
					int currSetIndex = StagesParser.currSetIndex;
					int currStageIndex = StagesParser.currStageIndex;
					Manage.pauseEnabled = true;
					this.pocniVreme = true;
					Manage.goTrenutak = Time.time;
					Manage.shouldRaycast = false;
					if (!StagesParser.bonusLevel && MissionManager.missions[LevelFactory.level - 1].distance > 0 && StagesParser.odgledaoTutorial > 0)
					{
						this.playerController.measureDistance = true;
						this.playerController.misijaSaDistance = true;
					}
					if (StagesParser.odgledaoTutorial > 0 && !StagesParser.bonusLevel)
					{
						base.transform.Find("POWERS HOLDER").GetChild(0).GetComponent<Animator>().Play("PowerUpDolazak");
						base.Invoke("DeaktivirajPowerUpAnimator", 4f);
						StagesParser.brojIgranja++;
						return;
					}
				}
				else if (this.releasedItem == "Pause Button")
				{
					if (Manage.pauseEnabled)
					{
						if (PlaySounds.soundOn)
						{
							PlaySounds.Play_Button_Pause();
						}
						PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
						PlayerPrefs.Save();
						if (Time.timeScale != 1f)
						{
							this.popupZaSpustanje = this.pauseScreenHolder.GetChild(0);
							base.Invoke("spustiPopup", 0.5f);
							this.pauseScreenHolder.GetChild(0).GetComponent<Animator>().Play("ClosePopup");
							Time.timeScale = 1f;
							return;
						}
						if (!this.neDozvoliPauzu)
						{
							Time.timeScale = 0f;
							base.StopAllCoroutines();
							this.pauseScreenHolder.GetChild(0).localPosition += new Vector3(0f, 35f, 0f);
							this.pauseScreenHolder.GetChild(0).GetComponent<Animator>().Play("OpenPopup");
							this.neDozvoliPauzu = true;
							this.pauseButton.GetComponent<Collider>().enabled = false;
							return;
						}
					}
				}
				else if (this.releasedItem == "Menu Button_Pause")
				{
					if (StagesParser.bonusLevel)
					{
						StagesParser.bonusLevel = false;
						StagesParser.dodatnaProveraIzasaoIzBonusa = true;
					}
					this.playerController.GetComponent<Rigidbody2D>().isKinematic = true;
					if (PlaySounds.soundOn)
					{
						PlaySounds.Play_Button_GoBack();
					}
					if (StagesParser.odgledaoTutorial == 0)
					{
						StagesParser.nivoZaUcitavanje = 1;
						base.StartCoroutine(this.closeDoorAndPlay());
						Time.timeScale = 1f;
						return;
					}
					StagesParser.nivoZaUcitavanje = 4 + StagesParser.currSetIndex;
					base.StartCoroutine(this.closeDoorAndPlay());
					Time.timeScale = 1f;
					return;
				}
				else
				{
					if (this.releasedItem == "Play Button_Pause")
					{
						if (PlaySounds.soundOn)
						{
							PlaySounds.Play_Button_Pause();
						}
						this.popupZaSpustanje = this.pauseScreenHolder.GetChild(0);
						base.Invoke("spustiPopup", 0.5f);
						this.pauseScreenHolder.GetChild(0).GetComponent<Animator>().Play("ClosePopup");
						Time.timeScale = 1f;
						return;
					}
					if (this.releasedItem == "Restart Button_Pause")
					{
						if (PlaySounds.soundOn)
						{
							PlaySounds.Play_Button_RestartLevel();
						}
						this.playerController.GetComponent<Rigidbody2D>().isKinematic = true;
						StagesParser.nivoZaUcitavanje = Application.loadedLevel;
						base.StartCoroutine(this.closeDoorAndPlay());
						Time.timeScale = 1f;
						return;
					}
					if (this.releasedItem == "Button Mapa_Failed")
					{
						if (PlaySounds.soundOn)
						{
							PlaySounds.Play_Button_GoBack();
						}
						if (PlaySounds.BackgroundMusic_Gameplay.isPlaying)
						{
							PlaySounds.Stop_BackgroundMusic_Gameplay();
						}
						StagesParser.nemojDaAnimirasZvezdice = true;
						StagesParser.nivoZaUcitavanje = 4 + StagesParser.currSetIndex;
						base.StartCoroutine(this.closeDoorAndPlay());
						return;
					}
					if (this.releasedItem == "Button Home_Failed")
					{
						if (StagesParser.bonusLevel)
						{
							StagesParser.bonusLevel = false;
						}
						if (PlaySounds.soundOn)
						{
							PlaySounds.Play_Button_GoBack();
						}
						if (PlaySounds.BackgroundMusic_Gameplay.isPlaying)
						{
							PlaySounds.Stop_BackgroundMusic_Gameplay();
						}
						StagesParser.nivoZaUcitavanje = 1;
						base.StartCoroutine(this.closeDoorAndPlay());
						return;
					}
					if (this.releasedItem == "Button Play_Revive")
					{
						this.pauseButton.GetComponent<Collider>().enabled = true;
						this.measureTime = true;
						this.makniPopup = 0;
						if (this.keepPlayingCount == 0)
						{
							this.keepPlayingCount = 1;
						}
						if (StagesParser.currentBananas >= this.keepPlayingCount)
						{
							if (PlaySounds.soundOn)
							{
								PlaySounds.Play_Button_OpenLevel();
							}
							this.popupZaSpustanje = base.transform.Find("Keep Playing HOLDER");
							base.Invoke("spustiPopup", 0.5f);
							base.transform.Find("Keep Playing HOLDER").GetChild(0).GetComponent<Animator>().Play("ClosePopup");
							StagesParser.currentBananas -= this.keepPlayingCount;
							PlayerPrefs.SetInt("TotalBananas", StagesParser.currentBananas);
							PlayerPrefs.Save();
							base.transform.Find("Keep Playing HOLDER/AnimationHolderGlavni/AnimationHolder/Popup za Keep Playing/Bananas/Banana Number").GetComponent<TextMesh>().text = StagesParser.currentBananas.ToString();
							base.transform.Find("Keep Playing HOLDER/AnimationHolderGlavni/AnimationHolder/Popup za Keep Playing/Bananas/Banana Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
							this.playerController.SetInvincible();
							this.keepPlayingCount++;
							return;
						}
						base.transform.Find("Keep Playing HOLDER/AnimationHolderGlavni/AnimationHolder/Popup za Keep Playing/Bananas").GetComponent<Animation>().Play();
						return;
					}
					else
					{
						if (this.releasedItem.Equals("Button Cancel"))
						{
							this.showFailedScreen();
							return;
						}
						if (this.releasedItem.Equals("Button Buy Banana"))
						{
							if (StagesParser.currentMoney > StagesParser.bananaCost)
							{
								if (PlaySounds.soundOn)
								{
									PlaySounds.Play_CollectBanana();
								}
								StagesParser.currentMoney -= StagesParser.bananaCost;
								StagesParser.currentBananas++;
								PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
								PlayerPrefs.Save();
								PlayerPrefs.SetInt("TotalBananas", StagesParser.currentBananas);
								PlayerPrefs.Save();
								StagesParser.ServerUpdate = 1;
								base.transform.Find("Keep Playing HOLDER/AnimationHolderGlavni/AnimationHolder/Popup za Keep Playing/Bananas/Banana Number").GetComponent<TextMesh>().text = StagesParser.currentBananas.ToString();
								base.transform.Find("Keep Playing HOLDER/AnimationHolderGlavni/AnimationHolder/Popup za Keep Playing/Bananas/Banana Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
								base.StartCoroutine(StagesParser.Instance.moneyCounter(-StagesParser.bananaCost, base.transform.Find("Keep Playing HOLDER/AnimationHolderGlavni/AnimationHolder/Popup za Keep Playing/CoinsHolder/Coins/Coins Number").GetComponent<TextMesh>(), true));
								return;
							}
							base.transform.Find("Keep Playing HOLDER/AnimationHolderGlavni/AnimationHolder/Popup za Keep Playing/CoinsHolder/Coins").GetComponent<Animation>().Play();
							return;
						}
						else
						{
							if (this.releasedItem == "Button Restart_Failed")
							{
								if (PlaySounds.soundOn)
								{
									PlaySounds.Play_Button_RestartLevel();
								}
								StagesParser.nivoZaUcitavanje = Application.loadedLevel;
								base.StartCoroutine(this.closeDoorAndPlay());
								return;
							}
							if (this.releasedItem == "Button Mapa_Win")
							{
								if (PlaySounds.soundOn)
								{
									PlaySounds.Play_Button_GoBack();
								}
								if (StagesParser.odgledaoTutorial == 0)
								{
									StagesParser.loadingTip = 2;
									StagesParser.odgledaoTutorial = 1;
									PlayerPrefs.SetString("OdgledaoTutorial", StagesParser.odgledaoTutorial.ToString() + "#" + StagesParser.otvaraoShopNekad.ToString());
									if (!PlayerPrefs.HasKey("VecPokrenuto"))
									{
										PlayerPrefs.SetInt("VecPokrenuto", 1);
									}
									PlayerPrefs.Save();
								}
								else if (StagesParser.odgledaoTutorial == 1)
								{
									StagesParser.odgledaoTutorial = 2;
									PlayerPrefs.SetString("OdgledaoTutorial", StagesParser.odgledaoTutorial.ToString() + "#" + StagesParser.otvaraoShopNekad.ToString());
									PlayerPrefs.Save();
								}
								if (StagesParser.currSetIndex == 5 && StagesParser.currStageIndex == 19)
								{
									StagesParser.nivoZaUcitavanje = 18;
									base.StartCoroutine(this.closeDoorAndPlay());
									return;
								}
								if (StagesParser.isJustOpened)
								{
									StagesParser.isJustOpened = false;
									StagesParser.currStageIndex = 0;
									StagesParser.currSetIndex = StagesParser.lastUnlockedWorldIndex;
									StagesParser.worldToFocus = StagesParser.currSetIndex;
									StagesParser.nivoZaUcitavanje = 3;
									base.StartCoroutine(this.closeDoorAndPlay());
									return;
								}
								StagesParser.nivoZaUcitavanje = 3;
								base.StartCoroutine(this.closeDoorAndPlay());
								return;
							}
							else if (this.releasedItem == "Button Next_Win")
							{
								if (PlaySounds.soundOn)
								{
									PlaySounds.Play_Button_NextLevel();
								}
								if (StagesParser.otvaraoShopNekad == 0)
								{
									StagesParser.otvaraoShopNekad = 2;
									PlayerPrefs.SetString("OdgledaoTutorial", StagesParser.odgledaoTutorial.ToString() + "#" + StagesParser.otvaraoShopNekad.ToString());
									PlayerPrefs.Save();
								}
								if (StagesParser.odgledaoTutorial == 0)
								{
									StagesParser.loadingTip = 2;
									StagesParser.odgledaoTutorial = 1;
									PlayerPrefs.SetString("OdgledaoTutorial", StagesParser.odgledaoTutorial.ToString() + "#" + StagesParser.otvaraoShopNekad.ToString());
									if (!PlayerPrefs.HasKey("VecPokrenuto"))
									{
										PlayerPrefs.SetInt("VecPokrenuto", 1);
									}
									PlayerPrefs.Save();
								}
								else if (StagesParser.odgledaoTutorial == 1)
								{
									StagesParser.odgledaoTutorial = 2;
									PlayerPrefs.SetString("OdgledaoTutorial", StagesParser.odgledaoTutorial.ToString() + "#" + StagesParser.otvaraoShopNekad.ToString());
									PlayerPrefs.Save();
								}
								if (StagesParser.bonusLevel)
								{
									StagesParser.bonusLevel = false;
									StagesParser.nivoZaUcitavanje = 4 + StagesParser.currSetIndex;
									base.StartCoroutine(this.closeDoorAndPlay());
								}
								if (StagesParser.currSetIndex == 5 && StagesParser.currStageIndex == 19)
								{
									StagesParser.nivoZaUcitavanje = 18;
									base.StartCoroutine(this.closeDoorAndPlay());
									return;
								}
								if (StagesParser.isJustOpened)
								{
									StagesParser.nemojDaAnimirasZvezdice = true;
									StagesParser.isJustOpened = false;
									StagesParser.currStageIndex = 0;
									StagesParser.currSetIndex = StagesParser.lastUnlockedWorldIndex;
									StagesParser.worldToFocus = StagesParser.currSetIndex;
									StagesParser.nivoZaUcitavanje = 3;
									base.StartCoroutine(this.closeDoorAndPlay());
									return;
								}
								if (StagesParser.NemaRequiredStars_VratiULevele)
								{
									StagesParser.nivoZaUcitavanje = 4 + StagesParser.currSetIndex;
									base.StartCoroutine(this.closeDoorAndPlay());
									return;
								}
							}
							else
							{
								if (this.releasedItem == "Button Restart_Win")
								{
									if (PlaySounds.soundOn)
									{
										PlaySounds.Play_Button_RestartLevel();
									}
									if (StagesParser.odgledaoTutorial == 0)
									{
										if (!PlayerPrefs.HasKey("VecPokrenuto"))
										{
											PlayerPrefs.SetInt("VecPokrenuto", 1);
										}
										PlayerPrefs.Save();
									}
									StagesParser.nivoZaUcitavanje = Application.loadedLevel;
									base.StartCoroutine(this.closeDoorAndPlay());
									return;
								}
								if (this.releasedItem == "RateButtonNO")
								{
									if (PlaySounds.soundOn)
									{
										PlaySounds.Play_Button_OpenLevel();
									}
									this.popupZaSpustanje = base.transform.Find("RATE HOLDER").GetChild(0);
									base.Invoke("spustiPopup", 0.5f);
									base.transform.Find("RATE HOLDER").GetChild(0).GetComponent<Animator>().Play("ClosePopup");
									this.kliknuoYes = 0;
									return;
								}
								if (this.releasedItem == "WatchButtonYES")
								{
									if (PlaySounds.soundOn)
									{
										PlaySounds.Play_Button_OpenLevel();
									}
									this.makniPopup = 5;
									base.StartCoroutine(this.checkConnectionForWatchVideo());
									return;
								}
								if (this.releasedItem == "WatchButtonNO")
								{
									if (PlaySounds.soundOn)
									{
										PlaySounds.Play_Button_OpenLevel();
									}
									this.popupZaSpustanje = base.transform.Find("WATCH VIDEO HOLDER");
									base.Invoke("spustiPopup", 0.5f);
									base.transform.Find("WATCH VIDEO HOLDER").GetChild(0).GetComponent<Animator>().Play("ClosePopup");
									return;
								}
								if (this.releasedItem == "Tutorial1_Screen")
								{
									if (TutorialEvents.postavljenCollider)
									{
										if (PlaySounds.soundOn)
										{
											PlaySounds.Play_Button_OpenLevel();
										}
										GameObject.Find(this.releasedItem).SetActive(false);
										Time.timeScale = 1f;
										Manage.pauseEnabled = true;
										return;
									}
								}
								else if (this.releasedItem == "Tutorial2_Screen")
								{
									if (TutorialEvents.postavljenCollider)
									{
										if (PlaySounds.soundOn)
										{
											PlaySounds.Play_Button_OpenLevel();
										}
										GameObject.Find(this.releasedItem).SetActive(false);
										Time.timeScale = 1f;
										Manage.pauseEnabled = true;
										return;
									}
								}
								else
								{
									if (this.releasedItem.Contains("Tutorial"))
									{
										GameObject.Find(this.releasedItem).transform.parent.parent.GetComponent<Animator>().Play("ClosePopup");
										Time.timeScale = 1f;
										Manage.pauseEnabled = true;
										return;
									}
									if (this.releasedItem == "Power_Double Coins Interface")
									{
										if (StagesParser.powerup_doublecoins > 0)
										{
											this.powerCard_CoinX2.GetComponent<Collider>().enabled = false;
											this.powerCard_CoinX2.Find("Disabled").GetComponent<SpriteRenderer>().enabled = true;
											StagesParser.powerup_doublecoins--;
											this.powerCard_CoinX2.Find("Number").GetComponent<TextMesh>().text = StagesParser.powerup_doublecoins.ToString();
											this.kupljenDoubleCoins = true;
											this.ApplyPowerUp(2);
											this.playerController.doublecoins = true;
											if (PlaySounds.soundOn)
											{
												PlaySounds.Play_CollectPowerUp();
											}
											PlayerPrefs.SetString("PowerUps", string.Concat(new object[]
											{
												StagesParser.powerup_doublecoins,
												"#",
												StagesParser.powerup_magnets,
												"#",
												StagesParser.powerup_shields
											}));
											PlayerPrefs.Save();
											return;
										}
										if (StagesParser.cost_doublecoins >= StagesParser.currentMoney)
										{
											this.powerCard_CoinX2.parent.Find("CoinsHolder/Coins").GetComponent<Animation>().Play();
											return;
										}
										this.powerCard_CoinX2.GetComponent<Collider>().enabled = false;
										this.powerCard_CoinX2.Find("Disabled").GetComponent<SpriteRenderer>().enabled = true;
										this.kupljenDoubleCoins = true;
										this.ApplyPowerUp(2);
										this.playerController.doublecoins = true;
										StagesParser.currentMoney -= StagesParser.cost_doublecoins;
										PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
										PlayerPrefs.Save();
										base.StartCoroutine(StagesParser.Instance.moneyCounter(-StagesParser.cost_doublecoins, this.powerCard_Magnet.parent.Find("CoinsHolder/Coins/Coins Number").GetComponent<TextMesh>(), true));
										if (PlaySounds.soundOn)
										{
											PlaySounds.Play_CollectPowerUp();
											return;
										}
									}
									else if (this.releasedItem == "Power_Magnet Interface")
									{
										if (StagesParser.powerup_magnets > 0)
										{
											this.powerCard_Magnet.GetComponent<Collider>().enabled = false;
											this.powerCard_Magnet.Find("Disabled").GetComponent<SpriteRenderer>().enabled = true;
											StagesParser.powerup_magnets--;
											this.powerCard_Magnet.Find("Number").GetComponent<TextMesh>().text = StagesParser.powerup_magnets.ToString();
											this.kupljenMagnet = true;
											this.ApplyPowerUp(1);
											this.playerController.magnet = true;
											if (PlaySounds.soundOn)
											{
												PlaySounds.Play_CollectPowerUp();
											}
											PlayerPrefs.SetString("PowerUps", string.Concat(new object[]
											{
												StagesParser.powerup_doublecoins,
												"#",
												StagesParser.powerup_magnets,
												"#",
												StagesParser.powerup_shields
											}));
											PlayerPrefs.Save();
											return;
										}
										if (StagesParser.cost_magnet >= StagesParser.currentMoney)
										{
											this.powerCard_Magnet.parent.Find("CoinsHolder/Coins").GetComponent<Animation>().Play();
											return;
										}
										this.powerCard_Magnet.GetComponent<Collider>().enabled = false;
										this.powerCard_Magnet.Find("Disabled").GetComponent<SpriteRenderer>().enabled = true;
										this.kupljenMagnet = true;
										this.ApplyPowerUp(1);
										this.playerController.magnet = true;
										StagesParser.currentMoney -= StagesParser.cost_magnet;
										PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
										PlayerPrefs.Save();
										base.StartCoroutine(StagesParser.Instance.moneyCounter(-StagesParser.cost_magnet, this.powerCard_Magnet.parent.Find("CoinsHolder/Coins/Coins Number").GetComponent<TextMesh>(), true));
										if (PlaySounds.soundOn)
										{
											PlaySounds.Play_CollectPowerUp();
											return;
										}
									}
									else if (this.releasedItem == "Power_Shield Interface")
									{
										if (StagesParser.powerup_shields > 0)
										{
											this.powerCard_Shield.GetComponent<Collider>().enabled = false;
											this.powerCard_Shield.Find("Disabled").GetComponent<SpriteRenderer>().enabled = true;
											StagesParser.powerup_shields--;
											this.powerCard_Shield.Find("Number").GetComponent<TextMesh>().text = StagesParser.powerup_shields.ToString();
											this.kupljenShield = true;
											this.ApplyPowerUp(3);
											this.playerController.activeShield = true;
											if (PlaySounds.soundOn)
											{
												PlaySounds.Play_CollectPowerUp();
											}
											PlayerPrefs.SetString("PowerUps", string.Concat(new object[]
											{
												StagesParser.powerup_doublecoins,
												"#",
												StagesParser.powerup_magnets,
												"#",
												StagesParser.powerup_shields
											}));
											PlayerPrefs.Save();
											return;
										}
										if (StagesParser.cost_shield >= StagesParser.currentMoney)
										{
											this.powerCard_Shield.parent.Find("CoinsHolder/Coins").GetComponent<Animation>().Play();
											return;
										}
										this.powerCard_Shield.GetComponent<Collider>().enabled = false;
										this.powerCard_Shield.Find("Disabled").GetComponent<SpriteRenderer>().enabled = true;
										this.kupljenShield = true;
										this.ApplyPowerUp(3);
										this.playerController.activeShield = true;
										StagesParser.currentMoney -= StagesParser.cost_shield;
										PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
										PlayerPrefs.Save();
										base.StartCoroutine(StagesParser.Instance.moneyCounter(-StagesParser.cost_shield, this.powerCard_Magnet.parent.Find("CoinsHolder/Coins/Coins Number").GetComponent<TextMesh>(), true));
										if (PlaySounds.soundOn)
										{
											PlaySounds.Play_CollectPowerUp();
											return;
										}
									}
									else
									{
										if (this.releasedItem.Contains("RateGame"))
										{
											if (PlaySounds.soundOn)
											{
												PlaySounds.Play_GetStar();
											}
											int num = int.Parse(this.releasedItem.Substring(8)) + 1;
											Transform transform = this.rateHolder.Find("RateGameHolder");
											for (int i = 0; i < transform.childCount; i++)
											{
												transform.GetChild(i).GetChild(0).GetComponent<Renderer>().enabled = false;
											}
											for (int j = 0; j < num; j++)
											{
												transform.Find("RateGame" + j.ToString()).GetChild(0).GetComponent<Renderer>().enabled = true;
											}
											if (num > 3)
											{
												base.StartCoroutine(this.checkConnectionForRate());
											}
											base.transform.Find("RATE HOLDER").GetChild(0).GetComponent<Animator>().Play("ClosePopup");
											return;
										}
										if (this.releasedItem.Equals("FB Share"))
										{
											this.makniPopup = 4;
											base.StartCoroutine(this.checkConnectionForShare());
											return;
										}
										if (this.releasedItem.Equals("FB Invite Large"))
										{
											this.makniPopup = 4;
											base.StartCoroutine(this.checkConnectionForInvite());
											return;
										}
										if (this.releasedItem.Contains("Friend Level"))
										{
											this.makniPopup = 4;
											base.StartCoroutine(this.checkConnectionForInvite());
											return;
										}
										if (this.releasedItem.Equals("Button_CheckOK"))
										{
											if (PlaySounds.soundOn)
											{
												PlaySounds.Play_Button_OpenLevel();
											}
											this.makniPopup = 0;
											base.StartCoroutine(CheckInternetConnection.Instance.ClosePopup());
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06002B58 RID: 11096 RVA: 0x000215E1 File Offset: 0x0001F7E1
	private IEnumerator RateGame(string url)
	{
		base.StartCoroutine(CheckInternetConnection.Instance.checkInternetConnection());
		while (!CheckInternetConnection.Instance.checkDone)
		{
			yield return null;
		}
		if (CheckInternetConnection.Instance.internetOK)
		{
			PlayerPrefs.SetInt("AlreadyRated", 1);
			PlayerPrefs.Save();
			Application.OpenURL(url);
		}
		else
		{
			CheckInternetConnection.Instance.openPopup();
		}
		yield break;
	}

	// Token: 0x06002B59 RID: 11097 RVA: 0x000215F7 File Offset: 0x0001F7F7
	private IEnumerator openingPage(string url)
	{
		WWW www = new WWW("http://www.google.com");
		yield return www;
		if (!string.IsNullOrEmpty(www.error))
		{
			this.rateHolder.Find("RateButtonYES/Text").GetComponent<TextMesh>().text = "Retry";
			this.rateHolder.Find("RateButtonYES/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
			this.rateHolder.Find("Text 2").GetComponent<TextMesh>().text = "No Internet\nConnection!";
			this.rateHolder.Find("Text 2").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		}
		else
		{
			this.popupZaSpustanje = base.transform.Find("RATE HOLDER").GetChild(0);
			base.Invoke("spustiPopup", 0.5f);
			base.transform.Find("RATE HOLDER").GetChild(0).GetComponent<Animator>().Play("ClosePopup");
			PlayerPrefs.SetInt("AlreadyRated", 1);
			PlayerPrefs.Save();
			Application.OpenURL(url);
		}
		yield break;
	}

	// Token: 0x06002B5A RID: 11098 RVA: 0x0002160D File Offset: 0x0001F80D
	private IEnumerator backToMenu()
	{
		base.StartCoroutine(GameObject.Find("Menu_Pause").GetComponent<Animation>().Play("ClickMenu", false, delegate(bool what)
		{
			this.helpBool = true;
		}));
		while (!this.helpBool)
		{
			yield return null;
		}
		this.helpBool = false;
		StagesParser.nivoZaUcitavanje = 4 + StagesParser.currSetIndex;
		base.StartCoroutine(this.closeDoorAndPlay());
		Time.timeScale = 1f;
		yield break;
	}

	// Token: 0x06002B5B RID: 11099 RVA: 0x0002161C File Offset: 0x0001F81C
	private IEnumerator restartLevel()
	{
		base.StartCoroutine(GameObject.Find("Restart_Pause").GetComponent<Animation>().Play("ClickRestart", false, delegate(bool what)
		{
			this.helpBool = true;
		}));
		while (!this.helpBool)
		{
			yield return null;
		}
		this.helpBool = false;
		StagesParser.nivoZaUcitavanje = Application.loadedLevel;
		base.StartCoroutine(this.closeDoorAndPlay());
		Time.timeScale = 1f;
		yield break;
	}

	// Token: 0x06002B5C RID: 11100 RVA: 0x0002162B File Offset: 0x0001F82B
	private IEnumerator unPause()
	{
		base.StartCoroutine(GameObject.Find("Play_Pause").GetComponent<Animation>().Play("ClickMenu", false, delegate(bool what)
		{
			this.helpBool = true;
		}));
		while (!this.helpBool)
		{
			yield return null;
		}
		this.helpBool = false;
		yield break;
	}

	// Token: 0x06002B5D RID: 11101 RVA: 0x00152DC0 File Offset: 0x00150FC0
	private void showFailedScreen()
	{
		if (PlaySounds.BackgroundMusic_Gameplay.isPlaying)
		{
			PlaySounds.Stop_BackgroundMusic_Gameplay();
		}
		if (PlaySounds.soundOn)
		{
			PlaySounds.Play_Level_Failed_Popup();
		}
		Manage.pauseEnabled = false;
		StagesParser.numberGotKilled++;
		if (StagesParser.numberGotKilled % 1 == 0)
		{
			StagesParser.numberGotKilled = 0;
		}
		this.FailedScreenHolder.parent.transform.position = new Vector3(this.guiCamera.transform.position.x, this.guiCamera.transform.position.y, this.FailedScreenHolder.transform.position.z);
		this.FailedScreenHolder.gameObject.SetActive(true);
		this.FailedScreenHolder.parent.GetComponent<Animator>().Play("LevelWinLoseDolazak");
		this.FailedScreenHolder.GetComponent<Animator>().Play("LevelLoseUlaz");
		PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
		PlayerPrefs.Save();
		try
		{
			if (this.interstitial.IsLoaded())
			{
				this.interstitial.Show();
			}
		}
		catch
		{
			Debug.Log("LEVEL FAILED - INTERSTITIAL NOT INITIALIZED");
		}
	}

	// Token: 0x06002B5E RID: 11102 RVA: 0x0002163A File Offset: 0x0001F83A
	private void OpaliPartikle()
	{
		this.Win_CompletedScreenHolder.parent.Find("Partikli Level Finish Win").gameObject.SetActive(true);
	}

	// Token: 0x06002B5F RID: 11103 RVA: 0x00152EF0 File Offset: 0x001510F0
	private void ShowWinScreen()
	{
		Manage.pauseEnabled = false;
		this.measureTime = false;
		if (StagesParser.bonusLevel)
		{
			base.transform.Find("Level Win_Lose SCENA/Popup za WIN HOLDER_BONUS/Popup za WIN/Header za win popup/Text/Level").GetComponent<TextMesh>().text = LanguageManager.BonusLevel;
			base.transform.Find("Level Win_Lose SCENA/Popup za WIN HOLDER_BONUS/Popup za WIN/Header za win popup/Text/Level").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			base.transform.Find("Level Win_Lose SCENA/Popup za WIN HOLDER_BONUS/Popup za WIN/Header za win popup/Text/Completed").GetComponent<TextMesh>().text = LanguageManager.Completed;
			base.transform.Find("Level Win_Lose SCENA/Popup za WIN HOLDER_BONUS/Popup za WIN/Header za win popup/Text/Completed").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			this.Win_CompletedScreenHolder.Find("Friends FB level WIN").gameObject.SetActive(false);
			this.Win_CompletedScreenHolder.Find("FB Share").gameObject.SetActive(false);
			this.Win_CompletedScreenHolder.gameObject.SetActive(true);
			if (!FB.IsLoggedIn)
			{
				this.Win_CompletedScreenHolder.Find("FB Invite Large").gameObject.SetActive(false);
			}
			if (PlaySounds.BackgroundMusic_Gameplay.isPlaying)
			{
				PlaySounds.Stop_BackgroundMusic_Gameplay();
			}
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Level_Completed_Popup();
			}
			this.Win_CompletedScreenHolder.parent.position = new Vector3(this.guiCamera.transform.position.x, this.guiCamera.transform.position.y, this.Win_CompletedScreenHolder.transform.position.z);
			this.Win_CompletedScreenHolder.parent.GetComponent<Animator>().Play("LevelWinLoseDolazak");
			this.Win_CompletedScreenHolder.GetComponent<Animator>().Play("LevelWinUlaz");
			base.Invoke("OpaliPartikle", 1.2f);
			StagesParser.currentMoney += Manage.coinsCollected;
			PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
			PlayerPrefs.Save();
			this.starManager.GoBack();
			base.StartCoroutine(this.WaitForSave());
			return;
		}
		this.Win_CompletedScreenHolder.Find("Popup za WIN/Header za win popup/Text/Level No").GetComponent<TextMesh>().text = (StagesParser.currStageIndex + 1).ToString();
		this.Win_CompletedScreenHolder.Find("Popup za WIN/Header za win popup/Text/Level No").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		StagesParser.currentMoney += Manage.coinsCollected;
		PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
		PlayerPrefs.Save();
		if (this.aktivnoVreme <= 40f)
		{
			Manage.points += MissionManager.points3Stars;
		}
		this.pointsForDisplay = Manage.points;
		int num = int.Parse(StagesParser.allLevels[StagesParser.currentLevel - 1].Split(new char[]
		{
			'#'
		})[2]);
		if (num >= Manage.points)
		{
			Manage.points = num;
		}
		else
		{
			int num2 = Manage.points - num;
			StagesParser.currentPoints += num2;
			PlayerPrefs.SetInt("TotalPoints", StagesParser.currentPoints);
			PlayerPrefs.Save();
		}
		if (!FB.IsLoggedIn)
		{
			this.Win_CompletedScreenHolder.Find("FB Invite Large").gameObject.SetActive(false);
			this.Win_CompletedScreenHolder.Find("FB Share").gameObject.SetActive(false);
			this.Win_CompletedScreenHolder.Find("Friends FB level WIN").gameObject.SetActive(false);
		}
		else
		{
			this.getFriendsScoresOnLevel(StagesParser.currentLevel);
		}
		this.Win_CompletedScreenHolder.gameObject.SetActive(true);
		if (PlaySounds.BackgroundMusic_Gameplay.isPlaying)
		{
			PlaySounds.Stop_BackgroundMusic_Gameplay();
		}
		if (PlaySounds.soundOn)
		{
			PlaySounds.Play_Level_Completed_Popup();
		}
		this.Win_CompletedScreenHolder.parent.position = new Vector3(this.guiCamera.transform.position.x, this.guiCamera.transform.position.y, this.Win_CompletedScreenHolder.transform.position.z);
		this.Win_CompletedScreenHolder.parent.GetComponent<Animator>().Play("LevelWinLoseDolazak");
		this.Win_CompletedScreenHolder.GetComponent<Animator>().Play("LevelWinUlaz");
		base.Invoke("OpaliPartikle", 1.2f);
		StagesParser.currentBananas += Manage.bananas;
		PlayerPrefs.SetInt("TotalBananas", StagesParser.currentBananas);
		PlayerPrefs.Save();
		StagesParser.ServerUpdate = 1;
		base.StartCoroutine(this.waitForStars());
	}

	// Token: 0x06002B60 RID: 11104 RVA: 0x0002165C File Offset: 0x0001F85C
	private IEnumerator waitForStars()
	{
		if (MissionManager.points3Stars > 0)
		{
			this.Win_CompletedScreenHolder.Find("Popup za WIN/ProgressBarHolder/3StarsPtsText").GetComponent<TextMesh>().text = MissionManager.points3Stars.ToString();
		}
		else
		{
			this.Win_CompletedScreenHolder.Find("Popup za WIN/ProgressBarHolder/3StarsPtsText").GetComponent<TextMesh>().text = "500";
		}
		this.Win_CompletedScreenHolder.Find("Popup za WIN/ProgressBarHolder/3StarsPtsText").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		StagesParser.saving = false;
		yield return new WaitForSeconds(2f);
		Transform progressBarPivot = this.Win_CompletedScreenHolder.Find("Popup za WIN/ProgressBarHolder/ProgressBarPivot");
		TextMesh scoreDisplay = this.Win_CompletedScreenHolder.Find("Popup za WIN/Polje za unos poena Na Level WIN/Points Number level win").GetComponent<TextMesh>();
		int currentProgressBarPoints = 0;
		bool starActivated = false;
		float targetScaleX = Mathf.Clamp01((float)this.pointsForDisplay / (float)MissionManager.points3Stars);
		this.star1.GetComponent<Animation>().Play();
		this.star1.transform.Find("Star Vatromet").gameObject.SetActive(true);
		this.starsGained = 1;
		if (PlaySounds.soundOn)
		{
			PlaySounds.Play_GetStar();
		}
		int step = (int)((float)this.pointsForDisplay * Time.deltaTime / 2f / targetScaleX);
		while (progressBarPivot.localScale.x < targetScaleX || currentProgressBarPoints <= this.pointsForDisplay)
		{
			if (progressBarPivot.localScale.x >= 0.7f && !starActivated)
			{
				starActivated = true;
				this.starsGained = 2;
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_GetStar2();
				}
				this.star2.GetComponent<Animation>().Play();
				this.star2.transform.Find("Star Vatromet").gameObject.SetActive(true);
			}
			currentProgressBarPoints += step;
			scoreDisplay.text = currentProgressBarPoints.ToString();
			scoreDisplay.GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
			if (currentProgressBarPoints > this.pointsForDisplay)
			{
				scoreDisplay.text = this.pointsForDisplay.ToString();
				scoreDisplay.GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
			}
			yield return null;
			progressBarPivot.localScale = new Vector3(Mathf.MoveTowards(progressBarPivot.localScale.x, targetScaleX, Time.deltaTime / 2f), progressBarPivot.localScale.y, progressBarPivot.localScale.z);
		}
		starActivated = false;
		if (progressBarPivot.localScale.x == 1f && !starActivated)
		{
			starActivated = true;
			this.starsGained = 3;
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_GetStar3();
			}
			this.star3.GetComponent<Animation>().Play();
			this.star3.transform.Find("Star Vatromet").gameObject.SetActive(true);
		}
		this.starManager.GoBack();
		base.StartCoroutine(this.WaitForSave());
		yield break;
	}

	// Token: 0x06002B61 RID: 11105 RVA: 0x0002166B File Offset: 0x0001F86B
	private IEnumerator waitForStars1()
	{
		StagesParser.saving = false;
		yield return new WaitForSeconds(2f);
		this.star1.GetComponent<Animation>().Play();
		this.star1.transform.Find("Star Vatromet").gameObject.SetActive(true);
		this.starsGained = 1;
		if (PlaySounds.soundOn)
		{
			PlaySounds.Play_GetStar();
		}
		yield return new WaitForSeconds(0.25f);
		if ((float)Manage.points >= (float)MissionManager.points3Stars * 0.7f)
		{
			this.starsGained = 2;
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_GetStar2();
			}
			this.star2.GetComponent<Animation>().Play();
			this.star2.transform.Find("Star Vatromet").gameObject.SetActive(true);
			yield return new WaitForSeconds(0.25f);
		}
		if (Manage.points >= MissionManager.points3Stars)
		{
			this.starsGained = 3;
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_GetStar3();
			}
			this.star3.GetComponent<Animation>().Play();
			this.star3.transform.Find("Star Vatromet").gameObject.SetActive(true);
		}
		this.starManager.GoBack();
		base.StartCoroutine(this.WaitForSave());
		yield break;
	}

	// Token: 0x06002B62 RID: 11106 RVA: 0x0002167A File Offset: 0x0001F87A
	private IEnumerator WaitForSave()
	{
		try
		{
			if (this.interstitial.IsLoaded())
			{
				this.interstitial.Show();
			}
			goto IL_5C;
		}
		catch
		{
			Debug.Log("LEVEL COMPLETED - INTERSTITIAL NOT INITIALIZED");
			goto IL_5C;
		}
		IL_45:
		yield return null;
		IL_5C:
		if (StagesParser.saving)
		{
			if (FB.IsLoggedIn)
			{
				FacebookManager.FacebookObject.scoreToSet = StagesParser.currentPoints;
				FacebookManager.FacebookObject.proveraPublish_ActionPermisije();
			}
			Transform transform = this.Win_CompletedScreenHolder.Find("Popup za WIN/Button Mapa_Win");
			Transform transform2 = this.Win_CompletedScreenHolder.Find("Popup za WIN/Button Next_Win");
			Component component = this.Win_CompletedScreenHolder.Find("Popup za WIN/Button Restart_Win");
			transform.GetComponent<Collider>().enabled = true;
			transform2.GetComponent<Collider>().enabled = true;
			component.GetComponent<Collider>().enabled = true;
			StagesParser.saving = false;
			if (!StagesParser.bonusLevel)
			{
				if (!PlayerPrefs.HasKey("AlreadyRated") && StagesParser.currStageIndex == 5)
				{
					base.transform.Find("RATE HOLDER").GetChild(0).localPosition += new Vector3(0f, 35f, 0f);
					base.transform.Find("RATE HOLDER").GetChild(0).GetComponent<Animator>().Play("OpenPopup");
					this.makniPopup = 1;
				}
				int num = StagesParser.currentLevel;
				num.ToString();
				if (PlayerPrefs.HasKey("PoslednjiOdigranNivo"))
				{
					if (PlayerPrefs.GetInt("PoslednjiOdigranNivo") >= num)
					{
						num = PlayerPrefs.GetInt("PoslednjiOdigranNivo");
					}
					else
					{
						PlayerPrefs.SetInt("PoslednjiOdigranNivo", num);
						PlayerPrefs.Save();
					}
				}
				else
				{
					PlayerPrefs.SetInt("PoslednjiOdigranNivo", num);
					PlayerPrefs.Save();
				}
			}
			yield break;
		}
		goto IL_45;
	}

	// Token: 0x06002B63 RID: 11107 RVA: 0x00153330 File Offset: 0x00151530
	public void UnlockWorld(int world)
	{
		Transform transform = base.transform.Find("WORLD UNLOCKED TURBO HOLDER");
		transform.Find("WORLD UNLOCKED HOLDER/AllWorldPicturesHolder/WorldBg" + world).gameObject.SetActive(true);
		transform.Find("WORLD UNLOCKED HOLDER/Number Holder/WorldNumber" + world).gameObject.SetActive(true);
		transform.position = Camera.main.transform.position + Vector3.forward * 2f;
		transform.localScale = transform.localScale * Camera.main.orthographicSize / 7.5f;
		transform.gameObject.SetActive(true);
	}

	// Token: 0x06002B64 RID: 11108 RVA: 0x00021689 File Offset: 0x0001F889
	private void CoinAdded()
	{
		if (this.PowerUp_doubleCoins)
		{
			Manage.coinsCollected += 2;
		}
		else
		{
			Manage.coinsCollected++;
		}
		this.coinsCollectedText.text = Manage.coinsCollected.ToString();
	}

	// Token: 0x06002B65 RID: 11109 RVA: 0x001533E8 File Offset: 0x001515E8
	private string RaycastFunction(Vector3 vector)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(this.guiCamera.ScreenPointToRay(vector), ref raycastHit))
		{
			return raycastHit.collider.name;
		}
		if (Physics.Raycast(Camera.main.ScreenPointToRay(vector), ref raycastHit))
		{
			return raycastHit.collider.name;
		}
		return "";
	}

	// Token: 0x06002B66 RID: 11110 RVA: 0x00153440 File Offset: 0x00151640
	public void ApplyPowerUp(int x)
	{
		if (x == 1)
		{
			base.CancelInvoke("pustiAnimacijuBlinkanja");
			base.transform.Find("Gameplay Scena Interface/_TopLeft/Magnet Icon Holder").GetComponent<Animation>().Play();
			base.Invoke("pustiAnimacijuBlinkanja", 17.5f);
			this.coinMagnet.SetActive(true);
			return;
		}
		if (x == -1)
		{
			this.coinMagnet.SetActive(false);
			base.CancelInvoke("pustiAnimacijuBlinkanja");
			base.transform.Find("Gameplay Scena Interface/_TopLeft/Magnet Icon Holder/Icon Animation").localScale = new Vector3(0.0001f, 0.0001f, 1f);
			return;
		}
		if (x == 2)
		{
			if (!this.PowerUp_doubleCoins)
			{
				base.transform.Find("Gameplay Scena Interface/_TopLeft/DoubleCoins Icon Holder").GetComponent<Animation>().Play();
				this.PowerUp_doubleCoins = true;
				LevelFactory.instance.doubleCoinsCollected = true;
				return;
			}
		}
		else
		{
			if (x == -2)
			{
				this.PowerUp_doubleCoins = false;
				base.transform.Find("Gameplay Scena Interface/_TopLeft/DoubleCoins Icon Holder/Icon Animation").localScale = new Vector3(0.0001f, 0.0001f, 1f);
				return;
			}
			if (x == 3)
			{
				this.PowerUp_shield = true;
				this.shield.SetActive(true);
				return;
			}
			if (x == -3)
			{
				this.PowerUp_shield = false;
				this.shield.SetActive(false);
			}
		}
	}

	// Token: 0x06002B67 RID: 11111 RVA: 0x000216C2 File Offset: 0x0001F8C2
	private void pustiAnimacijuBlinkanja()
	{
		if (!MissionManager.missionsComplete)
		{
			base.transform.Find("Gameplay Scena Interface/_TopLeft/Magnet Icon Holder").GetComponent<Animation>().Play("PowerUp Icon Disappear NEW");
			base.Invoke("UgasiMagnet", 1.5f);
		}
	}

	// Token: 0x06002B68 RID: 11112 RVA: 0x000216FB File Offset: 0x0001F8FB
	private void UgasiMagnet()
	{
		this.coinMagnet.SetActive(false);
	}

	// Token: 0x06002B69 RID: 11113 RVA: 0x00021709 File Offset: 0x0001F909
	private IEnumerator showPickPowers()
	{
		this.PickPowers.gameObject.SetActive(true);
		base.Invoke("DisappearPickPowers", 3.5f);
		yield return new WaitForSeconds(0.85f);
		this.powerCard_CoinX2.GetComponent<Animator>().enabled = true;
		this.powerCard_Magnet.GetComponent<Animator>().enabled = true;
		this.powerCard_Shield.GetComponent<Animator>().enabled = true;
		yield break;
	}

	// Token: 0x06002B6A RID: 11114 RVA: 0x0015357C File Offset: 0x0015177C
	private void DisappearPickPowers()
	{
		if (!this.kupljenMagnet)
		{
			this.powerCard_Magnet.GetComponent<Animator>().Play("PowerCardCoinx2_Disappear");
			this.powerCard_Magnet.GetComponent<Collider>().enabled = false;
		}
		if (!this.kupljenShield)
		{
			this.powerCard_Shield.GetComponent<Animator>().Play("PowerCardCoinx2_Disappear");
			this.powerCard_Shield.GetComponent<Collider>().enabled = false;
		}
		if (!this.kupljenDoubleCoins)
		{
			this.powerCard_CoinX2.GetComponent<Animator>().Play("PowerCardCoinx2_Disappear");
			this.powerCard_CoinX2.GetComponent<Collider>().enabled = false;
		}
		this.PickPowers.parent = null;
	}

	// Token: 0x06002B6B RID: 11115 RVA: 0x00021718 File Offset: 0x0001F918
	private void RemoveFog()
	{
		Camera.main.GetComponent<Animator>().Play("FogOfWar_Remove");
	}

	// Token: 0x06002B6C RID: 11116 RVA: 0x00153620 File Offset: 0x00151820
	private void ShowKeepPlayingScreen()
	{
		this.measureTime = false;
		Transform transform = base.transform.Find("Keep Playing HOLDER/AnimationHolderGlavni/AnimationHolder/Popup za Keep Playing");
		transform.Find("Text/Banana Number").GetComponent<TextMesh>().text = (this.keepPlayingCount + 1).ToString();
		transform.Find("Text/Banana Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		transform.Find("CoinsHolder/Coins/Coins Number").GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
		transform.Find("CoinsHolder/Coins/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		transform.Find("Bananas/Banana Number").GetComponent<TextMesh>().text = StagesParser.currentBananas.ToString();
		transform.Find("Bananas/Banana Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		transform.Find("Text/BananaCost").GetComponent<TextMesh>().text = StagesParser.bananaCost.ToString();
		transform.Find("Text/BananaCost").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		base.transform.Find("Keep Playing HOLDER").GetChild(0).localPosition += new Vector3(0f, 35f, 0f);
		base.transform.Find("Keep Playing HOLDER").GetChild(0).GetComponent<Animator>().Play("OpenPopup");
		this.makniPopup = 3;
	}

	// Token: 0x06002B6D RID: 11117 RVA: 0x0002172E File Offset: 0x0001F92E
	private IEnumerator closeDoorAndPlay()
	{
		base.transform.Find("LOADING HOLDER NEW/Loading Animation Vrata").GetComponent<Animator>().Play("Loading Zidovi Dolazak");
		yield return new WaitForSeconds(0.75f);
		Application.LoadLevel(2);
		yield break;
	}

	// Token: 0x06002B6E RID: 11118 RVA: 0x0015378C File Offset: 0x0015198C
	private void refreshText()
	{
		base.transform.Find("GO screen/GO screen Text").GetComponent<TextMesh>().text = LanguageManager.TapScreenToStart;
		base.transform.Find("GO screen/GO screen Text").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, false);
		base.transform.Find("Keep Playing HOLDER/AnimationHolderGlavni/AnimationHolder/Popup za Keep Playing/Button Buy Banana/BuyText").GetComponent<TextMesh>().text = LanguageManager.Buy;
		base.transform.Find("Keep Playing HOLDER/AnimationHolderGlavni/AnimationHolder/Popup za Keep Playing/Button Buy Banana/BuyText").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		base.transform.Find("Keep Playing HOLDER/AnimationHolderGlavni/AnimationHolder/Popup za Keep Playing/Text/Keep Playing").GetComponent<TextMesh>().text = LanguageManager.KeepPlaying;
		base.transform.Find("Keep Playing HOLDER/AnimationHolderGlavni/AnimationHolder/Popup za Keep Playing/Text/Keep Playing").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		base.transform.Find("Level Win_Lose SCENA/Popup za LOSE HOLDER/Popup za LOSE/Header za LOSE popup/Text/Level Failed").GetComponent<TextMesh>().text = LanguageManager.LevelFailed;
		base.transform.Find("Level Win_Lose SCENA/Popup za LOSE HOLDER/Popup za LOSE/Header za LOSE popup/Text/Level Failed").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		this.Win_CompletedScreenHolder.Find("FB Invite Large/Text/Invite").GetComponent<TextMesh>().text = LanguageManager.Invite;
		this.Win_CompletedScreenHolder.Find("FB Invite Large/Text/Invite").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		if (!StagesParser.bonusLevel)
		{
			this.Win_CompletedScreenHolder.Find("FB Share/Text/Share").GetComponent<TextMesh>().text = LanguageManager.Share;
			this.Win_CompletedScreenHolder.Find("FB Share/Text/Share").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		}
		this.Win_CompletedScreenHolder.Find("Friends FB level WIN/Friends Level Win 1 HOLDER/FB/Fb Invite 1").GetComponent<TextMesh>().text = LanguageManager.Invite;
		this.Win_CompletedScreenHolder.Find("Friends FB level WIN/Friends Level Win 2 HOLDER/FB/Fb Invite 1").GetComponent<TextMesh>().text = LanguageManager.Invite;
		this.Win_CompletedScreenHolder.Find("Friends FB level WIN/Friends Level Win 3 HOLDER/FB/Fb Invite 1").GetComponent<TextMesh>().text = LanguageManager.Invite;
		this.Win_CompletedScreenHolder.Find("Friends FB level WIN/Friends Level Win 4 HOLDER/FB/Fb Invite 1").GetComponent<TextMesh>().text = LanguageManager.Invite;
		this.Win_CompletedScreenHolder.Find("Friends FB level WIN/Friends Level Win 5 HOLDER/FB/Fb Invite 1").GetComponent<TextMesh>().text = LanguageManager.Invite;
		this.Win_CompletedScreenHolder.Find("Friends FB level WIN/Friends Level Win 1 HOLDER/FB/Fb Invite 1").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		this.Win_CompletedScreenHolder.Find("Friends FB level WIN/Friends Level Win 2 HOLDER/FB/Fb Invite 1").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		this.Win_CompletedScreenHolder.Find("Friends FB level WIN/Friends Level Win 3 HOLDER/FB/Fb Invite 1").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		this.Win_CompletedScreenHolder.Find("Friends FB level WIN/Friends Level Win 4 HOLDER/FB/Fb Invite 1").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		this.Win_CompletedScreenHolder.Find("Friends FB level WIN/Friends Level Win 5 HOLDER/FB/Fb Invite 1").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		this.Win_CompletedScreenHolder.Find("Popup za WIN/Header za win popup/Text/Level").GetComponent<TextMesh>().text = LanguageManager.Level;
		this.Win_CompletedScreenHolder.Find("Popup za WIN/Header za win popup/Text/Level").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		this.Win_CompletedScreenHolder.Find("Popup za WIN/Header za win popup/Text/Completed").GetComponent<TextMesh>().text = LanguageManager.Completed;
		this.Win_CompletedScreenHolder.Find("Popup za WIN/Header za win popup/Text/Completed").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		base.transform.Find("PAUSE HOLDER/AnimationHolderGlavni/AnimationHolder/PAUSE PopUp/PauseText").GetComponent<TextMesh>().text = LanguageManager.Pause;
		base.transform.Find("PAUSE HOLDER/AnimationHolderGlavni/AnimationHolder/PAUSE PopUp/PauseText").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		base.transform.Find("RATE HOLDER/AnimationHolderGlavni/AnimationHolder/RATE Popup/Rate").GetComponent<TextMesh>().text = LanguageManager.RateThisGame;
		base.transform.Find("RATE HOLDER/AnimationHolderGlavni/AnimationHolder/RATE Popup/Rate").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		base.transform.Find("RATE HOLDER/AnimationHolderGlavni/AnimationHolder/RATE Popup/Text 1").GetComponent<TextMesh>().text = LanguageManager.HowWouldYouRate;
		base.transform.Find("RATE HOLDER/AnimationHolderGlavni/AnimationHolder/RATE Popup/Text 1").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		base.transform.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/Free Coins").GetComponent<TextMesh>().text = LanguageManager.FreeCoins;
		base.transform.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/Free Coins").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		base.transform.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/WatchButtonNO/Text").GetComponent<TextMesh>().text = LanguageManager.No;
		base.transform.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/WatchButtonNO/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		base.transform.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/WatchButtonYES/Text").GetComponent<TextMesh>().text = LanguageManager.Yes;
		base.transform.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/WatchButtonYES/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		base.transform.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/WatchVideoText").GetComponent<TextMesh>().text = LanguageManager.WatchVideo;
		base.transform.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/WatchVideoText").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		base.transform.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/NotAvailableText").GetComponent<TextMesh>().text = LanguageManager.NoVideo;
		base.transform.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/NotAvailableText").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
	}

	// Token: 0x06002B6F RID: 11119 RVA: 0x00153C8C File Offset: 0x00151E8C
	private void getFriendsScoresOnLevel(int level)
	{
		if (!this.popunioSlike)
		{
			this.popunioSlike = true;
			for (int i = 0; i < FacebookManager.ListaStructPrijatelja.Count; i++)
			{
				for (int j = 0; j < FacebookManager.ProfileSlikePrijatelja.Count; j++)
				{
					if (FacebookManager.ListaStructPrijatelja[i].PrijateljID == FacebookManager.ProfileSlikePrijatelja[j].PrijateljID)
					{
						FacebookManager.StrukturaPrijatelja value = FacebookManager.ListaStructPrijatelja[i];
						value.profilePicture = FacebookManager.ProfileSlikePrijatelja[j].profilePicture;
						FacebookManager.ListaStructPrijatelja[i] = value;
					}
				}
			}
		}
		List<Manage.scoreAndIndex> list = new List<Manage.scoreAndIndex>();
		for (int k = 0; k < FacebookManager.ListaStructPrijatelja.Count; k++)
		{
			Manage.scoreAndIndex item = default(Manage.scoreAndIndex);
			if (level <= FacebookManager.ListaStructPrijatelja[k].scores.Count)
			{
				item.index = k;
				item.score = FacebookManager.ListaStructPrijatelja[k].scores[level - 1];
				if (FacebookManager.ListaStructPrijatelja[k].PrijateljID == FacebookManager.User)
				{
					int num = Manage.points;
					if (num > FacebookManager.ListaStructPrijatelja[k].scores[level - 1])
					{
						item.score = num;
					}
				}
				list.Add(item);
			}
		}
		Manage.scoreAndIndex scoreAndIndex = default(Manage.scoreAndIndex);
		default(Manage.scoreAndIndex).score = 0;
		for (int l = 0; l < list.Count; l++)
		{
			scoreAndIndex = list[l];
			int index = l;
			bool flag = false;
			for (int m = l + 1; m < list.Count; m++)
			{
				if (scoreAndIndex.score < list[m].score)
				{
					scoreAndIndex = list[m];
					index = m;
					flag = true;
				}
			}
			if (flag)
			{
				Manage.scoreAndIndex value2 = list[l];
				list[l] = list[index];
				list[index] = value2;
			}
		}
		int num2 = 1;
		bool flag2 = false;
		int num3 = 1;
		for (int n = 0; n < list.Count; n++)
		{
			if (FacebookManager.ListaStructPrijatelja[list[n].index].PrijateljID == FacebookManager.User)
			{
				num3 = num2;
			}
			if (n < 5)
			{
				if (list[n].score > 0 || FacebookManager.ListaStructPrijatelja[list[n].index].PrijateljID == FacebookManager.User)
				{
					Transform transform = this.Win_CompletedScreenHolder.Find("Friends FB level WIN/Friends Level Win " + num2 + " HOLDER");
					transform.Find("FB").gameObject.SetActive(false);
					if (!transform.Find("Friends Level Win " + num2).gameObject.activeSelf)
					{
						transform.Find("Friends Level Win " + num2).gameObject.SetActive(true);
					}
					transform.Find(string.Concat(new object[]
					{
						"Friends Level Win ",
						num2,
						"/Friends Level Win Picture ",
						num2
					})).GetComponent<Renderer>().material.mainTexture = FacebookManager.ProfileSlikePrijatelja[list[n].index].profilePicture;
					TextMesh component = transform.Find(string.Concat(new object[]
					{
						"Friends Level Win ",
						num2,
						"/Friends Level Win Picture ",
						num2,
						"/Points Number level win fb"
					})).GetComponent<TextMesh>();
					Manage.scoreAndIndex scoreAndIndex2 = list[n];
					component.text = scoreAndIndex2.score.ToString();
					if (FacebookManager.ListaStructPrijatelja[list[n].index].PrijateljID == FacebookManager.User)
					{
						flag2 = true;
						transform.Find("Friends Level Win " + num2).GetComponent<SpriteRenderer>().sprite = transform.parent.Find("ReferencaYOU").GetComponent<SpriteRenderer>().sprite;
					}
					else
					{
						transform.Find("Friends Level Win " + num2).GetComponent<SpriteRenderer>().sprite = transform.parent.Find("Referenca").GetComponent<SpriteRenderer>().sprite;
					}
				}
				else if (num2 <= 5)
				{
					this.Win_CompletedScreenHolder.Find("Friends FB level WIN/Friends Level Win " + num2 + " HOLDER/FB").gameObject.SetActive(true);
					this.Win_CompletedScreenHolder.Find(string.Concat(new object[]
					{
						"Friends FB level WIN/Friends Level Win ",
						num2,
						" HOLDER/Friends Level Win ",
						num2
					})).gameObject.SetActive(false);
				}
			}
			num2++;
		}
		if (list.Count < 5)
		{
			for (int num4 = num2; num4 <= 5; num4++)
			{
				this.Win_CompletedScreenHolder.Find(string.Concat(new object[]
				{
					"Friends FB level WIN/Friends Level Win ",
					num4,
					" HOLDER/Friends Level Win ",
					num4
				})).gameObject.SetActive(false);
			}
		}
		if (!flag2)
		{
			Transform transform = this.Win_CompletedScreenHolder.Find("Friends FB level WIN/Friends Level Win 5 HOLDER");
			transform.Find("Friends Level Win 5/Friends Level Win Picture 5").GetComponent<Renderer>().material.mainTexture = FacebookManager.ListaStructPrijatelja[list[num3 - 1].index].profilePicture;
			TextMesh component2 = transform.Find("Friends Level Win 5/Friends Level Win Picture 5/Points Number level win fb").GetComponent<TextMesh>();
			Manage.scoreAndIndex scoreAndIndex2 = list[num3 - 1];
			component2.text = scoreAndIndex2.score.ToString();
			transform.Find("Friends Level Win 5/Friends Level Win Picture 5/Position Number").GetComponent<TextMesh>().text = num3.ToString();
			transform.Find("Friends Level Win 5").GetComponent<SpriteRenderer>().sprite = transform.parent.Find("ReferencaYOU").GetComponent<SpriteRenderer>().sprite;
		}
		list.Clear();
	}

	// Token: 0x06002B70 RID: 11120 RVA: 0x001542D0 File Offset: 0x001524D0
	private void spustiPopup()
	{
		this.popupZaSpustanje.localPosition += new Vector3(0f, -35f, 0f);
		this.popupZaSpustanje = null;
		if (Manage.pauseEnabled && this.neDozvoliPauzu)
		{
			this.neDozvoliPauzu = false;
			this.pauseButton.GetComponent<Collider>().enabled = true;
		}
	}

	// Token: 0x06002B71 RID: 11121 RVA: 0x0002173D File Offset: 0x0001F93D
	private void DeaktivirajPowerUpAnimator()
	{
		base.transform.Find("POWERS HOLDER").gameObject.SetActive(false);
	}

	// Token: 0x06002B72 RID: 11122 RVA: 0x0002175A File Offset: 0x0001F95A
	private IEnumerator checkConnectionForWatchVideo()
	{
		base.StartCoroutine(CheckInternetConnection.Instance.checkInternetConnection());
		while (!CheckInternetConnection.Instance.checkDone)
		{
			yield return null;
		}
		if (CheckInternetConnection.Instance.internetOK)
		{
			StagesParser.sceneID = 2;
			this.popupZaSpustanje = base.transform.Find("WATCH VIDEO HOLDER");
			base.Invoke("spustiPopup", 0.5f);
			base.transform.Find("WATCH VIDEO HOLDER").GetChild(0).GetComponent<Animator>().Play("ClosePopup");
		}
		else
		{
			CheckInternetConnection.Instance.openPopup();
		}
		yield break;
	}

	// Token: 0x06002B73 RID: 11123 RVA: 0x00154338 File Offset: 0x00152538
	private void WatchVideoCallback()
	{
		StagesParser.currentMoney += this.watchVideoReward;
		PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
		PlayerPrefs.Save();
		base.StartCoroutine(StagesParser.Instance.moneyCounter(this.watchVideoReward, base.transform.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/Coins/Coins Number").GetComponent<TextMesh>(), true));
		StagesParser.ServerUpdate = 1;
	}

	// Token: 0x06002B74 RID: 11124 RVA: 0x00021769 File Offset: 0x0001F969
	private IEnumerator checkConnectionForRate()
	{
		base.StartCoroutine(CheckInternetConnection.Instance.checkInternetConnection());
		while (!CheckInternetConnection.Instance.checkDone)
		{
			yield return null;
		}
		if (CheckInternetConnection.Instance.internetOK)
		{
			base.StartCoroutine(this.RateGame(StagesParser.Instance.rateLink));
		}
		else
		{
			CheckInternetConnection.Instance.openPopup();
		}
		yield break;
	}

	// Token: 0x06002B75 RID: 11125 RVA: 0x00021778 File Offset: 0x0001F978
	private IEnumerator checkConnectionForInvite()
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

	// Token: 0x06002B76 RID: 11126 RVA: 0x00021787 File Offset: 0x0001F987
	private IEnumerator checkConnectionForShare()
	{
		base.StartCoroutine(CheckInternetConnection.Instance.checkInternetConnection());
		while (!CheckInternetConnection.Instance.checkDone)
		{
			yield return null;
		}
		if (CheckInternetConnection.Instance.internetOK)
		{
			FacebookManager.FacebookObject.ProveriPermisije();
		}
		else
		{
			CheckInternetConnection.Instance.openPopup();
		}
		yield break;
	}

	// Token: 0x04002592 RID: 9618
	[HideInInspector]
	public static int coinsCollected;

	// Token: 0x04002593 RID: 9619
	[HideInInspector]
	public int starsGained;

	// Token: 0x04002594 RID: 9620
	public static int baboonsKilled;

	// Token: 0x04002595 RID: 9621
	public static int fly_BaboonsKilled;

	// Token: 0x04002596 RID: 9622
	public static int boomerang_BaboonsKilled;

	// Token: 0x04002597 RID: 9623
	public static int gorillasKilled;

	// Token: 0x04002598 RID: 9624
	public static int fly_GorillasKilled;

	// Token: 0x04002599 RID: 9625
	public static int koplje_GorillasKilled;

	// Token: 0x0400259A RID: 9626
	public static int barrelsSmashed;

	// Token: 0x0400259B RID: 9627
	public static int redDiamonds;

	// Token: 0x0400259C RID: 9628
	public static int blueDiamonds;

	// Token: 0x0400259D RID: 9629
	public static int greenDiamonds;

	// Token: 0x0400259E RID: 9630
	private GameObject goScreen;

	// Token: 0x0400259F RID: 9631
	private GameObject player;

	// Token: 0x040025A0 RID: 9632
	private MonkeyController2D playerController;

	// Token: 0x040025A1 RID: 9633
	private Transform pauseButton;

	// Token: 0x040025A2 RID: 9634
	[HideInInspector]
	public TextMesh coinsCollectedText;

	// Token: 0x040025A3 RID: 9635
	private Transform pauseScreenHolder;

	// Token: 0x040025A4 RID: 9636
	private Transform Win_CompletedScreenHolder;

	// Token: 0x040025A5 RID: 9637
	private Transform FailedScreenHolder;

	// Token: 0x040025A6 RID: 9638
	private Transform Win_ShineHolder;

	// Token: 0x040025A7 RID: 9639
	private GameObject star1;

	// Token: 0x040025A8 RID: 9640
	private GameObject star2;

	// Token: 0x040025A9 RID: 9641
	private GameObject star3;

	// Token: 0x040025AA RID: 9642
	private bool helpBool;

	// Token: 0x040025AB RID: 9643
	private string clickedItem;

	// Token: 0x040025AC RID: 9644
	private string releasedItem;

	// Token: 0x040025AD RID: 9645
	private SetRandomStarsManager starManager;

	// Token: 0x040025AE RID: 9646
	[HideInInspector]
	public bool PowerUp_doubleCoins;

	// Token: 0x040025AF RID: 9647
	[HideInInspector]
	public bool PowerUp_shield;

	// Token: 0x040025B0 RID: 9648
	private GameObject coinMagnet;

	// Token: 0x040025B1 RID: 9649
	private GameObject shield;

	// Token: 0x040025B2 RID: 9650
	private DateTime timeToShowNextElement;

	// Token: 0x040025B3 RID: 9651
	private TextMesh zivotiText;

	// Token: 0x040025B4 RID: 9652
	private TextMesh zivotiText2;

	// Token: 0x040025B5 RID: 9653
	private Transform rateHolder;

	// Token: 0x040025B6 RID: 9654
	private int kliknuoYes;

	// Token: 0x040025B7 RID: 9655
	private Vector3 originalScale;

	// Token: 0x040025B8 RID: 9656
	public static bool pauseEnabled;

	// Token: 0x040025B9 RID: 9657
	private bool nemaReklame;

	// Token: 0x040025BA RID: 9658
	private float timeElapsed;

	// Token: 0x040025BB RID: 9659
	private bool pocniVreme;

	// Token: 0x040025BC RID: 9660
	public static float goTrenutak;

	// Token: 0x040025BD RID: 9661
	private int stepenBrzine;

	// Token: 0x040025BE RID: 9662
	public static TextMesh pointsText;

	// Token: 0x040025BF RID: 9663
	public static TextMeshEffects pointsEffects;

	// Token: 0x040025C0 RID: 9664
	public static int points;

	// Token: 0x040025C1 RID: 9665
	public static int bananas;

	// Token: 0x040025C2 RID: 9666
	private Transform holderLife;

	// Token: 0x040025C3 RID: 9667
	public static bool shouldRaycast;

	// Token: 0x040025C4 RID: 9668
	private Transform PickPowers;

	// Token: 0x040025C5 RID: 9669
	private Transform powerCard_CoinX2;

	// Token: 0x040025C6 RID: 9670
	private Transform powerCard_Magnet;

	// Token: 0x040025C7 RID: 9671
	private Transform powerCard_Shield;

	// Token: 0x040025C8 RID: 9672
	private bool kupljenShield;

	// Token: 0x040025C9 RID: 9673
	private bool kupljenDoubleCoins;

	// Token: 0x040025CA RID: 9674
	private bool kupljenMagnet;

	// Token: 0x040025CB RID: 9675
	private int povecanaTezina;

	// Token: 0x040025CC RID: 9676
	private bool postavljenFinish;

	// Token: 0x040025CD RID: 9677
	private GameObject temp;

	// Token: 0x040025CE RID: 9678
	private Transform _GUI;

	// Token: 0x040025CF RID: 9679
	private Camera guiCamera;

	// Token: 0x040025D0 RID: 9680
	private int watchVideoReward = 1000;

	// Token: 0x040025D1 RID: 9681
	private int makniPopup;

	// Token: 0x040025D2 RID: 9682
	private bool measureTime = true;

	// Token: 0x040025D3 RID: 9683
	[HideInInspector]
	public float aktivnoVreme;

	// Token: 0x040025D4 RID: 9684
	[HideInInspector]
	public int keepPlayingCount = 1;

	// Token: 0x040025D5 RID: 9685
	private Transform popupZaSpustanje;

	// Token: 0x040025D6 RID: 9686
	private int pointsForDisplay;

	// Token: 0x040025D7 RID: 9687
	private bool neDozvoliPauzu;

	// Token: 0x040025D8 RID: 9688
	private InterstitialAd interstitial;

	// Token: 0x040025D9 RID: 9689
	private AdRequest request;

	// Token: 0x040025DA RID: 9690
	private static Manage instance;

	// Token: 0x040025DB RID: 9691
	private bool popunioSlike;

	// Token: 0x020006C7 RID: 1735
	private struct scoreAndIndex
	{
		// Token: 0x06002B7C RID: 11132 RVA: 0x000217C0 File Offset: 0x0001F9C0
		public scoreAndIndex(int score, int index)
		{
			this.score = score;
			this.index = index;
		}

		// Token: 0x040025DC RID: 9692
		public int score;

		// Token: 0x040025DD RID: 9693
		public int index;
	}
}
