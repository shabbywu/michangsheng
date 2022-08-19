using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// Token: 0x02000321 RID: 801
public class LunDaoBtnBase : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	// Token: 0x06001BAD RID: 7085 RVA: 0x000C55C8 File Offset: 0x000C37C8
	protected void AddMouseUpCallBack(UnityAction call)
	{
		this.MouseUpCallBack.Add(call);
	}

	// Token: 0x06001BAE RID: 7086 RVA: 0x000C55D6 File Offset: 0x000C37D6
	protected void AddMouseDownCallBack(UnityAction call)
	{
		this.MouseDownCallBack.Add(call);
	}

	// Token: 0x06001BAF RID: 7087 RVA: 0x000C55E4 File Offset: 0x000C37E4
	public void ClearClickCallBack()
	{
		this.MouseUpCallBack = new List<UnityAction>();
		this.MouseDownCallBack = new List<UnityAction>();
	}

	// Token: 0x06001BB0 RID: 7088 RVA: 0x000C55FC File Offset: 0x000C37FC
	protected void AddMouseEnterCallBack(UnityAction call)
	{
		this.MouseEnterCallBack.Add(call);
	}

	// Token: 0x06001BB1 RID: 7089 RVA: 0x000C560A File Offset: 0x000C380A
	public void ClearMouseEnterCallBack()
	{
		this.MouseEnterCallBack = new List<UnityAction>();
	}

	// Token: 0x06001BB2 RID: 7090 RVA: 0x000C5617 File Offset: 0x000C3817
	protected void AddMouseExitCallBack(UnityAction call)
	{
		this.MouseExitCallBack.Add(call);
	}

	// Token: 0x06001BB3 RID: 7091 RVA: 0x000C5625 File Offset: 0x000C3825
	public void ClearMouseExitCallBack()
	{
		this.MouseExitCallBack = new List<UnityAction>();
	}

	// Token: 0x06001BB4 RID: 7092 RVA: 0x000C5634 File Offset: 0x000C3834
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

	// Token: 0x06001BB5 RID: 7093 RVA: 0x000C569C File Offset: 0x000C389C
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

	// Token: 0x06001BB6 RID: 7094 RVA: 0x000C5704 File Offset: 0x000C3904
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

	// Token: 0x06001BB7 RID: 7095 RVA: 0x000C576C File Offset: 0x000C396C
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

	// Token: 0x06001BB8 RID: 7096 RVA: 0x000C57D4 File Offset: 0x000C39D4
	public void ClearAll()
	{
		this.MouseDownCallBack = new List<UnityAction>();
		this.MouseUpCallBack = new List<UnityAction>();
		this.MouseEnterCallBack = new List<UnityAction>();
		this.MouseExitCallBack = new List<UnityAction>();
	}

	// Token: 0x04001637 RID: 5687
	private List<UnityAction> MouseDownCallBack = new List<UnityAction>();

	// Token: 0x04001638 RID: 5688
	private List<UnityAction> MouseUpCallBack = new List<UnityAction>();

	// Token: 0x04001639 RID: 5689
	private List<UnityAction> MouseEnterCallBack = new List<UnityAction>();

	// Token: 0x0400163A RID: 5690
	private List<UnityAction> MouseExitCallBack = new List<UnityAction>();

	// Token: 0x0400163B RID: 5691
	public bool Disable;
}
