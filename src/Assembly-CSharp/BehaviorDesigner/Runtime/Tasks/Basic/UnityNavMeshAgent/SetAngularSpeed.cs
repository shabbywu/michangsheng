using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent
{
	// Token: 0x020015AF RID: 5551
	[TaskCategory("Basic/NavMeshAgent")]
	[TaskDescription("Sets the maximum turning speed in (deg/s) while following a path. Returns Success.")]
	public class SetAngularSpeed : Action
	{
		// Token: 0x06008299 RID: 33433 RVA: 0x002CDC2C File Offset: 0x002CBE2C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600829A RID: 33434 RVA: 0x00059865 File Offset: 0x00057A65
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

		// Token: 0x0600829B RID: 33435 RVA: 0x00059898 File Offset: 0x00057A98
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.angularSpeed = 0f;
		}

		// Token: 0x04006F53 RID: 28499
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006F54 RID: 28500
		[Tooltip("The NavMeshAgent angular speed")]
		public SharedFloat angularSpeed;

		// Token: 0x04006F55 RID: 28501
		private NavMeshAgent navMeshAgent;

		// Token: 0x04006F56 RID: 28502
		private GameObject prevGameObject;
	}
}
