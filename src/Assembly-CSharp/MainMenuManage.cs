using System.Collections;
using UnityEngine;

public class MainMenuManage : MonoBehaviour
{
	private Sprite dugmeMuzikaSprite;

	private Sprite dugmeSoundSprite;

	private Sprite dugmeMuzikaOffSprite;

	private Sprite dugmeSoundOffSprite;

	private GameObject dugmeMuzika;

	private GameObject dugmeSound;

	private GameObject dugmePlay;

	private GameObject holderLogo;

	private GameObject majmunLogo;

	private ParticleSystem bananaRasipuje;

	private bool muzikaOff;

	private bool soundOff;

	private AudioSource MusicOn_Button;

	private AudioSource SoundOn_Button;

	private AudioSource Play_Button;

	private string clickedItem;

	private string releasedItem;

	private Vector3 originalScale;

	private Transform dock_goreDesno;

	private Transform dock_doleDesno;

	private Transform dock_goreLevo;

	private Transform dock_doleLevo;

	private void Awake()
	{
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		holderLogo = GameObject.Find("HolderLogoGlavni");
		majmunLogo = GameObject.Find("HolderMajmun");
		dugmeMuzika = GameObject.Find("MusicMain");
		dugmeSound = GameObject.Find("SoundMain");
		dugmeMuzikaSprite = dugmeMuzika.GetComponent<SpriteRenderer>().sprite;
		dugmeSoundSprite = dugmeSound.GetComponent<SpriteRenderer>().sprite;
		dugmeMuzikaOffSprite = GameObject.Find("MusicOffMain").GetComponent<SpriteRenderer>().sprite;
		dugmeSoundOffSprite = GameObject.Find("SoundOff").GetComponent<SpriteRenderer>().sprite;
		GameObject.Find("LifeManager").transform.position = new Vector3(50f, 50f, 0f);
		dock_goreDesno = GameObject.Find("Dock_GoreDesno").transform;
		dock_doleDesno = GameObject.Find("Dock_DoleDesno").transform;
		dock_goreLevo = GameObject.Find("Dock_GoreLevo").transform;
		dock_doleLevo = GameObject.Find("Dock_DoleLevo").transform;
		dock_doleDesno.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.one).x, Camera.main.ViewportToWorldPoint(Vector3.zero).y, holderLogo.transform.position.z);
		dock_doleLevo.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.zero).x, Camera.main.ViewportToWorldPoint(Vector3.zero).y, holderLogo.transform.position.z);
		dock_goreDesno.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.one).x, Camera.main.ViewportToWorldPoint(Vector3.one).y, holderLogo.transform.position.z);
		dock_goreLevo.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.zero).x, Camera.main.ViewportToWorldPoint(Vector3.one).y, holderLogo.transform.position.z);
	}

	private void Start()
	{
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
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
		if (!ShopManager.shopExists)
		{
			GameObject.Find("ButtonFreeCoinsDock").transform.position = GameObject.Find("ButtonShop").transform.position;
			GameObject.Find("ButtonShop").SetActive(false);
		}
		if (!ShopManager.freeCoinsExists)
		{
			GameObject.Find("ButtonFreeCoins").SetActive(false);
		}
		ShopManager.RescaleShop();
	}

	private void Update()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		if (Input.GetKeyUp((KeyCode)27))
		{
			_ = ShopManager.otvorenShop;
		}
		if (Input.GetMouseButtonDown(0))
		{
			clickedItem = RaycastFunction(Input.mousePosition);
			if (clickedItem.Equals("PlayMain") || clickedItem.Equals("PlayMainFly") || clickedItem.Equals("ButtonFreeCoins") || clickedItem.Equals("ButtonShop") || clickedItem.Equals("ButtonExit"))
			{
				GameObject val = GameObject.Find(clickedItem);
				originalScale = val.transform.localScale;
				val.transform.localScale = originalScale * 0.8f;
			}
			else if (clickedItem != string.Empty)
			{
				GameObject val2 = GameObject.Find(clickedItem);
				originalScale = val2.transform.localScale;
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
		GameObject.Find(clickedItem).transform.localScale = originalScale;
		if (releasedItem == "MusicMain")
		{
			if (!PlaySounds.musicOn)
			{
				PlaySounds.musicOn = true;
				muzikaOff = false;
				((Renderer)((Component)dugmeMuzika.transform.GetChild(0).GetChild(0)).GetComponent<SpriteRenderer>()).enabled = false;
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
				((Renderer)((Component)dugmeMuzika.transform.GetChild(0).GetChild(0)).GetComponent<SpriteRenderer>()).enabled = true;
				PlaySounds.Stop_BackgroundMusic_Menu();
				PlayerPrefs.SetInt("musicOn", 0);
				PlayerPrefs.Save();
			}
		}
		else if (releasedItem == "SoundMain")
		{
			if (!PlaySounds.soundOn)
			{
				PlaySounds.soundOn = true;
				soundOff = false;
				((Renderer)((Component)dugmeSound.transform.GetChild(0).GetChild(0)).GetComponent<SpriteRenderer>()).enabled = false;
				PlaySounds.Play_Button_SoundOn();
				PlayerPrefs.SetInt("soundOn", 1);
				PlayerPrefs.Save();
			}
			else
			{
				PlaySounds.soundOn = false;
				soundOff = true;
				((Renderer)((Component)dugmeSound.transform.GetChild(0).GetChild(0)).GetComponent<SpriteRenderer>()).enabled = true;
				PlayerPrefs.SetInt("soundOn", 0);
				PlayerPrefs.Save();
			}
		}
		else if (releasedItem == "PlayMainFly")
		{
			GameObject.Find(releasedItem).GetComponent<Collider>().enabled = false;
			if (PlayerPrefs.HasKey("soundOn") && PlayerPrefs.GetInt("soundOn") == 1)
			{
				PlaySounds.Play_Button_Play();
			}
			if (!PlayerPrefs.HasKey("OdgledaoTutorial"))
			{
				Application.LoadLevel("LoadingScene");
			}
			else
			{
				Application.LoadLevel(3);
			}
		}
		else if (releasedItem == "ButtonFreeCoins")
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
			((MonoBehaviour)this).StartCoroutine(ShopManager.OpenFreeCoinsCard());
		}
		else if (releasedItem == "ButtonShop")
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
			((MonoBehaviour)this).StartCoroutine(ShopManager.OpenShopCard());
		}
		else if (releasedItem == "ButtonExit")
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
			Application.Quit();
		}
	}

	private IEnumerator otvoriSledeciNivo()
	{
		yield return (object)new WaitForSeconds(0.25f);
		Application.LoadLevel("Worlds");
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
}
