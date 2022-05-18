using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
	// Token: 0x020014C7 RID: 5319
	[TaskCategory("Basic/Vector2")]
	[TaskDescription("Clamps the magnitude of the Vector2.")]
	public class ClampMagnitude : Action
	{
		// Token: 0x06007F5D RID: 32605 RVA: 0x000564F3 File Offset: 0x000546F3
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector2.ClampMagnitude(this.vector2Variable.Value, this.maxLength.Value);
			return 2;
		}

		// Token: 0x06007F5E RID: 32606 RVA: 0x002C9FB0 File Offset: 0x002C81B0
		public override void OnReset()
		{
			this.vector2Variable = (this.storeResult = Vector2.zero);
			this.maxLength = 0f;
		}

		// Token: 0x04006C3C RID: 27708
		[Tooltip("The Vector2 to clamp the magnitude of")]
		public SharedVector2 vector2Variable;

		// Token: 0x04006C3D RID: 27709
		[Tooltip("The max length of the magnitude")]
		public SharedFloat maxLength;

		// Token: 0x04006C3E RID: 27710
		[Tooltip("The clamp magnitude resut")]
		[RequiredField]
		public SharedVector2 storeResult;
	}
}
