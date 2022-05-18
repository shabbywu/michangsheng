using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace WXB
{
	// Token: 0x020009D1 RID: 2513
	public class TextNode : NodeBase
	{
		// Token: 0x06003FF7 RID: 16375 RVA: 0x0002DE6F File Offset: 0x0002C06F
		public bool isFontSame(TextNode n)
		{
			return this.d_font == n.d_font && this.d_fontSize == n.d_fontSize && this.d_fontStyle == n.d_fontStyle;
		}

		// Token: 0x06003FF8 RID: 16376 RVA: 0x0002DEA3 File Offset: 0x0002C0A3
		public override void Reset(Owner o, Anchor hf)
		{
			base.Reset(o, hf);
			this.d_font = null;
			this.d_bUnderline = false;
			this.d_bStrickout = false;
		}

		// Token: 0x06003FF9 RID: 16377 RVA: 0x0002DEC2 File Offset: 0x0002C0C2
		protected virtual bool isUnderLine()
		{
			return this.d_bUnderline;
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06003FFA RID: 16378 RVA: 0x0002DECA File Offset: 0x0002C0CA
		public virtual Color currentColor
		{
			get
			{
				return this.d_color;
			}
		}

		// Token: 0x06003FFB RID: 16379 RVA: 0x0002DED2 File Offset: 0x0002C0D2
		public override float getHeight()
		{
			return this.size.y;
		}

		// Token: 0x06003FFC RID: 16380 RVA: 0x0002DEDF File Offset: 0x0002C0DF
		public override float getWidth()
		{
			return this.size.x;
		}

		// Token: 0x06003FFD RID: 16381 RVA: 0x001BB14C File Offset: 0x001B934C
		protected override void UpdateWidthList(out List<NodeBase.Element> widths, float pixelsPerUnit)
		{
			widths = this.d_widthList;
			this.d_widthList.Clear();
			if (this.d_text.Length == 0)
			{
				return;
			}
			float unitsPerPixel = 1f / pixelsPerUnit;
			int fontsize = (int)((float)this.d_fontSize * pixelsPerUnit);
			this.size.x = 0f;
			this.size.y = (float)FontCache.GetLineHeight(this.d_font, fontsize, this.d_fontStyle) * unitsPerPixel;
			Func<char, float> func = (char code) => (float)FontCache.GetAdvance(this.d_font, fontsize, this.d_fontStyle, code) * unitsPerPixel;
			ElementSegment elementSegment = this.owner.elementSegment;
			if (elementSegment == null)
			{
				for (int i = 0; i < this.d_text.Length; i++)
				{
					NodeBase.Element item = new NodeBase.Element(func(this.d_text[i]));
					widths.Add(item);
				}
			}
			else
			{
				elementSegment.Segment(this.d_text, widths, func);
			}
			for (int j = 0; j < this.d_widthList.Count; j++)
			{
				this.size.x = this.size.x + this.d_widthList[j].totalwidth;
			}
		}

		// Token: 0x06003FFE RID: 16382 RVA: 0x00004050 File Offset: 0x00002250
		public virtual bool IsHyText()
		{
			return false;
		}

		// Token: 0x06003FFF RID: 16383 RVA: 0x001BB284 File Offset: 0x001B9484
		public override void render(float maxWidth, RenderCache cache, ref float x, ref uint yline, List<Line> lines, float offsetX, float offsetY)
		{
			if (this.d_font == null)
			{
				return;
			}
			using (PD<StringBuilder> sb = Pool.GetSB())
			{
				TextNode.Helper helper = new TextNode.Helper(maxWidth, cache, x, yline, lines, this.formatting, offsetX, offsetY, sb.value);
				helper.Draw(this);
				x = helper.x;
				yline = helper.yline;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06004000 RID: 16384 RVA: 0x001BB300 File Offset: 0x001B9500
		public new long keyPrefix
		{
			get
			{
				long num = base.keyPrefix;
				if (this.d_bDynStrickout)
				{
					num += 8192L;
				}
				if (this.d_bDynUnderline)
				{
					num += 8192L;
				}
				return num;
			}
		}

		// Token: 0x06004001 RID: 16385 RVA: 0x001BB338 File Offset: 0x001B9538
		public override void SetConfig(TextParser.Config c)
		{
			base.SetConfig(c);
			this.d_font = c.font;
			this.d_bUnderline = c.isUnderline;
			this.d_fontSize = c.fontSize;
			this.d_fontStyle = c.fontStyle;
			this.d_bStrickout = c.isStrickout;
			this.d_bDynUnderline = c.isDyncUnderline;
			this.d_bDynStrickout = c.isDyncStrickout;
			this.d_dynSpeed = c.dyncSpeed;
			this.effectType = c.effectType;
			this.effectColor = c.effectColor;
			this.effectDistance = c.effectDistance;
		}

		// Token: 0x06004002 RID: 16386 RVA: 0x0002DEEC File Offset: 0x0002C0EC
		public void GetLineCharacterInfo(out CharacterInfo info)
		{
			if (!this.d_font.GetCharacterInfo('_', ref info, 20, 1))
			{
				this.d_font.RequestCharactersInTexture("_", 20, 1);
				this.d_font.GetCharacterInfo('_', ref info, 20, 1);
			}
		}

		// Token: 0x06004003 RID: 16387 RVA: 0x0002DF26 File Offset: 0x0002C126
		public override void Release()
		{
			base.Release();
			this.d_text = null;
			this.d_font = null;
			this.d_fontSize = 0;
			this.d_bUnderline = false;
			this.d_bDynUnderline = false;
			this.d_bDynStrickout = false;
			this.d_dynSpeed = 0;
		}

		// Token: 0x0400391C RID: 14620
		private Vector2 size = Vector2.zero;

		// Token: 0x0400391D RID: 14621
		private List<NodeBase.Element> d_widthList = new List<NodeBase.Element>();

		// Token: 0x0400391E RID: 14622
		public string d_text;

		// Token: 0x0400391F RID: 14623
		public Font d_font;

		// Token: 0x04003920 RID: 14624
		public int d_fontSize;

		// Token: 0x04003921 RID: 14625
		public FontStyle d_fontStyle;

		// Token: 0x04003922 RID: 14626
		public bool d_bUnderline;

		// Token: 0x04003923 RID: 14627
		public bool d_bStrickout;

		// Token: 0x04003924 RID: 14628
		public bool d_bDynUnderline;

		// Token: 0x04003925 RID: 14629
		public bool d_bDynStrickout;

		// Token: 0x04003926 RID: 14630
		public int d_dynSpeed;

		// Token: 0x04003927 RID: 14631
		public EffectType effectType;

		// Token: 0x04003928 RID: 14632
		public Color effectColor;

		// Token: 0x04003929 RID: 14633
		public Vector2 effectDistance;

		// Token: 0x020009D2 RID: 2514
		private struct Helper
		{
			// Token: 0x06004005 RID: 16389 RVA: 0x001BB3D0 File Offset: 0x001B95D0
			public Helper(float maxWidth, RenderCache cache, float x, uint yline, List<Line> lines, Anchor xFormatting, float offsetX, float offsetY, StringBuilder sb)
			{
				this.maxWidth = maxWidth;
				this.cache = cache;
				this.x = x;
				this.yline = yline;
				this.lines = lines;
				this.xFormatting = xFormatting;
				this.offsetX = offsetX;
				this.offsetY = offsetY;
				this.pixelsPerUnit = 1f;
				this.alignedX = 0f;
				this.pt = Vector2.zero;
				this.node = null;
				this.fHeight = 0f;
				this.sb = sb;
			}

			// Token: 0x06004006 RID: 16390 RVA: 0x001BB458 File Offset: 0x001B9658
			private void DrawCurrent(bool isnewLine, Around around)
			{
				if (this.sb.Length != 0)
				{
					Rect rect;
					rect..ctor(this.pt.x + this.alignedX, this.pt.y, this.x - this.pt.x + this.offsetX, this.node.getHeight());
					this.cache.cacheText(this.lines[(int)this.yline], this.node, this.sb.ToString(), rect);
					this.sb.Remove(0, this.sb.Length);
				}
				if (isnewLine)
				{
					this.yline += 1U;
					this.x = 0f;
					this.pt.x = this.offsetX;
					this.pt.y = this.offsetY;
					int num = 0;
					while ((long)num < (long)((ulong)this.yline))
					{
						this.pt.y = this.pt.y + this.lines[num].y;
						num++;
					}
					if ((ulong)this.yline >= (ulong)((long)this.lines.Count))
					{
						this.yline -= 1U;
					}
					this.alignedX = NodeBase.AlignedFormatting(this.node.owner, this.xFormatting, this.maxWidth, this.lines[(int)this.yline].x);
					float num2;
					if (!around.isContain(this.pt.x + this.alignedX, this.pt.y, 1f, this.node.getHeight(), out num2))
					{
						this.pt.x = num2 - this.alignedX;
						this.x = this.pt.x;
					}
				}
			}

			// Token: 0x06004007 RID: 16391 RVA: 0x001BB630 File Offset: 0x001B9830
			public void Draw(TextNode n)
			{
				this.node = n;
				this.pt = new Vector2(this.x + this.offsetX, this.offsetY);
				int num = 0;
				while ((long)num < (long)((ulong)this.yline))
				{
					this.pt.y = this.pt.y + this.lines[num].y;
					num++;
				}
				if (this.maxWidth == 0f)
				{
					return;
				}
				this.alignedX = NodeBase.AlignedFormatting(n.owner, this.xFormatting, this.maxWidth, this.lines[(int)this.yline].x);
				this.fHeight = this.node.getHeight();
				this.sb.Remove(0, this.sb.Length);
				Around around = n.owner.around;
				int num2 = 0;
				float num3 = 0f;
				for (int i = 0; i < this.node.d_widthList.Count; i++)
				{
					NodeBase.Element element = this.node.d_widthList[i];
					float totalwidth = element.totalwidth;
					if (this.x + totalwidth > this.maxWidth)
					{
						if (this.x != 0f)
						{
							this.DrawCurrent(true, around);
						}
						if (element.widths == null)
						{
							if (this.x + element.totalwidth > this.maxWidth)
							{
								this.DrawCurrent(true, around);
							}
							else
							{
								this.x += element.totalwidth;
								this.sb.Append(this.node.d_text[num2++]);
							}
						}
						else
						{
							int j = 0;
							while (j < element.widths.Count)
							{
								if (this.x != 0f && this.x + element.widths[j] > this.maxWidth)
								{
									this.DrawCurrent(true, around);
								}
								else
								{
									this.x += element.widths[j];
									this.sb.Append(this.node.d_text[num2++]);
									j++;
								}
							}
						}
					}
					else if (!around.isContain(this.x, this.pt.y, totalwidth, this.fHeight, out num3))
					{
						this.DrawCurrent(false, around);
						this.x = num3;
						this.pt.x = num3;
						i--;
					}
					else
					{
						int count = element.count;
						this.sb.Append(this.node.d_text.Substring(num2, count));
						num2 += count;
						this.x += totalwidth;
					}
				}
				if (this.sb.Length != 0)
				{
					this.DrawCurrent(false, around);
				}
				if (this.node.d_bNewLine)
				{
					this.yline += 1U;
					this.x = 0f;
				}
			}

			// Token: 0x0400392A RID: 14634
			public RenderCache cache;

			// Token: 0x0400392B RID: 14635
			public float x;

			// Token: 0x0400392C RID: 14636
			public uint yline;

			// Token: 0x0400392D RID: 14637
			public List<Line> lines;

			// Token: 0x0400392E RID: 14638
			public Anchor xFormatting;

			// Token: 0x0400392F RID: 14639
			public float offsetX;

			// Token: 0x04003930 RID: 14640
			public float offsetY;

			// Token: 0x04003931 RID: 14641
			public StringBuilder sb;

			// Token: 0x04003932 RID: 14642
			public float pixelsPerUnit;

			// Token: 0x04003933 RID: 14643
			private Vector2 pt;

			// Token: 0x04003934 RID: 14644
			private float alignedX;

			// Token: 0x04003935 RID: 14645
			private TextNode node;

			// Token: 0x04003936 RID: 14646
			private float maxWidth;

			// Token: 0x04003937 RID: 14647
			private float fHeight;
		}
	}
}
