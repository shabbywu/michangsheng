using System;
using UnityEngine;

// Token: 0x0200067A RID: 1658
public class ChangeWorldBgd : MonoBehaviour
{
	// Token: 0x06002979 RID: 10617 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x0600297A RID: 10618 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x0600297B RID: 10619 RVA: 0x00020360 File Offset: 0x0001E560
	private void ChangeBgd()
	{
		Camera.main.transform.Find("Background").GetComponent<Renderer>().material = GameObject.Find("TempBgd").GetComponent<Renderer>().material;
	}
}
