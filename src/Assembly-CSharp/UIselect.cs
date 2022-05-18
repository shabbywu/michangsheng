using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005EB RID: 1515
public class UIselect : MonoBehaviour
{
	// Token: 0x170002D2 RID: 722
	// (get) Token: 0x06002606 RID: 9734 RVA: 0x0001E612 File Offset: 0x0001C812
	public int NowIndex
	{
		get
		{
			return this.nowIndex;
		}
	}

	// Token: 0x06002607 RID: 9735 RVA: 0x0001E61A File Offset: 0x0001C81A
	private void Start()
	{
		this.left.onClick.Add(this.leftEvent);
		this.right.onClick.Add(this.rightEvent);
	}

	// Token: 0x06002608 RID: 9736 RVA: 0x0001E648 File Offset: 0x0001C848
	public void setIndex(int index)
	{
		this.nowIndex = index;
		this.label.text = this.list[index];
	}

	// Token: 0x06002609 RID: 9737 RVA: 0x0001E668 File Offset: 0x0001C868
	public void setVoiceLeft()
	{
		this.clickLeft();
		this.setVoidSprite();
	}

	// Token: 0x0600260A RID: 9738 RVA: 0x0012D240 File Offset: 0x0012B440
	public void setVoidSprite()
	{
		Component component = base.transform.Find("percent");
		int num = 0;
		foreach (object obj in component.transform)
		{
			Transform transform = (Transform)obj;
			if (num < this.nowIndex)
			{
				transform.GetComponent<UISprite>().spriteName = "tiaozheng";
			}
			else
			{
				transform.GetComponent<UISprite>().spriteName = "weixuanzhong";
			}
			num++;
		}
	}

	// Token: 0x0600260B RID: 9739 RVA: 0x0001E676 File Offset: 0x0001C876
	public void setVoiceRight()
	{
		this.clickRight();
		this.setVoidSprite();
	}

	// Token: 0x0600260C RID: 9740 RVA: 0x0012D2D4 File Offset: 0x0012B4D4
	public void clickLeft()
	{
		this.nowIndex--;
		if (this.nowIndex < 0)
		{
			this.nowIndex = this.list.Count - 1;
		}
		this.label.text = this.list[this.nowIndex];
	}

	// Token: 0x0600260D RID: 9741 RVA: 0x0012D328 File Offset: 0x0012B528
	public void clickRight()
	{
		this.nowIndex++;
		if (this.nowIndex >= this.list.Count)
		{
			this.nowIndex = 0;
		}
		this.label.text = this.list[this.nowIndex];
	}

	// Token: 0x0400208A RID: 8330
	public UIButton left;

	// Token: 0x0400208B RID: 8331
	public UIButton right;

	// Token: 0x0400208C RID: 8332
	public EventDelegate leftEvent;

	// Token: 0x0400208D RID: 8333
	public EventDelegate rightEvent;

	// Token: 0x0400208E RID: 8334
	public List<string> list = new List<string>();

	// Token: 0x0400208F RID: 8335
	public UILabel label;

	// Token: 0x04002090 RID: 8336
	private int nowIndex;
}
