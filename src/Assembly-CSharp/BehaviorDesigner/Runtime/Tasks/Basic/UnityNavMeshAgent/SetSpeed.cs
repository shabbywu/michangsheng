using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent
{
	// Token: 0x020015B1 RID: 5553
	[TaskCategory("Basic/NavMeshAgent")]
	[TaskDescription("Sets the maximum movement speed when following a path. Returns Success.")]
	public class SetSpeed : Action
	{
		// Token: 0x060082A1 RID: 33441 RVA: 0x002CDCAC File Offset: 0x002CBEAC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060082A2 RID: 33442 RVA: 0x00059901 File Offset: 0x00057B01
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				Debug.LogWarning("NavMeshAgent is null");
				return 1;
			}
			this.navMeshAgent.speed = this.speed.Value;
			return 2;
		}

		// Token: 0x060082A3 RID: 33443 RVA: 0x00059934 File Offset: 0x00057B34
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.speed = 0f;
		}

		// Token: 0x04006F5B RID: 28507
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006F5C RID: 28508
		[Tooltip("The NavMeshAgent speed")]
		public SharedFloat speed;

		// Token: 0x04006F5D RID: 28509
		private NavMeshAgent navMeshAgent;

		// Token: 0x04006F5E RID: 28510
		private GameObject prevGameObject;
	}
}
