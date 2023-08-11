using System;

namespace Fungus;

public class EventHandlerInfoAttribute : Attribute
{
	public string Category { get; set; }

	public string EventHandlerName { get; set; }

	public string HelpText { get; set; }

	public EventHandlerInfoAttribute(string category, string eventHandlerName, string helpText)
	{
		Category = category;
		EventHandlerName = eventHandlerName;
		HelpText = helpText;
	}
}
