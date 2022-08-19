using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005CC RID: 1484
	public class Activity
	{
		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06002FD5 RID: 12245 RVA: 0x001593BF File Offset: 0x001575BF
		// (set) Token: 0x06002FD6 RID: 12246 RVA: 0x001593C7 File Offset: 0x001575C7
		public bool Active { get; private set; }

		// Token: 0x06002FD7 RID: 12247 RVA: 0x001593D0 File Offset: 0x001575D0
		public void AddStartTryer(TryerDelegate tryer)
		{
			this.m_StartTryers = (TryerDelegate)Delegate.Combine(this.m_StartTryers, tryer);
		}

		// Token: 0x06002FD8 RID: 12248 RVA: 0x001593E9 File Offset: 0x001575E9
		public void AddStopTryer(TryerDelegate tryer)
		{
			this.m_StopTryers = (TryerDelegate)Delegate.Combine(this.m_StopTryers, tryer);
		}

		// Token: 0x06002FD9 RID: 12249 RVA: 0x00159402 File Offset: 0x00157602
		public void AddStartListener(Action listener)
		{
			this.m_OnStart = (Action)Delegate.Combine(this.m_OnStart, listener);
		}

		// Token: 0x06002FDA RID: 12250 RVA: 0x0015941B File Offset: 0x0015761B
		public void AddStopListener(Action listener)
		{
			this.m_OnStop = (Action)Delegate.Combine(this.m_OnStop, listener);
		}

		// Token: 0x06002FDB RID: 12251 RVA: 0x00159434 File Offset: 0x00157634
		public void ForceStart()
		{
			if (this.Active)
			{
				return;
			}
			this.Active = true;
			if (this.m_OnStart != null)
			{
				this.m_OnStart();
			}
		}

		// Token: 0x06002FDC RID: 12252 RVA: 0x0015945C File Offset: 0x0015765C
		public bool TryStart()
		{
			if (this.Active)
			{
				return false;
			}
			if (this.m_StartTryers != null)
			{
				bool flag = this.CallStartApprovers();
				if (flag)
				{
					this.Active = true;
				}
				if (flag && this.m_OnStart != null)
				{
					this.m_OnStart();
				}
				return flag;
			}
			Debug.LogWarning("[Activity] - You tried to start an activity which has no tryer (if no one checks if the activity can start, it won't start).");
			return false;
		}

		// Token: 0x06002FDD RID: 12253 RVA: 0x001594AD File Offset: 0x001576AD
		public bool TryStop()
		{
			if (!this.Active)
			{
				return false;
			}
			if (this.m_StopTryers != null && this.CallStopApprovers())
			{
				this.Active = false;
				if (this.m_OnStop != null)
				{
					this.m_OnStop();
				}
				return true;
			}
			return false;
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x001594E6 File Offset: 0x001576E6
		public void ForceStop()
		{
			if (!this.Active)
			{
				return;
			}
			this.Active = false;
			if (this.m_OnStop != null)
			{
				this.m_OnStop();
			}
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x0015950C File Offset: 0x0015770C
		private bool CallStartApprovers()
		{
			Delegate[] invocationList = this.m_StartTryers.GetInvocationList();
			for (int i = 0; i < invocationList.Length; i++)
			{
				if (!(bool)invocationList[i].DynamicInvoke(Array.Empty<object>()))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002FE0 RID: 12256 RVA: 0x0015954C File Offset: 0x0015774C
		private bool CallStopApprovers()
		{
			Delegate[] invocationList = this.m_StopTryers.GetInvocationList();
			for (int i = 0; i < invocationList.Length; i++)
			{
				if (!(bool)invocationList[i].DynamicInvoke(Array.Empty<object>()))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04002A57 RID: 10839
		private TryerDelegate m_StartTryers;

		// Token: 0x04002A58 RID: 10840
		private TryerDelegate m_StopTryers;

		// Token: 0x04002A59 RID: 10841
		private Action m_OnStart;

		// Token: 0x04002A5A RID: 10842
		private Action m_OnStop;
	}
}
