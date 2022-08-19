using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent
{
	// Token: 0x020010F8 RID: 4344
	[TaskCategory("Basic/NavMeshAgent")]
	[TaskDescription("Stop movement of this agent along its current path. Returns Success.")]
	public class Stop : Action
	{
		// Token: 0x060074AB RID: 29867 RVA: 0x002B2CF4 File Offset: 0x002B0EF4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060074AC RID: 29868 RVA: 0x002B2D34 File Offset: 0x002B0F34
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				Debug.LogWarning("NavMeshAgent is null");
				return 1;
			}
			this.navMeshAgent.isStopped = true;
			return 2;
		}

		// Token: 0x060074AD RID: 29869 RVA: 0x002B2D5D File Offset: 0x002B0F5D
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x0400605F RID: 24671
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006060 RID: 24672
		private NavMeshAgent navMeshAgent;

		// Token: 0x04006061 RID: 24673
		private GameObject prevGameObject;
	}
}
