using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine.Events;
using YSGame;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x020011B3 RID: 4531
	[TaskCategory("YS")]
	[TaskDescription("结束回合")]
	public class EndRound : Action
	{
		// Token: 0x06007765 RID: 30565 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnAwake()
		{
		}

		// Token: 0x06007766 RID: 30566 RVA: 0x002B9020 File Offset: 0x002B7220
		public override void OnStart()
		{
			this.avatar = (Avatar)this.gameObject.GetComponent<AvaterAddScript>().entity;
		}

		// Token: 0x06007767 RID: 30567 RVA: 0x002B9040 File Offset: 0x002B7240
		public override TaskStatus OnUpdate()
		{
			if (!this.avatar.isPlayer())
			{
				if (!this.CanEndRound())
				{
					return 3;
				}
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

		// Token: 0x06007768 RID: 30568 RVA: 0x002B9098 File Offset: 0x002B7298
		private bool CanEndRound()
		{
			string text = this.transform.GetChild(this.transform.childCount - 1).name;
			if (RoundManager.instance != null)
			{
				text = text.Replace("(Clone)", "");
				if (RoundManager.instance.SkillList.Contains(text))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06007769 RID: 30569 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04006305 RID: 25349
		private Avatar avatar;

		// Token: 0x04006306 RID: 25350
		private SharedInt tempWeith;
	}
}
