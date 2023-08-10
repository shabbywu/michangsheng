using System;
using UnityEngine;

namespace Fungus;

public class VariablePropertyAttribute : PropertyAttribute
{
	public string defaultText = "<None>";

	public Type[] VariableTypes { get; set; }

	public VariablePropertyAttribute(params Type[] variableTypes)
	{
		VariableTypes = variableTypes;
	}

	public VariablePropertyAttribute(string defaultText, params Type[] variableTypes)
	{
		this.defaultText = defaultText;
		VariableTypes = variableTypes;
	}
}
