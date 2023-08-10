using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityString;

[TaskCategory("Basic/String")]
[TaskDescription("Stores a string with the specified format.")]
public class Format : Action
{
	[Tooltip("The format of the string")]
	public SharedString format;

	[Tooltip("Any variables to appear in the string")]
	public SharedGenericVariable[] variables;

	[Tooltip("The result of the format")]
	[RequiredField]
	public SharedString storeResult;

	private object[] variableValues;

	public override void OnAwake()
	{
		variableValues = new object[variables.Length];
	}

	public override TaskStatus OnUpdate()
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < variableValues.Length; i++)
		{
			variableValues[i] = ((SharedVariable<GenericVariable>)(object)variables[i]).Value.value.GetValue();
		}
		try
		{
			((SharedVariable<string>)storeResult).Value = string.Format(((SharedVariable<string>)format).Value, variableValues);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex.Message);
			return (TaskStatus)1;
		}
		return (TaskStatus)2;
	}

	public override void OnReset()
	{
		format = "";
		variables = null;
		storeResult = null;
	}
}
