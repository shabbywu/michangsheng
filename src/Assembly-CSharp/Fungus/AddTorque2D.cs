using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E20 RID: 3616
	[CommandInfo("Rigidbody2D", "AddTorque2D", "Add Torque to a Rigidbody2D", 0)]
	[AddComponentMenu("")]
	public class AddTorque2D : Command
	{
		// Token: 0x060065F3 RID: 26099 RVA: 0x00284840 File Offset: 0x00282A40
		public override void OnEnter()
		{
			this.rb.Value.AddTorque(this.force.Value, this.forceMode);
			this.Continue();
		}

		// Token: 0x060065F4 RID: 26100 RVA: 0x0028486C File Offset: 0x00282A6C
		public override string GetSummary()
		{
			if (this.rb.Value == null)
			{
				return "Error: rb not set";
			}
			return this.forceMode.ToString() + ": " + this.force.Value.ToString() + ((this.force.floatRef != null) ? (" (" + this.force.floatRef.Key + ")") : "");
		}

		// Token: 0x060065F5 RID: 26101 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x060065F6 RID: 26102 RVA: 0x002848F9 File Offset: 0x00282AF9
		public override bool HasReference(Variable variable)
		{
			return this.rb.rigidbody2DRef == variable || this.force.floatRef == variable;
		}

		// Token: 0x04005773 RID: 22387
		[SerializeField]
		protected Rigidbody2DData rb;

		// Token: 0x04005774 RID: 22388
		[SerializeField]
		protected ForceMode2D forceMode;

		// Token: 0x04005775 RID: 22389
		[Tooltip("Amount of torque to be added")]
		[SerializeField]
		protected FloatData force;
	}
}
