using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent
{
	// Token: 0x020015B0 RID: 5552
	[TaskCategory("Basic/NavMeshAgent")]
	[TaskDescription("Sets the destination of the agent in world-space units. Returns Success if the destination is valid.")]
	public class SetDestination : Action
	{
		// Token: 0x0600829D RID: 33437 RVA: 0x002CDC6C File Offset: 0x002CBE6C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600829E RID: 33438 RVA: 0x000598B1 File Offset: 0x00057AB1
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				Debug.LogWarning("NavMeshAgent is null");
				return 1;
			}
			if (!this.navMeshAgent.SetDestination(this.destination.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x0600829F RID: 33439 RVA: 0x000598E8 File Offset: 0x00057AE8
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.destination = Vector3.zero;
		}

		// Token: 0x04006F57 RID: 28503
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006F58 RID: 28504
		[SharedRequired]
		[Tooltip("The NavMeshAgent destination")]
		public SharedVector3 destination;

		// Token: 0x04006F59 RID: 28505
		private NavMeshAgent navMeshAgent;

		// Token: 0x04006F5A RID: 28506
		private GameObject prevGameObject;
	}
}
