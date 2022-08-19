using System;
using script.ExchangeMeeting.Logic.Interface;
using UnityEngine;

namespace script.ExchangeMeeting.UI.Factory
{
	// Token: 0x02000A2E RID: 2606
	public abstract class IExchangeFactory
	{
		// Token: 0x060047C9 RID: 18377
		public abstract void Create(GameObject gameObject, Transform parent, IExchangeData data);
	}
}
