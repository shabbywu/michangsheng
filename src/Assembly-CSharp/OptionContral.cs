using System;
using System.Collections.Generic;
using GUIPackage;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200028F RID: 655
public class OptionContral : MonoBehaviour
{
	// Token: 0x0600141B RID: 5147 RVA: 0x000B7EDC File Offset: 0x000B60DC
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

	// Token: 0x0600141C RID: 5148 RVA: 0x000B7F50 File Offset: 0x000B6150
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

	// Token: 0x0600141D RID: 5149 RVA: 0x00012AF7 File Offset: 0x00010CF7
	public void SetBtnCell(int index, UnityAction btnEvent)
	{
		this.btn[index].onClick.RemoveAllListeners();
		this.btn[index].onClick.AddListener(btnEvent);
	}

	// Token: 0x0600141E RID: 5150 RVA: 0x00012B26 File Offset: 0x00010D26
	private void Start()
	{
		OptionContral.OptionContralList.Add(base.gameObject);
	}

	// Token: 0x04000FA0 RID: 4000
	public Text title;

	// Token: 0x04000FA1 RID: 4001
	public Text Desc;

	// Token: 0x04000FA2 RID: 4002
	public GameObject caijiPlan;

	// Token: 0x04000FA3 RID: 4003
	public Inventory2 inventory2;

	// Token: 0x04000FA4 RID: 4004
	public List<Button> btn;

	// Token: 0x04000FA5 RID: 4005
	public static List<GameObject> OptionContralList = new List<GameObject>();
}
