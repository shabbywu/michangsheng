using System;
using UnityEngine;

namespace UltimateSurvival.AI;

[Serializable]
public class EntityAnimation
{
	private AIBrain m_Brain;

	private Animator m_Animator;

	public void Initialize(AIBrain brain)
	{
		m_Brain = brain;
		m_Animator = ((Component)m_Brain).GetComponent<Animator>();
	}

	public bool ParameterExists(string paramName)
	{
		if (m_Animator.parameterCount == 0)
		{
			return false;
		}
		bool result = false;
		AnimatorControllerParameter[] parameters = m_Animator.parameters;
		for (int i = 0; i < parameters.Length; i++)
		{
			if (parameters[i].name == paramName)
			{
				result = true;
			}
		}
		return result;
	}

	public void SetTrigger(string paramName)
	{
		if (ParameterExists(paramName))
		{
			m_Animator.SetTrigger(paramName);
		}
	}

	public void ToggleBool(string paramName, bool value)
	{
		if (ParameterExists(paramName))
		{
			m_Animator.SetBool(paramName, value);
		}
	}

	public bool IsBoolToggled(string paramName)
	{
		if (!ParameterExists(paramName))
		{
			Debug.LogError((object)("Parameter with name " + paramName + " does not exist."));
			return false;
		}
		return m_Animator.GetBool(paramName);
	}
}
