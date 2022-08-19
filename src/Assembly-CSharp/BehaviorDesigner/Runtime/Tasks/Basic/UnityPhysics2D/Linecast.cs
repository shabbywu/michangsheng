using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPhysics2D
{
	// Token: 0x020010C8 RID: 4296
	[TaskCategory("Basic/Physics2D")]
	[TaskDescription("Returns success if there is any collider intersecting the line between start and end")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=118")]
	public class Linecast : Action
	{
		// Token: 0x060073F4 RID: 29684 RVA: 0x002B1062 File Offset: 0x002AF262
		public override TaskStatus OnUpdate()
		{
			if (!Physics2D.Linecast(this.startPosition.Value, this.endPosition.Value, this.layerMask))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060073F5 RID: 29685 RVA: 0x002B1094 File Offset: 0x002AF294
		public override void OnReset()
		{
			this.startPosition = Vector2.zero;
			this.endPosition = Vector2.zero;
			this.layerMask = -1;
		}

		// Token: 0x04005FA0 RID: 24480
		[Tooltip("The starting position of the linecast.")]
		private SharedVector2 startPosition;

		// Token: 0x04005FA1 RID: 24481
		[Tooltip("The ending position of the linecast.")]
		private SharedVector2 endPosition;

		// Token: 0x04005FA2 RID: 24482
		[Tooltip("Selectively ignore colliders.")]
		public LayerMask layerMask = -1;
	}
}
