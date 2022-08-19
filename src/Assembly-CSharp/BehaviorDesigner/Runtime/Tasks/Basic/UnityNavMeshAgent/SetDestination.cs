using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent
{
	// Token: 0x020010F6 RID: 4342
	[TaskCategory("Basic/NavMeshAgent")]
	[TaskDescription("Sets the destination of the agent in world-space units. Returns Success if the destination is valid.")]
	public class SetDestination : Action
	{
		// Token: 0x060074A3 RID: 29859 RVA: 0x002B2BD8 File Offset: 0x002B0DD8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060074A4 RID: 29860 RVA: 0x002B2C18 File Offset: 0x002B0E18
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

		// Token: 0x060074A5 RID: 29861 RVA: 0x002B2C4F File Offset: 0x002B0E4F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.destination = Vector3.zero;
		}

		// Token: 0x04006057 RID: 24663
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006058 RID: 24664
		[SharedRequired]
		[Tooltip("The NavMeshAgent destination")]
		public SharedVector3 destination;

		// Token: 0x04006059 RID: 24665
		private NavMeshAgent navMeshAgent;

		// Token: 0x0400605A RID: 24666
		private GameObject prevGameObject;
	}
}
