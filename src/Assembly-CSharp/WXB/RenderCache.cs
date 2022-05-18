using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Sprites;
using UnityEngine.UI;

namespace WXB
{
	// Token: 0x020009C1 RID: 2497
	public class RenderCache
	{
		// Token: 0x06003F92 RID: 16274 RVA: 0x0002DAE9 File Offset: 0x0002BCE9
		public RenderCache(Owner st)
		{
			this.mOwner = st;
			this.materials = new List<Texture>();
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06003F93 RID: 16275 RVA: 0x0002DB0E File Offset: 0x0002BD0E
		// (set) Token: 0x06003F94 RID: 16276 RVA: 0x0002DB16 File Offset: 0x0002BD16
		public List<Texture> materials { get; protected set; }

		// Token: 0x06003F95 RID: 16277 RVA: 0x001B9B44 File Offset: 0x001B7D44
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

		// Token: 0x06003F96 RID: 16278 RVA: 0x001B9BBC File Offset: 0x001B7DBC
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

		// Token: 0x06003F97 RID: 16279 RVA: 0x001B9C3C File Offset: 0x001B7E3C
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

		// Token: 0x06003F98 RID: 16280 RVA: 0x001B9CB4 File Offset: 0x001B7EB4
		public void cacheCartoon(Line l, NodeBase n, Cartoon cartoon, Rect rect)
		{
			if (cartoon != null)
			{
				RenderCache.CartoonData cartoonData = PoolData<RenderCache.CartoonData>.Get();
				cartoonData.Reset(n, cartoon, rect, l);
				this.DataList.Add(cartoonData);
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06003F99 RID: 16281 RVA: 0x0002DB1F File Offset: 0x0002BD1F
		public bool isEmpty
		{
			get
			{
				return this.DataList.Count == 0;
			}
		}

		// Token: 0x06003F9A RID: 16282 RVA: 0x001B9CE4 File Offset: 0x001B7EE4
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

		// Token: 0x06003F9B RID: 16283 RVA: 0x001B9D58 File Offset: 0x001B7F58
		public void OnCheckLineY(float pixelsPerUnit)
		{
			for (int i = 0; i < this.DataList.Count; i++)
			{
				this.DataList[i].OnLineYCheck(pixelsPerUnit);
			}
		}

		// Token: 0x06003F9C RID: 16284 RVA: 0x001B9D90 File Offset: 0x001B7F90
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

		// Token: 0x06003F9D RID: 16285 RVA: 0x001B9F5C File Offset: 0x001B815C
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

		// Token: 0x06003F9E RID: 16286 RVA: 0x001B9FA8 File Offset: 0x001B81A8
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

		// Token: 0x040038EF RID: 14575
		private Owner mOwner;

		// Token: 0x040038F0 RID: 14576
		private List<RenderCache.BaseData> DataList = new List<RenderCache.BaseData>();

		// Token: 0x040038F2 RID: 14578
		private static List<RenderCache.Key> s_keys = new List<RenderCache.Key>();

		// Token: 0x040038F3 RID: 14579
		private Vector2 DrawOffset;

		// Token: 0x020009C2 RID: 2498
		public abstract class BaseData
		{
			// Token: 0x17000719 RID: 1817
			// (get) Token: 0x06003FA1 RID: 16289 RVA: 0x0002DB44 File Offset: 0x0002BD44
			// (set) Token: 0x06003FA0 RID: 16288 RVA: 0x0002DB3B File Offset: 0x0002BD3B
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

			// Token: 0x1700071A RID: 1818
			// (get) Token: 0x06003FA2 RID: 16290 RVA: 0x0002DB4C File Offset: 0x0002BD4C
			// (set) Token: 0x06003FA3 RID: 16291 RVA: 0x0002DB54 File Offset: 0x0002BD54
			public Line line { get; protected set; }

			// Token: 0x06003FA4 RID: 16292 RVA: 0x0002DB5D File Offset: 0x0002BD5D
			public bool isContain(Vector2 pos)
			{
				return this.m_Rect.Contains(pos);
			}

			// Token: 0x1700071B RID: 1819
			// (get) Token: 0x06003FA5 RID: 16293 RVA: 0x00004050 File Offset: 0x00002250
			public virtual bool isAlignByGeometry
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06003FA6 RID: 16294
			public abstract void Render(VertexHelper vh, Rect area, Vector2 offset, float pixelsPerUnit);

			// Token: 0x06003FA7 RID: 16295 RVA: 0x000042DD File Offset: 0x000024DD
			public virtual void OnMouseEnter()
			{
			}

			// Token: 0x06003FA8 RID: 16296 RVA: 0x000042DD File Offset: 0x000024DD
			public virtual void OnMouseLevel()
			{
			}

			// Token: 0x06003FA9 RID: 16297 RVA: 0x000042DD File Offset: 0x000024DD
			public virtual void OnMouseUp(PointerEventData eventData)
			{
			}

			// Token: 0x1700071C RID: 1820
			// (get) Token: 0x06003FAA RID: 16298 RVA: 0x0002DB6B File Offset: 0x0002BD6B
			// (set) Token: 0x06003FAB RID: 16299 RVA: 0x0002DB73 File Offset: 0x0002BD73
			public int subMaterial { get; set; }

			// Token: 0x06003FAC RID: 16300 RVA: 0x0002DB7C File Offset: 0x0002BD7C
			public virtual void Release()
			{
				this.node = null;
				this.line = null;
				this.OnRelease();
			}

			// Token: 0x06003FAD RID: 16301
			protected abstract void OnRelease();

			// Token: 0x06003FAE RID: 16302 RVA: 0x000042DD File Offset: 0x000024DD
			public virtual void OnAlignByGeometry(ref Vector2 offset, float pixelsPerUnit)
			{
			}

			// Token: 0x06003FAF RID: 16303 RVA: 0x000042DD File Offset: 0x000024DD
			public virtual void OnLineYCheck(float pixelsPerUnit)
			{
			}

			// Token: 0x1700071D RID: 1821
			// (get) Token: 0x06003FB0 RID: 16304 RVA: 0x0002DB92 File Offset: 0x0002BD92
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

			// Token: 0x06003FB1 RID: 16305 RVA: 0x001B9FEC File Offset: 0x001B81EC
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

			// Token: 0x040038F4 RID: 14580
			private Rect m_Rect;

			// Token: 0x040038F5 RID: 14581
			public NodeBase node;
		}

		// Token: 0x020009C3 RID: 2499
		private struct Key
		{
			// Token: 0x06003FB3 RID: 16307 RVA: 0x001BA0EC File Offset: 0x001B82EC
			public bool IsEquals(RenderCache.BaseData bd)
			{
				return this.subMaterial == bd.subMaterial && this.isBlink == bd.node.d_bBlink && ((!this.isOffset && !bd.node.d_bOffset) || (this.isOffset == bd.node.d_bOffset && this.offsetRect == bd.node.d_rectOffset));
			}

			// Token: 0x1700071E RID: 1822
			// (get) Token: 0x06003FB4 RID: 16308 RVA: 0x0002DBBE File Offset: 0x0002BDBE
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

			// Token: 0x06003FB5 RID: 16309 RVA: 0x001BA160 File Offset: 0x001B8360
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

			// Token: 0x040038F8 RID: 14584
			public int subMaterial;

			// Token: 0x040038F9 RID: 14585
			public bool isBlink;

			// Token: 0x040038FA RID: 14586
			public bool isOffset;

			// Token: 0x040038FB RID: 14587
			public Rect offsetRect;

			// Token: 0x040038FC RID: 14588
			public List<RenderCache.BaseData> nodes;
		}

		// Token: 0x020009C5 RID: 2501
		private class CartoonData : RenderCache.BaseData
		{
			// Token: 0x06003FB8 RID: 16312 RVA: 0x0002DC06 File Offset: 0x0002BE06
			public void Reset(NodeBase n, Cartoon c, Rect r, Line l)
			{
				this.node = n;
				this.cartoon = c;
				base.rect = r;
				base.line = l;
			}

			// Token: 0x06003FB9 RID: 16313 RVA: 0x0002DC25 File Offset: 0x0002BE25
			protected override void OnRelease()
			{
				this.cartoon = null;
			}

			// Token: 0x06003FBA RID: 16314 RVA: 0x001BA1C4 File Offset: 0x001B83C4
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

			// Token: 0x040038FE RID: 14590
			public Cartoon cartoon;
		}

		// Token: 0x020009C6 RID: 2502
		private class SpriteData : RenderCache.BaseData
		{
			// Token: 0x06003FBD RID: 16317 RVA: 0x0002DC61 File Offset: 0x0002BE61
			public void Reset(NodeBase n, Sprite s, Rect r, Line l)
			{
				this.node = n;
				this.sprite = s;
				base.rect = r;
				base.line = l;
			}

			// Token: 0x06003FBE RID: 16318 RVA: 0x0002DC80 File Offset: 0x0002BE80
			protected override void OnRelease()
			{
				this.sprite = null;
			}

			// Token: 0x06003FBF RID: 16319 RVA: 0x001BA26C File Offset: 0x001B846C
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

			// Token: 0x040038FF RID: 14591
			public Sprite sprite;
		}

		// Token: 0x020009C7 RID: 2503
		public class TextData : RenderCache.BaseData
		{
			// Token: 0x06003FC1 RID: 16321 RVA: 0x0002DC89 File Offset: 0x0002BE89
			public void Reset(TextNode n, string t, Rect r, Line l)
			{
				this.node = n;
				this.text = t;
				base.rect = r;
				base.line = l;
			}

			// Token: 0x06003FC2 RID: 16322 RVA: 0x0002DCA8 File Offset: 0x0002BEA8
			protected override void OnRelease()
			{
				this.text = null;
			}

			// Token: 0x1700071F RID: 1823
			// (get) Token: 0x06003FC3 RID: 16323 RVA: 0x0000A093 File Offset: 0x00008293
			public override bool isAlignByGeometry
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000720 RID: 1824
			// (get) Token: 0x06003FC4 RID: 16324 RVA: 0x0002DCB1 File Offset: 0x0002BEB1
			// (set) Token: 0x06003FC5 RID: 16325 RVA: 0x0002DCBE File Offset: 0x0002BEBE
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

			// Token: 0x06003FC6 RID: 16326 RVA: 0x0002DCC7 File Offset: 0x0002BEC7
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

			// Token: 0x06003FC7 RID: 16327 RVA: 0x001BA3CC File Offset: 0x001B85CC
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

			// Token: 0x06003FC8 RID: 16328 RVA: 0x001BA48C File Offset: 0x001B868C
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

			// Token: 0x06003FC9 RID: 16329 RVA: 0x001BA584 File Offset: 0x001B8784
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

			// Token: 0x06003FCA RID: 16330 RVA: 0x001BA694 File Offset: 0x001B8894
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

			// Token: 0x06003FCB RID: 16331 RVA: 0x0002DCFC File Offset: 0x0002BEFC
			public override void OnMouseEnter()
			{
				this.node.onMouseEnter();
			}

			// Token: 0x06003FCC RID: 16332 RVA: 0x0002DD09 File Offset: 0x0002BF09
			public override void OnMouseLevel()
			{
				this.node.onMouseLeave();
			}

			// Token: 0x04003900 RID: 14592
			public string text;

			// Token: 0x04003901 RID: 14593
			private const float line_height = 2f;

			// Token: 0x04003902 RID: 14594
			private static CharacterInfo s_Info;
		}
	}
}
