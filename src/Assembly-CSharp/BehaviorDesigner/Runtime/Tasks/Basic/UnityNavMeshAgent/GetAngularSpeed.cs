using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent
{
	// Token: 0x020015A7 RID: 5543
	[TaskCategory("Basic/NavMeshAgent")]
	[TaskDescription("Gets the maximum turning speed in (deg/s) while following a path.. Returns Success.")]
	public class GetAngularSpeed : Action
	{
		// Token: 0x06008279 RID: 33401 RVA: 0x002CDA2C File Offset: 0x002CBC2C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600827A RID: 33402 RVA: 0x0005963A File Offset: 0x0005783A
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				Debug.LogWarning("NavMeshAgent is null");
				return 1;
			}
			this.storeValue.Value = this.navMeshAgent.angularSpeed;
			return 2;
		}

		// Token: 0x0600827B RID: 33403 RVA: 0x0005966D File Offset: 0x0005786D
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04006F35 RID: 28469
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006F36 RID: 28470
		[SharedRequired]
		[Tooltip("The NavMeshAgent angular speed")]
		public SharedFloat storeValue;

		// Token: 0x04006F37 RID: 28471
		private NavMeshAgent navMeshAgent;

		// Token: 0x04006F38 RID: 28472
		private GameObject prevGameObject;
	}
}
