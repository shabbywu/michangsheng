using System;
using UnityEngine;

// Token: 0x020005D7 RID: 1495
public class setDoFaAvatarcell : MonoBehaviour
{
	// Token: 0x060025A4 RID: 9636 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x060025A5 RID: 9637 RVA: 0x0001E28D File Offset: 0x0001C48D
	private void Update()
	{
		if (PlayerPrefs.GetInt("NowPlayerFileAvatar") >= 100)
		{
			base.gameObject.SetActive(false);
		}
	}
}
