using System;
using System.Collections.Generic;
using UnityEngine;
using YSGame.TuJian;

// Token: 0x020003A9 RID: 937
public class PanelMamager : MonoBehaviour
{
	// Token: 0x06001E7B RID: 7803 RVA: 0x000D65F4 File Offset: 0x000D47F4
	private void Awake()
	{
		PanelMamager.inst = this;
		this.panelNameDictionary.Add(0, "LianDanPanel");
		this.panelNameDictionary.Add(1, "CyPanel");
		this.panelNameDictionary.Add(2, "TuJianPanel");
		this.panelNameDictionary.Add(3, "TaskPanel");
		this.panelNameDictionary.Add(4, "LianQiPanel");
		this.canOpenPanel = true;
	}

	// Token: 0x06001E7C RID: 7804 RVA: 0x000D6664 File Offset: 0x000D4864
	public void OpenPanel(PanelMamager.PanelType panelType, int type = 0)
	{
		if (!PanelMamager.CanOpenOrClose)
		{
			return;
		}
		if (this.nowPanel == PanelMamager.PanelType.图鉴 && !Tools.instance.canClick(false, true))
		{
			return;
		}
		if (type == 1 && !this.canOpenPanel)
		{
			return;
		}
		if (panelType == PanelMamager.PanelType.图鉴)
		{
			TuJianManager.Inst.OpenTuJian();
			this.nowPanel = panelType;
			return;
		}
		if (panelType == PanelMamager.PanelType.传音符)
		{
			if (JiaoYiManager.inst != null)
			{
				return;
			}
			if (!NpcJieSuanManager.inst.isCanJieSuan)
			{
				UIPopTip.Inst.Pop("正在结算中请稍等", PopTipIconType.叹号);
				return;
			}
			if (FpUIMag.inst != null)
			{
				return;
			}
		}
		this.nowPanel = panelType;
		string text = this.panelNameDictionary[(int)panelType];
		if (!this.PanelDictionary.ContainsKey(text))
		{
			this.PanelDictionary.Add(text, Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab(text)));
			JSONObject openPanelList = Tools.instance.getPlayer().openPanelList;
			int num = (int)panelType;
			openPanelList.SetField(num.ToString(), true);
		}
		else if (this.PanelDictionary[text] == null)
		{
			this.PanelDictionary.Add(text, Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab(text)));
			JSONObject openPanelList2 = Tools.instance.getPlayer().openPanelList;
			int num = (int)panelType;
			openPanelList2.SetField(num.ToString(), true);
		}
		if (panelType == PanelMamager.PanelType.炼丹)
		{
			PlayerEx.CheckLianDan();
		}
		if (panelType == PanelMamager.PanelType.炼器)
		{
			PlayerEx.CheckLianQi();
		}
	}

	// Token: 0x06001E7D RID: 7805 RVA: 0x000D67B0 File Offset: 0x000D49B0
	public bool checkIsOpenPanel(PanelMamager.PanelType panelType)
	{
		string key = this.panelNameDictionary[(int)panelType];
		return this.PanelDictionary.ContainsKey(key) && this.PanelDictionary[key] != null;
	}

	// Token: 0x06001E7E RID: 7806 RVA: 0x000D67EF File Offset: 0x000D49EF
	public void destoryUIGameObjet()
	{
		if (this.UISceneGameObject != null)
		{
			Object.Destroy(this.UISceneGameObject);
		}
		this.UISceneGameObject = null;
	}

	// Token: 0x06001E7F RID: 7807 RVA: 0x000D6814 File Offset: 0x000D4A14
	public void closePanel(PanelMamager.PanelType panelType, int type = 0)
	{
		if (!PanelMamager.CanOpenOrClose)
		{
			return;
		}
		string key = this.panelNameDictionary[(int)panelType];
		if (this.PanelDictionary.ContainsKey(key))
		{
			if (type == 0)
			{
				JSONObject openPanelList = Tools.instance.getPlayer().openPanelList;
				int num = (int)panelType;
				openPanelList.SetField(num.ToString(), false);
			}
			Object.Destroy(this.PanelDictionary[key]);
			this.PanelDictionary.Remove(key);
			this.nowPanel = PanelMamager.PanelType.空;
		}
		Object.FindObjectOfType<LianDanSystemManager>();
		Object.FindObjectOfType<LianQiTotalManager>();
	}

	// Token: 0x040018FC RID: 6396
	private Dictionary<int, string> panelNameDictionary = new Dictionary<int, string>();

	// Token: 0x040018FD RID: 6397
	[HideInInspector]
	public GameObject UISceneGameObject;

	// Token: 0x040018FE RID: 6398
	[HideInInspector]
	public GameObject UIBlackMaskGameObject;

	// Token: 0x040018FF RID: 6399
	public static bool CanOpenOrClose = true;

	// Token: 0x04001900 RID: 6400
	public bool canOpenPanel;

	// Token: 0x04001901 RID: 6401
	[HideInInspector]
	public PanelMamager.PanelType nowPanel = PanelMamager.PanelType.空;

	// Token: 0x04001902 RID: 6402
	public static PanelMamager inst;

	// Token: 0x04001903 RID: 6403
	private Dictionary<string, GameObject> PanelDictionary = new Dictionary<string, GameObject>();

	// Token: 0x02001364 RID: 4964
	public enum PanelType
	{
		// Token: 0x04006845 RID: 26693
		炼丹,
		// Token: 0x04006846 RID: 26694
		传音符,
		// Token: 0x04006847 RID: 26695
		图鉴,
		// Token: 0x04006848 RID: 26696
		任务,
		// Token: 0x04006849 RID: 26697
		炼器,
		// Token: 0x0400684A RID: 26698
		空
	}
}
