using System;
using System.Collections;
using UnityEngine;

public class ManageFull : MonoBehaviour
{
	[HideInInspector]
	public int coinsCollected;

	[HideInInspector]
	public int starsGained;

	[HideInInspector]
	public int keysCollected;

	[HideInInspector]
	public int collectedPoints;

	[HideInInspector]
	public int baboonSmashed;

	private int brojDoubleCoins = 1;

	private int brojMagneta = 1;

	private int brojShieldova = 1;

	private bool playerDead;

	public GameObject goScreen;

	public GameObject goScreen2;

	private GameObject player;

	private MonkeyController2D playerController;

	private float camera_z;

	private CameraFollow2D_new cameraFollow;

	private Transform pauseButton;

	private Transform coinsHolder;

	private TextMesh coinsCollectedText;

	private GameObject pauseScreenHolder;

	private GameObject Win_CompletedScreenHolder;

	private GameObject FailedScreenHolder;

	private GameObject Win_ShineHolder;

	private GameObject star1;

	private GameObject star2;

	private GameObject star3;

	private Transform holderKeys;

	private GameObject newHighScore;

	private GameObject holderFinishPts;

	private GameObject holderFinishKeys;

	private GameObject buttonFacebookShare;

	private GameObject buttonBuyKeys;

	private GameObject buttonPlay_Finish;

	private GameObject holderFinishInfo;

	private GameObject holderTextCompleted;

	private GameObject holderKeepPlaying;

	private GameObject keyHole1;

	private GameObject keyHole2;

	private GameObject keyHole3;

	[HideInInspector]
	public Transform progressBarScale;

	private Transform wonStar1;

	private Transform wonStar2;

	private Transform wonStar3;

	private TextMesh textKeyPrice1;

	private TextMesh textKeyPrice2;

	private Transform shopHolder;

	private Transform shopLevaIvica;

	private Transform shopDesnaIvica;

	private GameObject shopHeaderOn;

	private GameObject shopHeaderOff;

	private GameObject freeCoinsHeaderOn;

	private GameObject freeCoinsHeaderOff;

	private GameObject holderShopCard;

	private GameObject holderFreeCoinsCard;

	private Transform buttonShopBack;

	private Transform PickPowers;

	private Transform powerCard_CoinX2;

	private Transform powerCard_Magnet;

	private Transform powerCard_Shield;

	private bool kupljenShield;

	private bool kupljenDoubleCoins;

	private bool kupljenMagnet;

	public AnimationClip showPauseAnimation;

	public AnimationClip dropPauseAnimation;

	private bool helpBool;

	private bool playerStopiran;

	private Action command;

	private string releasedItem;

	private SetRandomStarsManager starManager;

	private bool PowerUp_magnet;

	[HideInInspector]
	public bool PowerUp_doubleCoins;

	[HideInInspector]
	public bool PowerUp_shield;

	private GameObject coinMagnet;

	private GameObject shield;

	private TextMesh textPtsGameplay;

	private TextMesh textPtsFinish;

	private void Awake()
	{
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0512: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0526: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_053b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0540: Unknown result type (might be due to invalid IL or missing references)
		//IL_054f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0554: Unknown result type (might be due to invalid IL or missing references)
		//IL_0564: Unknown result type (might be due to invalid IL or missing references)
		//IL_056e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0583: Unknown result type (might be due to invalid IL or missing references)
		//IL_0588: Unknown result type (might be due to invalid IL or missing references)
		//IL_0597: Unknown result type (might be due to invalid IL or missing references)
		//IL_059c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05da: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0608: Unknown result type (might be due to invalid IL or missing references)
		//IL_061d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0627: Unknown result type (might be due to invalid IL or missing references)
		//IL_062c: Unknown result type (might be due to invalid IL or missing references)
		//IL_063c: Unknown result type (might be due to invalid IL or missing references)
		//IL_064c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0656: Unknown result type (might be due to invalid IL or missing references)
		//IL_066b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0670: Unknown result type (might be due to invalid IL or missing references)
		//IL_0680: Unknown result type (might be due to invalid IL or missing references)
		//IL_0690: Unknown result type (might be due to invalid IL or missing references)
		//IL_069a: Unknown result type (might be due to invalid IL or missing references)
		//IL_06af: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0743: Unknown result type (might be due to invalid IL or missing references)
		//IL_074d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0752: Unknown result type (might be due to invalid IL or missing references)
		//IL_0761: Unknown result type (might be due to invalid IL or missing references)
		//IL_0766: Unknown result type (might be due to invalid IL or missing references)
		//IL_077c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0786: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_07df: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0809: Unknown result type (might be due to invalid IL or missing references)
		//IL_081d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0822: Unknown result type (might be due to invalid IL or missing references)
		cameraFollow = ((Component)((Component)Camera.main).transform.parent).GetComponent<CameraFollow2D_new>();
		goScreen.SetActive(true);
		goScreen2.SetActive(true);
		starManager = ((Component)this).GetComponent<SetRandomStarsManager>();
		player = GameObject.FindGameObjectWithTag("Monkey");
		playerController = player.GetComponent<MonkeyController2D>();
		coinsCollectedText = GameObject.Find("TextCoins").GetComponent<TextMesh>();
		pauseButton = GameObject.Find("HolderPause").transform;
		coinsHolder = GameObject.Find("HolderCoins").transform;
		pauseScreenHolder = GameObject.Find("HolderPauseScreen");
		FailedScreenHolder = GameObject.Find("HolderFailed");
		Win_CompletedScreenHolder = GameObject.Find("HolderFinish");
		Win_ShineHolder = GameObject.Find("HolderShineFinish");
		star1 = GameObject.Find("FinishStar1");
		star2 = GameObject.Find("FinishStar2");
		star3 = GameObject.Find("FinishStar3");
		holderKeys = GameObject.Find("HolderKeys").transform;
		camera_z = ((Component)Camera.main).transform.position.z;
		coinMagnet = ((Component)player.transform.Find("CoinMagnet")).gameObject;
		shield = GameObject.Find("Shield");
		shield.SetActive(false);
		newHighScore = GameObject.Find("NewHighScore");
		holderFinishPts = GameObject.Find("HolderFinishPts");
		holderFinishInfo = GameObject.Find("HolderFinishInfo");
		buttonFacebookShare = GameObject.Find("FinishFacebook");
		buttonBuyKeys = GameObject.Find("FinishKeyPrice");
		holderFinishKeys = GameObject.Find("HolderFinishKeys");
		buttonPlay_Finish = GameObject.Find("ButtonHolePlay");
		holderTextCompleted = GameObject.Find("HolderTextCompleted");
		keyHole1 = GameObject.Find("FinishKeyHole1_");
		keyHole2 = GameObject.Find("FinishKeyHole2_");
		keyHole3 = GameObject.Find("FinishKeyHole3_");
		holderKeepPlaying = GameObject.Find("HolderKeepPlaying");
		progressBarScale = GameObject.Find("ProgressBar_ScaleY").transform;
		wonStar1 = GameObject.Find("HolderWonStar1").transform;
		wonStar2 = GameObject.Find("HolderWonStar2").transform;
		wonStar3 = GameObject.Find("HolderWonStar3").transform;
		textPtsGameplay = GameObject.Find("TextPtsGameplay").GetComponent<TextMesh>();
		textPtsFinish = GameObject.Find("TextPts").GetComponent<TextMesh>();
		textKeyPrice1 = GameObject.Find("TextKeyPrice1").GetComponent<TextMesh>();
		textKeyPrice2 = GameObject.Find("TextKeyPrice2").GetComponent<TextMesh>();
		shopHolder = GameObject.Find("_HolderShop").transform;
		shopLevaIvica = GameObject.Find("ShopRamLevoHolder").transform;
		shopDesnaIvica = GameObject.Find("ShopRamDesnoHolder").transform;
		shopHeaderOn = GameObject.Find("ShopHeaderOn");
		shopHeaderOff = GameObject.Find("ShopHeaderOff1");
		freeCoinsHeaderOn = GameObject.Find("ShopHeaderOn1");
		freeCoinsHeaderOff = GameObject.Find("ShopHeaderOff");
		holderShopCard = GameObject.Find("HolderShopCard");
		holderFreeCoinsCard = GameObject.Find("HolderFreeCoinsCard");
		buttonShopBack = GameObject.Find("HolderBack").transform;
		PickPowers = GameObject.Find("HolderPowersMove").transform;
		powerCard_CoinX2 = GameObject.Find("PowersCardCoinx2").transform;
		powerCard_Magnet = GameObject.Find("PowersCardMagnet").transform;
		powerCard_Shield = GameObject.Find("PowersCardShield").transform;
		shopHeaderOff.SetActive(false);
		freeCoinsHeaderOn.SetActive(false);
		holderFreeCoinsCard.SetActive(false);
		holderKeepPlaying.SetActive(false);
		newHighScore.SetActive(false);
		holderTextCompleted.SetActive(false);
		pauseScreenHolder.SetActive(false);
		FailedScreenHolder.SetActive(false);
		Win_CompletedScreenHolder.SetActive(false);
		Win_ShineHolder.SetActive(false);
		if (Camera.main.aspect <= 1.5f)
		{
			Camera.main.orthographicSize = 18f;
			shopHolder.localScale = shopHolder.localScale * 18f / 5f;
		}
		if (Camera.main.aspect > 1.5f)
		{
			Camera.main.orthographicSize = 15f;
			shopHolder.localScale = shopHolder.localScale * 15f / 5f;
		}
		else
		{
			Camera.main.orthographicSize = 16.5f;
			shopHolder.localScale = shopHolder.localScale * 16.5f / 5f;
		}
		pauseButton.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.one).x, Camera.main.ViewportToWorldPoint(Vector3.one).y, pauseButton.position.z);
		coinsHolder.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.zero).x, Camera.main.ViewportToWorldPoint(Vector3.one).y, coinsHolder.position.z);
		holderKeys.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.one / 2f).x, Camera.main.ViewportToWorldPoint(Vector3.one).y, pauseButton.position.z);
		shopHolder.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.one / 2f).x, shopHolder.position.y, shopHolder.position.z);
		shopLevaIvica.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.zero).x, shopLevaIvica.position.y, shopLevaIvica.position.z);
		shopDesnaIvica.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.one).x, shopLevaIvica.position.y, shopLevaIvica.position.z);
		((Component)shopHolder).gameObject.SetActive(false);
		pauseButton.parent = ((Component)Camera.main).transform;
		coinsHolder.parent = ((Component)Camera.main).transform;
		holderKeys.parent = ((Component)Camera.main).transform;
		PickPowers.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.one / 2f).x, Camera.main.ViewportToWorldPoint(Vector3.one).y * 0.65f, PickPowers.position.z);
		((Component)PickPowers).gameObject.SetActive(false);
		goScreen.transform.position = new Vector3(((Component)Camera.main).transform.position.x, ((Component)Camera.main).transform.position.y, goScreen.transform.position.z);
		goScreen2.transform.position = goScreen.transform.position + new Vector3(0.1f, 0f, 1f);
		goScreen.transform.parent = ((Component)Camera.main).transform;
		goScreen2.transform.parent = ((Component)Camera.main).transform;
		PickPowers.parent = ((Component)Camera.main).transform;
		if (PlaySounds.musicOn)
		{
			PlaySounds.Play_BackgroundMusic_Gameplay();
			if (PlaySounds.Level_Failed_Popup.isPlaying)
			{
				PlaySounds.Stop_Level_Failed_Popup();
			}
		}
		((Component)powerCard_CoinX2.GetChild(3).GetChild(0)).GetComponent<TextMesh>().text = brojDoubleCoins.ToString();
		((Component)powerCard_CoinX2.GetChild(3).GetChild(1)).GetComponent<TextMesh>().text = brojDoubleCoins.ToString();
		((Component)powerCard_Magnet.GetChild(3).GetChild(0)).GetComponent<TextMesh>().text = brojMagneta.ToString();
		((Component)powerCard_Magnet.GetChild(3).GetChild(1)).GetComponent<TextMesh>().text = brojMagneta.ToString();
		((Component)powerCard_Shield.GetChild(3).GetChild(0)).GetComponent<TextMesh>().text = brojShieldova.ToString();
		((Component)powerCard_Shield.GetChild(3).GetChild(1)).GetComponent<TextMesh>().text = brojShieldova.ToString();
	}

	private void Update()
	{
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_086f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0874: Unknown result type (might be due to invalid IL or missing references)
		//IL_087e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0883: Unknown result type (might be due to invalid IL or missing references)
		//IL_08dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0956: Unknown result type (might be due to invalid IL or missing references)
		//IL_095b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0965: Unknown result type (might be due to invalid IL or missing references)
		//IL_096a: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_09de: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e3: Unknown result type (might be due to invalid IL or missing references)
		if (Input.GetKeyDown((KeyCode)32) && goScreen.activeSelf)
		{
			Time.timeScale = 1f;
			goScreen.transform.parent = ((Component)this).transform;
			goScreen2.transform.parent = ((Component)this).transform;
			goScreen.SetActive(false);
			goScreen2.SetActive(false);
			playerController.state = MonkeyController2D.State.running;
			PlaySounds.Play_Run();
		}
		if (!Input.GetMouseButtonUp(0))
		{
			return;
		}
		releasedItem = RaycastFunction(Input.mousePosition);
		if (releasedItem == "GO screen")
		{
			Time.timeScale = 1f;
			goScreen.transform.parent = ((Component)this).transform;
			goScreen2.transform.parent = ((Component)this).transform;
			goScreen.SetActive(false);
			goScreen2.SetActive(false);
			playerController.state = MonkeyController2D.State.running;
			GameObject.Find("PrinceGorilla").GetComponent<Animator>().SetBool("Run", true);
			PlaySounds.Play_Run();
			((MonoBehaviour)this).StartCoroutine(showPickPowers());
		}
		else if (releasedItem == "ButtonPause")
		{
			PlaySounds.Play_Button_Pause();
			pauseScreenHolder.transform.position = new Vector3(((Component)Camera.main).transform.position.x, ((Component)Camera.main).transform.position.y, pauseScreenHolder.transform.position.z);
			pauseScreenHolder.SetActive(true);
			if (Time.timeScale == 1f)
			{
				Time.timeScale = 0f;
				((MonoBehaviour)this).StopAllCoroutines();
				((MonoBehaviour)this).StartCoroutine(showPauseScreen());
			}
			else
			{
				((MonoBehaviour)this).StartCoroutine(dropPauseScreen());
			}
		}
		else if (releasedItem == "PauseHoleMain")
		{
			PlaySounds.Play_Button_GoBack();
			((MonoBehaviour)this).StartCoroutine(backToMenu());
		}
		else if (releasedItem == "PauseHolePlay")
		{
			PlaySounds.Play_Button_Pause();
			((MonoBehaviour)this).StartCoroutine(unPause());
			if (playerStopiran)
			{
				playerController.heCanJump = true;
				((Component)buttonShopBack.GetChild(0)).GetComponent<Animation>().Play("BackButtonClick");
				((MonoBehaviour)this).StartCoroutine(closeShop());
				playerStopiran = false;
				GameObject.Find("ButtonPause").GetComponent<Collider>().enabled = true;
				GameObject.Find("OBLACI").GetComponent<RunWithSpeed>().continueMoving = true;
				((Component)playerController).GetComponent<Rigidbody2D>().isKinematic = false;
				((Behaviour)playerController.animator).enabled = true;
				playerController.maxSpeedX = playerController.startSpeedX;
				cameraFollow.cameraFollowX = true;
			}
		}
		else if (releasedItem == "PauseHoleRestart")
		{
			PlaySounds.Play_Button_RestartLevel();
			((MonoBehaviour)this).StartCoroutine(restartLevel());
		}
		else if (releasedItem == "FailedMainHole")
		{
			PlaySounds.Play_Button_GoBack();
			GameObject val = GameObject.Find("ButtonMain_Failed");
			val.GetComponent<Animation>().Play("FinishButtonsClick");
			if (PlaySounds.BackgroundMusic_Gameplay.isPlaying)
			{
				PlaySounds.Stop_BackgroundMusic_Gameplay();
			}
			command = delegate
			{
				Application.LoadLevel(4);
			};
			((MonoBehaviour)this).StartCoroutine(((Component)FailedScreenHolder.transform.GetChild(0)).GetComponent<Animation>().Play("FailedGo", useTimeScale: false, delegate
			{
				helpBool = true;
			}));
			((MonoBehaviour)this).StartCoroutine(DoAfterAnimation(val, "FinishButtonsClick"));
		}
		else if (releasedItem == "FailedRestartHole")
		{
			PlaySounds.Play_Button_RestartLevel();
			GameObject val2 = GameObject.Find("ButtonRestart_Failed");
			val2.GetComponent<Animation>().Play("FinishButtonsClick");
			command = delegate
			{
				Application.LoadLevel(Application.loadedLevel);
			};
			((MonoBehaviour)this).StartCoroutine(((Component)FailedScreenHolder.transform.GetChild(0)).GetComponent<Animation>().Play("FailedGo", useTimeScale: false, delegate
			{
				helpBool = true;
			}));
			((MonoBehaviour)this).StartCoroutine(DoAfterAnimation(val2, "FinishButtonsClick"));
		}
		else if (releasedItem == "ButtonRestart1")
		{
			PlaySounds.Play_Button_RestartLevel();
			GameObject val3 = GameObject.Find("ButtonRestart1");
			val3.GetComponent<Animation>().Play("FinishButtonsClick");
			command = delegate
			{
				Application.LoadLevel(Application.loadedLevel);
			};
			((MonoBehaviour)this).StartCoroutine(((Component)Win_CompletedScreenHolder.transform.GetChild(0)).GetComponent<Animation>().Play("FinishTableGo1", useTimeScale: false, delegate
			{
				helpBool = true;
			}));
			((MonoBehaviour)this).StartCoroutine(DoAfterAnimation(val3, "FinishButtonsClick"));
		}
		else if (releasedItem == "ButtonMain1")
		{
			PlaySounds.Play_Button_GoBack();
			GameObject val4 = GameObject.Find("ButtonMain1");
			val4.GetComponent<Animation>().Play("FinishButtonsClick");
			if (PlaySounds.BackgroundMusic_Gameplay.isPlaying)
			{
				PlaySounds.Stop_BackgroundMusic_Gameplay();
			}
			command = delegate
			{
				Application.LoadLevel(4);
			};
			((MonoBehaviour)this).StartCoroutine(((Component)Win_CompletedScreenHolder.transform.GetChild(0)).GetComponent<Animation>().Play("FinishTableGo1", useTimeScale: false, delegate
			{
				helpBool = true;
			}));
			((MonoBehaviour)this).StartCoroutine(DoAfterAnimation(val4, "FinishButtonsClick"));
		}
		else if (releasedItem == "ButtonPlay1")
		{
			PlaySounds.Play_Button_NextLevel();
			GameObject val5 = GameObject.Find("ButtonPlay1");
			val5.GetComponent<Animation>().Play("FinishButtonsClick");
			if (PlaySounds.BackgroundMusic_Gameplay.isPlaying)
			{
				PlaySounds.Stop_BackgroundMusic_Gameplay();
			}
			StagesParser.currStageIndex++;
			command = delegate
			{
				Application.LoadLevel("LoadingScene");
			};
			((MonoBehaviour)this).StartCoroutine(((Component)Win_CompletedScreenHolder.transform.GetChild(0)).GetComponent<Animation>().Play("FinishTableGo1", useTimeScale: false, delegate
			{
				helpBool = true;
			}));
			((MonoBehaviour)this).StartCoroutine(DoAfterAnimation(val5, "FinishButtonsClick"));
		}
		else if (releasedItem == "PauseHoleFreeCoins")
		{
			playerStopiran = true;
			playerController.heCanJump = false;
			GameObject.Find("ButtonPause").GetComponent<Collider>().enabled = false;
			GameObject.Find("OBLACI").GetComponent<RunWithSpeed>().continueMoving = false;
			((Component)playerController).GetComponent<Rigidbody2D>().isKinematic = true;
			playerController.maxSpeedX = 0f;
			((Behaviour)playerController.animator).enabled = false;
			cameraFollow.cameraFollowX = false;
			cameraFollow.cameraFollowY = false;
			cameraFollow.moveUp = false;
			cameraFollow.moveDown = false;
			Time.timeScale = 1f;
			((MonoBehaviour)this).StartCoroutine(OpenFreeCoinsCard());
		}
		else if (releasedItem == "PauseHoleShop")
		{
			playerStopiran = true;
			playerController.heCanJump = false;
			GameObject.Find("ButtonPause").GetComponent<Collider>().enabled = false;
			GameObject.Find("OBLACI").GetComponent<RunWithSpeed>().continueMoving = false;
			((Component)playerController).GetComponent<Rigidbody2D>().isKinematic = true;
			playerController.maxSpeedX = 0f;
			((Behaviour)playerController.animator).enabled = false;
			cameraFollow.cameraFollowX = false;
			cameraFollow.cameraFollowY = false;
			cameraFollow.moveUp = false;
			cameraFollow.moveDown = false;
			Time.timeScale = 1f;
			((MonoBehaviour)this).StartCoroutine(OpenFreeCoinsCard());
		}
		else if (releasedItem == "FinishKeyPrice")
		{
			((MonoBehaviour)this).StartCoroutine(BuyKeys());
		}
		else if (releasedItem == "ButtonFreeCoins1")
		{
			GameObject.Find(releasedItem).GetComponent<Animation>().Play("FinishButtonsClick");
			((Component)shopHolder).transform.position = ((Component)Camera.main).transform.position + Vector3.forward * 5f;
			((MonoBehaviour)this).StartCoroutine(OpenFreeCoinsCard());
		}
		else if (releasedItem == "ButtonShop1")
		{
			GameObject.Find(releasedItem).GetComponent<Animation>().Play("FinishButtonsClick");
			((Component)shopHolder).transform.position = ((Component)Camera.main).transform.position + Vector3.forward * 5f;
			((MonoBehaviour)this).StartCoroutine(OpenShopCard());
		}
		else if (releasedItem == "FailedFreeCoinsHole")
		{
			((Component)GameObject.Find(releasedItem).transform.GetChild(0)).GetComponent<Animation>().Play("FinishButtonsClick");
			((Component)shopHolder).transform.position = ((Component)Camera.main).transform.position + Vector3.forward * 5f;
			((MonoBehaviour)this).StartCoroutine(OpenFreeCoinsCard());
		}
		else if (releasedItem == "FailedShopHole")
		{
			((Component)GameObject.Find(releasedItem).transform.GetChild(0)).GetComponent<Animation>().Play("FinishButtonsClick");
			((Component)shopHolder).transform.position = ((Component)Camera.main).transform.position + Vector3.forward * 5f;
			((MonoBehaviour)this).StartCoroutine(OpenShopCard());
		}
		else if (releasedItem == "HolderBack")
		{
			Debug.Log((object)("ime: " + GameObject.Find(releasedItem)));
			((Component)buttonShopBack.GetChild(0)).GetComponent<Animation>().Play("BackButtonClick");
			((MonoBehaviour)this).StartCoroutine(closeShop());
		}
		else if (releasedItem == "ShopHeaderOff1")
		{
			shopHeaderOff.SetActive(false);
			shopHeaderOn.SetActive(true);
			freeCoinsHeaderOn.SetActive(false);
			freeCoinsHeaderOff.SetActive(true);
			holderFreeCoinsCard.SetActive(false);
			holderShopCard.SetActive(true);
		}
		else if (releasedItem == "ShopHeaderOff")
		{
			shopHeaderOn.SetActive(false);
			shopHeaderOff.SetActive(true);
			freeCoinsHeaderOff.SetActive(false);
			freeCoinsHeaderOn.SetActive(true);
			holderShopCard.SetActive(false);
			holderFreeCoinsCard.SetActive(true);
		}
		else if (releasedItem == "PowersCardCoinx2")
		{
			((Component)powerCard_CoinX2).GetComponent<Collider>().enabled = false;
			brojDoubleCoins--;
			((Component)powerCard_CoinX2.GetChild(3).GetChild(0)).GetComponent<TextMesh>().text = brojDoubleCoins.ToString();
			((Component)powerCard_CoinX2.GetChild(3).GetChild(1)).GetComponent<TextMesh>().text = brojDoubleCoins.ToString();
			kupljenDoubleCoins = true;
			((Component)powerCard_CoinX2).GetComponent<Animator>().Play("GameplayPowerClick2");
			ApplyPowerUp(2);
		}
		else if (releasedItem == "PowersCardMagnet")
		{
			((Component)powerCard_Magnet).GetComponent<Collider>().enabled = false;
			brojMagneta--;
			((Component)powerCard_Magnet.GetChild(3).GetChild(0)).GetComponent<TextMesh>().text = brojMagneta.ToString();
			((Component)powerCard_Magnet.GetChild(3).GetChild(1)).GetComponent<TextMesh>().text = brojMagneta.ToString();
			kupljenMagnet = true;
			((Component)powerCard_Magnet).GetComponent<Animator>().Play("GameplayPowerClick2");
			ApplyPowerUp(1);
		}
		else if (releasedItem == "PowersCardShield")
		{
			((Component)powerCard_Shield).GetComponent<Collider>().enabled = false;
			brojShieldova--;
			((Component)powerCard_Shield.GetChild(3).GetChild(0)).GetComponent<TextMesh>().text = brojShieldova.ToString();
			((Component)powerCard_Shield.GetChild(3).GetChild(1)).GetComponent<TextMesh>().text = brojShieldova.ToString();
			kupljenShield = true;
			((Component)powerCard_Shield).GetComponent<Animator>().Play("GameplayPowerClick2");
			ApplyPowerUp(3);
		}
	}

	private IEnumerator OpenShopCard()
	{
		shopHeaderOff.SetActive(false);
		shopHeaderOn.SetActive(true);
		freeCoinsHeaderOn.SetActive(false);
		freeCoinsHeaderOff.SetActive(true);
		holderFreeCoinsCard.SetActive(false);
		holderShopCard.SetActive(true);
		yield return (object)new WaitForSeconds(0.5f);
		((Component)shopHolder).gameObject.SetActive(true);
	}

	private IEnumerator OpenFreeCoinsCard()
	{
		shopHeaderOn.SetActive(false);
		shopHeaderOff.SetActive(true);
		freeCoinsHeaderOff.SetActive(false);
		freeCoinsHeaderOn.SetActive(true);
		holderShopCard.SetActive(false);
		holderFreeCoinsCard.SetActive(true);
		yield return (object)new WaitForSeconds(0.5f);
		((Component)shopHolder).gameObject.SetActive(true);
	}

	private IEnumerator closeShop()
	{
		yield return (object)new WaitForSeconds(0.85f);
		((Component)shopHolder).gameObject.SetActive(false);
		shopHolder.position = new Vector3(-5f, -5f, shopHolder.position.z);
		buttonShopBack.GetChild(0).localPosition = Vector3.zero;
	}

	private IEnumerator DoAfterAnimation(GameObject obj, string animationName)
	{
		while (obj.GetComponent<Animation>().IsPlaying(animationName))
		{
			yield return null;
		}
		command();
	}

	private IEnumerator showPauseScreen()
	{
		((MonoBehaviour)this).StartCoroutine(((Component)((Component)pauseButton).transform.GetChild(0)).GetComponent<Animation>().Play("FinishButtonsClick", useTimeScale: false, delegate
		{
			helpBool = true;
		}));
		((MonoBehaviour)this).StartCoroutine(((Component)pauseScreenHolder.transform.GetChild(0)).GetComponent<Animation>().Play("PauseShow", useTimeScale: false, delegate
		{
			helpBool = true;
		}));
		while (!helpBool)
		{
			yield return null;
		}
		helpBool = false;
	}

	private IEnumerator dropPauseScreen()
	{
		((MonoBehaviour)this).StartCoroutine(((Component)((Component)pauseButton).transform.GetChild(0)).GetComponent<Animation>().Play("FinishButtonsClick", useTimeScale: false, delegate
		{
			helpBool = true;
		}));
		((MonoBehaviour)this).StartCoroutine(((Component)pauseScreenHolder.transform.GetChild(0)).GetComponent<Animation>().Play("PauseGo", useTimeScale: false, delegate
		{
			helpBool = true;
		}));
		while (!helpBool)
		{
			yield return null;
		}
		helpBool = false;
		Time.timeScale = 1f;
		((MonoBehaviour)this).Invoke("HidePauseScreen", 0.75f);
	}

	private void HidePauseScreen()
	{
		pauseScreenHolder.SetActive(false);
	}

	private IEnumerator backToMenu()
	{
		((MonoBehaviour)this).StartCoroutine(GameObject.Find("ButtonMain_Pause").GetComponent<Animation>().Play("FinishButtonsClick", useTimeScale: false, delegate
		{
			helpBool = true;
		}));
		((MonoBehaviour)this).StartCoroutine(((Component)pauseScreenHolder.transform.GetChild(0)).GetComponent<Animation>().Play("PauseGo", useTimeScale: false, delegate
		{
			helpBool = true;
		}));
		while (!helpBool)
		{
			yield return null;
		}
		helpBool = false;
		Application.LoadLevel(4);
		Time.timeScale = 1f;
	}

	private IEnumerator restartLevel()
	{
		((MonoBehaviour)this).StartCoroutine(GameObject.Find("ButtonRestart_Pause").GetComponent<Animation>().Play("FinishButtonsClick", useTimeScale: false, delegate
		{
			helpBool = true;
		}));
		((MonoBehaviour)this).StartCoroutine(((Component)pauseScreenHolder.transform.GetChild(0)).GetComponent<Animation>().Play("PauseGo", useTimeScale: false, delegate
		{
			helpBool = true;
		}));
		while (!helpBool)
		{
			yield return null;
		}
		helpBool = false;
		Application.LoadLevel(Application.loadedLevel);
		Time.timeScale = 1f;
	}

	private IEnumerator unPause()
	{
		((MonoBehaviour)this).StartCoroutine(GameObject.Find("ButtonPlay_Pause").GetComponent<Animation>().Play("FinishButtonsClick", useTimeScale: false, delegate
		{
			helpBool = true;
		}));
		((MonoBehaviour)this).StartCoroutine(dropPauseScreen());
		while (!helpBool)
		{
			yield return null;
		}
		helpBool = false;
	}

	private void showFailedScreen()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		FailedScreenHolder.transform.position = new Vector3(((Component)Camera.main).transform.position.x, ((Component)Camera.main).transform.position.y, FailedScreenHolder.transform.position.z);
		FailedScreenHolder.SetActive(true);
		((Component)FailedScreenHolder.transform.GetChild(0)).GetComponent<Animation>().Play("FailedShow");
	}

	private void ShowWinScreen()
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		TextMesh obj = textKeyPrice1;
		string text2 = (textKeyPrice2.text = ((3 - keysCollected) * 800).ToString());
		obj.text = text2;
		if (PlaySounds.BackgroundMusic_Gameplay.isPlaying)
		{
			PlaySounds.Stop_BackgroundMusic_Gameplay();
		}
		PlaySounds.Play_Level_Completed_Popup();
		Win_CompletedScreenHolder.transform.position = new Vector3(((Component)Camera.main).transform.position.x, ((Component)Camera.main).transform.position.y - 3f, Win_CompletedScreenHolder.transform.position.z);
		Win_CompletedScreenHolder.SetActive(true);
		Win_ShineHolder.transform.position = new Vector3(((Component)Camera.main).transform.position.x, ((Component)Camera.main).transform.position.y, Win_ShineHolder.transform.position.z);
		((Component)Win_CompletedScreenHolder.transform.GetChild(0)).GetComponent<Animation>().Play("FinishTableShow");
		((MonoBehaviour)this).StartCoroutine(CheckKeys());
	}

	private IEnumerator CheckKeys()
	{
		if (keysCollected == 1)
		{
			yield return (object)new WaitForSeconds(0.75f);
			Debug.Log((object)"Kljuc");
			keyHole1.GetComponent<Animation>().Play("FinishKeyYes");
			yield return (object)new WaitForSeconds(0.75f);
			keyHole2.GetComponent<Animation>().Play("FinishKeyNo");
			yield return (object)new WaitForSeconds(0.25f);
			keyHole3.GetComponent<Animation>().Play("FinishKeyNo");
		}
		else if (keysCollected == 2)
		{
			yield return (object)new WaitForSeconds(0.75f);
			keyHole1.GetComponent<Animation>().Play("FinishKeyYes");
			yield return (object)new WaitForSeconds(0.25f);
			keyHole2.GetComponent<Animation>().Play("FinishKeyYes");
			yield return (object)new WaitForSeconds(0.75f);
			keyHole3.GetComponent<Animation>().Play("FinishKeyNo");
		}
		else if (keysCollected == 3)
		{
			textPtsFinish.text = textPtsGameplay.text;
			((Component)buttonFacebookShare.transform.parent).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
			holderFinishInfo.SetActive(false);
			buttonBuyKeys.SetActive(false);
			((Component)buttonBuyKeys.transform.parent).gameObject.SetActive(false);
			yield return (object)new WaitForSeconds(0.75f);
			keyHole1.GetComponent<Animation>().Play("FinishKeyYes");
			yield return (object)new WaitForSeconds(0.25f);
			keyHole2.GetComponent<Animation>().Play("FinishKeyYes");
			yield return (object)new WaitForSeconds(0.25f);
			keyHole3.GetComponent<Animation>().Play("FinishKeyYes");
			yield return (object)new WaitForSeconds(0.5f);
			((Component)Win_CompletedScreenHolder.transform.GetChild(0)).GetComponent<Animation>().Play("FinishPriceClick");
			holderTextCompleted.SetActive(true);
			yield return (object)new WaitForSeconds(0.35f);
			((Component)buttonFacebookShare.transform.GetChild(0).GetChild(0)).gameObject.SetActive(true);
			yield return (object)new WaitForSeconds(0.45f);
			((Component)buttonFacebookShare.transform.parent).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
			((MonoBehaviour)this).StartCoroutine(waitForStars());
		}
		else
		{
			yield return (object)new WaitForSeconds(0.75f);
			keyHole1.GetComponent<Animation>().Play("FinishKeyNo");
			yield return (object)new WaitForSeconds(0.25f);
			keyHole2.GetComponent<Animation>().Play("FinishKeyNo");
			yield return (object)new WaitForSeconds(0.25f);
			keyHole3.GetComponent<Animation>().Play("FinishKeyNo");
		}
	}

	private IEnumerator waitForStars()
	{
		Win_ShineHolder.SetActive(true);
		yield return (object)new WaitForSeconds(0.75f);
		star1.GetComponent<Animation>().Play("FinishStars1");
		((Component)star1.transform.GetChild(0)).gameObject.SetActive(true);
		yield return (object)new WaitForSeconds(0.5f);
		GameObject.Find("FinishButtonTable").GetComponent<Animation>().Play("FinishShakingTableStars");
		starsGained = 1;
		PlaySounds.Play_GetStar();
		yield return (object)new WaitForSeconds(0.25f);
		if (coinsCollected >= 70)
		{
			starsGained = 2;
			star2.GetComponent<Animation>().Play("FinishStars2");
			((Component)star2.transform.GetChild(0)).gameObject.SetActive(true);
			yield return (object)new WaitForSeconds(0.5f);
			GameObject.Find("FinishButtonTable").GetComponent<Animation>().Play("FinishShakingTableStars");
			PlaySounds.Play_GetStar2();
			yield return (object)new WaitForSeconds(0.25f);
		}
		if (coinsCollected >= 90)
		{
			starsGained = 3;
			star3.GetComponent<Animation>().Play("FinishStars3");
			((Component)star3.transform.GetChild(0)).gameObject.SetActive(true);
			yield return (object)new WaitForSeconds(0.5f);
			GameObject.Find("FinishButtonTable").GetComponent<Animation>().Play("FinishShakingTableStars");
			PlaySounds.Play_GetStar3();
		}
		starManager.GoBack();
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
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		RaycastHit val = default(RaycastHit);
		if (Physics.Raycast(Camera.main.ScreenPointToRay(vector), ref val))
		{
			return ((Object)((RaycastHit)(ref val)).collider).name;
		}
		return "";
	}

	private void ApplyPowerUp(int x)
	{
		switch (x)
		{
		case 1:
			PowerUp_magnet = true;
			coinMagnet.SetActive(true);
			break;
		case 2:
			PowerUp_doubleCoins = true;
			break;
		case 3:
			PowerUp_shield = true;
			shield.SetActive(true);
			playerController.activeShield = true;
			break;
		case -3:
			PowerUp_shield = false;
			shield.SetActive(false);
			break;
		}
	}

	private void KeyCollected()
	{
		keysCollected++;
		if (keysCollected == 1)
		{
			GameObject.Find("GamePlayKeyHole1").GetComponent<Animation>().Play();
		}
		else if (keysCollected == 2)
		{
			GameObject.Find("GamePlayKeyHole2").GetComponent<Animation>().Play();
		}
		else if (keysCollected == 3)
		{
			GameObject.Find("GamePlayKeyHole3").GetComponent<Animation>().Play();
		}
	}

	public void AddPoints(int value)
	{
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		collectedPoints += value;
		textPtsGameplay.text = collectedPoints.ToString();
		if (coinsCollected <= 100 && baboonSmashed <= 20 && progressBarScale.localScale.y <= 1f)
		{
			((MonoBehaviour)this).StartCoroutine(graduallyFillScale((float)value / 1000f));
			((MonoBehaviour)this).StartCoroutine(graduallyFillTile((float)value / 1000f));
		}
		if (progressBarScale.localScale.y >= 1f)
		{
			progressBarScale.localScale = Vector3.one;
			((Component)progressBarScale.GetChild(0).GetChild(0)).GetComponent<Renderer>().material.mainTextureScale = new Vector2(1f, 1f);
		}
	}

	private IEnumerator graduallyFillScale(float value)
	{
		Debug.Log((object)"ulaazkak scale");
		float result = progressBarScale.localScale.y + value;
		float t = 0f;
		while (t < result)
		{
			yield return null;
			if (progressBarScale.localScale.y <= 1f)
			{
				progressBarScale.localScale = Vector3.Lerp(progressBarScale.localScale, new Vector3(progressBarScale.localScale.x, result, progressBarScale.localScale.z), 0.2f);
			}
			t += Time.deltaTime * 2f;
			if (progressBarScale.localScale.y >= 0.8f)
			{
				if (wonStar3.GetChild(0).localScale.x == 0f)
				{
					((Component)wonStar3).GetComponent<Animation>().Play();
				}
			}
			else if (progressBarScale.localScale.y < 0.8f && progressBarScale.localScale.y >= 0.5f)
			{
				if (wonStar2.GetChild(0).localScale.x == 0f)
				{
					((Component)wonStar2).GetComponent<Animation>().Play();
				}
			}
			else if (progressBarScale.localScale.y < 0.5f && progressBarScale.localScale.y >= 0.2f && wonStar1.GetChild(0).localScale.x == 0f)
			{
				((Component)wonStar1).GetComponent<Animation>().Play();
			}
			if (progressBarScale.localScale.y >= 1f)
			{
				progressBarScale.localScale = Vector3.one;
				((Component)progressBarScale.GetChild(0).GetChild(0)).GetComponent<Renderer>().material.mainTextureScale = new Vector2(1f, 1f);
			}
		}
	}

	private IEnumerator graduallyFillTile(float value)
	{
		Debug.Log((object)"ulaazkak tile");
		float result = ((Component)progressBarScale.GetChild(0).GetChild(0)).GetComponent<Renderer>().material.mainTextureScale.y + value;
		for (float t = 0f; t < result; t += Time.deltaTime * 2f)
		{
			yield return null;
			if (progressBarScale.localScale.y < 1f)
			{
				((Component)progressBarScale.GetChild(0).GetChild(0)).GetComponent<Renderer>().material.mainTextureScale = Vector2.Lerp(((Component)progressBarScale.GetChild(0).GetChild(0)).GetComponent<Renderer>().material.mainTextureScale, new Vector2(1f, result), 0.2f);
			}
		}
	}

	private IEnumerator BuyKeys()
	{
		buttonBuyKeys.GetComponent<Collider>().enabled = false;
		if (keysCollected == 0)
		{
			keyHole1.transform.GetChild(1).localScale = Vector3.zero;
			keyHole2.transform.GetChild(1).localScale = Vector3.zero;
			keyHole3.transform.GetChild(1).localScale = Vector3.zero;
			keyHole1.GetComponent<Animation>().Play("FinishKeyYes");
			yield return (object)new WaitForSeconds(0.25f);
			keyHole2.GetComponent<Animation>().Play("FinishKeyYes");
			yield return (object)new WaitForSeconds(0.25f);
			keyHole3.GetComponent<Animation>().Play("FinishKeyYes");
		}
		if (keysCollected == 1)
		{
			keyHole2.transform.GetChild(1).localScale = Vector3.zero;
			keyHole3.transform.GetChild(1).localScale = Vector3.zero;
			keyHole2.GetComponent<Animation>().Play("FinishKeyYes");
			yield return (object)new WaitForSeconds(0.25f);
			keyHole3.GetComponent<Animation>().Play("FinishKeyYes");
		}
		if (keysCollected == 2)
		{
			keyHole3.transform.GetChild(1).localScale = Vector3.zero;
			keyHole3.GetComponent<Animation>().Play("FinishKeyYes");
		}
		yield return (object)new WaitForSeconds(0.45f);
		textPtsFinish.text = textPtsGameplay.text;
		((Component)buttonFacebookShare.transform.parent).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
		holderFinishInfo.SetActive(false);
		((Component)Win_CompletedScreenHolder.transform.GetChild(0)).GetComponent<Animation>().Play("FinishPriceClick");
		holderTextCompleted.SetActive(true);
		yield return (object)new WaitForSeconds(0.35f);
		((Component)buttonBuyKeys.transform.parent).gameObject.SetActive(false);
		((Component)buttonFacebookShare.transform.GetChild(0).GetChild(0)).gameObject.SetActive(true);
		yield return (object)new WaitForSeconds(0.45f);
		((Component)buttonFacebookShare.transform.parent).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
		((MonoBehaviour)this).StartCoroutine(waitForStars());
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
	}
}
