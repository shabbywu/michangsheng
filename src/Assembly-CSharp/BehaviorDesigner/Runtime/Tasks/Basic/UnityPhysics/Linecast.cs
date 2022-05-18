using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityPhysics
{
	// Token: 0x02001584 RID: 5508
	[TaskCategory("Basic/Physics")]
	[TaskDescription("Returns success if there is any collider intersecting the line between start and end")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=117")]
	public class Linecast : Action
	{
		// Token: 0x060081F4 RID: 33268 RVA: 0x0005904D File Offset: 0x0005724D
		public override TaskStatus OnUpdate()
		{
			if (!Physics.Linecast(this.startPosition.Value, this.endPosition.Value, this.layerMask))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060081F5 RID: 33269 RVA: 0x0005907A File Offset: 0x0005727A
		public override void OnReset()
		{
			this.startPosition = Vector3.zero;
			this.endPosition = Vector3.zero;
			this.layerMask = -1;
		}

		// Token: 0x04006EAD RID: 28333
		[Tooltip("The starting position of the linecast")]
		private SharedVector3 startPosition;

		// Token: 0x04006EAE RID: 28334
		[Tooltip("The ending position of the linecast")]
		private SharedVector3 endPosition;

		// Token: 0x04006EAF RID: 28335
		[Tooltip("Selectively ignore colliders.")]
		public LayerMask layerMask = -1;
	}
}
