using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace YSGame
{
	// Token: 0x02000DA5 RID: 3493
	public class YSFuncList
	{
		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x0600544C RID: 21580 RVA: 0x0003C5C8 File Offset: 0x0003A7C8
		public static YSFuncList Ints
		{
			get
			{
				if (YSFuncList._Ints == null)
				{
					YSFuncList._Ints = new YSFuncList();
				}
				return YSFuncList._Ints;
			}
		}

		// Token: 0x0600544D RID: 21581 RVA: 0x0003C5E0 File Offset: 0x0003A7E0
		public void Clear()
		{
			YSFuncList._Ints = null;
		}

		// Token: 0x0600544E RID: 21582 RVA: 0x0003C5E8 File Offset: 0x0003A7E8
		public void AddFunc(Queue<UnityAction> Next)
		{
			this.funcslist.Enqueue(Next);
			this.Start();
		}

		// Token: 0x0600544F RID: 21583 RVA: 0x0003C5FC File Offset: 0x0003A7FC
		public void ClearQueue()
		{
			this.FlagSwitch = true;
			this.funcslist = new Queue<Queue<UnityAction>>();
		}

		// Token: 0x06005450 RID: 21584 RVA: 0x0003C610 File Offset: 0x0003A810
		public void AddFuncItem(UnityAction Next)
		{
			this.funcslist.Peek().Enqueue(Next);
		}

		// Token: 0x06005451 RID: 21585 RVA: 0x0003C623 File Offset: 0x0003A823
		public void Start()
		{
			if (this.FlagSwitch)
			{
				this.FlagSwitch = false;
				this.Continue();
			}
		}

		// Token: 0x06005452 RID: 21586 RVA: 0x0023155C File Offset: 0x0022F75C
		public void Continue()
		{
			if (this.funcslist.Count == 0)
			{
				this.FlagSwitch = true;
				return;
			}
			Queue<UnityAction> queue = this.funcslist.Peek();
			if (queue.Count > 0)
			{
				queue.Dequeue().Invoke();
				queue.TrimExcess();
				return;
			}
			this.funcslist.Dequeue();
			this.funcslist.TrimExcess();
			this.Continue();
		}

		// Token: 0x04005408 RID: 21512
		public Queue<Queue<UnityAction>> funcslist = new Queue<Queue<UnityAction>>();

		// Token: 0x04005409 RID: 21513
		private static YSFuncList _Ints;

		// Token: 0x0400540A RID: 21514
		public bool FlagSwitch = true;
	}
}
