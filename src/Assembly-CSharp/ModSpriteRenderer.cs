using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ModSpriteRenderer : MonoBehaviour
{
	private SpriteRenderer sr;

	public string SpritePath;

	private void Awake()
	{
		sr = ((Component)this).GetComponent<SpriteRenderer>();
	}

	public void Refresh()
	{
		sr.sprite = ModResources.LoadSprite(SpritePath);
	}

	private void Start()
	{
		Refresh();
	}
}
