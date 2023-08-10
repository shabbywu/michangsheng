using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace WXB;

public class TextNode : NodeBase
{
	private struct Helper
	{
		public RenderCache cache;

		public float x;

		public uint yline;

		public List<Line> lines;

		public Anchor xFormatting;

		public float offsetX;

		public float offsetY;

		public StringBuilder sb;

		public float pixelsPerUnit;

		private Vector2 pt;

		private float alignedX;

		private TextNode node;

		private float maxWidth;

		private float fHeight;

		public Helper(float maxWidth, RenderCache cache, float x, uint yline, List<Line> lines, Anchor xFormatting, float offsetX, float offsetY, StringBuilder sb)
		{
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			this.maxWidth = maxWidth;
			this.cache = cache;
			this.x = x;
			this.yline = yline;
			this.lines = lines;
			this.xFormatting = xFormatting;
			this.offsetX = offsetX;
			this.offsetY = offsetY;
			pixelsPerUnit = 1f;
			alignedX = 0f;
			pt = Vector2.zero;
			node = null;
			fHeight = 0f;
			this.sb = sb;
		}

		private void DrawCurrent(bool isnewLine, Around around)
		{
			//IL_0080: Unknown result type (might be due to invalid IL or missing references)
			if (sb.Length != 0)
			{
				Rect rect = default(Rect);
				((Rect)(ref rect))._002Ector(pt.x + alignedX, pt.y, x - pt.x + offsetX, node.getHeight());
				cache.cacheText(lines[(int)yline], node, sb.ToString(), rect);
				sb.Remove(0, sb.Length);
			}
			if (isnewLine)
			{
				yline++;
				x = 0f;
				pt.x = offsetX;
				pt.y = offsetY;
				for (int i = 0; i < yline; i++)
				{
					pt.y += lines[i].y;
				}
				if (yline >= lines.Count)
				{
					yline--;
				}
				alignedX = NodeBase.AlignedFormatting(node.owner, xFormatting, maxWidth, lines[(int)yline].x);
				if (!around.isContain(pt.x + alignedX, pt.y, 1f, node.getHeight(), out var ox))
				{
					pt.x = ox - alignedX;
					x = pt.x;
				}
			}
		}

		public void Draw(TextNode n)
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			node = n;
			pt = new Vector2(x + offsetX, offsetY);
			for (int i = 0; i < yline; i++)
			{
				pt.y += lines[i].y;
			}
			if (maxWidth == 0f)
			{
				return;
			}
			alignedX = NodeBase.AlignedFormatting(n.owner, xFormatting, maxWidth, lines[(int)yline].x);
			fHeight = node.getHeight();
			sb.Remove(0, sb.Length);
			Around around = n.owner.around;
			int num = 0;
			float ox = 0f;
			for (int j = 0; j < node.d_widthList.Count; j++)
			{
				Element element = node.d_widthList[j];
				float totalwidth = element.totalwidth;
				if (x + totalwidth > maxWidth)
				{
					if (x != 0f)
					{
						DrawCurrent(isnewLine: true, around);
					}
					if (element.widths == null)
					{
						if (x + element.totalwidth > maxWidth)
						{
							DrawCurrent(isnewLine: true, around);
							continue;
						}
						x += element.totalwidth;
						sb.Append(node.d_text[num++]);
						continue;
					}
					int num2 = 0;
					while (num2 < element.widths.Count)
					{
						if (x != 0f && x + element.widths[num2] > maxWidth)
						{
							DrawCurrent(isnewLine: true, around);
							continue;
						}
						x += element.widths[num2];
						sb.Append(node.d_text[num++]);
						num2++;
					}
				}
				else if (!around.isContain(x, pt.y, totalwidth, fHeight, out ox))
				{
					DrawCurrent(isnewLine: false, around);
					x = ox;
					pt.x = ox;
					j--;
				}
				else
				{
					int count = element.count;
					sb.Append(node.d_text.Substring(num, count));
					num += count;
					x += totalwidth;
				}
			}
			if (sb.Length != 0)
			{
				DrawCurrent(isnewLine: false, around);
			}
			if (node.d_bNewLine)
			{
				yline++;
				x = 0f;
			}
		}
	}

	private Vector2 size = Vector2.zero;

	private List<Element> d_widthList = new List<Element>();

	public string d_text;

	public Font d_font;

	public int d_fontSize;

	public FontStyle d_fontStyle;

	public bool d_bUnderline;

	public bool d_bStrickout;

	public bool d_bDynUnderline;

	public bool d_bDynStrickout;

	public int d_dynSpeed;

	public EffectType effectType;

	public Color effectColor;

	public Vector2 effectDistance;

	public virtual Color currentColor => d_color;

	public new long keyPrefix
	{
		get
		{
			long num = base.keyPrefix;
			if (d_bDynStrickout)
			{
				num += 8192;
			}
			if (d_bDynUnderline)
			{
				num += 8192;
			}
			return num;
		}
	}

	public bool isFontSame(TextNode n)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)d_font == (Object)(object)n.d_font && d_fontSize == n.d_fontSize && d_fontStyle == n.d_fontStyle)
		{
			return true;
		}
		return false;
	}

	public override void Reset(Owner o, Anchor hf)
	{
		base.Reset(o, hf);
		d_font = null;
		d_bUnderline = false;
		d_bStrickout = false;
	}

	protected virtual bool isUnderLine()
	{
		return d_bUnderline;
	}

	public override float getHeight()
	{
		return size.y;
	}

	public override float getWidth()
	{
		return size.x;
	}

	protected override void UpdateWidthList(out List<Element> widths, float pixelsPerUnit)
	{
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		widths = d_widthList;
		d_widthList.Clear();
		if (d_text.Length == 0)
		{
			return;
		}
		float unitsPerPixel = 1f / pixelsPerUnit;
		int fontsize = (int)((float)d_fontSize * pixelsPerUnit);
		size.x = 0f;
		size.y = (float)FontCache.GetLineHeight(d_font, fontsize, d_fontStyle) * unitsPerPixel;
		Func<char, float> func = (char code) => (float)FontCache.GetAdvance(d_font, fontsize, d_fontStyle, code) * unitsPerPixel;
		ElementSegment elementSegment = owner.elementSegment;
		if (elementSegment == null)
		{
			for (int i = 0; i < d_text.Length; i++)
			{
				Element item = new Element(func(d_text[i]));
				widths.Add(item);
			}
		}
		else
		{
			elementSegment.Segment(d_text, widths, func);
		}
		for (int j = 0; j < d_widthList.Count; j++)
		{
			size.x += d_widthList[j].totalwidth;
		}
	}

	public virtual bool IsHyText()
	{
		return false;
	}

	public override void render(float maxWidth, RenderCache cache, ref float x, ref uint yline, List<Line> lines, float offsetX, float offsetY)
	{
		if ((Object)(object)d_font == (Object)null)
		{
			return;
		}
		PD<StringBuilder> sB = Pool.GetSB();
		try
		{
			Helper helper = new Helper(maxWidth, cache, x, yline, lines, formatting, offsetX, offsetY, sB.value);
			helper.Draw(this);
			x = helper.x;
			yline = helper.yline;
		}
		finally
		{
			((IDisposable)sB).Dispose();
		}
	}

	public override void SetConfig(TextParser.Config c)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		base.SetConfig(c);
		d_font = c.font;
		d_bUnderline = c.isUnderline;
		d_fontSize = c.fontSize;
		d_fontStyle = c.fontStyle;
		d_bStrickout = c.isStrickout;
		d_bDynUnderline = c.isDyncUnderline;
		d_bDynStrickout = c.isDyncStrickout;
		d_dynSpeed = c.dyncSpeed;
		effectType = c.effectType;
		effectColor = c.effectColor;
		effectDistance = c.effectDistance;
	}

	public void GetLineCharacterInfo(out CharacterInfo info)
	{
		if (!d_font.GetCharacterInfo('_', ref info, 20, (FontStyle)1))
		{
			d_font.RequestCharactersInTexture("_", 20, (FontStyle)1);
			d_font.GetCharacterInfo('_', ref info, 20, (FontStyle)1);
		}
	}

	public override void Release()
	{
		base.Release();
		d_text = null;
		d_font = null;
		d_fontSize = 0;
		d_bUnderline = false;
		d_bDynUnderline = false;
		d_bDynStrickout = false;
		d_dynSpeed = 0;
	}
}
