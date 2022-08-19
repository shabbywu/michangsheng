using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DC3 RID: 3523
	public class ControlWithDisplay<TDisplayEnum> : Command
	{
		// Token: 0x0600643F RID: 25663 RVA: 0x0027E1C8 File Offset: 0x0027C3C8
		protected virtual bool IsDisplayNone<TEnum>(TEnum enumValue)
		{
			return Enum.GetName(typeof(TEnum), enumValue) == "None";
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06006440 RID: 25664 RVA: 0x0027E1E9 File Offset: 0x0027C3E9
		public virtual TDisplayEnum Display
		{
			get
			{
				return this.display;
			}
		}

		// Token: 0x04005633 RID: 22067
		[Tooltip("Display type")]
		[SerializeField]
		protected TDisplayEnum display;
	}
}
