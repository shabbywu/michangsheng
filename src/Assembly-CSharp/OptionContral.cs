using System;
using System.Collections.Generic;
using GUIPackage;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000199 RID: 409
public class OptionContral : MonoBehaviour
{
	// Token: 0x06001180 RID: 4480 RVA: 0x00069B1C File Offset: 0x00067D1C
	public static GameObject GetOptionContral()
	{
		if (OptionContral.OptionContralList.Count == 0)
		{
			OptionContral.OptionContralList.Add(Object.Instantiate<GameObject>(Resources.Load("uiPrefab/Option") as GameObject));
		}
		if (OptionContral.OptionContralList[0] == null)
		{
			OptionContral.OptionContralList[0] = Object.Instantiate<GameObject>(Resources.Load("uiPrefab/Option") as GameObject);
		}
		return OptionContral.OptionContralList[0];
	}

	// Token: 0x06001181 RID: 4481 RVA: 0x00069B90 File Offset: 0x00067D90
	public void setTextBtn(string text1, int index)
	{
		this.btn[index].transform.Find("Text").GetComponent<Text>().text = text1;
		if (text1 == "")
		{
			this.btn[index].gameObject.SetActive(false);
			return;
		}
		this.btn[index].gameObject.SetActive(true);
	}

	// Token: 0x06001182 RID: 4482 RVA: 0x00069BFF File Offset: 0x00067DFF
	public void SetBtnCell(int index, UnityAction btnEvent)
	{
		this.btn[index].onClick.RemoveAllListeners();
		this.btn[index].onClick.AddListener(btnEvent);
	}

	// Token: 0x06001183 RID: 4483 RVA: 0x00069C2E File Offset: 0x00067E2E
	private void Start()
	{
		OptionContral.OptionContralList.Add(base.gameObject);
	}

	// Token: 0x04000C92 RID: 3218
	public Text title;

	// Token: 0x04000C93 RID: 3219
	public Text Desc;

	// Token: 0x04000C94 RID: 3220
	public GameObject caijiPlan;

	// Token: 0x04000C95 RID: 3221
	public Inventory2 inventory2;

	// Token: 0x04000C96 RID: 3222
	public List<Button> btn;

	// Token: 0x04000C97 RID: 3223
	public static List<GameObject> OptionContralList = new List<GameObject>();
}
