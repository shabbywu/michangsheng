using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000771 RID: 1905
public class ShopManager : MonoBehaviour
{
	// Token: 0x06003094 RID: 12436 RVA: 0x00022ADD File Offset: 0x00020CDD
	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x06003095 RID: 12437 RVA: 0x00182948 File Offset: 0x00180B48
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

	// Token: 0x06003096 RID: 12438 RVA: 0x00182E04 File Offset: 0x00181004
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

	// Token: 0x06003097 RID: 12439 RVA: 0x00023D12 File Offset: 0x00021F12
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

	// Token: 0x06003098 RID: 12440 RVA: 0x00023D1A File Offset: 0x00021F1A
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

	// Token: 0x06003099 RID: 12441 RVA: 0x00023D22 File Offset: 0x00021F22
	public void ShopCardPaused()
	{
		base.StartCoroutine(this.OpenShopCardPaused());
	}

	// Token: 0x0600309A RID: 12442 RVA: 0x00023D31 File Offset: 0x00021F31
	public IEnumerator OpenShopCardPaused()
	{
		yield return null;
		base.StartCoroutine(this.PausedAnim(ShopManager.holderShopCard.transform.GetChild(0), "DolazakShop_A"));
		yield break;
	}

	// Token: 0x0600309B RID: 12443 RVA: 0x00023D40 File Offset: 0x00021F40
	public void FreeCoinsCardPaused()
	{
		base.StartCoroutine(this.OpenFreeCoinsCardPaused());
	}

	// Token: 0x0600309C RID: 12444 RVA: 0x0018361C File Offset: 0x0018181C
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

	// Token: 0x0600309D RID: 12445 RVA: 0x00023D4F File Offset: 0x00021F4F
	public IEnumerator OpenFreeCoinsCardPaused()
	{
		yield return null;
		base.StartCoroutine(this.PausedAnim(ShopManager.holderFreeCoinsCard.transform.GetChild(0), "DolazakShop_A"));
		yield break;
	}

	// Token: 0x0600309E RID: 12446 RVA: 0x0018372C File Offset: 0x0018192C
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

	// Token: 0x0600309F RID: 12447 RVA: 0x00023D5E File Offset: 0x00021F5E
	private IEnumerator CloseShop()
	{
		yield return new WaitForSeconds(0.85f);
		ShopManager.shopHolder.gameObject.SetActive(false);
		ShopManager.otvorenShop = false;
		ShopManager.shopHolder.position = new Vector3(-5f, -5f, ShopManager.shopHolder.position.z);
		ShopManager.buttonShopBack.GetChild(0).localPosition = Vector3.zero;
		yield break;
	}

	// Token: 0x060030A0 RID: 12448 RVA: 0x00023D66 File Offset: 0x00021F66
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

	// Token: 0x060030A1 RID: 12449 RVA: 0x00149A14 File Offset: 0x00147C14
	public string RaycastFunction(Vector3 vector)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(vector), ref raycastHit))
		{
			return raycastHit.collider.name;
		}
		return "";
	}

	// Token: 0x060030A2 RID: 12450 RVA: 0x00183848 File Offset: 0x00181A48
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

	// Token: 0x060030A3 RID: 12451 RVA: 0x00023D6E File Offset: 0x00021F6E
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

	// Token: 0x060030A4 RID: 12452 RVA: 0x00183998 File Offset: 0x00181B98
	private void VideoNotAvailable()
	{
		GameObject.Find("Card3_FC_WatchVideo").transform.Find("HolderCard").gameObject.SetActive(false);
		GameObject.Find("Card3_FC_WatchVideo").transform.Find("HolderCard_NotAvailable").gameObject.SetActive(true);
		ShopManager.videoNotAvailable = true;
	}

	// Token: 0x060030A5 RID: 12453 RVA: 0x001839F4 File Offset: 0x00181BF4
	private static void ResetVideoNotAvailable()
	{
		GameObject.Find("Card3_FC_WatchVideo").transform.Find("HolderCard").gameObject.SetActive(true);
		GameObject.Find("Card3_FC_WatchVideo").transform.Find("HolderCard_NotAvailable").gameObject.SetActive(false);
		ShopManager.videoNotAvailable = false;
	}

	// Token: 0x04002C94 RID: 11412
	public static Transform shopHolder;

	// Token: 0x04002C95 RID: 11413
	public static Transform shopLevaIvica;

	// Token: 0x04002C96 RID: 11414
	public static Transform shopDesnaIvica;

	// Token: 0x04002C97 RID: 11415
	public static GameObject shopHeaderOn;

	// Token: 0x04002C98 RID: 11416
	public static GameObject shopHeaderOff;

	// Token: 0x04002C99 RID: 11417
	public static GameObject freeCoinsHeaderOn;

	// Token: 0x04002C9A RID: 11418
	public static GameObject freeCoinsHeaderOff;

	// Token: 0x04002C9B RID: 11419
	public static GameObject holderShopCard;

	// Token: 0x04002C9C RID: 11420
	public static GameObject holderFreeCoinsCard;

	// Token: 0x04002C9D RID: 11421
	public static Transform buttonShopBack;

	// Token: 0x04002C9E RID: 11422
	private string clickedItem;

	// Token: 0x04002C9F RID: 11423
	private string releasedItem;

	// Token: 0x04002CA0 RID: 11424
	private float vremeKlika;

	// Token: 0x04002CA1 RID: 11425
	private float startX;

	// Token: 0x04002CA2 RID: 11426
	private float endX;

	// Token: 0x04002CA3 RID: 11427
	private float pomerajX;

	// Token: 0x04002CA4 RID: 11428
	private static float levaGranica;

	// Token: 0x04002CA5 RID: 11429
	private static float desnaGranica;

	// Token: 0x04002CA6 RID: 11430
	private bool moved;

	// Token: 0x04002CA7 RID: 11431
	private bool released;

	// Token: 0x04002CA8 RID: 11432
	private bool bounce;

	// Token: 0x04002CA9 RID: 11433
	private bool started;

	// Token: 0x04002CAA RID: 11434
	private Transform tempObject;

	// Token: 0x04002CAB RID: 11435
	private GameObject temp;

	// Token: 0x04002CAC RID: 11436
	private float clickedPos;

	// Token: 0x04002CAD RID: 11437
	public static bool shopExists = true;

	// Token: 0x04002CAE RID: 11438
	public static bool freeCoinsExists = true;

	// Token: 0x04002CAF RID: 11439
	private static Vector3 originalScale;

	// Token: 0x04002CB0 RID: 11440
	private static float offset;

	// Token: 0x04002CB1 RID: 11441
	private static DateTime timeToShowNextElement;

	// Token: 0x04002CB2 RID: 11442
	private bool helpBool;

	// Token: 0x04002CB3 RID: 11443
	private static bool videoNotAvailable = false;

	// Token: 0x04002CB4 RID: 11444
	public static bool otvorenShop = false;
}
