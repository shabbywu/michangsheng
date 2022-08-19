using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent
{
	// Token: 0x020010ED RID: 4333
	[TaskCategory("Basic/NavMeshAgent")]
	[TaskDescription("Gets the maximum turning speed in (deg/s) while following a path.. Returns Success.")]
	public class GetAngularSpeed : Action
	{
		// Token: 0x0600747F RID: 29823 RVA: 0x002B271C File Offset: 0x002B091C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007480 RID: 29824 RVA: 0x002B275C File Offset: 0x002B095C
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

		// Token: 0x06007481 RID: 29825 RVA: 0x002B278F File Offset: 0x002B098F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04006035 RID: 24629
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006036 RID: 24630
		[SharedRequired]
		[Tooltip("The NavMeshAgent angular speed")]
		public SharedFloat storeValue;

		// Token: 0x04006037 RID: 24631
		private NavMeshAgent navMeshAgent;

		// Token: 0x04006038 RID: 24632
		private GameObject prevGameObject;
	}
}
