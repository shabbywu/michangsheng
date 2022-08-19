using System;
using UltimateSurvival;
using UnityEngine;

// Token: 0x02000431 RID: 1073
public class UI_Backgroud : UltimateSurvival.MonoSingleton<UI_Backgroud>
{
	// Token: 0x06002238 RID: 8760 RVA: 0x000EBECE File Offset: 0x000EA0CE
	private void Start()
	{
		UltimateSurvival.MonoSingleton<UI_Backgroud>.Instance.gameObject.SetActive(false);
	}

	// Token: 0x17000287 RID: 647
	// (get) Token: 0x06002239 RID: 8761 RVA: 0x000EBEE0 File Offset: 0x000EA0E0
	// (set) Token: 0x0600223A RID: 8762 RVA: 0x000EBEE8 File Offset: 0x000EA0E8
	public bool Value
	{
		get
		{
			return this._value;
		}
		set
		{
			this._value = value;
			base.gameObject.SetActive(value);
		}
	}

	// Token: 0x0600223B RID: 8763 RVA: 0x000EBEFD File Offset: 0x000EA0FD
	public void OpenLayer(GameObject obj, bool _v)
	{
		this.Value = _v;
		this.playOpen(obj);
	}

	// Token: 0x0600223C RID: 8764 RVA: 0x000EBF10 File Offset: 0x000EA110
	public void playOpen(GameObject obj)
	{
		obj.transform.localScale = Vector3.zero;
		iTween.ScaleTo(obj, iTween.Hash(new object[]
		{
			"x",
			1,
			"y",
			1,
			"z",
			1,
			"time",
			1.0,
			"islocal",
			true
		}));
	}

	// Token: 0x04001BB7 RID: 7095
	private bool _value;
}
