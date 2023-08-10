using System;

namespace UltimateSurvival;

public class Attempt
{
	private TryerDelegate m_Tryer;

	private Action m_Listeners;

	public void SetTryer(TryerDelegate tryer)
	{
		m_Tryer = tryer;
	}

	public void AddListener(Action listener)
	{
		m_Listeners = (Action)Delegate.Combine(m_Listeners, listener);
	}

	public void RemoveListener(Action listener)
	{
		m_Listeners = (Action)Delegate.Remove(m_Listeners, listener);
	}

	public bool Try()
	{
		if (m_Tryer == null || m_Tryer())
		{
			if (m_Listeners != null)
			{
				m_Listeners();
			}
			return true;
		}
		return false;
	}
}
public class Attempt<T>
{
	public delegate bool GenericTryerDelegate(T arg);

	private GenericTryerDelegate m_Tryer;

	private Action<T> m_Listeners;

	public void SetTryer(GenericTryerDelegate tryer)
	{
		m_Tryer = tryer;
	}

	public void AddListener(Action<T> listener)
	{
		m_Listeners = (Action<T>)Delegate.Combine(m_Listeners, listener);
	}

	public void RemoveListener(Action<T> listener)
	{
		m_Listeners = (Action<T>)Delegate.Remove(m_Listeners, listener);
	}

	public bool Try(T arg)
	{
		if (m_Tryer != null && m_Tryer(arg))
		{
			if (m_Listeners != null)
			{
				m_Listeners(arg);
			}
			return true;
		}
		return false;
	}
}
public class Attempt<T, V>
{
	public delegate bool GenericTryerDelegate(T arg1, V arg2);

	private GenericTryerDelegate m_Tryer;

	private Action<T, V> m_Listeners;

	public void SetTryer(GenericTryerDelegate tryer)
	{
		m_Tryer = tryer;
	}

	public void AddListener(Action<T, V> listener)
	{
		m_Listeners = (Action<T, V>)Delegate.Combine(m_Listeners, listener);
	}

	public void RemoveListener(Action<T, V> listener)
	{
		m_Listeners = (Action<T, V>)Delegate.Remove(m_Listeners, listener);
	}

	public bool Try(T arg1, V arg2)
	{
		if (m_Tryer != null && m_Tryer(arg1, arg2))
		{
			if (m_Listeners != null)
			{
				m_Listeners(arg1, arg2);
			}
			return true;
		}
		return false;
	}
}
