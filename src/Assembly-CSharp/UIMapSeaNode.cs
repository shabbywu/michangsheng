using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020004BA RID: 1210
public class UIMapSeaNode : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	// Token: 0x06001FFA RID: 8186 RVA: 0x00111CB8 File Offset: 0x0010FEB8
	public void Init()
	{
		this.iconImage = base.transform.GetChild(0).GetComponent<Image>();
		this.nodeNameText = base.transform.GetChild(1).GetComponent<Text>();
		this.imageOriScale = this.iconImage.transform.localScale;
		this.textOriColor = this.nodeNameText.color;
	}

	// Token: 0x06001FFB RID: 8187 RVA: 0x0001A43F File Offset: 0x0001863F
	public void SetCanJiaoHu(bool can)
	{
		this.iconImage.transform.localScale = this.imageOriScale;
		this.iconImage.raycastTarget = can;
	}

	// Token: 0x06001FFC RID: 8188 RVA: 0x00111D1C File Offset: 0x0010FF1C
	public void SetNodeAlpha(bool alpha)
	{
		if (alpha)
		{
			this.iconImage.color = new Color(1f, 1f, 1f, 0.5f);
			this.nodeNameText.color = this.textAnColor;
			return;
		}
		this.iconImage.color = Color.white;
		this.nodeNameText.color = this.textOriColor;
	}

	// Token: 0x06001FFD RID: 8189 RVA: 0x00111D84 File Offset: 0x0010FF84
	public void RefreshUI()
	{
		if (this.AlwaysShow)
		{
			this.IsAccessed = true;
		}
		else if (PlayerEx.Player != null)
		{
			this.IsAccessed = (GlobalValue.Get(this.AccessStaticValueID, "UIMapSeaNode.RefreshUI 判断是否访问过岛屿") == 1);
		}
		else
		{
			this.IsAccessed = true;
		}
		if (this.IsAccessed)
		{
			this.nodeNameText.text = this.NodeName;
			return;
		}
		this.nodeNameText.text = "???";
	}

	// Token: 0x06001FFE RID: 8190 RVA: 0x0001A463 File Offset: 0x00018663
	void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
	{
		if (this.IsAccessed)
		{
			UIMapPanel.Inst.Sea.OnNodeClick(this);
		}
	}

	// Token: 0x06001FFF RID: 8191 RVA: 0x0001A47D File Offset: 0x0001867D
	void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
	{
		if (this.IsAccessed)
		{
			this.iconImage.transform.localScale = this.imageOriScale * 1.2f;
		}
	}

	// Token: 0x06002000 RID: 8192 RVA: 0x0001A4A7 File Offset: 0x000186A7
	void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
	{
		if (this.IsAccessed)
		{
			this.iconImage.transform.localScale = this.imageOriScale;
		}
	}

	// Token: 0x04001B6A RID: 7018
	public string WarpSceneName;

	// Token: 0x04001B6B RID: 7019
	public string NodeName;

	// Token: 0x04001B6C RID: 7020
	public int NodeIndex;

	// Token: 0x04001B6D RID: 7021
	public int AccessStaticValueID;

	// Token: 0x04001B6E RID: 7022
	public bool AlwaysShow;

	// Token: 0x04001B6F RID: 7023
	private Image iconImage;

	// Token: 0x04001B70 RID: 7024
	private Text nodeNameText;

	// Token: 0x04001B71 RID: 7025
	private Vector3 imageOriScale;

	// Token: 0x04001B72 RID: 7026
	private Color textOriColor;

	// Token: 0x04001B73 RID: 7027
	private Color textAnColor = new Color(0.76862746f, 0.75686276f, 0.59607846f);

	// Token: 0x04001B74 RID: 7028
	public bool IsAccessed;
}
