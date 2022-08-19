using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200049D RID: 1181
public class testtoggel : MonoBehaviour
{
	// Token: 0x06002551 RID: 9553 RVA: 0x00102DA4 File Offset: 0x00100FA4
	private void Start()
	{
		base.Invoke("set", 1f);
	}

	// Token: 0x06002552 RID: 9554 RVA: 0x00102DB6 File Offset: 0x00100FB6
	public void set()
	{
		base.transform.Find("Toggle").GetComponent<Toggle>().isOn = true;
	}

	// Token: 0x06002553 RID: 9555 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}
}
