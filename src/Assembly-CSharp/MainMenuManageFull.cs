using System.Collections;
using UnityEngine;

public class MainMenuManageFull : MonoBehaviour
{
	private FacebookManager Face;

	private Sprite dugmeMuzikaSprite;

	private Sprite dugmeSoundSprite;

	private Sprite dugmeMuzikaOffSprite;

	private Sprite dugmeSoundOffSprite;

	private GameObject dugmeMuzika;

	private GameObject dugmeSound;

	private GameObject dugmePlay;

	private GameObject holderLogo;

	private GameObject majmunLogo;

	private GameObject LeaderBoard;

	private GameObject Languages;

	private GameObject TrenutnaZastava;

	private bool muzikaOff;

	private bool soundOff;

	private NivoManager nivoManager;

	private AudioSource MusicOn_Button;

	private AudioSource SoundOn_Button;

	private AudioSource Play_Button;

	private bool LeaderBoardAktivan;

	private int BrojZastave;

	public static bool LanguagesAktivan;

	private float x;

	private float y;

	private float z;

	private GameObject Zastave;

	private GameObject TextJezik;

	private string[] JezikTekst = new string[14]
	{
		"RU", "GR", "CH", "ENG", "ESP", "SRB", "SVD", "IT", "FR", "POR",
		"AR", "CRO", "KO", "JPN"
	};

	private void Awake()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		GameObject.Find("HolderGornjiDesniUgaoDugmici").GetComponent<Transform>().position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.one).x, Camera.main.ViewportToWorldPoint(Vector3.one).y, -0.05f);
		GameObject.Find("HolderGornjiLeviUgaoDugmici").GetComponent<Transform>().position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.zero).x, Camera.main.ViewportToWorldPoint(Vector3.one).y, -0.05f);
		GameObject.Find("HolderDonjiDesniUgaoDugmici").GetComponent<Transform>().position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.one).x, Camera.main.ViewportToWorldPoint(Vector3.zero).y, -0.05f);
		GameObject.Find("HolderDonjiLeviUgaoDugmici").GetComponent<Transform>().position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.zero).x, Camera.main.ViewportToWorldPoint(Vector3.zero).y, -0.05f);
		Face = GameObject.Find("FacebookManager").GetComponent<FacebookManager>();
		LeaderBoard = GameObject.Find("HolderLeaderboardMove");
		dugmeMuzika = GameObject.Find("ButtonMusic");
		dugmeSound = GameObject.Find("ButtonSound");
		dugmeMuzikaSprite = GameObject.Find("ButtonMusic").GetComponent<SpriteRenderer>().sprite;
		dugmeSoundSprite = GameObject.Find("ButtonSound").GetComponent<SpriteRenderer>().sprite;
		dugmeMuzikaOffSprite = GameObject.Find("ButtonMusicOff").GetComponent<SpriteRenderer>().sprite;
		dugmeSoundOffSprite = GameObject.Find("ButtonSoundOff").GetComponent<SpriteRenderer>().sprite;
		Languages = GameObject.Find("HolderLanguageFlagsMove");
		Languages.GetComponent<Animation>().Play("MainLanguageFlagsPosition");
		Zastave = GameObject.Find("Zastave");
		Zastave.SetActive(false);
		LanguagesAktivan = false;
		TrenutnaZastava = GameObject.Find("TrenutniJezik");
		TextJezik = GameObject.Find("Text2letters");
	}

	private void Start()
	{
		GameObject.Find("PrinceGorilla").GetComponent<Animator>().Play("Idle Main Screen");
		if (PlaySounds.musicOn)
		{
			if (!PlaySounds.BackgroundMusic_Menu.isPlaying)
			{
				PlaySounds.Play_BackgroundMusic_Menu();
			}
			dugmeMuzika.GetComponent<SpriteRenderer>().sprite = dugmeMuzikaSprite;
		}
		else
		{
			dugmeMuzika.GetComponent<SpriteRenderer>().sprite = dugmeMuzikaOffSprite;
		}
		if (PlaySounds.soundOn)
		{
			dugmeSound.GetComponent<SpriteRenderer>().sprite = dugmeSoundSprite;
		}
		else
		{
			dugmeSound.GetComponent<SpriteRenderer>().sprite = dugmeSoundOffSprite;
		}
		BrojZastave = PlayerPrefs.GetInt("LanguageFlag");
		Debug.Log((object)("BrojZastave" + BrojZastave));
		switch (BrojZastave)
		{
		case 1:
			TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag1Referenca").GetComponent<SpriteRenderer>().sprite;
			TextJezik.GetComponent<TextMesh>().text = JezikTekst[0];
			break;
		case 2:
			TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag2Referenca").GetComponent<SpriteRenderer>().sprite;
			TextJezik.GetComponent<TextMesh>().text = JezikTekst[1];
			break;
		case 3:
			TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag3Referenca").GetComponent<SpriteRenderer>().sprite;
			TextJezik.GetComponent<TextMesh>().text = JezikTekst[2];
			break;
		case 4:
			TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag4Referenca").GetComponent<SpriteRenderer>().sprite;
			TextJezik.GetComponent<TextMesh>().text = JezikTekst[3];
			break;
		case 5:
			TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag5Referenca").GetComponent<SpriteRenderer>().sprite;
			TextJezik.GetComponent<TextMesh>().text = JezikTekst[4];
			break;
		case 6:
			TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag6Referenca").GetComponent<SpriteRenderer>().sprite;
			TextJezik.GetComponent<TextMesh>().text = JezikTekst[5];
			break;
		case 7:
			TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag7Referenca").GetComponent<SpriteRenderer>().sprite;
			TextJezik.GetComponent<TextMesh>().text = JezikTekst[6];
			break;
		case 8:
			TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag8Referenca").GetComponent<SpriteRenderer>().sprite;
			TextJezik.GetComponent<TextMesh>().text = JezikTekst[7];
			break;
		case 9:
			TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag9Referenca").GetComponent<SpriteRenderer>().sprite;
			TextJezik.GetComponent<TextMesh>().text = JezikTekst[8];
			break;
		case 10:
			TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag10Referenca").GetComponent<SpriteRenderer>().sprite;
			TextJezik.GetComponent<TextMesh>().text = JezikTekst[9];
			break;
		case 11:
			TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag11Referenca").GetComponent<SpriteRenderer>().sprite;
			TextJezik.GetComponent<TextMesh>().text = JezikTekst[10];
			break;
		case 12:
			TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag12Referenca").GetComponent<SpriteRenderer>().sprite;
			TextJezik.GetComponent<TextMesh>().text = JezikTekst[11];
			break;
		case 13:
			TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag13Referenca").GetComponent<SpriteRenderer>().sprite;
			TextJezik.GetComponent<TextMesh>().text = JezikTekst[12];
			break;
		case 14:
			TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag14Referenca").GetComponent<SpriteRenderer>().sprite;
			TextJezik.GetComponent<TextMesh>().text = JezikTekst[13];
			break;
		}
	}

	private void Update()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_051f: Unknown result type (might be due to invalid IL or missing references)
		//IL_059a: Unknown result type (might be due to invalid IL or missing references)
		//IL_054a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0563: Unknown result type (might be due to invalid IL or missing references)
		//IL_0615: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0583: Unknown result type (might be due to invalid IL or missing references)
		//IL_058d: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0608: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_089a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0971: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a48: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ccd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f57: Unknown result type (might be due to invalid IL or missing references)
		//IL_1030: Unknown result type (might be due to invalid IL or missing references)
		//IL_1109: Unknown result type (might be due to invalid IL or missing references)
		if (Input.GetKeyUp((KeyCode)27))
		{
			Application.Quit();
		}
		if (!Input.GetMouseButtonUp(0))
		{
			return;
		}
		if (RaycastFunction(Input.mousePosition) == "ButtonMusic")
		{
			Debug.Log((object)"Music Button");
			if (!PlaySounds.musicOn)
			{
				PlaySounds.musicOn = true;
				muzikaOff = false;
				dugmeMuzika.GetComponent<SpriteRenderer>().sprite = dugmeMuzikaSprite;
				if (PlayerPrefs.HasKey("soundOn") && PlayerPrefs.GetInt("soundOn") == 1)
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
				muzikaOff = true;
				dugmeMuzika.GetComponent<SpriteRenderer>().sprite = dugmeMuzikaOffSprite;
				PlaySounds.Stop_BackgroundMusic_Menu();
				PlayerPrefs.SetInt("musicOn", 0);
				PlayerPrefs.Save();
			}
			Debug.Log((object)("Music Promena :" + PlayerPrefs.GetInt("musicOn")));
			Debug.Log((object)("MusicON: " + PlaySounds.musicOn));
		}
		else if (RaycastFunction(Input.mousePosition) == "ButtonSound")
		{
			Debug.Log((object)"Sound Button");
			if (!PlaySounds.soundOn)
			{
				PlaySounds.soundOn = true;
				soundOff = false;
				dugmeSound.GetComponent<SpriteRenderer>().sprite = dugmeSoundSprite;
				PlaySounds.Play_Button_SoundOn();
				PlayerPrefs.SetInt("soundOn", 1);
				PlayerPrefs.Save();
			}
			else
			{
				PlaySounds.soundOn = false;
				soundOff = true;
				dugmeSound.GetComponent<SpriteRenderer>().sprite = dugmeSoundOffSprite;
				PlayerPrefs.SetInt("soundOn", 0);
				PlayerPrefs.Save();
			}
			Debug.Log((object)("Sound Promena :" + PlayerPrefs.GetInt("soundOn")));
			Debug.Log((object)("SoundON: " + PlaySounds.soundOn));
		}
		else if (RaycastFunction(Input.mousePosition) == "MainButtonLeaderboard")
		{
			Debug.Log((object)"Leaderboard Button");
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_SoundOn();
			}
		}
		else if (RaycastFunction(Input.mousePosition) == "MainButtonResetProgress")
		{
			Debug.Log((object)"ResetProgress Button");
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_SoundOn();
			}
		}
		else if (RaycastFunction(Input.mousePosition) == "MainButtonResetTutorial")
		{
			Debug.Log((object)"ResetTutorial Button");
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_SoundOn();
			}
		}
		else if (RaycastFunction(Input.mousePosition) == "MainLeaderboardArrow")
		{
			Debug.Log((object)"LeaderboardArrow Button");
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_SoundOn();
			}
			if (LeaderBoardAktivan)
			{
				LeaderBoard.GetComponent<Animation>().Play("MainLeaderboardGo");
			}
			else
			{
				LeaderBoard.GetComponent<Animation>().Play("MainLeaderboardShow");
			}
			LeaderBoardAktivan = !LeaderBoardAktivan;
		}
		else if (RaycastFunction(Input.mousePosition) == "ButtonLanguage")
		{
			Debug.Log((object)"ButtonLanguage Button");
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_SoundOn();
			}
			if (LanguagesAktivan)
			{
				Zastave.SetActive(false);
				Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].normalizedTime = 1f;
				Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].speed = -1.5f;
				Languages.GetComponent<Animation>().Play("MainLanguageFlagsShow");
			}
			else
			{
				Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].normalizedTime = 0f;
				Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].speed = 1f;
				Languages.GetComponent<Animation>().Play("MainLanguageFlagsShow");
				((MonoBehaviour)this).StartCoroutine("PrikaziZastave");
			}
			LanguagesAktivan = !LanguagesAktivan;
		}
		else if (RaycastFunction(Input.mousePosition) == "ButtonFreeCoins")
		{
			Debug.Log((object)"ButtonFreeCoins Button");
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_SoundOn();
			}
			((MonoBehaviour)this).StartCoroutine(ShopManager.OpenFreeCoinsCard());
		}
		else if (RaycastFunction(Input.mousePosition) == "ButtonShop")
		{
			Debug.Log((object)"ButtonShop Button");
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_SoundOn();
			}
			((MonoBehaviour)this).StartCoroutine(ShopManager.OpenShopCard());
		}
		else if (RaycastFunction(Input.mousePosition) == "ButtonNews")
		{
			Debug.Log((object)"ButtonNews Button");
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_SoundOn();
			}
		}
		else if (RaycastFunction(Input.mousePosition) == "FaceButton")
		{
			if (!FacebookManager.Ulogovan)
			{
				Debug.Log((object)"LogOut Button");
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_Button_SoundOn();
				}
				Face.FacebookLogin();
			}
		}
		else if (RaycastFunction(Input.mousePosition) == "MainPlayButton")
		{
			Debug.Log((object)"Play Button");
			if (PlayerPrefs.HasKey("soundOn") && PlayerPrefs.GetInt("soundOn") == 1)
			{
				PlaySounds.Play_Button_Play();
			}
			((MonoBehaviour)this).StartCoroutine(otvoriSledeciNivo());
		}
		else if (RaycastFunction(Input.mousePosition) == "MainLanguageSlideLevo")
		{
			Debug.Log((object)"SlideLevo Button");
			if (!(Zastave.transform.position.x <= GameObject.Find("MainLanguageSlideLevo").transform.position.x - 9f))
			{
				Zastave.transform.Translate(Vector3.left * 0.5f, (Space)0);
			}
		}
		else if (RaycastFunction(Input.mousePosition) == "MainLanguageSlideDesno")
		{
			Debug.Log((object)"SlideDesno Button");
			if (!(Zastave.transform.position.x >= GameObject.Find("MainLanguageSlideDesno").transform.position.x - 3.5f))
			{
				Zastave.transform.Translate(Vector3.right * 0.5f, (Space)0);
			}
		}
		else if (RaycastFunction(Input.mousePosition) == "Flag1")
		{
			Debug.Log((object)"Flag1");
			TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag1").GetComponent<SpriteRenderer>().sprite;
			TextJezik.GetComponent<TextMesh>().text = JezikTekst[0];
			Zastave.SetActive(false);
			LanguagesAktivan = false;
			Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].normalizedTime = 1f;
			Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].speed = -1.5f;
			Languages.GetComponent<Animation>().Play("MainLanguageFlagsShow");
			PlayerPrefs.SetInt("LanguageFlag", 1);
			PlayerPrefs.Save();
		}
		else if (RaycastFunction(Input.mousePosition) == "Flag2")
		{
			Debug.Log((object)"Flag2");
			TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag2").GetComponent<SpriteRenderer>().sprite;
			TextJezik.GetComponent<TextMesh>().text = JezikTekst[1];
			Zastave.SetActive(false);
			LanguagesAktivan = false;
			Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].normalizedTime = 1f;
			Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].speed = -1.5f;
			Languages.GetComponent<Animation>().Play("MainLanguageFlagsShow");
			PlayerPrefs.SetInt("LanguageFlag", 2);
			PlayerPrefs.Save();
		}
		else if (RaycastFunction(Input.mousePosition) == "Flag3")
		{
			Debug.Log((object)"Flag3");
			TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag3").GetComponent<SpriteRenderer>().sprite;
			TextJezik.GetComponent<TextMesh>().text = JezikTekst[2];
			Zastave.SetActive(false);
			LanguagesAktivan = false;
			Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].normalizedTime = 1f;
			Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].speed = -1.5f;
			Languages.GetComponent<Animation>().Play("MainLanguageFlagsShow");
			PlayerPrefs.SetInt("LanguageFlag", 3);
			PlayerPrefs.Save();
		}
		else if (RaycastFunction(Input.mousePosition) == "Flag4")
		{
			Debug.Log((object)"Flag4");
			TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag4").GetComponent<SpriteRenderer>().sprite;
			TextJezik.GetComponent<TextMesh>().text = JezikTekst[3];
			Zastave.SetActive(false);
			LanguagesAktivan = false;
			Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].normalizedTime = 1f;
			Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].speed = -1.5f;
			Languages.GetComponent<Animation>().Play("MainLanguageFlagsShow");
			PlayerPrefs.SetInt("LanguageFlag", 4);
			PlayerPrefs.Save();
		}
		else if (RaycastFunction(Input.mousePosition) == "Flag5")
		{
			Debug.Log((object)"Flag5");
			TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag5").GetComponent<SpriteRenderer>().sprite;
			TextJezik.GetComponent<TextMesh>().text = JezikTekst[4];
			Zastave.SetActive(false);
			LanguagesAktivan = false;
			Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].normalizedTime = 1f;
			Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].speed = -1.5f;
			Languages.GetComponent<Animation>().Play("MainLanguageFlagsShow");
			PlayerPrefs.SetInt("LanguageFlag", 5);
			PlayerPrefs.Save();
		}
		else if (RaycastFunction(Input.mousePosition) == "Flag6")
		{
			Debug.Log((object)"Flag6");
			TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag6").GetComponent<SpriteRenderer>().sprite;
			TextJezik.GetComponent<TextMesh>().text = JezikTekst[5];
			Zastave.SetActive(false);
			LanguagesAktivan = false;
			Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].normalizedTime = 1f;
			Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].speed = -1.5f;
			Languages.GetComponent<Animation>().Play("MainLanguageFlagsShow");
			PlayerPrefs.SetInt("LanguageFlag", 6);
			PlayerPrefs.Save();
		}
		else if (RaycastFunction(Input.mousePosition) == "Flag7")
		{
			Debug.Log((object)"Flag7");
			TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag7").GetComponent<SpriteRenderer>().sprite;
			TextJezik.GetComponent<TextMesh>().text = JezikTekst[6];
			Zastave.SetActive(false);
			LanguagesAktivan = false;
			Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].normalizedTime = 1f;
			Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].speed = -1.5f;
			Languages.GetComponent<Animation>().Play("MainLanguageFlagsShow");
			PlayerPrefs.SetInt("LanguageFlag", 7);
			PlayerPrefs.Save();
		}
		else if (RaycastFunction(Input.mousePosition) == "Flag8")
		{
			Debug.Log((object)"Flag8");
			TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag8").GetComponent<SpriteRenderer>().sprite;
			TextJezik.GetComponent<TextMesh>().text = JezikTekst[7];
			Zastave.SetActive(false);
			LanguagesAktivan = false;
			Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].normalizedTime = 1f;
			Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].speed = -1.5f;
			Languages.GetComponent<Animation>().Play("MainLanguageFlagsShow");
			PlayerPrefs.SetInt("LanguageFlag", 8);
			PlayerPrefs.Save();
		}
		else if (RaycastFunction(Input.mousePosition) == "Flag9")
		{
			Debug.Log((object)"Flag9");
			TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag9").GetComponent<SpriteRenderer>().sprite;
			TextJezik.GetComponent<TextMesh>().text = JezikTekst[8];
			Zastave.SetActive(false);
			LanguagesAktivan = false;
			Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].normalizedTime = 1f;
			Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].speed = -1.5f;
			Languages.GetComponent<Animation>().Play("MainLanguageFlagsShow");
			PlayerPrefs.SetInt("LanguageFlag", 9);
			PlayerPrefs.Save();
		}
		else if (RaycastFunction(Input.mousePosition) == "Flag10")
		{
			Debug.Log((object)"Flag10");
			TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag10").GetComponent<SpriteRenderer>().sprite;
			TextJezik.GetComponent<TextMesh>().text = JezikTekst[9];
			Zastave.SetActive(false);
			LanguagesAktivan = false;
			Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].normalizedTime = 1f;
			Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].speed = -1.5f;
			Languages.GetComponent<Animation>().Play("MainLanguageFlagsShow");
			PlayerPrefs.SetInt("LanguageFlag", 10);
			PlayerPrefs.Save();
		}
		else if (RaycastFunction(Input.mousePosition) == "Flag11")
		{
			Debug.Log((object)"Flag11");
			TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag11").GetComponent<SpriteRenderer>().sprite;
			TextJezik.GetComponent<TextMesh>().text = JezikTekst[10];
			Zastave.SetActive(false);
			LanguagesAktivan = false;
			Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].normalizedTime = 1f;
			Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].speed = -1.5f;
			Languages.GetComponent<Animation>().Play("MainLanguageFlagsShow");
			PlayerPrefs.SetInt("LanguageFlag", 11);
			PlayerPrefs.Save();
		}
		else if (RaycastFunction(Input.mousePosition) == "Flag12")
		{
			Debug.Log((object)"Flag12");
			TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag12").GetComponent<SpriteRenderer>().sprite;
			TextJezik.GetComponent<TextMesh>().text = JezikTekst[11];
			Zastave.SetActive(false);
			LanguagesAktivan = false;
			Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].normalizedTime = 1f;
			Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].speed = -1.5f;
			Languages.GetComponent<Animation>().Play("MainLanguageFlagsShow");
			PlayerPrefs.SetInt("LanguageFlag", 12);
			PlayerPrefs.Save();
		}
		else if (RaycastFunction(Input.mousePosition) == "Flag13")
		{
			Debug.Log((object)"Flag13");
			TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag13").GetComponent<SpriteRenderer>().sprite;
			TextJezik.GetComponent<TextMesh>().text = JezikTekst[12];
			Zastave.SetActive(false);
			LanguagesAktivan = false;
			Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].normalizedTime = 1f;
			Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].speed = -1.5f;
			Languages.GetComponent<Animation>().Play("MainLanguageFlagsShow");
			PlayerPrefs.SetInt("LanguageFlag", 13);
			PlayerPrefs.Save();
		}
		else if (RaycastFunction(Input.mousePosition) == "Flag14")
		{
			Debug.Log((object)"Flag14");
			TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag14").GetComponent<SpriteRenderer>().sprite;
			TextJezik.GetComponent<TextMesh>().text = JezikTekst[13];
			Zastave.SetActive(false);
			LanguagesAktivan = false;
			Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].normalizedTime = 1f;
			Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].speed = -1.5f;
			Languages.GetComponent<Animation>().Play("MainLanguageFlagsShow");
			PlayerPrefs.SetInt("LanguageFlag", 14);
			PlayerPrefs.Save();
		}
	}

	private IEnumerator otvoriSledeciNivo()
	{
		yield return null;
		if (StagesParser.odgledaoTutorial == 0)
		{
			StagesParser.loadingTip = 1;
			Application.LoadLevel("LoadingScene");
		}
		else
		{
			Application.LoadLevel("All Maps");
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

	private IEnumerator PrikaziZastave()
	{
		yield return (object)new WaitForSeconds(0.5f);
		Zastave.SetActive(true);
	}
}
