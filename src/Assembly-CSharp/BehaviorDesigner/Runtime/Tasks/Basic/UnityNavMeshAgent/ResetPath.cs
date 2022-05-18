using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent
{
	// Token: 0x020015AC RID: 5548
	[TaskCategory("Basic/NavMeshAgent")]
	[TaskDescription("Clears the current path. Returns Success.")]
	public class ResetPath : Action
	{
		// Token: 0x0600828D RID: 33421 RVA: 0x002CDB6C File Offset: 0x002CBD6C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600828E RID: 33422 RVA: 0x000597B6 File Offset: 0x000579B6
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				Debug.LogWarning("NavMeshAgent is null");
				return 1;
			}
			this.navMeshAgent.ResetPath();
			return 2;
		}

		// Token: 0x0600828F RID: 33423 RVA: 0x000597DE File Offset: 0x000579DE
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04006F49 RID: 28489
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006F4A RID: 28490
		private NavMeshAgent navMeshAgent;

		// Token: 0x04006F4B RID: 28491
		private GameObject prevGameObject;
	}
}
