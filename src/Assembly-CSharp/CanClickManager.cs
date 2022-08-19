using System;
using System.Collections.Generic;
using Fungus;
using JiaoYi;
using script.ExchangeMeeting.UI.Interface;
using script.MenPaiTask.ZhangLao.UI;
using Tab;
using UnityEngine;
using YSGame.TianJiDaBi;
using YSGame.TuJian;

// Token: 0x020001B8 RID: 440
public class CanClickManager : MonoBehaviour
{
	// Token: 0x17000225 RID: 549
	// (get) Token: 0x0600125F RID: 4703 RVA: 0x0006F4B0 File Offset: 0x0006D6B0
	public static CanClickManager Inst
	{
		get
		{
			if (CanClickManager.inst == null)
			{
				GameObject gameObject = new GameObject("CanClickManager");
				Object.DontDestroyOnLoad(gameObject);
				CanClickManager.inst = gameObject.AddComponent<CanClickManager>();
			}
			return CanClickManager.inst;
		}
	}

	// Token: 0x06001260 RID: 4704 RVA: 0x0006F4DE File Offset: 0x0006D6DE
	private void LateUpdate()
	{
		this.IsFinshed = false;
	}

	// Token: 0x06001261 RID: 4705 RVA: 0x0006F4E8 File Offset: 0x0006D6E8
	public void RefreshCanClick(bool show = false)
	{
		this.ResultCount = 0;
		for (int i = 0; i < CanClickManager.CacheCount; i++)
		{
			this.ResultCache[i] = false;
		}
		if (!Tools.canClickFlag)
		{
			if (show)
			{
				Debug.Log("CanClickManager:因canClickFlag而不允许点击");
			}
			this.ResultCache[0] = true;
		}
		if (SelectLianDanCaiLiaoPage.Inst != null && SelectLianDanCaiLiaoPage.Inst.gameObject.activeInHierarchy)
		{
			if (show)
			{
				Debug.Log("CanClickManager:因正在炼丹选择材料界面而不允许点击");
			}
			this.ResultCache[1] = true;
		}
		if (UINPCLeftList.Inst != null && UINPCLeftList.Inst.IsMouseInUI)
		{
			if (show)
			{
				Debug.Log("CanClickManager:因鼠标在NPC左侧列表内而不允许点击");
			}
			this.ResultCache[2] = true;
		}
		if (UIHeadPanel.Inst != null && UIHeadPanel.Inst.IsMouseInUI)
		{
			if (show)
			{
				Debug.Log("CanClickManager:因鼠标在左上头像区内而不允许点击");
			}
			this.ResultCache[3] = true;
		}
		if (UINPCJiaoHu.Inst.NowIsJiaoHu)
		{
			if (show)
			{
				Debug.Log("CanClickManager:因正在进行NPC交互而不允许点击");
			}
			this.ResultCache[4] = true;
		}
		if (CyUIMag.inst != null)
		{
			if (show)
			{
				Debug.Log("CanClickManager:因正在传音符界面而不允许点击");
			}
			this.ResultCache[5] = true;
		}
		if (FpUIMag.inst != null)
		{
			if (show)
			{
				Debug.Log("CanClickManager:因正在战前准备界面而不允许点击");
			}
			this.ResultCache[6] = true;
		}
		if (TpUIMag.inst != null)
		{
			if (show)
			{
				Debug.Log("CanClickManager:因正在突破准备界面而不允许点击");
			}
			this.ResultCache[7] = true;
		}
		if (KeFangSelectTime.inst != null)
		{
			if (show)
			{
				Debug.Log("CanClickManager:因正在客房选择时间界面而不允许点击");
			}
			this.ResultCache[8] = true;
		}
		if (UIBiGuanPanel.Inst.PanelObj.activeInHierarchy)
		{
			if (show)
			{
				Debug.Log("CanClickManager:因正在客房选择时间界面而不允许点击");
			}
			this.ResultCache[9] = true;
		}
		if (UIDongFu.Inst.ScaleObj.activeInHierarchy)
		{
			if (show)
			{
				Debug.Log("CanClickManager:因正在洞府界面而不允许点击");
			}
			this.ResultCache[10] = true;
		}
		if (UIMenPaiShop.Inst.ScaleObj.activeInHierarchy)
		{
			if (show)
			{
				Debug.Log("CanClickManager:因正在门派商店界面而不允许点击");
			}
			this.ResultCache[11] = true;
		}
		if (UInputBox.IsShow)
		{
			if (show)
			{
				Debug.Log("CanClickManager:因正在UInputBox界面而不允许点击");
			}
			this.ResultCache[12] = true;
		}
		if (USelectBox.IsShow)
		{
			if (show)
			{
				Debug.Log("CanClickManager:因正在确定选择框界面而不允许点击");
			}
			this.ResultCache[13] = true;
		}
		if (UCheckBox.IsShow)
		{
			if (show)
			{
				Debug.Log("CanClickManager:因正在确定框界面而不允许点击");
			}
			this.ResultCache[14] = true;
		}
		if (USelectNum.IsShow)
		{
			if (show)
			{
				Debug.Log("CanClickManager:因正在数量选择框界面而不允许点击");
			}
			this.ResultCache[15] = true;
		}
		if (TuJianManager.Inst != null && TuJianManager.Inst._Canvas.enabled)
		{
			if (show)
			{
				Debug.Log("CanClickManager:因正在图鉴界面而不允许点击");
			}
			this.ResultCache[16] = true;
		}
		if (UIHuaShenRuDaoSelect.Inst != null && UIHuaShenRuDaoSelect.Inst.IsShow)
		{
			if (show)
			{
				Debug.Log("CanClickManager:因正在化神入道选择界面而不允许点击");
			}
			this.ResultCache[17] = true;
		}
		if (SingletonMono<TabUIMag>.Instance != null)
		{
			if (show)
			{
				Debug.Log("CanClickManager:因正在Tab界面而不允许点击");
			}
			this.ResultCache[18] = true;
		}
		if (UIMapPanel.Inst != null && UIMapPanel.Inst.IsShow)
		{
			if (show)
			{
				Debug.Log("CanClickManager:因正在地图界面而不允许点击");
			}
			this.ResultCache[19] = true;
		}
		if (JiaoYiUIMag.Inst != null)
		{
			if (show)
			{
				Debug.Log("CanClickManager:因正在交易界面而不允许点击");
			}
			this.ResultCache[20] = true;
		}
		if (UIXiuChuanPanel.Inst != null)
		{
			if (show)
			{
				Debug.Log("CanClickManager:因正在修船界面而不允许点击");
			}
			this.ResultCache[21] = true;
		}
		if (UITianJiDaBiSaiChang.Inst != null)
		{
			if (show)
			{
				Debug.Log("CanClickManager:因正在天机大比赛场界面而不允许点击");
			}
			this.ResultCache[22] = true;
		}
		if (UITianJiDaBiPlayerInfo.Inst != null)
		{
			if (show)
			{
				Debug.Log("CanClickManager:因正在天机大比参赛人员信息界面而不允许点击");
			}
			this.ResultCache[23] = true;
		}
		if (UITianJiDaBiRankPanel.Inst != null)
		{
			if (show)
			{
				Debug.Log("CanClickManager:因正在天机榜界面而不允许点击");
			}
			this.ResultCache[24] = true;
		}
		if (UIMiniShop.Inst != null)
		{
			if (show)
			{
				Debug.Log("CanClickManager:因正在迷你商店界面而不允许点击");
			}
			this.ResultCache[25] = true;
		}
		if (UIDuJieZhunBei.Inst != null)
		{
			if (show)
			{
				Debug.Log("CanClickManager:因正在渡劫准备界面而不允许点击");
			}
			this.ResultCache[26] = true;
		}
		if (UIJianLingPanel.Inst != null)
		{
			if (show)
			{
				Debug.Log("CanClickManager:因正在剑灵界面而不允许点击");
			}
			this.ResultCache[27] = true;
		}
		if (UIJianLingXianSuoPanel.Inst != null)
		{
			if (show)
			{
				Debug.Log("CanClickManager:因正在剑灵线索界面而不允许点击");
			}
			this.ResultCache[28] = true;
		}
		if (UIJianLingQingJiaoPanel.Inst != null)
		{
			if (show)
			{
				Debug.Log("CanClickManager:因正在剑灵请教界面而不允许点击");
			}
			this.ResultCache[29] = true;
		}
		if (SetFaceUI.Inst != null)
		{
			if (show)
			{
				Debug.Log("CanClickManager:因捏脸界面不允许点击");
			}
			this.ResultCache[30] = true;
		}
		if (ElderTaskUIMag.Inst != null)
		{
			if (show)
			{
				Debug.Log("CanClickManager:因发布任务界面不允许点击");
			}
			this.ResultCache[31] = true;
		}
		if (IExchangeUIMag.Inst != null)
		{
			if (show)
			{
				Debug.Log("CanClickManager:因发交易会界面不允许点击");
			}
			this.ResultCache[32] = true;
		}
		for (int j = this.MenuDialogCache.Count - 1; j >= 0; j--)
		{
			if (this.MenuDialogCache[j] != null)
			{
				if (this.MenuDialogCache[j].gameObject.activeInHierarchy)
				{
					if (show)
					{
						Debug.Log("CanClickManager:因MenuDialog存在而不允许点击");
					}
					this.ResultCache[48] = true;
				}
			}
			else
			{
				this.MenuDialogCache.RemoveAt(j);
			}
		}
		for (int k = this.SayDialogCache.Count - 1; k >= 0; k--)
		{
			if (this.SayDialogCache[k] != null)
			{
				if (this.SayDialogCache[k].gameObject.activeInHierarchy)
				{
					if (show)
					{
						Debug.Log("CanClickManager:因SayDialog存在而不允许点击");
					}
					this.ResultCache[49] = true;
				}
			}
			else
			{
				this.SayDialogCache.RemoveAt(k);
			}
		}
		for (int l = 0; l < CanClickManager.CacheCount; l++)
		{
			if (this.ResultCache[l])
			{
				this.ResultCount++;
			}
		}
		this.Result = (this.ResultCount == 0);
	}

	// Token: 0x04000CFF RID: 3327
	private static CanClickManager inst;

	// Token: 0x04000D00 RID: 3328
	public bool IsFinshed;

	// Token: 0x04000D01 RID: 3329
	public bool Result;

	// Token: 0x04000D02 RID: 3330
	public bool[] ResultCache = new bool[CanClickManager.CacheCount];

	// Token: 0x04000D03 RID: 3331
	public int ResultCount;

	// Token: 0x04000D04 RID: 3332
	public List<MenuDialog> MenuDialogCache = new List<MenuDialog>();

	// Token: 0x04000D05 RID: 3333
	public List<SayDialog> SayDialogCache = new List<SayDialog>();

	// Token: 0x04000D06 RID: 3334
	public static int CacheCount = 50;
}
