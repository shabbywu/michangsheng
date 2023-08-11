using UnityEngine;

public class AllMapNodeClick : MonoBehaviour
{
	public SpriteRenderer sprite;

	public Sprite StartSprite;

	public Sprite OnHoverSprite;

	protected virtual void OnMouseDown()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		if (((Component)this).gameObject.GetComponent<MapComponent>().CanClick())
		{
			((Component)this).transform.localScale = new Vector3(0.42f, 0.42f, 0.42f);
		}
	}

	protected virtual void OnMouseUp()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
	}

	protected virtual void OnMouseEnter()
	{
		if ((Object)(object)OnHoverSprite != (Object)null)
		{
			sprite.sprite = OnHoverSprite;
		}
	}

	protected virtual void OnMouseExit()
	{
		if ((Object)(object)StartSprite != (Object)null)
		{
			sprite.sprite = StartSprite;
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
