using System;
using UnityEngine;

namespace UltimateSurvival.InputSystem;

[Serializable]
public class Button
{
	[SerializeField]
	private string m_ButtonName;

	[SerializeField]
	private KeyCode m_Key;

	public string Name
	{
		get
		{
			return m_ButtonName;
		}
		set
		{
			m_ButtonName = value;
		}
	}

	public KeyCode Key => m_Key;

	public Button(string name)
	{
		m_ButtonName = name;
	}

	public Button(string name, KeyCode key)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		m_ButtonName = name;
		m_Key = key;
	}

	public Button(string name, ButtonHandler handler)
	{
		m_ButtonName = name;
	}
}
