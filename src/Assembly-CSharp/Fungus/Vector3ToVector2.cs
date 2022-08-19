using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E5C RID: 3676
	[CommandInfo("Vector3", "ToVector2", "Convert Fungus Vector3 to Fungus Vector2", 0)]
	[AddComponentMenu("")]
	public class Vector3ToVector2 : Command
	{
		// Token: 0x0600673C RID: 26428 RVA: 0x00289D57 File Offset: 0x00287F57
		public override void OnEnter()
		{
			this.vec2.Value = this.vec3.Value;
			this.Continue();
		}

		// Token: 0x0600673D RID: 26429 RVA: 0x00289D7C File Offset: 0x00287F7C
		public override string GetSummary()
		{
			if (this.vec3.vector3Ref != null && this.vec2.vector2Ref != null)
			{
				return "Converting " + this.vec3.vector3Ref.Key + " to " + this.vec2.vector2Ref.Key;
			}
			return "Error: variables not set";
		}

		// Token: 0x0600673E RID: 26430 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x0600673F RID: 26431 RVA: 0x00289DE4 File Offset: 0x00287FE4
		public override bool HasReference(Variable variable)
		{
			return variable == this.vec3.vector3Ref || variable == this.vec2.vector2Ref;
		}

		// Token: 0x04005851 RID: 22609
		[SerializeField]
		protected Vector3Data vec3;

		// Token: 0x04005852 RID: 22610
		[SerializeField]
		protected Vector2Data vec2;
	}
}
