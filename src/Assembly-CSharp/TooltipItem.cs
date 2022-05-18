using System;
using System.Collections.Generic;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;

// Token: 0x020005E0 RID: 1504
public class TooltipItem : TooltipBase
{
	// Token: 0x060025C8 RID: 9672 RVA: 0x000042DD File Offset: 0x000024DD
	private new void Start()
	{
	}

	// Token: 0x060025C9 RID: 9673 RVA: 0x0001E3FB File Offset: 0x0001C5FB
	public void Close()
	{
		this.showTooltip = false;
	}

	// Token: 0x060025CA RID: 9674 RVA: 0x0012BA68 File Offset: 0x00129C68
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

	// Token: 0x060025CB RID: 9675 RVA: 0x0012BC78 File Offset: 0x00129E78
	public void ShowSkillGride()
	{
		this.LingQiGride.transform.parent.gameObject.SetActive(true);
		this.Label1.transform.localPosition = new Vector3(this.Label1.transform.localPosition.x, -316f, 0f);
	}

	// Token: 0x060025CC RID: 9676 RVA: 0x0001E404 File Offset: 0x0001C604
	public void ShowMoney()
	{
		this.moneyGameobject.SetActive(true);
	}

	// Token: 0x060025CD RID: 9677 RVA: 0x0012BCD4 File Offset: 0x00129ED4
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

	// Token: 0x060025CE RID: 9678 RVA: 0x0001E412 File Offset: 0x0001C612
	public void ShowSkillTime(string Desc)
	{
		this.LingWuInfo.SetActive(true);
		this.LingWuInfo.GetComponentInChildren<UILabel>().text = Desc;
	}

	// Token: 0x060025CF RID: 9679 RVA: 0x0012BE4C File Offset: 0x0012A04C
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

	// Token: 0x060025D0 RID: 9680 RVA: 0x0012C0C8 File Offset: 0x0012A2C8
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

	// Token: 0x060025D1 RID: 9681 RVA: 0x0012C174 File Offset: 0x0012A374
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

	// Token: 0x060025D2 RID: 9682 RVA: 0x0012C1CC File Offset: 0x0012A3CC
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

	// Token: 0x0400203C RID: 8252
	public UILabel Label1;

	// Token: 0x0400203D RID: 8253
	public UILabel Label2;

	// Token: 0x0400203E RID: 8254
	public UILabel Label3;

	// Token: 0x0400203F RID: 8255
	public UILabel Label4;

	// Token: 0x04002040 RID: 8256
	public UILabel Label5;

	// Token: 0x04002041 RID: 8257
	public UILabel Label6;

	// Token: 0x04002042 RID: 8258
	public UILabel Label7;

	// Token: 0x04002043 RID: 8259
	public UILabel Label8;

	// Token: 0x04002044 RID: 8260
	public UILabel Label9;

	// Token: 0x04002045 RID: 8261
	public GameObject LingQiGride;

	// Token: 0x04002046 RID: 8262
	public GameObject LingQifengexianImage;

	// Token: 0x04002047 RID: 8263
	public GameObject lingqiGridImage;

	// Token: 0x04002048 RID: 8264
	public List<Sprite> lingQiGrid;

	// Token: 0x04002049 RID: 8265
	public List<Sprite> DownSprite;

	// Token: 0x0400204A RID: 8266
	public GameObject DownSpriteList;

	// Token: 0x0400204B RID: 8267
	public GameObject DownBtn;

	// Token: 0x0400204C RID: 8268
	public GameObject DownSpriteObj;

	// Token: 0x0400204D RID: 8269
	public UITexture pingZhi;

	// Token: 0x0400204E RID: 8270
	public UITexture icon;

	// Token: 0x0400204F RID: 8271
	public GameObject CenterLayer;

	// Token: 0x04002050 RID: 8272
	public GameObject UpSprite;

	// Token: 0x04002051 RID: 8273
	public GameObject TooltipHelp;

	// Token: 0x04002052 RID: 8274
	public GameObject PlayerInfo;

	// Token: 0x04002053 RID: 8275
	public GameObject LingWuInfo;

	// Token: 0x04002054 RID: 8276
	public GameObject moneyGameobject;

	// Token: 0x04002055 RID: 8277
	public UIWidget centerSize;

	// Token: 0x04002056 RID: 8278
	public UILabel CenterText1;

	// Token: 0x04002057 RID: 8279
	public UILabel CenterText2;

	// Token: 0x04002058 RID: 8280
	public UILabel CenterText3;

	// Token: 0x04002059 RID: 8281
	public GameObject Slot;

	// Token: 0x0400205A RID: 8282
	public GameObject pingji;

	// Token: 0x0400205B RID: 8283
	public string btn1Name = "";

	// Token: 0x0400205C RID: 8284
	public string btn2Name = "";

	// Token: 0x0400205D RID: 8285
	public int BtnType = 1;

	// Token: 0x0400205E RID: 8286
	public int BaseHight;

	// Token: 0x0400205F RID: 8287
	private int UpHight;

	// Token: 0x04002060 RID: 8288
	private int CenterHight;

	// Token: 0x04002061 RID: 8289
	private int DownHight;

	// Token: 0x04002062 RID: 8290
	public GameObject wudao;

	// Token: 0x04002063 RID: 8291
	public UI2DSprite wudao_Icon;

	// Token: 0x04002064 RID: 8292
	public UILabel wuDaoCast;

	// Token: 0x04002065 RID: 8293
	public UILabel wudaoYaoQiu;
}
