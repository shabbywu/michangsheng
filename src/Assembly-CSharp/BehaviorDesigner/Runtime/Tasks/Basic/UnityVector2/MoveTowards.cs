using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
	// Token: 0x020014D1 RID: 5329
	[TaskCategory("Basic/Vector2")]
	[TaskDescription("Move from the current position to the target position.")]
	public class MoveTowards : Action
	{
		// Token: 0x06007F7B RID: 32635 RVA: 0x000566A9 File Offset: 0x000548A9
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector2.MoveTowards(this.currentPosition.Value, this.targetPosition.Value, this.speed.Value * Time.deltaTime);
			return 2;
		}

		// Token: 0x06007F7C RID: 32636 RVA: 0x002CA128 File Offset: 0x002C8328
		public override void OnReset()
		{
			this.currentPosition = (this.targetPosition = (this.storeResult = Vector2.zero));
			this.speed = 0f;
		}

		// Token: 0x04006C54 RID: 27732
		[Tooltip("The current position")]
		public SharedVector2 currentPosition;

		// Token: 0x04006C55 RID: 27733
		[Tooltip("The target position")]
		public SharedVector2 targetPosition;

		// Token: 0x04006C56 RID: 27734
		[Tooltip("The movement speed")]
		public SharedFloat speed;

		// Token: 0x04006C57 RID: 27735
		[Tooltip("The move resut")]
		[RequiredField]
		public SharedVector2 storeResult;
	}
}
