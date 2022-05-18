using System;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Samples
{
	// Token: 0x0200146D RID: 5229
	[TaskCategory("YSSea")]
	[TaskDescription("检测NPC是否能进行移动")]
	public class WhetherCanMove : Conditional
	{
		// Token: 0x06007DF5 RID: 32245 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnAwake()
		{
		}

		// Token: 0x06007DF6 RID: 32246 RVA: 0x00055292 File Offset: 0x00053492
		public override void OnStart()
		{
			this.avatar = this.gameObject.GetComponent<SeaAvatarObjBase>();
		}

		// Token: 0x06007DF7 RID: 32247 RVA: 0x000552A5 File Offset: 0x000534A5
		public override TaskStatus OnUpdate()
		{
			if (this.avatar.WhetherCanMove())
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x04006B65 RID: 27493
		private SeaAvatarObjBase avatar;
	}
}
