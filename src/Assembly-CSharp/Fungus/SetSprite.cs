using System.Collections.Generic;
using UnityEngine;

namespace Fungus;

[CommandInfo("Sprite", "Set Sprite", "Changes the sprite property of a list of Sprite Renderers.", 0)]
[AddComponentMenu("")]
public class SetSprite : Command
{
	[Tooltip("List of sprites to set the sprite property on")]
	[SerializeField]
	protected List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

	[Tooltip("The sprite set on the target sprite renderers")]
	[SerializeField]
	protected Sprite sprite;

	public override void OnEnter()
	{
		for (int i = 0; i < spriteRenderers.Count; i++)
		{
			spriteRenderers[i].sprite = sprite;
		}
		Continue();
	}

	public override string GetSummary()
	{
		string text = "";
		for (int i = 0; i < spriteRenderers.Count; i++)
		{
			SpriteRenderer val = spriteRenderers[i];
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
		if (propertyName == "spriteRenderers")
		{
			return true;
		}
		return false;
	}

	public override void OnCommandAdded(Block parentBlock)
	{
		spriteRenderers.Add(null);
	}
}
