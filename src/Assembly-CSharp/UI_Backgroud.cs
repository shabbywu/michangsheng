using System;
using UltimateSurvival;
using UnityEngine;

// Token: 0x020005E8 RID: 1512
public class UI_Backgroud : UltimateSurvival.MonoSingleton<UI_Backgroud>
{
	// Token: 0x060025F7 RID: 9719 RVA: 0x0001E576 File Offset: 0x0001C776
	private void Start()
	{
		UltimateSurvival.MonoSingleton<UI_Backgroud>.Instance.gameObject.SetActive(false);
	}

	// Token: 0x170002D1 RID: 721
	// (get) Token: 0x060025F8 RID: 9720 RVA: 0x0001E588 File Offset: 0x0001C788
	// (set) Token: 0x060025F9 RID: 9721 RVA: 0x0001E590 File Offset: 0x0001C790
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

	// Token: 0x060025FA RID: 9722 RVA: 0x0001E5A5 File Offset: 0x0001C7A5
	public void OpenLayer(GameObject obj, bool _v)
	{
		this.Value = _v;
		this.playOpen(obj);
	}

	// Token: 0x060025FB RID: 9723 RVA: 0x0012D068 File Offset: 0x0012B268
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

	// Token: 0x04002083 RID: 8323
	private bool _value;
}
