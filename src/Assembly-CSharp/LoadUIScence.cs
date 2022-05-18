using System;
using UnityEngine;

// Token: 0x02000521 RID: 1313
public class LoadUIScence : MonoBehaviour
{
	// Token: 0x060021BA RID: 8634 RVA: 0x0001BB8D File Offset: 0x00019D8D
	private void Start()
	{
		Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("GameObject"));
		Object.Destroy(base.gameObject);
	}
}
