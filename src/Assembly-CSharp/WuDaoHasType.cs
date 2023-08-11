using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class WuDaoHasType : MonoBehaviour
{
	[SerializeField]
	private Image icon;

	[SerializeField]
	private GameObject Content;

	[SerializeField]
	private Text NoContent;

	public void setIcon(Sprite sprite)
	{
		icon.sprite = sprite;
	}

	public void init(int type, Avatar player, out float height)
	{
		clear();
		List<JSONObject> list = player.wuDaoMag.getWuDaoStudy(type).list;
		int count = list.Count;
		if (list.Count > 0)
		{
			for (int i = 0; i < count; i++)
			{
				Tools.InstantiateGameObject(((Component)Content.transform.GetChild(0)).gameObject, Content.transform).GetComponent<WuDaoContentCell>().setContent(jsonData.instance.WuDaoJson[list[i].I.ToString()]["name"].str, "\u3000\u3000\u3000" + jsonData.instance.WuDaoJson[list[i].I.ToString()]["xiaoguo"].str);
			}
			if (count >= 2)
			{
				height = ((count % 2 == 0) ? (count / 2) : (count / 2 + 1)) * 85;
			}
			else
			{
				height = 85f;
			}
		}
		else
		{
			((Component)NoContent).gameObject.SetActive(true);
			NoContent.text = "无";
			height = 85f;
		}
	}

	public void clear()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		foreach (Transform item in Content.transform)
		{
			Transform val = item;
			if (((Component)val).gameObject.activeSelf)
			{
				Object.Destroy((Object)(object)((Component)val).gameObject);
			}
		}
	}
}
