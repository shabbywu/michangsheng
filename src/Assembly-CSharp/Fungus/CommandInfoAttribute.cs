using System;

namespace Fungus;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class CommandInfoAttribute : Attribute
{
	public string Category { get; set; }

	public string CommandName { get; set; }

	public string HelpText { get; set; }

	public int Priority { get; set; }

	public CommandInfoAttribute(string category, string commandName, string helpText, int priority = 0)
	{
		Category = category;
		CommandName = commandName;
		HelpText = helpText;
		Priority = priority;
	}
}
