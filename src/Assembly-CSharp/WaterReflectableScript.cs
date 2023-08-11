using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class WaterReflectableScript : MonoBehaviour
{
	[Header("Reflect properties")]
	public Vector3 localPosition = new Vector3(0f, -0.25f, 0f);

	public Vector3 localRotation = new Vector3(0f, 0f, -180f);

	[Tooltip("Optionnal: force the reflected sprite. If null it will be a copy of the source.")]
	public Sprite sprite;

	public string spriteLayer = "Default";

	public int spriteLayerOrder = -5;

	private SpriteRenderer spriteSource;

	private SpriteRenderer spriteRenderer;

	private void Awake()
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		GameObject val = new GameObject("Water Reflect");
		val.transform.parent = ((Component)this).transform;
		val.transform.localPosition = localPosition;
		val.transform.localRotation = Quaternion.Euler(localRotation);
		val.transform.localScale = new Vector3(val.transform.localScale.x, val.transform.localScale.y, val.transform.localScale.z);
		spriteRenderer = val.AddComponent<SpriteRenderer>();
		((Renderer)spriteRenderer).sortingLayerName = spriteLayer;
		((Renderer)spriteRenderer).sortingOrder = spriteLayerOrder;
		spriteSource = ((Component)this).GetComponent<SpriteRenderer>();
	}

	private void OnDestroy()
	{
		if ((Object)(object)spriteRenderer != (Object)null)
		{
			Object.Destroy((Object)(object)((Component)spriteRenderer).gameObject);
		}
	}

	private void LateUpdate()
	{
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)spriteSource != (Object)null)
		{
			if ((Object)(object)sprite == (Object)null)
			{
				spriteRenderer.sprite = spriteSource.sprite;
			}
			else
			{
				spriteRenderer.sprite = sprite;
			}
			spriteRenderer.flipX = spriteSource.flipX;
			spriteRenderer.flipY = spriteSource.flipY;
			spriteRenderer.color = spriteSource.color;
		}
	}
}
