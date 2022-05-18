using System;
using UnityEngine;

// Token: 0x0200062E RID: 1582
public class showYaoCaiMag : MonoBehaviour
{
	// Token: 0x0600274A RID: 10058 RVA: 0x000042DD File Offset: 0x000024DD
	private void Awake()
	{
	}

	// Token: 0x0600274B RID: 10059 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x0600274C RID: 10060 RVA: 0x00133804 File Offset: 0x00131A04
	public void open()
	{
		this.AddItems();
		foreach (object obj in this.content.transform)
		{
			showCaiLiaoImage component = ((Transform)obj).GetComponent<showCaiLiaoImage>();
			if (component != null && component.ItemID != -1 && component.gameObject.activeSelf)
			{
				component.Click();
				break;
			}
		}
	}

	// Token: 0x0600274D RID: 10061 RVA: 0x00133890 File Offset: 0x00131A90
	public void AddItems()
	{
		foreach (JSONObject jsonobject in Tools.instance.getPlayer().YaoCaiIsGet.list)
		{
			bool flag = true;
			foreach (object obj in this.content.transform)
			{
				showCaiLiaoImage component = ((Transform)obj).GetComponent<showCaiLiaoImage>();
				if (component != null && component.ItemID == jsonobject.I)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				GameObject gameObject = Tools.InstantiateGameObject(this.InstantObj, this.InstantObj.transform.parent);
				gameObject.GetComponent<showCaiLiaoImage>().ItemID = jsonobject.I;
				gameObject.GetComponent<showCaiLiaoImage>().TextName.text = Tools.Code64(jsonData.instance.ItemJsonData[jsonobject.I.ToString()]["name"].str);
			}
		}
	}

	// Token: 0x0600274E RID: 10062 RVA: 0x001339D0 File Offset: 0x00131BD0
	public void setShowType()
	{
		int inputID = this.getInputID(this.mList.value);
		foreach (object obj in this.content.transform)
		{
			Transform transform = (Transform)obj;
			showCaiLiaoImage component = transform.GetComponent<showCaiLiaoImage>();
			if (component != null && component.ItemID != -1)
			{
				int i = jsonData.instance.ItemJsonData[component.ItemID.ToString()]["quality"].I;
				if (inputID != 0)
				{
					if (i == inputID)
					{
						transform.gameObject.SetActive(true);
					}
					else
					{
						transform.gameObject.SetActive(false);
					}
				}
				else
				{
					transform.gameObject.SetActive(true);
				}
			}
		}
	}

	// Token: 0x0600274F RID: 10063 RVA: 0x00133AB8 File Offset: 0x00131CB8
	public int getInputID(string name)
	{
		int num = 0;
		foreach (string b in this.mList.items)
		{
			if (name == b)
			{
				break;
			}
			num++;
		}
		return num;
	}

	// Token: 0x06002750 RID: 10064 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x04002157 RID: 8535
	public GameObject InstantObj;

	// Token: 0x04002158 RID: 8536
	public UIPopupList mList;

	// Token: 0x04002159 RID: 8537
	public GameObject content;

	// Token: 0x0400215A RID: 8538
	public UIToggle caoYaoUIToggle;
}
