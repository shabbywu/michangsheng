using System.Collections.Generic;
using UnityEngine;
using YSGame.TuJian;

public class PanelMamager : MonoBehaviour
{
	public enum PanelType
	{
		炼丹,
		传音符,
		图鉴,
		任务,
		炼器,
		空
	}

	private Dictionary<int, string> panelNameDictionary = new Dictionary<int, string>();

	[HideInInspector]
	public GameObject UISceneGameObject;

	[HideInInspector]
	public GameObject UIBlackMaskGameObject;

	public static bool CanOpenOrClose = true;

	public bool canOpenPanel;

	[HideInInspector]
	public PanelType nowPanel = PanelType.空;

	public static PanelMamager inst;

	private Dictionary<string, GameObject> PanelDictionary = new Dictionary<string, GameObject>();

	private void Awake()
	{
		inst = this;
		panelNameDictionary.Add(0, "LianDanPanel");
		panelNameDictionary.Add(1, "CyPanel");
		panelNameDictionary.Add(2, "TuJianPanel");
		panelNameDictionary.Add(3, "TaskPanel");
		panelNameDictionary.Add(4, "LianQiPanel");
		canOpenPanel = true;
	}

	public void OpenPanel(PanelType panelType, int type = 0)
	{
		if (!CanOpenOrClose || (nowPanel == PanelType.图鉴 && !Tools.instance.canClick()) || (type == 1 && !canOpenPanel))
		{
			return;
		}
		switch (panelType)
		{
		case PanelType.图鉴:
			TuJianManager.Inst.OpenTuJian();
			nowPanel = panelType;
			return;
		case PanelType.传音符:
			if ((Object)(object)JiaoYiManager.inst != (Object)null)
			{
				return;
			}
			if (!NpcJieSuanManager.inst.isCanJieSuan)
			{
				UIPopTip.Inst.Pop("正在结算中请稍等");
				return;
			}
			if ((Object)(object)FpUIMag.inst != (Object)null)
			{
				return;
			}
			break;
		}
		nowPanel = panelType;
		string text = panelNameDictionary[(int)panelType];
		if (!PanelDictionary.ContainsKey(text))
		{
			PanelDictionary.Add(text, Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab(text)));
			JSONObject openPanelList = Tools.instance.getPlayer().openPanelList;
			int num = (int)panelType;
			openPanelList.SetField(num.ToString(), val: true);
		}
		else if ((Object)(object)PanelDictionary[text] == (Object)null)
		{
			PanelDictionary.Add(text, Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab(text)));
			JSONObject openPanelList2 = Tools.instance.getPlayer().openPanelList;
			int num = (int)panelType;
			openPanelList2.SetField(num.ToString(), val: true);
		}
		if (panelType == PanelType.炼丹)
		{
			PlayerEx.CheckLianDan();
		}
		if (panelType == PanelType.炼器)
		{
			PlayerEx.CheckLianQi();
		}
	}

	public bool checkIsOpenPanel(PanelType panelType)
	{
		string key = panelNameDictionary[(int)panelType];
		if (PanelDictionary.ContainsKey(key) && (Object)(object)PanelDictionary[key] != (Object)null)
		{
			return true;
		}
		return false;
	}

	public void destoryUIGameObjet()
	{
		if ((Object)(object)UISceneGameObject != (Object)null)
		{
			Object.Destroy((Object)(object)UISceneGameObject);
		}
		UISceneGameObject = null;
	}

	public void closePanel(PanelType panelType, int type = 0)
	{
		if (!CanOpenOrClose)
		{
			return;
		}
		string key = panelNameDictionary[(int)panelType];
		if (PanelDictionary.ContainsKey(key))
		{
			if (type == 0)
			{
				JSONObject openPanelList = Tools.instance.getPlayer().openPanelList;
				int num = (int)panelType;
				openPanelList.SetField(num.ToString(), val: false);
			}
			Object.Destroy((Object)(object)PanelDictionary[key]);
			PanelDictionary.Remove(key);
			nowPanel = PanelType.空;
		}
		Object.FindObjectOfType<LianDanSystemManager>();
		Object.FindObjectOfType<LianQiTotalManager>();
	}
}
