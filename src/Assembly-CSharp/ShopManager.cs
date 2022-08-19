using System;
using System.Collections;
using UnityEngine;

// Token: 0x020004F1 RID: 1265
public class ShopManager : MonoBehaviour
{
	// Token: 0x060028EF RID: 10479 RVA: 0x00128CE2 File Offset: 0x00126EE2
	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x060028F0 RID: 10480 RVA: 0x0013655C File Offset: 0x0013475C
	private void Start()
	{
		ShopManager.shopHolder = GameObject.Find("_HolderShop").transform;
		ShopManager.shopLevaIvica = GameObject.Find("ShopRamLevoHolder").transform;
		ShopManager.shopDesnaIvica = GameObject.Find("ShopRamDesnoHolder").transform;
		ShopManager.shopHeaderOn = GameObject.Find("ShopHeaderOn");
		ShopManager.shopHeaderOff = GameObject.Find("ShopHeaderOff1");
		ShopManager.freeCoinsHeaderOn = GameObject.Find("ShopHeaderOn1");
		ShopManager.freeCoinsHeaderOff = GameObject.Find("ShopHeaderOff");
		ShopManager.holderShopCard = GameObject.Find("HolderShopCard");
		ShopManager.holderFreeCoinsCard = GameObject.Find("HolderFreeCoinsCard");
		ShopManager.buttonShopBack = GameObject.Find("HolderBack").transform;
		ShopManager.originalScale = ShopManager.shopHolder.localScale;
		ShopManager.offset = 3.5f;
		ShopManager.shopHolder.localScale = ShopManager.shopHolder.localScale * Camera.main.orthographicSize / 5f;
		ShopManager.shopHolder.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.one / 2f).x, ShopManager.shopHolder.position.y, Camera.main.transform.position.z + 5f);
		ShopManager.shopLevaIvica.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.zero).x, ShopManager.shopLevaIvica.position.y, ShopManager.shopLevaIvica.position.z);
		ShopManager.shopDesnaIvica.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.one).x, ShopManager.shopLevaIvica.position.y, ShopManager.shopLevaIvica.position.z);
		ShopManager.shopHolder.gameObject.SetActive(false);
		ShopManager.desnaGranica = ShopManager.shopLevaIvica.transform.position.x + 3.5f;
		base.transform.Find("HolderFrame/ShopRamDesnoHolder/ShopRamDesno/FinishCoins/TextFreeCoinsUp1").GetComponent<Renderer>().sortingLayerID = 1;
		base.transform.Find("HolderHeader/ShopHeaderOff/TextFreeCoinsDown").GetComponent<Renderer>().sortingLayerID = 1;
		base.transform.Find("HolderHeader/ShopHeaderOff/TextFreeCoinsUp").GetComponent<Renderer>().sortingLayerID = 1;
		base.transform.Find("HolderHeader/ShopHeaderOff1/TextShopDown").GetComponent<Renderer>().sortingLayerID = 1;
		base.transform.Find("HolderHeader/ShopHeaderOff1/TextShopUp").GetComponent<Renderer>().sortingLayerID = 1;
		base.transform.Find("HolderHeader/ShopHeaderOn/TextShopDown").GetComponent<Renderer>().sortingLayerID = 1;
		base.transform.Find("HolderHeader/ShopHeaderOn/TextShopUp").GetComponent<Renderer>().sortingLayerID = 1;
		base.transform.Find("HolderHeader/ShopHeaderOn1/TextFreeCoinsDown").GetComponent<Renderer>().sortingLayerID = 1;
		base.transform.Find("HolderHeader/ShopHeaderOn1/TextFreeCoinsUp").GetComponent<Renderer>().sortingLayerID = 1;
		base.transform.Find("HolderFrame/ShopBackground").GetComponent<Renderer>().sortingLayerID = 1;
		base.transform.Find("HolderFrame/ShopBackground_Dif").GetComponent<Renderer>().sortingLayerID = 1;
		base.transform.Find("HolderFreeCoinsCard/HolderFreeCoinsCardAnimation/Card3_FC_WatchVideo/HolderCard_NotAvailable/ShopTextOnCard").GetComponent<Renderer>().sortingLayerID = 1;
		foreach (object obj in base.transform.Find("HolderFreeCoinsCard").GetChild(0).transform)
		{
			Transform transform = (Transform)obj;
			transform.Find("HolderCard/ShopPriceButton/ShopTextCoins1").GetComponent<Renderer>().sortingLayerID = 1;
			transform.Find("HolderCard/ShopPriceButton/ShopTextCoins2").GetComponent<Renderer>().sortingLayerID = 1;
			transform.Find("HolderCard/ShopTextOnCard").GetComponent<Renderer>().sortingLayerID = 1;
			transform.Find("HolderCard/ShopCardShine").GetComponent<Renderer>().sortingLayerID = 1;
		}
		foreach (object obj2 in base.transform.Find("HolderShopCard").GetChild(0).transform)
		{
			Transform transform2 = (Transform)obj2;
			transform2.Find("HolderCard/ShopPriceButton/ShopTextCoins1").GetComponent<Renderer>().sortingLayerID = 1;
			transform2.Find("HolderCard/ShopPriceButton/ShopTextCoins2").GetComponent<Renderer>().sortingLayerID = 1;
			transform2.Find("HolderCard/ShopBuyCoins/ShopTextCoins1").GetComponent<Renderer>().sortingLayerID = 1;
			transform2.Find("HolderCard/ShopBuyCoins/ShopTextCoins2").GetComponent<Renderer>().sortingLayerID = 1;
			transform2.Find("HolderCard/ShopCardShine").GetComponent<Renderer>().sortingLayerID = 1;
		}
	}

	// Token: 0x060028F1 RID: 10481 RVA: 0x00136A18 File Offset: 0x00134C18
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (this.released)
			{
				this.released = false;
			}
			this.clickedItem = this.RaycastFunction(Input.mousePosition);
			this.vremeKlika = Time.time;
			this.clickedPos = Input.mousePosition.x;
			if (this.started)
			{
				this.started = false;
				this.tempObject = null;
			}
			if (this.clickedItem.StartsWith("Card"))
			{
				this.startX = Input.mousePosition.x;
				this.started = true;
				this.tempObject = GameObject.Find(this.clickedItem).transform;
				ShopManager.levaGranica = ShopManager.shopDesnaIvica.position.x - 1.5f - (float)(this.tempObject.parent.childCount - 1) * this.tempObject.GetComponent<BoxCollider>().bounds.extents.x * 2f - this.tempObject.GetComponent<BoxCollider>().bounds.extents.x;
			}
		}
		if (Input.GetMouseButton(0) && this.started && ((this.tempObject.parent.childCount > 2 && Camera.main.aspect < 1.7777778f) || (this.tempObject.parent.childCount > 3 && Camera.main.aspect >= 1.7777778f)))
		{
			this.endX = Input.mousePosition.x;
			this.pomerajX = (this.endX - this.startX) * Camera.main.orthographicSize / 250f;
			if (this.pomerajX != 0f)
			{
				this.moved = true;
			}
			this.tempObject.parent.position = new Vector3(Mathf.Clamp(this.tempObject.parent.position.x + this.pomerajX, ShopManager.levaGranica, ShopManager.desnaGranica), this.tempObject.parent.position.y, this.tempObject.parent.position.z);
			this.startX = this.endX;
			Debug.Log("Uledj");
		}
		if (this.released)
		{
			if (this.tempObject.parent.position.x <= ShopManager.levaGranica - 0.5f)
			{
				if (this.bounce)
				{
					this.pomerajX = 0.075f;
					this.bounce = false;
				}
			}
			else if (this.tempObject.parent.position.x >= ShopManager.desnaGranica && this.bounce)
			{
				this.pomerajX = -0.075f;
				this.bounce = false;
			}
			this.tempObject.parent.Translate(this.pomerajX, 0f, 0f);
			this.pomerajX *= 0.92f;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			this.releasedItem = this.RaycastFunction(Input.mousePosition);
			if (this.moved)
			{
				this.moved = false;
				this.released = true;
				this.bounce = true;
			}
			this.startX = (this.endX = 0f);
			if (this.clickedItem == this.releasedItem && this.releasedItem != string.Empty && Time.time - this.vremeKlika < 0.35f && Mathf.Abs(Input.mousePosition.x - this.clickedPos) < 50f)
			{
				if (this.releasedItem == "HolderBack")
				{
					if (PlaySounds.soundOn)
					{
						PlaySounds.Play_Button_OpenLevel();
					}
					if (Time.timeScale == 0f)
					{
						base.StartCoroutine(this.PausedAnim(ShopManager.buttonShopBack.GetChild(0), "BackButtonClick"));
						base.StartCoroutine(this.CloseShopPaused());
					}
					else
					{
						ShopManager.buttonShopBack.GetChild(0).GetComponent<Animation>().Play("BackButtonClick");
						base.StartCoroutine(this.CloseShop());
					}
				}
				else if (this.releasedItem == "ShopHeaderOff1")
				{
					ShopManager.holderShopCard.transform.position = new Vector3(ShopManager.desnaGranica, ShopManager.holderShopCard.transform.position.y, ShopManager.holderShopCard.transform.position.z);
					ShopManager.shopHeaderOff.SetActive(false);
					ShopManager.shopHeaderOn.SetActive(true);
					ShopManager.freeCoinsHeaderOn.SetActive(false);
					ShopManager.freeCoinsHeaderOff.SetActive(true);
					ShopManager.holderFreeCoinsCard.SetActive(false);
					ShopManager.holderShopCard.SetActive(true);
					if (Time.timeScale == 0f)
					{
						base.StartCoroutine(this.PausedAnim(ShopManager.holderShopCard.transform.GetChild(0), "DolazakShop_A"));
					}
					else
					{
						ShopManager.holderShopCard.transform.GetChild(0).GetComponent<Animation>().Play("DolazakShop_A");
					}
				}
				else if (this.releasedItem == "ShopHeaderOff")
				{
					ShopManager.holderFreeCoinsCard.transform.position = new Vector3(ShopManager.desnaGranica, ShopManager.holderFreeCoinsCard.transform.position.y, ShopManager.holderFreeCoinsCard.transform.position.z);
					ShopManager.shopHeaderOn.SetActive(false);
					ShopManager.shopHeaderOff.SetActive(true);
					ShopManager.freeCoinsHeaderOff.SetActive(false);
					ShopManager.freeCoinsHeaderOn.SetActive(true);
					ShopManager.holderShopCard.SetActive(false);
					ShopManager.holderFreeCoinsCard.SetActive(true);
					if (Time.timeScale == 0f)
					{
						base.StartCoroutine(this.PausedAnim(ShopManager.holderFreeCoinsCard.transform.GetChild(0), "DolazakShop_A"));
					}
					else
					{
						ShopManager.holderFreeCoinsCard.transform.GetChild(0).GetComponent<Animation>().Play("DolazakShop_A");
					}
				}
				else if (this.releasedItem.StartsWith("Card"))
				{
					if (PlaySounds.soundOn)
					{
						PlaySounds.Play_Button_OpenLevel();
					}
					if (Time.timeScale == 0f)
					{
						this.temp = GameObject.Find(this.releasedItem);
						base.StartCoroutine(this.PausedAnim(this.temp.transform.GetChild(0), "ShopCardClick"));
					}
					else
					{
						this.temp = GameObject.Find(this.releasedItem);
						this.temp.transform.GetChild(0).GetComponent<Animation>().Play("ShopCardClick");
					}
					if (this.releasedItem.Contains("LikeBananaIsland"))
					{
						FacebookManager.stranica = "BananaIsland";
						if (FB.IsLoggedIn)
						{
							GameObject.Find("FacebookManager").SendMessage("OpenPage");
						}
						else
						{
							GameObject.Find("FacebookManager").SendMessage("FacebookLogin");
						}
					}
					else if (this.releasedItem.Contains("LikeWebelinx"))
					{
						FacebookManager.stranica = "Webelinx";
						if (FB.IsLoggedIn)
						{
							GameObject.Find("FacebookManager").SendMessage("OpenPage");
						}
						else
						{
							GameObject.Find("FacebookManager").SendMessage("FacebookLogin");
						}
					}
					if (!this.releasedItem.Contains("WatchVideo") && this.releasedItem.Contains("Buy"))
					{
						string str = this.releasedItem.Substring(this.releasedItem.IndexOf('y') + 1);
						Debug.Log("Sta: " + str);
					}
				}
			}
		}
		if (Input.GetKeyUp(27) && ShopManager.shopHolder.gameObject.activeSelf)
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
			if (Time.timeScale == 0f)
			{
				base.StartCoroutine(this.PausedAnim(ShopManager.buttonShopBack.GetChild(0), "BackButtonClick"));
				base.StartCoroutine(this.CloseShopPaused());
				return;
			}
			ShopManager.buttonShopBack.GetChild(0).GetComponent<Animation>().Play("BackButtonClick");
			base.StartCoroutine(this.CloseShop());
		}
	}

	// Token: 0x060028F2 RID: 10482 RVA: 0x0013722E File Offset: 0x0013542E
	public static IEnumerator OpenShopCard()
	{
		ShopManager.shopDesnaIvica.transform.Find("ShopRamDesno/FinishCoins/TextFreeCoinsUp1").GetComponent<TextMesh>().text = PlayerPrefs.GetInt("TotalMoney").ToString();
		ShopManager.shopHolder.transform.position = Camera.main.transform.position + Vector3.forward * 5f;
		ShopManager.shopHeaderOff.SetActive(false);
		ShopManager.shopHeaderOn.SetActive(true);
		if (ShopManager.freeCoinsExists)
		{
			ShopManager.freeCoinsHeaderOn.SetActive(false);
			ShopManager.freeCoinsHeaderOff.SetActive(true);
		}
		ShopManager.holderFreeCoinsCard.SetActive(false);
		ShopManager.holderShopCard.SetActive(true);
		ShopManager.holderShopCard.transform.position = new Vector3(ShopManager.desnaGranica, ShopManager.holderShopCard.transform.position.y, ShopManager.holderShopCard.transform.position.z);
		yield return new WaitForSeconds(0.25f);
		ShopManager.shopHolder.gameObject.SetActive(true);
		ShopManager.otvorenShop = true;
		ShopManager.holderShopCard.transform.GetChild(0).GetComponent<Animation>().Play("DolazakShop_A");
		if (PlayerPrefs.HasKey("otisaoDaLajkuje"))
		{
			FacebookManager.lokacijaProvere = "Shop";
			FacebookManager.stranica = PlayerPrefs.GetString("stranica");
			FacebookManager.IDstranice = PlayerPrefs.GetString("IDstranice");
			GameObject.Find("FacebookManager").SendMessage("CheckLikes");
			Debug.Log("Nagradi ga iz Shop");
		}
		yield break;
	}

	// Token: 0x060028F3 RID: 10483 RVA: 0x00137236 File Offset: 0x00135436
	public static IEnumerator OpenFreeCoinsCard()
	{
		ShopManager.shopDesnaIvica.transform.Find("ShopRamDesno/FinishCoins/TextFreeCoinsUp1").GetComponent<TextMesh>().text = PlayerPrefs.GetInt("TotalMoney").ToString();
		ShopManager.shopHolder.transform.position = Camera.main.transform.position + Vector3.forward * 5f;
		if (ShopManager.shopExists)
		{
			ShopManager.shopHeaderOn.SetActive(false);
			ShopManager.shopHeaderOff.SetActive(true);
		}
		ShopManager.freeCoinsHeaderOff.SetActive(false);
		ShopManager.freeCoinsHeaderOn.SetActive(true);
		ShopManager.holderShopCard.SetActive(false);
		ShopManager.holderFreeCoinsCard.SetActive(true);
		ShopManager.holderFreeCoinsCard.transform.position = new Vector3(ShopManager.desnaGranica, ShopManager.holderFreeCoinsCard.transform.position.y, ShopManager.holderFreeCoinsCard.transform.position.z);
		yield return new WaitForSeconds(0.25f);
		ShopManager.shopHolder.gameObject.SetActive(true);
		ShopManager.otvorenShop = true;
		if (ShopManager.videoNotAvailable)
		{
			ShopManager.ResetVideoNotAvailable();
		}
		ShopManager.holderFreeCoinsCard.transform.GetChild(0).GetComponent<Animation>().Play("DolazakShop_A");
		if (PlayerPrefs.HasKey("otisaoDaLajkuje"))
		{
			FacebookManager.lokacijaProvere = "Shop";
			FacebookManager.stranica = PlayerPrefs.GetString("stranica");
			FacebookManager.IDstranice = PlayerPrefs.GetString("IDstranice");
			GameObject.Find("FacebookManager").SendMessage("CheckLikes");
			Debug.Log("Nagradi ga iz Shop");
		}
		yield break;
	}

	// Token: 0x060028F4 RID: 10484 RVA: 0x0013723E File Offset: 0x0013543E
	public void ShopCardPaused()
	{
		base.StartCoroutine(this.OpenShopCardPaused());
	}

	// Token: 0x060028F5 RID: 10485 RVA: 0x0013724D File Offset: 0x0013544D
	public IEnumerator OpenShopCardPaused()
	{
		yield return null;
		base.StartCoroutine(this.PausedAnim(ShopManager.holderShopCard.transform.GetChild(0), "DolazakShop_A"));
		yield break;
	}

	// Token: 0x060028F6 RID: 10486 RVA: 0x0013725C File Offset: 0x0013545C
	public void FreeCoinsCardPaused()
	{
		base.StartCoroutine(this.OpenFreeCoinsCardPaused());
	}

	// Token: 0x060028F7 RID: 10487 RVA: 0x0013726C File Offset: 0x0013546C
	public static void shopPreparation_Paused()
	{
		ShopManager.shopDesnaIvica.transform.Find("ShopRamDesno/FinishCoins/TextFreeCoinsUp1").GetComponent<TextMesh>().text = PlayerPrefs.GetInt("TotalMoney").ToString();
		ShopManager.shopHolder.transform.position = Camera.main.transform.position + Vector3.forward * 5f;
		ShopManager.shopHeaderOff.SetActive(false);
		ShopManager.shopHeaderOn.SetActive(true);
		if (ShopManager.freeCoinsExists)
		{
			ShopManager.freeCoinsHeaderOn.SetActive(false);
			ShopManager.freeCoinsHeaderOff.SetActive(true);
		}
		ShopManager.holderFreeCoinsCard.SetActive(false);
		ShopManager.holderShopCard.SetActive(true);
		ShopManager.holderShopCard.transform.position = new Vector3(ShopManager.desnaGranica, ShopManager.holderShopCard.transform.position.y, ShopManager.holderShopCard.transform.position.z);
		ShopManager.shopHolder.gameObject.SetActive(true);
		ShopManager.otvorenShop = true;
	}

	// Token: 0x060028F8 RID: 10488 RVA: 0x0013737B File Offset: 0x0013557B
	public IEnumerator OpenFreeCoinsCardPaused()
	{
		yield return null;
		base.StartCoroutine(this.PausedAnim(ShopManager.holderFreeCoinsCard.transform.GetChild(0), "DolazakShop_A"));
		yield break;
	}

	// Token: 0x060028F9 RID: 10489 RVA: 0x0013738C File Offset: 0x0013558C
	public static void freeCoinsPreparation_Paused()
	{
		ShopManager.shopDesnaIvica.transform.Find("ShopRamDesno/FinishCoins/TextFreeCoinsUp1").GetComponent<TextMesh>().text = PlayerPrefs.GetInt("TotalMoney").ToString();
		ShopManager.shopHolder.transform.position = Camera.main.transform.position + Vector3.forward * 5f;
		if (ShopManager.shopExists)
		{
			ShopManager.shopHeaderOn.SetActive(false);
			ShopManager.shopHeaderOff.SetActive(true);
		}
		ShopManager.freeCoinsHeaderOff.SetActive(false);
		ShopManager.freeCoinsHeaderOn.SetActive(true);
		ShopManager.holderShopCard.SetActive(false);
		ShopManager.holderFreeCoinsCard.SetActive(true);
		ShopManager.holderFreeCoinsCard.transform.position = new Vector3(ShopManager.desnaGranica, ShopManager.holderFreeCoinsCard.transform.position.y, ShopManager.holderFreeCoinsCard.transform.position.z);
		ShopManager.shopHolder.gameObject.SetActive(true);
		ShopManager.otvorenShop = true;
		if (ShopManager.videoNotAvailable)
		{
			ShopManager.ResetVideoNotAvailable();
		}
	}

	// Token: 0x060028FA RID: 10490 RVA: 0x001374A7 File Offset: 0x001356A7
	private IEnumerator CloseShop()
	{
		yield return new WaitForSeconds(0.85f);
		ShopManager.shopHolder.gameObject.SetActive(false);
		ShopManager.otvorenShop = false;
		ShopManager.shopHolder.position = new Vector3(-5f, -5f, ShopManager.shopHolder.position.z);
		ShopManager.buttonShopBack.GetChild(0).localPosition = Vector3.zero;
		yield break;
	}

	// Token: 0x060028FB RID: 10491 RVA: 0x001374AF File Offset: 0x001356AF
	private IEnumerator CloseShopPaused()
	{
		ShopManager.timeToShowNextElement = DateTime.Now.AddSeconds(0.8500000238418579);
		while (DateTime.Now < ShopManager.timeToShowNextElement)
		{
			yield return null;
		}
		ShopManager.shopHolder.gameObject.SetActive(false);
		ShopManager.otvorenShop = false;
		ShopManager.shopHolder.position = new Vector3(-5f, -5f, ShopManager.shopHolder.position.z);
		ShopManager.buttonShopBack.GetChild(0).localPosition = Vector3.zero;
		yield break;
	}

	// Token: 0x060028FC RID: 10492 RVA: 0x001374B8 File Offset: 0x001356B8
	public string RaycastFunction(Vector3 vector)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(vector), ref raycastHit))
		{
			return raycastHit.collider.name;
		}
		return "";
	}

	// Token: 0x060028FD RID: 10493 RVA: 0x001374EC File Offset: 0x001356EC
	public static void RescaleShop()
	{
		ShopManager.shopHolder.localScale = ShopManager.originalScale * Camera.main.orthographicSize / 5f;
		float num = ShopManager.offset * Camera.main.orthographicSize / 5f;
		ShopManager.shopHolder.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.one / 2f).x, ShopManager.shopHolder.position.y, Camera.main.transform.position.z + 5f);
		ShopManager.shopLevaIvica.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.zero).x, ShopManager.shopLevaIvica.position.y, ShopManager.shopLevaIvica.position.z);
		ShopManager.shopDesnaIvica.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.one).x, ShopManager.shopLevaIvica.position.y, ShopManager.shopLevaIvica.position.z);
		ShopManager.shopHolder.gameObject.SetActive(false);
		ShopManager.desnaGranica = ShopManager.shopLevaIvica.transform.position.x + num;
	}

	// Token: 0x060028FE RID: 10494 RVA: 0x0013763B File Offset: 0x0013583B
	private IEnumerator PausedAnim(Transform obj, string ime)
	{
		base.StartCoroutine(obj.GetComponent<Animation>().Play(ime, false, delegate(bool what)
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

	// Token: 0x060028FF RID: 10495 RVA: 0x00137658 File Offset: 0x00135858
	private void VideoNotAvailable()
	{
		GameObject.Find("Card3_FC_WatchVideo").transform.Find("HolderCard").gameObject.SetActive(false);
		GameObject.Find("Card3_FC_WatchVideo").transform.Find("HolderCard_NotAvailable").gameObject.SetActive(true);
		ShopManager.videoNotAvailable = true;
	}

	// Token: 0x06002900 RID: 10496 RVA: 0x001376B4 File Offset: 0x001358B4
	private static void ResetVideoNotAvailable()
	{
		GameObject.Find("Card3_FC_WatchVideo").transform.Find("HolderCard").gameObject.SetActive(true);
		GameObject.Find("Card3_FC_WatchVideo").transform.Find("HolderCard_NotAvailable").gameObject.SetActive(false);
		ShopManager.videoNotAvailable = false;
	}

	// Token: 0x040024E8 RID: 9448
	public static Transform shopHolder;

	// Token: 0x040024E9 RID: 9449
	public static Transform shopLevaIvica;

	// Token: 0x040024EA RID: 9450
	public static Transform shopDesnaIvica;

	// Token: 0x040024EB RID: 9451
	public static GameObject shopHeaderOn;

	// Token: 0x040024EC RID: 9452
	public static GameObject shopHeaderOff;

	// Token: 0x040024ED RID: 9453
	public static GameObject freeCoinsHeaderOn;

	// Token: 0x040024EE RID: 9454
	public static GameObject freeCoinsHeaderOff;

	// Token: 0x040024EF RID: 9455
	public static GameObject holderShopCard;

	// Token: 0x040024F0 RID: 9456
	public static GameObject holderFreeCoinsCard;

	// Token: 0x040024F1 RID: 9457
	public static Transform buttonShopBack;

	// Token: 0x040024F2 RID: 9458
	private string clickedItem;

	// Token: 0x040024F3 RID: 9459
	private string releasedItem;

	// Token: 0x040024F4 RID: 9460
	private float vremeKlika;

	// Token: 0x040024F5 RID: 9461
	private float startX;

	// Token: 0x040024F6 RID: 9462
	private float endX;

	// Token: 0x040024F7 RID: 9463
	private float pomerajX;

	// Token: 0x040024F8 RID: 9464
	private static float levaGranica;

	// Token: 0x040024F9 RID: 9465
	private static float desnaGranica;

	// Token: 0x040024FA RID: 9466
	private bool moved;

	// Token: 0x040024FB RID: 9467
	private bool released;

	// Token: 0x040024FC RID: 9468
	private bool bounce;

	// Token: 0x040024FD RID: 9469
	private bool started;

	// Token: 0x040024FE RID: 9470
	private Transform tempObject;

	// Token: 0x040024FF RID: 9471
	private GameObject temp;

	// Token: 0x04002500 RID: 9472
	private float clickedPos;

	// Token: 0x04002501 RID: 9473
	public static bool shopExists = true;

	// Token: 0x04002502 RID: 9474
	public static bool freeCoinsExists = true;

	// Token: 0x04002503 RID: 9475
	private static Vector3 originalScale;

	// Token: 0x04002504 RID: 9476
	private static float offset;

	// Token: 0x04002505 RID: 9477
	private static DateTime timeToShowNextElement;

	// Token: 0x04002506 RID: 9478
	private bool helpBool;

	// Token: 0x04002507 RID: 9479
	private static bool videoNotAvailable = false;

	// Token: 0x04002508 RID: 9480
	public static bool otvorenShop = false;
}
