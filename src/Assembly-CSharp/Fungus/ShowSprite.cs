using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

[CommandInfo("Sprite", "Show Sprite", "Makes a sprite visible / invisible by setting the color alpha.", 0)]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class ShowSprite : Command
{
	[Tooltip("Sprite object to be made visible / invisible")]
	[SerializeField]
	protected SpriteRenderer spriteRenderer;

	[Tooltip("Make the sprite visible or invisible")]
	[SerializeField]
	protected BooleanData _visible = new BooleanData(v: false);

	[Tooltip("Affect the visibility of child sprites")]
	[SerializeField]
	protected bool affectChildren = true;

	[HideInInspector]
	[FormerlySerializedAs("visible")]
	public bool visibleOLD;

	protected virtual void SetSpriteAlpha(SpriteRenderer renderer, bool visible)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		Color color = renderer.color;
		color.a = (visible ? 1f : 0f);
		renderer.color = color;
	}

	public override void OnEnter()
	{
		if ((Object)(object)spriteRenderer != (Object)null)
		{
			if (affectChildren)
			{
				SpriteRenderer[] componentsInChildren = ((Component)spriteRenderer).gameObject.GetComponentsInChildren<SpriteRenderer>();
				foreach (SpriteRenderer renderer in componentsInChildren)
				{
					SetSpriteAlpha(renderer, _visible.Value);
				}
			}
			else
			{
				SetSpriteAlpha(spriteRenderer, _visible.Value);
			}
		}
		Continue();
	}

	public override string GetSummary()
	{
		if ((Object)(object)spriteRenderer == (Object)null)
		{
			return "Error: No sprite renderer selected";
		}
		return ((Object)spriteRenderer).name + " to " + (_visible.Value ? "visible" : "invisible");
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)221, (byte)184, (byte)169, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)_visible.booleanRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}

	protected virtual void OnEnable()
	{
		if (visibleOLD)
		{
			_visible.Value = visibleOLD;
			visibleOLD = false;
		}
	}
}
