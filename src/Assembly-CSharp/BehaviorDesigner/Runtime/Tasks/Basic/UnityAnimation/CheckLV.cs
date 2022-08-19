using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x020011B7 RID: 4535
	[TaskCategory("YSSea")]
	[TaskDescription("检测等级")]
	public class CheckLV : Conditional
	{
		// Token: 0x0600777B RID: 30587 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnAwake()
		{
		}

		// Token: 0x0600777C RID: 30588 RVA: 0x002B91C7 File Offset: 0x002B73C7
		public override void OnStart()
		{
			this.avatar = this.gameObject.GetComponent<SeaAvatarObjBase>();
		}

		// Token: 0x0600777D RID: 30589 RVA: 0x002B91DA File Offset: 0x002B73DA
		public override TaskStatus OnUpdate()
		{
			if ((int)Tools.instance.getPlayer().level >= this.Value.Value)
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x0600777E RID: 30590 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x0400630F RID: 25359
		private SeaAvatarObjBase avatar;

		// Token: 0x04006310 RID: 25360
		[Tooltip("等级的值大于等于该值返回true")]
		public SharedInt Value;
	}
}
