using System;
using UnityEngine;

// Token: 0x02000466 RID: 1126
public class qipaixianshi : MonoBehaviour
{
	// Token: 0x06002356 RID: 9046 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06002357 RID: 9047 RVA: 0x000F1EBA File Offset: 0x000F00BA
	private void Update()
	{
		if (base.GetComponent<UILabel>().text.Contains("弃置"))
		{
			this.num.gameObject.SetActive(true);
			return;
		}
		this.num.gameObject.SetActive(false);
	}

	// Token: 0x04001C67 RID: 7271
	public UILabel num;
}
