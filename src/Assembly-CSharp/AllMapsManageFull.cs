using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200048D RID: 1165
public class AllMapsManageFull : MonoBehaviour
{
	// Token: 0x060024C8 RID: 9416 RVA: 0x000FE270 File Offset: 0x000FC470
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

	// Token: 0x060024C9 RID: 9417 RVA: 0x000FE3A4 File Offset: 0x000FC5A4
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

	// Token: 0x060024CA RID: 9418 RVA: 0x000FE633 File Offset: 0x000FC833
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

	// Token: 0x060024CB RID: 9419 RVA: 0x000FE644 File Offset: 0x000FC844
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

	// Token: 0x060024CC RID: 9420 RVA: 0x000FEAB0 File Offset: 0x000FCCB0
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

	// Token: 0x060024CD RID: 9421 RVA: 0x000FEBB4 File Offset: 0x000FCDB4
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

	// Token: 0x060024CE RID: 9422 RVA: 0x000FECD4 File Offset: 0x000FCED4
	private void IspisiBrojLevela(int index)
	{
		GameObject gameObject = GameObject.Find("AllWorlds_holder").transform.Find(index.ToString() + "/LevelsHolder").gameObject;
		gameObject.SetActive(true);
		gameObject.transform.Find("LevelText").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		gameObject.transform.Find("LevelValue").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
	}

	// Token: 0x060024CF RID: 9423 RVA: 0x000FED4C File Offset: 0x000FCF4C
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

	// Token: 0x060024D0 RID: 9424 RVA: 0x000FF0B4 File Offset: 0x000FD2B4
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

	// Token: 0x060024D1 RID: 9425 RVA: 0x000FFA59 File Offset: 0x000FDC59
	private IEnumerator AnimacijaOblakaOstrvo(int index)
	{
		yield return new WaitForSeconds(1f);
		GameObject.Find("HolderClouds" + index + "/CloudsMove").GetComponent<Animation>().Play("CloudsOpenMap");
		yield break;
	}

	// Token: 0x060024D2 RID: 9426 RVA: 0x000FFA68 File Offset: 0x000FDC68
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

	// Token: 0x060024D3 RID: 9427 RVA: 0x000FFABD File Offset: 0x000FDCBD
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

	// Token: 0x060024D4 RID: 9428 RVA: 0x000FFAD4 File Offset: 0x000FDCD4
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

	// Token: 0x04001D5B RID: 7515
	private float TrenutniX;

	// Token: 0x04001D5C RID: 7516
	private float TrenutniY;

	// Token: 0x04001D5D RID: 7517
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

	// Token: 0x04001D5E RID: 7518
	private int KliknutoNa;

	// Token: 0x04001D5F RID: 7519
	private float startX;

	// Token: 0x04001D60 RID: 7520
	private float endX;

	// Token: 0x04001D61 RID: 7521
	private float startY;

	// Token: 0x04001D62 RID: 7522
	private float endY;

	// Token: 0x04001D63 RID: 7523
	private float vremeKlika;

	// Token: 0x04001D64 RID: 7524
	private string clickedItem;

	// Token: 0x04001D65 RID: 7525
	private string releasedItem;

	// Token: 0x04001D66 RID: 7526
	private bool moved;

	// Token: 0x04001D67 RID: 7527
	private bool released;

	// Token: 0x04001D68 RID: 7528
	private bool bounce;

	// Token: 0x04001D69 RID: 7529
	private float pomerajX;

	// Token: 0x04001D6A RID: 7530
	private float pomerajY;

	// Token: 0x04001D6B RID: 7531
	private GameObject bonusIsland;

	// Token: 0x04001D6C RID: 7532
	private float levaGranica = 87.9f;

	// Token: 0x04001D6D RID: 7533
	private float desnaGranica = 95.44f;

	// Token: 0x04001D6E RID: 7534
	private Transform lifeManager;

	// Token: 0x04001D6F RID: 7535
	public Transform levo;

	// Token: 0x04001D70 RID: 7536
	public Transform desno;

	// Token: 0x04001D71 RID: 7537
	private Transform _GUI;

	// Token: 0x04001D72 RID: 7538
	private Camera guiCamera;

	// Token: 0x04001D73 RID: 7539
	private float razlikaX;

	// Token: 0x04001D74 RID: 7540
	private float razlikaY;

	// Token: 0x04001D75 RID: 7541
	private GameObject temp;

	// Token: 0x04001D76 RID: 7542
	private Vector3 originalScale;

	// Token: 0x04001D77 RID: 7543
	public static int makniPopup;
}
