using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x02001005 RID: 4101
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Stores the X, Y, and Z values of the Vector3.")]
	public class GetXYZ : Action
	{
		// Token: 0x06007148 RID: 29000 RVA: 0x002AB654 File Offset: 0x002A9854
		public override TaskStatus OnUpdate()
		{
			this.storeX.Value = this.vector3Variable.Value.x;
			this.storeY.Value = this.vector3Variable.Value.y;
			this.storeZ.Value = this.vector3Variable.Value.z;
			return 2;
		}

		// Token: 0x06007149 RID: 29001 RVA: 0x002AB6B4 File Offset: 0x002A98B4
		public override void OnReset()
		{
			this.vector3Variable = Vector3.zero;
			this.storeX = (this.storeY = (this.storeZ = 0f));
		}

		// Token: 0x04005D20 RID: 23840
		[Tooltip("The Vector3 to get the values of")]
		public SharedVector3 vector3Variable;

		// Token: 0x04005D21 RID: 23841
		[Tooltip("The X value")]
		[RequiredField]
		public SharedFloat storeX;

		// Token: 0x04005D22 RID: 23842
		[Tooltip("The Y value")]
		[RequiredField]
		public SharedFloat storeY;

		// Token: 0x04005D23 RID: 23843
		[Tooltip("The Z value")]
		[RequiredField]
		public SharedFloat storeZ;
	}
}
