using System;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
	private static T m_Instance;

	public static T instance
	{
		get
		{
			//IL_006e: Unknown result type (might be due to invalid IL or missing references)
			if ((Object)(object)m_Instance == (Object)null)
			{
				m_Instance = Object.FindObjectOfType(typeof(T)) as T;
				if ((Object)(object)m_Instance == (Object)null)
				{
					m_Instance = new GameObject("Singleton of " + typeof(T).ToString(), new Type[1] { typeof(T) }).GetComponent<T>();
					m_Instance.Init();
				}
			}
			return m_Instance;
		}
	}

	private void Awake()
	{
		if ((Object)(object)m_Instance == (Object)null)
		{
			m_Instance = this as T;
		}
	}

	public virtual void Init()
	{
	}

	private void OnApplicationQuit()
	{
		m_Instance = null;
	}
}
