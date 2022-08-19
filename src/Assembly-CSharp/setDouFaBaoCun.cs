using System;
using UnityEngine;

// Token: 0x02000424 RID: 1060
public class setDouFaBaoCun : MonoBehaviour
{
	// Token: 0x060021ED RID: 8685 RVA: 0x000E9E8E File Offset: 0x000E808E
	private void Start()
	{
		if (PlayerPrefs.GetInt("NowPlayerFileAvatar") >= 100)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x060021EE RID: 8686 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}
}
