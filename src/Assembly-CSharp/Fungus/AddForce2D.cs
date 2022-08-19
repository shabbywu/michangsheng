using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E1F RID: 3615
	[CommandInfo("Rigidbody2D", "AddForce2D", "Add force to a Rigidbody2D", 0)]
	[AddComponentMenu("")]
	public class AddForce2D : Command
	{
		// Token: 0x060065EE RID: 26094 RVA: 0x002846C8 File Offset: 0x002828C8
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

		// Token: 0x060065EF RID: 26095 RVA: 0x0028479B File Offset: 0x0028299B
		public override string GetSummary()
		{
			return this.forceMode.ToString() + ": " + this.force.ToString();
		}

		// Token: 0x060065F0 RID: 26096 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x060065F1 RID: 26097 RVA: 0x002847CC File Offset: 0x002829CC
		public override bool HasReference(Variable variable)
		{
			return this.rb.rigidbody2DRef == variable || this.force.vector2Ref == variable || this.forceScaleFactor.floatRef == variable || this.atPosition.vector2Ref == variable;
		}

		// Token: 0x0400576D RID: 22381
		[SerializeField]
		protected Rigidbody2DData rb;

		// Token: 0x0400576E RID: 22382
		[SerializeField]
		protected ForceMode2D forceMode;

		// Token: 0x0400576F RID: 22383
		[SerializeField]
		protected AddForce2D.ForceFunction forceFunction;

		// Token: 0x04005770 RID: 22384
		[Tooltip("Vector of force to be added")]
		[SerializeField]
		protected Vector2Data force;

		// Token: 0x04005771 RID: 22385
		[Tooltip("Scale factor to be applied to force as it is used.")]
		[SerializeField]
		protected FloatData forceScaleFactor = new FloatData(1f);

		// Token: 0x04005772 RID: 22386
		[Tooltip("World position the force is being applied from. Used only in AddForceAtPosition")]
		[SerializeField]
		protected Vector2Data atPosition;

		// Token: 0x020016C1 RID: 5825
		public enum ForceFunction
		{
			// Token: 0x04007391 RID: 29585
			AddForce,
			// Token: 0x04007392 RID: 29586
			AddForceAtPosition,
			// Token: 0x04007393 RID: 29587
			AddRelativeForce
		}
	}
}
