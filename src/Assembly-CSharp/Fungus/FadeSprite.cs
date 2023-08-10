using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

[CommandInfo("Sprite", "Fade Sprite", "Fades a sprite to a target color over a period of time.", 0)]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class FadeSprite : Command
{
	[Tooltip("Sprite object to be faded")]
	[SerializeField]
	protected SpriteRenderer spriteRenderer;

	[Tooltip("Length of time to perform the fade")]
	[SerializeField]
	protected FloatData _duration = new FloatData(1f);

	[Tooltip("Target color to fade to. To only fade transparency level, set the color to white and set the alpha to required transparency.")]
	[SerializeField]
	protected ColorData _targetColor = new ColorData(Color.white);

	[Tooltip("Wait until the fade has finished before executing the next command")]
	[SerializeField]
	protected bool waitUntilFinished = true;

	[HideInInspector]
	[FormerlySerializedAs("duration")]
	public float durationOLD;

	[HideInInspector]
	[FormerlySerializedAs("targetColor")]
	public Color targetColorOLD;

	public override void OnEnter()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)spriteRenderer == (Object)null)
		{
			Continue();
			return;
		}
		SpriteFader.FadeSprite(spriteRenderer, _targetColor.Value, _duration.Value, Vector2.zero, delegate
		{
			if (waitUntilFinished)
			{
				Continue();
			}
		});
		if (!waitUntilFinished)
		{
			Continue();
		}
	}

	public override string GetSummary()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)spriteRenderer == (Object)null)
		{
			return "Error: No sprite renderer selected";
		}
		string name = ((Object)spriteRenderer).name;
		Color value = _targetColor.Value;
		return name + " to " + ((object)(Color)(ref value)).ToString();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)221, (byte)184, (byte)169, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)_duration.floatRef == (Object)(object)variable) && !((Object)(object)_targetColor.colorRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}

	protected virtual void OnEnable()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		if (durationOLD != 0f)
		{
			_duration.Value = durationOLD;
			durationOLD = 0f;
		}
		if (targetColorOLD != default(Color))
		{
			_targetColor.Value = targetColorOLD;
			targetColorOLD = default(Color);
		}
	}
}
