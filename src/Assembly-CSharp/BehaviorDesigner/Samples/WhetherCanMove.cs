using System;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Samples
{
	// Token: 0x02000FB5 RID: 4021
	[TaskCategory("YSSea")]
	[TaskDescription("检测NPC是否能进行移动")]
	public class WhetherCanMove : Conditional
	{
		// Token: 0x06006FFB RID: 28667 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnAwake()
		{
		}

		// Token: 0x06006FFC RID: 28668 RVA: 0x002A8B9E File Offset: 0x002A6D9E
		public override void OnStart()
		{
			this.avatar = this.gameObject.GetComponent<SeaAvatarObjBase>();
		}

		// Token: 0x06006FFD RID: 28669 RVA: 0x002A8BB1 File Offset: 0x002A6DB1
		public override TaskStatus OnUpdate()
		{
			if (this.avatar.WhetherCanMove())
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x04005C6D RID: 23661
		private SeaAvatarObjBase avatar;
	}
}
