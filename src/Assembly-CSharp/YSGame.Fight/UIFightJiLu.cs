using UnityEngine;
using UnityEngine.UI;

namespace YSGame.Fight;

public class UIFightJiLu : MonoBehaviour
{
	public Text JiLuText;

	public RectTransform ContentRT;

	public GameObject ScaleObj;

	private bool isShow;

	public void Clear()
	{
		JiLuText.text = "";
	}

	public void AddText(string text)
	{
		Text jiLuText = JiLuText;
		jiLuText.text = jiLuText.text + text + "\n";
	}

	public void Show()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		isShow = true;
		float y = ContentRT.sizeDelta.y;
		Transform parent = ((Transform)ContentRT).parent;
		float num = Mathf.Max(0f, y - ((RectTransform)((parent is RectTransform) ? parent : null)).sizeDelta.y);
		ContentRT.anchoredPosition = new Vector2(ContentRT.anchoredPosition.x, num);
		ScaleObj.SetActive(true);
	}

	public void Hide()
	{
		isShow = false;
		ScaleObj.SetActive(false);
	}

	public void ToggleOpen()
	{
		if (isShow)
		{
			Hide();
		}
		else
		{
			Show();
		}
	}
}
