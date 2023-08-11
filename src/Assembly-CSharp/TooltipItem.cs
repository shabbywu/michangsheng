using System.Collections.Generic;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;

public class TooltipItem : TooltipBase
{
	public UILabel Label1;

	public UILabel Label2;

	public UILabel Label3;

	public UILabel Label4;

	public UILabel Label5;

	public UILabel Label6;

	public UILabel Label7;

	public UILabel Label8;

	public UILabel Label9;

	public GameObject LingQiGride;

	public GameObject LingQifengexianImage;

	public GameObject lingqiGridImage;

	public List<Sprite> lingQiGrid;

	public List<Sprite> DownSprite;

	public GameObject DownSpriteList;

	public GameObject DownBtn;

	public GameObject DownSpriteObj;

	public UITexture pingZhi;

	public UITexture icon;

	public GameObject CenterLayer;

	public GameObject UpSprite;

	public GameObject TooltipHelp;

	public GameObject PlayerInfo;

	public GameObject LingWuInfo;

	public GameObject moneyGameobject;

	public UIWidget centerSize;

	public UILabel CenterText1;

	public UILabel CenterText2;

	public UILabel CenterText3;

	public GameObject Slot;

	public GameObject pingji;

	public string btn1Name = "";

	public string btn2Name = "";

	public int BtnType = 1;

	public int BaseHight;

	private int UpHight;

	private int CenterHight;

	private int DownHight;

	public GameObject wudao;

	public UI2DSprite wudao_Icon;

	public UILabel wuDaoCast;

	public UILabel wudaoYaoQiu;

	private new void Start()
	{
	}

	public void Close()
	{
		showTooltip = false;
	}

	public void Clear()
	{
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		showType = 0;
		if ((Object)(object)Slot != (Object)null)
		{
			Slot.SetActive(true);
		}
		pingji.SetActive(true);
		wudao.SetActive(false);
		PlayerInfo.SetActive(false);
		LingWuInfo.SetActive(false);
		((Component)wuDaoCast).gameObject.SetActive(false);
		((Component)wudaoYaoQiu).gameObject.SetActive(false);
		Label9.text = "";
		Label8.text = "";
		Label3.text = "";
		Label7.text = "";
		((Component)Label7).gameObject.SetActive(true);
		((Component)Label8).gameObject.SetActive(true);
		((Component)Label9).gameObject.SetActive(true);
		CenterLayer.SetActive(false);
		UpSprite.SetActive(false);
		CenterText1.text = "";
		CenterText2.text = "";
		CenterText3.text = "";
		((Component)CenterText1).gameObject.SetActive(true);
		((Component)CenterText2).gameObject.SetActive(true);
		((Component)CenterText3).gameObject.SetActive(true);
		((Component)LingQiGride.transform.parent).gameObject.SetActive(false);
		((Component)Label1).transform.localPosition = new Vector3(((Component)Label1).transform.localPosition.x, -251f, 0f);
		CenterHight = 0;
		SetBtnSprite();
		btn1Name = "";
		btn2Name = "";
		CenterHight = 40;
		centerSize.height = CenterHight;
		moneyGameobject.SetActive(false);
	}

	public void ShowSkillGride()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		((Component)LingQiGride.transform.parent).gameObject.SetActive(true);
		((Component)Label1).transform.localPosition = new Vector3(((Component)Label1).transform.localPosition.x, -316f, 0f);
	}

	public void ShowMoney()
	{
		moneyGameobject.SetActive(true);
	}

	public void SetBtnSprite(string name1 = "", string name2 = "")
	{
		if (name1 != "" && name2 != "")
		{
			DownSpriteObj.GetComponent<UI2DSprite>().sprite2D = DownSprite[2];
			DownSpriteObj.GetComponent<UI2DSprite>().height = 114;
			centerSize.height = CenterHight + ((!CenterLayer.activeSelf) ? 40 : 0);
		}
		else if (name1 != "")
		{
			DownSpriteObj.GetComponent<UI2DSprite>().sprite2D = DownSprite[1];
			DownSpriteObj.GetComponent<UI2DSprite>().height = 113;
			DownBtn.SetActive(true);
			DownBtn.GetComponentInChildren<UILabel>().text = name1;
			UIButton componentInChildren = DownBtn.GetComponentInChildren<UIButton>();
			componentInChildren.onClick.Clear();
			componentInChildren.onClick.Add(new EventDelegate(delegate
			{
				Singleton.ToolTipsBackGround.UseItem();
			}));
			centerSize.height = CenterHight + ((!CenterLayer.activeSelf) ? 40 : 0);
		}
		else
		{
			DownSpriteObj.GetComponent<UI2DSprite>().sprite2D = DownSprite[0];
			DownSpriteObj.GetComponent<UI2DSprite>().height = 87;
			DownBtn.SetActive(false);
		}
	}

	public void ShowSkillTime(string Desc)
	{
		LingWuInfo.SetActive(true);
		LingWuInfo.GetComponentInChildren<UILabel>().text = Desc;
	}

	public void ShowPlayerInfo()
	{
		PlayerInfo.SetActive(true);
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		ulong num = 99999999999uL;
		if (jsonData.instance.LevelUpDataJsonData[string.Concat(avatar.level)] != null)
		{
			num = (ulong)jsonData.instance.LevelUpDataJsonData[string.Concat(avatar.level)]["MaxExp"].n;
		}
		((Component)PlayerInfo.transform.Find("HP/Desc")).GetComponent<UILabel>().text = "[E0DDB4]" + avatar.HP + "[-][BFBA7D]/" + avatar.HP_Max;
		((Component)PlayerInfo.transform.Find("EXP/Desc")).GetComponent<UILabel>().text = "[E0DDB4]" + avatar.exp + "[-][BFBA7D]/" + num;
		((Component)PlayerInfo.transform.Find("dandu/Desc")).GetComponent<UILabel>().text = ("[E0DDB4]" + avatar.Dandu + "[-][BFBA7D]/" + 120) ?? "";
		if (jsonData.instance.XinJinJsonData[avatar.GetXinJingLevel().ToString()]["Max"].n > 0f)
		{
			int xinJingLevel = avatar.GetXinJingLevel();
			if (xinJingLevel == 7)
			{
				((Component)PlayerInfo.transform.Find("xinjin/Desc")).GetComponent<UILabel>().text = $"[E0DDB4]{avatar.xinjin}[-][BFBA7D]/-";
			}
			else
			{
				((Component)PlayerInfo.transform.Find("xinjin/Desc")).GetComponent<UILabel>().text = $"[E0DDB4]{avatar.xinjin}[-][BFBA7D]/{XinJinJsonData.DataDict[xinJingLevel].Max}";
			}
		}
		else
		{
			((Component)PlayerInfo.transform.Find("xinjin/Desc")).GetComponent<UILabel>().text = $"[E0DDB4]{avatar.xinjin}[-][BFBA7D]/0";
		}
	}

	public void setCenterTextTitle(string Title1 = "", string Title2 = "", string Title3 = "")
	{
		CenterLayer.SetActive(true);
		UpSprite.SetActive(true);
		setTitleText(Title1, CenterText1);
		setTitleText(Title2, CenterText2);
		setTitleText(Title3, CenterText3);
		if (Title1 != "")
		{
			CenterHight = 158;
		}
		if (Title3 != "")
		{
			CenterHight = 207;
			CenterText3.text = CenterText3.text ?? "";
		}
		centerSize.height = CenterHight;
	}

	private void setTitleText(string Title1, UILabel uILabel)
	{
		if (Title1 != "")
		{
			uILabel.text = ((Title1 == "【丹毒】") ? "[dd61ff]" : "[ab8d51]") + Title1;
			((Component)uILabel).gameObject.SetActive(true);
		}
		else
		{
			((Component)uILabel).gameObject.SetActive(false);
		}
	}

	protected override void Update()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		base.Update();
		if ((Object)(object)TooltipHelp != (Object)null)
		{
			int num = Screen.width / 2;
			float num2 = ((Input.mousePosition.x < (float)num) ? (-514f) : 544f);
			TooltipHelp.transform.parent.localPosition = new Vector3(num2, 185f, 0f);
		}
	}
}
