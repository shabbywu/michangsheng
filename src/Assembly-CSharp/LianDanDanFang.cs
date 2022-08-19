using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000460 RID: 1120
public class LianDanDanFang : MonoBehaviour
{
	// Token: 0x06002318 RID: 8984 RVA: 0x000EFB68 File Offset: 0x000EDD68
	public void InitDanFang()
	{
		foreach (JSONObject jsonobject in Tools.instance.getPlayer().DanFang.list)
		{
			if (jsonobject["ID"].I > 0)
			{
				bool flag = true;
				Transform transform = null;
				foreach (object obj in base.transform)
				{
					Transform transform2 = (Transform)obj;
					if (jsonobject["ID"].I == transform2.GetComponent<DanFang_UI>().ItemID)
					{
						flag = false;
						transform = transform2;
					}
				}
				if (flag)
				{
					GameObject gameObject = Object.Instantiate<GameObject>(this.DanFangItem);
					gameObject.GetComponent<DanFang_UI>().ItemID = jsonobject["ID"].I;
					gameObject.GetComponent<DanFang_UI>().text.text = Tools.Code64(jsonData.instance.ItemJsonData[jsonobject["ID"].I.ToString()]["name"].str);
					gameObject.transform.parent = base.transform;
					gameObject.SetActive(true);
					gameObject.transform.localScale = Vector3.one;
					transform = gameObject.transform;
				}
				bool flag2 = true;
				foreach (object obj2 in transform.GetComponent<DanFang_UI>().content.transform)
				{
					Transform transform3 = (Transform)obj2;
					if (transform3.gameObject.activeSelf)
					{
						bool flag3 = true;
						for (int i = 0; i < jsonobject["Type"].list.Count; i++)
						{
							if ((int)jsonobject["Type"][i].n != transform3.GetComponent<DanGeDanFang_UI>().danyao[i])
							{
								flag3 = false;
							}
						}
						for (int j = 0; j < jsonobject["Num"].list.Count; j++)
						{
							if ((int)jsonobject["Num"][j].n != transform3.GetComponent<DanGeDanFang_UI>().num[j])
							{
								flag3 = false;
							}
						}
						if (flag3)
						{
							flag2 = false;
						}
					}
				}
				if (flag2)
				{
					GameObject gameObject2 = Object.Instantiate<GameObject>(this.DanFang);
					foreach (JSONObject jsonobject2 in jsonobject["Type"].list)
					{
						gameObject2.GetComponent<DanGeDanFang_UI>().danyao.Add((int)jsonobject2.n);
					}
					foreach (JSONObject jsonobject3 in jsonobject["Num"].list)
					{
						gameObject2.GetComponent<DanGeDanFang_UI>().num.Add((int)jsonobject3.n);
					}
					gameObject2.GetComponent<DanGeDanFang_UI>().init();
					gameObject2.transform.parent = transform.GetComponent<DanFang_UI>().content.transform;
					gameObject2.SetActive(true);
					gameObject2.transform.localScale = Vector3.one;
					gameObject2.transform.localPosition = new Vector3(50f, -30f);
				}
			}
		}
	}

	// Token: 0x06002319 RID: 8985 RVA: 0x000EFF80 File Offset: 0x000EE180
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

	// Token: 0x0600231A RID: 8986 RVA: 0x000EFFE4 File Offset: 0x000EE1E4
	private void Start()
	{
		this.InitDanFang();
	}

	// Token: 0x0600231B RID: 8987 RVA: 0x000EFFEC File Offset: 0x000EE1EC
	public void setShowType()
	{
		this.showtype = this.getInputID(this.mList.value);
	}

	// Token: 0x0600231C RID: 8988 RVA: 0x000F0008 File Offset: 0x000EE208
	private void Update()
	{
		if (this.showtype != 0)
		{
			using (IEnumerator enumerator = base.transform.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					if (transform.GetComponent<DanFang_UI>().ItemID > 0)
					{
						if ((int)jsonData.instance.ItemJsonData[transform.GetComponent<DanFang_UI>().ItemID.ToString()]["quality"].n == this.showtype)
						{
							if (!transform.gameObject.activeSelf)
							{
								transform.gameObject.SetActive(true);
							}
						}
						else
						{
							transform.gameObject.SetActive(false);
						}
					}
				}
				return;
			}
		}
		foreach (object obj2 in base.transform)
		{
			Transform transform2 = (Transform)obj2;
			if (transform2.GetComponent<DanFang_UI>().ItemID > 0)
			{
				transform2.gameObject.SetActive(true);
			}
		}
	}

	// Token: 0x04001C45 RID: 7237
	public GameObject DanFangItem;

	// Token: 0x04001C46 RID: 7238
	public GameObject DanFang;

	// Token: 0x04001C47 RID: 7239
	public UIPopupList mList;

	// Token: 0x04001C48 RID: 7240
	public int showtype;
}
