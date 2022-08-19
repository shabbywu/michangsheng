using System;
using UnityEngine;

// Token: 0x0200001E RID: 30
[Serializable]
public class LTRect
{
	// Token: 0x0600014C RID: 332 RVA: 0x00008264 File Offset: 0x00006464
	public LTRect()
	{
		this.reset();
		this.rotateEnabled = (this.alphaEnabled = true);
		this._rect = new Rect(0f, 0f, 1f, 1f);
	}

	// Token: 0x0600014D RID: 333 RVA: 0x000082E8 File Offset: 0x000064E8
	public LTRect(Rect rect)
	{
		this._rect = rect;
		this.reset();
	}

	// Token: 0x0600014E RID: 334 RVA: 0x00008344 File Offset: 0x00006544
	public LTRect(float x, float y, float width, float height)
	{
		this._rect = new Rect(x, y, width, height);
		this.alpha = 1f;
		this.rotation = 0f;
		this.rotateEnabled = (this.alphaEnabled = false);
	}

	// Token: 0x0600014F RID: 335 RVA: 0x000083CC File Offset: 0x000065CC
	public LTRect(float x, float y, float width, float height, float alpha)
	{
		this._rect = new Rect(x, y, width, height);
		this.alpha = alpha;
		this.rotation = 0f;
		this.rotateEnabled = (this.alphaEnabled = false);
	}

	// Token: 0x06000150 RID: 336 RVA: 0x00008450 File Offset: 0x00006650
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

	// Token: 0x17000016 RID: 22
	// (get) Token: 0x06000151 RID: 337 RVA: 0x000084E5 File Offset: 0x000066E5
	public bool hasInitiliazed
	{
		get
		{
			return this._id != -1;
		}
	}

	// Token: 0x17000017 RID: 23
	// (get) Token: 0x06000152 RID: 338 RVA: 0x000084F3 File Offset: 0x000066F3
	public int id
	{
		get
		{
			return this._id | this.counter << 16;
		}
	}

	// Token: 0x06000153 RID: 339 RVA: 0x00008505 File Offset: 0x00006705
	public void setId(int id, int counter)
	{
		this._id = id;
		this.counter = counter;
	}

	// Token: 0x06000154 RID: 340 RVA: 0x00008518 File Offset: 0x00006718
	public void reset()
	{
		this.alpha = 1f;
		this.rotation = 0f;
		this.rotateEnabled = (this.alphaEnabled = false);
		this.margin = Vector2.zero;
		this.sizeByHeight = false;
		this.useColor = false;
	}

	// Token: 0x06000155 RID: 341 RVA: 0x00008564 File Offset: 0x00006764
	public void resetForRotation()
	{
		Vector3 vector;
		vector..ctor(GUI.matrix[0, 0], GUI.matrix[1, 1], GUI.matrix[2, 2]);
		if (this.pivot == Vector2.zero)
		{
			this.pivot = new Vector2((this._rect.x + this._rect.width * 0.5f) * vector.x + GUI.matrix[0, 3], (this._rect.y + this._rect.height * 0.5f) * vector.y + GUI.matrix[1, 3]);
		}
	}

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x06000156 RID: 342 RVA: 0x0000862A File Offset: 0x0000682A
	// (set) Token: 0x06000157 RID: 343 RVA: 0x00008637 File Offset: 0x00006837
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

	// Token: 0x17000019 RID: 25
	// (get) Token: 0x06000158 RID: 344 RVA: 0x00008645 File Offset: 0x00006845
	// (set) Token: 0x06000159 RID: 345 RVA: 0x00008652 File Offset: 0x00006852
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

	// Token: 0x1700001A RID: 26
	// (get) Token: 0x0600015A RID: 346 RVA: 0x00008660 File Offset: 0x00006860
	// (set) Token: 0x0600015B RID: 347 RVA: 0x0000866D File Offset: 0x0000686D
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

	// Token: 0x1700001B RID: 27
	// (get) Token: 0x0600015C RID: 348 RVA: 0x0000867B File Offset: 0x0000687B
	// (set) Token: 0x0600015D RID: 349 RVA: 0x00008688 File Offset: 0x00006888
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

	// Token: 0x1700001C RID: 28
	// (get) Token: 0x0600015E RID: 350 RVA: 0x00008698 File Offset: 0x00006898
	// (set) Token: 0x0600015F RID: 351 RVA: 0x000087A9 File Offset: 0x000069A9
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

	// Token: 0x06000160 RID: 352 RVA: 0x000087B2 File Offset: 0x000069B2
	public LTRect setStyle(GUIStyle style)
	{
		this.style = style;
		return this;
	}

	// Token: 0x06000161 RID: 353 RVA: 0x000087BC File Offset: 0x000069BC
	public LTRect setFontScaleToFit(bool fontScaleToFit)
	{
		this.fontScaleToFit = fontScaleToFit;
		return this;
	}

	// Token: 0x06000162 RID: 354 RVA: 0x000087C6 File Offset: 0x000069C6
	public LTRect setColor(Color color)
	{
		this.color = color;
		this.useColor = true;
		return this;
	}

	// Token: 0x06000163 RID: 355 RVA: 0x000087D7 File Offset: 0x000069D7
	public LTRect setAlpha(float alpha)
	{
		this.alpha = alpha;
		return this;
	}

	// Token: 0x06000164 RID: 356 RVA: 0x000087E1 File Offset: 0x000069E1
	public LTRect setLabel(string str)
	{
		this.labelStr = str;
		return this;
	}

	// Token: 0x06000165 RID: 357 RVA: 0x000087EB File Offset: 0x000069EB
	public LTRect setUseSimpleScale(bool useSimpleScale, Rect relativeRect)
	{
		this.useSimpleScale = useSimpleScale;
		this.relativeRect = relativeRect;
		return this;
	}

	// Token: 0x06000166 RID: 358 RVA: 0x000087FC File Offset: 0x000069FC
	public LTRect setUseSimpleScale(bool useSimpleScale)
	{
		this.useSimpleScale = useSimpleScale;
		this.relativeRect = new Rect(0f, 0f, (float)Screen.width, (float)Screen.height);
		return this;
	}

	// Token: 0x06000167 RID: 359 RVA: 0x00008827 File Offset: 0x00006A27
	public LTRect setSizeByHeight(bool sizeByHeight)
	{
		this.sizeByHeight = sizeByHeight;
		return this;
	}

	// Token: 0x06000168 RID: 360 RVA: 0x00008834 File Offset: 0x00006A34
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

	// Token: 0x040000FF RID: 255
	public Rect _rect;

	// Token: 0x04000100 RID: 256
	public float alpha = 1f;

	// Token: 0x04000101 RID: 257
	public float rotation;

	// Token: 0x04000102 RID: 258
	public Vector2 pivot;

	// Token: 0x04000103 RID: 259
	public Vector2 margin;

	// Token: 0x04000104 RID: 260
	public Rect relativeRect = new Rect(0f, 0f, float.PositiveInfinity, float.PositiveInfinity);

	// Token: 0x04000105 RID: 261
	public bool rotateEnabled;

	// Token: 0x04000106 RID: 262
	[HideInInspector]
	public bool rotateFinished;

	// Token: 0x04000107 RID: 263
	public bool alphaEnabled;

	// Token: 0x04000108 RID: 264
	public string labelStr;

	// Token: 0x04000109 RID: 265
	public LTGUI.Element_Type type;

	// Token: 0x0400010A RID: 266
	public GUIStyle style;

	// Token: 0x0400010B RID: 267
	public bool useColor;

	// Token: 0x0400010C RID: 268
	public Color color = Color.white;

	// Token: 0x0400010D RID: 269
	public bool fontScaleToFit;

	// Token: 0x0400010E RID: 270
	public bool useSimpleScale;

	// Token: 0x0400010F RID: 271
	public bool sizeByHeight;

	// Token: 0x04000110 RID: 272
	public Texture texture;

	// Token: 0x04000111 RID: 273
	private int _id = -1;

	// Token: 0x04000112 RID: 274
	[HideInInspector]
	public int counter;

	// Token: 0x04000113 RID: 275
	public static bool colorTouched;
}
