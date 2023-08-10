using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace WXB;

public class TextParser
{
	public struct Config
	{
		public Anchor anchor;

		public Font font;

		public FontStyle fontStyle;

		public int fontSize;

		public Color fontColor;

		public bool isUnderline;

		public bool isStrickout;

		public bool isBlink;

		public bool isDyncUnderline;

		public bool isDyncStrickout;

		public int dyncSpeed;

		public bool isOffset;

		public Rect offsetRect;

		public EffectType effectType;

		public Color effectColor;

		public Vector2 effectDistance;

		public LineAlignment lineAlignment;

		public void Clear()
		{
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0080: Unknown result type (might be due to invalid IL or missing references)
			//IL_0085: Unknown result type (might be due to invalid IL or missing references)
			//IL_008b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0090: Unknown result type (might be due to invalid IL or missing references)
			anchor = Anchor.Null;
			font = null;
			fontStyle = (FontStyle)0;
			fontSize = 0;
			fontColor = Color.white;
			isUnderline = false;
			isStrickout = false;
			isBlink = false;
			isDyncUnderline = false;
			isDyncStrickout = false;
			dyncSpeed = 0;
			isOffset = false;
			((Rect)(ref offsetRect)).Set(0f, 0f, 0f, 0f);
			effectType = EffectType.Null;
			effectColor = Color.black;
			effectDistance = Vector2.zero;
			lineAlignment = LineAlignment.Default;
		}

		public void Set(Config c)
		{
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_007a: Unknown result type (might be due to invalid IL or missing references)
			//IL_007f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0092: Unknown result type (might be due to invalid IL or missing references)
			//IL_0097: Unknown result type (might be due to invalid IL or missing references)
			//IL_009e: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
			anchor = c.anchor;
			font = c.font;
			fontStyle = c.fontStyle;
			fontSize = c.fontSize;
			fontColor = c.fontColor;
			isUnderline = c.isUnderline;
			isStrickout = c.isStrickout;
			isBlink = c.isBlink;
			dyncSpeed = c.dyncSpeed;
			isOffset = c.isOffset;
			offsetRect = c.offsetRect;
			effectType = c.effectType;
			effectColor = c.effectColor;
			effectDistance = c.effectDistance;
			isDyncUnderline = c.isDyncUnderline;
			isDyncStrickout = c.isDyncStrickout;
			lineAlignment = c.lineAlignment;
		}

		public bool isSame(Config c)
		{
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0105: Unknown result type (might be due to invalid IL or missing references)
			//IL_010b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0136: Unknown result type (might be due to invalid IL or missing references)
			//IL_013c: Unknown result type (might be due to invalid IL or missing references)
			if (anchor == c.anchor && (Object)(object)font == (Object)(object)c.font && fontStyle == c.fontStyle && isUnderline == c.isUnderline && fontColor == c.fontColor && isStrickout == c.isStrickout && isBlink == c.isBlink && fontSize == c.fontSize && lineAlignment == c.lineAlignment && isDyncUnderline == c.isDyncUnderline && isDyncStrickout == c.isDyncStrickout && dyncSpeed == c.dyncSpeed && ((effectType == EffectType.Null && c.effectType == EffectType.Null) || (effectType == c.effectType && effectColor == c.effectColor && effectDistance == c.effectDistance)))
			{
				if (isOffset || c.isOffset)
				{
					if (isOffset == c.isOffset)
					{
						return offsetRect == c.offsetRect;
					}
					return false;
				}
				return true;
			}
			return false;
		}
	}

	private delegate void OnFun(string c);

	private class HyConfig
	{
		private delegate void OnFunHy(string text);

		public string text = "";

		public HyperlinkNode node;

		public int startPos;

		public int lenght;

		private StringBuilder sb;

		public TextParser parser;

		private OnFunHy[] OnFunHys;

		public HyConfig(TextParser p)
		{
			parser = p;
			OnFunHys = new OnFunHy[128];
			OnFunHys[82] = ParserSureColor;
			OnFunHys[71] = ParserSureColor;
			OnFunHys[66] = ParserSureColor;
			OnFunHys[75] = ParserSureColor;
			OnFunHys[89] = ParserSureColor;
			OnFunHys[87] = ParserSureColor;
			OnFunHys[80] = ParserSureColor;
			OnFunHys[99] = ParserFontColor;
			OnFunHys[110] = ParserRestore;
			OnFunHys[115] = ParserFontSize;
			OnFunHys[102] = ParserFont;
			OnFunHys[35] = ParserOutputChar;
			OnFunHys[117] = delegate
			{
				node.d_bUnderline = !node.d_bUnderline;
				startPos++;
			};
			OnFunHys[101] = delegate
			{
				node.d_bStrickout = !node.d_bStrickout;
				startPos++;
			};
		}

		private void ParserOutputChar(string text)
		{
			sb.Append("#");
			startPos++;
		}

		private void ParserSureColor(string text)
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			node.d_color = GetColour(text[startPos]);
			startPos++;
		}

		private void ParserFontColor(string text)
		{
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			node.d_color = Tools.ParserColorName(text, ref startPos, node.d_color);
		}

		private void ParserRestore(string text)
		{
			node.SetConfig(parser.startConfig);
		}

		private void ParserFontSize(string text)
		{
			float value = 1f;
			if (ParserFloat(ref startPos, text, ref value))
			{
				node.d_fontSize = (int)value;
			}
		}

		private void ParserFont(string text)
		{
			startPos++;
			Font val = Tools.ParserFontName(text, ref startPos);
			if ((Object)(object)val != (Object)null)
			{
				node.d_font = val;
			}
			else
			{
				startPos--;
			}
		}

		public void Clear()
		{
			text = null;
			node = null;
			sb = null;
			startPos = 0;
		}

		public void BeginParser(StringBuilder s)
		{
			sb = s;
			bool flag = false;
			while (lenght > startPos)
			{
				if (!flag)
				{
					if (text[startPos] == '#')
					{
						flag = true;
						startPos++;
					}
					else
					{
						sb.Append(text[startPos]);
						startPos++;
					}
					continue;
				}
				char c = text[startPos];
				OnFunHy onFunHy = null;
				if (c < '\u0080' && (onFunHy = OnFunHys[(uint)c]) != null)
				{
					onFunHy(text);
				}
				else
				{
					sb.Append(text[startPos]);
					startPos++;
				}
				flag = false;
			}
			node.d_text = sb.ToString();
		}
	}

	private Owner mOwner;

	protected int d_curPos;

	protected Config startConfig;

	protected Config currentConfig;

	protected List<NodeBase> d_nodeList;

	protected StringBuilder d_text = new StringBuilder();

	protected bool d_bBegin;

	private OnFun[] OnFuns;

	private static HyConfig hyConfig = null;

	private Dictionary<string, Action<string, string>> TagFuns;

	private static TagAttributes s_TagAttributes = new TagAttributes();

	public T CreateNode<T>() where T : NodeBase, new()
	{
		T val = new T();
		val.Reset(mOwner, currentConfig.anchor);
		return val;
	}

	private static bool Get(char c, out Anchor a)
	{
		switch (c)
		{
		case '1':
			a = Anchor.MiddleLeft;
			return true;
		case '2':
			a = Anchor.MiddleCenter;
			return true;
		case '3':
			a = Anchor.MiddleRight;
			return true;
		default:
			a = Anchor.MiddleCenter;
			return false;
		}
	}

	private static bool Get(char c, out LineAlignment a)
	{
		switch (c)
		{
		case '1':
			a = LineAlignment.Top;
			return true;
		case '2':
			a = LineAlignment.Center;
			return true;
		case '3':
			a = LineAlignment.Bottom;
			return true;
		default:
			a = LineAlignment.Default;
			return false;
		}
	}

	public TextParser()
	{
		clear();
		Reg();
		RegTag();
	}

	private static bool ParserInt(ref int d_curPos, string text, ref int value, int num = 3)
	{
		PD<StringBuilder> sB = Pool.GetSB();
		try
		{
			StringBuilder value2 = sB.value;
			d_curPos++;
			while (text.Length > d_curPos && text[d_curPos] >= '0' && text[d_curPos] <= '9')
			{
				value2.Append(text[d_curPos]);
				d_curPos++;
				if (value2.Length >= num)
				{
					break;
				}
			}
			value = Tools.stringToInt(value2.ToString(), -1);
			if (value2.Length == 0)
			{
				d_curPos--;
				return false;
			}
			return true;
		}
		finally
		{
			((IDisposable)sB).Dispose();
		}
	}

	private static bool ParserFloat(ref int d_curPos, string text, ref float value, int num = 3)
	{
		PD<StringBuilder> sB = Pool.GetSB();
		try
		{
			StringBuilder value2 = sB.value;
			d_curPos++;
			bool flag = false;
			while (text.Length > d_curPos && ((text[d_curPos] >= '0' && text[d_curPos] <= '9') || text[d_curPos] == '.'))
			{
				if (text[d_curPos] == '.')
				{
					flag = true;
				}
				value2.Append(text[d_curPos]);
				d_curPos++;
				int num2 = (flag ? (num + 1) : num);
				if (value2.Length >= num2)
				{
					break;
				}
			}
			value = Tools.stringToFloat(value2.ToString(), 0f);
			if (value2.Length == 0)
			{
				d_curPos--;
				return false;
			}
			return true;
		}
		finally
		{
			((IDisposable)sB).Dispose();
		}
	}

	public void parser(Owner owner, string text, Config config, List<NodeBase> vList)
	{
		clear();
		mOwner = owner;
		d_nodeList = vList;
		startConfig.Set(config);
		currentConfig.Set(config);
		if ((Object)(object)currentConfig.font == (Object)null)
		{
			Debug.LogError((object)"TextParser pFont == null");
		}
		else
		{
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			int length = text.Length;
			while (length > d_curPos)
			{
				if (!d_bBegin)
				{
					switch (text[d_curPos])
					{
					case '#':
						d_bBegin = true;
						d_curPos++;
						break;
					case '<':
					{
						int num = text.IndexOf('>', d_curPos);
						if (num != -1)
						{
							string text2 = null;
							string param = null;
							int num2 = text.IndexOfAny(new char[2] { ' ', '=' }, d_curPos);
							if (num2 != -1 && num2 < num)
							{
								text2 = text.Substring(d_curPos + 1, num2 - d_curPos);
								param = text.Substring(num2 + 1, num - num2 - 1);
							}
							else
							{
								text2 = text.Substring(d_curPos + 1, num - d_curPos - 1);
							}
							if (d_text.Length != 0)
							{
								save(isNewLine: false);
							}
							TagParam(text2, param);
							d_curPos = num + 1;
						}
						else
						{
							d_text.Append(text[d_curPos]);
							d_curPos++;
						}
						break;
					}
					case '\n':
						save(isNewLine: true);
						d_curPos++;
						break;
					default:
						d_text.Append(text[d_curPos]);
						d_curPos++;
						break;
					}
				}
				else
				{
					char c = text[d_curPos];
					OnFun onFun = null;
					if (c < '\u0080' && (onFun = OnFuns[(uint)c]) != null)
					{
						onFun(text);
					}
					else
					{
						d_text.Append(text[d_curPos]);
						d_curPos++;
					}
					d_bBegin = false;
				}
			}
			if (d_text.Length != 0)
			{
				save(isNewLine: false);
			}
			clear();
		}
	}

	protected void save(bool isNewLine)
	{
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		if (d_text.Length == 0)
		{
			if (!isNewLine)
			{
				return;
			}
			if (d_nodeList.Count != 0)
			{
				NodeBase nodeBase = d_nodeList.back();
				if (!nodeBase.isNewLine())
				{
					nodeBase.setNewLine(line: true);
					return;
				}
			}
			LineNode lineNode = CreateNode<LineNode>();
			lineNode.SetConfig(currentConfig);
			lineNode.font = currentConfig.font;
			lineNode.fontSize = currentConfig.fontSize;
			lineNode.fs = currentConfig.fontStyle;
			lineNode.setNewLine(line: true);
			d_nodeList.Add(lineNode);
		}
		else
		{
			TextNode textNode = CreateNode<TextNode>();
			textNode.d_text = d_text.ToString();
			textNode.SetConfig(currentConfig);
			textNode.setNewLine(isNewLine);
			d_nodeList.Add(textNode);
			d_text.Remove(0, d_text.Length);
		}
	}

	protected void saveX(float value)
	{
		XSpaceNode xSpaceNode = CreateNode<XSpaceNode>();
		xSpaceNode.d_offset = value;
		d_nodeList.Add(xSpaceNode);
	}

	protected void saveY(float value)
	{
		if (d_nodeList.Count != 0 && !d_nodeList.back().isNewLine())
		{
			d_nodeList.back().setNewLine(line: true);
		}
		YSpaceNode ySpaceNode = CreateNode<YSpaceNode>();
		ySpaceNode.d_offset = value;
		ySpaceNode.setNewLine(line: true);
		d_nodeList.Add(ySpaceNode);
	}

	protected void saveZ(float value)
	{
		YSpaceNode ySpaceNode = CreateNode<YSpaceNode>();
		ySpaceNode.d_offset = value;
		ySpaceNode.setNewLine(line: false);
		d_nodeList.Add(ySpaceNode);
	}

	protected void saveHy()
	{
		if (d_text.Length == 0)
		{
			return;
		}
		string text = d_text.ToString();
		d_text.Remove(0, d_text.Length);
		HyperlinkNode hyperlinkNode = CreateNode<HyperlinkNode>();
		string empty = string.Empty;
		if (text[text.Length - 1] == '}')
		{
			int num = text.IndexOf('{', 0);
			if (num != -1)
			{
				empty = text.Substring(num, text.Length - num);
				hyperlinkNode.d_link = empty.Replace("{", "").Replace("}", "");
				text = text.Remove(num, text.Length - num);
			}
		}
		hyperlinkNode.d_text = "";
		hyperlinkNode.SetConfig(currentConfig);
		ParseHyText(text, hyperlinkNode);
		d_nodeList.Add(hyperlinkNode);
	}

	protected void clear()
	{
		startConfig.Clear();
		currentConfig.Clear();
		d_nodeList = null;
		d_curPos = 0;
		d_text.Remove(0, d_text.Length);
		d_bBegin = false;
		mOwner = null;
	}

	private static Color GetColour(uint code)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		return (Color)(code switch
		{
			82u => Color.red, 
			71u => Color.green, 
			66u => Color.blue, 
			75u => Color.black, 
			89u => Color.yellow, 
			87u => Color.white, 
			_ => Color.white, 
		});
	}

	private void Reg()
	{
		OnFuns = new OnFun[128];
		OnFuns[82] = ParserSureColor;
		OnFuns[71] = ParserSureColor;
		OnFuns[66] = ParserSureColor;
		OnFuns[75] = ParserSureColor;
		OnFuns[89] = ParserSureColor;
		OnFuns[87] = ParserSureColor;
		OnFuns[35] = ParserOutputChar;
		OnFuns[91] = ParserFontColorS;
		OnFuns[98] = ParserBlink;
		OnFuns[99] = ParserFontColor;
		OnFuns[100] = ParserFontStyle;
		OnFuns[101] = ParserStrickout;
		OnFuns[109] = ParserDynStrickout;
		OnFuns[102] = ParserFont;
		OnFuns[110] = ParserRestoreColor;
		OnFuns[103] = ParserRestore;
		OnFuns[114] = ParserNewLine;
		OnFuns[117] = ParserUnderLine;
		OnFuns[116] = ParserDynUnderline;
		OnFuns[108] = ParserDynSpeed;
		OnFuns[104] = ParserHyperlink;
		OnFuns[115] = ParserFontSize;
		OnFuns[120] = ParserXYZ;
		OnFuns[121] = ParserXYZ;
		OnFuns[122] = ParserXYZ;
		OnFuns[119] = ParserFormatting;
		OnFuns[97] = ParserLineAlignment;
		OnFuns[48] = ParserCartoon;
		OnFuns[49] = ParserCartoon;
		OnFuns[50] = ParserCartoon;
		OnFuns[51] = ParserCartoon;
		OnFuns[52] = ParserCartoon;
		OnFuns[53] = ParserCartoon;
		OnFuns[54] = ParserCartoon;
		OnFuns[55] = ParserCartoon;
		OnFuns[56] = ParserCartoon;
		OnFuns[57] = ParserCartoon;
	}

	private void ParserCartoon(string text)
	{
		Cartoon cartoon = null;
		int num = d_curPos - 1;
		for (int num2 = 3; num2 >= 1; num2--)
		{
			int value = -1;
			if (ParserInt(ref num, text, ref value, num2))
			{
				cartoon = Tools.GetCartoon(value.ToString());
				if (cartoon != null)
				{
					break;
				}
			}
			num = d_curPos - 1;
		}
		if (cartoon != null)
		{
			d_curPos = num;
			save(isNewLine: false);
			CartoonNode cartoonNode = CreateNode<CartoonNode>();
			cartoonNode.cartoon = cartoon;
			cartoonNode.width = cartoon.width;
			cartoonNode.height = cartoon.height;
			cartoonNode.SetConfig(currentConfig);
			d_nodeList.Add(cartoonNode);
		}
	}

	private void ParserDynSpeed(string text)
	{
		int value = -1;
		if (ParserInt(ref d_curPos, text, ref value, 5) && value > 0 && value != currentConfig.dyncSpeed)
		{
			save(isNewLine: false);
			currentConfig.dyncSpeed = value;
		}
	}

	private void ParserDynUnderline(string text)
	{
		save(isNewLine: false);
		currentConfig.isDyncUnderline = !currentConfig.isDyncUnderline;
		d_curPos++;
	}

	private void ParserDynStrickout(string text)
	{
		save(isNewLine: false);
		currentConfig.isDyncStrickout = !currentConfig.isDyncStrickout;
		d_curPos++;
	}

	private void ParserRestoreColor(string text)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		if (currentConfig.fontColor != startConfig.fontColor)
		{
			save(isNewLine: false);
			d_curPos++;
			currentConfig.Set(startConfig);
		}
		else
		{
			d_curPos++;
		}
	}

	private void ParserRestore(string text)
	{
		if (!currentConfig.isSame(startConfig))
		{
			save(isNewLine: false);
			d_curPos++;
			currentConfig.Set(startConfig);
		}
		else
		{
			d_curPos++;
		}
	}

	private void ParserSureColor(string text)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		Color colour = GetColour(text[d_curPos]);
		if (currentConfig.fontColor != colour)
		{
			save(isNewLine: false);
			currentConfig.fontColor = colour;
		}
		d_curPos++;
	}

	private void ParserBlink(string text)
	{
		save(isNewLine: false);
		currentConfig.isBlink = !currentConfig.isBlink;
		d_curPos++;
	}

	private void ParserLineAlignment(string text)
	{
		if (text.Length > d_curPos + 1 && Get(text[d_curPos + 1], out LineAlignment a))
		{
			d_curPos++;
			if (currentConfig.lineAlignment != a)
			{
				currentConfig.lineAlignment = a;
				save(isNewLine: false);
			}
		}
		d_curPos++;
	}

	private void ParserFormatting(string text)
	{
		if (text.Length > d_curPos + 1 && Get(text[d_curPos + 1], out Anchor a))
		{
			d_curPos++;
			if (currentConfig.anchor != a)
			{
				currentConfig.anchor = a;
				save(isNewLine: false);
			}
		}
		d_curPos++;
	}

	private static bool GetFontStyle(char c, out FontStyle fs)
	{
		switch (c)
		{
		case '1':
			fs = (FontStyle)0;
			return true;
		case '2':
			fs = (FontStyle)1;
			return true;
		case '3':
			fs = (FontStyle)2;
			return true;
		case '4':
			fs = (FontStyle)3;
			return true;
		default:
			fs = (FontStyle)0;
			return false;
		}
	}

	private void ParserFontStyle(string text)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		if (text.Length > d_curPos + 1)
		{
			if (GetFontStyle(text[d_curPos + 1], out var fs))
			{
				d_curPos += 2;
				if (currentConfig.fontStyle != fs)
				{
					save(isNewLine: false);
					currentConfig.fontStyle = fs;
				}
			}
			else
			{
				d_curPos++;
			}
		}
		else
		{
			d_curPos++;
		}
	}

	private void ParserStrickout(string text)
	{
		save(isNewLine: false);
		currentConfig.isStrickout = !currentConfig.isStrickout;
		d_curPos++;
	}

	private void ParserFontColorS(string text)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		d_curPos--;
		Color val = Tools.ParserColorName(text, ref d_curPos, currentConfig.fontColor);
		if (val != currentConfig.fontColor)
		{
			save(isNewLine: false);
			currentConfig.fontColor = val;
		}
	}

	private void ParserFontColor(string text)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		Color val = Tools.ParserColorName(text, ref d_curPos, currentConfig.fontColor);
		if (val != currentConfig.fontColor)
		{
			save(isNewLine: false);
			currentConfig.fontColor = val;
		}
	}

	private void ParserUnderLine(string text)
	{
		save(isNewLine: false);
		currentConfig.isUnderline = !currentConfig.isUnderline;
		d_curPos++;
	}

	private void ParserNewLine(string text)
	{
		save(isNewLine: true);
		d_curPos++;
	}

	private void ParserOutputChar(string text)
	{
		d_text.Append('#');
		d_curPos++;
	}

	private void ParserHyperlink(string text)
	{
		int num = text.IndexOf("#h", d_curPos + 1);
		if (num == -1)
		{
			d_curPos++;
			return;
		}
		save(isNewLine: false);
		d_text.Remove(0, d_text.Length);
		d_text.Append(text, d_curPos + 1, num - d_curPos - 1);
		saveHy();
		d_curPos = num + 2;
	}

	private void ParserFontSize(string text)
	{
		float value = 1f;
		if (ParserFloat(ref d_curPos, text, ref value, 2))
		{
			save(isNewLine: false);
			currentConfig.fontSize = (int)value;
		}
	}

	private void ParserXYZ(string text)
	{
		int index = d_curPos;
		float value = 0f;
		if (!ParserFloat(ref d_curPos, text, ref value) || value == 0f)
		{
			return;
		}
		if (text[index] == 'x')
		{
			save(isNewLine: false);
			saveX(value);
		}
		else if (text[index] == 'y')
		{
			if (d_text.Length != 0)
			{
				save(isNewLine: true);
			}
			saveY(value);
		}
		else if (text[index] == 'z')
		{
			if (d_text.Length != 0)
			{
				save(isNewLine: false);
			}
			saveZ(value);
		}
	}

	private void ParserFont(string text)
	{
		d_curPos++;
		Font val = Tools.ParserFontName(text, ref d_curPos);
		if ((Object)(object)val != (Object)null)
		{
			if ((Object)(object)currentConfig.font != (Object)(object)val)
			{
				save(isNewLine: false);
				currentConfig.font = val;
			}
		}
		else
		{
			d_curPos--;
		}
	}

	private void ParseHyText(string text, HyperlinkNode data)
	{
		if (hyConfig == null)
		{
			hyConfig = new HyConfig(this);
		}
		hyConfig.text = text;
		hyConfig.node = data;
		hyConfig.startPos = 0;
		hyConfig.lenght = text.Length;
		PD<StringBuilder> sB = Pool.GetSB();
		try
		{
			hyConfig.BeginParser(sB.value);
			hyConfig.Clear();
		}
		finally
		{
			((IDisposable)sB).Dispose();
		}
	}

	private void Reg(string type, Action<string, TagAttributes> fun)
	{
		TagFuns.Add(type, delegate(string key, string param)
		{
			s_TagAttributes.Release();
			s_TagAttributes.Add(param);
			try
			{
				fun(type, s_TagAttributes);
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
			s_TagAttributes.Release();
		});
	}

	private static Color ParserColorName(string name, int startpos, Color c)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		if (string.IsNullOrEmpty(name))
		{
			return c;
		}
		if (name[startpos] == '#')
		{
			return Tools.ParseColor(name, startpos, c);
		}
		return ColorConst.Get(name, c);
	}

	private static void SetDefaultConfig(NodeBase nb, TagAttributes att)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		nb.d_bBlink = att.getValueAsBool("b", nb.d_bBlink);
		nb.d_color = ParserColorName(att.getValueAsString("c"), 0, nb.d_color);
		int valueAsInteger = att.getValueAsInteger("x", -1);
		if (valueAsInteger > 0)
		{
			nb.d_bOffset = true;
			((Rect)(ref nb.d_rectOffset)).xMin = -valueAsInteger / 2;
			((Rect)(ref nb.d_rectOffset)).xMax = valueAsInteger / 2;
		}
		valueAsInteger = att.getValueAsInteger("y", -1);
		if (valueAsInteger > 0)
		{
			nb.d_bOffset = true;
			((Rect)(ref nb.d_rectOffset)).yMin = -valueAsInteger / 2;
			((Rect)(ref nb.d_rectOffset)).yMax = valueAsInteger / 2;
		}
	}

	private static void SetSizeConfig(RectNode nb, TagAttributes att, Vector2 size)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		nb.width = att.getValueAsFloat("w", size.x);
		nb.height = att.getValueAsFloat("h", size.y);
		switch (att.getValueAsInteger("t", 0))
		{
		case 1:
			nb.height = nb.width * size.y / size.x;
			break;
		case 2:
			nb.width = nb.height * size.x / size.y;
			break;
		}
	}

	private void RegTag()
	{
		TagFuns = new Dictionary<string, Action<string, string>>();
		Reg("sprite ", delegate(string tag, TagAttributes att)
		{
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_005d: Unknown result type (might be due to invalid IL or missing references)
			string valueAsString = att.getValueAsString("n");
			Sprite sprite2 = Tools.GetSprite(valueAsString);
			if ((Object)(object)sprite2 == (Object)null)
			{
				Debug.LogErrorFormat("not find sprite:{0}!", new object[1] { valueAsString });
			}
			else
			{
				Rect rect2 = sprite2.rect;
				Vector2 size = ((Rect)(ref rect2)).size;
				SpriteNode spriteNode = CreateNode<SpriteNode>();
				spriteNode.sprite = sprite2;
				spriteNode.SetConfig(currentConfig);
				SetSizeConfig(spriteNode, att, size);
				SetDefaultConfig(spriteNode, att);
				d_nodeList.Add(spriteNode);
			}
		});
		Reg("pos ", delegate(string tag, TagAttributes att)
		{
			SetPosNode setPosNode = CreateNode<SetPosNode>();
			setPosNode.d_value = att.getValueAsFloat("v", 0f);
			setPosNode.type = (TypePosition)att.getValueAsInteger("t", 0);
			d_nodeList.Add(setPosNode);
		});
		Reg("RectSprite ", delegate(string tag, TagAttributes att)
		{
			//IL_004d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0052: Unknown result type (might be due to invalid IL or missing references)
			Sprite sprite = Tools.GetSprite(att.getValueAsString("n"));
			if ((Object)(object)sprite == (Object)null)
			{
				Debug.LogErrorFormat("not find sprite:{0}!", new object[1] { att.getValueAsString("n") });
			}
			else
			{
				RectSpriteNode rectSpriteNode = CreateNode<RectSpriteNode>();
				rectSpriteNode.SetConfig(currentConfig);
				Rect rect = sprite.rect;
				rectSpriteNode.sprite = sprite;
				((Rect)(ref rectSpriteNode.rect)).width = att.getValueAsFloat("w", ((Rect)(ref rect)).width);
				((Rect)(ref rectSpriteNode.rect)).height = att.getValueAsFloat("h", ((Rect)(ref rect)).height);
				switch (att.getValueAsInteger("t", 0))
				{
				case 1:
					((Rect)(ref rectSpriteNode.rect)).height = ((Rect)(ref rectSpriteNode.rect)).width * ((Rect)(ref rect)).height / ((Rect)(ref rect)).width;
					break;
				case 2:
					((Rect)(ref rectSpriteNode.rect)).width = ((Rect)(ref rectSpriteNode.rect)).height * ((Rect)(ref rect)).width / ((Rect)(ref rect)).height;
					break;
				}
				((Rect)(ref rectSpriteNode.rect)).x = att.getValueAsFloat("px", 0f);
				((Rect)(ref rectSpriteNode.rect)).y = att.getValueAsFloat("py", 0f);
				SetDefaultConfig(rectSpriteNode, att);
				d_nodeList.Add(rectSpriteNode);
			}
		});
		Reg("hy ", delegate(string tag, TagAttributes att)
		{
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Expected I4, but got Unknown
			//IL_005e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0094: Unknown result type (might be due to invalid IL or missing references)
			//IL_0099: Unknown result type (might be due to invalid IL or missing references)
			//IL_009e: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
			HyperlinkNode hyperlinkNode = CreateNode<HyperlinkNode>();
			hyperlinkNode.SetConfig(currentConfig);
			hyperlinkNode.d_text = att.getValueAsString("t");
			hyperlinkNode.d_link = att.getValueAsString("l");
			hyperlinkNode.d_fontSize = att.getValueAsInteger("fs", hyperlinkNode.d_fontSize);
			hyperlinkNode.d_fontStyle = (FontStyle)att.getValueAsInteger("ft", (int)hyperlinkNode.d_fontStyle);
			if (att.exists("fn"))
			{
				hyperlinkNode.d_font = Tools.GetFont(att.getValueAsString("fn"));
			}
			hyperlinkNode.d_color = ParserColorName(att.getValueAsString("fc"), 0, hyperlinkNode.d_color);
			hyperlinkNode.hoveColor = ParserColorName(att.getValueAsString("fhc"), 0, hyperlinkNode.hoveColor);
			hyperlinkNode.d_bUnderline = att.getValueAsBool("ul", hyperlinkNode.d_bUnderline);
			hyperlinkNode.d_bStrickout = att.getValueAsBool("so", hyperlinkNode.d_bStrickout);
			d_nodeList.Add(hyperlinkNode);
		});
		Reg("face ", delegate(string tag, TagAttributes att)
		{
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			Cartoon cartoon = Tools.GetCartoon(att.getValueAsString("n"));
			if (cartoon != null)
			{
				CartoonNode cartoonNode = CreateNode<CartoonNode>();
				cartoonNode.cartoon = cartoon;
				cartoonNode.width = cartoon.width;
				cartoonNode.height = cartoon.height;
				cartoonNode.SetConfig(currentConfig);
				SetSizeConfig(cartoonNode, att, new Vector2((float)cartoon.width, (float)cartoon.height));
				SetDefaultConfig(cartoonNode, att);
				d_nodeList.Add(cartoonNode);
			}
		});
		TagFuns.Add("color=", delegate(string tag, string param)
		{
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			if (!string.IsNullOrEmpty(param))
			{
				currentConfig.fontColor = ParserColorName(param, 1, currentConfig.fontColor);
			}
		});
		TagFuns.Add("/color", delegate
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			currentConfig.fontColor = startConfig.fontColor;
		});
		TagFuns.Add("b", delegate
		{
			ref FontStyle fontStyle4 = ref currentConfig.fontStyle;
			fontStyle4 = (FontStyle)((uint)fontStyle4 | 1u);
		});
		TagFuns.Add("/b", delegate
		{
			ref FontStyle fontStyle3 = ref currentConfig.fontStyle;
			fontStyle3 = (FontStyle)((uint)fontStyle3 & 0xFFFFFFFEu);
		});
		TagFuns.Add("i", delegate
		{
			ref FontStyle fontStyle2 = ref currentConfig.fontStyle;
			fontStyle2 = (FontStyle)((uint)fontStyle2 | 2u);
		});
		TagFuns.Add("/i", delegate
		{
			ref FontStyle fontStyle = ref currentConfig.fontStyle;
			fontStyle = (FontStyle)((uint)fontStyle & 0xFFFFFFFDu);
		});
		TagFuns.Add("size=", delegate(string tag, string param)
		{
			currentConfig.fontSize = (int)Tools.stringToFloat(param, currentConfig.fontSize);
		});
		TagFuns.Add("/size", delegate
		{
			currentConfig.fontSize = startConfig.fontSize;
		});
		Reg("ol ", delegate(string tag, TagAttributes att)
		{
			currentConfig.effectType = EffectType.Outline;
			ParamEffectType(ref currentConfig, att);
		});
		TagFuns.Add("/ol", delegate
		{
			currentConfig.effectType = EffectType.Null;
		});
		Reg("so ", delegate(string tag, TagAttributes att)
		{
			currentConfig.effectType = EffectType.Outline;
			ParamEffectType(ref currentConfig, att);
		});
		TagFuns.Add("/so", delegate
		{
			currentConfig.effectType = EffectType.Null;
		});
		Reg("offset ", delegate(string tag, TagAttributes att)
		{
			float valueAsFloat = att.getValueAsFloat("x", 0f);
			float valueAsFloat2 = att.getValueAsFloat("y", 0f);
			if (!(valueAsFloat <= 0f) || !(valueAsFloat2 <= 0f))
			{
				currentConfig.isOffset = true;
				((Rect)(ref currentConfig.offsetRect)).xMin = (0f - valueAsFloat) / 2f;
				((Rect)(ref currentConfig.offsetRect)).xMax = valueAsFloat / 2f;
				((Rect)(ref currentConfig.offsetRect)).yMin = (0f - valueAsFloat) / 2f;
				((Rect)(ref currentConfig.offsetRect)).yMax = valueAsFloat / 2f;
			}
		});
	}

	private static void ParamEffectType(ref Config config, TagAttributes att)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		config.effectColor = ParserColorName(att.getValueAsString("c"), 0, Color.black);
		config.effectDistance.x = att.getValueAsFloat("x", 1f);
		config.effectDistance.y = att.getValueAsFloat("y", 1f);
	}

	private void TagParam(string tag, string param)
	{
		if (TagFuns.TryGetValue(tag, out var value))
		{
			value(tag, param);
			return;
		}
		Debug.LogErrorFormat("tag:{0} param:{1} not find!", new object[2] { tag, param });
	}
}
