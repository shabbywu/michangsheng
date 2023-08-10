using System;
using UnityEngine;

public abstract class UIBasicSprite : UIWidget
{
	public enum Type
	{
		Simple,
		Sliced,
		Tiled,
		Filled,
		Advanced
	}

	public enum FillDirection
	{
		Horizontal,
		Vertical,
		Radial90,
		Radial180,
		Radial360
	}

	public enum AdvancedType
	{
		Invisible,
		Sliced,
		Tiled
	}

	public enum Flip
	{
		Nothing,
		Horizontally,
		Vertically,
		Both
	}

	[HideInInspector]
	[SerializeField]
	protected Type mType;

	[HideInInspector]
	[SerializeField]
	protected FillDirection mFillDirection = FillDirection.Radial360;

	[Range(0f, 1f)]
	[HideInInspector]
	[SerializeField]
	protected float mFillAmount = 1f;

	[HideInInspector]
	[SerializeField]
	protected bool mInvert;

	[HideInInspector]
	[SerializeField]
	protected Flip mFlip;

	[NonSerialized]
	private Rect mInnerUV;

	[NonSerialized]
	private Rect mOuterUV;

	public AdvancedType centerType = AdvancedType.Sliced;

	public AdvancedType leftType = AdvancedType.Sliced;

	public AdvancedType rightType = AdvancedType.Sliced;

	public AdvancedType bottomType = AdvancedType.Sliced;

	public AdvancedType topType = AdvancedType.Sliced;

	protected static Vector2[] mTempPos = (Vector2[])(object)new Vector2[4];

	protected static Vector2[] mTempUVs = (Vector2[])(object)new Vector2[4];

	public virtual Type type
	{
		get
		{
			return mType;
		}
		set
		{
			if (mType != value)
			{
				mType = value;
				MarkAsChanged();
			}
		}
	}

	public Flip flip
	{
		get
		{
			return mFlip;
		}
		set
		{
			if (mFlip != value)
			{
				mFlip = value;
				MarkAsChanged();
			}
		}
	}

	public FillDirection fillDirection
	{
		get
		{
			return mFillDirection;
		}
		set
		{
			if (mFillDirection != value)
			{
				mFillDirection = value;
				mChanged = true;
			}
		}
	}

	public float fillAmount
	{
		get
		{
			return mFillAmount;
		}
		set
		{
			float num = Mathf.Clamp01(value);
			if (mFillAmount != num)
			{
				mFillAmount = num;
				mChanged = true;
			}
		}
	}

	public override int minWidth
	{
		get
		{
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			if (type == Type.Sliced || type == Type.Advanced)
			{
				Vector4 val = border * pixelSize;
				int num = Mathf.RoundToInt(val.x + val.z);
				return Mathf.Max(base.minWidth, ((num & 1) == 1) ? (num + 1) : num);
			}
			return base.minWidth;
		}
	}

	public override int minHeight
	{
		get
		{
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			if (type == Type.Sliced || type == Type.Advanced)
			{
				Vector4 val = border * pixelSize;
				int num = Mathf.RoundToInt(val.y + val.w);
				return Mathf.Max(base.minHeight, ((num & 1) == 1) ? (num + 1) : num);
			}
			return base.minHeight;
		}
	}

	public bool invert
	{
		get
		{
			return mInvert;
		}
		set
		{
			if (mInvert != value)
			{
				mInvert = value;
				mChanged = true;
			}
		}
	}

	public bool hasBorder
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			Vector4 val = border;
			if (val.x == 0f && val.y == 0f && val.z == 0f)
			{
				return val.w != 0f;
			}
			return true;
		}
	}

	public virtual bool premultipliedAlpha => false;

	public virtual float pixelSize => 1f;

	private Vector4 drawingUVs => (Vector4)(mFlip switch
	{
		Flip.Horizontally => new Vector4(((Rect)(ref mOuterUV)).xMax, ((Rect)(ref mOuterUV)).yMin, ((Rect)(ref mOuterUV)).xMin, ((Rect)(ref mOuterUV)).yMax), 
		Flip.Vertically => new Vector4(((Rect)(ref mOuterUV)).xMin, ((Rect)(ref mOuterUV)).yMax, ((Rect)(ref mOuterUV)).xMax, ((Rect)(ref mOuterUV)).yMin), 
		Flip.Both => new Vector4(((Rect)(ref mOuterUV)).xMax, ((Rect)(ref mOuterUV)).yMax, ((Rect)(ref mOuterUV)).xMin, ((Rect)(ref mOuterUV)).yMin), 
		_ => new Vector4(((Rect)(ref mOuterUV)).xMin, ((Rect)(ref mOuterUV)).yMin, ((Rect)(ref mOuterUV)).xMax, ((Rect)(ref mOuterUV)).yMax), 
	});

	private Color32 drawingColor
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			Color val = base.color;
			val.a = finalAlpha;
			return Color32.op_Implicit(premultipliedAlpha ? NGUITools.ApplyPMA(val) : val);
		}
	}

	protected void Fill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols, Rect outer, Rect inner)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		mOuterUV = outer;
		mInnerUV = inner;
		switch (type)
		{
		case Type.Simple:
			SimpleFill(verts, uvs, cols);
			break;
		case Type.Sliced:
			SlicedFill(verts, uvs, cols);
			break;
		case Type.Filled:
			FilledFill(verts, uvs, cols);
			break;
		case Type.Tiled:
			TiledFill(verts, uvs, cols);
			break;
		case Type.Advanced:
			AdvancedFill(verts, uvs, cols);
			break;
		}
	}

	private void SimpleFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		Vector4 val = drawingDimensions;
		Vector4 val2 = drawingUVs;
		Color32 item = drawingColor;
		verts.Add(new Vector3(val.x, val.y));
		verts.Add(new Vector3(val.x, val.w));
		verts.Add(new Vector3(val.z, val.w));
		verts.Add(new Vector3(val.z, val.y));
		uvs.Add(new Vector2(val2.x, val2.y));
		uvs.Add(new Vector2(val2.x, val2.w));
		uvs.Add(new Vector2(val2.z, val2.w));
		uvs.Add(new Vector2(val2.z, val2.y));
		cols.Add(item);
		cols.Add(item);
		cols.Add(item);
		cols.Add(item);
	}

	private void SlicedFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Unknown result type (might be due to invalid IL or missing references)
		//IL_055a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0565: Unknown result type (might be due to invalid IL or missing references)
		//IL_056c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_057a: Unknown result type (might be due to invalid IL or missing references)
		Vector4 val = border * pixelSize;
		if (val.x == 0f && val.y == 0f && val.z == 0f && val.w == 0f)
		{
			SimpleFill(verts, uvs, cols);
			return;
		}
		Color32 item = drawingColor;
		Vector4 val2 = drawingDimensions;
		mTempPos[0].x = val2.x;
		mTempPos[0].y = val2.y;
		mTempPos[3].x = val2.z;
		mTempPos[3].y = val2.w;
		if (mFlip == Flip.Horizontally || mFlip == Flip.Both)
		{
			mTempPos[1].x = mTempPos[0].x + val.z;
			mTempPos[2].x = mTempPos[3].x - val.x;
			mTempUVs[3].x = ((Rect)(ref mOuterUV)).xMin;
			mTempUVs[2].x = ((Rect)(ref mInnerUV)).xMin;
			mTempUVs[1].x = ((Rect)(ref mInnerUV)).xMax;
			mTempUVs[0].x = ((Rect)(ref mOuterUV)).xMax;
		}
		else
		{
			mTempPos[1].x = mTempPos[0].x + val.x;
			mTempPos[2].x = mTempPos[3].x - val.z;
			mTempUVs[0].x = ((Rect)(ref mOuterUV)).xMin;
			mTempUVs[1].x = ((Rect)(ref mInnerUV)).xMin;
			mTempUVs[2].x = ((Rect)(ref mInnerUV)).xMax;
			mTempUVs[3].x = ((Rect)(ref mOuterUV)).xMax;
		}
		if (mFlip == Flip.Vertically || mFlip == Flip.Both)
		{
			mTempPos[1].y = mTempPos[0].y + val.w;
			mTempPos[2].y = mTempPos[3].y - val.y;
			mTempUVs[3].y = ((Rect)(ref mOuterUV)).yMin;
			mTempUVs[2].y = ((Rect)(ref mInnerUV)).yMin;
			mTempUVs[1].y = ((Rect)(ref mInnerUV)).yMax;
			mTempUVs[0].y = ((Rect)(ref mOuterUV)).yMax;
		}
		else
		{
			mTempPos[1].y = mTempPos[0].y + val.y;
			mTempPos[2].y = mTempPos[3].y - val.w;
			mTempUVs[0].y = ((Rect)(ref mOuterUV)).yMin;
			mTempUVs[1].y = ((Rect)(ref mInnerUV)).yMin;
			mTempUVs[2].y = ((Rect)(ref mInnerUV)).yMax;
			mTempUVs[3].y = ((Rect)(ref mOuterUV)).yMax;
		}
		for (int i = 0; i < 3; i++)
		{
			int num = i + 1;
			for (int j = 0; j < 3; j++)
			{
				if (centerType != 0 || i != 1 || j != 1)
				{
					int num2 = j + 1;
					verts.Add(new Vector3(mTempPos[i].x, mTempPos[j].y));
					verts.Add(new Vector3(mTempPos[i].x, mTempPos[num2].y));
					verts.Add(new Vector3(mTempPos[num].x, mTempPos[num2].y));
					verts.Add(new Vector3(mTempPos[num].x, mTempPos[j].y));
					uvs.Add(new Vector2(mTempUVs[i].x, mTempUVs[j].y));
					uvs.Add(new Vector2(mTempUVs[i].x, mTempUVs[num2].y));
					uvs.Add(new Vector2(mTempUVs[num].x, mTempUVs[num2].y));
					uvs.Add(new Vector2(mTempUVs[num].x, mTempUVs[j].y));
					cols.Add(item);
					cols.Add(item);
					cols.Add(item);
					cols.Add(item);
				}
			}
		}
	}

	private void TiledFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		Texture val = mainTexture;
		if ((Object)(object)val == (Object)null)
		{
			return;
		}
		Vector2 val2 = default(Vector2);
		((Vector2)(ref val2))._002Ector(((Rect)(ref mInnerUV)).width * (float)val.width, ((Rect)(ref mInnerUV)).height * (float)val.height);
		val2 *= pixelSize;
		if ((Object)(object)val == (Object)null || val2.x < 2f || val2.y < 2f)
		{
			return;
		}
		Color32 item = drawingColor;
		Vector4 val3 = drawingDimensions;
		Vector4 val4 = default(Vector4);
		if (mFlip == Flip.Horizontally || mFlip == Flip.Both)
		{
			val4.x = ((Rect)(ref mInnerUV)).xMax;
			val4.z = ((Rect)(ref mInnerUV)).xMin;
		}
		else
		{
			val4.x = ((Rect)(ref mInnerUV)).xMin;
			val4.z = ((Rect)(ref mInnerUV)).xMax;
		}
		if (mFlip == Flip.Vertically || mFlip == Flip.Both)
		{
			val4.y = ((Rect)(ref mInnerUV)).yMax;
			val4.w = ((Rect)(ref mInnerUV)).yMin;
		}
		else
		{
			val4.y = ((Rect)(ref mInnerUV)).yMin;
			val4.w = ((Rect)(ref mInnerUV)).yMax;
		}
		float x = val3.x;
		float num = val3.y;
		float x2 = val4.x;
		float y = val4.y;
		for (; num < val3.w; num += val2.y)
		{
			x = val3.x;
			float num2 = num + val2.y;
			float num3 = val4.w;
			if (num2 > val3.w)
			{
				num3 = Mathf.Lerp(val4.y, val4.w, (val3.w - num) / val2.y);
				num2 = val3.w;
			}
			for (; x < val3.z; x += val2.x)
			{
				float num4 = x + val2.x;
				float num5 = val4.z;
				if (num4 > val3.z)
				{
					num5 = Mathf.Lerp(val4.x, val4.z, (val3.z - x) / val2.x);
					num4 = val3.z;
				}
				verts.Add(new Vector3(x, num));
				verts.Add(new Vector3(x, num2));
				verts.Add(new Vector3(num4, num2));
				verts.Add(new Vector3(num4, num));
				uvs.Add(new Vector2(x2, y));
				uvs.Add(new Vector2(x2, num3));
				uvs.Add(new Vector2(num5, num3));
				uvs.Add(new Vector2(num5, y));
				cols.Add(item);
				cols.Add(item);
				cols.Add(item);
				cols.Add(item);
			}
		}
	}

	private void FilledFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0906: Unknown result type (might be due to invalid IL or missing references)
		//IL_0911: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0620: Unknown result type (might be due to invalid IL or missing references)
		//IL_0626: Unknown result type (might be due to invalid IL or missing references)
		//IL_0663: Unknown result type (might be due to invalid IL or missing references)
		//IL_0669: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_072c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0732: Unknown result type (might be due to invalid IL or missing references)
		//IL_076f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0775: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_057a: Unknown result type (might be due to invalid IL or missing references)
		//IL_058c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0597: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c2: Unknown result type (might be due to invalid IL or missing references)
		if (mFillAmount < 0.001f)
		{
			return;
		}
		Vector4 val = drawingDimensions;
		Vector4 val2 = drawingUVs;
		Color32 item = drawingColor;
		if (mFillDirection == FillDirection.Horizontal || mFillDirection == FillDirection.Vertical)
		{
			if (mFillDirection == FillDirection.Horizontal)
			{
				float num = (val2.z - val2.x) * mFillAmount;
				if (mInvert)
				{
					val.x = val.z - (val.z - val.x) * mFillAmount;
					val2.x = val2.z - num;
				}
				else
				{
					val.z = val.x + (val.z - val.x) * mFillAmount;
					val2.z = val2.x + num;
				}
			}
			else if (mFillDirection == FillDirection.Vertical)
			{
				float num2 = (val2.w - val2.y) * mFillAmount;
				if (mInvert)
				{
					val.y = val.w - (val.w - val.y) * mFillAmount;
					val2.y = val2.w - num2;
				}
				else
				{
					val.w = val.y + (val.w - val.y) * mFillAmount;
					val2.w = val2.y + num2;
				}
			}
		}
		mTempPos[0] = new Vector2(val.x, val.y);
		mTempPos[1] = new Vector2(val.x, val.w);
		mTempPos[2] = new Vector2(val.z, val.w);
		mTempPos[3] = new Vector2(val.z, val.y);
		mTempUVs[0] = new Vector2(val2.x, val2.y);
		mTempUVs[1] = new Vector2(val2.x, val2.w);
		mTempUVs[2] = new Vector2(val2.z, val2.w);
		mTempUVs[3] = new Vector2(val2.z, val2.y);
		if (mFillAmount < 1f)
		{
			if (mFillDirection == FillDirection.Radial90)
			{
				if (RadialCut(mTempPos, mTempUVs, mFillAmount, mInvert, 0))
				{
					for (int i = 0; i < 4; i++)
					{
						verts.Add(Vector2.op_Implicit(mTempPos[i]));
						uvs.Add(mTempUVs[i]);
						cols.Add(item);
					}
				}
				return;
			}
			if (mFillDirection == FillDirection.Radial180)
			{
				for (int j = 0; j < 2; j++)
				{
					float num3 = 0f;
					float num4 = 1f;
					float num5;
					float num6;
					if (j == 0)
					{
						num5 = 0f;
						num6 = 0.5f;
					}
					else
					{
						num5 = 0.5f;
						num6 = 1f;
					}
					mTempPos[0].x = Mathf.Lerp(val.x, val.z, num5);
					mTempPos[1].x = mTempPos[0].x;
					mTempPos[2].x = Mathf.Lerp(val.x, val.z, num6);
					mTempPos[3].x = mTempPos[2].x;
					mTempPos[0].y = Mathf.Lerp(val.y, val.w, num3);
					mTempPos[1].y = Mathf.Lerp(val.y, val.w, num4);
					mTempPos[2].y = mTempPos[1].y;
					mTempPos[3].y = mTempPos[0].y;
					mTempUVs[0].x = Mathf.Lerp(val2.x, val2.z, num5);
					mTempUVs[1].x = mTempUVs[0].x;
					mTempUVs[2].x = Mathf.Lerp(val2.x, val2.z, num6);
					mTempUVs[3].x = mTempUVs[2].x;
					mTempUVs[0].y = Mathf.Lerp(val2.y, val2.w, num3);
					mTempUVs[1].y = Mathf.Lerp(val2.y, val2.w, num4);
					mTempUVs[2].y = mTempUVs[1].y;
					mTempUVs[3].y = mTempUVs[0].y;
					float num7 = ((!mInvert) ? (fillAmount * 2f - (float)j) : (mFillAmount * 2f - (float)(1 - j)));
					if (RadialCut(mTempPos, mTempUVs, Mathf.Clamp01(num7), !mInvert, NGUIMath.RepeatIndex(j + 3, 4)))
					{
						for (int k = 0; k < 4; k++)
						{
							verts.Add(Vector2.op_Implicit(mTempPos[k]));
							uvs.Add(mTempUVs[k]);
							cols.Add(item);
						}
					}
				}
				return;
			}
			if (mFillDirection == FillDirection.Radial360)
			{
				for (int l = 0; l < 4; l++)
				{
					float num8;
					float num9;
					if (l < 2)
					{
						num8 = 0f;
						num9 = 0.5f;
					}
					else
					{
						num8 = 0.5f;
						num9 = 1f;
					}
					float num10;
					float num11;
					if (l == 0 || l == 3)
					{
						num10 = 0f;
						num11 = 0.5f;
					}
					else
					{
						num10 = 0.5f;
						num11 = 1f;
					}
					mTempPos[0].x = Mathf.Lerp(val.x, val.z, num8);
					mTempPos[1].x = mTempPos[0].x;
					mTempPos[2].x = Mathf.Lerp(val.x, val.z, num9);
					mTempPos[3].x = mTempPos[2].x;
					mTempPos[0].y = Mathf.Lerp(val.y, val.w, num10);
					mTempPos[1].y = Mathf.Lerp(val.y, val.w, num11);
					mTempPos[2].y = mTempPos[1].y;
					mTempPos[3].y = mTempPos[0].y;
					mTempUVs[0].x = Mathf.Lerp(val2.x, val2.z, num8);
					mTempUVs[1].x = mTempUVs[0].x;
					mTempUVs[2].x = Mathf.Lerp(val2.x, val2.z, num9);
					mTempUVs[3].x = mTempUVs[2].x;
					mTempUVs[0].y = Mathf.Lerp(val2.y, val2.w, num10);
					mTempUVs[1].y = Mathf.Lerp(val2.y, val2.w, num11);
					mTempUVs[2].y = mTempUVs[1].y;
					mTempUVs[3].y = mTempUVs[0].y;
					float num12 = (mInvert ? (mFillAmount * 4f - (float)NGUIMath.RepeatIndex(l + 2, 4)) : (mFillAmount * 4f - (float)(3 - NGUIMath.RepeatIndex(l + 2, 4))));
					if (RadialCut(mTempPos, mTempUVs, Mathf.Clamp01(num12), mInvert, NGUIMath.RepeatIndex(l + 2, 4)))
					{
						for (int m = 0; m < 4; m++)
						{
							verts.Add(Vector2.op_Implicit(mTempPos[m]));
							uvs.Add(mTempUVs[m]);
							cols.Add(item);
						}
					}
				}
				return;
			}
		}
		for (int n = 0; n < 4; n++)
		{
			verts.Add(Vector2.op_Implicit(mTempPos[n]));
			uvs.Add(mTempUVs[n]);
			cols.Add(item);
		}
	}

	private void AdvancedFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0683: Unknown result type (might be due to invalid IL or missing references)
		//IL_0684: Unknown result type (might be due to invalid IL or missing references)
		//IL_0530: Unknown result type (might be due to invalid IL or missing references)
		//IL_0865: Unknown result type (might be due to invalid IL or missing references)
		//IL_0866: Unknown result type (might be due to invalid IL or missing references)
		//IL_0745: Unknown result type (might be due to invalid IL or missing references)
		//IL_054c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a47: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a48: Unknown result type (might be due to invalid IL or missing references)
		//IL_093a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0797: Unknown result type (might be due to invalid IL or missing references)
		//IL_0798: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0771: Unknown result type (might be due to invalid IL or missing references)
		//IL_0563: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0979: Unknown result type (might be due to invalid IL or missing references)
		//IL_097a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0986: Unknown result type (might be due to invalid IL or missing references)
		//IL_0953: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_058f: Unknown result type (might be due to invalid IL or missing references)
		Texture val = mainTexture;
		if ((Object)(object)val == (Object)null)
		{
			return;
		}
		Vector4 val2 = border * pixelSize;
		if (val2.x == 0f && val2.y == 0f && val2.z == 0f && val2.w == 0f)
		{
			SimpleFill(verts, uvs, cols);
			return;
		}
		Color32 val3 = drawingColor;
		Vector4 val4 = drawingDimensions;
		Vector2 val5 = default(Vector2);
		((Vector2)(ref val5))._002Ector(((Rect)(ref mInnerUV)).width * (float)val.width, ((Rect)(ref mInnerUV)).height * (float)val.height);
		val5 *= pixelSize;
		if (val5.x < 1f)
		{
			val5.x = 1f;
		}
		if (val5.y < 1f)
		{
			val5.y = 1f;
		}
		mTempPos[0].x = val4.x;
		mTempPos[0].y = val4.y;
		mTempPos[3].x = val4.z;
		mTempPos[3].y = val4.w;
		if (mFlip == Flip.Horizontally || mFlip == Flip.Both)
		{
			mTempPos[1].x = mTempPos[0].x + val2.z;
			mTempPos[2].x = mTempPos[3].x - val2.x;
			mTempUVs[3].x = ((Rect)(ref mOuterUV)).xMin;
			mTempUVs[2].x = ((Rect)(ref mInnerUV)).xMin;
			mTempUVs[1].x = ((Rect)(ref mInnerUV)).xMax;
			mTempUVs[0].x = ((Rect)(ref mOuterUV)).xMax;
		}
		else
		{
			mTempPos[1].x = mTempPos[0].x + val2.x;
			mTempPos[2].x = mTempPos[3].x - val2.z;
			mTempUVs[0].x = ((Rect)(ref mOuterUV)).xMin;
			mTempUVs[1].x = ((Rect)(ref mInnerUV)).xMin;
			mTempUVs[2].x = ((Rect)(ref mInnerUV)).xMax;
			mTempUVs[3].x = ((Rect)(ref mOuterUV)).xMax;
		}
		if (mFlip == Flip.Vertically || mFlip == Flip.Both)
		{
			mTempPos[1].y = mTempPos[0].y + val2.w;
			mTempPos[2].y = mTempPos[3].y - val2.y;
			mTempUVs[3].y = ((Rect)(ref mOuterUV)).yMin;
			mTempUVs[2].y = ((Rect)(ref mInnerUV)).yMin;
			mTempUVs[1].y = ((Rect)(ref mInnerUV)).yMax;
			mTempUVs[0].y = ((Rect)(ref mOuterUV)).yMax;
		}
		else
		{
			mTempPos[1].y = mTempPos[0].y + val2.y;
			mTempPos[2].y = mTempPos[3].y - val2.w;
			mTempUVs[0].y = ((Rect)(ref mOuterUV)).yMin;
			mTempUVs[1].y = ((Rect)(ref mInnerUV)).yMin;
			mTempUVs[2].y = ((Rect)(ref mInnerUV)).yMax;
			mTempUVs[3].y = ((Rect)(ref mOuterUV)).yMax;
		}
		for (int i = 0; i < 3; i++)
		{
			int num = i + 1;
			for (int j = 0; j < 3; j++)
			{
				if (centerType == AdvancedType.Invisible && i == 1 && j == 1)
				{
					continue;
				}
				int num2 = j + 1;
				if (i == 1 && j == 1)
				{
					if (centerType == AdvancedType.Tiled)
					{
						float x = mTempPos[i].x;
						float x2 = mTempPos[num].x;
						float y = mTempPos[j].y;
						float y2 = mTempPos[num2].y;
						float x3 = mTempUVs[i].x;
						float y3 = mTempUVs[j].y;
						for (float num3 = y; num3 < y2; num3 += val5.y)
						{
							float num4 = x;
							float num5 = mTempUVs[num2].y;
							float num6 = num3 + val5.y;
							if (num6 > y2)
							{
								num5 = Mathf.Lerp(y3, num5, (y2 - num3) / val5.y);
								num6 = y2;
							}
							for (; num4 < x2; num4 += val5.x)
							{
								float num7 = num4 + val5.x;
								float num8 = mTempUVs[num].x;
								if (num7 > x2)
								{
									num8 = Mathf.Lerp(x3, num8, (x2 - num4) / val5.x);
									num7 = x2;
								}
								Fill(verts, uvs, cols, num4, num7, num3, num6, x3, num8, y3, num5, Color32.op_Implicit(val3));
							}
						}
					}
					else if (centerType == AdvancedType.Sliced)
					{
						Fill(verts, uvs, cols, mTempPos[i].x, mTempPos[num].x, mTempPos[j].y, mTempPos[num2].y, mTempUVs[i].x, mTempUVs[num].x, mTempUVs[j].y, mTempUVs[num2].y, Color32.op_Implicit(val3));
					}
				}
				else if (i == 1)
				{
					if ((j == 0 && bottomType == AdvancedType.Tiled) || (j == 2 && topType == AdvancedType.Tiled))
					{
						float x4 = mTempPos[i].x;
						float x5 = mTempPos[num].x;
						float y4 = mTempPos[j].y;
						float y5 = mTempPos[num2].y;
						float x6 = mTempUVs[i].x;
						float y6 = mTempUVs[j].y;
						float y7 = mTempUVs[num2].y;
						for (float num9 = x4; num9 < x5; num9 += val5.x)
						{
							float num10 = num9 + val5.x;
							float num11 = mTempUVs[num].x;
							if (num10 > x5)
							{
								num11 = Mathf.Lerp(x6, num11, (x5 - num9) / val5.x);
								num10 = x5;
							}
							Fill(verts, uvs, cols, num9, num10, y4, y5, x6, num11, y6, y7, Color32.op_Implicit(val3));
						}
					}
					else if ((j == 0 && bottomType == AdvancedType.Sliced) || (j == 2 && topType == AdvancedType.Sliced))
					{
						Fill(verts, uvs, cols, mTempPos[i].x, mTempPos[num].x, mTempPos[j].y, mTempPos[num2].y, mTempUVs[i].x, mTempUVs[num].x, mTempUVs[j].y, mTempUVs[num2].y, Color32.op_Implicit(val3));
					}
				}
				else if (j == 1)
				{
					if ((i == 0 && leftType == AdvancedType.Tiled) || (i == 2 && rightType == AdvancedType.Tiled))
					{
						float x7 = mTempPos[i].x;
						float x8 = mTempPos[num].x;
						float y8 = mTempPos[j].y;
						float y9 = mTempPos[num2].y;
						float x9 = mTempUVs[i].x;
						float x10 = mTempUVs[num].x;
						float y10 = mTempUVs[j].y;
						for (float num12 = y8; num12 < y9; num12 += val5.y)
						{
							float num13 = mTempUVs[num2].y;
							float num14 = num12 + val5.y;
							if (num14 > y9)
							{
								num13 = Mathf.Lerp(y10, num13, (y9 - num12) / val5.y);
								num14 = y9;
							}
							Fill(verts, uvs, cols, x7, x8, num12, num14, x9, x10, y10, num13, Color32.op_Implicit(val3));
						}
					}
					else if ((i == 0 && leftType == AdvancedType.Sliced) || (i == 2 && rightType == AdvancedType.Sliced))
					{
						Fill(verts, uvs, cols, mTempPos[i].x, mTempPos[num].x, mTempPos[j].y, mTempPos[num2].y, mTempUVs[i].x, mTempUVs[num].x, mTempUVs[j].y, mTempUVs[num2].y, Color32.op_Implicit(val3));
					}
				}
				else
				{
					Fill(verts, uvs, cols, mTempPos[i].x, mTempPos[num].x, mTempPos[j].y, mTempPos[num2].y, mTempUVs[i].x, mTempUVs[num].x, mTempUVs[j].y, mTempUVs[num2].y, Color32.op_Implicit(val3));
				}
			}
		}
	}

	private static bool RadialCut(Vector2[] xy, Vector2[] uv, float fill, bool invert, int corner)
	{
		if (fill < 0.001f)
		{
			return false;
		}
		if ((corner & 1) == 1)
		{
			invert = !invert;
		}
		if (!invert && fill > 0.999f)
		{
			return true;
		}
		float num = Mathf.Clamp01(fill);
		if (invert)
		{
			num = 1f - num;
		}
		num *= (float)Math.PI / 2f;
		float cos = Mathf.Cos(num);
		float sin = Mathf.Sin(num);
		RadialCut(xy, cos, sin, invert, corner);
		RadialCut(uv, cos, sin, invert, corner);
		return true;
	}

	private static void RadialCut(Vector2[] xy, float cos, float sin, bool invert, int corner)
	{
		int num = NGUIMath.RepeatIndex(corner + 1, 4);
		int num2 = NGUIMath.RepeatIndex(corner + 2, 4);
		int num3 = NGUIMath.RepeatIndex(corner + 3, 4);
		if ((corner & 1) == 1)
		{
			if (sin > cos)
			{
				cos /= sin;
				sin = 1f;
				if (invert)
				{
					xy[num].x = Mathf.Lerp(xy[corner].x, xy[num2].x, cos);
					xy[num2].x = xy[num].x;
				}
			}
			else if (cos > sin)
			{
				sin /= cos;
				cos = 1f;
				if (!invert)
				{
					xy[num2].y = Mathf.Lerp(xy[corner].y, xy[num2].y, sin);
					xy[num3].y = xy[num2].y;
				}
			}
			else
			{
				cos = 1f;
				sin = 1f;
			}
			if (!invert)
			{
				xy[num3].x = Mathf.Lerp(xy[corner].x, xy[num2].x, cos);
			}
			else
			{
				xy[num].y = Mathf.Lerp(xy[corner].y, xy[num2].y, sin);
			}
			return;
		}
		if (cos > sin)
		{
			sin /= cos;
			cos = 1f;
			if (!invert)
			{
				xy[num].y = Mathf.Lerp(xy[corner].y, xy[num2].y, sin);
				xy[num2].y = xy[num].y;
			}
		}
		else if (sin > cos)
		{
			cos /= sin;
			sin = 1f;
			if (invert)
			{
				xy[num2].x = Mathf.Lerp(xy[corner].x, xy[num2].x, cos);
				xy[num3].x = xy[num2].x;
			}
		}
		else
		{
			cos = 1f;
			sin = 1f;
		}
		if (invert)
		{
			xy[num3].y = Mathf.Lerp(xy[corner].y, xy[num2].y, sin);
		}
		else
		{
			xy[num].x = Mathf.Lerp(xy[corner].x, xy[num2].x, cos);
		}
	}

	private static void Fill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols, float v0x, float v1x, float v0y, float v1y, float u0x, float u1x, float u0y, float u1y, Color col)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		verts.Add(new Vector3(v0x, v0y));
		verts.Add(new Vector3(v0x, v1y));
		verts.Add(new Vector3(v1x, v1y));
		verts.Add(new Vector3(v1x, v0y));
		uvs.Add(new Vector2(u0x, u0y));
		uvs.Add(new Vector2(u0x, u1y));
		uvs.Add(new Vector2(u1x, u1y));
		uvs.Add(new Vector2(u1x, u0y));
		cols.Add(Color32.op_Implicit(col));
		cols.Add(Color32.op_Implicit(col));
		cols.Add(Color32.op_Implicit(col));
		cols.Add(Color32.op_Implicit(col));
	}
}
