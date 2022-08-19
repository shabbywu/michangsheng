using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200037A RID: 890
public class LangHuaGroup : MonoBehaviour
{
	// Token: 0x06001DA0 RID: 7584 RVA: 0x000D14F6 File Offset: 0x000CF6F6
	private void Start()
	{
		this.Init();
		base.StartCoroutine("RandomPlay");
	}

	// Token: 0x06001DA1 RID: 7585 RVA: 0x000D150C File Offset: 0x000CF70C
	public void Init()
	{
		LangHuaItem[] componentsInChildren = base.GetComponentsInChildren<LangHuaItem>();
		this.langHuaList.Clear();
		foreach (LangHuaItem langHuaItem in componentsInChildren)
		{
			this.langHuaList.Add(langHuaItem);
			langHuaItem.gameObject.SetActive(false);
		}
		this.langHuaList = this.langHuaList.RandomSort<LangHuaItem>();
	}

	// Token: 0x06001DA2 RID: 7586 RVA: 0x000D1566 File Offset: 0x000CF766
	private IEnumerator RandomPlay()
	{
		if (this.langHuaList.Count > 0)
		{
			float timeSpace = this.TotalTime / (float)this.langHuaList.Count;
			int num;
			for (int i = 0; i < this.langHuaList.Count; i = num + 1)
			{
				if (this.langHuaList[i] != null)
				{
					this.langHuaList[i].Show();
					yield return new WaitForSeconds(timeSpace);
				}
				num = i;
			}
		}
		yield break;
	}

	// Token: 0x0400182D RID: 6189
	private List<LangHuaItem> langHuaList = new List<LangHuaItem>();

	// Token: 0x0400182E RID: 6190
	[Header("播放总时间")]
	[Tooltip("每个浪花间隔 总时间/浪花总数 开启播放")]
	public float TotalTime = 10f;
}
