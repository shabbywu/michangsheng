using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent
{
	// Token: 0x020010EF RID: 4335
	[TaskCategory("Basic/NavMeshAgent")]
	[TaskDescription("Gets the distance between the agent's position and the destination on the current path. Returns Success.")]
	public class GetRemainingDistance : Action
	{
		// Token: 0x06007487 RID: 29831 RVA: 0x002B2834 File Offset: 0x002B0A34
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007488 RID: 29832 RVA: 0x002B2874 File Offset: 0x002B0A74
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

		// Token: 0x06007489 RID: 29833 RVA: 0x002B28A7 File Offset: 0x002B0AA7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x0400603D RID: 24637
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400603E RID: 24638
		[SharedRequired]
		[Tooltip("The remaining distance")]
		public SharedFloat storeValue;

		// Token: 0x0400603F RID: 24639
		private NavMeshAgent navMeshAgent;

		// Token: 0x04006040 RID: 24640
		private GameObject prevGameObject;
	}
}
