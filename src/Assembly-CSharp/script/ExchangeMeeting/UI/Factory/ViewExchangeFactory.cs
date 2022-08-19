using System;
using script.ExchangeMeeting.Logic;
using script.ExchangeMeeting.Logic.Interface;
using script.ExchangeMeeting.UI.UI;
using UnityEngine;

namespace script.ExchangeMeeting.UI.Factory
{
	// Token: 0x02000A30 RID: 2608
	public class ViewExchangeFactory : IExchangeFactory
	{
		// Token: 0x060047CD RID: 18381 RVA: 0x001E527C File Offset: 0x001E347C
		public override void Create(GameObject gameObject, Transform parent, IExchangeData data)
		{
			GameObject gameObject2 = gameObject.Inst(parent);
			if (data is SysExchangeData)
			{
				new SysExchangeDataUI(gameObject2, data);
				return;
			}
			if (data is PlayerExchangeData)
			{
				new PlayerExchangeDataUI(gameObject2, data);
			}
		}
	}
}
