using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

// Token: 0x02000737 RID: 1847
public class KameraMovement : MonoBehaviour
{
	// Token: 0x06002ED7 RID: 11991 RVA: 0x00174DD4 File Offset: 0x00172FD4
	private void Awake()
	{
		if (Advertisement.isSupported)
		{
			Advertisement.Initialize(StagesParser.Instance.UnityAdsVideoGameID);
		}
		else
		{
			Debug.Log("UNITYADS Platform not supported");
		}
		KameraMovement.makniPopup = 0;
		string[] array = PlayerPrefs.GetString("WatchVideoWorld" + (StagesParser.currSetIndex + 1)).Split(new char[]
		{
			'#'
		});
		this._GUI = GameObject.Find("_GUI").transform;
		if (this.Televizori != null)
		{
			for (int i = 0; i < array.Length; i++)
			{
				for (int j = 0; j < this.Televizori.Length; j++)
				{
					if (array[i] == this.Televizori[j].name.Substring(this.Televizori[j].name.Length - 1))
					{
						this.Televizori[j].gameObject.SetActive(false);
					}
				}
			}
		}
		this.angles = new Vector3[]
		{
			new Vector3(18f, 102f, 336f),
			new Vector3(48f, 154f, 358f),
			new Vector3(30f, 232f, 25f),
			new Vector3(12f, 258f, 31f),
			new Vector3(350f, 293f, 37f),
			new Vector3(349f, 3f, 5f),
			new Vector3(344f, 45f, 337f),
			new Vector3(5f, 91f, 348f)
		};
		this.ortSize = Camera.main.orthographicSize;
		this.aspect = Camera.main.aspect;
		this.Kamera = GameObject.Find("Main Camera");
		this.guiCamera = GameObject.Find("GUICamera").GetComponent<Camera>();
		this.holderMajmun = GameObject.Find("HolderMajmun").transform;
		this.majmun = this.holderMajmun.GetChild(0).GetChild(0);
		this.animator = this.majmun.GetComponent<Animator>();
		this.guiCameraY = this.guiCamera.transform.position.y;
		this.levaGranica = this.doleLevo.position.x + this.ortSize * this.aspect;
		this.desnaGranica = this.doleDesno.position.x - this.ortSize * this.aspect;
		this.donjaGranica = this.doleLevo.position.y + this.ortSize;
		this.gornjaGranica = this.goreDesno.position.y - this.ortSize;
		if (StagesParser.otvaraoShopNekad == 2 && StagesParser.trenutniNivoNaOstrvu[StagesParser.currSetIndex] == 3 && StagesParser.currSetIndex == 0)
		{
			KameraMovement.makniPopup = 6;
			base.StartCoroutine(this.PokaziMuCustomize());
		}
		this.InitLevels(false);
		Camera.main.transform.position = new Vector3(Mathf.Clamp(GameObject.Find("Level" + StagesParser.trenutniNivoNaOstrvu[StagesParser.currSetIndex].ToString()).transform.position.x, this.levaGranica, this.desnaGranica), Mathf.Clamp(GameObject.Find("Level" + StagesParser.trenutniNivoNaOstrvu[StagesParser.currSetIndex].ToString()).transform.position.y, this.donjaGranica, this.gornjaGranica), Camera.main.transform.position.z);
		if (StagesParser.pozicijaMajmuncetaNaMapi == Vector3.zero)
		{
			if (StagesParser.SetsInGame[StagesParser.currSetIndex].CurrentStarsInStageNEW > 0)
			{
				this.holderMajmun.position = this.izmedjneTacke.Find(StagesParser.trenutniNivoNaOstrvu[StagesParser.currSetIndex].ToString()).position;
				this.monkeyCurrentLevelIndex = this.GetMapLevelIndex(StagesParser.trenutniNivoNaOstrvu[StagesParser.currSetIndex]);
			}
		}
		else
		{
			this.holderMajmun.position = StagesParser.pozicijaMajmuncetaNaMapi;
			this.kovcegNaPocetku = (this.trenutniKovceg = this.pronadjiKovceg(StagesParser.bonusName));
			string[] array2 = StagesParser.bonusName.Split(new char[]
			{
				'_'
			});
			this.monkeyCurrentLevelIndex = this.GetMapLevelIndex(int.Parse(array2[2]));
			Camera.main.transform.position = new Vector3(Mathf.Clamp(this.holderMajmun.position.x, this.levaGranica, this.desnaGranica), Mathf.Clamp(this.holderMajmun.position.y, this.donjaGranica, this.gornjaGranica), Camera.main.transform.position.z);
			if (!StagesParser.dodatnaProveraIzasaoIzBonusa)
			{
				this.kovcegNaPocetku.Find("Kovceg Zatvoren").GetComponent<Animator>().Play("Kovceg Otvaranje");
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_Otvaranje_Kovcega();
				}
				this.kovcegNaPocetku.GetComponent<Collider>().enabled = false;
				this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/PomocniColliderKodOtvaranjaKovcega").localScale = new Vector3(200f, 130f, 1f);
				this.PodesiReward();
			}
			else
			{
				StagesParser.dodatnaProveraIzasaoIzBonusa = false;
			}
		}
		if (PlaySounds.musicOn && !PlaySounds.BackgroundMusic_Menu.isPlaying)
		{
			PlaySounds.Play_BackgroundMusic_Menu();
		}
	}

	// Token: 0x06002ED8 RID: 11992 RVA: 0x00175364 File Offset: 0x00173564
	private void PodesiReward()
	{
		if (StagesParser.bonusID == 4)
		{
			this.reward1Type = 4;
			this.reward2Type = 0;
			this.reward3Type = 0;
			this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward2").gameObject.SetActive(false);
			this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward3").gameObject.SetActive(false);
			this.IspitajItem();
		}
		else if (StagesParser.currSetIndex == 0)
		{
			this.reward2Type = 0;
			this.reward3Type = 0;
			this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward2").gameObject.SetActive(false);
			this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward3").gameObject.SetActive(false);
			this.kolicinaReward1 = 1;
			this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1/Count").GetComponent<TextMesh>().text = this.kolicinaReward1.ToString();
			if (StagesParser.powerup_magnets <= StagesParser.powerup_doublecoins)
			{
				if (StagesParser.powerup_magnets <= StagesParser.powerup_shields)
				{
					this.reward1Type = 1;
					this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1").GetComponent<SpriteRenderer>().sprite = this._GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Magnet/Plavi Bedz/Magnet Icon").GetComponent<SpriteRenderer>().sprite;
				}
				else
				{
					this.reward1Type = 3;
					this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1").GetComponent<SpriteRenderer>().sprite = this._GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Shield/Plavi Bedz/Shield Icon").GetComponent<SpriteRenderer>().sprite;
				}
			}
			else if (StagesParser.powerup_doublecoins <= StagesParser.powerup_shields)
			{
				this.reward1Type = 2;
				this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1").GetComponent<SpriteRenderer>().sprite = this._GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Double Coins/Plavi Bedz/Double Coins Icon").GetComponent<SpriteRenderer>().sprite;
			}
			else
			{
				this.reward1Type = 3;
				this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1").GetComponent<SpriteRenderer>().sprite = this._GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Shield/Plavi Bedz/Shield Icon").GetComponent<SpriteRenderer>().sprite;
			}
		}
		else if (StagesParser.currSetIndex < 3)
		{
			this.reward3Type = 0;
			this.kolicinaReward1 = 1;
			this.kolicinaReward2 = 1;
			this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1/Count").GetComponent<TextMesh>().text = this.kolicinaReward1.ToString();
			this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward2/Count").GetComponent<TextMesh>().text = this.kolicinaReward2.ToString();
			this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1").localPosition = new Vector3(-0.75f, 0.55f, -0.5f);
			this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward2").localPosition = new Vector3(0.75f, 0.55f, -0.5f);
			this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward3").gameObject.SetActive(false);
			if (StagesParser.powerup_magnets <= StagesParser.powerup_doublecoins)
			{
				if (StagesParser.powerup_shields <= StagesParser.powerup_doublecoins)
				{
					this.reward1Type = 1;
					this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1").GetComponent<SpriteRenderer>().sprite = this._GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Magnet/Plavi Bedz/Magnet Icon").GetComponent<SpriteRenderer>().sprite;
					this.reward2Type = 3;
					this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward2").GetComponent<SpriteRenderer>().sprite = this._GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Shield/Plavi Bedz/Shield Icon").GetComponent<SpriteRenderer>().sprite;
				}
				else
				{
					this.reward1Type = 1;
					this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1").GetComponent<SpriteRenderer>().sprite = this._GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Magnet/Plavi Bedz/Magnet Icon").GetComponent<SpriteRenderer>().sprite;
					this.reward2Type = 2;
					this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward2").GetComponent<SpriteRenderer>().sprite = this._GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Double Coins/Plavi Bedz/Double Coins Icon").GetComponent<SpriteRenderer>().sprite;
				}
			}
			else if (StagesParser.powerup_magnets <= StagesParser.powerup_shields)
			{
				this.reward1Type = 2;
				this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1").GetComponent<SpriteRenderer>().sprite = this._GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Double Coins/Plavi Bedz/Double Coins Icon").GetComponent<SpriteRenderer>().sprite;
				this.reward2Type = 1;
				this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward2").GetComponent<SpriteRenderer>().sprite = this._GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Magnet/Plavi Bedz/Magnet Icon").GetComponent<SpriteRenderer>().sprite;
			}
			else
			{
				this.reward1Type = 2;
				this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1").GetComponent<SpriteRenderer>().sprite = this._GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Double Coins/Plavi Bedz/Double Coins Icon").GetComponent<SpriteRenderer>().sprite;
				this.reward2Type = 3;
				this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward2").GetComponent<SpriteRenderer>().sprite = this._GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Shield/Plavi Bedz/Shield Icon").GetComponent<SpriteRenderer>().sprite;
			}
		}
		else
		{
			this.kolicinaReward1 = 1;
			this.kolicinaReward2 = 1;
			this.kolicinaReward3 = 1;
			this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1/Count").GetComponent<TextMesh>().text = this.kolicinaReward1.ToString();
			this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward2/Count").GetComponent<TextMesh>().text = this.kolicinaReward2.ToString();
			this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward3/Count").GetComponent<TextMesh>().text = this.kolicinaReward3.ToString();
			this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1").GetComponent<SpriteRenderer>().sprite = this._GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Magnet/Plavi Bedz/Magnet Icon").GetComponent<SpriteRenderer>().sprite;
			this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward2").GetComponent<SpriteRenderer>().sprite = this._GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Shield/Plavi Bedz/Shield Icon").GetComponent<SpriteRenderer>().sprite;
			this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward3").GetComponent<SpriteRenderer>().sprite = this._GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Double Coins/Plavi Bedz/Double Coins Icon").GetComponent<SpriteRenderer>().sprite;
			this.reward1Type = 1;
			this.reward2Type = 2;
			this.reward3Type = 3;
		}
		base.Invoke("PozoviRewardPopup", 4.15f);
	}

	// Token: 0x06002ED9 RID: 11993 RVA: 0x001759A0 File Offset: 0x00173BA0
	private void PozoviRewardPopup()
	{
		this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni").localPosition += new Vector3(0f, 35f, 0f);
		this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni").GetComponent<Animator>().Play("OpenPopup");
		KameraMovement.makniPopup = 7;
	}

	// Token: 0x06002EDA RID: 11994 RVA: 0x00175A08 File Offset: 0x00173C08
	private void RefreshScene()
	{
		StagesParser.lastUnlockedWorldIndex = 0;
		for (int i = 0; i < StagesParser.totalSets; i++)
		{
			StagesParser.unlockedWorlds[i] = false;
			if (StagesParser.currentStarsNEW >= StagesParser.SetsInGame[i].StarRequirement && i > 0 && int.Parse(StagesParser.allLevels[(i - 1) * 20 + 19].Split(new char[]
			{
				'#'
			})[1]) > 0)
			{
				StagesParser.unlockedWorlds[i] = true;
				StagesParser.lastUnlockedWorldIndex = i;
			}
		}
		StagesParser.unlockedWorlds[0] = true;
		if (StagesParser.lastUnlockedWorldIndex < Application.loadedLevel - 4)
		{
			if (StagesParser.pozicijaMajmuncetaNaMapi != Vector3.zero)
			{
				StagesParser.pozicijaMajmuncetaNaMapi = Vector3.zero;
			}
			StagesParser.worldToFocus = StagesParser.currSetIndex;
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_GoBack();
			}
			StagesParser.vratioSeNaSvaOstrva = true;
			base.StartCoroutine(this.UcitajOstrvo("All Maps"));
			return;
		}
		this.InitLevels(true);
		if (StagesParser.pozicijaMajmuncetaNaMapi == Vector3.zero)
		{
			if (StagesParser.SetsInGame[StagesParser.currSetIndex].CurrentStarsInStageNEW > 0)
			{
				this.holderMajmun.position = this.izmedjneTacke.Find(StagesParser.trenutniNivoNaOstrvu[StagesParser.currSetIndex].ToString()).position;
				this.monkeyCurrentLevelIndex = this.GetMapLevelIndex(StagesParser.trenutniNivoNaOstrvu[StagesParser.currSetIndex]);
			}
			else
			{
				this.holderMajmun.position = this.izmedjneTacke.Find("1").position;
				this.monkeyCurrentLevelIndex = this.GetMapLevelIndex(1);
			}
		}
		else
		{
			this.holderMajmun.position = StagesParser.pozicijaMajmuncetaNaMapi;
			this.kovcegNaPocetku = (this.trenutniKovceg = this.pronadjiKovceg(StagesParser.bonusName));
			string[] array = StagesParser.bonusName.Split(new char[]
			{
				'_'
			});
			this.monkeyCurrentLevelIndex = this.GetMapLevelIndex(int.Parse(array[2]));
			Camera.main.transform.position = new Vector3(Mathf.Clamp(this.holderMajmun.position.x, this.levaGranica, this.desnaGranica), Mathf.Clamp(this.holderMajmun.position.y, this.donjaGranica, this.gornjaGranica), Camera.main.transform.position.z);
			if (!StagesParser.dodatnaProveraIzasaoIzBonusa)
			{
				this.kovcegNaPocetku.Find("Kovceg Zatvoren").GetComponent<Animator>().Play("Kovceg Otvaranje");
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_Otvaranje_Kovcega();
				}
				this.kovcegNaPocetku.GetComponent<Collider>().enabled = false;
				this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/PomocniColliderKodOtvaranjaKovcega").localScale = new Vector3(200f, 130f, 1f);
				this.PodesiReward();
			}
			else
			{
				StagesParser.dodatnaProveraIzasaoIzBonusa = false;
			}
		}
		this._GUI.Find("INTERFACE HOLDER/_TopLeft/Coins/Coins Number").GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
		this._GUI.Find("INTERFACE HOLDER/_TopLeft/Coins/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		this._GUI.Find("INTERFACE HOLDER/_TopLeft/PTS/PTS Number").GetComponent<TextMesh>().text = StagesParser.currentPoints.ToString();
		this._GUI.Find("INTERFACE HOLDER/_TopLeft/PTS/PTS Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		this._GUI.Find("INTERFACE HOLDER/_TopLeft/BananaHolder/Bananas/Banana Number").GetComponent<TextMesh>().text = StagesParser.currentBananas.ToString();
		this._GUI.Find("INTERFACE HOLDER/_TopLeft/BananaHolder/Bananas/Banana Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		this._GUI.Find("INTERFACE HOLDER/FB Login/Text/Number").GetComponent<TextMesh>().text = "+" + StagesParser.LoginReward.ToString();
		this._GUI.Find("INTERFACE HOLDER/FB Login/Text/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		this._GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/WatchVideoReward").GetComponent<TextMesh>().text = "+" + this.televizorCenePoSvetovima[StagesParser.currSetIndex].ToString();
		this._GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/WatchVideoReward").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		if (this.reward1Type > 0)
		{
			this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1/Count").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		}
		if (this.reward2Type > 0)
		{
			this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward2/Count").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		}
		if (this.reward3Type > 0)
		{
			this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward3/Count").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		}
		this._GUI.Find("INTERFACE HOLDER/TotalStars/Stars Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		this.changeLanguage();
		CheckInternetConnection.Instance.refreshText();
		StagesParser.LoadingPoruke.Clear();
		StagesParser.RedniBrojSlike.Clear();
		StagesParser.Instance.UcitajLoadingPoruke();
	}

	// Token: 0x06002EDB RID: 11995 RVA: 0x00175EE8 File Offset: 0x001740E8
	private void Start()
	{
		this.changeLanguage();
		if (Loading.Instance != null)
		{
			base.StartCoroutine(Loading.Instance.UcitanaScena(this.guiCamera, 2, 0f));
		}
		else
		{
			this._GUI.Find("LOADING HOLDER NEW/Loading Animation Vrata").GetComponent<Animator>().Play("Loading Zidovi Odlazak");
		}
		this._GUI.Find("INTERFACE HOLDER/_TopLeft/Coins/Coins Number").GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
		this._GUI.Find("INTERFACE HOLDER/_TopLeft/Coins/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		this._GUI.Find("INTERFACE HOLDER/_TopLeft/PTS/PTS Number").GetComponent<TextMesh>().text = StagesParser.currentPoints.ToString();
		this._GUI.Find("INTERFACE HOLDER/_TopLeft/PTS/PTS Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		this._GUI.Find("INTERFACE HOLDER/_TopLeft/BananaHolder/Bananas/Banana Number").GetComponent<TextMesh>().text = StagesParser.currentBananas.ToString();
		this._GUI.Find("INTERFACE HOLDER/_TopLeft/BananaHolder/Bananas/Banana Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		this._GUI.Find("INTERFACE HOLDER/FB Login/Text/Number").GetComponent<TextMesh>().text = "+" + StagesParser.LoginReward.ToString();
		this._GUI.Find("INTERFACE HOLDER/FB Login/Text/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		this._GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/WatchVideoReward").GetComponent<TextMesh>().text = "+" + this.televizorCenePoSvetovima[StagesParser.currSetIndex].ToString();
		this._GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/WatchVideoReward").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		this._GUI.Find("INTERFACE HOLDER/_TopLeft").position = new Vector3(this.guiCamera.ViewportToWorldPoint(Vector3.zero).x, this._GUI.Find("INTERFACE HOLDER/_TopLeft").position.y, this._GUI.Find("INTERFACE HOLDER/_TopLeft").position.z);
		this._GUI.Find("INTERFACE HOLDER/FB Login").position = new Vector3(this.guiCamera.ViewportToWorldPoint(new Vector3(0.91f, 0f, 0f)).x, this._GUI.Find("INTERFACE HOLDER/FB Login").position.y, this._GUI.Find("INTERFACE HOLDER/FB Login").position.z);
		this._GUI.Find("INTERFACE HOLDER/TotalStars").position = new Vector3(this.guiCamera.ViewportToWorldPoint(new Vector3(0.89f, 0f, 0f)).x, this._GUI.Find("INTERFACE HOLDER/TotalStars").position.y, this._GUI.Find("INTERFACE HOLDER/TotalStars").position.z);
		this._GUI.Find("INTERFACE HOLDER/TotalStars/Stars Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		ShopManagerFull.ShopObject.transform.Find("1 Free Coins/Free Coins Tabovi/ShopFCWatchVideo/Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMesh>().text = StagesParser.watchVideoReward.ToString();
		ShopManagerFull.ShopObject.transform.Find("1 Free Coins/Free Coins Tabovi/ShopFCWatchVideo/Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, false, true);
		if (this.reward1Type > 0)
		{
			this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1/Count").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		}
		if (this.reward2Type > 0)
		{
			this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward2/Count").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		}
		if (this.reward3Type > 0)
		{
			this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward3/Count").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		}
		GameObject.Find("NotAvailableText").SetActive(false);
		if (FB.IsLoggedIn)
		{
			GameObject.Find("FB Login").SetActive(false);
		}
		if (StagesParser.obucenSeLogovaoNaDrugojSceni)
		{
			StagesParser.obucenSeLogovaoNaDrugojSceni = false;
			base.Invoke("MaliDelayPreNegoDaSePozoveCompareScoresShopDeo", 1f);
		}
	}

	// Token: 0x06002EDC RID: 11996 RVA: 0x00022BC6 File Offset: 0x00020DC6
	private void MaliDelayPreNegoDaSePozoveCompareScoresShopDeo()
	{
		StagesParser.Instance.ShopDeoIzCompareScores();
	}

	// Token: 0x06002EDD RID: 11997 RVA: 0x00022BD2 File Offset: 0x00020DD2
	private void UvecavajBrojac()
	{
		if (this.angleIndex > 7)
		{
			this.angleIndex = 0;
		}
		this.angleIndex++;
	}

	// Token: 0x06002EDE RID: 11998 RVA: 0x00176310 File Offset: 0x00174510
	private void InitLevels(bool refreshed)
	{
		StagesParser.SetsInGame[StagesParser.currSetIndex].CurrentStarsInStageNEW = 0;
		if (StagesParser.zadnjiOtkljucanNivo != 0)
		{
			GameObject.Find(string.Concat(new object[]
			{
				"LevelsWorld",
				StagesParser.currSetIndex,
				"/Level",
				StagesParser.zadnjiOtkljucanNivo,
				"/Level",
				StagesParser.zadnjiOtkljucanNivo,
				"Move"
			})).GetComponent<Animator>().Play("KatanacExplosion");
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_OtkljucavanjeNivoa();
			}
		}
		for (int i = 0; i < StagesParser.SetsInGame[StagesParser.currSetIndex].StagesOnSet; i++)
		{
			string[] array = StagesParser.allLevels[StagesParser.currSetIndex * 20 + i].Split(new char[]
			{
				'#'
			});
			StagesParser.SetsInGame[StagesParser.currSetIndex].SetStarOnStage(i, int.Parse(array[1]));
			if (int.Parse(array[1]) > -1)
			{
				StagesParser.SetsInGame[StagesParser.currSetIndex].CurrentStarsInStageNEW += int.Parse(array[1]);
			}
			this.currentLevelStars = int.Parse(array[1]);
			if (this.currentLevelStars == -1)
			{
				GameObject.Find(string.Concat(new object[]
				{
					"LevelsWorld",
					StagesParser.currSetIndex,
					"/Level",
					i + 1,
					"/Level",
					i + 1,
					"Move"
				})).GetComponent<SpriteRenderer>().sprite = GameObject.Find("RefCardClose").GetComponent<SpriteRenderer>().sprite;
				GameObject.Find(string.Concat(new object[]
				{
					"LevelsWorld",
					StagesParser.currSetIndex,
					"/Level",
					i + 1,
					"/Level",
					i + 1,
					"Move"
				})).transform.Find("TextNumberLevel").GetComponent<TextMesh>().text = string.Empty;
				GameObject.Find(string.Concat(new object[]
				{
					"LevelsWorld",
					StagesParser.currSetIndex,
					"/Level",
					i + 1,
					"/Level",
					i + 1,
					"Move"
				})).transform.Find("TextNumberLevel").GetChild(0).GetComponent<TextMesh>().text = string.Empty;
				GameObject.Find(string.Concat(new object[]
				{
					"LevelsWorld",
					StagesParser.currSetIndex,
					"/Level",
					i + 1,
					"/Level",
					i + 1,
					"Move"
				})).transform.Find("Katanac").GetComponent<Renderer>().enabled = true;
				if (!GameObject.Find(string.Concat(new object[]
				{
					"LevelsWorld",
					StagesParser.currSetIndex,
					"/Level",
					i + 1,
					"/Level",
					i + 1,
					"Move"
				})).transform.Find("Katanac").gameObject.activeSelf)
				{
					GameObject.Find(string.Concat(new object[]
					{
						"LevelsWorld",
						StagesParser.currSetIndex,
						"/Level",
						i + 1,
						"/Level",
						i + 1,
						"Move"
					})).GetComponent<Animator>().enabled = false;
					GameObject.Find(string.Concat(new object[]
					{
						"LevelsWorld",
						StagesParser.currSetIndex,
						"/Level",
						i + 1,
						"/Level",
						i + 1,
						"Move"
					})).transform.Find("Katanac").gameObject.SetActive(true);
				}
			}
			else
			{
				if (i + 1 != StagesParser.zadnjiOtkljucanNivo || StagesParser.pozicijaMajmuncetaNaMapi != Vector3.zero)
				{
					GameObject.Find(string.Concat(new object[]
					{
						"LevelsWorld",
						StagesParser.currSetIndex,
						"/Level",
						i + 1,
						"/Level",
						i + 1,
						"Move"
					})).GetComponent<SpriteRenderer>().sprite = GameObject.Find("RefCardStar" + this.currentLevelStars).GetComponent<SpriteRenderer>().sprite;
					GameObject.Find(string.Concat(new object[]
					{
						"LevelsWorld",
						StagesParser.currSetIndex,
						"/Level",
						i + 1,
						"/Level",
						i + 1,
						"Move"
					})).transform.Find("TextNumberLevel").GetComponent<TextMesh>().text = (i + 1).ToString();
					GameObject.Find(string.Concat(new object[]
					{
						"LevelsWorld",
						StagesParser.currSetIndex,
						"/Level",
						i + 1,
						"/Level",
						i + 1,
						"Move"
					})).transform.Find("Katanac").GetComponent<Renderer>().enabled = false;
					if (this.currentLevelStars == 0)
					{
						GameObject.Find(string.Concat(new object[]
						{
							"LevelsWorld",
							StagesParser.currSetIndex,
							"/Level",
							i + 1,
							"/Level",
							i + 1,
							"Move"
						})).GetComponent<Animator>().Play("NewLevelLoop");
					}
				}
				this.zadnjiOtkljucanNivo_proveraZaBonus = i + 1;
			}
		}
		this._GUI.Find("INTERFACE HOLDER/TotalStars/Stars Number").GetComponent<TextMesh>().text = StagesParser.SetsInGame[StagesParser.currSetIndex].CurrentStarsInStageNEW + "/" + StagesParser.SetsInGame[StagesParser.currSetIndex].StagesOnSet * 3;
		string[] array2 = StagesParser.bonusLevels.Split(new char[]
		{
			'_'
		})[StagesParser.currSetIndex].Split(new char[]
		{
			'#'
		});
		for (int j = 0; j < this.BonusNivoi.Length; j++)
		{
			if (int.Parse(array2[j]) > -1)
			{
				this.BonusNivoi[j].Find("GateClosed").GetComponent<Renderer>().enabled = false;
				this.BonusNivoi[j].Find("GateOpen").GetComponent<Renderer>().enabled = true;
				if (int.Parse(array2[j]) > 0)
				{
					Transform transform = this.BonusNivoi[j].Find("Kovceg Zatvoren");
					transform.Find("Kovceg Otvoren").GetComponent<Renderer>().enabled = true;
					transform.Find("Kovceg Zatvoren").GetComponent<Renderer>().enabled = false;
					transform.GetComponent<Animator>().Play("Kovceg  Otvoren Idle");
					transform.parent.GetComponent<Collider>().enabled = false;
				}
			}
		}
		StagesParser.zadnjiOtkljucanNivo = 0;
	}

	// Token: 0x06002EDF RID: 11999 RVA: 0x00176A98 File Offset: 0x00174C98
	private void Update()
	{
		if (Input.GetKeyUp(27))
		{
			if (KameraMovement.makniPopup == 1)
			{
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_Button_GoBack();
				}
				this.popupZaSpustanje = this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni");
				base.Invoke("spustiPopup", 0.5f);
				this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni").GetComponent<Animator>().Play("ClosePopup");
				KameraMovement.makniPopup = 0;
				this.ocistiMisije();
				this.prejasiTelevizor = false;
			}
			else if (KameraMovement.makniPopup == 2)
			{
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_Button_GoBack();
				}
				this.popupZaSpustanje = this._GUI.Find("UNLOCK HOLDER/AnimationHolderGlavni");
				base.Invoke("spustiPopup", 0.5f);
				this._GUI.Find("UNLOCK HOLDER/AnimationHolderGlavni").GetComponent<Animator>().Play("ClosePopup");
				KameraMovement.makniPopup = 0;
			}
			else if (KameraMovement.makniPopup == 3)
			{
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_Button_GoBack();
				}
			}
			else if (KameraMovement.makniPopup == 4)
			{
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_Button_GoBack();
				}
				this.popupZaSpustanje = this._GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni");
				base.Invoke("spustiPopup", 0.5f);
				this._GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni").GetComponent<Animator>().Play("ClosePopup");
				KameraMovement.makniPopup = 0;
			}
			else if (KameraMovement.makniPopup == 5)
			{
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_Button_GoBack();
				}
				this._GUI.Find("INTERFACE HOLDER/_TopLeft/Coins/Coins Number").GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
				this._GUI.Find("INTERFACE HOLDER/_TopLeft/Coins/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
				this._GUI.Find("INTERFACE HOLDER/_TopLeft/BananaHolder/Bananas/Banana Number").GetComponent<TextMesh>().text = StagesParser.currentBananas.ToString();
				this._GUI.Find("INTERFACE HOLDER/_TopLeft/BananaHolder/Bananas/Banana Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
				ShopManagerFull.ShopObject.SkloniShop();
				KameraMovement.makniPopup = 0;
			}
			else if (KameraMovement.makniPopup == 7)
			{
				this.DodeliNagrade();
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_Button_GoBack();
				}
				this.popupZaSpustanje = this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni");
				base.Invoke("spustiPopup", 0.5f);
				this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni").GetComponent<Animator>().Play("ClosePopup");
				this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/PomocniColliderKodOtvaranjaKovcega").localScale = Vector3.zero;
				KameraMovement.makniPopup = 0;
			}
			else if (KameraMovement.makniPopup == 0)
			{
				if (StagesParser.pozicijaMajmuncetaNaMapi != Vector3.zero)
				{
					StagesParser.pozicijaMajmuncetaNaMapi = Vector3.zero;
				}
				StagesParser.worldToFocus = StagesParser.currSetIndex;
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_Button_GoBack();
				}
				StagesParser.vratioSeNaSvaOstrva = true;
				base.StartCoroutine(this.UcitajOstrvo("All Maps"));
			}
			else if (KameraMovement.makniPopup == 8)
			{
				KameraMovement.makniPopup = 0;
				base.StartCoroutine(CheckInternetConnection.Instance.ClosePopup());
			}
			else if (KameraMovement.makniPopup == 9)
			{
				KameraMovement.makniPopup = 4;
				base.StartCoroutine(CheckInternetConnection.Instance.ClosePopup());
			}
		}
		if (this.angleIndex >= 0)
		{
			int num = this.angleIndex;
		}
		if (Input.touchCount == 2)
		{
			this.zoom = true;
		}
		if (Input.touchCount == 2 && Input.GetTouch(0).phase == 1 && Input.GetTouch(1).phase == 1 && KameraMovement.makniPopup == 0)
		{
			this.zoom = true;
			this.pomeriKameruUGranice = false;
			this.curDist = Input.GetTouch(0).position - Input.GetTouch(1).position;
			this.prevDist = Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition - (Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition);
			this.touchDelta = this.curDist.magnitude - this.prevDist.magnitude;
			float num2 = 0.07f * (this.prevDist.magnitude - this.curDist.magnitude);
			this.speedTouch0 = Input.GetTouch(0).deltaPosition.magnitude / Input.GetTouch(0).deltaTime;
			this.speedTouch1 = Input.GetTouch(1).deltaPosition.magnitude / Input.GetTouch(1).deltaTime;
			if (this.touchDelta - this.varianceInDistances <= -10f && (this.speedTouch0 > this.minPinchSpeed || this.speedTouch1 > this.minPinchSpeed))
			{
				Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, 11f, num2 / 2f);
			}
			if (this.touchDelta + this.varianceInDistances > 10f && (this.speedTouch0 > this.minPinchSpeed || this.speedTouch1 > this.minPinchSpeed))
			{
				Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, 4f, -num2 / 2f);
			}
			Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 4f, 11f);
		}
		if (Input.touchCount == 0 && this.zoom)
		{
			this.ivicaEkrana = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
			if (this.doleLevo.position.x > this.ivicaEkrana.x)
			{
				this.pomeriKameruUGranice = true;
				Vector3 rez;
				rez..ctor(this.doleLevo.position.x - this.ivicaEkrana.x, 0f, 0f);
				if (this.doleLevo.position.y > this.ivicaEkrana.y)
				{
					rez = this.doleLevo.position - this.ivicaEkrana;
				}
				this.ivicaEkrana = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, 0f));
				if (this.goreLevo.position.y < this.ivicaEkrana.y)
				{
					rez = this.goreLevo.position - this.ivicaEkrana;
				}
				base.StartCoroutine(this.PostaviKameruUGranice(rez));
			}
			this.ivicaEkrana = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 0f));
			if (this.doleDesno.position.x < this.ivicaEkrana.x)
			{
				this.pomeriKameruUGranice = true;
				Vector3 rez2;
				rez2..ctor(this.doleDesno.position.x - this.ivicaEkrana.x, 0f, 0f);
				if (this.doleDesno.position.y > this.ivicaEkrana.y)
				{
					rez2 = this.doleDesno.position - this.ivicaEkrana;
				}
				this.ivicaEkrana = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 0f));
				if (this.goreDesno.position.y < this.ivicaEkrana.y)
				{
					rez2 = this.goreDesno.position - this.ivicaEkrana;
				}
				base.StartCoroutine(this.PostaviKameruUGranice(rez2));
			}
			this.zoom = false;
			this.ortSize = Camera.main.orthographicSize;
			this.aspect = Camera.main.aspect;
			this.levaGranica = this.doleLevo.position.x + this.ortSize * this.aspect;
			this.desnaGranica = this.doleDesno.position.x - this.ortSize * this.aspect;
			this.donjaGranica = this.doleLevo.position.y + this.ortSize;
			this.gornjaGranica = this.goreDesno.position.y - this.ortSize;
		}
		if (!this.zoom)
		{
			if (Input.GetMouseButtonDown(0))
			{
				this.pomeriKameruUGranice = false;
				if (this.released)
				{
					this.released = false;
				}
				this.clickedItem = this.RaycastFunction(Input.mousePosition);
				this.trajanjeKlika = Time.time;
				this.pomerajOdKlika_X = (this.startX = Input.mousePosition.x);
				this.pomerajOdKlika_Y = (this.startY = Input.mousePosition.y);
				if (this.clickedItem.Equals("ClearAll"))
				{
					ShopManagerFull.ShopObject.transform.Find("3 Customize/Costumization BG/ClearAll/ClearAll_Selected").GetComponent<SpriteRenderer>().enabled = true;
				}
			}
			if (Input.GetMouseButton(0) && KameraMovement.makniPopup == 0)
			{
				this.endX = Input.mousePosition.x;
				this.endY = Input.mousePosition.y;
				this.pomerajX = Camera.main.orthographicSize * (this.endX - this.startX) / 350f;
				this.pomerajY = Camera.main.orthographicSize * (this.endY - this.startY) / 350f;
				if (this.pomerajX != 0f || this.pomerajY != 0f)
				{
					this.moved = true;
				}
				Camera.main.transform.position = new Vector3(Mathf.Clamp(Camera.main.transform.position.x - this.pomerajX, this.levaGranica, this.desnaGranica), Mathf.Clamp(Camera.main.transform.position.y - this.pomerajY, this.donjaGranica, this.gornjaGranica), Camera.main.transform.position.z);
				this.startX = this.endX;
				this.startY = this.endY;
			}
			if (this.released && Mathf.Abs(this.pomerajX) > 0.0001f)
			{
				if (Camera.main.transform.position.x <= this.levaGranica + 0.25f)
				{
					if (this.bounce)
					{
						this.pomerajX = -0.04f;
						this.bounce = false;
					}
				}
				else if (Camera.main.transform.position.x >= this.desnaGranica - 0.25f)
				{
					if (this.bounce)
					{
						this.pomerajX = 0.04f;
						this.bounce = false;
					}
				}
				else if (Camera.main.transform.position.y <= this.donjaGranica + 0.25f)
				{
					if (this.bounce)
					{
						this.pomerajY = -0.04f;
						this.bounce = false;
					}
				}
				else if (Camera.main.transform.position.y >= this.gornjaGranica - 0.25f && this.bounce)
				{
					this.pomerajY = 0.04f;
					this.bounce = false;
				}
				Camera.main.transform.Translate(-this.pomerajX, -this.pomerajY, 0f);
				this.pomerajX *= 0.92f;
				this.pomerajY *= 0.92f;
				Camera.main.transform.position = new Vector3(Mathf.Clamp(Camera.main.transform.position.x, this.levaGranica, this.desnaGranica), Mathf.Clamp(Camera.main.transform.position.y, this.donjaGranica, this.gornjaGranica), Camera.main.transform.position.z);
			}
		}
		if (Input.GetMouseButtonUp(0))
		{
			if (this.moved)
			{
				this.moved = false;
				this.released = true;
				this.bounce = true;
			}
			this.releasedItem = this.RaycastFunction(Input.mousePosition);
			this.startX = (this.endX = 0f);
			this.startY = (this.endY = 0f);
			if (ShopManagerFull.ShopObject.transform.Find("3 Customize/Costumization BG/ClearAll/ClearAll_Selected").GetComponent<SpriteRenderer>().enabled)
			{
				ShopManagerFull.ShopObject.transform.Find("3 Customize/Costumization BG/ClearAll/ClearAll_Selected").GetComponent<SpriteRenderer>().enabled = false;
			}
			if (this.clickedItem == this.releasedItem && Time.time - this.trajanjeKlika < 0.35f && Mathf.Abs(Input.mousePosition.x - this.pomerajOdKlika_X) < 80f && Mathf.Abs(Input.mousePosition.y - this.pomerajOdKlika_Y) < 80f)
			{
				if (this.releasedItem.StartsWith("Level"))
				{
					if (int.TryParse(this.releasedItem.Substring(5), out this.levelName) && StagesParser.StarsPoNivoima[StagesParser.currSetIndex * 20 + this.levelName - 1] != -1)
					{
						if (PlaySounds.soundOn)
						{
							PlaySounds.Play_Button_OpenLevel();
						}
						this.monkeyDestinationLevelIndex = this.GetMapLevelIndex(this.levelName);
						if (this.majmunceSeMrda)
						{
							base.StopCoroutine("KretanjeMajmunceta");
						}
						if ((this.monkeyCurrentLevelIndex != this.monkeyDestinationLevelIndex || StagesParser.pozicijaMajmuncetaNaMapi != Vector3.zero) && KameraMovement.makniPopup == 0)
						{
							if (this.kretanjeDoKovcega)
							{
								this.kretanjeDoKovcega = false;
							}
							this.animator.Play("Running");
							base.StartCoroutine("KretanjeMajmunceta");
							return;
						}
						if (KameraMovement.makniPopup == 0)
						{
							StagesParser.currStageIndex = this.levelName - 1;
							StagesParser.currentLevel = StagesParser.currSetIndex * 20 + this.levelName;
							StagesParser.nivoZaUcitavanje = 10 + StagesParser.currSetIndex;
							MissionManager.OdrediMisiju(StagesParser.currentLevel - 1, true);
							if (!FB.IsLoggedIn)
							{
								if (this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN").gameObject.activeSelf)
								{
									this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN").gameObject.SetActive(false);
								}
							}
							else
							{
								if (!this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN").gameObject.activeSelf)
								{
									this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN").gameObject.SetActive(true);
								}
								this.getFriendsScoresOnLevel(StagesParser.currentLevel);
							}
							this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni").localPosition += new Vector3(0f, 35f, 0f);
							this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni").GetComponent<Animator>().Play("OpenPopup");
							KameraMovement.makniPopup = 1;
							return;
						}
					}
				}
				else
				{
					if (this.releasedItem == "HouseShop")
					{
						if (PlaySounds.soundOn)
						{
							PlaySounds.Play_Button_GoBack();
						}
						base.StartCoroutine(ShopManager.OpenShopCard());
						return;
					}
					if (this.releasedItem == "HolderShipFreeCoins")
					{
						if (PlaySounds.soundOn)
						{
							PlaySounds.Play_Button_GoBack();
						}
						base.StartCoroutine(ShopManager.OpenFreeCoinsCard());
						return;
					}
					if (this.releasedItem == "ButtonBackToWorlds")
					{
						if (StagesParser.pozicijaMajmuncetaNaMapi != Vector3.zero)
						{
							StagesParser.pozicijaMajmuncetaNaMapi = Vector3.zero;
						}
						StagesParser.worldToFocus = StagesParser.currSetIndex;
						if (PlaySounds.soundOn)
						{
							PlaySounds.Play_Button_GoBack();
						}
						StagesParser.vratioSeNaSvaOstrva = true;
						base.StartCoroutine(this.UcitajOstrvo("All Maps"));
						return;
					}
					if (this.releasedItem.StartsWith("Kovceg"))
					{
						StagesParser.bonusName = this.releasedItem;
						string[] array = StagesParser.bonusName.Split(new char[]
						{
							'_'
						});
						StagesParser.bonusID = int.Parse(array[1]);
						this.monkeyDestinationLevelIndex = this.GetMapLevelIndex(int.Parse(array[2]));
						this._GUI.Find("UNLOCK HOLDER/AnimationHolderGlavni/AnimationHolder/Unlock Bonus Level Popup/Text/Number of Bananas").GetComponent<TextMesh>().text = array[3];
						this._GUI.Find("UNLOCK HOLDER/AnimationHolderGlavni/AnimationHolder/Unlock Bonus Level Popup/Text/Number of Bananas").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
						if (int.Parse(array[2]) < this.zadnjiOtkljucanNivo_proveraZaBonus && KameraMovement.makniPopup == 0)
						{
							if (this.pronadjiKovceg(StagesParser.bonusName).Find("GateClosed").GetComponent<Renderer>().enabled)
							{
								this._GUI.Find("UNLOCK HOLDER/AnimationHolderGlavni").localPosition += new Vector3(0f, 35f, 0f);
								this._GUI.Find("UNLOCK HOLDER/AnimationHolderGlavni").GetComponent<Animator>().Play("OpenPopup");
								KameraMovement.makniPopup = 2;
								return;
							}
							if (PlaySounds.soundOn)
							{
								PlaySounds.Play_Button_GoBack();
							}
							this.kretanjeDoKovcega = true;
							if (this.majmunceSeMrda)
							{
								base.StopCoroutine("KretanjeMajmunceta");
							}
							if (StagesParser.pozicijaMajmuncetaNaMapi == Vector3.zero)
							{
								this.kovcegNaPocetku = (this.trenutniKovceg = this.pronadjiKovceg(StagesParser.bonusName));
							}
							else
							{
								this.trenutniKovceg = this.pronadjiKovceg(StagesParser.bonusName);
							}
							this.trenutniKovceg.Find("GateClosed").GetComponent<Renderer>().enabled = false;
							this.trenutniKovceg.Find("GateOpen").GetComponent<Renderer>().enabled = true;
							this.animator.Play("Running");
							base.StartCoroutine("KretanjeMajmunceta");
							return;
						}
					}
					else if (this.releasedItem.Equals("Button_UnlockBonusYES"))
					{
						this.popupZaSpustanje = this._GUI.Find("UNLOCK HOLDER/AnimationHolderGlavni");
						base.Invoke("spustiPopup", 0.5f);
						this._GUI.Find("UNLOCK HOLDER/AnimationHolderGlavni").GetComponent<Animator>().Play("ClosePopup");
						KameraMovement.makniPopup = 0;
						string[] array2 = StagesParser.bonusName.Split(new char[]
						{
							'_'
						});
						StagesParser.bonusID = int.Parse(array2[1]);
						this.monkeyDestinationLevelIndex = this.GetMapLevelIndex(int.Parse(array2[2]));
						if (int.Parse(array2[2]) < this.zadnjiOtkljucanNivo_proveraZaBonus)
						{
							if (int.Parse(array2[3]) <= StagesParser.currentBananas)
							{
								if (PlaySounds.soundOn)
								{
									PlaySounds.Play_Button_GoBack();
								}
								this.kretanjeDoKovcega = true;
								StagesParser.currentBananas -= int.Parse(array2[3]);
								this._GUI.Find("INTERFACE HOLDER/_TopLeft/BananaHolder/Bananas/Banana Number").GetComponent<TextMesh>().text = StagesParser.currentBananas.ToString();
								this._GUI.Find("INTERFACE HOLDER/_TopLeft/BananaHolder/Bananas/Banana Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
								string[] array3 = StagesParser.bonusLevels.Split(new char[]
								{
									'_'
								});
								string text = array3[StagesParser.currSetIndex];
								string[] array4 = text.Split(new char[]
								{
									'#'
								});
								array4[StagesParser.bonusID - 1] = "0";
								string text2 = string.Empty;
								text = string.Empty;
								for (int i = 0; i < array4.Length; i++)
								{
									text = text + array4[i] + "#";
								}
								text = text.Remove(text.Length - 1);
								array3[StagesParser.currSetIndex] = text;
								for (int j = 0; j < StagesParser.totalSets; j++)
								{
									text2 = text2 + array3[j] + "_";
								}
								text2 = text2.Remove(text2.Length - 1);
								PlayerPrefs.SetString("BonusLevel", text2);
								PlayerPrefs.Save();
								StagesParser.bonusLevels = text2;
								StagesParser.ServerUpdate = 1;
								if (this.majmunceSeMrda)
								{
									base.StopCoroutine("KretanjeMajmunceta");
								}
								if (StagesParser.pozicijaMajmuncetaNaMapi == Vector3.zero)
								{
									this.kovcegNaPocetku = (this.trenutniKovceg = this.pronadjiKovceg(StagesParser.bonusName));
								}
								else
								{
									this.trenutniKovceg = this.pronadjiKovceg(StagesParser.bonusName);
								}
								this.trenutniKovceg.Find("GateClosed").GetComponent<Renderer>().enabled = false;
								this.trenutniKovceg.Find("GateOpen").GetComponent<Renderer>().enabled = true;
								this.animator.Play("Running");
								base.StartCoroutine("KretanjeMajmunceta");
								return;
							}
							this._GUI.Find("INTERFACE HOLDER/_TopLeft/BananaHolder/Bananas").GetComponent<Animation>().Play();
							return;
						}
					}
					else
					{
						if (this.releasedItem.Equals("Button_UnlockBonusNO"))
						{
							this.popupZaSpustanje = this._GUI.Find("UNLOCK HOLDER/AnimationHolderGlavni");
							base.Invoke("spustiPopup", 0.5f);
							this._GUI.Find("UNLOCK HOLDER/AnimationHolderGlavni").GetComponent<Animator>().Play("ClosePopup");
							KameraMovement.makniPopup = 0;
							return;
						}
						if (this.releasedItem.Contains("tvtv"))
						{
							this.televizorIzabrao = true;
							this.prejasiTelevizor = true;
							int num3 = int.Parse(this.releasedItem.Substring(0, this.releasedItem.IndexOf("-")));
							if (StagesParser.SetsInGame[StagesParser.currSetIndex].GetStarOnStage(num3 - 1) > 0 && KameraMovement.makniPopup == 0)
							{
								if (PlaySounds.soundOn)
								{
									PlaySounds.Play_Button_GoBack();
								}
								this.monkeyDestinationLevelIndex = this.GetWatchVideoIndex(this.releasedItem.Substring(this.releasedItem.Length - 1));
								this.trenutniTelevizor = int.Parse(this.releasedItem.Substring(this.releasedItem.Length - 1));
								this.watchVideoIndex_pom = this.monkeyDestinationLevelIndex;
								this.televizorNaMapi = true;
								if (this.majmunceSeMrda)
								{
									base.StopCoroutine("KretanjeMajmunceta");
								}
								this.animator.Play("Running");
								base.StartCoroutine("KretanjeMajmunceta");
								return;
							}
						}
						else
						{
							if (this.releasedItem.Equals("Button_WatchVideoYES"))
							{
								if (PlaySounds.soundOn)
								{
									PlaySounds.Play_Button_GoBack();
								}
								KameraMovement.makniPopup = 9;
								base.StartCoroutine(this.checkConnectionForTelevizor());
								return;
							}
							if (this.releasedItem.Equals("Button_WatchVideoNO"))
							{
								if (PlaySounds.soundOn)
								{
									PlaySounds.Play_Button_GoBack();
								}
								this.popupZaSpustanje = this._GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni");
								base.Invoke("spustiPopup", 0.5f);
								this._GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni").GetComponent<Animator>().Play("ClosePopup");
								KameraMovement.makniPopup = 0;
								if (!this.televizorIzabrao)
								{
									this.prejasiTelevizor = true;
									this.animator.Play("Running");
									base.StartCoroutine("KretanjeMajmunceta");
									return;
								}
								this.televizorIzabrao = false;
								this.prejasiTelevizor = false;
								return;
							}
							else
							{
								if (this.releasedItem.Equals("Button_MissionCancel"))
								{
									if (PlaySounds.soundOn)
									{
										PlaySounds.Play_Button_GoBack();
									}
									this.popupZaSpustanje = this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni");
									base.Invoke("spustiPopup", 0.5f);
									this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni").GetComponent<Animator>().Play("ClosePopup");
									KameraMovement.makniPopup = 0;
									this.ocistiMisije();
									this.prejasiTelevizor = false;
									return;
								}
								if (this.releasedItem.Equals("Button_MissionPlay"))
								{
									if (PlaySounds.soundOn)
									{
										PlaySounds.Play_Button_GoBack();
									}
									if (StagesParser.odgledaoTutorial == 1 && StagesParser.currentLevel == 2)
									{
										StagesParser.loadingTip = 3;
									}
									base.StartCoroutine(this.closeDoorAndPlay());
									return;
								}
								if (this.releasedItem.Equals("ShopKucica"))
								{
									if (StagesParser.otvaraoShopNekad == 0 || StagesParser.otvaraoShopNekad == 2)
									{
										if (KameraMovement.makniPopup == 6)
										{
											StagesParser.currentMoney += int.Parse(ShopManagerFull.ShopObject.CoinsHats[0]);
											PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
											PlayerPrefs.Save();
											this.TutorialShop.SetActive(false);
											SwipeControlCustomizationHats.allowInput = false;
											base.Invoke("prebaciStrelicuNaItem", 1.2f);
										}
										else
										{
											StagesParser.otvaraoShopNekad = 1;
											PlayerPrefs.SetString("OdgledaoTutorial", StagesParser.odgledaoTutorial.ToString() + "#" + StagesParser.otvaraoShopNekad.ToString());
											PlayerPrefs.Save();
										}
									}
									this._GUI.Find("ShopHolder/Shop").GetComponent<Animation>().Play("MeniDolazak");
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
									ShopManagerFull.ShopObject.PozoviTab(3);
									if (PlaySounds.soundOn)
									{
										PlaySounds.Play_Button_OpenLevel();
									}
									if (KameraMovement.makniPopup == 0)
									{
										KameraMovement.makniPopup = 5;
										return;
									}
								}
								else
								{
									if (this.releasedItem.Equals("BankInApp") || this.releasedItem.Equals("Bananas"))
									{
										this._GUI.Find("ShopHolder/Shop").GetComponent<Animation>().Play("MeniDolazak");
										ShopManagerFull.ShopObject.PozoviTab(2);
										if (PlaySounds.soundOn)
										{
											PlaySounds.Play_Button_OpenLevel();
										}
										KameraMovement.makniPopup = 5;
										return;
									}
									if (this.releasedItem.Equals("Coins") || this.releasedItem.Equals("FreeCoins"))
									{
										this._GUI.Find("ShopHolder/Shop").GetComponent<Animation>().Play("MeniDolazak");
										ShopManagerFull.ShopObject.PozoviTab(1);
										if (PlaySounds.soundOn)
										{
											PlaySounds.Play_Button_OpenLevel();
										}
										KameraMovement.makniPopup = 5;
										return;
									}
									if (this.releasedItem.Equals("ButtonBackShop"))
									{
										this._GUI.Find("INTERFACE HOLDER/_TopLeft/Coins/Coins Number").GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
										this._GUI.Find("INTERFACE HOLDER/_TopLeft/Coins/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
										this._GUI.Find("INTERFACE HOLDER/_TopLeft/BananaHolder/Bananas/Banana Number").GetComponent<TextMesh>().text = StagesParser.currentBananas.ToString();
										this._GUI.Find("INTERFACE HOLDER/_TopLeft/BananaHolder/Bananas/Banana Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
										ShopManagerFull.ShopObject.SkloniShop();
										if (PlaySounds.soundOn)
										{
											PlaySounds.Play_Button_OpenLevel();
										}
										KameraMovement.makniPopup = 0;
										return;
									}
									if (this.releasedItem.Equals("ButtonCustomize"))
									{
										ShopManagerFull.ShopObject.PozoviTab(3);
										if (PlaySounds.soundOn)
										{
											PlaySounds.Play_Button_OpenLevel();
											return;
										}
									}
									else if (this.releasedItem.Equals("ButtonFreeCoins"))
									{
										ShopManagerFull.ShopObject.PozoviTab(1);
										if (PlaySounds.soundOn)
										{
											PlaySounds.Play_Button_OpenLevel();
											return;
										}
									}
									else if (this.releasedItem.Equals("ButtonPowerUps"))
									{
										ShopManagerFull.ShopObject.PozoviTab(4);
										if (PlaySounds.soundOn)
										{
											PlaySounds.Play_Button_OpenLevel();
											return;
										}
									}
									else if (this.releasedItem.Equals("ButtonShop"))
									{
										ShopManagerFull.ShopObject.PozoviTab(2);
										if (PlaySounds.soundOn)
										{
											PlaySounds.Play_Button_OpenLevel();
											return;
										}
									}
									else if (this.releasedItem.Equals("1HatsShopTab"))
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
									else if (this.releasedItem.Equals("2TShirtsShopTab"))
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
									else if (this.releasedItem.Equals("3BackPackShopTab"))
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
										for (int k = 0; k < ShopManagerFull.ShopObject.HatsObjects.Length; k++)
										{
											if (this.releasedItem.StartsWith("Hats " + (k + 1)))
											{
												ObjCustomizationHats.swipeCtrl.currentValue = ShopManagerFull.ShopObject.HatsObjects.Length - k - 1;
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
										for (int l = 0; l < ShopManagerFull.ShopObject.ShirtsObjects.Length; l++)
										{
											if (this.releasedItem.StartsWith("Shirts " + (l + 1)))
											{
												ObjCustomizationShirts.swipeCtrl.currentValue = ShopManagerFull.ShopObject.ShirtsObjects.Length - l - 1;
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
										for (int m = 0; m < ShopManagerFull.ShopObject.BackPacksObjects.Length; m++)
										{
											if (this.releasedItem.StartsWith("BackPacks " + (m + 1)))
											{
												ObjCustomizationBackPacks.swipeCtrl.currentValue = ShopManagerFull.ShopObject.BackPacksObjects.Length - m - 1;
											}
										}
										if (PlaySounds.soundOn)
										{
											PlaySounds.Play_Button_OpenLevel();
											return;
										}
									}
									else if (this.releasedItem.Equals("ClearAll"))
									{
										ShopManagerFull.ShopObject.OcistiMajmuna();
										if (PlaySounds.soundOn)
										{
											PlaySounds.Play_Button_OpenLevel();
											return;
										}
									}
									else if (this.releasedItem.Equals("Preview Button"))
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
									else if (this.releasedItem.Equals("Buy Button"))
									{
										if (KameraMovement.makniPopup == 6 && ShopManagerFull.BuyButtonState == 2)
										{
											this.TutorialShop.SetActive(false);
											SwipeControlCustomizationHats.allowInput = true;
											StagesParser.otvaraoShopNekad = 1;
											StagesParser.odgledaoTutorial = 3;
											PlayerPrefs.SetString("OdgledaoTutorial", StagesParser.odgledaoTutorial.ToString() + "#" + StagesParser.otvaraoShopNekad.ToString());
											PlayerPrefs.Save();
											KameraMovement.makniPopup = 5;
										}
										ShopManagerFull.ShopObject.KupiItem();
										if (PlaySounds.soundOn)
										{
											PlaySounds.Play_Button_OpenLevel();
											return;
										}
									}
									else
									{
										if (this.releasedItem.Equals("Button_CollectReward"))
										{
											this.DodeliNagrade();
											if (PlaySounds.soundOn)
											{
												PlaySounds.Play_Button_GoBack();
											}
											this.popupZaSpustanje = this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni");
											base.Invoke("spustiPopup", 0.5f);
											this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni").GetComponent<Animator>().Play("ClosePopup");
											this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/PomocniColliderKodOtvaranjaKovcega").localScale = Vector3.zero;
											KameraMovement.makniPopup = 0;
											return;
										}
										if (this.releasedItem.Equals("Shop Banana"))
										{
											ShopManagerFull.ShopObject.KupiBananu();
											if (PlaySounds.soundOn)
											{
												PlaySounds.Play_Button_OpenLevel();
												return;
											}
										}
										else if (this.releasedItem.Equals("Shop POWERUP Double Coins"))
										{
											ShopManagerFull.ShopObject.KupiDoubleCoins();
											if (PlaySounds.soundOn)
											{
												PlaySounds.Play_Button_OpenLevel();
												return;
											}
										}
										else if (this.releasedItem.Equals("Shop POWERUP Magnet"))
										{
											ShopManagerFull.ShopObject.KupiMagnet();
											if (PlaySounds.soundOn)
											{
												PlaySounds.Play_Button_OpenLevel();
												return;
											}
										}
										else if (this.releasedItem.Equals("Shop POWERUP Shield"))
										{
											ShopManagerFull.ShopObject.KupiShield();
											if (PlaySounds.soundOn)
											{
												PlaySounds.Play_Button_OpenLevel();
												return;
											}
										}
										else
										{
											if (this.releasedItem.Equals("FB Login"))
											{
												KameraMovement.makniPopup = 8;
												base.StartCoroutine(this.checkConnectionForLoginButton());
												return;
											}
											if (this.releasedItem.StartsWith("Friends Level"))
											{
												KameraMovement.makniPopup = 8;
												base.StartCoroutine(this.checkConnectionForInviteFriend());
												return;
											}
											if (this.releasedItem.Equals("Button_CheckOK"))
											{
												KameraMovement.makniPopup = 0;
												base.StartCoroutine(CheckInternetConnection.Instance.ClosePopup());
												return;
											}
											if (this.releasedItem.Equals("ShopFCBILikePage"))
											{
												KameraMovement.makniPopup = 8;
												base.StartCoroutine(this.checkConnectionForPageLike("https://www.facebook.com/pages/Banana-Island/636650059721490", "BananaIsland"));
												return;
											}
											if (this.releasedItem.Equals("ShopFCWatchVideo"))
											{
												KameraMovement.makniPopup = 8;
												base.StartCoroutine(this.checkConnectionForWatchVideo());
												return;
											}
											if (this.releasedItem.Equals("ShopFCWLLikePage"))
											{
												KameraMovement.makniPopup = 8;
												base.StartCoroutine(this.checkConnectionForPageLike("https://www.facebook.com/WebelinxGamesApps", "Webelinx"));
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
	}

	// Token: 0x06002EE0 RID: 12000 RVA: 0x00178B08 File Offset: 0x00176D08
	private void DodeliNagrade()
	{
		switch (this.reward1Type)
		{
		case 1:
			StagesParser.powerup_magnets += this.kolicinaReward1;
			break;
		case 2:
			StagesParser.powerup_doublecoins += this.kolicinaReward1;
			break;
		case 3:
			StagesParser.powerup_shields += this.kolicinaReward1;
			break;
		case 4:
			this.DajMuItem();
			break;
		}
		switch (this.reward2Type)
		{
		case 1:
			StagesParser.powerup_magnets += this.kolicinaReward2;
			break;
		case 2:
			StagesParser.powerup_doublecoins += this.kolicinaReward2;
			break;
		case 3:
			StagesParser.powerup_shields += this.kolicinaReward2;
			break;
		}
		switch (this.reward3Type)
		{
		case 1:
			StagesParser.powerup_magnets += this.kolicinaReward3;
			break;
		case 2:
			StagesParser.powerup_doublecoins += this.kolicinaReward3;
			break;
		case 3:
			StagesParser.powerup_shields += this.kolicinaReward3;
			break;
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
		GameObject.Find("Double Coins Number/Number").GetComponent<TextMesh>().text = StagesParser.powerup_doublecoins.ToString();
		GameObject.Find("Double Coins Number/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Magnet Number/Number").GetComponent<TextMesh>().text = StagesParser.powerup_magnets.ToString();
		GameObject.Find("Magnet Number/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Shield Number/Number").GetComponent<TextMesh>().text = StagesParser.powerup_shields.ToString();
		GameObject.Find("Shield Number/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		this.reward1Type = 0;
		this.reward2Type = 0;
		this.reward3Type = 0;
		this.kolicinaReward1 = 0;
		this.kolicinaReward2 = 0;
		this.kolicinaReward3 = 0;
		StagesParser.ServerUpdate = 1;
	}

	// Token: 0x06002EE1 RID: 12001 RVA: 0x00178D40 File Offset: 0x00176F40
	private void DajMuItem()
	{
		if (this.cetvrtiKovcegNagrada == "Glava")
		{
			string[] array = StagesParser.svekupovineGlava.Split(new char[]
			{
				'#'
			});
			array[this.indexNagradeZaCetvrtiKovceg] = "1";
			string text = "";
			for (int i = 0; i < array.Length; i++)
			{
				text = text + array[i] + "#";
			}
			text = text.Remove(text.Length - 1);
			StagesParser.svekupovineGlava = text;
			ShopManagerFull.ShopObject.HatsObjects[this.indexNagradeZaCetvrtiKovceg].Find("Stikla").gameObject.SetActive(true);
			ShopManagerFull.SveStvariZaOblacenjeHats[this.indexNagradeZaCetvrtiKovceg] = 1;
			PlayerPrefs.SetString("UserSveKupovineHats", StagesParser.svekupovineGlava);
		}
		else if (this.cetvrtiKovcegNagrada == "Majica")
		{
			string[] array = StagesParser.svekupovineMajica.Split(new char[]
			{
				'#'
			});
			array[this.indexNagradeZaCetvrtiKovceg] = "1";
			string text2 = "";
			for (int j = 0; j < array.Length; j++)
			{
				text2 = text2 + array[j] + "#";
			}
			text2 = text2.Remove(text2.Length - 1);
			StagesParser.svekupovineMajica = text2;
			ShopManagerFull.ShopObject.ShirtsObjects[this.indexNagradeZaCetvrtiKovceg].Find("Stikla").gameObject.SetActive(true);
			ShopManagerFull.SveStvariZaOblacenjeShirts[this.indexNagradeZaCetvrtiKovceg] = 1;
			PlayerPrefs.SetString("UserSveKupovineShirts", StagesParser.svekupovineMajica);
		}
		else if (this.cetvrtiKovcegNagrada == "Ledja")
		{
			string[] array = StagesParser.svekupovineLedja.Split(new char[]
			{
				'#'
			});
			array[this.indexNagradeZaCetvrtiKovceg] = "1";
			string text3 = "";
			for (int k = 0; k < array.Length; k++)
			{
				text3 = text3 + array[k] + "#";
			}
			text3 = text3.Remove(text3.Length - 1);
			StagesParser.svekupovineLedja = text3;
			ShopManagerFull.ShopObject.BackPacksObjects[this.indexNagradeZaCetvrtiKovceg].Find("Stikla").gameObject.SetActive(true);
			ShopManagerFull.SveStvariZaOblacenjeBackPack[this.indexNagradeZaCetvrtiKovceg] = 1;
			PlayerPrefs.SetString("UserSveKupovineBackPacks", StagesParser.svekupovineLedja);
		}
		else if (this.cetvrtiKovcegNagrada == "PowerUps")
		{
			StagesParser.powerup_magnets += this.kolicinaReward1;
			StagesParser.powerup_doublecoins += this.kolicinaReward2;
			StagesParser.powerup_shields += this.kolicinaReward3;
		}
		ShopManagerFull.ShopObject.ProveriStanjeCelogShopa();
		PlayerPrefs.Save();
		this.indexNagradeZaCetvrtiKovceg = -1;
	}

	// Token: 0x06002EE2 RID: 12002 RVA: 0x00178FE8 File Offset: 0x001771E8
	private void IspitajItem()
	{
		string text = string.Empty;
		string[] array = StagesParser.svekupovineGlava.Split(new char[]
		{
			'#'
		});
		for (int i = 0; i <= ShopManagerFull.BrojOtkljucanihKapa; i++)
		{
			if (int.Parse(array[i]) == 0)
			{
				this.cetvrtiKovcegNagrada = "Glava";
				this.indexNagradeZaCetvrtiKovceg = i;
				break;
			}
		}
		if (this.indexNagradeZaCetvrtiKovceg == -1)
		{
			array = StagesParser.svekupovineMajica.Split(new char[]
			{
				'#'
			});
			for (int j = 0; j <= ShopManagerFull.BrojOtkljucanihMajici; j++)
			{
				if (int.Parse(array[j]) == 0)
				{
					this.cetvrtiKovcegNagrada = "Majica";
					this.indexNagradeZaCetvrtiKovceg = j;
					break;
				}
			}
			if (this.indexNagradeZaCetvrtiKovceg == -1)
			{
				array = StagesParser.svekupovineLedja.Split(new char[]
				{
					'#'
				});
				for (int k = 0; k <= ShopManagerFull.BrojOtkljucanihRanceva; k++)
				{
					if (int.Parse(array[k]) == 0)
					{
						this.cetvrtiKovcegNagrada = "Ledja";
						this.indexNagradeZaCetvrtiKovceg = k;
						break;
					}
				}
				if (this.indexNagradeZaCetvrtiKovceg == -1)
				{
					this.cetvrtiKovcegNagrada = "PowerUps";
					this.kolicinaReward1 = 2;
					this.kolicinaReward2 = 2;
					this.kolicinaReward3 = 2;
					this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward2").gameObject.SetActive(true);
					this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward3").gameObject.SetActive(true);
					this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1/Count").GetComponent<TextMesh>().text = this.kolicinaReward1.ToString();
					this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward2/Count").GetComponent<TextMesh>().text = this.kolicinaReward2.ToString();
					this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward3/Count").GetComponent<TextMesh>().text = this.kolicinaReward3.ToString();
					this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1").GetComponent<SpriteRenderer>().sprite = this._GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Magnet/Plavi Bedz/Magnet Icon").GetComponent<SpriteRenderer>().sprite;
					this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward2").GetComponent<SpriteRenderer>().sprite = this._GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Shield/Plavi Bedz/Shield Icon").GetComponent<SpriteRenderer>().sprite;
					this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward3").GetComponent<SpriteRenderer>().sprite = this._GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Double Coins/Plavi Bedz/Double Coins Icon").GetComponent<SpriteRenderer>().sprite;
					return;
				}
				Transform transform = GameObject.Find("3 Customize/Customize Tabovi/3BackPack").transform;
				for (int l = 0; l < transform.childCount; l++)
				{
					if (transform.GetChild(l).name.StartsWith("BackPacks " + (this.indexNagradeZaCetvrtiKovceg + 1).ToString()))
					{
						text = transform.GetChild(l).name;
						break;
					}
				}
				if (!text.Equals(string.Empty))
				{
					string str = text.Substring(text.IndexOf("- ") + 2);
					this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1").GetComponent<SpriteRenderer>().sprite = transform.Find(text).Find("Plavi Bedz/" + str + " Icon").GetComponent<SpriteRenderer>().sprite;
					return;
				}
			}
			else
			{
				Transform transform = GameObject.Find("3 Customize/Customize Tabovi/2Shirts").transform;
				for (int m = 0; m < transform.childCount; m++)
				{
					if (transform.GetChild(m).name.StartsWith("Shirts " + (this.indexNagradeZaCetvrtiKovceg + 1).ToString()))
					{
						text = transform.GetChild(m).name;
						break;
					}
				}
				if (!text.Equals(string.Empty))
				{
					string str2 = text.Substring(text.IndexOf("- ") + 2);
					this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1").GetComponent<SpriteRenderer>().sprite = transform.Find(text).Find("Plavi Bedz/" + str2 + " Icon").GetComponent<SpriteRenderer>().sprite;
					this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1").GetComponent<SpriteRenderer>().color = transform.Find(text).Find("Plavi Bedz/" + str2 + " Icon").GetComponent<SpriteRenderer>().color;
					return;
				}
			}
		}
		else
		{
			Transform transform = GameObject.Find("3 Customize/Customize Tabovi/1Hats").transform;
			for (int n = 0; n < transform.childCount; n++)
			{
				if (transform.GetChild(n).name.StartsWith("Hats " + (this.indexNagradeZaCetvrtiKovceg + 1).ToString()))
				{
					text = transform.GetChild(n).name;
					break;
				}
			}
			if (!text.Equals(string.Empty))
			{
				string str3 = text.Substring(text.IndexOf("- ") + 2);
				this._GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1").GetComponent<SpriteRenderer>().sprite = transform.Find(text).Find("Plavi Bedz/" + str3 + " Icon").GetComponent<SpriteRenderer>().sprite;
			}
		}
	}

	// Token: 0x06002EE3 RID: 12003 RVA: 0x001794F8 File Offset: 0x001776F8
	private Transform pronadjiKovceg(string name)
	{
		for (int i = 0; i < this.BonusNivoi.Length; i++)
		{
			if (this.BonusNivoi[i].name.Equals(name))
			{
				return this.BonusNivoi[i];
			}
		}
		return null;
	}

	// Token: 0x06002EE4 RID: 12004 RVA: 0x00179538 File Offset: 0x00177738
	private int GetMapLevelIndex(int value)
	{
		for (int i = 0; i < this.izmedjneTacke.childCount; i++)
		{
			int num;
			if (int.TryParse(this.izmedjneTacke.GetChild(i).name, out num) && num == value)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06002EE5 RID: 12005 RVA: 0x0017957C File Offset: 0x0017777C
	private int GetWatchVideoIndex(string tvsuffix)
	{
		for (int i = 0; i < this.izmedjneTacke.childCount; i++)
		{
			if (this.izmedjneTacke.GetChild(i).name.Contains("tvtv" + tvsuffix))
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06002EE6 RID: 12006 RVA: 0x001795C8 File Offset: 0x001777C8
	private int findAngleDir(Transform start, Vector3 target)
	{
		Vector3 vector = target - start.position;
		if (!(vector != Vector3.zero))
		{
			return -1;
		}
		float num = (vector.y <= start.right.y) ? Vector3.Angle(start.right, vector) : (360f - Vector3.Angle(start.right, vector));
		if (num >= 22f && num < 67f)
		{
			return 0;
		}
		if (num >= 67f && num < 112f)
		{
			return 1;
		}
		if (num >= 112f && num < 157f)
		{
			return 2;
		}
		if (num >= 157f && num < 202f)
		{
			return 3;
		}
		if (num >= 202f && num < 247f)
		{
			return 4;
		}
		if (num >= 247f && num < 292f)
		{
			return 5;
		}
		if (num >= 292f && num < 337f)
		{
			return 6;
		}
		return 7;
	}

	// Token: 0x06002EE7 RID: 12007 RVA: 0x00022BF2 File Offset: 0x00020DF2
	private IEnumerator KretanjeMajmunceta()
	{
		this.majmunceSeMrda = true;
		int num = Mathf.Abs(this.monkeyCurrentLevelIndex - this.monkeyDestinationLevelIndex);
		float brzina = (float)num * Time.deltaTime;
		if (num == 0)
		{
			brzina = 4f * Time.deltaTime;
		}
		Quaternion a = Quaternion.identity;
		bool izadji = false;
		brzina = Mathf.Clamp(brzina, 0.065f, 1f);
		if (StagesParser.pozicijaMajmuncetaNaMapi != Vector3.zero)
		{
			Transform koraciDoKovcega = this.kovcegNaPocetku.Find("Koraci do kovcega");
			int num2;
			for (int i = koraciDoKovcega.childCount - 1; i >= 0; i = num2 - 1)
			{
				int angleDir = this.findAngleDir(this.holderMajmun, koraciDoKovcega.GetChild(i).position);
				if (angleDir != -1)
				{
					a = Quaternion.Euler(this.angles[angleDir]);
				}
				while (this.holderMajmun.position != koraciDoKovcega.GetChild(i).position)
				{
					if (!PlaySounds.Run.isPlaying && PlaySounds.soundOn)
					{
						PlaySounds.Play_Run();
					}
					this.holderMajmun.position = Vector3.MoveTowards(this.holderMajmun.position, koraciDoKovcega.GetChild(i).position, brzina);
					yield return null;
					if (angleDir != -1)
					{
						this.majmun.rotation = Quaternion.Lerp(this.majmun.rotation, a, 0.2f);
					}
				}
				num2 = i;
			}
			StagesParser.pozicijaMajmuncetaNaMapi = Vector3.zero;
			koraciDoKovcega = null;
		}
		if (this.monkeyCurrentLevelIndex < this.monkeyDestinationLevelIndex)
		{
			int num2;
			for (int i = this.monkeyCurrentLevelIndex; i <= this.monkeyDestinationLevelIndex; i = num2 + 1)
			{
				int angleDir = this.findAngleDir(this.holderMajmun, this.izmedjneTacke.GetChild(i).position);
				if (angleDir != -1)
				{
					a = Quaternion.Euler(this.angles[angleDir]);
				}
				while (this.holderMajmun.position != this.izmedjneTacke.GetChild(i).position && !izadji)
				{
					if (!PlaySounds.Run.isPlaying && PlaySounds.soundOn)
					{
						PlaySounds.Play_Run();
					}
					this.holderMajmun.position = Vector3.MoveTowards(this.holderMajmun.position, this.izmedjneTacke.GetChild(i).position, brzina);
					yield return null;
					this.monkeyCurrentLevelIndex = i;
					if (angleDir != -1)
					{
						this.majmun.rotation = Quaternion.Lerp(this.majmun.rotation, a, 0.2f);
					}
					if (this.izmedjneTacke.GetChild(i).name.Contains("tvtv") && this.izmedjneTacke.GetChild(i).gameObject.activeSelf && !this.prejasiTelevizor)
					{
						yield return new WaitForEndOfFrame();
						this.televizorNaMapi = true;
						this.majmunceSeMrda = false;
						izadji = true;
						this.trenutniTelevizor = int.Parse(this.izmedjneTacke.GetChild(i).name.Substring(this.izmedjneTacke.GetChild(i).name.IndexOf("tvtv") + 4));
						this.animator.Play("Idle");
						this.monkeyCurrentLevelIndex = i + 1;
					}
				}
				num2 = i;
			}
		}
		else
		{
			int num2;
			for (int i = this.monkeyCurrentLevelIndex; i >= this.monkeyDestinationLevelIndex; i = num2 - 1)
			{
				int angleDir = this.findAngleDir(this.holderMajmun, this.izmedjneTacke.GetChild(i).position);
				if (angleDir != -1)
				{
					a = Quaternion.Euler(this.angles[angleDir]);
				}
				while (this.holderMajmun.position != this.izmedjneTacke.GetChild(i).position && !izadji)
				{
					if (!PlaySounds.Run.isPlaying && PlaySounds.soundOn)
					{
						PlaySounds.Play_Run();
					}
					this.holderMajmun.position = Vector3.MoveTowards(this.holderMajmun.position, this.izmedjneTacke.GetChild(i).position, brzina);
					yield return null;
					this.monkeyCurrentLevelIndex = i;
					if (angleDir != -1)
					{
						this.majmun.rotation = Quaternion.Lerp(this.majmun.rotation, a, 0.2f);
					}
					if (this.izmedjneTacke.GetChild(i).name.Contains("tvtv") && this.izmedjneTacke.GetChild(i).gameObject.activeSelf && !this.prejasiTelevizor)
					{
						yield return new WaitForEndOfFrame();
						this.televizorNaMapi = true;
						this.majmunceSeMrda = false;
						izadji = true;
						this.trenutniTelevizor = int.Parse(this.izmedjneTacke.GetChild(i).name.Substring(this.izmedjneTacke.GetChild(i).name.IndexOf("tvtv") + 4));
						this.animator.Play("Idle");
						this.monkeyCurrentLevelIndex = i - 1;
					}
				}
				num2 = i;
			}
		}
		if (this.kretanjeDoKovcega && !this.televizorNaMapi)
		{
			if (StagesParser.pozicijaMajmuncetaNaMapi == Vector3.zero)
			{
				Transform koraciDoKovcega = this.trenutniKovceg.Find("Koraci do kovcega");
				int num2;
				for (int i = 0; i < koraciDoKovcega.childCount; i = num2 + 1)
				{
					int angleDir = this.findAngleDir(this.holderMajmun, koraciDoKovcega.GetChild(i).position);
					if (angleDir != -1)
					{
						a = Quaternion.Euler(this.angles[angleDir]);
					}
					while (this.holderMajmun.position != koraciDoKovcega.GetChild(i).position)
					{
						if (!PlaySounds.Run.isPlaying && PlaySounds.soundOn)
						{
							PlaySounds.Play_Run();
						}
						this.holderMajmun.position = Vector3.MoveTowards(this.holderMajmun.position, koraciDoKovcega.GetChild(i).position, brzina);
						yield return null;
						if (angleDir != -1)
						{
							this.majmun.rotation = Quaternion.Lerp(this.majmun.rotation, a, 0.2f);
						}
					}
					num2 = i;
				}
				koraciDoKovcega = null;
			}
			StagesParser.bonusLevel = true;
			StagesParser.pozicijaMajmuncetaNaMapi = this.holderMajmun.position;
			this.animator.Play("Idle");
			this.majmunceSeMrda = false;
			float t = 0f;
			while (t < 1f)
			{
				Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(this.holderMajmun.position.x, this.holderMajmun.position.y, Camera.main.transform.position.z), t);
				t += Time.deltaTime;
				yield return null;
			}
			Camera.main.transform.position = new Vector3(Mathf.Clamp(Camera.main.transform.position.x, this.levaGranica, this.desnaGranica), Mathf.Clamp(Camera.main.transform.position.y, this.donjaGranica, this.gornjaGranica), Camera.main.transform.position.z);
			if (this.kretanjeDoKovcega)
			{
				this.kretanjeDoKovcega = false;
				base.StartCoroutine(this.closeDoorAndPlay());
			}
			MissionManager.OdrediMisiju(StagesParser.currentLevel - 1, true);
			if (!StagesParser.bonusLevel)
			{
				if (!FB.IsLoggedIn)
				{
					if (this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN").gameObject.activeSelf)
					{
						this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN").gameObject.SetActive(false);
					}
				}
				else
				{
					if (!this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN").gameObject.activeSelf)
					{
						this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN").gameObject.SetActive(true);
					}
					this.getFriendsScoresOnLevel(StagesParser.currentLevel);
				}
				this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni").localPosition += new Vector3(0f, 35f, 0f);
				this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni").GetComponent<Animator>().Play("OpenPopup");
				KameraMovement.makniPopup = 1;
			}
		}
		else if (this.televizorNaMapi)
		{
			this.televizorNaMapi = false;
			this.animator.Play("Idle");
			this._GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/Coins/Coins Number").GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
			this._GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/Coins/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
			this._GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni").localPosition += new Vector3(0f, 35f, 0f);
			this._GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni").GetComponent<Animator>().Play("OpenPopup");
			KameraMovement.makniPopup = 4;
		}
		else
		{
			this.animator.Play("Idle");
			this.majmunceSeMrda = false;
			float t = 0f;
			float limitX;
			if (this.holderMajmun.position.x > this.desnaGranica)
			{
				limitX = this.desnaGranica;
			}
			else if (this.holderMajmun.position.x < this.levaGranica)
			{
				limitX = this.levaGranica;
			}
			else
			{
				limitX = this.holderMajmun.position.x;
			}
			while (Camera.main.transform.position.x != limitX)
			{
				Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(limitX, this.holderMajmun.position.y, Camera.main.transform.position.z), t);
				t += Time.deltaTime;
				yield return null;
			}
			StagesParser.currStageIndex = this.levelName - 1;
			StagesParser.currentLevel = StagesParser.currSetIndex * 20 + this.levelName;
			StagesParser.nivoZaUcitavanje = 10 + StagesParser.currSetIndex;
			MissionManager.OdrediMisiju(StagesParser.currentLevel - 1, true);
			if (!FB.IsLoggedIn)
			{
				if (this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN").gameObject.activeSelf)
				{
					this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN").gameObject.SetActive(false);
				}
			}
			else
			{
				if (!this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN").gameObject.activeSelf)
				{
					this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN").gameObject.SetActive(true);
				}
				this.getFriendsScoresOnLevel(StagesParser.currentLevel);
			}
			this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni").localPosition += new Vector3(0f, 35f, 0f);
			this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni").GetComponent<Animator>().Play("OpenPopup");
			KameraMovement.makniPopup = 1;
		}
		yield break;
	}

	// Token: 0x06002EE8 RID: 12008 RVA: 0x001796A8 File Offset: 0x001778A8
	private void ocistiMisije()
	{
		Transform transform = this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Popup za Mission HOLDER/Popup za Mission");
		Transform transform2 = transform.Find("Mission Icons/Mission 1");
		Transform transform3 = transform.Find("Mission Icons/Mission 2");
		Transform transform4 = transform.Find("Mission Icons/Mission 3");
		for (int i = 0; i < transform2.childCount; i++)
		{
			transform2.GetChild(i).GetComponent<Renderer>().enabled = false;
		}
		for (int j = 0; j < transform3.childCount; j++)
		{
			transform3.GetChild(j).GetComponent<Renderer>().enabled = false;
		}
		for (int k = 0; k < transform4.childCount; k++)
		{
			transform4.GetChild(k).GetComponent<Renderer>().enabled = false;
		}
		transform.Find("Text/Mission 1").GetComponent<TextMesh>().text = string.Empty;
		transform.Find("Text/Mission 1").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		transform.Find("Text/Mission 2").GetComponent<TextMesh>().text = string.Empty;
		transform.Find("Text/Mission 2").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		transform.Find("Text/Mission 3").GetComponent<TextMesh>().text = string.Empty;
		transform.Find("Text/Mission 3").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
	}

	// Token: 0x06002EE9 RID: 12009 RVA: 0x001797F8 File Offset: 0x001779F8
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

	// Token: 0x06002EEA RID: 12010 RVA: 0x00022C01 File Offset: 0x00020E01
	private IEnumerator closeDoorAndPlay()
	{
		this._GUI.Find("LOADING HOLDER NEW/Loading Animation Vrata").GetComponent<Animator>().Play("Loading Zidovi Dolazak");
		yield return new WaitForSeconds(0.75f);
		Application.LoadLevel(2);
		yield break;
	}

	// Token: 0x06002EEB RID: 12011 RVA: 0x00022C10 File Offset: 0x00020E10
	private IEnumerator UcitajOstrvo(string ime)
	{
		this._GUI.Find("LOADING HOLDER NEW/Loading Animation Vrata").GetComponent<Animator>().Play("Loading Zidovi Dolazak");
		yield return new WaitForSeconds(1.1f);
		Application.LoadLevel(ime);
		yield break;
	}

	// Token: 0x06002EEC RID: 12012 RVA: 0x00022C26 File Offset: 0x00020E26
	private IEnumerator PlayAndWaitForAnimation(Animation animation, string animName, bool loadAnotherScene, int indexOfSceneToLoad)
	{
		yield return null;
		StagesParser.nivoZaUcitavanje = 10 + StagesParser.currSetIndex;
		if (loadAnotherScene)
		{
			Application.LoadLevel(indexOfSceneToLoad);
		}
		yield break;
	}

	// Token: 0x06002EED RID: 12013 RVA: 0x00022C3D File Offset: 0x00020E3D
	private IEnumerator PostaviKameruUGranice(Vector3 rez)
	{
		float t = 0f;
		Vector3 rez2 = Camera.main.transform.position + new Vector3(rez.x, rez.y, 0f);
		while (t < 1f)
		{
			Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, rez2, t);
			t += Time.deltaTime / 0.5f;
			yield return null;
		}
		yield break;
	}

	// Token: 0x06002EEE RID: 12014 RVA: 0x00022C4C File Offset: 0x00020E4C
	private IEnumerator PokaziMuCustomize()
	{
		this.TutorialShop = Object.Instantiate<GameObject>(this.TutorialShopPrefab, new Vector3(-33.2f, -95f, -60f), Quaternion.identity);
		yield return new WaitForSeconds(1.2f);
		while (Camera.main.transform.position.y <= -18.45f)
		{
			Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, new Vector3(Camera.main.transform.position.x, -18.4f, Camera.main.transform.position.z), 0.055f);
			yield return null;
		}
		this.TutorialShop.transform.GetChild(0).GetComponent<Animation>().Play();
		yield return new WaitForSeconds(0.5f);
		this.TutorialShop.transform.GetChild(0).Find("RedArrowHolder/RedArrow").GetComponent<Renderer>().enabled = true;
		this.TutorialShop.transform.GetChild(0).Find("RedArrowHolder/RedArrow").GetComponent<Animation>().Play();
		this.TutorialShop.transform.GetChild(0).GetComponent<Collider>().enabled = false;
		yield break;
	}

	// Token: 0x06002EEF RID: 12015 RVA: 0x00179850 File Offset: 0x00177A50
	private void changeLanguage()
	{
		if (!FB.IsLoggedIn)
		{
			GameObject.Find("Log In").GetComponent<TextMesh>().text = LanguageManager.LogIn;
			GameObject.Find("Log In").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		}
		GameObject.Find("Level No").GetComponent<TextMesh>().text = LanguageManager.Level;
		GameObject.Find("Level No").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("MissionText").GetComponent<TextMesh>().text = LanguageManager.Mission;
		GameObject.Find("MissionText").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN/Friends Level Win 1 HOLDER/FB/Fb Invite 1").GetComponent<TextMesh>().text = LanguageManager.Invite;
		this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN/Friends Level Win 2 HOLDER/FB/Fb Invite 1").GetComponent<TextMesh>().text = LanguageManager.Invite;
		this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN/Friends Level Win 3 HOLDER/FB/Fb Invite 1").GetComponent<TextMesh>().text = LanguageManager.Invite;
		this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN/Friends Level Win 4 HOLDER/FB/Fb Invite 1").GetComponent<TextMesh>().text = LanguageManager.Invite;
		this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN/Friends Level Win 5 HOLDER/FB/Fb Invite 1").GetComponent<TextMesh>().text = LanguageManager.Invite;
		this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN/Friends Level Win 1 HOLDER/FB/Fb Invite 1").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN/Friends Level Win 2 HOLDER/FB/Fb Invite 1").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN/Friends Level Win 3 HOLDER/FB/Fb Invite 1").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN/Friends Level Win 4 HOLDER/FB/Fb Invite 1").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN/Friends Level Win 5 HOLDER/FB/Fb Invite 1").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("RewardText").GetComponent<TextMesh>().text = LanguageManager.Reward;
		GameObject.Find("RewardText").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Button_CollectReward/Text").GetComponent<TextMesh>().text = LanguageManager.Collect;
		GameObject.Find("Button_CollectReward/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Bonus Level").GetComponent<TextMesh>().text = LanguageManager.BonusLevel;
		GameObject.Find("Bonus Level").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Unlock").GetComponent<TextMesh>().text = LanguageManager.Unlock;
		GameObject.Find("Unlock").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Button_UnlockBonusYES/Text").GetComponent<TextMesh>().text = LanguageManager.Yes;
		GameObject.Find("Button_UnlockBonusYES/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Button_UnlockBonusNO/Text").GetComponent<TextMesh>().text = LanguageManager.No;
		GameObject.Find("Button_UnlockBonusNO/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Free Coins").GetComponent<TextMesh>().text = LanguageManager.FreeCoins;
		GameObject.Find("Free Coins").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Button_WatchVideoYES/Text").GetComponent<TextMesh>().text = LanguageManager.Yes;
		GameObject.Find("Button_WatchVideoYES/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Button_WatchVideoNO/Text").GetComponent<TextMesh>().text = LanguageManager.No;
		GameObject.Find("Button_WatchVideoNO/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Button_WatchVideoNO/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		this._GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/WatchVideoText").GetComponent<TextMesh>().text = LanguageManager.WatchVideo;
		this._GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/WatchVideoText").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		this._GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/NotAvailableText").GetComponent<TextMesh>().text = LanguageManager.NoVideo;
		this._GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/NotAvailableText").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
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
		GameObject.Find("ButtonBuy").GetComponent<TextMesh>().text = LanguageManager.Buy;
		GameObject.Find("ButtonBuy").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Shop POWERUP Double Coins/Text/ime").GetComponent<TextMesh>().text = LanguageManager.DoubleCoins;
		GameObject.Find("Shop POWERUP Double Coins/Text/ime").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Shop POWERUP Magnet/Text/ime").GetComponent<TextMesh>().text = LanguageManager.CoinsMagnet;
		GameObject.Find("Shop POWERUP Magnet/Text/ime").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Shop POWERUP Shield/Text/ime").GetComponent<TextMesh>().text = LanguageManager.Shield;
		GameObject.Find("Shop POWERUP Shield/Text/ime").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
	}

	// Token: 0x06002EF0 RID: 12016 RVA: 0x00179E40 File Offset: 0x00178040
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
		List<KameraMovement.scoreAndIndex> list = new List<KameraMovement.scoreAndIndex>();
		for (int k = 0; k < FacebookManager.ListaStructPrijatelja.Count; k++)
		{
			KameraMovement.scoreAndIndex scoreAndIndex = default(KameraMovement.scoreAndIndex);
			if (level <= FacebookManager.ListaStructPrijatelja[k].scores.Count)
			{
				scoreAndIndex.index = k;
				scoreAndIndex.score = FacebookManager.ListaStructPrijatelja[k].scores[level - 1];
				if (scoreAndIndex.score == 0 && FacebookManager.ListaStructPrijatelja[k].PrijateljID != FacebookManager.User)
				{
					scoreAndIndex.score = -1;
				}
				if (FacebookManager.ListaStructPrijatelja[k].PrijateljID == FacebookManager.User)
				{
					int num = int.Parse(StagesParser.allLevels[StagesParser.currentLevel - 1].Split(new char[]
					{
						'#'
					})[2]);
					if (num > FacebookManager.ListaStructPrijatelja[k].scores[level - 1])
					{
						scoreAndIndex.score = num;
					}
				}
				list.Add(scoreAndIndex);
			}
		}
		KameraMovement.scoreAndIndex scoreAndIndex2 = default(KameraMovement.scoreAndIndex);
		default(KameraMovement.scoreAndIndex).score = 0;
		for (int l = 0; l < list.Count; l++)
		{
			scoreAndIndex2 = list[l];
			int index = l;
			bool flag = false;
			for (int m = l + 1; m < list.Count; m++)
			{
				if (scoreAndIndex2.score < list[m].score)
				{
					scoreAndIndex2 = list[m];
					index = m;
					flag = true;
				}
			}
			if (flag)
			{
				KameraMovement.scoreAndIndex value2 = list[l];
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
					Transform transform = this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN/Friends Level Win " + num2 + " HOLDER");
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
					})).GetComponent<Renderer>().material.mainTexture = FacebookManager.ListaStructPrijatelja[list[n].index].profilePicture;
					TextMesh component = transform.Find(string.Concat(new object[]
					{
						"Friends Level Win ",
						num2,
						"/Friends Level Win Picture ",
						num2,
						"/Points Number level win fb"
					})).GetComponent<TextMesh>();
					KameraMovement.scoreAndIndex scoreAndIndex3 = list[n];
					component.text = scoreAndIndex3.score.ToString();
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
					this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN/Friends Level Win " + num2 + " HOLDER/FB").gameObject.SetActive(true);
					this._GUI.Find(string.Concat(new object[]
					{
						"MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN/Friends Level Win ",
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
				this._GUI.Find(string.Concat(new object[]
				{
					"MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN/Friends Level Win ",
					num4,
					" HOLDER/Friends Level Win ",
					num4
				})).gameObject.SetActive(false);
			}
		}
		if (!flag2)
		{
			Transform transform = this._GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN/Friends Level Win 5 HOLDER");
			transform.Find("Friends Level Win 5/Friends Level Win Picture 5").GetComponent<Renderer>().material.mainTexture = FacebookManager.ListaStructPrijatelja[list[num3 - 1].index].profilePicture;
			TextMesh component2 = transform.Find("Friends Level Win 5/Friends Level Win Picture 5/Points Number level win fb").GetComponent<TextMesh>();
			KameraMovement.scoreAndIndex scoreAndIndex3 = list[num3 - 1];
			component2.text = scoreAndIndex3.score.ToString();
			transform.Find("Friends Level Win 5/Friends Level Win Picture 5/Position Number").GetComponent<TextMesh>().text = num3.ToString();
			transform.Find("Friends Level Win 5").GetComponent<SpriteRenderer>().sprite = transform.parent.Find("ReferencaYOU").GetComponent<SpriteRenderer>().sprite;
		}
		list.Clear();
	}

	// Token: 0x06002EF1 RID: 12017 RVA: 0x0017A4D4 File Offset: 0x001786D4
	private void prebaciStrelicuNaItem()
	{
		this.TutorialShop.transform.position = new Vector3(-20f, -105.5f, -75f);
		this.TutorialShop.transform.GetChild(0).Find("SpotLightMalaKocka2").localPosition = new Vector3(-1.6f, -2.39f, 0f);
		this.TutorialShop.transform.GetChild(0).Find("RedArrowHolder").localPosition = new Vector3(0.5f, 0.4f, -0.8f);
		this.TutorialShop.transform.GetChild(0).Find("RedArrowHolder").rotation = Quaternion.Euler(0f, 0f, -43f);
		this.TutorialShop.SetActive(true);
		this.TutorialShop.transform.GetChild(0).GetComponent<Animation>().Play();
		this.TutorialShop.transform.GetChild(0).Find("RedArrowHolder/RedArrow").GetComponent<Animation>().Play();
	}

	// Token: 0x06002EF2 RID: 12018 RVA: 0x00022C5B File Offset: 0x00020E5B
	private void spustiPopup()
	{
		this.popupZaSpustanje.localPosition += new Vector3(0f, -35f, 0f);
		this.popupZaSpustanje = null;
	}

	// Token: 0x06002EF3 RID: 12019 RVA: 0x00022C8E File Offset: 0x00020E8E
	private IEnumerator checkConnectionForLoginButton()
	{
		base.StartCoroutine(CheckInternetConnection.Instance.checkInternetConnection());
		while (!CheckInternetConnection.Instance.checkDone)
		{
			yield return null;
		}
		if (CheckInternetConnection.Instance.internetOK)
		{
			FacebookManager.KorisnikoviPodaciSpremni = false;
			ShopManagerFull.ShopInicijalizovan = false;
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
			if (!FB.IsLoggedIn)
			{
				FacebookManager.MestoPozivanjaLogina = 3;
				FacebookManager.FacebookObject.FacebookLogin();
			}
		}
		else
		{
			CheckInternetConnection.Instance.openPopup();
		}
		yield break;
	}

	// Token: 0x06002EF4 RID: 12020 RVA: 0x00022C9D File Offset: 0x00020E9D
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

	// Token: 0x06002EF5 RID: 12021 RVA: 0x00022CAC File Offset: 0x00020EAC
	private IEnumerator checkConnectionForWatchVideo()
	{
		base.StartCoroutine(CheckInternetConnection.Instance.checkInternetConnection());
		while (!CheckInternetConnection.Instance.checkDone)
		{
			yield return null;
		}
		if (CheckInternetConnection.Instance.internetOK)
		{
			StagesParser.sceneID = 1;
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

	// Token: 0x06002EF6 RID: 12022 RVA: 0x00022CBB File Offset: 0x00020EBB
	private IEnumerator checkConnectionForInviteFriend()
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

	// Token: 0x06002EF7 RID: 12023 RVA: 0x00022CCA File Offset: 0x00020ECA
	private IEnumerator checkConnectionForTelevizor()
	{
		base.StartCoroutine(CheckInternetConnection.Instance.checkInternetConnection());
		while (!CheckInternetConnection.Instance.checkDone)
		{
			yield return null;
		}
		if (CheckInternetConnection.Instance.internetOK)
		{
			this.popupZaSpustanje = this._GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni");
			base.Invoke("spustiPopup", 0.5f);
			this._GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni").GetComponent<Animator>().Play("ClosePopup");
			KameraMovement.makniPopup = 0;
			bool flag = false;
			string text = PlayerPrefs.GetString("WatchVideoWorld" + (StagesParser.currSetIndex + 1));
			if (PlayerPrefs.HasKey("WatchVideoWorld" + (StagesParser.currSetIndex + 1)))
			{
				string[] array = text.Split(new char[]
				{
					'#'
				});
				for (int i = 0; i < array.Length; i++)
				{
					if (int.Parse(array[i]) == this.trenutniTelevizor)
					{
						flag = true;
					}
				}
				if (!flag)
				{
					text = text + "#" + this.trenutniTelevizor;
					PlayerPrefs.SetString("WatchVideoWorld" + (StagesParser.currSetIndex + 1), text);
					PlayerPrefs.Save();
				}
				this.Televizori[this.trenutniTelevizor - 1].gameObject.SetActive(false);
			}
			else
			{
				this.Televizori[this.trenutniTelevizor - 1].gameObject.SetActive(false);
				PlayerPrefs.SetString("WatchVideoWorld" + (StagesParser.currSetIndex + 1), this.trenutniTelevizor.ToString());
				PlayerPrefs.Save();
			}
			if (!this.televizorIzabrao)
			{
				this.animator.Play("Running");
				base.StartCoroutine("KretanjeMajmunceta");
			}
			else
			{
				this.televizorIzabrao = false;
			}
			StagesParser.sceneID = 1;
		}
		else
		{
			CheckInternetConnection.Instance.openPopup();
		}
		yield break;
	}

	// Token: 0x06002EF8 RID: 12024 RVA: 0x0017A5F0 File Offset: 0x001787F0
	public void WatchVideoCallback(int value)
	{
		if (value == 1)
		{
			if (KameraMovement.makniPopup == 0)
			{
				StagesParser.currentMoney += this.televizorCenePoSvetovima[StagesParser.currSetIndex];
				PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
				PlayerPrefs.Save();
				base.StartCoroutine(StagesParser.Instance.moneyCounter(this.televizorCenePoSvetovima[StagesParser.currSetIndex], this._GUI.Find("INTERFACE HOLDER/_TopLeft/Coins/Coins Number").GetComponent<TextMesh>(), true));
				StagesParser.ServerUpdate = 1;
				return;
			}
			if (KameraMovement.makniPopup == 8)
			{
				StagesParser.currentMoney += StagesParser.watchVideoReward;
				PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
				PlayerPrefs.Save();
				base.StartCoroutine(StagesParser.Instance.moneyCounter(StagesParser.watchVideoReward, ShopManagerFull.ShopObject.transform.Find("Shop Interface/Coins/Coins Number").GetComponent<TextMesh>(), true));
				return;
			}
		}
		else if (value != 2 && value == 3)
		{
			CheckInternetConnection.Instance.NoVideosAvailable_OpenPopup();
		}
	}

	// Token: 0x040029E9 RID: 10729
	private GameObject Kamera;

	// Token: 0x040029EA RID: 10730
	private Camera guiCamera;

	// Token: 0x040029EB RID: 10731
	private int currentLevelStars;

	// Token: 0x040029EC RID: 10732
	private string clickedItem;

	// Token: 0x040029ED RID: 10733
	private string releasedItem;

	// Token: 0x040029EE RID: 10734
	private float trajanjeKlika;

	// Token: 0x040029EF RID: 10735
	private float pomerajOdKlika_X;

	// Token: 0x040029F0 RID: 10736
	private float pomerajOdKlika_Y;

	// Token: 0x040029F1 RID: 10737
	private float startX;

	// Token: 0x040029F2 RID: 10738
	private float startY;

	// Token: 0x040029F3 RID: 10739
	private float endX;

	// Token: 0x040029F4 RID: 10740
	private float endY;

	// Token: 0x040029F5 RID: 10741
	private float pomerajX;

	// Token: 0x040029F6 RID: 10742
	private float pomerajY;

	// Token: 0x040029F7 RID: 10743
	private bool moved;

	// Token: 0x040029F8 RID: 10744
	private bool released;

	// Token: 0x040029F9 RID: 10745
	private bool bounce;

	// Token: 0x040029FA RID: 10746
	private float levaGranica = 9f;

	// Token: 0x040029FB RID: 10747
	private float desnaGranica = 31.95f;

	// Token: 0x040029FC RID: 10748
	private float donjaGranica = -15.35f;

	// Token: 0x040029FD RID: 10749
	private float gornjaGranica = -5.2f;

	// Token: 0x040029FE RID: 10750
	private Transform lifeManager;

	// Token: 0x040029FF RID: 10751
	private Vector2 prevDist = new Vector2(0f, 0f);

	// Token: 0x04002A00 RID: 10752
	private Vector2 curDist = new Vector2(0f, 0f);

	// Token: 0x04002A01 RID: 10753
	private float touchDelta;

	// Token: 0x04002A02 RID: 10754
	private float touchDeltaY;

	// Token: 0x04002A03 RID: 10755
	private float minPinchSpeed = 0.001f;

	// Token: 0x04002A04 RID: 10756
	private float varianceInDistances = 9f;

	// Token: 0x04002A05 RID: 10757
	private float speedTouch0;

	// Token: 0x04002A06 RID: 10758
	private float speedTouch1;

	// Token: 0x04002A07 RID: 10759
	private float moveFactor = 0.07f;

	// Token: 0x04002A08 RID: 10760
	private bool zoom;

	// Token: 0x04002A09 RID: 10761
	public Transform doleLevo;

	// Token: 0x04002A0A RID: 10762
	public Transform doleDesno;

	// Token: 0x04002A0B RID: 10763
	public Transform goreLevo;

	// Token: 0x04002A0C RID: 10764
	public Transform goreDesno;

	// Token: 0x04002A0D RID: 10765
	private bool pomeriKameruUGranice;

	// Token: 0x04002A0E RID: 10766
	private float ortSize;

	// Token: 0x04002A0F RID: 10767
	private float aspect;

	// Token: 0x04002A10 RID: 10768
	private Vector3 ivicaEkrana;

	// Token: 0x04002A11 RID: 10769
	private Transform holderMajmun;

	// Token: 0x04002A12 RID: 10770
	private Transform majmun;

	// Token: 0x04002A13 RID: 10771
	private Animator animator;

	// Token: 0x04002A14 RID: 10772
	private Vector3[] angles;

	// Token: 0x04002A15 RID: 10773
	public int angleIndex;

	// Token: 0x04002A16 RID: 10774
	private Vector3 newAngle;

	// Token: 0x04002A17 RID: 10775
	private Vector3 monkeyDestination;

	// Token: 0x04002A18 RID: 10776
	public Transform izmedjneTacke;

	// Token: 0x04002A19 RID: 10777
	private bool majmunceSeMrda;

	// Token: 0x04002A1A RID: 10778
	private int monkeyCurrentLevelIndex;

	// Token: 0x04002A1B RID: 10779
	private int monkeyDestinationLevelIndex;

	// Token: 0x04002A1C RID: 10780
	private int levelName;

	// Token: 0x04002A1D RID: 10781
	private bool kretanjeDoKovcega;

	// Token: 0x04002A1E RID: 10782
	private Transform trenutniKovceg;

	// Token: 0x04002A1F RID: 10783
	private Transform kovcegNaPocetku;

	// Token: 0x04002A20 RID: 10784
	public Transform[] BonusNivoi;

	// Token: 0x04002A21 RID: 10785
	private int zadnjiOtkljucanNivo_proveraZaBonus;

	// Token: 0x04002A22 RID: 10786
	private bool televizorNaMapi;

	// Token: 0x04002A23 RID: 10787
	public GameObject quad;

	// Token: 0x04002A24 RID: 10788
	private int watchVideoIndex_pom;

	// Token: 0x04002A25 RID: 10789
	public Transform[] Televizori;

	// Token: 0x04002A26 RID: 10790
	private int trenutniTelevizor;

	// Token: 0x04002A27 RID: 10791
	private Transform _GUI;

	// Token: 0x04002A28 RID: 10792
	private bool televizorIzabrao;

	// Token: 0x04002A29 RID: 10793
	private bool prejasiTelevizor;

	// Token: 0x04002A2A RID: 10794
	public static Renderer aktivnaIkonicaMisija1;

	// Token: 0x04002A2B RID: 10795
	public static Renderer aktivnaIkonicaMisija2;

	// Token: 0x04002A2C RID: 10796
	public static Renderer aktivnaIkonicaMisija3;

	// Token: 0x04002A2D RID: 10797
	private float guiCameraY;

	// Token: 0x04002A2E RID: 10798
	private int[] televizorCenePoSvetovima;

	// Token: 0x04002A2F RID: 10799
	public static int makniPopup;

	// Token: 0x04002A30 RID: 10800
	public GameObject TutorialShopPrefab;

	// Token: 0x04002A31 RID: 10801
	private GameObject TutorialShop;

	// Token: 0x04002A32 RID: 10802
	private GameObject shop;

	// Token: 0x04002A33 RID: 10803
	private int reward1Type;

	// Token: 0x04002A34 RID: 10804
	private int reward2Type;

	// Token: 0x04002A35 RID: 10805
	private int reward3Type;

	// Token: 0x04002A36 RID: 10806
	private int kolicinaReward1;

	// Token: 0x04002A37 RID: 10807
	private int kolicinaReward2;

	// Token: 0x04002A38 RID: 10808
	private int kolicinaReward3;

	// Token: 0x04002A39 RID: 10809
	private string cetvrtiKovcegNagrada = "";

	// Token: 0x04002A3A RID: 10810
	private int indexNagradeZaCetvrtiKovceg = -1;

	// Token: 0x04002A3B RID: 10811
	private Transform popupZaSpustanje;

	// Token: 0x04002A3C RID: 10812
	private bool popunioSlike;

	// Token: 0x02000738 RID: 1848
	private struct scoreAndIndex
	{
		// Token: 0x06002EFC RID: 12028 RVA: 0x00022CD9 File Offset: 0x00020ED9
		public scoreAndIndex(int score, int index)
		{
			this.score = score;
			this.index = index;
		}

		// Token: 0x04002A3D RID: 10813
		public int score;

		// Token: 0x04002A3E RID: 10814
		public int index;
	}
}
