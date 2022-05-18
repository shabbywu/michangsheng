using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001324 RID: 4900
	[AddComponentMenu("")]
	public abstract class BasePhysicsEventHandler : TagFilteredEventHandler
	{
		// Token: 0x0600774D RID: 30541 RVA: 0x000514EA File Offset: 0x0004F6EA
		protected void ProcessCollider(BasePhysicsEventHandler.PhysicsMessageType from, string tagOnOther)
		{
			if ((from & this.FireOn) != (BasePhysicsEventHandler.PhysicsMessageType)0)
			{
				base.ProcessTagFilter(tagOnOther);
			}
		}

		// Token: 0x040067FF RID: 26623
		[Tooltip("Which of the 3d physics messages to we trigger on.")]
		[SerializeField]
		[EnumFlag]
		protected BasePhysicsEventHandler.PhysicsMessageType FireOn = BasePhysicsEventHandler.PhysicsMessageType.Enter;

		// Token: 0x02001325 RID: 4901
		[Flags]
		public enum PhysicsMessageType
		{
			// Token: 0x04006801 RID: 26625
			Enter = 1,
			// Token: 0x04006802 RID: 26626
			Stay = 2,
			// Token: 0x04006803 RID: 26627
			Exit = 4
		}
	}
}
