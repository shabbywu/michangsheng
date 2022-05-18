using System;
using System.IO;
using QiYu;
using UnityEngine;
using YSGame.TianJiDaBi;

// Token: 0x020002AA RID: 682
public class AuToSLMgr : MonoBehaviour
{
	// Token: 0x17000267 RID: 615
	// (get) Token: 0x060014C5 RID: 5317 RVA: 0x0001310B File Offset: 0x0001130B
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

	// Token: 0x060014C6 RID: 5318 RVA: 0x0001313B File Offset: 0x0001133B
	private void Awake()
	{
		AuToSLMgr._inst = this;
	}

	// Token: 0x060014C7 RID: 5319 RVA: 0x000BBC0C File Offset: 0x000B9E0C
	private void Update()
	{
		if (Input.GetKeyUp(286) && PanelMamager.inst.UISceneGameObject != null)
		{
			if (Tools.instance.IsInDF)
			{
				UIPopTip.Inst.Pop("神仙斗法模式禁止快速存档", PopTipIconType.叹号);
				return;
			}
			if (QiYuUIMag.Inst != null)
			{
				UIPopTip.Inst.Pop("当前状态禁止快速存档", PopTipIconType.叹号);
				return;
			}
			if (UIDuJieZhunBei.Inst != null && UIDuJieZhunBei.Inst.IsOpenByDuJie)
			{
				UIPopTip.Inst.Pop("此时无法进行快速存档", PopTipIconType.叹号);
				return;
			}
			if (UITianJiDaBiSaiChang.Inst != null)
			{
				return;
			}
			Tools.instance.AuToSave();
		}
		if (Input.GetKeyUp(289) && this.CanLoad())
		{
			Tools.instance.AuToLoad();
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060014C8 RID: 5320 RVA: 0x000BBCE0 File Offset: 0x000B9EE0
	private bool CanLoad()
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
		if (!File.Exists(Paths.GetSavePath() + "/StreamData" + Tools.instance.getSaveID(PlayerPrefs.GetInt("NowPlayerFileAvatar"), 1) + ".sav"))
		{
			UIPopTip.Inst.Pop("请先按F5存档后，再读档", PopTipIconType.叹号);
			return false;
		}
		return !(Object.FindObjectOfType<UITianJiDaBiSaiChang>() != null);
	}

	// Token: 0x04000FFD RID: 4093
	private static AuToSLMgr _inst;
}
