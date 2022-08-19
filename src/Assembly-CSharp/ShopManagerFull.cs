using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

// Token: 0x020004C5 RID: 1221
public class ShopManagerFull : MonoBehaviour
{
	// Token: 0x060026FA RID: 9978 RVA: 0x0011A074 File Offset: 0x00118274
	private void Awake()
	{
		ShopManagerFull.ShopObject = this;
		if (this.EarsAndHairCustomization)
		{
			this.UsiHats.Add("1");
			this.UsiHats.Add("1");
			this.UsiHats.Add("1");
			this.UsiHats.Add("1");
			this.UsiHats.Add("1");
			this.UsiHats.Add("1");
			this.UsiHats.Add("1");
			this.UsiHats.Add("1");
			this.UsiHats.Add("1");
			this.UsiHats.Add("1");
			this.UsiHats.Add("0");
			this.UsiHats.Add("1");
			this.UsiHats.Add("0");
			this.UsiHats.Add("1");
			this.UsiHats.Add("0");
			this.KosaHats.Add("0");
			this.KosaHats.Add("0");
			this.KosaHats.Add("0");
			this.KosaHats.Add("1");
			this.KosaHats.Add("0");
			this.KosaHats.Add("0");
			this.KosaHats.Add("0");
			this.KosaHats.Add("0");
			this.KosaHats.Add("0");
			this.KosaHats.Add("0");
			this.KosaHats.Add("0");
			this.KosaHats.Add("0");
			this.KosaHats.Add("0");
			this.KosaHats.Add("0");
			this.KosaHats.Add("0");
		}
		else
		{
			this.UsiHats.Add("1");
			this.UsiHats.Add("1");
			this.UsiHats.Add("1");
			this.UsiHats.Add("1");
			this.UsiHats.Add("1");
			this.UsiHats.Add("1");
			this.UsiHats.Add("1");
			this.UsiHats.Add("1");
			this.UsiHats.Add("1");
			this.UsiHats.Add("1");
			this.UsiHats.Add("1");
			this.UsiHats.Add("1");
			this.UsiHats.Add("1");
			this.UsiHats.Add("1");
			this.UsiHats.Add("1");
			this.KosaHats.Add("1");
			this.KosaHats.Add("1");
			this.KosaHats.Add("1");
			this.KosaHats.Add("1");
			this.KosaHats.Add("1");
			this.KosaHats.Add("1");
			this.KosaHats.Add("1");
			this.KosaHats.Add("1");
			this.KosaHats.Add("1");
			this.KosaHats.Add("1");
			this.KosaHats.Add("1");
			this.KosaHats.Add("1");
			this.KosaHats.Add("1");
			this.KosaHats.Add("1");
			this.KosaHats.Add("1");
		}
		this.ButtonShop = GameObject.Find("ButtonBuy");
		this.ButtonShopSprite = GameObject.Find("Buy Button");
		this.PreviewShopButton = GameObject.Find("Preview Button");
	}

	// Token: 0x060026FB RID: 9979 RVA: 0x0011A488 File Offset: 0x00118688
	private void Start()
	{
		if ((double)Camera.main.aspect < 1.51)
		{
			GameObject.Find("ButtonBackShop").transform.localPosition = new Vector3(-1.58f, -0.8f, 0f);
		}
		this.ShopBanana = GameObject.Find("Shop Banana");
		ShopManagerFull.AktivanCustomizationTab = 1;
		this.ZidFooter = GameObject.Find("ZidFooterShop");
		this.Custumization = GameObject.Find("Custumization");
		this.Custumization.transform.Find("Znak Uzvika telo").gameObject.SetActive(false);
		this.CoinsNumber = GameObject.Find("Shop/Shop Interface/Coins");
		this.CoinsNumber.transform.Find("Coins Number").GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
		this.CoinsNumber.transform.Find("Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Shop/2 Shop - BANANA/Zid Shop/Zid Header i Footer/Zid Footer Shop/Banana Number/Number").GetComponent<TextMesh>().text = StagesParser.currentBananas.ToString();
		GameObject.Find("Shop/2 Shop - BANANA/Zid Shop/Zid Header i Footer/Zid Footer Shop/Banana Number/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Double Coins Number/Number").GetComponent<TextMesh>().text = StagesParser.powerup_doublecoins.ToString();
		GameObject.Find("Double Coins Number/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Magnet Number/Number").GetComponent<TextMesh>().text = StagesParser.powerup_magnets.ToString();
		GameObject.Find("Magnet Number/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Shield Number/Number").GetComponent<TextMesh>().text = StagesParser.powerup_shields.ToString();
		GameObject.Find("Shield Number/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		this.ImaNovihMajica = false;
		this.ImaNovihKapa = false;
		this.ImaNovihRanceva = false;
		if (Application.loadedLevel == 1)
		{
			this.ShopCustomizationPozicija = new Vector3(6.586132f, -5.05306f, -31.75042f);
		}
		else
		{
			this.ShopCustomizationPozicija = new Vector3(-16.58703f, -98.95457f, -50f);
		}
		if (PlayerPrefs.HasKey("AktivniItemi"))
		{
			this.AktivniItemString = PlayerPrefs.GetString("AktivniItemi");
			this.AktivniItemi = this.AktivniItemString.Split(new char[]
			{
				'#'
			});
			ShopManagerFull.AktivanSesir = int.Parse(this.AktivniItemi[0]);
			ShopManagerFull.AktivnaMajica = int.Parse(this.AktivniItemi[1]);
			ShopManagerFull.AktivanRanac = int.Parse(this.AktivniItemi[2]);
		}
		else
		{
			ShopManagerFull.AktivanSesir = -1;
			ShopManagerFull.AktivnaMajica = -1;
			ShopManagerFull.AktivanRanac = -1;
		}
		this.PreviewSesir = -1;
		this.PreviewMajica = -1;
		this.PreviewRanac = -1;
		this.MajmunBobo = GameObject.Find("MonkeyHolder");
		this.CustomizationHats = GameObject.Find("1Hats");
		this.CustomizationShirts = GameObject.Find("2Shirts");
		this.CustomizationBackPack = GameObject.Find("3BackPack");
		this.MainScenaPozicija = this.MajmunBobo.transform.position;
		this.BrojItemaShopHats = this.CountItemsInShop(GameObject.Find("Shop/3 Customize/Customize Tabovi/1Hats").GetComponent<Transform>());
		this.BrojItemaShopShirts = this.CountItemsInShop(GameObject.Find("Shop/3 Customize/Customize Tabovi/2Shirts").GetComponent<Transform>());
		this.BrojItemaShopBackPack = this.CountItemsInShop(GameObject.Find("Shop/3 Customize/Customize Tabovi/3BackPack").GetComponent<Transform>());
		this.BrojItemaShop = this.BrojItemaShopHats + this.BrojItemaShopShirts + this.BrojItemaShopBackPack;
		this.ObuciMajmunaNaStartu();
		base.transform.name = "Shop";
		this.SviItemiInvetory();
		base.StartCoroutine(this.PokreniInicijalizacijuShopa());
	}

	// Token: 0x060026FC RID: 9980 RVA: 0x0011A82C File Offset: 0x00118A2C
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			this.clickedItem = this.RaycastFunction(Input.mousePosition);
			if (this.clickedItem.Equals("NekiNaziv") || this.clickedItem.Equals("NekiNaziv1"))
			{
				this.temp = GameObject.Find(this.clickedItem);
				this.originalScale = this.temp.transform.localScale;
				this.temp.transform.localScale = this.originalScale * 0.8f;
			}
			else if (this.clickedItem != string.Empty)
			{
				this.temp = GameObject.Find(this.clickedItem);
				this.originalScale = this.temp.transform.localScale;
			}
		}
		if (Input.GetMouseButtonUp(0))
		{
			this.releasedItem = this.RaycastFunction(Input.mousePosition);
			if (!this.clickedItem.Equals(string.Empty))
			{
				if (this.temp != null)
				{
					this.temp.transform.localScale = this.originalScale;
				}
				if (this.releasedItem == "NekoDugme" && PlaySounds.soundOn)
				{
					PlaySounds.Play_Button_OpenLevel();
				}
			}
		}
		if (ObjCustomizationHats.CustomizationHats || ObjCustomizationShirts.CustomizationShirts || ObjCustomizationBackPacks.CustomizationBackPacks)
		{
			if (ShopManagerFull.AktivanCustomizationTab == 1)
			{
				this.ProveraTrenutnogItema(ShopManagerFull.AktivanItemSesir);
				return;
			}
			if (ShopManagerFull.AktivanCustomizationTab == 2)
			{
				this.ProveraTrenutnogItema(ShopManagerFull.AktivanItemMajica);
				return;
			}
			if (ShopManagerFull.AktivanCustomizationTab == 3)
			{
				this.ProveraTrenutnogItema(ShopManagerFull.AktivanItemRanac);
			}
		}
	}

	// Token: 0x060026FD RID: 9981 RVA: 0x0011A9B8 File Offset: 0x00118BB8
	private string RaycastFunction(Vector3 vector)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(vector), ref raycastHit))
		{
			return raycastHit.collider.name;
		}
		return "";
	}

	// Token: 0x060026FE RID: 9982 RVA: 0x0011A9EC File Offset: 0x00118BEC
	private int CountItemsInShop(Transform Shop)
	{
		int num = 0;
		foreach (object obj in Shop)
		{
			Transform transform = (Transform)obj;
			num++;
		}
		return num;
	}

	// Token: 0x060026FF RID: 9983 RVA: 0x0011AA40 File Offset: 0x00118C40
	public void RefresujImenaItema()
	{
		base.StartCoroutine(this.ParsirajImenaItemaIzRadnje());
	}

	// Token: 0x06002700 RID: 9984 RVA: 0x0011AA4F File Offset: 0x00118C4F
	public void PobrisiSveOtkljucanoIzShopa()
	{
		this.ZakljucaniHats.Clear();
		this.ZakljucaniShirts.Clear();
		this.ZakljucaniBackPacks.Clear();
	}

	// Token: 0x06002701 RID: 9985 RVA: 0x0011AA72 File Offset: 0x00118C72
	private IEnumerator ParsirajImenaItemaIzRadnje()
	{
		yield return null;
		this.aset2 = (TextAsset)Resources.Load("xmls/Shop/Shop" + LanguageManager.chosenLanguage);
		this.aset = this.aset2.text;
		this.CoinsHats = new List<string>();
		this.CoinsShirts = new List<string>();
		this.CoinsBackPacks = new List<string>();
		this.CoinsPowerUps = new List<string>();
		this.ImenaHats = new List<string>();
		this.ImenaShirts = new List<string>();
		this.ImenaBackPacks = new List<string>();
		this.ImenaPowerUps = new List<string>();
		IEnumerable<XElement> source = XElement.Parse(this.aset.ToString()).Elements();
		source.Count<XElement>();
		if (StagesParser.unlockedWorlds[4])
		{
			this.ProcenatOtkljucan = 1f;
		}
		else if (StagesParser.unlockedWorlds[3])
		{
			this.ProcenatOtkljucan = 0.9f;
		}
		else if (StagesParser.unlockedWorlds[2])
		{
			this.ProcenatOtkljucan = 0.8f;
		}
		else if (StagesParser.unlockedWorlds[1])
		{
			this.ProcenatOtkljucan = 0.7f;
		}
		else if (StagesParser.unlockedWorlds[0])
		{
			this.ProcenatOtkljucan = 0.6f;
		}
		if (PlayerPrefs.HasKey("OtkljucaniItemi"))
		{
			this.StariBrojOtkljucanihItema = PlayerPrefs.GetString("OtkljucaniItemi");
		}
		else
		{
			this.StariBrojOtkljucanihItema = "0#0#0";
		}
		this.StariBrojOtkljucanihItemaNiz = this.StariBrojOtkljucanihItema.Split(new char[]
		{
			'#'
		});
		ShopManagerFull.StariBrojOtkljucanihKapa = int.Parse(this.StariBrojOtkljucanihItemaNiz[0]);
		ShopManagerFull.StariBrojOtkljucanihMajici = int.Parse(this.StariBrojOtkljucanihItemaNiz[1]);
		ShopManagerFull.StariBrojOtkljucanihRanceva = int.Parse(this.StariBrojOtkljucanihItemaNiz[2]);
		ShopManagerFull.BrojOtkljucanihKapa = Mathf.FloorToInt((float)this.BrojItemaShopHats * this.ProcenatOtkljucan) - 1;
		ShopManagerFull.BrojOtkljucanihMajici = Mathf.FloorToInt((float)this.BrojItemaShopShirts * this.ProcenatOtkljucan) - 1;
		ShopManagerFull.BrojOtkljucanihRanceva = Mathf.FloorToInt((float)this.BrojItemaShopBackPack * this.ProcenatOtkljucan) - 1;
		this.StariBrojOtkljucanihItema = string.Concat(new object[]
		{
			ShopManagerFull.BrojOtkljucanihKapa,
			"#",
			ShopManagerFull.BrojOtkljucanihMajici,
			"#",
			ShopManagerFull.BrojOtkljucanihRanceva
		});
		PlayerPrefs.SetString("OtkljucaniItemi", this.StariBrojOtkljucanihItema);
		PlayerPrefs.Save();
		for (int i = 0; i < this.BrojItemaShopHats; i++)
		{
			if (ShopManagerFull.BrojOtkljucanihKapa >= i)
			{
				this.ZakljucaniHats.Add(1);
			}
			else
			{
				this.ZakljucaniHats.Add(0);
			}
		}
		for (int j = 0; j < this.BrojItemaShopShirts; j++)
		{
			if (ShopManagerFull.BrojOtkljucanihMajici >= j)
			{
				this.ZakljucaniShirts.Add(1);
			}
			else
			{
				this.ZakljucaniShirts.Add(0);
			}
		}
		for (int k = 0; k < this.BrojItemaShopBackPack; k++)
		{
			if (ShopManagerFull.BrojOtkljucanihRanceva >= k)
			{
				this.ZakljucaniBackPacks.Add(1);
			}
			else
			{
				this.ZakljucaniBackPacks.Add(0);
			}
		}
		if (source.Count<XElement>() == this.BrojItemaShop + 4)
		{
			for (int l = 0; l < this.BrojItemaShopHats; l++)
			{
				if (this.ZakljucaniHats[l] == 1)
				{
					this.HatsObjects[l].Find("Zakkljucano").gameObject.SetActive(false);
					this.HatsObjects[l].Find("Polje za unos COINA U shopu - Shop").gameObject.SetActive(true);
				}
				else
				{
					this.HatsObjects[l].Find("Polje za unos COINA U shopu - Shop").gameObject.SetActive(false);
					this.HatsObjects[l].Find("Bedz - Popust").gameObject.SetActive(false);
					this.HatsObjects[l].Find("Zakkljucano").gameObject.SetActive(true);
					this.HatsObjects[l].Find("Polje za unos COINA U shopu - Shop_NoDiscount").gameObject.SetActive(false);
				}
				if (source.ElementAt(l).Attribute("kategorija").Value == "Hats")
				{
					this.ImenaHats.Add(source.ElementAt(l).Value);
					this.CoinsHats.Add(source.ElementAt(l).Attribute("coins").Value);
					this.BananaHats.Add(source.ElementAt(l).Attribute("banana").Value);
					this.PopustHats.Add(source.ElementAt(l).Attribute("popust").Value);
					if (ShopManagerFull.SveStvariZaOblacenjeHats[l] == 1)
					{
						this.HatsObjects[l].Find("Polje za unos COINA U shopu - Shop").gameObject.SetActive(false);
						this.HatsObjects[l].Find("Bedz - Popust").gameObject.SetActive(false);
						this.HatsObjects[l].Find("Polje za unos COINA U shopu - Shop_NoDiscount").gameObject.SetActive(false);
					}
					else if (this.PopustHats[l] == "0")
					{
						this.HatsObjects[l].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMesh>().text = this.CoinsHats[l];
						this.HatsObjects[l].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
						if (l > ShopManagerFull.StariBrojOtkljucanihKapa & l <= ShopManagerFull.BrojOtkljucanihKapa)
						{
							this.ImaNovihKapa = true;
							this.HatsObjects[l].Find("Bedz - Popust/Text/Number").GetComponent<TextMesh>().text = LanguageManager.New;
							this.HatsObjects[l].Find("Bedz - Popust/Text/Number/Number Shadow").GetComponent<TextMesh>().text = LanguageManager.New;
						}
						else
						{
							this.HatsObjects[l].Find("Bedz - Popust").gameObject.SetActive(false);
						}
					}
					else if (l > ShopManagerFull.StariBrojOtkljucanihKapa & l <= ShopManagerFull.BrojOtkljucanihKapa)
					{
						this.ImaNovihKapa = true;
						this.HatsObjects[l].Find("Bedz - Popust/Text/Number").GetComponent<TextMesh>().text = LanguageManager.New;
						this.HatsObjects[l].Find("Bedz - Popust/Text/Number/Number Shadow").GetComponent<TextMesh>().text = LanguageManager.New;
						if (this.ZakljucaniHats[l] == 1)
						{
							this.HatsObjects[l].Find("Polje za unos COINA U shopu - Shop_NoDiscount").gameObject.SetActive(true);
							this.HatsObjects[l].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number").GetComponent<TextMesh>().text = this.CoinsHats[l];
							if (this.HatsObjects[l].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number").gameObject.activeSelf)
							{
								this.HatsObjects[l].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
							}
							string s = "0." + this.PopustHats[l];
							float num = float.Parse(this.CoinsHats[l]) - float.Parse(this.CoinsHats[l]) * float.Parse(s);
							this.CoinsHats[l] = num.ToString();
							this.HatsObjects[l].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMesh>().text = num.ToString();
							this.HatsObjects[l].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
							this.HatsObjects[l].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMesh>().color = ShopManagerFull.PopustBoja;
						}
					}
					else if (this.ZakljucaniHats[l] == 1)
					{
						this.HatsObjects[l].Find("Polje za unos COINA U shopu - Shop_NoDiscount").gameObject.SetActive(true);
						this.HatsObjects[l].Find("Bedz - Popust/Text/Number").GetComponent<TextMesh>().text = "-" + this.PopustHats[l] + "%";
						this.HatsObjects[l].Find("Bedz - Popust/Text/Number/Number Shadow").GetComponent<TextMesh>().text = this.PopustHats[l] + "%";
						this.HatsObjects[l].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number").GetComponent<TextMesh>().text = this.CoinsHats[l];
						if (this.HatsObjects[l].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number").gameObject.activeSelf)
						{
							this.HatsObjects[l].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
						}
						string s2 = "0." + this.PopustHats[l];
						float num2 = float.Parse(this.CoinsHats[l]) - float.Parse(this.CoinsHats[l]) * float.Parse(s2);
						this.CoinsHats[l] = num2.ToString();
						this.HatsObjects[l].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMesh>().text = num2.ToString();
						this.HatsObjects[l].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
						this.HatsObjects[l].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMesh>().color = ShopManagerFull.PopustBoja;
					}
					this.HatsObjects[l].Find("Text/ime").GetComponent<TextMesh>().text = this.ImenaHats[l];
					this.HatsObjects[l].Find("Text/ime").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
				}
			}
			for (int m = 0; m < this.BrojItemaShopShirts; m++)
			{
				if (this.ZakljucaniShirts[m] == 1)
				{
					this.ShirtsObjects[m].Find("Zakkljucano").gameObject.SetActive(false);
					this.ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop").gameObject.SetActive(true);
				}
				else
				{
					this.ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop").gameObject.SetActive(false);
					this.ShirtsObjects[m].Find("Bedz - Popust").gameObject.SetActive(false);
					this.ShirtsObjects[m].Find("Zakkljucano").gameObject.SetActive(true);
					this.ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop_NoDiscount").gameObject.SetActive(false);
				}
				if (source.ElementAt(this.BrojItemaShopHats + m).Attribute("kategorija").Value == "Shirts")
				{
					this.ImenaShirts.Add(source.ElementAt(this.BrojItemaShopHats + m).Value);
					this.CoinsShirts.Add(source.ElementAt(this.BrojItemaShopHats + m).Attribute("coins").Value);
					this.BananaShirts.Add(source.ElementAt(this.BrojItemaShopHats + m).Attribute("banana").Value);
					this.PopustShirts.Add(source.ElementAt(this.BrojItemaShopHats + m).Attribute("popust").Value);
					if (ShopManagerFull.SveStvariZaOblacenjeShirts[m] == 1)
					{
						this.ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop").gameObject.SetActive(false);
						this.ShirtsObjects[m].Find("Bedz - Popust").gameObject.SetActive(false);
						this.ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop_NoDiscount").gameObject.SetActive(false);
					}
					else if (this.PopustShirts[m] == "0")
					{
						this.ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMesh>().text = this.CoinsShirts[m];
						this.ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
						if (m > ShopManagerFull.StariBrojOtkljucanihMajici & m <= ShopManagerFull.BrojOtkljucanihMajici)
						{
							this.ImaNovihMajica = true;
							this.ShirtsObjects[m].Find("Bedz - Popust/Text/Number").GetComponent<TextMesh>().text = LanguageManager.New;
							this.ShirtsObjects[m].Find("Bedz - Popust/Text/Number/Number Shadow").GetComponent<TextMesh>().text = LanguageManager.New;
						}
						else
						{
							this.ShirtsObjects[m].Find("Bedz - Popust").gameObject.SetActive(false);
						}
					}
					else if (m > ShopManagerFull.StariBrojOtkljucanihMajici & m <= ShopManagerFull.BrojOtkljucanihMajici)
					{
						this.ImaNovihMajica = true;
						this.ShirtsObjects[m].Find("Bedz - Popust/Text/Number").GetComponent<TextMesh>().text = LanguageManager.New;
						this.ShirtsObjects[m].Find("Bedz - Popust/Text/Number/Number Shadow").GetComponent<TextMesh>().text = LanguageManager.New;
						if (this.ZakljucaniShirts[m] == 1)
						{
							this.ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop_NoDiscount").gameObject.SetActive(true);
							this.ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number").GetComponent<TextMesh>().text = this.CoinsShirts[m].ToString();
							if (this.ShirtsObjects[m].parent.gameObject.activeSelf)
							{
								this.ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
							}
							string s3 = "0." + this.PopustShirts[m];
							float num3 = float.Parse(this.CoinsShirts[m]) - float.Parse(this.CoinsShirts[m]) * float.Parse(s3);
							this.CoinsShirts[m] = num3.ToString();
							this.ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMesh>().text = num3.ToString();
							this.ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
							this.ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMesh>().color = ShopManagerFull.PopustBoja;
						}
					}
					else if (this.ZakljucaniShirts[m] == 1)
					{
						this.ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop_NoDiscount").gameObject.SetActive(true);
						this.ShirtsObjects[m].Find("Bedz - Popust/Text/Number").GetComponent<TextMesh>().text = "-" + this.PopustShirts[m] + "%";
						this.ShirtsObjects[m].Find("Bedz - Popust/Text/Number/Number Shadow").GetComponent<TextMesh>().text = this.PopustShirts[m] + "%";
						this.ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number").GetComponent<TextMesh>().text = this.CoinsShirts[m].ToString();
						if (this.ShirtsObjects[m].parent.gameObject.activeSelf)
						{
							this.ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
						}
						string s4 = "0." + this.PopustShirts[m];
						float num4 = float.Parse(this.CoinsShirts[m]) - float.Parse(this.CoinsShirts[m]) * float.Parse(s4);
						this.CoinsShirts[m] = num4.ToString();
						this.ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMesh>().text = num4.ToString();
						this.ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
						this.ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMesh>().color = ShopManagerFull.PopustBoja;
					}
					this.ShirtsObjects[m].Find("Text/ime").GetComponent<TextMesh>().text = this.ImenaShirts[m];
					this.ShirtsObjects[m].Find("Text/ime").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
				}
			}
			for (int n = 0; n < this.BrojItemaShopBackPack; n++)
			{
				if (this.ZakljucaniBackPacks[n] == 1)
				{
					this.BackPacksObjects[n].Find("Zakkljucano").gameObject.SetActive(false);
					this.BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop").gameObject.SetActive(true);
				}
				else
				{
					this.BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop").gameObject.SetActive(false);
					this.BackPacksObjects[n].Find("Bedz - Popust").gameObject.SetActive(false);
					this.BackPacksObjects[n].Find("Zakkljucano").gameObject.SetActive(true);
					this.BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop_NoDiscount").gameObject.SetActive(false);
				}
				if (source.ElementAt(this.BrojItemaShopHats + this.BrojItemaShopShirts + n).Attribute("kategorija").Value == "BackPack")
				{
					this.ImenaBackPacks.Add(source.ElementAt(this.BrojItemaShopHats + this.BrojItemaShopShirts + n).Value);
					this.CoinsBackPacks.Add(source.ElementAt(this.BrojItemaShopHats + this.BrojItemaShopShirts + n).Attribute("coins").Value);
					this.BananaBackPacks.Add(source.ElementAt(this.BrojItemaShopHats + this.BrojItemaShopShirts + n).Attribute("banana").Value);
					this.PopustBackPacks.Add(source.ElementAt(this.BrojItemaShopHats + this.BrojItemaShopShirts + n).Attribute("popust").Value);
					if (ShopManagerFull.SveStvariZaOblacenjeBackPack[n] == 1)
					{
						this.BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop").gameObject.SetActive(false);
						this.BackPacksObjects[n].Find("Bedz - Popust").gameObject.SetActive(false);
						this.BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop_NoDiscount").gameObject.SetActive(false);
					}
					else if (this.PopustBackPacks[n] == "0")
					{
						this.BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMesh>().text = this.CoinsBackPacks[n];
						this.BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
						if (n > ShopManagerFull.StariBrojOtkljucanihRanceva & n <= ShopManagerFull.BrojOtkljucanihRanceva)
						{
							this.ImaNovihRanceva = true;
							this.BackPacksObjects[n].Find("Bedz - Popust/Text/Number").GetComponent<TextMesh>().text = LanguageManager.New;
							this.BackPacksObjects[n].Find("Bedz - Popust/Text/Number/Number Shadow").GetComponent<TextMesh>().text = LanguageManager.New;
						}
						else
						{
							this.BackPacksObjects[n].Find("Bedz - Popust").gameObject.SetActive(false);
						}
					}
					else
					{
						this.BackPacksObjects[n].Find("Bedz - Popust/Text/Number").GetComponent<TextMesh>().text = "-" + this.PopustBackPacks[n] + "%";
						this.BackPacksObjects[n].Find("Bedz - Popust/Text/Number/Number Shadow").GetComponent<TextMesh>().text = this.PopustBackPacks[n] + "%";
						if (n > ShopManagerFull.StariBrojOtkljucanihRanceva & n <= ShopManagerFull.BrojOtkljucanihRanceva)
						{
							this.ImaNovihRanceva = true;
							this.BackPacksObjects[n].Find("Bedz - Popust/Text/Number").GetComponent<TextMesh>().text = LanguageManager.New;
							this.BackPacksObjects[n].Find("Bedz - Popust/Text/Number/Number Shadow").GetComponent<TextMesh>().text = LanguageManager.New;
							if (this.ZakljucaniBackPacks[n] == 1)
							{
								this.BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop_NoDiscount").gameObject.SetActive(true);
								this.BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number").GetComponent<TextMesh>().text = this.CoinsBackPacks[n].ToString();
								if (this.BackPacksObjects[n].parent.gameObject.activeSelf)
								{
									this.BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
								}
								string s5 = "0." + this.PopustBackPacks[n];
								float num5 = float.Parse(this.CoinsBackPacks[n]) - float.Parse(this.CoinsBackPacks[n]) * float.Parse(s5);
								this.CoinsBackPacks[n] = num5.ToString();
								this.BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMesh>().text = num5.ToString();
								this.BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
								this.BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMesh>().color = ShopManagerFull.PopustBoja;
							}
						}
						else if (this.ZakljucaniBackPacks[n] == 1)
						{
							this.BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop_NoDiscount").gameObject.SetActive(true);
							this.BackPacksObjects[n].Find("Bedz - Popust/Text/Number").GetComponent<TextMesh>().text = "-" + this.PopustBackPacks[n] + "%";
							this.BackPacksObjects[n].Find("Bedz - Popust/Text/Number/Number Shadow").GetComponent<TextMesh>().text = this.PopustBackPacks[n] + "%";
							this.BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number").GetComponent<TextMesh>().text = this.CoinsBackPacks[n].ToString();
							if (this.BackPacksObjects[n].parent.gameObject.activeSelf)
							{
								this.BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
							}
							string s6 = "0." + this.PopustBackPacks[n];
							float num6 = float.Parse(this.CoinsBackPacks[n]) - float.Parse(this.CoinsBackPacks[n]) * float.Parse(s6);
							this.CoinsBackPacks[n] = num6.ToString();
							this.BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMesh>().text = num6.ToString();
							this.BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
							this.BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMesh>().color = ShopManagerFull.PopustBoja;
						}
					}
					this.BackPacksObjects[n].Find("Text/ime").GetComponent<TextMesh>().text = this.ImenaBackPacks[n];
					this.BackPacksObjects[n].Find("Text/ime").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
				}
			}
			for (int num7 = 0; num7 < 3; num7++)
			{
				if (source.ElementAt(this.BrojItemaShopHats + this.BrojItemaShopShirts + this.BrojItemaShopBackPack + num7).Attribute("kategorija").Value == "PowerUps")
				{
					this.ImenaPowerUps.Add(source.ElementAt(this.BrojItemaShopHats + this.BrojItemaShopShirts + this.BrojItemaShopBackPack + num7).Value);
					this.CoinsPowerUps.Add(source.ElementAt(this.BrojItemaShopHats + this.BrojItemaShopShirts + this.BrojItemaShopBackPack + num7).Attribute("coins").Value);
					this.PopustPowerUps.Add(source.ElementAt(this.BrojItemaShopHats + this.BrojItemaShopShirts + this.BrojItemaShopBackPack + num7).Attribute("popust").Value);
					if (this.PopustPowerUps[num7] == "0")
					{
						this.PowerUpsObjects[num7].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMesh>().text = this.CoinsPowerUps[num7];
						this.PowerUpsObjects[num7].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
						this.PowerUpsObjects[num7].Find("Popust").gameObject.SetActive(false);
					}
					else
					{
						this.PowerUpsObjects[num7].Find("Popust/Text/Number").GetComponent<TextMesh>().text = "-" + this.PopustPowerUps[num7] + "%";
						this.PowerUpsObjects[num7].Find("Popust/Text/Number/Number Shadow").GetComponent<TextMesh>().text = this.PopustPowerUps[num7] + "%";
						string s7 = "0." + this.PopustPowerUps[num7];
						float num8 = float.Parse(this.CoinsPowerUps[num7]) - float.Parse(this.CoinsPowerUps[num7]) * float.Parse(s7);
						this.CoinsPowerUps[num7] = num8.ToString();
						this.PowerUpsObjects[num7].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMesh>().text = num8.ToString();
						this.PowerUpsObjects[num7].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
						this.PowerUpsObjects[num7].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMesh>().color = ShopManagerFull.PopustBoja;
					}
					this.PowerUpsObjects[num7].Find("Text/ime").GetComponent<TextMesh>().text = this.ImenaPowerUps[num7];
					this.PowerUpsObjects[num7].Find("Text/ime").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
				}
			}
			StagesParser.cost_doublecoins = int.Parse(this.CoinsPowerUps[0]);
			StagesParser.cost_magnet = int.Parse(this.CoinsPowerUps[1]);
			StagesParser.cost_shield = int.Parse(this.CoinsPowerUps[2]);
			if (source.ElementAt(this.BrojItemaShopHats + this.BrojItemaShopShirts + this.BrojItemaShopBackPack + 3).Attribute("kategorija").Value == "Banana")
			{
				this.ImeBanana = source.ElementAt(this.BrojItemaShopHats + this.BrojItemaShopShirts + this.BrojItemaShopBackPack + 3).Value;
				this.cenaBanana = source.ElementAt(this.BrojItemaShopHats + this.BrojItemaShopShirts + this.BrojItemaShopBackPack + 3).Attribute("coins").Value;
				this.PopustBanana = source.ElementAt(this.BrojItemaShopHats + this.BrojItemaShopShirts + this.BrojItemaShopBackPack + 3).Attribute("popust").Value;
				string s8 = "0." + this.PopustBanana;
				float num9 = float.Parse(this.cenaBanana) - float.Parse(this.cenaBanana) * float.Parse(s8);
				this.cenaBanana = num9.ToString();
				StagesParser.bananaCost = (int)num9;
				if (int.Parse(this.PopustBanana) > 0)
				{
					this.ShopBanana.transform.Find("Popust").gameObject.SetActive(true);
					this.ShopBanana.transform.Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMesh>().color = ShopManagerFull.PopustBoja;
					this.ShopBanana.transform.Find("Popust/Text/Number").GetComponent<TextMesh>().text = this.PopustBanana + "%";
					this.ShopBanana.transform.Find("Popust/Text/Number/Number Shadow").GetComponent<TextMesh>().text = this.PopustBanana + "%";
				}
				else
				{
					this.ShopBanana.transform.Find("Popust").gameObject.SetActive(false);
				}
				this.ShopBanana.transform.Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMesh>().text = this.cenaBanana;
				this.ShopBanana.transform.Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
				this.ShopBanana.transform.Find("Text/Banana").GetComponent<TextMesh>().text = this.ImeBanana;
				this.ShopBanana.transform.Find("Text/Banana").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
			}
			if (this.ImaNovihKapa)
			{
				this.ZidFooter.transform.Find("1HatsShopTab/Znak Uzvika telo").gameObject.SetActive(true);
			}
			else
			{
				this.ZidFooter.transform.Find("1HatsShopTab/Znak Uzvika telo").gameObject.SetActive(false);
			}
			if (this.ImaNovihMajica)
			{
				this.ZidFooter.transform.Find("2TShirtsShopTab/Znak Uzvika telo").gameObject.SetActive(true);
			}
			else
			{
				this.ZidFooter.transform.Find("2TShirtsShopTab/Znak Uzvika telo").gameObject.SetActive(false);
			}
			if (this.ImaNovihRanceva)
			{
				this.ZidFooter.transform.Find("3BackPackShopTab/Znak Uzvika telo").gameObject.SetActive(true);
			}
			else
			{
				this.ZidFooter.transform.Find("3BackPackShopTab/Znak Uzvika telo").gameObject.SetActive(false);
			}
			if (this.ImaNovihKapa | this.ImaNovihMajica | this.ImaNovihRanceva)
			{
				this.Custumization.transform.Find("Znak Uzvika telo").gameObject.SetActive(true);
				this.Custumization.GetComponent<Animation>().PlayQueued("Button Customization Idle", 0);
			}
			else
			{
				this.Custumization.transform.Find("Znak Uzvika telo").gameObject.SetActive(false);
			}
		}
		yield break;
	}

	// Token: 0x06002702 RID: 9986 RVA: 0x0011AA84 File Offset: 0x00118C84
	public void SviItemiInvetory()
	{
		ShopManagerFull.SveStvariZaOblacenjeHats.Clear();
		ShopManagerFull.SveStvariZaOblacenjeShirts.Clear();
		ShopManagerFull.SveStvariZaOblacenjeBackPack.Clear();
		this.Hats = StagesParser.svekupovineGlava.Split(new char[]
		{
			'#'
		});
		this.Shirts = StagesParser.svekupovineMajica.Split(new char[]
		{
			'#'
		});
		this.BackPacks = StagesParser.svekupovineLedja.Split(new char[]
		{
			'#'
		});
		for (int i = 0; i < this.BrojItemaShopHats; i++)
		{
			if (this.Hats.Length - 1 > i)
			{
				ShopManagerFull.SveStvariZaOblacenjeHats.Add(int.Parse(this.Hats[i]));
			}
			else
			{
				ShopManagerFull.SveStvariZaOblacenjeHats.Add(0);
			}
			if (ShopManagerFull.SveStvariZaOblacenjeHats[i] == 0)
			{
				this.HatsObjects[i].Find("Stikla").gameObject.SetActive(false);
			}
			else
			{
				this.HatsObjects[i].Find("Stikla").gameObject.SetActive(true);
			}
		}
		for (int j = 0; j < this.BrojItemaShopShirts; j++)
		{
			if (this.Shirts.Length - 1 > j)
			{
				ShopManagerFull.SveStvariZaOblacenjeShirts.Add(int.Parse(this.Shirts[j]));
			}
			else
			{
				ShopManagerFull.SveStvariZaOblacenjeShirts.Add(0);
			}
			if (ShopManagerFull.SveStvariZaOblacenjeShirts[j] == 0)
			{
				this.ShirtsObjects[j].Find("Stikla").gameObject.SetActive(false);
			}
			else
			{
				this.ShirtsObjects[j].Find("Stikla").gameObject.SetActive(true);
			}
		}
		for (int k = 0; k < this.BrojItemaShopBackPack; k++)
		{
			if (this.BackPacks.Length - 1 > k)
			{
				ShopManagerFull.SveStvariZaOblacenjeBackPack.Add(int.Parse(this.BackPacks[k]));
			}
			else
			{
				ShopManagerFull.SveStvariZaOblacenjeBackPack.Add(0);
			}
			if (ShopManagerFull.SveStvariZaOblacenjeBackPack[k] == 0)
			{
				this.BackPacksObjects[k].Find("Stikla").gameObject.SetActive(false);
			}
			else
			{
				this.BackPacksObjects[k].Find("Stikla").gameObject.SetActive(true);
			}
		}
		ShopManagerFull.ShopInicijalizovan = true;
		this.PokreniShop();
		this.Hats = null;
		this.Shirts = null;
		this.BackPacks = null;
	}

	// Token: 0x06002703 RID: 9987 RVA: 0x0011ACCB File Offset: 0x00118ECB
	private IEnumerator PokreniInicijalizacijuShopa()
	{
		if (FacebookManager.KorisnikoviPodaciSpremni)
		{
			base.StartCoroutine(this.ParsirajImenaItemaIzRadnje());
		}
		else
		{
			FacebookManager.UserCoins = StagesParser.currentMoney;
			FacebookManager.UserScore = StagesParser.currentPoints;
			FacebookManager.UserLanguage = LanguageManager.chosenLanguage;
			FacebookManager.UserBanana = StagesParser.currentBananas;
			FacebookManager.UserPowerMagnet = StagesParser.powerup_magnets;
			FacebookManager.UserPowerShield = StagesParser.powerup_shields;
			FacebookManager.UserPowerDoubleCoins = StagesParser.powerup_doublecoins;
			FacebookManager.UserSveKupovineHats = StagesParser.svekupovineGlava;
			FacebookManager.UserSveKupovineShirts = StagesParser.svekupovineMajica;
			FacebookManager.UserSveKupovineBackPacks = StagesParser.svekupovineLedja;
			FacebookManager.GlavaItem = StagesParser.glava;
			FacebookManager.TeloItem = StagesParser.majica;
			FacebookManager.LedjaItem = StagesParser.ledja;
			FacebookManager.Usi = StagesParser.imaUsi;
			FacebookManager.Kosa = StagesParser.imaKosu;
			FacebookManager.KorisnikoviPodaciSpremni = true;
			while (!FacebookManager.KorisnikoviPodaciSpremni)
			{
				yield return null;
			}
			base.StartCoroutine(this.PokreniInicijalizacijuShopa());
		}
		yield break;
	}

	// Token: 0x06002704 RID: 9988 RVA: 0x0011ACDA File Offset: 0x00118EDA
	public void PokreniShop()
	{
		if (!ShopManagerFull.ShopInicijalizovan)
		{
			base.StartCoroutine(this.PokreniInicijalizacijuShopa());
		}
	}

	// Token: 0x06002705 RID: 9989 RVA: 0x0011ACF0 File Offset: 0x00118EF0
	public void SkloniShop()
	{
		if (Application.loadedLevel == 1)
		{
			this.MajmunBobo.transform.Find("PrinceGorilla").GetComponent<Animator>().Play("Idle Main Screen");
			this.MajmunBobo.transform.Find("ButterflyHolder").gameObject.SetActive(true);
			if (ShopManagerFull.AktivanRanac == 0)
			{
				this.MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + ShopManagerFull.AktivanRanac).transform.GetChild(0).GetComponent<MeshFilter>().mesh = GameObject.Find("RefZaSedenje_AndjeoskaKrila").GetComponent<MeshFilter>().mesh;
			}
			else if (ShopManagerFull.AktivanRanac == 5)
			{
				this.MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + ShopManagerFull.AktivanRanac).transform.GetChild(0).GetComponent<MeshFilter>().mesh = GameObject.Find("RefZaSedenje_SlepiMisKrila").GetComponent<MeshFilter>().mesh;
			}
		}
		ShopManagerFull.otvorenShop = false;
		this.OcistiPreview();
		this.DeaktivirajCustomization();
		this.MajmunBobo.transform.position = this.MainScenaPozicija;
		this.MajmunBobo.transform.Find("PrinceGorilla").rotation = Quaternion.Euler(new Vector3(0f, 104f, 0f));
		this.DeaktivirajFreeCoins();
		this.DeaktivirajPowerUps();
		this.DeaktivirajShopTab();
		if (PlayerPrefs.HasKey("OtkljucaniItemi"))
		{
			this.StariBrojOtkljucanihItema = PlayerPrefs.GetString("OtkljucaniItemi");
		}
		else
		{
			this.StariBrojOtkljucanihItema = "0#0#0";
		}
		this.StariBrojOtkljucanihItemaNiz = this.StariBrojOtkljucanihItema.Split(new char[]
		{
			'#'
		});
		ShopManagerFull.StariBrojOtkljucanihKapa = int.Parse(this.StariBrojOtkljucanihItemaNiz[0]);
		ShopManagerFull.StariBrojOtkljucanihMajici = int.Parse(this.StariBrojOtkljucanihItemaNiz[1]);
		ShopManagerFull.StariBrojOtkljucanihRanceva = int.Parse(this.StariBrojOtkljucanihItemaNiz[2]);
		if (this.ImaNovihKapa)
		{
			ShopManagerFull.BrojOtkljucanihKapa = Mathf.FloorToInt((float)this.BrojItemaShopHats * this.ProcenatOtkljucan) - 1;
		}
		else
		{
			ShopManagerFull.BrojOtkljucanihKapa = ShopManagerFull.StariBrojOtkljucanihKapa;
		}
		if (this.ImaNovihMajica)
		{
			ShopManagerFull.BrojOtkljucanihMajici = Mathf.FloorToInt((float)this.BrojItemaShopShirts * this.ProcenatOtkljucan) - 1;
		}
		else
		{
			ShopManagerFull.BrojOtkljucanihMajici = ShopManagerFull.StariBrojOtkljucanihMajici;
		}
		if (this.ImaNovihRanceva)
		{
			ShopManagerFull.BrojOtkljucanihRanceva = Mathf.FloorToInt((float)this.BrojItemaShopBackPack * this.ProcenatOtkljucan) - 1;
		}
		else
		{
			ShopManagerFull.BrojOtkljucanihRanceva = ShopManagerFull.StariBrojOtkljucanihRanceva;
		}
		if (!this.ImaNovihKapa && !this.ImaNovihMajica && !this.ImaNovihRanceva)
		{
			this.StariBrojOtkljucanihItema = string.Concat(new object[]
			{
				ShopManagerFull.BrojOtkljucanihKapa,
				"#",
				ShopManagerFull.BrojOtkljucanihMajici,
				"#",
				ShopManagerFull.BrojOtkljucanihRanceva
			});
			PlayerPrefs.SetString("OtkljucaniItemi", this.StariBrojOtkljucanihItema);
			PlayerPrefs.Save();
		}
		this.ProveriStanjeCelogShopa();
		GameObject.Find("Shop").GetComponent<Animation>().Play("MeniOdlazak");
		if (ShopManagerFull.AktivanTab == 1)
		{
			GameObject.Find("ButtonFreeCoins").GetComponent<SpriteRenderer>().sprite = GameObject.Find("ShopTab").GetComponent<SpriteRenderer>().sprite;
		}
		else if (ShopManagerFull.AktivanTab == 2)
		{
			GameObject.Find("ButtonShop").GetComponent<SpriteRenderer>().sprite = GameObject.Find("ShopTab").GetComponent<SpriteRenderer>().sprite;
		}
		else if (ShopManagerFull.AktivanTab == 3)
		{
			GameObject.Find("ButtonCustomize").GetComponent<SpriteRenderer>().sprite = GameObject.Find("ShopTab").GetComponent<SpriteRenderer>().sprite;
		}
		else if (ShopManagerFull.AktivanTab == 4)
		{
			GameObject.Find("ButtonPowerUps").GetComponent<SpriteRenderer>().sprite = GameObject.Find("ShopTab").GetComponent<SpriteRenderer>().sprite;
		}
		ShopManagerFull.AktivanTab = 0;
		ShopManagerFull.AktivanItemSesir = 998;
		ShopManagerFull.AktivanItemMajica = 998;
		ShopManagerFull.AktivanItemRanac = 998;
	}

	// Token: 0x06002706 RID: 9990 RVA: 0x0011B0EC File Offset: 0x001192EC
	public void PozoviTab(int RedniBrojTaba)
	{
		if (this.mozeDaOtvoriSledeciTab && this.kliknuoJednomNaTab)
		{
			this.mozeDaOtvoriSledeciTab = false;
			this.kliknuoJednomNaTab = false;
			if (RedniBrojTaba == 3)
			{
				base.Invoke("MozeDaKliknePonovoNaTab", 1.5f);
			}
			else
			{
				base.Invoke("MozeDaKliknePonovoNaTab", 0.75f);
			}
			if (StagesParser.otvaraoShopNekad == 0)
			{
				StagesParser.otvaraoShopNekad = 1;
				PlayerPrefs.SetString("OdgledaoTutorial", StagesParser.odgledaoTutorial.ToString() + "#" + StagesParser.otvaraoShopNekad.ToString());
				PlayerPrefs.Save();
			}
			ShopManagerFull.otvorenShop = true;
			this.CustomizationShirts.SetActive(false);
			this.CustomizationBackPack.SetActive(false);
			this.CoinsNumber.transform.Find("Coins Number").GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
			this.CoinsNumber.transform.Find("Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
			if (ShopManagerFull.AktivanTab == RedniBrojTaba)
			{
				return;
			}
			if (ShopManagerFull.AktivanTab == 1)
			{
				this.DeaktivirajFreeCoins();
				GameObject.Find("ButtonFreeCoins").GetComponent<SpriteRenderer>().sprite = GameObject.Find("ShopTab").GetComponent<SpriteRenderer>().sprite;
				GameObject.Find("Shop/1 Free Coins").GetComponent<Animation>().Play("TabOdlazak");
			}
			else if (ShopManagerFull.AktivanTab == 2)
			{
				this.DeaktivirajShopTab();
				GameObject.Find("ButtonShop").GetComponent<SpriteRenderer>().sprite = GameObject.Find("ShopTab").GetComponent<SpriteRenderer>().sprite;
				GameObject.Find("Shop/2 Shop - BANANA").GetComponent<Animation>().Play("TabOdlazak");
			}
			else if (ShopManagerFull.AktivanTab == 3)
			{
				this.DeaktivirajCustomization();
				GameObject.Find("ButtonCustomize").GetComponent<SpriteRenderer>().sprite = GameObject.Find("ShopTab").GetComponent<SpriteRenderer>().sprite;
				GameObject.Find("Shop/3 Customize").GetComponent<Animation>().Play("TabOdlazak");
				this.MajmunBobo.transform.position = this.MainScenaPozicija;
			}
			else if (ShopManagerFull.AktivanTab == 4)
			{
				this.DeaktivirajPowerUps();
				GameObject.Find("ButtonPowerUps").GetComponent<SpriteRenderer>().sprite = GameObject.Find("ShopTab").GetComponent<SpriteRenderer>().sprite;
				GameObject.Find("Shop/4 Power-Ups").GetComponent<Animation>().Play("TabOdlazak");
			}
			ShopManagerFull.AktivanTab = RedniBrojTaba;
			if (ShopManagerFull.AktivanTab == 1)
			{
				if (PlayerPrefs.HasKey("LikeBananaIsland"))
				{
					ShopManagerFull.ShopObject.transform.Find("1 Free Coins/Free Coins Tabovi/ShopFCBILikePage").GetComponent<Collider>().enabled = false;
					ShopManagerFull.ShopObject.transform.Find("1 Free Coins/Free Coins Tabovi/ShopFCBILikePage").GetComponent<Renderer>().material.color = new Color(0.58f, 0.58f, 0.58f);
					ShopManagerFull.ShopObject.transform.Find("1 Free Coins/Free Coins Tabovi/ShopFCBILikePage/Like BananaIsland FC").GetComponent<Renderer>().material.color = new Color(0.58f, 0.58f, 0.58f);
				}
				if (PlayerPrefs.HasKey("LikeWebelinx"))
				{
					ShopManagerFull.ShopObject.transform.Find("1 Free Coins/Free Coins Tabovi/ShopFCWLLikePage").GetComponent<Collider>().enabled = false;
					ShopManagerFull.ShopObject.transform.Find("1 Free Coins/Free Coins Tabovi/ShopFCWLLikePage").GetComponent<Renderer>().material.color = new Color(0.58f, 0.58f, 0.58f);
					ShopManagerFull.ShopObject.transform.Find("1 Free Coins/Free Coins Tabovi/ShopFCWLLikePage/Like Webelinx FC").GetComponent<Renderer>().material.color = new Color(0.58f, 0.58f, 0.58f);
				}
				GameObject.Find("ButtonFreeCoins").GetComponent<SpriteRenderer>().sprite = GameObject.Find("ShopTabSelected").GetComponent<SpriteRenderer>().sprite;
				GameObject.Find("Shop/1 Free Coins").GetComponent<Animation>().Play("TabDolazak");
				this.AktivirajFreeCoins();
				return;
			}
			if (ShopManagerFull.AktivanTab == 2)
			{
				this.AktivirajShopTab();
				GameObject.Find("ButtonShop").GetComponent<SpriteRenderer>().sprite = GameObject.Find("ShopTabSelected").GetComponent<SpriteRenderer>().sprite;
				GameObject.Find("Shop/2 Shop - BANANA").GetComponent<Animation>().Play("TabDolazak");
				return;
			}
			if (ShopManagerFull.AktivanTab == 3)
			{
				if (ShopManagerFull.AktivanCustomizationTab == 1)
				{
					GameObject.Find("1HatsShopTab").GetComponent<SpriteRenderer>().color = Color.green;
					GameObject.Find("2TShirtsShopTab").GetComponent<SpriteRenderer>().color = ShopManagerFull.KakiBoja;
					GameObject.Find("3BackPackShopTab").GetComponent<SpriteRenderer>().color = ShopManagerFull.KakiBoja;
					this.ImaNovihKapa = false;
					this.CustomizationHats.SetActive(true);
					this.CustomizationShirts.SetActive(false);
					this.CustomizationBackPack.SetActive(false);
				}
				else if (ShopManagerFull.AktivanCustomizationTab == 2)
				{
					GameObject.Find("1HatsShopTab").GetComponent<SpriteRenderer>().color = ShopManagerFull.KakiBoja;
					GameObject.Find("2TShirtsShopTab").GetComponent<SpriteRenderer>().color = Color.green;
					GameObject.Find("3BackPackShopTab").GetComponent<SpriteRenderer>().color = ShopManagerFull.KakiBoja;
					this.ImaNovihMajica = false;
					this.CustomizationHats.SetActive(false);
					this.CustomizationShirts.SetActive(true);
					this.CustomizationBackPack.SetActive(false);
				}
				else if (ShopManagerFull.AktivanCustomizationTab == 3)
				{
					GameObject.Find("1HatsShopTab").GetComponent<SpriteRenderer>().color = ShopManagerFull.KakiBoja;
					GameObject.Find("2TShirtsShopTab").GetComponent<SpriteRenderer>().color = ShopManagerFull.KakiBoja;
					GameObject.Find("3BackPackShopTab").GetComponent<SpriteRenderer>().color = Color.green;
					this.ImaNovihRanceva = false;
					this.CustomizationHats.SetActive(false);
					this.CustomizationShirts.SetActive(false);
					this.CustomizationBackPack.SetActive(true);
				}
				GameObject.Find("ButtonCustomize").GetComponent<SpriteRenderer>().sprite = GameObject.Find("ShopTabSelected").GetComponent<SpriteRenderer>().sprite;
				GameObject.Find("Shop/3 Customize").GetComponent<Animation>().Play("TabDolazak");
				base.Invoke("AktivirajCustomization", 0.4f);
				this.MajmunBobo.transform.Find("PrinceGorilla").GetComponent<Animator>().Play("Povlacenje");
				this.MajmunBobo.transform.Find("ButterflyHolder").gameObject.SetActive(false);
				return;
			}
			if (ShopManagerFull.AktivanTab == 4)
			{
				this.AktivirajPowerUps();
				GameObject.Find("ButtonPowerUps").GetComponent<SpriteRenderer>().sprite = GameObject.Find("ShopTabSelected").GetComponent<SpriteRenderer>().sprite;
				GameObject.Find("Shop/4 Power-Ups").GetComponent<Animation>().Play("TabDolazak");
				return;
			}
		}
		else if (!this.mozeDaOtvoriSledeciTab && this.kliknuoJednomNaTab)
		{
			this.kliknuoJednomNaTab = false;
		}
	}

	// Token: 0x06002707 RID: 9991 RVA: 0x0011B7C6 File Offset: 0x001199C6
	private void MozeDaKliknePonovoNaTab()
	{
		this.mozeDaOtvoriSledeciTab = true;
		this.kliknuoJednomNaTab = true;
	}

	// Token: 0x06002708 RID: 9992 RVA: 0x0011B7D6 File Offset: 0x001199D6
	public void PozoviCustomizationTab(int RedniBrojCustomizationTaba)
	{
		base.StopCoroutine("CustomizationTab");
		base.StartCoroutine("CustomizationTab", RedniBrojCustomizationTaba);
	}

	// Token: 0x06002709 RID: 9993 RVA: 0x0011B7F5 File Offset: 0x001199F5
	public IEnumerator CustomizationTab(int RedniBrojCustomizationTaba1)
	{
		if (ShopManagerFull.AktivanCustomizationTab != RedniBrojCustomizationTaba1)
		{
			if (ShopManagerFull.AktivanCustomizationTab == 1)
			{
				GameObject.Find("1HatsShopTab").GetComponent<SpriteRenderer>().color = ShopManagerFull.KakiBoja;
				this.CustomizationHats.SetActive(false);
				this.ImaNovihKapa = false;
			}
			else if (ShopManagerFull.AktivanCustomizationTab == 2)
			{
				GameObject.Find("2TShirtsShopTab").GetComponent<SpriteRenderer>().color = ShopManagerFull.KakiBoja;
				this.CustomizationShirts.SetActive(false);
				this.ImaNovihMajica = false;
			}
			else if (ShopManagerFull.AktivanCustomizationTab == 3)
			{
				GameObject.Find("3BackPackShopTab").GetComponent<SpriteRenderer>().color = ShopManagerFull.KakiBoja;
				this.CustomizationBackPack.SetActive(false);
				this.ImaNovihRanceva = false;
			}
			yield return new WaitForSeconds(0.15f);
			ShopManagerFull.AktivanCustomizationTab = RedniBrojCustomizationTaba1;
			if (ShopManagerFull.AktivanCustomizationTab == 1)
			{
				this.TrenutniSelektovanSesir = 999;
				GameObject.Find("1HatsShopTab").GetComponent<SpriteRenderer>().color = Color.green;
				this.CustomizationHats.SetActive(true);
				this.ImaNovihKapa = false;
				Quaternion a = Quaternion.Euler(new Vector3(0f, 90f, 0f));
				float t = 0f;
				while (t < 0.3f)
				{
					this.MajmunBobo.transform.Find("PrinceGorilla").rotation = Quaternion.Lerp(this.MajmunBobo.transform.Find("PrinceGorilla").rotation, a, t);
					t += Time.deltaTime / 2f;
					yield return null;
				}
				a = default(Quaternion);
			}
			else if (ShopManagerFull.AktivanCustomizationTab == 2)
			{
				this.TrenutnoSelektovanaMajica = 999;
				GameObject.Find("2TShirtsShopTab").GetComponent<SpriteRenderer>().color = Color.green;
				this.CustomizationShirts.SetActive(true);
				this.ImaNovihMajica = false;
				Quaternion a = Quaternion.Euler(new Vector3(0f, 150f, 0f));
				float t = 0f;
				while (t < 0.3f)
				{
					this.MajmunBobo.transform.Find("PrinceGorilla").rotation = Quaternion.Lerp(this.MajmunBobo.transform.Find("PrinceGorilla").rotation, a, t);
					t += Time.deltaTime / 2f;
					yield return null;
				}
				a = default(Quaternion);
			}
			else if (ShopManagerFull.AktivanCustomizationTab == 3)
			{
				this.TrenutnoSelektovanRanac = 999;
				GameObject.Find("3BackPackShopTab").GetComponent<SpriteRenderer>().color = Color.green;
				this.CustomizationBackPack.SetActive(true);
				this.ImaNovihRanceva = false;
				Quaternion a = Quaternion.Euler(new Vector3(0f, 35f, 0f));
				float t = 0f;
				while (t < 0.3f)
				{
					this.MajmunBobo.transform.Find("PrinceGorilla").rotation = Quaternion.Lerp(this.MajmunBobo.transform.Find("PrinceGorilla").rotation, a, t);
					t += Time.deltaTime / 2f;
					yield return null;
				}
				a = default(Quaternion);
			}
		}
		else if (ShopManagerFull.AktivanCustomizationTab == 1)
		{
			this.TrenutniSelektovanSesir = 999;
			GameObject.Find("1HatsShopTab").GetComponent<SpriteRenderer>().color = Color.green;
			this.CustomizationHats.SetActive(true);
		}
		else if (ShopManagerFull.AktivanCustomizationTab == 2)
		{
			this.TrenutnoSelektovanaMajica = 999;
			GameObject.Find("2TShirtsShopTab").GetComponent<SpriteRenderer>().color = Color.green;
			this.CustomizationShirts.SetActive(true);
		}
		else if (ShopManagerFull.AktivanCustomizationTab == 3)
		{
			this.TrenutnoSelektovanRanac = 999;
			GameObject.Find("3BackPackShopTab").GetComponent<SpriteRenderer>().color = Color.green;
			this.CustomizationBackPack.SetActive(true);
		}
		this.AktivirajCustomization();
		yield break;
	}

	// Token: 0x0600270A RID: 9994 RVA: 0x0011B80C File Offset: 0x00119A0C
	public void AktivirajCustomization()
	{
		if (ShopManagerFull.AktivanCustomizationTab == 1)
		{
			ObjCustomizationShirts.CustomizationShirts = false;
			SwipeControlCustomizationShirts.controlEnabled = false;
			ObjCustomizationBackPacks.CustomizationBackPacks = false;
			SwipeControlCustomizationBackPacks.controlEnabled = false;
			ObjCustomizationHats.CustomizationHats = true;
			SwipeControlCustomizationHats.controlEnabled = true;
		}
		else if (ShopManagerFull.AktivanCustomizationTab == 2)
		{
			ObjCustomizationHats.CustomizationHats = false;
			SwipeControlCustomizationHats.controlEnabled = false;
			ObjCustomizationShirts.CustomizationShirts = true;
			SwipeControlCustomizationShirts.controlEnabled = true;
			ObjCustomizationBackPacks.CustomizationBackPacks = false;
			SwipeControlCustomizationBackPacks.controlEnabled = false;
		}
		else if (ShopManagerFull.AktivanCustomizationTab == 3)
		{
			ObjCustomizationHats.CustomizationHats = false;
			SwipeControlCustomizationHats.controlEnabled = false;
			ObjCustomizationShirts.CustomizationShirts = false;
			SwipeControlCustomizationShirts.controlEnabled = false;
			ObjCustomizationBackPacks.CustomizationBackPacks = true;
			SwipeControlCustomizationBackPacks.controlEnabled = true;
		}
		this.MajmunBobo.transform.position = this.ShopCustomizationPozicija;
	}

	// Token: 0x0600270B RID: 9995 RVA: 0x0011B8B7 File Offset: 0x00119AB7
	public void DeaktivirajCustomization()
	{
		if (ShopManagerFull.AktivanCustomizationTab == 1)
		{
			ObjCustomizationHats.CustomizationHats = false;
			SwipeControlCustomizationHats.controlEnabled = false;
			return;
		}
		if (ShopManagerFull.AktivanCustomizationTab == 2)
		{
			ObjCustomizationShirts.CustomizationShirts = false;
			SwipeControlCustomizationShirts.controlEnabled = false;
			return;
		}
		if (ShopManagerFull.AktivanCustomizationTab == 3)
		{
			ObjCustomizationBackPacks.CustomizationBackPacks = false;
			SwipeControlCustomizationBackPacks.controlEnabled = false;
		}
	}

	// Token: 0x0600270C RID: 9996 RVA: 0x0011B8F7 File Offset: 0x00119AF7
	public void AktivirajFreeCoins()
	{
		ObjFreeCoins.FreeCoins = true;
		SwipeControlFreeCoins.controlEnabled = true;
	}

	// Token: 0x0600270D RID: 9997 RVA: 0x0011B905 File Offset: 0x00119B05
	public void DeaktivirajFreeCoins()
	{
		ObjFreeCoins.FreeCoins = false;
		SwipeControlFreeCoins.controlEnabled = false;
	}

	// Token: 0x0600270E RID: 9998 RVA: 0x0011B913 File Offset: 0x00119B13
	public void AktivirajPowerUps()
	{
		ObjPowerUps.PowerUps = true;
		SwipeControlPowerUps.controlEnabled = true;
	}

	// Token: 0x0600270F RID: 9999 RVA: 0x0011B921 File Offset: 0x00119B21
	public void DeaktivirajPowerUps()
	{
		ObjPowerUps.PowerUps = false;
		SwipeControlPowerUps.controlEnabled = false;
	}

	// Token: 0x06002710 RID: 10000 RVA: 0x0011B92F File Offset: 0x00119B2F
	public void AktivirajShopTab()
	{
		Debug.Log("Aktiviraj Shop pozvan");
		ObjShop.Shop = true;
		SwipeControlShop.controlEnabled = true;
	}

	// Token: 0x06002711 RID: 10001 RVA: 0x0011B947 File Offset: 0x00119B47
	public void DeaktivirajShopTab()
	{
		Debug.Log("Deaktiviraj Shop pozvan");
		ObjShop.Shop = false;
		SwipeControlShop.controlEnabled = false;
	}

	// Token: 0x06002712 RID: 10002 RVA: 0x0011B95F File Offset: 0x00119B5F
	public void ProveraTrenutnogItema(int TrenutniItem)
	{
		if (ShopManagerFull.AktivanCustomizationTab == 1)
		{
			this.ProveriStanjeSesira(TrenutniItem);
			return;
		}
		if (ShopManagerFull.AktivanCustomizationTab == 2)
		{
			this.ProveriStanjeMajica(TrenutniItem);
			return;
		}
		if (ShopManagerFull.AktivanCustomizationTab == 3)
		{
			this.ProveriStanjeRanca(TrenutniItem);
		}
	}

	// Token: 0x06002713 RID: 10003 RVA: 0x0011B990 File Offset: 0x00119B90
	public void ProveriStanjeSesira(int TrenutniItem)
	{
		if (TrenutniItem != this.TrenutniSelektovanSesir)
		{
			this.TrenutniSelektovanSesir = TrenutniItem;
			if (TrenutniItem < ShopManagerFull.SveStvariZaOblacenjeHats.Count)
			{
				if (ShopManagerFull.SveStvariZaOblacenjeHats[this.TrenutniSelektovanSesir] == 1)
				{
					this.ButtonShopSprite.GetComponent<SpriteRenderer>().color = Color.white;
					ShopManagerFull.PreviewState = false;
					if (ShopManagerFull.AktivanSesir == TrenutniItem)
					{
						this.ButtonShop.GetComponent<TextMesh>().text = LanguageManager.Unequip;
						ShopManagerFull.BuyButtonState = 3;
					}
					else
					{
						this.ButtonShop.GetComponent<TextMesh>().text = LanguageManager.Equip;
						ShopManagerFull.BuyButtonState = 2;
					}
					this.ButtonShop.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
					return;
				}
				ShopManagerFull.PreviewState = true;
				this.ButtonShop.GetComponent<TextMesh>().text = LanguageManager.Buy;
				this.ButtonShop.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
				int num = int.Parse(this.CoinsHats[TrenutniItem]);
				if (this.ZakljucaniHats[this.TrenutniSelektovanSesir] == 1)
				{
					if (StagesParser.currentMoney < num)
					{
						this.ButtonShopSprite.GetComponent<SpriteRenderer>().color = new Color(0.41176f, 0.41176f, 0.41176f);
						ShopManagerFull.BuyButtonState = 1;
						return;
					}
					this.ButtonShopSprite.GetComponent<SpriteRenderer>().color = Color.white;
					ShopManagerFull.BuyButtonState = 0;
					return;
				}
				else
				{
					ShopManagerFull.BuyButtonState = 4;
					this.ButtonShopSprite.GetComponent<SpriteRenderer>().color = new Color(0.41176f, 0.41176f, 0.41176f);
				}
			}
		}
	}

	// Token: 0x06002714 RID: 10004 RVA: 0x0011BB10 File Offset: 0x00119D10
	public void ProveriStanjeMajica(int TrenutniItem)
	{
		if (TrenutniItem != this.TrenutnoSelektovanaMajica)
		{
			this.TrenutnoSelektovanaMajica = TrenutniItem;
			if (TrenutniItem < ShopManagerFull.SveStvariZaOblacenjeShirts.Count)
			{
				if (ShopManagerFull.SveStvariZaOblacenjeShirts[TrenutniItem] == 1)
				{
					this.ButtonShopSprite.GetComponent<SpriteRenderer>().color = Color.white;
					ShopManagerFull.PreviewState = false;
					if (ShopManagerFull.AktivnaMajica == TrenutniItem)
					{
						this.ButtonShop.GetComponent<TextMesh>().text = LanguageManager.Unequip;
						ShopManagerFull.BuyButtonState = 3;
					}
					else
					{
						this.ButtonShop.GetComponent<TextMesh>().text = LanguageManager.Equip;
						ShopManagerFull.BuyButtonState = 2;
					}
					this.ButtonShop.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
					return;
				}
				ShopManagerFull.PreviewState = true;
				this.ButtonShop.GetComponent<TextMesh>().text = LanguageManager.Buy;
				this.ButtonShop.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
				int num = int.Parse(this.CoinsShirts[TrenutniItem]);
				if (this.ZakljucaniShirts[this.TrenutnoSelektovanaMajica] == 1)
				{
					if (StagesParser.currentMoney < num)
					{
						this.ButtonShopSprite.GetComponent<SpriteRenderer>().color = new Color(0.41176f, 0.41176f, 0.41176f);
						ShopManagerFull.BuyButtonState = 1;
						return;
					}
					this.ButtonShopSprite.GetComponent<SpriteRenderer>().color = Color.white;
					ShopManagerFull.BuyButtonState = 0;
					return;
				}
				else
				{
					ShopManagerFull.BuyButtonState = 4;
					this.ButtonShopSprite.GetComponent<SpriteRenderer>().color = new Color(0.41176f, 0.41176f, 0.41176f);
				}
			}
		}
	}

	// Token: 0x06002715 RID: 10005 RVA: 0x0011BC8C File Offset: 0x00119E8C
	public void ProveriStanjeRanca(int TrenutniItem)
	{
		if (TrenutniItem != this.TrenutnoSelektovanRanac)
		{
			this.TrenutnoSelektovanRanac = TrenutniItem;
			if (TrenutniItem < ShopManagerFull.SveStvariZaOblacenjeBackPack.Count)
			{
				if (ShopManagerFull.SveStvariZaOblacenjeBackPack[TrenutniItem] == 1)
				{
					this.ButtonShopSprite.GetComponent<SpriteRenderer>().color = Color.white;
					ShopManagerFull.PreviewState = false;
					if (ShopManagerFull.AktivanRanac == TrenutniItem)
					{
						this.ButtonShop.GetComponent<TextMesh>().text = LanguageManager.Unequip;
						ShopManagerFull.BuyButtonState = 3;
					}
					else
					{
						this.ButtonShop.GetComponent<TextMesh>().text = LanguageManager.Equip;
						ShopManagerFull.BuyButtonState = 2;
					}
					this.ButtonShop.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
					return;
				}
				ShopManagerFull.PreviewState = true;
				this.ButtonShop.GetComponent<TextMesh>().text = LanguageManager.Buy;
				this.ButtonShop.GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
				int num = int.Parse(this.CoinsBackPacks[TrenutniItem]);
				if (this.ZakljucaniBackPacks[this.TrenutnoSelektovanRanac] == 1)
				{
					if (StagesParser.currentMoney < num)
					{
						this.ButtonShopSprite.GetComponent<SpriteRenderer>().color = new Color(0.41176f, 0.41176f, 0.41176f);
						ShopManagerFull.BuyButtonState = 1;
						return;
					}
					this.ButtonShopSprite.GetComponent<SpriteRenderer>().color = Color.white;
					ShopManagerFull.BuyButtonState = 0;
					return;
				}
				else
				{
					ShopManagerFull.BuyButtonState = 4;
					this.ButtonShopSprite.GetComponent<SpriteRenderer>().color = new Color(0.41176f, 0.41176f, 0.41176f);
				}
			}
		}
	}

	// Token: 0x06002716 RID: 10006 RVA: 0x0011BE08 File Offset: 0x0011A008
	public void ProveriStanjeCelogShopa()
	{
		for (int i = 0; i < this.BrojItemaShopHats; i++)
		{
			if (this.ZakljucaniHats[i] == 1)
			{
				this.HatsObjects[i].Find("Zakkljucano").gameObject.SetActive(false);
			}
			else
			{
				this.HatsObjects[i].Find("Polje za unos COINA U shopu - Shop").gameObject.SetActive(false);
				this.HatsObjects[i].Find("Bedz - Popust").gameObject.SetActive(false);
			}
			if (ShopManagerFull.SveStvariZaOblacenjeHats[i] == 1)
			{
				this.HatsObjects[i].Find("Polje za unos COINA U shopu - Shop").gameObject.SetActive(false);
				this.HatsObjects[i].Find("Bedz - Popust").gameObject.SetActive(false);
			}
			else if (this.PopustHats[i] == "0")
			{
				this.HatsObjects[i].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMesh>().text = this.CoinsHats[i];
				this.HatsObjects[i].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
				if (i > ShopManagerFull.StariBrojOtkljucanihKapa & i <= ShopManagerFull.BrojOtkljucanihKapa)
				{
					this.ImaNovihKapa = true;
					this.HatsObjects[i].Find("Bedz - Popust/Text/Number").GetComponent<TextMesh>().text = LanguageManager.New;
					this.HatsObjects[i].Find("Bedz - Popust/Text/Number/Number Shadow").GetComponent<TextMesh>().text = LanguageManager.New;
				}
				else
				{
					this.HatsObjects[i].Find("Bedz - Popust").gameObject.SetActive(false);
				}
			}
			else
			{
				if (i > ShopManagerFull.StariBrojOtkljucanihKapa & i <= ShopManagerFull.BrojOtkljucanihKapa)
				{
					this.ImaNovihKapa = true;
					this.HatsObjects[i].Find("Bedz - Popust/Text/Number").GetComponent<TextMesh>().text = LanguageManager.New;
					this.HatsObjects[i].Find("Bedz - Popust/Text/Number/Number Shadow").GetComponent<TextMesh>().text = LanguageManager.New;
				}
				else
				{
					this.HatsObjects[i].Find("Bedz - Popust/Text/Number").GetComponent<TextMesh>().text = "-" + this.PopustHats[i] + "%";
					this.HatsObjects[i].Find("Bedz - Popust/Text/Number/Number Shadow").GetComponent<TextMesh>().text = this.PopustHats[i] + "%";
				}
				string s = "0." + this.PopustHats[i];
				if (this.ZakljucaniHats[i] == 1)
				{
					float num = float.Parse(this.CoinsHats[i]) / (1f - float.Parse(s));
					this.HatsObjects[i].Find("Polje za unos COINA U shopu - Shop_NoDiscount").gameObject.SetActive(true);
					this.HatsObjects[i].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number").GetComponent<TextMesh>().text = num.ToString();
					if (this.HatsObjects[i].parent.gameObject.activeSelf)
					{
						this.HatsObjects[i].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
					}
					this.HatsObjects[i].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMesh>().text = this.CoinsHats[i];
					this.HatsObjects[i].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
					this.HatsObjects[i].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMesh>().color = ShopManagerFull.PopustBoja;
				}
			}
			this.HatsObjects[i].Find("Text/ime").GetComponent<TextMesh>().text = this.ImenaHats[i];
			this.HatsObjects[i].Find("Text/ime").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		}
		for (int j = 0; j < this.BrojItemaShopShirts; j++)
		{
			if (this.ZakljucaniShirts[j] == 1)
			{
				this.ShirtsObjects[j].Find("Zakkljucano").gameObject.SetActive(false);
			}
			else
			{
				this.ShirtsObjects[j].Find("Polje za unos COINA U shopu - Shop").gameObject.SetActive(false);
				this.ShirtsObjects[j].Find("Bedz - Popust").gameObject.SetActive(false);
			}
			if (ShopManagerFull.SveStvariZaOblacenjeShirts[j] == 1)
			{
				this.ShirtsObjects[j].Find("Polje za unos COINA U shopu - Shop").gameObject.SetActive(false);
				this.ShirtsObjects[j].Find("Bedz - Popust").gameObject.SetActive(false);
			}
			else if (this.PopustShirts[j] == "0")
			{
				this.ShirtsObjects[j].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMesh>().text = this.CoinsShirts[j];
				this.ShirtsObjects[j].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
				if (j > ShopManagerFull.StariBrojOtkljucanihMajici & j <= ShopManagerFull.BrojOtkljucanihMajici)
				{
					this.ImaNovihMajica = true;
					this.ShirtsObjects[j].Find("Bedz - Popust/Text/Number").GetComponent<TextMesh>().text = LanguageManager.New;
					this.ShirtsObjects[j].Find("Bedz - Popust/Text/Number/Number Shadow").GetComponent<TextMesh>().text = LanguageManager.New;
				}
				else
				{
					this.ShirtsObjects[j].Find("Bedz - Popust").gameObject.SetActive(false);
				}
			}
			else
			{
				if (j > ShopManagerFull.StariBrojOtkljucanihMajici & j <= ShopManagerFull.BrojOtkljucanihMajici)
				{
					this.ImaNovihMajica = true;
					this.ShirtsObjects[j].Find("Bedz - Popust/Text/Number").GetComponent<TextMesh>().text = LanguageManager.New;
					this.ShirtsObjects[j].Find("Bedz - Popust/Text/Number/Number Shadow").GetComponent<TextMesh>().text = LanguageManager.New;
				}
				else
				{
					this.ShirtsObjects[j].Find("Bedz - Popust/Text/Number").GetComponent<TextMesh>().text = "-" + this.PopustShirts[j] + "%";
					this.ShirtsObjects[j].Find("Bedz - Popust/Text/Number/Number Shadow").GetComponent<TextMesh>().text = this.PopustShirts[j] + "%";
				}
				string s2 = "0." + this.PopustShirts[j];
				if (this.ZakljucaniShirts[j] == 1)
				{
					float num2 = float.Parse(this.CoinsShirts[j]) / (1f - float.Parse(s2));
					this.ShirtsObjects[j].Find("Polje za unos COINA U shopu - Shop_NoDiscount").gameObject.SetActive(true);
					this.ShirtsObjects[j].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number").GetComponent<TextMesh>().text = num2.ToString();
					if (this.ShirtsObjects[j].parent.gameObject.activeSelf)
					{
						this.ShirtsObjects[j].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
					}
					this.ShirtsObjects[j].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMesh>().text = this.CoinsShirts[j];
					this.ShirtsObjects[j].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
					this.ShirtsObjects[j].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMesh>().color = ShopManagerFull.PopustBoja;
				}
			}
			this.ShirtsObjects[j].Find("Text/ime").GetComponent<TextMesh>().text = this.ImenaShirts[j];
			this.ShirtsObjects[j].Find("Text/ime").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		}
		for (int k = 0; k < this.BrojItemaShopBackPack; k++)
		{
			if (this.ZakljucaniBackPacks[k] == 1)
			{
				this.BackPacksObjects[k].Find("Zakkljucano").gameObject.SetActive(false);
			}
			else
			{
				this.BackPacksObjects[k].Find("Polje za unos COINA U shopu - Shop").gameObject.SetActive(false);
				this.BackPacksObjects[k].Find("Bedz - Popust").gameObject.SetActive(false);
			}
			if (ShopManagerFull.SveStvariZaOblacenjeBackPack[k] == 1)
			{
				this.BackPacksObjects[k].Find("Polje za unos COINA U shopu - Shop").gameObject.SetActive(false);
				this.BackPacksObjects[k].Find("Bedz - Popust").gameObject.SetActive(false);
			}
			else if (this.PopustBackPacks[k] == "0")
			{
				this.BackPacksObjects[k].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMesh>().text = this.CoinsBackPacks[k];
				this.BackPacksObjects[k].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
				if (k > ShopManagerFull.StariBrojOtkljucanihRanceva & k <= ShopManagerFull.BrojOtkljucanihRanceva)
				{
					this.ImaNovihRanceva = true;
					this.BackPacksObjects[k].Find("Bedz - Popust/Text/Number").GetComponent<TextMesh>().text = LanguageManager.New;
					this.BackPacksObjects[k].Find("Bedz - Popust/Text/Number/Number Shadow").GetComponent<TextMesh>().text = LanguageManager.New;
				}
				else
				{
					this.BackPacksObjects[k].Find("Bedz - Popust").gameObject.SetActive(false);
				}
			}
			else
			{
				this.BackPacksObjects[k].Find("Bedz - Popust/Text/Number").GetComponent<TextMesh>().text = "-" + this.PopustBackPacks[k] + "%";
				this.BackPacksObjects[k].Find("Bedz - Popust/Text/Number/Number Shadow").GetComponent<TextMesh>().text = this.PopustBackPacks[k] + "%";
				if (k > ShopManagerFull.StariBrojOtkljucanihRanceva & k <= ShopManagerFull.BrojOtkljucanihRanceva)
				{
					this.ImaNovihRanceva = true;
					this.BackPacksObjects[k].Find("Bedz - Popust/Text/Number").GetComponent<TextMesh>().text = LanguageManager.New;
					this.BackPacksObjects[k].Find("Bedz - Popust/Text/Number/Number Shadow").GetComponent<TextMesh>().text = LanguageManager.New;
				}
				else
				{
					this.BackPacksObjects[k].Find("Bedz - Popust/Text/Number").GetComponent<TextMesh>().text = "-" + this.PopustBackPacks[k] + "%";
					this.BackPacksObjects[k].Find("Bedz - Popust/Text/Number/Number Shadow").GetComponent<TextMesh>().text = this.PopustBackPacks[k] + "%";
				}
				string s3 = "0." + this.PopustBackPacks[k];
				if (this.ZakljucaniBackPacks[k] == 1)
				{
					float num3 = float.Parse(this.CoinsBackPacks[k]) / (1f - float.Parse(s3));
					this.BackPacksObjects[k].Find("Polje za unos COINA U shopu - Shop_NoDiscount").gameObject.SetActive(true);
					this.BackPacksObjects[k].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number").GetComponent<TextMesh>().text = num3.ToString();
					if (this.BackPacksObjects[k].parent.gameObject.activeSelf)
					{
						this.BackPacksObjects[k].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
					}
					this.BackPacksObjects[k].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMesh>().text = this.CoinsBackPacks[k];
					this.BackPacksObjects[k].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
					this.BackPacksObjects[k].Find("Polje za unos COINA U shopu - Shop/Coins Number").GetComponent<TextMesh>().color = ShopManagerFull.PopustBoja;
				}
			}
			this.BackPacksObjects[k].Find("Text/ime").GetComponent<TextMesh>().text = this.ImenaBackPacks[k];
			this.BackPacksObjects[k].Find("Text/ime").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		}
		if (this.ImaNovihKapa)
		{
			this.ZidFooter.transform.Find("1HatsShopTab/Znak Uzvika telo").gameObject.SetActive(true);
		}
		else
		{
			this.ZidFooter.transform.Find("1HatsShopTab/Znak Uzvika telo").gameObject.SetActive(false);
		}
		if (this.ImaNovihMajica)
		{
			this.ZidFooter.transform.Find("2TShirtsShopTab/Znak Uzvika telo").gameObject.SetActive(true);
		}
		else
		{
			this.ZidFooter.transform.Find("2TShirtsShopTab/Znak Uzvika telo").gameObject.SetActive(false);
		}
		if (this.ImaNovihRanceva)
		{
			this.ZidFooter.transform.Find("3BackPackShopTab/Znak Uzvika telo").gameObject.SetActive(true);
		}
		else
		{
			this.ZidFooter.transform.Find("3BackPackShopTab/Znak Uzvika telo").gameObject.SetActive(false);
		}
		if (this.ImaNovihKapa | this.ImaNovihMajica | this.ImaNovihRanceva)
		{
			this.Custumization.transform.Find("Znak Uzvika telo").gameObject.SetActive(true);
			return;
		}
		this.Custumization.transform.Find("Znak Uzvika telo").gameObject.SetActive(false);
	}

	// Token: 0x06002717 RID: 10007 RVA: 0x0011CBB0 File Offset: 0x0011ADB0
	public void KupiItem()
	{
		if (ShopManagerFull.BuyButtonState == 0)
		{
			if (ShopManagerFull.AktivanCustomizationTab == 1)
			{
				StagesParser.currentMoney -= int.Parse(this.CoinsHats[ShopManagerFull.AktivanItemSesir]);
				ShopManagerFull.SveStvariZaOblacenjeHats[ShopManagerFull.AktivanItemSesir] = 1;
				this.HatsObjects[ShopManagerFull.AktivanItemSesir].Find("Stikla").gameObject.SetActive(true);
				this.HatsObjects[ShopManagerFull.AktivanItemSesir].Find("Polje za unos COINA U shopu - Shop").gameObject.SetActive(false);
				this.HatsObjects[ShopManagerFull.AktivanItemSesir].Find("Polje za unos COINA U shopu - Shop_NoDiscount").gameObject.SetActive(false);
				this.HatsObjects[ShopManagerFull.AktivanItemSesir].Find("Bedz - Popust").gameObject.SetActive(false);
				this.TrenutniSelektovanSesir = -1;
				this.ProveraTrenutnogItema(ShopManagerFull.AktivanItemSesir);
				FacebookManager.UserSveKupovineHats = "";
				for (int i = 0; i < ShopManagerFull.SveStvariZaOblacenjeHats.Count; i++)
				{
					FacebookManager.UserSveKupovineHats += ShopManagerFull.SveStvariZaOblacenjeHats[i] + "#";
				}
				StagesParser.svekupovineGlava = FacebookManager.UserSveKupovineHats;
				PlayerPrefs.SetString("UserSveKupovineHats", FacebookManager.UserSveKupovineHats);
				PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
				PlayerPrefs.Save();
			}
			else if (ShopManagerFull.AktivanCustomizationTab == 2)
			{
				StagesParser.currentMoney -= int.Parse(this.CoinsShirts[ShopManagerFull.AktivanItemMajica]);
				ShopManagerFull.SveStvariZaOblacenjeShirts[ShopManagerFull.AktivanItemMajica] = 1;
				this.ShirtsObjects[ShopManagerFull.AktivanItemMajica].Find("Stikla").gameObject.SetActive(true);
				this.ShirtsObjects[ShopManagerFull.AktivanItemMajica].Find("Polje za unos COINA U shopu - Shop").gameObject.SetActive(false);
				this.ShirtsObjects[ShopManagerFull.AktivanItemMajica].Find("Polje za unos COINA U shopu - Shop_NoDiscount").gameObject.SetActive(false);
				this.ShirtsObjects[ShopManagerFull.AktivanItemMajica].Find("Bedz - Popust").gameObject.SetActive(false);
				this.TrenutnoSelektovanaMajica = -1;
				this.ProveraTrenutnogItema(ShopManagerFull.AktivanItemMajica);
				FacebookManager.UserSveKupovineShirts = "";
				for (int j = 0; j < ShopManagerFull.SveStvariZaOblacenjeShirts.Count; j++)
				{
					FacebookManager.UserSveKupovineShirts += ShopManagerFull.SveStvariZaOblacenjeShirts[j] + "#";
				}
				StagesParser.svekupovineMajica = FacebookManager.UserSveKupovineShirts;
				PlayerPrefs.SetString("UserSveKupovineShirts", FacebookManager.UserSveKupovineShirts);
				PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
				PlayerPrefs.Save();
			}
			if (ShopManagerFull.AktivanCustomizationTab == 3)
			{
				StagesParser.currentMoney -= int.Parse(this.CoinsBackPacks[ShopManagerFull.AktivanItemRanac]);
				ShopManagerFull.SveStvariZaOblacenjeBackPack[ShopManagerFull.AktivanItemRanac] = 1;
				this.BackPacksObjects[ShopManagerFull.AktivanItemRanac].Find("Stikla").gameObject.SetActive(true);
				this.BackPacksObjects[ShopManagerFull.AktivanItemRanac].Find("Polje za unos COINA U shopu - Shop").gameObject.SetActive(false);
				this.BackPacksObjects[ShopManagerFull.AktivanItemRanac].Find("Polje za unos COINA U shopu - Shop_NoDiscount").gameObject.SetActive(false);
				this.BackPacksObjects[ShopManagerFull.AktivanItemRanac].Find("Bedz - Popust").gameObject.SetActive(false);
				this.TrenutnoSelektovanRanac = -1;
				this.ProveraTrenutnogItema(ShopManagerFull.AktivanItemRanac);
				FacebookManager.UserSveKupovineBackPacks = "";
				for (int k = 0; k < ShopManagerFull.SveStvariZaOblacenjeBackPack.Count; k++)
				{
					FacebookManager.UserSveKupovineBackPacks += ShopManagerFull.SveStvariZaOblacenjeBackPack[k] + "#";
				}
				StagesParser.svekupovineLedja = FacebookManager.UserSveKupovineBackPacks;
				PlayerPrefs.SetString("UserSveKupovineBackPacks", FacebookManager.UserSveKupovineBackPacks);
				PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
				PlayerPrefs.Save();
			}
			StagesParser.ServerUpdate = 1;
			this.CoinsNumber.transform.Find("Coins Number").GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
			this.CoinsNumber.transform.Find("Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(false, true, true);
		}
		else if (ShopManagerFull.BuyButtonState == 1)
		{
			this.CoinsNumber.GetComponent<Animation>().Play("Not Enough Coins");
		}
		else if (ShopManagerFull.BuyButtonState == 2)
		{
			if (ShopManagerFull.AktivanCustomizationTab == 1)
			{
				if (this.PreviewSesir != -1)
				{
					this.MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/Chest/Neck/Head/" + this.PreviewSesir).transform.GetChild(0).gameObject.SetActive(false);
					this.PreviewSesir = -1;
				}
				if (int.Parse(this.UsiHats[ShopManagerFull.AktivanItemSesir]) == 1)
				{
					if (!ShopManagerFull.ImaUsi)
					{
						ShopManagerFull.ImaUsi = true;
						this.MajmunBobo.transform.Find("PrinceGorilla/Usi").gameObject.SetActive(true);
					}
				}
				else if (ShopManagerFull.ImaUsi)
				{
					ShopManagerFull.ImaUsi = false;
					this.MajmunBobo.transform.Find("PrinceGorilla/Usi").gameObject.SetActive(false);
				}
				if (int.Parse(this.KosaHats[ShopManagerFull.AktivanItemSesir]) == 1)
				{
					ShopManagerFull.ImaKosu = true;
					this.MajmunBobo.transform.Find("PrinceGorilla/Kosa").gameObject.SetActive(true);
				}
				else
				{
					ShopManagerFull.ImaKosu = false;
					this.MajmunBobo.transform.Find("PrinceGorilla/Kosa").gameObject.SetActive(false);
				}
				if (ShopManagerFull.AktivanSesir != -1)
				{
					this.MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/Chest/Neck/Head/" + ShopManagerFull.AktivanSesir).transform.GetChild(0).gameObject.SetActive(false);
				}
				this.MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/Chest/Neck/Head/" + ShopManagerFull.AktivanItemSesir).transform.GetChild(0).gameObject.SetActive(true);
				ShopManagerFull.AktivanSesir = ShopManagerFull.AktivanItemSesir;
				this.TrenutniSelektovanSesir = -1;
				this.ProveraTrenutnogItema(ShopManagerFull.AktivanItemSesir);
				StagesParser.glava = ShopManagerFull.AktivanSesir;
				StagesParser.imaKosu = ShopManagerFull.ImaKosu;
				StagesParser.imaUsi = ShopManagerFull.ImaUsi;
			}
			else if (ShopManagerFull.AktivanCustomizationTab == 2)
			{
				this.MajmunBobo.transform.Find("PrinceGorilla/custom_Majica").gameObject.SetActive(true);
				Texture texture = Resources.Load("Majice/Bg" + ShopManagerFull.AktivanItemMajica) as Texture;
				this.MajmunBobo.transform.Find("PrinceGorilla/custom_Majica").GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
				this.MajmunBobo.transform.Find("PrinceGorilla/custom_Majica").GetComponent<Renderer>().material.color = this.TShirtColors[ShopManagerFull.AktivanItemMajica];
				ShopManagerFull.AktivnaMajica = ShopManagerFull.AktivanItemMajica;
				this.TrenutnoSelektovanaMajica = -1;
				this.ProveraTrenutnogItema(ShopManagerFull.AktivanItemMajica);
				StagesParser.majica = ShopManagerFull.AktivnaMajica;
				StagesParser.bojaMajice = this.TShirtColors[ShopManagerFull.AktivnaMajica];
			}
			if (ShopManagerFull.AktivanCustomizationTab == 3)
			{
				if (this.PreviewRanac != -1)
				{
					this.MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + this.PreviewRanac).transform.GetChild(0).gameObject.SetActive(false);
					this.PreviewRanac = -1;
				}
				if (ShopManagerFull.AktivanRanac != -1)
				{
					this.MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + ShopManagerFull.AktivanRanac).transform.GetChild(0).gameObject.SetActive(false);
				}
				this.MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + ShopManagerFull.AktivanItemRanac).transform.GetChild(0).gameObject.SetActive(true);
				ShopManagerFull.AktivanRanac = ShopManagerFull.AktivanItemRanac;
				this.TrenutnoSelektovanRanac = -1;
				this.ProveraTrenutnogItema(ShopManagerFull.AktivanItemRanac);
				StagesParser.ledja = ShopManagerFull.AktivanRanac;
			}
			string text = string.Concat(new string[]
			{
				ShopManagerFull.AktivanSesir.ToString(),
				"#",
				ShopManagerFull.AktivnaMajica.ToString(),
				"#",
				ShopManagerFull.AktivanRanac.ToString()
			});
			PlayerPrefs.SetString("AktivniItemi", text);
			PlayerPrefs.Save();
		}
		else if (ShopManagerFull.BuyButtonState == 3)
		{
			if (ShopManagerFull.AktivanCustomizationTab == 1)
			{
				ShopManagerFull.ImaUsi = true;
				this.MajmunBobo.transform.Find("PrinceGorilla/Usi").gameObject.SetActive(true);
				ShopManagerFull.ImaKosu = true;
				this.MajmunBobo.transform.Find("PrinceGorilla/Kosa").gameObject.SetActive(true);
				this.MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/Chest/Neck/Head/" + ShopManagerFull.AktivanItemSesir).transform.GetChild(0).gameObject.SetActive(false);
				ShopManagerFull.AktivanSesir = -1;
				this.TrenutniSelektovanSesir = -1;
				this.ProveraTrenutnogItema(ShopManagerFull.AktivanItemSesir);
				StagesParser.imaKosu = true;
				StagesParser.imaUsi = true;
				StagesParser.glava = -1;
			}
			else if (ShopManagerFull.AktivanCustomizationTab == 2)
			{
				this.MajmunBobo.transform.Find("PrinceGorilla/custom_Majica").gameObject.SetActive(false);
				ShopManagerFull.AktivnaMajica = -1;
				this.TrenutnoSelektovanaMajica = -1;
				this.ProveraTrenutnogItema(ShopManagerFull.AktivanItemMajica);
				StagesParser.majica = -1;
			}
			if (ShopManagerFull.AktivanCustomizationTab == 3)
			{
				this.MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + ShopManagerFull.AktivanItemRanac).transform.GetChild(0).gameObject.SetActive(false);
				ShopManagerFull.AktivanRanac = -1;
				this.TrenutnoSelektovanRanac = -1;
				this.ProveraTrenutnogItema(ShopManagerFull.AktivanItemRanac);
				StagesParser.ledja = -1;
			}
			string text2 = string.Concat(new string[]
			{
				ShopManagerFull.AktivanSesir.ToString(),
				"#",
				ShopManagerFull.AktivnaMajica.ToString(),
				"#",
				ShopManagerFull.AktivanRanac.ToString()
			});
			PlayerPrefs.SetString("AktivniItemi", text2);
			PlayerPrefs.Save();
		}
		int buyButtonState = ShopManagerFull.BuyButtonState;
	}

	// Token: 0x06002718 RID: 10008 RVA: 0x0011D610 File Offset: 0x0011B810
	public void PreviewItem()
	{
		if (ShopManagerFull.AktivanCustomizationTab == 1)
		{
			if (this.ZakljucaniHats[ShopManagerFull.AktivanItemSesir] == 1)
			{
				if (int.Parse(this.UsiHats[ShopManagerFull.AktivanItemSesir]) == 1)
				{
					if (!ShopManagerFull.ImaUsi)
					{
						ShopManagerFull.ImaUsi = true;
						this.MajmunBobo.transform.Find("PrinceGorilla/Usi").gameObject.SetActive(true);
					}
				}
				else if (ShopManagerFull.ImaUsi)
				{
					ShopManagerFull.ImaUsi = false;
					this.MajmunBobo.transform.Find("PrinceGorilla/Usi").gameObject.SetActive(false);
				}
				if (int.Parse(this.KosaHats[ShopManagerFull.AktivanItemSesir]) == 1)
				{
					ShopManagerFull.ImaKosu = true;
					this.MajmunBobo.transform.Find("PrinceGorilla/Kosa").gameObject.SetActive(true);
				}
				else
				{
					ShopManagerFull.ImaKosu = false;
					this.MajmunBobo.transform.Find("PrinceGorilla/Kosa").gameObject.SetActive(false);
				}
				if (this.PreviewSesir != -1)
				{
					this.MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/Chest/Neck/Head/" + this.PreviewSesir).transform.GetChild(0).gameObject.SetActive(false);
				}
				if (ShopManagerFull.AktivanSesir != -1)
				{
					this.MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/Chest/Neck/Head/" + ShopManagerFull.AktivanSesir).transform.GetChild(0).gameObject.SetActive(false);
				}
				this.MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/Chest/Neck/Head/" + ShopManagerFull.AktivanItemSesir).transform.GetChild(0).gameObject.SetActive(true);
				this.PreviewSesir = ShopManagerFull.AktivanItemSesir;
				this.TrenutniSelektovanSesir = -1;
				this.ProveraTrenutnogItema(this.PreviewSesir);
			}
		}
		else if (ShopManagerFull.AktivanCustomizationTab == 2 && this.ZakljucaniShirts[ShopManagerFull.AktivanItemMajica] == 1)
		{
			this.MajmunBobo.transform.Find("PrinceGorilla/custom_Majica").gameObject.SetActive(true);
			Texture texture = Resources.Load("Majice/Bg" + ShopManagerFull.AktivanItemMajica) as Texture;
			this.MajmunBobo.transform.Find("PrinceGorilla/custom_Majica").GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
			this.MajmunBobo.transform.Find("PrinceGorilla/custom_Majica").GetComponent<Renderer>().material.color = this.TShirtColors[ShopManagerFull.AktivanItemMajica];
			this.PreviewMajica = ShopManagerFull.AktivanItemMajica;
			this.TrenutnoSelektovanaMajica = -1;
			this.ProveraTrenutnogItema(this.PreviewMajica);
		}
		if (ShopManagerFull.AktivanCustomizationTab == 3 && this.ZakljucaniBackPacks[ShopManagerFull.AktivanItemRanac] == 1)
		{
			if (this.PreviewRanac != -1)
			{
				this.MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + this.PreviewRanac).transform.GetChild(0).gameObject.SetActive(false);
			}
			if (ShopManagerFull.AktivanRanac != -1)
			{
				this.MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + ShopManagerFull.AktivanRanac).transform.GetChild(0).gameObject.SetActive(false);
			}
			this.MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + ShopManagerFull.AktivanItemRanac).transform.GetChild(0).gameObject.SetActive(true);
			this.PreviewRanac = ShopManagerFull.AktivanItemRanac;
			this.TrenutnoSelektovanRanac = -1;
			this.ProveraTrenutnogItema(this.PreviewRanac);
		}
	}

	// Token: 0x06002719 RID: 10009 RVA: 0x0011D9D8 File Offset: 0x0011BBD8
	public void KupiDoubleCoins()
	{
		if (StagesParser.currentMoney < int.Parse(this.CoinsPowerUps[0]))
		{
			this.CoinsNumber.GetComponent<Animation>().Play("Not Enough Coins");
			return;
		}
		StagesParser.currentMoney -= int.Parse(this.CoinsPowerUps[0]);
		StagesParser.powerup_doublecoins++;
		GameObject.Find("Double Coins Number").GetComponent<Animation>().Play("BoughtPowerUp");
		this.CoinsNumber.transform.Find("Coins Number").GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
		this.CoinsNumber.transform.Find("Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Double Coins Number/Number").GetComponent<TextMesh>().text = StagesParser.powerup_doublecoins.ToString();
		GameObject.Find("Double Coins Number/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
		PlayerPrefs.SetString("PowerUps", string.Concat(new object[]
		{
			StagesParser.powerup_doublecoins,
			"#",
			StagesParser.powerup_magnets,
			"#",
			StagesParser.powerup_shields
		}));
		PlayerPrefs.Save();
		StagesParser.ServerUpdate = 1;
	}

	// Token: 0x0600271A RID: 10010 RVA: 0x0011DB3C File Offset: 0x0011BD3C
	public void KupiMagnet()
	{
		if (StagesParser.currentMoney < int.Parse(this.CoinsPowerUps[1]))
		{
			this.CoinsNumber.GetComponent<Animation>().Play("Not Enough Coins");
			return;
		}
		StagesParser.currentMoney -= int.Parse(this.CoinsPowerUps[1]);
		StagesParser.powerup_magnets++;
		GameObject.Find("Magnet Number").GetComponent<Animation>().Play("BoughtPowerUp");
		this.CoinsNumber.transform.Find("Coins Number").GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
		this.CoinsNumber.transform.Find("Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Magnet Number/Number").GetComponent<TextMesh>().text = StagesParser.powerup_magnets.ToString();
		GameObject.Find("Magnet Number/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
		PlayerPrefs.SetString("PowerUps", string.Concat(new object[]
		{
			StagesParser.powerup_doublecoins,
			"#",
			StagesParser.powerup_magnets,
			"#",
			StagesParser.powerup_shields
		}));
		PlayerPrefs.Save();
		StagesParser.ServerUpdate = 1;
	}

	// Token: 0x0600271B RID: 10011 RVA: 0x0011DCA0 File Offset: 0x0011BEA0
	public void KupiShield()
	{
		if (StagesParser.currentMoney < int.Parse(this.CoinsPowerUps[2]))
		{
			this.CoinsNumber.GetComponent<Animation>().Play("Not Enough Coins");
			return;
		}
		StagesParser.currentMoney -= int.Parse(this.CoinsPowerUps[2]);
		StagesParser.powerup_shields++;
		GameObject.Find("Shield Number").GetComponent<Animation>().Play("BoughtPowerUp");
		this.CoinsNumber.transform.Find("Coins Number").GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
		this.CoinsNumber.transform.Find("Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Shield Number/Number").GetComponent<TextMesh>().text = StagesParser.powerup_shields.ToString();
		GameObject.Find("Shield Number/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
		PlayerPrefs.SetString("PowerUps", string.Concat(new object[]
		{
			StagesParser.powerup_doublecoins,
			"#",
			StagesParser.powerup_magnets,
			"#",
			StagesParser.powerup_shields
		}));
		PlayerPrefs.Save();
		StagesParser.ServerUpdate = 1;
	}

	// Token: 0x0600271C RID: 10012 RVA: 0x0011DE04 File Offset: 0x0011C004
	public void KupiBananu()
	{
		if (StagesParser.currentMoney < StagesParser.bananaCost)
		{
			this.CoinsNumber.GetComponent<Animation>().Play("Not Enough Coins");
			return;
		}
		StagesParser.currentMoney -= StagesParser.bananaCost;
		StagesParser.currentBananas++;
		GameObject.Find("Shop/2 Shop - BANANA/Zid Shop/Zid Header i Footer/Zid Footer Shop/Banana Number").GetComponent<Animation>().Play("BoughtPowerUp");
		this.CoinsNumber.transform.Find("Coins Number").GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
		this.CoinsNumber.transform.Find("Coins Number").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		GameObject.Find("Shop/2 Shop - BANANA/Zid Shop/Zid Header i Footer/Zid Footer Shop/Banana Number/Number").GetComponent<TextMesh>().text = StagesParser.currentBananas.ToString();
		GameObject.Find("Shop/2 Shop - BANANA/Zid Shop/Zid Header i Footer/Zid Footer Shop/Banana Number/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(true, true, true);
		PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
		PlayerPrefs.SetInt("TotalBananas", StagesParser.currentBananas);
		PlayerPrefs.Save();
		StagesParser.ServerUpdate = 1;
	}

	// Token: 0x0600271D RID: 10013 RVA: 0x0011DF14 File Offset: 0x0011C114
	public void OcistiPreview()
	{
		if (this.PreviewSesir != -1)
		{
			ShopManagerFull.ImaUsi = true;
			this.MajmunBobo.transform.Find("PrinceGorilla/Usi").gameObject.SetActive(true);
			ShopManagerFull.ImaKosu = true;
			this.MajmunBobo.transform.Find("PrinceGorilla/Kosa").gameObject.SetActive(true);
			this.MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/Chest/Neck/Head/" + this.PreviewSesir).transform.GetChild(0).gameObject.SetActive(false);
			this.PreviewSesir = -1;
		}
		if (this.PreviewMajica != -1)
		{
			this.MajmunBobo.transform.Find("PrinceGorilla/custom_Majica").gameObject.SetActive(false);
			this.PreviewMajica = -1;
		}
		if (this.PreviewRanac != -1)
		{
			this.MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + this.PreviewRanac).transform.GetChild(0).gameObject.SetActive(false);
			this.PreviewRanac = -1;
		}
		if (ShopManagerFull.AktivanCustomizationTab == 1)
		{
			this.TrenutniSelektovanSesir = -1;
			this.ProveraTrenutnogItema(ShopManagerFull.AktivanItemSesir);
		}
		else if (ShopManagerFull.AktivanCustomizationTab == 2)
		{
			this.TrenutnoSelektovanaMajica = -1;
			this.ProveraTrenutnogItema(ShopManagerFull.AktivanItemMajica);
		}
		else if (ShopManagerFull.AktivanCustomizationTab == 3)
		{
			this.TrenutnoSelektovanRanac = -1;
			this.ProveraTrenutnogItema(ShopManagerFull.AktivanItemRanac);
		}
		if (ShopManagerFull.AktivanSesir != -1)
		{
			if (int.Parse(this.UsiHats[ShopManagerFull.AktivanSesir]) == 1)
			{
				if (!ShopManagerFull.ImaUsi)
				{
					ShopManagerFull.ImaUsi = true;
					this.MajmunBobo.transform.Find("PrinceGorilla/Usi").gameObject.SetActive(true);
				}
			}
			else if (ShopManagerFull.ImaUsi)
			{
				ShopManagerFull.ImaUsi = false;
				this.MajmunBobo.transform.Find("PrinceGorilla/Usi").gameObject.SetActive(false);
			}
			if (int.Parse(this.KosaHats[ShopManagerFull.AktivanSesir]) == 1)
			{
				ShopManagerFull.ImaKosu = true;
				this.MajmunBobo.transform.Find("PrinceGorilla/Kosa").gameObject.SetActive(true);
			}
			else
			{
				ShopManagerFull.ImaKosu = false;
				this.MajmunBobo.transform.Find("PrinceGorilla/Kosa").gameObject.SetActive(false);
			}
			this.MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/Chest/Neck/Head/" + ShopManagerFull.AktivanSesir).transform.GetChild(0).gameObject.SetActive(true);
			this.TrenutniSelektovanSesir = -1;
			this.ProveraTrenutnogItema(ShopManagerFull.AktivanItemSesir);
		}
		if (ShopManagerFull.AktivnaMajica != -1)
		{
			this.MajmunBobo.transform.Find("PrinceGorilla/custom_Majica").gameObject.SetActive(true);
			Texture texture = Resources.Load("Majice/Bg" + ShopManagerFull.AktivnaMajica) as Texture;
			this.MajmunBobo.transform.Find("PrinceGorilla/custom_Majica").GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
			this.MajmunBobo.transform.Find("PrinceGorilla/custom_Majica").GetComponent<Renderer>().material.color = this.TShirtColors[ShopManagerFull.AktivnaMajica];
			this.TrenutnoSelektovanaMajica = -1;
			this.ProveraTrenutnogItema(ShopManagerFull.AktivanItemMajica);
		}
		if (ShopManagerFull.AktivanRanac != -1)
		{
			this.MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + ShopManagerFull.AktivanRanac).transform.GetChild(0).gameObject.SetActive(true);
			this.TrenutnoSelektovanRanac = -1;
			this.ProveraTrenutnogItema(ShopManagerFull.AktivanItemRanac);
		}
	}

	// Token: 0x0600271E RID: 10014 RVA: 0x0011E2C8 File Offset: 0x0011C4C8
	public void OcistiMajmuna()
	{
		ShopManagerFull.ImaUsi = true;
		this.MajmunBobo.transform.Find("PrinceGorilla/Usi").gameObject.SetActive(true);
		ShopManagerFull.ImaKosu = true;
		this.MajmunBobo.transform.Find("PrinceGorilla/Kosa").gameObject.SetActive(true);
		StagesParser.glava = -1;
		StagesParser.majica = -1;
		StagesParser.ledja = -1;
		StagesParser.imaKosu = true;
		StagesParser.imaUsi = true;
		this.AktivniItemString = "-1#-1#-1";
		PlayerPrefs.SetString("AktivniItemi", this.AktivniItemString);
		PlayerPrefs.Save();
		if (ShopManagerFull.AktivanSesir != -1)
		{
			this.MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/Chest/Neck/Head/" + ShopManagerFull.AktivanSesir).transform.GetChild(0).gameObject.SetActive(false);
			ShopManagerFull.AktivanSesir = -1;
		}
		if (this.PreviewSesir != -1)
		{
			this.MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/Chest/Neck/Head/" + this.PreviewSesir).transform.GetChild(0).gameObject.SetActive(false);
			this.PreviewSesir = -1;
		}
		if (ShopManagerFull.AktivnaMajica != -1)
		{
			this.MajmunBobo.transform.Find("PrinceGorilla/custom_Majica").gameObject.SetActive(false);
			ShopManagerFull.AktivnaMajica = -1;
		}
		if (this.PreviewMajica != -1)
		{
			this.MajmunBobo.transform.Find("PrinceGorilla/custom_Majica").gameObject.SetActive(false);
			this.PreviewMajica = -1;
		}
		if (ShopManagerFull.AktivanRanac != -1)
		{
			this.MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + ShopManagerFull.AktivanRanac).transform.GetChild(0).gameObject.SetActive(false);
			ShopManagerFull.AktivanRanac = -1;
		}
		if (this.PreviewRanac != -1)
		{
			this.MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + this.PreviewRanac).transform.GetChild(0).gameObject.SetActive(false);
			this.PreviewRanac = -1;
		}
		if (ShopManagerFull.AktivanCustomizationTab == 1)
		{
			this.TrenutniSelektovanSesir = -1;
			this.ProveraTrenutnogItema(ShopManagerFull.AktivanItemSesir);
			return;
		}
		if (ShopManagerFull.AktivanCustomizationTab == 2)
		{
			this.TrenutnoSelektovanaMajica = -1;
			this.ProveraTrenutnogItema(ShopManagerFull.AktivanItemMajica);
			return;
		}
		if (ShopManagerFull.AktivanCustomizationTab == 3)
		{
			this.TrenutnoSelektovanRanac = -1;
			this.ProveraTrenutnogItema(ShopManagerFull.AktivanItemRanac);
		}
	}

	// Token: 0x0600271F RID: 10015 RVA: 0x0011E534 File Offset: 0x0011C734
	public void ObuciMajmunaNaStartu()
	{
		if (PlayerPrefs.HasKey("AktivniItemi"))
		{
			this.AktivniItemString = PlayerPrefs.GetString("AktivniItemi");
			this.AktivniItemi = this.AktivniItemString.Split(new char[]
			{
				'#'
			});
			ShopManagerFull.AktivanSesir = int.Parse(this.AktivniItemi[0]);
			ShopManagerFull.AktivnaMajica = int.Parse(this.AktivniItemi[1]);
			ShopManagerFull.AktivanRanac = int.Parse(this.AktivniItemi[2]);
		}
		else
		{
			ShopManagerFull.AktivanSesir = -1;
			ShopManagerFull.AktivnaMajica = -1;
			ShopManagerFull.AktivanRanac = -1;
			StagesParser.glava = -1;
			StagesParser.imaKosu = true;
			StagesParser.imaUsi = true;
			StagesParser.majica = -1;
			StagesParser.ledja = -1;
		}
		if (ShopManagerFull.AktivanSesir != -1)
		{
			if (int.Parse(this.UsiHats[ShopManagerFull.AktivanSesir]) == 1)
			{
				if (!ShopManagerFull.ImaUsi)
				{
					ShopManagerFull.ImaUsi = true;
					this.MajmunBobo.transform.Find("PrinceGorilla/Usi").gameObject.SetActive(true);
					StagesParser.imaUsi = true;
				}
			}
			else if (ShopManagerFull.ImaUsi)
			{
				ShopManagerFull.ImaUsi = false;
				this.MajmunBobo.transform.Find("PrinceGorilla/Usi").gameObject.SetActive(false);
				StagesParser.imaUsi = false;
			}
			if (int.Parse(this.KosaHats[ShopManagerFull.AktivanSesir]) == 1)
			{
				ShopManagerFull.ImaKosu = true;
				this.MajmunBobo.transform.Find("PrinceGorilla/Kosa").gameObject.SetActive(true);
				StagesParser.imaKosu = true;
			}
			else
			{
				ShopManagerFull.ImaKosu = false;
				this.MajmunBobo.transform.Find("PrinceGorilla/Kosa").gameObject.SetActive(false);
				StagesParser.imaKosu = false;
			}
			this.MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/Chest/Neck/Head/" + ShopManagerFull.AktivanSesir).transform.GetChild(0).gameObject.SetActive(true);
			StagesParser.glava = ShopManagerFull.AktivanSesir;
		}
		else
		{
			StagesParser.glava = -1;
			StagesParser.imaKosu = true;
			StagesParser.imaUsi = true;
		}
		if (ShopManagerFull.AktivnaMajica != -1)
		{
			this.MajmunBobo.transform.Find("PrinceGorilla/custom_Majica").gameObject.SetActive(true);
			Texture texture = Resources.Load("Majice/Bg" + ShopManagerFull.AktivnaMajica) as Texture;
			this.MajmunBobo.transform.Find("PrinceGorilla/custom_Majica").GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
			this.MajmunBobo.transform.Find("PrinceGorilla/custom_Majica").GetComponent<Renderer>().material.color = this.TShirtColors[ShopManagerFull.AktivnaMajica];
			StagesParser.majica = ShopManagerFull.AktivnaMajica;
			StagesParser.bojaMajice = this.TShirtColors[ShopManagerFull.AktivnaMajica];
		}
		else
		{
			StagesParser.majica = -1;
			StagesParser.bojaMajice = Color.white;
		}
		if (ShopManagerFull.AktivanRanac != -1)
		{
			if (Application.loadedLevel == 1)
			{
				if (ShopManagerFull.AktivanRanac == 0)
				{
					this.MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + ShopManagerFull.AktivanRanac).transform.GetChild(0).GetComponent<MeshFilter>().mesh = GameObject.Find("RefZaSedenje_AndjeoskaKrila").GetComponent<MeshFilter>().mesh;
				}
				else if (ShopManagerFull.AktivanRanac == 5)
				{
					this.MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + ShopManagerFull.AktivanRanac).transform.GetChild(0).GetComponent<MeshFilter>().mesh = GameObject.Find("RefZaSedenje_SlepiMisKrila").GetComponent<MeshFilter>().mesh;
				}
			}
			this.MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + ShopManagerFull.AktivanRanac).transform.GetChild(0).gameObject.SetActive(true);
			StagesParser.ledja = ShopManagerFull.AktivanRanac;
			return;
		}
		StagesParser.ledja = -1;
	}

	// Token: 0x06002720 RID: 10016 RVA: 0x0011E91C File Offset: 0x0011CB1C
	private void OnApplicationQuit()
	{
		this.StariBrojOtkljucanihItema = string.Concat(new object[]
		{
			ShopManagerFull.BrojOtkljucanihKapa,
			"#",
			ShopManagerFull.BrojOtkljucanihMajici,
			"#",
			ShopManagerFull.BrojOtkljucanihRanceva
		});
		PlayerPrefs.SetString("OtkljucaniItemi", this.StariBrojOtkljucanihItema);
		PlayerPrefs.Save();
	}

	// Token: 0x040020C8 RID: 8392
	public bool EarsAndHairCustomization;

	// Token: 0x040020C9 RID: 8393
	public Transform[] HatsObjects = new Transform[0];

	// Token: 0x040020CA RID: 8394
	public Transform[] ShirtsObjects = new Transform[0];

	// Token: 0x040020CB RID: 8395
	public Transform[] BackPacksObjects = new Transform[0];

	// Token: 0x040020CC RID: 8396
	public Transform[] PowerUpsObjects = new Transform[0];

	// Token: 0x040020CD RID: 8397
	public static int BuyButtonState;

	// Token: 0x040020CE RID: 8398
	public static bool PreviewState = false;

	// Token: 0x040020CF RID: 8399
	private GameObject ZidFooter;

	// Token: 0x040020D0 RID: 8400
	private GameObject Custumization;

	// Token: 0x040020D1 RID: 8401
	private bool ImaNovihMajica;

	// Token: 0x040020D2 RID: 8402
	private bool ImaNovihKapa;

	// Token: 0x040020D3 RID: 8403
	private bool ImaNovihRanceva;

	// Token: 0x040020D4 RID: 8404
	private GameObject ButtonShop;

	// Token: 0x040020D5 RID: 8405
	private GameObject ButtonShopSprite;

	// Token: 0x040020D6 RID: 8406
	private GameObject PreviewShopButton;

	// Token: 0x040020D7 RID: 8407
	private GameObject ShopBanana;

	// Token: 0x040020D8 RID: 8408
	public static int AktivanSesir;

	// Token: 0x040020D9 RID: 8409
	public static int AktivnaMajica;

	// Token: 0x040020DA RID: 8410
	public static int AktivanRanac;

	// Token: 0x040020DB RID: 8411
	private int PreviewSesir;

	// Token: 0x040020DC RID: 8412
	private int PreviewMajica;

	// Token: 0x040020DD RID: 8413
	private int PreviewRanac;

	// Token: 0x040020DE RID: 8414
	public static bool otvorenShop = false;

	// Token: 0x040020DF RID: 8415
	public static int AktivanTab;

	// Token: 0x040020E0 RID: 8416
	public static int AktivanCustomizationTab;

	// Token: 0x040020E1 RID: 8417
	public static int AktivanItemSesir;

	// Token: 0x040020E2 RID: 8418
	public static int AktivanItemMajica;

	// Token: 0x040020E3 RID: 8419
	public static int AktivanItemRanac;

	// Token: 0x040020E4 RID: 8420
	private int TrenutniSelektovanSesir = 999;

	// Token: 0x040020E5 RID: 8421
	private int TrenutnoSelektovanaMajica = 999;

	// Token: 0x040020E6 RID: 8422
	private int TrenutnoSelektovanRanac = 999;

	// Token: 0x040020E7 RID: 8423
	private string[] Hats;

	// Token: 0x040020E8 RID: 8424
	private string[] Shirts;

	// Token: 0x040020E9 RID: 8425
	private string[] BackPacks;

	// Token: 0x040020EA RID: 8426
	private string[] AktivniItemi;

	// Token: 0x040020EB RID: 8427
	private string AktivniItemString;

	// Token: 0x040020EC RID: 8428
	private GameObject MajmunBobo;

	// Token: 0x040020ED RID: 8429
	private Vector3 MainScenaPozicija;

	// Token: 0x040020EE RID: 8430
	private Vector3 ShopCustomizationPozicija;

	// Token: 0x040020EF RID: 8431
	public static bool ImaUsi;

	// Token: 0x040020F0 RID: 8432
	public static bool ImaKosu;

	// Token: 0x040020F1 RID: 8433
	public static ShopManagerFull ShopObject;

	// Token: 0x040020F2 RID: 8434
	private string releasedItem;

	// Token: 0x040020F3 RID: 8435
	private string clickedItem;

	// Token: 0x040020F4 RID: 8436
	private Vector3 originalScale;

	// Token: 0x040020F5 RID: 8437
	private static Color KakiBoja = new Color(0.97255f, 0.79216f, 0.40784f);

	// Token: 0x040020F6 RID: 8438
	private static Color PopustBoja = new Color(0.11373f, 0.82353f, 0.38039f);

	// Token: 0x040020F7 RID: 8439
	private static float gornjaGranica;

	// Token: 0x040020F8 RID: 8440
	private static float donjaGranica;

	// Token: 0x040020F9 RID: 8441
	private TextAsset aset2;

	// Token: 0x040020FA RID: 8442
	private string aset;

	// Token: 0x040020FB RID: 8443
	public static bool ShopInicijalizovan = false;

	// Token: 0x040020FC RID: 8444
	private int BrojItemaShopHats;

	// Token: 0x040020FD RID: 8445
	private int BrojItemaShopShirts;

	// Token: 0x040020FE RID: 8446
	private int BrojItemaShopBackPack;

	// Token: 0x040020FF RID: 8447
	private int BrojItemaShop;

	// Token: 0x04002100 RID: 8448
	public static List<int> SveStvariZaOblacenjeHats = new List<int>();

	// Token: 0x04002101 RID: 8449
	public static List<int> SveStvariZaOblacenjeShirts = new List<int>();

	// Token: 0x04002102 RID: 8450
	public static List<int> SveStvariZaOblacenjeBackPack = new List<int>();

	// Token: 0x04002103 RID: 8451
	private List<string> ImenaHats;

	// Token: 0x04002104 RID: 8452
	private List<string> ImenaShirts;

	// Token: 0x04002105 RID: 8453
	private List<string> ImenaBackPacks;

	// Token: 0x04002106 RID: 8454
	private List<string> ImenaPowerUps;

	// Token: 0x04002107 RID: 8455
	private string ImeBanana;

	// Token: 0x04002108 RID: 8456
	public List<string> CoinsHats;

	// Token: 0x04002109 RID: 8457
	private List<string> CoinsShirts;

	// Token: 0x0400210A RID: 8458
	private List<string> CoinsBackPacks;

	// Token: 0x0400210B RID: 8459
	private List<string> CoinsPowerUps;

	// Token: 0x0400210C RID: 8460
	private string cenaBanana;

	// Token: 0x0400210D RID: 8461
	private List<string> BananaHats = new List<string>();

	// Token: 0x0400210E RID: 8462
	private List<string> BananaShirts = new List<string>();

	// Token: 0x0400210F RID: 8463
	private List<string> BananaBackPacks = new List<string>();

	// Token: 0x04002110 RID: 8464
	private List<string> PopustHats = new List<string>();

	// Token: 0x04002111 RID: 8465
	private List<string> PopustShirts = new List<string>();

	// Token: 0x04002112 RID: 8466
	private List<string> PopustBackPacks = new List<string>();

	// Token: 0x04002113 RID: 8467
	private List<string> PopustPowerUps = new List<string>();

	// Token: 0x04002114 RID: 8468
	private string PopustBanana;

	// Token: 0x04002115 RID: 8469
	private List<string> UsiHats = new List<string>();

	// Token: 0x04002116 RID: 8470
	private List<string> KosaHats = new List<string>();

	// Token: 0x04002117 RID: 8471
	private float ProcenatOtkljucan;

	// Token: 0x04002118 RID: 8472
	private string StariBrojOtkljucanihItema;

	// Token: 0x04002119 RID: 8473
	private string[] StariBrojOtkljucanihItemaNiz;

	// Token: 0x0400211A RID: 8474
	public static int BrojOtkljucanihMajici;

	// Token: 0x0400211B RID: 8475
	public static int BrojOtkljucanihRanceva;

	// Token: 0x0400211C RID: 8476
	public static int BrojOtkljucanihKapa;

	// Token: 0x0400211D RID: 8477
	public static int StariBrojOtkljucanihMajici;

	// Token: 0x0400211E RID: 8478
	public static int StariBrojOtkljucanihRanceva;

	// Token: 0x0400211F RID: 8479
	public static int StariBrojOtkljucanihKapa;

	// Token: 0x04002120 RID: 8480
	private List<int> ZakljucaniHats = new List<int>();

	// Token: 0x04002121 RID: 8481
	private List<int> ZakljucaniShirts = new List<int>();

	// Token: 0x04002122 RID: 8482
	private List<int> ZakljucaniBackPacks = new List<int>();

	// Token: 0x04002123 RID: 8483
	private GameObject CustomizationHats;

	// Token: 0x04002124 RID: 8484
	private GameObject CustomizationShirts;

	// Token: 0x04002125 RID: 8485
	private GameObject CustomizationBackPack;

	// Token: 0x04002126 RID: 8486
	private GameObject CoinsNumber;

	// Token: 0x04002127 RID: 8487
	private GameObject temp;

	// Token: 0x04002128 RID: 8488
	private bool mozeDaOtvoriSledeciTab = true;

	// Token: 0x04002129 RID: 8489
	private bool kliknuoJednomNaTab = true;

	// Token: 0x0400212A RID: 8490
	public Color[] TShirtColors;
}
