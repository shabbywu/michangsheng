using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000360 RID: 864
[RequireComponent(typeof(Image))]
public class UIBarAutoSync : MonoBehaviour
{
	// Token: 0x06001D18 RID: 7448 RVA: 0x000CEA35 File Offset: 0x000CCC35
	private void Awake()
	{
		this.me = base.GetComponent<Image>();
	}

	// Token: 0x06001D19 RID: 7449 RVA: 0x000CEA44 File Offset: 0x000CCC44
	private void Update()
	{
		if (this.Target != null && !this.nowAnim && this.me.fillAmount != this.Target.fillAmount)
		{
			this.nowAnim = true;
			base.StartCoroutine(this.MoveAnim());
		}
	}

	// Token: 0x06001D1A RID: 7450 RVA: 0x000CEA93 File Offset: 0x000CCC93
	private IEnumerator MoveAnim()
	{
		yield return new WaitForSeconds(this.DelayTime);
		DOTweenModuleUI.DOFillAmount(this.me, this.Target.fillAmount, 0.3f).onComplete = delegate()
		{
			this.nowAnim = false;
		};
		yield break;
	}

	// Token: 0x0400179F RID: 6047
	public Image Target;

	// Token: 0x040017A0 RID: 6048
	public float DelayTime = 0.1f;

	// Token: 0x040017A1 RID: 6049
	private Image me;

	// Token: 0x040017A2 RID: 6050
	private bool nowAnim;
}
