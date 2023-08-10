using System;

namespace UltimateSurvival;

public class Value<T>
{
	public delegate T Filter(T lastValue, T newValue);

	private Action m_Set;

	private Filter m_Filter;

	private T m_CurrentValue;

	private T m_LastValue;

	public Value(T initialValue)
	{
		m_CurrentValue = initialValue;
		m_LastValue = m_CurrentValue;
	}

	public bool Is(T value)
	{
		if (m_CurrentValue != null)
		{
			return m_CurrentValue.Equals(value);
		}
		return false;
	}

	public void AddChangeListener(Action callback)
	{
		m_Set = (Action)Delegate.Combine(m_Set, callback);
	}

	public void RemoveChangeListener(Action callback)
	{
		m_Set = (Action)Delegate.Remove(m_Set, callback);
	}

	public void SetFilter(Filter filter)
	{
		m_Filter = filter;
	}

	public T Get()
	{
		return m_CurrentValue;
	}

	public T GetLastValue()
	{
		return m_LastValue;
	}

	public void Set(T value)
	{
		m_LastValue = m_CurrentValue;
		m_CurrentValue = value;
		if (m_Filter != null)
		{
			m_CurrentValue = m_Filter(m_LastValue, m_CurrentValue);
		}
		if (m_Set != null && (m_LastValue == null || !m_LastValue.Equals(m_CurrentValue)))
		{
			m_Set();
		}
	}

	public void SetAndForceUpdate(T value)
	{
		m_LastValue = m_CurrentValue;
		m_CurrentValue = value;
		if (m_Filter != null)
		{
			m_CurrentValue = m_Filter(m_LastValue, m_CurrentValue);
		}
		if (m_Set != null)
		{
			m_Set();
		}
	}

	public void SetAndDontUpdate(T value)
	{
		m_LastValue = m_CurrentValue;
		m_CurrentValue = value;
		if (m_Filter != null)
		{
			m_CurrentValue = m_Filter(m_LastValue, m_CurrentValue);
		}
	}
}
