using System;
using System.Collections;
using UnityEngine;

// Token: 0x020004BE RID: 1214
public class MainMenuManageFull : MonoBehaviour
{
	// Token: 0x0600265E RID: 9822 RVA: 0x0010AC68 File Offset: 0x00108E68
	private void Awake()
	{
		GameObject.Find("HolderGornjiDesniUgaoDugmici").GetComponent<Transform>().position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.one).x, Camera.main.ViewportToWorldPoint(Vector3.one).y, -0.05f);
		GameObject.Find("HolderGornjiLeviUgaoDugmici").GetComponent<Transform>().position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.zero).x, Camera.main.ViewportToWorldPoint(Vector3.one).y, -0.05f);
		GameObject.Find("HolderDonjiDesniUgaoDugmici").GetComponent<Transform>().position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.one).x, Camera.main.ViewportToWorldPoint(Vector3.zero).y, -0.05f);
		GameObject.Find("HolderDonjiLeviUgaoDugmici").GetComponent<Transform>().position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.zero).x, Camera.main.ViewportToWorldPoint(Vector3.zero).y, -0.05f);
		this.Face = GameObject.Find("FacebookManager").GetComponent<FacebookManager>();
		this.LeaderBoard = GameObject.Find("HolderLeaderboardMove");
		this.dugmeMuzika = GameObject.Find("ButtonMusic");
		this.dugmeSound = GameObject.Find("ButtonSound");
		this.dugmeMuzikaSprite = GameObject.Find("ButtonMusic").GetComponent<SpriteRenderer>().sprite;
		this.dugmeSoundSprite = GameObject.Find("ButtonSound").GetComponent<SpriteRenderer>().sprite;
		this.dugmeMuzikaOffSprite = GameObject.Find("ButtonMusicOff").GetComponent<SpriteRenderer>().sprite;
		this.dugmeSoundOffSprite = GameObject.Find("ButtonSoundOff").GetComponent<SpriteRenderer>().sprite;
		this.Languages = GameObject.Find("HolderLanguageFlagsMove");
		this.Languages.GetComponent<Animation>().Play("MainLanguageFlagsPosition");
		this.Zastave = GameObject.Find("Zastave");
		this.Zastave.SetActive(false);
		MainMenuManageFull.LanguagesAktivan = false;
		this.TrenutnaZastava = GameObject.Find("TrenutniJezik");
		this.TextJezik = GameObject.Find("Text2letters");
	}

	// Token: 0x0600265F RID: 9823 RVA: 0x0010AEA4 File Offset: 0x001090A4
	private void Start()
	{
		GameObject.Find("PrinceGorilla").GetComponent<Animator>().Play("Idle Main Screen");
		if (PlaySounds.musicOn)
		{
			if (!PlaySounds.BackgroundMusic_Menu.isPlaying)
			{
				PlaySounds.Play_BackgroundMusic_Menu();
			}
			this.dugmeMuzika.GetComponent<SpriteRenderer>().sprite = this.dugmeMuzikaSprite;
		}
		else
		{
			this.dugmeMuzika.GetComponent<SpriteRenderer>().sprite = this.dugmeMuzikaOffSprite;
		}
		if (PlaySounds.soundOn)
		{
			this.dugmeSound.GetComponent<SpriteRenderer>().sprite = this.dugmeSoundSprite;
		}
		else
		{
			this.dugmeSound.GetComponent<SpriteRenderer>().sprite = this.dugmeSoundOffSprite;
		}
		this.BrojZastave = PlayerPrefs.GetInt("LanguageFlag");
		Debug.Log("BrojZastave" + this.BrojZastave);
		switch (this.BrojZastave)
		{
		case 1:
			this.TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag1Referenca").GetComponent<SpriteRenderer>().sprite;
			this.TextJezik.GetComponent<TextMesh>().text = this.JezikTekst[0];
			return;
		case 2:
			this.TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag2Referenca").GetComponent<SpriteRenderer>().sprite;
			this.TextJezik.GetComponent<TextMesh>().text = this.JezikTekst[1];
			return;
		case 3:
			this.TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag3Referenca").GetComponent<SpriteRenderer>().sprite;
			this.TextJezik.GetComponent<TextMesh>().text = this.JezikTekst[2];
			return;
		case 4:
			this.TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag4Referenca").GetComponent<SpriteRenderer>().sprite;
			this.TextJezik.GetComponent<TextMesh>().text = this.JezikTekst[3];
			return;
		case 5:
			this.TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag5Referenca").GetComponent<SpriteRenderer>().sprite;
			this.TextJezik.GetComponent<TextMesh>().text = this.JezikTekst[4];
			return;
		case 6:
			this.TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag6Referenca").GetComponent<SpriteRenderer>().sprite;
			this.TextJezik.GetComponent<TextMesh>().text = this.JezikTekst[5];
			return;
		case 7:
			this.TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag7Referenca").GetComponent<SpriteRenderer>().sprite;
			this.TextJezik.GetComponent<TextMesh>().text = this.JezikTekst[6];
			return;
		case 8:
			this.TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag8Referenca").GetComponent<SpriteRenderer>().sprite;
			this.TextJezik.GetComponent<TextMesh>().text = this.JezikTekst[7];
			return;
		case 9:
			this.TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag9Referenca").GetComponent<SpriteRenderer>().sprite;
			this.TextJezik.GetComponent<TextMesh>().text = this.JezikTekst[8];
			return;
		case 10:
			this.TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag10Referenca").GetComponent<SpriteRenderer>().sprite;
			this.TextJezik.GetComponent<TextMesh>().text = this.JezikTekst[9];
			return;
		case 11:
			this.TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag11Referenca").GetComponent<SpriteRenderer>().sprite;
			this.TextJezik.GetComponent<TextMesh>().text = this.JezikTekst[10];
			return;
		case 12:
			this.TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag12Referenca").GetComponent<SpriteRenderer>().sprite;
			this.TextJezik.GetComponent<TextMesh>().text = this.JezikTekst[11];
			return;
		case 13:
			this.TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag13Referenca").GetComponent<SpriteRenderer>().sprite;
			this.TextJezik.GetComponent<TextMesh>().text = this.JezikTekst[12];
			return;
		case 14:
			this.TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag14Referenca").GetComponent<SpriteRenderer>().sprite;
			this.TextJezik.GetComponent<TextMesh>().text = this.JezikTekst[13];
			return;
		default:
			return;
		}
	}

	// Token: 0x06002660 RID: 9824 RVA: 0x0010B314 File Offset: 0x00109514
	private void Update()
	{
		if (Input.GetKeyUp(27))
		{
			Application.Quit();
		}
		if (Input.GetMouseButtonUp(0))
		{
			if (this.RaycastFunction(Input.mousePosition) == "ButtonMusic")
			{
				Debug.Log("Music Button");
				if (!PlaySounds.musicOn)
				{
					PlaySounds.musicOn = true;
					this.muzikaOff = false;
					this.dugmeMuzika.GetComponent<SpriteRenderer>().sprite = this.dugmeMuzikaSprite;
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
					this.muzikaOff = true;
					this.dugmeMuzika.GetComponent<SpriteRenderer>().sprite = this.dugmeMuzikaOffSprite;
					PlaySounds.Stop_BackgroundMusic_Menu();
					PlayerPrefs.SetInt("musicOn", 0);
					PlayerPrefs.Save();
				}
				Debug.Log("Music Promena :" + PlayerPrefs.GetInt("musicOn"));
				Debug.Log("MusicON: " + PlaySounds.musicOn.ToString());
				return;
			}
			if (this.RaycastFunction(Input.mousePosition) == "ButtonSound")
			{
				Debug.Log("Sound Button");
				if (!PlaySounds.soundOn)
				{
					PlaySounds.soundOn = true;
					this.soundOff = false;
					this.dugmeSound.GetComponent<SpriteRenderer>().sprite = this.dugmeSoundSprite;
					PlaySounds.Play_Button_SoundOn();
					PlayerPrefs.SetInt("soundOn", 1);
					PlayerPrefs.Save();
				}
				else
				{
					PlaySounds.soundOn = false;
					this.soundOff = true;
					this.dugmeSound.GetComponent<SpriteRenderer>().sprite = this.dugmeSoundOffSprite;
					PlayerPrefs.SetInt("soundOn", 0);
					PlayerPrefs.Save();
				}
				Debug.Log("Sound Promena :" + PlayerPrefs.GetInt("soundOn"));
				Debug.Log("SoundON: " + PlaySounds.soundOn.ToString());
				return;
			}
			if (this.RaycastFunction(Input.mousePosition) == "MainButtonLeaderboard")
			{
				Debug.Log("Leaderboard Button");
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_Button_SoundOn();
					return;
				}
			}
			else if (this.RaycastFunction(Input.mousePosition) == "MainButtonResetProgress")
			{
				Debug.Log("ResetProgress Button");
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_Button_SoundOn();
					return;
				}
			}
			else if (this.RaycastFunction(Input.mousePosition) == "MainButtonResetTutorial")
			{
				Debug.Log("ResetTutorial Button");
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_Button_SoundOn();
					return;
				}
			}
			else
			{
				if (this.RaycastFunction(Input.mousePosition) == "MainLeaderboardArrow")
				{
					Debug.Log("LeaderboardArrow Button");
					if (PlaySounds.soundOn)
					{
						PlaySounds.Play_Button_SoundOn();
					}
					if (this.LeaderBoardAktivan)
					{
						this.LeaderBoard.GetComponent<Animation>().Play("MainLeaderboardGo");
					}
					else
					{
						this.LeaderBoard.GetComponent<Animation>().Play("MainLeaderboardShow");
					}
					this.LeaderBoardAktivan = !this.LeaderBoardAktivan;
					return;
				}
				if (this.RaycastFunction(Input.mousePosition) == "ButtonLanguage")
				{
					Debug.Log("ButtonLanguage Button");
					if (PlaySounds.soundOn)
					{
						PlaySounds.Play_Button_SoundOn();
					}
					if (MainMenuManageFull.LanguagesAktivan)
					{
						this.Zastave.SetActive(false);
						this.Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].normalizedTime = 1f;
						this.Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].speed = -1.5f;
						this.Languages.GetComponent<Animation>().Play("MainLanguageFlagsShow");
					}
					else
					{
						this.Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].normalizedTime = 0f;
						this.Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].speed = 1f;
						this.Languages.GetComponent<Animation>().Play("MainLanguageFlagsShow");
						base.StartCoroutine("PrikaziZastave");
					}
					MainMenuManageFull.LanguagesAktivan = !MainMenuManageFull.LanguagesAktivan;
					return;
				}
				if (this.RaycastFunction(Input.mousePosition) == "ButtonFreeCoins")
				{
					Debug.Log("ButtonFreeCoins Button");
					if (PlaySounds.soundOn)
					{
						PlaySounds.Play_Button_SoundOn();
					}
					base.StartCoroutine(ShopManager.OpenFreeCoinsCard());
					return;
				}
				if (this.RaycastFunction(Input.mousePosition) == "ButtonShop")
				{
					Debug.Log("ButtonShop Button");
					if (PlaySounds.soundOn)
					{
						PlaySounds.Play_Button_SoundOn();
					}
					base.StartCoroutine(ShopManager.OpenShopCard());
					return;
				}
				if (this.RaycastFunction(Input.mousePosition) == "ButtonNews")
				{
					Debug.Log("ButtonNews Button");
					if (PlaySounds.soundOn)
					{
						PlaySounds.Play_Button_SoundOn();
						return;
					}
				}
				else if (this.RaycastFunction(Input.mousePosition) == "FaceButton")
				{
					if (!FacebookManager.Ulogovan)
					{
						Debug.Log("LogOut Button");
						if (PlaySounds.soundOn)
						{
							PlaySounds.Play_Button_SoundOn();
						}
						this.Face.FacebookLogin();
						return;
					}
				}
				else
				{
					if (this.RaycastFunction(Input.mousePosition) == "MainPlayButton")
					{
						Debug.Log("Play Button");
						if (PlayerPrefs.HasKey("soundOn") && PlayerPrefs.GetInt("soundOn") == 1)
						{
							PlaySounds.Play_Button_Play();
						}
						base.StartCoroutine(this.otvoriSledeciNivo());
						return;
					}
					if (this.RaycastFunction(Input.mousePosition) == "MainLanguageSlideLevo")
					{
						Debug.Log("SlideLevo Button");
						if (this.Zastave.transform.position.x > GameObject.Find("MainLanguageSlideLevo").transform.position.x - 9f)
						{
							this.Zastave.transform.Translate(Vector3.left * 0.5f, 0);
							return;
						}
					}
					else if (this.RaycastFunction(Input.mousePosition) == "MainLanguageSlideDesno")
					{
						Debug.Log("SlideDesno Button");
						if (this.Zastave.transform.position.x < GameObject.Find("MainLanguageSlideDesno").transform.position.x - 3.5f)
						{
							this.Zastave.transform.Translate(Vector3.right * 0.5f, 0);
							return;
						}
					}
					else
					{
						if (this.RaycastFunction(Input.mousePosition) == "Flag1")
						{
							Debug.Log("Flag1");
							this.TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag1").GetComponent<SpriteRenderer>().sprite;
							this.TextJezik.GetComponent<TextMesh>().text = this.JezikTekst[0];
							this.Zastave.SetActive(false);
							MainMenuManageFull.LanguagesAktivan = false;
							this.Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].normalizedTime = 1f;
							this.Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].speed = -1.5f;
							this.Languages.GetComponent<Animation>().Play("MainLanguageFlagsShow");
							PlayerPrefs.SetInt("LanguageFlag", 1);
							PlayerPrefs.Save();
							return;
						}
						if (this.RaycastFunction(Input.mousePosition) == "Flag2")
						{
							Debug.Log("Flag2");
							this.TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag2").GetComponent<SpriteRenderer>().sprite;
							this.TextJezik.GetComponent<TextMesh>().text = this.JezikTekst[1];
							this.Zastave.SetActive(false);
							MainMenuManageFull.LanguagesAktivan = false;
							this.Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].normalizedTime = 1f;
							this.Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].speed = -1.5f;
							this.Languages.GetComponent<Animation>().Play("MainLanguageFlagsShow");
							PlayerPrefs.SetInt("LanguageFlag", 2);
							PlayerPrefs.Save();
							return;
						}
						if (this.RaycastFunction(Input.mousePosition) == "Flag3")
						{
							Debug.Log("Flag3");
							this.TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag3").GetComponent<SpriteRenderer>().sprite;
							this.TextJezik.GetComponent<TextMesh>().text = this.JezikTekst[2];
							this.Zastave.SetActive(false);
							MainMenuManageFull.LanguagesAktivan = false;
							this.Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].normalizedTime = 1f;
							this.Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].speed = -1.5f;
							this.Languages.GetComponent<Animation>().Play("MainLanguageFlagsShow");
							PlayerPrefs.SetInt("LanguageFlag", 3);
							PlayerPrefs.Save();
							return;
						}
						if (this.RaycastFunction(Input.mousePosition) == "Flag4")
						{
							Debug.Log("Flag4");
							this.TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag4").GetComponent<SpriteRenderer>().sprite;
							this.TextJezik.GetComponent<TextMesh>().text = this.JezikTekst[3];
							this.Zastave.SetActive(false);
							MainMenuManageFull.LanguagesAktivan = false;
							this.Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].normalizedTime = 1f;
							this.Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].speed = -1.5f;
							this.Languages.GetComponent<Animation>().Play("MainLanguageFlagsShow");
							PlayerPrefs.SetInt("LanguageFlag", 4);
							PlayerPrefs.Save();
							return;
						}
						if (this.RaycastFunction(Input.mousePosition) == "Flag5")
						{
							Debug.Log("Flag5");
							this.TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag5").GetComponent<SpriteRenderer>().sprite;
							this.TextJezik.GetComponent<TextMesh>().text = this.JezikTekst[4];
							this.Zastave.SetActive(false);
							MainMenuManageFull.LanguagesAktivan = false;
							this.Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].normalizedTime = 1f;
							this.Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].speed = -1.5f;
							this.Languages.GetComponent<Animation>().Play("MainLanguageFlagsShow");
							PlayerPrefs.SetInt("LanguageFlag", 5);
							PlayerPrefs.Save();
							return;
						}
						if (this.RaycastFunction(Input.mousePosition) == "Flag6")
						{
							Debug.Log("Flag6");
							this.TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag6").GetComponent<SpriteRenderer>().sprite;
							this.TextJezik.GetComponent<TextMesh>().text = this.JezikTekst[5];
							this.Zastave.SetActive(false);
							MainMenuManageFull.LanguagesAktivan = false;
							this.Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].normalizedTime = 1f;
							this.Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].speed = -1.5f;
							this.Languages.GetComponent<Animation>().Play("MainLanguageFlagsShow");
							PlayerPrefs.SetInt("LanguageFlag", 6);
							PlayerPrefs.Save();
							return;
						}
						if (this.RaycastFunction(Input.mousePosition) == "Flag7")
						{
							Debug.Log("Flag7");
							this.TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag7").GetComponent<SpriteRenderer>().sprite;
							this.TextJezik.GetComponent<TextMesh>().text = this.JezikTekst[6];
							this.Zastave.SetActive(false);
							MainMenuManageFull.LanguagesAktivan = false;
							this.Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].normalizedTime = 1f;
							this.Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].speed = -1.5f;
							this.Languages.GetComponent<Animation>().Play("MainLanguageFlagsShow");
							PlayerPrefs.SetInt("LanguageFlag", 7);
							PlayerPrefs.Save();
							return;
						}
						if (this.RaycastFunction(Input.mousePosition) == "Flag8")
						{
							Debug.Log("Flag8");
							this.TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag8").GetComponent<SpriteRenderer>().sprite;
							this.TextJezik.GetComponent<TextMesh>().text = this.JezikTekst[7];
							this.Zastave.SetActive(false);
							MainMenuManageFull.LanguagesAktivan = false;
							this.Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].normalizedTime = 1f;
							this.Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].speed = -1.5f;
							this.Languages.GetComponent<Animation>().Play("MainLanguageFlagsShow");
							PlayerPrefs.SetInt("LanguageFlag", 8);
							PlayerPrefs.Save();
							return;
						}
						if (this.RaycastFunction(Input.mousePosition) == "Flag9")
						{
							Debug.Log("Flag9");
							this.TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag9").GetComponent<SpriteRenderer>().sprite;
							this.TextJezik.GetComponent<TextMesh>().text = this.JezikTekst[8];
							this.Zastave.SetActive(false);
							MainMenuManageFull.LanguagesAktivan = false;
							this.Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].normalizedTime = 1f;
							this.Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].speed = -1.5f;
							this.Languages.GetComponent<Animation>().Play("MainLanguageFlagsShow");
							PlayerPrefs.SetInt("LanguageFlag", 9);
							PlayerPrefs.Save();
							return;
						}
						if (this.RaycastFunction(Input.mousePosition) == "Flag10")
						{
							Debug.Log("Flag10");
							this.TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag10").GetComponent<SpriteRenderer>().sprite;
							this.TextJezik.GetComponent<TextMesh>().text = this.JezikTekst[9];
							this.Zastave.SetActive(false);
							MainMenuManageFull.LanguagesAktivan = false;
							this.Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].normalizedTime = 1f;
							this.Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].speed = -1.5f;
							this.Languages.GetComponent<Animation>().Play("MainLanguageFlagsShow");
							PlayerPrefs.SetInt("LanguageFlag", 10);
							PlayerPrefs.Save();
							return;
						}
						if (this.RaycastFunction(Input.mousePosition) == "Flag11")
						{
							Debug.Log("Flag11");
							this.TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag11").GetComponent<SpriteRenderer>().sprite;
							this.TextJezik.GetComponent<TextMesh>().text = this.JezikTekst[10];
							this.Zastave.SetActive(false);
							MainMenuManageFull.LanguagesAktivan = false;
							this.Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].normalizedTime = 1f;
							this.Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].speed = -1.5f;
							this.Languages.GetComponent<Animation>().Play("MainLanguageFlagsShow");
							PlayerPrefs.SetInt("LanguageFlag", 11);
							PlayerPrefs.Save();
							return;
						}
						if (this.RaycastFunction(Input.mousePosition) == "Flag12")
						{
							Debug.Log("Flag12");
							this.TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag12").GetComponent<SpriteRenderer>().sprite;
							this.TextJezik.GetComponent<TextMesh>().text = this.JezikTekst[11];
							this.Zastave.SetActive(false);
							MainMenuManageFull.LanguagesAktivan = false;
							this.Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].normalizedTime = 1f;
							this.Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].speed = -1.5f;
							this.Languages.GetComponent<Animation>().Play("MainLanguageFlagsShow");
							PlayerPrefs.SetInt("LanguageFlag", 12);
							PlayerPrefs.Save();
							return;
						}
						if (this.RaycastFunction(Input.mousePosition) == "Flag13")
						{
							Debug.Log("Flag13");
							this.TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag13").GetComponent<SpriteRenderer>().sprite;
							this.TextJezik.GetComponent<TextMesh>().text = this.JezikTekst[12];
							this.Zastave.SetActive(false);
							MainMenuManageFull.LanguagesAktivan = false;
							this.Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].normalizedTime = 1f;
							this.Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].speed = -1.5f;
							this.Languages.GetComponent<Animation>().Play("MainLanguageFlagsShow");
							PlayerPrefs.SetInt("LanguageFlag", 13);
							PlayerPrefs.Save();
							return;
						}
						if (this.RaycastFunction(Input.mousePosition) == "Flag14")
						{
							Debug.Log("Flag14");
							this.TrenutnaZastava.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Flag14").GetComponent<SpriteRenderer>().sprite;
							this.TextJezik.GetComponent<TextMesh>().text = this.JezikTekst[13];
							this.Zastave.SetActive(false);
							MainMenuManageFull.LanguagesAktivan = false;
							this.Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].normalizedTime = 1f;
							this.Languages.GetComponent<Animation>()["MainLanguageFlagsShow"].speed = -1.5f;
							this.Languages.GetComponent<Animation>().Play("MainLanguageFlagsShow");
							PlayerPrefs.SetInt("LanguageFlag", 14);
							PlayerPrefs.Save();
						}
					}
				}
			}
		}
	}

	// Token: 0x06002661 RID: 9825 RVA: 0x0010C501 File Offset: 0x0010A701
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
		yield break;
	}

	// Token: 0x06002662 RID: 9826 RVA: 0x0010C50C File Offset: 0x0010A70C
	private string RaycastFunction(Vector3 vector)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(vector), ref raycastHit))
		{
			return raycastHit.collider.name;
		}
		return "";
	}

	// Token: 0x06002663 RID: 9827 RVA: 0x0010C53F File Offset: 0x0010A73F
	private IEnumerator PrikaziZastave()
	{
		yield return new WaitForSeconds(0.5f);
		this.Zastave.SetActive(true);
		yield break;
	}

	// Token: 0x04001FAC RID: 8108
	private FacebookManager Face;

	// Token: 0x04001FAD RID: 8109
	private Sprite dugmeMuzikaSprite;

	// Token: 0x04001FAE RID: 8110
	private Sprite dugmeSoundSprite;

	// Token: 0x04001FAF RID: 8111
	private Sprite dugmeMuzikaOffSprite;

	// Token: 0x04001FB0 RID: 8112
	private Sprite dugmeSoundOffSprite;

	// Token: 0x04001FB1 RID: 8113
	private GameObject dugmeMuzika;

	// Token: 0x04001FB2 RID: 8114
	private GameObject dugmeSound;

	// Token: 0x04001FB3 RID: 8115
	private GameObject dugmePlay;

	// Token: 0x04001FB4 RID: 8116
	private GameObject holderLogo;

	// Token: 0x04001FB5 RID: 8117
	private GameObject majmunLogo;

	// Token: 0x04001FB6 RID: 8118
	private GameObject LeaderBoard;

	// Token: 0x04001FB7 RID: 8119
	private GameObject Languages;

	// Token: 0x04001FB8 RID: 8120
	private GameObject TrenutnaZastava;

	// Token: 0x04001FB9 RID: 8121
	private bool muzikaOff;

	// Token: 0x04001FBA RID: 8122
	private bool soundOff;

	// Token: 0x04001FBB RID: 8123
	private NivoManager nivoManager;

	// Token: 0x04001FBC RID: 8124
	private AudioSource MusicOn_Button;

	// Token: 0x04001FBD RID: 8125
	private AudioSource SoundOn_Button;

	// Token: 0x04001FBE RID: 8126
	private AudioSource Play_Button;

	// Token: 0x04001FBF RID: 8127
	private bool LeaderBoardAktivan;

	// Token: 0x04001FC0 RID: 8128
	private int BrojZastave;

	// Token: 0x04001FC1 RID: 8129
	public static bool LanguagesAktivan;

	// Token: 0x04001FC2 RID: 8130
	private float x;

	// Token: 0x04001FC3 RID: 8131
	private float y;

	// Token: 0x04001FC4 RID: 8132
	private float z;

	// Token: 0x04001FC5 RID: 8133
	private GameObject Zastave;

	// Token: 0x04001FC6 RID: 8134
	private GameObject TextJezik;

	// Token: 0x04001FC7 RID: 8135
	private string[] JezikTekst = new string[]
	{
		"RU",
		"GR",
		"CH",
		"ENG",
		"ESP",
		"SRB",
		"SVD",
		"IT",
		"FR",
		"POR",
		"AR",
		"CRO",
		"KO",
		"JPN"
	};
}
