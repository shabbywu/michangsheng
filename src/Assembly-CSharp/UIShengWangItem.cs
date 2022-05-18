using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000503 RID: 1283
public class UIShengWangItem : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06002129 RID: 8489 RVA: 0x0001B4F0 File Offset: 0x000196F0
	public void Set(bool active)
	{
		this.ActiveImage.SetActive(active);
		this.UnActiveImage.SetActive(!active);
		this.NameImage.SetActive(active);
		this.NameText.SetActive(!active);
	}

	// Token: 0x0600212A RID: 8490 RVA: 0x0001B528 File Offset: 0x00019728
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!string.IsNullOrWhiteSpace(this.TeQuanDesc))
		{
			UToolTip.Show(this.TeQuanDesc, 500f, 100f);
			UToolTip.BindObj = base.gameObject;
		}
	}

	// Token: 0x0600212B RID: 8491 RVA: 0x0001B557 File Offset: 0x00019757
	public void OnPointerExit(PointerEventData eventData)
	{
		UToolTip.Close();
	}

	// Token: 0x04001C99 RID: 7321
	public GameObject ActiveImage;

	// Token: 0x04001C9A RID: 7322
	public GameObject UnActiveImage;

	// Token: 0x04001C9B RID: 7323
	public GameObject NameImage;

	// Token: 0x04001C9C RID: 7324
	public GameObject NameText;

	// Token: 0x04001C9D RID: 7325
	public string TeQuanDesc;
}
