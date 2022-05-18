using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent
{
	// Token: 0x020015AB RID: 5547
	[TaskCategory("Basic/NavMeshAgent")]
	[TaskDescription("Apply relative movement to the current position. Returns Success.")]
	public class Move : Action
	{
		// Token: 0x06008289 RID: 33417 RVA: 0x002CDB2C File Offset: 0x002CBD2C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600828A RID: 33418 RVA: 0x0005976A File Offset: 0x0005796A
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				Debug.LogWarning("NavMeshAgent is null");
				return 1;
			}
			this.navMeshAgent.Move(this.offset.Value);
			return 2;
		}

		// Token: 0x0600828B RID: 33419 RVA: 0x0005979D File Offset: 0x0005799D
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.offset = Vector3.zero;
		}

		// Token: 0x04006F45 RID: 28485
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006F46 RID: 28486
		[Tooltip("The relative movement vector")]
		public SharedVector3 offset;

		// Token: 0x04006F47 RID: 28487
		private NavMeshAgent navMeshAgent;

		// Token: 0x04006F48 RID: 28488
		private GameObject prevGameObject;
	}
}
