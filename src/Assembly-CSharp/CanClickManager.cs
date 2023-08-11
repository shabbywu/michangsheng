using System.Collections.Generic;
using Fungus;
using JiaoYi;
using Tab;
using UnityEngine;
using YSGame.TianJiDaBi;
using YSGame.TuJian;
using script.ExchangeMeeting.UI.Interface;
using script.MenPaiTask.ZhangLao.UI;

public class CanClickManager : MonoBehaviour
{
	private static CanClickManager inst;

	public bool IsFinshed;

	public bool Result;

	public bool[] ResultCache = new bool[CacheCount];

	public int ResultCount;

	public List<MenuDialog> MenuDialogCache = new List<MenuDialog>();

	public List<SayDialog> SayDialogCache = new List<SayDialog>();

	public static int CacheCount = 50;

	public static CanClickManager Inst
	{
		get
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Expected O, but got Unknown
			if ((Object)(object)inst == (Object)null)
			{
				GameObject val = new GameObject("CanClickManager");
				Object.DontDestroyOnLoad((Object)val);
				inst = val.AddComponent<CanClickManager>();
			}
			return inst;
		}
	}

	private void LateUpdate()
	{
		IsFinshed = false;
	}

	public void RefreshCanClick(bool show = false)
	{
		ResultCount = 0;
		for (int i = 0; i < CacheCount; i++)
		{
			ResultCache[i] = false;
		}
		if (!Tools.canClickFlag)
		{
			if (show)
			{
				Debug.Log((object)"CanClickManager:因canClickFlag而不允许点击");
			}
			ResultCache[0] = true;
		}
		if ((Object)(object)SelectLianDanCaiLiaoPage.Inst != (Object)null && ((Component)SelectLianDanCaiLiaoPage.Inst).gameObject.activeInHierarchy)
		{
			if (show)
			{
				Debug.Log((object)"CanClickManager:因正在炼丹选择材料界面而不允许点击");
			}
			ResultCache[1] = true;
		}
		if ((Object)(object)UINPCLeftList.Inst != (Object)null && UINPCLeftList.Inst.IsMouseInUI)
		{
			if (show)
			{
				Debug.Log((object)"CanClickManager:因鼠标在NPC左侧列表内而不允许点击");
			}
			ResultCache[2] = true;
		}
		if ((Object)(object)UIHeadPanel.Inst != (Object)null && UIHeadPanel.Inst.IsMouseInUI)
		{
			if (show)
			{
				Debug.Log((object)"CanClickManager:因鼠标在左上头像区内而不允许点击");
			}
			ResultCache[3] = true;
		}
		if (UINPCJiaoHu.Inst.NowIsJiaoHu)
		{
			if (show)
			{
				Debug.Log((object)"CanClickManager:因正在进行NPC交互而不允许点击");
			}
			ResultCache[4] = true;
		}
		if ((Object)(object)CyUIMag.inst != (Object)null)
		{
			if (show)
			{
				Debug.Log((object)"CanClickManager:因正在传音符界面而不允许点击");
			}
			ResultCache[5] = true;
		}
		if ((Object)(object)FpUIMag.inst != (Object)null)
		{
			if (show)
			{
				Debug.Log((object)"CanClickManager:因正在战前准备界面而不允许点击");
			}
			ResultCache[6] = true;
		}
		if ((Object)(object)TpUIMag.inst != (Object)null)
		{
			if (show)
			{
				Debug.Log((object)"CanClickManager:因正在突破准备界面而不允许点击");
			}
			ResultCache[7] = true;
		}
		if ((Object)(object)KeFangSelectTime.inst != (Object)null)
		{
			if (show)
			{
				Debug.Log((object)"CanClickManager:因正在客房选择时间界面而不允许点击");
			}
			ResultCache[8] = true;
		}
		if (UIBiGuanPanel.Inst.PanelObj.activeInHierarchy)
		{
			if (show)
			{
				Debug.Log((object)"CanClickManager:因正在客房选择时间界面而不允许点击");
			}
			ResultCache[9] = true;
		}
		if (UIDongFu.Inst.ScaleObj.activeInHierarchy)
		{
			if (show)
			{
				Debug.Log((object)"CanClickManager:因正在洞府界面而不允许点击");
			}
			ResultCache[10] = true;
		}
		if (UIMenPaiShop.Inst.ScaleObj.activeInHierarchy)
		{
			if (show)
			{
				Debug.Log((object)"CanClickManager:因正在门派商店界面而不允许点击");
			}
			ResultCache[11] = true;
		}
		if (UInputBox.IsShow)
		{
			if (show)
			{
				Debug.Log((object)"CanClickManager:因正在UInputBox界面而不允许点击");
			}
			ResultCache[12] = true;
		}
		if (USelectBox.IsShow)
		{
			if (show)
			{
				Debug.Log((object)"CanClickManager:因正在确定选择框界面而不允许点击");
			}
			ResultCache[13] = true;
		}
		if (UCheckBox.IsShow)
		{
			if (show)
			{
				Debug.Log((object)"CanClickManager:因正在确定框界面而不允许点击");
			}
			ResultCache[14] = true;
		}
		if (USelectNum.IsShow)
		{
			if (show)
			{
				Debug.Log((object)"CanClickManager:因正在数量选择框界面而不允许点击");
			}
			ResultCache[15] = true;
		}
		if ((Object)(object)TuJianManager.Inst != (Object)null && ((Behaviour)TuJianManager.Inst._Canvas).enabled)
		{
			if (show)
			{
				Debug.Log((object)"CanClickManager:因正在图鉴界面而不允许点击");
			}
			ResultCache[16] = true;
		}
		if ((Object)(object)UIHuaShenRuDaoSelect.Inst != (Object)null && UIHuaShenRuDaoSelect.Inst.IsShow)
		{
			if (show)
			{
				Debug.Log((object)"CanClickManager:因正在化神入道选择界面而不允许点击");
			}
			ResultCache[17] = true;
		}
		if ((Object)(object)SingletonMono<TabUIMag>.Instance != (Object)null)
		{
			if (show)
			{
				Debug.Log((object)"CanClickManager:因正在Tab界面而不允许点击");
			}
			ResultCache[18] = true;
		}
		if ((Object)(object)UIMapPanel.Inst != (Object)null && UIMapPanel.Inst.IsShow)
		{
			if (show)
			{
				Debug.Log((object)"CanClickManager:因正在地图界面而不允许点击");
			}
			ResultCache[19] = true;
		}
		if ((Object)(object)JiaoYiUIMag.Inst != (Object)null)
		{
			if (show)
			{
				Debug.Log((object)"CanClickManager:因正在交易界面而不允许点击");
			}
			ResultCache[20] = true;
		}
		if ((Object)(object)UIXiuChuanPanel.Inst != (Object)null)
		{
			if (show)
			{
				Debug.Log((object)"CanClickManager:因正在修船界面而不允许点击");
			}
			ResultCache[21] = true;
		}
		if ((Object)(object)UITianJiDaBiSaiChang.Inst != (Object)null)
		{
			if (show)
			{
				Debug.Log((object)"CanClickManager:因正在天机大比赛场界面而不允许点击");
			}
			ResultCache[22] = true;
		}
		if ((Object)(object)UITianJiDaBiPlayerInfo.Inst != (Object)null)
		{
			if (show)
			{
				Debug.Log((object)"CanClickManager:因正在天机大比参赛人员信息界面而不允许点击");
			}
			ResultCache[23] = true;
		}
		if ((Object)(object)UITianJiDaBiRankPanel.Inst != (Object)null)
		{
			if (show)
			{
				Debug.Log((object)"CanClickManager:因正在天机榜界面而不允许点击");
			}
			ResultCache[24] = true;
		}
		if ((Object)(object)UIMiniShop.Inst != (Object)null)
		{
			if (show)
			{
				Debug.Log((object)"CanClickManager:因正在迷你商店界面而不允许点击");
			}
			ResultCache[25] = true;
		}
		if ((Object)(object)UIDuJieZhunBei.Inst != (Object)null)
		{
			if (show)
			{
				Debug.Log((object)"CanClickManager:因正在渡劫准备界面而不允许点击");
			}
			ResultCache[26] = true;
		}
		if ((Object)(object)UIJianLingPanel.Inst != (Object)null)
		{
			if (show)
			{
				Debug.Log((object)"CanClickManager:因正在剑灵界面而不允许点击");
			}
			ResultCache[27] = true;
		}
		if ((Object)(object)UIJianLingXianSuoPanel.Inst != (Object)null)
		{
			if (show)
			{
				Debug.Log((object)"CanClickManager:因正在剑灵线索界面而不允许点击");
			}
			ResultCache[28] = true;
		}
		if ((Object)(object)UIJianLingQingJiaoPanel.Inst != (Object)null)
		{
			if (show)
			{
				Debug.Log((object)"CanClickManager:因正在剑灵请教界面而不允许点击");
			}
			ResultCache[29] = true;
		}
		if ((Object)(object)SetFaceUI.Inst != (Object)null)
		{
			if (show)
			{
				Debug.Log((object)"CanClickManager:因捏脸界面不允许点击");
			}
			ResultCache[30] = true;
		}
		if ((Object)(object)ElderTaskUIMag.Inst != (Object)null)
		{
			if (show)
			{
				Debug.Log((object)"CanClickManager:因发布任务界面不允许点击");
			}
			ResultCache[31] = true;
		}
		if (IExchangeUIMag.Inst != null)
		{
			if (show)
			{
				Debug.Log((object)"CanClickManager:因发交易会界面不允许点击");
			}
			ResultCache[32] = true;
		}
		for (int num = MenuDialogCache.Count - 1; num >= 0; num--)
		{
			if ((Object)(object)MenuDialogCache[num] != (Object)null)
			{
				if (((Component)MenuDialogCache[num]).gameObject.activeInHierarchy)
				{
					if (show)
					{
						Debug.Log((object)"CanClickManager:因MenuDialog存在而不允许点击");
					}
					ResultCache[48] = true;
				}
			}
			else
			{
				MenuDialogCache.RemoveAt(num);
			}
		}
		for (int num2 = SayDialogCache.Count - 1; num2 >= 0; num2--)
		{
			if ((Object)(object)SayDialogCache[num2] != (Object)null)
			{
				if (((Component)SayDialogCache[num2]).gameObject.activeInHierarchy)
				{
					if (show)
					{
						Debug.Log((object)"CanClickManager:因SayDialog存在而不允许点击");
					}
					ResultCache[49] = true;
				}
			}
			else
			{
				SayDialogCache.RemoveAt(num2);
			}
		}
		for (int j = 0; j < CacheCount; j++)
		{
			if (ResultCache[j])
			{
				ResultCount++;
			}
		}
		Result = ResultCount == 0;
	}
}
