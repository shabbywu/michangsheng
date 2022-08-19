using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000434 RID: 1076
public class UIselect : MonoBehaviour
{
	// Token: 0x17000288 RID: 648
	// (get) Token: 0x06002247 RID: 8775 RVA: 0x000EC142 File Offset: 0x000EA342
	public int NowIndex
	{
		get
		{
			return this.nowIndex;
		}
	}

	// Token: 0x06002248 RID: 8776 RVA: 0x000EC14A File Offset: 0x000EA34A
	private void Start()
	{
		this.left.onClick.Add(this.leftEvent);
		this.right.onClick.Add(this.rightEvent);
	}

	// Token: 0x06002249 RID: 8777 RVA: 0x000EC178 File Offset: 0x000EA378
	public void setIndex(int index)
	{
		this.nowIndex = index;
		this.label.text = this.list[index];
	}

	// Token: 0x0600224A RID: 8778 RVA: 0x000EC198 File Offset: 0x000EA398
	public void setVoiceLeft()
	{
		this.clickLeft();
		this.setVoidSprite();
	}

	// Token: 0x0600224B RID: 8779 RVA: 0x000EC1A8 File Offset: 0x000EA3A8
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

	// Token: 0x0600224C RID: 8780 RVA: 0x000EC23C File Offset: 0x000EA43C
	public void setVoiceRight()
	{
		this.clickRight();
		this.setVoidSprite();
	}

	// Token: 0x0600224D RID: 8781 RVA: 0x000EC24C File Offset: 0x000EA44C
	public void clickLeft()
	{
		this.nowIndex--;
		if (this.nowIndex < 0)
		{
			this.nowIndex = this.list.Count - 1;
		}
		this.label.text = this.list[this.nowIndex];
	}

	// Token: 0x0600224E RID: 8782 RVA: 0x000EC2A0 File Offset: 0x000EA4A0
	public void clickRight()
	{
		this.nowIndex++;
		if (this.nowIndex >= this.list.Count)
		{
			this.nowIndex = 0;
		}
		this.label.text = this.list[this.nowIndex];
	}

	// Token: 0x04001BBE RID: 7102
	public UIButton left;

	// Token: 0x04001BBF RID: 7103
	public UIButton right;

	// Token: 0x04001BC0 RID: 7104
	public EventDelegate leftEvent;

	// Token: 0x04001BC1 RID: 7105
	public EventDelegate rightEvent;

	// Token: 0x04001BC2 RID: 7106
	public List<string> list = new List<string>();

	// Token: 0x04001BC3 RID: 7107
	public UILabel label;

	// Token: 0x04001BC4 RID: 7108
	private int nowIndex;
}
