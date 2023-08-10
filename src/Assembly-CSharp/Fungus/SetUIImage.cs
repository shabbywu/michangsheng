using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus;

[CommandInfo("UI", "Set UI Image", "Changes the Image property of a list of UI Images.", 0)]
[AddComponentMenu("")]
public class SetUIImage : Command
{
	[Tooltip("List of UI Images to set the source image property on")]
	[SerializeField]
	protected List<Image> images = new List<Image>();

	[Tooltip("The sprite set on the source image property")]
	[SerializeField]
	protected Sprite sprite;

	public override void OnEnter()
	{
		for (int i = 0; i < images.Count; i++)
		{
			images[i].sprite = sprite;
		}
		Continue();
	}

	public override string GetSummary()
	{
		string text = "";
		for (int i = 0; i < images.Count; i++)
		{
			Image val = images[i];
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
			return "Error: No sprite selected";
		}
		return text + " = " + sprite;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool IsReorderableArray(string propertyName)
	{
		if (propertyName == "images")
		{
			return true;
		}
		return false;
	}

	public override void OnCommandAdded(Block parentBlock)
	{
		images.Add(null);
	}
}
