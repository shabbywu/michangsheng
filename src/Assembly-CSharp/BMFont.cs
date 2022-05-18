using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B4 RID: 180
[Serializable]
public class BMFont
{
	// Token: 0x170000C2 RID: 194
	// (get) Token: 0x060006AA RID: 1706 RVA: 0x00009CA4 File Offset: 0x00007EA4
	public bool isValid
	{
		get
		{
			return this.mSaved.Count > 0;
		}
	}

	// Token: 0x170000C3 RID: 195
	// (get) Token: 0x060006AB RID: 1707 RVA: 0x00009CB4 File Offset: 0x00007EB4
	// (set) Token: 0x060006AC RID: 1708 RVA: 0x00009CBC File Offset: 0x00007EBC
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

	// Token: 0x170000C4 RID: 196
	// (get) Token: 0x060006AD RID: 1709 RVA: 0x00009CC5 File Offset: 0x00007EC5
	// (set) Token: 0x060006AE RID: 1710 RVA: 0x00009CCD File Offset: 0x00007ECD
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

	// Token: 0x170000C5 RID: 197
	// (get) Token: 0x060006AF RID: 1711 RVA: 0x00009CD6 File Offset: 0x00007ED6
	// (set) Token: 0x060006B0 RID: 1712 RVA: 0x00009CDE File Offset: 0x00007EDE
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

	// Token: 0x170000C6 RID: 198
	// (get) Token: 0x060006B1 RID: 1713 RVA: 0x00009CE7 File Offset: 0x00007EE7
	// (set) Token: 0x060006B2 RID: 1714 RVA: 0x00009CEF File Offset: 0x00007EEF
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

	// Token: 0x170000C7 RID: 199
	// (get) Token: 0x060006B3 RID: 1715 RVA: 0x00009CF8 File Offset: 0x00007EF8
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

	// Token: 0x170000C8 RID: 200
	// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00009D0F File Offset: 0x00007F0F
	// (set) Token: 0x060006B5 RID: 1717 RVA: 0x00009D17 File Offset: 0x00007F17
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

	// Token: 0x170000C9 RID: 201
	// (get) Token: 0x060006B6 RID: 1718 RVA: 0x00009D20 File Offset: 0x00007F20
	public List<BMGlyph> glyphs
	{
		get
		{
			return this.mSaved;
		}
	}

	// Token: 0x060006B7 RID: 1719 RVA: 0x00078F5C File Offset: 0x0007715C
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

	// Token: 0x060006B8 RID: 1720 RVA: 0x00009D28 File Offset: 0x00007F28
	public BMGlyph GetGlyph(int index)
	{
		return this.GetGlyph(index, false);
	}

	// Token: 0x060006B9 RID: 1721 RVA: 0x00009D32 File Offset: 0x00007F32
	public void Clear()
	{
		this.mDict.Clear();
		this.mSaved.Clear();
	}

	// Token: 0x060006BA RID: 1722 RVA: 0x00078FEC File Offset: 0x000771EC
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

	// Token: 0x040004FC RID: 1276
	[HideInInspector]
	[SerializeField]
	private int mSize = 16;

	// Token: 0x040004FD RID: 1277
	[HideInInspector]
	[SerializeField]
	private int mBase;

	// Token: 0x040004FE RID: 1278
	[HideInInspector]
	[SerializeField]
	private int mWidth;

	// Token: 0x040004FF RID: 1279
	[HideInInspector]
	[SerializeField]
	private int mHeight;

	// Token: 0x04000500 RID: 1280
	[HideInInspector]
	[SerializeField]
	private string mSpriteName;

	// Token: 0x04000501 RID: 1281
	[HideInInspector]
	[SerializeField]
	private List<BMGlyph> mSaved = new List<BMGlyph>();

	// Token: 0x04000502 RID: 1282
	private Dictionary<int, BMGlyph> mDict = new Dictionary<int, BMGlyph>();
}
