using System;
using System.Collections;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
	public static Transform shopHolder;

	public static Transform shopLevaIvica;

	public static Transform shopDesnaIvica;

	public static GameObject shopHeaderOn;

	public static GameObject shopHeaderOff;

	public static GameObject freeCoinsHeaderOn;

	public static GameObject freeCoinsHeaderOff;

	public static GameObject holderShopCard;

	public static GameObject holderFreeCoinsCard;

	public static Transform buttonShopBack;

	private string clickedItem;

	private string releasedItem;

	private float vremeKlika;

	private float startX;

	private float endX;

	private float pomerajX;

	private static float levaGranica;

	private static float desnaGranica;

	private bool moved;

	private bool released;

	private bool bounce;

	private bool started;

	private Transform tempObject;

	private GameObject temp;

	private float clickedPos;

	public static bool shopExists = true;

	public static bool freeCoinsExists = true;

	private static Vector3 originalScale;

	private static float offset;

	private static DateTime timeToShowNextElement;

	private bool helpBool;

	private static bool videoNotAvailable = false;

	public static bool otvorenShop = false;

	private void Awake()
	{
		Object.DontDestroyOnLoad((Object)(object)((Component)this).gameObject);
	}

	private void Start()
	{
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		shopHolder = GameObject.Find("_HolderShop").transform;
		shopLevaIvica = GameObject.Find("ShopRamLevoHolder").transform;
		shopDesnaIvica = GameObject.Find("ShopRamDesnoHolder").transform;
		shopHeaderOn = GameObject.Find("ShopHeaderOn");
		shopHeaderOff = GameObject.Find("ShopHeaderOff1");
		freeCoinsHeaderOn = GameObject.Find("ShopHeaderOn1");
		freeCoinsHeaderOff = GameObject.Find("ShopHeaderOff");
		holderShopCard = GameObject.Find("HolderShopCard");
		holderFreeCoinsCard = GameObject.Find("HolderFreeCoinsCard");
		buttonShopBack = GameObject.Find("HolderBack").transform;
		originalScale = shopHolder.localScale;
		offset = 3.5f;
		shopHolder.localScale = shopHolder.localScale * Camera.main.orthographicSize / 5f;
		shopHolder.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.one / 2f).x, shopHolder.position.y, ((Component)Camera.main).transform.position.z + 5f);
		shopLevaIvica.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.zero).x, shopLevaIvica.position.y, shopLevaIvica.position.z);
		shopDesnaIvica.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.one).x, shopLevaIvica.position.y, shopLevaIvica.position.z);
		((Component)shopHolder).gameObject.SetActive(false);
		desnaGranica = ((Component)shopLevaIvica).transform.position.x + 3.5f;
		((Component)((Component)this).transform.Find("HolderFrame/ShopRamDesnoHolder/ShopRamDesno/FinishCoins/TextFreeCoinsUp1")).GetComponent<Renderer>().sortingLayerID = 1;
		((Component)((Component)this).transform.Find("HolderHeader/ShopHeaderOff/TextFreeCoinsDown")).GetComponent<Renderer>().sortingLayerID = 1;
		((Component)((Component)this).transform.Find("HolderHeader/ShopHeaderOff/TextFreeCoinsUp")).GetComponent<Renderer>().sortingLayerID = 1;
		((Component)((Component)this).transform.Find("HolderHeader/ShopHeaderOff1/TextShopDown")).GetComponent<Renderer>().sortingLayerID = 1;
		((Component)((Component)this).transform.Find("HolderHeader/ShopHeaderOff1/TextShopUp")).GetComponent<Renderer>().sortingLayerID = 1;
		((Component)((Component)this).transform.Find("HolderHeader/ShopHeaderOn/TextShopDown")).GetComponent<Renderer>().sortingLayerID = 1;
		((Component)((Component)this).transform.Find("HolderHeader/ShopHeaderOn/TextShopUp")).GetComponent<Renderer>().sortingLayerID = 1;
		((Component)((Component)this).transform.Find("HolderHeader/ShopHeaderOn1/TextFreeCoinsDown")).GetComponent<Renderer>().sortingLayerID = 1;
		((Component)((Component)this).transform.Find("HolderHeader/ShopHeaderOn1/TextFreeCoinsUp")).GetComponent<Renderer>().sortingLayerID = 1;
		((Component)((Component)this).transform.Find("HolderFrame/ShopBackground")).GetComponent<Renderer>().sortingLayerID = 1;
		((Component)((Component)this).transform.Find("HolderFrame/ShopBackground_Dif")).GetComponent<Renderer>().sortingLayerID = 1;
		((Component)((Component)this).transform.Find("HolderFreeCoinsCard/HolderFreeCoinsCardAnimation/Card3_FC_WatchVideo/HolderCard_NotAvailable/ShopTextOnCard")).GetComponent<Renderer>().sortingLayerID = 1;
		foreach (Transform item in ((Component)((Component)this).transform.Find("HolderFreeCoinsCard").GetChild(0)).transform)
		{
			((Component)item.Find("HolderCard/ShopPriceButton/ShopTextCoins1")).GetComponent<Renderer>().sortingLayerID = 1;
			((Component)item.Find("HolderCard/ShopPriceButton/ShopTextCoins2")).GetComponent<Renderer>().sortingLayerID = 1;
			((Component)item.Find("HolderCard/ShopTextOnCard")).GetComponent<Renderer>().sortingLayerID = 1;
			((Component)item.Find("HolderCard/ShopCardShine")).GetComponent<Renderer>().sortingLayerID = 1;
		}
		foreach (Transform item2 in ((Component)((Component)this).transform.Find("HolderShopCard").GetChild(0)).transform)
		{
			((Component)item2.Find("HolderCard/ShopPriceButton/ShopTextCoins1")).GetComponent<Renderer>().sortingLayerID = 1;
			((Component)item2.Find("HolderCard/ShopPriceButton/ShopTextCoins2")).GetComponent<Renderer>().sortingLayerID = 1;
			((Component)item2.Find("HolderCard/ShopBuyCoins/ShopTextCoins1")).GetComponent<Renderer>().sortingLayerID = 1;
			((Component)item2.Find("HolderCard/ShopBuyCoins/ShopTextCoins2")).GetComponent<Renderer>().sortingLayerID = 1;
			((Component)item2.Find("HolderCard/ShopCardShine")).GetComponent<Renderer>().sortingLayerID = 1;
		}
	}

	private void Update()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_0542: Unknown result type (might be due to invalid IL or missing references)
		//IL_0556: Unknown result type (might be due to invalid IL or missing references)
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		if (Input.GetMouseButtonDown(0))
		{
			if (released)
			{
				released = false;
			}
			clickedItem = RaycastFunction(Input.mousePosition);
			vremeKlika = Time.time;
			clickedPos = Input.mousePosition.x;
			if (started)
			{
				started = false;
				tempObject = null;
			}
			if (clickedItem.StartsWith("Card"))
			{
				startX = Input.mousePosition.x;
				started = true;
				tempObject = GameObject.Find(clickedItem).transform;
				float num = shopDesnaIvica.position.x - 1.5f;
				float num2 = tempObject.parent.childCount - 1;
				Bounds bounds = ((Collider)((Component)tempObject).GetComponent<BoxCollider>()).bounds;
				float num3 = num - num2 * ((Bounds)(ref bounds)).extents.x * 2f;
				bounds = ((Collider)((Component)tempObject).GetComponent<BoxCollider>()).bounds;
				levaGranica = num3 - ((Bounds)(ref bounds)).extents.x;
			}
		}
		if (Input.GetMouseButton(0) && started && ((tempObject.parent.childCount > 2 && Camera.main.aspect < 1.7777778f) || (tempObject.parent.childCount > 3 && Camera.main.aspect >= 1.7777778f)))
		{
			endX = Input.mousePosition.x;
			pomerajX = (endX - startX) * Camera.main.orthographicSize / 250f;
			if (pomerajX != 0f)
			{
				moved = true;
			}
			tempObject.parent.position = new Vector3(Mathf.Clamp(tempObject.parent.position.x + pomerajX, levaGranica, desnaGranica), tempObject.parent.position.y, tempObject.parent.position.z);
			startX = endX;
			Debug.Log((object)"Uledj");
		}
		if (released)
		{
			if (tempObject.parent.position.x <= levaGranica - 0.5f)
			{
				if (bounce)
				{
					pomerajX = 0.075f;
					bounce = false;
				}
			}
			else if (tempObject.parent.position.x >= desnaGranica && bounce)
			{
				pomerajX = -0.075f;
				bounce = false;
			}
			tempObject.parent.Translate(pomerajX, 0f, 0f);
			pomerajX *= 0.92f;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			releasedItem = RaycastFunction(Input.mousePosition);
			if (moved)
			{
				moved = false;
				released = true;
				bounce = true;
			}
			startX = (endX = 0f);
			if (clickedItem == releasedItem && releasedItem != string.Empty && Time.time - vremeKlika < 0.35f && Mathf.Abs(Input.mousePosition.x - clickedPos) < 50f)
			{
				if (releasedItem == "HolderBack")
				{
					if (PlaySounds.soundOn)
					{
						PlaySounds.Play_Button_OpenLevel();
					}
					if (Time.timeScale == 0f)
					{
						((MonoBehaviour)this).StartCoroutine(PausedAnim(buttonShopBack.GetChild(0), "BackButtonClick"));
						((MonoBehaviour)this).StartCoroutine(CloseShopPaused());
					}
					else
					{
						((Component)buttonShopBack.GetChild(0)).GetComponent<Animation>().Play("BackButtonClick");
						((MonoBehaviour)this).StartCoroutine(CloseShop());
					}
				}
				else if (releasedItem == "ShopHeaderOff1")
				{
					holderShopCard.transform.position = new Vector3(desnaGranica, holderShopCard.transform.position.y, holderShopCard.transform.position.z);
					shopHeaderOff.SetActive(false);
					shopHeaderOn.SetActive(true);
					freeCoinsHeaderOn.SetActive(false);
					freeCoinsHeaderOff.SetActive(true);
					holderFreeCoinsCard.SetActive(false);
					holderShopCard.SetActive(true);
					if (Time.timeScale == 0f)
					{
						((MonoBehaviour)this).StartCoroutine(PausedAnim(holderShopCard.transform.GetChild(0), "DolazakShop_A"));
					}
					else
					{
						((Component)holderShopCard.transform.GetChild(0)).GetComponent<Animation>().Play("DolazakShop_A");
					}
				}
				else if (releasedItem == "ShopHeaderOff")
				{
					holderFreeCoinsCard.transform.position = new Vector3(desnaGranica, holderFreeCoinsCard.transform.position.y, holderFreeCoinsCard.transform.position.z);
					shopHeaderOn.SetActive(false);
					shopHeaderOff.SetActive(true);
					freeCoinsHeaderOff.SetActive(false);
					freeCoinsHeaderOn.SetActive(true);
					holderShopCard.SetActive(false);
					holderFreeCoinsCard.SetActive(true);
					if (Time.timeScale == 0f)
					{
						((MonoBehaviour)this).StartCoroutine(PausedAnim(holderFreeCoinsCard.transform.GetChild(0), "DolazakShop_A"));
					}
					else
					{
						((Component)holderFreeCoinsCard.transform.GetChild(0)).GetComponent<Animation>().Play("DolazakShop_A");
					}
				}
				else if (releasedItem.StartsWith("Card"))
				{
					if (PlaySounds.soundOn)
					{
						PlaySounds.Play_Button_OpenLevel();
					}
					if (Time.timeScale == 0f)
					{
						temp = GameObject.Find(releasedItem);
						((MonoBehaviour)this).StartCoroutine(PausedAnim(temp.transform.GetChild(0), "ShopCardClick"));
					}
					else
					{
						temp = GameObject.Find(releasedItem);
						((Component)temp.transform.GetChild(0)).GetComponent<Animation>().Play("ShopCardClick");
					}
					if (releasedItem.Contains("LikeBananaIsland"))
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
					else if (releasedItem.Contains("LikeWebelinx"))
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
					if (!releasedItem.Contains("WatchVideo") && releasedItem.Contains("Buy"))
					{
						string text = releasedItem.Substring(releasedItem.IndexOf('y') + 1);
						Debug.Log((object)("Sta: " + text));
					}
				}
			}
		}
		if (Input.GetKeyUp((KeyCode)27) && ((Component)shopHolder).gameObject.activeSelf)
		{
			if (PlaySounds.soundOn)
			{
				PlaySounds.Play_Button_OpenLevel();
			}
			if (Time.timeScale == 0f)
			{
				((MonoBehaviour)this).StartCoroutine(PausedAnim(buttonShopBack.GetChild(0), "BackButtonClick"));
				((MonoBehaviour)this).StartCoroutine(CloseShopPaused());
			}
			else
			{
				((Component)buttonShopBack.GetChild(0)).GetComponent<Animation>().Play("BackButtonClick");
				((MonoBehaviour)this).StartCoroutine(CloseShop());
			}
		}
	}

	public static IEnumerator OpenShopCard()
	{
		((Component)((Component)shopDesnaIvica).transform.Find("ShopRamDesno/FinishCoins/TextFreeCoinsUp1")).GetComponent<TextMesh>().text = PlayerPrefs.GetInt("TotalMoney").ToString();
		((Component)shopHolder).transform.position = ((Component)Camera.main).transform.position + Vector3.forward * 5f;
		shopHeaderOff.SetActive(false);
		shopHeaderOn.SetActive(true);
		if (freeCoinsExists)
		{
			freeCoinsHeaderOn.SetActive(false);
			freeCoinsHeaderOff.SetActive(true);
		}
		holderFreeCoinsCard.SetActive(false);
		holderShopCard.SetActive(true);
		holderShopCard.transform.position = new Vector3(desnaGranica, holderShopCard.transform.position.y, holderShopCard.transform.position.z);
		yield return (object)new WaitForSeconds(0.25f);
		((Component)shopHolder).gameObject.SetActive(true);
		otvorenShop = true;
		((Component)holderShopCard.transform.GetChild(0)).GetComponent<Animation>().Play("DolazakShop_A");
		if (PlayerPrefs.HasKey("otisaoDaLajkuje"))
		{
			FacebookManager.lokacijaProvere = "Shop";
			FacebookManager.stranica = PlayerPrefs.GetString("stranica");
			FacebookManager.IDstranice = PlayerPrefs.GetString("IDstranice");
			GameObject.Find("FacebookManager").SendMessage("CheckLikes");
			Debug.Log((object)"Nagradi ga iz Shop");
		}
	}

	public static IEnumerator OpenFreeCoinsCard()
	{
		((Component)((Component)shopDesnaIvica).transform.Find("ShopRamDesno/FinishCoins/TextFreeCoinsUp1")).GetComponent<TextMesh>().text = PlayerPrefs.GetInt("TotalMoney").ToString();
		((Component)shopHolder).transform.position = ((Component)Camera.main).transform.position + Vector3.forward * 5f;
		if (shopExists)
		{
			shopHeaderOn.SetActive(false);
			shopHeaderOff.SetActive(true);
		}
		freeCoinsHeaderOff.SetActive(false);
		freeCoinsHeaderOn.SetActive(true);
		holderShopCard.SetActive(false);
		holderFreeCoinsCard.SetActive(true);
		holderFreeCoinsCard.transform.position = new Vector3(desnaGranica, holderFreeCoinsCard.transform.position.y, holderFreeCoinsCard.transform.position.z);
		yield return (object)new WaitForSeconds(0.25f);
		((Component)shopHolder).gameObject.SetActive(true);
		otvorenShop = true;
		if (videoNotAvailable)
		{
			ResetVideoNotAvailable();
		}
		((Component)holderFreeCoinsCard.transform.GetChild(0)).GetComponent<Animation>().Play("DolazakShop_A");
		if (PlayerPrefs.HasKey("otisaoDaLajkuje"))
		{
			FacebookManager.lokacijaProvere = "Shop";
			FacebookManager.stranica = PlayerPrefs.GetString("stranica");
			FacebookManager.IDstranice = PlayerPrefs.GetString("IDstranice");
			GameObject.Find("FacebookManager").SendMessage("CheckLikes");
			Debug.Log((object)"Nagradi ga iz Shop");
		}
	}

	public void ShopCardPaused()
	{
		((MonoBehaviour)this).StartCoroutine(OpenShopCardPaused());
	}

	public IEnumerator OpenShopCardPaused()
	{
		yield return null;
		((MonoBehaviour)this).StartCoroutine(PausedAnim(holderShopCard.transform.GetChild(0), "DolazakShop_A"));
	}

	public void FreeCoinsCardPaused()
	{
		((MonoBehaviour)this).StartCoroutine(OpenFreeCoinsCardPaused());
	}

	public static void shopPreparation_Paused()
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		((Component)((Component)shopDesnaIvica).transform.Find("ShopRamDesno/FinishCoins/TextFreeCoinsUp1")).GetComponent<TextMesh>().text = PlayerPrefs.GetInt("TotalMoney").ToString();
		((Component)shopHolder).transform.position = ((Component)Camera.main).transform.position + Vector3.forward * 5f;
		shopHeaderOff.SetActive(false);
		shopHeaderOn.SetActive(true);
		if (freeCoinsExists)
		{
			freeCoinsHeaderOn.SetActive(false);
			freeCoinsHeaderOff.SetActive(true);
		}
		holderFreeCoinsCard.SetActive(false);
		holderShopCard.SetActive(true);
		holderShopCard.transform.position = new Vector3(desnaGranica, holderShopCard.transform.position.y, holderShopCard.transform.position.z);
		((Component)shopHolder).gameObject.SetActive(true);
		otvorenShop = true;
	}

	public IEnumerator OpenFreeCoinsCardPaused()
	{
		yield return null;
		((MonoBehaviour)this).StartCoroutine(PausedAnim(holderFreeCoinsCard.transform.GetChild(0), "DolazakShop_A"));
	}

	public static void freeCoinsPreparation_Paused()
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		((Component)((Component)shopDesnaIvica).transform.Find("ShopRamDesno/FinishCoins/TextFreeCoinsUp1")).GetComponent<TextMesh>().text = PlayerPrefs.GetInt("TotalMoney").ToString();
		((Component)shopHolder).transform.position = ((Component)Camera.main).transform.position + Vector3.forward * 5f;
		if (shopExists)
		{
			shopHeaderOn.SetActive(false);
			shopHeaderOff.SetActive(true);
		}
		freeCoinsHeaderOff.SetActive(false);
		freeCoinsHeaderOn.SetActive(true);
		holderShopCard.SetActive(false);
		holderFreeCoinsCard.SetActive(true);
		holderFreeCoinsCard.transform.position = new Vector3(desnaGranica, holderFreeCoinsCard.transform.position.y, holderFreeCoinsCard.transform.position.z);
		((Component)shopHolder).gameObject.SetActive(true);
		otvorenShop = true;
		if (videoNotAvailable)
		{
			ResetVideoNotAvailable();
		}
	}

	private IEnumerator CloseShop()
	{
		yield return (object)new WaitForSeconds(0.85f);
		((Component)shopHolder).gameObject.SetActive(false);
		otvorenShop = false;
		shopHolder.position = new Vector3(-5f, -5f, shopHolder.position.z);
		buttonShopBack.GetChild(0).localPosition = Vector3.zero;
	}

	private IEnumerator CloseShopPaused()
	{
		timeToShowNextElement = DateTime.Now.AddSeconds(0.8500000238418579);
		while (DateTime.Now < timeToShowNextElement)
		{
			yield return null;
		}
		((Component)shopHolder).gameObject.SetActive(false);
		otvorenShop = false;
		shopHolder.position = new Vector3(-5f, -5f, shopHolder.position.z);
		buttonShopBack.GetChild(0).localPosition = Vector3.zero;
	}

	public string RaycastFunction(Vector3 vector)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		RaycastHit val = default(RaycastHit);
		if (Physics.Raycast(Camera.main.ScreenPointToRay(vector), ref val))
		{
			return ((Object)((RaycastHit)(ref val)).collider).name;
		}
		return "";
	}

	public static void RescaleShop()
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		shopHolder.localScale = originalScale * Camera.main.orthographicSize / 5f;
		float num = offset * Camera.main.orthographicSize / 5f;
		shopHolder.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.one / 2f).x, shopHolder.position.y, ((Component)Camera.main).transform.position.z + 5f);
		shopLevaIvica.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.zero).x, shopLevaIvica.position.y, shopLevaIvica.position.z);
		shopDesnaIvica.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.one).x, shopLevaIvica.position.y, shopLevaIvica.position.z);
		((Component)shopHolder).gameObject.SetActive(false);
		desnaGranica = ((Component)shopLevaIvica).transform.position.x + num;
	}

	private IEnumerator PausedAnim(Transform obj, string ime)
	{
		((MonoBehaviour)this).StartCoroutine(((Component)obj).GetComponent<Animation>().Play(ime, useTimeScale: false, delegate
		{
			helpBool = true;
		}));
		while (!helpBool)
		{
			yield return null;
		}
		helpBool = false;
	}

	private void VideoNotAvailable()
	{
		((Component)GameObject.Find("Card3_FC_WatchVideo").transform.Find("HolderCard")).gameObject.SetActive(false);
		((Component)GameObject.Find("Card3_FC_WatchVideo").transform.Find("HolderCard_NotAvailable")).gameObject.SetActive(true);
		videoNotAvailable = true;
	}

	private static void ResetVideoNotAvailable()
	{
		((Component)GameObject.Find("Card3_FC_WatchVideo").transform.Find("HolderCard")).gameObject.SetActive(true);
		((Component)GameObject.Find("Card3_FC_WatchVideo").transform.Find("HolderCard_NotAvailable")).gameObject.SetActive(false);
		videoNotAvailable = false;
	}
}
