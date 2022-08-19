using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x0200106F RID: 4207
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Sets the SharedGameObjectList values from the GameObjects. Returns Success.")]
	public class SharedGameObjectsToGameObjectList : Action
	{
		// Token: 0x060072AA RID: 29354 RVA: 0x002AE4DE File Offset: 0x002AC6DE
		public override void OnAwake()
		{
			this.storedGameObjectList.Value = new List<GameObject>();
		}

		// Token: 0x060072AB RID: 29355 RVA: 0x002AE4F0 File Offset: 0x002AC6F0
		public override TaskStatus OnUpdate()
		{
			if (this.gameObjects == null || this.gameObjects.Length == 0)
			{
				return 1;
			}
			this.storedGameObjectList.Value.Clear();
			for (int i = 0; i < this.gameObjects.Length; i++)
			{
				this.storedGameObjectList.Value.Add(this.gameObjects[i].Value);
			}
			return 2;
		}

		// Token: 0x060072AC RID: 29356 RVA: 0x002AE551 File Offset: 0x002AC751
		public override void OnReset()
		{
			this.gameObjects = null;
			this.storedGameObjectList = null;
		}

		// Token: 0x04005E5F RID: 24159
		[Tooltip("The GameObjects value")]
		public SharedGameObject[] gameObjects;

		// Token: 0x04005E60 RID: 24160
		[RequiredField]
		[Tooltip("The SharedTransformList to set")]
		public SharedGameObjectList storedGameObjectList;
	}
}
