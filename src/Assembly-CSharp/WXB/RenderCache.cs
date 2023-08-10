using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Sprites;
using UnityEngine.UI;

namespace WXB;

public class RenderCache
{
	public abstract class BaseData
	{
		private Rect m_Rect;

		public NodeBase node;

		public Rect rect
		{
			get
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				return m_Rect;
			}
			set
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				//IL_0002: Unknown result type (might be due to invalid IL or missing references)
				m_Rect = value;
			}
		}

		public Line line { get; protected set; }

		public virtual bool isAlignByGeometry => false;

		public int subMaterial { get; set; }

		protected LineAlignment lineAlignment
		{
			get
			{
				if (node.lineAlignment != LineAlignment.Default)
				{
					return node.lineAlignment;
				}
				return node.owner.lineAlignment;
			}
		}

		public bool isContain(Vector2 pos)
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return ((Rect)(ref m_Rect)).Contains(pos);
		}

		public abstract void Render(VertexHelper vh, Rect area, Vector2 offset, float pixelsPerUnit);

		public virtual void OnMouseEnter()
		{
		}

		public virtual void OnMouseLevel()
		{
		}

		public virtual void OnMouseUp(PointerEventData eventData)
		{
		}

		public virtual void Release()
		{
			node = null;
			line = null;
			OnRelease();
		}

		protected abstract void OnRelease();

		public virtual void OnAlignByGeometry(ref Vector2 offset, float pixelsPerUnit)
		{
		}

		public virtual void OnLineYCheck(float pixelsPerUnit)
		{
		}

		public virtual Vector2 GetStartLeftBottom(float unitsPerPixel)
		{
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			//IL_004f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0058: Unknown result type (might be due to invalid IL or missing references)
			//IL_005d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0092: Unknown result type (might be due to invalid IL or missing references)
			//IL_0097: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
			Rect val;
			if (line == null)
			{
				val = rect;
				float x = ((Rect)(ref val)).x;
				val = rect;
				float y = ((Rect)(ref val)).y;
				val = rect;
				return new Vector2(x, y + ((Rect)(ref val)).height);
			}
			val = rect;
			float x2 = ((Rect)(ref val)).x;
			val = rect;
			float y2 = ((Rect)(ref val)).y;
			val = rect;
			Vector2 result = default(Vector2);
			((Vector2)(ref result))._002Ector(x2, y2 + ((Rect)(ref val)).height);
			switch (lineAlignment)
			{
			case LineAlignment.Center:
			{
				float y3 = line.y;
				val = rect;
				if (y3 != ((Rect)(ref val)).height)
				{
					float y4 = line.y;
					val = rect;
					float num = (y4 - ((Rect)(ref val)).height) * 0.5f;
					result.y += num;
				}
				break;
			}
			case LineAlignment.Bottom:
				val = rect;
				result.y = ((Rect)(ref val)).y + line.y;
				break;
			}
			return result;
		}
	}

	private struct Key
	{
		public int subMaterial;

		public bool isBlink;

		public bool isOffset;

		public Rect offsetRect;

		public List<BaseData> nodes;

		public DrawType drawType
		{
			get
			{
				if (isBlink)
				{
					if (isOffset)
					{
						return DrawType.OffsetAndAlpha;
					}
					return DrawType.Alpha;
				}
				if (isOffset)
				{
					return DrawType.Offset;
				}
				return DrawType.Default;
			}
		}

		public bool IsEquals(BaseData bd)
		{
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			if (subMaterial == bd.subMaterial && isBlink == bd.node.d_bBlink)
			{
				if (isOffset || bd.node.d_bOffset)
				{
					if (isOffset == bd.node.d_bOffset)
					{
						return offsetRect == bd.node.d_rectOffset;
					}
					return false;
				}
				return true;
			}
			return false;
		}

		public Draw Get(Owner owner, Texture texture)
		{
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			long keyPrefix = nodes[0].node.keyPrefix;
			keyPrefix += ((Object)texture).GetInstanceID();
			return owner.GetDraw(drawType, keyPrefix, delegate(Draw d, object p)
			{
				//IL_001b: Unknown result type (might be due to invalid IL or missing references)
				d.texture = texture;
				if (d is OffsetDraw)
				{
					(d as OffsetDraw).Set((Rect)p);
				}
			}, offsetRect);
		}
	}

	private class CartoonData : BaseData
	{
		public Cartoon cartoon;

		public void Reset(NodeBase n, Cartoon c, Rect r, Line l)
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			node = n;
			cartoon = c;
			base.rect = r;
			base.line = l;
		}

		protected override void OnRelease()
		{
			cartoon = null;
		}

		public override void Render(VertexHelper vh, Rect area, Vector2 offset, float pixelsPerUnit)
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0075: Unknown result type (might be due to invalid IL or missing references)
			//IL_0077: Unknown result type (might be due to invalid IL or missing references)
			//IL_007c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0085: Unknown result type (might be due to invalid IL or missing references)
			//IL_008a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0092: Unknown result type (might be due to invalid IL or missing references)
			Color d_color = node.d_color;
			if (!(d_color.a <= 0.01f))
			{
				Vector2 pos = GetStartLeftBottom(1f) + offset;
				Tools.LB2LT(ref pos, ((Rect)(ref area)).height);
				CartoonDraw obj = node.owner.GetDraw(DrawType.Cartoon, node.keyPrefix + cartoon.GetHashCode(), delegate(Draw d, object p)
				{
					CartoonDraw obj2 = d as CartoonDraw;
					obj2.cartoon = cartoon;
					obj2.isOpenAlpha = node.d_bBlink;
					obj2.isOpenOffset = false;
				}) as CartoonDraw;
				Vector2 leftPos = pos;
				Rect val = base.rect;
				float width = ((Rect)(ref val)).width;
				val = base.rect;
				obj.Add(leftPos, width, ((Rect)(ref val)).height, d_color);
			}
		}
	}

	private class SpriteData : BaseData
	{
		public Sprite sprite;

		public void Reset(NodeBase n, Sprite s, Rect r, Line l)
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			node = n;
			sprite = s;
			base.rect = r;
			base.line = l;
		}

		protected override void OnRelease()
		{
			sprite = null;
		}

		public override void Render(VertexHelper vh, Rect area, Vector2 offset, float pixelsPerUnit)
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			//IL_004c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0057: Unknown result type (might be due to invalid IL or missing references)
			//IL_005c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0070: Unknown result type (might be due to invalid IL or missing references)
			//IL_0076: Unknown result type (might be due to invalid IL or missing references)
			//IL_007c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0081: Unknown result type (might be due to invalid IL or missing references)
			//IL_0082: Unknown result type (might be due to invalid IL or missing references)
			//IL_0087: Unknown result type (might be due to invalid IL or missing references)
			//IL_008d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0093: Unknown result type (might be due to invalid IL or missing references)
			//IL_009e: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00be: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0102: Unknown result type (might be due to invalid IL or missing references)
			//IL_010a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0110: Unknown result type (might be due to invalid IL or missing references)
			//IL_0115: Unknown result type (might be due to invalid IL or missing references)
			//IL_0116: Unknown result type (might be due to invalid IL or missing references)
			//IL_011b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0121: Unknown result type (might be due to invalid IL or missing references)
			//IL_0127: Unknown result type (might be due to invalid IL or missing references)
			Color d_color = node.d_color;
			if (!(d_color.a <= 0.01f))
			{
				Vector4 outerUV = DataUtility.GetOuterUV(sprite);
				Vector2 pos = GetStartLeftBottom(1f) + offset;
				Tools.LB2LT(ref pos, ((Rect)(ref area)).height);
				Rect val = base.rect;
				float width = ((Rect)(ref val)).width;
				val = base.rect;
				float height = ((Rect)(ref val)).height;
				int currentVertCount = vh.currentVertCount;
				vh.AddVert(new Vector3(pos.x, pos.y), Color32.op_Implicit(d_color), new Vector2(outerUV.x, outerUV.y));
				vh.AddVert(new Vector3(pos.x, pos.y + height), Color32.op_Implicit(d_color), new Vector2(outerUV.x, outerUV.w));
				vh.AddVert(new Vector3(pos.x + width, pos.y + height), Color32.op_Implicit(d_color), new Vector2(outerUV.z, outerUV.w));
				vh.AddVert(new Vector3(pos.x + width, pos.y), Color32.op_Implicit(d_color), new Vector2(outerUV.z, outerUV.y));
				vh.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
				vh.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
			}
		}
	}

	public class TextData : BaseData
	{
		public string text;

		private const float line_height = 2f;

		private static CharacterInfo s_Info;

		public override bool isAlignByGeometry => true;

		public TextNode Node
		{
			get
			{
				return (TextNode)node;
			}
			set
			{
				node = value;
			}
		}

		public void Reset(TextNode n, string t, Rect r, Line l)
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			node = n;
			text = t;
			base.rect = r;
			base.line = l;
		}

		protected override void OnRelease()
		{
			text = null;
		}

		private static TextAnchor SwitchTextAnchor(TextAnchor anchor)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Expected I4, but got Unknown
			switch ((int)anchor)
			{
			case 0:
			case 1:
			case 2:
				return (TextAnchor)0;
			case 3:
			case 4:
			case 5:
				return (TextAnchor)4;
			case 6:
			case 7:
			case 8:
				return (TextAnchor)6;
			default:
				return (TextAnchor)4;
			}
		}

		public override void OnLineYCheck(float pixelsPerUnit)
		{
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_004c: Unknown result type (might be due to invalid IL or missing references)
			if (base.line == null)
			{
				return;
			}
			TextNode textNode = Node;
			Font d_font = textNode.d_font;
			FontStyle d_fontStyle = textNode.d_fontStyle;
			int num = (int)((float)textNode.d_fontSize * pixelsPerUnit);
			d_font.RequestCharactersInTexture(text, num, d_fontStyle);
			for (int i = 0; i < text.Length; i++)
			{
				if (d_font.GetCharacterInfo(text[i], ref s_Info, num, d_fontStyle))
				{
					base.line.minY = Mathf.Min((float)((CharacterInfo)(ref s_Info)).minY, base.line.minY);
					base.line.maxY = Mathf.Max((float)((CharacterInfo)(ref s_Info)).maxY, base.line.maxY);
				}
			}
		}

		public override void OnAlignByGeometry(ref Vector2 offset, float pixelsPerUnit)
		{
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0096: Unknown result type (might be due to invalid IL or missing references)
			if (base.line == null)
			{
				return;
			}
			TextNode textNode = Node;
			Font d_font = textNode.d_font;
			FontStyle d_fontStyle = textNode.d_fontStyle;
			int num = (int)((float)textNode.d_fontSize * pixelsPerUnit);
			d_font.RequestCharactersInTexture(text, num, d_fontStyle);
			Rect val = base.rect;
			if (((Rect)(ref val)).x == 0f && d_font.GetCharacterInfo(text[0], ref s_Info, num, d_fontStyle))
			{
				offset.x = ((CharacterInfo)(ref s_Info)).minX;
			}
			float num2 = float.MinValue;
			for (int i = 0; i < text.Length; i++)
			{
				if (d_font.GetCharacterInfo(text[i], ref s_Info, num, d_fontStyle))
				{
					num2 = Mathf.Max((float)((CharacterInfo)(ref s_Info)).maxY, offset.y);
				}
			}
			offset.y = Mathf.Max(offset.y, base.line.y - num2);
		}

		public override Vector2 GetStartLeftBottom(float unitsPerPixel)
		{
			//IL_0003: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0053: Unknown result type (might be due to invalid IL or missing references)
			//IL_0058: Unknown result type (might be due to invalid IL or missing references)
			//IL_007b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0080: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00db: Unknown result type (might be due to invalid IL or missing references)
			//IL_0102: Unknown result type (might be due to invalid IL or missing references)
			Rect val = base.rect;
			float x = ((Rect)(ref val)).x;
			val = base.rect;
			float y = ((Rect)(ref val)).y;
			val = base.rect;
			Vector2 result = default(Vector2);
			((Vector2)(ref result))._002Ector(x, y + ((Rect)(ref val)).height);
			switch (base.lineAlignment)
			{
			case LineAlignment.Top:
				val = base.rect;
				result.y = ((Rect)(ref val)).y + base.line.maxY * unitsPerPixel;
				break;
			case LineAlignment.Center:
				val = base.rect;
				result.y = ((Rect)(ref val)).y + base.line.maxY * unitsPerPixel;
				result.y += (base.line.y - (base.line.maxY - base.line.minY) * unitsPerPixel) * 0.5f;
				break;
			case LineAlignment.Bottom:
				val = base.rect;
				result.y = ((Rect)(ref val)).y + base.line.y + base.line.minY * unitsPerPixel;
				break;
			}
			return result;
		}

		public override void Render(VertexHelper vh, Rect area, Vector2 offset, float pixelsPerUnit)
		{
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			//IL_007a: Unknown result type (might be due to invalid IL or missing references)
			//IL_007f: Unknown result type (might be due to invalid IL or missing references)
			//IL_009d: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_0101: Unknown result type (might be due to invalid IL or missing references)
			//IL_0117: Unknown result type (might be due to invalid IL or missing references)
			//IL_011e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0134: Unknown result type (might be due to invalid IL or missing references)
			//IL_013b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0151: Unknown result type (might be due to invalid IL or missing references)
			//IL_0156: Unknown result type (might be due to invalid IL or missing references)
			//IL_0167: Unknown result type (might be due to invalid IL or missing references)
			//IL_016c: Unknown result type (might be due to invalid IL or missing references)
			//IL_016d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0177: Unknown result type (might be due to invalid IL or missing references)
			//IL_0190: Unknown result type (might be due to invalid IL or missing references)
			//IL_0195: Unknown result type (might be due to invalid IL or missing references)
			//IL_0196: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_01be: Unknown result type (might be due to invalid IL or missing references)
			//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_029f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0384: Unknown result type (might be due to invalid IL or missing references)
			//IL_0390: Unknown result type (might be due to invalid IL or missing references)
			//IL_0396: Unknown result type (might be due to invalid IL or missing references)
			//IL_039b: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_0343: Unknown result type (might be due to invalid IL or missing references)
			//IL_0346: Unknown result type (might be due to invalid IL or missing references)
			//IL_034b: Unknown result type (might be due to invalid IL or missing references)
			//IL_035b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0366: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_04dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_04e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0504: Unknown result type (might be due to invalid IL or missing references)
			//IL_049c: Unknown result type (might be due to invalid IL or missing references)
			//IL_049f: Unknown result type (might be due to invalid IL or missing references)
			//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_04bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0547: Unknown result type (might be due to invalid IL or missing references)
			//IL_0552: Unknown result type (might be due to invalid IL or missing references)
			//IL_0529: Unknown result type (might be due to invalid IL or missing references)
			//IL_0534: Unknown result type (might be due to invalid IL or missing references)
			TextNode node = Node;
			Color currentColor = node.currentColor;
			if (currentColor.a <= 0.01f)
			{
				return;
			}
			int currentIndexCount = vh.currentIndexCount;
			float num = 1f / pixelsPerUnit;
			Vector2 pos = GetStartLeftBottom(num) + offset;
			Tools.LB2LT(ref pos, ((Rect)(ref area)).height);
			float num2 = float.MaxValue;
			float num3 = float.MinValue;
			Font d_font = node.d_font;
			FontStyle d_fontStyle = node.d_fontStyle;
			int num4 = (int)((float)node.d_fontSize * pixelsPerUnit);
			d_font.RequestCharactersInTexture(text, num4, d_fontStyle);
			Vector2 zero = Vector2.zero;
			for (int i = 0; i < text.Length; i++)
			{
				if (d_font.GetCharacterInfo(text[i], ref s_Info, num4, d_fontStyle))
				{
					int currentVertCount = vh.currentVertCount;
					Rect val = Rect.MinMaxRect(pos.x + (zero.x + (float)((CharacterInfo)(ref s_Info)).minX) * num, pos.y + (zero.y + (float)((CharacterInfo)(ref s_Info)).minY) * num, pos.x + (zero.x + (float)((CharacterInfo)(ref s_Info)).maxX) * num, pos.y + (zero.y + (float)((CharacterInfo)(ref s_Info)).maxY) * num);
					vh.AddVert(new Vector3(((Rect)(ref val)).xMin, ((Rect)(ref val)).yMax), Color32.op_Implicit(currentColor), ((CharacterInfo)(ref s_Info)).uvTopLeft);
					vh.AddVert(new Vector3(((Rect)(ref val)).xMax, ((Rect)(ref val)).yMax), Color32.op_Implicit(currentColor), ((CharacterInfo)(ref s_Info)).uvTopRight);
					vh.AddVert(new Vector3(((Rect)(ref val)).xMax, ((Rect)(ref val)).yMin), Color32.op_Implicit(currentColor), ((CharacterInfo)(ref s_Info)).uvBottomRight);
					vh.AddVert(new Vector3(((Rect)(ref val)).xMin, ((Rect)(ref val)).yMin), Color32.op_Implicit(currentColor), ((CharacterInfo)(ref s_Info)).uvBottomLeft);
					vh.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
					vh.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
					zero.x += ((CharacterInfo)(ref s_Info)).advance;
					num2 = Mathf.Min(((Rect)(ref val)).yMin, num2);
					num3 = Mathf.Max(((Rect)(ref val)).yMax, num3);
				}
			}
			bool flag = false;
			Rect val3;
			if (node.d_bDynUnderline || node.d_bUnderline)
			{
				flag = true;
				node.GetLineCharacterInfo(out s_Info);
				Vector2 val2 = default(Vector2);
				((Vector2)(ref val2))._002Ector(pos.x, num2);
				if (node.d_bDynUnderline)
				{
					Texture mainTexture2 = node.d_font.material.mainTexture;
					OutlineDraw obj = node.owner.GetDraw(DrawType.Outline, node.keyPrefix + ((Object)mainTexture2).GetInstanceID(), delegate(Draw d, object p)
					{
						OutlineDraw obj4 = d as OutlineDraw;
						obj4.isOpenAlpha = node.d_bBlink;
						obj4.texture = mainTexture2;
					}) as OutlineDraw;
					TextNode n = node;
					Vector2 left = val2;
					val3 = base.rect;
					obj.AddLine(n, left, ((Rect)(ref val3)).width, 2f * num, currentColor, ((Rect)(ref s_Info.uv)).center, node.d_dynSpeed);
				}
				else
				{
					Vector2 leftPos = val2;
					Vector2 center = ((Rect)(ref s_Info.uv)).center;
					val3 = base.rect;
					Tools.AddLine(vh, leftPos, center, ((Rect)(ref val3)).width, 2f * num, currentColor);
				}
			}
			if (node.d_bDynStrickout || node.d_bStrickout)
			{
				if (!flag)
				{
					flag = true;
					node.GetLineCharacterInfo(out s_Info);
				}
				Vector2 val4 = default(Vector2);
				((Vector2)(ref val4))._002Ector(pos.x, (num3 + num2) * 0.5f + 1f * num);
				if (node.d_bDynStrickout)
				{
					Texture mainTexture = node.d_font.material.mainTexture;
					OutlineDraw obj2 = node.owner.GetDraw(DrawType.Outline, node.keyPrefix + ((Object)mainTexture).GetInstanceID(), delegate(Draw d, object p)
					{
						OutlineDraw obj3 = d as OutlineDraw;
						obj3.isOpenAlpha = node.d_bBlink;
						obj3.texture = mainTexture;
					}) as OutlineDraw;
					TextNode n2 = node;
					Vector2 left2 = val4;
					val3 = base.rect;
					obj2.AddLine(n2, left2, ((Rect)(ref val3)).width, 2f * num, currentColor, ((Rect)(ref s_Info.uv)).center, node.d_dynSpeed);
				}
				else
				{
					Vector2 leftPos2 = val4;
					Vector2 center2 = ((Rect)(ref s_Info.uv)).center;
					val3 = base.rect;
					Tools.AddLine(vh, leftPos2, center2, ((Rect)(ref val3)).width, 2f * num, currentColor);
				}
			}
			switch (node.effectType)
			{
			case EffectType.Outline:
				Effect.Outline(vh, currentIndexCount, node.effectColor, node.effectDistance);
				break;
			case EffectType.Shadow:
				Effect.Shadow(vh, currentIndexCount, node.effectColor, node.effectDistance);
				break;
			}
		}

		public override void OnMouseEnter()
		{
			node.onMouseEnter();
		}

		public override void OnMouseLevel()
		{
			node.onMouseLeave();
		}
	}

	private Owner mOwner;

	private List<BaseData> DataList = new List<BaseData>();

	private static List<Key> s_keys = new List<Key>();

	private Vector2 DrawOffset;

	public List<Texture> materials { get; protected set; }

	public bool isEmpty => DataList.Count == 0;

	public RenderCache(Owner st)
	{
		mOwner = st;
		materials = new List<Texture>();
	}

	public void Release()
	{
		materials.Clear();
		BaseData baseData = null;
		for (int i = 0; i < DataList.Count; i++)
		{
			baseData = DataList[i];
			baseData.Release();
			if (baseData is TextData)
			{
				PoolData<TextData>.Free((TextData)baseData);
			}
			else if (baseData is SpriteData)
			{
				PoolData<SpriteData>.Free((SpriteData)baseData);
			}
		}
		DataList.Clear();
	}

	public void cacheText(Line l, TextNode n, string text, Rect rect)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		TextData textData = PoolData<TextData>.Get();
		textData.Reset(n, text, rect, l);
		DataList.Add(textData);
		textData.subMaterial = materials.IndexOf(n.d_font.material.mainTexture);
		if (textData.subMaterial == -1)
		{
			textData.subMaterial = materials.Count;
			materials.Add(n.d_font.material.mainTexture);
		}
	}

	public void cacheSprite(Line l, NodeBase n, Sprite sprite, Rect rect)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)sprite != (Object)null)
		{
			SpriteData spriteData = PoolData<SpriteData>.Get();
			spriteData.Reset(n, sprite, rect, l);
			DataList.Add(spriteData);
			spriteData.subMaterial = materials.IndexOf((Texture)(object)sprite.texture);
			if (spriteData.subMaterial == -1)
			{
				spriteData.subMaterial = materials.Count;
				materials.Add((Texture)(object)sprite.texture);
			}
		}
	}

	public void cacheCartoon(Line l, NodeBase n, Cartoon cartoon, Rect rect)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		if (cartoon != null)
		{
			CartoonData cartoonData = PoolData<CartoonData>.Get();
			cartoonData.Reset(n, cartoon, rect, l);
			DataList.Add(cartoonData);
		}
	}

	public void OnAlignByGeometry(ref Vector2 offset, float pixelsPerUnit, float firstHeight)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < DataList.Count; i++)
		{
			Rect rect = DataList[i].rect;
			if (!(((Rect)(ref rect)).y > firstHeight))
			{
				if (!DataList[i].isAlignByGeometry)
				{
					offset = Vector2.zero;
					break;
				}
				DataList[i].OnAlignByGeometry(ref offset, pixelsPerUnit);
			}
		}
	}

	public void OnCheckLineY(float pixelsPerUnit)
	{
		for (int i = 0; i < DataList.Count; i++)
		{
			DataList[i].OnLineYCheck(pixelsPerUnit);
		}
	}

	public void Render(VertexHelper vh, Rect rect, Vector2 offset, float pixelsPerUnit, Mesh workerMesh, Material defaultMaterial)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		DrawOffset = offset;
		s_keys.Clear();
		for (int i = 0; i < DataList.Count; i++)
		{
			BaseData bd = DataList[i];
			int num = s_keys.FindIndex((Key k) => k.IsEquals(bd));
			if (num == -1)
			{
				Key item = default(Key);
				item.subMaterial = bd.subMaterial;
				item.isOffset = bd.node.d_bOffset;
				item.isBlink = bd.node.d_bBlink;
				item.offsetRect = bd.node.d_rectOffset;
				item.nodes = ListPool<BaseData>.Get();
				s_keys.Add(item);
				item.nodes.Add(bd);
			}
			else
			{
				s_keys[num].nodes.Add(bd);
			}
		}
		vh.Clear();
		for (int j = 0; j < s_keys.Count; j++)
		{
			Key key = s_keys[j];
			for (int l = 0; l < key.nodes.Count; l++)
			{
				key.nodes[l].Render(vh, rect, offset, pixelsPerUnit);
			}
			if (vh.currentVertCount != 0)
			{
				Draw draw = key.Get(mOwner, materials[key.subMaterial]);
				vh.FillMesh(workerMesh);
				draw.FillMesh(workerMesh);
				vh.Clear();
			}
			ListPool<BaseData>.Release(key.nodes);
		}
		s_keys.Clear();
	}

	public BaseData Get(Vector2 pos)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		BaseData baseData = null;
		pos -= DrawOffset;
		for (int i = 0; i < DataList.Count; i++)
		{
			baseData = DataList[i];
			if (baseData.isContain(pos))
			{
				return baseData;
			}
		}
		return null;
	}

	public void Get(List<BaseData> bds, NodeBase nb)
	{
		BaseData baseData = null;
		for (int i = 0; i < DataList.Count; i++)
		{
			baseData = DataList[i];
			if (baseData.node == nb)
			{
				bds.Add(baseData);
			}
		}
	}
}
