using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ModImage : MonoBehaviour
{
	private Image image;

	public string SpritePath;

	private void Awake()
	{
		image = ((Component)this).GetComponent<Image>();
	}

	public void Refresh()
	{
		image.sprite = ModResources.LoadSprite(SpritePath);
	}

	private void Start()
	{
		Refresh();
	}
}
