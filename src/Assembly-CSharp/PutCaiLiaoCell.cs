using System.Collections.Generic;
using UnityEngine;

public class PutCaiLiaoCell : MonoBehaviour
{
	[SerializeField]
	public List<PutMaterialCell> caiLiaoCells = new List<PutMaterialCell>();

	public GameObject caiLiaoCellParent;

	public Dictionary<string, PutMaterialCell> caiLiaoCellDictionary;

	public void init()
	{
		caiLiaoCellParent.SetActive(true);
		caiLiaoCellDictionary = new Dictionary<string, PutMaterialCell>();
		for (int i = 0; i < caiLiaoCells.Count; i++)
		{
			caiLiaoCellDictionary.Add(((Object)caiLiaoCells[i]).name, caiLiaoCells[i]);
		}
		ResetCaiLiao();
	}

	private void ResetCaiLiao()
	{
		foreach (PutMaterialCell caiLiaoCell in caiLiaoCells)
		{
			caiLiaoCell.SetNull();
		}
		LianQiTotalManager.inst.putCaiLiaoCallBack();
	}

	public void RemoveItem(int itemId)
	{
		foreach (PutMaterialCell caiLiaoCell in caiLiaoCells)
		{
			if (!caiLiaoCell.IsNull() && caiLiaoCell.Item.Id == itemId)
			{
				caiLiaoCell.SetNull();
			}
		}
	}

	public Dictionary<int, int> GetNeedDict()
	{
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		foreach (PutMaterialCell caiLiaoCell in caiLiaoCells)
		{
			if (dictionary.ContainsKey(caiLiaoCell.Item.Id))
			{
				dictionary[caiLiaoCell.Item.Id] += caiLiaoCell.Item.Count;
			}
			else
			{
				dictionary.Add(caiLiaoCell.Item.Id, caiLiaoCell.Item.Count);
			}
		}
		return dictionary;
	}

	public void UpdateCell()
	{
		foreach (PutMaterialCell caiLiaoCell in caiLiaoCells)
		{
			caiLiaoCell.SetShuXing();
		}
	}
}
