using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent
{
	// Token: 0x020015B3 RID: 5555
	[TaskCategory("Basic/NavMeshAgent")]
	[TaskDescription("Warps agent to the provided position. Returns Success.")]
	public class Warp : Action
	{
		// Token: 0x060082A9 RID: 33449 RVA: 0x002CDD2C File Offset: 0x002CBF2C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060082AA RID: 33450 RVA: 0x0005997F File Offset: 0x00057B7F
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				Debug.LogWarning("NavMeshAgent is null");
				return 1;
			}
			this.navMeshAgent.Warp(this.newPosition.Value);
			return 2;
		}

		// Token: 0x060082AB RID: 33451 RVA: 0x000599B3 File Offset: 0x00057BB3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.newPosition = Vector3.zero;
		}

		// Token: 0x04006F62 RID: 28514
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006F63 RID: 28515
		[Tooltip("The position to warp to")]
		public SharedVector3 newPosition;

		// Token: 0x04006F64 RID: 28516
		private NavMeshAgent navMeshAgent;

		// Token: 0x04006F65 RID: 28517
		private GameObject prevGameObject;
	}
}
