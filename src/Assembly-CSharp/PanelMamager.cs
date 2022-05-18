using System;
using System.Collections.Generic;
using UnityEngine;
using YSGame.TuJian;

// Token: 0x02000533 RID: 1331
public class PanelMamager : MonoBehaviour
{
	// Token: 0x060021FE RID: 8702 RVA: 0x00119D50 File Offset: 0x00117F50
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

	// Token: 0x060021FF RID: 8703 RVA: 0x00119DC0 File Offset: 0x00117FC0
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

	// Token: 0x06002200 RID: 8704 RVA: 0x00119F0C File Offset: 0x0011810C
	public bool checkIsOpenPanel(PanelMamager.PanelType panelType)
	{
		string key = this.panelNameDictionary[(int)panelType];
		return this.PanelDictionary.ContainsKey(key) && this.PanelDictionary[key] != null;
	}

	// Token: 0x06002201 RID: 8705 RVA: 0x0001BE35 File Offset: 0x0001A035
	public void destoryUIGameObjet()
	{
		if (this.UISceneGameObject != null)
		{
			Object.Destroy(this.UISceneGameObject);
		}
		this.UISceneGameObject = null;
	}

	// Token: 0x06002202 RID: 8706 RVA: 0x00119F4C File Offset: 0x0011814C
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

	// Token: 0x04001D69 RID: 7529
	private Dictionary<int, string> panelNameDictionary = new Dictionary<int, string>();

	// Token: 0x04001D6A RID: 7530
	[HideInInspector]
	public GameObject UISceneGameObject;

	// Token: 0x04001D6B RID: 7531
	[HideInInspector]
	public GameObject UIBlackMaskGameObject;

	// Token: 0x04001D6C RID: 7532
	public static bool CanOpenOrClose = true;

	// Token: 0x04001D6D RID: 7533
	public bool canOpenPanel;

	// Token: 0x04001D6E RID: 7534
	[HideInInspector]
	public PanelMamager.PanelType nowPanel = PanelMamager.PanelType.空;

	// Token: 0x04001D6F RID: 7535
	public static PanelMamager inst;

	// Token: 0x04001D70 RID: 7536
	private Dictionary<string, GameObject> PanelDictionary = new Dictionary<string, GameObject>();

	// Token: 0x02000534 RID: 1332
	public enum PanelType
	{
		// Token: 0x04001D72 RID: 7538
		炼丹,
		// Token: 0x04001D73 RID: 7539
		传音符,
		// Token: 0x04001D74 RID: 7540
		图鉴,
		// Token: 0x04001D75 RID: 7541
		任务,
		// Token: 0x04001D76 RID: 7542
		炼器,
		// Token: 0x04001D77 RID: 7543
		空
	}
}
