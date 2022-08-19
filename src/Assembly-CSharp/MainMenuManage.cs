using System;
using System.Collections;
using UnityEngine;

// Token: 0x020004BD RID: 1213
public class MainMenuManage : MonoBehaviour
{
	// Token: 0x06002658 RID: 9816 RVA: 0x0010A55C File Offset: 0x0010875C
	private void Awake()
	{
		this.holderLogo = GameObject.Find("HolderLogoGlavni");
		this.majmunLogo = GameObject.Find("HolderMajmun");
		this.dugmeMuzika = GameObject.Find("MusicMain");
		this.dugmeSound = GameObject.Find("SoundMain");
		this.dugmeMuzikaSprite = this.dugmeMuzika.GetComponent<SpriteRenderer>().sprite;
		this.dugmeSoundSprite = this.dugmeSound.GetComponent<SpriteRenderer>().sprite;
		this.dugmeMuzikaOffSprite = GameObject.Find("MusicOffMain").GetComponent<SpriteRenderer>().sprite;
		this.dugmeSoundOffSprite = GameObject.Find("SoundOff").GetComponent<SpriteRenderer>().sprite;
		GameObject.Find("LifeManager").transform.position = new Vector3(50f, 50f, 0f);
		this.dock_goreDesno = GameObject.Find("Dock_GoreDesno").transform;
		this.dock_doleDesno = GameObject.Find("Dock_DoleDesno").transform;
		this.dock_goreLevo = GameObject.Find("Dock_GoreLevo").transform;
		this.dock_doleLevo = GameObject.Find("Dock_DoleLevo").transform;
		this.dock_doleDesno.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.one).x, Camera.main.ViewportToWorldPoint(Vector3.zero).y, this.holderLogo.transform.position.z);
		this.dock_doleLevo.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.zero).x, Camera.main.ViewportToWorldPoint(Vector3.zero).y, this.holderLogo.transform.position.z);
		this.dock_goreDesno.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.one).x, Camera.main.ViewportToWorldPoint(Vector3.one).y, this.holderLogo.transform.position.z);
		this.dock_goreLevo.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.zero).x, Camera.main.ViewportToWorldPoint(Vector3.one).y, this.holderLogo.transform.position.z);
	}

	// Token: 0x06002659 RID: 9817 RVA: 0x0010A7BC File Offset: 0x001089BC
	private void Start()
	{
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

	// Token: 0x0600265A RID: 9818 RVA: 0x0010A8A0 File Offset: 0x00108AA0
	private void Update()
	{
		if (Input.GetKeyUp(27))
		{
			bool otvorenShop = ShopManager.otvorenShop;
		}
		if (Input.GetMouseButtonDown(0))
		{
			this.clickedItem = this.RaycastFunction(Input.mousePosition);
			if (this.clickedItem.Equals("PlayMain") || this.clickedItem.Equals("PlayMainFly") || this.clickedItem.Equals("ButtonFreeCoins") || this.clickedItem.Equals("ButtonShop") || this.clickedItem.Equals("ButtonExit"))
			{
				GameObject gameObject = GameObject.Find(this.clickedItem);
				this.originalScale = gameObject.transform.localScale;
				gameObject.transform.localScale = this.originalScale * 0.8f;
			}
			else if (this.clickedItem != string.Empty)
			{
				GameObject gameObject2 = GameObject.Find(this.clickedItem);
				this.originalScale = gameObject2.transform.localScale;
			}
		}
		if (Input.GetMouseButtonUp(0))
		{
			this.releasedItem = this.RaycastFunction(Input.mousePosition);
			if (!this.clickedItem.Equals(string.Empty))
			{
				GameObject.Find(this.clickedItem).transform.localScale = this.originalScale;
				if (this.releasedItem == "MusicMain")
				{
					if (!PlaySounds.musicOn)
					{
						PlaySounds.musicOn = true;
						this.muzikaOff = false;
						this.dugmeMuzika.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
						if (PlayerPrefs.HasKey("soundOn") && PlayerPrefs.GetInt("soundOn") == 1)
						{
							PlaySounds.Play_Button_MusicOn();
						}
						PlaySounds.Play_BackgroundMusic_Menu();
						PlayerPrefs.SetInt("musicOn", 1);
						PlayerPrefs.Save();
						return;
					}
					PlaySounds.musicOn = false;
					this.muzikaOff = true;
					this.dugmeMuzika.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
					PlaySounds.Stop_BackgroundMusic_Menu();
					PlayerPrefs.SetInt("musicOn", 0);
					PlayerPrefs.Save();
					return;
				}
				else if (this.releasedItem == "SoundMain")
				{
					if (!PlaySounds.soundOn)
					{
						PlaySounds.soundOn = true;
						this.soundOff = false;
						this.dugmeSound.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
						PlaySounds.Play_Button_SoundOn();
						PlayerPrefs.SetInt("soundOn", 1);
						PlayerPrefs.Save();
						return;
					}
					PlaySounds.soundOn = false;
					this.soundOff = true;
					this.dugmeSound.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
					PlayerPrefs.SetInt("soundOn", 0);
					PlayerPrefs.Save();
					return;
				}
				else if (this.releasedItem == "PlayMainFly")
				{
					GameObject.Find(this.releasedItem).GetComponent<Collider>().enabled = false;
					if (PlayerPrefs.HasKey("soundOn") && PlayerPrefs.GetInt("soundOn") == 1)
					{
						PlaySounds.Play_Button_Play();
					}
					if (!PlayerPrefs.HasKey("OdgledaoTutorial"))
					{
						Application.LoadLevel("LoadingScene");
						return;
					}
					Application.LoadLevel(3);
					return;
				}
				else
				{
					if (this.releasedItem == "ButtonFreeCoins")
					{
						if (PlaySounds.soundOn)
						{
							PlaySounds.Play_Button_OpenLevel();
						}
						base.StartCoroutine(ShopManager.OpenFreeCoinsCard());
						return;
					}
					if (this.releasedItem == "ButtonShop")
					{
						if (PlaySounds.soundOn)
						{
							PlaySounds.Play_Button_OpenLevel();
						}
						base.StartCoroutine(ShopManager.OpenShopCard());
						return;
					}
					if (this.releasedItem == "ButtonExit")
					{
						if (PlaySounds.soundOn)
						{
							PlaySounds.Play_Button_OpenLevel();
						}
						Application.Quit();
					}
				}
			}
		}
	}

	// Token: 0x0600265B RID: 9819 RVA: 0x0010AC29 File Offset: 0x00108E29
	private IEnumerator otvoriSledeciNivo()
	{
		yield return new WaitForSeconds(0.25f);
		Application.LoadLevel("Worlds");
		yield break;
	}

	// Token: 0x0600265C RID: 9820 RVA: 0x0010AC34 File Offset: 0x00108E34
	private string RaycastFunction(Vector3 vector)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(vector), ref raycastHit))
		{
			return raycastHit.collider.name;
		}
		return "";
	}

	// Token: 0x04001F96 RID: 8086
	private Sprite dugmeMuzikaSprite;

	// Token: 0x04001F97 RID: 8087
	private Sprite dugmeSoundSprite;

	// Token: 0x04001F98 RID: 8088
	private Sprite dugmeMuzikaOffSprite;

	// Token: 0x04001F99 RID: 8089
	private Sprite dugmeSoundOffSprite;

	// Token: 0x04001F9A RID: 8090
	private GameObject dugmeMuzika;

	// Token: 0x04001F9B RID: 8091
	private GameObject dugmeSound;

	// Token: 0x04001F9C RID: 8092
	private GameObject dugmePlay;

	// Token: 0x04001F9D RID: 8093
	private GameObject holderLogo;

	// Token: 0x04001F9E RID: 8094
	private GameObject majmunLogo;

	// Token: 0x04001F9F RID: 8095
	private ParticleSystem bananaRasipuje;

	// Token: 0x04001FA0 RID: 8096
	private bool muzikaOff;

	// Token: 0x04001FA1 RID: 8097
	private bool soundOff;

	// Token: 0x04001FA2 RID: 8098
	private AudioSource MusicOn_Button;

	// Token: 0x04001FA3 RID: 8099
	private AudioSource SoundOn_Button;

	// Token: 0x04001FA4 RID: 8100
	private AudioSource Play_Button;

	// Token: 0x04001FA5 RID: 8101
	private string clickedItem;

	// Token: 0x04001FA6 RID: 8102
	private string releasedItem;

	// Token: 0x04001FA7 RID: 8103
	private Vector3 originalScale;

	// Token: 0x04001FA8 RID: 8104
	private Transform dock_goreDesno;

	// Token: 0x04001FA9 RID: 8105
	private Transform dock_doleDesno;

	// Token: 0x04001FAA RID: 8106
	private Transform dock_goreLevo;

	// Token: 0x04001FAB RID: 8107
	private Transform dock_doleLevo;
}
