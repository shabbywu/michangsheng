using System;

namespace UltimateSurvival
{
	// Token: 0x020005D3 RID: 1491
	public class Value<T>
	{
		// Token: 0x06002FFD RID: 12285 RVA: 0x00159760 File Offset: 0x00157960
		public Value(T initialValue)
		{
			this.m_CurrentValue = initialValue;
			this.m_LastValue = this.m_CurrentValue;
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x0015977B File Offset: 0x0015797B
		public bool Is(T value)
		{
			return this.m_CurrentValue != null && this.m_CurrentValue.Equals(value);
		}

		// Token: 0x06002FFF RID: 12287 RVA: 0x001597A3 File Offset: 0x001579A3
		public void AddChangeListener(Action callback)
		{
			this.m_Set = (Action)Delegate.Combine(this.m_Set, callback);
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x001597BC File Offset: 0x001579BC
		public void RemoveChangeListener(Action callback)
		{
			this.m_Set = (Action)Delegate.Remove(this.m_Set, callback);
		}

		// Token: 0x06003001 RID: 12289 RVA: 0x001597D5 File Offset: 0x001579D5
		public void SetFilter(Value<T>.Filter filter)
		{
			this.m_Filter = filter;
		}

		// Token: 0x06003002 RID: 12290 RVA: 0x001597DE File Offset: 0x001579DE
		public T Get()
		{
			return this.m_CurrentValue;
		}

		// Token: 0x06003003 RID: 12291 RVA: 0x001597E6 File Offset: 0x001579E6
		public T GetLastValue()
		{
			return this.m_LastValue;
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x001597F0 File Offset: 0x001579F0
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

		// Token: 0x06003005 RID: 12293 RVA: 0x00159874 File Offset: 0x00157A74
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

		// Token: 0x06003006 RID: 12294 RVA: 0x001598CC File Offset: 0x00157ACC
		public void SetAndDontUpdate(T value)
		{
			this.m_LastValue = this.m_CurrentValue;
			this.m_CurrentValue = value;
			if (this.m_Filter != null)
			{
				this.m_CurrentValue = this.m_Filter(this.m_LastValue, this.m_CurrentValue);
			}
		}

		// Token: 0x04002A63 RID: 10851
		private Action m_Set;

		// Token: 0x04002A64 RID: 10852
		private Value<T>.Filter m_Filter;

		// Token: 0x04002A65 RID: 10853
		private T m_CurrentValue;

		// Token: 0x04002A66 RID: 10854
		private T m_LastValue;

		// Token: 0x020014AE RID: 5294
		// (Invoke) Token: 0x06008175 RID: 33141
		public delegate T Filter(T lastValue, T newValue);
	}
}
