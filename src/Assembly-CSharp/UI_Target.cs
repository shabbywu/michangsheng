using System;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000416 RID: 1046
public class UI_Target : MonoBehaviour
{
	// Token: 0x060021B3 RID: 8627 RVA: 0x000E9826 File Offset: 0x000E7A26
	public bool canAtack()
	{
		return this.GE_target.canAttack;
	}

	// Token: 0x060021B4 RID: 8628 RVA: 0x000E9833 File Offset: 0x000E7A33
	public void deactivate()
	{
		this.GO_targetUI.SetActive(false);
	}

	// Token: 0x04001B22 RID: 6946
	public bool bShowDetail;

	// Token: 0x04001B23 RID: 6947
	public Slider slider_hp;

	// Token: 0x04001B24 RID: 6948
	public Text text_targetName;

	// Token: 0x04001B25 RID: 6949
	public GameObject GO_targetUI;

	// Token: 0x04001B26 RID: 6950
	public GameEntity GE_target;

	// Token: 0x04001B27 RID: 6951
	public Avatar avatar;

	// Token: 0x04001B28 RID: 6952
	public PlayerSetRandomFace randomFace;

	// Token: 0x04001B29 RID: 6953
	public GameObject bufflist;

	// Token: 0x04001B2A RID: 6954
	public GameObject buffTemp;

	// Token: 0x04001B2B RID: 6955
	public GameObject buffTemp2;

	// Token: 0x04001B2C RID: 6956
	public Image leveIcon;

	// Token: 0x04001B2D RID: 6957
	public int BuffCount;

	// Token: 0x04001B2E RID: 6958
	public Text text_hpDetail;

	// Token: 0x04001B2F RID: 6959
	private bool shouldUpdataBuff;

	// Token: 0x04001B30 RID: 6960
	private bool isException;
}
