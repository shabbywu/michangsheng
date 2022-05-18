using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000326 RID: 806
public class UILingTianPanel : MonoBehaviour, IESCClose
{
	// Token: 0x17000281 RID: 641
	// (get) Token: 0x060017BE RID: 6078 RVA: 0x00014F41 File Offset: 0x00013141
	// (set) Token: 0x060017BF RID: 6079 RVA: 0x00014F49 File Offset: 0x00013149
	[HideInInspector]
	public bool IsShouGe
	{
		get
		{
			return this.isShouGe;
		}
		set
		{
			this.isShouGe = value;
			if (this.isShouGe)
			{
				Cursor.SetCursor(this.ShouGeCur, Vector2.zero, 0);
				return;
			}
			Cursor.SetCursor(null, Vector2.zero, 0);
		}
	}

	// Token: 0x060017C0 RID: 6080 RVA: 0x00014F78 File Offset: 0x00013178
	private void Awake()
	{
		UILingTianPanel.Inst = this;
	}

	// Token: 0x060017C1 RID: 6081 RVA: 0x000CF114 File Offset: 0x000CD314
	public void RefreshUI()
	{
		this.IsShouGe = false;
		this.CalcSpeed(0);
		foreach (UILingTianCell uilingTianCell in this.LingTianList)
		{
			uilingTianCell.RefreshUI();
		}
		this.PlayerInventory.RefreshUI();
		Avatar player = PlayerEx.Player;
		int i = player.DongFuData[string.Format("DongFu{0}", DongFuManager.NowDongFuID)]["LingYanLevel"].I;
		int i2 = player.DongFuData[string.Format("DongFu{0}", DongFuManager.NowDongFuID)]["JuLingZhenLevel"].I;
		int lingtiansudu = DFLingYanLevel.DataDict[i].lingtiansudu;
		int lingtiansudu2 = DFZhenYanLevel.DataDict[i2].lingtiansudu;
		int lingtiancuishengsudu = DFZhenYanLevel.DataDict[i2].lingtiancuishengsudu;
		int i3 = player.DongFuData[string.Format("DongFu{0}", DongFuManager.NowDongFuID)]["LingTian"]["CuiShengLingLi"].I;
		int num = lingtiansudu + lingtiansudu2;
		if (i3 > 0)
		{
			num += lingtiancuishengsudu;
			this.LingLiSpeedText.text = string.Format("灵气供给 <color=#99d0d1>{0}({1}+{2})</color>/月", num, lingtiansudu + lingtiansudu2, lingtiancuishengsudu);
			this.LvPingBtn1.SetActive(false);
			this.LvPingbtn2.SetActive(true);
			int num2 = this.CuiShengTime / 12;
			int num3 = this.CuiShengTime % 12;
			if (num2 > 0)
			{
				this.LvPingShengYuTime.text = string.Format("剩{0}年{1}月", num2, num3);
			}
			else
			{
				this.LvPingShengYuTime.text = string.Format("剩{0}月", num3);
			}
		}
		else
		{
			this.LingLiSpeedText.text = string.Format("灵气供给 <color=#99d0d1>{0}</color>/月", num);
			this.LvPingBtn1.SetActive(true);
			this.LvPingbtn2.SetActive(false);
		}
		if (!this.HasLvPing())
		{
			this.LvPingBtn1.SetActive(false);
			this.LvPingbtn2.SetActive(false);
		}
	}

	// Token: 0x060017C2 RID: 6082 RVA: 0x000CF354 File Offset: 0x000CD554
	public void CalcSpeed(int exLingShi = 0)
	{
		Avatar player = PlayerEx.Player;
		string index = string.Format("DongFu{0}", DongFuManager.NowDongFuID);
		int num = 0;
		for (int i = 0; i < DongFuManager.LingTianCount; i++)
		{
			if (player.DongFuData[index]["LingTian"][string.Format("Slot{0}", i)]["ID"].I > 0)
			{
				num++;
			}
		}
		int i2 = player.DongFuData[index]["LingYanLevel"].I;
		int i3 = player.DongFuData[index]["JuLingZhenLevel"].I;
		int lingtiansudu = DFLingYanLevel.DataDict[i2].lingtiansudu;
		int lingtiansudu2 = DFZhenYanLevel.DataDict[i3].lingtiansudu;
		int lingtiancuishengsudu = DFZhenYanLevel.DataDict[i3].lingtiancuishengsudu;
		int num2 = player.DongFuData[index]["LingTian"]["CuiShengLingLi"].I + exLingShi;
		if (num > 0)
		{
			this.BaseSpeedPer = (float)(lingtiansudu + lingtiansudu2) / (float)num;
			this.CuiShengSpeedPer = (float)lingtiancuishengsudu / (float)num;
		}
		this.CuiShengTime = num2 / lingtiancuishengsudu;
		this.CuiShengLingShi50Year = 600 * lingtiancuishengsudu;
	}

	// Token: 0x060017C3 RID: 6083 RVA: 0x000CF4A8 File Offset: 0x000CD6A8
	public void OnShouGeBtnClick()
	{
		this.IsShouGe = !this.IsShouGe;
		if (this.IsShouGe)
		{
			this.ShouGeBtn1.SetActive(false);
			this.ShouGeBtn2.SetActive(true);
			return;
		}
		this.ShouGeBtn1.SetActive(true);
		this.ShouGeBtn2.SetActive(false);
	}

	// Token: 0x060017C4 RID: 6084 RVA: 0x00014F80 File Offset: 0x00013180
	public bool HasLvPing()
	{
		return PlayerEx.Player.hasItem(10046);
	}

	// Token: 0x060017C5 RID: 6085 RVA: 0x00014F91 File Offset: 0x00013191
	public void OnLiKaiBtnClick()
	{
		this.IsShouGe = false;
		UIDongFu.Inst.HideLingTianPanel();
	}

	// Token: 0x060017C6 RID: 6086 RVA: 0x00014FA4 File Offset: 0x000131A4
	public void OnLvPingBtnClick()
	{
		if (PlayerEx.Player.money > 0UL)
		{
			UILingLiChongNeng.Show();
			return;
		}
		UIPopTip.Inst.Pop("没有灵石无法使用", PopTipIconType.叹号);
	}

	// Token: 0x060017C7 RID: 6087 RVA: 0x00014FCA File Offset: 0x000131CA
	public bool TryEscClose()
	{
		this.OnLiKaiBtnClick();
		return true;
	}

	// Token: 0x0400131E RID: 4894
	public static UILingTianPanel Inst;

	// Token: 0x0400131F RID: 4895
	public List<UILingTianCell> LingTianList;

	// Token: 0x04001320 RID: 4896
	public Text LingLiSpeedText;

	// Token: 0x04001321 RID: 4897
	public UIInventory PlayerInventory;

	// Token: 0x04001322 RID: 4898
	public GameObject ShouGeBtn1;

	// Token: 0x04001323 RID: 4899
	public GameObject ShouGeBtn2;

	// Token: 0x04001324 RID: 4900
	public Texture2D ShouGeCur;

	// Token: 0x04001325 RID: 4901
	public GameObject LvPingBtn1;

	// Token: 0x04001326 RID: 4902
	public GameObject LvPingbtn2;

	// Token: 0x04001327 RID: 4903
	public Text LvPingShengYuTime;

	// Token: 0x04001328 RID: 4904
	[HideInInspector]
	public float BaseSpeedPer;

	// Token: 0x04001329 RID: 4905
	[HideInInspector]
	public float CuiShengSpeedPer;

	// Token: 0x0400132A RID: 4906
	[HideInInspector]
	public int CuiShengTime;

	// Token: 0x0400132B RID: 4907
	[HideInInspector]
	public int CuiShengLingShi50Year;

	// Token: 0x0400132C RID: 4908
	private bool isShouGe;
}
