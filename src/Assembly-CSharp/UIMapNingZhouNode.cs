using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020004B1 RID: 1201
public class UIMapNingZhouNode : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	// Token: 0x06001FCB RID: 8139 RVA: 0x0001A28C File Offset: 0x0001848C
	public void SetNodeName(string nodeName)
	{
		this.NodeName = nodeName;
		this.nodeNameText.text = nodeName;
	}

	// Token: 0x06001FCC RID: 8140 RVA: 0x00110B10 File Offset: 0x0010ED10
	public void Init()
	{
		this.iconImage = base.transform.GetChild(0).GetComponent<Image>();
		this.nodeNameText = base.transform.GetChild(1).GetComponent<Text>();
		this.imageOriScale = this.iconImage.transform.localScale;
		this.textOriColor = this.nodeNameText.color;
	}

	// Token: 0x06001FCD RID: 8141 RVA: 0x0001A2A1 File Offset: 0x000184A1
	public void SetCanJiaoHu(bool can)
	{
		this.iconImage.transform.localScale = this.imageOriScale;
		this.iconImage.raycastTarget = can;
	}

	// Token: 0x06001FCE RID: 8142 RVA: 0x00110B74 File Offset: 0x0010ED74
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

	// Token: 0x06001FCF RID: 8143 RVA: 0x0001A2C5 File Offset: 0x000184C5
	void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
	{
		UIMapPanel.Inst.NingZhou.OnNodeClick(this);
	}

	// Token: 0x06001FD0 RID: 8144 RVA: 0x0001A2D7 File Offset: 0x000184D7
	void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
	{
		this.iconImage.transform.localScale = this.imageOriScale * 1.2f;
	}

	// Token: 0x06001FD1 RID: 8145 RVA: 0x0001A2F9 File Offset: 0x000184F9
	void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
	{
		this.iconImage.transform.localScale = this.imageOriScale;
	}

	// Token: 0x04001B31 RID: 6961
	public string NodeName;

	// Token: 0x04001B32 RID: 6962
	public string WarpSceneName;

	// Token: 0x04001B33 RID: 6963
	private Image iconImage;

	// Token: 0x04001B34 RID: 6964
	private Text nodeNameText;

	// Token: 0x04001B35 RID: 6965
	private Vector3 imageOriScale;

	// Token: 0x04001B36 RID: 6966
	private Color textOriColor;

	// Token: 0x04001B37 RID: 6967
	private Color textAnColor = new Color(0.76862746f, 0.75686276f, 0.59607846f);
}
