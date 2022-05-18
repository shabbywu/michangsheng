using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
	// Token: 0x020015F6 RID: 5622
	[TaskCategory("Basic/GameObject")]
	[TaskDescription("Finds a GameObject by tag. Returns Success.")]
	public class FindGameObjectsWithTag : Action
	{
		// Token: 0x06008376 RID: 33654 RVA: 0x002CE9B8 File Offset: 0x002CCBB8
		public override TaskStatus OnUpdate()
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag(this.tag.Value);
			for (int i = 0; i < array.Length; i++)
			{
				this.storeValue.Value.Add(array[i]);
			}
			return 2;
		}

		// Token: 0x06008377 RID: 33655 RVA: 0x0005A688 File Offset: 0x00058888
		public override void OnReset()
		{
			this.tag.Value = null;
			this.storeValue.Value = null;
		}

		// Token: 0x0400702A RID: 28714
		[Tooltip("The tag of the GameObject to find")]
		public SharedString tag;

		// Token: 0x0400702B RID: 28715
		[Tooltip("The objects found by name")]
		[RequiredField]
		public SharedGameObjectList storeValue;
	}
}
