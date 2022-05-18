using System;
using UnityEngine;

// Token: 0x02000022 RID: 34
[Serializable]
public class LTRect
{
	// Token: 0x06000152 RID: 338 RVA: 0x00061B68 File Offset: 0x0005FD68
	public LTRect()
	{
		this.reset();
		this.rotateEnabled = (this.alphaEnabled = true);
		this._rect = new Rect(0f, 0f, 1f, 1f);
	}

	// Token: 0x06000153 RID: 339 RVA: 0x00061BEC File Offset: 0x0005FDEC
	public LTRect(Rect rect)
	{
		this._rect = rect;
		this.reset();
	}

	// Token: 0x06000154 RID: 340 RVA: 0x00061C48 File Offset: 0x0005FE48
	public LTRect(float x, float y, float width, float height)
	{
		this._rect = new Rect(x, y, width, height);
		this.alpha = 1f;
		this.rotation = 0f;
		this.rotateEnabled = (this.alphaEnabled = false);
	}

	// Token: 0x06000155 RID: 341 RVA: 0x00061CD0 File Offset: 0x0005FED0
	public LTRect(float x, float y, float width, float height, float alpha)
	{
		this._rect = new Rect(x, y, width, height);
		this.alpha = alpha;
		this.rotation = 0f;
		this.rotateEnabled = (this.alphaEnabled = false);
	}

	// Token: 0x06000156 RID: 342 RVA: 0x00061D54 File Offset: 0x0005FF54
	public LTRect(float x, float y, float width, float height, float alpha, float rotation)
	{
		this._rect = new Rect(x, y, width, height);
		this.alpha = alpha;
		this.rotation = rotation;
		this.rotateEnabled = (this.alphaEnabled = false);
		if (rotation != 0f)
		{
			this.rotateEnabled = true;
			this.resetForRotation();
		}
	}

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x06000157 RID: 343 RVA: 0x000050FA File Offset: 0x000032FA
	public bool hasInitiliazed
	{
		get
		{
			return this._id != -1;
		}
	}

	// Token: 0x17000019 RID: 25
	// (get) Token: 0x06000158 RID: 344 RVA: 0x00005108 File Offset: 0x00003308
	public int id
	{
		get
		{
			return this._id | this.counter << 16;
		}
	}

	// Token: 0x06000159 RID: 345 RVA: 0x0000511A File Offset: 0x0000331A
	public void setId(int id, int counter)
	{
		this._id = id;
		this.counter = counter;
	}

	// Token: 0x0600015A RID: 346 RVA: 0x00061DEC File Offset: 0x0005FFEC
	public void reset()
	{
		this.alpha = 1f;
		this.rotation = 0f;
		this.rotateEnabled = (this.alphaEnabled = false);
		this.margin = Vector2.zero;
		this.sizeByHeight = false;
		this.useColor = false;
	}

	// Token: 0x0600015B RID: 347 RVA: 0x00061E38 File Offset: 0x00060038
	public void resetForRotation()
	{
		Vector3 vector;
		vector..ctor(GUI.matrix[0, 0], GUI.matrix[1, 1], GUI.matrix[2, 2]);
		if (this.pivot == Vector2.zero)
		{
			this.pivot = new Vector2((this._rect.x + this._rect.width * 0.5f) * vector.x + GUI.matrix[0, 3], (this._rect.y + this._rect.height * 0.5f) * vector.y + GUI.matrix[1, 3]);
		}
	}

	// Token: 0x1700001A RID: 26
	// (get) Token: 0x0600015C RID: 348 RVA: 0x0000512A File Offset: 0x0000332A
	// (set) Token: 0x0600015D RID: 349 RVA: 0x00005137 File Offset: 0x00003337
	public float x
	{
		get
		{
			return this._rect.x;
		}
		set
		{
			this._rect.x = value;
		}
	}

	// Token: 0x1700001B RID: 27
	// (get) Token: 0x0600015E RID: 350 RVA: 0x00005145 File Offset: 0x00003345
	// (set) Token: 0x0600015F RID: 351 RVA: 0x00005152 File Offset: 0x00003352
	public float y
	{
		get
		{
			return this._rect.y;
		}
		set
		{
			this._rect.y = value;
		}
	}

	// Token: 0x1700001C RID: 28
	// (get) Token: 0x06000160 RID: 352 RVA: 0x00005160 File Offset: 0x00003360
	// (set) Token: 0x06000161 RID: 353 RVA: 0x0000516D File Offset: 0x0000336D
	public float width
	{
		get
		{
			return this._rect.width;
		}
		set
		{
			this._rect.width = value;
		}
	}

	// Token: 0x1700001D RID: 29
	// (get) Token: 0x06000162 RID: 354 RVA: 0x0000517B File Offset: 0x0000337B
	// (set) Token: 0x06000163 RID: 355 RVA: 0x00005188 File Offset: 0x00003388
	public float height
	{
		get
		{
			return this._rect.height;
		}
		set
		{
			this._rect.height = value;
		}
	}

	// Token: 0x1700001E RID: 30
	// (get) Token: 0x06000164 RID: 356 RVA: 0x00061F00 File Offset: 0x00060100
	// (set) Token: 0x06000165 RID: 357 RVA: 0x00005196 File Offset: 0x00003396
	public Rect rect
	{
		get
		{
			if (LTRect.colorTouched)
			{
				LTRect.colorTouched = false;
				GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, 1f);
			}
			if (this.rotateEnabled)
			{
				if (this.rotateFinished)
				{
					this.rotateFinished = false;
					this.rotateEnabled = false;
					this.pivot = Vector2.zero;
				}
				else
				{
					GUIUtility.RotateAroundPivot(this.rotation, this.pivot);
				}
			}
			if (this.alphaEnabled)
			{
				GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, this.alpha);
				LTRect.colorTouched = true;
			}
			if (this.fontScaleToFit)
			{
				if (this.useSimpleScale)
				{
					this.style.fontSize = (int)(this._rect.height * this.relativeRect.height);
				}
				else
				{
					this.style.fontSize = (int)this._rect.height;
				}
			}
			return this._rect;
		}
		set
		{
			this._rect = value;
		}
	}

	// Token: 0x06000166 RID: 358 RVA: 0x0000519F File Offset: 0x0000339F
	public LTRect setStyle(GUIStyle style)
	{
		this.style = style;
		return this;
	}

	// Token: 0x06000167 RID: 359 RVA: 0x000051A9 File Offset: 0x000033A9
	public LTRect setFontScaleToFit(bool fontScaleToFit)
	{
		this.fontScaleToFit = fontScaleToFit;
		return this;
	}

	// Token: 0x06000168 RID: 360 RVA: 0x000051B3 File Offset: 0x000033B3
	public LTRect setColor(Color color)
	{
		this.color = color;
		this.useColor = true;
		return this;
	}

	// Token: 0x06000169 RID: 361 RVA: 0x000051C4 File Offset: 0x000033C4
	public LTRect setAlpha(float alpha)
	{
		this.alpha = alpha;
		return this;
	}

	// Token: 0x0600016A RID: 362 RVA: 0x000051CE File Offset: 0x000033CE
	public LTRect setLabel(string str)
	{
		this.labelStr = str;
		return this;
	}

	// Token: 0x0600016B RID: 363 RVA: 0x000051D8 File Offset: 0x000033D8
	public LTRect setUseSimpleScale(bool useSimpleScale, Rect relativeRect)
	{
		this.useSimpleScale = useSimpleScale;
		this.relativeRect = relativeRect;
		return this;
	}

	// Token: 0x0600016C RID: 364 RVA: 0x000051E9 File Offset: 0x000033E9
	public LTRect setUseSimpleScale(bool useSimpleScale)
	{
		this.useSimpleScale = useSimpleScale;
		this.relativeRect = new Rect(0f, 0f, (float)Screen.width, (float)Screen.height);
		return this;
	}

	// Token: 0x0600016D RID: 365 RVA: 0x00005214 File Offset: 0x00003414
	public LTRect setSizeByHeight(bool sizeByHeight)
	{
		this.sizeByHeight = sizeByHeight;
		return this;
	}

	// Token: 0x0600016E RID: 366 RVA: 0x00062014 File Offset: 0x00060214
	public override string ToString()
	{
		return string.Concat(new object[]
		{
			"x:",
			this._rect.x,
			" y:",
			this._rect.y,
			" width:",
			this._rect.width,
			" height:",
			this._rect.height
		});
	}

	// Token: 0x0400010E RID: 270
	public Rect _rect;

	// Token: 0x0400010F RID: 271
	public float alpha = 1f;

	// Token: 0x04000110 RID: 272
	public float rotation;

	// Token: 0x04000111 RID: 273
	public Vector2 pivot;

	// Token: 0x04000112 RID: 274
	public Vector2 margin;

	// Token: 0x04000113 RID: 275
	public Rect relativeRect = new Rect(0f, 0f, float.PositiveInfinity, float.PositiveInfinity);

	// Token: 0x04000114 RID: 276
	public bool rotateEnabled;

	// Token: 0x04000115 RID: 277
	[HideInInspector]
	public bool rotateFinished;

	// Token: 0x04000116 RID: 278
	public bool alphaEnabled;

	// Token: 0x04000117 RID: 279
	public string labelStr;

	// Token: 0x04000118 RID: 280
	public LTGUI.Element_Type type;

	// Token: 0x04000119 RID: 281
	public GUIStyle style;

	// Token: 0x0400011A RID: 282
	public bool useColor;

	// Token: 0x0400011B RID: 283
	public Color color = Color.white;

	// Token: 0x0400011C RID: 284
	public bool fontScaleToFit;

	// Token: 0x0400011D RID: 285
	public bool useSimpleScale;

	// Token: 0x0400011E RID: 286
	public bool sizeByHeight;

	// Token: 0x0400011F RID: 287
	public Texture texture;

	// Token: 0x04000120 RID: 288
	private int _id = -1;

	// Token: 0x04000121 RID: 289
	[HideInInspector]
	public int counter;

	// Token: 0x04000122 RID: 290
	public static bool colorTouched;
}
