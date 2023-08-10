using UnityEngine;
using UnityEngine.UI;

public class setImageSprite : MonoBehaviour
{
	public Image image;

	public SpriteRenderer sprite;

	private void Start()
	{
	}

	private void Update()
	{
		if ((Object)(object)image.sprite != (Object)(object)sprite.sprite)
		{
			image.sprite = sprite.sprite;
		}
	}
}
