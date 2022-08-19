using System;
using UnityEngine;

// Token: 0x02000398 RID: 920
public class LoadUIScence : MonoBehaviour
{
	// Token: 0x06001E39 RID: 7737 RVA: 0x000D5474 File Offset: 0x000D3674
	private void Start()
	{
		Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("GameObject"));
		Object.Destroy(base.gameObject);
	}
}
