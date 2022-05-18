using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020012FD RID: 4861
	public class VariablePropertyAttribute : PropertyAttribute
	{
		// Token: 0x06007684 RID: 30340 RVA: 0x00050A9E File Offset: 0x0004EC9E
		public VariablePropertyAttribute(params Type[] variableTypes)
		{
			this.VariableTypes = variableTypes;
		}

		// Token: 0x06007685 RID: 30341 RVA: 0x00050AB8 File Offset: 0x0004ECB8
		public VariablePropertyAttribute(string defaultText, params Type[] variableTypes)
		{
			this.defaultText = defaultText;
			this.VariableTypes = variableTypes;
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x06007686 RID: 30342 RVA: 0x00050AD9 File Offset: 0x0004ECD9
		// (set) Token: 0x06007687 RID: 30343 RVA: 0x00050AE1 File Offset: 0x0004ECE1
		public Type[] VariableTypes { get; set; }

		// Token: 0x04006762 RID: 26466
		public string defaultText = "<None>";
	}
}
