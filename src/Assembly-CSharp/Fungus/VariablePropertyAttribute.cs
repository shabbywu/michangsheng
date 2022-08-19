using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E8C RID: 3724
	public class VariablePropertyAttribute : PropertyAttribute
	{
		// Token: 0x06006985 RID: 27013 RVA: 0x0029129B File Offset: 0x0028F49B
		public VariablePropertyAttribute(params Type[] variableTypes)
		{
			this.VariableTypes = variableTypes;
		}

		// Token: 0x06006986 RID: 27014 RVA: 0x002912B5 File Offset: 0x0028F4B5
		public VariablePropertyAttribute(string defaultText, params Type[] variableTypes)
		{
			this.defaultText = defaultText;
			this.VariableTypes = variableTypes;
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06006987 RID: 27015 RVA: 0x002912D6 File Offset: 0x0028F4D6
		// (set) Token: 0x06006988 RID: 27016 RVA: 0x002912DE File Offset: 0x0028F4DE
		public Type[] VariableTypes { get; set; }

		// Token: 0x04005981 RID: 22913
		public string defaultText = "<None>";
	}
}
