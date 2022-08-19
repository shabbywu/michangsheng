using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E5A RID: 3674
	[CommandInfo("Vector3", "Fields", "Get or Set the x,y,z fields of a vector3 via floatvars", 0)]
	[AddComponentMenu("")]
	public class Vector3Fields : Command
	{
		// Token: 0x06006732 RID: 26418 RVA: 0x00289B84 File Offset: 0x00287D84
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

		// Token: 0x06006733 RID: 26419 RVA: 0x00289C1C File Offset: 0x00287E1C
		public override string GetSummary()
		{
			if (this.vec3.vector3Ref == null)
			{
				return "Error: vec3 not set";
			}
			return this.getOrSet.ToString() + " (" + this.vec3.vector3Ref.Key + ")";
		}

		// Token: 0x06006734 RID: 26420 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x06006735 RID: 26421 RVA: 0x00289C74 File Offset: 0x00287E74
		public override bool HasReference(Variable variable)
		{
			return this.vec3.vector3Ref == variable || this.x.floatRef == variable || this.y.floatRef == variable || this.z.floatRef == variable;
		}

		// Token: 0x0400584A RID: 22602
		public Vector3Fields.GetSet getOrSet;

		// Token: 0x0400584B RID: 22603
		[SerializeField]
		protected Vector3Data vec3;

		// Token: 0x0400584C RID: 22604
		[SerializeField]
		protected FloatData x;

		// Token: 0x0400584D RID: 22605
		[SerializeField]
		protected FloatData y;

		// Token: 0x0400584E RID: 22606
		[SerializeField]
		protected FloatData z;

		// Token: 0x020016C8 RID: 5832
		public enum GetSet
		{
			// Token: 0x040073B8 RID: 29624
			Get,
			// Token: 0x040073B9 RID: 29625
			Set
		}
	}
}
