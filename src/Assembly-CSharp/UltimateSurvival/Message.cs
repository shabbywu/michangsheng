using System;

namespace UltimateSurvival;

public class Message
{
	private Action m_Listeners;

	public void AddListener(Action listener)
	{
		m_Listeners = (Action)Delegate.Combine(m_Listeners, listener);
	}

	public void RemoveListener(Action listener)
	{
		m_Listeners = (Action)Delegate.Remove(m_Listeners, listener);
	}

	public void Send()
	{
		if (m_Listeners != null)
		{
			m_Listeners();
		}
	}
}
public class Message<T>
{
	private Action<T> m_Listeners;

	public void AddListener(Action<T> listener)
	{
		m_Listeners = (Action<T>)Delegate.Combine(m_Listeners, listener);
	}

	public void RemoveListener(Action<T> callback)
	{
		m_Listeners = (Action<T>)Delegate.Remove(m_Listeners, callback);
	}

	public void Send(T message)
	{
		if (m_Listeners != null)
		{
			m_Listeners(message);
		}
	}
}
