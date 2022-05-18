using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020012B1 RID: 4785
	[CommandInfo("Vector3", "ToVector2", "Convert Fungus Vector3 to Fungus Vector2", 0)]
	[AddComponentMenu("")]
	public class Vector3ToVector2 : Command
	{
		// Token: 0x060073CA RID: 29642 RVA: 0x0004F033 File Offset: 0x0004D233
		public override void OnEnter()
		{
			this.vec2.Value = this.vec3.Value;
			this.Continue();
		}

		// Token: 0x060073CB RID: 29643 RVA: 0x002AC830 File Offset: 0x002AAA30
		public override string GetSummary()
		{
			if (this.vec3.vector3Ref != null && this.vec2.vector2Ref != null)
			{
				return "Converting " + this.vec3.vector3Ref.Key + " to " + this.vec2.vector2Ref.Key;
			}
			return "Error: variables not set";
		}

		// Token: 0x060073CC RID: 29644 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x060073CD RID: 29645 RVA: 0x0004F056 File Offset: 0x0004D256
		public override bool HasReference(Variable variable)
		{
			return variable == this.vec3.vector3Ref || variable == this.vec2.vector2Ref;
		}

		// Token: 0x040065B0 RID: 26032
		[SerializeField]
		protected Vector3Data vec3;

		// Token: 0x040065B1 RID: 26033
		[SerializeField]
		protected Vector2Data vec2;
	}
}
