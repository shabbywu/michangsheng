using System;
using System.Collections.Generic;

namespace script.YarnEditor.Manager
{
	// Token: 0x020009C7 RID: 2503
	[Serializable]
	public class SaveValueManager
	{
		// Token: 0x06004594 RID: 17812 RVA: 0x001D7B90 File Offset: 0x001D5D90
		public void Init()
		{
			if (this.ValueDict == null)
			{
				this.ValueDict = new Dictionary<string, string>();
			}
		}

		// Token: 0x06004595 RID: 17813 RVA: 0x001D7BA5 File Offset: 0x001D5DA5
		public void SetValue(string key, string value)
		{
			this.ValueDict[key] = value;
		}

		// Token: 0x06004596 RID: 17814 RVA: 0x001D7BB4 File Offset: 0x001D5DB4
		public string GetValue(string key)
		{
			if (this.ValueDict.ContainsKey(key))
			{
				return this.ValueDict[key];
			}
			return "-1";
		}

		// Token: 0x04004715 RID: 18197
		public Dictionary<string, string> ValueDict = new Dictionary<string, string>();
	}
}
