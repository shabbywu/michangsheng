using UnityEngine;

public class CyUIIconShow : UIIconShow
{
	[SerializeField]
	private GameObject hasTiJiaoImage;

	[SerializeField]
	private GameObject hasGetImage;

	public void Init()
	{
		hasTiJiaoImage.SetActive(false);
		hasGetImage.SetActive(false);
		((Component)((Component)this).transform.parent).gameObject.SetActive(true);
	}

	public void ShowHasTiJiao()
	{
		hasTiJiaoImage.SetActive(true);
	}

	public void ShowHasGet()
	{
		hasGetImage.SetActive(true);
	}
}
