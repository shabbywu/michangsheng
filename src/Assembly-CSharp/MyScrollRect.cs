using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000300 RID: 768
public class MyScrollRect : MonoBehaviour
{
	// Token: 0x06001AC5 RID: 6853 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06001AC6 RID: 6854 RVA: 0x000BF135 File Offset: 0x000BD335
	public void setContentChild(int count)
	{
		this.contentChildCount = count;
		this.setContentHeight();
	}

	// Token: 0x06001AC7 RID: 6855 RVA: 0x000BF144 File Offset: 0x000BD344
	private void setContentHeight()
	{
		this.scrollRect.content.sizeDelta = new Vector2(this.contentWidth, (this.childHeight + this.speceY) * (float)this.contentChildCount - this.speceY);
	}

	// Token: 0x04001581 RID: 5505
	[SerializeField]
	private ScrollRect scrollRect;

	// Token: 0x04001582 RID: 5506
	[SerializeField]
	private int contentChildCount;

	// Token: 0x04001583 RID: 5507
	[SerializeField]
	private float childHeight;

	// Token: 0x04001584 RID: 5508
	[SerializeField]
	private float speceY;

	// Token: 0x04001585 RID: 5509
	[SerializeField]
	private float contentWidth;
}
