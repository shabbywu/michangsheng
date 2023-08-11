using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class KameraMovement : MonoBehaviour
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

	private GameObject Kamera;

	private Camera guiCamera;

	private int currentLevelStars;

	private string clickedItem;

	private string releasedItem;

	private float trajanjeKlika;

	private float pomerajOdKlika_X;

	private float pomerajOdKlika_Y;

	private float startX;

	private float startY;

	private float endX;

	private float endY;

	private float pomerajX;

	private float pomerajY;

	private bool moved;

	private bool released;

	private bool bounce;

	private float levaGranica = 9f;

	private float desnaGranica = 31.95f;

	private float donjaGranica = -15.35f;

	private float gornjaGranica = -5.2f;

	private Transform lifeManager;

	private Vector2 prevDist = new Vector2(0f, 0f);

	private Vector2 curDist = new Vector2(0f, 0f);

	private float touchDelta;

	private float touchDeltaY;

	private float minPinchSpeed = 0.001f;

	private float varianceInDistances = 9f;

	private float speedTouch0;

	private float speedTouch1;

	private float moveFactor = 0.07f;

	private bool zoom;

	public Transform doleLevo;

	public Transform doleDesno;

	public Transform goreLevo;

	public Transform goreDesno;

	private bool pomeriKameruUGranice;

	private float ortSize;

	private float aspect;

	private Vector3 ivicaEkrana;

	private Transform holderMajmun;

	private Transform majmun;

	private Animator animator;

	private Vector3[] angles;

	public int angleIndex;

	private Vector3 newAngle;

	private Vector3 monkeyDestination;

	public Transform izmedjneTacke;

	private bool majmunceSeMrda;

	private int monkeyCurrentLevelIndex;

	private int monkeyDestinationLevelIndex;

	private int levelName;

	private bool kretanjeDoKovcega;

	private Transform trenutniKovceg;

	private Transform kovcegNaPocetku;

	public Transform[] BonusNivoi;

	private int zadnjiOtkljucanNivo_proveraZaBonus;

	private bool televizorNaMapi;

	public GameObject quad;

	private int watchVideoIndex_pom;

	public Transform[] Televizori;

	private int trenutniTelevizor;

	private Transform _GUI;

	private bool televizorIzabrao;

	private bool prejasiTelevizor;

	public static Renderer aktivnaIkonicaMisija1;

	public static Renderer aktivnaIkonicaMisija2;

	public static Renderer aktivnaIkonicaMisija3;

	private float guiCameraY;

	private int[] televizorCenePoSvetovima;

	public static int makniPopup;

	public GameObject TutorialShopPrefab;

	private GameObject TutorialShop;

	private GameObject shop;

	private int reward1Type;

	private int reward2Type;

	private int reward3Type;

	private int kolicinaReward1;

	private int kolicinaReward2;

	private int kolicinaReward3;

	private string cetvrtiKovcegNagrada = "";

	private int indexNagradeZaCetvrtiKovceg = -1;

	private Transform popupZaSpustanje;

	private bool popunioSlike;

	private void Awake()
	{
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		if (Advertisement.isSupported)
		{
			Advertisement.Initialize(StagesParser.Instance.UnityAdsVideoGameID);
		}
		else
		{
			Debug.Log((object)"UNITYADS Platform not supported");
		}
		makniPopup = 0;
		string[] array = PlayerPrefs.GetString("WatchVideoWorld" + (StagesParser.currSetIndex + 1)).Split(new char[1] { '#' });
		_GUI = GameObject.Find("_GUI").transform;
		if (Televizori != null)
		{
			for (int i = 0; i < array.Length; i++)
			{
				for (int j = 0; j < Televizori.Length; j++)
				{
					if (array[i] == ((Object)Televizori[j]).name.Substring(((Object)Televizori[j]).name.Length - 1))
					{
						((Component)Televizori[j]).gameObject.SetActive(false);
					}
				}
			}
		}
		angles = (Vector3[])(object)new Vector3[8]
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
		ortSize = Camera.main.orthographicSize;
		aspect = Camera.main.aspect;
		Kamera = GameObject.Find("Main Camera");
		guiCamera = GameObject.Find("GUICamera").GetComponent<Camera>();
		holderMajmun = GameObject.Find("HolderMajmun").transform;
		majmun = holderMajmun.GetChild(0).GetChild(0);
		animator = ((Component)majmun).GetComponent<Animator>();
		guiCameraY = ((Component)guiCamera).transform.position.y;
		levaGranica = doleLevo.position.x + ortSize * aspect;
		desnaGranica = doleDesno.position.x - ortSize * aspect;
		donjaGranica = doleLevo.position.y + ortSize;
		gornjaGranica = goreDesno.position.y - ortSize;
		if (StagesParser.otvaraoShopNekad == 2 && StagesParser.trenutniNivoNaOstrvu[StagesParser.currSetIndex] == 3 && StagesParser.currSetIndex == 0)
		{
			makniPopup = 6;
			((MonoBehaviour)this).StartCoroutine(PokaziMuCustomize());
		}
		InitLevels(refreshed: false);
		((Component)Camera.main).transform.position = new Vector3(Mathf.Clamp(GameObject.Find("Level" + StagesParser.trenutniNivoNaOstrvu[StagesParser.currSetIndex]).transform.position.x, levaGranica, desnaGranica), Mathf.Clamp(GameObject.Find("Level" + StagesParser.trenutniNivoNaOstrvu[StagesParser.currSetIndex]).transform.position.y, donjaGranica, gornjaGranica), ((Component)Camera.main).transform.position.z);
		if (StagesParser.pozicijaMajmuncetaNaMapi == Vector3.zero)
		{
			if (StagesParser.SetsInGame[StagesParser.currSetIndex].CurrentStarsInStageNEW > 0)
			{
				holderMajmun.position = izmedjneTacke.Find(StagesParser.trenutniNivoNaOstrvu[StagesParser.currSetIndex].ToString()).position;
				monkeyCurrentLevelIndex = GetMapLevelIndex(StagesParser.trenutniNivoNaOstrvu[StagesParser.currSetIndex]);
			}
		}
		else
		{
			holderMajmun.position = StagesParser.pozicijaMajmuncetaNaMapi;
			kovcegNaPocetku = (trenutniKovceg = pronadjiKovceg(StagesParser.bonusName));
			string[] array2 = StagesParser.bonusName.Split(new char[1] { '_' });
			monkeyCurrentLevelIndex = GetMapLevelIndex(int.Parse(array2[2]));
			((Component)Camera.main).transform.position = new Vector3(Mathf.Clamp(holderMajmun.position.x, levaGranica, desnaGranica), Mathf.Clamp(holderMajmun.position.y, donjaGranica, gornjaGranica), ((Component)Camera.main).transform.position.z);
			if (!StagesParser.dodatnaProveraIzasaoIzBonusa)
			{
				((Component)kovcegNaPocetku.Find("Kovceg Zatvoren")).GetComponent<Animator>().Play("Kovceg Otvaranje");
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_Otvaranje_Kovcega();
				}
				((Component)kovcegNaPocetku).GetComponent<Collider>().enabled = false;
				_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/PomocniColliderKodOtvaranjaKovcega").localScale = new Vector3(200f, 130f, 1f);
				PodesiReward();
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

	private void PodesiReward()
	{
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		if (StagesParser.bonusID == 4)
		{
			reward1Type = 4;
			reward2Type = 0;
			reward3Type = 0;
			((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward2")).gameObject.SetActive(false);
			((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward3")).gameObject.SetActive(false);
			IspitajItem();
		}
		else if (StagesParser.currSetIndex == 0)
		{
			reward2Type = 0;
			reward3Type = 0;
			((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward2")).gameObject.SetActive(false);
			((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward3")).gameObject.SetActive(false);
			kolicinaReward1 = 1;
			((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1/Count")).GetComponent<TextMesh>().text = kolicinaReward1.ToString();
			if (StagesParser.powerup_magnets <= StagesParser.powerup_doublecoins)
			{
				if (StagesParser.powerup_magnets <= StagesParser.powerup_shields)
				{
					reward1Type = 1;
					((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1")).GetComponent<SpriteRenderer>().sprite = ((Component)_GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Magnet/Plavi Bedz/Magnet Icon")).GetComponent<SpriteRenderer>().sprite;
				}
				else
				{
					reward1Type = 3;
					((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1")).GetComponent<SpriteRenderer>().sprite = ((Component)_GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Shield/Plavi Bedz/Shield Icon")).GetComponent<SpriteRenderer>().sprite;
				}
			}
			else if (StagesParser.powerup_doublecoins <= StagesParser.powerup_shields)
			{
				reward1Type = 2;
				((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1")).GetComponent<SpriteRenderer>().sprite = ((Component)_GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Double Coins/Plavi Bedz/Double Coins Icon")).GetComponent<SpriteRenderer>().sprite;
			}
			else
			{
				reward1Type = 3;
				((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1")).GetComponent<SpriteRenderer>().sprite = ((Component)_GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Shield/Plavi Bedz/Shield Icon")).GetComponent<SpriteRenderer>().sprite;
			}
		}
		else if (StagesParser.currSetIndex < 3)
		{
			reward3Type = 0;
			kolicinaReward1 = 1;
			kolicinaReward2 = 1;
			((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1/Count")).GetComponent<TextMesh>().text = kolicinaReward1.ToString();
			((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward2/Count")).GetComponent<TextMesh>().text = kolicinaReward2.ToString();
			_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1").localPosition = new Vector3(-0.75f, 0.55f, -0.5f);
			_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward2").localPosition = new Vector3(0.75f, 0.55f, -0.5f);
			((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward3")).gameObject.SetActive(false);
			if (StagesParser.powerup_magnets <= StagesParser.powerup_doublecoins)
			{
				if (StagesParser.powerup_shields <= StagesParser.powerup_doublecoins)
				{
					reward1Type = 1;
					((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1")).GetComponent<SpriteRenderer>().sprite = ((Component)_GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Magnet/Plavi Bedz/Magnet Icon")).GetComponent<SpriteRenderer>().sprite;
					reward2Type = 3;
					((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward2")).GetComponent<SpriteRenderer>().sprite = ((Component)_GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Shield/Plavi Bedz/Shield Icon")).GetComponent<SpriteRenderer>().sprite;
				}
				else
				{
					reward1Type = 1;
					((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1")).GetComponent<SpriteRenderer>().sprite = ((Component)_GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Magnet/Plavi Bedz/Magnet Icon")).GetComponent<SpriteRenderer>().sprite;
					reward2Type = 2;
					((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward2")).GetComponent<SpriteRenderer>().sprite = ((Component)_GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Double Coins/Plavi Bedz/Double Coins Icon")).GetComponent<SpriteRenderer>().sprite;
				}
			}
			else if (StagesParser.powerup_magnets <= StagesParser.powerup_shields)
			{
				reward1Type = 2;
				((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1")).GetComponent<SpriteRenderer>().sprite = ((Component)_GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Double Coins/Plavi Bedz/Double Coins Icon")).GetComponent<SpriteRenderer>().sprite;
				reward2Type = 1;
				((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward2")).GetComponent<SpriteRenderer>().sprite = ((Component)_GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Magnet/Plavi Bedz/Magnet Icon")).GetComponent<SpriteRenderer>().sprite;
			}
			else
			{
				reward1Type = 2;
				((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1")).GetComponent<SpriteRenderer>().sprite = ((Component)_GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Double Coins/Plavi Bedz/Double Coins Icon")).GetComponent<SpriteRenderer>().sprite;
				reward2Type = 3;
				((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward2")).GetComponent<SpriteRenderer>().sprite = ((Component)_GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Shield/Plavi Bedz/Shield Icon")).GetComponent<SpriteRenderer>().sprite;
			}
		}
		else
		{
			kolicinaReward1 = 1;
			kolicinaReward2 = 1;
			kolicinaReward3 = 1;
			((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1/Count")).GetComponent<TextMesh>().text = kolicinaReward1.ToString();
			((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward2/Count")).GetComponent<TextMesh>().text = kolicinaReward2.ToString();
			((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward3/Count")).GetComponent<TextMesh>().text = kolicinaReward3.ToString();
			((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1")).GetComponent<SpriteRenderer>().sprite = ((Component)_GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Magnet/Plavi Bedz/Magnet Icon")).GetComponent<SpriteRenderer>().sprite;
			((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward2")).GetComponent<SpriteRenderer>().sprite = ((Component)_GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Shield/Plavi Bedz/Shield Icon")).GetComponent<SpriteRenderer>().sprite;
			((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward3")).GetComponent<SpriteRenderer>().sprite = ((Component)_GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Double Coins/Plavi Bedz/Double Coins Icon")).GetComponent<SpriteRenderer>().sprite;
			reward1Type = 1;
			reward2Type = 2;
			reward3Type = 3;
		}
		((MonoBehaviour)this).Invoke("PozoviRewardPopup", 4.15f);
	}

	private void PozoviRewardPopup()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		Transform obj = _GUI.Find("REWARD HOLDER/AnimationHolderGlavni");
		obj.localPosition += new Vector3(0f, 35f, 0f);
		((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni")).GetComponent<Animator>().Play("OpenPopup");
		makniPopup = 7;
	}

	private void RefreshScene()
	{
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		StagesParser.lastUnlockedWorldIndex = 0;
		for (int i = 0; i < StagesParser.totalSets; i++)
		{
			StagesParser.unlockedWorlds[i] = false;
			if (StagesParser.currentStarsNEW >= StagesParser.SetsInGame[i].StarRequirement && i > 0 && int.Parse(StagesParser.allLevels[(i - 1) * 20 + 19].Split(new char[1] { '#' })[1]) > 0)
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
			((MonoBehaviour)this).StartCoroutine(UcitajOstrvo("All Maps"));
			return;
		}
		InitLevels(refreshed: true);
		if (StagesParser.pozicijaMajmuncetaNaMapi == Vector3.zero)
		{
			if (StagesParser.SetsInGame[StagesParser.currSetIndex].CurrentStarsInStageNEW > 0)
			{
				holderMajmun.position = izmedjneTacke.Find(StagesParser.trenutniNivoNaOstrvu[StagesParser.currSetIndex].ToString()).position;
				monkeyCurrentLevelIndex = GetMapLevelIndex(StagesParser.trenutniNivoNaOstrvu[StagesParser.currSetIndex]);
			}
			else
			{
				holderMajmun.position = izmedjneTacke.Find("1").position;
				monkeyCurrentLevelIndex = GetMapLevelIndex(1);
			}
		}
		else
		{
			holderMajmun.position = StagesParser.pozicijaMajmuncetaNaMapi;
			kovcegNaPocetku = (trenutniKovceg = pronadjiKovceg(StagesParser.bonusName));
			string[] array = StagesParser.bonusName.Split(new char[1] { '_' });
			monkeyCurrentLevelIndex = GetMapLevelIndex(int.Parse(array[2]));
			((Component)Camera.main).transform.position = new Vector3(Mathf.Clamp(holderMajmun.position.x, levaGranica, desnaGranica), Mathf.Clamp(holderMajmun.position.y, donjaGranica, gornjaGranica), ((Component)Camera.main).transform.position.z);
			if (!StagesParser.dodatnaProveraIzasaoIzBonusa)
			{
				((Component)kovcegNaPocetku.Find("Kovceg Zatvoren")).GetComponent<Animator>().Play("Kovceg Otvaranje");
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_Otvaranje_Kovcega();
				}
				((Component)kovcegNaPocetku).GetComponent<Collider>().enabled = false;
				_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/PomocniColliderKodOtvaranjaKovcega").localScale = new Vector3(200f, 130f, 1f);
				PodesiReward();
			}
			else
			{
				StagesParser.dodatnaProveraIzasaoIzBonusa = false;
			}
		}
		((Component)_GUI.Find("INTERFACE HOLDER/_TopLeft/Coins/Coins Number")).GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
		((Component)_GUI.Find("INTERFACE HOLDER/_TopLeft/Coins/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		((Component)_GUI.Find("INTERFACE HOLDER/_TopLeft/PTS/PTS Number")).GetComponent<TextMesh>().text = StagesParser.currentPoints.ToString();
		((Component)_GUI.Find("INTERFACE HOLDER/_TopLeft/PTS/PTS Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		((Component)_GUI.Find("INTERFACE HOLDER/_TopLeft/BananaHolder/Bananas/Banana Number")).GetComponent<TextMesh>().text = StagesParser.currentBananas.ToString();
		((Component)_GUI.Find("INTERFACE HOLDER/_TopLeft/BananaHolder/Bananas/Banana Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		((Component)_GUI.Find("INTERFACE HOLDER/FB Login/Text/Number")).GetComponent<TextMesh>().text = "+" + StagesParser.LoginReward;
		((Component)_GUI.Find("INTERFACE HOLDER/FB Login/Text/Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		((Component)_GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/WatchVideoReward")).GetComponent<TextMesh>().text = "+" + televizorCenePoSvetovima[StagesParser.currSetIndex];
		((Component)_GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/WatchVideoReward")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		if (reward1Type > 0)
		{
			((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1/Count")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		}
		if (reward2Type > 0)
		{
			((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward2/Count")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		}
		if (reward3Type > 0)
		{
			((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward3/Count")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		}
		((Component)_GUI.Find("INTERFACE HOLDER/TotalStars/Stars Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		changeLanguage();
		CheckInternetConnection.Instance.refreshText();
		StagesParser.LoadingPoruke.Clear();
		StagesParser.RedniBrojSlike.Clear();
		StagesParser.Instance.UcitajLoadingPoruke();
	}

	private void Start()
	{
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		changeLanguage();
		if ((Object)(object)Loading.Instance != (Object)null)
		{
			((MonoBehaviour)this).StartCoroutine(Loading.Instance.UcitanaScena(guiCamera, 2, 0f));
		}
		else
		{
			((Component)_GUI.Find("LOADING HOLDER NEW/Loading Animation Vrata")).GetComponent<Animator>().Play("Loading Zidovi Odlazak");
		}
		((Component)_GUI.Find("INTERFACE HOLDER/_TopLeft/Coins/Coins Number")).GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
		((Component)_GUI.Find("INTERFACE HOLDER/_TopLeft/Coins/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		((Component)_GUI.Find("INTERFACE HOLDER/_TopLeft/PTS/PTS Number")).GetComponent<TextMesh>().text = StagesParser.currentPoints.ToString();
		((Component)_GUI.Find("INTERFACE HOLDER/_TopLeft/PTS/PTS Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		((Component)_GUI.Find("INTERFACE HOLDER/_TopLeft/BananaHolder/Bananas/Banana Number")).GetComponent<TextMesh>().text = StagesParser.currentBananas.ToString();
		((Component)_GUI.Find("INTERFACE HOLDER/_TopLeft/BananaHolder/Bananas/Banana Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		((Component)_GUI.Find("INTERFACE HOLDER/FB Login/Text/Number")).GetComponent<TextMesh>().text = "+" + StagesParser.LoginReward;
		((Component)_GUI.Find("INTERFACE HOLDER/FB Login/Text/Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		((Component)_GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/WatchVideoReward")).GetComponent<TextMesh>().text = "+" + televizorCenePoSvetovima[StagesParser.currSetIndex];
		((Component)_GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/WatchVideoReward")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		_GUI.Find("INTERFACE HOLDER/_TopLeft").position = new Vector3(guiCamera.ViewportToWorldPoint(Vector3.zero).x, _GUI.Find("INTERFACE HOLDER/_TopLeft").position.y, _GUI.Find("INTERFACE HOLDER/_TopLeft").position.z);
		_GUI.Find("INTERFACE HOLDER/FB Login").position = new Vector3(guiCamera.ViewportToWorldPoint(new Vector3(0.91f, 0f, 0f)).x, _GUI.Find("INTERFACE HOLDER/FB Login").position.y, _GUI.Find("INTERFACE HOLDER/FB Login").position.z);
		_GUI.Find("INTERFACE HOLDER/TotalStars").position = new Vector3(guiCamera.ViewportToWorldPoint(new Vector3(0.89f, 0f, 0f)).x, _GUI.Find("INTERFACE HOLDER/TotalStars").position.y, _GUI.Find("INTERFACE HOLDER/TotalStars").position.z);
		((Component)_GUI.Find("INTERFACE HOLDER/TotalStars/Stars Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		((Component)((Component)ShopManagerFull.ShopObject).transform.Find("1 Free Coins/Free Coins Tabovi/ShopFCWatchVideo/Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMesh>().text = StagesParser.watchVideoReward.ToString();
		((Component)((Component)ShopManagerFull.ShopObject).transform.Find("1 Free Coins/Free Coins Tabovi/ShopFCWatchVideo/Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: false);
		if (reward1Type > 0)
		{
			((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1/Count")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		}
		if (reward2Type > 0)
		{
			((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward2/Count")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		}
		if (reward3Type > 0)
		{
			((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward3/Count")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		}
		GameObject.Find("NotAvailableText").SetActive(false);
		if (FB.IsLoggedIn)
		{
			GameObject.Find("FB Login").SetActive(false);
		}
		if (StagesParser.obucenSeLogovaoNaDrugojSceni)
		{
			StagesParser.obucenSeLogovaoNaDrugojSceni = false;
			((MonoBehaviour)this).Invoke("MaliDelayPreNegoDaSePozoveCompareScoresShopDeo", 1f);
		}
	}

	private void MaliDelayPreNegoDaSePozoveCompareScoresShopDeo()
	{
		StagesParser.Instance.ShopDeoIzCompareScores();
	}

	private void UvecavajBrojac()
	{
		if (angleIndex > 7)
		{
			angleIndex = 0;
		}
		angleIndex++;
	}

	private void InitLevels(bool refreshed)
	{
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		StagesParser.SetsInGame[StagesParser.currSetIndex].CurrentStarsInStageNEW = 0;
		if (StagesParser.zadnjiOtkljucanNivo != 0)
		{
			GameObject.Find("LevelsWorld" + StagesParser.currSetIndex + "/Level" + StagesParser.zadnjiOtkljucanNivo + "/Level" + StagesParser.zadnjiOtkljucanNivo + "Move").GetComponent<Animator>().Play("KatanacExplosion");
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_OtkljucavanjeNivoa();
			}
		}
		for (int i = 0; i < StagesParser.SetsInGame[StagesParser.currSetIndex].StagesOnSet; i++)
		{
			string[] array = StagesParser.allLevels[StagesParser.currSetIndex * 20 + i].Split(new char[1] { '#' });
			StagesParser.SetsInGame[StagesParser.currSetIndex].SetStarOnStage(i, int.Parse(array[1]));
			if (int.Parse(array[1]) > -1)
			{
				StagesParser.SetsInGame[StagesParser.currSetIndex].CurrentStarsInStageNEW += int.Parse(array[1]);
			}
			currentLevelStars = int.Parse(array[1]);
			if (currentLevelStars == -1)
			{
				GameObject.Find("LevelsWorld" + StagesParser.currSetIndex + "/Level" + (i + 1) + "/Level" + (i + 1) + "Move").GetComponent<SpriteRenderer>().sprite = GameObject.Find("RefCardClose").GetComponent<SpriteRenderer>().sprite;
				((Component)GameObject.Find("LevelsWorld" + StagesParser.currSetIndex + "/Level" + (i + 1) + "/Level" + (i + 1) + "Move").transform.Find("TextNumberLevel")).GetComponent<TextMesh>().text = string.Empty;
				((Component)GameObject.Find("LevelsWorld" + StagesParser.currSetIndex + "/Level" + (i + 1) + "/Level" + (i + 1) + "Move").transform.Find("TextNumberLevel").GetChild(0)).GetComponent<TextMesh>().text = string.Empty;
				((Component)GameObject.Find("LevelsWorld" + StagesParser.currSetIndex + "/Level" + (i + 1) + "/Level" + (i + 1) + "Move").transform.Find("Katanac")).GetComponent<Renderer>().enabled = true;
				if (!((Component)GameObject.Find("LevelsWorld" + StagesParser.currSetIndex + "/Level" + (i + 1) + "/Level" + (i + 1) + "Move").transform.Find("Katanac")).gameObject.activeSelf)
				{
					((Behaviour)GameObject.Find("LevelsWorld" + StagesParser.currSetIndex + "/Level" + (i + 1) + "/Level" + (i + 1) + "Move").GetComponent<Animator>()).enabled = false;
					((Component)GameObject.Find("LevelsWorld" + StagesParser.currSetIndex + "/Level" + (i + 1) + "/Level" + (i + 1) + "Move").transform.Find("Katanac")).gameObject.SetActive(true);
				}
				continue;
			}
			if (i + 1 != StagesParser.zadnjiOtkljucanNivo || StagesParser.pozicijaMajmuncetaNaMapi != Vector3.zero)
			{
				GameObject.Find("LevelsWorld" + StagesParser.currSetIndex + "/Level" + (i + 1) + "/Level" + (i + 1) + "Move").GetComponent<SpriteRenderer>().sprite = GameObject.Find("RefCardStar" + currentLevelStars).GetComponent<SpriteRenderer>().sprite;
				((Component)GameObject.Find("LevelsWorld" + StagesParser.currSetIndex + "/Level" + (i + 1) + "/Level" + (i + 1) + "Move").transform.Find("TextNumberLevel")).GetComponent<TextMesh>().text = (i + 1).ToString();
				((Component)GameObject.Find("LevelsWorld" + StagesParser.currSetIndex + "/Level" + (i + 1) + "/Level" + (i + 1) + "Move").transform.Find("Katanac")).GetComponent<Renderer>().enabled = false;
				if (currentLevelStars == 0)
				{
					GameObject.Find("LevelsWorld" + StagesParser.currSetIndex + "/Level" + (i + 1) + "/Level" + (i + 1) + "Move").GetComponent<Animator>().Play("NewLevelLoop");
				}
			}
			zadnjiOtkljucanNivo_proveraZaBonus = i + 1;
		}
		((Component)_GUI.Find("INTERFACE HOLDER/TotalStars/Stars Number")).GetComponent<TextMesh>().text = StagesParser.SetsInGame[StagesParser.currSetIndex].CurrentStarsInStageNEW + "/" + StagesParser.SetsInGame[StagesParser.currSetIndex].StagesOnSet * 3;
		string[] array2 = StagesParser.bonusLevels.Split(new char[1] { '_' })[StagesParser.currSetIndex].Split(new char[1] { '#' });
		for (int j = 0; j < BonusNivoi.Length; j++)
		{
			if (int.Parse(array2[j]) > -1)
			{
				((Component)BonusNivoi[j].Find("GateClosed")).GetComponent<Renderer>().enabled = false;
				((Component)BonusNivoi[j].Find("GateOpen")).GetComponent<Renderer>().enabled = true;
				if (int.Parse(array2[j]) > 0)
				{
					Transform obj = BonusNivoi[j].Find("Kovceg Zatvoren");
					((Component)obj.Find("Kovceg Otvoren")).GetComponent<Renderer>().enabled = true;
					((Component)obj.Find("Kovceg Zatvoren")).GetComponent<Renderer>().enabled = false;
					((Component)obj).GetComponent<Animator>().Play("Kovceg  Otvoren Idle");
					((Component)obj.parent).GetComponent<Collider>().enabled = false;
				}
			}
		}
		StagesParser.zadnjiOtkljucanNivo = 0;
	}

	private void Update()
	{
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Invalid comparison between Unknown and I4
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Invalid comparison between Unknown and I4
		//IL_058f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0594: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_0443: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_0489: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c30: Unknown result type (might be due to invalid IL or missing references)
		//IL_0878: Unknown result type (might be due to invalid IL or missing references)
		//IL_0894: Unknown result type (might be due to invalid IL or missing references)
		//IL_08af: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0817: Unknown result type (might be due to invalid IL or missing references)
		//IL_0834: Unknown result type (might be due to invalid IL or missing references)
		//IL_06df: Unknown result type (might be due to invalid IL or missing references)
		//IL_070a: Unknown result type (might be due to invalid IL or missing references)
		//IL_063b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0640: Unknown result type (might be due to invalid IL or missing references)
		//IL_0645: Unknown result type (might be due to invalid IL or missing references)
		//IL_0650: Unknown result type (might be due to invalid IL or missing references)
		//IL_0615: Unknown result type (might be due to invalid IL or missing references)
		//IL_061b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0620: Unknown result type (might be due to invalid IL or missing references)
		//IL_0625: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_090f: Unknown result type (might be due to invalid IL or missing references)
		//IL_091f: Unknown result type (might be due to invalid IL or missing references)
		//IL_074e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0753: Unknown result type (might be due to invalid IL or missing references)
		//IL_0758: Unknown result type (might be due to invalid IL or missing references)
		//IL_0763: Unknown result type (might be due to invalid IL or missing references)
		//IL_0727: Unknown result type (might be due to invalid IL or missing references)
		//IL_072d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0732: Unknown result type (might be due to invalid IL or missing references)
		//IL_0737: Unknown result type (might be due to invalid IL or missing references)
		//IL_0680: Unknown result type (might be due to invalid IL or missing references)
		//IL_066d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0673: Unknown result type (might be due to invalid IL or missing references)
		//IL_0678: Unknown result type (might be due to invalid IL or missing references)
		//IL_067d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0794: Unknown result type (might be due to invalid IL or missing references)
		//IL_0780: Unknown result type (might be due to invalid IL or missing references)
		//IL_0786: Unknown result type (might be due to invalid IL or missing references)
		//IL_078b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0790: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a03: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ade: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bcd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bfc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f70: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f81: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f86: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da8: Unknown result type (might be due to invalid IL or missing references)
		//IL_109f: Unknown result type (might be due to invalid IL or missing references)
		//IL_10b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_10b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_110e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ebf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed8: Unknown result type (might be due to invalid IL or missing references)
		//IL_13c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_13cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ea5: Unknown result type (might be due to invalid IL or missing references)
		if (Input.GetKeyUp((KeyCode)27))
		{
			if (makniPopup == 1)
			{
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_Button_GoBack();
				}
				popupZaSpustanje = _GUI.Find("MISSION HOLDER/AnimationHolderGlavni");
				((MonoBehaviour)this).Invoke("spustiPopup", 0.5f);
				((Component)_GUI.Find("MISSION HOLDER/AnimationHolderGlavni")).GetComponent<Animator>().Play("ClosePopup");
				makniPopup = 0;
				ocistiMisije();
				prejasiTelevizor = false;
			}
			else if (makniPopup == 2)
			{
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_Button_GoBack();
				}
				popupZaSpustanje = _GUI.Find("UNLOCK HOLDER/AnimationHolderGlavni");
				((MonoBehaviour)this).Invoke("spustiPopup", 0.5f);
				((Component)_GUI.Find("UNLOCK HOLDER/AnimationHolderGlavni")).GetComponent<Animator>().Play("ClosePopup");
				makniPopup = 0;
			}
			else if (makniPopup == 3)
			{
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_Button_GoBack();
				}
			}
			else if (makniPopup == 4)
			{
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_Button_GoBack();
				}
				popupZaSpustanje = _GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni");
				((MonoBehaviour)this).Invoke("spustiPopup", 0.5f);
				((Component)_GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni")).GetComponent<Animator>().Play("ClosePopup");
				makniPopup = 0;
			}
			else if (makniPopup == 5)
			{
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_Button_GoBack();
				}
				((Component)_GUI.Find("INTERFACE HOLDER/_TopLeft/Coins/Coins Number")).GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
				((Component)_GUI.Find("INTERFACE HOLDER/_TopLeft/Coins/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
				((Component)_GUI.Find("INTERFACE HOLDER/_TopLeft/BananaHolder/Bananas/Banana Number")).GetComponent<TextMesh>().text = StagesParser.currentBananas.ToString();
				((Component)_GUI.Find("INTERFACE HOLDER/_TopLeft/BananaHolder/Bananas/Banana Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
				ShopManagerFull.ShopObject.SkloniShop();
				makniPopup = 0;
			}
			else if (makniPopup == 7)
			{
				DodeliNagrade();
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_Button_GoBack();
				}
				popupZaSpustanje = _GUI.Find("REWARD HOLDER/AnimationHolderGlavni");
				((MonoBehaviour)this).Invoke("spustiPopup", 0.5f);
				((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni")).GetComponent<Animator>().Play("ClosePopup");
				_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/PomocniColliderKodOtvaranjaKovcega").localScale = Vector3.zero;
				makniPopup = 0;
			}
			else if (makniPopup == 0)
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
				((MonoBehaviour)this).StartCoroutine(UcitajOstrvo("All Maps"));
			}
			else if (makniPopup == 8)
			{
				makniPopup = 0;
				((MonoBehaviour)this).StartCoroutine(CheckInternetConnection.Instance.ClosePopup());
			}
			else if (makniPopup == 9)
			{
				makniPopup = 4;
				((MonoBehaviour)this).StartCoroutine(CheckInternetConnection.Instance.ClosePopup());
			}
		}
		if (angleIndex >= 0)
		{
			_ = angleIndex;
			_ = 8;
		}
		if (Input.touchCount == 2)
		{
			zoom = true;
		}
		if (Input.touchCount == 2)
		{
			Touch touch = Input.GetTouch(0);
			if ((int)((Touch)(ref touch)).phase == 1)
			{
				touch = Input.GetTouch(1);
				if ((int)((Touch)(ref touch)).phase == 1 && makniPopup == 0)
				{
					zoom = true;
					pomeriKameruUGranice = false;
					touch = Input.GetTouch(0);
					Vector2 position = ((Touch)(ref touch)).position;
					touch = Input.GetTouch(1);
					curDist = position - ((Touch)(ref touch)).position;
					touch = Input.GetTouch(0);
					Vector2 position2 = ((Touch)(ref touch)).position;
					touch = Input.GetTouch(0);
					Vector2 val = position2 - ((Touch)(ref touch)).deltaPosition;
					touch = Input.GetTouch(1);
					Vector2 position3 = ((Touch)(ref touch)).position;
					touch = Input.GetTouch(1);
					prevDist = val - (position3 - ((Touch)(ref touch)).deltaPosition);
					touchDelta = ((Vector2)(ref curDist)).magnitude - ((Vector2)(ref prevDist)).magnitude;
					float num = 0.07f * (((Vector2)(ref prevDist)).magnitude - ((Vector2)(ref curDist)).magnitude);
					touch = Input.GetTouch(0);
					Vector2 deltaPosition = ((Touch)(ref touch)).deltaPosition;
					float magnitude = ((Vector2)(ref deltaPosition)).magnitude;
					touch = Input.GetTouch(0);
					speedTouch0 = magnitude / ((Touch)(ref touch)).deltaTime;
					touch = Input.GetTouch(1);
					deltaPosition = ((Touch)(ref touch)).deltaPosition;
					float magnitude2 = ((Vector2)(ref deltaPosition)).magnitude;
					touch = Input.GetTouch(1);
					speedTouch1 = magnitude2 / ((Touch)(ref touch)).deltaTime;
					if (touchDelta - varianceInDistances <= -10f && (speedTouch0 > minPinchSpeed || speedTouch1 > minPinchSpeed))
					{
						Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, 11f, num / 2f);
					}
					if (touchDelta + varianceInDistances > 10f && (speedTouch0 > minPinchSpeed || speedTouch1 > minPinchSpeed))
					{
						Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, 4f, (0f - num) / 2f);
					}
					Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 4f, 11f);
				}
			}
		}
		if (Input.touchCount == 0 && zoom)
		{
			ivicaEkrana = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
			if (doleLevo.position.x > ivicaEkrana.x)
			{
				pomeriKameruUGranice = true;
				Vector3 rez = default(Vector3);
				((Vector3)(ref rez))._002Ector(doleLevo.position.x - ivicaEkrana.x, 0f, 0f);
				if (doleLevo.position.y > ivicaEkrana.y)
				{
					rez = doleLevo.position - ivicaEkrana;
				}
				ivicaEkrana = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, 0f));
				if (goreLevo.position.y < ivicaEkrana.y)
				{
					rez = goreLevo.position - ivicaEkrana;
				}
				((MonoBehaviour)this).StartCoroutine(PostaviKameruUGranice(rez));
			}
			ivicaEkrana = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 0f));
			if (doleDesno.position.x < ivicaEkrana.x)
			{
				pomeriKameruUGranice = true;
				Vector3 rez2 = default(Vector3);
				((Vector3)(ref rez2))._002Ector(doleDesno.position.x - ivicaEkrana.x, 0f, 0f);
				if (doleDesno.position.y > ivicaEkrana.y)
				{
					rez2 = doleDesno.position - ivicaEkrana;
				}
				ivicaEkrana = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 0f));
				if (goreDesno.position.y < ivicaEkrana.y)
				{
					rez2 = goreDesno.position - ivicaEkrana;
				}
				((MonoBehaviour)this).StartCoroutine(PostaviKameruUGranice(rez2));
			}
			zoom = false;
			ortSize = Camera.main.orthographicSize;
			aspect = Camera.main.aspect;
			levaGranica = doleLevo.position.x + ortSize * aspect;
			desnaGranica = doleDesno.position.x - ortSize * aspect;
			donjaGranica = doleLevo.position.y + ortSize;
			gornjaGranica = goreDesno.position.y - ortSize;
		}
		if (!zoom)
		{
			if (Input.GetMouseButtonDown(0))
			{
				pomeriKameruUGranice = false;
				if (released)
				{
					released = false;
				}
				clickedItem = RaycastFunction(Input.mousePosition);
				trajanjeKlika = Time.time;
				pomerajOdKlika_X = (startX = Input.mousePosition.x);
				pomerajOdKlika_Y = (startY = Input.mousePosition.y);
				if (clickedItem.Equals("ClearAll"))
				{
					((Renderer)((Component)((Component)ShopManagerFull.ShopObject).transform.Find("3 Customize/Costumization BG/ClearAll/ClearAll_Selected")).GetComponent<SpriteRenderer>()).enabled = true;
				}
			}
			if (Input.GetMouseButton(0) && makniPopup == 0)
			{
				endX = Input.mousePosition.x;
				endY = Input.mousePosition.y;
				pomerajX = Camera.main.orthographicSize * (endX - startX) / 350f;
				pomerajY = Camera.main.orthographicSize * (endY - startY) / 350f;
				if (pomerajX != 0f || pomerajY != 0f)
				{
					moved = true;
				}
				((Component)Camera.main).transform.position = new Vector3(Mathf.Clamp(((Component)Camera.main).transform.position.x - pomerajX, levaGranica, desnaGranica), Mathf.Clamp(((Component)Camera.main).transform.position.y - pomerajY, donjaGranica, gornjaGranica), ((Component)Camera.main).transform.position.z);
				startX = endX;
				startY = endY;
			}
			if (released && Mathf.Abs(pomerajX) > 0.0001f)
			{
				if (((Component)Camera.main).transform.position.x <= levaGranica + 0.25f)
				{
					if (bounce)
					{
						pomerajX = -0.04f;
						bounce = false;
					}
				}
				else if (((Component)Camera.main).transform.position.x >= desnaGranica - 0.25f)
				{
					if (bounce)
					{
						pomerajX = 0.04f;
						bounce = false;
					}
				}
				else if (((Component)Camera.main).transform.position.y <= donjaGranica + 0.25f)
				{
					if (bounce)
					{
						pomerajY = -0.04f;
						bounce = false;
					}
				}
				else if (((Component)Camera.main).transform.position.y >= gornjaGranica - 0.25f && bounce)
				{
					pomerajY = 0.04f;
					bounce = false;
				}
				((Component)Camera.main).transform.Translate(0f - pomerajX, 0f - pomerajY, 0f);
				pomerajX *= 0.92f;
				pomerajY *= 0.92f;
				((Component)Camera.main).transform.position = new Vector3(Mathf.Clamp(((Component)Camera.main).transform.position.x, levaGranica, desnaGranica), Mathf.Clamp(((Component)Camera.main).transform.position.y, donjaGranica, gornjaGranica), ((Component)Camera.main).transform.position.z);
			}
		}
		if (!Input.GetMouseButtonUp(0))
		{
			return;
		}
		if (moved)
		{
			moved = false;
			released = true;
			bounce = true;
		}
		releasedItem = RaycastFunction(Input.mousePosition);
		startX = (endX = 0f);
		startY = (endY = 0f);
		if (((Renderer)((Component)((Component)ShopManagerFull.ShopObject).transform.Find("3 Customize/Costumization BG/ClearAll/ClearAll_Selected")).GetComponent<SpriteRenderer>()).enabled)
		{
			((Renderer)((Component)((Component)ShopManagerFull.ShopObject).transform.Find("3 Customize/Costumization BG/ClearAll/ClearAll_Selected")).GetComponent<SpriteRenderer>()).enabled = false;
		}
		if (!(clickedItem == releasedItem) || !(Time.time - trajanjeKlika < 0.35f) || !(Mathf.Abs(Input.mousePosition.x - pomerajOdKlika_X) < 80f) || !(Mathf.Abs(Input.mousePosition.y - pomerajOdKlika_Y) < 80f))
		{
			return;
		}
		if (releasedItem.StartsWith("Level"))
		{
			if (!int.TryParse(releasedItem.Substring(5), out levelName) || StagesParser.StarsPoNivoima[StagesParser.currSetIndex * 20 + levelName - 1] == -1)
			{
				return;
			}
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
			monkeyDestinationLevelIndex = GetMapLevelIndex(levelName);
			if (majmunceSeMrda)
			{
				((MonoBehaviour)this).StopCoroutine("KretanjeMajmunceta");
			}
			if ((monkeyCurrentLevelIndex != monkeyDestinationLevelIndex || StagesParser.pozicijaMajmuncetaNaMapi != Vector3.zero) && makniPopup == 0)
			{
				if (kretanjeDoKovcega)
				{
					kretanjeDoKovcega = false;
				}
				animator.Play("Running");
				((MonoBehaviour)this).StartCoroutine("KretanjeMajmunceta");
			}
			else
			{
				if (makniPopup != 0)
				{
					return;
				}
				StagesParser.currStageIndex = levelName - 1;
				StagesParser.currentLevel = StagesParser.currSetIndex * 20 + levelName;
				StagesParser.nivoZaUcitavanje = 10 + StagesParser.currSetIndex;
				MissionManager.OdrediMisiju(StagesParser.currentLevel - 1, mapa: true);
				if (!FB.IsLoggedIn)
				{
					if (((Component)_GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN")).gameObject.activeSelf)
					{
						((Component)_GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN")).gameObject.SetActive(false);
					}
				}
				else
				{
					if (!((Component)_GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN")).gameObject.activeSelf)
					{
						((Component)_GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN")).gameObject.SetActive(true);
					}
					getFriendsScoresOnLevel(StagesParser.currentLevel);
				}
				Transform obj = _GUI.Find("MISSION HOLDER/AnimationHolderGlavni");
				obj.localPosition += new Vector3(0f, 35f, 0f);
				((Component)_GUI.Find("MISSION HOLDER/AnimationHolderGlavni")).GetComponent<Animator>().Play("OpenPopup");
				makniPopup = 1;
			}
		}
		else if (releasedItem == "HouseShop")
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_GoBack();
			}
			((MonoBehaviour)this).StartCoroutine(ShopManager.OpenShopCard());
		}
		else if (releasedItem == "HolderShipFreeCoins")
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_GoBack();
			}
			((MonoBehaviour)this).StartCoroutine(ShopManager.OpenFreeCoinsCard());
		}
		else if (releasedItem == "ButtonBackToWorlds")
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
			((MonoBehaviour)this).StartCoroutine(UcitajOstrvo("All Maps"));
		}
		else if (releasedItem.StartsWith("Kovceg"))
		{
			StagesParser.bonusName = releasedItem;
			string[] array = StagesParser.bonusName.Split(new char[1] { '_' });
			StagesParser.bonusID = int.Parse(array[1]);
			monkeyDestinationLevelIndex = GetMapLevelIndex(int.Parse(array[2]));
			((Component)_GUI.Find("UNLOCK HOLDER/AnimationHolderGlavni/AnimationHolder/Unlock Bonus Level Popup/Text/Number of Bananas")).GetComponent<TextMesh>().text = array[3];
			((Component)_GUI.Find("UNLOCK HOLDER/AnimationHolderGlavni/AnimationHolder/Unlock Bonus Level Popup/Text/Number of Bananas")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			if (int.Parse(array[2]) >= zadnjiOtkljucanNivo_proveraZaBonus || makniPopup != 0)
			{
				return;
			}
			if (((Component)pronadjiKovceg(StagesParser.bonusName).Find("GateClosed")).GetComponent<Renderer>().enabled)
			{
				Transform obj2 = _GUI.Find("UNLOCK HOLDER/AnimationHolderGlavni");
				obj2.localPosition += new Vector3(0f, 35f, 0f);
				((Component)_GUI.Find("UNLOCK HOLDER/AnimationHolderGlavni")).GetComponent<Animator>().Play("OpenPopup");
				makniPopup = 2;
				return;
			}
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_GoBack();
			}
			kretanjeDoKovcega = true;
			if (majmunceSeMrda)
			{
				((MonoBehaviour)this).StopCoroutine("KretanjeMajmunceta");
			}
			if (StagesParser.pozicijaMajmuncetaNaMapi == Vector3.zero)
			{
				kovcegNaPocetku = (trenutniKovceg = pronadjiKovceg(StagesParser.bonusName));
			}
			else
			{
				trenutniKovceg = pronadjiKovceg(StagesParser.bonusName);
			}
			((Component)trenutniKovceg.Find("GateClosed")).GetComponent<Renderer>().enabled = false;
			((Component)trenutniKovceg.Find("GateOpen")).GetComponent<Renderer>().enabled = true;
			animator.Play("Running");
			((MonoBehaviour)this).StartCoroutine("KretanjeMajmunceta");
		}
		else if (releasedItem.Equals("Button_UnlockBonusYES"))
		{
			popupZaSpustanje = _GUI.Find("UNLOCK HOLDER/AnimationHolderGlavni");
			((MonoBehaviour)this).Invoke("spustiPopup", 0.5f);
			((Component)_GUI.Find("UNLOCK HOLDER/AnimationHolderGlavni")).GetComponent<Animator>().Play("ClosePopup");
			makniPopup = 0;
			string[] array2 = StagesParser.bonusName.Split(new char[1] { '_' });
			StagesParser.bonusID = int.Parse(array2[1]);
			monkeyDestinationLevelIndex = GetMapLevelIndex(int.Parse(array2[2]));
			if (int.Parse(array2[2]) >= zadnjiOtkljucanNivo_proveraZaBonus)
			{
				return;
			}
			if (int.Parse(array2[3]) <= StagesParser.currentBananas)
			{
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_Button_GoBack();
				}
				kretanjeDoKovcega = true;
				StagesParser.currentBananas -= int.Parse(array2[3]);
				((Component)_GUI.Find("INTERFACE HOLDER/_TopLeft/BananaHolder/Bananas/Banana Number")).GetComponent<TextMesh>().text = StagesParser.currentBananas.ToString();
				((Component)_GUI.Find("INTERFACE HOLDER/_TopLeft/BananaHolder/Bananas/Banana Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
				string[] array3 = StagesParser.bonusLevels.Split(new char[1] { '_' });
				string text = array3[StagesParser.currSetIndex];
				string[] array4 = text.Split(new char[1] { '#' });
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
				if (majmunceSeMrda)
				{
					((MonoBehaviour)this).StopCoroutine("KretanjeMajmunceta");
				}
				if (StagesParser.pozicijaMajmuncetaNaMapi == Vector3.zero)
				{
					kovcegNaPocetku = (trenutniKovceg = pronadjiKovceg(StagesParser.bonusName));
				}
				else
				{
					trenutniKovceg = pronadjiKovceg(StagesParser.bonusName);
				}
				((Component)trenutniKovceg.Find("GateClosed")).GetComponent<Renderer>().enabled = false;
				((Component)trenutniKovceg.Find("GateOpen")).GetComponent<Renderer>().enabled = true;
				animator.Play("Running");
				((MonoBehaviour)this).StartCoroutine("KretanjeMajmunceta");
			}
			else
			{
				((Component)_GUI.Find("INTERFACE HOLDER/_TopLeft/BananaHolder/Bananas")).GetComponent<Animation>().Play();
			}
		}
		else if (releasedItem.Equals("Button_UnlockBonusNO"))
		{
			popupZaSpustanje = _GUI.Find("UNLOCK HOLDER/AnimationHolderGlavni");
			((MonoBehaviour)this).Invoke("spustiPopup", 0.5f);
			((Component)_GUI.Find("UNLOCK HOLDER/AnimationHolderGlavni")).GetComponent<Animator>().Play("ClosePopup");
			makniPopup = 0;
		}
		else if (releasedItem.Contains("tvtv"))
		{
			televizorIzabrao = true;
			prejasiTelevizor = true;
			int num2 = int.Parse(releasedItem.Substring(0, releasedItem.IndexOf("-")));
			if (StagesParser.SetsInGame[StagesParser.currSetIndex].GetStarOnStage(num2 - 1) > 0 && makniPopup == 0)
			{
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_Button_GoBack();
				}
				monkeyDestinationLevelIndex = GetWatchVideoIndex(releasedItem.Substring(releasedItem.Length - 1));
				trenutniTelevizor = int.Parse(releasedItem.Substring(releasedItem.Length - 1));
				watchVideoIndex_pom = monkeyDestinationLevelIndex;
				televizorNaMapi = true;
				if (majmunceSeMrda)
				{
					((MonoBehaviour)this).StopCoroutine("KretanjeMajmunceta");
				}
				animator.Play("Running");
				((MonoBehaviour)this).StartCoroutine("KretanjeMajmunceta");
			}
		}
		else if (releasedItem.Equals("Button_WatchVideoYES"))
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_GoBack();
			}
			makniPopup = 9;
			((MonoBehaviour)this).StartCoroutine(checkConnectionForTelevizor());
		}
		else if (releasedItem.Equals("Button_WatchVideoNO"))
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_GoBack();
			}
			popupZaSpustanje = _GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni");
			((MonoBehaviour)this).Invoke("spustiPopup", 0.5f);
			((Component)_GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni")).GetComponent<Animator>().Play("ClosePopup");
			makniPopup = 0;
			if (!televizorIzabrao)
			{
				prejasiTelevizor = true;
				animator.Play("Running");
				((MonoBehaviour)this).StartCoroutine("KretanjeMajmunceta");
			}
			else
			{
				televizorIzabrao = false;
				prejasiTelevizor = false;
			}
		}
		else if (releasedItem.Equals("Button_MissionCancel"))
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_GoBack();
			}
			popupZaSpustanje = _GUI.Find("MISSION HOLDER/AnimationHolderGlavni");
			((MonoBehaviour)this).Invoke("spustiPopup", 0.5f);
			((Component)_GUI.Find("MISSION HOLDER/AnimationHolderGlavni")).GetComponent<Animator>().Play("ClosePopup");
			makniPopup = 0;
			ocistiMisije();
			prejasiTelevizor = false;
		}
		else if (releasedItem.Equals("Button_MissionPlay"))
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_GoBack();
			}
			if (StagesParser.odgledaoTutorial == 1 && StagesParser.currentLevel == 2)
			{
				StagesParser.loadingTip = 3;
			}
			((MonoBehaviour)this).StartCoroutine(closeDoorAndPlay());
		}
		else if (releasedItem.Equals("ShopKucica"))
		{
			if (StagesParser.otvaraoShopNekad == 0 || StagesParser.otvaraoShopNekad == 2)
			{
				if (makniPopup == 6)
				{
					StagesParser.currentMoney += int.Parse(ShopManagerFull.ShopObject.CoinsHats[0]);
					PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
					PlayerPrefs.Save();
					TutorialShop.SetActive(false);
					SwipeControlCustomizationHats.allowInput = false;
					((MonoBehaviour)this).Invoke("prebaciStrelicuNaItem", 1.2f);
				}
				else
				{
					StagesParser.otvaraoShopNekad = 1;
					PlayerPrefs.SetString("OdgledaoTutorial", StagesParser.odgledaoTutorial + "#" + StagesParser.otvaraoShopNekad);
					PlayerPrefs.Save();
				}
			}
			((Component)_GUI.Find("ShopHolder/Shop")).GetComponent<Animation>().Play("MeniDolazak");
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
			if (makniPopup == 0)
			{
				makniPopup = 5;
			}
		}
		else if (releasedItem.Equals("BankInApp") || releasedItem.Equals("Bananas"))
		{
			((Component)_GUI.Find("ShopHolder/Shop")).GetComponent<Animation>().Play("MeniDolazak");
			ShopManagerFull.ShopObject.PozoviTab(2);
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
			makniPopup = 5;
		}
		else if (releasedItem.Equals("Coins") || releasedItem.Equals("FreeCoins"))
		{
			((Component)_GUI.Find("ShopHolder/Shop")).GetComponent<Animation>().Play("MeniDolazak");
			ShopManagerFull.ShopObject.PozoviTab(1);
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
			makniPopup = 5;
		}
		else if (releasedItem.Equals("ButtonBackShop"))
		{
			((Component)_GUI.Find("INTERFACE HOLDER/_TopLeft/Coins/Coins Number")).GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
			((Component)_GUI.Find("INTERFACE HOLDER/_TopLeft/Coins/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			((Component)_GUI.Find("INTERFACE HOLDER/_TopLeft/BananaHolder/Bananas/Banana Number")).GetComponent<TextMesh>().text = StagesParser.currentBananas.ToString();
			((Component)_GUI.Find("INTERFACE HOLDER/_TopLeft/BananaHolder/Bananas/Banana Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			ShopManagerFull.ShopObject.SkloniShop();
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
			makniPopup = 0;
		}
		else if (releasedItem.Equals("ButtonCustomize"))
		{
			ShopManagerFull.ShopObject.PozoviTab(3);
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (releasedItem.Equals("ButtonFreeCoins"))
		{
			ShopManagerFull.ShopObject.PozoviTab(1);
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (releasedItem.Equals("ButtonPowerUps"))
		{
			ShopManagerFull.ShopObject.PozoviTab(4);
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (releasedItem.Equals("ButtonShop"))
		{
			ShopManagerFull.ShopObject.PozoviTab(2);
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (releasedItem.Equals("1HatsShopTab"))
		{
			ShopManagerFull.ShopObject.DeaktivirajCustomization();
			ShopManagerFull.AktivanItemSesir++;
			ShopManagerFull.ShopObject.PozoviCustomizationTab(1);
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (releasedItem.Equals("2TShirtsShopTab"))
		{
			ShopManagerFull.ShopObject.DeaktivirajCustomization();
			ShopManagerFull.AktivanItemMajica++;
			ShopManagerFull.ShopObject.PozoviCustomizationTab(2);
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (releasedItem.Equals("3BackPackShopTab"))
		{
			ShopManagerFull.ShopObject.DeaktivirajCustomization();
			ShopManagerFull.AktivanItemRanac++;
			ShopManagerFull.ShopObject.PozoviCustomizationTab(3);
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem.StartsWith("Hats"))
		{
			for (int k = 0; k < ShopManagerFull.ShopObject.HatsObjects.Length; k++)
			{
				if (releasedItem.StartsWith("Hats " + (k + 1)))
				{
					ObjCustomizationHats.swipeCtrl.currentValue = ShopManagerFull.ShopObject.HatsObjects.Length - k - 1;
				}
			}
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem.StartsWith("Shirts"))
		{
			for (int l = 0; l < ShopManagerFull.ShopObject.ShirtsObjects.Length; l++)
			{
				if (releasedItem.StartsWith("Shirts " + (l + 1)))
				{
					ObjCustomizationShirts.swipeCtrl.currentValue = ShopManagerFull.ShopObject.ShirtsObjects.Length - l - 1;
				}
			}
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (clickedItem == releasedItem && releasedItem.StartsWith("BackPacks"))
		{
			for (int m = 0; m < ShopManagerFull.ShopObject.BackPacksObjects.Length; m++)
			{
				if (releasedItem.StartsWith("BackPacks " + (m + 1)))
				{
					ObjCustomizationBackPacks.swipeCtrl.currentValue = ShopManagerFull.ShopObject.BackPacksObjects.Length - m - 1;
				}
			}
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (releasedItem.Equals("ClearAll"))
		{
			ShopManagerFull.ShopObject.OcistiMajmuna();
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (releasedItem.Equals("Preview Button"))
		{
			if (ShopManagerFull.PreviewState)
			{
				ShopManagerFull.ShopObject.PreviewItem();
			}
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (releasedItem.Equals("Buy Button"))
		{
			if (makniPopup == 6 && ShopManagerFull.BuyButtonState == 2)
			{
				TutorialShop.SetActive(false);
				SwipeControlCustomizationHats.allowInput = true;
				StagesParser.otvaraoShopNekad = 1;
				StagesParser.odgledaoTutorial = 3;
				PlayerPrefs.SetString("OdgledaoTutorial", StagesParser.odgledaoTutorial + "#" + StagesParser.otvaraoShopNekad);
				PlayerPrefs.Save();
				makniPopup = 5;
			}
			ShopManagerFull.ShopObject.KupiItem();
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (releasedItem.Equals("Button_CollectReward"))
		{
			DodeliNagrade();
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_GoBack();
			}
			popupZaSpustanje = _GUI.Find("REWARD HOLDER/AnimationHolderGlavni");
			((MonoBehaviour)this).Invoke("spustiPopup", 0.5f);
			((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni")).GetComponent<Animator>().Play("ClosePopup");
			_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/PomocniColliderKodOtvaranjaKovcega").localScale = Vector3.zero;
			makniPopup = 0;
		}
		else if (releasedItem.Equals("Shop Banana"))
		{
			ShopManagerFull.ShopObject.KupiBananu();
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (releasedItem.Equals("Shop POWERUP Double Coins"))
		{
			ShopManagerFull.ShopObject.KupiDoubleCoins();
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (releasedItem.Equals("Shop POWERUP Magnet"))
		{
			ShopManagerFull.ShopObject.KupiMagnet();
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (releasedItem.Equals("Shop POWERUP Shield"))
		{
			ShopManagerFull.ShopObject.KupiShield();
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
		}
		else if (releasedItem.Equals("FB Login"))
		{
			makniPopup = 8;
			((MonoBehaviour)this).StartCoroutine(checkConnectionForLoginButton());
		}
		else if (releasedItem.StartsWith("Friends Level"))
		{
			makniPopup = 8;
			((MonoBehaviour)this).StartCoroutine(checkConnectionForInviteFriend());
		}
		else if (releasedItem.Equals("Button_CheckOK"))
		{
			makniPopup = 0;
			((MonoBehaviour)this).StartCoroutine(CheckInternetConnection.Instance.ClosePopup());
		}
		else if (releasedItem.Equals("ShopFCBILikePage"))
		{
			makniPopup = 8;
			((MonoBehaviour)this).StartCoroutine(checkConnectionForPageLike("https://www.facebook.com/pages/Banana-Island/636650059721490", "BananaIsland"));
		}
		else if (releasedItem.Equals("ShopFCWatchVideo"))
		{
			makniPopup = 8;
			((MonoBehaviour)this).StartCoroutine(checkConnectionForWatchVideo());
		}
		else if (releasedItem.Equals("ShopFCWLLikePage"))
		{
			makniPopup = 8;
			((MonoBehaviour)this).StartCoroutine(checkConnectionForPageLike("https://www.facebook.com/WebelinxGamesApps", "Webelinx"));
		}
	}

	private void DodeliNagrade()
	{
		switch (reward1Type)
		{
		case 1:
			StagesParser.powerup_magnets += kolicinaReward1;
			break;
		case 2:
			StagesParser.powerup_doublecoins += kolicinaReward1;
			break;
		case 3:
			StagesParser.powerup_shields += kolicinaReward1;
			break;
		case 4:
			DajMuItem();
			break;
		}
		switch (reward2Type)
		{
		case 1:
			StagesParser.powerup_magnets += kolicinaReward2;
			break;
		case 2:
			StagesParser.powerup_doublecoins += kolicinaReward2;
			break;
		case 3:
			StagesParser.powerup_shields += kolicinaReward2;
			break;
		}
		switch (reward3Type)
		{
		case 1:
			StagesParser.powerup_magnets += kolicinaReward3;
			break;
		case 2:
			StagesParser.powerup_doublecoins += kolicinaReward3;
			break;
		case 3:
			StagesParser.powerup_shields += kolicinaReward3;
			break;
		}
		PlayerPrefs.SetString("PowerUps", StagesParser.powerup_doublecoins + "#" + StagesParser.powerup_magnets + "#" + StagesParser.powerup_shields);
		PlayerPrefs.Save();
		GameObject.Find("Double Coins Number/Number").GetComponent<TextMesh>().text = StagesParser.powerup_doublecoins.ToString();
		GameObject.Find("Double Coins Number/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Magnet Number/Number").GetComponent<TextMesh>().text = StagesParser.powerup_magnets.ToString();
		GameObject.Find("Magnet Number/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Shield Number/Number").GetComponent<TextMesh>().text = StagesParser.powerup_shields.ToString();
		GameObject.Find("Shield Number/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		reward1Type = 0;
		reward2Type = 0;
		reward3Type = 0;
		kolicinaReward1 = 0;
		kolicinaReward2 = 0;
		kolicinaReward3 = 0;
		StagesParser.ServerUpdate = 1;
	}

	private void DajMuItem()
	{
		if (cetvrtiKovcegNagrada == "Glava")
		{
			string[] array = StagesParser.svekupovineGlava.Split(new char[1] { '#' });
			array[indexNagradeZaCetvrtiKovceg] = "1";
			string text = "";
			for (int i = 0; i < array.Length; i++)
			{
				text = text + array[i] + "#";
			}
			text = text.Remove(text.Length - 1);
			StagesParser.svekupovineGlava = text;
			((Component)ShopManagerFull.ShopObject.HatsObjects[indexNagradeZaCetvrtiKovceg].Find("Stikla")).gameObject.SetActive(true);
			ShopManagerFull.SveStvariZaOblacenjeHats[indexNagradeZaCetvrtiKovceg] = 1;
			PlayerPrefs.SetString("UserSveKupovineHats", StagesParser.svekupovineGlava);
		}
		else if (cetvrtiKovcegNagrada == "Majica")
		{
			string[] array = StagesParser.svekupovineMajica.Split(new char[1] { '#' });
			array[indexNagradeZaCetvrtiKovceg] = "1";
			string text2 = "";
			for (int j = 0; j < array.Length; j++)
			{
				text2 = text2 + array[j] + "#";
			}
			text2 = text2.Remove(text2.Length - 1);
			StagesParser.svekupovineMajica = text2;
			((Component)ShopManagerFull.ShopObject.ShirtsObjects[indexNagradeZaCetvrtiKovceg].Find("Stikla")).gameObject.SetActive(true);
			ShopManagerFull.SveStvariZaOblacenjeShirts[indexNagradeZaCetvrtiKovceg] = 1;
			PlayerPrefs.SetString("UserSveKupovineShirts", StagesParser.svekupovineMajica);
		}
		else if (cetvrtiKovcegNagrada == "Ledja")
		{
			string[] array = StagesParser.svekupovineLedja.Split(new char[1] { '#' });
			array[indexNagradeZaCetvrtiKovceg] = "1";
			string text3 = "";
			for (int k = 0; k < array.Length; k++)
			{
				text3 = text3 + array[k] + "#";
			}
			text3 = text3.Remove(text3.Length - 1);
			StagesParser.svekupovineLedja = text3;
			((Component)ShopManagerFull.ShopObject.BackPacksObjects[indexNagradeZaCetvrtiKovceg].Find("Stikla")).gameObject.SetActive(true);
			ShopManagerFull.SveStvariZaOblacenjeBackPack[indexNagradeZaCetvrtiKovceg] = 1;
			PlayerPrefs.SetString("UserSveKupovineBackPacks", StagesParser.svekupovineLedja);
		}
		else if (cetvrtiKovcegNagrada == "PowerUps")
		{
			StagesParser.powerup_magnets += kolicinaReward1;
			StagesParser.powerup_doublecoins += kolicinaReward2;
			StagesParser.powerup_shields += kolicinaReward3;
		}
		ShopManagerFull.ShopObject.ProveriStanjeCelogShopa();
		PlayerPrefs.Save();
		indexNagradeZaCetvrtiKovceg = -1;
	}

	private void IspitajItem()
	{
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		string text = string.Empty;
		string[] array = StagesParser.svekupovineGlava.Split(new char[1] { '#' });
		for (int i = 0; i <= ShopManagerFull.BrojOtkljucanihKapa; i++)
		{
			if (int.Parse(array[i]) == 0)
			{
				cetvrtiKovcegNagrada = "Glava";
				indexNagradeZaCetvrtiKovceg = i;
				break;
			}
		}
		Transform transform;
		if (indexNagradeZaCetvrtiKovceg == -1)
		{
			array = StagesParser.svekupovineMajica.Split(new char[1] { '#' });
			for (int j = 0; j <= ShopManagerFull.BrojOtkljucanihMajici; j++)
			{
				if (int.Parse(array[j]) == 0)
				{
					cetvrtiKovcegNagrada = "Majica";
					indexNagradeZaCetvrtiKovceg = j;
					break;
				}
			}
			if (indexNagradeZaCetvrtiKovceg == -1)
			{
				array = StagesParser.svekupovineLedja.Split(new char[1] { '#' });
				for (int k = 0; k <= ShopManagerFull.BrojOtkljucanihRanceva; k++)
				{
					if (int.Parse(array[k]) == 0)
					{
						cetvrtiKovcegNagrada = "Ledja";
						indexNagradeZaCetvrtiKovceg = k;
						break;
					}
				}
				if (indexNagradeZaCetvrtiKovceg == -1)
				{
					cetvrtiKovcegNagrada = "PowerUps";
					kolicinaReward1 = 2;
					kolicinaReward2 = 2;
					kolicinaReward3 = 2;
					((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward2")).gameObject.SetActive(true);
					((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward3")).gameObject.SetActive(true);
					((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1/Count")).GetComponent<TextMesh>().text = kolicinaReward1.ToString();
					((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward2/Count")).GetComponent<TextMesh>().text = kolicinaReward2.ToString();
					((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward3/Count")).GetComponent<TextMesh>().text = kolicinaReward3.ToString();
					((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1")).GetComponent<SpriteRenderer>().sprite = ((Component)_GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Magnet/Plavi Bedz/Magnet Icon")).GetComponent<SpriteRenderer>().sprite;
					((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward2")).GetComponent<SpriteRenderer>().sprite = ((Component)_GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Shield/Plavi Bedz/Shield Icon")).GetComponent<SpriteRenderer>().sprite;
					((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward3")).GetComponent<SpriteRenderer>().sprite = ((Component)_GUI.Find("ShopHolder/Shop/4 Power-Ups/Power-Ups Tabovi/Shop POWERUP Double Coins/Plavi Bedz/Double Coins Icon")).GetComponent<SpriteRenderer>().sprite;
					return;
				}
				transform = GameObject.Find("3 Customize/Customize Tabovi/3BackPack").transform;
				for (int l = 0; l < transform.childCount; l++)
				{
					if (((Object)transform.GetChild(l)).name.StartsWith("BackPacks " + (indexNagradeZaCetvrtiKovceg + 1)))
					{
						text = ((Object)transform.GetChild(l)).name;
						break;
					}
				}
				if (!text.Equals(string.Empty))
				{
					string text2 = text.Substring(text.IndexOf("- ") + 2);
					((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1")).GetComponent<SpriteRenderer>().sprite = ((Component)transform.Find(text).Find("Plavi Bedz/" + text2 + " Icon")).GetComponent<SpriteRenderer>().sprite;
				}
				return;
			}
			transform = GameObject.Find("3 Customize/Customize Tabovi/2Shirts").transform;
			for (int m = 0; m < transform.childCount; m++)
			{
				if (((Object)transform.GetChild(m)).name.StartsWith("Shirts " + (indexNagradeZaCetvrtiKovceg + 1)))
				{
					text = ((Object)transform.GetChild(m)).name;
					break;
				}
			}
			if (!text.Equals(string.Empty))
			{
				string text3 = text.Substring(text.IndexOf("- ") + 2);
				((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1")).GetComponent<SpriteRenderer>().sprite = ((Component)transform.Find(text).Find("Plavi Bedz/" + text3 + " Icon")).GetComponent<SpriteRenderer>().sprite;
				((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1")).GetComponent<SpriteRenderer>().color = ((Component)transform.Find(text).Find("Plavi Bedz/" + text3 + " Icon")).GetComponent<SpriteRenderer>().color;
			}
			return;
		}
		transform = GameObject.Find("3 Customize/Customize Tabovi/1Hats").transform;
		for (int n = 0; n < transform.childCount; n++)
		{
			if (((Object)transform.GetChild(n)).name.StartsWith("Hats " + (indexNagradeZaCetvrtiKovceg + 1)))
			{
				text = ((Object)transform.GetChild(n)).name;
				break;
			}
		}
		if (!text.Equals(string.Empty))
		{
			string text4 = text.Substring(text.IndexOf("- ") + 2);
			((Component)_GUI.Find("REWARD HOLDER/AnimationHolderGlavni/AnimationHolder/RewardCollectPopup/Reward1")).GetComponent<SpriteRenderer>().sprite = ((Component)transform.Find(text).Find("Plavi Bedz/" + text4 + " Icon")).GetComponent<SpriteRenderer>().sprite;
		}
	}

	private Transform pronadjiKovceg(string name)
	{
		for (int i = 0; i < BonusNivoi.Length; i++)
		{
			if (((Object)BonusNivoi[i]).name.Equals(name))
			{
				return BonusNivoi[i];
			}
		}
		return null;
	}

	private int GetMapLevelIndex(int value)
	{
		for (int i = 0; i < izmedjneTacke.childCount; i++)
		{
			if (int.TryParse(((Object)izmedjneTacke.GetChild(i)).name, out var result) && result == value)
			{
				return i;
			}
		}
		return -1;
	}

	private int GetWatchVideoIndex(string tvsuffix)
	{
		for (int i = 0; i < izmedjneTacke.childCount; i++)
		{
			if (((Object)izmedjneTacke.GetChild(i)).name.Contains("tvtv" + tvsuffix))
			{
				return i;
			}
		}
		return -1;
	}

	private int findAngleDir(Transform start, Vector3 target)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = target - start.position;
		if (val != Vector3.zero)
		{
			float num = ((val.y <= start.right.y) ? Vector3.Angle(start.right, val) : (360f - Vector3.Angle(start.right, val)));
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
		return -1;
	}

	private IEnumerator KretanjeMajmunceta()
	{
		majmunceSeMrda = true;
		int num = Mathf.Abs(monkeyCurrentLevelIndex - monkeyDestinationLevelIndex);
		float brzina2 = (float)num * Time.deltaTime;
		if (num == 0)
		{
			brzina2 = 4f * Time.deltaTime;
		}
		Quaternion a = Quaternion.identity;
		bool izadji = false;
		brzina2 = Mathf.Clamp(brzina2, 0.065f, 1f);
		if (StagesParser.pozicijaMajmuncetaNaMapi != Vector3.zero)
		{
			Transform koraciDoKovcega2 = kovcegNaPocetku.Find("Koraci do kovcega");
			for (int l = koraciDoKovcega2.childCount - 1; l >= 0; l--)
			{
				int angleDir4 = findAngleDir(holderMajmun, koraciDoKovcega2.GetChild(l).position);
				if (angleDir4 != -1)
				{
					a = Quaternion.Euler(angles[angleDir4]);
				}
				while (holderMajmun.position != koraciDoKovcega2.GetChild(l).position)
				{
					if (!PlaySounds.Run.isPlaying && PlaySounds.soundOn)
					{
						PlaySounds.Play_Run();
					}
					holderMajmun.position = Vector3.MoveTowards(holderMajmun.position, koraciDoKovcega2.GetChild(l).position, brzina2);
					yield return null;
					if (angleDir4 != -1)
					{
						majmun.rotation = Quaternion.Lerp(majmun.rotation, a, 0.2f);
					}
				}
			}
			StagesParser.pozicijaMajmuncetaNaMapi = Vector3.zero;
		}
		if (monkeyCurrentLevelIndex < monkeyDestinationLevelIndex)
		{
			for (int l = monkeyCurrentLevelIndex; l <= monkeyDestinationLevelIndex; l++)
			{
				int angleDir4 = findAngleDir(holderMajmun, izmedjneTacke.GetChild(l).position);
				if (angleDir4 != -1)
				{
					a = Quaternion.Euler(angles[angleDir4]);
				}
				while (holderMajmun.position != izmedjneTacke.GetChild(l).position && !izadji)
				{
					if (!PlaySounds.Run.isPlaying && PlaySounds.soundOn)
					{
						PlaySounds.Play_Run();
					}
					holderMajmun.position = Vector3.MoveTowards(holderMajmun.position, izmedjneTacke.GetChild(l).position, brzina2);
					yield return null;
					monkeyCurrentLevelIndex = l;
					if (angleDir4 != -1)
					{
						majmun.rotation = Quaternion.Lerp(majmun.rotation, a, 0.2f);
					}
					if (((Object)izmedjneTacke.GetChild(l)).name.Contains("tvtv") && ((Component)izmedjneTacke.GetChild(l)).gameObject.activeSelf && !prejasiTelevizor)
					{
						yield return (object)new WaitForEndOfFrame();
						televizorNaMapi = true;
						majmunceSeMrda = false;
						izadji = true;
						trenutniTelevizor = int.Parse(((Object)izmedjneTacke.GetChild(l)).name.Substring(((Object)izmedjneTacke.GetChild(l)).name.IndexOf("tvtv") + 4));
						animator.Play("Idle");
						monkeyCurrentLevelIndex = l + 1;
					}
				}
			}
		}
		else
		{
			for (int l = monkeyCurrentLevelIndex; l >= monkeyDestinationLevelIndex; l--)
			{
				int angleDir4 = findAngleDir(holderMajmun, izmedjneTacke.GetChild(l).position);
				if (angleDir4 != -1)
				{
					a = Quaternion.Euler(angles[angleDir4]);
				}
				while (holderMajmun.position != izmedjneTacke.GetChild(l).position && !izadji)
				{
					if (!PlaySounds.Run.isPlaying && PlaySounds.soundOn)
					{
						PlaySounds.Play_Run();
					}
					holderMajmun.position = Vector3.MoveTowards(holderMajmun.position, izmedjneTacke.GetChild(l).position, brzina2);
					yield return null;
					monkeyCurrentLevelIndex = l;
					if (angleDir4 != -1)
					{
						majmun.rotation = Quaternion.Lerp(majmun.rotation, a, 0.2f);
					}
					if (((Object)izmedjneTacke.GetChild(l)).name.Contains("tvtv") && ((Component)izmedjneTacke.GetChild(l)).gameObject.activeSelf && !prejasiTelevizor)
					{
						yield return (object)new WaitForEndOfFrame();
						televizorNaMapi = true;
						majmunceSeMrda = false;
						izadji = true;
						trenutniTelevizor = int.Parse(((Object)izmedjneTacke.GetChild(l)).name.Substring(((Object)izmedjneTacke.GetChild(l)).name.IndexOf("tvtv") + 4));
						animator.Play("Idle");
						monkeyCurrentLevelIndex = l - 1;
					}
				}
			}
		}
		float t2;
		if (kretanjeDoKovcega && !televizorNaMapi)
		{
			if (StagesParser.pozicijaMajmuncetaNaMapi == Vector3.zero)
			{
				Transform koraciDoKovcega2 = trenutniKovceg.Find("Koraci do kovcega");
				for (int l = 0; l < koraciDoKovcega2.childCount; l++)
				{
					int angleDir4 = findAngleDir(holderMajmun, koraciDoKovcega2.GetChild(l).position);
					if (angleDir4 != -1)
					{
						a = Quaternion.Euler(angles[angleDir4]);
					}
					while (holderMajmun.position != koraciDoKovcega2.GetChild(l).position)
					{
						if (!PlaySounds.Run.isPlaying && PlaySounds.soundOn)
						{
							PlaySounds.Play_Run();
						}
						holderMajmun.position = Vector3.MoveTowards(holderMajmun.position, koraciDoKovcega2.GetChild(l).position, brzina2);
						yield return null;
						if (angleDir4 != -1)
						{
							majmun.rotation = Quaternion.Lerp(majmun.rotation, a, 0.2f);
						}
					}
				}
			}
			StagesParser.bonusLevel = true;
			StagesParser.pozicijaMajmuncetaNaMapi = holderMajmun.position;
			animator.Play("Idle");
			majmunceSeMrda = false;
			t2 = 0f;
			while (t2 < 1f)
			{
				((Component)Camera.main).transform.position = Vector3.Lerp(((Component)Camera.main).transform.position, new Vector3(holderMajmun.position.x, holderMajmun.position.y, ((Component)Camera.main).transform.position.z), t2);
				t2 += Time.deltaTime;
				yield return null;
			}
			((Component)Camera.main).transform.position = new Vector3(Mathf.Clamp(((Component)Camera.main).transform.position.x, levaGranica, desnaGranica), Mathf.Clamp(((Component)Camera.main).transform.position.y, donjaGranica, gornjaGranica), ((Component)Camera.main).transform.position.z);
			if (kretanjeDoKovcega)
			{
				kretanjeDoKovcega = false;
				((MonoBehaviour)this).StartCoroutine(closeDoorAndPlay());
			}
			MissionManager.OdrediMisiju(StagesParser.currentLevel - 1, mapa: true);
			if (StagesParser.bonusLevel)
			{
				yield break;
			}
			if (!FB.IsLoggedIn)
			{
				if (((Component)_GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN")).gameObject.activeSelf)
				{
					((Component)_GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN")).gameObject.SetActive(false);
				}
			}
			else
			{
				if (!((Component)_GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN")).gameObject.activeSelf)
				{
					((Component)_GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN")).gameObject.SetActive(true);
				}
				getFriendsScoresOnLevel(StagesParser.currentLevel);
			}
			Transform obj = _GUI.Find("MISSION HOLDER/AnimationHolderGlavni");
			obj.localPosition += new Vector3(0f, 35f, 0f);
			((Component)_GUI.Find("MISSION HOLDER/AnimationHolderGlavni")).GetComponent<Animator>().Play("OpenPopup");
			makniPopup = 1;
			yield break;
		}
		if (televizorNaMapi)
		{
			televizorNaMapi = false;
			animator.Play("Idle");
			((Component)_GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/Coins/Coins Number")).GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
			((Component)_GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/Coins/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			Transform obj2 = _GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni");
			obj2.localPosition += new Vector3(0f, 35f, 0f);
			((Component)_GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni")).GetComponent<Animator>().Play("OpenPopup");
			makniPopup = 4;
			yield break;
		}
		animator.Play("Idle");
		majmunceSeMrda = false;
		t2 = 0f;
		float limitX = ((holderMajmun.position.x > desnaGranica) ? desnaGranica : ((!(holderMajmun.position.x < levaGranica)) ? holderMajmun.position.x : levaGranica));
		while (((Component)Camera.main).transform.position.x != limitX)
		{
			((Component)Camera.main).transform.position = Vector3.Lerp(((Component)Camera.main).transform.position, new Vector3(limitX, holderMajmun.position.y, ((Component)Camera.main).transform.position.z), t2);
			t2 += Time.deltaTime;
			yield return null;
		}
		StagesParser.currStageIndex = levelName - 1;
		StagesParser.currentLevel = StagesParser.currSetIndex * 20 + levelName;
		StagesParser.nivoZaUcitavanje = 10 + StagesParser.currSetIndex;
		MissionManager.OdrediMisiju(StagesParser.currentLevel - 1, mapa: true);
		if (!FB.IsLoggedIn)
		{
			if (((Component)_GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN")).gameObject.activeSelf)
			{
				((Component)_GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN")).gameObject.SetActive(false);
			}
		}
		else
		{
			if (!((Component)_GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN")).gameObject.activeSelf)
			{
				((Component)_GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN")).gameObject.SetActive(true);
			}
			getFriendsScoresOnLevel(StagesParser.currentLevel);
		}
		Transform obj3 = _GUI.Find("MISSION HOLDER/AnimationHolderGlavni");
		obj3.localPosition += new Vector3(0f, 35f, 0f);
		((Component)_GUI.Find("MISSION HOLDER/AnimationHolderGlavni")).GetComponent<Animator>().Play("OpenPopup");
		makniPopup = 1;
	}

	private void ocistiMisije()
	{
		Transform val = _GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Popup za Mission HOLDER/Popup za Mission");
		Transform val2 = val.Find("Mission Icons/Mission 1");
		Transform val3 = val.Find("Mission Icons/Mission 2");
		Transform val4 = val.Find("Mission Icons/Mission 3");
		for (int i = 0; i < val2.childCount; i++)
		{
			((Component)val2.GetChild(i)).GetComponent<Renderer>().enabled = false;
		}
		for (int j = 0; j < val3.childCount; j++)
		{
			((Component)val3.GetChild(j)).GetComponent<Renderer>().enabled = false;
		}
		for (int k = 0; k < val4.childCount; k++)
		{
			((Component)val4.GetChild(k)).GetComponent<Renderer>().enabled = false;
		}
		((Component)val.Find("Text/Mission 1")).GetComponent<TextMesh>().text = string.Empty;
		((Component)val.Find("Text/Mission 1")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		((Component)val.Find("Text/Mission 2")).GetComponent<TextMesh>().text = string.Empty;
		((Component)val.Find("Text/Mission 2")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		((Component)val.Find("Text/Mission 3")).GetComponent<TextMesh>().text = string.Empty;
		((Component)val.Find("Text/Mission 3")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
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

	private IEnumerator closeDoorAndPlay()
	{
		((Component)_GUI.Find("LOADING HOLDER NEW/Loading Animation Vrata")).GetComponent<Animator>().Play("Loading Zidovi Dolazak");
		yield return (object)new WaitForSeconds(0.75f);
		Application.LoadLevel(2);
	}

	private IEnumerator UcitajOstrvo(string ime)
	{
		((Component)_GUI.Find("LOADING HOLDER NEW/Loading Animation Vrata")).GetComponent<Animator>().Play("Loading Zidovi Dolazak");
		yield return (object)new WaitForSeconds(1.1f);
		Application.LoadLevel(ime);
	}

	private IEnumerator PlayAndWaitForAnimation(Animation animation, string animName, bool loadAnotherScene, int indexOfSceneToLoad)
	{
		yield return null;
		StagesParser.nivoZaUcitavanje = 10 + StagesParser.currSetIndex;
		if (loadAnotherScene)
		{
			Application.LoadLevel(indexOfSceneToLoad);
		}
	}

	private IEnumerator PostaviKameruUGranice(Vector3 rez)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		float t = 0f;
		Vector3 rez2 = ((Component)Camera.main).transform.position + new Vector3(rez.x, rez.y, 0f);
		while (t < 1f)
		{
			((Component)Camera.main).transform.position = Vector3.Lerp(((Component)Camera.main).transform.position, rez2, t);
			t += Time.deltaTime / 0.5f;
			yield return null;
		}
	}

	private IEnumerator PokaziMuCustomize()
	{
		TutorialShop = Object.Instantiate<GameObject>(TutorialShopPrefab, new Vector3(-33.2f, -95f, -60f), Quaternion.identity);
		yield return (object)new WaitForSeconds(1.2f);
		while (((Component)Camera.main).transform.position.y <= -18.45f)
		{
			((Component)Camera.main).transform.position = Vector3.MoveTowards(((Component)Camera.main).transform.position, new Vector3(((Component)Camera.main).transform.position.x, -18.4f, ((Component)Camera.main).transform.position.z), 0.055f);
			yield return null;
		}
		((Component)TutorialShop.transform.GetChild(0)).GetComponent<Animation>().Play();
		yield return (object)new WaitForSeconds(0.5f);
		((Component)TutorialShop.transform.GetChild(0).Find("RedArrowHolder/RedArrow")).GetComponent<Renderer>().enabled = true;
		((Component)TutorialShop.transform.GetChild(0).Find("RedArrowHolder/RedArrow")).GetComponent<Animation>().Play();
		((Component)TutorialShop.transform.GetChild(0)).GetComponent<Collider>().enabled = false;
	}

	private void changeLanguage()
	{
		if (!FB.IsLoggedIn)
		{
			GameObject.Find("Log In").GetComponent<TextMesh>().text = LanguageManager.LogIn;
			GameObject.Find("Log In").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		}
		GameObject.Find("Level No").GetComponent<TextMesh>().text = LanguageManager.Level;
		GameObject.Find("Level No").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("MissionText").GetComponent<TextMesh>().text = LanguageManager.Mission;
		GameObject.Find("MissionText").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)_GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN/Friends Level Win 1 HOLDER/FB/Fb Invite 1")).GetComponent<TextMesh>().text = LanguageManager.Invite;
		((Component)_GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN/Friends Level Win 2 HOLDER/FB/Fb Invite 1")).GetComponent<TextMesh>().text = LanguageManager.Invite;
		((Component)_GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN/Friends Level Win 3 HOLDER/FB/Fb Invite 1")).GetComponent<TextMesh>().text = LanguageManager.Invite;
		((Component)_GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN/Friends Level Win 4 HOLDER/FB/Fb Invite 1")).GetComponent<TextMesh>().text = LanguageManager.Invite;
		((Component)_GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN/Friends Level Win 5 HOLDER/FB/Fb Invite 1")).GetComponent<TextMesh>().text = LanguageManager.Invite;
		((Component)_GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN/Friends Level Win 1 HOLDER/FB/Fb Invite 1")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)_GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN/Friends Level Win 2 HOLDER/FB/Fb Invite 1")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)_GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN/Friends Level Win 3 HOLDER/FB/Fb Invite 1")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)_GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN/Friends Level Win 4 HOLDER/FB/Fb Invite 1")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)_GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN/Friends Level Win 5 HOLDER/FB/Fb Invite 1")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("RewardText").GetComponent<TextMesh>().text = LanguageManager.Reward;
		GameObject.Find("RewardText").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Button_CollectReward/Text").GetComponent<TextMesh>().text = LanguageManager.Collect;
		GameObject.Find("Button_CollectReward/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Bonus Level").GetComponent<TextMesh>().text = LanguageManager.BonusLevel;
		GameObject.Find("Bonus Level").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Unlock").GetComponent<TextMesh>().text = LanguageManager.Unlock;
		GameObject.Find("Unlock").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Button_UnlockBonusYES/Text").GetComponent<TextMesh>().text = LanguageManager.Yes;
		GameObject.Find("Button_UnlockBonusYES/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Button_UnlockBonusNO/Text").GetComponent<TextMesh>().text = LanguageManager.No;
		GameObject.Find("Button_UnlockBonusNO/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Free Coins").GetComponent<TextMesh>().text = LanguageManager.FreeCoins;
		GameObject.Find("Free Coins").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Button_WatchVideoYES/Text").GetComponent<TextMesh>().text = LanguageManager.Yes;
		GameObject.Find("Button_WatchVideoYES/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Button_WatchVideoNO/Text").GetComponent<TextMesh>().text = LanguageManager.No;
		GameObject.Find("Button_WatchVideoNO/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Button_WatchVideoNO/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)_GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/WatchVideoText")).GetComponent<TextMesh>().text = LanguageManager.WatchVideo;
		((Component)_GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/WatchVideoText")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		((Component)_GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/NotAvailableText")).GetComponent<TextMesh>().text = LanguageManager.NoVideo;
		((Component)_GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni/AnimationHolder/WATCH VIDEO Popup/NotAvailableText")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("ButtonFreeCoins/Text").GetComponent<TextMesh>().text = LanguageManager.FreeCoins;
		GameObject.Find("ButtonFreeCoins/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("ButtonCustomize/Text").GetComponent<TextMesh>().text = LanguageManager.Customize;
		GameObject.Find("ButtonCustomize/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("ButtonPowerUps/Text").GetComponent<TextMesh>().text = LanguageManager.PowerUps;
		GameObject.Find("ButtonPowerUps/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("ButtonShop/Text").GetComponent<TextMesh>().text = LanguageManager.Shop;
		GameObject.Find("ButtonShop/Text").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Shop Banana/Text/Banana").GetComponent<TextMesh>().text = LanguageManager.Banana;
		GameObject.Find("Shop Banana/Text/Banana").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("ShopFCWatchVideo/Text/Watch Video").GetComponent<TextMesh>().text = LanguageManager.WatchVideo;
		GameObject.Find("ShopFCWatchVideo/Text/Watch Video").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("ButtonBuy").GetComponent<TextMesh>().text = LanguageManager.Buy;
		GameObject.Find("ButtonBuy").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Shop POWERUP Double Coins/Text/ime").GetComponent<TextMesh>().text = LanguageManager.DoubleCoins;
		GameObject.Find("Shop POWERUP Double Coins/Text/ime").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Shop POWERUP Magnet/Text/ime").GetComponent<TextMesh>().text = LanguageManager.CoinsMagnet;
		GameObject.Find("Shop POWERUP Magnet/Text/ime").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Shop POWERUP Shield/Text/ime").GetComponent<TextMesh>().text = LanguageManager.Shield;
		GameObject.Find("Shop POWERUP Shield/Text/ime").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
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
			if (item.score == 0 && FacebookManager.ListaStructPrijatelja[k].PrijateljID != FacebookManager.User)
			{
				item.score = -1;
			}
			if (FacebookManager.ListaStructPrijatelja[k].PrijateljID == FacebookManager.User)
			{
				int num = int.Parse(StagesParser.allLevels[StagesParser.currentLevel - 1].Split(new char[1] { '#' })[2]);
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
					Transform val = _GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN/Friends Level Win " + num3 + " HOLDER");
					((Component)val.Find("FB")).gameObject.SetActive(false);
					if (!((Component)val.Find("Friends Level Win " + num3)).gameObject.activeSelf)
					{
						((Component)val.Find("Friends Level Win " + num3)).gameObject.SetActive(true);
					}
					((Component)val.Find("Friends Level Win " + num3 + "/Friends Level Win Picture " + num3)).GetComponent<Renderer>().material.mainTexture = FacebookManager.ListaStructPrijatelja[list[n].index].profilePicture;
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
					((Component)_GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN/Friends Level Win " + num3 + " HOLDER/FB")).gameObject.SetActive(true);
					((Component)_GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN/Friends Level Win " + num3 + " HOLDER/Friends Level Win " + num3)).gameObject.SetActive(false);
				}
			}
			num3++;
		}
		if (list.Count < 5)
		{
			for (int num5 = num3; num5 <= 5; num5++)
			{
				((Component)_GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN/Friends Level Win " + num5 + " HOLDER/Friends Level Win " + num5)).gameObject.SetActive(false);
			}
		}
		if (!flag2)
		{
			Transform val = _GUI.Find("MISSION HOLDER/AnimationHolderGlavni/AnimationHolder/Friends FB level WIN/Friends Level Win 5 HOLDER");
			((Component)val.Find("Friends Level Win 5/Friends Level Win Picture 5")).GetComponent<Renderer>().material.mainTexture = FacebookManager.ListaStructPrijatelja[list[num4 - 1].index].profilePicture;
			((Component)val.Find("Friends Level Win 5/Friends Level Win Picture 5/Points Number level win fb")).GetComponent<TextMesh>().text = list[num4 - 1].score.ToString();
			((Component)val.Find("Friends Level Win 5/Friends Level Win Picture 5/Position Number")).GetComponent<TextMesh>().text = num4.ToString();
			((Component)val.Find("Friends Level Win 5")).GetComponent<SpriteRenderer>().sprite = ((Component)val.parent.Find("ReferencaYOU")).GetComponent<SpriteRenderer>().sprite;
		}
		list.Clear();
	}

	private void prebaciStrelicuNaItem()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		TutorialShop.transform.position = new Vector3(-20f, -105.5f, -75f);
		TutorialShop.transform.GetChild(0).Find("SpotLightMalaKocka2").localPosition = new Vector3(-1.6f, -2.39f, 0f);
		TutorialShop.transform.GetChild(0).Find("RedArrowHolder").localPosition = new Vector3(0.5f, 0.4f, -0.8f);
		TutorialShop.transform.GetChild(0).Find("RedArrowHolder").rotation = Quaternion.Euler(0f, 0f, -43f);
		TutorialShop.SetActive(true);
		((Component)TutorialShop.transform.GetChild(0)).GetComponent<Animation>().Play();
		((Component)TutorialShop.transform.GetChild(0).Find("RedArrowHolder/RedArrow")).GetComponent<Animation>().Play();
	}

	private void spustiPopup()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Transform obj = popupZaSpustanje;
		obj.localPosition += new Vector3(0f, -35f, 0f);
		popupZaSpustanje = null;
	}

	private IEnumerator checkConnectionForLoginButton()
	{
		((MonoBehaviour)this).StartCoroutine(CheckInternetConnection.Instance.checkInternetConnection());
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
	}

	private IEnumerator checkConnectionForPageLike(string url, string key)
	{
		((MonoBehaviour)this).StartCoroutine(CheckInternetConnection.Instance.checkInternetConnection());
		while (!CheckInternetConnection.Instance.checkDone)
		{
			yield return null;
		}
		if (!CheckInternetConnection.Instance.internetOK)
		{
			CheckInternetConnection.Instance.openPopup();
		}
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
			StagesParser.sceneID = 1;
			if (Advertisement.IsReady())
			{
				Advertisement.Show((string)null, new ShowOptions
				{
					resultCallback = delegate(ShowResult result)
					{
						Debug.Log((object)((object)(ShowResult)(ref result)).ToString());
						if (((object)(ShowResult)(ref result)).ToString().Equals("Finished"))
						{
							if (StagesParser.sceneID == 0)
							{
								Debug.Log((object)"ovde li sam");
								StagesParser.currentMoney += StagesParser.watchVideoReward;
								PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
								PlayerPrefs.Save();
								((MonoBehaviour)this).StartCoroutine(StagesParser.Instance.moneyCounter(StagesParser.watchVideoReward, ((Component)((Component)ShopManagerFull.ShopObject).transform.Find("Shop Interface/Coins/Coins Number")).GetComponent<TextMesh>(), hasOutline: true));
							}
							else if (StagesParser.sceneID == 1)
							{
								((Component)Camera.main).SendMessage("WatchVideoCallback", (object)1, (SendMessageOptions)1);
							}
							else if (StagesParser.sceneID == 2)
							{
								GameObject.Find("_GameManager").SendMessage("WatchVideoCallback", (SendMessageOptions)1);
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
	}

	private IEnumerator checkConnectionForInviteFriend()
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

	private IEnumerator checkConnectionForTelevizor()
	{
		((MonoBehaviour)this).StartCoroutine(CheckInternetConnection.Instance.checkInternetConnection());
		while (!CheckInternetConnection.Instance.checkDone)
		{
			yield return null;
		}
		if (CheckInternetConnection.Instance.internetOK)
		{
			popupZaSpustanje = _GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni");
			((MonoBehaviour)this).Invoke("spustiPopup", 0.5f);
			((Component)_GUI.Find("WATCH VIDEO HOLDER/AnimationHolderGlavni")).GetComponent<Animator>().Play("ClosePopup");
			makniPopup = 0;
			bool flag = false;
			string @string = PlayerPrefs.GetString("WatchVideoWorld" + (StagesParser.currSetIndex + 1));
			if (PlayerPrefs.HasKey("WatchVideoWorld" + (StagesParser.currSetIndex + 1)))
			{
				string[] array = @string.Split(new char[1] { '#' });
				for (int i = 0; i < array.Length; i++)
				{
					if (int.Parse(array[i]) == trenutniTelevizor)
					{
						flag = true;
					}
				}
				if (!flag)
				{
					@string = @string + "#" + trenutniTelevizor;
					PlayerPrefs.SetString("WatchVideoWorld" + (StagesParser.currSetIndex + 1), @string);
					PlayerPrefs.Save();
				}
				((Component)Televizori[trenutniTelevizor - 1]).gameObject.SetActive(false);
			}
			else
			{
				((Component)Televizori[trenutniTelevizor - 1]).gameObject.SetActive(false);
				PlayerPrefs.SetString("WatchVideoWorld" + (StagesParser.currSetIndex + 1), trenutniTelevizor.ToString());
				PlayerPrefs.Save();
			}
			if (!televizorIzabrao)
			{
				animator.Play("Running");
				((MonoBehaviour)this).StartCoroutine("KretanjeMajmunceta");
			}
			else
			{
				televizorIzabrao = false;
			}
			StagesParser.sceneID = 1;
		}
		else
		{
			CheckInternetConnection.Instance.openPopup();
		}
	}

	public void WatchVideoCallback(int value)
	{
		switch (value)
		{
		case 1:
			if (makniPopup == 0)
			{
				StagesParser.currentMoney += televizorCenePoSvetovima[StagesParser.currSetIndex];
				PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
				PlayerPrefs.Save();
				((MonoBehaviour)this).StartCoroutine(StagesParser.Instance.moneyCounter(televizorCenePoSvetovima[StagesParser.currSetIndex], ((Component)_GUI.Find("INTERFACE HOLDER/_TopLeft/Coins/Coins Number")).GetComponent<TextMesh>(), hasOutline: true));
				StagesParser.ServerUpdate = 1;
			}
			else if (makniPopup == 8)
			{
				StagesParser.currentMoney += StagesParser.watchVideoReward;
				PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
				PlayerPrefs.Save();
				((MonoBehaviour)this).StartCoroutine(StagesParser.Instance.moneyCounter(StagesParser.watchVideoReward, ((Component)((Component)ShopManagerFull.ShopObject).transform.Find("Shop Interface/Coins/Coins Number")).GetComponent<TextMesh>(), hasOutline: true));
			}
			break;
		case 3:
			CheckInternetConnection.Instance.NoVideosAvailable_OpenPopup();
			break;
		}
	}
}
