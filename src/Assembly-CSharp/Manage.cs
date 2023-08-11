using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class Manage : MonoBehaviour
{
	private struct scoreAndIndex
	{
		public int score;

		public int index;

		public scoreAndIndex(int score, int index)
		{
			this.score = score;
			this.index = index;
		}
	}

	[HideInInspector]
	public static int coinsCollected;

	[HideInInspector]
	public int starsGained;

	public static int baboonsKilled;

	public static int fly_BaboonsKilled;

	public static int boomerang_BaboonsKilled;

	public static int gorillasKilled;

	public static int fly_GorillasKilled;

	public static int koplje_GorillasKilled;

	public static int barrelsSmashed;

	public static int redDiamonds;

	public static int blueDiamonds;

	public static int greenDiamonds;

	private GameObject goScreen;

	private GameObject player;

	private MonkeyController2D playerController;

	private Transform pauseButton;

	[HideInInspector]
	public TextMesh coinsCollectedText;

	private Transform pauseScreenHolder;

	private Transform Win_CompletedScreenHolder;

	private Transform FailedScreenHolder;

	private Transform Win_ShineHolder;

	private GameObject star1;

	private GameObject star2;

	private GameObject star3;

	private bool helpBool;

	private string clickedItem;

	private string releasedItem;

	private SetRandomStarsManager starManager;

	[HideInInspector]
	public bool PowerUp_doubleCoins;

	[HideInInspector]
	public bool PowerUp_shield;

	private GameObject coinMagnet;

	private GameObject shield;

	private DateTime timeToShowNextElement;

	private TextMesh zivotiText;

	private TextMesh zivotiText2;

	private Transform rateHolder;

	private int kliknuoYes;

	private Vector3 originalScale;

	public static bool pauseEnabled;

	private bool nemaReklame;

	private float timeElapsed;

	private bool pocniVreme;

	public static float goTrenutak;

	private int stepenBrzine;

	public static TextMesh pointsText;

	public static TextMeshEffects pointsEffects;

	public static int points;

	public static int bananas;

	private Transform holderLife;

	public static bool shouldRaycast;

	private Transform PickPowers;

	private Transform powerCard_CoinX2;

	private Transform powerCard_Magnet;

	private Transform powerCard_Shield;

	private bool kupljenShield;

	private bool kupljenDoubleCoins;

	private bool kupljenMagnet;

	private int povecanaTezina;

	private bool postavljenFinish;

	private GameObject temp;

	private Transform _GUI;

	private Camera guiCamera;

	private int watchVideoReward = 1000;

	private int makniPopup;

	private bool measureTime = true;

	[HideInInspector]
	public float aktivnoVreme;

	[HideInInspector]
	public int keepPlayingCount = 1;

	private Transform popupZaSpustanje;

	private int pointsForDisplay;

	private bool neDozvoliPauzu;

	private InterstitialAd interstitial;

	private AdRequest request;

	private static Manage instance;

	private bool popunioSlike;

	public static Manage Instance
	{
		get
		{
			if ((Object)(object)instance == (Object)null)
			{
				instance = Object.FindObjectOfType(typeof(Manage)) as Manage;
			}
			return instance;
		}
	}

	private void Awake()
	{
		try
		{
			interstitial = new InterstitialAd(StagesParser.Instance.AdMobInterstitialID);
			request = new AdRequest.Builder().Build();
			interstitial.LoadAd(request);
		}
		catch
		{
			Debug.Log((object)"AD NOT INITIALIZED");
		}
		instance = this;
		goScreen = ((Component)((Component)this).transform.Find("GO screen").GetChild(0)).gameObject;
		guiCamera = GameObject.Find("GUICamera").GetComponent<Camera>();
		starManager = ((Component)this).GetComponent<SetRandomStarsManager>();
		player = GameObject.FindGameObjectWithTag("Monkey");
		playerController = player.GetComponent<MonkeyController2D>();
		coinsCollectedText = ((Component)((Component)this).transform.Find("Gameplay Scena Interface/_TopLeft/Coins/CoinsGamePlayText")).GetComponent<TextMesh>();
		pauseButton = ((Component)this).transform.Find("Gameplay Scena Interface/_TopRight/Pause Button");
		pointsText = ((Component)((Component)this).transform.Find("Gameplay Scena Interface/_TopLeft/PTS/PTS Number")).GetComponent<TextMesh>();
		pointsEffects = ((Component)pointsText).GetComponent<TextMeshEffects>();
		pauseScreenHolder = ((Component)this).transform.Find("PAUSE HOLDER");
		FailedScreenHolder = ((Component)this).transform.Find("Level Win_Lose SCENA/Popup za LOSE HOLDER");
		if (StagesParser.bonusLevel)
		{
			Win_CompletedScreenHolder = ((Component)this).transform.Find("Level Win_Lose SCENA/Popup za WIN HOLDER_BONUS");
		}
		else
		{
			Win_CompletedScreenHolder = ((Component)this).transform.Find("Level Win_Lose SCENA/Popup za WIN HOLDER");
		}
		star1 = GameObject.Find("Stars Polja 1");
		star2 = GameObject.Find("Stars Polja 2");
		star3 = GameObject.Find("Stars Polja 3");
		coinMagnet = ((Component)player.transform.Find("CoinMagnet")).gameObject;
		shield = GameObject.Find("Shield");
		shield.SetActive(false);
		rateHolder = ((Component)((Component)this).transform.Find("RATE HOLDER/AnimationHolderGlavni/AnimationHolder/RATE Popup")).transform;
		PickPowers = GameObject.Find("POWERS HOLDER").transform;
		powerCard_CoinX2 = GameObject.Find("Power_Double Coins Interface").transform;
		powerCard_Magnet = GameObject.Find("Power_Magnet Interface").transform;
		powerCard_Shield = GameObject.Find("Power_Shield Interface").transform;
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
		shouldRaycast = false;
		coinsCollected = 0;
		baboonsKilled = 0;
		fly_BaboonsKilled = 0;
		boomerang_BaboonsKilled = 0;
		gorillasKilled = 0;
		fly_GorillasKilled = 0;
		koplje_GorillasKilled = 0;
		points = 0;
		barrelsSmashed = 0;
		redDiamonds = 0;
		blueDiamonds = 0;
		greenDiamonds = 0;
		bananas = 0;
	}

	private void Start()
	{
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		refreshText();
		if ((Object)(object)Loading.Instance != (Object)null)
		{
			((MonoBehaviour)this).StartCoroutine(Loading.Instance.UcitanaScena(guiCamera, 1, 0.5f));
		}
		pauseEnabled = false;
		if (StagesParser.bonusLevel)
		{
			TextMesh component = goScreen.GetComponent<TextMesh>();
			component.text = component.text + "\n" + LanguageManager.BonusLevel;
		}
		else
		{
			TextMesh component2 = goScreen.GetComponent<TextMesh>();
			component2.text = component2.text + "\n" + LanguageManager.Level + " " + (StagesParser.currStageIndex + 1);
		}
		goScreen.GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true, increaseFont: false);
		((Component)powerCard_CoinX2.parent.Find("CoinsHolder/Coins/Coins Number")).GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
		((Component)powerCard_CoinX2.parent.Find("CoinsHolder/Coins/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		((Component)powerCard_CoinX2.Find("Number")).GetComponent<TextMesh>().text = StagesParser.powerup_doublecoins.ToString();
		((Component)powerCard_Magnet.Find("Number")).GetComponent<TextMesh>().text = StagesParser.powerup_magnets.ToString();
		((Component)powerCard_Shield.Find("Number")).GetComponent<TextMesh>().text = StagesParser.powerup_shields.ToString();
		((Component)powerCard_CoinX2.Find("Text/Cost Number")).GetComponent<TextMesh>().text = StagesParser.cost_doublecoins.ToString();
		((Component)powerCard_CoinX2.Find("Text/Cost Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		((Component)powerCard_Magnet.Find("Text/Cost Number")).GetComponent<TextMesh>().text = StagesParser.cost_magnet.ToString();
		((Component)powerCard_Magnet.Find("Text/Cost Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		((Component)powerCard_Shield.Find("Text/Cost Number")).GetComponent<TextMesh>().text = StagesParser.cost_shield.ToString();
		((Component)powerCard_Shield.Find("Text/Cost Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		((Component)Win_CompletedScreenHolder.Find("FB Invite Large/Text/Number")).GetComponent<TextMesh>().text = "+" + StagesParser.InviteReward;
		((Component)Win_CompletedScreenHolder.Find("FB Invite Large/Text/Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: false);
		if (!StagesParser.bonusLevel)
		{
			((Component)Win_CompletedScreenHolder.Find("FB Share/Text/Number")).GetComponent<TextMesh>().text = "+" + StagesParser.ShareReward;
			((Component)Win_CompletedScreenHolder.Find("FB Share/Text/Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: false);
		}
		((Component)FailedScreenHolder).gameObject.SetActive(false);
		((Component)Win_CompletedScreenHolder).gameObject.SetActive(false);
		if (guiCamera.aspect <= 1.4f && MissionManager.NumberOfQuests == 3)
		{
			((Component)this).transform.Find("Gameplay Scena Interface").localScale = Vector3.one * 0.91f;
			((Component)this).transform.Find("Gameplay Scena Interface").localPosition = new Vector3(-1.2f, 13.5f, 5f);
			((Component)this).transform.Find("Gameplay Scena Interface/_TopMissions").localPosition = new Vector3(1.6f, 0f, 0f);
		}
		((Component)this).transform.Find("Gameplay Scena Interface/_TopLeft").position = new Vector3(guiCamera.ViewportToWorldPoint(Vector3.zero).x, ((Component)this).transform.Find("Gameplay Scena Interface/_TopLeft").position.y, ((Component)this).transform.Find("Gameplay Scena Interface/_TopLeft").position.z);
		((Component)this).transform.Find("Gameplay Scena Interface/_TopRight").position = new Vector3(guiCamera.ViewportToWorldPoint(Vector3.one).x, ((Component)this).transform.Find("Gameplay Scena Interface/_TopRight").position.y, ((Component)this).transform.Find("Gameplay Scena Interface/_TopRight").position.z);
		if (Application.loadedLevelName.Equals("_Tutorial Level") && PlayerPrefs.HasKey("VecPokrenuto"))
		{
			GameObject.Find("Banana_collect_Holder").SetActive(false);
		}
	}

	private void Update()
	{
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_0609: Unknown result type (might be due to invalid IL or missing references)
		//IL_0585: Unknown result type (might be due to invalid IL or missing references)
		//IL_058a: Unknown result type (might be due to invalid IL or missing references)
		//IL_059b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0656: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0859: Unknown result type (might be due to invalid IL or missing references)
		//IL_086d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0872: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		if (pocniVreme)
		{
			if (measureTime)
			{
				aktivnoVreme += 1f * Time.deltaTime;
			}
			if (aktivnoVreme >= 12f)
			{
				if (StagesParser.odgledaoTutorial == 0 && aktivnoVreme >= 35f && !postavljenFinish)
				{
					postavljenFinish = true;
					playerController.Finish();
				}
				if (aktivnoVreme >= 55f && povecanaTezina == 0)
				{
					povecanaTezina = 1;
					LevelFactory.instance.overallDifficulty = 11;
				}
				if (aktivnoVreme >= 70f && povecanaTezina == 1)
				{
					povecanaTezina = 2;
					LevelFactory.instance.overallDifficulty = 16;
				}
			}
			if (aktivnoVreme - timeElapsed >= 20f && stepenBrzine <= 7)
			{
				playerController.startSpeedX += 1f;
				playerController.maxSpeedX += 1f;
				Animator component = ((Component)playerController.majmun).GetComponent<Animator>();
				component.speed += 0.075f;
				timeElapsed = aktivnoVreme;
				stepenBrzine++;
				if (StagesParser.bossStage)
				{
					BossScript.Instance.maxSpeedX = playerController.maxSpeedX;
				}
			}
		}
		if (Input.GetKeyDown((KeyCode)32) && goScreen.activeSelf)
		{
			Time.timeScale = 1f;
			goScreen.transform.parent = ((Component)this).transform;
			goScreen.SetActive(false);
			playerController.state = MonkeyController2D.State.running;
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Run();
			}
			pocniVreme = true;
			goTrenutak = Time.time;
		}
		else if (Input.GetKeyUp((KeyCode)27))
		{
			if (pauseEnabled && makniPopup == 0)
			{
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_Button_Pause();
				}
				PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
				PlayerPrefs.Save();
				if (Time.timeScale == 1f)
				{
					if (!neDozvoliPauzu)
					{
						Time.timeScale = 0f;
						((MonoBehaviour)this).StopAllCoroutines();
						Transform child = pauseScreenHolder.GetChild(0);
						child.localPosition += new Vector3(0f, 35f, 0f);
						((Component)pauseScreenHolder.GetChild(0)).GetComponent<Animator>().Play("OpenPopup");
						((Component)pauseButton).GetComponent<Collider>().enabled = false;
						neDozvoliPauzu = true;
					}
				}
				else
				{
					popupZaSpustanje = pauseScreenHolder.GetChild(0);
					((MonoBehaviour)this).Invoke("spustiPopup", 0.5f);
					((Component)pauseScreenHolder.GetChild(0)).GetComponent<Animator>().Play("ClosePopup");
					Time.timeScale = 1f;
				}
			}
			else if (makniPopup == 1)
			{
				popupZaSpustanje = ((Component)this).transform.Find("RATE HOLDER").GetChild(0);
				((MonoBehaviour)this).Invoke("spustiPopup", 0.5f);
				((Component)((Component)this).transform.Find("RATE HOLDER").GetChild(0)).GetComponent<Animator>().Play("ClosePopup");
				kliknuoYes = 0;
				makniPopup = 0;
			}
			else if (makniPopup == 2)
			{
				popupZaSpustanje = ((Component)this).transform.Find("WATCH VIDEO HOLDER");
				((MonoBehaviour)this).Invoke("spustiPopup", 0.5f);
				((Component)((Component)this).transform.Find("WATCH VIDEO HOLDER").GetChild(0)).GetComponent<Animator>().Play("ClosePopup");
				makniPopup = 0;
			}
			else if (makniPopup == 3)
			{
				showFailedScreen();
				makniPopup = 0;
			}
			else if (makniPopup == 4)
			{
				makniPopup = 0;
				((MonoBehaviour)this).StartCoroutine(CheckInternetConnection.Instance.ClosePopup());
			}
			else if (makniPopup == 5)
			{
				makniPopup = 2;
				((MonoBehaviour)this).StartCoroutine(CheckInternetConnection.Instance.ClosePopup());
			}
		}
		if (Input.GetMouseButtonDown(0))
		{
			clickedItem = RaycastFunction(Input.mousePosition);
			if (clickedItem == "RateButtonNO" || clickedItem == "RateButtonYES" || clickedItem == "WatchButtonNO" || clickedItem == "WatchButtonYES" || clickedItem == "Button Buy Banana" || clickedItem == "Button Cancel" || clickedItem == "Button Play_Revive" || clickedItem == "Button Mapa_Win" || clickedItem == "Button Next_Win" || clickedItem == "Button Restart_Win" || clickedItem == "Button Home_Failed" || clickedItem == "Button Mapa_Failed" || clickedItem == "Button Restart_Failed" || clickedItem == "Menu Button_Pause" || clickedItem == "Play Button_Pause" || clickedItem == "Restart Button_Pause")
			{
				temp = GameObject.Find(clickedItem);
				originalScale = temp.transform.localScale;
				temp.transform.localScale = originalScale * 0.8f;
			}
			else if (clickedItem != string.Empty && clickedItem != "Buy Button")
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
		if (clickedItem != string.Empty && clickedItem != "Buy Button" && (Object)(object)temp != (Object)null)
		{
			temp.transform.localScale = originalScale;
		}
		if (!(clickedItem == releasedItem))
		{
			return;
		}
		if (releasedItem == "GO screen")
		{
			if (!StagesParser.bossStage)
			{
				Time.timeScale = 1f;
				((Component)goScreen.transform.parent).gameObject.SetActive(false);
				playerController.state = MonkeyController2D.State.running;
				((Component)playerController.majmun).GetComponent<Animator>().SetBool("Run", true);
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_Run();
				}
				_ = StagesParser.currSetIndex;
				_ = StagesParser.currStageIndex;
				pauseEnabled = true;
				pocniVreme = true;
				goTrenutak = Time.time;
				shouldRaycast = false;
				if (!StagesParser.bonusLevel && MissionManager.missions[LevelFactory.level - 1].distance > 0 && StagesParser.odgledaoTutorial > 0)
				{
					playerController.measureDistance = true;
					playerController.misijaSaDistance = true;
				}
				if (StagesParser.odgledaoTutorial > 0 && !StagesParser.bonusLevel)
				{
					((Component)((Component)this).transform.Find("POWERS HOLDER").GetChild(0)).GetComponent<Animator>().Play("PowerUpDolazak");
					((MonoBehaviour)this).Invoke("DeaktivirajPowerUpAnimator", 4f);
					StagesParser.brojIgranja++;
				}
			}
			else
			{
				BossScript.Instance.comeIntoTheWorld();
				((Component)goScreen.transform.parent).gameObject.SetActive(false);
				pauseEnabled = true;
				pocniVreme = true;
				goTrenutak = Time.time;
			}
		}
		else if (releasedItem == "Pause Button")
		{
			if (!pauseEnabled)
			{
				return;
			}
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_Pause();
			}
			PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
			PlayerPrefs.Save();
			if (Time.timeScale == 1f)
			{
				if (!neDozvoliPauzu)
				{
					Time.timeScale = 0f;
					((MonoBehaviour)this).StopAllCoroutines();
					Transform child2 = pauseScreenHolder.GetChild(0);
					child2.localPosition += new Vector3(0f, 35f, 0f);
					((Component)pauseScreenHolder.GetChild(0)).GetComponent<Animator>().Play("OpenPopup");
					neDozvoliPauzu = true;
					((Component)pauseButton).GetComponent<Collider>().enabled = false;
				}
			}
			else
			{
				popupZaSpustanje = pauseScreenHolder.GetChild(0);
				((MonoBehaviour)this).Invoke("spustiPopup", 0.5f);
				((Component)pauseScreenHolder.GetChild(0)).GetComponent<Animator>().Play("ClosePopup");
				Time.timeScale = 1f;
			}
		}
		else if (releasedItem == "Menu Button_Pause")
		{
			if (StagesParser.bonusLevel)
			{
				StagesParser.bonusLevel = false;
				StagesParser.dodatnaProveraIzasaoIzBonusa = true;
			}
			((Component)playerController).GetComponent<Rigidbody2D>().isKinematic = true;
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_GoBack();
			}
			if (StagesParser.odgledaoTutorial == 0)
			{
				StagesParser.nivoZaUcitavanje = 1;
				((MonoBehaviour)this).StartCoroutine(closeDoorAndPlay());
				Time.timeScale = 1f;
			}
			else
			{
				StagesParser.nivoZaUcitavanje = 4 + StagesParser.currSetIndex;
				((MonoBehaviour)this).StartCoroutine(closeDoorAndPlay());
				Time.timeScale = 1f;
			}
		}
		else if (releasedItem == "Play Button_Pause")
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_Pause();
			}
			popupZaSpustanje = pauseScreenHolder.GetChild(0);
			((MonoBehaviour)this).Invoke("spustiPopup", 0.5f);
			((Component)pauseScreenHolder.GetChild(0)).GetComponent<Animator>().Play("ClosePopup");
			Time.timeScale = 1f;
		}
		else if (releasedItem == "Restart Button_Pause")
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_RestartLevel();
			}
			((Component)playerController).GetComponent<Rigidbody2D>().isKinematic = true;
			StagesParser.nivoZaUcitavanje = Application.loadedLevel;
			((MonoBehaviour)this).StartCoroutine(closeDoorAndPlay());
			Time.timeScale = 1f;
		}
		else if (releasedItem == "Button Mapa_Failed")
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
			((MonoBehaviour)this).StartCoroutine(closeDoorAndPlay());
		}
		else if (releasedItem == "Button Home_Failed")
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
			((MonoBehaviour)this).StartCoroutine(closeDoorAndPlay());
		}
		else if (releasedItem == "Button Play_Revive")
		{
			((Component)pauseButton).GetComponent<Collider>().enabled = true;
			measureTime = true;
			makniPopup = 0;
			if (keepPlayingCount == 0)
			{
				keepPlayingCount = 1;
			}
			if (StagesParser.currentBananas >= keepPlayingCount)
			{
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_Button_OpenLevel();
				}
				popupZaSpustanje = ((Component)this).transform.Find("Keep Playing HOLDER");
				((MonoBehaviour)this).Invoke("spustiPopup", 0.5f);
				((Component)((Component)this).transform.Find("Keep Playing HOLDER").GetChild(0)).GetComponent<Animator>().Play("ClosePopup");
				StagesParser.currentBananas -= keepPlayingCount;
				PlayerPrefs.SetInt("TotalBananas", StagesParser.currentBananas);
				PlayerPrefs.Save();
				((Component)((Component)this).transform.Find("Keep Playing HOLDER/AnimationHolderGlavni/AnimationHolder/Popup za Keep Playing/Bananas/Banana Number")).GetComponent<TextMesh>().text = StagesParser.currentBananas.ToString();
				((Component)((Component)this).transform.Find("Keep Playing HOLDER/AnimationHolderGlavni/AnimationHolder/Popup za Keep Playing/Bananas/Banana Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
				playerController.SetInvincible();
				keepPlayingCount++;
			}
			else
			{
				((Component)((Component)this).transform.Find("Keep Playing HOLDER/AnimationHolderGlavni/AnimationHolder/Popup za Keep Playing/Bananas")).GetComponent<Animation>().Play();
			}
		}
		else if (releasedItem.Equals("Button Cancel"))
		{
			showFailedScreen();
		}
		else if (releasedItem.Equals("Button Buy Banana"))
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
				((Component)((Component)this).transform.Find("Keep Playing HOLDER/AnimationHolderGlavni/AnimationHolder/Popup za Keep Playing/Bananas/Banana Number")).GetComponent<TextMesh>().text = StagesParser.currentBananas.ToString();
				((Component)((Component)this).transform.Find("Keep Playing HOLDER/AnimationHolderGlavni/AnimationHolder/Popup za Keep Playing/Bananas/Banana Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
				((MonoBehaviour)this).StartCoroutine(StagesParser.Instance.moneyCounter(-StagesParser.bananaCost, ((Component)((Component)this).transform.Find("Keep Playing HOLDER/AnimationHolderGlavni/AnimationHolder/Popup za Keep Playing/CoinsHolder/Coins/Coins Number")).GetComponent<TextMesh>(), hasOutline: true));
			}
			else
			{
				((Component)((Component)this).transform.Find("Keep Playing HOLDER/AnimationHolderGlavni/AnimationHolder/Popup za Keep Playing/CoinsHolder/Coins")).GetComponent<Animation>().Play();
			}
		}
		else if (releasedItem == "Button Restart_Failed")
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_RestartLevel();
			}
			StagesParser.nivoZaUcitavanje = Application.loadedLevel;
			((MonoBehaviour)this).StartCoroutine(closeDoorAndPlay());
		}
		else if (releasedItem == "Button Mapa_Win")
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_GoBack();
			}
			if (StagesParser.odgledaoTutorial == 0)
			{
				StagesParser.loadingTip = 2;
				StagesParser.odgledaoTutorial = 1;
				PlayerPrefs.SetString("OdgledaoTutorial", StagesParser.odgledaoTutorial + "#" + StagesParser.otvaraoShopNekad);
				if (!PlayerPrefs.HasKey("VecPokrenuto"))
				{
					PlayerPrefs.SetInt("VecPokrenuto", 1);
				}
				PlayerPrefs.Save();
			}
			else if (StagesParser.odgledaoTutorial == 1)
			{
				StagesParser.odgledaoTutorial = 2;
				PlayerPrefs.SetString("OdgledaoTutorial", StagesParser.odgledaoTutorial + "#" + StagesParser.otvaraoShopNekad);
				PlayerPrefs.Save();
			}
			if (StagesParser.currSetIndex == 5 && StagesParser.currStageIndex == 19)
			{
				StagesParser.nivoZaUcitavanje = 18;
				((MonoBehaviour)this).StartCoroutine(closeDoorAndPlay());
			}
			else if (StagesParser.isJustOpened)
			{
				StagesParser.isJustOpened = false;
				StagesParser.currStageIndex = 0;
				StagesParser.currSetIndex = StagesParser.lastUnlockedWorldIndex;
				StagesParser.worldToFocus = StagesParser.currSetIndex;
				StagesParser.nivoZaUcitavanje = 3;
				((MonoBehaviour)this).StartCoroutine(closeDoorAndPlay());
			}
			else
			{
				StagesParser.nivoZaUcitavanje = 3;
				((MonoBehaviour)this).StartCoroutine(closeDoorAndPlay());
			}
		}
		else if (releasedItem == "Button Next_Win")
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_NextLevel();
			}
			if (StagesParser.otvaraoShopNekad == 0)
			{
				StagesParser.otvaraoShopNekad = 2;
				PlayerPrefs.SetString("OdgledaoTutorial", StagesParser.odgledaoTutorial + "#" + StagesParser.otvaraoShopNekad);
				PlayerPrefs.Save();
			}
			if (StagesParser.odgledaoTutorial == 0)
			{
				StagesParser.loadingTip = 2;
				StagesParser.odgledaoTutorial = 1;
				PlayerPrefs.SetString("OdgledaoTutorial", StagesParser.odgledaoTutorial + "#" + StagesParser.otvaraoShopNekad);
				if (!PlayerPrefs.HasKey("VecPokrenuto"))
				{
					PlayerPrefs.SetInt("VecPokrenuto", 1);
				}
				PlayerPrefs.Save();
			}
			else if (StagesParser.odgledaoTutorial == 1)
			{
				StagesParser.odgledaoTutorial = 2;
				PlayerPrefs.SetString("OdgledaoTutorial", StagesParser.odgledaoTutorial + "#" + StagesParser.otvaraoShopNekad);
				PlayerPrefs.Save();
			}
			if (StagesParser.bonusLevel)
			{
				StagesParser.bonusLevel = false;
				StagesParser.nivoZaUcitavanje = 4 + StagesParser.currSetIndex;
				((MonoBehaviour)this).StartCoroutine(closeDoorAndPlay());
			}
			if (StagesParser.currSetIndex == 5 && StagesParser.currStageIndex == 19)
			{
				StagesParser.nivoZaUcitavanje = 18;
				((MonoBehaviour)this).StartCoroutine(closeDoorAndPlay());
			}
			else if (StagesParser.isJustOpened)
			{
				StagesParser.nemojDaAnimirasZvezdice = true;
				StagesParser.isJustOpened = false;
				StagesParser.currStageIndex = 0;
				StagesParser.currSetIndex = StagesParser.lastUnlockedWorldIndex;
				StagesParser.worldToFocus = StagesParser.currSetIndex;
				StagesParser.nivoZaUcitavanje = 3;
				((MonoBehaviour)this).StartCoroutine(closeDoorAndPlay());
			}
			else if (StagesParser.NemaRequiredStars_VratiULevele)
			{
				StagesParser.nivoZaUcitavanje = 4 + StagesParser.currSetIndex;
				((MonoBehaviour)this).StartCoroutine(closeDoorAndPlay());
			}
		}
		else if (releasedItem == "Button Restart_Win")
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
			((MonoBehaviour)this).StartCoroutine(closeDoorAndPlay());
		}
		else if (releasedItem == "RateButtonNO")
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
			popupZaSpustanje = ((Component)this).transform.Find("RATE HOLDER").GetChild(0);
			((MonoBehaviour)this).Invoke("spustiPopup", 0.5f);
			((Component)((Component)this).transform.Find("RATE HOLDER").GetChild(0)).GetComponent<Animator>().Play("ClosePopup");
			kliknuoYes = 0;
		}
		else if (releasedItem == "WatchButtonYES")
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
			makniPopup = 5;
			((MonoBehaviour)this).StartCoroutine(checkConnectionForWatchVideo());
		}
		else if (releasedItem == "WatchButtonNO")
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
			popupZaSpustanje = ((Component)this).transform.Find("WATCH VIDEO HOLDER");
			((MonoBehaviour)this).Invoke("spustiPopup", 0.5f);
			((Component)((Component)this).transform.Find("WATCH VIDEO HOLDER").GetChild(0)).GetComponent<Animator>().Play("ClosePopup");
		}
		else if (releasedItem == "Tutorial1_Screen")
		{
			if (TutorialEvents.postavljenCollider)
			{
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_Button_OpenLevel();
				}
				GameObject.Find(releasedItem).SetActive(false);
				Time.timeScale = 1f;
				pauseEnabled = true;
			}
		}
		else if (releasedItem == "Tutorial2_Screen")
		{
			if (TutorialEvents.postavljenCollider)
			{
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_Button_OpenLevel();
				}
				GameObject.Find(releasedItem).SetActive(false);
				Time.timeScale = 1f;
				pauseEnabled = true;
			}
		}
		else if (releasedItem.Contains("Tutorial"))
		{
			((Component)GameObject.Find(releasedItem).transform.parent.parent).GetComponent<Animator>().Play("ClosePopup");
			Time.timeScale = 1f;
			pauseEnabled = true;
		}
		else if (releasedItem == "Power_Double Coins Interface")
		{
			if (StagesParser.powerup_doublecoins > 0)
			{
				((Component)powerCard_CoinX2).GetComponent<Collider>().enabled = false;
				((Renderer)((Component)powerCard_CoinX2.Find("Disabled")).GetComponent<SpriteRenderer>()).enabled = true;
				StagesParser.powerup_doublecoins--;
				((Component)powerCard_CoinX2.Find("Number")).GetComponent<TextMesh>().text = StagesParser.powerup_doublecoins.ToString();
				kupljenDoubleCoins = true;
				ApplyPowerUp(2);
				playerController.doublecoins = true;
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_CollectPowerUp();
				}
				PlayerPrefs.SetString("PowerUps", StagesParser.powerup_doublecoins + "#" + StagesParser.powerup_magnets + "#" + StagesParser.powerup_shields);
				PlayerPrefs.Save();
			}
			else if (StagesParser.cost_doublecoins < StagesParser.currentMoney)
			{
				((Component)powerCard_CoinX2).GetComponent<Collider>().enabled = false;
				((Renderer)((Component)powerCard_CoinX2.Find("Disabled")).GetComponent<SpriteRenderer>()).enabled = true;
				kupljenDoubleCoins = true;
				ApplyPowerUp(2);
				playerController.doublecoins = true;
				StagesParser.currentMoney -= StagesParser.cost_doublecoins;
				PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
				PlayerPrefs.Save();
				((MonoBehaviour)this).StartCoroutine(StagesParser.Instance.moneyCounter(-StagesParser.cost_doublecoins, ((Component)powerCard_Magnet.parent.Find("CoinsHolder/Coins/Coins Number")).GetComponent<TextMesh>(), hasOutline: true));
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_CollectPowerUp();
				}
			}
			else
			{
				((Component)powerCard_CoinX2.parent.Find("CoinsHolder/Coins")).GetComponent<Animation>().Play();
			}
		}
		else if (releasedItem == "Power_Magnet Interface")
		{
			if (StagesParser.powerup_magnets > 0)
			{
				((Component)powerCard_Magnet).GetComponent<Collider>().enabled = false;
				((Renderer)((Component)powerCard_Magnet.Find("Disabled")).GetComponent<SpriteRenderer>()).enabled = true;
				StagesParser.powerup_magnets--;
				((Component)powerCard_Magnet.Find("Number")).GetComponent<TextMesh>().text = StagesParser.powerup_magnets.ToString();
				kupljenMagnet = true;
				ApplyPowerUp(1);
				playerController.magnet = true;
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_CollectPowerUp();
				}
				PlayerPrefs.SetString("PowerUps", StagesParser.powerup_doublecoins + "#" + StagesParser.powerup_magnets + "#" + StagesParser.powerup_shields);
				PlayerPrefs.Save();
			}
			else if (StagesParser.cost_magnet < StagesParser.currentMoney)
			{
				((Component)powerCard_Magnet).GetComponent<Collider>().enabled = false;
				((Renderer)((Component)powerCard_Magnet.Find("Disabled")).GetComponent<SpriteRenderer>()).enabled = true;
				kupljenMagnet = true;
				ApplyPowerUp(1);
				playerController.magnet = true;
				StagesParser.currentMoney -= StagesParser.cost_magnet;
				PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
				PlayerPrefs.Save();
				((MonoBehaviour)this).StartCoroutine(StagesParser.Instance.moneyCounter(-StagesParser.cost_magnet, ((Component)powerCard_Magnet.parent.Find("CoinsHolder/Coins/Coins Number")).GetComponent<TextMesh>(), hasOutline: true));
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_CollectPowerUp();
				}
			}
			else
			{
				((Component)powerCard_Magnet.parent.Find("CoinsHolder/Coins")).GetComponent<Animation>().Play();
			}
		}
		else if (releasedItem == "Power_Shield Interface")
		{
			if (StagesParser.powerup_shields > 0)
			{
				((Component)powerCard_Shield).GetComponent<Collider>().enabled = false;
				((Renderer)((Component)powerCard_Shield.Find("Disabled")).GetComponent<SpriteRenderer>()).enabled = true;
				StagesParser.powerup_shields--;
				((Component)powerCard_Shield.Find("Number")).GetComponent<TextMesh>().text = StagesParser.powerup_shields.ToString();
				kupljenShield = true;
				ApplyPowerUp(3);
				playerController.activeShield = true;
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_CollectPowerUp();
				}
				PlayerPrefs.SetString("PowerUps", StagesParser.powerup_doublecoins + "#" + StagesParser.powerup_magnets + "#" + StagesParser.powerup_shields);
				PlayerPrefs.Save();
			}
			else if (StagesParser.cost_shield < StagesParser.currentMoney)
			{
				((Component)powerCard_Shield).GetComponent<Collider>().enabled = false;
				((Renderer)((Component)powerCard_Shield.Find("Disabled")).GetComponent<SpriteRenderer>()).enabled = true;
				kupljenShield = true;
				ApplyPowerUp(3);
				playerController.activeShield = true;
				StagesParser.currentMoney -= StagesParser.cost_shield;
				PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
				PlayerPrefs.Save();
				((MonoBehaviour)this).StartCoroutine(StagesParser.Instance.moneyCounter(-StagesParser.cost_shield, ((Component)powerCard_Magnet.parent.Find("CoinsHolder/Coins/Coins Number")).GetComponent<TextMesh>(), hasOutline: true));
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_CollectPowerUp();
				}
			}
			else
			{
				((Component)powerCard_Shield.parent.Find("CoinsHolder/Coins")).GetComponent<Animation>().Play();
			}
		}
		else if (releasedItem.Contains("RateGame"))
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_GetStar();
			}
			int num = int.Parse(releasedItem.Substring(8)) + 1;
			Transform val = rateHolder.Find("RateGameHolder");
			for (int i = 0; i < val.childCount; i++)
			{
				((Component)val.GetChild(i).GetChild(0)).GetComponent<Renderer>().enabled = false;
			}
			for (int j = 0; j < num; j++)
			{
				((Component)val.Find("RateGame" + j).GetChild(0)).GetComponent<Renderer>().enabled = true;
			}
			if (num > 3)
			{
				((MonoBehaviour)this).StartCoroutine(checkConnectionForRate());
			}
			((Component)((Component)this).transform.Find("RATE HOLDER").GetChild(0)).GetComponent<Animator>().Play("ClosePopup");
		}
		else if (releasedItem.Equals("FB Share"))
		{
			makniPopup = 4;
			((MonoBehaviour)this).StartCoroutine(checkConnectionForShare());
		}
		else if (releasedItem.Equals("FB Invite Large"))
		{
			makniPopup = 4;
			((MonoBehaviour)this).StartCoroutine(checkConnectionForInvite());
		}
		else if (releasedItem.Contains("Friend Level"))
		{
			makniPopup = 4;
			((MonoBehaviour)this).StartCoroutine(checkConnectionForInvite());
		}
		else if (releasedItem.Equals("Button_CheckOK"))
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
			makniPopup = 0;
			((MonoBehaviour)this).StartCoroutine(CheckInternetConnection.Instance.ClosePopup());
		}
	}

	private IEnumerator RateGame(string url)
	{
		((MonoBehaviour)this).StartCoroutine(CheckInternetConnection.Instance.checkInternetConnection());
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
	}

	private IEnumerator openingPage(string url)
	{
		WWW www = new WWW("http://www.google.com");
		yield return www;
		if (!string.IsNullOrEmpty(www.error))
		{
			((Component)rateHolder.Find("RateButtonYES/Text")).GetComponent<TextMesh>().text = "Retry";
			((Component)rateHolder.Find("RateButtonYES/Text")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			((Component)rateHolder.Find("Text 2")).GetComponent<TextMesh>().text = "No Internet\nConnection!";
			((Component)rateHolder.Find("Text 2")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		}
		else
		{
			popupZaSpustanje = ((Component)this).transform.Find("RATE HOLDER").GetChild(0);
			((MonoBehaviour)this).Invoke("spustiPopup", 0.5f);
			((Component)((Component)this).transform.Find("RATE HOLDER").GetChild(0)).GetComponent<Animator>().Play("ClosePopup");
			PlayerPrefs.SetInt("AlreadyRated", 1);
			PlayerPrefs.Save();
			Application.OpenURL(url);
		}
	}

	private IEnumerator backToMenu()
	{
		((MonoBehaviour)this).StartCoroutine(GameObject.Find("Menu_Pause").GetComponent<Animation>().Play("ClickMenu", useTimeScale: false, delegate
		{
			helpBool = true;
		}));
		while (!helpBool)
		{
			yield return null;
		}
		helpBool = false;
		StagesParser.nivoZaUcitavanje = 4 + StagesParser.currSetIndex;
		((MonoBehaviour)this).StartCoroutine(closeDoorAndPlay());
		Time.timeScale = 1f;
	}

	private IEnumerator restartLevel()
	{
		((MonoBehaviour)this).StartCoroutine(GameObject.Find("Restart_Pause").GetComponent<Animation>().Play("ClickRestart", useTimeScale: false, delegate
		{
			helpBool = true;
		}));
		while (!helpBool)
		{
			yield return null;
		}
		helpBool = false;
		StagesParser.nivoZaUcitavanje = Application.loadedLevel;
		((MonoBehaviour)this).StartCoroutine(closeDoorAndPlay());
		Time.timeScale = 1f;
	}

	private IEnumerator unPause()
	{
		((MonoBehaviour)this).StartCoroutine(GameObject.Find("Play_Pause").GetComponent<Animation>().Play("ClickMenu", useTimeScale: false, delegate
		{
			helpBool = true;
		}));
		while (!helpBool)
		{
			yield return null;
		}
		helpBool = false;
	}

	private void showFailedScreen()
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		if (PlaySounds.BackgroundMusic_Gameplay.isPlaying)
		{
			PlaySounds.Stop_BackgroundMusic_Gameplay();
		}
		if (PlaySounds.soundOn)
		{
			PlaySounds.Play_Level_Failed_Popup();
		}
		pauseEnabled = false;
		StagesParser.numberGotKilled++;
		if (StagesParser.numberGotKilled % 1 == 0)
		{
			StagesParser.numberGotKilled = 0;
		}
		((Component)FailedScreenHolder.parent).transform.position = new Vector3(((Component)guiCamera).transform.position.x, ((Component)guiCamera).transform.position.y, ((Component)FailedScreenHolder).transform.position.z);
		((Component)FailedScreenHolder).gameObject.SetActive(true);
		((Component)FailedScreenHolder.parent).GetComponent<Animator>().Play("LevelWinLoseDolazak");
		((Component)FailedScreenHolder).GetComponent<Animator>().Play("LevelLoseUlaz");
		PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
		PlayerPrefs.Save();
		try
		{
			if (interstitial.IsLoaded())
			{
				interstitial.Show();
			}
		}
		catch
		{
			Debug.Log((object)"LEVEL FAILED - INTERSTITIAL NOT INITIALIZED");
		}
	}

	private void OpaliPartikle()
	{
		((Component)Win_CompletedScreenHolder.parent.Find("Partikli Level Finish Win")).gameObject.SetActive(true);
	}

	private void ShowWinScreen()
	{
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		pauseEnabled = false;
		measureTime = false;
		if (StagesParser.bonusLevel)
		{
			((Component)((Component)this).transform.Find("Level Win_Lose SCENA/Popup za WIN HOLDER_BONUS/Popup za WIN/Header za win popup/Text/Level")).GetComponent<TextMesh>().text = LanguageManager.BonusLevel;
			((Component)((Component)this).transform.Find("Level Win_Lose SCENA/Popup za WIN HOLDER_BONUS/Popup za WIN/Header za win popup/Text/Level")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
			((Component)((Component)this).transform.Find("Level Win_Lose SCENA/Popup za WIN HOLDER_BONUS/Popup za WIN/Header za win popup/Text/Completed")).GetComponent<TextMesh>().text = LanguageManager.Completed;
			((Component)((Component)this).transform.Find("Level Win_Lose SCENA/Popup za WIN HOLDER_BONUS/Popup za WIN/Header za win popup/Text/Completed")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
			((Component)Win_CompletedScreenHolder.Find("Friends FB level WIN")).gameObject.SetActive(false);
			((Component)Win_CompletedScreenHolder.Find("FB Share")).gameObject.SetActive(false);
			((Component)Win_CompletedScreenHolder).gameObject.SetActive(true);
			if (!FB.IsLoggedIn)
			{
				((Component)Win_CompletedScreenHolder.Find("FB Invite Large")).gameObject.SetActive(false);
			}
			if (PlaySounds.BackgroundMusic_Gameplay.isPlaying)
			{
				PlaySounds.Stop_BackgroundMusic_Gameplay();
			}
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Level_Completed_Popup();
			}
			Win_CompletedScreenHolder.parent.position = new Vector3(((Component)guiCamera).transform.position.x, ((Component)guiCamera).transform.position.y, ((Component)Win_CompletedScreenHolder).transform.position.z);
			((Component)Win_CompletedScreenHolder.parent).GetComponent<Animator>().Play("LevelWinLoseDolazak");
			((Component)Win_CompletedScreenHolder).GetComponent<Animator>().Play("LevelWinUlaz");
			((MonoBehaviour)this).Invoke("OpaliPartikle", 1.2f);
			StagesParser.currentMoney += coinsCollected;
			PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
			PlayerPrefs.Save();
			starManager.GoBack();
			((MonoBehaviour)this).StartCoroutine(WaitForSave());
			return;
		}
		((Component)Win_CompletedScreenHolder.Find("Popup za WIN/Header za win popup/Text/Level No")).GetComponent<TextMesh>().text = (StagesParser.currStageIndex + 1).ToString();
		((Component)Win_CompletedScreenHolder.Find("Popup za WIN/Header za win popup/Text/Level No")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		StagesParser.currentMoney += coinsCollected;
		PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
		PlayerPrefs.Save();
		if (aktivnoVreme <= 40f)
		{
			points += MissionManager.points3Stars;
		}
		pointsForDisplay = points;
		int num = int.Parse(StagesParser.allLevels[StagesParser.currentLevel - 1].Split(new char[1] { '#' })[2]);
		if (num >= points)
		{
			points = num;
		}
		else
		{
			int num2 = points - num;
			StagesParser.currentPoints += num2;
			PlayerPrefs.SetInt("TotalPoints", StagesParser.currentPoints);
			PlayerPrefs.Save();
		}
		if (!FB.IsLoggedIn)
		{
			((Component)Win_CompletedScreenHolder.Find("FB Invite Large")).gameObject.SetActive(false);
			((Component)Win_CompletedScreenHolder.Find("FB Share")).gameObject.SetActive(false);
			((Component)Win_CompletedScreenHolder.Find("Friends FB level WIN")).gameObject.SetActive(false);
		}
		else
		{
			getFriendsScoresOnLevel(StagesParser.currentLevel);
		}
		((Component)Win_CompletedScreenHolder).gameObject.SetActive(true);
		if (PlaySounds.BackgroundMusic_Gameplay.isPlaying)
		{
			PlaySounds.Stop_BackgroundMusic_Gameplay();
		}
		if (PlaySounds.soundOn)
		{
			PlaySounds.Play_Level_Completed_Popup();
		}
		Win_CompletedScreenHolder.parent.position = new Vector3(((Component)guiCamera).transform.position.x, ((Component)guiCamera).transform.position.y, ((Component)Win_CompletedScreenHolder).transform.position.z);
		((Component)Win_CompletedScreenHolder.parent).GetComponent<Animator>().Play("LevelWinLoseDolazak");
		((Component)Win_CompletedScreenHolder).GetComponent<Animator>().Play("LevelWinUlaz");
		((MonoBehaviour)this).Invoke("OpaliPartikle", 1.2f);
		StagesParser.currentBananas += bananas;
		PlayerPrefs.SetInt("TotalBananas", StagesParser.currentBananas);
		PlayerPrefs.Save();
		StagesParser.ServerUpdate = 1;
		((MonoBehaviour)this).StartCoroutine(waitForStars());
	}

	private IEnumerator waitForStars()
	{
		if (MissionManager.points3Stars > 0)
		{
			((Component)Win_CompletedScreenHolder.Find("Popup za WIN/ProgressBarHolder/3StarsPtsText")).GetComponent<TextMesh>().text = MissionManager.points3Stars.ToString();
		}
		else
		{
			((Component)Win_CompletedScreenHolder.Find("Popup za WIN/ProgressBarHolder/3StarsPtsText")).GetComponent<TextMesh>().text = "500";
		}
		((Component)Win_CompletedScreenHolder.Find("Popup za WIN/ProgressBarHolder/3StarsPtsText")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		StagesParser.saving = false;
		yield return (object)new WaitForSeconds(2f);
		Transform progressBarPivot = Win_CompletedScreenHolder.Find("Popup za WIN/ProgressBarHolder/ProgressBarPivot");
		TextMesh scoreDisplay = ((Component)Win_CompletedScreenHolder.Find("Popup za WIN/Polje za unos poena Na Level WIN/Points Number level win")).GetComponent<TextMesh>();
		int currentProgressBarPoints = 0;
		bool starActivated2 = false;
		float targetScaleX = Mathf.Clamp01((float)pointsForDisplay / (float)MissionManager.points3Stars);
		star1.GetComponent<Animation>().Play();
		((Component)star1.transform.Find("Star Vatromet")).gameObject.SetActive(true);
		starsGained = 1;
		if (PlaySounds.soundOn)
		{
			PlaySounds.Play_GetStar();
		}
		int step = (int)((float)pointsForDisplay * Time.deltaTime / 2f / targetScaleX);
		while (progressBarPivot.localScale.x < targetScaleX || currentProgressBarPoints <= pointsForDisplay)
		{
			if (progressBarPivot.localScale.x >= 0.7f && !starActivated2)
			{
				starActivated2 = true;
				starsGained = 2;
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_GetStar2();
				}
				star2.GetComponent<Animation>().Play();
				((Component)star2.transform.Find("Star Vatromet")).gameObject.SetActive(true);
			}
			currentProgressBarPoints += step;
			scoreDisplay.text = currentProgressBarPoints.ToString();
			((Component)scoreDisplay).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			if (currentProgressBarPoints > pointsForDisplay)
			{
				scoreDisplay.text = pointsForDisplay.ToString();
				((Component)scoreDisplay).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			}
			yield return null;
			progressBarPivot.localScale = new Vector3(Mathf.MoveTowards(progressBarPivot.localScale.x, targetScaleX, Time.deltaTime / 2f), progressBarPivot.localScale.y, progressBarPivot.localScale.z);
		}
		starActivated2 = false;
		if (progressBarPivot.localScale.x == 1f && !starActivated2)
		{
			starsGained = 3;
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_GetStar3();
			}
			star3.GetComponent<Animation>().Play();
			((Component)star3.transform.Find("Star Vatromet")).gameObject.SetActive(true);
		}
		starManager.GoBack();
		((MonoBehaviour)this).StartCoroutine(WaitForSave());
	}

	private IEnumerator waitForStars1()
	{
		StagesParser.saving = false;
		yield return (object)new WaitForSeconds(2f);
		star1.GetComponent<Animation>().Play();
		((Component)star1.transform.Find("Star Vatromet")).gameObject.SetActive(true);
		starsGained = 1;
		if (PlaySounds.soundOn)
		{
			PlaySounds.Play_GetStar();
		}
		yield return (object)new WaitForSeconds(0.25f);
		if ((float)points >= (float)MissionManager.points3Stars * 0.7f)
		{
			starsGained = 2;
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_GetStar2();
			}
			star2.GetComponent<Animation>().Play();
			((Component)star2.transform.Find("Star Vatromet")).gameObject.SetActive(true);
			yield return (object)new WaitForSeconds(0.25f);
		}
		if (points >= MissionManager.points3Stars)
		{
			starsGained = 3;
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_GetStar3();
			}
			star3.GetComponent<Animation>().Play();
			((Component)star3.transform.Find("Star Vatromet")).gameObject.SetActive(true);
		}
		starManager.GoBack();
		((MonoBehaviour)this).StartCoroutine(WaitForSave());
	}

	private IEnumerator WaitForSave()
	{
		try
		{
			if (interstitial.IsLoaded())
			{
				interstitial.Show();
			}
		}
		catch
		{
			Debug.Log((object)"LEVEL COMPLETED - INTERSTITIAL NOT INITIALIZED");
		}
		while (!StagesParser.saving)
		{
			yield return null;
		}
		if (FB.IsLoggedIn)
		{
			FacebookManager.FacebookObject.scoreToSet = StagesParser.currentPoints;
			FacebookManager.FacebookObject.proveraPublish_ActionPermisije();
		}
		Transform val = Win_CompletedScreenHolder.Find("Popup za WIN/Button Mapa_Win");
		Transform val2 = Win_CompletedScreenHolder.Find("Popup za WIN/Button Next_Win");
		Transform obj2 = Win_CompletedScreenHolder.Find("Popup za WIN/Button Restart_Win");
		((Component)val).GetComponent<Collider>().enabled = true;
		((Component)val2).GetComponent<Collider>().enabled = true;
		((Component)obj2).GetComponent<Collider>().enabled = true;
		StagesParser.saving = false;
		if (StagesParser.bonusLevel)
		{
			yield break;
		}
		if (!PlayerPrefs.HasKey("AlreadyRated") && StagesParser.currStageIndex == 5)
		{
			Transform child = ((Component)this).transform.Find("RATE HOLDER").GetChild(0);
			child.localPosition += new Vector3(0f, 35f, 0f);
			((Component)((Component)this).transform.Find("RATE HOLDER").GetChild(0)).GetComponent<Animator>().Play("OpenPopup");
			makniPopup = 1;
		}
		int currentLevel = StagesParser.currentLevel;
		currentLevel.ToString();
		if (PlayerPrefs.HasKey("PoslednjiOdigranNivo"))
		{
			if (PlayerPrefs.GetInt("PoslednjiOdigranNivo") >= currentLevel)
			{
				PlayerPrefs.GetInt("PoslednjiOdigranNivo");
				yield break;
			}
			PlayerPrefs.SetInt("PoslednjiOdigranNivo", currentLevel);
			PlayerPrefs.Save();
		}
		else
		{
			PlayerPrefs.SetInt("PoslednjiOdigranNivo", currentLevel);
			PlayerPrefs.Save();
		}
	}

	public void UnlockWorld(int world)
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		Transform obj = ((Component)this).transform.Find("WORLD UNLOCKED TURBO HOLDER");
		((Component)obj.Find("WORLD UNLOCKED HOLDER/AllWorldPicturesHolder/WorldBg" + world)).gameObject.SetActive(true);
		((Component)obj.Find("WORLD UNLOCKED HOLDER/Number Holder/WorldNumber" + world)).gameObject.SetActive(true);
		obj.position = ((Component)Camera.main).transform.position + Vector3.forward * 2f;
		obj.localScale = obj.localScale * Camera.main.orthographicSize / 7.5f;
		((Component)obj).gameObject.SetActive(true);
	}

	private void CoinAdded()
	{
		if (PowerUp_doubleCoins)
		{
			coinsCollected += 2;
		}
		else
		{
			coinsCollected++;
		}
		coinsCollectedText.text = coinsCollected.ToString();
	}

	private string RaycastFunction(Vector3 vector)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		RaycastHit val = default(RaycastHit);
		if (Physics.Raycast(guiCamera.ScreenPointToRay(vector), ref val))
		{
			return ((Object)((RaycastHit)(ref val)).collider).name;
		}
		if (Physics.Raycast(Camera.main.ScreenPointToRay(vector), ref val))
		{
			return ((Object)((RaycastHit)(ref val)).collider).name;
		}
		return "";
	}

	public void ApplyPowerUp(int x)
	{
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		switch (x)
		{
		case 1:
			((MonoBehaviour)this).CancelInvoke("pustiAnimacijuBlinkanja");
			((Component)((Component)this).transform.Find("Gameplay Scena Interface/_TopLeft/Magnet Icon Holder")).GetComponent<Animation>().Play();
			((MonoBehaviour)this).Invoke("pustiAnimacijuBlinkanja", 17.5f);
			coinMagnet.SetActive(true);
			break;
		case -1:
			coinMagnet.SetActive(false);
			((MonoBehaviour)this).CancelInvoke("pustiAnimacijuBlinkanja");
			((Component)this).transform.Find("Gameplay Scena Interface/_TopLeft/Magnet Icon Holder/Icon Animation").localScale = new Vector3(0.0001f, 0.0001f, 1f);
			break;
		case 2:
			if (!PowerUp_doubleCoins)
			{
				((Component)((Component)this).transform.Find("Gameplay Scena Interface/_TopLeft/DoubleCoins Icon Holder")).GetComponent<Animation>().Play();
				PowerUp_doubleCoins = true;
				LevelFactory.instance.doubleCoinsCollected = true;
			}
			break;
		case -2:
			PowerUp_doubleCoins = false;
			((Component)this).transform.Find("Gameplay Scena Interface/_TopLeft/DoubleCoins Icon Holder/Icon Animation").localScale = new Vector3(0.0001f, 0.0001f, 1f);
			break;
		case 3:
			PowerUp_shield = true;
			shield.SetActive(true);
			break;
		case -3:
			PowerUp_shield = false;
			shield.SetActive(false);
			break;
		}
	}

	private void pustiAnimacijuBlinkanja()
	{
		if (!MissionManager.missionsComplete)
		{
			((Component)((Component)this).transform.Find("Gameplay Scena Interface/_TopLeft/Magnet Icon Holder")).GetComponent<Animation>().Play("PowerUp Icon Disappear NEW");
			((MonoBehaviour)this).Invoke("UgasiMagnet", 1.5f);
		}
	}

	private void UgasiMagnet()
	{
		coinMagnet.SetActive(false);
	}

	private IEnumerator showPickPowers()
	{
		((Component)PickPowers).gameObject.SetActive(true);
		((MonoBehaviour)this).Invoke("DisappearPickPowers", 3.5f);
		yield return (object)new WaitForSeconds(0.85f);
		((Behaviour)((Component)powerCard_CoinX2).GetComponent<Animator>()).enabled = true;
		((Behaviour)((Component)powerCard_Magnet).GetComponent<Animator>()).enabled = true;
		((Behaviour)((Component)powerCard_Shield).GetComponent<Animator>()).enabled = true;
	}

	private void DisappearPickPowers()
	{
		if (!kupljenMagnet)
		{
			((Component)powerCard_Magnet).GetComponent<Animator>().Play("PowerCardCoinx2_Disappear");
			((Component)powerCard_Magnet).GetComponent<Collider>().enabled = false;
		}
		if (!kupljenShield)
		{
			((Component)powerCard_Shield).GetComponent<Animator>().Play("PowerCardCoinx2_Disappear");
			((Component)powerCard_Shield).GetComponent<Collider>().enabled = false;
		}
		if (!kupljenDoubleCoins)
		{
			((Component)powerCard_CoinX2).GetComponent<Animator>().Play("PowerCardCoinx2_Disappear");
			((Component)powerCard_CoinX2).GetComponent<Collider>().enabled = false;
		}
		PickPowers.parent = null;
	}

	private void RemoveFog()
	{
		((Component)Camera.main).GetComponent<Animator>().Play("FogOfWar_Remove");
	}

	private void ShowKeepPlayingScreen()
	{
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		measureTime = false;
		Transform obj = ((Component)this).transform.Find("Keep Playing HOLDER/AnimationHolderGlavni/AnimationHolder/Popup za Keep Playing");
		((Component)obj.Find("Text/Banana Number")).GetComponent<TextMesh>().text = (keepPlayingCount + 1).ToString();
		((Component)obj.Find("Text/Banana Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		((Component)obj.Find("CoinsHolder/Coins/Coins Number")).GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
		((Component)obj.Find("CoinsHolder/Coins/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		((Component)obj.Find("Bananas/Banana Number")).GetComponent<TextMesh>().text = StagesParser.currentBananas.ToString();
		((Component)obj.Find("Bananas/Banana Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		((Component)obj.Find("Text/BananaCost")).GetComponent<TextMesh>().text = StagesParser.bananaCost.ToString();
		((Component)obj.Find("Text/BananaCost")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		Transform child = ((Component)this).transform.Find("Keep Playing HOLDER").GetChild(0);
		child.localPosition += new Vector3(0f, 35f, 0f);
		((Component)((Component)this).transform.Find("Keep Playing HOLDER").GetChild(0)).GetComponent<Animator>().Play("OpenPopup");
		makniPopup = 3;
	}

	private IEnumerator closeDoorAndPlay()
	{
		((Component)((Component)this).transform.Find("LOADING HOLDER NEW/Loading Animation Vrata")).GetComponent<Animator>().Play("Loading Zidovi Dolazak");
		yield return (object)new WaitForSeconds(0.75f);
		Application.LoadLevel(2);
	}

	private void refreshText()
	{
		((Component)((Component)this).transform.Find("GO screen/GO screen Text")).GetComponent<TextMesh>().text = LanguageManager.TapScreenToStart;
		((Component)((Component)this).transform.Find("GO screen/GO screen Text")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true, increaseFont: false);
		((Component)((Component)this).transform.Find("Keep Playing HOLDER/AnimationHolderGlavni/AnimationHolder/Popup za Keep Playing/Button Buy Banana/BuyText")).GetComponent<TextMesh>().text = LanguageManager.Buy;
		((Component)((Component)this).transform.Find("Keep Playing HOLDER/AnimationHolderGlavni/AnimationHolder/Popup za Keep Playing/Button Buy Banana/BuyText")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)((Component)this).transform.Find("Keep Playing HOLDER/AnimationHolderGlavni/AnimationHolder/Popup za Keep Playing/Text/Keep Playing")).GetComponent<TextMesh>().text = LanguageManager.KeepPlaying;
		((Component)((Component)this).transform.Find("Keep Playing HOLDER/AnimationHolderGlavni/AnimationHolder/Popup za Keep Playing/Text/Keep Playing")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)((Component)this).transform.Find("Level Win_Lose SCENA/Popup za LOSE HOLDER/Popup za LOSE/Header za LOSE popup/Text/Level Failed")).GetComponent<TextMesh>().text = LanguageManager.LevelFailed;
		((Component)((Component)this).transform.Find("Level Win_Lose SCENA/Popup za LOSE HOLDER/Popup za LOSE/Header za LOSE popup/Text/Level Failed")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)Win_CompletedScreenHolder.Find("FB Invite Large/Text/Invite")).GetComponent<TextMesh>().text = LanguageManager.Invite;
		((Component)Win_CompletedScreenHolder.Find("FB Invite Large/Text/Invite")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		if (!StagesParser.bonusLevel)
		{
			((Component)Win_CompletedScreenHolder.Find("FB Share/Text/Share")).GetComponent<TextMesh>().text = LanguageManager.Share;
			((Component)Win_CompletedScreenHolder.Find("FB Share/Text/Share")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		}
		((Component)Win_CompletedScreenHolder.Find("Friends FB level WIN/Friends Level Win 1 HOLDER/FB/Fb Invite 1")).GetComponent<TextMesh>().text = LanguageManager.Invite;
		((Component)Win_CompletedScreenHolder.Find("Friends FB level WIN/Friends Level Win 2 HOLDER/FB/Fb Invite 1")).GetComponent<TextMesh>().text = LanguageManager.Invite;
		((Component)Win_CompletedScreenHolder.Find("Friends FB level WIN/Friends Level Win 3 HOLDER/FB/Fb Invite 1")).GetComponent<TextMesh>().text = LanguageManager.Invite;
		((Component)Win_CompletedScreenHolder.Find("Friends FB level WIN/Friends Level Win 4 HOLDER/FB/Fb Invite 1")).GetComponent<TextMesh>().text = LanguageManager.Invite;
		((Component)Win_CompletedScreenHolder.Find("Friends FB level WIN/Friends Level Win 5 HOLDER/FB/Fb Invite 1")).GetComponent<TextMesh>().text = LanguageManager.Invite;
		((Component)Win_CompletedScreenHolder.Find("Friends FB level WIN/Friends Level Win 1 HOLDER/FB/Fb Invite 1")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)Win_CompletedScreenHolder.Find("Friends FB level WIN/Friends Level Win 2 HOLDER/FB/Fb Invite 1")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)Win_CompletedScreenHolder.Find("Friends FB level WIN/Friends Level Win 3 HOLDER/FB/Fb Invite 1")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)Win_CompletedScreenHolder.Find("Friends FB level WIN/Friends Level Win 4 HOLDER/FB/Fb Invite 1")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)Win_CompletedScreenHolder.Find("Friends FB level WIN/Friends Level Win 5 HOLDER/FB/Fb Invite 1")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)Win_CompletedScreenHolder.Find("Popup za WIN/Header za win popup/Text/Level")).GetComponent<TextMesh>().text = LanguageManager.Level;
		((Component)Win_CompletedScreenHolder.Find("Popup za WIN/Header za win popup/Text/Level")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)Win_CompletedScreenHolder.Find("Popup za WIN/Header za win popup/Text/Completed")).GetComponent<TextMesh>().text = LanguageManager.Completed;
		((Component)Win_CompletedScreenHolder.Find("Popup za WIN/Header za win popup/Text/Completed")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)((Component)this).transform.Find("PAUSE HOLDER/AnimationHolderGlavni/AnimationHolder/PAUSE PopUp/PauseText")).GetComponent<TextMesh>().text = LanguageManager.Pause;
		((Component)((Component)this).transform.Find("PAUSE HOLDER/AnimationHolderGlavni/AnimationHolder/PAUSE PopUp/PauseText")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)((Component)this).transform.Find("RATE HOLDER/AnimationHolderGlavni/AnimationHolder/RATE Popup/Rate")).GetComponent<TextMesh>().text = LanguageManager.RateThisGame;
		((Component)((Component)this).transform.Find("RATE HOLDER/AnimationHolderGlavni/AnimationHolder/RATE Popup/Rate")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)((Component)this).transform.Find("RATE HOLDER/AnimationHolderGlavni/AnimationHolder/RATE Popup/Text 1")).GetComponent<TextMesh>().text = LanguageManager.HowWouldYouRate;
		((Component)((Component)this).transform.Find("RATE HOLDER/AnimationHolderGlavni/AnimationHolder/RATE Popup/Text 1")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)((Component)this).transform.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/Free Coins")).GetComponent<TextMesh>().text = LanguageManager.FreeCoins;
		((Component)((Component)this).transform.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/Free Coins")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)((Component)this).transform.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/WatchButtonNO/Text")).GetComponent<TextMesh>().text = LanguageManager.No;
		((Component)((Component)this).transform.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/WatchButtonNO/Text")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)((Component)this).transform.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/WatchButtonYES/Text")).GetComponent<TextMesh>().text = LanguageManager.Yes;
		((Component)((Component)this).transform.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/WatchButtonYES/Text")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)((Component)this).transform.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/WatchVideoText")).GetComponent<TextMesh>().text = LanguageManager.WatchVideo;
		((Component)((Component)this).transform.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/WatchVideoText")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)((Component)this).transform.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/NotAvailableText")).GetComponent<TextMesh>().text = LanguageManager.NoVideo;
		((Component)((Component)this).transform.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/NotAvailableText")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
	}

	private void getFriendsScoresOnLevel(int level)
	{
		if (!popunioSlike)
		{
			popunioSlike = true;
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
		List<scoreAndIndex> list = new List<scoreAndIndex>();
		for (int k = 0; k < FacebookManager.ListaStructPrijatelja.Count; k++)
		{
			scoreAndIndex item = default(scoreAndIndex);
			if (level > FacebookManager.ListaStructPrijatelja[k].scores.Count)
			{
				continue;
			}
			item.index = k;
			item.score = FacebookManager.ListaStructPrijatelja[k].scores[level - 1];
			if (FacebookManager.ListaStructPrijatelja[k].PrijateljID == FacebookManager.User)
			{
				int num = points;
				if (num > FacebookManager.ListaStructPrijatelja[k].scores[level - 1])
				{
					item.score = num;
				}
			}
			list.Add(item);
		}
		scoreAndIndex scoreAndIndex = default(scoreAndIndex);
		scoreAndIndex scoreAndIndex2 = default(scoreAndIndex);
		scoreAndIndex2.score = 0;
		int num2 = 0;
		bool flag = false;
		for (int l = 0; l < list.Count; l++)
		{
			scoreAndIndex = list[l];
			num2 = l;
			flag = false;
			for (int m = l + 1; m < list.Count; m++)
			{
				if (scoreAndIndex.score < list[m].score)
				{
					scoreAndIndex = list[m];
					num2 = m;
					flag = true;
				}
			}
			if (flag)
			{
				scoreAndIndex2 = list[l];
				list[l] = list[num2];
				list[num2] = scoreAndIndex2;
			}
		}
		int num3 = 1;
		bool flag2 = false;
		int num4 = 1;
		for (int n = 0; n < list.Count; n++)
		{
			if (FacebookManager.ListaStructPrijatelja[list[n].index].PrijateljID == FacebookManager.User)
			{
				num4 = num3;
			}
			if (n < 5)
			{
				if (list[n].score > 0 || FacebookManager.ListaStructPrijatelja[list[n].index].PrijateljID == FacebookManager.User)
				{
					Transform val = Win_CompletedScreenHolder.Find("Friends FB level WIN/Friends Level Win " + num3 + " HOLDER");
					((Component)val.Find("FB")).gameObject.SetActive(false);
					if (!((Component)val.Find("Friends Level Win " + num3)).gameObject.activeSelf)
					{
						((Component)val.Find("Friends Level Win " + num3)).gameObject.SetActive(true);
					}
					((Component)val.Find("Friends Level Win " + num3 + "/Friends Level Win Picture " + num3)).GetComponent<Renderer>().material.mainTexture = FacebookManager.ProfileSlikePrijatelja[list[n].index].profilePicture;
					((Component)val.Find("Friends Level Win " + num3 + "/Friends Level Win Picture " + num3 + "/Points Number level win fb")).GetComponent<TextMesh>().text = list[n].score.ToString();
					if (FacebookManager.ListaStructPrijatelja[list[n].index].PrijateljID == FacebookManager.User)
					{
						flag2 = true;
						((Component)val.Find("Friends Level Win " + num3)).GetComponent<SpriteRenderer>().sprite = ((Component)val.parent.Find("ReferencaYOU")).GetComponent<SpriteRenderer>().sprite;
					}
					else
					{
						((Component)val.Find("Friends Level Win " + num3)).GetComponent<SpriteRenderer>().sprite = ((Component)val.parent.Find("Referenca")).GetComponent<SpriteRenderer>().sprite;
					}
				}
				else if (num3 <= 5)
				{
					((Component)Win_CompletedScreenHolder.Find("Friends FB level WIN/Friends Level Win " + num3 + " HOLDER/FB")).gameObject.SetActive(true);
					((Component)Win_CompletedScreenHolder.Find("Friends FB level WIN/Friends Level Win " + num3 + " HOLDER/Friends Level Win " + num3)).gameObject.SetActive(false);
				}
			}
			num3++;
		}
		if (list.Count < 5)
		{
			for (int num5 = num3; num5 <= 5; num5++)
			{
				((Component)Win_CompletedScreenHolder.Find("Friends FB level WIN/Friends Level Win " + num5 + " HOLDER/Friends Level Win " + num5)).gameObject.SetActive(false);
			}
		}
		if (!flag2)
		{
			Transform val = Win_CompletedScreenHolder.Find("Friends FB level WIN/Friends Level Win 5 HOLDER");
			((Component)val.Find("Friends Level Win 5/Friends Level Win Picture 5")).GetComponent<Renderer>().material.mainTexture = FacebookManager.ListaStructPrijatelja[list[num4 - 1].index].profilePicture;
			((Component)val.Find("Friends Level Win 5/Friends Level Win Picture 5/Points Number level win fb")).GetComponent<TextMesh>().text = list[num4 - 1].score.ToString();
			((Component)val.Find("Friends Level Win 5/Friends Level Win Picture 5/Position Number")).GetComponent<TextMesh>().text = num4.ToString();
			((Component)val.Find("Friends Level Win 5")).GetComponent<SpriteRenderer>().sprite = ((Component)val.parent.Find("ReferencaYOU")).GetComponent<SpriteRenderer>().sprite;
		}
		list.Clear();
	}

	private void spustiPopup()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Transform obj = popupZaSpustanje;
		obj.localPosition += new Vector3(0f, -35f, 0f);
		popupZaSpustanje = null;
		if (pauseEnabled && neDozvoliPauzu)
		{
			neDozvoliPauzu = false;
			((Component)pauseButton).GetComponent<Collider>().enabled = true;
		}
	}

	private void DeaktivirajPowerUpAnimator()
	{
		((Component)((Component)this).transform.Find("POWERS HOLDER")).gameObject.SetActive(false);
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
			StagesParser.sceneID = 2;
			popupZaSpustanje = ((Component)this).transform.Find("WATCH VIDEO HOLDER");
			((MonoBehaviour)this).Invoke("spustiPopup", 0.5f);
			((Component)((Component)this).transform.Find("WATCH VIDEO HOLDER").GetChild(0)).GetComponent<Animator>().Play("ClosePopup");
		}
		else
		{
			CheckInternetConnection.Instance.openPopup();
		}
	}

	private void WatchVideoCallback()
	{
		StagesParser.currentMoney += watchVideoReward;
		PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
		PlayerPrefs.Save();
		((MonoBehaviour)this).StartCoroutine(StagesParser.Instance.moneyCounter(watchVideoReward, ((Component)((Component)this).transform.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/Coins/Coins Number")).GetComponent<TextMesh>(), hasOutline: true));
		StagesParser.ServerUpdate = 1;
	}

	private IEnumerator checkConnectionForRate()
	{
		((MonoBehaviour)this).StartCoroutine(CheckInternetConnection.Instance.checkInternetConnection());
		while (!CheckInternetConnection.Instance.checkDone)
		{
			yield return null;
		}
		if (CheckInternetConnection.Instance.internetOK)
		{
			((MonoBehaviour)this).StartCoroutine(RateGame(StagesParser.Instance.rateLink));
		}
		else
		{
			CheckInternetConnection.Instance.openPopup();
		}
	}

	private IEnumerator checkConnectionForInvite()
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

	private IEnumerator checkConnectionForShare()
	{
		((MonoBehaviour)this).StartCoroutine(CheckInternetConnection.Instance.checkInternetConnection());
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
	}
}
