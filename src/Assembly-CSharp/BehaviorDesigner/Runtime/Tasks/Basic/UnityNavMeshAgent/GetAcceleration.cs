using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent
{
	// Token: 0x020015A6 RID: 5542
	[TaskCategory("Basic/NavMeshAgent")]
	[TaskDescription("Gets the maximum acceleration of an agent as it follows a path, given in units / sec^2.. Returns Success.")]
	public class GetAcceleration : Action
	{
		// Token: 0x06008275 RID: 33397 RVA: 0x002CD9EC File Offset: 0x002CBBEC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008276 RID: 33398 RVA: 0x000595EE File Offset: 0x000577EE
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				Debug.LogWarning("NavMeshAgent is null");
				return 1;
			}
			this.storeValue.Value = this.navMeshAgent.acceleration;
			return 2;
		}

		// Token: 0x06008277 RID: 33399 RVA: 0x00059621 File Offset: 0x00057821
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04006F31 RID: 28465
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006F32 RID: 28466
		[SharedRequired]
		[Tooltip("The NavMeshAgent acceleration")]
		public SharedFloat storeValue;

		// Token: 0x04006F33 RID: 28467
		private NavMeshAgent navMeshAgent;

		// Token: 0x04006F34 RID: 28468
		private GameObject prevGameObject;
	}
}
