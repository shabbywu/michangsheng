using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003B0 RID: 944
public class UIBiGuanGanWuPanel : TabPanelBase
{
	// Token: 0x06001A33 RID: 6707 RVA: 0x0001667D File Offset: 0x0001487D
	private void Awake()
	{
		UIBiGuanGanWuPanel.Inst = this;
	}

	// Token: 0x06001A34 RID: 6708 RVA: 0x00016685 File Offset: 0x00014885
	public override void OnPanelShow()
	{
		base.OnPanelShow();
		this.RefreshUI();
	}

	// Token: 0x06001A35 RID: 6709 RVA: 0x00016693 File Offset: 0x00014893
	public void RefreshUI()
	{
		this.RefreshInventory();
		this.SetNull();
		UIBiGuanPanel.Inst.RefreshKeFangTime();
	}

	// Token: 0x06001A36 RID: 6710 RVA: 0x000E7958 File Offset: 0x000E5B58
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

	// Token: 0x06001A37 RID: 6711 RVA: 0x000E7A20 File Offset: 0x000E5C20
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

	// Token: 0x06001A38 RID: 6712 RVA: 0x000E7AA8 File Offset: 0x000E5CA8
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

	// Token: 0x06001A39 RID: 6713 RVA: 0x000166AB File Offset: 0x000148AB
	public void OnGanWuButtonClick()
	{
		this.GanWu();
	}

	// Token: 0x06001A3A RID: 6714 RVA: 0x000E7B34 File Offset: 0x000E5D34
	public void GanWu()
	{
		if (this.NowSiXu != null)
		{
			ResManager.inst.LoadPrefab("GanWuSelect").Inst(null).GetComponent<GanWuSelect>().Init(this.NowSiXu.info["uuid"].str, WuDaoAllTypeJson.DataDict[this.NowSiXu.WuDaoType].name1, Tools.instance.getPlayer().wuDaoMag.CalcGanWuTime(this.NowSiXu.info));
		}
	}

	// Token: 0x06001A3B RID: 6715 RVA: 0x000166B3 File Offset: 0x000148B3
	public void OnFilterChanged()
	{
		this.RefreshUI();
	}

	// Token: 0x04001588 RID: 5512
	public static UIBiGuanGanWuPanel Inst;

	// Token: 0x04001589 RID: 5513
	public RectTransform SVContent;

	// Token: 0x0400158A RID: 5514
	public Dropdown Fillter1;

	// Token: 0x0400158B RID: 5515
	public Dropdown Filter2;

	// Token: 0x0400158C RID: 5516
	public Text RightTitle;

	// Token: 0x0400158D RID: 5517
	public Text XiaoGuoText;

	// Token: 0x0400158E RID: 5518
	public Text TimeText;

	// Token: 0x0400158F RID: 5519
	public Text XiaoHaoText;

	// Token: 0x04001590 RID: 5520
	public Text ShuoMingText;

	// Token: 0x04001591 RID: 5521
	public ToggleGroup ToggleGroup;

	// Token: 0x04001592 RID: 5522
	public GameObject SiXuPrefab;

	// Token: 0x04001593 RID: 5523
	public Image BtnImage1;

	// Token: 0x04001594 RID: 5524
	public Image BtnImage2;

	// Token: 0x04001595 RID: 5525
	public Material GreyMat;

	// Token: 0x04001596 RID: 5526
	public List<Sprite> WuDaoTypeSpriteList;

	// Token: 0x04001597 RID: 5527
	[HideInInspector]
	public SiXuData NowSiXu;
}
