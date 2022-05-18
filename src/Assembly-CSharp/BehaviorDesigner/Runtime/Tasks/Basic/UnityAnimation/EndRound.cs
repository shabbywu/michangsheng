using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine.Events;
using YSGame;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x02001675 RID: 5749
	[TaskCategory("YS")]
	[TaskDescription("结束回合")]
	public class EndRound : Action
	{
		// Token: 0x06008571 RID: 34161 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnAwake()
		{
		}

		// Token: 0x06008572 RID: 34162 RVA: 0x0005C95E File Offset: 0x0005AB5E
		public override void OnStart()
		{
			this.avatar = (Avatar)this.gameObject.GetComponent<AvaterAddScript>().entity;
		}

		// Token: 0x06008573 RID: 34163 RVA: 0x002D110C File Offset: 0x002CF30C
		public override TaskStatus OnUpdate()
		{
			if (!this.avatar.isPlayer())
			{
				this.avatar.state = 2;
				Queue<UnityAction> queue = new Queue<UnityAction>();
				UnityAction item = delegate()
				{
					this.avatar.AvatarEndRound();
					YSFuncList.Ints.Continue();
				};
				queue.Enqueue(item);
				YSFuncList.Ints.AddFunc(queue);
			}
			return 2;
		}

		// Token: 0x06008574 RID: 34164 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04007234 RID: 29236
		private Avatar avatar;

		// Token: 0x04007235 RID: 29237
		private SharedInt tempWeith;
	}
}
