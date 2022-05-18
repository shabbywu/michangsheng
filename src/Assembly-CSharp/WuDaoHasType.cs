using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200052B RID: 1323
public class WuDaoHasType : MonoBehaviour
{
	// Token: 0x060021DA RID: 8666 RVA: 0x0001BCC5 File Offset: 0x00019EC5
	public void setIcon(Sprite sprite)
	{
		this.icon.sprite = sprite;
	}

	// Token: 0x060021DB RID: 8667 RVA: 0x001193DC File Offset: 0x001175DC
	public void init(int type, Avatar player, out float height)
	{
		this.clear();
		List<JSONObject> list = player.wuDaoMag.getWuDaoStudy(type).list;
		int count = list.Count;
		if (list.Count <= 0)
		{
			this.NoContent.gameObject.SetActive(true);
			this.NoContent.text = "无";
			height = 85f;
			return;
		}
		for (int i = 0; i < count; i++)
		{
			Tools.InstantiateGameObject(this.Content.transform.GetChild(0).gameObject, this.Content.transform).GetComponent<WuDaoContentCell>().setContent(jsonData.instance.WuDaoJson[list[i].I.ToString()]["name"].str, "\u3000\u3000\u3000" + jsonData.instance.WuDaoJson[list[i].I.ToString()]["xiaoguo"].str);
		}
		if (count >= 2)
		{
			height = (float)(((count % 2 == 0) ? (count / 2) : (count / 2 + 1)) * 85);
			return;
		}
		height = 85f;
	}

	// Token: 0x060021DC RID: 8668 RVA: 0x00119510 File Offset: 0x00117710
	public void clear()
	{
		foreach (object obj in this.Content.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.gameObject.activeSelf)
			{
				Object.Destroy(transform.gameObject);
			}
		}
	}

	// Token: 0x04001D4B RID: 7499
	[SerializeField]
	private Image icon;

	// Token: 0x04001D4C RID: 7500
	[SerializeField]
	private GameObject Content;

	// Token: 0x04001D4D RID: 7501
	[SerializeField]
	private Text NoContent;
}
