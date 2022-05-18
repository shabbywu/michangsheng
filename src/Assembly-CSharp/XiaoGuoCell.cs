using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200045E RID: 1118
public class XiaoGuoCell : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06001DEF RID: 7663 RVA: 0x00018E1D File Offset: 0x0001701D
	public void setDesc(string str)
	{
		this.desc.text = Tools.Code64(str);
	}

	// Token: 0x06001DF0 RID: 7664 RVA: 0x00018E30 File Offset: 0x00017030
	public string setColor(string str, int color = 0)
	{
		if (color == 0)
		{
			str = "<color=#f5e929>" + str + "</color>";
		}
		else
		{
			str = "<color=#ff624d>" + str + "</color>";
		}
		return str;
	}

	// Token: 0x06001DF1 RID: 7665 RVA: 0x00018E5C File Offset: 0x0001705C
	private void OnDestroy()
	{
		Object.Destroy(this.image);
	}

	// Token: 0x06001DF2 RID: 7666 RVA: 0x00104E44 File Offset: 0x00103044
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (this.status != -1)
		{
			if (this.status == 1)
			{
				if (this.ID != 0)
				{
					string text = Tools.Code64(jsonData.instance.LianQiHeCheng[this.ID.ToString()]["xiangxidesc"].str);
					if (text == "无" || text == "")
					{
						this.XiaoGuoTips.close();
					}
					else
					{
						this.XiaoGuoTips.setPosition(base.gameObject.transform.position.y);
						this.XiaoGuoTips.show();
						this.XiaoGuoTips.setContent(text);
					}
				}
				else
				{
					this.XiaoGuoTips.close();
				}
			}
			if (this.status == 2)
			{
				if (this.linWenID == 0)
				{
					this.XiaoGuoTips.close();
					return;
				}
				string text2 = Tools.Code64(jsonData.instance.LianQiLingWenBiao[this.linWenID.ToString()]["xiangxidesc"].str);
				if (text2 == "无" || text2 == "")
				{
					this.XiaoGuoTips.close();
					return;
				}
				this.XiaoGuoTips.setPosition(base.gameObject.transform.position.y);
				this.XiaoGuoTips.show();
				this.XiaoGuoTips.setContent(text2);
				return;
			}
		}
		else
		{
			this.XiaoGuoTips.close();
		}
	}

	// Token: 0x06001DF3 RID: 7667 RVA: 0x00018E69 File Offset: 0x00017069
	public void OnPointerExit(PointerEventData eventData)
	{
		this.XiaoGuoTips.close();
	}

	// Token: 0x04001993 RID: 6547
	[SerializeField]
	private Text desc;

	// Token: 0x04001994 RID: 6548
	[SerializeField]
	private GameObject image;

	// Token: 0x04001995 RID: 6549
	public int ID;

	// Token: 0x04001996 RID: 6550
	public int linWenID;

	// Token: 0x04001997 RID: 6551
	[SerializeField]
	private XiaoGuoTips XiaoGuoTips;

	// Token: 0x04001998 RID: 6552
	public int status = -1;
}
