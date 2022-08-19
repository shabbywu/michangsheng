using System;
using System.Collections.Generic;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;

// Token: 0x0200042A RID: 1066
public class TooltipItem : TooltipBase
{
	// Token: 0x0600220C RID: 8716 RVA: 0x00004095 File Offset: 0x00002295
	private new void Start()
	{
	}

	// Token: 0x0600220D RID: 8717 RVA: 0x000EA768 File Offset: 0x000E8968
	public void Close()
	{
		this.showTooltip = false;
	}

	// Token: 0x0600220E RID: 8718 RVA: 0x000EA774 File Offset: 0x000E8974
	public void Clear()
	{
		this.showType = 0;
		if (this.Slot != null)
		{
			this.Slot.SetActive(true);
		}
		this.pingji.SetActive(true);
		this.wudao.SetActive(false);
		this.PlayerInfo.SetActive(false);
		this.LingWuInfo.SetActive(false);
		this.wuDaoCast.gameObject.SetActive(false);
		this.wudaoYaoQiu.gameObject.SetActive(false);
		this.Label9.text = "";
		this.Label8.text = "";
		this.Label3.text = "";
		this.Label7.text = "";
		this.Label7.gameObject.SetActive(true);
		this.Label8.gameObject.SetActive(true);
		this.Label9.gameObject.SetActive(true);
		this.CenterLayer.SetActive(false);
		this.UpSprite.SetActive(false);
		this.CenterText1.text = "";
		this.CenterText2.text = "";
		this.CenterText3.text = "";
		this.CenterText1.gameObject.SetActive(true);
		this.CenterText2.gameObject.SetActive(true);
		this.CenterText3.gameObject.SetActive(true);
		this.LingQiGride.transform.parent.gameObject.SetActive(false);
		this.Label1.transform.localPosition = new Vector3(this.Label1.transform.localPosition.x, -251f, 0f);
		this.CenterHight = 0;
		this.SetBtnSprite("", "");
		this.btn1Name = "";
		this.btn2Name = "";
		this.CenterHight = 40;
		this.centerSize.height = this.CenterHight;
		this.moneyGameobject.SetActive(false);
	}

	// Token: 0x0600220F RID: 8719 RVA: 0x000EA984 File Offset: 0x000E8B84
	public void ShowSkillGride()
	{
		this.LingQiGride.transform.parent.gameObject.SetActive(true);
		this.Label1.transform.localPosition = new Vector3(this.Label1.transform.localPosition.x, -316f, 0f);
	}

	// Token: 0x06002210 RID: 8720 RVA: 0x000EA9E0 File Offset: 0x000E8BE0
	public void ShowMoney()
	{
		this.moneyGameobject.SetActive(true);
	}

	// Token: 0x06002211 RID: 8721 RVA: 0x000EA9F0 File Offset: 0x000E8BF0
	public void SetBtnSprite(string name1 = "", string name2 = "")
	{
		if (name1 != "" && name2 != "")
		{
			this.DownSpriteObj.GetComponent<UI2DSprite>().sprite2D = this.DownSprite[2];
			this.DownSpriteObj.GetComponent<UI2DSprite>().height = 114;
			this.centerSize.height = this.CenterHight + (this.CenterLayer.activeSelf ? 0 : 40);
			return;
		}
		if (name1 != "")
		{
			this.DownSpriteObj.GetComponent<UI2DSprite>().sprite2D = this.DownSprite[1];
			this.DownSpriteObj.GetComponent<UI2DSprite>().height = 113;
			this.DownBtn.SetActive(true);
			this.DownBtn.GetComponentInChildren<UILabel>().text = name1;
			UIButton componentInChildren = this.DownBtn.GetComponentInChildren<UIButton>();
			componentInChildren.onClick.Clear();
			componentInChildren.onClick.Add(new EventDelegate(delegate()
			{
				Singleton.ToolTipsBackGround.UseItem();
			}));
			this.centerSize.height = this.CenterHight + (this.CenterLayer.activeSelf ? 0 : 40);
			return;
		}
		this.DownSpriteObj.GetComponent<UI2DSprite>().sprite2D = this.DownSprite[0];
		this.DownSpriteObj.GetComponent<UI2DSprite>().height = 87;
		this.DownBtn.SetActive(false);
	}

	// Token: 0x06002212 RID: 8722 RVA: 0x000EAB68 File Offset: 0x000E8D68
	public void ShowSkillTime(string Desc)
	{
		this.LingWuInfo.SetActive(true);
		this.LingWuInfo.GetComponentInChildren<UILabel>().text = Desc;
	}

	// Token: 0x06002213 RID: 8723 RVA: 0x000EAB88 File Offset: 0x000E8D88
	public void ShowPlayerInfo()
	{
		this.PlayerInfo.SetActive(true);
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		ulong num = 99999999999UL;
		if (jsonData.instance.LevelUpDataJsonData[string.Concat(avatar.level)] != null)
		{
			num = (ulong)jsonData.instance.LevelUpDataJsonData[string.Concat(avatar.level)]["MaxExp"].n;
		}
		this.PlayerInfo.transform.Find("HP/Desc").GetComponent<UILabel>().text = string.Concat(new object[]
		{
			"[E0DDB4]",
			avatar.HP,
			"[-][BFBA7D]/",
			avatar.HP_Max
		});
		this.PlayerInfo.transform.Find("EXP/Desc").GetComponent<UILabel>().text = string.Concat(new object[]
		{
			"[E0DDB4]",
			avatar.exp,
			"[-][BFBA7D]/",
			num
		});
		this.PlayerInfo.transform.Find("dandu/Desc").GetComponent<UILabel>().text = (string.Concat(new object[]
		{
			"[E0DDB4]",
			avatar.Dandu,
			"[-][BFBA7D]/",
			120
		}) ?? "");
		if (jsonData.instance.XinJinJsonData[avatar.GetXinJingLevel().ToString()]["Max"].n <= 0f)
		{
			this.PlayerInfo.transform.Find("xinjin/Desc").GetComponent<UILabel>().text = string.Format("[E0DDB4]{0}[-][BFBA7D]/0", avatar.xinjin);
			return;
		}
		int xinJingLevel = avatar.GetXinJingLevel();
		if (xinJingLevel == 7)
		{
			this.PlayerInfo.transform.Find("xinjin/Desc").GetComponent<UILabel>().text = string.Format("[E0DDB4]{0}[-][BFBA7D]/-", avatar.xinjin);
			return;
		}
		this.PlayerInfo.transform.Find("xinjin/Desc").GetComponent<UILabel>().text = string.Format("[E0DDB4]{0}[-][BFBA7D]/{1}", avatar.xinjin, XinJinJsonData.DataDict[xinJingLevel].Max);
	}

	// Token: 0x06002214 RID: 8724 RVA: 0x000EAE04 File Offset: 0x000E9004
	public void setCenterTextTitle(string Title1 = "", string Title2 = "", string Title3 = "")
	{
		this.CenterLayer.SetActive(true);
		this.UpSprite.SetActive(true);
		this.setTitleText(Title1, this.CenterText1);
		this.setTitleText(Title2, this.CenterText2);
		this.setTitleText(Title3, this.CenterText3);
		if (Title1 != "")
		{
			this.CenterHight = 158;
		}
		if (Title3 != "")
		{
			this.CenterHight = 207;
			this.CenterText3.text = (this.CenterText3.text ?? "");
		}
		this.centerSize.height = this.CenterHight;
	}

	// Token: 0x06002215 RID: 8725 RVA: 0x000EAEB0 File Offset: 0x000E90B0
	private void setTitleText(string Title1, UILabel uILabel)
	{
		if (Title1 != "")
		{
			uILabel.text = ((Title1 == "【丹毒】") ? "[dd61ff]" : "[ab8d51]") + Title1;
			uILabel.gameObject.SetActive(true);
			return;
		}
		uILabel.gameObject.SetActive(false);
	}

	// Token: 0x06002216 RID: 8726 RVA: 0x000EAF08 File Offset: 0x000E9108
	protected override void Update()
	{
		base.Update();
		if (this.TooltipHelp != null)
		{
			int num = Screen.width / 2;
			float num2 = (Input.mousePosition.x < (float)num) ? -514f : 544f;
			this.TooltipHelp.transform.parent.localPosition = new Vector3(num2, 185f, 0f);
		}
	}

	// Token: 0x04001B72 RID: 7026
	public UILabel Label1;

	// Token: 0x04001B73 RID: 7027
	public UILabel Label2;

	// Token: 0x04001B74 RID: 7028
	public UILabel Label3;

	// Token: 0x04001B75 RID: 7029
	public UILabel Label4;

	// Token: 0x04001B76 RID: 7030
	public UILabel Label5;

	// Token: 0x04001B77 RID: 7031
	public UILabel Label6;

	// Token: 0x04001B78 RID: 7032
	public UILabel Label7;

	// Token: 0x04001B79 RID: 7033
	public UILabel Label8;

	// Token: 0x04001B7A RID: 7034
	public UILabel Label9;

	// Token: 0x04001B7B RID: 7035
	public GameObject LingQiGride;

	// Token: 0x04001B7C RID: 7036
	public GameObject LingQifengexianImage;

	// Token: 0x04001B7D RID: 7037
	public GameObject lingqiGridImage;

	// Token: 0x04001B7E RID: 7038
	public List<Sprite> lingQiGrid;

	// Token: 0x04001B7F RID: 7039
	public List<Sprite> DownSprite;

	// Token: 0x04001B80 RID: 7040
	public GameObject DownSpriteList;

	// Token: 0x04001B81 RID: 7041
	public GameObject DownBtn;

	// Token: 0x04001B82 RID: 7042
	public GameObject DownSpriteObj;

	// Token: 0x04001B83 RID: 7043
	public UITexture pingZhi;

	// Token: 0x04001B84 RID: 7044
	public UITexture icon;

	// Token: 0x04001B85 RID: 7045
	public GameObject CenterLayer;

	// Token: 0x04001B86 RID: 7046
	public GameObject UpSprite;

	// Token: 0x04001B87 RID: 7047
	public GameObject TooltipHelp;

	// Token: 0x04001B88 RID: 7048
	public GameObject PlayerInfo;

	// Token: 0x04001B89 RID: 7049
	public GameObject LingWuInfo;

	// Token: 0x04001B8A RID: 7050
	public GameObject moneyGameobject;

	// Token: 0x04001B8B RID: 7051
	public UIWidget centerSize;

	// Token: 0x04001B8C RID: 7052
	public UILabel CenterText1;

	// Token: 0x04001B8D RID: 7053
	public UILabel CenterText2;

	// Token: 0x04001B8E RID: 7054
	public UILabel CenterText3;

	// Token: 0x04001B8F RID: 7055
	public GameObject Slot;

	// Token: 0x04001B90 RID: 7056
	public GameObject pingji;

	// Token: 0x04001B91 RID: 7057
	public string btn1Name = "";

	// Token: 0x04001B92 RID: 7058
	public string btn2Name = "";

	// Token: 0x04001B93 RID: 7059
	public int BtnType = 1;

	// Token: 0x04001B94 RID: 7060
	public int BaseHight;

	// Token: 0x04001B95 RID: 7061
	private int UpHight;

	// Token: 0x04001B96 RID: 7062
	private int CenterHight;

	// Token: 0x04001B97 RID: 7063
	private int DownHight;

	// Token: 0x04001B98 RID: 7064
	public GameObject wudao;

	// Token: 0x04001B99 RID: 7065
	public UI2DSprite wudao_Icon;

	// Token: 0x04001B9A RID: 7066
	public UILabel wuDaoCast;

	// Token: 0x04001B9B RID: 7067
	public UILabel wudaoYaoQiu;
}
