using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003A0 RID: 928
public class WuDaoContentCell : MonoBehaviour
{
	// Token: 0x06001E52 RID: 7762 RVA: 0x000D598F File Offset: 0x000D3B8F
	public void setContent(string name, string descr)
	{
		this.Name.text = Tools.Code64(name);
		this.Descr.text = Tools.Code64(descr);
	}

	// Token: 0x040018DC RID: 6364
	[SerializeField]
	private Text Name;

	// Token: 0x040018DD RID: 6365
	[SerializeField]
	private Text Descr;
}
