using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using script.ExchangeMeeting.UI.Base;

namespace script.ExchangeMeeting.UI.Interface;

public abstract class IExchangeUIMag : UIBase, IESCClose
{
	public static IExchangeUIMag Inst;

	public IPublishCtr PublishCtr;

	public IExchangeCtr ExchangeCtr;

	public ExchangeBag NeedBag;

	public ExchangeBag PlayerBag;

	public ExchangeBagB SubmitBag;

	protected Text sayContent;

	public static void Open()
	{
		ExchangeUIMag exchangeUIMag = (ExchangeUIMag)(Inst = new ExchangeUIMag(ResManager.inst.LoadPrefab("ExchangeUI").Inst(((Component)NewUICanvas.Inst).transform)));
		try
		{
			exchangeUIMag.Init();
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			UIPopTip.Inst.Pop("交易会初始化失败，请验证游戏完整性");
		}
		ESCCloseManager.Inst.RegisterClose(Inst);
	}

	public void Close()
	{
		Object.Destroy((Object)(object)_go);
		ESCCloseManager.Inst.UnRegisterClose(this);
		Inst = null;
	}

	public bool TryEscClose()
	{
		Close();
		return true;
	}

	public abstract void Say(int id);

	public abstract void OpenPublish();

	public abstract void OpenExchange();

	public abstract List<int> GetCanGetItemList();
}
