using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x0200088E RID: 2190
	public class Activity
	{
		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06003865 RID: 14437 RVA: 0x00029049 File Offset: 0x00027249
		// (set) Token: 0x06003866 RID: 14438 RVA: 0x00029051 File Offset: 0x00027251
		public bool Active { get; private set; }

		// Token: 0x06003867 RID: 14439 RVA: 0x0002905A File Offset: 0x0002725A
		public void AddStartTryer(TryerDelegate tryer)
		{
			this.m_StartTryers = (TryerDelegate)Delegate.Combine(this.m_StartTryers, tryer);
		}

		// Token: 0x06003868 RID: 14440 RVA: 0x00029073 File Offset: 0x00027273
		public void AddStopTryer(TryerDelegate tryer)
		{
			this.m_StopTryers = (TryerDelegate)Delegate.Combine(this.m_StopTryers, tryer);
		}

		// Token: 0x06003869 RID: 14441 RVA: 0x0002908C File Offset: 0x0002728C
		public void AddStartListener(Action listener)
		{
			this.m_OnStart = (Action)Delegate.Combine(this.m_OnStart, listener);
		}

		// Token: 0x0600386A RID: 14442 RVA: 0x000290A5 File Offset: 0x000272A5
		public void AddStopListener(Action listener)
		{
			this.m_OnStop = (Action)Delegate.Combine(this.m_OnStop, listener);
		}

		// Token: 0x0600386B RID: 14443 RVA: 0x000290BE File Offset: 0x000272BE
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

		// Token: 0x0600386C RID: 14444 RVA: 0x001A32B0 File Offset: 0x001A14B0
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

		// Token: 0x0600386D RID: 14445 RVA: 0x000290E3 File Offset: 0x000272E3
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

		// Token: 0x0600386E RID: 14446 RVA: 0x0002911C File Offset: 0x0002731C
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

		// Token: 0x0600386F RID: 14447 RVA: 0x001A3304 File Offset: 0x001A1504
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

		// Token: 0x06003870 RID: 14448 RVA: 0x001A3344 File Offset: 0x001A1544
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

		// Token: 0x040032ED RID: 13037
		private TryerDelegate m_StartTryers;

		// Token: 0x040032EE RID: 13038
		private TryerDelegate m_StopTryers;

		// Token: 0x040032EF RID: 13039
		private Action m_OnStart;

		// Token: 0x040032F0 RID: 13040
		private Action m_OnStop;
	}
}
