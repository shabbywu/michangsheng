using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000865 RID: 2149
	public class HelpboxAttribute : PropertyAttribute
	{
		// Token: 0x060037BD RID: 14269 RVA: 0x0002873F File Offset: 0x0002693F
		public HelpboxAttribute(string message)
		{
			this.Message = message;
		}

		// Token: 0x040031F8 RID: 12792
		public readonly string Message;
	}
}
