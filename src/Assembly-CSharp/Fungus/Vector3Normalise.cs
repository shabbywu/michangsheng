using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020012B0 RID: 4784
	[CommandInfo("Vector3", "Normalise", "Normalise a Vector3", 0)]
	[AddComponentMenu("")]
	public class Vector3Normalise : Command
	{
		// Token: 0x060073C5 RID: 29637 RVA: 0x002AC7FC File Offset: 0x002AA9FC
		public override void OnEnter()
		{
			this.vec3Out.Value = this.vec3In.Value.normalized;
			this.Continue();
		}

		// Token: 0x060073C6 RID: 29638 RVA: 0x0004EFDD File Offset: 0x0004D1DD
		public override string GetSummary()
		{
			if (this.vec3Out.vector3Ref == null)
			{
				return "";
			}
			return this.vec3Out.vector3Ref.Key;
		}

		// Token: 0x060073C7 RID: 29639 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x060073C8 RID: 29640 RVA: 0x0004F008 File Offset: 0x0004D208
		public override bool HasReference(Variable variable)
		{
			return this.vec3In.vector3Ref == variable || this.vec3Out.vector3Ref == variable;
		}

		// Token: 0x040065AE RID: 26030
		[SerializeField]
		protected Vector3Data vec3In;

		// Token: 0x040065AF RID: 26031
		[SerializeField]
		protected Vector3Data vec3Out;
	}
}
