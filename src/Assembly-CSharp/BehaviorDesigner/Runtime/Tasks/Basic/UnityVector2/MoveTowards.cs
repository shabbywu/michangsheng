using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
	// Token: 0x02001018 RID: 4120
	[TaskCategory("Basic/Vector2")]
	[TaskDescription("Move from the current position to the target position.")]
	public class MoveTowards : Action
	{
		// Token: 0x06007181 RID: 29057 RVA: 0x002ABE33 File Offset: 0x002AA033
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector2.MoveTowards(this.currentPosition.Value, this.targetPosition.Value, this.speed.Value * Time.deltaTime);
			return 2;
		}

		// Token: 0x06007182 RID: 29058 RVA: 0x002ABE70 File Offset: 0x002AA070
		public override void OnReset()
		{
			this.currentPosition = (this.targetPosition = (this.storeResult = Vector2.zero));
			this.speed = 0f;
		}

		// Token: 0x04005D58 RID: 23896
		[Tooltip("The current position")]
		public SharedVector2 currentPosition;

		// Token: 0x04005D59 RID: 23897
		[Tooltip("The target position")]
		public SharedVector2 targetPosition;

		// Token: 0x04005D5A RID: 23898
		[Tooltip("The movement speed")]
		public SharedFloat speed;

		// Token: 0x04005D5B RID: 23899
		[Tooltip("The move resut")]
		[RequiredField]
		public SharedVector2 storeResult;
	}
}
