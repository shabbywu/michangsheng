using System;
using UnityEngine;

// Token: 0x0200048B RID: 1163
public class MainUILoadData : MonoBehaviour
{
	// Token: 0x06001F0E RID: 7950 RVA: 0x0010AFB4 File Offset: 0x001091B4
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

	// Token: 0x04001A88 RID: 6792
	[SerializeField]
	private GameObject dataObj;

	// Token: 0x04001A89 RID: 6793
	[SerializeField]
	private RectTransform dataList;
}
