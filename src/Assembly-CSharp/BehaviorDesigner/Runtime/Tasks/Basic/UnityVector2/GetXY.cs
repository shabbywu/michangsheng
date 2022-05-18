using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
	// Token: 0x020014CF RID: 5327
	[TaskCategory("Basic/Vector2")]
	[TaskDescription("Stores the X and Y values of the Vector2.")]
	public class GetXY : Action
	{
		// Token: 0x06007F75 RID: 32629 RVA: 0x0005663C File Offset: 0x0005483C
		public override TaskStatus OnUpdate()
		{
			this.storeX.Value = this.vector2Variable.Value.x;
			this.storeY.Value = this.vector2Variable.Value.y;
			return 2;
		}

		// Token: 0x06007F76 RID: 32630 RVA: 0x002CA0B0 File Offset: 0x002C82B0
		public override void OnReset()
		{
			this.vector2Variable = Vector2.zero;
			this.storeX = (this.storeY = 0f);
		}

		// Token: 0x04006C4D RID: 27725
		[Tooltip("The Vector2 to get the values of")]
		public SharedVector2 vector2Variable;

		// Token: 0x04006C4E RID: 27726
		[Tooltip("The X value")]
		[RequiredField]
		public SharedFloat storeX;

		// Token: 0x04006C4F RID: 27727
		[Tooltip("The Y value")]
		[RequiredField]
		public SharedFloat storeY;
	}
}
