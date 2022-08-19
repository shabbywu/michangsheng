using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000287 RID: 647
public class UIBiGuanGanWuPanel : TabPanelBase
{
	// Token: 0x0600175F RID: 5983 RVA: 0x000A041C File Offset: 0x0009E61C
	private void Awake()
	{
		UIBiGuanGanWuPanel.Inst = this;
	}

	// Token: 0x06001760 RID: 5984 RVA: 0x000A0424 File Offset: 0x0009E624
	public override void OnPanelShow()
	{
		base.OnPanelShow();
		this.RefreshUI();
	}

	// Token: 0x06001761 RID: 5985 RVA: 0x000A0432 File Offset: 0x0009E632
	public void RefreshUI()
	{
		this.RefreshInventory();
		this.SetNull();
		UIBiGuanPanel.Inst.RefreshKeFangTime();
	}

	// Token: 0x06001762 RID: 5986 RVA: 0x000A044C File Offset: 0x0009E64C
	public void RefreshInventory()
	{
		this.SVContent.DestoryAllChild();
		Avatar player = PlayerEx.Player;
		int value = this.Fillter1.value;
		int value2 = this.Filter2.value;
		foreach (JSONObject json in player.LingGuang.list)
		{
			SiXuData siXuData = new SiXuData(json);
			if ((value2 == 0 || siXuData.PinJie == value2) && (value == 0 || siXuData.wuDaoFilter == value))
			{
				UISiXuItem component = Object.Instantiate<GameObject>(this.SiXuPrefab, this.SVContent).GetComponent<UISiXuItem>();
				component.SetData(siXuData);
				component.gameObject.GetComponent<Toggle>().group = this.ToggleGroup;
			}
		}
	}

	// Token: 0x06001763 RID: 5987 RVA: 0x000A0514 File Offset: 0x0009E714
	public void SetNull()
	{
		this.RightTitle.text = "感悟";
		this.XiaoGuoText.text = "";
		this.TimeText.text = "";
		this.XiaoHaoText.text = "";
		this.ShuoMingText.text = "";
		this.BtnImage1.material = this.GreyMat;
		this.BtnImage2.material = this.GreyMat;
		this.NowSiXu = null;
	}

	// Token: 0x06001764 RID: 5988 RVA: 0x000A059C File Offset: 0x0009E79C
	public void SetGanWu(SiXuData data)
	{
		if (data == null)
		{
			this.SetNull();
			return;
		}
		this.NowSiXu = data;
		this.RightTitle.text = data.WuDaoTypeStr;
		this.XiaoGuoText.text = data.XiaoGuo;
		this.TimeText.text = data.ShengYuTimeFull;
		this.XiaoHaoText.text = data.XiaoHao;
		this.ShuoMingText.text = data.ShuoMing;
		this.BtnImage1.material = null;
		this.BtnImage2.material = null;
	}

	// Token: 0x06001765 RID: 5989 RVA: 0x000A0627 File Offset: 0x0009E827
	public void OnGanWuButtonClick()
	{
		this.GanWu();
	}

	// Token: 0x06001766 RID: 5990 RVA: 0x000A0630 File Offset: 0x0009E830
	public void GanWu()
	{
		if (this.NowSiXu != null)
		{
			ResManager.inst.LoadPrefab("GanWuSelect").Inst(null).GetComponent<GanWuSelect>().Init(this.NowSiXu.info["uuid"].str, WuDaoAllTypeJson.DataDict[this.NowSiXu.WuDaoType].name1, Tools.instance.getPlayer().wuDaoMag.CalcGanWuTime(this.NowSiXu.info));
		}
	}

	// Token: 0x06001767 RID: 5991 RVA: 0x000A06B7 File Offset: 0x0009E8B7
	public void OnFilterChanged()
	{
		this.RefreshUI();
	}

	// Token: 0x04001212 RID: 4626
	public static UIBiGuanGanWuPanel Inst;

	// Token: 0x04001213 RID: 4627
	public RectTransform SVContent;

	// Token: 0x04001214 RID: 4628
	public Dropdown Fillter1;

	// Token: 0x04001215 RID: 4629
	public Dropdown Filter2;

	// Token: 0x04001216 RID: 4630
	public Text RightTitle;

	// Token: 0x04001217 RID: 4631
	public Text XiaoGuoText;

	// Token: 0x04001218 RID: 4632
	public Text TimeText;

	// Token: 0x04001219 RID: 4633
	public Text XiaoHaoText;

	// Token: 0x0400121A RID: 4634
	public Text ShuoMingText;

	// Token: 0x0400121B RID: 4635
	public ToggleGroup ToggleGroup;

	// Token: 0x0400121C RID: 4636
	public GameObject SiXuPrefab;

	// Token: 0x0400121D RID: 4637
	public Image BtnImage1;

	// Token: 0x0400121E RID: 4638
	public Image BtnImage2;

	// Token: 0x0400121F RID: 4639
	public Material GreyMat;

	// Token: 0x04001220 RID: 4640
	public List<Sprite> WuDaoTypeSpriteList;

	// Token: 0x04001221 RID: 4641
	[HideInInspector]
	public SiXuData NowSiXu;
}
