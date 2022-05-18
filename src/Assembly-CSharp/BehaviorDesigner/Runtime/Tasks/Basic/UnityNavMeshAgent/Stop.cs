using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent
{
	// Token: 0x020015B2 RID: 5554
	[TaskCategory("Basic/NavMeshAgent")]
	[TaskDescription("Stop movement of this agent along its current path. Returns Success.")]
	public class Stop : Action
	{
		// Token: 0x060082A5 RID: 33445 RVA: 0x002CDCEC File Offset: 0x002CBEEC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060082A6 RID: 33446 RVA: 0x0005994D File Offset: 0x00057B4D
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

		// Token: 0x060082A7 RID: 33447 RVA: 0x00059976 File Offset: 0x00057B76
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04006F5F RID: 28511
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006F60 RID: 28512
		private NavMeshAgent navMeshAgent;

		// Token: 0x04006F61 RID: 28513
		private GameObject prevGameObject;
	}
}
