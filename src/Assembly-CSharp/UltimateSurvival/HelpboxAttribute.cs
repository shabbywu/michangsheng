using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005A9 RID: 1449
	public class HelpboxAttribute : PropertyAttribute
	{
		// Token: 0x06002F3F RID: 12095 RVA: 0x001566CB File Offset: 0x001548CB
		public HelpboxAttribute(string message)
		{
			this.Message = message;
		}

		// Token: 0x04002977 RID: 10615
		public readonly string Message;
	}
}
