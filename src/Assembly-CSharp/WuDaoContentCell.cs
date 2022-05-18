using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000529 RID: 1321
public class WuDaoContentCell : MonoBehaviour
{
	// Token: 0x060021D3 RID: 8659 RVA: 0x0001BC7F File Offset: 0x00019E7F
	public void setContent(string name, string descr)
	{
		this.Name.text = Tools.Code64(name);
		this.Descr.text = Tools.Code64(descr);
	}

	// Token: 0x04001D45 RID: 7493
	[SerializeField]
	private Text Name;

	// Token: 0x04001D46 RID: 7494
	[SerializeField]
	private Text Descr;
}
