using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200138B RID: 5003
	[Serializable]
	public struct StringData
	{
		// Token: 0x0600791A RID: 31002 RVA: 0x00052876 File Offset: 0x00050A76
		public StringData(string v)
		{
			this.stringVal = v;
			this.stringRef = null;
		}

		// Token: 0x0600791B RID: 31003 RVA: 0x00052886 File Offset: 0x00050A86
		public static implicit operator string(StringData spriteData)
		{
			return spriteData.Value;
		}

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x0600791C RID: 31004 RVA: 0x0005288F File Offset: 0x00050A8F
		// (set) Token: 0x0600791D RID: 31005 RVA: 0x000528C4 File Offset: 0x00050AC4
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

		// Token: 0x0600791E RID: 31006 RVA: 0x000528E8 File Offset: 0x00050AE8
		public string GetDescription()
		{
			if (this.stringRef == null)
			{
				return this.stringVal;
			}
			return this.stringRef.Key;
		}

		// Token: 0x040068FB RID: 26875
		[SerializeField]
		[VariableProperty("<Value>", new Type[]
		{
			typeof(StringVariable)
		})]
		public StringVariable stringRef;

		// Token: 0x040068FC RID: 26876
		[SerializeField]
		public string stringVal;
	}
}
