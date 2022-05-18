using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004F9 RID: 1273
public class LangHuaGroup : MonoBehaviour
{
	// Token: 0x06002109 RID: 8457 RVA: 0x0001B3C1 File Offset: 0x000195C1
	private void Start()
	{
		this.Init();
		base.StartCoroutine("RandomPlay");
	}

	// Token: 0x0600210A RID: 8458 RVA: 0x0011541C File Offset: 0x0011361C
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

	// Token: 0x0600210B RID: 8459 RVA: 0x0001B3D5 File Offset: 0x000195D5
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

	// Token: 0x04001C7D RID: 7293
	private List<LangHuaItem> langHuaList = new List<LangHuaItem>();

	// Token: 0x04001C7E RID: 7294
	[Header("播放总时间")]
	[Tooltip("每个浪花间隔 总时间/浪花总数 开启播放")]
	public float TotalTime = 10f;
}
