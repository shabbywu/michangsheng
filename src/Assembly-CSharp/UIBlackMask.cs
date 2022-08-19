using System;
using UnityEngine;

// Token: 0x0200039B RID: 923
public class UIBlackMask : MonoBehaviour
{
	// Token: 0x06001E41 RID: 7745 RVA: 0x000D563F File Offset: 0x000D383F
	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
		PanelMamager.inst.UIBlackMaskGameObject = base.gameObject;
		base.gameObject.SetActive(false);
	}

	// Token: 0x040018D4 RID: 6356
	public Animator animator;
}
