using System;
using script.ExchangeMeeting.Logic.Interface;
using script.ExchangeMeeting.UI.Factory;
using script.ExchangeMeeting.UI.Interface;
using script.ExchangeMeeting.UI.UI;
using UnityEngine;
using UnityEngine.Events;

namespace script.ExchangeMeeting.UI.Ctr
{
	// Token: 0x02000A31 RID: 2609
	public class ExchangeCtr : IExchangeCtr
	{
		// Token: 0x060047CF RID: 18383 RVA: 0x001E52B2 File Offset: 0x001E34B2
		public ExchangeCtr(GameObject gameObject)
		{
			this.UI = new ExchangeUI(gameObject, new UnityAction(this.UpdateEventList));
			this.Factory = new ViewExchangeFactory();
			this.Init();
		}

		// Token: 0x060047D0 RID: 18384 RVA: 0x001E52E4 File Offset: 0x001E34E4
		private void Init()
		{
			this.UI.BackBtn.mouseUpEvent.AddListener(new UnityAction(IExchangeUIMag.Inst.OpenPublish));
		}

		// Token: 0x060047D1 RID: 18385 RVA: 0x001E530C File Offset: 0x001E350C
		public override void UpdateEventList()
		{
			Tools.ClearChild(this.UI.EventParent);
			foreach (IExchangeData data in IExchangeMag.Inst.ExchangeIO.GetSystemList())
			{
				try
				{
					this.Factory.Create(this.UI.SysEvent, this.UI.EventParent, data);
				}
				catch (Exception ex)
				{
					Debug.LogError("创建系统交易会事件失败：" + ex.Message);
				}
			}
			foreach (IExchangeData data2 in IExchangeMag.Inst.ExchangeIO.GetPlayerList())
			{
				try
				{
					this.Factory.Create(this.UI.PlayerEvent, this.UI.EventParent, data2);
				}
				catch (Exception ex2)
				{
					Debug.LogError("创建玩家交易会事件失败：" + ex2.Message);
				}
			}
		}
	}
}
