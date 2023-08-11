using System;
using UnityEngine;

namespace UltimateSurvival.InputSystem;

[Serializable]
public class Axis
{
	[SerializeField]
	private string m_AxisName;

	[SerializeField]
	private bool m_Normalize;

	[SerializeField]
	private ET.StandaloneAxisType m_AxisType;

	[SerializeField]
	private string m_UnityAxisName;

	[SerializeField]
	private KeyCode m_PositiveKey;

	[SerializeField]
	private KeyCode m_NegativeKey;

	public string AxisName
	{
		get
		{
			return m_AxisName;
		}
		set
		{
			m_AxisName = value;
		}
	}

	public ET.StandaloneAxisType AxisType => m_AxisType;

	public string UnityAxisName => m_UnityAxisName;

	public KeyCode NegativeKey => m_NegativeKey;

	public KeyCode PositiveKey => m_PositiveKey;

	public bool Normalize => m_Normalize;

	public Axis(string name, ET.StandaloneAxisType axisType)
	{
		m_AxisName = name;
		m_AxisType = axisType;
	}

	public Axis(string name, ET.StandaloneAxisType axisType, Joystick joystick)
	{
		m_AxisName = name;
		m_AxisType = axisType;
	}

	public Axis(string name, ET.StandaloneAxisType axisType, string unityAxisName)
	{
		m_AxisName = name;
		m_AxisType = axisType;
		m_UnityAxisName = unityAxisName;
	}

	public Axis(string name, ET.StandaloneAxisType axisType, KeyCode positiveKey, KeyCode negativeKey, string unityAxisName)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		m_AxisName = name;
		m_AxisType = axisType;
		m_PositiveKey = positiveKey;
		m_NegativeKey = negativeKey;
		m_UnityAxisName = unityAxisName;
	}
}
