using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent
{
	// Token: 0x020010F3 RID: 4339
	[TaskCategory("Basic/NavMeshAgent")]
	[TaskDescription("Resumes the movement along the current path after a pause. Returns Success.")]
	public class Resume : Action
	{
		// Token: 0x06007497 RID: 29847 RVA: 0x002B2A4C File Offset: 0x002B0C4C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007498 RID: 29848 RVA: 0x002B2A8C File Offset: 0x002B0C8C
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				Debug.LogWarning("NavMeshAgent is null");
				return 1;
			}
			this.navMeshAgent.isStopped = false;
			return 2;
		}

		// Token: 0x06007499 RID: 29849 RVA: 0x002B2AB5 File Offset: 0x002B0CB5
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x0400604C RID: 24652
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400604D RID: 24653
		private NavMeshAgent navMeshAgent;

		// Token: 0x0400604E RID: 24654
		private GameObject prevGameObject;
	}
}
