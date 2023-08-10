using System.IO;
using QiYu;
using TuPo;
using UnityEngine;
using YSGame.TianJiDaBi;

public class AuToSLMgr : MonoBehaviour
{
	private static AuToSLMgr _inst;

	private bool autoLoadNewSave = true;

	public static AuToSLMgr Inst
	{
		get
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Expected O, but got Unknown
			if ((Object)(object)_inst == (Object)null)
			{
				GameObject val = new GameObject();
				val.AddComponent<AuToSLMgr>();
				((Object)val).name = "AuToSLMgr";
				Object.DontDestroyOnLoad((Object)val);
			}
			return _inst;
		}
	}

	private void Awake()
	{
		_inst = this;
	}

	private void Update()
	{
		if (Input.GetKeyUp((KeyCode)286) && (Object)(object)PanelMamager.inst.UISceneGameObject != (Object)null && CanSave())
		{
			YSNewSaveSystem.AutoSave();
		}
		if (Input.GetKeyUp((KeyCode)289) && CanLoad())
		{
			if (!Tools.instance.getPlayer().StreamData.FungusSaveMgr.IsEnd())
			{
				Tools.instance.getPlayer().StreamData.FungusSaveMgr.CurCommand.StopParentBlock();
				Tools.instance.getPlayer().StreamData.FungusSaveMgr.CurCommand.Continue();
			}
			if (autoLoadNewSave)
			{
				YSNewSaveSystem.AutoLoad();
			}
			else
			{
				YSNewSaveSystem.LoadOldSave(PlayerPrefs.GetInt("NowPlayerFileAvatar"), 1);
			}
			Object.Destroy((Object)(object)((Component)this).gameObject);
		}
	}

	public bool CanSave()
	{
		if ((Object)(object)SetFaceUI.Inst != (Object)null)
		{
			UIPopTip.Inst.Pop("此界面禁止存档");
			return false;
		}
		if (Tools.instance.IsInDF)
		{
			UIPopTip.Inst.Pop("神仙斗法模式禁止快速存档");
			return false;
		}
		if ((Object)(object)QiYuUIMag.Inst != (Object)null)
		{
			UIPopTip.Inst.Pop("当前状态禁止快速存档");
			return false;
		}
		if ((Object)(object)UIDuJieZhunBei.Inst != (Object)null && UIDuJieZhunBei.Inst.IsOpenByDuJie)
		{
			UIPopTip.Inst.Pop("此时无法进行快速存档");
			return false;
		}
		if ((Object)(object)UITianJiDaBiSaiChang.Inst != (Object)null)
		{
			return false;
		}
		return true;
	}

	public bool CanLoad()
	{
		if (jsonData.instance.saveState == 1)
		{
			UIPopTip.Inst.Pop("正在存档,请存完后再读取");
			return false;
		}
		if (Tools.instance.IsInDF)
		{
			UIPopTip.Inst.Pop("神仙斗法模式禁止快速读档");
			return false;
		}
		if (!NpcJieSuanManager.inst.isCanJieSuan)
		{
			UIPopTip.Inst.Pop("正在结算中,请稍后读档");
			UIPopTip.Inst.Pop("如果一直提示,请向官方报备");
			return false;
		}
		if ((Object)(object)UIDuJieZhunBei.Inst != (Object)null && UIDuJieZhunBei.Inst.IsOpenByDuJie)
		{
			UIPopTip.Inst.Pop("此时无法进行快速读档");
			return false;
		}
		if ((Object)(object)BigTuPoResultIMag.Inst != (Object)null)
		{
			UIPopTip.Inst.Pop("此时无法进行快速读档");
			return false;
		}
		int @int = PlayerPrefs.GetInt("NowPlayerFileAvatar");
		bool flag = YSNewSaveSystem.HasFile(Paths.GetNewSavePath() + "/" + YSNewSaveSystem.GetAvatarSavePathPre(@int, 1) + "/StreamData.bin");
		bool flag2 = File.Exists(Paths.GetSavePath() + "/StreamData" + Tools.instance.getSaveID(@int, 1) + ".sav");
		if (!flag && !flag2)
		{
			UIPopTip.Inst.Pop("请先按F5存档后，再读档");
			return false;
		}
		autoLoadNewSave = flag;
		if ((Object)(object)Object.FindObjectOfType<UITianJiDaBiSaiChang>() != (Object)null)
		{
			return false;
		}
		if ((Object)(object)UInputBox.Inst != (Object)null)
		{
			return false;
		}
		return true;
	}
}
