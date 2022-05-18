using System;
using System.Collections.Generic;

namespace script.YarnEditor.Manager
{
	// Token: 0x02000AAC RID: 2732
	[Serializable]
	public class SaveValueManager
	{
		// Token: 0x060045E9 RID: 17897 RVA: 0x00031FC7 File Offset: 0x000301C7
		public void Init()
		{
			if (this.ValueDict == null)
			{
				this.ValueDict = new Dictionary<string, string>();
			}
		}

		// Token: 0x060045EA RID: 17898 RVA: 0x00031FDC File Offset: 0x000301DC
		public void SetValue(string key, string value)
		{
			this.ValueDict[key] = value;
		}

		// Token: 0x060045EB RID: 17899 RVA: 0x00031FEB File Offset: 0x000301EB
		public string GetValue(string key)
		{
			if (this.ValueDict.ContainsKey(key))
			{
				return this.ValueDict[key];
			}
			return "-1";
		}

		// Token: 0x04003E21 RID: 15905
		public Dictionary<string, string> ValueDict = new Dictionary<string, string>();
	}
}
