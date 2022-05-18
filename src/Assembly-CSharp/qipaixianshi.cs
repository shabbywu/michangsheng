using System;
using UnityEngine;

// Token: 0x02000622 RID: 1570
public class qipaixianshi : MonoBehaviour
{
	// Token: 0x0600270C RID: 9996 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x0600270D RID: 9997 RVA: 0x0001F061 File Offset: 0x0001D261
	private void Update()
	{
		if (base.GetComponent<UILabel>().text.Contains("弃置"))
		{
			this.num.gameObject.SetActive(true);
			return;
		}
		this.num.gameObject.SetActive(false);
	}

	// Token: 0x0400213C RID: 8508
	public UILabel num;
}
