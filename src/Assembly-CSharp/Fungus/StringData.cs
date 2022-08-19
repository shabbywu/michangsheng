using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EEA RID: 3818
	[Serializable]
	public struct StringData
	{
		// Token: 0x06006B7B RID: 27515 RVA: 0x002968D9 File Offset: 0x00294AD9
		public StringData(string v)
		{
			this.stringVal = v;
			this.stringRef = null;
		}

		// Token: 0x06006B7C RID: 27516 RVA: 0x002968E9 File Offset: 0x00294AE9
		public static implicit operator string(StringData spriteData)
		{
			return spriteData.Value;
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06006B7D RID: 27517 RVA: 0x002968F2 File Offset: 0x00294AF2
		// (set) Token: 0x06006B7E RID: 27518 RVA: 0x00296927 File Offset: 0x00294B27
		public string Value
		{
			get
			{
				if (this.stringVal == null)
				{
					this.stringVal = "";
				}
				if (!(this.stringRef == null))
				{
					return this.stringRef.Value;
				}
				return this.stringVal;
			}
			set
			{
				if (this.stringRef == null)
				{
					this.stringVal = value;
					return;
				}
				this.stringRef.Value = value;
			}
		}

		// Token: 0x06006B7F RID: 27519 RVA: 0x0029694B File Offset: 0x00294B4B
		public string GetDescription()
		{
			if (this.stringRef == null)
			{
				return this.stringVal;
			}
			return this.stringRef.Key;
		}

		// Token: 0x04005A92 RID: 23186
		[SerializeField]
		[VariableProperty("<Value>", new Type[]
		{
			typeof(StringVariable)
		})]
		public StringVariable stringRef;

		// Token: 0x04005A93 RID: 23187
		[SerializeField]
		public string stringVal;
	}
}
