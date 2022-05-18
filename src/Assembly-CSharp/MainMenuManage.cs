using System;
using System.Collections;
using UnityEngine;

// Token: 0x020006BF RID: 1727
public class MainMenuManage : MonoBehaviour
{
	// Token: 0x06002B2A RID: 11050 RVA: 0x0014E8E0 File Offset: 0x0014CAE0
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

	// Token: 0x06002B2B RID: 11051 RVA: 0x0014EB40 File Offset: 0x0014CD40
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

	// Token: 0x06002B2C RID: 11052 RVA: 0x0014EC24 File Offset: 0x0014CE24
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

	// Token: 0x06002B2D RID: 11053 RVA: 0x00021506 File Offset: 0x0001F706
	private IEnumerator otvoriSledeciNivo()
	{
		yield return new WaitForSeconds(0.25f);
		Application.LoadLevel("Worlds");
		yield break;
	}

	// Token: 0x06002B2E RID: 11054 RVA: 0x00149A14 File Offset: 0x00147C14
	private string RaycastFunction(Vector3 vector)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(vector), ref raycastHit))
		{
			return raycastHit.collider.name;
		}
		return "";
	}

	// Token: 0x0400254D RID: 9549
	private Sprite dugmeMuzikaSprite;

	// Token: 0x0400254E RID: 9550
	private Sprite dugmeSoundSprite;

	// Token: 0x0400254F RID: 9551
	private Sprite dugmeMuzikaOffSprite;

	// Token: 0x04002550 RID: 9552
	private Sprite dugmeSoundOffSprite;

	// Token: 0x04002551 RID: 9553
	private GameObject dugmeMuzika;

	// Token: 0x04002552 RID: 9554
	private GameObject dugmeSound;

	// Token: 0x04002553 RID: 9555
	private GameObject dugmePlay;

	// Token: 0x04002554 RID: 9556
	private GameObject holderLogo;

	// Token: 0x04002555 RID: 9557
	private GameObject majmunLogo;

	// Token: 0x04002556 RID: 9558
	private ParticleSystem bananaRasipuje;

	// Token: 0x04002557 RID: 9559
	private bool muzikaOff;

	// Token: 0x04002558 RID: 9560
	private bool soundOff;

	// Token: 0x04002559 RID: 9561
	private AudioSource MusicOn_Button;

	// Token: 0x0400255A RID: 9562
	private AudioSource SoundOn_Button;

	// Token: 0x0400255B RID: 9563
	private AudioSource Play_Button;

	// Token: 0x0400255C RID: 9564
	private string clickedItem;

	// Token: 0x0400255D RID: 9565
	private string releasedItem;

	// Token: 0x0400255E RID: 9566
	private Vector3 originalScale;

	// Token: 0x0400255F RID: 9567
	private Transform dock_goreDesno;

	// Token: 0x04002560 RID: 9568
	private Transform dock_doleDesno;

	// Token: 0x04002561 RID: 9569
	private Transform dock_goreLevo;

	// Token: 0x04002562 RID: 9570
	private Transform dock_doleLevo;
}
