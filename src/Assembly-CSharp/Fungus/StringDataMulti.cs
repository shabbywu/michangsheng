using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200138C RID: 5004
	[Serializable]
	public struct StringDataMulti
	{
		// Token: 0x0600791F RID: 31007 RVA: 0x0005290A File Offset: 0x00050B0A
		public StringDataMulti(string v)
		{
			this.stringVal = v;
			this.stringRef = null;
		}

		// Token: 0x06007920 RID: 31008 RVA: 0x0005291A File Offset: 0x00050B1A
		public static implicit operator string(StringDataMulti spriteData)
		{
			return spriteData.Value;
		}

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x06007921 RID: 31009 RVA: 0x00052923 File Offset: 0x00050B23
		// (set) Token: 0x06007922 RID: 31010 RVA: 0x00052958 File Offset: 0x00050B58
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

		// Token: 0x06007923 RID: 31011 RVA: 0x0005297C File Offset: 0x00050B7C
		public string GetDescription()
		{
			if (this.stringRef == null)
			{
				return this.stringVal;
			}
			return this.stringRef.Key;
		}

		// Token: 0x040068FD RID: 26877
		[SerializeField]
		[VariableProperty("<Value>", new Type[]
		{
			typeof(StringVariable)
		})]
		public StringVariable stringRef;

		// Token: 0x040068FE RID: 26878
		[TextArea(1, 15)]
		[SerializeField]
		public string stringVal;
	}
}
