using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPhysics
{
	// Token: 0x020010CA RID: 4298
	[TaskCategory("Basic/Physics")]
	[TaskDescription("Returns success if there is any collider intersecting the line between start and end")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=117")]
	public class Linecast : Action
	{
		// Token: 0x060073FA RID: 29690 RVA: 0x002B127A File Offset: 0x002AF47A
		public override TaskStatus OnUpdate()
		{
			if (!Physics.Linecast(this.startPosition.Value, this.endPosition.Value, this.layerMask))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060073FB RID: 29691 RVA: 0x002B12A7 File Offset: 0x002AF4A7
		public override void OnReset()
		{
			this.startPosition = Vector3.zero;
			this.endPosition = Vector3.zero;
			this.layerMask = -1;
		}

		// Token: 0x04005FAD RID: 24493
		[Tooltip("The starting position of the linecast")]
		private SharedVector3 startPosition;

		// Token: 0x04005FAE RID: 24494
		[Tooltip("The ending position of the linecast")]
		private SharedVector3 endPosition;

		// Token: 0x04005FAF RID: 24495
		[Tooltip("Selectively ignore colliders.")]
		public LayerMask layerMask = -1;
	}
}
