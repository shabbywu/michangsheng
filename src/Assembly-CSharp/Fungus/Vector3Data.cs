using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EF3 RID: 3827
	[Serializable]
	public struct Vector3Data
	{
		// Token: 0x06006BA4 RID: 27556 RVA: 0x00296EE0 File Offset: 0x002950E0
		public Vector3Data(Vector3 v)
		{
			this.vector3Val = v;
			this.vector3Ref = null;
		}

		// Token: 0x06006BA5 RID: 27557 RVA: 0x00296EF0 File Offset: 0x002950F0
		public static implicit operator Vector3(Vector3Data vector3Data)
		{
			return vector3Data.Value;
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06006BA6 RID: 27558 RVA: 0x00296EF9 File Offset: 0x002950F9
		// (set) Token: 0x06006BA7 RID: 27559 RVA: 0x00296F1B File Offset: 0x0029511B
		public Vector3 Value
		{
			get
			{
				if (!(this.vector3Ref == null))
				{
					return this.vector3Ref.Value;
				}
				return this.vector3Val;
			}
			set
			{
				if (this.vector3Ref == null)
				{
					this.vector3Val = value;
					return;
				}
				this.vector3Ref.Value = value;
			}
		}

		// Token: 0x06006BA8 RID: 27560 RVA: 0x00296F3F File Offset: 0x0029513F
		public string GetDescription()
		{
			if (this.vector3Ref == null)
			{
				return this.vector3Val.ToString();
			}
			return this.vector3Ref.Key;
		}

		// Token: 0x04005AA4 RID: 23204
		[SerializeField]
		[VariableProperty("<Value>", new Type[]
		{
			typeof(Vector3Variable)
		})]
		public Vector3Variable vector3Ref;

		// Token: 0x04005AA5 RID: 23205
		[SerializeField]
		public Vector3 vector3Val;
	}
}
