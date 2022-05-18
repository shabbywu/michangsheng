using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent
{
	// Token: 0x020015AE RID: 5550
	[TaskCategory("Basic/NavMeshAgent")]
	[TaskDescription("Sets the maximum acceleration of an agent as it follows a path, given in units / sec^2. Returns Success.")]
	public class SetAcceleration : Action
	{
		// Token: 0x06008295 RID: 33429 RVA: 0x002CDBEC File Offset: 0x002CBDEC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008296 RID: 33430 RVA: 0x00059819 File Offset: 0x00057A19
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

		// Token: 0x06008297 RID: 33431 RVA: 0x0005984C File Offset: 0x00057A4C
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.acceleration = 0f;
		}

		// Token: 0x04006F4F RID: 28495
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006F50 RID: 28496
		[Tooltip("The NavMeshAgent acceleration")]
		public SharedFloat acceleration;

		// Token: 0x04006F51 RID: 28497
		private NavMeshAgent navMeshAgent;

		// Token: 0x04006F52 RID: 28498
		private GameObject prevGameObject;
	}
}
