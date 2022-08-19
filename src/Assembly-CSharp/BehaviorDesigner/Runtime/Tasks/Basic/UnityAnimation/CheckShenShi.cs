using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x020011B8 RID: 4536
	[TaskCategory("YSSea")]
	[TaskDescription("检测神识")]
	public class CheckShenShi : Conditional
	{
		// Token: 0x06007780 RID: 30592 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnAwake()
		{
		}

		// Token: 0x06007781 RID: 30593 RVA: 0x002B91FB File Offset: 0x002B73FB
		public override void OnStart()
		{
			this.avatar = this.gameObject.GetComponent<SeaAvatarObjBase>();
		}

		// Token: 0x06007782 RID: 30594 RVA: 0x002B920E File Offset: 0x002B740E
		public override TaskStatus OnUpdate()
		{
			if (Tools.instance.getPlayer().shengShi >= this.avatar.ShenShi)
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x06007783 RID: 30595 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04006311 RID: 25361
		private SeaAvatarObjBase avatar;

		// Token: 0x04006312 RID: 25362
		[Tooltip("神识的值大于等于该值返回true")]
		public SharedInt Value;
	}
}
