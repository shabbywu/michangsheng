using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020011FC RID: 4604
	public class ControlWithDisplay<TDisplayEnum> : Command
	{
		// Token: 0x060070B8 RID: 28856 RVA: 0x0004C8D0 File Offset: 0x0004AAD0
		protected virtual bool IsDisplayNone<TEnum>(TEnum enumValue)
		{
			return Enum.GetName(typeof(TEnum), enumValue) == "None";
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x060070B9 RID: 28857 RVA: 0x0004C8F1 File Offset: 0x0004AAF1
		public virtual TDisplayEnum Display
		{
			get
			{
				return this.display;
			}
		}

		// Token: 0x0400632F RID: 25391
		[Tooltip("Display type")]
		[SerializeField]
		protected TDisplayEnum display;
	}
}
