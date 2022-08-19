using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent
{
	// Token: 0x020010EE RID: 4334
	[TaskCategory("Basic/NavMeshAgent")]
	[TaskDescription("Gets the destination of the agent in world-space units. Returns Success.")]
	public class GetDestination : Action
	{
		// Token: 0x06007483 RID: 29827 RVA: 0x002B27A8 File Offset: 0x002B09A8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007484 RID: 29828 RVA: 0x002B27E8 File Offset: 0x002B09E8
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				Debug.LogWarning("NavMeshAgent is null");
				return 1;
			}
			this.storeValue.Value = this.navMeshAgent.destination;
			return 2;
		}

		// Token: 0x06007485 RID: 29829 RVA: 0x002B281B File Offset: 0x002B0A1B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04006039 RID: 24633
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400603A RID: 24634
		[SharedRequired]
		[Tooltip("The NavMeshAgent destination")]
		public SharedVector3 storeValue;

		// Token: 0x0400603B RID: 24635
		private NavMeshAgent navMeshAgent;

		// Token: 0x0400603C RID: 24636
		private GameObject prevGameObject;
	}
}
