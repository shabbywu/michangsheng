using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000455 RID: 1109
public class PutCaiLiaoCell : MonoBehaviour
{
	// Token: 0x06001DAE RID: 7598 RVA: 0x00102F94 File Offset: 0x00101194
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

	// Token: 0x06001DAF RID: 7599 RVA: 0x00102FFC File Offset: 0x001011FC
	private void ResetCaiLiao()
	{
		foreach (PutMaterialCell putMaterialCell in this.caiLiaoCells)
		{
			putMaterialCell.SetNull();
		}
		LianQiTotalManager.inst.putCaiLiaoCallBack();
	}

	// Token: 0x06001DB0 RID: 7600 RVA: 0x00103058 File Offset: 0x00101258
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

	// Token: 0x06001DB1 RID: 7601 RVA: 0x001030C0 File Offset: 0x001012C0
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

	// Token: 0x06001DB2 RID: 7602 RVA: 0x00103170 File Offset: 0x00101370
	public void UpdateCell()
	{
		foreach (PutMaterialCell putMaterialCell in this.caiLiaoCells)
		{
			putMaterialCell.SetShuXing();
		}
	}

	// Token: 0x04001960 RID: 6496
	[SerializeField]
	public List<PutMaterialCell> caiLiaoCells = new List<PutMaterialCell>();

	// Token: 0x04001961 RID: 6497
	public GameObject caiLiaoCellParent;

	// Token: 0x04001962 RID: 6498
	public Dictionary<string, PutMaterialCell> caiLiaoCellDictionary;
}
