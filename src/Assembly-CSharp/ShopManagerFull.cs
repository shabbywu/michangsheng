using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class ShopManagerFull : MonoBehaviour
{
	public bool EarsAndHairCustomization;

	public Transform[] HatsObjects = (Transform[])(object)new Transform[0];

	public Transform[] ShirtsObjects = (Transform[])(object)new Transform[0];

	public Transform[] BackPacksObjects = (Transform[])(object)new Transform[0];

	public Transform[] PowerUpsObjects = (Transform[])(object)new Transform[0];

	public static int BuyButtonState;

	public static bool PreviewState = false;

	private GameObject ZidFooter;

	private GameObject Custumization;

	private bool ImaNovihMajica;

	private bool ImaNovihKapa;

	private bool ImaNovihRanceva;

	private GameObject ButtonShop;

	private GameObject ButtonShopSprite;

	private GameObject PreviewShopButton;

	private GameObject ShopBanana;

	public static int AktivanSesir;

	public static int AktivnaMajica;

	public static int AktivanRanac;

	private int PreviewSesir;

	private int PreviewMajica;

	private int PreviewRanac;

	public static bool otvorenShop = false;

	public static int AktivanTab;

	public static int AktivanCustomizationTab;

	public static int AktivanItemSesir;

	public static int AktivanItemMajica;

	public static int AktivanItemRanac;

	private int TrenutniSelektovanSesir = 999;

	private int TrenutnoSelektovanaMajica = 999;

	private int TrenutnoSelektovanRanac = 999;

	private string[] Hats;

	private string[] Shirts;

	private string[] BackPacks;

	private string[] AktivniItemi;

	private string AktivniItemString;

	private GameObject MajmunBobo;

	private Vector3 MainScenaPozicija;

	private Vector3 ShopCustomizationPozicija;

	public static bool ImaUsi;

	public static bool ImaKosu;

	public static ShopManagerFull ShopObject;

	private string releasedItem;

	private string clickedItem;

	private Vector3 originalScale;

	private static Color KakiBoja = new Color(0.97255f, 0.79216f, 0.40784f);

	private static Color PopustBoja = new Color(0.11373f, 0.82353f, 0.38039f);

	private static float gornjaGranica;

	private static float donjaGranica;

	private TextAsset aset2;

	private string aset;

	public static bool ShopInicijalizovan = false;

	private int BrojItemaShopHats;

	private int BrojItemaShopShirts;

	private int BrojItemaShopBackPack;

	private int BrojItemaShop;

	public static List<int> SveStvariZaOblacenjeHats = new List<int>();

	public static List<int> SveStvariZaOblacenjeShirts = new List<int>();

	public static List<int> SveStvariZaOblacenjeBackPack = new List<int>();

	private List<string> ImenaHats;

	private List<string> ImenaShirts;

	private List<string> ImenaBackPacks;

	private List<string> ImenaPowerUps;

	private string ImeBanana;

	public List<string> CoinsHats;

	private List<string> CoinsShirts;

	private List<string> CoinsBackPacks;

	private List<string> CoinsPowerUps;

	private string cenaBanana;

	private List<string> BananaHats = new List<string>();

	private List<string> BananaShirts = new List<string>();

	private List<string> BananaBackPacks = new List<string>();

	private List<string> PopustHats = new List<string>();

	private List<string> PopustShirts = new List<string>();

	private List<string> PopustBackPacks = new List<string>();

	private List<string> PopustPowerUps = new List<string>();

	private string PopustBanana;

	private List<string> UsiHats = new List<string>();

	private List<string> KosaHats = new List<string>();

	private float ProcenatOtkljucan;

	private string StariBrojOtkljucanihItema;

	private string[] StariBrojOtkljucanihItemaNiz;

	public static int BrojOtkljucanihMajici;

	public static int BrojOtkljucanihRanceva;

	public static int BrojOtkljucanihKapa;

	public static int StariBrojOtkljucanihMajici;

	public static int StariBrojOtkljucanihRanceva;

	public static int StariBrojOtkljucanihKapa;

	private List<int> ZakljucaniHats = new List<int>();

	private List<int> ZakljucaniShirts = new List<int>();

	private List<int> ZakljucaniBackPacks = new List<int>();

	private GameObject CustomizationHats;

	private GameObject CustomizationShirts;

	private GameObject CustomizationBackPack;

	private GameObject CoinsNumber;

	private GameObject temp;

	private bool mozeDaOtvoriSledeciTab = true;

	private bool kliknuoJednomNaTab = true;

	public Color[] TShirtColors;

	private void Awake()
	{
		ShopObject = this;
		if (EarsAndHairCustomization)
		{
			UsiHats.Add("1");
			UsiHats.Add("1");
			UsiHats.Add("1");
			UsiHats.Add("1");
			UsiHats.Add("1");
			UsiHats.Add("1");
			UsiHats.Add("1");
			UsiHats.Add("1");
			UsiHats.Add("1");
			UsiHats.Add("1");
			UsiHats.Add("0");
			UsiHats.Add("1");
			UsiHats.Add("0");
			UsiHats.Add("1");
			UsiHats.Add("0");
			KosaHats.Add("0");
			KosaHats.Add("0");
			KosaHats.Add("0");
			KosaHats.Add("1");
			KosaHats.Add("0");
			KosaHats.Add("0");
			KosaHats.Add("0");
			KosaHats.Add("0");
			KosaHats.Add("0");
			KosaHats.Add("0");
			KosaHats.Add("0");
			KosaHats.Add("0");
			KosaHats.Add("0");
			KosaHats.Add("0");
			KosaHats.Add("0");
		}
		else
		{
			UsiHats.Add("1");
			UsiHats.Add("1");
			UsiHats.Add("1");
			UsiHats.Add("1");
			UsiHats.Add("1");
			UsiHats.Add("1");
			UsiHats.Add("1");
			UsiHats.Add("1");
			UsiHats.Add("1");
			UsiHats.Add("1");
			UsiHats.Add("1");
			UsiHats.Add("1");
			UsiHats.Add("1");
			UsiHats.Add("1");
			UsiHats.Add("1");
			KosaHats.Add("1");
			KosaHats.Add("1");
			KosaHats.Add("1");
			KosaHats.Add("1");
			KosaHats.Add("1");
			KosaHats.Add("1");
			KosaHats.Add("1");
			KosaHats.Add("1");
			KosaHats.Add("1");
			KosaHats.Add("1");
			KosaHats.Add("1");
			KosaHats.Add("1");
			KosaHats.Add("1");
			KosaHats.Add("1");
			KosaHats.Add("1");
		}
		ButtonShop = GameObject.Find("ButtonBuy");
		ButtonShopSprite = GameObject.Find("Buy Button");
		PreviewShopButton = GameObject.Find("Preview Button");
	}

	private void Start()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		if ((double)Camera.main.aspect < 1.51)
		{
			GameObject.Find("ButtonBackShop").transform.localPosition = new Vector3(-1.58f, -0.8f, 0f);
		}
		ShopBanana = GameObject.Find("Shop Banana");
		AktivanCustomizationTab = 1;
		ZidFooter = GameObject.Find("ZidFooterShop");
		Custumization = GameObject.Find("Custumization");
		((Component)Custumization.transform.Find("Znak Uzvika telo")).gameObject.SetActive(false);
		CoinsNumber = GameObject.Find("Shop/Shop Interface/Coins");
		((Component)CoinsNumber.transform.Find("Coins Number")).GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
		((Component)CoinsNumber.transform.Find("Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Shop/2 Shop - BANANA/Zid Shop/Zid Header i Footer/Zid Footer Shop/Banana Number/Number").GetComponent<TextMesh>().text = StagesParser.currentBananas.ToString();
		GameObject.Find("Shop/2 Shop - BANANA/Zid Shop/Zid Header i Footer/Zid Footer Shop/Banana Number/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Double Coins Number/Number").GetComponent<TextMesh>().text = StagesParser.powerup_doublecoins.ToString();
		GameObject.Find("Double Coins Number/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Magnet Number/Number").GetComponent<TextMesh>().text = StagesParser.powerup_magnets.ToString();
		GameObject.Find("Magnet Number/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Shield Number/Number").GetComponent<TextMesh>().text = StagesParser.powerup_shields.ToString();
		GameObject.Find("Shield Number/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		ImaNovihMajica = false;
		ImaNovihKapa = false;
		ImaNovihRanceva = false;
		if (Application.loadedLevel == 1)
		{
			ShopCustomizationPozicija = new Vector3(6.586132f, -5.05306f, -31.75042f);
		}
		else
		{
			ShopCustomizationPozicija = new Vector3(-16.58703f, -98.95457f, -50f);
		}
		if (PlayerPrefs.HasKey("AktivniItemi"))
		{
			AktivniItemString = PlayerPrefs.GetString("AktivniItemi");
			AktivniItemi = AktivniItemString.Split(new char[1] { '#' });
			AktivanSesir = int.Parse(AktivniItemi[0]);
			AktivnaMajica = int.Parse(AktivniItemi[1]);
			AktivanRanac = int.Parse(AktivniItemi[2]);
		}
		else
		{
			AktivanSesir = -1;
			AktivnaMajica = -1;
			AktivanRanac = -1;
		}
		PreviewSesir = -1;
		PreviewMajica = -1;
		PreviewRanac = -1;
		MajmunBobo = GameObject.Find("MonkeyHolder");
		CustomizationHats = GameObject.Find("1Hats");
		CustomizationShirts = GameObject.Find("2Shirts");
		CustomizationBackPack = GameObject.Find("3BackPack");
		MainScenaPozicija = MajmunBobo.transform.position;
		BrojItemaShopHats = CountItemsInShop(GameObject.Find("Shop/3 Customize/Customize Tabovi/1Hats").GetComponent<Transform>());
		BrojItemaShopShirts = CountItemsInShop(GameObject.Find("Shop/3 Customize/Customize Tabovi/2Shirts").GetComponent<Transform>());
		BrojItemaShopBackPack = CountItemsInShop(GameObject.Find("Shop/3 Customize/Customize Tabovi/3BackPack").GetComponent<Transform>());
		BrojItemaShop = BrojItemaShopHats + BrojItemaShopShirts + BrojItemaShopBackPack;
		ObuciMajmunaNaStartu();
		((Object)((Component)this).transform).name = "Shop";
		SviItemiInvetory();
		((MonoBehaviour)this).StartCoroutine(PokreniInicijalizacijuShopa());
	}

	private void Update()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		if (Input.GetMouseButtonDown(0))
		{
			clickedItem = RaycastFunction(Input.mousePosition);
			if (clickedItem.Equals("NekiNaziv") || clickedItem.Equals("NekiNaziv1"))
			{
				temp = GameObject.Find(clickedItem);
				originalScale = temp.transform.localScale;
				temp.transform.localScale = originalScale * 0.8f;
			}
			else if (clickedItem != string.Empty)
			{
				temp = GameObject.Find(clickedItem);
				originalScale = temp.transform.localScale;
			}
		}
		if (Input.GetMouseButtonUp(0))
		{
			releasedItem = RaycastFunction(Input.mousePosition);
			if (!clickedItem.Equals(string.Empty))
			{
				if ((Object)(object)temp != (Object)null)
				{
					temp.transform.localScale = originalScale;
				}
				if (releasedItem == "NekoDugme" && PlaySounds.soundOn)
				{
					PlaySounds.Play_Button_OpenLevel();
				}
			}
		}
		if (ObjCustomizationHats.CustomizationHats || ObjCustomizationShirts.CustomizationShirts || ObjCustomizationBackPacks.CustomizationBackPacks)
		{
			if (AktivanCustomizationTab == 1)
			{
				ProveraTrenutnogItema(AktivanItemSesir);
			}
			else if (AktivanCustomizationTab == 2)
			{
				ProveraTrenutnogItema(AktivanItemMajica);
			}
			else if (AktivanCustomizationTab == 3)
			{
				ProveraTrenutnogItema(AktivanItemRanac);
			}
		}
	}

	private string RaycastFunction(Vector3 vector)
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

	private int CountItemsInShop(Transform Shop)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		int num = 0;
		foreach (Transform item in Shop)
		{
			_ = item;
			num++;
		}
		return num;
	}

	public void RefresujImenaItema()
	{
		((MonoBehaviour)this).StartCoroutine(ParsirajImenaItemaIzRadnje());
	}

	public void PobrisiSveOtkljucanoIzShopa()
	{
		ZakljucaniHats.Clear();
		ZakljucaniShirts.Clear();
		ZakljucaniBackPacks.Clear();
	}

	private IEnumerator ParsirajImenaItemaIzRadnje()
	{
		yield return null;
		aset2 = (TextAsset)Resources.Load("xmls/Shop/Shop" + LanguageManager.chosenLanguage);
		aset = aset2.text;
		CoinsHats = new List<string>();
		CoinsShirts = new List<string>();
		CoinsBackPacks = new List<string>();
		CoinsPowerUps = new List<string>();
		ImenaHats = new List<string>();
		ImenaShirts = new List<string>();
		ImenaBackPacks = new List<string>();
		ImenaPowerUps = new List<string>();
		IEnumerable<XElement> source = ((XContainer)XElement.Parse(aset.ToString())).Elements();
		source.Count();
		if (StagesParser.unlockedWorlds[4])
		{
			ProcenatOtkljucan = 1f;
		}
		else if (StagesParser.unlockedWorlds[3])
		{
			ProcenatOtkljucan = 0.9f;
		}
		else if (StagesParser.unlockedWorlds[2])
		{
			ProcenatOtkljucan = 0.8f;
		}
		else if (StagesParser.unlockedWorlds[1])
		{
			ProcenatOtkljucan = 0.7f;
		}
		else if (StagesParser.unlockedWorlds[0])
		{
			ProcenatOtkljucan = 0.6f;
		}
		if (PlayerPrefs.HasKey("OtkljucaniItemi"))
		{
			StariBrojOtkljucanihItema = PlayerPrefs.GetString("OtkljucaniItemi");
		}
		else
		{
			StariBrojOtkljucanihItema = "0#0#0";
		}
		StariBrojOtkljucanihItemaNiz = StariBrojOtkljucanihItema.Split(new char[1] { '#' });
		StariBrojOtkljucanihKapa = int.Parse(StariBrojOtkljucanihItemaNiz[0]);
		StariBrojOtkljucanihMajici = int.Parse(StariBrojOtkljucanihItemaNiz[1]);
		StariBrojOtkljucanihRanceva = int.Parse(StariBrojOtkljucanihItemaNiz[2]);
		BrojOtkljucanihKapa = Mathf.FloorToInt((float)BrojItemaShopHats * ProcenatOtkljucan) - 1;
		BrojOtkljucanihMajici = Mathf.FloorToInt((float)BrojItemaShopShirts * ProcenatOtkljucan) - 1;
		BrojOtkljucanihRanceva = Mathf.FloorToInt((float)BrojItemaShopBackPack * ProcenatOtkljucan) - 1;
		StariBrojOtkljucanihItema = BrojOtkljucanihKapa + "#" + BrojOtkljucanihMajici + "#" + BrojOtkljucanihRanceva;
		PlayerPrefs.SetString("OtkljucaniItemi", StariBrojOtkljucanihItema);
		PlayerPrefs.Save();
		for (int i = 0; i < BrojItemaShopHats; i++)
		{
			if (BrojOtkljucanihKapa >= i)
			{
				ZakljucaniHats.Add(1);
			}
			else
			{
				ZakljucaniHats.Add(0);
			}
		}
		for (int j = 0; j < BrojItemaShopShirts; j++)
		{
			if (BrojOtkljucanihMajici >= j)
			{
				ZakljucaniShirts.Add(1);
			}
			else
			{
				ZakljucaniShirts.Add(0);
			}
		}
		for (int k = 0; k < BrojItemaShopBackPack; k++)
		{
			if (BrojOtkljucanihRanceva >= k)
			{
				ZakljucaniBackPacks.Add(1);
			}
			else
			{
				ZakljucaniBackPacks.Add(0);
			}
		}
		if (source.Count() != BrojItemaShop + 4)
		{
			yield break;
		}
		for (int l = 0; l < BrojItemaShopHats; l++)
		{
			if (ZakljucaniHats[l] == 1)
			{
				((Component)HatsObjects[l].Find("Zakkljucano")).gameObject.SetActive(false);
				((Component)HatsObjects[l].Find("Polje za unos COINA U shopu - Shop")).gameObject.SetActive(true);
			}
			else
			{
				((Component)HatsObjects[l].Find("Polje za unos COINA U shopu - Shop")).gameObject.SetActive(false);
				((Component)HatsObjects[l].Find("Bedz - Popust")).gameObject.SetActive(false);
				((Component)HatsObjects[l].Find("Zakkljucano")).gameObject.SetActive(true);
				((Component)HatsObjects[l].Find("Polje za unos COINA U shopu - Shop_NoDiscount")).gameObject.SetActive(false);
			}
			if (!(source.ElementAt(l).Attribute(XName.op_Implicit("kategorija")).Value == "Hats"))
			{
				continue;
			}
			ImenaHats.Add(source.ElementAt(l).Value);
			CoinsHats.Add(source.ElementAt(l).Attribute(XName.op_Implicit("coins")).Value);
			BananaHats.Add(source.ElementAt(l).Attribute(XName.op_Implicit("banana")).Value);
			PopustHats.Add(source.ElementAt(l).Attribute(XName.op_Implicit("popust")).Value);
			if (SveStvariZaOblacenjeHats[l] == 1)
			{
				((Component)HatsObjects[l].Find("Polje za unos COINA U shopu - Shop")).gameObject.SetActive(false);
				((Component)HatsObjects[l].Find("Bedz - Popust")).gameObject.SetActive(false);
				((Component)HatsObjects[l].Find("Polje za unos COINA U shopu - Shop_NoDiscount")).gameObject.SetActive(false);
			}
			else if (PopustHats[l] == "0")
			{
				((Component)HatsObjects[l].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMesh>().text = CoinsHats[l];
				((Component)HatsObjects[l].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
				if ((l > StariBrojOtkljucanihKapa) & (l <= BrojOtkljucanihKapa))
				{
					ImaNovihKapa = true;
					((Component)HatsObjects[l].Find("Bedz - Popust/Text/Number")).GetComponent<TextMesh>().text = LanguageManager.New;
					((Component)HatsObjects[l].Find("Bedz - Popust/Text/Number/Number Shadow")).GetComponent<TextMesh>().text = LanguageManager.New;
				}
				else
				{
					((Component)HatsObjects[l].Find("Bedz - Popust")).gameObject.SetActive(false);
				}
			}
			else if ((l > StariBrojOtkljucanihKapa) & (l <= BrojOtkljucanihKapa))
			{
				ImaNovihKapa = true;
				((Component)HatsObjects[l].Find("Bedz - Popust/Text/Number")).GetComponent<TextMesh>().text = LanguageManager.New;
				((Component)HatsObjects[l].Find("Bedz - Popust/Text/Number/Number Shadow")).GetComponent<TextMesh>().text = LanguageManager.New;
				if (ZakljucaniHats[l] == 1)
				{
					((Component)HatsObjects[l].Find("Polje za unos COINA U shopu - Shop_NoDiscount")).gameObject.SetActive(true);
					((Component)HatsObjects[l].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number")).GetComponent<TextMesh>().text = CoinsHats[l];
					if (((Component)HatsObjects[l].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number")).gameObject.activeSelf)
					{
						((Component)HatsObjects[l].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
					}
					string s = "0." + PopustHats[l];
					float num = float.Parse(CoinsHats[l]) - float.Parse(CoinsHats[l]) * float.Parse(s);
					CoinsHats[l] = num.ToString();
					((Component)HatsObjects[l].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMesh>().text = num.ToString();
					((Component)HatsObjects[l].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
					((Component)HatsObjects[l].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMesh>().color = PopustBoja;
				}
			}
			else if (ZakljucaniHats[l] == 1)
			{
				((Component)HatsObjects[l].Find("Polje za unos COINA U shopu - Shop_NoDiscount")).gameObject.SetActive(true);
				((Component)HatsObjects[l].Find("Bedz - Popust/Text/Number")).GetComponent<TextMesh>().text = "-" + PopustHats[l] + "%";
				((Component)HatsObjects[l].Find("Bedz - Popust/Text/Number/Number Shadow")).GetComponent<TextMesh>().text = PopustHats[l] + "%";
				((Component)HatsObjects[l].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number")).GetComponent<TextMesh>().text = CoinsHats[l];
				if (((Component)HatsObjects[l].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number")).gameObject.activeSelf)
				{
					((Component)HatsObjects[l].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
				}
				string s2 = "0." + PopustHats[l];
				float num2 = float.Parse(CoinsHats[l]) - float.Parse(CoinsHats[l]) * float.Parse(s2);
				CoinsHats[l] = num2.ToString();
				((Component)HatsObjects[l].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMesh>().text = num2.ToString();
				((Component)HatsObjects[l].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
				((Component)HatsObjects[l].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMesh>().color = PopustBoja;
			}
			((Component)HatsObjects[l].Find("Text/ime")).GetComponent<TextMesh>().text = ImenaHats[l];
			((Component)HatsObjects[l].Find("Text/ime")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		}
		for (int m = 0; m < BrojItemaShopShirts; m++)
		{
			if (ZakljucaniShirts[m] == 1)
			{
				((Component)ShirtsObjects[m].Find("Zakkljucano")).gameObject.SetActive(false);
				((Component)ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop")).gameObject.SetActive(true);
			}
			else
			{
				((Component)ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop")).gameObject.SetActive(false);
				((Component)ShirtsObjects[m].Find("Bedz - Popust")).gameObject.SetActive(false);
				((Component)ShirtsObjects[m].Find("Zakkljucano")).gameObject.SetActive(true);
				((Component)ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop_NoDiscount")).gameObject.SetActive(false);
			}
			if (!(source.ElementAt(BrojItemaShopHats + m).Attribute(XName.op_Implicit("kategorija")).Value == "Shirts"))
			{
				continue;
			}
			ImenaShirts.Add(source.ElementAt(BrojItemaShopHats + m).Value);
			CoinsShirts.Add(source.ElementAt(BrojItemaShopHats + m).Attribute(XName.op_Implicit("coins")).Value);
			BananaShirts.Add(source.ElementAt(BrojItemaShopHats + m).Attribute(XName.op_Implicit("banana")).Value);
			PopustShirts.Add(source.ElementAt(BrojItemaShopHats + m).Attribute(XName.op_Implicit("popust")).Value);
			if (SveStvariZaOblacenjeShirts[m] == 1)
			{
				((Component)ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop")).gameObject.SetActive(false);
				((Component)ShirtsObjects[m].Find("Bedz - Popust")).gameObject.SetActive(false);
				((Component)ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop_NoDiscount")).gameObject.SetActive(false);
			}
			else if (PopustShirts[m] == "0")
			{
				((Component)ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMesh>().text = CoinsShirts[m];
				((Component)ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
				if ((m > StariBrojOtkljucanihMajici) & (m <= BrojOtkljucanihMajici))
				{
					ImaNovihMajica = true;
					((Component)ShirtsObjects[m].Find("Bedz - Popust/Text/Number")).GetComponent<TextMesh>().text = LanguageManager.New;
					((Component)ShirtsObjects[m].Find("Bedz - Popust/Text/Number/Number Shadow")).GetComponent<TextMesh>().text = LanguageManager.New;
				}
				else
				{
					((Component)ShirtsObjects[m].Find("Bedz - Popust")).gameObject.SetActive(false);
				}
			}
			else if ((m > StariBrojOtkljucanihMajici) & (m <= BrojOtkljucanihMajici))
			{
				ImaNovihMajica = true;
				((Component)ShirtsObjects[m].Find("Bedz - Popust/Text/Number")).GetComponent<TextMesh>().text = LanguageManager.New;
				((Component)ShirtsObjects[m].Find("Bedz - Popust/Text/Number/Number Shadow")).GetComponent<TextMesh>().text = LanguageManager.New;
				if (ZakljucaniShirts[m] == 1)
				{
					((Component)ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop_NoDiscount")).gameObject.SetActive(true);
					((Component)ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number")).GetComponent<TextMesh>().text = CoinsShirts[m].ToString();
					if (((Component)ShirtsObjects[m].parent).gameObject.activeSelf)
					{
						((Component)ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
					}
					string s3 = "0." + PopustShirts[m];
					float num3 = float.Parse(CoinsShirts[m]) - float.Parse(CoinsShirts[m]) * float.Parse(s3);
					CoinsShirts[m] = num3.ToString();
					((Component)ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMesh>().text = num3.ToString();
					((Component)ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
					((Component)ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMesh>().color = PopustBoja;
				}
			}
			else if (ZakljucaniShirts[m] == 1)
			{
				((Component)ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop_NoDiscount")).gameObject.SetActive(true);
				((Component)ShirtsObjects[m].Find("Bedz - Popust/Text/Number")).GetComponent<TextMesh>().text = "-" + PopustShirts[m] + "%";
				((Component)ShirtsObjects[m].Find("Bedz - Popust/Text/Number/Number Shadow")).GetComponent<TextMesh>().text = PopustShirts[m] + "%";
				((Component)ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number")).GetComponent<TextMesh>().text = CoinsShirts[m].ToString();
				if (((Component)ShirtsObjects[m].parent).gameObject.activeSelf)
				{
					((Component)ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
				}
				string s4 = "0." + PopustShirts[m];
				float num4 = float.Parse(CoinsShirts[m]) - float.Parse(CoinsShirts[m]) * float.Parse(s4);
				CoinsShirts[m] = num4.ToString();
				((Component)ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMesh>().text = num4.ToString();
				((Component)ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
				((Component)ShirtsObjects[m].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMesh>().color = PopustBoja;
			}
			((Component)ShirtsObjects[m].Find("Text/ime")).GetComponent<TextMesh>().text = ImenaShirts[m];
			((Component)ShirtsObjects[m].Find("Text/ime")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		}
		for (int n = 0; n < BrojItemaShopBackPack; n++)
		{
			if (ZakljucaniBackPacks[n] == 1)
			{
				((Component)BackPacksObjects[n].Find("Zakkljucano")).gameObject.SetActive(false);
				((Component)BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop")).gameObject.SetActive(true);
			}
			else
			{
				((Component)BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop")).gameObject.SetActive(false);
				((Component)BackPacksObjects[n].Find("Bedz - Popust")).gameObject.SetActive(false);
				((Component)BackPacksObjects[n].Find("Zakkljucano")).gameObject.SetActive(true);
				((Component)BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop_NoDiscount")).gameObject.SetActive(false);
			}
			if (!(source.ElementAt(BrojItemaShopHats + BrojItemaShopShirts + n).Attribute(XName.op_Implicit("kategorija")).Value == "BackPack"))
			{
				continue;
			}
			ImenaBackPacks.Add(source.ElementAt(BrojItemaShopHats + BrojItemaShopShirts + n).Value);
			CoinsBackPacks.Add(source.ElementAt(BrojItemaShopHats + BrojItemaShopShirts + n).Attribute(XName.op_Implicit("coins")).Value);
			BananaBackPacks.Add(source.ElementAt(BrojItemaShopHats + BrojItemaShopShirts + n).Attribute(XName.op_Implicit("banana")).Value);
			PopustBackPacks.Add(source.ElementAt(BrojItemaShopHats + BrojItemaShopShirts + n).Attribute(XName.op_Implicit("popust")).Value);
			if (SveStvariZaOblacenjeBackPack[n] == 1)
			{
				((Component)BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop")).gameObject.SetActive(false);
				((Component)BackPacksObjects[n].Find("Bedz - Popust")).gameObject.SetActive(false);
				((Component)BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop_NoDiscount")).gameObject.SetActive(false);
			}
			else if (PopustBackPacks[n] == "0")
			{
				((Component)BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMesh>().text = CoinsBackPacks[n];
				((Component)BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
				if ((n > StariBrojOtkljucanihRanceva) & (n <= BrojOtkljucanihRanceva))
				{
					ImaNovihRanceva = true;
					((Component)BackPacksObjects[n].Find("Bedz - Popust/Text/Number")).GetComponent<TextMesh>().text = LanguageManager.New;
					((Component)BackPacksObjects[n].Find("Bedz - Popust/Text/Number/Number Shadow")).GetComponent<TextMesh>().text = LanguageManager.New;
				}
				else
				{
					((Component)BackPacksObjects[n].Find("Bedz - Popust")).gameObject.SetActive(false);
				}
			}
			else
			{
				((Component)BackPacksObjects[n].Find("Bedz - Popust/Text/Number")).GetComponent<TextMesh>().text = "-" + PopustBackPacks[n] + "%";
				((Component)BackPacksObjects[n].Find("Bedz - Popust/Text/Number/Number Shadow")).GetComponent<TextMesh>().text = PopustBackPacks[n] + "%";
				if ((n > StariBrojOtkljucanihRanceva) & (n <= BrojOtkljucanihRanceva))
				{
					ImaNovihRanceva = true;
					((Component)BackPacksObjects[n].Find("Bedz - Popust/Text/Number")).GetComponent<TextMesh>().text = LanguageManager.New;
					((Component)BackPacksObjects[n].Find("Bedz - Popust/Text/Number/Number Shadow")).GetComponent<TextMesh>().text = LanguageManager.New;
					if (ZakljucaniBackPacks[n] == 1)
					{
						((Component)BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop_NoDiscount")).gameObject.SetActive(true);
						((Component)BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number")).GetComponent<TextMesh>().text = CoinsBackPacks[n].ToString();
						if (((Component)BackPacksObjects[n].parent).gameObject.activeSelf)
						{
							((Component)BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
						}
						string s5 = "0." + PopustBackPacks[n];
						float num5 = float.Parse(CoinsBackPacks[n]) - float.Parse(CoinsBackPacks[n]) * float.Parse(s5);
						CoinsBackPacks[n] = num5.ToString();
						((Component)BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMesh>().text = num5.ToString();
						((Component)BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
						((Component)BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMesh>().color = PopustBoja;
					}
				}
				else if (ZakljucaniBackPacks[n] == 1)
				{
					((Component)BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop_NoDiscount")).gameObject.SetActive(true);
					((Component)BackPacksObjects[n].Find("Bedz - Popust/Text/Number")).GetComponent<TextMesh>().text = "-" + PopustBackPacks[n] + "%";
					((Component)BackPacksObjects[n].Find("Bedz - Popust/Text/Number/Number Shadow")).GetComponent<TextMesh>().text = PopustBackPacks[n] + "%";
					((Component)BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number")).GetComponent<TextMesh>().text = CoinsBackPacks[n].ToString();
					if (((Component)BackPacksObjects[n].parent).gameObject.activeSelf)
					{
						((Component)BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
					}
					string s6 = "0." + PopustBackPacks[n];
					float num6 = float.Parse(CoinsBackPacks[n]) - float.Parse(CoinsBackPacks[n]) * float.Parse(s6);
					CoinsBackPacks[n] = num6.ToString();
					((Component)BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMesh>().text = num6.ToString();
					((Component)BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
					((Component)BackPacksObjects[n].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMesh>().color = PopustBoja;
				}
			}
			((Component)BackPacksObjects[n].Find("Text/ime")).GetComponent<TextMesh>().text = ImenaBackPacks[n];
			((Component)BackPacksObjects[n].Find("Text/ime")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		}
		for (int num7 = 0; num7 < 3; num7++)
		{
			if (source.ElementAt(BrojItemaShopHats + BrojItemaShopShirts + BrojItemaShopBackPack + num7).Attribute(XName.op_Implicit("kategorija")).Value == "PowerUps")
			{
				ImenaPowerUps.Add(source.ElementAt(BrojItemaShopHats + BrojItemaShopShirts + BrojItemaShopBackPack + num7).Value);
				CoinsPowerUps.Add(source.ElementAt(BrojItemaShopHats + BrojItemaShopShirts + BrojItemaShopBackPack + num7).Attribute(XName.op_Implicit("coins")).Value);
				PopustPowerUps.Add(source.ElementAt(BrojItemaShopHats + BrojItemaShopShirts + BrojItemaShopBackPack + num7).Attribute(XName.op_Implicit("popust")).Value);
				if (PopustPowerUps[num7] == "0")
				{
					((Component)PowerUpsObjects[num7].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMesh>().text = CoinsPowerUps[num7];
					((Component)PowerUpsObjects[num7].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
					((Component)PowerUpsObjects[num7].Find("Popust")).gameObject.SetActive(false);
				}
				else
				{
					((Component)PowerUpsObjects[num7].Find("Popust/Text/Number")).GetComponent<TextMesh>().text = "-" + PopustPowerUps[num7] + "%";
					((Component)PowerUpsObjects[num7].Find("Popust/Text/Number/Number Shadow")).GetComponent<TextMesh>().text = PopustPowerUps[num7] + "%";
					string s7 = "0." + PopustPowerUps[num7];
					float num8 = float.Parse(CoinsPowerUps[num7]) - float.Parse(CoinsPowerUps[num7]) * float.Parse(s7);
					CoinsPowerUps[num7] = num8.ToString();
					((Component)PowerUpsObjects[num7].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMesh>().text = num8.ToString();
					((Component)PowerUpsObjects[num7].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
					((Component)PowerUpsObjects[num7].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMesh>().color = PopustBoja;
				}
				((Component)PowerUpsObjects[num7].Find("Text/ime")).GetComponent<TextMesh>().text = ImenaPowerUps[num7];
				((Component)PowerUpsObjects[num7].Find("Text/ime")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
			}
		}
		StagesParser.cost_doublecoins = int.Parse(CoinsPowerUps[0]);
		StagesParser.cost_magnet = int.Parse(CoinsPowerUps[1]);
		StagesParser.cost_shield = int.Parse(CoinsPowerUps[2]);
		if (source.ElementAt(BrojItemaShopHats + BrojItemaShopShirts + BrojItemaShopBackPack + 3).Attribute(XName.op_Implicit("kategorija")).Value == "Banana")
		{
			ImeBanana = source.ElementAt(BrojItemaShopHats + BrojItemaShopShirts + BrojItemaShopBackPack + 3).Value;
			cenaBanana = source.ElementAt(BrojItemaShopHats + BrojItemaShopShirts + BrojItemaShopBackPack + 3).Attribute(XName.op_Implicit("coins")).Value;
			PopustBanana = source.ElementAt(BrojItemaShopHats + BrojItemaShopShirts + BrojItemaShopBackPack + 3).Attribute(XName.op_Implicit("popust")).Value;
			string s8 = "0." + PopustBanana;
			float num9 = float.Parse(cenaBanana) - float.Parse(cenaBanana) * float.Parse(s8);
			cenaBanana = num9.ToString();
			StagesParser.bananaCost = (int)num9;
			if (int.Parse(PopustBanana) > 0)
			{
				((Component)ShopBanana.transform.Find("Popust")).gameObject.SetActive(true);
				((Component)ShopBanana.transform.Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMesh>().color = PopustBoja;
				((Component)ShopBanana.transform.Find("Popust/Text/Number")).GetComponent<TextMesh>().text = PopustBanana + "%";
				((Component)ShopBanana.transform.Find("Popust/Text/Number/Number Shadow")).GetComponent<TextMesh>().text = PopustBanana + "%";
			}
			else
			{
				((Component)ShopBanana.transform.Find("Popust")).gameObject.SetActive(false);
			}
			((Component)ShopBanana.transform.Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMesh>().text = cenaBanana;
			((Component)ShopBanana.transform.Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
			((Component)ShopBanana.transform.Find("Text/Banana")).GetComponent<TextMesh>().text = ImeBanana;
			((Component)ShopBanana.transform.Find("Text/Banana")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		}
		if (ImaNovihKapa)
		{
			((Component)ZidFooter.transform.Find("1HatsShopTab/Znak Uzvika telo")).gameObject.SetActive(true);
		}
		else
		{
			((Component)ZidFooter.transform.Find("1HatsShopTab/Znak Uzvika telo")).gameObject.SetActive(false);
		}
		if (ImaNovihMajica)
		{
			((Component)ZidFooter.transform.Find("2TShirtsShopTab/Znak Uzvika telo")).gameObject.SetActive(true);
		}
		else
		{
			((Component)ZidFooter.transform.Find("2TShirtsShopTab/Znak Uzvika telo")).gameObject.SetActive(false);
		}
		if (ImaNovihRanceva)
		{
			((Component)ZidFooter.transform.Find("3BackPackShopTab/Znak Uzvika telo")).gameObject.SetActive(true);
		}
		else
		{
			((Component)ZidFooter.transform.Find("3BackPackShopTab/Znak Uzvika telo")).gameObject.SetActive(false);
		}
		if (ImaNovihKapa | ImaNovihMajica | ImaNovihRanceva)
		{
			((Component)Custumization.transform.Find("Znak Uzvika telo")).gameObject.SetActive(true);
			Custumization.GetComponent<Animation>().PlayQueued("Button Customization Idle", (QueueMode)0);
		}
		else
		{
			((Component)Custumization.transform.Find("Znak Uzvika telo")).gameObject.SetActive(false);
		}
	}

	public void SviItemiInvetory()
	{
		SveStvariZaOblacenjeHats.Clear();
		SveStvariZaOblacenjeShirts.Clear();
		SveStvariZaOblacenjeBackPack.Clear();
		Hats = StagesParser.svekupovineGlava.Split(new char[1] { '#' });
		Shirts = StagesParser.svekupovineMajica.Split(new char[1] { '#' });
		BackPacks = StagesParser.svekupovineLedja.Split(new char[1] { '#' });
		for (int i = 0; i < BrojItemaShopHats; i++)
		{
			if (Hats.Length - 1 > i)
			{
				SveStvariZaOblacenjeHats.Add(int.Parse(Hats[i]));
			}
			else
			{
				SveStvariZaOblacenjeHats.Add(0);
			}
			if (SveStvariZaOblacenjeHats[i] == 0)
			{
				((Component)HatsObjects[i].Find("Stikla")).gameObject.SetActive(false);
			}
			else
			{
				((Component)HatsObjects[i].Find("Stikla")).gameObject.SetActive(true);
			}
		}
		for (int j = 0; j < BrojItemaShopShirts; j++)
		{
			if (Shirts.Length - 1 > j)
			{
				SveStvariZaOblacenjeShirts.Add(int.Parse(Shirts[j]));
			}
			else
			{
				SveStvariZaOblacenjeShirts.Add(0);
			}
			if (SveStvariZaOblacenjeShirts[j] == 0)
			{
				((Component)ShirtsObjects[j].Find("Stikla")).gameObject.SetActive(false);
			}
			else
			{
				((Component)ShirtsObjects[j].Find("Stikla")).gameObject.SetActive(true);
			}
		}
		for (int k = 0; k < BrojItemaShopBackPack; k++)
		{
			if (BackPacks.Length - 1 > k)
			{
				SveStvariZaOblacenjeBackPack.Add(int.Parse(BackPacks[k]));
			}
			else
			{
				SveStvariZaOblacenjeBackPack.Add(0);
			}
			if (SveStvariZaOblacenjeBackPack[k] == 0)
			{
				((Component)BackPacksObjects[k].Find("Stikla")).gameObject.SetActive(false);
			}
			else
			{
				((Component)BackPacksObjects[k].Find("Stikla")).gameObject.SetActive(true);
			}
		}
		ShopInicijalizovan = true;
		PokreniShop();
		Hats = null;
		Shirts = null;
		BackPacks = null;
	}

	private IEnumerator PokreniInicijalizacijuShopa()
	{
		if (FacebookManager.KorisnikoviPodaciSpremni)
		{
			((MonoBehaviour)this).StartCoroutine(ParsirajImenaItemaIzRadnje());
			yield break;
		}
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
		((MonoBehaviour)this).StartCoroutine(PokreniInicijalizacijuShopa());
	}

	public void PokreniShop()
	{
		if (!ShopInicijalizovan)
		{
			((MonoBehaviour)this).StartCoroutine(PokreniInicijalizacijuShopa());
		}
	}

	public void SkloniShop()
	{
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		if (Application.loadedLevel == 1)
		{
			((Component)MajmunBobo.transform.Find("PrinceGorilla")).GetComponent<Animator>().Play("Idle Main Screen");
			((Component)MajmunBobo.transform.Find("ButterflyHolder")).gameObject.SetActive(true);
			if (AktivanRanac == 0)
			{
				((Component)((Component)MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + AktivanRanac)).transform.GetChild(0)).GetComponent<MeshFilter>().mesh = GameObject.Find("RefZaSedenje_AndjeoskaKrila").GetComponent<MeshFilter>().mesh;
			}
			else if (AktivanRanac == 5)
			{
				((Component)((Component)MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + AktivanRanac)).transform.GetChild(0)).GetComponent<MeshFilter>().mesh = GameObject.Find("RefZaSedenje_SlepiMisKrila").GetComponent<MeshFilter>().mesh;
			}
		}
		otvorenShop = false;
		OcistiPreview();
		DeaktivirajCustomization();
		MajmunBobo.transform.position = MainScenaPozicija;
		MajmunBobo.transform.Find("PrinceGorilla").rotation = Quaternion.Euler(new Vector3(0f, 104f, 0f));
		DeaktivirajFreeCoins();
		DeaktivirajPowerUps();
		DeaktivirajShopTab();
		if (PlayerPrefs.HasKey("OtkljucaniItemi"))
		{
			StariBrojOtkljucanihItema = PlayerPrefs.GetString("OtkljucaniItemi");
		}
		else
		{
			StariBrojOtkljucanihItema = "0#0#0";
		}
		StariBrojOtkljucanihItemaNiz = StariBrojOtkljucanihItema.Split(new char[1] { '#' });
		StariBrojOtkljucanihKapa = int.Parse(StariBrojOtkljucanihItemaNiz[0]);
		StariBrojOtkljucanihMajici = int.Parse(StariBrojOtkljucanihItemaNiz[1]);
		StariBrojOtkljucanihRanceva = int.Parse(StariBrojOtkljucanihItemaNiz[2]);
		if (ImaNovihKapa)
		{
			BrojOtkljucanihKapa = Mathf.FloorToInt((float)BrojItemaShopHats * ProcenatOtkljucan) - 1;
		}
		else
		{
			BrojOtkljucanihKapa = StariBrojOtkljucanihKapa;
		}
		if (ImaNovihMajica)
		{
			BrojOtkljucanihMajici = Mathf.FloorToInt((float)BrojItemaShopShirts * ProcenatOtkljucan) - 1;
		}
		else
		{
			BrojOtkljucanihMajici = StariBrojOtkljucanihMajici;
		}
		if (ImaNovihRanceva)
		{
			BrojOtkljucanihRanceva = Mathf.FloorToInt((float)BrojItemaShopBackPack * ProcenatOtkljucan) - 1;
		}
		else
		{
			BrojOtkljucanihRanceva = StariBrojOtkljucanihRanceva;
		}
		if (!ImaNovihKapa && !ImaNovihMajica && !ImaNovihRanceva)
		{
			StariBrojOtkljucanihItema = BrojOtkljucanihKapa + "#" + BrojOtkljucanihMajici + "#" + BrojOtkljucanihRanceva;
			PlayerPrefs.SetString("OtkljucaniItemi", StariBrojOtkljucanihItema);
			PlayerPrefs.Save();
		}
		ProveriStanjeCelogShopa();
		GameObject.Find("Shop").GetComponent<Animation>().Play("MeniOdlazak");
		if (AktivanTab == 1)
		{
			GameObject.Find("ButtonFreeCoins").GetComponent<SpriteRenderer>().sprite = GameObject.Find("ShopTab").GetComponent<SpriteRenderer>().sprite;
		}
		else if (AktivanTab == 2)
		{
			GameObject.Find("ButtonShop").GetComponent<SpriteRenderer>().sprite = GameObject.Find("ShopTab").GetComponent<SpriteRenderer>().sprite;
		}
		else if (AktivanTab == 3)
		{
			GameObject.Find("ButtonCustomize").GetComponent<SpriteRenderer>().sprite = GameObject.Find("ShopTab").GetComponent<SpriteRenderer>().sprite;
		}
		else if (AktivanTab == 4)
		{
			GameObject.Find("ButtonPowerUps").GetComponent<SpriteRenderer>().sprite = GameObject.Find("ShopTab").GetComponent<SpriteRenderer>().sprite;
		}
		AktivanTab = 0;
		AktivanItemSesir = 998;
		AktivanItemMajica = 998;
		AktivanItemRanac = 998;
	}

	public void PozoviTab(int RedniBrojTaba)
	{
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0514: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		//IL_057b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0594: Unknown result type (might be due to invalid IL or missing references)
		if (mozeDaOtvoriSledeciTab && kliknuoJednomNaTab)
		{
			mozeDaOtvoriSledeciTab = false;
			kliknuoJednomNaTab = false;
			if (RedniBrojTaba == 3)
			{
				((MonoBehaviour)this).Invoke("MozeDaKliknePonovoNaTab", 1.5f);
			}
			else
			{
				((MonoBehaviour)this).Invoke("MozeDaKliknePonovoNaTab", 0.75f);
			}
			if (StagesParser.otvaraoShopNekad == 0)
			{
				StagesParser.otvaraoShopNekad = 1;
				PlayerPrefs.SetString("OdgledaoTutorial", StagesParser.odgledaoTutorial + "#" + StagesParser.otvaraoShopNekad);
				PlayerPrefs.Save();
			}
			otvorenShop = true;
			CustomizationShirts.SetActive(false);
			CustomizationBackPack.SetActive(false);
			((Component)CoinsNumber.transform.Find("Coins Number")).GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
			((Component)CoinsNumber.transform.Find("Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
			if (AktivanTab != RedniBrojTaba)
			{
				if (AktivanTab == 1)
				{
					DeaktivirajFreeCoins();
					GameObject.Find("ButtonFreeCoins").GetComponent<SpriteRenderer>().sprite = GameObject.Find("ShopTab").GetComponent<SpriteRenderer>().sprite;
					GameObject.Find("Shop/1 Free Coins").GetComponent<Animation>().Play("TabOdlazak");
				}
				else if (AktivanTab == 2)
				{
					DeaktivirajShopTab();
					GameObject.Find("ButtonShop").GetComponent<SpriteRenderer>().sprite = GameObject.Find("ShopTab").GetComponent<SpriteRenderer>().sprite;
					GameObject.Find("Shop/2 Shop - BANANA").GetComponent<Animation>().Play("TabOdlazak");
				}
				else if (AktivanTab == 3)
				{
					DeaktivirajCustomization();
					GameObject.Find("ButtonCustomize").GetComponent<SpriteRenderer>().sprite = GameObject.Find("ShopTab").GetComponent<SpriteRenderer>().sprite;
					GameObject.Find("Shop/3 Customize").GetComponent<Animation>().Play("TabOdlazak");
					MajmunBobo.transform.position = MainScenaPozicija;
				}
				else if (AktivanTab == 4)
				{
					DeaktivirajPowerUps();
					GameObject.Find("ButtonPowerUps").GetComponent<SpriteRenderer>().sprite = GameObject.Find("ShopTab").GetComponent<SpriteRenderer>().sprite;
					GameObject.Find("Shop/4 Power-Ups").GetComponent<Animation>().Play("TabOdlazak");
				}
				AktivanTab = RedniBrojTaba;
				if (AktivanTab == 1)
				{
					if (PlayerPrefs.HasKey("LikeBananaIsland"))
					{
						((Component)((Component)ShopObject).transform.Find("1 Free Coins/Free Coins Tabovi/ShopFCBILikePage")).GetComponent<Collider>().enabled = false;
						((Component)((Component)ShopObject).transform.Find("1 Free Coins/Free Coins Tabovi/ShopFCBILikePage")).GetComponent<Renderer>().material.color = new Color(0.58f, 0.58f, 0.58f);
						((Component)((Component)ShopObject).transform.Find("1 Free Coins/Free Coins Tabovi/ShopFCBILikePage/Like BananaIsland FC")).GetComponent<Renderer>().material.color = new Color(0.58f, 0.58f, 0.58f);
					}
					if (PlayerPrefs.HasKey("LikeWebelinx"))
					{
						((Component)((Component)ShopObject).transform.Find("1 Free Coins/Free Coins Tabovi/ShopFCWLLikePage")).GetComponent<Collider>().enabled = false;
						((Component)((Component)ShopObject).transform.Find("1 Free Coins/Free Coins Tabovi/ShopFCWLLikePage")).GetComponent<Renderer>().material.color = new Color(0.58f, 0.58f, 0.58f);
						((Component)((Component)ShopObject).transform.Find("1 Free Coins/Free Coins Tabovi/ShopFCWLLikePage/Like Webelinx FC")).GetComponent<Renderer>().material.color = new Color(0.58f, 0.58f, 0.58f);
					}
					GameObject.Find("ButtonFreeCoins").GetComponent<SpriteRenderer>().sprite = GameObject.Find("ShopTabSelected").GetComponent<SpriteRenderer>().sprite;
					GameObject.Find("Shop/1 Free Coins").GetComponent<Animation>().Play("TabDolazak");
					AktivirajFreeCoins();
				}
				else if (AktivanTab == 2)
				{
					AktivirajShopTab();
					GameObject.Find("ButtonShop").GetComponent<SpriteRenderer>().sprite = GameObject.Find("ShopTabSelected").GetComponent<SpriteRenderer>().sprite;
					GameObject.Find("Shop/2 Shop - BANANA").GetComponent<Animation>().Play("TabDolazak");
				}
				else if (AktivanTab == 3)
				{
					if (AktivanCustomizationTab == 1)
					{
						GameObject.Find("1HatsShopTab").GetComponent<SpriteRenderer>().color = Color.green;
						GameObject.Find("2TShirtsShopTab").GetComponent<SpriteRenderer>().color = KakiBoja;
						GameObject.Find("3BackPackShopTab").GetComponent<SpriteRenderer>().color = KakiBoja;
						ImaNovihKapa = false;
						CustomizationHats.SetActive(true);
						CustomizationShirts.SetActive(false);
						CustomizationBackPack.SetActive(false);
					}
					else if (AktivanCustomizationTab == 2)
					{
						GameObject.Find("1HatsShopTab").GetComponent<SpriteRenderer>().color = KakiBoja;
						GameObject.Find("2TShirtsShopTab").GetComponent<SpriteRenderer>().color = Color.green;
						GameObject.Find("3BackPackShopTab").GetComponent<SpriteRenderer>().color = KakiBoja;
						ImaNovihMajica = false;
						CustomizationHats.SetActive(false);
						CustomizationShirts.SetActive(true);
						CustomizationBackPack.SetActive(false);
					}
					else if (AktivanCustomizationTab == 3)
					{
						GameObject.Find("1HatsShopTab").GetComponent<SpriteRenderer>().color = KakiBoja;
						GameObject.Find("2TShirtsShopTab").GetComponent<SpriteRenderer>().color = KakiBoja;
						GameObject.Find("3BackPackShopTab").GetComponent<SpriteRenderer>().color = Color.green;
						ImaNovihRanceva = false;
						CustomizationHats.SetActive(false);
						CustomizationShirts.SetActive(false);
						CustomizationBackPack.SetActive(true);
					}
					GameObject.Find("ButtonCustomize").GetComponent<SpriteRenderer>().sprite = GameObject.Find("ShopTabSelected").GetComponent<SpriteRenderer>().sprite;
					GameObject.Find("Shop/3 Customize").GetComponent<Animation>().Play("TabDolazak");
					((MonoBehaviour)this).Invoke("AktivirajCustomization", 0.4f);
					((Component)MajmunBobo.transform.Find("PrinceGorilla")).GetComponent<Animator>().Play("Povlacenje");
					((Component)MajmunBobo.transform.Find("ButterflyHolder")).gameObject.SetActive(false);
				}
				else if (AktivanTab == 4)
				{
					AktivirajPowerUps();
					GameObject.Find("ButtonPowerUps").GetComponent<SpriteRenderer>().sprite = GameObject.Find("ShopTabSelected").GetComponent<SpriteRenderer>().sprite;
					GameObject.Find("Shop/4 Power-Ups").GetComponent<Animation>().Play("TabDolazak");
				}
			}
			else
			{
				_ = 3;
			}
		}
		else if (!mozeDaOtvoriSledeciTab && kliknuoJednomNaTab)
		{
			kliknuoJednomNaTab = false;
		}
	}

	private void MozeDaKliknePonovoNaTab()
	{
		mozeDaOtvoriSledeciTab = true;
		kliknuoJednomNaTab = true;
	}

	public void PozoviCustomizationTab(int RedniBrojCustomizationTaba)
	{
		((MonoBehaviour)this).StopCoroutine("CustomizationTab");
		((MonoBehaviour)this).StartCoroutine("CustomizationTab", (object)RedniBrojCustomizationTaba);
	}

	public IEnumerator CustomizationTab(int RedniBrojCustomizationTaba1)
	{
		if (AktivanCustomizationTab != RedniBrojCustomizationTaba1)
		{
			if (AktivanCustomizationTab == 1)
			{
				GameObject.Find("1HatsShopTab").GetComponent<SpriteRenderer>().color = KakiBoja;
				CustomizationHats.SetActive(false);
				ImaNovihKapa = false;
			}
			else if (AktivanCustomizationTab == 2)
			{
				GameObject.Find("2TShirtsShopTab").GetComponent<SpriteRenderer>().color = KakiBoja;
				CustomizationShirts.SetActive(false);
				ImaNovihMajica = false;
			}
			else if (AktivanCustomizationTab == 3)
			{
				GameObject.Find("3BackPackShopTab").GetComponent<SpriteRenderer>().color = KakiBoja;
				CustomizationBackPack.SetActive(false);
				ImaNovihRanceva = false;
			}
			yield return (object)new WaitForSeconds(0.15f);
			AktivanCustomizationTab = RedniBrojCustomizationTaba1;
			if (AktivanCustomizationTab == 1)
			{
				TrenutniSelektovanSesir = 999;
				GameObject.Find("1HatsShopTab").GetComponent<SpriteRenderer>().color = Color.green;
				CustomizationHats.SetActive(true);
				ImaNovihKapa = false;
				Quaternion a3 = Quaternion.Euler(new Vector3(0f, 90f, 0f));
				float t3 = 0f;
				while (t3 < 0.3f)
				{
					MajmunBobo.transform.Find("PrinceGorilla").rotation = Quaternion.Lerp(MajmunBobo.transform.Find("PrinceGorilla").rotation, a3, t3);
					t3 += Time.deltaTime / 2f;
					yield return null;
				}
			}
			else if (AktivanCustomizationTab == 2)
			{
				TrenutnoSelektovanaMajica = 999;
				GameObject.Find("2TShirtsShopTab").GetComponent<SpriteRenderer>().color = Color.green;
				CustomizationShirts.SetActive(true);
				ImaNovihMajica = false;
				Quaternion a3 = Quaternion.Euler(new Vector3(0f, 150f, 0f));
				float t3 = 0f;
				while (t3 < 0.3f)
				{
					MajmunBobo.transform.Find("PrinceGorilla").rotation = Quaternion.Lerp(MajmunBobo.transform.Find("PrinceGorilla").rotation, a3, t3);
					t3 += Time.deltaTime / 2f;
					yield return null;
				}
			}
			else if (AktivanCustomizationTab == 3)
			{
				TrenutnoSelektovanRanac = 999;
				GameObject.Find("3BackPackShopTab").GetComponent<SpriteRenderer>().color = Color.green;
				CustomizationBackPack.SetActive(true);
				ImaNovihRanceva = false;
				Quaternion a3 = Quaternion.Euler(new Vector3(0f, 35f, 0f));
				float t3 = 0f;
				while (t3 < 0.3f)
				{
					MajmunBobo.transform.Find("PrinceGorilla").rotation = Quaternion.Lerp(MajmunBobo.transform.Find("PrinceGorilla").rotation, a3, t3);
					t3 += Time.deltaTime / 2f;
					yield return null;
				}
			}
		}
		else if (AktivanCustomizationTab == 1)
		{
			TrenutniSelektovanSesir = 999;
			GameObject.Find("1HatsShopTab").GetComponent<SpriteRenderer>().color = Color.green;
			CustomizationHats.SetActive(true);
		}
		else if (AktivanCustomizationTab == 2)
		{
			TrenutnoSelektovanaMajica = 999;
			GameObject.Find("2TShirtsShopTab").GetComponent<SpriteRenderer>().color = Color.green;
			CustomizationShirts.SetActive(true);
		}
		else if (AktivanCustomizationTab == 3)
		{
			TrenutnoSelektovanRanac = 999;
			GameObject.Find("3BackPackShopTab").GetComponent<SpriteRenderer>().color = Color.green;
			CustomizationBackPack.SetActive(true);
		}
		AktivirajCustomization();
	}

	public void AktivirajCustomization()
	{
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		if (AktivanCustomizationTab == 1)
		{
			ObjCustomizationShirts.CustomizationShirts = false;
			SwipeControlCustomizationShirts.controlEnabled = false;
			ObjCustomizationBackPacks.CustomizationBackPacks = false;
			SwipeControlCustomizationBackPacks.controlEnabled = false;
			ObjCustomizationHats.CustomizationHats = true;
			SwipeControlCustomizationHats.controlEnabled = true;
		}
		else if (AktivanCustomizationTab == 2)
		{
			ObjCustomizationHats.CustomizationHats = false;
			SwipeControlCustomizationHats.controlEnabled = false;
			ObjCustomizationShirts.CustomizationShirts = true;
			SwipeControlCustomizationShirts.controlEnabled = true;
			ObjCustomizationBackPacks.CustomizationBackPacks = false;
			SwipeControlCustomizationBackPacks.controlEnabled = false;
		}
		else if (AktivanCustomizationTab == 3)
		{
			ObjCustomizationHats.CustomizationHats = false;
			SwipeControlCustomizationHats.controlEnabled = false;
			ObjCustomizationShirts.CustomizationShirts = false;
			SwipeControlCustomizationShirts.controlEnabled = false;
			ObjCustomizationBackPacks.CustomizationBackPacks = true;
			SwipeControlCustomizationBackPacks.controlEnabled = true;
		}
		MajmunBobo.transform.position = ShopCustomizationPozicija;
	}

	public void DeaktivirajCustomization()
	{
		if (AktivanCustomizationTab == 1)
		{
			ObjCustomizationHats.CustomizationHats = false;
			SwipeControlCustomizationHats.controlEnabled = false;
		}
		else if (AktivanCustomizationTab == 2)
		{
			ObjCustomizationShirts.CustomizationShirts = false;
			SwipeControlCustomizationShirts.controlEnabled = false;
		}
		else if (AktivanCustomizationTab == 3)
		{
			ObjCustomizationBackPacks.CustomizationBackPacks = false;
			SwipeControlCustomizationBackPacks.controlEnabled = false;
		}
	}

	public void AktivirajFreeCoins()
	{
		ObjFreeCoins.FreeCoins = true;
		SwipeControlFreeCoins.controlEnabled = true;
	}

	public void DeaktivirajFreeCoins()
	{
		ObjFreeCoins.FreeCoins = false;
		SwipeControlFreeCoins.controlEnabled = false;
	}

	public void AktivirajPowerUps()
	{
		ObjPowerUps.PowerUps = true;
		SwipeControlPowerUps.controlEnabled = true;
	}

	public void DeaktivirajPowerUps()
	{
		ObjPowerUps.PowerUps = false;
		SwipeControlPowerUps.controlEnabled = false;
	}

	public void AktivirajShopTab()
	{
		Debug.Log((object)"Aktiviraj Shop pozvan");
		ObjShop.Shop = true;
		SwipeControlShop.controlEnabled = true;
	}

	public void DeaktivirajShopTab()
	{
		Debug.Log((object)"Deaktiviraj Shop pozvan");
		ObjShop.Shop = false;
		SwipeControlShop.controlEnabled = false;
	}

	public void ProveraTrenutnogItema(int TrenutniItem)
	{
		if (AktivanCustomizationTab == 1)
		{
			ProveriStanjeSesira(TrenutniItem);
		}
		else if (AktivanCustomizationTab == 2)
		{
			ProveriStanjeMajica(TrenutniItem);
		}
		else if (AktivanCustomizationTab == 3)
		{
			ProveriStanjeRanca(TrenutniItem);
		}
	}

	public void ProveriStanjeSesira(int TrenutniItem)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		if (TrenutniItem == TrenutniSelektovanSesir)
		{
			return;
		}
		TrenutniSelektovanSesir = TrenutniItem;
		if (TrenutniItem >= SveStvariZaOblacenjeHats.Count)
		{
			return;
		}
		if (SveStvariZaOblacenjeHats[TrenutniSelektovanSesir] == 1)
		{
			ButtonShopSprite.GetComponent<SpriteRenderer>().color = Color.white;
			PreviewState = false;
			if (AktivanSesir == TrenutniItem)
			{
				ButtonShop.GetComponent<TextMesh>().text = LanguageManager.Unequip;
				BuyButtonState = 3;
			}
			else
			{
				ButtonShop.GetComponent<TextMesh>().text = LanguageManager.Equip;
				BuyButtonState = 2;
			}
			ButtonShop.GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
			return;
		}
		PreviewState = true;
		ButtonShop.GetComponent<TextMesh>().text = LanguageManager.Buy;
		ButtonShop.GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		int num = int.Parse(CoinsHats[TrenutniItem]);
		if (ZakljucaniHats[TrenutniSelektovanSesir] == 1)
		{
			if (StagesParser.currentMoney < num)
			{
				ButtonShopSprite.GetComponent<SpriteRenderer>().color = new Color(0.41176f, 0.41176f, 0.41176f);
				BuyButtonState = 1;
			}
			else
			{
				ButtonShopSprite.GetComponent<SpriteRenderer>().color = Color.white;
				BuyButtonState = 0;
			}
		}
		else
		{
			BuyButtonState = 4;
			ButtonShopSprite.GetComponent<SpriteRenderer>().color = new Color(0.41176f, 0.41176f, 0.41176f);
		}
	}

	public void ProveriStanjeMajica(int TrenutniItem)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		if (TrenutniItem == TrenutnoSelektovanaMajica)
		{
			return;
		}
		TrenutnoSelektovanaMajica = TrenutniItem;
		if (TrenutniItem >= SveStvariZaOblacenjeShirts.Count)
		{
			return;
		}
		if (SveStvariZaOblacenjeShirts[TrenutniItem] == 1)
		{
			ButtonShopSprite.GetComponent<SpriteRenderer>().color = Color.white;
			PreviewState = false;
			if (AktivnaMajica == TrenutniItem)
			{
				ButtonShop.GetComponent<TextMesh>().text = LanguageManager.Unequip;
				BuyButtonState = 3;
			}
			else
			{
				ButtonShop.GetComponent<TextMesh>().text = LanguageManager.Equip;
				BuyButtonState = 2;
			}
			ButtonShop.GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
			return;
		}
		PreviewState = true;
		ButtonShop.GetComponent<TextMesh>().text = LanguageManager.Buy;
		ButtonShop.GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		int num = int.Parse(CoinsShirts[TrenutniItem]);
		if (ZakljucaniShirts[TrenutnoSelektovanaMajica] == 1)
		{
			if (StagesParser.currentMoney < num)
			{
				ButtonShopSprite.GetComponent<SpriteRenderer>().color = new Color(0.41176f, 0.41176f, 0.41176f);
				BuyButtonState = 1;
			}
			else
			{
				ButtonShopSprite.GetComponent<SpriteRenderer>().color = Color.white;
				BuyButtonState = 0;
			}
		}
		else
		{
			BuyButtonState = 4;
			ButtonShopSprite.GetComponent<SpriteRenderer>().color = new Color(0.41176f, 0.41176f, 0.41176f);
		}
	}

	public void ProveriStanjeRanca(int TrenutniItem)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		if (TrenutniItem == TrenutnoSelektovanRanac)
		{
			return;
		}
		TrenutnoSelektovanRanac = TrenutniItem;
		if (TrenutniItem >= SveStvariZaOblacenjeBackPack.Count)
		{
			return;
		}
		if (SveStvariZaOblacenjeBackPack[TrenutniItem] == 1)
		{
			ButtonShopSprite.GetComponent<SpriteRenderer>().color = Color.white;
			PreviewState = false;
			if (AktivanRanac == TrenutniItem)
			{
				ButtonShop.GetComponent<TextMesh>().text = LanguageManager.Unequip;
				BuyButtonState = 3;
			}
			else
			{
				ButtonShop.GetComponent<TextMesh>().text = LanguageManager.Equip;
				BuyButtonState = 2;
			}
			ButtonShop.GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
			return;
		}
		PreviewState = true;
		ButtonShop.GetComponent<TextMesh>().text = LanguageManager.Buy;
		ButtonShop.GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		int num = int.Parse(CoinsBackPacks[TrenutniItem]);
		if (ZakljucaniBackPacks[TrenutnoSelektovanRanac] == 1)
		{
			if (StagesParser.currentMoney < num)
			{
				ButtonShopSprite.GetComponent<SpriteRenderer>().color = new Color(0.41176f, 0.41176f, 0.41176f);
				BuyButtonState = 1;
			}
			else
			{
				ButtonShopSprite.GetComponent<SpriteRenderer>().color = Color.white;
				BuyButtonState = 0;
			}
		}
		else
		{
			BuyButtonState = 4;
			ButtonShopSprite.GetComponent<SpriteRenderer>().color = new Color(0.41176f, 0.41176f, 0.41176f);
		}
	}

	public void ProveriStanjeCelogShopa()
	{
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_077c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bfd: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < BrojItemaShopHats; i++)
		{
			if (ZakljucaniHats[i] == 1)
			{
				((Component)HatsObjects[i].Find("Zakkljucano")).gameObject.SetActive(false);
			}
			else
			{
				((Component)HatsObjects[i].Find("Polje za unos COINA U shopu - Shop")).gameObject.SetActive(false);
				((Component)HatsObjects[i].Find("Bedz - Popust")).gameObject.SetActive(false);
			}
			if (SveStvariZaOblacenjeHats[i] == 1)
			{
				((Component)HatsObjects[i].Find("Polje za unos COINA U shopu - Shop")).gameObject.SetActive(false);
				((Component)HatsObjects[i].Find("Bedz - Popust")).gameObject.SetActive(false);
			}
			else if (PopustHats[i] == "0")
			{
				((Component)HatsObjects[i].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMesh>().text = CoinsHats[i];
				((Component)HatsObjects[i].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
				if ((i > StariBrojOtkljucanihKapa) & (i <= BrojOtkljucanihKapa))
				{
					ImaNovihKapa = true;
					((Component)HatsObjects[i].Find("Bedz - Popust/Text/Number")).GetComponent<TextMesh>().text = LanguageManager.New;
					((Component)HatsObjects[i].Find("Bedz - Popust/Text/Number/Number Shadow")).GetComponent<TextMesh>().text = LanguageManager.New;
				}
				else
				{
					((Component)HatsObjects[i].Find("Bedz - Popust")).gameObject.SetActive(false);
				}
			}
			else
			{
				if ((i > StariBrojOtkljucanihKapa) & (i <= BrojOtkljucanihKapa))
				{
					ImaNovihKapa = true;
					((Component)HatsObjects[i].Find("Bedz - Popust/Text/Number")).GetComponent<TextMesh>().text = LanguageManager.New;
					((Component)HatsObjects[i].Find("Bedz - Popust/Text/Number/Number Shadow")).GetComponent<TextMesh>().text = LanguageManager.New;
				}
				else
				{
					((Component)HatsObjects[i].Find("Bedz - Popust/Text/Number")).GetComponent<TextMesh>().text = "-" + PopustHats[i] + "%";
					((Component)HatsObjects[i].Find("Bedz - Popust/Text/Number/Number Shadow")).GetComponent<TextMesh>().text = PopustHats[i] + "%";
				}
				string s = "0." + PopustHats[i];
				if (ZakljucaniHats[i] == 1)
				{
					float num = float.Parse(CoinsHats[i]) / (1f - float.Parse(s));
					((Component)HatsObjects[i].Find("Polje za unos COINA U shopu - Shop_NoDiscount")).gameObject.SetActive(true);
					((Component)HatsObjects[i].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number")).GetComponent<TextMesh>().text = num.ToString();
					if (((Component)HatsObjects[i].parent).gameObject.activeSelf)
					{
						((Component)HatsObjects[i].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
					}
					((Component)HatsObjects[i].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMesh>().text = CoinsHats[i];
					((Component)HatsObjects[i].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
					((Component)HatsObjects[i].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMesh>().color = PopustBoja;
				}
			}
			((Component)HatsObjects[i].Find("Text/ime")).GetComponent<TextMesh>().text = ImenaHats[i];
			((Component)HatsObjects[i].Find("Text/ime")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		}
		for (int j = 0; j < BrojItemaShopShirts; j++)
		{
			if (ZakljucaniShirts[j] == 1)
			{
				((Component)ShirtsObjects[j].Find("Zakkljucano")).gameObject.SetActive(false);
			}
			else
			{
				((Component)ShirtsObjects[j].Find("Polje za unos COINA U shopu - Shop")).gameObject.SetActive(false);
				((Component)ShirtsObjects[j].Find("Bedz - Popust")).gameObject.SetActive(false);
			}
			if (SveStvariZaOblacenjeShirts[j] == 1)
			{
				((Component)ShirtsObjects[j].Find("Polje za unos COINA U shopu - Shop")).gameObject.SetActive(false);
				((Component)ShirtsObjects[j].Find("Bedz - Popust")).gameObject.SetActive(false);
			}
			else if (PopustShirts[j] == "0")
			{
				((Component)ShirtsObjects[j].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMesh>().text = CoinsShirts[j];
				((Component)ShirtsObjects[j].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
				if ((j > StariBrojOtkljucanihMajici) & (j <= BrojOtkljucanihMajici))
				{
					ImaNovihMajica = true;
					((Component)ShirtsObjects[j].Find("Bedz - Popust/Text/Number")).GetComponent<TextMesh>().text = LanguageManager.New;
					((Component)ShirtsObjects[j].Find("Bedz - Popust/Text/Number/Number Shadow")).GetComponent<TextMesh>().text = LanguageManager.New;
				}
				else
				{
					((Component)ShirtsObjects[j].Find("Bedz - Popust")).gameObject.SetActive(false);
				}
			}
			else
			{
				if ((j > StariBrojOtkljucanihMajici) & (j <= BrojOtkljucanihMajici))
				{
					ImaNovihMajica = true;
					((Component)ShirtsObjects[j].Find("Bedz - Popust/Text/Number")).GetComponent<TextMesh>().text = LanguageManager.New;
					((Component)ShirtsObjects[j].Find("Bedz - Popust/Text/Number/Number Shadow")).GetComponent<TextMesh>().text = LanguageManager.New;
				}
				else
				{
					((Component)ShirtsObjects[j].Find("Bedz - Popust/Text/Number")).GetComponent<TextMesh>().text = "-" + PopustShirts[j] + "%";
					((Component)ShirtsObjects[j].Find("Bedz - Popust/Text/Number/Number Shadow")).GetComponent<TextMesh>().text = PopustShirts[j] + "%";
				}
				string s2 = "0." + PopustShirts[j];
				if (ZakljucaniShirts[j] == 1)
				{
					float num2 = float.Parse(CoinsShirts[j]) / (1f - float.Parse(s2));
					((Component)ShirtsObjects[j].Find("Polje za unos COINA U shopu - Shop_NoDiscount")).gameObject.SetActive(true);
					((Component)ShirtsObjects[j].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number")).GetComponent<TextMesh>().text = num2.ToString();
					if (((Component)ShirtsObjects[j].parent).gameObject.activeSelf)
					{
						((Component)ShirtsObjects[j].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
					}
					((Component)ShirtsObjects[j].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMesh>().text = CoinsShirts[j];
					((Component)ShirtsObjects[j].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
					((Component)ShirtsObjects[j].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMesh>().color = PopustBoja;
				}
			}
			((Component)ShirtsObjects[j].Find("Text/ime")).GetComponent<TextMesh>().text = ImenaShirts[j];
			((Component)ShirtsObjects[j].Find("Text/ime")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		}
		for (int k = 0; k < BrojItemaShopBackPack; k++)
		{
			if (ZakljucaniBackPacks[k] == 1)
			{
				((Component)BackPacksObjects[k].Find("Zakkljucano")).gameObject.SetActive(false);
			}
			else
			{
				((Component)BackPacksObjects[k].Find("Polje za unos COINA U shopu - Shop")).gameObject.SetActive(false);
				((Component)BackPacksObjects[k].Find("Bedz - Popust")).gameObject.SetActive(false);
			}
			if (SveStvariZaOblacenjeBackPack[k] == 1)
			{
				((Component)BackPacksObjects[k].Find("Polje za unos COINA U shopu - Shop")).gameObject.SetActive(false);
				((Component)BackPacksObjects[k].Find("Bedz - Popust")).gameObject.SetActive(false);
			}
			else if (PopustBackPacks[k] == "0")
			{
				((Component)BackPacksObjects[k].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMesh>().text = CoinsBackPacks[k];
				((Component)BackPacksObjects[k].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
				if ((k > StariBrojOtkljucanihRanceva) & (k <= BrojOtkljucanihRanceva))
				{
					ImaNovihRanceva = true;
					((Component)BackPacksObjects[k].Find("Bedz - Popust/Text/Number")).GetComponent<TextMesh>().text = LanguageManager.New;
					((Component)BackPacksObjects[k].Find("Bedz - Popust/Text/Number/Number Shadow")).GetComponent<TextMesh>().text = LanguageManager.New;
				}
				else
				{
					((Component)BackPacksObjects[k].Find("Bedz - Popust")).gameObject.SetActive(false);
				}
			}
			else
			{
				((Component)BackPacksObjects[k].Find("Bedz - Popust/Text/Number")).GetComponent<TextMesh>().text = "-" + PopustBackPacks[k] + "%";
				((Component)BackPacksObjects[k].Find("Bedz - Popust/Text/Number/Number Shadow")).GetComponent<TextMesh>().text = PopustBackPacks[k] + "%";
				if ((k > StariBrojOtkljucanihRanceva) & (k <= BrojOtkljucanihRanceva))
				{
					ImaNovihRanceva = true;
					((Component)BackPacksObjects[k].Find("Bedz - Popust/Text/Number")).GetComponent<TextMesh>().text = LanguageManager.New;
					((Component)BackPacksObjects[k].Find("Bedz - Popust/Text/Number/Number Shadow")).GetComponent<TextMesh>().text = LanguageManager.New;
				}
				else
				{
					((Component)BackPacksObjects[k].Find("Bedz - Popust/Text/Number")).GetComponent<TextMesh>().text = "-" + PopustBackPacks[k] + "%";
					((Component)BackPacksObjects[k].Find("Bedz - Popust/Text/Number/Number Shadow")).GetComponent<TextMesh>().text = PopustBackPacks[k] + "%";
				}
				string s3 = "0." + PopustBackPacks[k];
				if (ZakljucaniBackPacks[k] == 1)
				{
					float num3 = float.Parse(CoinsBackPacks[k]) / (1f - float.Parse(s3));
					((Component)BackPacksObjects[k].Find("Polje za unos COINA U shopu - Shop_NoDiscount")).gameObject.SetActive(true);
					((Component)BackPacksObjects[k].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number")).GetComponent<TextMesh>().text = num3.ToString();
					if (((Component)BackPacksObjects[k].parent).gameObject.activeSelf)
					{
						((Component)BackPacksObjects[k].Find("Polje za unos COINA U shopu - Shop_NoDiscount/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
					}
					((Component)BackPacksObjects[k].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMesh>().text = CoinsBackPacks[k];
					((Component)BackPacksObjects[k].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
					((Component)BackPacksObjects[k].Find("Polje za unos COINA U shopu - Shop/Coins Number")).GetComponent<TextMesh>().color = PopustBoja;
				}
			}
			((Component)BackPacksObjects[k].Find("Text/ime")).GetComponent<TextMesh>().text = ImenaBackPacks[k];
			((Component)BackPacksObjects[k].Find("Text/ime")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		}
		if (ImaNovihKapa)
		{
			((Component)ZidFooter.transform.Find("1HatsShopTab/Znak Uzvika telo")).gameObject.SetActive(true);
		}
		else
		{
			((Component)ZidFooter.transform.Find("1HatsShopTab/Znak Uzvika telo")).gameObject.SetActive(false);
		}
		if (ImaNovihMajica)
		{
			((Component)ZidFooter.transform.Find("2TShirtsShopTab/Znak Uzvika telo")).gameObject.SetActive(true);
		}
		else
		{
			((Component)ZidFooter.transform.Find("2TShirtsShopTab/Znak Uzvika telo")).gameObject.SetActive(false);
		}
		if (ImaNovihRanceva)
		{
			((Component)ZidFooter.transform.Find("3BackPackShopTab/Znak Uzvika telo")).gameObject.SetActive(true);
		}
		else
		{
			((Component)ZidFooter.transform.Find("3BackPackShopTab/Znak Uzvika telo")).gameObject.SetActive(false);
		}
		if (ImaNovihKapa | ImaNovihMajica | ImaNovihRanceva)
		{
			((Component)Custumization.transform.Find("Znak Uzvika telo")).gameObject.SetActive(true);
		}
		else
		{
			((Component)Custumization.transform.Find("Znak Uzvika telo")).gameObject.SetActive(false);
		}
	}

	public void KupiItem()
	{
		//IL_06f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_072b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0730: Unknown result type (might be due to invalid IL or missing references)
		if (BuyButtonState == 0)
		{
			if (AktivanCustomizationTab == 1)
			{
				StagesParser.currentMoney -= int.Parse(CoinsHats[AktivanItemSesir]);
				SveStvariZaOblacenjeHats[AktivanItemSesir] = 1;
				((Component)HatsObjects[AktivanItemSesir].Find("Stikla")).gameObject.SetActive(true);
				((Component)HatsObjects[AktivanItemSesir].Find("Polje za unos COINA U shopu - Shop")).gameObject.SetActive(false);
				((Component)HatsObjects[AktivanItemSesir].Find("Polje za unos COINA U shopu - Shop_NoDiscount")).gameObject.SetActive(false);
				((Component)HatsObjects[AktivanItemSesir].Find("Bedz - Popust")).gameObject.SetActive(false);
				TrenutniSelektovanSesir = -1;
				ProveraTrenutnogItema(AktivanItemSesir);
				FacebookManager.UserSveKupovineHats = "";
				for (int i = 0; i < SveStvariZaOblacenjeHats.Count; i++)
				{
					FacebookManager.UserSveKupovineHats += SveStvariZaOblacenjeHats[i] + "#";
				}
				StagesParser.svekupovineGlava = FacebookManager.UserSveKupovineHats;
				PlayerPrefs.SetString("UserSveKupovineHats", FacebookManager.UserSveKupovineHats);
				PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
				PlayerPrefs.Save();
			}
			else if (AktivanCustomizationTab == 2)
			{
				StagesParser.currentMoney -= int.Parse(CoinsShirts[AktivanItemMajica]);
				SveStvariZaOblacenjeShirts[AktivanItemMajica] = 1;
				((Component)ShirtsObjects[AktivanItemMajica].Find("Stikla")).gameObject.SetActive(true);
				((Component)ShirtsObjects[AktivanItemMajica].Find("Polje za unos COINA U shopu - Shop")).gameObject.SetActive(false);
				((Component)ShirtsObjects[AktivanItemMajica].Find("Polje za unos COINA U shopu - Shop_NoDiscount")).gameObject.SetActive(false);
				((Component)ShirtsObjects[AktivanItemMajica].Find("Bedz - Popust")).gameObject.SetActive(false);
				TrenutnoSelektovanaMajica = -1;
				ProveraTrenutnogItema(AktivanItemMajica);
				FacebookManager.UserSveKupovineShirts = "";
				for (int j = 0; j < SveStvariZaOblacenjeShirts.Count; j++)
				{
					FacebookManager.UserSveKupovineShirts += SveStvariZaOblacenjeShirts[j] + "#";
				}
				StagesParser.svekupovineMajica = FacebookManager.UserSveKupovineShirts;
				PlayerPrefs.SetString("UserSveKupovineShirts", FacebookManager.UserSveKupovineShirts);
				PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
				PlayerPrefs.Save();
			}
			if (AktivanCustomizationTab == 3)
			{
				StagesParser.currentMoney -= int.Parse(CoinsBackPacks[AktivanItemRanac]);
				SveStvariZaOblacenjeBackPack[AktivanItemRanac] = 1;
				((Component)BackPacksObjects[AktivanItemRanac].Find("Stikla")).gameObject.SetActive(true);
				((Component)BackPacksObjects[AktivanItemRanac].Find("Polje za unos COINA U shopu - Shop")).gameObject.SetActive(false);
				((Component)BackPacksObjects[AktivanItemRanac].Find("Polje za unos COINA U shopu - Shop_NoDiscount")).gameObject.SetActive(false);
				((Component)BackPacksObjects[AktivanItemRanac].Find("Bedz - Popust")).gameObject.SetActive(false);
				TrenutnoSelektovanRanac = -1;
				ProveraTrenutnogItema(AktivanItemRanac);
				FacebookManager.UserSveKupovineBackPacks = "";
				for (int k = 0; k < SveStvariZaOblacenjeBackPack.Count; k++)
				{
					FacebookManager.UserSveKupovineBackPacks += SveStvariZaOblacenjeBackPack[k] + "#";
				}
				StagesParser.svekupovineLedja = FacebookManager.UserSveKupovineBackPacks;
				PlayerPrefs.SetString("UserSveKupovineBackPacks", FacebookManager.UserSveKupovineBackPacks);
				PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
				PlayerPrefs.Save();
			}
			StagesParser.ServerUpdate = 1;
			((Component)CoinsNumber.transform.Find("Coins Number")).GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
			((Component)CoinsNumber.transform.Find("Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: false, hasWhiteSpaces: true);
		}
		else if (BuyButtonState == 1)
		{
			CoinsNumber.GetComponent<Animation>().Play("Not Enough Coins");
		}
		else if (BuyButtonState == 2)
		{
			if (AktivanCustomizationTab == 1)
			{
				if (PreviewSesir != -1)
				{
					((Component)((Component)MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/Chest/Neck/Head/" + PreviewSesir)).transform.GetChild(0)).gameObject.SetActive(false);
					PreviewSesir = -1;
				}
				if (int.Parse(UsiHats[AktivanItemSesir]) == 1)
				{
					if (!ImaUsi)
					{
						ImaUsi = true;
						((Component)MajmunBobo.transform.Find("PrinceGorilla/Usi")).gameObject.SetActive(true);
					}
				}
				else if (ImaUsi)
				{
					ImaUsi = false;
					((Component)MajmunBobo.transform.Find("PrinceGorilla/Usi")).gameObject.SetActive(false);
				}
				if (int.Parse(KosaHats[AktivanItemSesir]) == 1)
				{
					ImaKosu = true;
					((Component)MajmunBobo.transform.Find("PrinceGorilla/Kosa")).gameObject.SetActive(true);
				}
				else
				{
					ImaKosu = false;
					((Component)MajmunBobo.transform.Find("PrinceGorilla/Kosa")).gameObject.SetActive(false);
				}
				if (AktivanSesir != -1)
				{
					((Component)((Component)MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/Chest/Neck/Head/" + AktivanSesir)).transform.GetChild(0)).gameObject.SetActive(false);
				}
				((Component)((Component)MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/Chest/Neck/Head/" + AktivanItemSesir)).transform.GetChild(0)).gameObject.SetActive(true);
				AktivanSesir = AktivanItemSesir;
				TrenutniSelektovanSesir = -1;
				ProveraTrenutnogItema(AktivanItemSesir);
				StagesParser.glava = AktivanSesir;
				StagesParser.imaKosu = ImaKosu;
				StagesParser.imaUsi = ImaUsi;
			}
			else if (AktivanCustomizationTab == 2)
			{
				((Component)MajmunBobo.transform.Find("PrinceGorilla/custom_Majica")).gameObject.SetActive(true);
				Object obj = Resources.Load("Majice/Bg" + AktivanItemMajica);
				Texture val = (Texture)(object)((obj is Texture) ? obj : null);
				((Component)MajmunBobo.transform.Find("PrinceGorilla/custom_Majica")).GetComponent<Renderer>().material.SetTexture("_MainTex", val);
				((Component)MajmunBobo.transform.Find("PrinceGorilla/custom_Majica")).GetComponent<Renderer>().material.color = TShirtColors[AktivanItemMajica];
				AktivnaMajica = AktivanItemMajica;
				TrenutnoSelektovanaMajica = -1;
				ProveraTrenutnogItema(AktivanItemMajica);
				StagesParser.majica = AktivnaMajica;
				StagesParser.bojaMajice = TShirtColors[AktivnaMajica];
			}
			if (AktivanCustomizationTab == 3)
			{
				if (PreviewRanac != -1)
				{
					((Component)((Component)MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + PreviewRanac)).transform.GetChild(0)).gameObject.SetActive(false);
					PreviewRanac = -1;
				}
				if (AktivanRanac != -1)
				{
					((Component)((Component)MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + AktivanRanac)).transform.GetChild(0)).gameObject.SetActive(false);
				}
				((Component)((Component)MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + AktivanItemRanac)).transform.GetChild(0)).gameObject.SetActive(true);
				AktivanRanac = AktivanItemRanac;
				TrenutnoSelektovanRanac = -1;
				ProveraTrenutnogItema(AktivanItemRanac);
				StagesParser.ledja = AktivanRanac;
			}
			string text = AktivanSesir + "#" + AktivnaMajica + "#" + AktivanRanac;
			PlayerPrefs.SetString("AktivniItemi", text);
			PlayerPrefs.Save();
		}
		else if (BuyButtonState == 3)
		{
			if (AktivanCustomizationTab == 1)
			{
				ImaUsi = true;
				((Component)MajmunBobo.transform.Find("PrinceGorilla/Usi")).gameObject.SetActive(true);
				ImaKosu = true;
				((Component)MajmunBobo.transform.Find("PrinceGorilla/Kosa")).gameObject.SetActive(true);
				((Component)((Component)MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/Chest/Neck/Head/" + AktivanItemSesir)).transform.GetChild(0)).gameObject.SetActive(false);
				AktivanSesir = -1;
				TrenutniSelektovanSesir = -1;
				ProveraTrenutnogItema(AktivanItemSesir);
				StagesParser.imaKosu = true;
				StagesParser.imaUsi = true;
				StagesParser.glava = -1;
			}
			else if (AktivanCustomizationTab == 2)
			{
				((Component)MajmunBobo.transform.Find("PrinceGorilla/custom_Majica")).gameObject.SetActive(false);
				AktivnaMajica = -1;
				TrenutnoSelektovanaMajica = -1;
				ProveraTrenutnogItema(AktivanItemMajica);
				StagesParser.majica = -1;
			}
			if (AktivanCustomizationTab == 3)
			{
				((Component)((Component)MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + AktivanItemRanac)).transform.GetChild(0)).gameObject.SetActive(false);
				AktivanRanac = -1;
				TrenutnoSelektovanRanac = -1;
				ProveraTrenutnogItema(AktivanItemRanac);
				StagesParser.ledja = -1;
			}
			string text2 = AktivanSesir + "#" + AktivnaMajica + "#" + AktivanRanac;
			PlayerPrefs.SetString("AktivniItemi", text2);
			PlayerPrefs.Save();
		}
		_ = BuyButtonState;
		_ = 4;
	}

	public void PreviewItem()
	{
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		if (AktivanCustomizationTab == 1)
		{
			if (ZakljucaniHats[AktivanItemSesir] == 1)
			{
				if (int.Parse(UsiHats[AktivanItemSesir]) == 1)
				{
					if (!ImaUsi)
					{
						ImaUsi = true;
						((Component)MajmunBobo.transform.Find("PrinceGorilla/Usi")).gameObject.SetActive(true);
					}
				}
				else if (ImaUsi)
				{
					ImaUsi = false;
					((Component)MajmunBobo.transform.Find("PrinceGorilla/Usi")).gameObject.SetActive(false);
				}
				if (int.Parse(KosaHats[AktivanItemSesir]) == 1)
				{
					ImaKosu = true;
					((Component)MajmunBobo.transform.Find("PrinceGorilla/Kosa")).gameObject.SetActive(true);
				}
				else
				{
					ImaKosu = false;
					((Component)MajmunBobo.transform.Find("PrinceGorilla/Kosa")).gameObject.SetActive(false);
				}
				if (PreviewSesir != -1)
				{
					((Component)((Component)MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/Chest/Neck/Head/" + PreviewSesir)).transform.GetChild(0)).gameObject.SetActive(false);
				}
				if (AktivanSesir != -1)
				{
					((Component)((Component)MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/Chest/Neck/Head/" + AktivanSesir)).transform.GetChild(0)).gameObject.SetActive(false);
				}
				((Component)((Component)MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/Chest/Neck/Head/" + AktivanItemSesir)).transform.GetChild(0)).gameObject.SetActive(true);
				PreviewSesir = AktivanItemSesir;
				TrenutniSelektovanSesir = -1;
				ProveraTrenutnogItema(PreviewSesir);
			}
		}
		else if (AktivanCustomizationTab == 2 && ZakljucaniShirts[AktivanItemMajica] == 1)
		{
			((Component)MajmunBobo.transform.Find("PrinceGorilla/custom_Majica")).gameObject.SetActive(true);
			Object obj = Resources.Load("Majice/Bg" + AktivanItemMajica);
			Texture val = (Texture)(object)((obj is Texture) ? obj : null);
			((Component)MajmunBobo.transform.Find("PrinceGorilla/custom_Majica")).GetComponent<Renderer>().material.SetTexture("_MainTex", val);
			((Component)MajmunBobo.transform.Find("PrinceGorilla/custom_Majica")).GetComponent<Renderer>().material.color = TShirtColors[AktivanItemMajica];
			PreviewMajica = AktivanItemMajica;
			TrenutnoSelektovanaMajica = -1;
			ProveraTrenutnogItema(PreviewMajica);
		}
		if (AktivanCustomizationTab == 3 && ZakljucaniBackPacks[AktivanItemRanac] == 1)
		{
			if (PreviewRanac != -1)
			{
				((Component)((Component)MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + PreviewRanac)).transform.GetChild(0)).gameObject.SetActive(false);
			}
			if (AktivanRanac != -1)
			{
				((Component)((Component)MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + AktivanRanac)).transform.GetChild(0)).gameObject.SetActive(false);
			}
			((Component)((Component)MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + AktivanItemRanac)).transform.GetChild(0)).gameObject.SetActive(true);
			PreviewRanac = AktivanItemRanac;
			TrenutnoSelektovanRanac = -1;
			ProveraTrenutnogItema(PreviewRanac);
		}
	}

	public void KupiDoubleCoins()
	{
		if (StagesParser.currentMoney < int.Parse(CoinsPowerUps[0]))
		{
			CoinsNumber.GetComponent<Animation>().Play("Not Enough Coins");
			return;
		}
		StagesParser.currentMoney -= int.Parse(CoinsPowerUps[0]);
		StagesParser.powerup_doublecoins++;
		GameObject.Find("Double Coins Number").GetComponent<Animation>().Play("BoughtPowerUp");
		((Component)CoinsNumber.transform.Find("Coins Number")).GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
		((Component)CoinsNumber.transform.Find("Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Double Coins Number/Number").GetComponent<TextMesh>().text = StagesParser.powerup_doublecoins.ToString();
		GameObject.Find("Double Coins Number/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
		PlayerPrefs.SetString("PowerUps", StagesParser.powerup_doublecoins + "#" + StagesParser.powerup_magnets + "#" + StagesParser.powerup_shields);
		PlayerPrefs.Save();
		StagesParser.ServerUpdate = 1;
	}

	public void KupiMagnet()
	{
		if (StagesParser.currentMoney < int.Parse(CoinsPowerUps[1]))
		{
			CoinsNumber.GetComponent<Animation>().Play("Not Enough Coins");
			return;
		}
		StagesParser.currentMoney -= int.Parse(CoinsPowerUps[1]);
		StagesParser.powerup_magnets++;
		GameObject.Find("Magnet Number").GetComponent<Animation>().Play("BoughtPowerUp");
		((Component)CoinsNumber.transform.Find("Coins Number")).GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
		((Component)CoinsNumber.transform.Find("Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Magnet Number/Number").GetComponent<TextMesh>().text = StagesParser.powerup_magnets.ToString();
		GameObject.Find("Magnet Number/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
		PlayerPrefs.SetString("PowerUps", StagesParser.powerup_doublecoins + "#" + StagesParser.powerup_magnets + "#" + StagesParser.powerup_shields);
		PlayerPrefs.Save();
		StagesParser.ServerUpdate = 1;
	}

	public void KupiShield()
	{
		if (StagesParser.currentMoney < int.Parse(CoinsPowerUps[2]))
		{
			CoinsNumber.GetComponent<Animation>().Play("Not Enough Coins");
			return;
		}
		StagesParser.currentMoney -= int.Parse(CoinsPowerUps[2]);
		StagesParser.powerup_shields++;
		GameObject.Find("Shield Number").GetComponent<Animation>().Play("BoughtPowerUp");
		((Component)CoinsNumber.transform.Find("Coins Number")).GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
		((Component)CoinsNumber.transform.Find("Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Shield Number/Number").GetComponent<TextMesh>().text = StagesParser.powerup_shields.ToString();
		GameObject.Find("Shield Number/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
		PlayerPrefs.SetString("PowerUps", StagesParser.powerup_doublecoins + "#" + StagesParser.powerup_magnets + "#" + StagesParser.powerup_shields);
		PlayerPrefs.Save();
		StagesParser.ServerUpdate = 1;
	}

	public void KupiBananu()
	{
		if (StagesParser.currentMoney < StagesParser.bananaCost)
		{
			CoinsNumber.GetComponent<Animation>().Play("Not Enough Coins");
			return;
		}
		StagesParser.currentMoney -= StagesParser.bananaCost;
		StagesParser.currentBananas++;
		GameObject.Find("Shop/2 Shop - BANANA/Zid Shop/Zid Header i Footer/Zid Footer Shop/Banana Number").GetComponent<Animation>().Play("BoughtPowerUp");
		((Component)CoinsNumber.transform.Find("Coins Number")).GetComponent<TextMesh>().text = StagesParser.currentMoney.ToString();
		((Component)CoinsNumber.transform.Find("Coins Number")).GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		GameObject.Find("Shop/2 Shop - BANANA/Zid Shop/Zid Header i Footer/Zid Footer Shop/Banana Number/Number").GetComponent<TextMesh>().text = StagesParser.currentBananas.ToString();
		GameObject.Find("Shop/2 Shop - BANANA/Zid Shop/Zid Header i Footer/Zid Footer Shop/Banana Number/Number").GetComponent<TextMeshEffects>().RefreshTextOutline(adjustTextSize: true, hasWhiteSpaces: true);
		PlayerPrefs.SetInt("TotalMoney", StagesParser.currentMoney);
		PlayerPrefs.SetInt("TotalBananas", StagesParser.currentBananas);
		PlayerPrefs.Save();
		StagesParser.ServerUpdate = 1;
	}

	public void OcistiPreview()
	{
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		if (PreviewSesir != -1)
		{
			ImaUsi = true;
			((Component)MajmunBobo.transform.Find("PrinceGorilla/Usi")).gameObject.SetActive(true);
			ImaKosu = true;
			((Component)MajmunBobo.transform.Find("PrinceGorilla/Kosa")).gameObject.SetActive(true);
			((Component)((Component)MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/Chest/Neck/Head/" + PreviewSesir)).transform.GetChild(0)).gameObject.SetActive(false);
			PreviewSesir = -1;
		}
		if (PreviewMajica != -1)
		{
			((Component)MajmunBobo.transform.Find("PrinceGorilla/custom_Majica")).gameObject.SetActive(false);
			PreviewMajica = -1;
		}
		if (PreviewRanac != -1)
		{
			((Component)((Component)MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + PreviewRanac)).transform.GetChild(0)).gameObject.SetActive(false);
			PreviewRanac = -1;
		}
		if (AktivanCustomizationTab == 1)
		{
			TrenutniSelektovanSesir = -1;
			ProveraTrenutnogItema(AktivanItemSesir);
		}
		else if (AktivanCustomizationTab == 2)
		{
			TrenutnoSelektovanaMajica = -1;
			ProveraTrenutnogItema(AktivanItemMajica);
		}
		else if (AktivanCustomizationTab == 3)
		{
			TrenutnoSelektovanRanac = -1;
			ProveraTrenutnogItema(AktivanItemRanac);
		}
		if (AktivanSesir != -1)
		{
			if (int.Parse(UsiHats[AktivanSesir]) == 1)
			{
				if (!ImaUsi)
				{
					ImaUsi = true;
					((Component)MajmunBobo.transform.Find("PrinceGorilla/Usi")).gameObject.SetActive(true);
				}
			}
			else if (ImaUsi)
			{
				ImaUsi = false;
				((Component)MajmunBobo.transform.Find("PrinceGorilla/Usi")).gameObject.SetActive(false);
			}
			if (int.Parse(KosaHats[AktivanSesir]) == 1)
			{
				ImaKosu = true;
				((Component)MajmunBobo.transform.Find("PrinceGorilla/Kosa")).gameObject.SetActive(true);
			}
			else
			{
				ImaKosu = false;
				((Component)MajmunBobo.transform.Find("PrinceGorilla/Kosa")).gameObject.SetActive(false);
			}
			((Component)((Component)MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/Chest/Neck/Head/" + AktivanSesir)).transform.GetChild(0)).gameObject.SetActive(true);
			TrenutniSelektovanSesir = -1;
			ProveraTrenutnogItema(AktivanItemSesir);
		}
		if (AktivnaMajica != -1)
		{
			((Component)MajmunBobo.transform.Find("PrinceGorilla/custom_Majica")).gameObject.SetActive(true);
			Object obj = Resources.Load("Majice/Bg" + AktivnaMajica);
			Texture val = (Texture)(object)((obj is Texture) ? obj : null);
			((Component)MajmunBobo.transform.Find("PrinceGorilla/custom_Majica")).GetComponent<Renderer>().material.SetTexture("_MainTex", val);
			((Component)MajmunBobo.transform.Find("PrinceGorilla/custom_Majica")).GetComponent<Renderer>().material.color = TShirtColors[AktivnaMajica];
			TrenutnoSelektovanaMajica = -1;
			ProveraTrenutnogItema(AktivanItemMajica);
		}
		if (AktivanRanac != -1)
		{
			((Component)((Component)MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + AktivanRanac)).transform.GetChild(0)).gameObject.SetActive(true);
			TrenutnoSelektovanRanac = -1;
			ProveraTrenutnogItema(AktivanItemRanac);
		}
	}

	public void OcistiMajmuna()
	{
		ImaUsi = true;
		((Component)MajmunBobo.transform.Find("PrinceGorilla/Usi")).gameObject.SetActive(true);
		ImaKosu = true;
		((Component)MajmunBobo.transform.Find("PrinceGorilla/Kosa")).gameObject.SetActive(true);
		StagesParser.glava = -1;
		StagesParser.majica = -1;
		StagesParser.ledja = -1;
		StagesParser.imaKosu = true;
		StagesParser.imaUsi = true;
		AktivniItemString = "-1#-1#-1";
		PlayerPrefs.SetString("AktivniItemi", AktivniItemString);
		PlayerPrefs.Save();
		if (AktivanSesir != -1)
		{
			((Component)((Component)MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/Chest/Neck/Head/" + AktivanSesir)).transform.GetChild(0)).gameObject.SetActive(false);
			AktivanSesir = -1;
		}
		if (PreviewSesir != -1)
		{
			((Component)((Component)MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/Chest/Neck/Head/" + PreviewSesir)).transform.GetChild(0)).gameObject.SetActive(false);
			PreviewSesir = -1;
		}
		if (AktivnaMajica != -1)
		{
			((Component)MajmunBobo.transform.Find("PrinceGorilla/custom_Majica")).gameObject.SetActive(false);
			AktivnaMajica = -1;
		}
		if (PreviewMajica != -1)
		{
			((Component)MajmunBobo.transform.Find("PrinceGorilla/custom_Majica")).gameObject.SetActive(false);
			PreviewMajica = -1;
		}
		if (AktivanRanac != -1)
		{
			((Component)((Component)MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + AktivanRanac)).transform.GetChild(0)).gameObject.SetActive(false);
			AktivanRanac = -1;
		}
		if (PreviewRanac != -1)
		{
			((Component)((Component)MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + PreviewRanac)).transform.GetChild(0)).gameObject.SetActive(false);
			PreviewRanac = -1;
		}
		if (AktivanCustomizationTab == 1)
		{
			TrenutniSelektovanSesir = -1;
			ProveraTrenutnogItema(AktivanItemSesir);
		}
		else if (AktivanCustomizationTab == 2)
		{
			TrenutnoSelektovanaMajica = -1;
			ProveraTrenutnogItema(AktivanItemMajica);
		}
		else if (AktivanCustomizationTab == 3)
		{
			TrenutnoSelektovanRanac = -1;
			ProveraTrenutnogItema(AktivanItemRanac);
		}
	}

	public void ObuciMajmunaNaStartu()
	{
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		if (PlayerPrefs.HasKey("AktivniItemi"))
		{
			AktivniItemString = PlayerPrefs.GetString("AktivniItemi");
			AktivniItemi = AktivniItemString.Split(new char[1] { '#' });
			AktivanSesir = int.Parse(AktivniItemi[0]);
			AktivnaMajica = int.Parse(AktivniItemi[1]);
			AktivanRanac = int.Parse(AktivniItemi[2]);
		}
		else
		{
			AktivanSesir = -1;
			AktivnaMajica = -1;
			AktivanRanac = -1;
			StagesParser.glava = -1;
			StagesParser.imaKosu = true;
			StagesParser.imaUsi = true;
			StagesParser.majica = -1;
			StagesParser.ledja = -1;
		}
		if (AktivanSesir != -1)
		{
			if (int.Parse(UsiHats[AktivanSesir]) == 1)
			{
				if (!ImaUsi)
				{
					ImaUsi = true;
					((Component)MajmunBobo.transform.Find("PrinceGorilla/Usi")).gameObject.SetActive(true);
					StagesParser.imaUsi = true;
				}
			}
			else if (ImaUsi)
			{
				ImaUsi = false;
				((Component)MajmunBobo.transform.Find("PrinceGorilla/Usi")).gameObject.SetActive(false);
				StagesParser.imaUsi = false;
			}
			if (int.Parse(KosaHats[AktivanSesir]) == 1)
			{
				ImaKosu = true;
				((Component)MajmunBobo.transform.Find("PrinceGorilla/Kosa")).gameObject.SetActive(true);
				StagesParser.imaKosu = true;
			}
			else
			{
				ImaKosu = false;
				((Component)MajmunBobo.transform.Find("PrinceGorilla/Kosa")).gameObject.SetActive(false);
				StagesParser.imaKosu = false;
			}
			((Component)((Component)MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/Chest/Neck/Head/" + AktivanSesir)).transform.GetChild(0)).gameObject.SetActive(true);
			StagesParser.glava = AktivanSesir;
		}
		else
		{
			StagesParser.glava = -1;
			StagesParser.imaKosu = true;
			StagesParser.imaUsi = true;
		}
		if (AktivnaMajica != -1)
		{
			((Component)MajmunBobo.transform.Find("PrinceGorilla/custom_Majica")).gameObject.SetActive(true);
			Object obj = Resources.Load("Majice/Bg" + AktivnaMajica);
			Texture val = (Texture)(object)((obj is Texture) ? obj : null);
			((Component)MajmunBobo.transform.Find("PrinceGorilla/custom_Majica")).GetComponent<Renderer>().material.SetTexture("_MainTex", val);
			((Component)MajmunBobo.transform.Find("PrinceGorilla/custom_Majica")).GetComponent<Renderer>().material.color = TShirtColors[AktivnaMajica];
			StagesParser.majica = AktivnaMajica;
			StagesParser.bojaMajice = TShirtColors[AktivnaMajica];
		}
		else
		{
			StagesParser.majica = -1;
			StagesParser.bojaMajice = Color.white;
		}
		if (AktivanRanac != -1)
		{
			if (Application.loadedLevel == 1)
			{
				if (AktivanRanac == 0)
				{
					((Component)((Component)MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + AktivanRanac)).transform.GetChild(0)).GetComponent<MeshFilter>().mesh = GameObject.Find("RefZaSedenje_AndjeoskaKrila").GetComponent<MeshFilter>().mesh;
				}
				else if (AktivanRanac == 5)
				{
					((Component)((Component)MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + AktivanRanac)).transform.GetChild(0)).GetComponent<MeshFilter>().mesh = GameObject.Find("RefZaSedenje_SlepiMisKrila").GetComponent<MeshFilter>().mesh;
				}
			}
			((Component)((Component)MajmunBobo.transform.Find("PrinceGorilla/ROOT/Hip/Spine/" + AktivanRanac)).transform.GetChild(0)).gameObject.SetActive(true);
			StagesParser.ledja = AktivanRanac;
		}
		else
		{
			StagesParser.ledja = -1;
		}
	}

	private void OnApplicationQuit()
	{
		StariBrojOtkljucanihItema = BrojOtkljucanihKapa + "#" + BrojOtkljucanihMajici + "#" + BrojOtkljucanihRanceva;
		PlayerPrefs.SetString("OtkljucaniItemi", StariBrojOtkljucanihItema);
		PlayerPrefs.Save();
	}
}
