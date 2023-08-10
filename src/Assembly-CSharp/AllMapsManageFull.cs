using System.Collections;
using UnityEngine;

public class AllMapsManageFull : MonoBehaviour
{
	private float TrenutniX;

	private float TrenutniY;

	private int[] Klik = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

	private int KliknutoNa;

	private float startX;

	private float endX;

	private float startY;

	private float endY;

	private float vremeKlika;

	private string clickedItem;

	private string releasedItem;

	private bool moved;

	private bool released;

	private bool bounce;

	private float pomerajX;

	private float pomerajY;

	private GameObject bonusIsland;

	private float levaGranica = 87.9f;

	private float desnaGranica = 95.44f;

	private Transform lifeManager;

	public Transform levo;

	public Transform desno;

	private Transform _GUI;

	private Camera guiCamera;

	private float razlikaX;

	private float razlikaY;

	private GameObject temp;

	private Vector3 originalScale;

	public static int makniPopup;

	private void Awake()
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		if (PlayerPrefs.HasKey("PrvoPokretanjeIgre"))
		{
			if (PlayerPrefs.GetInt("PrvoPokretanjeIgre") == 1)
			{
				StagesParser.openedButNotPlayed[0] = false;
			}
		}
		else
		{
			StagesParser.openedButNotPlayed[0] = true;
		}
		_GUI = GameObject.Find("_GUI/INTERFACE HOLDER").transform;
		guiCamera = GameObject.Find("GUICamera").GetComponent<Camera>();
		KliknutoNa = 0;
		levaGranica = levo.position.x + Camera.main.orthographicSize * Camera.main.aspect;
		desnaGranica = desno.position.x - Camera.main.orthographicSize * Camera.main.aspect;
		InitWorlds();
		((Component)Camera.main).transform.position = new Vector3(Mathf.Clamp(GameObject.Find(StagesParser.worldToFocus.ToString()).transform.position.x, levaGranica, desnaGranica), ((Component)Camera.main).transform.position.y, ((Component)Camera.main).transform.position.z);
	}

	private void RefreshScene()
	{
		InitWorlds();
		((Component)_GUI.Find("_TopLeft/Coins/Coins Number")).GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
		((Component)_GUI.Find("_TopLeft/Coins/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		((Component)_GUI.Find("_TopLeft/PTS/PTS Number")).GetComponent<TextMesh>().text = StagesParser.currentPoints.ToString();
		((Component)_GUI.Find("_TopLeft/PTS/PTS Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		((Component)_GUI.Find("_TopLeft/Bananas/Banana Number")).GetComponent<TextMesh>().text = StagesParser.currentBananas.ToString();
		((Component)_GUI.Find("_TopLeft/Bananas/Banana Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		((Component)_GUI.Find("FB Login/Text/Number")).GetComponent<TextMesh>().text = "+" + StagesParser.LoginReward;
		((Component)_GUI.Find("FB Login/Text/Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		bool flag = true;
		for (int i = 0; i < StagesParser.totalSets; i++)
		{
			GameObject.Find("AllWorlds_holder/" + i + "/TotalStars/Stars Number").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			if (i > 0 && ((Component)GameObject.Find("AllWorlds_holder").transform.Find("HolderClouds" + i)).gameObject.activeSelf)
			{
				((Component)GameObject.Find("AllWorlds_holder").transform.Find("HolderClouds" + i + "/CloudsMove/AllMapsKatanac/Stars Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
				((Component)GameObject.Find("AllWorlds_holder").transform.Find("HolderClouds" + i + "/CloudsMove/AllMapsKatanac/Level Text")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
				((Component)GameObject.Find("AllWorlds_holder").transform.Find(i + "/LevelsHolder")).gameObject.SetActive(false);
				if (!StagesParser.unlockedWorlds[i])
				{
					if (!flag)
					{
						UgasiOstrvce(i);
						continue;
					}
					flag = false;
					UpaliOstrvce(i);
				}
			}
			else
			{
				IspisiBrojLevela(i);
			}
		}
		changeLanguage();
		CheckInternetConnection.Instance.refreshText();
		StagesParser.LoadingPoruke.Clear();
		StagesParser.RedniBrojSlike.Clear();
		StagesParser.Instance.UcitajLoadingPoruke();
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
				FacebookManager.MestoPozivanjaLogina = 2;
				FacebookManager.FacebookObject.FacebookLogin();
			}
		}
		else
		{
			CheckInternetConnection.Instance.openPopup();
		}
	}

	private void Start()
	{
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		makniPopup = 0;
		changeLanguage();
		if ((Object)(object)Loading.Instance != (Object)null)
		{
			((MonoBehaviour)this).StartCoroutine(Loading.Instance.UcitanaScena(guiCamera, 3, 0f));
		}
		if (StagesParser.vratioSeNaSvaOstrva)
		{
			((Component)_GUI.parent.Find("LOADING HOLDER NEW/Loading Animation Vrata")).GetComponent<Animator>().Play("Loading Zidovi Odlazak");
			StagesParser.vratioSeNaSvaOstrva = false;
		}
		((Component)_GUI.Find("_TopLeft/Coins/Coins Number")).GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
		((Component)_GUI.Find("_TopLeft/Coins/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		((Component)_GUI.Find("_TopLeft/PTS/PTS Number")).GetComponent<TextMesh>().text = StagesParser.currentPoints.ToString();
		((Component)_GUI.Find("_TopLeft/PTS/PTS Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		((Component)_GUI.Find("_TopLeft/Bananas/Banana Number")).GetComponent<TextMesh>().text = StagesParser.currentBananas.ToString();
		((Component)_GUI.Find("_TopLeft/Bananas/Banana Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		((Component)_GUI.Find("FB Login/Text/Number")).GetComponent<TextMesh>().text = "+" + StagesParser.LoginReward;
		((Component)_GUI.Find("FB Login/Text/Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		if (PlaySounds.musicOn && !PlaySounds.BackgroundMusic_Menu.isPlaying)
		{
			PlaySounds.Play_BackgroundMusic_Menu();
		}
		_GUI.Find("_TopLeft").position = new Vector3(guiCamera.ViewportToWorldPoint(Vector3.zero).x, _GUI.Find("_TopLeft").position.y, _GUI.Find("_TopLeft").position.z);
		_GUI.Find("FB Login").position = new Vector3(guiCamera.ViewportToWorldPoint(new Vector3(0.91f, 0f, 0f)).x, _GUI.Find("FB Login").position.y, _GUI.Find("FB Login").position.z);
		_GUI.Find("TotalStars").position = new Vector3(guiCamera.ViewportToWorldPoint(new Vector3(0.89f, 0f, 0f)).x, _GUI.Find("TotalStars").position.y, _GUI.Find("TotalStars").position.z);
		bool flag = true;
		for (int i = 0; i < StagesParser.totalSets; i++)
		{
			GameObject.Find("AllWorlds_holder/" + i + "/TotalStars/Stars Number").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			GameObject.Find("AllWorlds_holder/" + i + "/LevelsHolder/LevelText").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			GameObject.Find("AllWorlds_holder/" + i + "/LevelsHolder/LevelValue").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			if (i <= 0 || !((Component)GameObject.Find("AllWorlds_holder").transform.Find("HolderClouds" + i)).gameObject.activeSelf)
			{
				continue;
			}
			((Component)GameObject.Find("AllWorlds_holder").transform.Find("HolderClouds" + i + "/CloudsMove/AllMapsKatanac/Stars Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			((Component)GameObject.Find("AllWorlds_holder").transform.Find("HolderClouds" + i + "/CloudsMove/AllMapsKatanac/Level Text")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			GameObject.Find("AllWorlds_holder/" + i + "/LevelsHolder").SetActive(false);
			if (!StagesParser.unlockedWorlds[i])
			{
				if (!flag)
				{
					UgasiOstrvce(i);
				}
				else
				{
					flag = false;
				}
			}
		}
		if (FB.IsLoggedIn)
		{
			GameObject.Find("FB Login").SetActive(false);
		}
	}

	private void UgasiOstrvce(int index)
	{
		Transform transform = ((Component)GameObject.Find("AllWorlds_holder").transform.Find("HolderClouds" + index + "/CloudsMove/AllMapsKatanac")).transform;
		((Component)transform.Find("Level Number")).gameObject.SetActive(false);
		((Component)transform.Find("Level Text")).gameObject.SetActive(false);
		((Component)transform.Find("Fields/BlueFields/BlueField2")).gameObject.SetActive(false);
		((Component)transform.Find("Fields/BlueFields/BlueField4")).gameObject.SetActive(false);
		((Component)transform.Find("Fields/BlueFields/WhiteFields/WhiteField2")).gameObject.SetActive(false);
		((Component)transform.Find("Fields/BlueFields/WhiteFields/WhiteField3")).gameObject.SetActive(false);
		((Component)transform.Find("Fields/TextField2")).gameObject.SetActive(false);
		((Component)transform.Find("Fields/Senka")).gameObject.SetActive(false);
		((Component)transform.Find("Fields/LastIslandIconHOLDER")).gameObject.SetActive(false);
	}

	private void UpaliOstrvce(int index)
	{
		Transform transform = ((Component)GameObject.Find("AllWorlds_holder").transform.Find("HolderClouds" + index + "/CloudsMove/AllMapsKatanac")).transform;
		if (!((Component)transform.Find("Fields/BlueFields/BlueField2")).gameObject.activeSelf)
		{
			((Component)transform.Find("Level Number")).gameObject.SetActive(true);
			((Component)transform.Find("Level Text")).gameObject.SetActive(true);
			((Component)transform.Find("Fields/BlueFields/BlueField2")).gameObject.SetActive(true);
			((Component)transform.Find("Fields/BlueFields/BlueField4")).gameObject.SetActive(true);
			((Component)transform.Find("Fields/BlueFields/WhiteFields/WhiteField2")).gameObject.SetActive(true);
			((Component)transform.Find("Fields/BlueFields/WhiteFields/WhiteField3")).gameObject.SetActive(true);
			((Component)transform.Find("Fields/TextField2")).gameObject.SetActive(true);
			((Component)transform.Find("Fields/Senka")).gameObject.SetActive(true);
			((Component)transform.Find("Fields/LastIslandIconHOLDER")).gameObject.SetActive(true);
		}
	}

	private void IspisiBrojLevela(int index)
	{
		GameObject gameObject = ((Component)GameObject.Find("AllWorlds_holder").transform.Find(index + "/LevelsHolder")).gameObject;
		gameObject.SetActive(true);
		((Component)gameObject.transform.Find("LevelText")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		((Component)gameObject.transform.Find("LevelValue")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
	}

	private void InitWorlds()
	{
		for (int i = 0; i < StagesParser.totalSets; i++)
		{
			if (StagesParser.currentStarsNEW >= StagesParser.SetsInGame[i].StarRequirement && i > 0)
			{
				if (int.Parse(StagesParser.allLevels[(i - 1) * 20 + 19].Split(new char[1] { '#' })[1]) > 0)
				{
					if (StagesParser.allLevels[i * 20].Split(new char[1] { '#' })[1].Equals("-1"))
					{
						StagesParser.allLevels[i * 20] = i * 20 + 1 + "#0#0";
						StagesParser.StarsPoNivoima[i * 20] = 0;
					}
					StagesParser.unlockedWorlds[i] = true;
					StagesParser.lastUnlockedWorldIndex = i;
					if (StagesParser.openedButNotPlayed[i])
					{
						((MonoBehaviour)this).StartCoroutine("AnimacijaOblakaOstrvo", (object)i);
						StagesParser.openedButNotPlayed[i] = false;
					}
					else
					{
						GameObject.Find("HolderClouds" + i).SetActive(false);
						if (StagesParser.lastUnlockedWorldIndex == 5 && FB.IsLoggedIn)
						{
							for (int j = 0; j < FacebookManager.ListaStructPrijatelja.Count; j++)
							{
								if (FacebookManager.ListaStructPrijatelja[j].PrijateljID.Equals(FacebookManager.User) && FacebookManager.ListaStructPrijatelja[j].scores.Count < StagesParser.allLevels.Length)
								{
									for (int k = FacebookManager.ListaStructPrijatelja[j].scores.Count; k < StagesParser.allLevels.Length; k++)
									{
										FacebookManager.ListaStructPrijatelja[j].scores.Add(0);
									}
								}
							}
						}
					}
				}
				else
				{
					StagesParser.unlockedWorlds[i] = false;
				}
			}
			GameObject.Find("AllWorlds_holder/" + i + "/TotalStars/Stars Number").GetComponent<TextMesh>().text = StagesParser.SetsInGame[i].CurrentStarsInStageNEW + "/" + StagesParser.SetsInGame[i].StagesOnSet * 3;
			((Component)GameObject.Find("AllWorlds_holder").transform.Find(i + "/LevelsHolder/LevelText")).GetComponent<TextMesh>().text = LanguageManager.Level;
			((Component)GameObject.Find("AllWorlds_holder").transform.Find(i + "/LevelsHolder/LevelValue")).GetComponent<TextMesh>().text = StagesParser.maxLevelNaOstrvu[i] + "/20";
			if (i > 0)
			{
				((Component)GameObject.Find("AllWorlds_holder").transform.Find("HolderClouds" + i + "/CloudsMove/AllMapsKatanac/Stars Number")).GetComponent<TextMesh>().text = StagesParser.SetsInGame[i].StarRequirement.ToString();
				((Component)GameObject.Find("AllWorlds_holder").transform.Find("HolderClouds" + i + "/CloudsMove/AllMapsKatanac/Level Text")).GetComponent<TextMesh>().text = LanguageManager.Level;
			}
		}
		((Component)_GUI.Find("TotalStars/Stars Number")).GetComponent<TextMesh>().text = StagesParser.currentStarsNEW + "/" + StagesParser.totalStars;
		((Component)_GUI.Find("TotalStars/Stars Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
	}

	private void Update()
	{
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05db: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_061c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0633: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0663: Unknown result type (might be due to invalid IL or missing references)
		//IL_0565: Unknown result type (might be due to invalid IL or missing references)
		//IL_058a: Unknown result type (might be due to invalid IL or missing references)
		//IL_059e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0700: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0748: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_0790: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0820: Unknown result type (might be due to invalid IL or missing references)
		if (Input.GetKeyUp((KeyCode)27))
		{
			if (makniPopup == 1)
			{
				makniPopup = 0;
				((MonoBehaviour)this).StartCoroutine(CheckInternetConnection.Instance.ClosePopup());
			}
			else if (makniPopup == 0)
			{
				if (StagesParser.ServerUpdate == 1 && FB.IsLoggedIn)
				{
					FacebookManager.FacebookObject.scoreToSet = StagesParser.currentPoints;
					FacebookManager.FacebookObject.proveraPublish_ActionPermisije();
					FacebookManager.FacebookObject.SacuvajScoreNaNivoima(StagesParser.PointsPoNivoima, StagesParser.StarsPoNivoima, StagesParser.maxLevel, StagesParser.bonusLevels);
					FacebookManager.FacebookObject.UpdateujPodatkeKorisnika(StagesParser.currentMoney, StagesParser.currentPoints, LanguageManager.chosenLanguage, StagesParser.currentBananas, StagesParser.powerup_magnets, StagesParser.powerup_shields, StagesParser.powerup_doublecoins, StagesParser.svekupovineGlava, StagesParser.svekupovineMajica, StagesParser.svekupovineLedja, StagesParser.ledja, StagesParser.glava, StagesParser.majica, StagesParser.imaUsi, StagesParser.imaKosu, FacebookManager.NumberOfFriends);
				}
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_Button_GoBack();
				}
				Application.LoadLevel(1);
			}
		}
		if (Input.GetMouseButtonDown(0))
		{
			if (released)
			{
				released = false;
			}
			clickedItem = RaycastFunction(Input.mousePosition);
			startX = Input.mousePosition.x;
			startY = Input.mousePosition.y;
			vremeKlika = Time.time;
			razlikaX = Input.mousePosition.x;
			razlikaY = Input.mousePosition.y;
			if (clickedItem.Equals("Button_CheckOK"))
			{
				temp = GameObject.Find(clickedItem);
				originalScale = temp.transform.localScale;
				temp.transform.localScale = originalScale * 1.2f;
			}
			else if (clickedItem != string.Empty)
			{
				temp = GameObject.Find(clickedItem);
				originalScale = temp.transform.localScale;
			}
			if (RaycastFunction(Input.mousePosition) == "0")
			{
				KliknutoNa = 1;
			}
			else if (RaycastFunction(Input.mousePosition) == "1")
			{
				KliknutoNa = 2;
			}
			else if (RaycastFunction(Input.mousePosition) == "2")
			{
				KliknutoNa = 3;
			}
			else if (RaycastFunction(Input.mousePosition) == "3")
			{
				KliknutoNa = 4;
			}
			else if (RaycastFunction(Input.mousePosition) == "4")
			{
				KliknutoNa = 5;
			}
			else if (RaycastFunction(Input.mousePosition) == "HouseShop")
			{
				KliknutoNa = 6;
			}
			else if (RaycastFunction(Input.mousePosition) == "HolderBonus")
			{
				KliknutoNa = 7;
			}
			else if (RaycastFunction(Input.mousePosition) == "HolderShipFreeCoins")
			{
				KliknutoNa = 8;
			}
			else if (RaycastFunction(Input.mousePosition) == "ButtonBackToMenu")
			{
				KliknutoNa = 9;
			}
		}
		if (Input.GetMouseButton(0) && makniPopup == 0)
		{
			endX = Input.mousePosition.x;
			pomerajX = (endX - startX) / 45f;
			endY = Input.mousePosition.y;
			pomerajY = (endY - startY) / 45f;
			if (pomerajX != 0f || pomerajY != 0f)
			{
				moved = true;
			}
			float num = Mathf.Clamp(Mathf.Lerp(((Component)Camera.main).transform.position.y, ((Component)Camera.main).transform.position.y - pomerajY, 0.75f), -12.56f, 42.56f);
			float num2 = Mathf.Clamp(Mathf.Lerp(((Component)Camera.main).transform.position.x, ((Component)Camera.main).transform.position.x - pomerajX, 0.75f), levaGranica, desnaGranica);
			((Component)Camera.main).transform.position = new Vector3(num2, num, ((Component)Camera.main).transform.position.z);
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
			else if (((Component)Camera.main).transform.position.x >= desnaGranica - 0.25f && bounce)
			{
				pomerajX = 0.04f;
				bounce = false;
			}
			((Component)Camera.main).transform.Translate(0f - pomerajX, 0f, 0f);
			pomerajX *= 0.92f;
			((Component)Camera.main).transform.position = new Vector3(Mathf.Clamp(((Component)Camera.main).transform.position.x, levaGranica, desnaGranica), ((Component)Camera.main).transform.position.y, ((Component)Camera.main).transform.position.z);
		}
		_ = ((Component)Camera.main).transform.position.x;
		_ = desnaGranica;
		if (!Input.GetMouseButtonUp(0))
		{
			return;
		}
		releasedItem = RaycastFunction(Input.mousePosition);
		if (moved)
		{
			moved = false;
			released = true;
			bounce = true;
		}
		startX = (endX = 0f);
		razlikaX = Input.mousePosition.x - razlikaX;
		razlikaY = Input.mousePosition.y - razlikaY;
		if ((Object)(object)temp != (Object)null)
		{
			temp.transform.localScale = originalScale;
		}
		if (!(clickedItem == releasedItem) || !(Time.time - vremeKlika < 0.35f) || !(Mathf.Abs(razlikaX) < 40f) || !(Mathf.Abs(razlikaY) < 40f))
		{
			return;
		}
		if (RaycastFunction(Input.mousePosition) == "0")
		{
			StagesParser.currSetIndex = 0;
			StagesParser.currentWorld = 1;
			StagesParser.zadnjiOtkljucanNivo = 0;
			((MonoBehaviour)this).StartCoroutine(UcitajOstrvo("AllMaps"));
		}
		else if (RaycastFunction(Input.mousePosition) == "1")
		{
			if (StagesParser.unlockedWorlds[1])
			{
				StagesParser.currSetIndex = 1;
				StagesParser.currentWorld = 2;
				StagesParser.zadnjiOtkljucanNivo = 0;
				((MonoBehaviour)this).StartCoroutine(UcitajOstrvo("_Mapa 2 Savanna"));
			}
		}
		else if (RaycastFunction(Input.mousePosition) == "2")
		{
			if (StagesParser.unlockedWorlds[2])
			{
				StagesParser.currSetIndex = 2;
				StagesParser.currentWorld = 3;
				StagesParser.zadnjiOtkljucanNivo = 0;
				((MonoBehaviour)this).StartCoroutine(UcitajOstrvo("_Mapa 3 Jungle"));
			}
		}
		else if (RaycastFunction(Input.mousePosition) == "3")
		{
			if (StagesParser.unlockedWorlds[3])
			{
				StagesParser.currSetIndex = 3;
				StagesParser.currentWorld = 4;
				StagesParser.zadnjiOtkljucanNivo = 0;
				((MonoBehaviour)this).StartCoroutine(UcitajOstrvo("_Mapa 4 Temple"));
			}
		}
		else if (RaycastFunction(Input.mousePosition) == "4")
		{
			if (StagesParser.unlockedWorlds[4])
			{
				StagesParser.currSetIndex = 4;
				StagesParser.currentWorld = 5;
				StagesParser.zadnjiOtkljucanNivo = 0;
				((MonoBehaviour)this).StartCoroutine(UcitajOstrvo("_Mapa 5 Volcano"));
			}
		}
		else if (RaycastFunction(Input.mousePosition) == "5")
		{
			if (StagesParser.unlockedWorlds[5])
			{
				StagesParser.currSetIndex = 5;
				StagesParser.currentWorld = 6;
				StagesParser.zadnjiOtkljucanNivo = 0;
				((MonoBehaviour)this).StartCoroutine(UcitajOstrvo("_Mapa 6 Ice"));
			}
		}
		else if (releasedItem.Equals("ButtonBackToMenu"))
		{
			if (KliknutoNa == Klik[9])
			{
				if (StagesParser.ServerUpdate == 1 && FB.IsLoggedIn)
				{
					FacebookManager.FacebookObject.scoreToSet = StagesParser.currentPoints;
					FacebookManager.FacebookObject.proveraPublish_ActionPermisije();
					FacebookManager.FacebookObject.SacuvajScoreNaNivoima(StagesParser.PointsPoNivoima, StagesParser.StarsPoNivoima, StagesParser.maxLevel, StagesParser.bonusLevels);
					FacebookManager.FacebookObject.UpdateujPodatkeKorisnika(StagesParser.currentMoney, StagesParser.currentPoints, LanguageManager.chosenLanguage, StagesParser.currentBananas, StagesParser.powerup_magnets, StagesParser.powerup_shields, StagesParser.powerup_doublecoins, StagesParser.svekupovineGlava, StagesParser.svekupovineMajica, StagesParser.svekupovineLedja, StagesParser.ledja, StagesParser.glava, StagesParser.majica, StagesParser.imaUsi, StagesParser.imaKosu, FacebookManager.NumberOfFriends);
				}
				if (PlaySounds.soundOn)
				{
					PlaySounds.Play_Button_GoBack();
				}
				Application.LoadLevel(1);
			}
		}
		else if (releasedItem.Equals("FB Login"))
		{
			makniPopup = 1;
			((MonoBehaviour)this).StartCoroutine(checkConnectionForLoginButton());
		}
		else if (releasedItem.Equals("Button_CheckOK"))
		{
			makniPopup = 0;
			((MonoBehaviour)this).StartCoroutine(CheckInternetConnection.Instance.ClosePopup());
		}
	}

	private IEnumerator AnimacijaOblakaOstrvo(int index)
	{
		yield return (object)new WaitForSeconds(1f);
		GameObject.Find("HolderClouds" + index + "/CloudsMove").GetComponent<Animation>().Play("CloudsOpenMap");
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

	private IEnumerator UcitajOstrvo(string ime)
	{
		if (PlaySounds.soundOn)
		{
			PlaySounds.Play_Button_OpenWorld();
		}
		((Component)_GUI.parent.Find("LOADING HOLDER NEW/Loading Animation Vrata")).GetComponent<Animator>().Play("Loading Zidovi Dolazak");
		yield return (object)new WaitForSeconds(1.1f);
		Application.LoadLevel(ime);
	}

	private void changeLanguage()
	{
		if (!FB.IsLoggedIn)
		{
			GameObject.Find("Log In").GetComponent<TextMesh>().text = LanguageManager.LogIn;
			GameObject.Find("Log In").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		}
		GameObject.Find("Coming Soon").GetComponent<TextMesh>().text = LanguageManager.ComingSoon;
		GameObject.Find("Coming Soon").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
	}
}
