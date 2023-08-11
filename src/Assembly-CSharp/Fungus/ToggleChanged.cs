using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Fungus;

[EventHandlerInfo("UI", "Toggle Changed", "The block will execute when the state of the target UI toggle object changes. The state of the toggle is stored in the Toggle State boolean variable.")]
[AddComponentMenu("")]
public class ToggleChanged : EventHandler
{
	[Tooltip("The block will execute when the state of the target UI toggle object changes.")]
	[SerializeField]
	protected Toggle targetToggle;

	[Tooltip("The new state of the UI toggle object is stored in this boolean variable.")]
	[VariableProperty(new Type[] { typeof(BooleanVariable) })]
	[SerializeField]
	protected BooleanVariable toggleState;

	public virtual void Start()
	{
		if ((Object)(object)targetToggle != (Object)null)
		{
			((UnityEvent<bool>)(object)targetToggle.onValueChanged).AddListener((UnityAction<bool>)OnToggleChanged);
		}
	}

	protected virtual void OnToggleChanged(bool state)
	{
		if ((Object)(object)toggleState != (Object)null)
		{
			toggleState.Value = state;
		}
		ExecuteBlock();
	}

	public override string GetSummary()
	{
		if ((Object)(object)targetToggle != (Object)null)
		{
			return ((Object)targetToggle).name;
		}
		return "None";
	}
}
