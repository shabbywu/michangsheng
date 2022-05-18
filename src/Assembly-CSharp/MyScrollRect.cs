using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200045D RID: 1117
public class MyScrollRect : MonoBehaviour
{
	// Token: 0x06001DEB RID: 7659 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06001DEC RID: 7660 RVA: 0x00018DD5 File Offset: 0x00016FD5
	public void setContentChild(int count)
	{
		this.contentChildCount = count;
		this.setContentHeight();
	}

	// Token: 0x06001DED RID: 7661 RVA: 0x00018DE4 File Offset: 0x00016FE4
	private void setContentHeight()
	{
		this.scrollRect.content.sizeDelta = new Vector2(this.contentWidth, (this.childHeight + this.speceY) * (float)this.contentChildCount - this.speceY);
	}

	// Token: 0x0400198E RID: 6542
	[SerializeField]
	private ScrollRect scrollRect;

	// Token: 0x0400198F RID: 6543
	[SerializeField]
	private int contentChildCount;

	// Token: 0x04001990 RID: 6544
	[SerializeField]
	private float childHeight;

	// Token: 0x04001991 RID: 6545
	[SerializeField]
	private float speceY;

	// Token: 0x04001992 RID: 6546
	[SerializeField]
	private float contentWidth;
}
