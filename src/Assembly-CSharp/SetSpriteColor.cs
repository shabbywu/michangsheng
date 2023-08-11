using UnityEngine;

public class SetSpriteColor : MonoBehaviour
{
	public Color Namel;

	public Color onHover;

	public SpriteRenderer sprite;

	private void Start()
	{
	}

	protected virtual void OnMouseEnter()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		sprite.color = onHover;
	}

	protected virtual void OnMouseExit()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		sprite.color = Namel;
	}
}
