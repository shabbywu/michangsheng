using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003DE RID: 990
public class help_UI : MonoBehaviour
{
	// Token: 0x0600200E RID: 8206 RVA: 0x000E1D24 File Offset: 0x000DFF24
	private void Start()
	{
		foreach (JSONObject jsonobject in jsonData.instance.helpJsonData.list)
		{
			this.mList.items.Add(Tools.instance.Code64ToString(jsonobject["Titile"].str));
		}
		this.mList.value = this.mList.items[0];
	}

	// Token: 0x0600200F RID: 8207 RVA: 0x000E1DC0 File Offset: 0x000DFFC0
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

	// Token: 0x06002010 RID: 8208 RVA: 0x000E1E24 File Offset: 0x000E0024
	public void excheng()
	{
		Tools.instance.getPlayer();
		int inputID = this.getInputID(this.mList.value);
		int i = jsonData.instance.helpJsonData.list[inputID]["id"].I;
		if ((int)jsonData.instance.helpJsonData.list[inputID]["Image"].n > 0)
		{
			Sprite sprite = Resources.Load<Sprite>("Ui Icon/Help/" + (int)jsonData.instance.helpJsonData.list[inputID]["Image"].n);
			if (sprite != null)
			{
				this.helpImage.sprite = sprite;
			}
		}
		else
		{
			this.helpImage.sprite = null;
		}
		for (int j = 2; j < this.content.transform.childCount; j++)
		{
			Object.Destroy(this.content.transform.GetChild(j).gameObject);
		}
		Transform child = this.content.transform.GetChild(0);
		foreach (JSONObject jsonobject in jsonData.instance.helpTextJsonData.list)
		{
			if (i == (int)jsonobject["link"].n)
			{
				Transform transform = Object.Instantiate<Transform>(child);
				transform.SetParent(this.content.transform);
				transform.transform.localScale = Vector3.one;
				transform.gameObject.SetActive(true);
				transform.GetComponent<Text>().text = Tools.instance.Code64ToString(jsonobject["desc"].str);
				transform.transform.Find("Title").GetComponent<Text>().text = Tools.instance.Code64ToString(jsonobject["Titile"].str);
			}
		}
	}

	// Token: 0x06002011 RID: 8209 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04001A09 RID: 6665
	public UIPopupList mList;

	// Token: 0x04001A0A RID: 6666
	public Image helpImage;

	// Token: 0x04001A0B RID: 6667
	public GameObject content;
}
