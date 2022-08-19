using System;
using UnityEngine;

// Token: 0x02000423 RID: 1059
public class setDoFaAvatarcell : MonoBehaviour
{
	// Token: 0x060021EA RID: 8682 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x060021EB RID: 8683 RVA: 0x000E9E8E File Offset: 0x000E808E
	private void Update()
	{
		if (PlayerPrefs.GetInt("NowPlayerFileAvatar") >= 100)
		{
			base.gameObject.SetActive(false);
		}
	}
}
