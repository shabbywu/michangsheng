using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent
{
	// Token: 0x020010F9 RID: 4345
	[TaskCategory("Basic/NavMeshAgent")]
	[TaskDescription("Warps agent to the provided position. Returns Success.")]
	public class Warp : Action
	{
		// Token: 0x060074AF RID: 29871 RVA: 0x002B2D68 File Offset: 0x002B0F68
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060074B0 RID: 29872 RVA: 0x002B2DA8 File Offset: 0x002B0FA8
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				Debug.LogWarning("NavMeshAgent is null");
				return 1;
			}
			this.navMeshAgent.Warp(this.newPosition.Value);
			return 2;
		}

		// Token: 0x060074B1 RID: 29873 RVA: 0x002B2DDC File Offset: 0x002B0FDC
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.newPosition = Vector3.zero;
		}

		// Token: 0x04006062 RID: 24674
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006063 RID: 24675
		[Tooltip("The position to warp to")]
		public SharedVector3 newPosition;

		// Token: 0x04006064 RID: 24676
		private NavMeshAgent navMeshAgent;

		// Token: 0x04006065 RID: 24677
		private GameObject prevGameObject;
	}
}
