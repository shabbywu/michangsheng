using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200126E RID: 4718
	[CommandInfo("Rigidbody2D", "AddTorque2D", "Add Torque to a Rigidbody2D", 0)]
	[AddComponentMenu("")]
	public class AddTorque2D : Command
	{
		// Token: 0x06007281 RID: 29313 RVA: 0x0004DFB9 File Offset: 0x0004C1B9
		public override void OnEnter()
		{
			this.rb.Value.AddTorque(this.force.Value, this.forceMode);
			this.Continue();
		}

		// Token: 0x06007282 RID: 29314 RVA: 0x002A839C File Offset: 0x002A659C
		public override string GetSummary()
		{
			if (this.rb.Value == null)
			{
				return "Error: rb not set";
			}
			return this.forceMode.ToString() + ": " + this.force.Value.ToString() + ((this.force.floatRef != null) ? (" (" + this.force.floatRef.Key + ")") : "");
		}

		// Token: 0x06007283 RID: 29315 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x06007284 RID: 29316 RVA: 0x0004DFE2 File Offset: 0x0004C1E2
		public override bool HasReference(Variable variable)
		{
			return this.rb.rigidbody2DRef == variable || this.force.floatRef == variable;
		}

		// Token: 0x040064AC RID: 25772
		[SerializeField]
		protected Rigidbody2DData rb;

		// Token: 0x040064AD RID: 25773
		[SerializeField]
		protected ForceMode2D forceMode;

		// Token: 0x040064AE RID: 25774
		[Tooltip("Amount of torque to be added")]
		[SerializeField]
		protected FloatData force;
	}
}
