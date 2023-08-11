using System;
using UnityEngine;

namespace UltimateSurvival;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class HideSwitchAttribute : PropertyAttribute
{
	public readonly string m_BasedOnValue;

	public readonly float m_IndentAmount;

	public readonly bool m_ShowOnBool;

	public readonly string m_ShowOnString = "s";

	public readonly int m_ShowOnInt = -1;

	public readonly float m_ShowOnFloat = -1f;

	public readonly Vector3 m_ShowOnVector3 = new Vector3(1f, 1f, 1f);

	public HideSwitchAttribute(string basedOn, bool showOnValue, float indentAmount = 20f)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		m_BasedOnValue = basedOn;
		m_ShowOnBool = showOnValue;
		m_IndentAmount = indentAmount;
	}

	public HideSwitchAttribute(string basedOn, string showOnValue, float indentAmount = 20f)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		m_BasedOnValue = basedOn;
		m_ShowOnString = showOnValue;
		m_IndentAmount = indentAmount;
	}

	public HideSwitchAttribute(string basedOn, int showOnValue, float indentAmount = 20f)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		m_BasedOnValue = basedOn;
		m_ShowOnInt = showOnValue;
		m_IndentAmount = indentAmount;
	}

	public HideSwitchAttribute(string basedOn, float showOnValue, float indentAmount = 20f)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		m_BasedOnValue = basedOn;
		m_ShowOnFloat = showOnValue;
		m_IndentAmount = indentAmount;
	}

	public HideSwitchAttribute(string basedOn, Vector3 showOnValue, float indentAmount = 20f)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		m_BasedOnValue = basedOn;
		m_ShowOnVector3 = showOnValue;
		m_IndentAmount = indentAmount;
	}
}
