using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent
{
	// Token: 0x020010F7 RID: 4343
	[TaskCategory("Basic/NavMeshAgent")]
	[TaskDescription("Sets the maximum movement speed when following a path. Returns Success.")]
	public class SetSpeed : Action
	{
		// Token: 0x060074A7 RID: 29863 RVA: 0x002B2C68 File Offset: 0x002B0E68
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060074A8 RID: 29864 RVA: 0x002B2CA8 File Offset: 0x002B0EA8
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

		// Token: 0x060074A9 RID: 29865 RVA: 0x002B2CDB File Offset: 0x002B0EDB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.speed = 0f;
		}

		// Token: 0x0400605B RID: 24667
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400605C RID: 24668
		[Tooltip("The NavMeshAgent speed")]
		public SharedFloat speed;

		// Token: 0x0400605D RID: 24669
		private NavMeshAgent navMeshAgent;

		// Token: 0x0400605E RID: 24670
		private GameObject prevGameObject;
	}
}
