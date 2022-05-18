using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPhysics2D
{
	// Token: 0x02001582 RID: 5506
	[TaskCategory("Basic/Physics2D")]
	[TaskDescription("Returns success if there is any collider intersecting the line between start and end")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=118")]
	public class Linecast : Action
	{
		// Token: 0x060081EE RID: 33262 RVA: 0x00058FAE File Offset: 0x000571AE
		public override TaskStatus OnUpdate()
		{
			if (!Physics2D.Linecast(this.startPosition.Value, this.endPosition.Value, this.layerMask))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060081EF RID: 33263 RVA: 0x00058FE0 File Offset: 0x000571E0
		public override void OnReset()
		{
			this.startPosition = Vector2.zero;
			this.endPosition = Vector2.zero;
			this.layerMask = -1;
		}

		// Token: 0x04006EA0 RID: 28320
		[Tooltip("The starting position of the linecast.")]
		private SharedVector2 startPosition;

		// Token: 0x04006EA1 RID: 28321
		[Tooltip("The ending position of the linecast.")]
		private SharedVector2 endPosition;

		// Token: 0x04006EA2 RID: 28322
		[Tooltip("Selectively ignore colliders.")]
		public LayerMask layerMask = -1;
	}
}
