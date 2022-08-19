using System;
using System.Collections.Generic;
using UnityEngine;

namespace WXB
{
	// Token: 0x020006AC RID: 1708
	public abstract class NodeBase
	{
		// Token: 0x060035DF RID: 13791 RVA: 0x0017252A File Offset: 0x0017072A
		public virtual void Reset(Owner o, Anchor hf)
		{
			this.d_bNewLine = false;
			this.owner = o;
			this.formatting = hf;
		}

		// Token: 0x060035E0 RID: 13792 RVA: 0x00172544 File Offset: 0x00170744
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

		// Token: 0x060035E1 RID: 13793
		public abstract float getHeight();

		// Token: 0x060035E2 RID: 13794
		public abstract float getWidth();

		// Token: 0x060035E3 RID: 13795 RVA: 0x001725A1 File Offset: 0x001707A1
		public void setNewLine(bool line)
		{
			this.d_bNewLine = line;
		}

		// Token: 0x060035E4 RID: 13796 RVA: 0x001725AA File Offset: 0x001707AA
		public bool isNewLine()
		{
			return this.d_bNewLine;
		}

		// Token: 0x060035E5 RID: 13797
		public abstract void render(float maxWidth, RenderCache cache, ref float x, ref uint yline, List<Line> lines, float offsetX, float offsetY);

		// Token: 0x060035E6 RID: 13798 RVA: 0x00004095 File Offset: 0x00002295
		protected virtual void AlterX(ref float x, float maxWidth)
		{
		}

		// Token: 0x060035E7 RID: 13799 RVA: 0x001725B4 File Offset: 0x001707B4
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

		// Token: 0x060035E8 RID: 13800 RVA: 0x001726F0 File Offset: 0x001708F0
		protected virtual void UpdateWidthList(out List<NodeBase.Element> widths, float pixelsPerUnit)
		{
			NodeBase.TempElementList.Clear();
			NodeBase.TempElementList.Add(new NodeBase.Element(this.getWidth()));
			widths = NodeBase.TempElementList;
		}

		// Token: 0x060035E9 RID: 13801 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onMouseEnter()
		{
		}

		// Token: 0x060035EA RID: 13802 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onMouseLeave()
		{
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060035EB RID: 13803 RVA: 0x00172718 File Offset: 0x00170918
		// (set) Token: 0x060035EC RID: 13804 RVA: 0x00172720 File Offset: 0x00170920
		public object userdata { get; set; }

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060035ED RID: 13805 RVA: 0x0017272C File Offset: 0x0017092C
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

		// Token: 0x060035EE RID: 13806 RVA: 0x001727AC File Offset: 0x001709AC
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

		// Token: 0x060035EF RID: 13807 RVA: 0x00172820 File Offset: 0x00170A20
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

		// Token: 0x04002F32 RID: 12082
		public Owner owner;

		// Token: 0x04002F33 RID: 12083
		public Anchor formatting = Anchor.Null;

		// Token: 0x04002F34 RID: 12084
		protected static List<NodeBase.Element> TempElementList = new List<NodeBase.Element>();

		// Token: 0x04002F35 RID: 12085
		protected bool d_bNewLine;

		// Token: 0x04002F36 RID: 12086
		public bool d_bBlink;

		// Token: 0x04002F37 RID: 12087
		public bool d_bOffset;

		// Token: 0x04002F38 RID: 12088
		public Rect d_rectOffset;

		// Token: 0x04002F39 RID: 12089
		public Color d_color;

		// Token: 0x04002F3A RID: 12090
		public LineAlignment lineAlignment = LineAlignment.Default;

		// Token: 0x02001507 RID: 5383
		public struct Element
		{
			// Token: 0x060082CB RID: 33483 RVA: 0x002DC500 File Offset: 0x002DA700
			public Element(List<float> ws)
			{
				this.widthList = ws;
				this.totalWidth = 0f;
				for (int i = 0; i < this.widthList.Count; i++)
				{
					this.totalWidth += ws[i];
				}
			}

			// Token: 0x060082CC RID: 33484 RVA: 0x002DC549 File Offset: 0x002DA749
			public Element(float width)
			{
				this.totalWidth = width;
				this.widthList = null;
			}

			// Token: 0x17000B2F RID: 2863
			// (get) Token: 0x060082CD RID: 33485 RVA: 0x002DC559 File Offset: 0x002DA759
			public float totalwidth
			{
				get
				{
					return this.totalWidth;
				}
			}

			// Token: 0x17000B30 RID: 2864
			// (get) Token: 0x060082CE RID: 33486 RVA: 0x002DC561 File Offset: 0x002DA761
			public List<float> widths
			{
				get
				{
					return this.widthList;
				}
			}

			// Token: 0x17000B31 RID: 2865
			// (get) Token: 0x060082CF RID: 33487 RVA: 0x002DC569 File Offset: 0x002DA769
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

			// Token: 0x060082D0 RID: 33488 RVA: 0x002DC580 File Offset: 0x002DA780
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

			// Token: 0x060082D1 RID: 33489 RVA: 0x002DC6D4 File Offset: 0x002DA8D4
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

			// Token: 0x04006E17 RID: 28183
			private List<float> widthList;

			// Token: 0x04006E18 RID: 28184
			private float totalWidth;
		}
	}
}
