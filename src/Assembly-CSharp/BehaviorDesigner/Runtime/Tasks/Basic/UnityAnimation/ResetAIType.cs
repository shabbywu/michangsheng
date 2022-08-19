using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x020011BD RID: 4541
	[TaskCategory("YSSea")]
	[TaskDescription("重置NPC AI类型")]
	public class ResetAIType : Action
	{
		// Token: 0x06007799 RID: 30617 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnAwake()
		{
		}

		// Token: 0x0600779A RID: 30618 RVA: 0x002B931A File Offset: 0x002B751A
		public override void OnStart()
		{
			this.avatar = this.gameObject.GetComponent<SeaAvatarObjBase>();
		}

		// Token: 0x0600779B RID: 30619 RVA: 0x002B932D File Offset: 0x002B752D
		public override TaskStatus OnUpdate()
		{
			this.avatar.ResetBehavirTree(this.AIType.Value);
			return 2;
		}

		// Token: 0x0600779C RID: 30620 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x0400631B RID: 25371
		private SeaAvatarObjBase avatar;

		// Token: 0x0400631C RID: 25372
		public SharedInt AIType;
	}
}
