using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace YSGame
{
	// Token: 0x02000A76 RID: 2678
	public class YSFuncList
	{
		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06004B38 RID: 19256 RVA: 0x001FFA15 File Offset: 0x001FDC15
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

		// Token: 0x06004B39 RID: 19257 RVA: 0x001FFA2D File Offset: 0x001FDC2D
		public void Clear()
		{
			YSFuncList._Ints = null;
		}

		// Token: 0x06004B3A RID: 19258 RVA: 0x001FFA35 File Offset: 0x001FDC35
		public void AddFunc(Queue<UnityAction> Next)
		{
			this.funcslist.Enqueue(Next);
			this.Start();
		}

		// Token: 0x06004B3B RID: 19259 RVA: 0x001FFA49 File Offset: 0x001FDC49
		public void ClearQueue()
		{
			this.FlagSwitch = true;
			this.funcslist = new Queue<Queue<UnityAction>>();
		}

		// Token: 0x06004B3C RID: 19260 RVA: 0x001FFA5D File Offset: 0x001FDC5D
		public void AddFuncItem(UnityAction Next)
		{
			this.funcslist.Peek().Enqueue(Next);
		}

		// Token: 0x06004B3D RID: 19261 RVA: 0x001FFA70 File Offset: 0x001FDC70
		public void Start()
		{
			if (this.FlagSwitch)
			{
				this.FlagSwitch = false;
				this.Continue();
			}
		}

		// Token: 0x06004B3E RID: 19262 RVA: 0x001FFA88 File Offset: 0x001FDC88
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

		// Token: 0x04004A63 RID: 19043
		public Queue<Queue<UnityAction>> funcslist = new Queue<Queue<UnityAction>>();

		// Token: 0x04004A64 RID: 19044
		private static YSFuncList _Ints;

		// Token: 0x04004A65 RID: 19045
		public bool FlagSwitch = true;
	}
}
