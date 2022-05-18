using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x020014B4 RID: 5300
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Clamps the magnitude of the Vector3.")]
	public class ClampMagnitude : Action
	{
		// Token: 0x06007F27 RID: 32551 RVA: 0x000562B7 File Offset: 0x000544B7
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.ClampMagnitude(this.vector3Variable.Value, this.maxLength.Value);
			return 2;
		}

		// Token: 0x06007F28 RID: 32552 RVA: 0x002C9ACC File Offset: 0x002C7CCC
		public override void OnReset()
		{
			this.vector3Variable = (this.storeResult = Vector3.zero);
			this.maxLength = 0f;
		}

		// Token: 0x04006C06 RID: 27654
		[Tooltip("The Vector3 to clamp the magnitude of")]
		public SharedVector3 vector3Variable;

		// Token: 0x04006C07 RID: 27655
		[Tooltip("The max length of the magnitude")]
		public SharedFloat maxLength;

		// Token: 0x04006C08 RID: 27656
		[Tooltip("The clamp magnitude resut")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}
