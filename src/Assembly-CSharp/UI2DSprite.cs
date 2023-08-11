using System;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Unity2D Sprite")]
public class UI2DSprite : UIBasicSprite
{
	[HideInInspector]
	[SerializeField]
	private Sprite mSprite;

	[HideInInspector]
	[SerializeField]
	private Material mMat;

	[HideInInspector]
	[SerializeField]
	private Shader mShader;

	[HideInInspector]
	[SerializeField]
	private Vector4 mBorder = Vector4.zero;

	public Sprite nextSprite;

	[NonSerialized]
	private int mPMA = -1;

	public Sprite sprite2D
	{
		get
		{
			return mSprite;
		}
		set
		{
			if ((Object)(object)mSprite != (Object)(object)value)
			{
				RemoveFromPanel();
				mSprite = value;
				nextSprite = null;
				MarkAsChanged();
			}
		}
	}

	public override Material material
	{
		get
		{
			return mMat;
		}
		set
		{
			if ((Object)(object)mMat != (Object)(object)value)
			{
				RemoveFromPanel();
				mMat = value;
				mPMA = -1;
				MarkAsChanged();
			}
		}
	}

	public override Shader shader
	{
		get
		{
			if ((Object)(object)mMat != (Object)null)
			{
				return mMat.shader;
			}
			if ((Object)(object)mShader == (Object)null)
			{
				mShader = Shader.Find("Unlit/Transparent Colored");
			}
			return mShader;
		}
		set
		{
			if ((Object)(object)mShader != (Object)(object)value)
			{
				RemoveFromPanel();
				mShader = value;
				if ((Object)(object)mMat == (Object)null)
				{
					mPMA = -1;
					MarkAsChanged();
				}
			}
		}
	}

	public override Texture mainTexture
	{
		get
		{
			if ((Object)(object)mSprite != (Object)null)
			{
				return (Texture)(object)mSprite.texture;
			}
			if ((Object)(object)mMat != (Object)null)
			{
				return mMat.mainTexture;
			}
			return null;
		}
	}

	public override bool premultipliedAlpha
	{
		get
		{
			if (mPMA == -1)
			{
				Shader val = shader;
				mPMA = (((Object)(object)val != (Object)null && ((Object)val).name.Contains("Premultiplied")) ? 1 : 0);
			}
			return mPMA == 1;
		}
	}

	public override Vector4 drawingDimensions
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0214: Unknown result type (might be due to invalid IL or missing references)
			//IL_0219: Unknown result type (might be due to invalid IL or missing references)
			//IL_021b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0222: Unknown result type (might be due to invalid IL or missing references)
			//IL_022c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0233: Unknown result type (might be due to invalid IL or missing references)
			//IL_029d: Unknown result type (might be due to invalid IL or missing references)
			//IL_005c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0061: Unknown result type (might be due to invalid IL or missing references)
			//IL_0077: Unknown result type (might be due to invalid IL or missing references)
			//IL_007c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0092: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0101: Unknown result type (might be due to invalid IL or missing references)
			//IL_0106: Unknown result type (might be due to invalid IL or missing references)
			//IL_0115: Unknown result type (might be due to invalid IL or missing references)
			//IL_011a: Unknown result type (might be due to invalid IL or missing references)
			//IL_012a: Unknown result type (might be due to invalid IL or missing references)
			Vector2 val = base.pivotOffset;
			float num = (0f - val.x) * (float)mWidth;
			float num2 = (0f - val.y) * (float)mHeight;
			float num3 = num + (float)mWidth;
			float num4 = num2 + (float)mHeight;
			if ((Object)(object)mSprite != (Object)null && mType != Type.Tiled)
			{
				Rect val2 = mSprite.rect;
				int num5 = Mathf.RoundToInt(((Rect)(ref val2)).width);
				val2 = mSprite.rect;
				int num6 = Mathf.RoundToInt(((Rect)(ref val2)).height);
				int num7 = Mathf.RoundToInt(mSprite.textureRectOffset.x);
				int num8 = Mathf.RoundToInt(mSprite.textureRectOffset.y);
				val2 = mSprite.rect;
				float num9 = ((Rect)(ref val2)).width;
				val2 = mSprite.textureRect;
				int num10 = Mathf.RoundToInt(num9 - ((Rect)(ref val2)).width - mSprite.textureRectOffset.x);
				val2 = mSprite.rect;
				float num11 = ((Rect)(ref val2)).height;
				val2 = mSprite.textureRect;
				int num12 = Mathf.RoundToInt(num11 - ((Rect)(ref val2)).height - mSprite.textureRectOffset.y);
				float num13 = 1f;
				float num14 = 1f;
				if (num5 > 0 && num6 > 0 && (mType == Type.Simple || mType == Type.Filled))
				{
					if (((uint)num5 & (true ? 1u : 0u)) != 0)
					{
						num10++;
					}
					if (((uint)num6 & (true ? 1u : 0u)) != 0)
					{
						num12++;
					}
					num13 = 1f / (float)num5 * (float)mWidth;
					num14 = 1f / (float)num6 * (float)mHeight;
				}
				if (mFlip == Flip.Horizontally || mFlip == Flip.Both)
				{
					num += (float)num10 * num13;
					num3 -= (float)num7 * num13;
				}
				else
				{
					num += (float)num7 * num13;
					num3 -= (float)num10 * num13;
				}
				if (mFlip == Flip.Vertically || mFlip == Flip.Both)
				{
					num2 += (float)num12 * num14;
					num4 -= (float)num8 * num14;
				}
				else
				{
					num2 += (float)num8 * num14;
					num4 -= (float)num12 * num14;
				}
			}
			Vector4 val3 = border;
			float num15 = val3.x + val3.z;
			float num16 = val3.y + val3.w;
			float num17 = Mathf.Lerp(num, num3 - num15, mDrawRegion.x);
			float num18 = Mathf.Lerp(num2, num4 - num16, mDrawRegion.y);
			float num19 = Mathf.Lerp(num + num15, num3, mDrawRegion.z);
			float num20 = Mathf.Lerp(num2 + num16, num4, mDrawRegion.w);
			return new Vector4(num17, num18, num19, num20);
		}
	}

	public override Vector4 border
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return mBorder;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			if (mBorder != value)
			{
				mBorder = value;
				MarkAsChanged();
			}
		}
	}

	protected override void OnUpdate()
	{
		if ((Object)(object)nextSprite != (Object)null)
		{
			if ((Object)(object)nextSprite != (Object)(object)mSprite)
			{
				sprite2D = nextSprite;
			}
			nextSprite = null;
		}
		base.OnUpdate();
	}

	public override void MakePixelPerfect()
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		base.MakePixelPerfect();
		if (mType == Type.Tiled)
		{
			return;
		}
		Texture val = mainTexture;
		if (!((Object)(object)val == (Object)null) && (mType == Type.Simple || mType == Type.Filled || !base.hasBorder) && (Object)(object)val != (Object)null)
		{
			Rect rect = mSprite.rect;
			int num = Mathf.RoundToInt(((Rect)(ref rect)).width);
			int num2 = Mathf.RoundToInt(((Rect)(ref rect)).height);
			if ((num & 1) == 1)
			{
				num++;
			}
			if ((num2 & 1) == 1)
			{
				num2++;
			}
			base.width = num;
			base.height = num2;
		}
	}

	public override void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		Texture val = mainTexture;
		if (!((Object)(object)val == (Object)null))
		{
			Rect textureRect = mSprite.textureRect;
			Rect inner = textureRect;
			Vector4 val2 = border;
			((Rect)(ref inner)).xMin = ((Rect)(ref inner)).xMin + val2.x;
			((Rect)(ref inner)).yMin = ((Rect)(ref inner)).yMin + val2.y;
			((Rect)(ref inner)).xMax = ((Rect)(ref inner)).xMax - val2.z;
			((Rect)(ref inner)).yMax = ((Rect)(ref inner)).yMax - val2.w;
			float num = 1f / (float)val.width;
			float num2 = 1f / (float)val.height;
			((Rect)(ref textureRect)).xMin = ((Rect)(ref textureRect)).xMin * num;
			((Rect)(ref textureRect)).xMax = ((Rect)(ref textureRect)).xMax * num;
			((Rect)(ref textureRect)).yMin = ((Rect)(ref textureRect)).yMin * num2;
			((Rect)(ref textureRect)).yMax = ((Rect)(ref textureRect)).yMax * num2;
			((Rect)(ref inner)).xMin = ((Rect)(ref inner)).xMin * num;
			((Rect)(ref inner)).xMax = ((Rect)(ref inner)).xMax * num;
			((Rect)(ref inner)).yMin = ((Rect)(ref inner)).yMin * num2;
			((Rect)(ref inner)).yMax = ((Rect)(ref inner)).yMax * num2;
			int size = verts.size;
			Fill(verts, uvs, cols, textureRect, inner);
			if (onPostFill != null)
			{
				onPostFill(this, size, verts, uvs, cols);
			}
		}
	}
}
