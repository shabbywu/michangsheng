using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E21 RID: 3617
	[CommandInfo("Rigidbody2D", "StopMotion2D", "Stop velocity and angular velocity on a Rigidbody2D", 0)]
	[AddComponentMenu("")]
	public class StopMotionRigidBody2D : Command
	{
		// Token: 0x060065F8 RID: 26104 RVA: 0x00284924 File Offset: 0x00282B24
		public override void OnEnter()
		{
			switch (this.motionToStop)
			{
			case StopMotionRigidBody2D.Motion.Velocity:
				this.rb.Value.velocity = Vector2.zero;
				break;
			case StopMotionRigidBody2D.Motion.AngularVelocity:
				this.rb.Value.angularVelocity = 0f;
				break;
			case StopMotionRigidBody2D.Motion.AngularAndLinearVelocity:
				this.rb.Value.angularVelocity = 0f;
				this.rb.Value.velocity = Vector2.zero;
				break;
			}
			this.Continue();
		}

		// Token: 0x060065F9 RID: 26105 RVA: 0x002849AA File Offset: 0x00282BAA
		public override string GetSummary()
		{
			return this.motionToStop.ToString();
		}

		// Token: 0x060065FA RID: 26106 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x060065FB RID: 26107 RVA: 0x002849BD File Offset: 0x00282BBD
		public override bool HasReference(Variable variable)
		{
			return this.rb.rigidbody2DRef == variable;
		}

		// Token: 0x04005776 RID: 22390
		[SerializeField]
		protected Rigidbody2DData rb;

		// Token: 0x04005777 RID: 22391
		[SerializeField]
		protected StopMotionRigidBody2D.Motion motionToStop = StopMotionRigidBody2D.Motion.AngularAndLinearVelocity;

		// Token: 0x020016C2 RID: 5826
		public enum Motion
		{
			// Token: 0x04007395 RID: 29589
			Velocity,
			// Token: 0x04007396 RID: 29590
			AngularVelocity,
			// Token: 0x04007397 RID: 29591
			AngularAndLinearVelocity
		}
	}
}
