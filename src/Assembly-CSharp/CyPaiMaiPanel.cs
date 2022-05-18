using System;
using JSONClass;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003C0 RID: 960
public class CyPaiMaiPanel : MonoBehaviour, IESCClose
{
	// Token: 0x06001A87 RID: 6791 RVA: 0x000EA63C File Offset: 0x000E883C
	public void Show(CyPaiMaiInfo paiMaiInfo)
	{
		base.gameObject.SetActive(true);
		ESCCloseManager.Inst.RegisterClose(this);
		if (paiMaiInfo == null)
		{
			Debug.LogError("传音符查看拍卖物品信息失败,对象paiMaiInfo为空");
			return;
		}
		Tools.ClearChild(this._itemList);
		foreach (int item in paiMaiInfo.ItemList)
		{
			UIIconShow component = this._item.Inst(this._itemList).GetComponent<UIIconShow>();
			component.SetItem(item);
			component.gameObject.SetActive(true);
		}
		this._title.text = PaiMaiBiao.DataDict[paiMaiInfo.PaiMaiId].Name;
		this._time.text = string.Format("{0}年{1}月{2}日", paiMaiInfo.StartTime.Year, paiMaiInfo.StartTime.Month, paiMaiInfo.StartTime.Day) + string.Format("至{0}月{1}日", paiMaiInfo.EndTime.Month, paiMaiInfo.EndTime.Day);
	}

	// Token: 0x06001A88 RID: 6792 RVA: 0x0001694F File Offset: 0x00014B4F
	public void Close()
	{
		base.gameObject.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	// Token: 0x06001A89 RID: 6793 RVA: 0x00016968 File Offset: 0x00014B68
	public bool TryEscClose()
	{
		this.Close();
		return true;
	}

	// Token: 0x040015F7 RID: 5623
	[SerializeField]
	private Text _title;

	// Token: 0x040015F8 RID: 5624
	[SerializeField]
	private Text _time;

	// Token: 0x040015F9 RID: 5625
	[SerializeField]
	private Transform _itemList;

	// Token: 0x040015FA RID: 5626
	[SerializeField]
	private GameObject _item;
}
