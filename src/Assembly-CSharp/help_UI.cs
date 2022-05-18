using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000579 RID: 1401
public class help_UI : MonoBehaviour
{
	// Token: 0x0600238E RID: 9102 RVA: 0x001246B0 File Offset: 0x001228B0
	private void Start()
	{
		foreach (JSONObject jsonobject in jsonData.instance.helpJsonData.list)
		{
			this.mList.items.Add(Tools.instance.Code64ToString(jsonobject["Titile"].str));
		}
		this.mList.value = this.mList.items[0];
	}

	// Token: 0x0600238F RID: 9103 RVA: 0x0012474C File Offset: 0x0012294C
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

	// Token: 0x06002390 RID: 9104 RVA: 0x001247B0 File Offset: 0x001229B0
	public void excheng()
	{
		Tools.instance.getPlayer();
		int inputID = this.getInputID(this.mList.value);
		int num = (int)jsonData.instance.helpJsonData.list[inputID]["id"].n;
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
		for (int i = 2; i < this.content.transform.childCount; i++)
		{
			Object.Destroy(this.content.transform.GetChild(i).gameObject);
		}
		Transform child = this.content.transform.GetChild(0);
		foreach (JSONObject jsonobject in jsonData.instance.helpTextJsonData.list)
		{
			if (num == (int)jsonobject["link"].n)
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

	// Token: 0x06002391 RID: 9105 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x04001E9B RID: 7835
	public UIPopupList mList;

	// Token: 0x04001E9C RID: 7836
	public Image helpImage;

	// Token: 0x04001E9D RID: 7837
	public GameObject content;
}
