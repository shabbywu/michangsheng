using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenImages : MonoBehaviour
{
	public List<string> imageNames = new List<string>();

	[HideInInspector]
	public List<Sprite> sprites = new List<Sprite>();

	private LoadingScreen LS;

	private void Awake()
	{
		LoadSprites();
		LS = ((Component)this).GetComponent<LoadingScreen>();
		LS.LoadingScreenImages = sprites.ToArray();
	}

	public void LoadSprites()
	{
		foreach (string imageName in imageNames)
		{
			Sprite sprite = BackGroundImage.GetSprite(imageName);
			if ((Object)(object)sprite != (Object)null)
			{
				sprites.Add(sprite);
			}
		}
	}
}
