using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
	// Token: 0x02001137 RID: 4407
	[TaskCategory("Basic/GameObject")]
	[TaskDescription("Finds a GameObject by tag. Returns Success.")]
	public class FindGameObjectsWithTag : Action
	{
		// Token: 0x0600757C RID: 30076 RVA: 0x002B4704 File Offset: 0x002B2904
		public override TaskStatus OnUpdate()
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag(this.tag.Value);
			for (int i = 0; i < array.Length; i++)
			{
				this.storeValue.Value.Add(array[i]);
			}
			return 2;
		}

		// Token: 0x0600757D RID: 30077 RVA: 0x002B4744 File Offset: 0x002B2944
		public override void OnReset()
		{
			this.tag.Value = null;
			this.storeValue.Value = null;
		}

		// Token: 0x04006107 RID: 24839
		[Tooltip("The tag of the GameObject to find")]
		public SharedString tag;

		// Token: 0x04006108 RID: 24840
		[Tooltip("The objects found by name")]
		[RequiredField]
		public SharedGameObjectList storeValue;
	}
}
