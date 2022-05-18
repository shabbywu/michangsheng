using System;
using System.Collections.Generic;

// Token: 0x0200050B RID: 1291
public class SSVPool
{
	// Token: 0x0600214C RID: 8524 RVA: 0x001161D0 File Offset: 0x001143D0
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

	// Token: 0x0600214D RID: 8525 RVA: 0x0001B764 File Offset: 0x00019964
	public void Recovery(SSVItem item)
	{
		item.gameObject.SetActive(false);
		this.Items.Push(item);
	}

	// Token: 0x04001CD6 RID: 7382
	private Stack<SSVItem> Items = new Stack<SSVItem>();
}
