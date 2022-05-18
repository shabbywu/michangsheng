using System;

namespace UltimateSurvival
{
	// Token: 0x02000897 RID: 2199
	public class Value<T>
	{
		// Token: 0x06003895 RID: 14485 RVA: 0x00029317 File Offset: 0x00027517
		public Value(T initialValue)
		{
			this.m_CurrentValue = initialValue;
			this.m_LastValue = this.m_CurrentValue;
		}

		// Token: 0x06003896 RID: 14486 RVA: 0x00029332 File Offset: 0x00027532
		public bool Is(T value)
		{
			return this.m_CurrentValue != null && this.m_CurrentValue.Equals(value);
		}

		// Token: 0x06003897 RID: 14487 RVA: 0x0002935A File Offset: 0x0002755A
		public void AddChangeListener(Action callback)
		{
			this.m_Set = (Action)Delegate.Combine(this.m_Set, callback);
		}

		// Token: 0x06003898 RID: 14488 RVA: 0x00029373 File Offset: 0x00027573
		public void RemoveChangeListener(Action callback)
		{
			this.m_Set = (Action)Delegate.Remove(this.m_Set, callback);
		}

		// Token: 0x06003899 RID: 14489 RVA: 0x0002938C File Offset: 0x0002758C
		public void SetFilter(Value<T>.Filter filter)
		{
			this.m_Filter = filter;
		}

		// Token: 0x0600389A RID: 14490 RVA: 0x00029395 File Offset: 0x00027595
		public T Get()
		{
			return this.m_CurrentValue;
		}

		// Token: 0x0600389B RID: 14491 RVA: 0x0002939D File Offset: 0x0002759D
		public T GetLastValue()
		{
			return this.m_LastValue;
		}

		// Token: 0x0600389C RID: 14492 RVA: 0x001A3384 File Offset: 0x001A1584
		public void Set(T value)
		{
			this.m_LastValue = this.m_CurrentValue;
			this.m_CurrentValue = value;
			if (this.m_Filter != null)
			{
				this.m_CurrentValue = this.m_Filter(this.m_LastValue, this.m_CurrentValue);
			}
			if (this.m_Set != null && (this.m_LastValue == null || !this.m_LastValue.Equals(this.m_CurrentValue)))
			{
				this.m_Set();
			}
		}

		// Token: 0x0600389D RID: 14493 RVA: 0x001A3408 File Offset: 0x001A1608
		public void SetAndForceUpdate(T value)
		{
			this.m_LastValue = this.m_CurrentValue;
			this.m_CurrentValue = value;
			if (this.m_Filter != null)
			{
				this.m_CurrentValue = this.m_Filter(this.m_LastValue, this.m_CurrentValue);
			}
			if (this.m_Set != null)
			{
				this.m_Set();
			}
		}

		// Token: 0x0600389E RID: 14494 RVA: 0x000293A5 File Offset: 0x000275A5
		public void SetAndDontUpdate(T value)
		{
			this.m_LastValue = this.m_CurrentValue;
			this.m_CurrentValue = value;
			if (this.m_Filter != null)
			{
				this.m_CurrentValue = this.m_Filter(this.m_LastValue, this.m_CurrentValue);
			}
		}

		// Token: 0x040032F9 RID: 13049
		private Action m_Set;

		// Token: 0x040032FA RID: 13050
		private Value<T>.Filter m_Filter;

		// Token: 0x040032FB RID: 13051
		private T m_CurrentValue;

		// Token: 0x040032FC RID: 13052
		private T m_LastValue;

		// Token: 0x02000898 RID: 2200
		// (Invoke) Token: 0x060038A0 RID: 14496
		public delegate T Filter(T lastValue, T newValue);
	}
}
