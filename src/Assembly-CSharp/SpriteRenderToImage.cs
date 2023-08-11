using UnityEngine;
using UnityEngine.UI;

public class SpriteRenderToImage : MonoBehaviour
{
	private Image image;

	private SpriteRenderer sr;

	private void Awake()
	{
		image = ((Component)this).GetComponent<Image>();
		sr = ((Component)this).GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
		if ((Object)(object)sr.sprite != (Object)null && (Object)(object)image.sprite != (Object)(object)sr.sprite)
		{
			image.sprite = sr.sprite;
		}
	}
}
