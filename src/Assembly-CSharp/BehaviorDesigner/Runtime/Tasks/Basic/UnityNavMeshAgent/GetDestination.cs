using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent
{
	// Token: 0x020015A8 RID: 5544
	[TaskCategory("Basic/NavMeshAgent")]
	[TaskDescription("Gets the destination of the agent in world-space units. Returns Success.")]
	public class GetDestination : Action
	{
		// Token: 0x0600827D RID: 33405 RVA: 0x002CDA6C File Offset: 0x002CBC6C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600827E RID: 33406 RVA: 0x00059686 File Offset: 0x00057886
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				Debug.LogWarning("NavMeshAgent is null");
				return 1;
			}
			this.storeValue.Value = this.navMeshAgent.destination;
			return 2;
		}

		// Token: 0x0600827F RID: 33407 RVA: 0x000596B9 File Offset: 0x000578B9
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04006F39 RID: 28473
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006F3A RID: 28474
		[SharedRequired]
		[Tooltip("The NavMeshAgent destination")]
		public SharedVector3 storeValue;

		// Token: 0x04006F3B RID: 28475
		private NavMeshAgent navMeshAgent;

		// Token: 0x04006F3C RID: 28476
		private GameObject prevGameObject;
	}
}
