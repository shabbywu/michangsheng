using System;
using System.Collections.Generic;

// Token: 0x02000387 RID: 903
public class SSVPool
{
	// Token: 0x06001DD3 RID: 7635 RVA: 0x000D2498 File Offset: 0x000D0698
	public SSVItem Get()
	{
		SSVItem ssvitem = null;
		if (this.Items.Count > 0)
		{
			ssvitem = this.Items.Pop();
			ssvitem.gameObject.SetActive(true);
		}
		return ssvitem;
	}

	// Token: 0x06001DD4 RID: 7636 RVA: 0x000D24CE File Offset: 0x000D06CE
	public void Recovery(SSVItem item)
	{
		item.gameObject.SetActive(false);
		this.Items.Push(item);
	}

	// Token: 0x04001878 RID: 6264
	private Stack<SSVItem> Items = new Stack<SSVItem>();
}
