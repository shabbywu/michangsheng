using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using YSGame.TuJian;

// Token: 0x0200050A RID: 1290
[RequireComponent(typeof(Button))]
public class SSVItem : MonoBehaviour
{
	// Token: 0x170002AC RID: 684
	// (get) Token: 0x06002144 RID: 8516 RVA: 0x0001B69C File Offset: 0x0001989C
	// (set) Token: 0x06002145 RID: 8517 RVA: 0x0001B6A4 File Offset: 0x000198A4
	public int DataIndex
	{
		get
		{
			return this._DataIndex;
		}
		set
		{
			this._DataIndex = value;
			this.ShowText = this.DataList[this._DataIndex].Values.First<string>();
		}
	}

	// Token: 0x170002AD RID: 685
	// (get) Token: 0x06002146 RID: 8518 RVA: 0x0001B6CE File Offset: 0x000198CE
	// (set) Token: 0x06002147 RID: 8519 RVA: 0x0001B6EF File Offset: 0x000198EF
	public string ShowText
	{
		get
		{
			if (this._text == null)
			{
				return "";
			}
			return this._text.text;
		}
		set
		{
			if (this._text != null)
			{
				this._text.text = value;
				return;
			}
			this._updateTextCache = value;
			this._needUpdateText = true;
		}
	}

	// Token: 0x06002148 RID: 8520 RVA: 0x0001B71A File Offset: 0x0001991A
	public virtual void Awake()
	{
		this._button = base.GetComponent<Button>();
		this._text = base.GetComponentInChildren<Text>();
	}

	// Token: 0x06002149 RID: 8521 RVA: 0x000042DD File Offset: 0x000024DD
	public virtual void Start()
	{
	}

	// Token: 0x0600214A RID: 8522 RVA: 0x0001B734 File Offset: 0x00019934
	public virtual void Update()
	{
		if (this._needUpdateText && this._text != null)
		{
			this._text.text = this._updateTextCache;
			this._needUpdateText = false;
		}
	}

	// Token: 0x04001CCF RID: 7375
	[HideInInspector]
	public SuperScrollView SSV;

	// Token: 0x04001CD0 RID: 7376
	[HideInInspector]
	public List<Dictionary<int, string>> DataList;

	// Token: 0x04001CD1 RID: 7377
	protected int _DataIndex;

	// Token: 0x04001CD2 RID: 7378
	protected Button _button;

	// Token: 0x04001CD3 RID: 7379
	protected Text _text;

	// Token: 0x04001CD4 RID: 7380
	protected bool _needUpdateText;

	// Token: 0x04001CD5 RID: 7381
	protected string _updateTextCache;
}
