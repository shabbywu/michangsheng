using System;
using UnityEngine;

namespace UltimateSurvival;

public class Activity
{
	private TryerDelegate m_StartTryers;

	private TryerDelegate m_StopTryers;

	private Action m_OnStart;

	private Action m_OnStop;

	public bool Active { get; private set; }

	public void AddStartTryer(TryerDelegate tryer)
	{
		m_StartTryers = (TryerDelegate)Delegate.Combine(m_StartTryers, tryer);
	}

	public void AddStopTryer(TryerDelegate tryer)
	{
		m_StopTryers = (TryerDelegate)Delegate.Combine(m_StopTryers, tryer);
	}

	public void AddStartListener(Action listener)
	{
		m_OnStart = (Action)Delegate.Combine(m_OnStart, listener);
	}

	public void AddStopListener(Action listener)
	{
		m_OnStop = (Action)Delegate.Combine(m_OnStop, listener);
	}

	public void ForceStart()
	{
		if (!Active)
		{
			Active = true;
			if (m_OnStart != null)
			{
				m_OnStart();
			}
		}
	}

	public bool TryStart()
	{
		if (Active)
		{
			return false;
		}
		if (m_StartTryers != null)
		{
			bool num = CallStartApprovers();
			if (num)
			{
				Active = true;
			}
			if (num && m_OnStart != null)
			{
				m_OnStart();
			}
			return num;
		}
		Debug.LogWarning((object)"[Activity] - You tried to start an activity which has no tryer (if no one checks if the activity can start, it won't start).");
		return false;
	}

	public bool TryStop()
	{
		if (!Active)
		{
			return false;
		}
		if (m_StopTryers != null && CallStopApprovers())
		{
			Active = false;
			if (m_OnStop != null)
			{
				m_OnStop();
			}
			return true;
		}
		return false;
	}

	public void ForceStop()
	{
		if (Active)
		{
			Active = false;
			if (m_OnStop != null)
			{
				m_OnStop();
			}
		}
	}

	private bool CallStartApprovers()
	{
		Delegate[] invocationList = m_StartTryers.GetInvocationList();
		for (int i = 0; i < invocationList.Length; i++)
		{
			if (!(bool)invocationList[i].DynamicInvoke())
			{
				return false;
			}
		}
		return true;
	}

	private bool CallStopApprovers()
	{
		Delegate[] invocationList = m_StopTryers.GetInvocationList();
		for (int i = 0; i < invocationList.Length; i++)
		{
			if (!(bool)invocationList[i].DynamicInvoke())
			{
				return false;
			}
		}
		return true;
	}
}
