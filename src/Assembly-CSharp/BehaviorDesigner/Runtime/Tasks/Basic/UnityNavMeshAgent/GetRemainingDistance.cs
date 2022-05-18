using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent
{
	// Token: 0x020015A9 RID: 5545
	[TaskCategory("Basic/NavMeshAgent")]
	[TaskDescription("Gets the distance between the agent's position and the destination on the current path. Returns Success.")]
	public class GetRemainingDistance : Action
	{
		// Token: 0x06008281 RID: 33409 RVA: 0x002CDAAC File Offset: 0x002CBCAC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008282 RID: 33410 RVA: 0x000596D2 File Offset: 0x000578D2
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				Debug.LogWarning("NavMeshAgent is null");
				return 1;
			}
			this.storeValue.Value = this.navMeshAgent.remainingDistance;
			return 2;
		}

		// Token: 0x06008283 RID: 33411 RVA: 0x00059705 File Offset: 0x00057905
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04006F3D RID: 28477
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006F3E RID: 28478
		[SharedRequired]
		[Tooltip("The remaining distance")]
		public SharedFloat storeValue;

		// Token: 0x04006F3F RID: 28479
		private NavMeshAgent navMeshAgent;

		// Token: 0x04006F40 RID: 28480
		private GameObject prevGameObject;
	}
}
