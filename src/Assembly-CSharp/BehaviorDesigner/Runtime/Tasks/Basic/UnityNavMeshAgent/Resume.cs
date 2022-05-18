using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent
{
	// Token: 0x020015AD RID: 5549
	[TaskCategory("Basic/NavMeshAgent")]
	[TaskDescription("Resumes the movement along the current path after a pause. Returns Success.")]
	public class Resume : Action
	{
		// Token: 0x06008291 RID: 33425 RVA: 0x002CDBAC File Offset: 0x002CBDAC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008292 RID: 33426 RVA: 0x000597E7 File Offset: 0x000579E7
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

		// Token: 0x06008293 RID: 33427 RVA: 0x00059810 File Offset: 0x00057A10
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04006F4C RID: 28492
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006F4D RID: 28493
		private NavMeshAgent navMeshAgent;

		// Token: 0x04006F4E RID: 28494
		private GameObject prevGameObject;
	}
}
