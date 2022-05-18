using System;
using System.Collections;
using UnityEngine;

// Token: 0x020006D6 RID: 1750
public class ManageFull : MonoBehaviour
{
	// Token: 0x06002BD1 RID: 11217 RVA: 0x001551DC File Offset: 0x001533DC
	private void Awake()
	{
		this.cameraFollow = Camera.main.transform.parent.GetComponent<CameraFollow2D_new>();
		this.goScreen.SetActive(true);
		this.goScreen2.SetActive(true);
		this.starManager = base.GetComponent<SetRandomStarsManager>();
		this.player = GameObject.FindGameObjectWithTag("Monkey");
		this.playerController = this.player.GetComponent<MonkeyController2D>();
		this.coinsCollectedText = GameObject.Find("TextCoins").GetComponent<TextMesh>();
		this.pauseButton = GameObject.Find("HolderPause").transform;
		this.coinsHolder = GameObject.Find("HolderCoins").transform;
		this.pauseScreenHolder = GameObject.Find("HolderPauseScreen");
		this.FailedScreenHolder = GameObject.Find("HolderFailed");
		this.Win_CompletedScreenHolder = GameObject.Find("HolderFinish");
		this.Win_ShineHolder = GameObject.Find("HolderShineFinish");
		this.star1 = GameObject.Find("FinishStar1");
		this.star2 = GameObject.Find("FinishStar2");
		this.star3 = GameObject.Find("FinishStar3");
		this.holderKeys = GameObject.Find("HolderKeys").transform;
		this.camera_z = Camera.main.transform.position.z;
		this.coinMagnet = this.player.transform.Find("CoinMagnet").gameObject;
		this.shield = GameObject.Find("Shield");
		this.shield.SetActive(false);
		this.newHighScore = GameObject.Find("NewHighScore");
		this.holderFinishPts = GameObject.Find("HolderFinishPts");
		this.holderFinishInfo = GameObject.Find("HolderFinishInfo");
		this.buttonFacebookShare = GameObject.Find("FinishFacebook");
		this.buttonBuyKeys = GameObject.Find("FinishKeyPrice");
		this.holderFinishKeys = GameObject.Find("HolderFinishKeys");
		this.buttonPlay_Finish = GameObject.Find("ButtonHolePlay");
		this.holderTextCompleted = GameObject.Find("HolderTextCompleted");
		this.keyHole1 = GameObject.Find("FinishKeyHole1_");
		this.keyHole2 = GameObject.Find("FinishKeyHole2_");
		this.keyHole3 = GameObject.Find("FinishKeyHole3_");
		this.holderKeepPlaying = GameObject.Find("HolderKeepPlaying");
		this.progressBarScale = GameObject.Find("ProgressBar_ScaleY").transform;
		this.wonStar1 = GameObject.Find("HolderWonStar1").transform;
		this.wonStar2 = GameObject.Find("HolderWonStar2").transform;
		this.wonStar3 = GameObject.Find("HolderWonStar3").transform;
		this.textPtsGameplay = GameObject.Find("TextPtsGameplay").GetComponent<TextMesh>();
		this.textPtsFinish = GameObject.Find("TextPts").GetComponent<TextMesh>();
		this.textKeyPrice1 = GameObject.Find("TextKeyPrice1").GetComponent<TextMesh>();
		this.textKeyPrice2 = GameObject.Find("TextKeyPrice2").GetComponent<TextMesh>();
		this.shopHolder = GameObject.Find("_HolderShop").transform;
		this.shopLevaIvica = GameObject.Find("ShopRamLevoHolder").transform;
		this.shopDesnaIvica = GameObject.Find("ShopRamDesnoHolder").transform;
		this.shopHeaderOn = GameObject.Find("ShopHeaderOn");
		this.shopHeaderOff = GameObject.Find("ShopHeaderOff1");
		this.freeCoinsHeaderOn = GameObject.Find("ShopHeaderOn1");
		this.freeCoinsHeaderOff = GameObject.Find("ShopHeaderOff");
		this.holderShopCard = GameObject.Find("HolderShopCard");
		this.holderFreeCoinsCard = GameObject.Find("HolderFreeCoinsCard");
		this.buttonShopBack = GameObject.Find("HolderBack").transform;
		this.PickPowers = GameObject.Find("HolderPowersMove").transform;
		this.powerCard_CoinX2 = GameObject.Find("PowersCardCoinx2").transform;
		this.powerCard_Magnet = GameObject.Find("PowersCardMagnet").transform;
		this.powerCard_Shield = GameObject.Find("PowersCardShield").transform;
		this.shopHeaderOff.SetActive(false);
		this.freeCoinsHeaderOn.SetActive(false);
		this.holderFreeCoinsCard.SetActive(false);
		this.holderKeepPlaying.SetActive(false);
		this.newHighScore.SetActive(false);
		this.holderTextCompleted.SetActive(false);
		this.pauseScreenHolder.SetActive(false);
		this.FailedScreenHolder.SetActive(false);
		this.Win_CompletedScreenHolder.SetActive(false);
		this.Win_ShineHolder.SetActive(false);
		if (Camera.main.aspect <= 1.5f)
		{
			Camera.main.orthographicSize = 18f;
			this.shopHolder.localScale = this.shopHolder.localScale * 18f / 5f;
		}
		if (Camera.main.aspect > 1.5f)
		{
			Camera.main.orthographicSize = 15f;
			this.shopHolder.localScale = this.shopHolder.localScale * 15f / 5f;
		}
		else
		{
			Camera.main.orthographicSize = 16.5f;
			this.shopHolder.localScale = this.shopHolder.localScale * 16.5f / 5f;
		}
		this.pauseButton.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.one).x, Camera.main.ViewportToWorldPoint(Vector3.one).y, this.pauseButton.position.z);
		this.coinsHolder.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.zero).x, Camera.main.ViewportToWorldPoint(Vector3.one).y, this.coinsHolder.position.z);
		this.holderKeys.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.one / 2f).x, Camera.main.ViewportToWorldPoint(Vector3.one).y, this.pauseButton.position.z);
		this.shopHolder.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.one / 2f).x, this.shopHolder.position.y, this.shopHolder.position.z);
		this.shopLevaIvica.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.zero).x, this.shopLevaIvica.position.y, this.shopLevaIvica.position.z);
		this.shopDesnaIvica.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.one).x, this.shopLevaIvica.position.y, this.shopLevaIvica.position.z);
		this.shopHolder.gameObject.SetActive(false);
		this.pauseButton.parent = Camera.main.transform;
		this.coinsHolder.parent = Camera.main.transform;
		this.holderKeys.parent = Camera.main.transform;
		this.PickPowers.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.one / 2f).x, Camera.main.ViewportToWorldPoint(Vector3.one).y * 0.65f, this.PickPowers.position.z);
		this.PickPowers.gameObject.SetActive(false);
		this.goScreen.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, this.goScreen.transform.position.z);
		this.goScreen2.transform.position = this.goScreen.transform.position + new Vector3(0.1f, 0f, 1f);
		this.goScreen.transform.parent = Camera.main.transform;
		this.goScreen2.transform.parent = Camera.main.transform;
		this.PickPowers.parent = Camera.main.transform;
		if (PlaySounds.musicOn)
		{
			PlaySounds.Play_BackgroundMusic_Gameplay();
			if (PlaySounds.Level_Failed_Popup.isPlaying)
			{
				PlaySounds.Stop_Level_Failed_Popup();
			}
		}
		this.powerCard_CoinX2.GetChild(3).GetChild(0).GetComponent<TextMesh>().text = this.brojDoubleCoins.ToString();
		this.powerCard_CoinX2.GetChild(3).GetChild(1).GetComponent<TextMesh>().text = this.brojDoubleCoins.ToString();
		this.powerCard_Magnet.GetChild(3).GetChild(0).GetComponent<TextMesh>().text = this.brojMagneta.ToString();
		this.powerCard_Magnet.GetChild(3).GetChild(1).GetComponent<TextMesh>().text = this.brojMagneta.ToString();
		this.powerCard_Shield.GetChild(3).GetChild(0).GetComponent<TextMesh>().text = this.brojShieldova.ToString();
		this.powerCard_Shield.GetChild(3).GetChild(1).GetComponent<TextMesh>().text = this.brojShieldova.ToString();
	}

	// Token: 0x06002BD2 RID: 11218 RVA: 0x00155B68 File Offset: 0x00153D68
	private void Update()
	{
		if (Input.GetKeyDown(32) && this.goScreen.activeSelf)
		{
			Time.timeScale = 1f;
			this.goScreen.transform.parent = base.transform;
			this.goScreen2.transform.parent = base.transform;
			this.goScreen.SetActive(false);
			this.goScreen2.SetActive(false);
			this.playerController.state = MonkeyController2D.State.running;
			PlaySounds.Play_Run();
		}
		if (Input.GetMouseButtonUp(0))
		{
			this.releasedItem = this.RaycastFunction(Input.mousePosition);
			if (this.releasedItem == "GO screen")
			{
				Time.timeScale = 1f;
				this.goScreen.transform.parent = base.transform;
				this.goScreen2.transform.parent = base.transform;
				this.goScreen.SetActive(false);
				this.goScreen2.SetActive(false);
				this.playerController.state = MonkeyController2D.State.running;
				GameObject.Find("PrinceGorilla").GetComponent<Animator>().SetBool("Run", true);
				PlaySounds.Play_Run();
				base.StartCoroutine(this.showPickPowers());
				return;
			}
			if (this.releasedItem == "ButtonPause")
			{
				PlaySounds.Play_Button_Pause();
				this.pauseScreenHolder.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, this.pauseScreenHolder.transform.position.z);
				this.pauseScreenHolder.SetActive(true);
				if (Time.timeScale == 1f)
				{
					Time.timeScale = 0f;
					base.StopAllCoroutines();
					base.StartCoroutine(this.showPauseScreen());
					return;
				}
				base.StartCoroutine(this.dropPauseScreen());
				return;
			}
			else
			{
				if (this.releasedItem == "PauseHoleMain")
				{
					PlaySounds.Play_Button_GoBack();
					base.StartCoroutine(this.backToMenu());
					return;
				}
				if (this.releasedItem == "PauseHolePlay")
				{
					PlaySounds.Play_Button_Pause();
					base.StartCoroutine(this.unPause());
					if (this.playerStopiran)
					{
						this.playerController.heCanJump = true;
						this.buttonShopBack.GetChild(0).GetComponent<Animation>().Play("BackButtonClick");
						base.StartCoroutine(this.closeShop());
						this.playerStopiran = false;
						GameObject.Find("ButtonPause").GetComponent<Collider>().enabled = true;
						GameObject.Find("OBLACI").GetComponent<RunWithSpeed>().continueMoving = true;
						this.playerController.GetComponent<Rigidbody2D>().isKinematic = false;
						this.playerController.animator.enabled = true;
						this.playerController.maxSpeedX = this.playerController.startSpeedX;
						this.cameraFollow.cameraFollowX = true;
						return;
					}
				}
				else
				{
					if (this.releasedItem == "PauseHoleRestart")
					{
						PlaySounds.Play_Button_RestartLevel();
						base.StartCoroutine(this.restartLevel());
						return;
					}
					if (this.releasedItem == "FailedMainHole")
					{
						PlaySounds.Play_Button_GoBack();
						GameObject gameObject = GameObject.Find("ButtonMain_Failed");
						gameObject.GetComponent<Animation>().Play("FinishButtonsClick");
						if (PlaySounds.BackgroundMusic_Gameplay.isPlaying)
						{
							PlaySounds.Stop_BackgroundMusic_Gameplay();
						}
						this.command = delegate()
						{
							Application.LoadLevel(4);
						};
						base.StartCoroutine(this.FailedScreenHolder.transform.GetChild(0).GetComponent<Animation>().Play("FailedGo", false, delegate(bool what)
						{
							this.helpBool = true;
						}));
						base.StartCoroutine(this.DoAfterAnimation(gameObject, "FinishButtonsClick"));
						return;
					}
					if (this.releasedItem == "FailedRestartHole")
					{
						PlaySounds.Play_Button_RestartLevel();
						GameObject gameObject2 = GameObject.Find("ButtonRestart_Failed");
						gameObject2.GetComponent<Animation>().Play("FinishButtonsClick");
						this.command = delegate()
						{
							Application.LoadLevel(Application.loadedLevel);
						};
						base.StartCoroutine(this.FailedScreenHolder.transform.GetChild(0).GetComponent<Animation>().Play("FailedGo", false, delegate(bool what)
						{
							this.helpBool = true;
						}));
						base.StartCoroutine(this.DoAfterAnimation(gameObject2, "FinishButtonsClick"));
						return;
					}
					if (this.releasedItem == "ButtonRestart1")
					{
						PlaySounds.Play_Button_RestartLevel();
						GameObject gameObject3 = GameObject.Find("ButtonRestart1");
						gameObject3.GetComponent<Animation>().Play("FinishButtonsClick");
						this.command = delegate()
						{
							Application.LoadLevel(Application.loadedLevel);
						};
						base.StartCoroutine(this.Win_CompletedScreenHolder.transform.GetChild(0).GetComponent<Animation>().Play("FinishTableGo1", false, delegate(bool what)
						{
							this.helpBool = true;
						}));
						base.StartCoroutine(this.DoAfterAnimation(gameObject3, "FinishButtonsClick"));
						return;
					}
					if (this.releasedItem == "ButtonMain1")
					{
						PlaySounds.Play_Button_GoBack();
						GameObject gameObject4 = GameObject.Find("ButtonMain1");
						gameObject4.GetComponent<Animation>().Play("FinishButtonsClick");
						if (PlaySounds.BackgroundMusic_Gameplay.isPlaying)
						{
							PlaySounds.Stop_BackgroundMusic_Gameplay();
						}
						this.command = delegate()
						{
							Application.LoadLevel(4);
						};
						base.StartCoroutine(this.Win_CompletedScreenHolder.transform.GetChild(0).GetComponent<Animation>().Play("FinishTableGo1", false, delegate(bool what)
						{
							this.helpBool = true;
						}));
						base.StartCoroutine(this.DoAfterAnimation(gameObject4, "FinishButtonsClick"));
						return;
					}
					if (this.releasedItem == "ButtonPlay1")
					{
						PlaySounds.Play_Button_NextLevel();
						GameObject gameObject5 = GameObject.Find("ButtonPlay1");
						gameObject5.GetComponent<Animation>().Play("FinishButtonsClick");
						if (PlaySounds.BackgroundMusic_Gameplay.isPlaying)
						{
							PlaySounds.Stop_BackgroundMusic_Gameplay();
						}
						StagesParser.currStageIndex++;
						this.command = delegate()
						{
							Application.LoadLevel("LoadingScene");
						};
						base.StartCoroutine(this.Win_CompletedScreenHolder.transform.GetChild(0).GetComponent<Animation>().Play("FinishTableGo1", false, delegate(bool what)
						{
							this.helpBool = true;
						}));
						base.StartCoroutine(this.DoAfterAnimation(gameObject5, "FinishButtonsClick"));
						return;
					}
					if (this.releasedItem == "PauseHoleFreeCoins")
					{
						this.playerStopiran = true;
						this.playerController.heCanJump = false;
						GameObject.Find("ButtonPause").GetComponent<Collider>().enabled = false;
						GameObject.Find("OBLACI").GetComponent<RunWithSpeed>().continueMoving = false;
						this.playerController.GetComponent<Rigidbody2D>().isKinematic = true;
						this.playerController.maxSpeedX = 0f;
						this.playerController.animator.enabled = false;
						this.cameraFollow.cameraFollowX = false;
						this.cameraFollow.cameraFollowY = false;
						this.cameraFollow.moveUp = false;
						this.cameraFollow.moveDown = false;
						Time.timeScale = 1f;
						base.StartCoroutine(this.OpenFreeCoinsCard());
						return;
					}
					if (this.releasedItem == "PauseHoleShop")
					{
						this.playerStopiran = true;
						this.playerController.heCanJump = false;
						GameObject.Find("ButtonPause").GetComponent<Collider>().enabled = false;
						GameObject.Find("OBLACI").GetComponent<RunWithSpeed>().continueMoving = false;
						this.playerController.GetComponent<Rigidbody2D>().isKinematic = true;
						this.playerController.maxSpeedX = 0f;
						this.playerController.animator.enabled = false;
						this.cameraFollow.cameraFollowX = false;
						this.cameraFollow.cameraFollowY = false;
						this.cameraFollow.moveUp = false;
						this.cameraFollow.moveDown = false;
						Time.timeScale = 1f;
						base.StartCoroutine(this.OpenFreeCoinsCard());
						return;
					}
					if (this.releasedItem == "FinishKeyPrice")
					{
						base.StartCoroutine(this.BuyKeys());
						return;
					}
					if (this.releasedItem == "ButtonFreeCoins1")
					{
						GameObject.Find(this.releasedItem).GetComponent<Animation>().Play("FinishButtonsClick");
						this.shopHolder.transform.position = Camera.main.transform.position + Vector3.forward * 5f;
						base.StartCoroutine(this.OpenFreeCoinsCard());
						return;
					}
					if (this.releasedItem == "ButtonShop1")
					{
						GameObject.Find(this.releasedItem).GetComponent<Animation>().Play("FinishButtonsClick");
						this.shopHolder.transform.position = Camera.main.transform.position + Vector3.forward * 5f;
						base.StartCoroutine(this.OpenShopCard());
						return;
					}
					if (this.releasedItem == "FailedFreeCoinsHole")
					{
						GameObject.Find(this.releasedItem).transform.GetChild(0).GetComponent<Animation>().Play("FinishButtonsClick");
						this.shopHolder.transform.position = Camera.main.transform.position + Vector3.forward * 5f;
						base.StartCoroutine(this.OpenFreeCoinsCard());
						return;
					}
					if (this.releasedItem == "FailedShopHole")
					{
						GameObject.Find(this.releasedItem).transform.GetChild(0).GetComponent<Animation>().Play("FinishButtonsClick");
						this.shopHolder.transform.position = Camera.main.transform.position + Vector3.forward * 5f;
						base.StartCoroutine(this.OpenShopCard());
						return;
					}
					if (this.releasedItem == "HolderBack")
					{
						Debug.Log("ime: " + GameObject.Find(this.releasedItem));
						this.buttonShopBack.GetChild(0).GetComponent<Animation>().Play("BackButtonClick");
						base.StartCoroutine(this.closeShop());
						return;
					}
					if (this.releasedItem == "ShopHeaderOff1")
					{
						this.shopHeaderOff.SetActive(false);
						this.shopHeaderOn.SetActive(true);
						this.freeCoinsHeaderOn.SetActive(false);
						this.freeCoinsHeaderOff.SetActive(true);
						this.holderFreeCoinsCard.SetActive(false);
						this.holderShopCard.SetActive(true);
						return;
					}
					if (this.releasedItem == "ShopHeaderOff")
					{
						this.shopHeaderOn.SetActive(false);
						this.shopHeaderOff.SetActive(true);
						this.freeCoinsHeaderOff.SetActive(false);
						this.freeCoinsHeaderOn.SetActive(true);
						this.holderShopCard.SetActive(false);
						this.holderFreeCoinsCard.SetActive(true);
						return;
					}
					if (this.releasedItem == "PowersCardCoinx2")
					{
						this.powerCard_CoinX2.GetComponent<Collider>().enabled = false;
						this.brojDoubleCoins--;
						this.powerCard_CoinX2.GetChild(3).GetChild(0).GetComponent<TextMesh>().text = this.brojDoubleCoins.ToString();
						this.powerCard_CoinX2.GetChild(3).GetChild(1).GetComponent<TextMesh>().text = this.brojDoubleCoins.ToString();
						this.kupljenDoubleCoins = true;
						this.powerCard_CoinX2.GetComponent<Animator>().Play("GameplayPowerClick2");
						this.ApplyPowerUp(2);
						return;
					}
					if (this.releasedItem == "PowersCardMagnet")
					{
						this.powerCard_Magnet.GetComponent<Collider>().enabled = false;
						this.brojMagneta--;
						this.powerCard_Magnet.GetChild(3).GetChild(0).GetComponent<TextMesh>().text = this.brojMagneta.ToString();
						this.powerCard_Magnet.GetChild(3).GetChild(1).GetComponent<TextMesh>().text = this.brojMagneta.ToString();
						this.kupljenMagnet = true;
						this.powerCard_Magnet.GetComponent<Animator>().Play("GameplayPowerClick2");
						this.ApplyPowerUp(1);
						return;
					}
					if (this.releasedItem == "PowersCardShield")
					{
						this.powerCard_Shield.GetComponent<Collider>().enabled = false;
						this.brojShieldova--;
						this.powerCard_Shield.GetChild(3).GetChild(0).GetComponent<TextMesh>().text = this.brojShieldova.ToString();
						this.powerCard_Shield.GetChild(3).GetChild(1).GetComponent<TextMesh>().text = this.brojShieldova.ToString();
						this.kupljenShield = true;
						this.powerCard_Shield.GetComponent<Animator>().Play("GameplayPowerClick2");
						this.ApplyPowerUp(3);
					}
				}
			}
		}
	}

	// Token: 0x06002BD3 RID: 11219 RVA: 0x00021912 File Offset: 0x0001FB12
	private IEnumerator OpenShopCard()
	{
		this.shopHeaderOff.SetActive(false);
		this.shopHeaderOn.SetActive(true);
		this.freeCoinsHeaderOn.SetActive(false);
		this.freeCoinsHeaderOff.SetActive(true);
		this.holderFreeCoinsCard.SetActive(false);
		this.holderShopCard.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		this.shopHolder.gameObject.SetActive(true);
		yield break;
	}

	// Token: 0x06002BD4 RID: 11220 RVA: 0x00021921 File Offset: 0x0001FB21
	private IEnumerator OpenFreeCoinsCard()
	{
		this.shopHeaderOn.SetActive(false);
		this.shopHeaderOff.SetActive(true);
		this.freeCoinsHeaderOff.SetActive(false);
		this.freeCoinsHeaderOn.SetActive(true);
		this.holderShopCard.SetActive(false);
		this.holderFreeCoinsCard.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		this.shopHolder.gameObject.SetActive(true);
		yield break;
	}

	// Token: 0x06002BD5 RID: 11221 RVA: 0x00021930 File Offset: 0x0001FB30
	private IEnumerator closeShop()
	{
		yield return new WaitForSeconds(0.85f);
		this.shopHolder.gameObject.SetActive(false);
		this.shopHolder.position = new Vector3(-5f, -5f, this.shopHolder.position.z);
		this.buttonShopBack.GetChild(0).localPosition = Vector3.zero;
		yield break;
	}

	// Token: 0x06002BD6 RID: 11222 RVA: 0x0002193F File Offset: 0x0001FB3F
	private IEnumerator DoAfterAnimation(GameObject obj, string animationName)
	{
		while (obj.GetComponent<Animation>().IsPlaying(animationName))
		{
			yield return null;
		}
		this.command();
		yield break;
	}

	// Token: 0x06002BD7 RID: 11223 RVA: 0x0002195C File Offset: 0x0001FB5C
	private IEnumerator showPauseScreen()
	{
		base.StartCoroutine(this.pauseButton.transform.GetChild(0).GetComponent<Animation>().Play("FinishButtonsClick", false, delegate(bool what)
		{
			this.helpBool = true;
		}));
		base.StartCoroutine(this.pauseScreenHolder.transform.GetChild(0).GetComponent<Animation>().Play("PauseShow", false, delegate(bool what)
		{
			this.helpBool = true;
		}));
		while (!this.helpBool)
		{
			yield return null;
		}
		this.helpBool = false;
		yield break;
	}

	// Token: 0x06002BD8 RID: 11224 RVA: 0x0002196B File Offset: 0x0001FB6B
	private IEnumerator dropPauseScreen()
	{
		base.StartCoroutine(this.pauseButton.transform.GetChild(0).GetComponent<Animation>().Play("FinishButtonsClick", false, delegate(bool what)
		{
			this.helpBool = true;
		}));
		base.StartCoroutine(this.pauseScreenHolder.transform.GetChild(0).GetComponent<Animation>().Play("PauseGo", false, delegate(bool what)
		{
			this.helpBool = true;
		}));
		while (!this.helpBool)
		{
			yield return null;
		}
		this.helpBool = false;
		Time.timeScale = 1f;
		base.Invoke("HidePauseScreen", 0.75f);
		yield break;
	}

	// Token: 0x06002BD9 RID: 11225 RVA: 0x0002197A File Offset: 0x0001FB7A
	private void HidePauseScreen()
	{
		this.pauseScreenHolder.SetActive(false);
	}

	// Token: 0x06002BDA RID: 11226 RVA: 0x00021988 File Offset: 0x0001FB88
	private IEnumerator backToMenu()
	{
		base.StartCoroutine(GameObject.Find("ButtonMain_Pause").GetComponent<Animation>().Play("FinishButtonsClick", false, delegate(bool what)
		{
			this.helpBool = true;
		}));
		base.StartCoroutine(this.pauseScreenHolder.transform.GetChild(0).GetComponent<Animation>().Play("PauseGo", false, delegate(bool what)
		{
			this.helpBool = true;
		}));
		while (!this.helpBool)
		{
			yield return null;
		}
		this.helpBool = false;
		Application.LoadLevel(4);
		Time.timeScale = 1f;
		yield break;
	}

	// Token: 0x06002BDB RID: 11227 RVA: 0x00021997 File Offset: 0x0001FB97
	private IEnumerator restartLevel()
	{
		base.StartCoroutine(GameObject.Find("ButtonRestart_Pause").GetComponent<Animation>().Play("FinishButtonsClick", false, delegate(bool what)
		{
			this.helpBool = true;
		}));
		base.StartCoroutine(this.pauseScreenHolder.transform.GetChild(0).GetComponent<Animation>().Play("PauseGo", false, delegate(bool what)
		{
			this.helpBool = true;
		}));
		while (!this.helpBool)
		{
			yield return null;
		}
		this.helpBool = false;
		Application.LoadLevel(Application.loadedLevel);
		Time.timeScale = 1f;
		yield break;
	}

	// Token: 0x06002BDC RID: 11228 RVA: 0x000219A6 File Offset: 0x0001FBA6
	private IEnumerator unPause()
	{
		base.StartCoroutine(GameObject.Find("ButtonPlay_Pause").GetComponent<Animation>().Play("FinishButtonsClick", false, delegate(bool what)
		{
			this.helpBool = true;
		}));
		base.StartCoroutine(this.dropPauseScreen());
		while (!this.helpBool)
		{
			yield return null;
		}
		this.helpBool = false;
		yield break;
	}

	// Token: 0x06002BDD RID: 11229 RVA: 0x00156870 File Offset: 0x00154A70
	private void showFailedScreen()
	{
		this.FailedScreenHolder.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, this.FailedScreenHolder.transform.position.z);
		this.FailedScreenHolder.SetActive(true);
		this.FailedScreenHolder.transform.GetChild(0).GetComponent<Animation>().Play("FailedShow");
	}

	// Token: 0x06002BDE RID: 11230 RVA: 0x001568FC File Offset: 0x00154AFC
	private void ShowWinScreen()
	{
		this.textKeyPrice1.text = (this.textKeyPrice2.text = ((3 - this.keysCollected) * 800).ToString());
		if (PlaySounds.BackgroundMusic_Gameplay.isPlaying)
		{
			PlaySounds.Stop_BackgroundMusic_Gameplay();
		}
		PlaySounds.Play_Level_Completed_Popup();
		this.Win_CompletedScreenHolder.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 3f, this.Win_CompletedScreenHolder.transform.position.z);
		this.Win_CompletedScreenHolder.SetActive(true);
		this.Win_ShineHolder.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, this.Win_ShineHolder.transform.position.z);
		this.Win_CompletedScreenHolder.transform.GetChild(0).GetComponent<Animation>().Play("FinishTableShow");
		base.StartCoroutine(this.CheckKeys());
	}

	// Token: 0x06002BDF RID: 11231 RVA: 0x000219B5 File Offset: 0x0001FBB5
	private IEnumerator CheckKeys()
	{
		if (this.keysCollected == 1)
		{
			yield return new WaitForSeconds(0.75f);
			Debug.Log("Kljuc");
			this.keyHole1.GetComponent<Animation>().Play("FinishKeyYes");
			yield return new WaitForSeconds(0.75f);
			this.keyHole2.GetComponent<Animation>().Play("FinishKeyNo");
			yield return new WaitForSeconds(0.25f);
			this.keyHole3.GetComponent<Animation>().Play("FinishKeyNo");
		}
		else if (this.keysCollected == 2)
		{
			yield return new WaitForSeconds(0.75f);
			this.keyHole1.GetComponent<Animation>().Play("FinishKeyYes");
			yield return new WaitForSeconds(0.25f);
			this.keyHole2.GetComponent<Animation>().Play("FinishKeyYes");
			yield return new WaitForSeconds(0.75f);
			this.keyHole3.GetComponent<Animation>().Play("FinishKeyNo");
		}
		else if (this.keysCollected == 3)
		{
			this.textPtsFinish.text = this.textPtsGameplay.text;
			this.buttonFacebookShare.transform.parent.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
			this.holderFinishInfo.SetActive(false);
			this.buttonBuyKeys.SetActive(false);
			this.buttonBuyKeys.transform.parent.gameObject.SetActive(false);
			yield return new WaitForSeconds(0.75f);
			this.keyHole1.GetComponent<Animation>().Play("FinishKeyYes");
			yield return new WaitForSeconds(0.25f);
			this.keyHole2.GetComponent<Animation>().Play("FinishKeyYes");
			yield return new WaitForSeconds(0.25f);
			this.keyHole3.GetComponent<Animation>().Play("FinishKeyYes");
			yield return new WaitForSeconds(0.5f);
			this.Win_CompletedScreenHolder.transform.GetChild(0).GetComponent<Animation>().Play("FinishPriceClick");
			this.holderTextCompleted.SetActive(true);
			yield return new WaitForSeconds(0.35f);
			this.buttonFacebookShare.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
			yield return new WaitForSeconds(0.45f);
			this.buttonFacebookShare.transform.parent.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
			base.StartCoroutine(this.waitForStars());
		}
		else
		{
			yield return new WaitForSeconds(0.75f);
			this.keyHole1.GetComponent<Animation>().Play("FinishKeyNo");
			yield return new WaitForSeconds(0.25f);
			this.keyHole2.GetComponent<Animation>().Play("FinishKeyNo");
			yield return new WaitForSeconds(0.25f);
			this.keyHole3.GetComponent<Animation>().Play("FinishKeyNo");
		}
		yield break;
	}

	// Token: 0x06002BE0 RID: 11232 RVA: 0x000219C4 File Offset: 0x0001FBC4
	private IEnumerator waitForStars()
	{
		this.Win_ShineHolder.SetActive(true);
		yield return new WaitForSeconds(0.75f);
		this.star1.GetComponent<Animation>().Play("FinishStars1");
		this.star1.transform.GetChild(0).gameObject.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		GameObject.Find("FinishButtonTable").GetComponent<Animation>().Play("FinishShakingTableStars");
		this.starsGained = 1;
		PlaySounds.Play_GetStar();
		yield return new WaitForSeconds(0.25f);
		if (this.coinsCollected >= 70)
		{
			this.starsGained = 2;
			this.star2.GetComponent<Animation>().Play("FinishStars2");
			this.star2.transform.GetChild(0).gameObject.SetActive(true);
			yield return new WaitForSeconds(0.5f);
			GameObject.Find("FinishButtonTable").GetComponent<Animation>().Play("FinishShakingTableStars");
			PlaySounds.Play_GetStar2();
			yield return new WaitForSeconds(0.25f);
		}
		if (this.coinsCollected >= 90)
		{
			this.starsGained = 3;
			this.star3.GetComponent<Animation>().Play("FinishStars3");
			this.star3.transform.GetChild(0).gameObject.SetActive(true);
			yield return new WaitForSeconds(0.5f);
			GameObject.Find("FinishButtonTable").GetComponent<Animation>().Play("FinishShakingTableStars");
			PlaySounds.Play_GetStar3();
		}
		this.starManager.GoBack();
		yield break;
	}

	// Token: 0x06002BE1 RID: 11233 RVA: 0x000219D3 File Offset: 0x0001FBD3
	private void CoinAdded()
	{
		if (this.PowerUp_doubleCoins)
		{
			this.coinsCollected += 2;
		}
		else
		{
			this.coinsCollected++;
		}
		this.coinsCollectedText.text = this.coinsCollected.ToString();
	}

	// Token: 0x06002BE2 RID: 11234 RVA: 0x00149A14 File Offset: 0x00147C14
	private string RaycastFunction(Vector3 vector)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(vector), ref raycastHit))
		{
			return raycastHit.collider.name;
		}
		return "";
	}

	// Token: 0x06002BE3 RID: 11235 RVA: 0x00156A34 File Offset: 0x00154C34
	private void ApplyPowerUp(int x)
	{
		if (x == 1)
		{
			this.PowerUp_magnet = true;
			this.coinMagnet.SetActive(true);
			return;
		}
		if (x == 2)
		{
			this.PowerUp_doubleCoins = true;
			return;
		}
		if (x == 3)
		{
			this.PowerUp_shield = true;
			this.shield.SetActive(true);
			this.playerController.activeShield = true;
			return;
		}
		if (x == -3)
		{
			this.PowerUp_shield = false;
			this.shield.SetActive(false);
		}
	}

	// Token: 0x06002BE4 RID: 11236 RVA: 0x00156AA4 File Offset: 0x00154CA4
	private void KeyCollected()
	{
		this.keysCollected++;
		if (this.keysCollected == 1)
		{
			GameObject.Find("GamePlayKeyHole1").GetComponent<Animation>().Play();
			return;
		}
		if (this.keysCollected == 2)
		{
			GameObject.Find("GamePlayKeyHole2").GetComponent<Animation>().Play();
			return;
		}
		if (this.keysCollected == 3)
		{
			GameObject.Find("GamePlayKeyHole3").GetComponent<Animation>().Play();
		}
	}

	// Token: 0x06002BE5 RID: 11237 RVA: 0x00156B1C File Offset: 0x00154D1C
	public void AddPoints(int value)
	{
		this.collectedPoints += value;
		this.textPtsGameplay.text = this.collectedPoints.ToString();
		if (this.coinsCollected <= 100 && this.baboonSmashed <= 20 && this.progressBarScale.localScale.y <= 1f)
		{
			base.StartCoroutine(this.graduallyFillScale((float)value / 1000f));
			base.StartCoroutine(this.graduallyFillTile((float)value / 1000f));
		}
		if (this.progressBarScale.localScale.y >= 1f)
		{
			this.progressBarScale.localScale = Vector3.one;
			this.progressBarScale.GetChild(0).GetChild(0).GetComponent<Renderer>().material.mainTextureScale = new Vector2(1f, 1f);
		}
	}

	// Token: 0x06002BE6 RID: 11238 RVA: 0x00021A11 File Offset: 0x0001FC11
	private IEnumerator graduallyFillScale(float value)
	{
		Debug.Log("ulaazkak scale");
		float result = this.progressBarScale.localScale.y + value;
		float t = 0f;
		while (t < result)
		{
			yield return null;
			if (this.progressBarScale.localScale.y <= 1f)
			{
				this.progressBarScale.localScale = Vector3.Lerp(this.progressBarScale.localScale, new Vector3(this.progressBarScale.localScale.x, result, this.progressBarScale.localScale.z), 0.2f);
			}
			t += Time.deltaTime * 2f;
			if (this.progressBarScale.localScale.y >= 0.8f)
			{
				if (this.wonStar3.GetChild(0).localScale.x == 0f)
				{
					this.wonStar3.GetComponent<Animation>().Play();
				}
			}
			else if (this.progressBarScale.localScale.y < 0.8f && this.progressBarScale.localScale.y >= 0.5f)
			{
				if (this.wonStar2.GetChild(0).localScale.x == 0f)
				{
					this.wonStar2.GetComponent<Animation>().Play();
				}
			}
			else if (this.progressBarScale.localScale.y < 0.5f && this.progressBarScale.localScale.y >= 0.2f && this.wonStar1.GetChild(0).localScale.x == 0f)
			{
				this.wonStar1.GetComponent<Animation>().Play();
			}
			if (this.progressBarScale.localScale.y >= 1f)
			{
				this.progressBarScale.localScale = Vector3.one;
				this.progressBarScale.GetChild(0).GetChild(0).GetComponent<Renderer>().material.mainTextureScale = new Vector2(1f, 1f);
			}
		}
		yield break;
	}

	// Token: 0x06002BE7 RID: 11239 RVA: 0x00021A27 File Offset: 0x0001FC27
	private IEnumerator graduallyFillTile(float value)
	{
		Debug.Log("ulaazkak tile");
		float result = this.progressBarScale.GetChild(0).GetChild(0).GetComponent<Renderer>().material.mainTextureScale.y + value;
		for (float t = 0f; t < result; t += Time.deltaTime * 2f)
		{
			yield return null;
			if (this.progressBarScale.localScale.y < 1f)
			{
				this.progressBarScale.GetChild(0).GetChild(0).GetComponent<Renderer>().material.mainTextureScale = Vector2.Lerp(this.progressBarScale.GetChild(0).GetChild(0).GetComponent<Renderer>().material.mainTextureScale, new Vector2(1f, result), 0.2f);
			}
		}
		yield break;
	}

	// Token: 0x06002BE8 RID: 11240 RVA: 0x00021A3D File Offset: 0x0001FC3D
	private IEnumerator BuyKeys()
	{
		this.buttonBuyKeys.GetComponent<Collider>().enabled = false;
		if (this.keysCollected == 0)
		{
			this.keyHole1.transform.GetChild(1).localScale = Vector3.zero;
			this.keyHole2.transform.GetChild(1).localScale = Vector3.zero;
			this.keyHole3.transform.GetChild(1).localScale = Vector3.zero;
			this.keyHole1.GetComponent<Animation>().Play("FinishKeyYes");
			yield return new WaitForSeconds(0.25f);
			this.keyHole2.GetComponent<Animation>().Play("FinishKeyYes");
			yield return new WaitForSeconds(0.25f);
			this.keyHole3.GetComponent<Animation>().Play("FinishKeyYes");
		}
		if (this.keysCollected == 1)
		{
			this.keyHole2.transform.GetChild(1).localScale = Vector3.zero;
			this.keyHole3.transform.GetChild(1).localScale = Vector3.zero;
			this.keyHole2.GetComponent<Animation>().Play("FinishKeyYes");
			yield return new WaitForSeconds(0.25f);
			this.keyHole3.GetComponent<Animation>().Play("FinishKeyYes");
		}
		if (this.keysCollected == 2)
		{
			this.keyHole3.transform.GetChild(1).localScale = Vector3.zero;
			this.keyHole3.GetComponent<Animation>().Play("FinishKeyYes");
		}
		yield return new WaitForSeconds(0.45f);
		this.textPtsFinish.text = this.textPtsGameplay.text;
		this.buttonFacebookShare.transform.parent.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
		this.holderFinishInfo.SetActive(false);
		this.Win_CompletedScreenHolder.transform.GetChild(0).GetComponent<Animation>().Play("FinishPriceClick");
		this.holderTextCompleted.SetActive(true);
		yield return new WaitForSeconds(0.35f);
		this.buttonBuyKeys.transform.parent.gameObject.SetActive(false);
		this.buttonFacebookShare.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
		yield return new WaitForSeconds(0.45f);
		this.buttonFacebookShare.transform.parent.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
		base.StartCoroutine(this.waitForStars());
		yield break;
	}

	// Token: 0x06002BE9 RID: 11241 RVA: 0x00021A4C File Offset: 0x0001FC4C
	private IEnumerator showPickPowers()
	{
		this.PickPowers.gameObject.SetActive(true);
		base.Invoke("DisappearPickPowers", 3.5f);
		yield return new WaitForSeconds(0.85f);
		this.powerCard_CoinX2.GetComponent<Animator>().enabled = true;
		this.powerCard_Magnet.GetComponent<Animator>().enabled = true;
		this.powerCard_Shield.GetComponent<Animator>().enabled = true;
		yield break;
	}

	// Token: 0x06002BEA RID: 11242 RVA: 0x00156BFC File Offset: 0x00154DFC
	private void DisappearPickPowers()
	{
		if (!this.kupljenMagnet)
		{
			this.powerCard_Magnet.GetComponent<Animator>().Play("PowerCardCoinx2_Disappear");
			this.powerCard_Magnet.GetComponent<Collider>().enabled = false;
		}
		if (!this.kupljenShield)
		{
			this.powerCard_Shield.GetComponent<Animator>().Play("PowerCardCoinx2_Disappear");
			this.powerCard_Shield.GetComponent<Collider>().enabled = false;
		}
		if (!this.kupljenDoubleCoins)
		{
			this.powerCard_CoinX2.GetComponent<Animator>().Play("PowerCardCoinx2_Disappear");
			this.powerCard_CoinX2.GetComponent<Collider>().enabled = false;
		}
	}

	// Token: 0x04002611 RID: 9745
	[HideInInspector]
	public int coinsCollected;

	// Token: 0x04002612 RID: 9746
	[HideInInspector]
	public int starsGained;

	// Token: 0x04002613 RID: 9747
	[HideInInspector]
	public int keysCollected;

	// Token: 0x04002614 RID: 9748
	[HideInInspector]
	public int collectedPoints;

	// Token: 0x04002615 RID: 9749
	[HideInInspector]
	public int baboonSmashed;

	// Token: 0x04002616 RID: 9750
	private int brojDoubleCoins = 1;

	// Token: 0x04002617 RID: 9751
	private int brojMagneta = 1;

	// Token: 0x04002618 RID: 9752
	private int brojShieldova = 1;

	// Token: 0x04002619 RID: 9753
	private bool playerDead;

	// Token: 0x0400261A RID: 9754
	public GameObject goScreen;

	// Token: 0x0400261B RID: 9755
	public GameObject goScreen2;

	// Token: 0x0400261C RID: 9756
	private GameObject player;

	// Token: 0x0400261D RID: 9757
	private MonkeyController2D playerController;

	// Token: 0x0400261E RID: 9758
	private float camera_z;

	// Token: 0x0400261F RID: 9759
	private CameraFollow2D_new cameraFollow;

	// Token: 0x04002620 RID: 9760
	private Transform pauseButton;

	// Token: 0x04002621 RID: 9761
	private Transform coinsHolder;

	// Token: 0x04002622 RID: 9762
	private TextMesh coinsCollectedText;

	// Token: 0x04002623 RID: 9763
	private GameObject pauseScreenHolder;

	// Token: 0x04002624 RID: 9764
	private GameObject Win_CompletedScreenHolder;

	// Token: 0x04002625 RID: 9765
	private GameObject FailedScreenHolder;

	// Token: 0x04002626 RID: 9766
	private GameObject Win_ShineHolder;

	// Token: 0x04002627 RID: 9767
	private GameObject star1;

	// Token: 0x04002628 RID: 9768
	private GameObject star2;

	// Token: 0x04002629 RID: 9769
	private GameObject star3;

	// Token: 0x0400262A RID: 9770
	private Transform holderKeys;

	// Token: 0x0400262B RID: 9771
	private GameObject newHighScore;

	// Token: 0x0400262C RID: 9772
	private GameObject holderFinishPts;

	// Token: 0x0400262D RID: 9773
	private GameObject holderFinishKeys;

	// Token: 0x0400262E RID: 9774
	private GameObject buttonFacebookShare;

	// Token: 0x0400262F RID: 9775
	private GameObject buttonBuyKeys;

	// Token: 0x04002630 RID: 9776
	private GameObject buttonPlay_Finish;

	// Token: 0x04002631 RID: 9777
	private GameObject holderFinishInfo;

	// Token: 0x04002632 RID: 9778
	private GameObject holderTextCompleted;

	// Token: 0x04002633 RID: 9779
	private GameObject holderKeepPlaying;

	// Token: 0x04002634 RID: 9780
	private GameObject keyHole1;

	// Token: 0x04002635 RID: 9781
	private GameObject keyHole2;

	// Token: 0x04002636 RID: 9782
	private GameObject keyHole3;

	// Token: 0x04002637 RID: 9783
	[HideInInspector]
	public Transform progressBarScale;

	// Token: 0x04002638 RID: 9784
	private Transform wonStar1;

	// Token: 0x04002639 RID: 9785
	private Transform wonStar2;

	// Token: 0x0400263A RID: 9786
	private Transform wonStar3;

	// Token: 0x0400263B RID: 9787
	private TextMesh textKeyPrice1;

	// Token: 0x0400263C RID: 9788
	private TextMesh textKeyPrice2;

	// Token: 0x0400263D RID: 9789
	private Transform shopHolder;

	// Token: 0x0400263E RID: 9790
	private Transform shopLevaIvica;

	// Token: 0x0400263F RID: 9791
	private Transform shopDesnaIvica;

	// Token: 0x04002640 RID: 9792
	private GameObject shopHeaderOn;

	// Token: 0x04002641 RID: 9793
	private GameObject shopHeaderOff;

	// Token: 0x04002642 RID: 9794
	private GameObject freeCoinsHeaderOn;

	// Token: 0x04002643 RID: 9795
	private GameObject freeCoinsHeaderOff;

	// Token: 0x04002644 RID: 9796
	private GameObject holderShopCard;

	// Token: 0x04002645 RID: 9797
	private GameObject holderFreeCoinsCard;

	// Token: 0x04002646 RID: 9798
	private Transform buttonShopBack;

	// Token: 0x04002647 RID: 9799
	private Transform PickPowers;

	// Token: 0x04002648 RID: 9800
	private Transform powerCard_CoinX2;

	// Token: 0x04002649 RID: 9801
	private Transform powerCard_Magnet;

	// Token: 0x0400264A RID: 9802
	private Transform powerCard_Shield;

	// Token: 0x0400264B RID: 9803
	private bool kupljenShield;

	// Token: 0x0400264C RID: 9804
	private bool kupljenDoubleCoins;

	// Token: 0x0400264D RID: 9805
	private bool kupljenMagnet;

	// Token: 0x0400264E RID: 9806
	public AnimationClip showPauseAnimation;

	// Token: 0x0400264F RID: 9807
	public AnimationClip dropPauseAnimation;

	// Token: 0x04002650 RID: 9808
	private bool helpBool;

	// Token: 0x04002651 RID: 9809
	private bool playerStopiran;

	// Token: 0x04002652 RID: 9810
	private Action command;

	// Token: 0x04002653 RID: 9811
	private string releasedItem;

	// Token: 0x04002654 RID: 9812
	private SetRandomStarsManager starManager;

	// Token: 0x04002655 RID: 9813
	private bool PowerUp_magnet;

	// Token: 0x04002656 RID: 9814
	[HideInInspector]
	public bool PowerUp_doubleCoins;

	// Token: 0x04002657 RID: 9815
	[HideInInspector]
	public bool PowerUp_shield;

	// Token: 0x04002658 RID: 9816
	private GameObject coinMagnet;

	// Token: 0x04002659 RID: 9817
	private GameObject shield;

	// Token: 0x0400265A RID: 9818
	private TextMesh textPtsGameplay;

	// Token: 0x0400265B RID: 9819
	private TextMesh textPtsFinish;
}
