using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent
{
	// Token: 0x020010F0 RID: 4336
	[TaskCategory("Basic/NavMeshAgent")]
	[TaskDescription("Gets the maximum movement speed when following a path. Returns Success.")]
	public class GetSpeed : Action
	{
		// Token: 0x0600748B RID: 29835 RVA: 0x002B28C0 File Offset: 0x002B0AC0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600748C RID: 29836 RVA: 0x002B2900 File Offset: 0x002B0B00
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				Debug.LogWarning("NavMeshAgent is null");
				return 1;
			}
			this.storeValue.Value = this.navMeshAgent.speed;
			return 2;
		}

		// Token: 0x0600748D RID: 29837 RVA: 0x002B2933 File Offset: 0x002B0B33
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04006041 RID: 24641
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006042 RID: 24642
		[SharedRequired]
		[Tooltip("The NavMeshAgent speed")]
		public SharedFloat storeValue;

		// Token: 0x04006043 RID: 24643
		private NavMeshAgent navMeshAgent;

		// Token: 0x04006044 RID: 24644
		private GameObject prevGameObject;
	}
}
