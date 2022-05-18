using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020012AE RID: 4782
	[CommandInfo("Vector3", "Fields", "Get or Set the x,y,z fields of a vector3 via floatvars", 0)]
	[AddComponentMenu("")]
	public class Vector3Fields : Command
	{
		// Token: 0x060073C0 RID: 29632 RVA: 0x002AC6B0 File Offset: 0x002AA8B0
		public override void OnEnter()
		{
			Vector3Fields.GetSet getSet = this.getOrSet;
			if (getSet != Vector3Fields.GetSet.Get)
			{
				if (getSet == Vector3Fields.GetSet.Set)
				{
					this.vec3.Value = new Vector3(this.x.Value, this.y.Value, this.z.Value);
				}
			}
			else
			{
				Vector3 value = this.vec3.Value;
				this.x.Value = value.x;
				this.y.Value = value.y;
				this.z.Value = value.z;
			}
			this.Continue();
		}

		// Token: 0x060073C1 RID: 29633 RVA: 0x002AC748 File Offset: 0x002AA948
		public override string GetSummary()
		{
			if (this.vec3.vector3Ref == null)
			{
				return "Error: vec3 not set";
			}
			return this.getOrSet.ToString() + " (" + this.vec3.vector3Ref.Key + ")";
		}

		// Token: 0x060073C2 RID: 29634 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x060073C3 RID: 29635 RVA: 0x002AC7A0 File Offset: 0x002AA9A0
		public override bool HasReference(Variable variable)
		{
			return this.vec3.vector3Ref == variable || this.x.floatRef == variable || this.y.floatRef == variable || this.z.floatRef == variable;
		}

		// Token: 0x040065A6 RID: 26022
		public Vector3Fields.GetSet getOrSet;

		// Token: 0x040065A7 RID: 26023
		[SerializeField]
		protected Vector3Data vec3;

		// Token: 0x040065A8 RID: 26024
		[SerializeField]
		protected FloatData x;

		// Token: 0x040065A9 RID: 26025
		[SerializeField]
		protected FloatData y;

		// Token: 0x040065AA RID: 26026
		[SerializeField]
		protected FloatData z;

		// Token: 0x020012AF RID: 4783
		public enum GetSet
		{
			// Token: 0x040065AC RID: 26028
			Get,
			// Token: 0x040065AD RID: 26029
			Set
		}
	}
}
