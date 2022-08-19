using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace WXB
{
	// Token: 0x020006AF RID: 1711
	public class TextNode : NodeBase
	{
		// Token: 0x060035FA RID: 13818 RVA: 0x00172914 File Offset: 0x00170B14
		public bool isFontSame(TextNode n)
		{
			return this.d_font == n.d_font && this.d_fontSize == n.d_fontSize && this.d_fontStyle == n.d_fontStyle;
		}

		// Token: 0x060035FB RID: 13819 RVA: 0x00172948 File Offset: 0x00170B48
		public override void Reset(Owner o, Anchor hf)
		{
			base.Reset(o, hf);
			this.d_font = null;
			this.d_bUnderline = false;
			this.d_bStrickout = false;
		}

		// Token: 0x060035FC RID: 13820 RVA: 0x00172967 File Offset: 0x00170B67
		protected virtual bool isUnderLine()
		{
			return this.d_bUnderline;
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060035FD RID: 13821 RVA: 0x0017296F File Offset: 0x00170B6F
		public virtual Color currentColor
		{
			get
			{
				return this.d_color;
			}
		}

		// Token: 0x060035FE RID: 13822 RVA: 0x00172977 File Offset: 0x00170B77
		public override float getHeight()
		{
			return this.size.y;
		}

		// Token: 0x060035FF RID: 13823 RVA: 0x00172984 File Offset: 0x00170B84
		public override float getWidth()
		{
			return this.size.x;
		}

		// Token: 0x06003600 RID: 13824 RVA: 0x00172994 File Offset: 0x00170B94
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

		// Token: 0x06003601 RID: 13825 RVA: 0x0000280F File Offset: 0x00000A0F
		public virtual bool IsHyText()
		{
			return false;
		}

		// Token: 0x06003602 RID: 13826 RVA: 0x00172ACC File Offset: 0x00170CCC
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

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06003603 RID: 13827 RVA: 0x00172B48 File Offset: 0x00170D48
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

		// Token: 0x06003604 RID: 13828 RVA: 0x00172B80 File Offset: 0x00170D80
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

		// Token: 0x06003605 RID: 13829 RVA: 0x00172C18 File Offset: 0x00170E18
		public void GetLineCharacterInfo(out CharacterInfo info)
		{
			if (!this.d_font.GetCharacterInfo('_', ref info, 20, 1))
			{
				this.d_font.RequestCharactersInTexture("_", 20, 1);
				this.d_font.GetCharacterInfo('_', ref info, 20, 1);
			}
		}

		// Token: 0x06003606 RID: 13830 RVA: 0x00172C52 File Offset: 0x00170E52
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

		// Token: 0x04002F3F RID: 12095
		private Vector2 size = Vector2.zero;

		// Token: 0x04002F40 RID: 12096
		private List<NodeBase.Element> d_widthList = new List<NodeBase.Element>();

		// Token: 0x04002F41 RID: 12097
		public string d_text;

		// Token: 0x04002F42 RID: 12098
		public Font d_font;

		// Token: 0x04002F43 RID: 12099
		public int d_fontSize;

		// Token: 0x04002F44 RID: 12100
		public FontStyle d_fontStyle;

		// Token: 0x04002F45 RID: 12101
		public bool d_bUnderline;

		// Token: 0x04002F46 RID: 12102
		public bool d_bStrickout;

		// Token: 0x04002F47 RID: 12103
		public bool d_bDynUnderline;

		// Token: 0x04002F48 RID: 12104
		public bool d_bDynStrickout;

		// Token: 0x04002F49 RID: 12105
		public int d_dynSpeed;

		// Token: 0x04002F4A RID: 12106
		public EffectType effectType;

		// Token: 0x04002F4B RID: 12107
		public Color effectColor;

		// Token: 0x04002F4C RID: 12108
		public Vector2 effectDistance;

		// Token: 0x02001508 RID: 5384
		private struct Helper
		{
			// Token: 0x060082D2 RID: 33490 RVA: 0x002DC75C File Offset: 0x002DA95C
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

			// Token: 0x060082D3 RID: 33491 RVA: 0x002DC7E4 File Offset: 0x002DA9E4
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

			// Token: 0x060082D4 RID: 33492 RVA: 0x002DC9BC File Offset: 0x002DABBC
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

			// Token: 0x04006E19 RID: 28185
			public RenderCache cache;

			// Token: 0x04006E1A RID: 28186
			public float x;

			// Token: 0x04006E1B RID: 28187
			public uint yline;

			// Token: 0x04006E1C RID: 28188
			public List<Line> lines;

			// Token: 0x04006E1D RID: 28189
			public Anchor xFormatting;

			// Token: 0x04006E1E RID: 28190
			public float offsetX;

			// Token: 0x04006E1F RID: 28191
			public float offsetY;

			// Token: 0x04006E20 RID: 28192
			public StringBuilder sb;

			// Token: 0x04006E21 RID: 28193
			public float pixelsPerUnit;

			// Token: 0x04006E22 RID: 28194
			private Vector2 pt;

			// Token: 0x04006E23 RID: 28195
			private float alignedX;

			// Token: 0x04006E24 RID: 28196
			private TextNode node;

			// Token: 0x04006E25 RID: 28197
			private float maxWidth;

			// Token: 0x04006E26 RID: 28198
			private float fHeight;
		}
	}
}
