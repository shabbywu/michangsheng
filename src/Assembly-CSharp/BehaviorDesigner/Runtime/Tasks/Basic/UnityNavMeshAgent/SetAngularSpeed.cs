using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent
{
	// Token: 0x020010F5 RID: 4341
	[TaskCategory("Basic/NavMeshAgent")]
	[TaskDescription("Sets the maximum turning speed in (deg/s) while following a path. Returns Success.")]
	public class SetAngularSpeed : Action
	{
		// Token: 0x0600749F RID: 29855 RVA: 0x002B2B4C File Offset: 0x002B0D4C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060074A0 RID: 29856 RVA: 0x002B2B8C File Offset: 0x002B0D8C
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				Debug.LogWarning("NavMeshAgent is null");
				return 1;
			}
			this.navMeshAgent.angularSpeed = this.angularSpeed.Value;
			return 2;
		}

		// Token: 0x060074A1 RID: 29857 RVA: 0x002B2BBF File Offset: 0x002B0DBF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.angularSpeed = 0f;
		}

		// Token: 0x04006053 RID: 24659
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006054 RID: 24660
		[Tooltip("The NavMeshAgent angular speed")]
		public SharedFloat angularSpeed;

		// Token: 0x04006055 RID: 24661
		private NavMeshAgent navMeshAgent;

		// Token: 0x04006056 RID: 24662
		private GameObject prevGameObject;
	}
}
