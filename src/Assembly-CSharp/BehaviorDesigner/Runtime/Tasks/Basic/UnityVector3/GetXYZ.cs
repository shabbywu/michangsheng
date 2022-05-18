using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x020014BD RID: 5309
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Stores the X, Y, and Z values of the Vector3.")]
	public class GetXYZ : Action
	{
		// Token: 0x06007F42 RID: 32578 RVA: 0x002C9BCC File Offset: 0x002C7DCC
		public override TaskStatus OnUpdate()
		{
			this.storeX.Value = this.vector3Variable.Value.x;
			this.storeY.Value = this.vector3Variable.Value.y;
			this.storeZ.Value = this.vector3Variable.Value.z;
			return 2;
		}

		// Token: 0x06007F43 RID: 32579 RVA: 0x002C9C2C File Offset: 0x002C7E2C
		public override void OnReset()
		{
			this.vector3Variable = Vector3.zero;
			this.storeX = (this.storeY = (this.storeZ = 0f));
		}

		// Token: 0x04006C18 RID: 27672
		[Tooltip("The Vector3 to get the values of")]
		public SharedVector3 vector3Variable;

		// Token: 0x04006C19 RID: 27673
		[Tooltip("The X value")]
		[RequiredField]
		public SharedFloat storeX;

		// Token: 0x04006C1A RID: 27674
		[Tooltip("The Y value")]
		[RequiredField]
		public SharedFloat storeY;

		// Token: 0x04006C1B RID: 27675
		[Tooltip("The Z value")]
		[RequiredField]
		public SharedFloat storeZ;
	}
}
