using System;
using UnityEngine;

namespace Fungus;

[AddComponentMenu("")]
public abstract class TagFilteredEventHandler : EventHandler
{
	[Tooltip("Only fire the event if one of the tags match. Empty means any will fire.")]
	[SerializeField]
	protected string[] tagFilter;

	protected void ProcessTagFilter(string tagOnOther)
	{
		if (tagFilter.Length == 0)
		{
			ExecuteBlock();
		}
		else if (Array.IndexOf(tagFilter, tagOnOther) != -1)
		{
			ExecuteBlock();
		}
	}
}
