using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EEB RID: 3819
	[Serializable]
	public struct StringDataMulti
	{
		// Token: 0x06006B80 RID: 27520 RVA: 0x0029696D File Offset: 0x00294B6D
		public StringDataMulti(string v)
		{
			this.stringVal = v;
			this.stringRef = null;
		}

		// Token: 0x06006B81 RID: 27521 RVA: 0x0029697D File Offset: 0x00294B7D
		public static implicit operator string(StringDataMulti spriteData)
		{
			return spriteData.Value;
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06006B82 RID: 27522 RVA: 0x00296986 File Offset: 0x00294B86
		// (set) Token: 0x06006B83 RID: 27523 RVA: 0x002969BB File Offset: 0x00294BBB
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

		// Token: 0x06006B84 RID: 27524 RVA: 0x002969DF File Offset: 0x00294BDF
		public string GetDescription()
		{
			if (this.stringRef == null)
			{
				return this.stringVal;
			}
			return this.stringRef.Key;
		}

		// Token: 0x04005A94 RID: 23188
		[SerializeField]
		[VariableProperty("<Value>", new Type[]
		{
			typeof(StringVariable)
		})]
		public StringVariable stringRef;

		// Token: 0x04005A95 RID: 23189
		[TextArea(1, 15)]
		[SerializeField]
		public string stringVal;
	}
}
