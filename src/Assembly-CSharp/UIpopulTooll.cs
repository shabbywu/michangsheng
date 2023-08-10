using UnityEngine;

public class UIpopulTooll : MonoBehaviour
{
	public enum TType
	{
		wight,
		hight
	}

	public GameObject uiBase;

	public UIGrid TextGrid;

	public UI2DSprite i2DSprite;

	public UILabel iLabelBase;

	public int WightSize;

	public int StartSize;

	public TType ttypeA;

	private UIPopupList popupList;

	private void Start()
	{
		popupList = ((Component)this).GetComponent<UIPopupList>();
	}

	public void OnCheng()
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Expected O, but got Unknown
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		popupList.Close();
		if (uiBase.activeSelf)
		{
			uiBase.SetActive(false);
			return;
		}
		uiBase.SetActive(true);
		foreach (Transform item in ((Component)TextGrid).transform)
		{
			Transform val = item;
			if (((Component)val).gameObject.activeSelf)
			{
				Object.Destroy((Object)(object)((Component)val).gameObject);
			}
		}
		int num = 0;
		int i = 0;
		for (int count = popupList.items.Count; i < count; i++)
		{
			string text = popupList.items[i];
			UILabel uILabel = Object.Instantiate<UILabel>(iLabelBase);
			((Component)uILabel).gameObject.SetActive(true);
			((Component)uILabel).transform.parent = ((Component)TextGrid).transform;
			((Component)uILabel).transform.localScale = Vector3.one;
			uILabel.text = text;
			UIEventListener uIEventListener = UIEventListener.Get(((Component)uILabel).gameObject);
			uIEventListener.onHover = popupList.OnItemHover;
			uIEventListener.onPress = ItemPress;
			uIEventListener.onClick = ItemClicek;
			uIEventListener.parameter = text;
			num += WightSize;
		}
		num += StartSize;
		if (ttypeA == TType.wight)
		{
			i2DSprite.width = num;
		}
		else
		{
			i2DSprite.height = num;
		}
		TextGrid.repositionNow = true;
	}

	public void ItemPress(GameObject go, bool isslelct)
	{
		popupList.OnItemPress(go, isslelct);
		Close();
	}

	public void ItemClicek(GameObject go)
	{
		Close();
	}

	public void Close()
	{
		uiBase.SetActive(false);
		((Component)this).GetComponent<UIToggle>().value = false;
	}
}
