using System;
using System.Collections.Generic;
using System.Linq;
using SoftMasking.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.Sprites;
using UnityEngine.UI;

namespace SoftMasking;

[ExecuteInEditMode]
[DisallowMultipleComponent]
[AddComponentMenu("UI/Soft Mask", 14)]
[RequireComponent(typeof(RectTransform))]
[HelpURL("https://docs.google.com/document/d/1xFZQGn_odhTCokMFR0LyCPXWtqWXN-bBGVS9GETglx8")]
public class SoftMask : UIBehaviour, ISoftMask, ICanvasRaycastFilter
{
	[Serializable]
	public enum MaskSource
	{
		Graphic,
		Sprite,
		Texture
	}

	[Serializable]
	public enum BorderMode
	{
		Simple,
		Sliced,
		Tiled
	}

	[Serializable]
	[Flags]
	public enum Errors
	{
		NoError = 0,
		UnsupportedShaders = 1,
		NestedMasks = 2,
		TightPackedSprite = 4,
		AlphaSplitSprite = 8,
		UnsupportedImageType = 0x10,
		UnreadableTexture = 0x20,
		UnreadableRenderTexture = 0x40
	}

	private struct SourceParameters
	{
		public Image image;

		public Sprite sprite;

		public BorderMode spriteBorderMode;

		public float spritePixelsPerUnit;

		public Texture texture;

		public Rect textureUVRect;
	}

	private class MaterialReplacerImpl : IMaterialReplacer
	{
		private readonly SoftMask _owner;

		public int order => 0;

		public MaterialReplacerImpl(SoftMask owner)
		{
			_owner = owner;
		}

		public Material Replace(Material original)
		{
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_004c: Expected O, but got Unknown
			if ((Object)(object)original == (Object)null || original.HasDefaultUIShader())
			{
				return Replace(original, _owner._defaultShader);
			}
			if (original.HasDefaultETC1UIShader())
			{
				return Replace(original, _owner._defaultETC1Shader);
			}
			if (original.SupportsSoftMask())
			{
				return new Material(original);
			}
			return null;
		}

		private static Material Replace(Material original, Shader defaultReplacementShader)
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			Material val = ((!Object.op_Implicit((Object)(object)defaultReplacementShader)) ? ((Material)null) : new Material(defaultReplacementShader));
			if (Object.op_Implicit((Object)(object)val) && Object.op_Implicit((Object)(object)original))
			{
				val.CopyPropertiesFromMaterial(original);
			}
			return val;
		}
	}

	private static class Mathr
	{
		public static Vector4 ToVector(Rect r)
		{
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			return new Vector4(((Rect)(ref r)).xMin, ((Rect)(ref r)).yMin, ((Rect)(ref r)).xMax, ((Rect)(ref r)).yMax);
		}

		public static Vector4 Div(Vector4 v, Vector2 s)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			return new Vector4(v.x / s.x, v.y / s.y, v.z / s.x, v.w / s.y);
		}

		public static Vector2 Div(Vector2 v, Vector2 s)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(v.x / s.x, v.y / s.y);
		}

		public static Vector4 Mul(Vector4 v, Vector2 s)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			return new Vector4(v.x * s.x, v.y * s.y, v.z * s.x, v.w * s.y);
		}

		public static Vector2 Size(Vector4 r)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(r.z - r.x, r.w - r.y);
		}

		public static Vector4 Move(Vector4 v, Vector2 o)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			return new Vector4(v.x + o.x, v.y + o.y, v.z + o.x, v.w + o.y);
		}

		public static Vector4 BorderOf(Vector4 outer, Vector4 inner)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			return new Vector4(inner.x - outer.x, inner.y - outer.y, outer.z - inner.z, outer.w - inner.w);
		}

		public static Vector4 ApplyBorder(Vector4 v, Vector4 b)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			return new Vector4(v.x + b.x, v.y + b.y, v.z - b.z, v.w - b.w);
		}

		public static Vector2 Min(Vector4 r)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(r.x, r.y);
		}

		public static Vector2 Max(Vector4 r)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(r.z, r.w);
		}

		public static Vector2 Remap(Vector2 c, Vector4 from, Vector4 to)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			Vector2 s = Max(from) - Min(from);
			Vector2 val = Max(to) - Min(to);
			return Vector2.Scale(Div(c - Min(from), s), val) + Min(to);
		}

		public static bool Inside(Vector2 v, Vector4 r)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			if (v.x >= r.x && v.y >= r.y && v.x <= r.z)
			{
				return v.y <= r.w;
			}
			return false;
		}
	}

	private struct MaterialParameters
	{
		public enum SampleMaskResult
		{
			Success,
			NonReadable,
			NonTexture2D
		}

		private static class Ids
		{
			public static readonly int SoftMask = Shader.PropertyToID("_SoftMask");

			public static readonly int SoftMask_Rect = Shader.PropertyToID("_SoftMask_Rect");

			public static readonly int SoftMask_UVRect = Shader.PropertyToID("_SoftMask_UVRect");

			public static readonly int SoftMask_ChannelWeights = Shader.PropertyToID("_SoftMask_ChannelWeights");

			public static readonly int SoftMask_WorldToMask = Shader.PropertyToID("_SoftMask_WorldToMask");

			public static readonly int SoftMask_BorderRect = Shader.PropertyToID("_SoftMask_BorderRect");

			public static readonly int SoftMask_UVBorderRect = Shader.PropertyToID("_SoftMask_UVBorderRect");

			public static readonly int SoftMask_TileRepeat = Shader.PropertyToID("_SoftMask_TileRepeat");

			public static readonly int SoftMask_InvertMask = Shader.PropertyToID("_SoftMask_InvertMask");

			public static readonly int SoftMask_InvertOutsides = Shader.PropertyToID("_SoftMask_InvertOutsides");
		}

		public Vector4 maskRect;

		public Vector4 maskBorder;

		public Vector4 maskRectUV;

		public Vector4 maskBorderUV;

		public Vector2 tileRepeat;

		public Color maskChannelWeights;

		public Matrix4x4 worldToMask;

		public Texture texture;

		public BorderMode borderMode;

		public bool invertMask;

		public bool invertOutsides;

		public Texture activeTexture
		{
			get
			{
				if (!Object.op_Implicit((Object)(object)texture))
				{
					return (Texture)(object)Texture2D.whiteTexture;
				}
				return texture;
			}
		}

		public SampleMaskResult SampleMask(Vector2 localPos, out float mask)
		{
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			mask = 0f;
			Texture obj = texture;
			Texture2D val = (Texture2D)(object)((obj is Texture2D) ? obj : null);
			if (!Object.op_Implicit((Object)(object)val))
			{
				return SampleMaskResult.NonTexture2D;
			}
			Vector2 val2 = XY2UV(localPos);
			try
			{
				mask = MaskValue(val.GetPixelBilinear(val2.x, val2.y));
				return SampleMaskResult.Success;
			}
			catch (UnityException)
			{
				return SampleMaskResult.NonReadable;
			}
		}

		public void Apply(Material mat)
		{
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0100: Unknown result type (might be due to invalid IL or missing references)
			mat.SetTexture(Ids.SoftMask, activeTexture);
			mat.SetVector(Ids.SoftMask_Rect, maskRect);
			mat.SetVector(Ids.SoftMask_UVRect, maskRectUV);
			mat.SetColor(Ids.SoftMask_ChannelWeights, maskChannelWeights);
			mat.SetMatrix(Ids.SoftMask_WorldToMask, worldToMask);
			mat.SetFloat(Ids.SoftMask_InvertMask, (float)(invertMask ? 1 : 0));
			mat.SetFloat(Ids.SoftMask_InvertOutsides, (float)(invertOutsides ? 1 : 0));
			mat.EnableKeyword("SOFTMASK_SIMPLE", borderMode == BorderMode.Simple);
			mat.EnableKeyword("SOFTMASK_SLICED", borderMode == BorderMode.Sliced);
			mat.EnableKeyword("SOFTMASK_TILED", borderMode == BorderMode.Tiled);
			if (borderMode != 0)
			{
				mat.SetVector(Ids.SoftMask_BorderRect, maskBorder);
				mat.SetVector(Ids.SoftMask_UVBorderRect, maskBorderUV);
				if (borderMode == BorderMode.Tiled)
				{
					mat.SetVector(Ids.SoftMask_TileRepeat, Vector4.op_Implicit(tileRepeat));
				}
			}
		}

		private Vector2 XY2UV(Vector2 localPos)
		{
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			return (Vector2)(borderMode switch
			{
				BorderMode.Simple => MapSimple(localPos), 
				BorderMode.Sliced => MapBorder(localPos, repeat: false), 
				BorderMode.Tiled => MapBorder(localPos, repeat: true), 
				_ => MapSimple(localPos), 
			});
		}

		private Vector2 MapSimple(Vector2 localPos)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			return Mathr.Remap(localPos, maskRect, maskRectUV);
		}

		private Vector2 MapBorder(Vector2 localPos, bool repeat)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_007a: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(Inset(localPos.x, maskRect.x, maskBorder.x, maskBorder.z, maskRect.z, maskRectUV.x, maskBorderUV.x, maskBorderUV.z, maskRectUV.z, repeat ? tileRepeat.x : 1f), Inset(localPos.y, maskRect.y, maskBorder.y, maskBorder.w, maskRect.w, maskRectUV.y, maskBorderUV.y, maskBorderUV.w, maskRectUV.w, repeat ? tileRepeat.y : 1f));
		}

		private float Inset(float v, float x1, float x2, float u1, float u2, float repeat = 1f)
		{
			float num = x2 - x1;
			return Mathf.Lerp(u1, u2, (num != 0f) ? Frac((v - x1) / num * repeat) : 0f);
		}

		private float Inset(float v, float x1, float x2, float x3, float x4, float u1, float u2, float u3, float u4, float repeat = 1f)
		{
			if (v < x2)
			{
				return Inset(v, x1, x2, u1, u2);
			}
			if (v < x3)
			{
				return Inset(v, x2, x3, u2, u3, repeat);
			}
			return Inset(v, x3, x4, u3, u4);
		}

		private float Frac(float v)
		{
			return v - Mathf.Floor(v);
		}

		private float MaskValue(Color mask)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			Color val = mask * maskChannelWeights;
			return val.a + val.r + val.g + val.b;
		}
	}

	private struct Diagnostics
	{
		private SoftMask _softMask;

		private Image image => _softMask.DeduceSourceParameters().image;

		private Sprite sprite => _softMask.DeduceSourceParameters().sprite;

		private Texture texture => _softMask.DeduceSourceParameters().texture;

		public Diagnostics(SoftMask softMask)
		{
			_softMask = softMask;
		}

		public Errors PollErrors()
		{
			SoftMask softMask = _softMask;
			Errors errors = Errors.NoError;
			((Component)softMask).GetComponentsInChildren<SoftMaskable>(s_maskables);
			using (new ClearListAtExit<SoftMaskable>(s_maskables))
			{
				if (s_maskables.Any((SoftMaskable m) => m.mask == softMask && m.shaderIsNotSupported))
				{
					errors |= Errors.UnsupportedShaders;
				}
			}
			if (ThereAreNestedMasks())
			{
				errors |= Errors.NestedMasks;
			}
			errors |= CheckSprite(sprite);
			errors |= CheckImage();
			return errors | CheckTexture();
		}

		public static Errors CheckSprite(Sprite sprite)
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			Errors errors = Errors.NoError;
			if (!Object.op_Implicit((Object)(object)sprite))
			{
				return errors;
			}
			if (sprite.packed && (int)sprite.packingMode == 0)
			{
				errors |= Errors.TightPackedSprite;
			}
			if (Object.op_Implicit((Object)(object)sprite.associatedAlphaSplitTexture))
			{
				errors |= Errors.AlphaSplitSprite;
			}
			return errors;
		}

		private bool ThereAreNestedMasks()
		{
			SoftMask softMask = _softMask;
			bool flag = false;
			using (new ClearListAtExit<SoftMask>(s_masks))
			{
				((Component)softMask).GetComponentsInParent<SoftMask>(false, s_masks);
				flag |= s_masks.Any((SoftMask x) => AreCompeting(softMask, x));
				((Component)softMask).GetComponentsInChildren<SoftMask>(false, s_masks);
				return flag | s_masks.Any((SoftMask x) => AreCompeting(softMask, x));
			}
		}

		private Errors CheckImage()
		{
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			Errors errors = Errors.NoError;
			if (!_softMask.isBasedOnGraphic)
			{
				return errors;
			}
			if (Object.op_Implicit((Object)(object)image) && !IsImageTypeSupported(image.type))
			{
				errors |= Errors.UnsupportedImageType;
			}
			return errors;
		}

		private Errors CheckTexture()
		{
			Errors errors = Errors.NoError;
			if (_softMask.isUsingRaycastFiltering && Object.op_Implicit((Object)(object)texture))
			{
				Texture obj = texture;
				Texture2D val = (Texture2D)(object)((obj is Texture2D) ? obj : null);
				if (!Object.op_Implicit((Object)(object)val))
				{
					errors |= Errors.UnreadableRenderTexture;
				}
				else if (!IsReadable(val))
				{
					errors |= Errors.UnreadableTexture;
				}
			}
			return errors;
		}

		private static bool AreCompeting(SoftMask softMask, SoftMask other)
		{
			if (softMask.isMaskingEnabled && (Object)(object)softMask != (Object)(object)other && other.isMaskingEnabled && (Object)(object)softMask.canvas.rootCanvas == (Object)(object)other.canvas.rootCanvas)
			{
				return !SelectChild<SoftMask>(softMask, other).canvas.overrideSorting;
			}
			return false;
		}

		private static T SelectChild<T>(T first, T second) where T : Component
		{
			if (!((Component)first).transform.IsChildOf(((Component)second).transform))
			{
				return second;
			}
			return first;
		}

		private static bool IsReadable(Texture2D texture)
		{
			//IL_0003: Unknown result type (might be due to invalid IL or missing references)
			try
			{
				texture.GetPixel(0, 0);
				return true;
			}
			catch (UnityException)
			{
				return false;
			}
		}
	}

	private struct WarningReporter
	{
		private Object _owner;

		private Texture _lastReadTexture;

		private Sprite _lastUsedSprite;

		private Sprite _lastUsedImageSprite;

		private Type _lastUsedImageType;

		public WarningReporter(Object owner)
		{
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			_owner = owner;
			_lastReadTexture = null;
			_lastUsedSprite = null;
			_lastUsedImageSprite = null;
			_lastUsedImageType = (Type)0;
		}

		public void TextureRead(Texture texture, MaterialParameters.SampleMaskResult sampleResult)
		{
			if (!((Object)(object)_lastReadTexture == (Object)(object)texture))
			{
				_lastReadTexture = texture;
				switch (sampleResult)
				{
				case MaterialParameters.SampleMaskResult.NonReadable:
					Debug.LogErrorFormat(_owner, "Raycast Threshold greater than 0 can't be used on Soft Mask with texture '{0}' because it's not readable. You can make the texture readable in the Texture Import Settings.", new object[1] { ((Object)texture).name });
					break;
				case MaterialParameters.SampleMaskResult.NonTexture2D:
					Debug.LogErrorFormat(_owner, "Raycast Threshold greater than 0 can't be used on Soft Mask with texture '{0}' because it's not a Texture2D. Raycast Threshold may be used only with regular 2D textures.", new object[1] { ((Object)texture).name });
					break;
				}
			}
		}

		public void SpriteUsed(Sprite sprite, Errors errors)
		{
			if (!((Object)(object)_lastUsedSprite == (Object)(object)sprite))
			{
				_lastUsedSprite = sprite;
				if ((errors & Errors.TightPackedSprite) != 0)
				{
					Debug.LogError((object)"SoftMask doesn't support tight packed sprites", _owner);
				}
				if ((errors & Errors.AlphaSplitSprite) != 0)
				{
					Debug.LogError((object)"SoftMask doesn't support sprites with an alpha split texture", _owner);
				}
			}
		}

		public void ImageUsed(Image image)
		{
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			//IL_004c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_005b: Unknown result type (might be due to invalid IL or missing references)
			//IL_007c: Unknown result type (might be due to invalid IL or missing references)
			if (!Object.op_Implicit((Object)(object)image))
			{
				_lastUsedImageSprite = null;
				_lastUsedImageType = (Type)0;
			}
			else if (!((Object)(object)_lastUsedImageSprite == (Object)(object)image.sprite) || _lastUsedImageType != image.type)
			{
				_lastUsedImageSprite = image.sprite;
				_lastUsedImageType = image.type;
				if (Object.op_Implicit((Object)(object)image) && !IsImageTypeSupported(image.type))
				{
					Debug.LogErrorFormat(_owner, "SoftMask doesn't support image type {0}. Image type Simple will be used.", new object[1] { image.type });
				}
			}
		}
	}

	[SerializeField]
	private Shader _defaultShader;

	[SerializeField]
	private Shader _defaultETC1Shader;

	[SerializeField]
	private MaskSource _source;

	[SerializeField]
	private RectTransform _separateMask;

	[SerializeField]
	private Sprite _sprite;

	[SerializeField]
	private BorderMode _spriteBorderMode;

	[SerializeField]
	private float _spritePixelsPerUnitMultiplier = 1f;

	[SerializeField]
	private Texture _texture;

	[SerializeField]
	private Rect _textureUVRect = DefaultUVRect;

	[SerializeField]
	private Color _channelWeights = MaskChannel.alpha;

	[SerializeField]
	private float _raycastThreshold;

	[SerializeField]
	private bool _invertMask;

	[SerializeField]
	private bool _invertOutsides;

	private MaterialReplacements _materials;

	private MaterialParameters _parameters;

	private WarningReporter _warningReporter;

	private Rect _lastMaskRect;

	private bool _maskingWasEnabled;

	private bool _destroyed;

	private bool _dirty;

	private RectTransform _maskTransform;

	private Graphic _graphic;

	private Canvas _canvas;

	private static readonly Rect DefaultUVRect = new Rect(0f, 0f, 1f, 1f);

	private const float DefaultPixelsPerUnit = 100f;

	private static readonly List<SoftMask> s_masks = new List<SoftMask>();

	private static readonly List<SoftMaskable> s_maskables = new List<SoftMaskable>();

	public Shader defaultShader
	{
		get
		{
			return _defaultShader;
		}
		set
		{
			SetShader(ref _defaultShader, value);
		}
	}

	public Shader defaultETC1Shader
	{
		get
		{
			return _defaultETC1Shader;
		}
		set
		{
			SetShader(ref _defaultETC1Shader, value, warnIfNotSet: false);
		}
	}

	public MaskSource source
	{
		get
		{
			return _source;
		}
		set
		{
			if (_source != value)
			{
				Set(ref _source, value);
			}
		}
	}

	public RectTransform separateMask
	{
		get
		{
			return _separateMask;
		}
		set
		{
			if ((Object)(object)_separateMask != (Object)(object)value)
			{
				Set(ref _separateMask, value);
				_graphic = null;
				_maskTransform = null;
			}
		}
	}

	public Sprite sprite
	{
		get
		{
			return _sprite;
		}
		set
		{
			if ((Object)(object)_sprite != (Object)(object)value)
			{
				Set(ref _sprite, value);
			}
		}
	}

	public BorderMode spriteBorderMode
	{
		get
		{
			return _spriteBorderMode;
		}
		set
		{
			if (_spriteBorderMode != value)
			{
				Set(ref _spriteBorderMode, value);
			}
		}
	}

	public float spritePixelsPerUnitMultiplier
	{
		get
		{
			return _spritePixelsPerUnitMultiplier;
		}
		set
		{
			if (_spritePixelsPerUnitMultiplier != value)
			{
				Set(ref _spritePixelsPerUnitMultiplier, ClampPixelsPerUnitMultiplier(value));
			}
		}
	}

	public Texture2D texture
	{
		get
		{
			Texture obj = _texture;
			return (Texture2D)(object)((obj is Texture2D) ? obj : null);
		}
		set
		{
			if ((Object)(object)_texture != (Object)(object)value)
			{
				Set(ref _texture, (Texture)(object)value);
			}
		}
	}

	public RenderTexture renderTexture
	{
		get
		{
			Texture obj = _texture;
			return (RenderTexture)(object)((obj is RenderTexture) ? obj : null);
		}
		set
		{
			if ((Object)(object)_texture != (Object)(object)value)
			{
				Set(ref _texture, (Texture)(object)value);
			}
		}
	}

	public Rect textureUVRect
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _textureUVRect;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			if (_textureUVRect != value)
			{
				Set(ref _textureUVRect, value);
			}
		}
	}

	public Color channelWeights
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _channelWeights;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			if (_channelWeights != value)
			{
				Set(ref _channelWeights, value);
			}
		}
	}

	public float raycastThreshold
	{
		get
		{
			return _raycastThreshold;
		}
		set
		{
			_raycastThreshold = value;
		}
	}

	public bool invertMask
	{
		get
		{
			return _invertMask;
		}
		set
		{
			if (_invertMask != value)
			{
				Set(ref _invertMask, value);
			}
		}
	}

	public bool invertOutsides
	{
		get
		{
			return _invertOutsides;
		}
		set
		{
			if (_invertOutsides != value)
			{
				Set(ref _invertOutsides, value);
			}
		}
	}

	public bool isUsingRaycastFiltering => _raycastThreshold > 0f;

	public bool isMaskingEnabled
	{
		get
		{
			if (((Behaviour)this).isActiveAndEnabled)
			{
				return Object.op_Implicit((Object)(object)canvas);
			}
			return false;
		}
	}

	private RectTransform maskTransform
	{
		get
		{
			if (!Object.op_Implicit((Object)(object)_maskTransform))
			{
				return _maskTransform = (Object.op_Implicit((Object)(object)_separateMask) ? _separateMask : ((Component)this).GetComponent<RectTransform>());
			}
			return _maskTransform;
		}
	}

	private Canvas canvas
	{
		get
		{
			if (!Object.op_Implicit((Object)(object)_canvas))
			{
				return _canvas = NearestEnabledCanvas();
			}
			return _canvas;
		}
	}

	private bool isBasedOnGraphic => _source == MaskSource.Graphic;

	bool ISoftMask.isAlive
	{
		get
		{
			if (Object.op_Implicit((Object)(object)this))
			{
				return !_destroyed;
			}
			return false;
		}
	}

	public SoftMask()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		MaterialReplacerChain replacer = new MaterialReplacerChain(MaterialReplacer.globalReplacers, new MaterialReplacerImpl(this));
		_materials = new MaterialReplacements(replacer, delegate(Material m)
		{
			_parameters.Apply(m);
		});
		_warningReporter = new WarningReporter((Object)(object)this);
	}

	public Errors PollErrors()
	{
		return new Diagnostics(this).PollErrors();
	}

	public bool IsRaycastLocationValid(Vector2 sp, Camera cam)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val = default(Vector2);
		if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(maskTransform, sp, cam, ref val))
		{
			return false;
		}
		if (!Mathr.Inside(val, LocalMaskRect(Vector4.zero)))
		{
			return _invertOutsides;
		}
		if (!Object.op_Implicit((Object)(object)_parameters.texture))
		{
			return true;
		}
		if (!isUsingRaycastFiltering)
		{
			return true;
		}
		float mask;
		MaterialParameters.SampleMaskResult sampleMaskResult = _parameters.SampleMask(val, out mask);
		_warningReporter.TextureRead(_parameters.texture, sampleMaskResult);
		if (sampleMaskResult != 0)
		{
			return true;
		}
		if (_invertMask)
		{
			mask = 1f - mask;
		}
		return mask >= _raycastThreshold;
	}

	protected override void Start()
	{
		((UIBehaviour)this).Start();
		WarnIfDefaultShaderIsNotSet();
	}

	protected override void OnEnable()
	{
		((UIBehaviour)this).OnEnable();
		SubscribeOnWillRenderCanvases();
		SpawnMaskablesInChildren(((Component)this).transform);
		FindGraphic();
		if (isMaskingEnabled)
		{
			UpdateMaskParameters();
		}
		NotifyChildrenThatMaskMightChanged();
	}

	protected override void OnDisable()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		((UIBehaviour)this).OnDisable();
		UnsubscribeFromWillRenderCanvases();
		if (Object.op_Implicit((Object)(object)_graphic))
		{
			_graphic.UnregisterDirtyVerticesCallback(new UnityAction(OnGraphicDirty));
			_graphic.UnregisterDirtyMaterialCallback(new UnityAction(OnGraphicDirty));
			_graphic = null;
		}
		NotifyChildrenThatMaskMightChanged();
		DestroyMaterials();
	}

	protected override void OnDestroy()
	{
		((UIBehaviour)this).OnDestroy();
		_destroyed = true;
		NotifyChildrenThatMaskMightChanged();
	}

	protected virtual void LateUpdate()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		bool flag = isMaskingEnabled;
		if (flag)
		{
			if (_maskingWasEnabled != flag)
			{
				SpawnMaskablesInChildren(((Component)this).transform);
			}
			Graphic graphic = _graphic;
			FindGraphic();
			if (_lastMaskRect != maskTransform.rect || _graphic != graphic)
			{
				_dirty = true;
			}
		}
		_maskingWasEnabled = flag;
	}

	protected override void OnRectTransformDimensionsChange()
	{
		((UIBehaviour)this).OnRectTransformDimensionsChange();
		_dirty = true;
	}

	protected override void OnDidApplyAnimationProperties()
	{
		((UIBehaviour)this).OnDidApplyAnimationProperties();
		_dirty = true;
	}

	private static float ClampPixelsPerUnitMultiplier(float value)
	{
		return Mathf.Max(value, 0.01f);
	}

	protected override void OnTransformParentChanged()
	{
		((UIBehaviour)this).OnTransformParentChanged();
		_canvas = null;
		_dirty = true;
	}

	protected override void OnCanvasHierarchyChanged()
	{
		((UIBehaviour)this).OnCanvasHierarchyChanged();
		_canvas = null;
		_dirty = true;
		NotifyChildrenThatMaskMightChanged();
	}

	private void OnTransformChildrenChanged()
	{
		SpawnMaskablesInChildren(((Component)this).transform);
	}

	private void SubscribeOnWillRenderCanvases()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		Touch<CanvasUpdateRegistry>(CanvasUpdateRegistry.instance);
		Canvas.willRenderCanvases += new WillRenderCanvases(OnWillRenderCanvases);
	}

	private void UnsubscribeFromWillRenderCanvases()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		Canvas.willRenderCanvases -= new WillRenderCanvases(OnWillRenderCanvases);
	}

	private void OnWillRenderCanvases()
	{
		if (isMaskingEnabled)
		{
			UpdateMaskParameters();
		}
	}

	private static T Touch<T>(T obj)
	{
		return obj;
	}

	Material ISoftMask.GetReplacement(Material original)
	{
		return _materials.Get(original);
	}

	void ISoftMask.ReleaseReplacement(Material replacement)
	{
		_materials.Release(replacement);
	}

	void ISoftMask.UpdateTransformChildren(Transform transform)
	{
		SpawnMaskablesInChildren(transform);
	}

	private void OnGraphicDirty()
	{
		if (isBasedOnGraphic)
		{
			_dirty = true;
		}
	}

	private void FindGraphic()
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		if (!Object.op_Implicit((Object)(object)_graphic) && isBasedOnGraphic)
		{
			_graphic = ((Component)maskTransform).GetComponent<Graphic>();
			if (Object.op_Implicit((Object)(object)_graphic))
			{
				_graphic.RegisterDirtyVerticesCallback(new UnityAction(OnGraphicDirty));
				_graphic.RegisterDirtyMaterialCallback(new UnityAction(OnGraphicDirty));
			}
		}
	}

	private Canvas NearestEnabledCanvas()
	{
		Canvas[] componentsInParent = ((Component)this).GetComponentsInParent<Canvas>(false);
		for (int i = 0; i < componentsInParent.Length; i++)
		{
			if (((Behaviour)componentsInParent[i]).isActiveAndEnabled)
			{
				return componentsInParent[i];
			}
		}
		return null;
	}

	private void UpdateMaskParameters()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		if (_dirty || ((Transform)maskTransform).hasChanged)
		{
			CalculateMaskParameters();
			((Transform)maskTransform).hasChanged = false;
			_lastMaskRect = maskTransform.rect;
			_dirty = false;
		}
		_materials.ApplyAll();
	}

	private void SpawnMaskablesInChildren(Transform root)
	{
		using (new ClearListAtExit<SoftMaskable>(s_maskables))
		{
			for (int i = 0; i < root.childCount; i++)
			{
				Transform child = root.GetChild(i);
				((Component)child).GetComponents<SoftMaskable>(s_maskables);
				if (s_maskables.Count == 0)
				{
					((Component)child).gameObject.AddComponent<SoftMaskable>();
				}
			}
		}
	}

	private void InvalidateChildren()
	{
		ForEachChildMaskable(delegate(SoftMaskable x)
		{
			x.Invalidate();
		});
	}

	private void NotifyChildrenThatMaskMightChanged()
	{
		ForEachChildMaskable(delegate(SoftMaskable x)
		{
			x.MaskMightChanged();
		});
	}

	private void ForEachChildMaskable(Action<SoftMaskable> f)
	{
		((Component)((Component)this).transform).GetComponentsInChildren<SoftMaskable>(s_maskables);
		using (new ClearListAtExit<SoftMaskable>(s_maskables))
		{
			for (int i = 0; i < s_maskables.Count; i++)
			{
				SoftMaskable softMaskable = s_maskables[i];
				if (Object.op_Implicit((Object)(object)softMaskable) && (Object)(object)((Component)softMaskable).gameObject != (Object)(object)((Component)this).gameObject)
				{
					f(softMaskable);
				}
			}
		}
	}

	private void DestroyMaterials()
	{
		_materials.DestroyAllAndClear();
	}

	private SourceParameters DeduceSourceParameters()
	{
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Expected O, but got Unknown
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Expected O, but got Unknown
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		SourceParameters result = default(SourceParameters);
		switch (_source)
		{
		case MaskSource.Graphic:
			if (_graphic is Image)
			{
				Image val = (Image)_graphic;
				Sprite val2 = val.sprite;
				result.image = val;
				result.sprite = val2;
				result.spriteBorderMode = ImageTypeToBorderMode(val.type);
				if (Object.op_Implicit((Object)(object)val2))
				{
					result.spritePixelsPerUnit = val2.pixelsPerUnit;
					result.texture = (Texture)(object)val2.texture;
				}
				else
				{
					result.spritePixelsPerUnit = 100f;
				}
			}
			else if (_graphic is RawImage)
			{
				RawImage val3 = (RawImage)_graphic;
				result.texture = val3.texture;
				result.textureUVRect = val3.uvRect;
			}
			break;
		case MaskSource.Sprite:
			result.sprite = _sprite;
			result.spriteBorderMode = _spriteBorderMode;
			if (Object.op_Implicit((Object)(object)_sprite))
			{
				result.spritePixelsPerUnit = _sprite.pixelsPerUnit * _spritePixelsPerUnitMultiplier;
				result.texture = (Texture)(object)_sprite.texture;
			}
			else
			{
				result.spritePixelsPerUnit = 100f;
			}
			break;
		case MaskSource.Texture:
			result.texture = _texture;
			result.textureUVRect = _textureUVRect;
			break;
		}
		return result;
	}

	public static BorderMode ImageTypeToBorderMode(Type type)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected I4, but got Unknown
		return (int)type switch
		{
			0 => BorderMode.Simple, 
			1 => BorderMode.Sliced, 
			2 => BorderMode.Tiled, 
			_ => BorderMode.Simple, 
		};
	}

	public static bool IsImageTypeSupported(Type type)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Invalid comparison between Unknown and I4
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Invalid comparison between Unknown and I4
		if ((int)type != 0 && (int)type != 1)
		{
			return (int)type == 2;
		}
		return true;
	}

	private void CalculateMaskParameters()
	{
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		SourceParameters sourceParameters = DeduceSourceParameters();
		_warningReporter.ImageUsed(sourceParameters.image);
		Errors errors = Diagnostics.CheckSprite(sourceParameters.sprite);
		_warningReporter.SpriteUsed(sourceParameters.sprite, errors);
		if (Object.op_Implicit((Object)(object)sourceParameters.sprite))
		{
			if (errors == Errors.NoError)
			{
				CalculateSpriteBased(sourceParameters.sprite, sourceParameters.spriteBorderMode, sourceParameters.spritePixelsPerUnit);
			}
			else
			{
				CalculateSolidFill();
			}
		}
		else if (Object.op_Implicit((Object)(object)sourceParameters.texture))
		{
			CalculateTextureBased(sourceParameters.texture, sourceParameters.textureUVRect);
		}
		else
		{
			CalculateSolidFill();
		}
	}

	private void CalculateSpriteBased(Sprite sprite, BorderMode borderMode, float spritePixelsPerUnit)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		FillCommonParameters();
		Vector4 innerUV = DataUtility.GetInnerUV(sprite);
		Vector4 outerUV = DataUtility.GetOuterUV(sprite);
		Vector4 padding = DataUtility.GetPadding(sprite);
		Vector4 val = LocalMaskRect(Vector4.zero);
		_parameters.maskRectUV = outerUV;
		if (borderMode == BorderMode.Simple)
		{
			Rect rect = sprite.rect;
			Vector4 v = Mathr.Div(padding, ((Rect)(ref rect)).size);
			_parameters.maskRect = Mathr.ApplyBorder(val, Mathr.Mul(v, Mathr.Size(val)));
		}
		else
		{
			float num = SpriteToCanvasScale(spritePixelsPerUnit);
			_parameters.maskRect = Mathr.ApplyBorder(val, padding * num);
			Vector4 border = AdjustBorders(sprite.border * num, val);
			_parameters.maskBorder = LocalMaskRect(border);
			_parameters.maskBorderUV = innerUV;
		}
		_parameters.texture = (Texture)(object)sprite.texture;
		_parameters.borderMode = borderMode;
		if (borderMode == BorderMode.Tiled)
		{
			_parameters.tileRepeat = MaskRepeat(sprite, spritePixelsPerUnit, _parameters.maskBorder);
		}
	}

	private static Vector4 AdjustBorders(Vector4 border, Vector4 rect)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val = Mathr.Size(rect);
		for (int i = 0; i <= 1; i++)
		{
			float num = ((Vector4)(ref border))[i] + ((Vector4)(ref border))[i + 2];
			if (((Vector2)(ref val))[i] < num && num != 0f)
			{
				float num2 = ((Vector2)(ref val))[i] / num;
				ref Vector4 reference = ref border;
				int num3 = i;
				((Vector4)(ref reference))[num3] = ((Vector4)(ref reference))[num3] * num2;
				reference = ref border;
				num3 = i + 2;
				((Vector4)(ref reference))[num3] = ((Vector4)(ref reference))[num3] * num2;
			}
		}
		return border;
	}

	private void CalculateTextureBased(Texture texture, Rect uvRect)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		FillCommonParameters();
		_parameters.maskRect = LocalMaskRect(Vector4.zero);
		_parameters.maskRectUV = Mathr.ToVector(uvRect);
		_parameters.texture = texture;
		_parameters.borderMode = BorderMode.Simple;
	}

	private void CalculateSolidFill()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		CalculateTextureBased(null, DefaultUVRect);
	}

	private void FillCommonParameters()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		_parameters.worldToMask = WorldToMask();
		_parameters.maskChannelWeights = _channelWeights;
		_parameters.invertMask = _invertMask;
		_parameters.invertOutsides = _invertOutsides;
	}

	private float SpriteToCanvasScale(float spritePixelsPerUnit)
	{
		return (Object.op_Implicit((Object)(object)canvas) ? canvas.referencePixelsPerUnit : 100f) / spritePixelsPerUnit;
	}

	private Matrix4x4 WorldToMask()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		return ((Transform)maskTransform).worldToLocalMatrix * ((Component)canvas.rootCanvas).transform.localToWorldMatrix;
	}

	private Vector4 LocalMaskRect(Vector4 border)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		return Mathr.ApplyBorder(Mathr.ToVector(maskTransform.rect), border);
	}

	private Vector2 MaskRepeat(Sprite sprite, float spritePixelsPerUnit, Vector4 centralPart)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		Vector4 r = Mathr.ApplyBorder(Mathr.ToVector(sprite.rect), sprite.border);
		return Mathr.Div(Mathr.Size(centralPart) * SpriteToCanvasScale(spritePixelsPerUnit), Mathr.Size(r));
	}

	private void WarnIfDefaultShaderIsNotSet()
	{
		if (!Object.op_Implicit((Object)(object)_defaultShader))
		{
			Debug.LogWarning((object)"SoftMask may not work because its defaultShader is not set", (Object)(object)this);
		}
	}

	private void Set<T>(ref T field, T value)
	{
		field = value;
		_dirty = true;
	}

	private void SetShader(ref Shader field, Shader value, bool warnIfNotSet = true)
	{
		if ((Object)(object)field != (Object)(object)value)
		{
			field = value;
			if (warnIfNotSet)
			{
				WarnIfDefaultShaderIsNotSet();
			}
			DestroyMaterials();
			InvalidateChildren();
		}
	}
}
