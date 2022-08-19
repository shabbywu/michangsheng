using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using YSGame.TuJian;

// Token: 0x02000386 RID: 902
[RequireComponent(typeof(Button))]
public class SSVItem : MonoBehaviour
{
	// Token: 0x1700025A RID: 602
	// (get) Token: 0x06001DCB RID: 7627 RVA: 0x000D23CE File Offset: 0x000D05CE
	// (set) Token: 0x06001DCC RID: 7628 RVA: 0x000D23D6 File Offset: 0x000D05D6
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

	// Token: 0x1700025B RID: 603
	// (get) Token: 0x06001DCD RID: 7629 RVA: 0x000D2400 File Offset: 0x000D0600
	// (set) Token: 0x06001DCE RID: 7630 RVA: 0x000D2421 File Offset: 0x000D0621
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

	// Token: 0x06001DCF RID: 7631 RVA: 0x000D244C File Offset: 0x000D064C
	public virtual void Awake()
	{
		this._button = base.GetComponent<Button>();
		this._text = base.GetComponentInChildren<Text>();
	}

	// Token: 0x06001DD0 RID: 7632 RVA: 0x00004095 File Offset: 0x00002295
	public virtual void Start()
	{
	}

	// Token: 0x06001DD1 RID: 7633 RVA: 0x000D2466 File Offset: 0x000D0666
	public virtual void Update()
	{
		if (this._needUpdateText && this._text != null)
		{
			this._text.text = this._updateTextCache;
			this._needUpdateText = false;
		}
	}

	// Token: 0x04001871 RID: 6257
	[HideInInspector]
	public SuperScrollView SSV;

	// Token: 0x04001872 RID: 6258
	[HideInInspector]
	public List<Dictionary<int, string>> DataList;

	// Token: 0x04001873 RID: 6259
	protected int _DataIndex;

	// Token: 0x04001874 RID: 6260
	protected Button _button;

	// Token: 0x04001875 RID: 6261
	protected Text _text;

	// Token: 0x04001876 RID: 6262
	protected bool _needUpdateText;

	// Token: 0x04001877 RID: 6263
	protected string _updateTextCache;
}
