using System;
using UnityEngine;

// Token: 0x02000524 RID: 1316
public class UIBlackMask : MonoBehaviour
{
	// Token: 0x060021C2 RID: 8642 RVA: 0x0001BBC5 File Offset: 0x00019DC5
	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
		PanelMamager.inst.UIBlackMaskGameObject = base.gameObject;
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001D3D RID: 7485
	public Animator animator;
}
