using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class XiaoGuoCell : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	[SerializeField]
	private Text desc;

	[SerializeField]
	private GameObject image;

	public int ID;

	public int linWenID;

	[SerializeField]
	private XiaoGuoTips XiaoGuoTips;

	public int status = -1;

	public void setDesc(string str)
	{
		desc.text = Tools.Code64(str);
	}

	public string setColor(string str, int color = 0)
	{
		str = ((color != 0) ? ("<color=#ff624d>" + str + "</color>") : ("<color=#f5e929>" + str + "</color>"));
		return str;
	}

	private void OnDestroy()
	{
		Object.Destroy((Object)(object)image);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		if (status != -1)
		{
			if (status == 1)
			{
				if (ID != 0)
				{
					string text = Tools.Code64(jsonData.instance.LianQiHeCheng[ID.ToString()]["xiangxidesc"].str);
					if (text == "无" || text == "")
					{
						XiaoGuoTips.close();
					}
					else
					{
						XiaoGuoTips.setPosition(((Component)this).gameObject.transform.position.y);
						XiaoGuoTips.show();
						XiaoGuoTips.setContent(text);
					}
				}
				else
				{
					XiaoGuoTips.close();
				}
			}
			if (status != 2)
			{
				return;
			}
			if (linWenID != 0)
			{
				string text2 = Tools.Code64(jsonData.instance.LianQiLingWenBiao[linWenID.ToString()]["xiangxidesc"].str);
				if (text2 == "无" || text2 == "")
				{
					XiaoGuoTips.close();
					return;
				}
				XiaoGuoTips.setPosition(((Component)this).gameObject.transform.position.y);
				XiaoGuoTips.show();
				XiaoGuoTips.setContent(text2);
			}
			else
			{
				XiaoGuoTips.close();
			}
		}
		else
		{
			XiaoGuoTips.close();
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		XiaoGuoTips.close();
	}
}
