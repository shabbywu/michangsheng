using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.SharedVariables
{
	// Token: 0x02001529 RID: 5417
	[TaskCategory("Basic/SharedVariable")]
	[TaskDescription("Sets the SharedGameObjectList values from the GameObjects. Returns Success.")]
	public class SharedGameObjectsToGameObjectList : Action
	{
		// Token: 0x060080A4 RID: 32932 RVA: 0x00057905 File Offset: 0x00055B05
		public override void OnAwake()
		{
			this.storedGameObjectList.Value = new List<GameObject>();
		}

		// Token: 0x060080A5 RID: 32933 RVA: 0x002CB55C File Offset: 0x002C975C
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

		// Token: 0x060080A6 RID: 32934 RVA: 0x00057917 File Offset: 0x00055B17
		public override void OnReset()
		{
			this.gameObjects = null;
			this.storedGameObjectList = null;
		}

		// Token: 0x04006D5F RID: 27999
		[Tooltip("The GameObjects value")]
		public SharedGameObject[] gameObjects;

		// Token: 0x04006D60 RID: 28000
		[RequiredField]
		[Tooltip("The SharedTransformList to set")]
		public SharedGameObjectList storedGameObjectList;
	}
}
