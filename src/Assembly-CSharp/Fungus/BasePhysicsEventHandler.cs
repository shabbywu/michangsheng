using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EA3 RID: 3747
	[AddComponentMenu("")]
	public abstract class BasePhysicsEventHandler : TagFilteredEventHandler
	{
		// Token: 0x06006A17 RID: 27159 RVA: 0x00292A12 File Offset: 0x00290C12
		protected void ProcessCollider(BasePhysicsEventHandler.PhysicsMessageType from, string tagOnOther)
		{
			if ((from & this.FireOn) != (BasePhysicsEventHandler.PhysicsMessageType)0)
			{
				base.ProcessTagFilter(tagOnOther);
			}
		}

		// Token: 0x040059DC RID: 23004
		[Tooltip("Which of the 3d physics messages to we trigger on.")]
		[SerializeField]
		[EnumFlag]
		protected BasePhysicsEventHandler.PhysicsMessageType FireOn = BasePhysicsEventHandler.PhysicsMessageType.Enter;

		// Token: 0x020016F5 RID: 5877
		[Flags]
		public enum PhysicsMessageType
		{
			// Token: 0x0400747F RID: 29823
			Enter = 1,
			// Token: 0x04007480 RID: 29824
			Stay = 2,
			// Token: 0x04007481 RID: 29825
			Exit = 4
		}
	}
}
