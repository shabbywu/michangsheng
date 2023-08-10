using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CySubmitBtn : MonoBehaviour
{
	public Image bg;

	public Text text;

	public BtnCell btnCell;

	public void Init(Sprite sprite, string name, UnityAction unityAction)
	{
		bg.sprite = sprite;
		text.text = name;
		if (unityAction == null)
		{
			btnCell.Disable = true;
		}
		else
		{
			btnCell.mouseUp.AddListener(unityAction);
		}
		((Component)this).gameObject.SetActive(true);
	}
}
