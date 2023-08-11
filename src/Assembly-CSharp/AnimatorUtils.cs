using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AnimatorUtils : MonoBehaviour
{
	public bool iscomplete;

	private SpriteRenderer spriteRenderer;

	private Image image;

	public UnityAction completeCallBack;

	private void Awake()
	{
		spriteRenderer = ((Component)this).GetComponent<SpriteRenderer>();
		image = ((Component)this).GetComponent<Image>();
	}

	private void Update()
	{
		if (iscomplete)
		{
			completeCallBack.Invoke();
			Object.Destroy((Object)(object)((Component)this).gameObject);
		}
		else
		{
			image.sprite = spriteRenderer.sprite;
		}
	}
}
