using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001392 RID: 5010
	[Serializable]
	public struct Vector2Data
	{
		// Token: 0x0600793A RID: 31034 RVA: 0x00052B7A File Offset: 0x00050D7A
		public Vector2Data(Vector2 v)
		{
			this.vector2Val = v;
			this.vector2Ref = null;
		}

		// Token: 0x0600793B RID: 31035 RVA: 0x00052B8A File Offset: 0x00050D8A
		public static implicit operator Vector2(Vector2Data vector2Data)
		{
			return vector2Data.Value;
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x0600793C RID: 31036 RVA: 0x00052B93 File Offset: 0x00050D93
		// (set) Token: 0x0600793D RID: 31037 RVA: 0x00052BB5 File Offset: 0x00050DB5
		public Vector2 Value
		{
			get
			{
				if (!(this.vector2Ref == null))
				{
					return this.vector2Ref.Value;
				}
				return this.vector2Val;
			}
			set
			{
				if (this.vector2Ref == null)
				{
					this.vector2Val = value;
					return;
				}
				this.vector2Ref.Value = value;
			}
		}

		// Token: 0x0600793E RID: 31038 RVA: 0x00052BD9 File Offset: 0x00050DD9
		public string GetDescription()
		{
			if (this.vector2Ref == null)
			{
				return this.vector2Val.ToString();
			}
			return this.vector2Ref.Key;
		}

		// Token: 0x04006909 RID: 26889
		[SerializeField]
		[VariableProperty("<Value>", new Type[]
		{
			typeof(Vector2Variable)
		})]
		public Vector2Variable vector2Ref;

		// Token: 0x0400690A RID: 26890
		[SerializeField]
		public Vector2 vector2Val;
	}
}
