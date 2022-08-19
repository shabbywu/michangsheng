using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200020E RID: 526
public class UILingTianPanel : MonoBehaviour, IESCClose
{
	// Token: 0x17000239 RID: 569
	// (get) Token: 0x0600150D RID: 5389 RVA: 0x000869AB File Offset: 0x00084BAB
	// (set) Token: 0x0600150E RID: 5390 RVA: 0x000869B3 File Offset: 0x00084BB3
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

	// Token: 0x0600150F RID: 5391 RVA: 0x000869E2 File Offset: 0x00084BE2
	private void Awake()
	{
		UILingTianPanel.Inst = this;
	}

	// Token: 0x06001510 RID: 5392 RVA: 0x000869EC File Offset: 0x00084BEC
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

	// Token: 0x06001511 RID: 5393 RVA: 0x00086C2C File Offset: 0x00084E2C
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

	// Token: 0x06001512 RID: 5394 RVA: 0x00086D80 File Offset: 0x00084F80
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

	// Token: 0x06001513 RID: 5395 RVA: 0x00086DD5 File Offset: 0x00084FD5
	public bool HasLvPing()
	{
		return PlayerEx.Player.hasItem(10046);
	}

	// Token: 0x06001514 RID: 5396 RVA: 0x00086DE6 File Offset: 0x00084FE6
	public void OnLiKaiBtnClick()
	{
		this.IsShouGe = false;
		UIDongFu.Inst.HideLingTianPanel();
	}

	// Token: 0x06001515 RID: 5397 RVA: 0x00086DF9 File Offset: 0x00084FF9
	public void OnLvPingBtnClick()
	{
		if (PlayerEx.Player.money > 0UL)
		{
			UILingLiChongNeng.Show();
			return;
		}
		UIPopTip.Inst.Pop("没有灵石无法使用", PopTipIconType.叹号);
	}

	// Token: 0x06001516 RID: 5398 RVA: 0x00086E1F File Offset: 0x0008501F
	public bool TryEscClose()
	{
		this.OnLiKaiBtnClick();
		return true;
	}

	// Token: 0x04000FCE RID: 4046
	public static UILingTianPanel Inst;

	// Token: 0x04000FCF RID: 4047
	public List<UILingTianCell> LingTianList;

	// Token: 0x04000FD0 RID: 4048
	public Text LingLiSpeedText;

	// Token: 0x04000FD1 RID: 4049
	public UIInventory PlayerInventory;

	// Token: 0x04000FD2 RID: 4050
	public GameObject ShouGeBtn1;

	// Token: 0x04000FD3 RID: 4051
	public GameObject ShouGeBtn2;

	// Token: 0x04000FD4 RID: 4052
	public Texture2D ShouGeCur;

	// Token: 0x04000FD5 RID: 4053
	public GameObject LvPingBtn1;

	// Token: 0x04000FD6 RID: 4054
	public GameObject LvPingbtn2;

	// Token: 0x04000FD7 RID: 4055
	public Text LvPingShengYuTime;

	// Token: 0x04000FD8 RID: 4056
	[HideInInspector]
	public float BaseSpeedPer;

	// Token: 0x04000FD9 RID: 4057
	[HideInInspector]
	public float CuiShengSpeedPer;

	// Token: 0x04000FDA RID: 4058
	[HideInInspector]
	public int CuiShengTime;

	// Token: 0x04000FDB RID: 4059
	[HideInInspector]
	public int CuiShengLingShi50Year;

	// Token: 0x04000FDC RID: 4060
	private bool isShouGe;
}
