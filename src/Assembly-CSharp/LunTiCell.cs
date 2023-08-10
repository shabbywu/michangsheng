using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LunTiCell : MonoBehaviour
{
	private bool state;

	[SerializeField]
	private Image selectImage;

	[SerializeField]
	private Image unSelectImage;

	[SerializeField]
	private Text lunTiName;

	public int lunTiId;

	private UnityAction<int> selectAction;

	private UnityAction<int> unSelectAction;

	public void InitLunTiCell(Sprite selectSprite, Sprite unselectSprite, int lunTiId, string lunTiName, UnityAction<int> selectAction, UnityAction<int> unSelectAction)
	{
		selectImage.sprite = selectSprite;
		unSelectImage.sprite = unselectSprite;
		this.lunTiId = lunTiId;
		this.lunTiName.text = lunTiName;
		this.selectAction = selectAction;
		this.unSelectAction = unSelectAction;
	}

	public void MouseUp()
	{
		state = !state;
		((Component)selectImage).gameObject.SetActive(state);
		((Component)unSelectImage).gameObject.SetActive(!state);
		if (state)
		{
			selectAction.Invoke(lunTiId);
		}
		else
		{
			unSelectAction.Invoke(lunTiId);
		}
	}
}
