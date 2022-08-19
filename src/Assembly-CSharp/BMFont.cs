using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200007E RID: 126
[Serializable]
public class BMFont
{
	// Token: 0x170000B0 RID: 176
	// (get) Token: 0x06000630 RID: 1584 RVA: 0x00023515 File Offset: 0x00021715
	public bool isValid
	{
		get
		{
			return this.mSaved.Count > 0;
		}
	}

	// Token: 0x170000B1 RID: 177
	// (get) Token: 0x06000631 RID: 1585 RVA: 0x00023525 File Offset: 0x00021725
	// (set) Token: 0x06000632 RID: 1586 RVA: 0x0002352D File Offset: 0x0002172D
	public int charSize
	{
		get
		{
			return this.mSize;
		}
		set
		{
			this.mSize = value;
		}
	}

	// Token: 0x170000B2 RID: 178
	// (get) Token: 0x06000633 RID: 1587 RVA: 0x00023536 File Offset: 0x00021736
	// (set) Token: 0x06000634 RID: 1588 RVA: 0x0002353E File Offset: 0x0002173E
	public int baseOffset
	{
		get
		{
			return this.mBase;
		}
		set
		{
			this.mBase = value;
		}
	}

	// Token: 0x170000B3 RID: 179
	// (get) Token: 0x06000635 RID: 1589 RVA: 0x00023547 File Offset: 0x00021747
	// (set) Token: 0x06000636 RID: 1590 RVA: 0x0002354F File Offset: 0x0002174F
	public int texWidth
	{
		get
		{
			return this.mWidth;
		}
		set
		{
			this.mWidth = value;
		}
	}

	// Token: 0x170000B4 RID: 180
	// (get) Token: 0x06000637 RID: 1591 RVA: 0x00023558 File Offset: 0x00021758
	// (set) Token: 0x06000638 RID: 1592 RVA: 0x00023560 File Offset: 0x00021760
	public int texHeight
	{
		get
		{
			return this.mHeight;
		}
		set
		{
			this.mHeight = value;
		}
	}

	// Token: 0x170000B5 RID: 181
	// (get) Token: 0x06000639 RID: 1593 RVA: 0x00023569 File Offset: 0x00021769
	public int glyphCount
	{
		get
		{
			if (!this.isValid)
			{
				return 0;
			}
			return this.mSaved.Count;
		}
	}

	// Token: 0x170000B6 RID: 182
	// (get) Token: 0x0600063A RID: 1594 RVA: 0x00023580 File Offset: 0x00021780
	// (set) Token: 0x0600063B RID: 1595 RVA: 0x00023588 File Offset: 0x00021788
	public string spriteName
	{
		get
		{
			return this.mSpriteName;
		}
		set
		{
			this.mSpriteName = value;
		}
	}

	// Token: 0x170000B7 RID: 183
	// (get) Token: 0x0600063C RID: 1596 RVA: 0x00023591 File Offset: 0x00021791
	public List<BMGlyph> glyphs
	{
		get
		{
			return this.mSaved;
		}
	}

	// Token: 0x0600063D RID: 1597 RVA: 0x0002359C File Offset: 0x0002179C
	public BMGlyph GetGlyph(int index, bool createIfMissing)
	{
		BMGlyph bmglyph = null;
		if (this.mDict.Count == 0)
		{
			int i = 0;
			int count = this.mSaved.Count;
			while (i < count)
			{
				BMGlyph bmglyph2 = this.mSaved[i];
				this.mDict.Add(bmglyph2.index, bmglyph2);
				i++;
			}
		}
		if (!this.mDict.TryGetValue(index, out bmglyph) && createIfMissing)
		{
			bmglyph = new BMGlyph();
			bmglyph.index = index;
			this.mSaved.Add(bmglyph);
			this.mDict.Add(index, bmglyph);
		}
		return bmglyph;
	}

	// Token: 0x0600063E RID: 1598 RVA: 0x0002362B File Offset: 0x0002182B
	public BMGlyph GetGlyph(int index)
	{
		return this.GetGlyph(index, false);
	}

	// Token: 0x0600063F RID: 1599 RVA: 0x00023635 File Offset: 0x00021835
	public void Clear()
	{
		this.mDict.Clear();
		this.mSaved.Clear();
	}

	// Token: 0x06000640 RID: 1600 RVA: 0x00023650 File Offset: 0x00021850
	public void Trim(int xMin, int yMin, int xMax, int yMax)
	{
		if (this.isValid)
		{
			int i = 0;
			int count = this.mSaved.Count;
			while (i < count)
			{
				BMGlyph bmglyph = this.mSaved[i];
				if (bmglyph != null)
				{
					bmglyph.Trim(xMin, yMin, xMax, yMax);
				}
				i++;
			}
		}
	}

	// Token: 0x04000428 RID: 1064
	[HideInInspector]
	[SerializeField]
	private int mSize = 16;

	// Token: 0x04000429 RID: 1065
	[HideInInspector]
	[SerializeField]
	private int mBase;

	// Token: 0x0400042A RID: 1066
	[HideInInspector]
	[SerializeField]
	private int mWidth;

	// Token: 0x0400042B RID: 1067
	[HideInInspector]
	[SerializeField]
	private int mHeight;

	// Token: 0x0400042C RID: 1068
	[HideInInspector]
	[SerializeField]
	private string mSpriteName;

	// Token: 0x0400042D RID: 1069
	[HideInInspector]
	[SerializeField]
	private List<BMGlyph> mSaved = new List<BMGlyph>();

	// Token: 0x0400042E RID: 1070
	private Dictionary<int, BMGlyph> mDict = new Dictionary<int, BMGlyph>();
}
