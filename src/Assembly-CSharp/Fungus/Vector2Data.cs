using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EF1 RID: 3825
	[Serializable]
	public struct Vector2Data
	{
		// Token: 0x06006B9B RID: 27547 RVA: 0x00296D5C File Offset: 0x00294F5C
		public Vector2Data(Vector2 v)
		{
			this.vector2Val = v;
			this.vector2Ref = null;
		}

		// Token: 0x06006B9C RID: 27548 RVA: 0x00296D6C File Offset: 0x00294F6C
		public static implicit operator Vector2(Vector2Data vector2Data)
		{
			return vector2Data.Value;
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06006B9D RID: 27549 RVA: 0x00296D75 File Offset: 0x00294F75
		// (set) Token: 0x06006B9E RID: 27550 RVA: 0x00296D97 File Offset: 0x00294F97
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

		// Token: 0x06006B9F RID: 27551 RVA: 0x00296DBB File Offset: 0x00294FBB
		public string GetDescription()
		{
			if (this.vector2Ref == null)
			{
				return this.vector2Val.ToString();
			}
			return this.vector2Ref.Key;
		}

		// Token: 0x04005AA0 RID: 23200
		[SerializeField]
		[VariableProperty("<Value>", new Type[]
		{
			typeof(Vector2Variable)
		})]
		public Vector2Variable vector2Ref;

		// Token: 0x04005AA1 RID: 23201
		[SerializeField]
		public Vector2 vector2Val;
	}
}
