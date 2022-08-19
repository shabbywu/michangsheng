using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent
{
	// Token: 0x020010F4 RID: 4340
	[TaskCategory("Basic/NavMeshAgent")]
	[TaskDescription("Sets the maximum acceleration of an agent as it follows a path, given in units / sec^2. Returns Success.")]
	public class SetAcceleration : Action
	{
		// Token: 0x0600749B RID: 29851 RVA: 0x002B2AC0 File Offset: 0x002B0CC0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600749C RID: 29852 RVA: 0x002B2B00 File Offset: 0x002B0D00
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				Debug.LogWarning("NavMeshAgent is null");
				return 1;
			}
			this.navMeshAgent.acceleration = this.acceleration.Value;
			return 2;
		}

		// Token: 0x0600749D RID: 29853 RVA: 0x002B2B33 File Offset: 0x002B0D33
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.acceleration = 0f;
		}

		// Token: 0x0400604F RID: 24655
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006050 RID: 24656
		[Tooltip("The NavMeshAgent acceleration")]
		public SharedFloat acceleration;

		// Token: 0x04006051 RID: 24657
		private NavMeshAgent navMeshAgent;

		// Token: 0x04006052 RID: 24658
		private GameObject prevGameObject;
	}
}
