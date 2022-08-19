using System;
using System.Collections;
using UnityEngine;

// Token: 0x020004C1 RID: 1217
public class ManageFull : MonoBehaviour
{
	// Token: 0x06002692 RID: 9874 RVA: 0x00110188 File Offset: 0x0010E388
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

	// Token: 0x06002693 RID: 9875 RVA: 0x00110B14 File Offset: 0x0010ED14
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

	// Token: 0x06002694 RID: 9876 RVA: 0x00111819 File Offset: 0x0010FA19
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

	// Token: 0x06002695 RID: 9877 RVA: 0x00111828 File Offset: 0x0010FA28
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

	// Token: 0x06002696 RID: 9878 RVA: 0x00111837 File Offset: 0x0010FA37
	private IEnumerator closeShop()
	{
		yield return new WaitForSeconds(0.85f);
		this.shopHolder.gameObject.SetActive(false);
		this.shopHolder.position = new Vector3(-5f, -5f, this.shopHolder.position.z);
		this.buttonShopBack.GetChild(0).localPosition = Vector3.zero;
		yield break;
	}

	// Token: 0x06002697 RID: 9879 RVA: 0x00111846 File Offset: 0x0010FA46
	private IEnumerator DoAfterAnimation(GameObject obj, string animationName)
	{
		while (obj.GetComponent<Animation>().IsPlaying(animationName))
		{
			yield return null;
		}
		this.command();
		yield break;
	}

	// Token: 0x06002698 RID: 9880 RVA: 0x00111863 File Offset: 0x0010FA63
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

	// Token: 0x06002699 RID: 9881 RVA: 0x00111872 File Offset: 0x0010FA72
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

	// Token: 0x0600269A RID: 9882 RVA: 0x00111881 File Offset: 0x0010FA81
	private void HidePauseScreen()
	{
		this.pauseScreenHolder.SetActive(false);
	}

	// Token: 0x0600269B RID: 9883 RVA: 0x0011188F File Offset: 0x0010FA8F
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

	// Token: 0x0600269C RID: 9884 RVA: 0x0011189E File Offset: 0x0010FA9E
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

	// Token: 0x0600269D RID: 9885 RVA: 0x001118AD File Offset: 0x0010FAAD
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

	// Token: 0x0600269E RID: 9886 RVA: 0x001118BC File Offset: 0x0010FABC
	private void showFailedScreen()
	{
		this.FailedScreenHolder.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, this.FailedScreenHolder.transform.position.z);
		this.FailedScreenHolder.SetActive(true);
		this.FailedScreenHolder.transform.GetChild(0).GetComponent<Animation>().Play("FailedShow");
	}

	// Token: 0x0600269F RID: 9887 RVA: 0x00111948 File Offset: 0x0010FB48
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

	// Token: 0x060026A0 RID: 9888 RVA: 0x00111A7E File Offset: 0x0010FC7E
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

	// Token: 0x060026A1 RID: 9889 RVA: 0x00111A8D File Offset: 0x0010FC8D
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

	// Token: 0x060026A2 RID: 9890 RVA: 0x00111A9C File Offset: 0x0010FC9C
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

	// Token: 0x060026A3 RID: 9891 RVA: 0x00111ADC File Offset: 0x0010FCDC
	private string RaycastFunction(Vector3 vector)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(vector), ref raycastHit))
		{
			return raycastHit.collider.name;
		}
		return "";
	}

	// Token: 0x060026A4 RID: 9892 RVA: 0x00111B10 File Offset: 0x0010FD10
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

	// Token: 0x060026A5 RID: 9893 RVA: 0x00111B80 File Offset: 0x0010FD80
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

	// Token: 0x060026A6 RID: 9894 RVA: 0x00111BF8 File Offset: 0x0010FDF8
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

	// Token: 0x060026A7 RID: 9895 RVA: 0x00111CD5 File Offset: 0x0010FED5
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

	// Token: 0x060026A8 RID: 9896 RVA: 0x00111CEB File Offset: 0x0010FEEB
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

	// Token: 0x060026A9 RID: 9897 RVA: 0x00111D01 File Offset: 0x0010FF01
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

	// Token: 0x060026AA RID: 9898 RVA: 0x00111D10 File Offset: 0x0010FF10
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

	// Token: 0x060026AB RID: 9899 RVA: 0x00111D20 File Offset: 0x0010FF20
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

	// Token: 0x04002019 RID: 8217
	[HideInInspector]
	public int coinsCollected;

	// Token: 0x0400201A RID: 8218
	[HideInInspector]
	public int starsGained;

	// Token: 0x0400201B RID: 8219
	[HideInInspector]
	public int keysCollected;

	// Token: 0x0400201C RID: 8220
	[HideInInspector]
	public int collectedPoints;

	// Token: 0x0400201D RID: 8221
	[HideInInspector]
	public int baboonSmashed;

	// Token: 0x0400201E RID: 8222
	private int brojDoubleCoins = 1;

	// Token: 0x0400201F RID: 8223
	private int brojMagneta = 1;

	// Token: 0x04002020 RID: 8224
	private int brojShieldova = 1;

	// Token: 0x04002021 RID: 8225
	private bool playerDead;

	// Token: 0x04002022 RID: 8226
	public GameObject goScreen;

	// Token: 0x04002023 RID: 8227
	public GameObject goScreen2;

	// Token: 0x04002024 RID: 8228
	private GameObject player;

	// Token: 0x04002025 RID: 8229
	private MonkeyController2D playerController;

	// Token: 0x04002026 RID: 8230
	private float camera_z;

	// Token: 0x04002027 RID: 8231
	private CameraFollow2D_new cameraFollow;

	// Token: 0x04002028 RID: 8232
	private Transform pauseButton;

	// Token: 0x04002029 RID: 8233
	private Transform coinsHolder;

	// Token: 0x0400202A RID: 8234
	private TextMesh coinsCollectedText;

	// Token: 0x0400202B RID: 8235
	private GameObject pauseScreenHolder;

	// Token: 0x0400202C RID: 8236
	private GameObject Win_CompletedScreenHolder;

	// Token: 0x0400202D RID: 8237
	private GameObject FailedScreenHolder;

	// Token: 0x0400202E RID: 8238
	private GameObject Win_ShineHolder;

	// Token: 0x0400202F RID: 8239
	private GameObject star1;

	// Token: 0x04002030 RID: 8240
	private GameObject star2;

	// Token: 0x04002031 RID: 8241
	private GameObject star3;

	// Token: 0x04002032 RID: 8242
	private Transform holderKeys;

	// Token: 0x04002033 RID: 8243
	private GameObject newHighScore;

	// Token: 0x04002034 RID: 8244
	private GameObject holderFinishPts;

	// Token: 0x04002035 RID: 8245
	private GameObject holderFinishKeys;

	// Token: 0x04002036 RID: 8246
	private GameObject buttonFacebookShare;

	// Token: 0x04002037 RID: 8247
	private GameObject buttonBuyKeys;

	// Token: 0x04002038 RID: 8248
	private GameObject buttonPlay_Finish;

	// Token: 0x04002039 RID: 8249
	private GameObject holderFinishInfo;

	// Token: 0x0400203A RID: 8250
	private GameObject holderTextCompleted;

	// Token: 0x0400203B RID: 8251
	private GameObject holderKeepPlaying;

	// Token: 0x0400203C RID: 8252
	private GameObject keyHole1;

	// Token: 0x0400203D RID: 8253
	private GameObject keyHole2;

	// Token: 0x0400203E RID: 8254
	private GameObject keyHole3;

	// Token: 0x0400203F RID: 8255
	[HideInInspector]
	public Transform progressBarScale;

	// Token: 0x04002040 RID: 8256
	private Transform wonStar1;

	// Token: 0x04002041 RID: 8257
	private Transform wonStar2;

	// Token: 0x04002042 RID: 8258
	private Transform wonStar3;

	// Token: 0x04002043 RID: 8259
	private TextMesh textKeyPrice1;

	// Token: 0x04002044 RID: 8260
	private TextMesh textKeyPrice2;

	// Token: 0x04002045 RID: 8261
	private Transform shopHolder;

	// Token: 0x04002046 RID: 8262
	private Transform shopLevaIvica;

	// Token: 0x04002047 RID: 8263
	private Transform shopDesnaIvica;

	// Token: 0x04002048 RID: 8264
	private GameObject shopHeaderOn;

	// Token: 0x04002049 RID: 8265
	private GameObject shopHeaderOff;

	// Token: 0x0400204A RID: 8266
	private GameObject freeCoinsHeaderOn;

	// Token: 0x0400204B RID: 8267
	private GameObject freeCoinsHeaderOff;

	// Token: 0x0400204C RID: 8268
	private GameObject holderShopCard;

	// Token: 0x0400204D RID: 8269
	private GameObject holderFreeCoinsCard;

	// Token: 0x0400204E RID: 8270
	private Transform buttonShopBack;

	// Token: 0x0400204F RID: 8271
	private Transform PickPowers;

	// Token: 0x04002050 RID: 8272
	private Transform powerCard_CoinX2;

	// Token: 0x04002051 RID: 8273
	private Transform powerCard_Magnet;

	// Token: 0x04002052 RID: 8274
	private Transform powerCard_Shield;

	// Token: 0x04002053 RID: 8275
	private bool kupljenShield;

	// Token: 0x04002054 RID: 8276
	private bool kupljenDoubleCoins;

	// Token: 0x04002055 RID: 8277
	private bool kupljenMagnet;

	// Token: 0x04002056 RID: 8278
	public AnimationClip showPauseAnimation;

	// Token: 0x04002057 RID: 8279
	public AnimationClip dropPauseAnimation;

	// Token: 0x04002058 RID: 8280
	private bool helpBool;

	// Token: 0x04002059 RID: 8281
	private bool playerStopiran;

	// Token: 0x0400205A RID: 8282
	private Action command;

	// Token: 0x0400205B RID: 8283
	private string releasedItem;

	// Token: 0x0400205C RID: 8284
	private SetRandomStarsManager starManager;

	// Token: 0x0400205D RID: 8285
	private bool PowerUp_magnet;

	// Token: 0x0400205E RID: 8286
	[HideInInspector]
	public bool PowerUp_doubleCoins;

	// Token: 0x0400205F RID: 8287
	[HideInInspector]
	public bool PowerUp_shield;

	// Token: 0x04002060 RID: 8288
	private GameObject coinMagnet;

	// Token: 0x04002061 RID: 8289
	private GameObject shield;

	// Token: 0x04002062 RID: 8290
	private TextMesh textPtsGameplay;

	// Token: 0x04002063 RID: 8291
	private TextMesh textPtsFinish;
}
