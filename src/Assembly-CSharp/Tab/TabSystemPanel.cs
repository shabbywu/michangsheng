using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using YSGame;
using script.NewLianDan;
using script.Submit;

namespace Tab;

[Serializable]
public class TabSystemPanel : ITabPanelBase
{
	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static UnityAction _003C_003E9__9_0;

		internal void _003CReturnTittle_003Eb__9_0()
		{
			if ((Object)(object)FpUIMag.inst != (Object)null)
			{
				Object.Destroy((Object)(object)((Component)FpUIMag.inst).gameObject);
			}
			if ((Object)(object)TpUIMag.inst != (Object)null)
			{
				Object.Destroy((Object)(object)((Component)TpUIMag.inst).gameObject);
			}
			if ((Object)(object)SubmitUIMag.Inst != (Object)null)
			{
				SubmitUIMag.Inst.Close();
			}
			if ((Object)(object)LianDanUIMag.Instance != (Object)null)
			{
				Object.Destroy((Object)(object)((Component)LianDanUIMag.Instance).gameObject);
			}
			if ((Object)(object)LianQiTotalManager.inst != (Object)null)
			{
				Object.Destroy((Object)(object)((Component)LianQiTotalManager.inst).gameObject);
			}
			SingletonMono<TabUIMag>.Instance.TryEscClose();
			YSSaveGame.Reset();
			KBEngineApp.app.entities[10] = null;
			KBEngineApp.app.entities.Remove(10);
			Tools.instance.loadOtherScenes("MainMenu");
		}
	}

	private bool _isInit;

	public TabSavePanel SavePanel;

	public TabLoadPanel LoadPanel;

	public TabSetPanel SetPanel;

	public SysSelectMag SelectMag;

	public List<ISysPanelBase> PanelList;

	public TabSystemPanel(GameObject gameObject)
	{
		PanelList = new List<ISysPanelBase>();
		_go = gameObject;
		_isInit = false;
	}

	private void Init()
	{
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Expected O, but got Unknown
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Expected O, but got Unknown
		SavePanel = new TabSavePanel(Get("Select/Panel/保存"));
		LoadPanel = new TabLoadPanel(Get("Select/Panel/读取"));
		SetPanel = new TabSetPanel(Get("Select/Panel/设置"));
		SelectMag = new SysSelectMag(Get("Select"));
		Get<FpBtn>("Select/返回标题/UnSelect").mouseUpEvent.AddListener(new UnityAction(ReturnTittle));
		Get<FpBtn>("Select/退出/UnSelect").mouseUpEvent.AddListener(new UnityAction(QuitGame));
		_isInit = true;
		SelectMag.SetDeafultSelect();
	}

	public override void Show()
	{
		if (!_isInit)
		{
			Init();
			_isInit = true;
		}
		_go.SetActive(true);
	}

	public void ReturnTittle()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		TySelect inst = TySelect.inst;
		object obj = _003C_003Ec._003C_003E9__9_0;
		if (obj == null)
		{
			UnityAction val = delegate
			{
				if ((Object)(object)FpUIMag.inst != (Object)null)
				{
					Object.Destroy((Object)(object)((Component)FpUIMag.inst).gameObject);
				}
				if ((Object)(object)TpUIMag.inst != (Object)null)
				{
					Object.Destroy((Object)(object)((Component)TpUIMag.inst).gameObject);
				}
				if ((Object)(object)SubmitUIMag.Inst != (Object)null)
				{
					SubmitUIMag.Inst.Close();
				}
				if ((Object)(object)LianDanUIMag.Instance != (Object)null)
				{
					Object.Destroy((Object)(object)((Component)LianDanUIMag.Instance).gameObject);
				}
				if ((Object)(object)LianQiTotalManager.inst != (Object)null)
				{
					Object.Destroy((Object)(object)((Component)LianQiTotalManager.inst).gameObject);
				}
				SingletonMono<TabUIMag>.Instance.TryEscClose();
				YSSaveGame.Reset();
				KBEngineApp.app.entities[10] = null;
				KBEngineApp.app.entities.Remove(10);
				Tools.instance.loadOtherScenes("MainMenu");
			};
			_003C_003Ec._003C_003E9__9_0 = val;
			obj = (object)val;
		}
		inst.Show("是否要返回主界面？", (UnityAction)obj);
	}

	public void QuitGame()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		TySelect.inst.Show("是否要退出游戏？", new UnityAction(Application.Quit));
	}
}
