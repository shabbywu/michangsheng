using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200126F RID: 4719
	[CommandInfo("Rigidbody2D", "StopMotion2D", "Stop velocity and angular velocity on a Rigidbody2D", 0)]
	[AddComponentMenu("")]
	public class StopMotionRigidBody2D : Command
	{
		// Token: 0x06007286 RID: 29318 RVA: 0x002A842C File Offset: 0x002A662C
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

		// Token: 0x06007287 RID: 29319 RVA: 0x0004E00D File Offset: 0x0004C20D
		public override string GetSummary()
		{
			return this.motionToStop.ToString();
		}

		// Token: 0x06007288 RID: 29320 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x06007289 RID: 29321 RVA: 0x0004E020 File Offset: 0x0004C220
		public override bool HasReference(Variable variable)
		{
			return this.rb.rigidbody2DRef == variable;
		}

		// Token: 0x040064AF RID: 25775
		[SerializeField]
		protected Rigidbody2DData rb;

		// Token: 0x040064B0 RID: 25776
		[SerializeField]
		protected StopMotionRigidBody2D.Motion motionToStop = StopMotionRigidBody2D.Motion.AngularAndLinearVelocity;

		// Token: 0x02001270 RID: 4720
		public enum Motion
		{
			// Token: 0x040064B2 RID: 25778
			Velocity,
			// Token: 0x040064B3 RID: 25779
			AngularVelocity,
			// Token: 0x040064B4 RID: 25780
			AngularAndLinearVelocity
		}
	}
}
