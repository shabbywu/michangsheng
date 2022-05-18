using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x0200152B RID: 5419
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Sets the SharedTransformList values from the Transforms. Returns Success.")]
	public class SharedTransformsToTransformList : Action
	{
		// Token: 0x060080AB RID: 32939 RVA: 0x0005796A File Offset: 0x00055B6A
		public override void OnAwake()
		{
			this.storedTransformList.Value = new List<Transform>();
		}

		// Token: 0x060080AC RID: 32940 RVA: 0x002CB5C0 File Offset: 0x002C97C0
		public override TaskStatus OnUpdate()
		{
			if (this.transforms == null || this.transforms.Length == 0)
			{
				return 1;
			}
			this.storedTransformList.Value.Clear();
			for (int i = 0; i < this.transforms.Length; i++)
			{
				this.storedTransformList.Value.Add(this.transforms[i].Value);
			}
			return 2;
		}

		// Token: 0x060080AD RID: 32941 RVA: 0x0005797C File Offset: 0x00055B7C
		public override void OnReset()
		{
			this.transforms = null;
			this.storedTransformList = null;
		}

		// Token: 0x04006D63 RID: 28003
		[Tooltip("The Transforms value")]
		public SharedTransform[] transforms;

		// Token: 0x04006D64 RID: 28004
		[RequiredField]
		[Tooltip("The SharedTransformList to set")]
		public SharedTransformList storedTransformList;
	}
}
