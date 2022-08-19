using System;
using UnityEngine;

// Token: 0x0200039C RID: 924
public class UISceneTotal : MonoBehaviour
{
	// Token: 0x06001E43 RID: 7747 RVA: 0x000D5668 File Offset: 0x000D3868
	private void Start()
	{
		Object.DontDestroyOnLoad(base.gameObject);
		PanelMamager.inst.UISceneGameObject = base.gameObject;
	}
}
