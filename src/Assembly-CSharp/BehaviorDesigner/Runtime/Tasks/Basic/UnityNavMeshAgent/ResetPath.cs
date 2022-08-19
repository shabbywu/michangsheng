using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent
{
	// Token: 0x020010F2 RID: 4338
	[TaskCategory("Basic/NavMeshAgent")]
	[TaskDescription("Clears the current path. Returns Success.")]
	public class ResetPath : Action
	{
		// Token: 0x06007493 RID: 29843 RVA: 0x002B29D8 File Offset: 0x002B0BD8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007494 RID: 29844 RVA: 0x002B2A18 File Offset: 0x002B0C18
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

		// Token: 0x06007495 RID: 29845 RVA: 0x002B2A40 File Offset: 0x002B0C40
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04006049 RID: 24649
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400604A RID: 24650
		private NavMeshAgent navMeshAgent;

		// Token: 0x0400604B RID: 24651
		private GameObject prevGameObject;
	}
}
