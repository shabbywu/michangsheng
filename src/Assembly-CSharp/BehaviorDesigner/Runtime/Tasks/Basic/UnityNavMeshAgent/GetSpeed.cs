using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent
{
	// Token: 0x020015AA RID: 5546
	[TaskCategory("Basic/NavMeshAgent")]
	[TaskDescription("Gets the maximum movement speed when following a path. Returns Success.")]
	public class GetSpeed : Action
	{
		// Token: 0x06008285 RID: 33413 RVA: 0x002CDAEC File Offset: 0x002CBCEC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008286 RID: 33414 RVA: 0x0005971E File Offset: 0x0005791E
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

		// Token: 0x06008287 RID: 33415 RVA: 0x00059751 File Offset: 0x00057951
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04006F41 RID: 28481
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006F42 RID: 28482
		[SharedRequired]
		[Tooltip("The NavMeshAgent speed")]
		public SharedFloat storeValue;

		// Token: 0x04006F43 RID: 28483
		private NavMeshAgent navMeshAgent;

		// Token: 0x04006F44 RID: 28484
		private GameObject prevGameObject;
	}
}
