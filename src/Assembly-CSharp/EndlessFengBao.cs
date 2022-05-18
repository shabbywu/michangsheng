using System;
using System.Collections;
using DG.Tweening;
using Spine.Unity;
using UnityEngine;

// Token: 0x0200055B RID: 1371
public class EndlessFengBao : MonoBehaviour
{
	// Token: 0x0600230C RID: 8972 RVA: 0x0001C827 File Offset: 0x0001AA27
	private void Awake()
	{
		this.mr = base.GetComponentInChildren<MeshRenderer>();
		this.anim = base.GetComponentInChildren<SkeletonAnimation>();
	}

	// Token: 0x0600230D RID: 8973 RVA: 0x00121BE4 File Offset: 0x0011FDE4
	public void Show()
	{
		this.anim.gameObject.SetActive(false);
		if (this.lv == 1)
		{
			if (SceneEx.NowSceneName == "Sea2")
			{
				this.anim.AnimationName = string.Format("fengbao1_1_{0}", PlayerEx.Player.RandomSeedNext() % 2 + 1);
			}
			else
			{
				this.anim.AnimationName = string.Format("fengbao1_2_{0}", PlayerEx.Player.RandomSeedNext() % 2 + 1);
			}
		}
		else
		{
			this.anim.AnimationName = string.Format("fengbao{0}", this.lv);
		}
		this.mr.sortingOrder = -1000;
		base.StartCoroutine("RandomPlay");
	}

	// Token: 0x0600230E RID: 8974 RVA: 0x0001C841 File Offset: 0x0001AA41
	private IEnumerator RandomPlay()
	{
		float num;
		if (this.lv == 1)
		{
			num = Random.Range(0f, 3f);
		}
		else
		{
			num = Random.Range(0f, 0.5f);
		}
		yield return new WaitForSeconds(num);
		this.anim.gameObject.SetActive(true);
		yield return new WaitForEndOfFrame();
		this.mr.sortingOrder = this.lv;
		yield break;
	}

	// Token: 0x0600230F RID: 8975 RVA: 0x0001C850 File Offset: 0x0001AA50
	public void Move(Vector3 endPositon)
	{
		ShortcutExtensions.DOMove(base.transform, endPositon, 1f, false);
	}

	// Token: 0x04001E23 RID: 7715
	public int id;

	// Token: 0x04001E24 RID: 7716
	public int index;

	// Token: 0x04001E25 RID: 7717
	public int lv;

	// Token: 0x04001E26 RID: 7718
	public int seaid;

	// Token: 0x04001E27 RID: 7719
	private MeshRenderer mr;

	// Token: 0x04001E28 RID: 7720
	private SkeletonAnimation anim;
}
