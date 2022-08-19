using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001071 RID: 4209
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Sets the SharedTransformList values from the Transforms. Returns Success.")]
	public class SharedTransformsToTransformList : Action
	{
		// Token: 0x060072B1 RID: 29361 RVA: 0x002AE5A4 File Offset: 0x002AC7A4
		public override void OnAwake()
		{
			this.storedTransformList.Value = new List<Transform>();
		}

		// Token: 0x060072B2 RID: 29362 RVA: 0x002AE5B8 File Offset: 0x002AC7B8
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

		// Token: 0x060072B3 RID: 29363 RVA: 0x002AE619 File Offset: 0x002AC819
		public override void OnReset()
		{
			this.transforms = null;
			this.storedTransformList = null;
		}

		// Token: 0x04005E63 RID: 24163
		[Tooltip("The Transforms value")]
		public SharedTransform[] transforms;

		// Token: 0x04005E64 RID: 24164
		[RequiredField]
		[Tooltip("The SharedTransformList to set")]
		public SharedTransformList storedTransformList;
	}
}
