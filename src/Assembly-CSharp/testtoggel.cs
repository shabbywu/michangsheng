using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000679 RID: 1657
public class testtoggel : MonoBehaviour
{
	// Token: 0x06002975 RID: 10613 RVA: 0x00020331 File Offset: 0x0001E531
	private void Start()
	{
		base.Invoke("set", 1f);
	}

	// Token: 0x06002976 RID: 10614 RVA: 0x00020343 File Offset: 0x0001E543
	public void set()
	{
		base.transform.Find("Toggle").GetComponent<Toggle>().isOn = true;
	}

	// Token: 0x06002977 RID: 10615 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}
}
