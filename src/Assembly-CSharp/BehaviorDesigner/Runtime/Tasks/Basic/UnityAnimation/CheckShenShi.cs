using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x0200167A RID: 5754
	[TaskCategory("YSSea")]
	[TaskDescription("检测神识")]
	public class CheckShenShi : Conditional
	{
		// Token: 0x0600858B RID: 34187 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnAwake()
		{
		}

		// Token: 0x0600858C RID: 34188 RVA: 0x0005CA80 File Offset: 0x0005AC80
		public override void OnStart()
		{
			this.avatar = this.gameObject.GetComponent<SeaAvatarObjBase>();
		}

		// Token: 0x0600858D RID: 34189 RVA: 0x0005CA93 File Offset: 0x0005AC93
		public override TaskStatus OnUpdate()
		{
			if (Tools.instance.getPlayer().shengShi >= this.avatar.ShenShi)
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x0600858E RID: 34190 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04007240 RID: 29248
		private SeaAvatarObjBase avatar;

		// Token: 0x04007241 RID: 29249
		[Tooltip("神识的值大于等于该值返回true")]
		public SharedInt Value;
	}
}
