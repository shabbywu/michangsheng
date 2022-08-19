using System;
using System.Collections;
using DG.Tweening;
using Spine.Unity;
using UnityEngine;

// Token: 0x020003C7 RID: 967
public class EndlessFengBao : MonoBehaviour
{
	// Token: 0x06001F99 RID: 8089 RVA: 0x000DF06C File Offset: 0x000DD26C
	private void Awake()
	{
		this.mr = base.GetComponentInChildren<MeshRenderer>();
		this.anim = base.GetComponentInChildren<SkeletonAnimation>();
	}

	// Token: 0x06001F9A RID: 8090 RVA: 0x000DF088 File Offset: 0x000DD288
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

	// Token: 0x06001F9B RID: 8091 RVA: 0x000DF150 File Offset: 0x000DD350
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

	// Token: 0x06001F9C RID: 8092 RVA: 0x000DF15F File Offset: 0x000DD35F
	public void Move(Vector3 endPositon)
	{
		ShortcutExtensions.DOMove(base.transform, endPositon, 1f, false);
	}

	// Token: 0x040019A5 RID: 6565
	public int id;

	// Token: 0x040019A6 RID: 6566
	public int index;

	// Token: 0x040019A7 RID: 6567
	public int lv;

	// Token: 0x040019A8 RID: 6568
	public int seaid;

	// Token: 0x040019A9 RID: 6569
	private MeshRenderer mr;

	// Token: 0x040019AA RID: 6570
	private SkeletonAnimation anim;
}
