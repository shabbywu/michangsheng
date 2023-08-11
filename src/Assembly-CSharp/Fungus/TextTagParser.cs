using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Fungus;

public static class TextTagParser
{
	private const string TextTokenRegexString = "\\{.*?\\}";

	private static void AddWordsToken(List<TextTagToken> tokenList, string words)
	{
		TextTagToken textTagToken = new TextTagToken();
		textTagToken.type = TokenType.Words;
		textTagToken.paramList = new List<string>();
		textTagToken.paramList.Add(words);
		tokenList.Add(textTagToken);
	}

	private static void AddTagToken(List<TextTagToken> tokenList, string tagText)
	{
		if (tagText.Length < 3 || tagText.Substring(0, 1) != "{" || tagText.Substring(tagText.Length - 1, 1) != "}")
		{
			return;
		}
		string text = tagText.Substring(1, tagText.Length - 2);
		TokenType tokenType = TokenType.Invalid;
		List<string> paramList = ExtractParameters(text);
		switch (text)
		{
		case "b":
			tokenType = TokenType.BoldStart;
			break;
		case "/b":
			tokenType = TokenType.BoldEnd;
			break;
		case "i":
			tokenType = TokenType.ItalicStart;
			break;
		case "/i":
			tokenType = TokenType.ItalicEnd;
			break;
		default:
			if (text.StartsWith("color="))
			{
				tokenType = TokenType.ColorStart;
				break;
			}
			if (text == "/color")
			{
				tokenType = TokenType.ColorEnd;
				break;
			}
			if (text.StartsWith("size="))
			{
				tokenType = TokenType.SizeStart;
				break;
			}
			switch (text)
			{
			case "/size":
				tokenType = TokenType.SizeEnd;
				break;
			case "wi":
				tokenType = TokenType.WaitForInputNoClear;
				break;
			case "wc":
				tokenType = TokenType.WaitForInputAndClear;
				break;
			case "wvo":
				tokenType = TokenType.WaitForVoiceOver;
				break;
			default:
				if (text.StartsWith("wp="))
				{
					tokenType = TokenType.WaitOnPunctuationStart;
					break;
				}
				if (text == "wp")
				{
					tokenType = TokenType.WaitOnPunctuationStart;
					break;
				}
				if (text == "/wp")
				{
					tokenType = TokenType.WaitOnPunctuationEnd;
					break;
				}
				if (text.StartsWith("w="))
				{
					tokenType = TokenType.Wait;
					break;
				}
				if (text == "w")
				{
					tokenType = TokenType.Wait;
					break;
				}
				if (text == "c")
				{
					tokenType = TokenType.Clear;
					break;
				}
				if (text.StartsWith("s="))
				{
					tokenType = TokenType.SpeedStart;
					break;
				}
				switch (text)
				{
				case "s":
					tokenType = TokenType.SpeedStart;
					break;
				case "/s":
					tokenType = TokenType.SpeedEnd;
					break;
				case "x":
					tokenType = TokenType.Exit;
					break;
				default:
					if (text.StartsWith("m="))
					{
						tokenType = TokenType.Message;
					}
					else if (text.StartsWith("vpunch") || text.StartsWith("vpunch="))
					{
						tokenType = TokenType.VerticalPunch;
					}
					else if (text.StartsWith("hpunch") || text.StartsWith("hpunch="))
					{
						tokenType = TokenType.HorizontalPunch;
					}
					else if (text.StartsWith("punch") || text.StartsWith("punch="))
					{
						tokenType = TokenType.Punch;
					}
					else if (text.StartsWith("flash") || text.StartsWith("flash="))
					{
						tokenType = TokenType.Flash;
					}
					else if (text.StartsWith("audio="))
					{
						tokenType = TokenType.Audio;
					}
					else if (text.StartsWith("audioloop="))
					{
						tokenType = TokenType.AudioLoop;
					}
					else if (text.StartsWith("audiopause="))
					{
						tokenType = TokenType.AudioPause;
					}
					else if (text.StartsWith("audiostop="))
					{
						tokenType = TokenType.AudioStop;
					}
					break;
				}
				break;
			}
			break;
		}
		if (tokenType != 0)
		{
			TextTagToken textTagToken = new TextTagToken();
			textTagToken.type = tokenType;
			textTagToken.paramList = paramList;
			tokenList.Add(textTagToken);
		}
		else
		{
			Debug.LogWarning((object)("Invalid text tag " + text));
		}
	}

	private static List<string> ExtractParameters(string input)
	{
		List<string> list = new List<string>();
		int num = input.IndexOf('=');
		if (num == -1)
		{
			return list;
		}
		string[] array = input.Substring(num + 1).Split(new char[1] { ',' });
		foreach (string text in array)
		{
			list.Add(text.Trim());
		}
		return list;
	}

	public static string GetTagHelp()
	{
		return "\t{b} Bold Text {/b}\n\t{i} Italic Text {/i}\n\t{color=red} Color Text (color){/color}\n\t{size=30} Text size {/size}\n\n\t{s}, {s=60} Writing speed (chars per sec){/s}\n\t{w}, {w=0.5} Wait (seconds)\n\t{wi} Wait for input\n\t{wc} Wait for input and clear\n\t{wvo} Wait for voice over line to complete\n\t{wp}, {wp=0.5} Wait on punctuation (seconds){/wp}\n\t{c} Clear\n\t{x} Exit, advance to the next command without waiting for input\n\n\t{vpunch=10,0.5} Vertically punch screen (intensity,time)\n\t{hpunch=10,0.5} Horizontally punch screen (intensity,time)\n\t{punch=10,0.5} Punch screen (intensity,time)\n\t{flash=0.5} Flash screen (duration)\n\n\t{audio=AudioObjectName} Play Audio Once\n\t{audioloop=AudioObjectName} Play Audio Loop\n\t{audiopause=AudioObjectName} Pause Audio\n\t{audiostop=AudioObjectName} Stop Audio\n\n\t{m=MessageName} Broadcast message\n\t{$VarName} Substitute variable";
	}

	public static List<TextTagToken> Tokenize(string storyText)
	{
		List<TextTagToken> list = new List<TextTagToken>();
		Match match = new Regex("\\{.*?\\}").Match(storyText);
		int num = 0;
		while (match.Success)
		{
			string text = storyText.Substring(num, match.Index - num);
			string value = match.Value;
			if (text != "")
			{
				AddWordsToken(list, text);
			}
			AddTagToken(list, value);
			num = match.Index + value.Length;
			match = match.NextMatch();
		}
		if (num < storyText.Length)
		{
			string text2 = storyText.Substring(num, storyText.Length - num);
			if (text2.Length > 0)
			{
				AddWordsToken(list, text2);
			}
		}
		bool flag = false;
		for (int i = 0; i < list.Count; i++)
		{
			TextTagToken textTagToken = list[i];
			if (flag && textTagToken.type == TokenType.Words)
			{
				textTagToken.paramList[0] = textTagToken.paramList[0].TrimStart(' ', '\t', '\r', '\n');
			}
			flag = ((textTagToken.type == TokenType.Clear || textTagToken.type == TokenType.WaitForInputAndClear) ? true : false);
		}
		return list;
	}
}
