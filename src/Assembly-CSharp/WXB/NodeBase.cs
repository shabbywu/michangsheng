using System;
using System.Collections.Generic;
using UnityEngine;

namespace WXB
{
	// Token: 0x020009CD RID: 2509
	public abstract class NodeBase
	{
		// Token: 0x06003FD5 RID: 16341 RVA: 0x0002DD77 File Offset: 0x0002BF77
		public virtual void Reset(Owner o, Anchor hf)
		{
			this.d_bNewLine = false;
			this.owner = o;
			this.formatting = hf;
		}

		// Token: 0x06003FD6 RID: 16342 RVA: 0x001BAC00 File Offset: 0x001B8E00
		protected static float AlignedFormatting(Owner owner, Anchor formatting, float maxWidth, float curWidth)
		{
			if (formatting == Anchor.Null)
			{
				formatting = owner.anchor;
			}
			float result = 0f;
			switch (formatting)
			{
			case Anchor.UpperCenter:
			case Anchor.MiddleCenter:
			case Anchor.LowerCenter:
				result = (maxWidth - curWidth) / 2f;
				break;
			case Anchor.UpperRight:
			case Anchor.MiddleRight:
			case Anchor.LowerRight:
				result = maxWidth - curWidth;
				break;
			}
			return result;
		}

		// Token: 0x06003FD7 RID: 16343
		public abstract float getHeight();

		// Token: 0x06003FD8 RID: 16344
		public abstract float getWidth();

		// Token: 0x06003FD9 RID: 16345 RVA: 0x0002DD8E File Offset: 0x0002BF8E
		public void setNewLine(bool line)
		{
			this.d_bNewLine = line;
		}

		// Token: 0x06003FDA RID: 16346 RVA: 0x0002DD97 File Offset: 0x0002BF97
		public bool isNewLine()
		{
			return this.d_bNewLine;
		}

		// Token: 0x06003FDB RID: 16347
		public abstract void render(float maxWidth, RenderCache cache, ref float x, ref uint yline, List<Line> lines, float offsetX, float offsetY);

		// Token: 0x06003FDC RID: 16348 RVA: 0x000042DD File Offset: 0x000024DD
		protected virtual void AlterX(ref float x, float maxWidth)
		{
		}

		// Token: 0x06003FDD RID: 16349 RVA: 0x001BAC60 File Offset: 0x001B8E60
		public virtual void fill(ref Vector2 currentpos, List<Line> lines, float maxWidth, float pixelsPerUnit)
		{
			List<NodeBase.Element> list;
			this.UpdateWidthList(out list, pixelsPerUnit);
			float height = this.getHeight();
			this.AlterX(ref currentpos.x, maxWidth);
			if (list.Count == 0)
			{
				return;
			}
			Around around = this.owner.around;
			bool flag = false;
			int i = 0;
			while (i < list.Count)
			{
				float totalwidth = list[i].totalwidth;
				float x = 0f;
				if (currentpos.x + totalwidth > maxWidth)
				{
					currentpos = list[i].Next(this, currentpos, lines, maxWidth, height, around, totalwidth, ref flag);
					i++;
				}
				else if (around != null && !around.isContain(currentpos.x, currentpos.y, totalwidth, height, out x))
				{
					currentpos.x = x;
				}
				else
				{
					currentpos.x += totalwidth;
					flag = true;
					i++;
				}
			}
			Line line = lines.back<Line>();
			line.x = currentpos.x;
			line.y = Mathf.Max(height, line.y);
			if (this.d_bNewLine)
			{
				lines.Add(new Line(Vector2.zero));
				currentpos.y += height;
				currentpos.x = 0f;
			}
		}

		// Token: 0x06003FDE RID: 16350 RVA: 0x0002DD9F File Offset: 0x0002BF9F
		protected virtual void UpdateWidthList(out List<NodeBase.Element> widths, float pixelsPerUnit)
		{
			NodeBase.TempElementList.Clear();
			NodeBase.TempElementList.Add(new NodeBase.Element(this.getWidth()));
			widths = NodeBase.TempElementList;
		}

		// Token: 0x06003FDF RID: 16351 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onMouseEnter()
		{
		}

		// Token: 0x06003FE0 RID: 16352 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onMouseLeave()
		{
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06003FE1 RID: 16353 RVA: 0x0002DDC7 File Offset: 0x0002BFC7
		// (set) Token: 0x06003FE2 RID: 16354 RVA: 0x0002DDCF File Offset: 0x0002BFCF
		public object userdata { get; set; }

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06003FE3 RID: 16355 RVA: 0x001BAD9C File Offset: 0x001B8F9C
		public long keyPrefix
		{
			get
			{
				long num = 0L;
				if (this.d_bBlink)
				{
					num = -2147483648L;
				}
				if (this.d_bOffset)
				{
					num += 1073741824L;
					num += (long)((long)((byte)this.d_rectOffset.xMin) << 26);
					num += (long)((long)((byte)this.d_rectOffset.xMax) << 22);
					num += (long)((long)((byte)this.d_rectOffset.yMin) << 18);
					num += (long)((long)((byte)this.d_rectOffset.yMax) << 14);
				}
				return num;
			}
		}

		// Token: 0x06003FE4 RID: 16356 RVA: 0x001BAE1C File Offset: 0x001B901C
		public virtual void SetConfig(TextParser.Config c)
		{
			this.d_bBlink = c.isBlink;
			this.lineAlignment = c.lineAlignment;
			this.d_color = c.fontColor;
			this.d_bOffset = c.isOffset;
			if (c.isOffset)
			{
				this.d_rectOffset = c.offsetRect;
				return;
			}
			this.d_rectOffset.Set(0f, 0f, 0f, 0f);
		}

		// Token: 0x06003FE5 RID: 16357 RVA: 0x001BAE90 File Offset: 0x001B9090
		public virtual void Release()
		{
			this.d_color = Color.white;
			this.d_bNewLine = false;
			this.owner = null;
			this.formatting = Anchor.Null;
			this.d_bBlink = false;
			this.d_bOffset = false;
			this.d_rectOffset.Set(0f, 0f, 0f, 0f);
			this.userdata = null;
		}

		// Token: 0x0400390D RID: 14605
		public Owner owner;

		// Token: 0x0400390E RID: 14606
		public Anchor formatting = Anchor.Null;

		// Token: 0x0400390F RID: 14607
		protected static List<NodeBase.Element> TempElementList = new List<NodeBase.Element>();

		// Token: 0x04003910 RID: 14608
		protected bool d_bNewLine;

		// Token: 0x04003911 RID: 14609
		public bool d_bBlink;

		// Token: 0x04003912 RID: 14610
		public bool d_bOffset;

		// Token: 0x04003913 RID: 14611
		public Rect d_rectOffset;

		// Token: 0x04003914 RID: 14612
		public Color d_color;

		// Token: 0x04003915 RID: 14613
		public LineAlignment lineAlignment = LineAlignment.Default;

		// Token: 0x020009CE RID: 2510
		public struct Element
		{
			// Token: 0x06003FE8 RID: 16360 RVA: 0x001BAEF4 File Offset: 0x001B90F4
			public Element(List<float> ws)
			{
				this.widthList = ws;
				this.totalWidth = 0f;
				for (int i = 0; i < this.widthList.Count; i++)
				{
					this.totalWidth += ws[i];
				}
			}

			// Token: 0x06003FE9 RID: 16361 RVA: 0x0002DDFB File Offset: 0x0002BFFB
			public Element(float width)
			{
				this.totalWidth = width;
				this.widthList = null;
			}

			// Token: 0x17000723 RID: 1827
			// (get) Token: 0x06003FEA RID: 16362 RVA: 0x0002DE0B File Offset: 0x0002C00B
			public float totalwidth
			{
				get
				{
					return this.totalWidth;
				}
			}

			// Token: 0x17000724 RID: 1828
			// (get) Token: 0x06003FEB RID: 16363 RVA: 0x0002DE13 File Offset: 0x0002C013
			public List<float> widths
			{
				get
				{
					return this.widthList;
				}
			}

			// Token: 0x17000725 RID: 1829
			// (get) Token: 0x06003FEC RID: 16364 RVA: 0x0002DE1B File Offset: 0x0002C01B
			public int count
			{
				get
				{
					if (this.widthList != null)
					{
						return this.widthList.Count;
					}
					return 1;
				}
			}

			// Token: 0x06003FED RID: 16365 RVA: 0x001BAF40 File Offset: 0x001B9140
			public Vector2 Next(NodeBase n, Vector2 currentPos, List<Line> lines, float maxWidth, float height, Around round, float tw, ref bool currentLineContain)
			{
				if (currentPos.x != 0f)
				{
					Line line = lines.back<Line>();
					line.x = currentPos.x;
					if (currentLineContain)
					{
						line.y = Mathf.Max(line.y, height);
					}
					currentPos.x = 0f;
					currentPos.y += line.y;
					currentLineContain = false;
					lines.Add(new Line(new Vector2(0f, 0f)));
				}
				if (round != null)
				{
					float x = 0f;
					while (!round.isContain(currentPos.x, currentPos.y, tw, height, out x))
					{
						currentPos.x = x;
						if (currentPos.x + tw > maxWidth)
						{
							currentPos.x = 0f;
							lines.Add(new Line(new Vector2(0f, height)));
							currentPos.y += height;
						}
					}
				}
				if (this.widthList != null)
				{
					for (int i = 0; i < this.widthList.Count; i++)
					{
						currentPos = this.Add(n, currentPos, this.widthList[i], maxWidth, lines, height, ref currentLineContain);
					}
				}
				else
				{
					currentPos = this.Add(n, currentPos, this.totalWidth, maxWidth, lines, height, ref currentLineContain);
				}
				lines.back<Line>().x = currentPos.x;
				return currentPos;
			}

			// Token: 0x06003FEE RID: 16366 RVA: 0x001BB094 File Offset: 0x001B9294
			private Vector2 Add(NodeBase n, Vector2 currentPos, float width, float maxWidth, List<Line> lines, float height, ref bool currentLineContain)
			{
				if (currentPos.x + width > maxWidth)
				{
					Line line = lines.back<Line>();
					line.x = currentPos.x;
					if (currentLineContain)
					{
						line.y = Mathf.Max(line.y, height);
					}
					currentPos.x = width;
					lines.Add(new Line(new Vector2(currentPos.x, height)));
					currentPos.y += height;
				}
				else
				{
					currentPos.x += width;
				}
				currentLineContain = true;
				return currentPos;
			}

			// Token: 0x04003917 RID: 14615
			private List<float> widthList;

			// Token: 0x04003918 RID: 14616
			private float totalWidth;
		}
	}
}
