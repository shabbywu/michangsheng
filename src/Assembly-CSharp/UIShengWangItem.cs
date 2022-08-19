using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000381 RID: 897
public class UIShengWangItem : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06001DB2 RID: 7602 RVA: 0x000D187F File Offset: 0x000CFA7F
	public void Set(bool active)
	{
		this.ActiveImage.SetActive(active);
		this.UnActiveImage.SetActive(!active);
		this.NameImage.SetActive(active);
		this.NameText.SetActive(!active);
	}

	// Token: 0x06001DB3 RID: 7603 RVA: 0x000D18B7 File Offset: 0x000CFAB7
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!string.IsNullOrWhiteSpace(this.TeQuanDesc))
		{
			UToolTip.Show(this.TeQuanDesc, 500f, 100f);
			UToolTip.BindObj = base.gameObject;
		}
	}

	// Token: 0x06001DB4 RID: 7604 RVA: 0x000D18E6 File Offset: 0x000CFAE6
	public void OnPointerExit(PointerEventData eventData)
	{
		UToolTip.Close();
	}

	// Token: 0x04001840 RID: 6208
	public GameObject ActiveImage;

	// Token: 0x04001841 RID: 6209
	public GameObject UnActiveImage;

	// Token: 0x04001842 RID: 6210
	public GameObject NameImage;

	// Token: 0x04001843 RID: 6211
	public GameObject NameText;

	// Token: 0x04001844 RID: 6212
	public string TeQuanDesc;
}
