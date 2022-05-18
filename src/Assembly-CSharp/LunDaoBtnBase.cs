using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// Token: 0x02000486 RID: 1158
public class LunDaoBtnBase : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	// Token: 0x06001EED RID: 7917 RVA: 0x000199F2 File Offset: 0x00017BF2
	protected void AddMouseUpCallBack(UnityAction call)
	{
		this.MouseUpCallBack.Add(call);
	}

	// Token: 0x06001EEE RID: 7918 RVA: 0x00019A00 File Offset: 0x00017C00
	protected void AddMouseDownCallBack(UnityAction call)
	{
		this.MouseDownCallBack.Add(call);
	}

	// Token: 0x06001EEF RID: 7919 RVA: 0x00019A0E File Offset: 0x00017C0E
	public void ClearClickCallBack()
	{
		this.MouseUpCallBack = new List<UnityAction>();
		this.MouseDownCallBack = new List<UnityAction>();
	}

	// Token: 0x06001EF0 RID: 7920 RVA: 0x00019A26 File Offset: 0x00017C26
	protected void AddMouseEnterCallBack(UnityAction call)
	{
		this.MouseEnterCallBack.Add(call);
	}

	// Token: 0x06001EF1 RID: 7921 RVA: 0x00019A34 File Offset: 0x00017C34
	public void ClearMouseEnterCallBack()
	{
		this.MouseEnterCallBack = new List<UnityAction>();
	}

	// Token: 0x06001EF2 RID: 7922 RVA: 0x00019A41 File Offset: 0x00017C41
	protected void AddMouseExitCallBack(UnityAction call)
	{
		this.MouseExitCallBack.Add(call);
	}

	// Token: 0x06001EF3 RID: 7923 RVA: 0x00019A4F File Offset: 0x00017C4F
	public void ClearMouseExitCallBack()
	{
		this.MouseExitCallBack = new List<UnityAction>();
	}

	// Token: 0x06001EF4 RID: 7924 RVA: 0x0010A868 File Offset: 0x00108A68
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (this.Disable || this.MouseEnterCallBack.Count < 1)
		{
			return;
		}
		foreach (UnityAction unityAction in this.MouseEnterCallBack)
		{
			unityAction.Invoke();
		}
	}

	// Token: 0x06001EF5 RID: 7925 RVA: 0x0010A8D0 File Offset: 0x00108AD0
	public void OnPointerExit(PointerEventData eventData)
	{
		if (this.Disable || this.MouseExitCallBack.Count < 1)
		{
			return;
		}
		foreach (UnityAction unityAction in this.MouseExitCallBack)
		{
			unityAction.Invoke();
		}
	}

	// Token: 0x06001EF6 RID: 7926 RVA: 0x0010A938 File Offset: 0x00108B38
	public void OnPointerDown(PointerEventData eventData)
	{
		if (this.Disable || this.MouseDownCallBack.Count < 1)
		{
			return;
		}
		foreach (UnityAction unityAction in this.MouseDownCallBack)
		{
			unityAction.Invoke();
		}
	}

	// Token: 0x06001EF7 RID: 7927 RVA: 0x0010A9A0 File Offset: 0x00108BA0
	public void OnPointerUp(PointerEventData eventData)
	{
		if (this.Disable || this.MouseUpCallBack.Count < 1)
		{
			return;
		}
		foreach (UnityAction unityAction in this.MouseUpCallBack)
		{
			unityAction.Invoke();
		}
	}

	// Token: 0x06001EF8 RID: 7928 RVA: 0x00019A5C File Offset: 0x00017C5C
	public void ClearAll()
	{
		this.MouseDownCallBack = new List<UnityAction>();
		this.MouseUpCallBack = new List<UnityAction>();
		this.MouseEnterCallBack = new List<UnityAction>();
		this.MouseExitCallBack = new List<UnityAction>();
	}

	// Token: 0x04001A5C RID: 6748
	private List<UnityAction> MouseDownCallBack = new List<UnityAction>();

	// Token: 0x04001A5D RID: 6749
	private List<UnityAction> MouseUpCallBack = new List<UnityAction>();

	// Token: 0x04001A5E RID: 6750
	private List<UnityAction> MouseEnterCallBack = new List<UnityAction>();

	// Token: 0x04001A5F RID: 6751
	private List<UnityAction> MouseExitCallBack = new List<UnityAction>();

	// Token: 0x04001A60 RID: 6752
	public bool Disable;
}
