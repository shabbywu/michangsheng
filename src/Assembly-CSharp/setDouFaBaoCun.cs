using System;
using UnityEngine;

// Token: 0x020005D8 RID: 1496
public class setDouFaBaoCun : MonoBehaviour
{
	// Token: 0x060025A7 RID: 9639 RVA: 0x0001E28D File Offset: 0x0001C48D
	private void Start()
	{
		if (PlayerPrefs.GetInt("NowPlayerFileAvatar") >= 100)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x060025A8 RID: 9640 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}
}
