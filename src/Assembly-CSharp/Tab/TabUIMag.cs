using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JSONClass;
using KBEngine;
using PaiMai;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tab;

public class TabUIMag : SingletonMono<TabUIMag>, IESCClose
{
	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static UnityAction _003C_003E9__19_0;

		public static UnityAction _003C_003E9__19_1;

		internal void _003COpenTab_003Eb__19_0()
		{
			if ((Object)(object)LianQiTotalManager.inst != (Object)null)
			{
				((Component)LianQiTotalManager.inst).gameObject.SetActive(true);
			}
		}

		internal void _003COpenTab_003Eb__19_1()
		{
			if ((Object)(object)LianDanSystemManager.inst != (Object)null)
			{
				((Component)LianDanSystemManager.inst).gameObject.SetActive(true);
			}
		}
	}

	public TabSelectMag TabSelect;

	public TabShuXingPanel XingPanel;

	public TabWuDaoPanel WuDaoPanel;

	public TabGongFaPanel GongFaPanel;

	public TabShenTongPanel ShenTongPanel;

	public TabWuPingPanel WuPingPanel;

	public TabShengWangPanel ShengWangPanel;

	public TabSystemPanel SystemPanel;

	public TabFangAnPanel TabFangAnPanel;

	public GameObject BaseDataPanel;

	public UnityAction CloseAction;

	public Text HPText;

	public Transform Fire1;

	public Transform Fire2;

	public Text ExpText;

	public TabBag TabBag;

	public List<ITabPanelBase> PanelList = new List<ITabPanelBase>();

	private void Awake()
	{
		SingletonMono<TabUIMag>._instance = this;
	}

	private void Init()
	{
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Expected O, but got Unknown
		ESCCloseManager.Inst.RegisterClose(this);
		BaseDataPanel = ((Component)((Component)this).transform.Find("CommonUI/基本属性")).gameObject;
		HPText = ((Component)((Component)this).transform.Find("CommonUI/基本属性/Hp/HP_Text")).GetComponent<Text>();
		Fire1 = ((Component)this).transform.Find("CommonUI/基本属性/Hp/Bg/Bg2");
		ExpText = ((Component)((Component)this).transform.Find("CommonUI/基本属性/Exp/Exp_Text")).GetComponent<Text>();
		Fire2 = ((Component)this).transform.Find("CommonUI/基本属性/Exp/Bg/Bg2");
		XingPanel = new TabShuXingPanel(((Component)((Component)this).transform.Find("TabSelect/Panel/属性")).gameObject);
		WuDaoPanel = new TabWuDaoPanel(((Component)((Component)this).transform.Find("TabSelect/Panel/悟道")).gameObject);
		GongFaPanel = new TabGongFaPanel(((Component)((Component)this).transform.Find("TabSelect/Panel/功法")).gameObject);
		ShenTongPanel = new TabShenTongPanel(((Component)((Component)this).transform.Find("TabSelect/Panel/神通")).gameObject);
		WuPingPanel = new TabWuPingPanel(((Component)((Component)this).transform.Find("TabSelect/Panel/物品")).gameObject);
		ShengWangPanel = new TabShengWangPanel(((Component)((Component)this).transform.Find("TabSelect/Panel/声望")).gameObject);
		SystemPanel = new TabSystemPanel(((Component)((Component)this).transform.Find("TabSelect/Panel/系统")).gameObject);
		TabSelect = new TabSelectMag(((Component)((Component)this).transform.Find("TabSelect")).gameObject);
		TabBag = new TabBag(((Component)((Component)this).transform.Find("Bag")).gameObject);
		TabFangAnPanel = new TabFangAnPanel(((Component)((Component)this).transform.Find("CommonUI/切换")).gameObject);
		((Component)((Component)this).transform.Find("TabSelect/Panel/Close")).GetComponent<FpBtn>().mouseUpEvent.AddListener(new UnityAction(Close));
		TabSelect.SetDeafultSelect();
		MessageMag.Instance.Register(MessageName.MSG_PLAYER_USE_ITEM, UpdateBaseUI);
	}

	public static void OpenTab(int index = 0, UnityAction action = null)
	{
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Expected O, but got Unknown
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Expected O, but got Unknown
		if ((Object)(object)RoundManager.instance != (Object)null || (Object)(object)SingletonMono<PaiMaiUiMag>.Instance != (Object)null || !Tools.instance.canClick(show: false, useCache: false))
		{
			return;
		}
		if ((Object)(object)LianQiTotalManager.inst != (Object)null)
		{
			((Component)LianQiTotalManager.inst).gameObject.SetActive(false);
			object obj = _003C_003Ec._003C_003E9__19_0;
			if (obj == null)
			{
				UnityAction val = delegate
				{
					if ((Object)(object)LianQiTotalManager.inst != (Object)null)
					{
						((Component)LianQiTotalManager.inst).gameObject.SetActive(true);
					}
				};
				_003C_003Ec._003C_003E9__19_0 = val;
				obj = (object)val;
			}
			action = (UnityAction)obj;
		}
		else if ((Object)(object)LianDanSystemManager.inst != (Object)null)
		{
			((Component)LianDanSystemManager.inst).gameObject.SetActive(false);
			object obj2 = _003C_003Ec._003C_003E9__19_1;
			if (obj2 == null)
			{
				UnityAction val2 = delegate
				{
					if ((Object)(object)LianDanSystemManager.inst != (Object)null)
					{
						((Component)LianDanSystemManager.inst).gameObject.SetActive(true);
					}
				};
				_003C_003Ec._003C_003E9__19_1 = val2;
				obj2 = (object)val2;
			}
			action = (UnityAction)obj2;
		}
		ResManager.inst.LoadPrefab("TabPanel").Inst(((Component)NewUICanvas.Inst).gameObject.transform).AddComponent<TabUIMag>();
		((Component)SingletonMono<TabUIMag>.Instance).transform.localPosition = Vector3.zero;
		SingletonMono<TabUIMag>.Instance.Init();
		((Component)SingletonMono<TabUIMag>.Instance).transform.localScale = Vector3.one;
		SingletonMono<TabUIMag>.Instance.TabSelect.SetDeafultSelect(index);
		SingletonMono<TabUIMag>.Instance.CloseAction = action;
	}

	public static void OpenTab2(int index = 0)
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		if (!((Object)(object)RoundManager.instance != (Object)null) && !((Object)(object)SingletonMono<PaiMaiUiMag>.Instance != (Object)null))
		{
			ResManager.inst.LoadPrefab("TabPanel").Inst(((Component)NewUICanvas.Inst).gameObject.transform).AddComponent<TabUIMag>();
			((Component)SingletonMono<TabUIMag>.Instance).transform.localPosition = Vector3.zero;
			SingletonMono<TabUIMag>.Instance.Init();
			((Component)SingletonMono<TabUIMag>.Instance).transform.localScale = Vector3.one;
			SingletonMono<TabUIMag>.Instance.TabSelect.SetDeafultSelect(index);
		}
	}

	private void Close()
	{
		if (CloseAction != null)
		{
			CloseAction.Invoke();
			CloseAction = null;
		}
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	public bool TryEscClose()
	{
		Close();
		return true;
	}

	public void ShowBaseData()
	{
		BaseDataPanel.SetActive(true);
		UpdateBaseUI(new MessageData(0));
	}

	public void UpdateBaseUI(MessageData data)
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		Avatar player = Tools.instance.getPlayer();
		HPText.SetText($"{player.HP}/{player.HP_Max}");
		float num = (float)player.HP / (float)player.HP_Max;
		Fire1.localScale = new Vector3(num * 0.6f + 0.4f, num * 0.6f + 0.4f, 1f);
		int maxExp = LevelUpDataJsonData.DataDict[player.level].MaxExp;
		float num2 = (float)player.exp / (float)maxExp;
		Fire2.localScale = new Vector3(num2 * 0.6f + 0.4f, num2 * 0.6f + 0.4f, 1f);
		ExpText.SetText(player.exp.ToCNNumberWithUnit() + "/" + ((ulong)maxExp).ToCNNumberWithUnit());
	}

	public void HideBaseData()
	{
		BaseDataPanel.SetActive(false);
	}

	private void OnDestroy()
	{
		ESCCloseManager.Inst.UnRegisterClose(this);
		MessageMag.Instance.Remove(MessageName.MSG_PLAYER_USE_ITEM, UpdateBaseUI);
		MessageMag.Instance.Remove(MessageName.MSG_PLAYER_USE_ITEM, SingletonMono<TabUIMag>.Instance.TabBag.UseItemCallBack);
		UToolTip.Close();
		SingletonMono<TabUIMag>._instance = null;
	}
}
