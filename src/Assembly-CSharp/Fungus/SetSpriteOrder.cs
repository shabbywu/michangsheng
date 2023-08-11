using System.Collections.Generic;
using UnityEngine;

namespace Fungus;

[CommandInfo("Sprite", "Set Sprite Order", "Controls the render order of sprites by setting the Order In Layer property of a list of sprites.", 0)]
[AddComponentMenu("")]
public class SetSpriteOrder : Command
{
	[Tooltip("List of sprites to set the order in layer property on")]
	[SerializeField]
	protected List<SpriteRenderer> targetSprites = new List<SpriteRenderer>();

	[Tooltip("The order in layer value to set on the target sprites")]
	[SerializeField]
	protected IntegerData orderInLayer;

	public override void OnEnter()
	{
		for (int i = 0; i < targetSprites.Count; i++)
		{
			((Renderer)targetSprites[i]).sortingOrder = orderInLayer;
		}
		Continue();
	}

	public override string GetSummary()
	{
		string text = "";
		for (int i = 0; i < targetSprites.Count; i++)
		{
			SpriteRenderer val = targetSprites[i];
			if (!((Object)(object)val == (Object)null))
			{
				if (text.Length > 0)
				{
					text += ", ";
				}
				text += ((Object)val).name;
			}
		}
		if (text.Length == 0)
		{
			return "Error: No cursor sprite selected";
		}
		return text + " = " + orderInLayer.Value;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool IsReorderableArray(string propertyName)
	{
		if (propertyName == "targetSprites")
		{
			return true;
		}
		return false;
	}

	public override void OnCommandAdded(Block parentBlock)
	{
		targetSprites.Add(null);
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)orderInLayer.integerRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}
}
