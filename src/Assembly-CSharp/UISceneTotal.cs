using System;
using UnityEngine;

// Token: 0x02000525 RID: 1317
public class UISceneTotal : MonoBehaviour
{
	// Token: 0x060021C4 RID: 8644 RVA: 0x0001BBEE File Offset: 0x00019DEE
	private void Start()
	{
		Object.DontDestroyOnLoad(base.gameObject);
		PanelMamager.inst.UISceneGameObject = base.gameObject;
	}
}
