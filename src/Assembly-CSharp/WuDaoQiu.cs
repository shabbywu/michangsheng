using UnityEngine;
using UnityEngine.UI;

public class WuDaoQiu : MonoBehaviour
{
	[SerializeField]
	private Image wuDaoQiuImage;

	[SerializeField]
	private Text wuDaoQiuLevel;

	public void Init(Sprite sprite, int level)
	{
		((Component)this).gameObject.SetActive(true);
		wuDaoQiuImage.sprite = sprite;
		wuDaoQiuLevel.text = level.ToString();
	}
}
