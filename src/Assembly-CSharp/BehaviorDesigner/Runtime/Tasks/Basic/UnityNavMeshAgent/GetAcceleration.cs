using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent
{
	// Token: 0x020010EC RID: 4332
	[TaskCategory("Basic/NavMeshAgent")]
	[TaskDescription("Gets the maximum acceleration of an agent as it follows a path, given in units / sec^2.. Returns Success.")]
	public class GetAcceleration : Action
	{
		// Token: 0x0600747B RID: 29819 RVA: 0x002B2690 File Offset: 0x002B0890
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600747C RID: 29820 RVA: 0x002B26D0 File Offset: 0x002B08D0
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

		// Token: 0x0600747D RID: 29821 RVA: 0x002B2703 File Offset: 0x002B0903
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04006031 RID: 24625
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006032 RID: 24626
		[SharedRequired]
		[Tooltip("The NavMeshAgent acceleration")]
		public SharedFloat storeValue;

		// Token: 0x04006033 RID: 24627
		private NavMeshAgent navMeshAgent;

		// Token: 0x04006034 RID: 24628
		private GameObject prevGameObject;
	}
}
