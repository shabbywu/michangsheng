using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E5B RID: 3675
	[CommandInfo("Vector3", "Normalise", "Normalise a Vector3", 0)]
	[AddComponentMenu("")]
	public class Vector3Normalise : Command
	{
		// Token: 0x06006737 RID: 26423 RVA: 0x00289CD0 File Offset: 0x00287ED0
		public override void OnEnter()
		{
			this.vec3Out.Value = this.vec3In.Value.normalized;
			this.Continue();
		}

		// Token: 0x06006738 RID: 26424 RVA: 0x00289D01 File Offset: 0x00287F01
		public override string GetSummary()
		{
			if (this.vec3Out.vector3Ref == null)
			{
				return "";
			}
			return this.vec3Out.vector3Ref.Key;
		}

		// Token: 0x06006739 RID: 26425 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x0600673A RID: 26426 RVA: 0x00289D2C File Offset: 0x00287F2C
		public override bool HasReference(Variable variable)
		{
			return this.vec3In.vector3Ref == variable || this.vec3Out.vector3Ref == variable;
		}

		// Token: 0x0400584F RID: 22607
		[SerializeField]
		protected Vector3Data vec3In;

		// Token: 0x04005850 RID: 22608
		[SerializeField]
		protected Vector3Data vec3Out;
	}
}
