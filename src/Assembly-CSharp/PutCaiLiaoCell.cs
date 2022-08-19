using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002F8 RID: 760
public class PutCaiLiaoCell : MonoBehaviour
{
	// Token: 0x06001A88 RID: 6792 RVA: 0x000BCF34 File Offset: 0x000BB134
	public void init()
	{
		this.caiLiaoCellParent.SetActive(true);
		this.caiLiaoCellDictionary = new Dictionary<string, PutMaterialCell>();
		for (int i = 0; i < this.caiLiaoCells.Count; i++)
		{
			this.caiLiaoCellDictionary.Add(this.caiLiaoCells[i].name, this.caiLiaoCells[i]);
		}
		this.ResetCaiLiao();
	}

	// Token: 0x06001A89 RID: 6793 RVA: 0x000BCF9C File Offset: 0x000BB19C
	private void ResetCaiLiao()
	{
		foreach (PutMaterialCell putMaterialCell in this.caiLiaoCells)
		{
			putMaterialCell.SetNull();
		}
		LianQiTotalManager.inst.putCaiLiaoCallBack();
	}

	// Token: 0x06001A8A RID: 6794 RVA: 0x000BCFF8 File Offset: 0x000BB1F8
	public void RemoveItem(int itemId)
	{
		foreach (PutMaterialCell putMaterialCell in this.caiLiaoCells)
		{
			if (!putMaterialCell.IsNull() && putMaterialCell.Item.Id == itemId)
			{
				putMaterialCell.SetNull();
			}
		}
	}

	// Token: 0x06001A8B RID: 6795 RVA: 0x000BD060 File Offset: 0x000BB260
	public Dictionary<int, int> GetNeedDict()
	{
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		foreach (PutMaterialCell putMaterialCell in this.caiLiaoCells)
		{
			if (dictionary.ContainsKey(putMaterialCell.Item.Id))
			{
				Dictionary<int, int> dictionary2 = dictionary;
				int id = putMaterialCell.Item.Id;
				dictionary2[id] += putMaterialCell.Item.Count;
			}
			else
			{
				dictionary.Add(putMaterialCell.Item.Id, putMaterialCell.Item.Count);
			}
		}
		return dictionary;
	}

	// Token: 0x06001A8C RID: 6796 RVA: 0x000BD110 File Offset: 0x000BB310
	public void UpdateCell()
	{
		foreach (PutMaterialCell putMaterialCell in this.caiLiaoCells)
		{
			putMaterialCell.SetShuXing();
		}
	}

	// Token: 0x04001553 RID: 5459
	[SerializeField]
	public List<PutMaterialCell> caiLiaoCells = new List<PutMaterialCell>();

	// Token: 0x04001554 RID: 5460
	public GameObject caiLiaoCellParent;

	// Token: 0x04001555 RID: 5461
	public Dictionary<string, PutMaterialCell> caiLiaoCellDictionary;
}
