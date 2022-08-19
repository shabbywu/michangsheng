using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
	// Token: 0x0200100E RID: 4110
	[TaskCategory("Basic/Vector2")]
	[TaskDescription("Clamps the magnitude of the Vector2.")]
	public class ClampMagnitude : Action
	{
		// Token: 0x06007163 RID: 29027 RVA: 0x002ABB07 File Offset: 0x002A9D07
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector2.ClampMagnitude(this.vector2Variable.Value, this.maxLength.Value);
			return 2;
		}

		// Token: 0x06007164 RID: 29028 RVA: 0x002ABB30 File Offset: 0x002A9D30
		public override void OnReset()
		{
			this.vector2Variable = (this.storeResult = Vector2.zero);
			this.maxLength = 0f;
		}

		// Token: 0x04005D40 RID: 23872
		[Tooltip("The Vector2 to clamp the magnitude of")]
		public SharedVector2 vector2Variable;

		// Token: 0x04005D41 RID: 23873
		[Tooltip("The max length of the magnitude")]
		public SharedFloat maxLength;

		// Token: 0x04005D42 RID: 23874
		[Tooltip("The clamp magnitude resut")]
		[RequiredField]
		public SharedVector2 storeResult;
	}
}
