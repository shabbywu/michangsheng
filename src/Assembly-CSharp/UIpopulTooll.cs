using System;
using UnityEngine;

// Token: 0x020003F4 RID: 1012
public class UIpopulTooll : MonoBehaviour
{
	// Token: 0x060020AC RID: 8364 RVA: 0x000E61CE File Offset: 0x000E43CE
	private void Start()
	{
		this.popupList = base.GetComponent<UIPopupList>();
	}

	// Token: 0x060020AD RID: 8365 RVA: 0x000E61DC File Offset: 0x000E43DC
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

	// Token: 0x060020AE RID: 8366 RVA: 0x000E638C File Offset: 0x000E458C
	public void ItemPress(GameObject go, bool isslelct)
	{
		this.popupList.OnItemPress(go, isslelct);
		this.Close();
	}

	// Token: 0x060020AF RID: 8367 RVA: 0x000E63A1 File Offset: 0x000E45A1
	public void ItemClicek(GameObject go)
	{
		this.Close();
	}

	// Token: 0x060020B0 RID: 8368 RVA: 0x000E63A9 File Offset: 0x000E45A9
	public void Close()
	{
		this.uiBase.SetActive(false);
		base.GetComponent<UIToggle>().value = false;
	}

	// Token: 0x04001A8E RID: 6798
	public GameObject uiBase;

	// Token: 0x04001A8F RID: 6799
	public UIGrid TextGrid;

	// Token: 0x04001A90 RID: 6800
	public UI2DSprite i2DSprite;

	// Token: 0x04001A91 RID: 6801
	public UILabel iLabelBase;

	// Token: 0x04001A92 RID: 6802
	public int WightSize;

	// Token: 0x04001A93 RID: 6803
	public int StartSize;

	// Token: 0x04001A94 RID: 6804
	public UIpopulTooll.TType ttypeA;

	// Token: 0x04001A95 RID: 6805
	private UIPopupList popupList;

	// Token: 0x02001393 RID: 5011
	public enum TType
	{
		// Token: 0x040068CB RID: 26827
		wight,
		// Token: 0x040068CC RID: 26828
		hight
	}
}
