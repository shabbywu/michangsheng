using System;
using UnityEngine;

// Token: 0x02000471 RID: 1137
public class showYaoCaiMag : MonoBehaviour
{
	// Token: 0x06002391 RID: 9105 RVA: 0x00004095 File Offset: 0x00002295
	private void Awake()
	{
	}

	// Token: 0x06002392 RID: 9106 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06002393 RID: 9107 RVA: 0x000F3790 File Offset: 0x000F1990
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

	// Token: 0x06002394 RID: 9108 RVA: 0x000F381C File Offset: 0x000F1A1C
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

	// Token: 0x06002395 RID: 9109 RVA: 0x000F395C File Offset: 0x000F1B5C
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

	// Token: 0x06002396 RID: 9110 RVA: 0x000F3A44 File Offset: 0x000F1C44
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

	// Token: 0x06002397 RID: 9111 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04001C7F RID: 7295
	public GameObject InstantObj;

	// Token: 0x04001C80 RID: 7296
	public UIPopupList mList;

	// Token: 0x04001C81 RID: 7297
	public GameObject content;

	// Token: 0x04001C82 RID: 7298
	public UIToggle caoYaoUIToggle;
}
