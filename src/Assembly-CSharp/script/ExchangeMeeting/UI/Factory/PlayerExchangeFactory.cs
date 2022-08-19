using System;
using script.ExchangeMeeting.Logic.Interface;
using script.ExchangeMeeting.UI.UI;
using UnityEngine;

namespace script.ExchangeMeeting.UI.Factory
{
	// Token: 0x02000A2F RID: 2607
	public class PlayerExchangeFactory : IExchangeFactory
	{
		// Token: 0x060047CB RID: 18379 RVA: 0x001E5261 File Offset: 0x001E3461
		public override void Create(GameObject gameObject, Transform parent, IExchangeData data)
		{
			new PublishingDataUI(gameObject.Inst(parent), data);
		}
	}
}
