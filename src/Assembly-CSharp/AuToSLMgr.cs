using System;
using System.IO;
using QiYu;
using TuPo;
using UnityEngine;
using YSGame.TianJiDaBi;

// Token: 0x020001AD RID: 429
public class AuToSLMgr : MonoBehaviour
{
	// Token: 0x1700021F RID: 543
	// (get) Token: 0x0600121E RID: 4638 RVA: 0x0006DDF9 File Offset: 0x0006BFF9
	public static AuToSLMgr Inst
	{
		get
		{
			if (AuToSLMgr._inst == null)
			{
				GameObject gameObject = new GameObject();
				gameObject.AddComponent<AuToSLMgr>();
				gameObject.name = "AuToSLMgr";
				Object.DontDestroyOnLoad(gameObject);
			}
			return AuToSLMgr._inst;
		}
	}

	// Token: 0x0600121F RID: 4639 RVA: 0x0006DE29 File Offset: 0x0006C029
	private void Awake()
	{
		AuToSLMgr._inst = this;
	}

	// Token: 0x06001220 RID: 4640 RVA: 0x0006DE34 File Offset: 0x0006C034
	private void Update()
	{
		if (Input.GetKeyUp(286) && PanelMamager.inst.UISceneGameObject != null && this.CanSave())
		{
			YSNewSaveSystem.AutoSave();
		}
		if (Input.GetKeyUp(289) && this.CanLoad())
		{
			if (!Tools.instance.getPlayer().StreamData.FungusSaveMgr.IsEnd())
			{
				Tools.instance.getPlayer().StreamData.FungusSaveMgr.CurCommand.StopParentBlock();
				Tools.instance.getPlayer().StreamData.FungusSaveMgr.CurCommand.Continue();
			}
			if (this.autoLoadNewSave)
			{
				YSNewSaveSystem.AutoLoad();
			}
			else
			{
				YSNewSaveSystem.LoadOldSave(PlayerPrefs.GetInt("NowPlayerFileAvatar"), 1);
			}
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001221 RID: 4641 RVA: 0x0006DF08 File Offset: 0x0006C108
	public bool CanSave()
	{
		if (SetFaceUI.Inst != null)
		{
			UIPopTip.Inst.Pop("此界面禁止存档", PopTipIconType.叹号);
			return false;
		}
		if (Tools.instance.IsInDF)
		{
			UIPopTip.Inst.Pop("神仙斗法模式禁止快速存档", PopTipIconType.叹号);
			return false;
		}
		if (QiYuUIMag.Inst != null)
		{
			UIPopTip.Inst.Pop("当前状态禁止快速存档", PopTipIconType.叹号);
			return false;
		}
		if (UIDuJieZhunBei.Inst != null && UIDuJieZhunBei.Inst.IsOpenByDuJie)
		{
			UIPopTip.Inst.Pop("此时无法进行快速存档", PopTipIconType.叹号);
			return false;
		}
		return !(UITianJiDaBiSaiChang.Inst != null);
	}

	// Token: 0x06001222 RID: 4642 RVA: 0x0006DFAC File Offset: 0x0006C1AC
	public bool CanLoad()
	{
		if (jsonData.instance.saveState == 1)
		{
			UIPopTip.Inst.Pop("正在存档,请存完后再读取", PopTipIconType.叹号);
			return false;
		}
		if (Tools.instance.IsInDF)
		{
			UIPopTip.Inst.Pop("神仙斗法模式禁止快速读档", PopTipIconType.叹号);
			return false;
		}
		if (!NpcJieSuanManager.inst.isCanJieSuan)
		{
			UIPopTip.Inst.Pop("正在结算中,请稍后读档", PopTipIconType.叹号);
			UIPopTip.Inst.Pop("如果一直提示,请向官方报备", PopTipIconType.叹号);
			return false;
		}
		if (UIDuJieZhunBei.Inst != null && UIDuJieZhunBei.Inst.IsOpenByDuJie)
		{
			UIPopTip.Inst.Pop("此时无法进行快速读档", PopTipIconType.叹号);
			return false;
		}
		if (BigTuPoResultIMag.Inst != null)
		{
			UIPopTip.Inst.Pop("此时无法进行快速读档", PopTipIconType.叹号);
			return false;
		}
		int @int = PlayerPrefs.GetInt("NowPlayerFileAvatar");
		bool flag = YSNewSaveSystem.HasFile(Paths.GetNewSavePath() + "/" + YSNewSaveSystem.GetAvatarSavePathPre(@int, 1) + "/StreamData.bin");
		bool flag2 = File.Exists(Paths.GetSavePath() + "/StreamData" + Tools.instance.getSaveID(@int, 1) + ".sav");
		if (!flag && !flag2)
		{
			UIPopTip.Inst.Pop("请先按F5存档后，再读档", PopTipIconType.叹号);
			return false;
		}
		this.autoLoadNewSave = flag;
		return !(Object.FindObjectOfType<UITianJiDaBiSaiChang>() != null) && !(UInputBox.Inst != null);
	}

	// Token: 0x04000CD5 RID: 3285
	private static AuToSLMgr _inst;

	// Token: 0x04000CD6 RID: 3286
	private bool autoLoadNewSave = true;
}
