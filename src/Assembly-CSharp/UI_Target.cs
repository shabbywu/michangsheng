using System;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005C9 RID: 1481
public class UI_Target : MonoBehaviour
{
	// Token: 0x0600256D RID: 9581 RVA: 0x0001E033 File Offset: 0x0001C233
	public bool canAtack()
	{
		return this.GE_target.canAttack;
	}

	// Token: 0x0600256E RID: 9582 RVA: 0x0001E040 File Offset: 0x0001C240
	public void deactivate()
	{
		this.GO_targetUI.SetActive(false);
	}

	// Token: 0x04001FE3 RID: 8163
	public bool bShowDetail;

	// Token: 0x04001FE4 RID: 8164
	public Slider slider_hp;

	// Token: 0x04001FE5 RID: 8165
	public Text text_targetName;

	// Token: 0x04001FE6 RID: 8166
	public GameObject GO_targetUI;

	// Token: 0x04001FE7 RID: 8167
	public GameEntity GE_target;

	// Token: 0x04001FE8 RID: 8168
	public Avatar avatar;

	// Token: 0x04001FE9 RID: 8169
	public PlayerSetRandomFace randomFace;

	// Token: 0x04001FEA RID: 8170
	public GameObject bufflist;

	// Token: 0x04001FEB RID: 8171
	public GameObject buffTemp;

	// Token: 0x04001FEC RID: 8172
	public GameObject buffTemp2;

	// Token: 0x04001FED RID: 8173
	public Image leveIcon;

	// Token: 0x04001FEE RID: 8174
	public int BuffCount;

	// Token: 0x04001FEF RID: 8175
	public Text text_hpDetail;

	// Token: 0x04001FF0 RID: 8176
	private bool shouldUpdataBuff;

	// Token: 0x04001FF1 RID: 8177
	private bool isException;
}
