using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class TextMeshPlugin : MonoBehaviour
{
	public enum TextEffect
	{
		None,
		Shadow,
		Outline,
		SingleOutline
	}

	public TextEffect textEffect = TextEffect.Outline;

	public float outlineOffset = 0.05f;

	public float singleOutlineOffset = 1.1f;

	public Vector2 shadowPosition;

	public Color effectColor = Color.black;

	private TextMesh thisComponent;

	private void Start()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		thisComponent = ((Component)this).GetComponent<TextMesh>();
		if (textEffect == TextEffect.Outline)
		{
			thisComponent.AddOutline(effectColor, outlineOffset);
		}
		else if (textEffect == TextEffect.Shadow)
		{
			thisComponent.AddShadow(effectColor, shadowPosition);
		}
		else if (textEffect == TextEffect.SingleOutline)
		{
			thisComponent.AddSingleOutline(effectColor, singleOutlineOffset);
		}
	}
}
