using System;
using UnityEngine;

// Token: 0x020005A3 RID: 1443
public class UIpopulTooll : MonoBehaviour
{
	// Token: 0x0600245E RID: 9310 RVA: 0x0001D43F File Offset: 0x0001B63F
	private void Start()
	{
		this.popupList = base.GetComponent<UIPopupList>();
	}

	// Token: 0x0600245F RID: 9311 RVA: 0x00128420 File Offset: 0x00126620
	public void OnCheng()
	{
		this.popupList.Close();
		if (this.uiBase.activeSelf)
		{
			this.uiBase.SetActive(false);
			return;
		}
		this.uiBase.SetActive(true);
		foreach (object obj in this.TextGrid.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.gameObject.activeSelf)
			{
				Object.Destroy(transform.gameObject);
			}
		}
		int num = 0;
		int i = 0;
		int count = this.popupList.items.Count;
		while (i < count)
		{
			string text = this.popupList.items[i];
			UILabel uilabel = Object.Instantiate<UILabel>(this.iLabelBase);
			uilabel.gameObject.SetActive(true);
			uilabel.transform.parent = this.TextGrid.transform;
			uilabel.transform.localScale = Vector3.one;
			uilabel.text = text;
			UIEventListener uieventListener = UIEventListener.Get(uilabel.gameObject);
			uieventListener.onHover = new UIEventListener.BoolDelegate(this.popupList.OnItemHover);
			uieventListener.onPress = new UIEventListener.BoolDelegate(this.ItemPress);
			uieventListener.onClick = new UIEventListener.VoidDelegate(this.ItemClicek);
			uieventListener.parameter = text;
			num += this.WightSize;
			i++;
		}
		num += this.StartSize;
		if (this.ttypeA == UIpopulTooll.TType.wight)
		{
			this.i2DSprite.width = num;
		}
		else
		{
			this.i2DSprite.height = num;
		}
		this.TextGrid.repositionNow = true;
	}

	// Token: 0x06002460 RID: 9312 RVA: 0x0001D44D File Offset: 0x0001B64D
	public void ItemPress(GameObject go, bool isslelct)
	{
		this.popupList.OnItemPress(go, isslelct);
		this.Close();
	}

	// Token: 0x06002461 RID: 9313 RVA: 0x0001D462 File Offset: 0x0001B662
	public void ItemClicek(GameObject go)
	{
		this.Close();
	}

	// Token: 0x06002462 RID: 9314 RVA: 0x0001D46A File Offset: 0x0001B66A
	public void Close()
	{
		this.uiBase.SetActive(false);
		base.GetComponent<UIToggle>().value = false;
	}

	// Token: 0x04001F47 RID: 8007
	public GameObject uiBase;

	// Token: 0x04001F48 RID: 8008
	public UIGrid TextGrid;

	// Token: 0x04001F49 RID: 8009
	public UI2DSprite i2DSprite;

	// Token: 0x04001F4A RID: 8010
	public UILabel iLabelBase;

	// Token: 0x04001F4B RID: 8011
	public int WightSize;

	// Token: 0x04001F4C RID: 8012
	public int StartSize;

	// Token: 0x04001F4D RID: 8013
	public UIpopulTooll.TType ttypeA;

	// Token: 0x04001F4E RID: 8014
	private UIPopupList popupList;

	// Token: 0x020005A4 RID: 1444
	public enum TType
	{
		// Token: 0x04001F50 RID: 8016
		wight,
		// Token: 0x04001F51 RID: 8017
		hight
	}
}
