using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus;

[CommandInfo("UI", "Set Interactable", "Set the interactable state of selectable objects.", 0)]
public class SetInteractable : Command
{
	[Tooltip("List of objects to be affected by the command")]
	[SerializeField]
	protected List<GameObject> targetObjects = new List<GameObject>();

	[Tooltip("Controls if the selectable UI object be interactable or not")]
	[SerializeField]
	protected BooleanData interactableState = new BooleanData(v: true);

	public override void OnEnter()
	{
		if (targetObjects.Count == 0)
		{
			Continue();
			return;
		}
		for (int i = 0; i < targetObjects.Count; i++)
		{
			Selectable[] components = targetObjects[i].GetComponents<Selectable>();
			for (int j = 0; j < components.Length; j++)
			{
				components[j].interactable = interactableState.Value;
			}
		}
		Continue();
	}

	public override string GetSummary()
	{
		if (targetObjects.Count == 0)
		{
			return "Error: No targetObjects selected";
		}
		if (targetObjects.Count == 1)
		{
			if ((Object)(object)targetObjects[0] == (Object)null)
			{
				return "Error: No targetObjects selected";
			}
			return ((Object)targetObjects[0]).name + " = " + interactableState.Value;
		}
		string text = "";
		for (int i = 0; i < targetObjects.Count; i++)
		{
			GameObject val = targetObjects[i];
			if (!((Object)(object)val == (Object)null))
			{
				text = ((!(text == "")) ? (text + ", " + ((Object)val).name) : (text + ((Object)val).name));
			}
		}
		return text + " = " + interactableState.Value;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)180, (byte)250, (byte)250, byte.MaxValue));
	}

	public override void OnCommandAdded(Block parentBlock)
	{
		targetObjects.Add(null);
	}

	public override bool IsReorderableArray(string propertyName)
	{
		if (propertyName == "targetObjects")
		{
			return true;
		}
		return false;
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)interactableState.booleanRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}
}
