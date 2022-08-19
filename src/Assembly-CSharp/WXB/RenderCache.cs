using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Sprites;
using UnityEngine.UI;

namespace WXB
{
	// Token: 0x020006AA RID: 1706
	public class RenderCache
	{
		// Token: 0x060035D1 RID: 13777 RVA: 0x00172032 File Offset: 0x00170232
		public RenderCache(Owner st)
		{
			this.mOwner = st;
			this.materials = new List<Texture>();
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060035D2 RID: 13778 RVA: 0x00172057 File Offset: 0x00170257
		// (set) Token: 0x060035D3 RID: 13779 RVA: 0x0017205F File Offset: 0x0017025F
		public List<Texture> materials { get; protected set; }

		// Token: 0x060035D4 RID: 13780 RVA: 0x00172068 File Offset: 0x00170268
		public void Release()
		{
			this.materials.Clear();
			for (int i = 0; i < this.DataList.Count; i++)
			{
				RenderCache.BaseData baseData = this.DataList[i];
				baseData.Release();
				if (baseData is RenderCache.TextData)
				{
					PoolData<RenderCache.TextData>.Free((RenderCache.TextData)baseData);
				}
				else if (baseData is RenderCache.SpriteData)
				{
					PoolData<RenderCache.SpriteData>.Free((RenderCache.SpriteData)baseData);
				}
			}
			this.DataList.Clear();
		}

		// Token: 0x060035D5 RID: 13781 RVA: 0x001720E0 File Offset: 0x001702E0
		public void cacheText(Line l, TextNode n, string text, Rect rect)
		{
			RenderCache.TextData textData = PoolData<RenderCache.TextData>.Get();
			textData.Reset(n, text, rect, l);
			this.DataList.Add(textData);
			textData.subMaterial = this.materials.IndexOf(n.d_font.material.mainTexture);
			if (textData.subMaterial == -1)
			{
				textData.subMaterial = this.materials.Count;
				this.materials.Add(n.d_font.material.mainTexture);
			}
		}

		// Token: 0x060035D6 RID: 13782 RVA: 0x00172160 File Offset: 0x00170360
		public void cacheSprite(Line l, NodeBase n, Sprite sprite, Rect rect)
		{
			if (sprite != null)
			{
				RenderCache.SpriteData spriteData = PoolData<RenderCache.SpriteData>.Get();
				spriteData.Reset(n, sprite, rect, l);
				this.DataList.Add(spriteData);
				spriteData.subMaterial = this.materials.IndexOf(sprite.texture);
				if (spriteData.subMaterial == -1)
				{
					spriteData.subMaterial = this.materials.Count;
					this.materials.Add(sprite.texture);
				}
			}
		}

		// Token: 0x060035D7 RID: 13783 RVA: 0x001721D8 File Offset: 0x001703D8
		public void cacheCartoon(Line l, NodeBase n, Cartoon cartoon, Rect rect)
		{
			if (cartoon != null)
			{
				RenderCache.CartoonData cartoonData = PoolData<RenderCache.CartoonData>.Get();
				cartoonData.Reset(n, cartoon, rect, l);
				this.DataList.Add(cartoonData);
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060035D8 RID: 13784 RVA: 0x00172205 File Offset: 0x00170405
		public bool isEmpty
		{
			get
			{
				return this.DataList.Count == 0;
			}
		}

		// Token: 0x060035D9 RID: 13785 RVA: 0x00172218 File Offset: 0x00170418
		public void OnAlignByGeometry(ref Vector2 offset, float pixelsPerUnit, float firstHeight)
		{
			for (int i = 0; i < this.DataList.Count; i++)
			{
				if (this.DataList[i].rect.y <= firstHeight)
				{
					if (!this.DataList[i].isAlignByGeometry)
					{
						offset = Vector2.zero;
						return;
					}
					this.DataList[i].OnAlignByGeometry(ref offset, pixelsPerUnit);
				}
			}
		}

		// Token: 0x060035DA RID: 13786 RVA: 0x0017228C File Offset: 0x0017048C
		public void OnCheckLineY(float pixelsPerUnit)
		{
			for (int i = 0; i < this.DataList.Count; i++)
			{
				this.DataList[i].OnLineYCheck(pixelsPerUnit);
			}
		}

		// Token: 0x060035DB RID: 13787 RVA: 0x001722C4 File Offset: 0x001704C4
		public void Render(VertexHelper vh, Rect rect, Vector2 offset, float pixelsPerUnit, Mesh workerMesh, Material defaultMaterial)
		{
			this.DrawOffset = offset;
			RenderCache.s_keys.Clear();
			for (int i = 0; i < this.DataList.Count; i++)
			{
				RenderCache.BaseData bd = this.DataList[i];
				int num = RenderCache.s_keys.FindIndex((RenderCache.Key k) => k.IsEquals(bd));
				if (num == -1)
				{
					RenderCache.Key key = default(RenderCache.Key);
					key.subMaterial = bd.subMaterial;
					key.isOffset = bd.node.d_bOffset;
					key.isBlink = bd.node.d_bBlink;
					key.offsetRect = bd.node.d_rectOffset;
					key.nodes = ListPool<RenderCache.BaseData>.Get();
					RenderCache.s_keys.Add(key);
					key.nodes.Add(bd);
				}
				else
				{
					RenderCache.s_keys[num].nodes.Add(bd);
				}
			}
			vh.Clear();
			for (int j = 0; j < RenderCache.s_keys.Count; j++)
			{
				RenderCache.Key key2 = RenderCache.s_keys[j];
				for (int l = 0; l < key2.nodes.Count; l++)
				{
					key2.nodes[l].Render(vh, rect, offset, pixelsPerUnit);
				}
				if (vh.currentVertCount != 0)
				{
					Draw draw = key2.Get(this.mOwner, this.materials[key2.subMaterial]);
					vh.FillMesh(workerMesh);
					draw.FillMesh(workerMesh);
					vh.Clear();
				}
				ListPool<RenderCache.BaseData>.Release(key2.nodes);
			}
			RenderCache.s_keys.Clear();
		}

		// Token: 0x060035DC RID: 13788 RVA: 0x00172490 File Offset: 0x00170690
		public RenderCache.BaseData Get(Vector2 pos)
		{
			pos -= this.DrawOffset;
			for (int i = 0; i < this.DataList.Count; i++)
			{
				RenderCache.BaseData baseData = this.DataList[i];
				if (baseData.isContain(pos))
				{
					return baseData;
				}
			}
			return null;
		}

		// Token: 0x060035DD RID: 13789 RVA: 0x001724DC File Offset: 0x001706DC
		public void Get(List<RenderCache.BaseData> bds, NodeBase nb)
		{
			for (int i = 0; i < this.DataList.Count; i++)
			{
				RenderCache.BaseData baseData = this.DataList[i];
				if (baseData.node == nb)
				{
					bds.Add(baseData);
				}
			}
		}

		// Token: 0x04002F29 RID: 12073
		private Owner mOwner;

		// Token: 0x04002F2A RID: 12074
		private List<RenderCache.BaseData> DataList = new List<RenderCache.BaseData>();

		// Token: 0x04002F2C RID: 12076
		private static List<RenderCache.Key> s_keys = new List<RenderCache.Key>();

		// Token: 0x04002F2D RID: 12077
		private Vector2 DrawOffset;

		// Token: 0x02001501 RID: 5377
		public abstract class BaseData
		{
			// Token: 0x17000B27 RID: 2855
			// (get) Token: 0x0600829E RID: 33438 RVA: 0x002DB734 File Offset: 0x002D9934
			// (set) Token: 0x0600829D RID: 33437 RVA: 0x002DB72B File Offset: 0x002D992B
			public Rect rect
			{
				get
				{
					return this.m_Rect;
				}
				set
				{
					this.m_Rect = value;
				}
			}

			// Token: 0x17000B28 RID: 2856
			// (get) Token: 0x0600829F RID: 33439 RVA: 0x002DB73C File Offset: 0x002D993C
			// (set) Token: 0x060082A0 RID: 33440 RVA: 0x002DB744 File Offset: 0x002D9944
			public Line line { get; protected set; }

			// Token: 0x060082A1 RID: 33441 RVA: 0x002DB74D File Offset: 0x002D994D
			public bool isContain(Vector2 pos)
			{
				return this.m_Rect.Contains(pos);
			}

			// Token: 0x17000B29 RID: 2857
			// (get) Token: 0x060082A2 RID: 33442 RVA: 0x0000280F File Offset: 0x00000A0F
			public virtual bool isAlignByGeometry
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060082A3 RID: 33443
			public abstract void Render(VertexHelper vh, Rect area, Vector2 offset, float pixelsPerUnit);

			// Token: 0x060082A4 RID: 33444 RVA: 0x00004095 File Offset: 0x00002295
			public virtual void OnMouseEnter()
			{
			}

			// Token: 0x060082A5 RID: 33445 RVA: 0x00004095 File Offset: 0x00002295
			public virtual void OnMouseLevel()
			{
			}

			// Token: 0x060082A6 RID: 33446 RVA: 0x00004095 File Offset: 0x00002295
			public virtual void OnMouseUp(PointerEventData eventData)
			{
			}

			// Token: 0x17000B2A RID: 2858
			// (get) Token: 0x060082A7 RID: 33447 RVA: 0x002DB75B File Offset: 0x002D995B
			// (set) Token: 0x060082A8 RID: 33448 RVA: 0x002DB763 File Offset: 0x002D9963
			public int subMaterial { get; set; }

			// Token: 0x060082A9 RID: 33449 RVA: 0x002DB76C File Offset: 0x002D996C
			public virtual void Release()
			{
				this.node = null;
				this.line = null;
				this.OnRelease();
			}

			// Token: 0x060082AA RID: 33450
			protected abstract void OnRelease();

			// Token: 0x060082AB RID: 33451 RVA: 0x00004095 File Offset: 0x00002295
			public virtual void OnAlignByGeometry(ref Vector2 offset, float pixelsPerUnit)
			{
			}

			// Token: 0x060082AC RID: 33452 RVA: 0x00004095 File Offset: 0x00002295
			public virtual void OnLineYCheck(float pixelsPerUnit)
			{
			}

			// Token: 0x17000B2B RID: 2859
			// (get) Token: 0x060082AD RID: 33453 RVA: 0x002DB782 File Offset: 0x002D9982
			protected LineAlignment lineAlignment
			{
				get
				{
					if (this.node.lineAlignment != LineAlignment.Default)
					{
						return this.node.lineAlignment;
					}
					return this.node.owner.lineAlignment;
				}
			}

			// Token: 0x060082AE RID: 33454 RVA: 0x002DB7B0 File Offset: 0x002D99B0
			public virtual Vector2 GetStartLeftBottom(float unitsPerPixel)
			{
				if (this.line == null)
				{
					return new Vector2(this.rect.x, this.rect.y + this.rect.height);
				}
				Vector2 result;
				result..ctor(this.rect.x, this.rect.y + this.rect.height);
				switch (this.lineAlignment)
				{
				case LineAlignment.Center:
					if (this.line.y != this.rect.height)
					{
						float num = (this.line.y - this.rect.height) * 0.5f;
						result.y += num;
					}
					break;
				case LineAlignment.Bottom:
					result.y = this.rect.y + this.line.y;
					break;
				}
				return result;
			}

			// Token: 0x04006E08 RID: 28168
			private Rect m_Rect;

			// Token: 0x04006E09 RID: 28169
			public NodeBase node;
		}

		// Token: 0x02001502 RID: 5378
		private struct Key
		{
			// Token: 0x060082B0 RID: 33456 RVA: 0x002DB8B0 File Offset: 0x002D9AB0
			public bool IsEquals(RenderCache.BaseData bd)
			{
				return this.subMaterial == bd.subMaterial && this.isBlink == bd.node.d_bBlink && ((!this.isOffset && !bd.node.d_bOffset) || (this.isOffset == bd.node.d_bOffset && this.offsetRect == bd.node.d_rectOffset));
			}

			// Token: 0x17000B2C RID: 2860
			// (get) Token: 0x060082B1 RID: 33457 RVA: 0x002DB922 File Offset: 0x002D9B22
			public DrawType drawType
			{
				get
				{
					if (this.isBlink)
					{
						if (this.isOffset)
						{
							return DrawType.OffsetAndAlpha;
						}
						return DrawType.Alpha;
					}
					else
					{
						if (this.isOffset)
						{
							return DrawType.Offset;
						}
						return DrawType.Default;
					}
				}
			}

			// Token: 0x060082B2 RID: 33458 RVA: 0x002DB944 File Offset: 0x002D9B44
			public Draw Get(Owner owner, Texture texture)
			{
				long num = this.nodes[0].node.keyPrefix;
				num += (long)texture.GetInstanceID();
				return owner.GetDraw(this.drawType, num, delegate(Draw d, object p)
				{
					d.texture = texture;
					if (d is OffsetDraw)
					{
						(d as OffsetDraw).Set((Rect)p);
					}
				}, this.offsetRect);
			}

			// Token: 0x04006E0C RID: 28172
			public int subMaterial;

			// Token: 0x04006E0D RID: 28173
			public bool isBlink;

			// Token: 0x04006E0E RID: 28174
			public bool isOffset;

			// Token: 0x04006E0F RID: 28175
			public Rect offsetRect;

			// Token: 0x04006E10 RID: 28176
			public List<RenderCache.BaseData> nodes;
		}

		// Token: 0x02001503 RID: 5379
		private class CartoonData : RenderCache.BaseData
		{
			// Token: 0x060082B3 RID: 33459 RVA: 0x002DB9A8 File Offset: 0x002D9BA8
			public void Reset(NodeBase n, Cartoon c, Rect r, Line l)
			{
				this.node = n;
				this.cartoon = c;
				base.rect = r;
				base.line = l;
			}

			// Token: 0x060082B4 RID: 33460 RVA: 0x002DB9C7 File Offset: 0x002D9BC7
			protected override void OnRelease()
			{
				this.cartoon = null;
			}

			// Token: 0x060082B5 RID: 33461 RVA: 0x002DB9D0 File Offset: 0x002D9BD0
			public override void Render(VertexHelper vh, Rect area, Vector2 offset, float pixelsPerUnit)
			{
				Color d_color = this.node.d_color;
				if (d_color.a <= 0.01f)
				{
					return;
				}
				Vector2 leftPos = this.GetStartLeftBottom(1f) + offset;
				Tools.LB2LT(ref leftPos, area.height);
				(this.node.owner.GetDraw(DrawType.Cartoon, this.node.keyPrefix + (long)this.cartoon.GetHashCode(), delegate(Draw d, object p)
				{
					CartoonDraw cartoonDraw = d as CartoonDraw;
					cartoonDraw.cartoon = this.cartoon;
					cartoonDraw.isOpenAlpha = this.node.d_bBlink;
					cartoonDraw.isOpenOffset = false;
				}, null) as CartoonDraw).Add(leftPos, base.rect.width, base.rect.height, d_color);
			}

			// Token: 0x04006E11 RID: 28177
			public Cartoon cartoon;
		}

		// Token: 0x02001504 RID: 5380
		private class SpriteData : RenderCache.BaseData
		{
			// Token: 0x060082B8 RID: 33464 RVA: 0x002DBAA8 File Offset: 0x002D9CA8
			public void Reset(NodeBase n, Sprite s, Rect r, Line l)
			{
				this.node = n;
				this.sprite = s;
				base.rect = r;
				base.line = l;
			}

			// Token: 0x060082B9 RID: 33465 RVA: 0x002DBAC7 File Offset: 0x002D9CC7
			protected override void OnRelease()
			{
				this.sprite = null;
			}

			// Token: 0x060082BA RID: 33466 RVA: 0x002DBAD0 File Offset: 0x002D9CD0
			public override void Render(VertexHelper vh, Rect area, Vector2 offset, float pixelsPerUnit)
			{
				Color d_color = this.node.d_color;
				if (d_color.a <= 0.01f)
				{
					return;
				}
				Vector4 outerUV = DataUtility.GetOuterUV(this.sprite);
				Vector2 vector = this.GetStartLeftBottom(1f) + offset;
				Tools.LB2LT(ref vector, area.height);
				float width = base.rect.width;
				float height = base.rect.height;
				int currentVertCount = vh.currentVertCount;
				vh.AddVert(new Vector3(vector.x, vector.y), d_color, new Vector2(outerUV.x, outerUV.y));
				vh.AddVert(new Vector3(vector.x, vector.y + height), d_color, new Vector2(outerUV.x, outerUV.w));
				vh.AddVert(new Vector3(vector.x + width, vector.y + height), d_color, new Vector2(outerUV.z, outerUV.w));
				vh.AddVert(new Vector3(vector.x + width, vector.y), d_color, new Vector2(outerUV.z, outerUV.y));
				vh.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
				vh.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
			}

			// Token: 0x04006E12 RID: 28178
			public Sprite sprite;
		}

		// Token: 0x02001505 RID: 5381
		public class TextData : RenderCache.BaseData
		{
			// Token: 0x060082BC RID: 33468 RVA: 0x002DBC2E File Offset: 0x002D9E2E
			public void Reset(TextNode n, string t, Rect r, Line l)
			{
				this.node = n;
				this.text = t;
				base.rect = r;
				base.line = l;
			}

			// Token: 0x060082BD RID: 33469 RVA: 0x002DBC4D File Offset: 0x002D9E4D
			protected override void OnRelease()
			{
				this.text = null;
			}

			// Token: 0x17000B2D RID: 2861
			// (get) Token: 0x060082BE RID: 33470 RVA: 0x00024C5F File Offset: 0x00022E5F
			public override bool isAlignByGeometry
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000B2E RID: 2862
			// (get) Token: 0x060082BF RID: 33471 RVA: 0x002DBC56 File Offset: 0x002D9E56
			// (set) Token: 0x060082C0 RID: 33472 RVA: 0x002DBC63 File Offset: 0x002D9E63
			public TextNode Node
			{
				get
				{
					return (TextNode)this.node;
				}
				set
				{
					this.node = value;
				}
			}

			// Token: 0x060082C1 RID: 33473 RVA: 0x002DBC6C File Offset: 0x002D9E6C
			private static TextAnchor SwitchTextAnchor(TextAnchor anchor)
			{
				switch (anchor)
				{
				case 0:
				case 1:
				case 2:
					return 0;
				case 3:
				case 4:
				case 5:
					return 4;
				case 6:
				case 7:
				case 8:
					return 6;
				default:
					return 4;
				}
			}

			// Token: 0x060082C2 RID: 33474 RVA: 0x002DBCA4 File Offset: 0x002D9EA4
			public override void OnLineYCheck(float pixelsPerUnit)
			{
				if (base.line == null)
				{
					return;
				}
				TextNode node = this.Node;
				Font d_font = node.d_font;
				FontStyle d_fontStyle = node.d_fontStyle;
				int num = (int)((float)node.d_fontSize * pixelsPerUnit);
				d_font.RequestCharactersInTexture(this.text, num, d_fontStyle);
				for (int i = 0; i < this.text.Length; i++)
				{
					if (d_font.GetCharacterInfo(this.text[i], ref RenderCache.TextData.s_Info, num, d_fontStyle))
					{
						base.line.minY = Mathf.Min((float)RenderCache.TextData.s_Info.minY, base.line.minY);
						base.line.maxY = Mathf.Max((float)RenderCache.TextData.s_Info.maxY, base.line.maxY);
					}
				}
			}

			// Token: 0x060082C3 RID: 33475 RVA: 0x002DBD64 File Offset: 0x002D9F64
			public override void OnAlignByGeometry(ref Vector2 offset, float pixelsPerUnit)
			{
				if (base.line == null)
				{
					return;
				}
				TextNode node = this.Node;
				Font d_font = node.d_font;
				FontStyle d_fontStyle = node.d_fontStyle;
				int num = (int)((float)node.d_fontSize * pixelsPerUnit);
				d_font.RequestCharactersInTexture(this.text, num, d_fontStyle);
				if (base.rect.x == 0f && d_font.GetCharacterInfo(this.text[0], ref RenderCache.TextData.s_Info, num, d_fontStyle))
				{
					offset.x = (float)RenderCache.TextData.s_Info.minX;
				}
				float num2 = float.MinValue;
				for (int i = 0; i < this.text.Length; i++)
				{
					if (d_font.GetCharacterInfo(this.text[i], ref RenderCache.TextData.s_Info, num, d_fontStyle))
					{
						num2 = Mathf.Max((float)RenderCache.TextData.s_Info.maxY, offset.y);
					}
				}
				offset.y = Mathf.Max(offset.y, base.line.y - num2);
			}

			// Token: 0x060082C4 RID: 33476 RVA: 0x002DBE5C File Offset: 0x002DA05C
			public override Vector2 GetStartLeftBottom(float unitsPerPixel)
			{
				Vector2 result;
				result..ctor(base.rect.x, base.rect.y + base.rect.height);
				switch (base.lineAlignment)
				{
				case LineAlignment.Top:
					result.y = base.rect.y + base.line.maxY * unitsPerPixel;
					break;
				case LineAlignment.Center:
					result.y = base.rect.y + base.line.maxY * unitsPerPixel;
					result.y += (base.line.y - (base.line.maxY - base.line.minY) * unitsPerPixel) * 0.5f;
					break;
				case LineAlignment.Bottom:
					result.y = base.rect.y + base.line.y + base.line.minY * unitsPerPixel;
					break;
				}
				return result;
			}

			// Token: 0x060082C5 RID: 33477 RVA: 0x002DBF6C File Offset: 0x002DA16C
			public override void Render(VertexHelper vh, Rect area, Vector2 offset, float pixelsPerUnit)
			{
				TextNode node = this.Node;
				Color currentColor = node.currentColor;
				if (currentColor.a <= 0.01f)
				{
					return;
				}
				int currentIndexCount = vh.currentIndexCount;
				float num = 1f / pixelsPerUnit;
				Vector2 vector = this.GetStartLeftBottom(num) + offset;
				Tools.LB2LT(ref vector, area.height);
				float num2 = float.MaxValue;
				float num3 = float.MinValue;
				Font d_font = node.d_font;
				FontStyle d_fontStyle = node.d_fontStyle;
				int num4 = (int)((float)node.d_fontSize * pixelsPerUnit);
				d_font.RequestCharactersInTexture(this.text, num4, d_fontStyle);
				Vector2 zero = Vector2.zero;
				for (int i = 0; i < this.text.Length; i++)
				{
					if (d_font.GetCharacterInfo(this.text[i], ref RenderCache.TextData.s_Info, num4, d_fontStyle))
					{
						int currentVertCount = vh.currentVertCount;
						Rect rect = Rect.MinMaxRect(vector.x + (zero.x + (float)RenderCache.TextData.s_Info.minX) * num, vector.y + (zero.y + (float)RenderCache.TextData.s_Info.minY) * num, vector.x + (zero.x + (float)RenderCache.TextData.s_Info.maxX) * num, vector.y + (zero.y + (float)RenderCache.TextData.s_Info.maxY) * num);
						vh.AddVert(new Vector3(rect.xMin, rect.yMax), currentColor, RenderCache.TextData.s_Info.uvTopLeft);
						vh.AddVert(new Vector3(rect.xMax, rect.yMax), currentColor, RenderCache.TextData.s_Info.uvTopRight);
						vh.AddVert(new Vector3(rect.xMax, rect.yMin), currentColor, RenderCache.TextData.s_Info.uvBottomRight);
						vh.AddVert(new Vector3(rect.xMin, rect.yMin), currentColor, RenderCache.TextData.s_Info.uvBottomLeft);
						vh.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
						vh.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
						zero.x += (float)RenderCache.TextData.s_Info.advance;
						num2 = Mathf.Min(rect.yMin, num2);
						num3 = Mathf.Max(rect.yMax, num3);
					}
				}
				bool flag = false;
				if (node.d_bDynUnderline || node.d_bUnderline)
				{
					flag = true;
					node.GetLineCharacterInfo(out RenderCache.TextData.s_Info);
					Vector2 vector2;
					vector2..ctor(vector.x, num2);
					if (node.d_bDynUnderline)
					{
						Texture mainTexture = node.d_font.material.mainTexture;
						(node.owner.GetDraw(DrawType.Outline, node.keyPrefix + (long)mainTexture.GetInstanceID(), delegate(Draw d, object p)
						{
							OutlineDraw outlineDraw = d as OutlineDraw;
							outlineDraw.isOpenAlpha = node.d_bBlink;
							outlineDraw.texture = mainTexture;
						}, null) as OutlineDraw).AddLine(node, vector2, base.rect.width, 2f * num, currentColor, RenderCache.TextData.s_Info.uv.center, node.d_dynSpeed);
					}
					else
					{
						Tools.AddLine(vh, vector2, RenderCache.TextData.s_Info.uv.center, base.rect.width, 2f * num, currentColor);
					}
				}
				if (node.d_bDynStrickout || node.d_bStrickout)
				{
					if (!flag)
					{
						node.GetLineCharacterInfo(out RenderCache.TextData.s_Info);
					}
					Vector2 vector3;
					vector3..ctor(vector.x, (num3 + num2) * 0.5f + 1f * num);
					if (node.d_bDynStrickout)
					{
						Texture mainTexture = node.d_font.material.mainTexture;
						(node.owner.GetDraw(DrawType.Outline, node.keyPrefix + (long)mainTexture.GetInstanceID(), delegate(Draw d, object p)
						{
							OutlineDraw outlineDraw = d as OutlineDraw;
							outlineDraw.isOpenAlpha = node.d_bBlink;
							outlineDraw.texture = mainTexture;
						}, null) as OutlineDraw).AddLine(node, vector3, base.rect.width, 2f * num, currentColor, RenderCache.TextData.s_Info.uv.center, node.d_dynSpeed);
					}
					else
					{
						Tools.AddLine(vh, vector3, RenderCache.TextData.s_Info.uv.center, base.rect.width, 2f * num, currentColor);
					}
				}
				EffectType effectType = node.effectType;
				if (effectType != EffectType.Shadow)
				{
					if (effectType == EffectType.Outline)
					{
						Effect.Outline(vh, currentIndexCount, node.effectColor, node.effectDistance);
						return;
					}
				}
				else
				{
					Effect.Shadow(vh, currentIndexCount, node.effectColor, node.effectDistance);
				}
			}

			// Token: 0x060082C6 RID: 33478 RVA: 0x002DC4D5 File Offset: 0x002DA6D5
			public override void OnMouseEnter()
			{
				this.node.onMouseEnter();
			}

			// Token: 0x060082C7 RID: 33479 RVA: 0x002DC4E2 File Offset: 0x002DA6E2
			public override void OnMouseLevel()
			{
				this.node.onMouseLeave();
			}

			// Token: 0x04006E13 RID: 28179
			public string text;

			// Token: 0x04006E14 RID: 28180
			private const float line_height = 2f;

			// Token: 0x04006E15 RID: 28181
			private static CharacterInfo s_Info;
		}
	}
}
