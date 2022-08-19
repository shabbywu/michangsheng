using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003A2 RID: 930
public class WuDaoHasType : MonoBehaviour
{
	// Token: 0x06001E59 RID: 7769 RVA: 0x000D5B83 File Offset: 0x000D3D83
	public void setIcon(Sprite sprite)
	{
		this.icon.sprite = sprite;
	}

	// Token: 0x06001E5A RID: 7770 RVA: 0x000D5B94 File Offset: 0x000D3D94
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

	// Token: 0x06001E5B RID: 7771 RVA: 0x000D5CC8 File Offset: 0x000D3EC8
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

	// Token: 0x040018E2 RID: 6370
	[SerializeField]
	private Image icon;

	// Token: 0x040018E3 RID: 6371
	[SerializeField]
	private GameObject Content;

	// Token: 0x040018E4 RID: 6372
	[SerializeField]
	private Text NoContent;
}
