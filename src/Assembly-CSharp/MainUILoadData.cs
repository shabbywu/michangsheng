using System;
using UnityEngine;

// Token: 0x02000326 RID: 806
public class MainUILoadData : MonoBehaviour
{
	// Token: 0x06001BCE RID: 7118 RVA: 0x000C5DEC File Offset: 0x000C3FEC
	public void Init(int index)
	{
		Tools.ClearObj(this.dataObj.transform);
		for (int i = 0; i < MainUIMag.inst.smallDataNum; i++)
		{
			Object.Instantiate<GameObject>(this.dataObj, this.dataList).GetComponent<MainUIDataCell>().Init(index, i);
		}
		this.dataList.anchoredPosition = new Vector2(this.dataList.anchoredPosition.x, 0f);
		base.gameObject.SetActive(true);
	}

	// Token: 0x04001664 RID: 5732
	[SerializeField]
	private GameObject dataObj;

	// Token: 0x04001665 RID: 5733
	[SerializeField]
	private RectTransform dataList;
}
