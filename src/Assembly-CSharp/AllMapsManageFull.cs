using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200065C RID: 1628
public class AllMapsManageFull : MonoBehaviour
{
	// Token: 0x060028A2 RID: 10402 RVA: 0x0013D61C File Offset: 0x0013B81C
	private void Awake()
	{
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
		this._GUI = GameObject.Find("_GUI/INTERFACE HOLDER").transform;
		this.guiCamera = GameObject.Find("GUICamera").GetComponent<Camera>();
		this.KliknutoNa = 0;
		this.levaGranica = this.levo.position.x + Camera.main.orthographicSize * Camera.main.aspect;
		this.desnaGranica = this.desno.position.x - Camera.main.orthographicSize * Camera.main.aspect;
		this.InitWorlds();
		Camera.main.transform.position = new Vector3(Mathf.Clamp(GameObject.Find(StagesParser.worldToFocus.ToString()).transform.position.x, this.levaGranica, this.desnaGranica), Camera.main.transform.position.y, Camera.main.transform.position.z);
	}

	// Token: 0x060028A3 RID: 10403 RVA: 0x0013D750 File Offset: 0x0013B950
	private void RefreshScene()
	{
		this.InitWorlds();
		this._GUI.Find("_TopLeft/Coins/Coins Number").GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
		this._GUI.Find("_TopLeft/Coins/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		this._GUI.Find("_TopLeft/PTS/PTS Number").GetComponent<TextMesh>().text = StagesParser.currentPoints.ToString();
		this._GUI.Find("_TopLeft/PTS/PTS Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		this._GUI.Find("_TopLeft/Bananas/Banana Number").GetComponent<TextMesh>().text = StagesParser.currentBananas.ToString();
		this._GUI.Find("_TopLeft/Bananas/Banana Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		this._GUI.Find("FB Login/Text/Number").GetComponent<TextMesh>().text = "+" + StagesParser.LoginReward.ToString();
		this._GUI.Find("FB Login/Text/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		bool flag = true;
		for (int i = 0; i < StagesParser.totalSets; i++)
		{
			GameObject.Find("AllWorlds_holder/" + i.ToString() + "/TotalStars/Stars Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
			if (i > 0 && GameObject.Find("AllWorlds_holder").transform.Find("HolderClouds" + i.ToString()).gameObject.activeSelf)
			{
				GameObject.Find("AllWorlds_holder").transform.Find("HolderClouds" + i.ToString() + "/CloudsMove/AllMapsKatanac/Stars Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
				GameObject.Find("AllWorlds_holder").transform.Find("HolderClouds" + i.ToString() + "/CloudsMove/AllMapsKatanac/Level Text").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
				GameObject.Find("AllWorlds_holder").transform.Find(i.ToString() + "/LevelsHolder").gameObject.SetActive(false);
				if (!StagesParser.unlockedWorlds[i])
				{
					if (!flag)
					{
						this.UgasiOstrvce(i);
					}
					else
					{
						flag = false;
						this.UpaliOstrvce(i);
					}
				}
			}
			else
			{
				this.IspisiBrojLevela(i);
			}
		}
		this.changeLanguage();
		CheckInternetConnection.Instance.refreshText();
		StagesParser.LoadingPoruke.Clear();
		StagesParser.RedniBrojSlike.Clear();
		StagesParser.Instance.UcitajLoadingPoruke();
	}

	// Token: 0x060028A4 RID: 10404 RVA: 0x0001FBE2 File Offset: 0x0001DDE2
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
				FacebookManager.MestoPozivanjaLogina = 2;
				FacebookManager.FacebookObject.FacebookLogin();
			}
		}
		else
		{
			CheckInternetConnection.Instance.openPopup();
		}
		yield break;
	}

	// Token: 0x060028A5 RID: 10405 RVA: 0x0013D9E0 File Offset: 0x0013BBE0
	private void Start()
	{
		AllMapsManageFull.makniPopup = 0;
		this.changeLanguage();
		if (Loading.Instance != null)
		{
			base.StartCoroutine(Loading.Instance.UcitanaScena(this.guiCamera, 3, 0f));
		}
		if (StagesParser.vratioSeNaSvaOstrva)
		{
			this._GUI.parent.Find("LOADING HOLDER NEW/Loading Animation Vrata").GetComponent<Animator>().Play("Loading Zidovi Odlazak");
			StagesParser.vratioSeNaSvaOstrva = false;
		}
		this._GUI.Find("_TopLeft/Coins/Coins Number").GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
		this._GUI.Find("_TopLeft/Coins/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		this._GUI.Find("_TopLeft/PTS/PTS Number").GetComponent<TextMesh>().text = StagesParser.currentPoints.ToString();
		this._GUI.Find("_TopLeft/PTS/PTS Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		this._GUI.Find("_TopLeft/Bananas/Banana Number").GetComponent<TextMesh>().text = StagesParser.currentBananas.ToString();
		this._GUI.Find("_TopLeft/Bananas/Banana Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		this._GUI.Find("FB Login/Text/Number").GetComponent<TextMesh>().text = "+" + StagesParser.LoginReward.ToString();
		this._GUI.Find("FB Login/Text/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		if (PlaySounds.musicOn && !PlaySounds.BackgroundMusic_Menu.isPlaying)
		{
			PlaySounds.Play_BackgroundMusic_Menu();
		}
		this._GUI.Find("_TopLeft").position = new Vector3(this.guiCamera.ViewportToWorldPoint(Vector3.zero).x, this._GUI.Find("_TopLeft").position.y, this._GUI.Find("_TopLeft").position.z);
		this._GUI.Find("FB Login").position = new Vector3(this.guiCamera.ViewportToWorldPoint(new Vector3(0.91f, 0f, 0f)).x, this._GUI.Find("FB Login").position.y, this._GUI.Find("FB Login").position.z);
		this._GUI.Find("TotalStars").position = new Vector3(this.guiCamera.ViewportToWorldPoint(new Vector3(0.89f, 0f, 0f)).x, this._GUI.Find("TotalStars").position.y, this._GUI.Find("TotalStars").position.z);
		bool flag = true;
		for (int i = 0; i < StagesParser.totalSets; i++)
		{
			GameObject.Find("AllWorlds_holder/" + i.ToString() + "/TotalStars/Stars Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
			GameObject.Find("AllWorlds_holder/" + i.ToString() + "/LevelsHolder/LevelText").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
			GameObject.Find("AllWorlds_holder/" + i.ToString() + "/LevelsHolder/LevelValue").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
			if (i > 0 && GameObject.Find("AllWorlds_holder").transform.Find("HolderClouds" + i.ToString()).gameObject.activeSelf)
			{
				GameObject.Find("AllWorlds_holder").transform.Find("HolderClouds" + i.ToString() + "/CloudsMove/AllMapsKatanac/Stars Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
				GameObject.Find("AllWorlds_holder").transform.Find("HolderClouds" + i.ToString() + "/CloudsMove/AllMapsKatanac/Level Text").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
				GameObject.Find("AllWorlds_holder/" + i.ToString() + "/LevelsHolder").SetActive(false);
				if (!StagesParser.unlockedWorlds[i])
				{
					if (!flag)
					{
						this.UgasiOstrvce(i);
					}
					else
					{
						flag = false;
					}
				}
			}
		}
		if (FB.IsLoggedIn)
		{
			GameObject.Find("FB Login").SetActive(false);
		}
	}

	// Token: 0x060028A6 RID: 10406 RVA: 0x0013DE4C File Offset: 0x0013C04C
	private void UgasiOstrvce(int index)
	{
		Transform transform = GameObject.Find("AllWorlds_holder").transform.Find("HolderClouds" + index.ToString() + "/CloudsMove/AllMapsKatanac").transform;
		transform.Find("Level Number").gameObject.SetActive(false);
		transform.Find("Level Text").gameObject.SetActive(false);
		transform.Find("Fields/BlueFields/BlueField2").gameObject.SetActive(false);
		transform.Find("Fields/BlueFields/BlueField4").gameObject.SetActive(false);
		transform.Find("Fields/BlueFields/WhiteFields/WhiteField2").gameObject.SetActive(false);
		transform.Find("Fields/BlueFields/WhiteFields/WhiteField3").gameObject.SetActive(false);
		transform.Find("Fields/TextField2").gameObject.SetActive(false);
		transform.Find("Fields/Senka").gameObject.SetActive(false);
		transform.Find("Fields/LastIslandIconHOLDER").gameObject.SetActive(false);
	}

	// Token: 0x060028A7 RID: 10407 RVA: 0x0013DF50 File Offset: 0x0013C150
	private void UpaliOstrvce(int index)
	{
		Transform transform = GameObject.Find("AllWorlds_holder").transform.Find("HolderClouds" + index.ToString() + "/CloudsMove/AllMapsKatanac").transform;
		if (!transform.Find("Fields/BlueFields/BlueField2").gameObject.activeSelf)
		{
			transform.Find("Level Number").gameObject.SetActive(true);
			transform.Find("Level Text").gameObject.SetActive(true);
			transform.Find("Fields/BlueFields/BlueField2").gameObject.SetActive(true);
			transform.Find("Fields/BlueFields/BlueField4").gameObject.SetActive(true);
			transform.Find("Fields/BlueFields/WhiteFields/WhiteField2").gameObject.SetActive(true);
			transform.Find("Fields/BlueFields/WhiteFields/WhiteField3").gameObject.SetActive(true);
			transform.Find("Fields/TextField2").gameObject.SetActive(true);
			transform.Find("Fields/Senka").gameObject.SetActive(true);
			transform.Find("Fields/LastIslandIconHOLDER").gameObject.SetActive(true);
		}
	}

	// Token: 0x060028A8 RID: 10408 RVA: 0x0013E070 File Offset: 0x0013C270
	private void IspisiBrojLevela(int index)
	{
		GameObject gameObject = GameObject.Find("AllWorlds_holder").transform.Find(index.ToString() + "/LevelsHolder").gameObject;
		gameObject.SetActive(true);
		gameObject.transform.Find("LevelText").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		gameObject.transform.Find("LevelValue").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
	}

	// Token: 0x060028A9 RID: 10409 RVA: 0x0013E0E8 File Offset: 0x0013C2E8
	private void InitWorlds()
	{
		for (int i = 0; i < StagesParser.totalSets; i++)
		{
			if (StagesParser.currentStarsNEW >= StagesParser.SetsInGame[i].StarRequirement && i > 0)
			{
				if (int.Parse(StagesParser.allLevels[(i - 1) * 20 + 19].Split(new char[]
				{
					'#'
				})[1]) > 0)
				{
					if (StagesParser.allLevels[i * 20].Split(new char[]
					{
						'#'
					})[1].Equals("-1"))
					{
						StagesParser.allLevels[i * 20] = i * 20 + 1 + "#0#0";
						StagesParser.StarsPoNivoima[i * 20] = 0;
					}
					StagesParser.unlockedWorlds[i] = true;
					StagesParser.lastUnlockedWorldIndex = i;
					if (StagesParser.openedButNotPlayed[i])
					{
						base.StartCoroutine("AnimacijaOblakaOstrvo", i);
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
			GameObject.Find("AllWorlds_holder/" + i.ToString() + "/TotalStars/Stars Number").GetComponent<TextMesh>().text = StagesParser.SetsInGame[i].CurrentStarsInStageNEW + "/" + StagesParser.SetsInGame[i].StagesOnSet * 3;
			GameObject.Find("AllWorlds_holder").transform.Find(i.ToString() + "/LevelsHolder/LevelText").GetComponent<TextMesh>().text = LanguageManager.Level;
			GameObject.Find("AllWorlds_holder").transform.Find(i.ToString() + "/LevelsHolder/LevelValue").GetComponent<TextMesh>().text = StagesParser.maxLevelNaOstrvu[i] + "/20";
			if (i > 0)
			{
				GameObject.Find("AllWorlds_holder").transform.Find("HolderClouds" + i.ToString() + "/CloudsMove/AllMapsKatanac/Stars Number").GetComponent<TextMesh>().text = StagesParser.SetsInGame[i].StarRequirement.ToString();
				GameObject.Find("AllWorlds_holder").transform.Find("HolderClouds" + i.ToString() + "/CloudsMove/AllMapsKatanac/Level Text").GetComponent<TextMesh>().text = LanguageManager.Level;
			}
		}
		this._GUI.Find("TotalStars/Stars Number").GetComponent<TextMesh>().text = StagesParser.currentStarsNEW + "/" + StagesParser.totalStars;
		this._GUI.Find("TotalStars/Stars Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
	}

	// Token: 0x060028AA RID: 10410 RVA: 0x0013E450 File Offset: 0x0013C650
	private void Update()
	{
		if (Input.GetKeyUp(27))
		{
			if (AllMapsManageFull.makniPopup == 1)
			{
				AllMapsManageFull.makniPopup = 0;
				base.StartCoroutine(CheckInternetConnection.Instance.ClosePopup());
			}
			else if (AllMapsManageFull.makniPopup == 0)
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
			if (this.released)
			{
				this.released = false;
			}
			this.clickedItem = this.RaycastFunction(Input.mousePosition);
			this.startX = Input.mousePosition.x;
			this.startY = Input.mousePosition.y;
			this.vremeKlika = Time.time;
			this.razlikaX = Input.mousePosition.x;
			this.razlikaY = Input.mousePosition.y;
			if (this.clickedItem.Equals("Button_CheckOK"))
			{
				this.temp = GameObject.Find(this.clickedItem);
				this.originalScale = this.temp.transform.localScale;
				this.temp.transform.localScale = this.originalScale * 1.2f;
			}
			else if (this.clickedItem != string.Empty)
			{
				this.temp = GameObject.Find(this.clickedItem);
				this.originalScale = this.temp.transform.localScale;
			}
			if (this.RaycastFunction(Input.mousePosition) == "0")
			{
				this.KliknutoNa = 1;
			}
			else if (this.RaycastFunction(Input.mousePosition) == "1")
			{
				this.KliknutoNa = 2;
			}
			else if (this.RaycastFunction(Input.mousePosition) == "2")
			{
				this.KliknutoNa = 3;
			}
			else if (this.RaycastFunction(Input.mousePosition) == "3")
			{
				this.KliknutoNa = 4;
			}
			else if (this.RaycastFunction(Input.mousePosition) == "4")
			{
				this.KliknutoNa = 5;
			}
			else if (this.RaycastFunction(Input.mousePosition) == "HouseShop")
			{
				this.KliknutoNa = 6;
			}
			else if (this.RaycastFunction(Input.mousePosition) == "HolderBonus")
			{
				this.KliknutoNa = 7;
			}
			else if (this.RaycastFunction(Input.mousePosition) == "HolderShipFreeCoins")
			{
				this.KliknutoNa = 8;
			}
			else if (this.RaycastFunction(Input.mousePosition) == "ButtonBackToMenu")
			{
				this.KliknutoNa = 9;
			}
		}
		if (Input.GetMouseButton(0) && AllMapsManageFull.makniPopup == 0)
		{
			this.endX = Input.mousePosition.x;
			this.pomerajX = (this.endX - this.startX) / 45f;
			this.endY = Input.mousePosition.y;
			this.pomerajY = (this.endY - this.startY) / 45f;
			if (this.pomerajX != 0f || this.pomerajY != 0f)
			{
				this.moved = true;
			}
			float num = Mathf.Clamp(Mathf.Lerp(Camera.main.transform.position.y, Camera.main.transform.position.y - this.pomerajY, 0.75f), -12.56f, 42.56f);
			float num2 = Mathf.Clamp(Mathf.Lerp(Camera.main.transform.position.x, Camera.main.transform.position.x - this.pomerajX, 0.75f), this.levaGranica, this.desnaGranica);
			Camera.main.transform.position = new Vector3(num2, num, Camera.main.transform.position.z);
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
			else if (Camera.main.transform.position.x >= this.desnaGranica - 0.25f && this.bounce)
			{
				this.pomerajX = 0.04f;
				this.bounce = false;
			}
			Camera.main.transform.Translate(-this.pomerajX, 0f, 0f);
			this.pomerajX *= 0.92f;
			Camera.main.transform.position = new Vector3(Mathf.Clamp(Camera.main.transform.position.x, this.levaGranica, this.desnaGranica), Camera.main.transform.position.y, Camera.main.transform.position.z);
		}
		float x = Camera.main.transform.position.x;
		float num3 = this.desnaGranica;
		if (Input.GetMouseButtonUp(0))
		{
			this.releasedItem = this.RaycastFunction(Input.mousePosition);
			if (this.moved)
			{
				this.moved = false;
				this.released = true;
				this.bounce = true;
			}
			this.startX = (this.endX = 0f);
			this.razlikaX = Input.mousePosition.x - this.razlikaX;
			this.razlikaY = Input.mousePosition.y - this.razlikaY;
			if (this.temp != null)
			{
				this.temp.transform.localScale = this.originalScale;
			}
			if (this.clickedItem == this.releasedItem && Time.time - this.vremeKlika < 0.35f && Mathf.Abs(this.razlikaX) < 40f && Mathf.Abs(this.razlikaY) < 40f)
			{
				if (this.RaycastFunction(Input.mousePosition) == "0")
				{
					StagesParser.currSetIndex = 0;
					StagesParser.currentWorld = 1;
					StagesParser.zadnjiOtkljucanNivo = 0;
					base.StartCoroutine(this.UcitajOstrvo("AllMaps"));
					return;
				}
				if (this.RaycastFunction(Input.mousePosition) == "1")
				{
					if (StagesParser.unlockedWorlds[1])
					{
						StagesParser.currSetIndex = 1;
						StagesParser.currentWorld = 2;
						StagesParser.zadnjiOtkljucanNivo = 0;
						base.StartCoroutine(this.UcitajOstrvo("_Mapa 2 Savanna"));
						return;
					}
				}
				else if (this.RaycastFunction(Input.mousePosition) == "2")
				{
					if (StagesParser.unlockedWorlds[2])
					{
						StagesParser.currSetIndex = 2;
						StagesParser.currentWorld = 3;
						StagesParser.zadnjiOtkljucanNivo = 0;
						base.StartCoroutine(this.UcitajOstrvo("_Mapa 3 Jungle"));
						return;
					}
				}
				else if (this.RaycastFunction(Input.mousePosition) == "3")
				{
					if (StagesParser.unlockedWorlds[3])
					{
						StagesParser.currSetIndex = 3;
						StagesParser.currentWorld = 4;
						StagesParser.zadnjiOtkljucanNivo = 0;
						base.StartCoroutine(this.UcitajOstrvo("_Mapa 4 Temple"));
						return;
					}
				}
				else if (this.RaycastFunction(Input.mousePosition) == "4")
				{
					if (StagesParser.unlockedWorlds[4])
					{
						StagesParser.currSetIndex = 4;
						StagesParser.currentWorld = 5;
						StagesParser.zadnjiOtkljucanNivo = 0;
						base.StartCoroutine(this.UcitajOstrvo("_Mapa 5 Volcano"));
						return;
					}
				}
				else if (this.RaycastFunction(Input.mousePosition) == "5")
				{
					if (StagesParser.unlockedWorlds[5])
					{
						StagesParser.currSetIndex = 5;
						StagesParser.currentWorld = 6;
						StagesParser.zadnjiOtkljucanNivo = 0;
						base.StartCoroutine(this.UcitajOstrvo("_Mapa 6 Ice"));
						return;
					}
				}
				else if (this.releasedItem.Equals("ButtonBackToMenu"))
				{
					if (this.KliknutoNa == this.Klik[9])
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
						return;
					}
				}
				else
				{
					if (this.releasedItem.Equals("FB Login"))
					{
						AllMapsManageFull.makniPopup = 1;
						base.StartCoroutine(this.checkConnectionForLoginButton());
						return;
					}
					if (this.releasedItem.Equals("Button_CheckOK"))
					{
						AllMapsManageFull.makniPopup = 0;
						base.StartCoroutine(CheckInternetConnection.Instance.ClosePopup());
					}
				}
			}
		}
	}

	// Token: 0x060028AB RID: 10411 RVA: 0x0001FBF1 File Offset: 0x0001DDF1
	private IEnumerator AnimacijaOblakaOstrvo(int index)
	{
		yield return new WaitForSeconds(1f);
		GameObject.Find("HolderClouds" + index + "/CloudsMove").GetComponent<Animation>().Play("CloudsOpenMap");
		yield break;
	}

	// Token: 0x060028AC RID: 10412 RVA: 0x0013EDF8 File Offset: 0x0013CFF8
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

	// Token: 0x060028AD RID: 10413 RVA: 0x0001FC00 File Offset: 0x0001DE00
	private IEnumerator UcitajOstrvo(string ime)
	{
		if (PlaySounds.soundOn)
		{
			PlaySounds.Play_Button_OpenWorld();
		}
		this._GUI.parent.Find("LOADING HOLDER NEW/Loading Animation Vrata").GetComponent<Animator>().Play("Loading Zidovi Dolazak");
		yield return new WaitForSeconds(1.1f);
		Application.LoadLevel(ime);
		yield break;
	}

	// Token: 0x060028AE RID: 10414 RVA: 0x0013EE50 File Offset: 0x0013D050
	private void changeLanguage()
	{
		if (!FB.IsLoggedIn)
		{
			GameObject.Find("Log In").GetComponent<TextMesh>().text = LanguageManager.LogIn;
			GameObject.Find("Log In").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		}
		GameObject.Find("Coming Soon").GetComponent<TextMesh>().text = LanguageManager.ComingSoon;
		GameObject.Find("Coming Soon").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
	}

	// Token: 0x0400223E RID: 8766
	private float TrenutniX;

	// Token: 0x0400223F RID: 8767
	private float TrenutniY;

	// Token: 0x04002240 RID: 8768
	private int[] Klik = new int[]
	{
		0,
		1,
		2,
		3,
		4,
		5,
		6,
		7,
		8,
		9
	};

	// Token: 0x04002241 RID: 8769
	private int KliknutoNa;

	// Token: 0x04002242 RID: 8770
	private float startX;

	// Token: 0x04002243 RID: 8771
	private float endX;

	// Token: 0x04002244 RID: 8772
	private float startY;

	// Token: 0x04002245 RID: 8773
	private float endY;

	// Token: 0x04002246 RID: 8774
	private float vremeKlika;

	// Token: 0x04002247 RID: 8775
	private string clickedItem;

	// Token: 0x04002248 RID: 8776
	private string releasedItem;

	// Token: 0x04002249 RID: 8777
	private bool moved;

	// Token: 0x0400224A RID: 8778
	private bool released;

	// Token: 0x0400224B RID: 8779
	private bool bounce;

	// Token: 0x0400224C RID: 8780
	private float pomerajX;

	// Token: 0x0400224D RID: 8781
	private float pomerajY;

	// Token: 0x0400224E RID: 8782
	private GameObject bonusIsland;

	// Token: 0x0400224F RID: 8783
	private float levaGranica = 87.9f;

	// Token: 0x04002250 RID: 8784
	private float desnaGranica = 95.44f;

	// Token: 0x04002251 RID: 8785
	private Transform lifeManager;

	// Token: 0x04002252 RID: 8786
	public Transform levo;

	// Token: 0x04002253 RID: 8787
	public Transform desno;

	// Token: 0x04002254 RID: 8788
	private Transform _GUI;

	// Token: 0x04002255 RID: 8789
	private Camera guiCamera;

	// Token: 0x04002256 RID: 8790
	private float razlikaX;

	// Token: 0x04002257 RID: 8791
	private float razlikaY;

	// Token: 0x04002258 RID: 8792
	private GameObject temp;

	// Token: 0x04002259 RID: 8793
	private Vector3 originalScale;

	// Token: 0x0400225A RID: 8794
	public static int makniPopup;
}
