using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200126C RID: 4716
	[CommandInfo("Rigidbody2D", "AddForce2D", "Add force to a Rigidbody2D", 0)]
	[AddComponentMenu("")]
	public class AddForce2D : Command
	{
		// Token: 0x0600727C RID: 29308 RVA: 0x002A826C File Offset: 0x002A646C
		public override void OnEnter()
		{
			switch (this.forceFunction)
			{
			case AddForce2D.ForceFunction.AddForce:
				this.rb.Value.AddForce(this.force.Value * this.forceScaleFactor.Value, this.forceMode);
				break;
			case AddForce2D.ForceFunction.AddForceAtPosition:
				this.rb.Value.AddForceAtPosition(this.force.Value * this.forceScaleFactor.Value, this.atPosition.Value, this.forceMode);
				break;
			case AddForce2D.ForceFunction.AddRelativeForce:
				this.rb.Value.AddRelativeForce(this.force.Value * this.forceScaleFactor.Value, this.forceMode);
				break;
			}
			this.Continue();
		}

		// Token: 0x0600727D RID: 29309 RVA: 0x0004DF73 File Offset: 0x0004C173
		public override string GetSummary()
		{
			return this.forceMode.ToString() + ": " + this.force.ToString();
		}

		// Token: 0x0600727E RID: 29310 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x0600727F RID: 29311 RVA: 0x002A8340 File Offset: 0x002A6540
		public override bool HasReference(Variable variable)
		{
			return this.rb.rigidbody2DRef == variable || this.force.vector2Ref == variable || this.forceScaleFactor.floatRef == variable || this.atPosition.vector2Ref == variable;
		}

		// Token: 0x040064A2 RID: 25762
		[SerializeField]
		protected Rigidbody2DData rb;

		// Token: 0x040064A3 RID: 25763
		[SerializeField]
		protected ForceMode2D forceMode;

		// Token: 0x040064A4 RID: 25764
		[SerializeField]
		protected AddForce2D.ForceFunction forceFunction;

		// Token: 0x040064A5 RID: 25765
		[Tooltip("Vector of force to be added")]
		[SerializeField]
		protected Vector2Data force;

		// Token: 0x040064A6 RID: 25766
		[Tooltip("Scale factor to be applied to force as it is used.")]
		[SerializeField]
		protected FloatData forceScaleFactor = new FloatData(1f);

		// Token: 0x040064A7 RID: 25767
		[Tooltip("World position the force is being applied from. Used only in AddForceAtPosition")]
		[SerializeField]
		protected Vector2Data atPosition;

		// Token: 0x0200126D RID: 4717
		public enum ForceFunction
		{
			// Token: 0x040064A9 RID: 25769
			AddForce,
			// Token: 0x040064AA RID: 25770
			AddForceAtPosition,
			// Token: 0x040064AB RID: 25771
			AddRelativeForce
		}
	}
}
