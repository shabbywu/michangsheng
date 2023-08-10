using System.Collections.Generic;
using UnityEngine;

namespace WXB;

public abstract class NodeBase
{
	public struct Element
	{
		private List<float> widthList;

		private float totalWidth;

		public float totalwidth => totalWidth;

		public List<float> widths => widthList;

		public int count
		{
			get
			{
				if (widthList != null)
				{
					return widthList.Count;
				}
				return 1;
			}
		}

		public Element(List<float> ws)
		{
			widthList = ws;
			totalWidth = 0f;
			for (int i = 0; i < widthList.Count; i++)
			{
				totalWidth += ws[i];
			}
		}

		public Element(float width)
		{
			totalWidth = width;
			widthList = null;
		}

		public Vector2 Next(NodeBase n, Vector2 currentPos, List<Line> lines, float maxWidth, float height, Around round, float tw, ref bool currentLineContain)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_011e: Unknown result type (might be due to invalid IL or missing references)
			//IL_012c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0131: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0139: Unknown result type (might be due to invalid IL or missing references)
			//IL_0144: Unknown result type (might be due to invalid IL or missing references)
			//IL_0087: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_0101: Unknown result type (might be due to invalid IL or missing references)
			//IL_0106: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
			if (currentPos.x != 0f)
			{
				Line line = lines.back();
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
				float ox = 0f;
				while (!round.isContain(currentPos.x, currentPos.y, tw, height, out ox))
				{
					currentPos.x = ox;
					if (currentPos.x + tw > maxWidth)
					{
						currentPos.x = 0f;
						lines.Add(new Line(new Vector2(0f, height)));
						currentPos.y += height;
					}
				}
			}
			if (widthList != null)
			{
				for (int i = 0; i < widthList.Count; i++)
				{
					currentPos = Add(n, currentPos, widthList[i], maxWidth, lines, height, ref currentLineContain);
				}
			}
			else
			{
				currentPos = Add(n, currentPos, totalWidth, maxWidth, lines, height, ref currentLineContain);
			}
			lines.back().x = currentPos.x;
			return currentPos;
		}

		private Vector2 Add(NodeBase n, Vector2 currentPos, float width, float maxWidth, List<Line> lines, float height, ref bool currentLineContain)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0078: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			if (currentPos.x + width > maxWidth)
			{
				Line line = lines.back();
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
	}

	public Owner owner;

	public Anchor formatting = Anchor.Null;

	protected static List<Element> TempElementList = new List<Element>();

	protected bool d_bNewLine;

	public bool d_bBlink;

	public bool d_bOffset;

	public Rect d_rectOffset;

	public Color d_color;

	public LineAlignment lineAlignment = LineAlignment.Default;

	public object userdata { get; set; }

	public long keyPrefix
	{
		get
		{
			long num = 0L;
			if (d_bBlink)
			{
				num = -2147483648L;
			}
			if (d_bOffset)
			{
				num += 1073741824;
				num += (byte)((Rect)(ref d_rectOffset)).xMin << 26;
				num += (byte)((Rect)(ref d_rectOffset)).xMax << 22;
				num += (byte)((Rect)(ref d_rectOffset)).yMin << 18;
				num += (byte)((Rect)(ref d_rectOffset)).yMax << 14;
			}
			return num;
		}
	}

	public virtual void Reset(Owner o, Anchor hf)
	{
		d_bNewLine = false;
		owner = o;
		formatting = hf;
	}

	protected static float AlignedFormatting(Owner owner, Anchor formatting, float maxWidth, float curWidth)
	{
		if (formatting == Anchor.Null)
		{
			formatting = owner.anchor;
		}
		float result = 0f;
		switch (formatting)
		{
		case Anchor.UpperRight:
		case Anchor.MiddleRight:
		case Anchor.LowerRight:
			result = maxWidth - curWidth;
			break;
		case Anchor.UpperCenter:
		case Anchor.MiddleCenter:
		case Anchor.LowerCenter:
			result = (maxWidth - curWidth) / 2f;
			break;
		}
		return result;
	}

	public abstract float getHeight();

	public abstract float getWidth();

	public void setNewLine(bool line)
	{
		d_bNewLine = line;
	}

	public bool isNewLine()
	{
		return d_bNewLine;
	}

	public abstract void render(float maxWidth, RenderCache cache, ref float x, ref uint yline, List<Line> lines, float offsetX, float offsetY);

	protected virtual void AlterX(ref float x, float maxWidth)
	{
	}

	public virtual void fill(ref Vector2 currentpos, List<Line> lines, float maxWidth, float pixelsPerUnit)
	{
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		UpdateWidthList(out var widths, pixelsPerUnit);
		float height = getHeight();
		AlterX(ref currentpos.x, maxWidth);
		if (widths.Count == 0)
		{
			return;
		}
		Around around = owner.around;
		bool currentLineContain = false;
		int num = 0;
		while (num < widths.Count)
		{
			float totalwidth = widths[num].totalwidth;
			float ox = 0f;
			if (currentpos.x + totalwidth > maxWidth)
			{
				currentpos = widths[num].Next(this, currentpos, lines, maxWidth, height, around, totalwidth, ref currentLineContain);
				num++;
			}
			else if (around != null && !around.isContain(currentpos.x, currentpos.y, totalwidth, height, out ox))
			{
				currentpos.x = ox;
			}
			else
			{
				currentpos.x += totalwidth;
				currentLineContain = true;
				num++;
			}
		}
		Line line = lines.back();
		line.x = currentpos.x;
		line.y = Mathf.Max(height, line.y);
		if (d_bNewLine)
		{
			lines.Add(new Line(Vector2.zero));
			currentpos.y += height;
			currentpos.x = 0f;
		}
	}

	protected virtual void UpdateWidthList(out List<Element> widths, float pixelsPerUnit)
	{
		TempElementList.Clear();
		TempElementList.Add(new Element(getWidth()));
		widths = TempElementList;
	}

	public virtual void onMouseEnter()
	{
	}

	public virtual void onMouseLeave()
	{
	}

	public virtual void SetConfig(TextParser.Config c)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		d_bBlink = c.isBlink;
		lineAlignment = c.lineAlignment;
		d_color = c.fontColor;
		d_bOffset = c.isOffset;
		if (c.isOffset)
		{
			d_rectOffset = c.offsetRect;
		}
		else
		{
			((Rect)(ref d_rectOffset)).Set(0f, 0f, 0f, 0f);
		}
	}

	public virtual void Release()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		d_color = Color.white;
		d_bNewLine = false;
		owner = null;
		formatting = Anchor.Null;
		d_bBlink = false;
		d_bOffset = false;
		((Rect)(ref d_rectOffset)).Set(0f, 0f, 0f, 0f);
		userdata = null;
	}
}
