using System;
using UnityEngine;

// Token: 0x0200049E RID: 1182
public class ChangeWorldBgd : MonoBehaviour
{
	// Token: 0x06002555 RID: 9557 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06002556 RID: 9558 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x06002557 RID: 9559 RVA: 0x00102DD3 File Offset: 0x00100FD3
	private void ChangeBgd()
	{
		Camera.main.transform.Find("Background").GetComponent<Renderer>().material = GameObject.Find("TempBgd").GetComponent<Renderer>().material;
	}
}
