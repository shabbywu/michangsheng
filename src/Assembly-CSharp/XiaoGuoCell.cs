using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000301 RID: 769
public class XiaoGuoCell : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06001AC9 RID: 6857 RVA: 0x000BF17D File Offset: 0x000BD37D
	public void setDesc(string str)
	{
		this.desc.text = Tools.Code64(str);
	}

	// Token: 0x06001ACA RID: 6858 RVA: 0x000BF190 File Offset: 0x000BD390
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

	// Token: 0x06001ACB RID: 6859 RVA: 0x000BF1BC File Offset: 0x000BD3BC
	private void OnDestroy()
	{
		Object.Destroy(this.image);
	}

	// Token: 0x06001ACC RID: 6860 RVA: 0x000BF1CC File Offset: 0x000BD3CC
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

	// Token: 0x06001ACD RID: 6861 RVA: 0x000BF351 File Offset: 0x000BD551
	public void OnPointerExit(PointerEventData eventData)
	{
		this.XiaoGuoTips.close();
	}

	// Token: 0x04001586 RID: 5510
	[SerializeField]
	private Text desc;

	// Token: 0x04001587 RID: 5511
	[SerializeField]
	private GameObject image;

	// Token: 0x04001588 RID: 5512
	public int ID;

	// Token: 0x04001589 RID: 5513
	public int linWenID;

	// Token: 0x0400158A RID: 5514
	[SerializeField]
	private XiaoGuoTips XiaoGuoTips;

	// Token: 0x0400158B RID: 5515
	public int status = -1;
}
