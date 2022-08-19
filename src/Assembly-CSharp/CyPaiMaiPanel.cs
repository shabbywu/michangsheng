using System;
using JSONClass;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000291 RID: 657
public class CyPaiMaiPanel : MonoBehaviour, IESCClose
{
	// Token: 0x060017AA RID: 6058 RVA: 0x000A3408 File Offset: 0x000A1608
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

	// Token: 0x060017AB RID: 6059 RVA: 0x000A3540 File Offset: 0x000A1740
	public void Close()
	{
		base.gameObject.SetActive(false);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	// Token: 0x060017AC RID: 6060 RVA: 0x000A3559 File Offset: 0x000A1759
	public bool TryEscClose()
	{
		this.Close();
		return true;
	}

	// Token: 0x04001271 RID: 4721
	[SerializeField]
	private Text _title;

	// Token: 0x04001272 RID: 4722
	[SerializeField]
	private Text _time;

	// Token: 0x04001273 RID: 4723
	[SerializeField]
	private Transform _itemList;

	// Token: 0x04001274 RID: 4724
	[SerializeField]
	private GameObject _item;
}
