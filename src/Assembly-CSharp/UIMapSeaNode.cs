using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000346 RID: 838
public class UIMapSeaNode : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	// Token: 0x06001CA8 RID: 7336 RVA: 0x000CD2A4 File Offset: 0x000CB4A4
	public void Init()
	{
		this.iconImage = base.transform.GetChild(0).GetComponent<Image>();
		this.nodeNameText = base.transform.GetChild(1).GetComponent<Text>();
		this.imageOriScale = this.iconImage.transform.localScale;
		this.textOriColor = this.nodeNameText.color;
	}

	// Token: 0x06001CA9 RID: 7337 RVA: 0x000CD306 File Offset: 0x000CB506
	public void SetCanJiaoHu(bool can)
	{
		this.iconImage.transform.localScale = this.imageOriScale;
		this.iconImage.raycastTarget = can;
	}

	// Token: 0x06001CAA RID: 7338 RVA: 0x000CD32C File Offset: 0x000CB52C
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

	// Token: 0x06001CAB RID: 7339 RVA: 0x000CD394 File Offset: 0x000CB594
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

	// Token: 0x06001CAC RID: 7340 RVA: 0x000CD405 File Offset: 0x000CB605
	void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
	{
		if (this.IsAccessed)
		{
			UIMapPanel.Inst.Sea.OnNodeClick(this);
		}
	}

	// Token: 0x06001CAD RID: 7341 RVA: 0x000CD41F File Offset: 0x000CB61F
	void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
	{
		if (this.IsAccessed)
		{
			this.iconImage.transform.localScale = this.imageOriScale * 1.2f;
		}
	}

	// Token: 0x06001CAE RID: 7342 RVA: 0x000CD449 File Offset: 0x000CB649
	void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
	{
		if (this.IsAccessed)
		{
			this.iconImage.transform.localScale = this.imageOriScale;
		}
	}

	// Token: 0x04001729 RID: 5929
	public string WarpSceneName;

	// Token: 0x0400172A RID: 5930
	public string NodeName;

	// Token: 0x0400172B RID: 5931
	public int NodeIndex;

	// Token: 0x0400172C RID: 5932
	public int AccessStaticValueID;

	// Token: 0x0400172D RID: 5933
	public bool AlwaysShow;

	// Token: 0x0400172E RID: 5934
	private Image iconImage;

	// Token: 0x0400172F RID: 5935
	private Text nodeNameText;

	// Token: 0x04001730 RID: 5936
	private Vector3 imageOriScale;

	// Token: 0x04001731 RID: 5937
	private Color textOriColor;

	// Token: 0x04001732 RID: 5938
	private Color textAnColor = new Color(0.76862746f, 0.75686276f, 0.59607846f);

	// Token: 0x04001733 RID: 5939
	public bool IsAccessed;
}
