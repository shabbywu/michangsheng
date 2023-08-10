using System;

namespace Fungus;

public class VariableInfoAttribute : Attribute
{
	public string Category { get; set; }

	public string VariableType { get; set; }

	public int Order { get; set; }

	public VariableInfoAttribute(string category, string variableType, int order = 0)
	{
		Category = category;
		VariableType = variableType;
		Order = order;
	}
}
