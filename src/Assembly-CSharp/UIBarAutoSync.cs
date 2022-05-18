using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004DC RID: 1244
[RequireComponent(typeof(Image))]
public class UIBarAutoSync : MonoBehaviour
{
	// Token: 0x0600207C RID: 8316 RVA: 0x0001AB76 File Offset: 0x00018D76
	private void Awake()
	{
		this.me = base.GetComponent<Image>();
	}

	// Token: 0x0600207D RID: 8317 RVA: 0x0011345C File Offset: 0x0011165C
	private void Update()
	{
		if (this.Target != null && !this.nowAnim && this.me.fillAmount != this.Target.fillAmount)
		{
			this.nowAnim = true;
			base.StartCoroutine(this.MoveAnim());
		}
	}

	// Token: 0x0600207E RID: 8318 RVA: 0x0001AB84 File Offset: 0x00018D84
	private IEnumerator MoveAnim()
	{
		yield return new WaitForSeconds(this.DelayTime);
		DOTweenModuleUI.DOFillAmount(this.me, this.Target.fillAmount, 0.3f).onComplete = delegate()
		{
			this.nowAnim = false;
		};
		yield break;
	}

	// Token: 0x04001BEE RID: 7150
	public Image Target;

	// Token: 0x04001BEF RID: 7151
	public float DelayTime = 0.1f;

	// Token: 0x04001BF0 RID: 7152
	private Image me;

	// Token: 0x04001BF1 RID: 7153
	private bool nowAnim;
}
