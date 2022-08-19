using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent
{
	// Token: 0x020010F1 RID: 4337
	[TaskCategory("Basic/NavMeshAgent")]
	[TaskDescription("Apply relative movement to the current position. Returns Success.")]
	public class Move : Action
	{
		// Token: 0x0600748F RID: 29839 RVA: 0x002B294C File Offset: 0x002B0B4C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007490 RID: 29840 RVA: 0x002B298C File Offset: 0x002B0B8C
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				Debug.LogWarning("NavMeshAgent is null");
				return 1;
			}
			this.navMeshAgent.Move(this.offset.Value);
			return 2;
		}

		// Token: 0x06007491 RID: 29841 RVA: 0x002B29BF File Offset: 0x002B0BBF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.offset = Vector3.zero;
		}

		// Token: 0x04006045 RID: 24645
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006046 RID: 24646
		[Tooltip("The relative movement vector")]
		public SharedVector3 offset;

		// Token: 0x04006047 RID: 24647
		private NavMeshAgent navMeshAgent;

		// Token: 0x04006048 RID: 24648
		private GameObject prevGameObject;
	}
}
