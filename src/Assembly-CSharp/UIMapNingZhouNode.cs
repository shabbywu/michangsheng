using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200033F RID: 831
public class UIMapNingZhouNode : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	// Token: 0x06001C7E RID: 7294 RVA: 0x000CC013 File Offset: 0x000CA213
	public void SetNodeName(string nodeName)
	{
		this.NodeName = nodeName;
		this.nodeNameText.text = nodeName;
	}

	// Token: 0x06001C7F RID: 7295 RVA: 0x000CC028 File Offset: 0x000CA228
	public void Init()
	{
		this.iconImage = base.transform.GetChild(0).GetComponent<Image>();
		this.nodeNameText = base.transform.GetChild(1).GetComponent<Text>();
		this.imageOriScale = this.iconImage.transform.localScale;
		this.textOriColor = this.nodeNameText.color;
	}

	// Token: 0x06001C80 RID: 7296 RVA: 0x000CC08A File Offset: 0x000CA28A
	public void SetCanJiaoHu(bool can)
	{
		this.iconImage.transform.localScale = this.imageOriScale;
		this.iconImage.raycastTarget = can;
	}

	// Token: 0x06001C81 RID: 7297 RVA: 0x000CC0B0 File Offset: 0x000CA2B0
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

	// Token: 0x06001C82 RID: 7298 RVA: 0x000CC117 File Offset: 0x000CA317
	void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
	{
		UIMapPanel.Inst.NingZhou.OnNodeClick(this);
	}

	// Token: 0x06001C83 RID: 7299 RVA: 0x000CC129 File Offset: 0x000CA329
	void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
	{
		this.iconImage.transform.localScale = this.imageOriScale * 1.2f;
	}

	// Token: 0x06001C84 RID: 7300 RVA: 0x000CC14B File Offset: 0x000CA34B
	void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
	{
		this.iconImage.transform.localScale = this.imageOriScale;
	}

	// Token: 0x040016F5 RID: 5877
	public string NodeName;

	// Token: 0x040016F6 RID: 5878
	public string WarpSceneName;

	// Token: 0x040016F7 RID: 5879
	private Image iconImage;

	// Token: 0x040016F8 RID: 5880
	private Text nodeNameText;

	// Token: 0x040016F9 RID: 5881
	private Vector3 imageOriScale;

	// Token: 0x040016FA RID: 5882
	private Color textOriColor;

	// Token: 0x040016FB RID: 5883
	private Color textAnColor = new Color(0.76862746f, 0.75686276f, 0.59607846f);
}
