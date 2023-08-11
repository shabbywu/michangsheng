using System;
using UnityEngine;
using UnityEngine.Events;
using script.ExchangeMeeting.Logic.Interface;
using script.ExchangeMeeting.UI.Factory;
using script.ExchangeMeeting.UI.Interface;
using script.ExchangeMeeting.UI.UI;

namespace script.ExchangeMeeting.UI.Ctr;

public class ExchangeCtr : IExchangeCtr
{
	public ExchangeCtr(GameObject gameObject)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected O, but got Unknown
		UI = new ExchangeUI(gameObject, new UnityAction(UpdateEventList));
		Factory = new ViewExchangeFactory();
		Init();
	}

	private void Init()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		UnityEvent mouseUpEvent = UI.BackBtn.mouseUpEvent;
		IExchangeUIMag inst = IExchangeUIMag.Inst;
		mouseUpEvent.AddListener(new UnityAction(inst.OpenPublish));
	}

	public override void UpdateEventList()
	{
		Tools.ClearChild(UI.EventParent);
		foreach (IExchangeData system in IExchangeMag.Inst.ExchangeIO.GetSystemList())
		{
			try
			{
				Factory.Create(UI.SysEvent, UI.EventParent, system);
			}
			catch (Exception ex)
			{
				Debug.LogError((object)("创建系统交易会事件失败：" + ex.Message));
			}
		}
		foreach (IExchangeData player in IExchangeMag.Inst.ExchangeIO.GetPlayerList())
		{
			try
			{
				Factory.Create(UI.PlayerEvent, UI.EventParent, player);
			}
			catch (Exception ex2)
			{
				Debug.LogError((object)("创建玩家交易会事件失败：" + ex2.Message));
			}
		}
	}
}
