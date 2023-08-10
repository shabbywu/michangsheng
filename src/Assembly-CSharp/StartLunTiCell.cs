using UnityEngine;
using UnityEngine.UI;

public class StartLunTiCell : MonoBehaviour
{
	[SerializeField]
	private Image lunTiName;

	public int lunTiId;

	public Transform wuDaoParent;

	public Image finshIBg;

	public Image finshImage;

	public void Init(Sprite sprite, int id)
	{
		((Component)this).gameObject.SetActive(true);
		lunTiName.sprite = sprite;
		lunTiId = id;
	}
}
