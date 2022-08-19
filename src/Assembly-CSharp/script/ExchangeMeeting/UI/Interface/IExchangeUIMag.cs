using System;
using System.Collections.Generic;
using script.ExchangeMeeting.UI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace script.ExchangeMeeting.UI.Interface
{
	// Token: 0x02000A29 RID: 2601
	public abstract class IExchangeUIMag : UIBase, IESCClose
	{
		// Token: 0x060047B7 RID: 18359 RVA: 0x001E50D8 File Offset: 0x001E32D8
		public static void Open()
		{
			ExchangeUIMag exchangeUIMag = new ExchangeUIMag(ResManager.inst.LoadPrefab("ExchangeUI").Inst(NewUICanvas.Inst.transform));
			IExchangeUIMag.Inst = exchangeUIMag;
			try
			{
				exchangeUIMag.Init();
			}
			catch (Exception ex)
			{
				Debug.LogError(ex);
				UIPopTip.Inst.Pop("交易会初始化失败，请验证游戏完整性", PopTipIconType.叹号);
			}
			ESCCloseManager.Inst.RegisterClose(IExchangeUIMag.Inst);
		}

		// Token: 0x060047B8 RID: 18360 RVA: 0x001E5150 File Offset: 0x001E3350
		public void Close()
		{
			Object.Destroy(this._go);
			ESCCloseManager.Inst.UnRegisterClose(this);
			IExchangeUIMag.Inst = null;
		}

		// Token: 0x060047B9 RID: 18361 RVA: 0x001E516E File Offset: 0x001E336E
		public bool TryEscClose()
		{
			this.Close();
			return true;
		}

		// Token: 0x060047BA RID: 18362
		public abstract void Say(int id);

		// Token: 0x060047BB RID: 18363
		public abstract void OpenPublish();

		// Token: 0x060047BC RID: 18364
		public abstract void OpenExchange();

		// Token: 0x060047BD RID: 18365
		public abstract List<int> GetCanGetItemList();

		// Token: 0x0400489E RID: 18590
		public static IExchangeUIMag Inst;

		// Token: 0x0400489F RID: 18591
		public IPublishCtr PublishCtr;

		// Token: 0x040048A0 RID: 18592
		public IExchangeCtr ExchangeCtr;

		// Token: 0x040048A1 RID: 18593
		public ExchangeBag NeedBag;

		// Token: 0x040048A2 RID: 18594
		public ExchangeBag PlayerBag;

		// Token: 0x040048A3 RID: 18595
		public ExchangeBagB SubmitBag;

		// Token: 0x040048A4 RID: 18596
		protected Text sayContent;
	}
}
