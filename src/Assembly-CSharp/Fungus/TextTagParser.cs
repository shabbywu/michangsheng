using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200136D RID: 4973
	public static class TextTagParser
	{
		// Token: 0x060078A4 RID: 30884 RVA: 0x002B7444 File Offset: 0x002B5644
		private static void AddWordsToken(List<TextTagToken> tokenList, string words)
		{
			tokenList.Add(new TextTagToken
			{
				type = TokenType.Words,
				paramList = new List<string>(),
				paramList = 
				{
					words
				}
			});
		}

		// Token: 0x060078A5 RID: 30885 RVA: 0x002B747C File Offset: 0x002B567C
		private static void AddTagToken(List<TextTagToken> tokenList, string tagText)
		{
			if (tagText.Length < 3 || tagText.Substring(0, 1) != "{" || tagText.Substring(tagText.Length - 1, 1) != "}")
			{
				return;
			}
			string text = tagText.Substring(1, tagText.Length - 2);
			TokenType tokenType = TokenType.Invalid;
			List<string> paramList = TextTagParser.ExtractParameters(text);
			if (text == "b")
			{
				tokenType = TokenType.BoldStart;
			}
			else if (text == "/b")
			{
				tokenType = TokenType.BoldEnd;
			}
			else if (text == "i")
			{
				tokenType = TokenType.ItalicStart;
			}
			else if (text == "/i")
			{
				tokenType = TokenType.ItalicEnd;
			}
			else if (text.StartsWith("color="))
			{
				tokenType = TokenType.ColorStart;
			}
			else if (text == "/color")
			{
				tokenType = TokenType.ColorEnd;
			}
			else if (text.StartsWith("size="))
			{
				tokenType = TokenType.SizeStart;
			}
			else if (text == "/size")
			{
				tokenType = TokenType.SizeEnd;
			}
			else if (text == "wi")
			{
				tokenType = TokenType.WaitForInputNoClear;
			}
			else if (text == "wc")
			{
				tokenType = TokenType.WaitForInputAndClear;
			}
			else if (text == "wvo")
			{
				tokenType = TokenType.WaitForVoiceOver;
			}
			else if (text.StartsWith("wp="))
			{
				tokenType = TokenType.WaitOnPunctuationStart;
			}
			else if (text == "wp")
			{
				tokenType = TokenType.WaitOnPunctuationStart;
			}
			else if (text == "/wp")
			{
				tokenType = TokenType.WaitOnPunctuationEnd;
			}
			else if (text.StartsWith("w="))
			{
				tokenType = TokenType.Wait;
			}
			else if (text == "w")
			{
				tokenType = TokenType.Wait;
			}
			else if (text == "c")
			{
				tokenType = TokenType.Clear;
			}
			else if (text.StartsWith("s="))
			{
				tokenType = TokenType.SpeedStart;
			}
			else if (text == "s")
			{
				tokenType = TokenType.SpeedStart;
			}
			else if (text == "/s")
			{
				tokenType = TokenType.SpeedEnd;
			}
			else if (text == "x")
			{
				tokenType = TokenType.Exit;
			}
			else if (text.StartsWith("m="))
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
			if (tokenType != TokenType.Invalid)
			{
				tokenList.Add(new TextTagToken
				{
					type = tokenType,
					paramList = paramList
				});
				return;
			}
			Debug.LogWarning("Invalid text tag " + text);
		}

		// Token: 0x060078A6 RID: 30886 RVA: 0x002B779C File Offset: 0x002B599C
		private static List<string> ExtractParameters(string input)
		{
			List<string> list = new List<string>();
			int num = input.IndexOf('=');
			if (num == -1)
			{
				return list;
			}
			foreach (string text in input.Substring(num + 1).Split(new char[]
			{
				','
			}))
			{
				list.Add(text.Trim());
			}
			return list;
		}

		// Token: 0x060078A7 RID: 30887 RVA: 0x00051ECC File Offset: 0x000500CC
		public static string GetTagHelp()
		{
			return "\t{b} Bold Text {/b}\n\t{i} Italic Text {/i}\n\t{color=red} Color Text (color){/color}\n\t{size=30} Text size {/size}\n\n\t{s}, {s=60} Writing speed (chars per sec){/s}\n\t{w}, {w=0.5} Wait (seconds)\n\t{wi} Wait for input\n\t{wc} Wait for input and clear\n\t{wvo} Wait for voice over line to complete\n\t{wp}, {wp=0.5} Wait on punctuation (seconds){/wp}\n\t{c} Clear\n\t{x} Exit, advance to the next command without waiting for input\n\n\t{vpunch=10,0.5} Vertically punch screen (intensity,time)\n\t{hpunch=10,0.5} Horizontally punch screen (intensity,time)\n\t{punch=10,0.5} Punch screen (intensity,time)\n\t{flash=0.5} Flash screen (duration)\n\n\t{audio=AudioObjectName} Play Audio Once\n\t{audioloop=AudioObjectName} Play Audio Loop\n\t{audiopause=AudioObjectName} Pause Audio\n\t{audiostop=AudioObjectName} Stop Audio\n\n\t{m=MessageName} Broadcast message\n\t{$VarName} Substitute variable";
		}

		// Token: 0x060078A8 RID: 30888 RVA: 0x002B77FC File Offset: 0x002B59FC
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
					TextTagParser.AddWordsToken(list, text);
				}
				TextTagParser.AddTagToken(list, value);
				num = match.Index + value.Length;
				match = match.NextMatch();
			}
			if (num < storyText.Length)
			{
				string text2 = storyText.Substring(num, storyText.Length - num);
				if (text2.Length > 0)
				{
					TextTagParser.AddWordsToken(list, text2);
				}
			}
			bool flag = false;
			for (int i = 0; i < list.Count; i++)
			{
				TextTagToken textTagToken = list[i];
				if (flag && textTagToken.type == TokenType.Words)
				{
					textTagToken.paramList[0] = textTagToken.paramList[0].TrimStart(new char[]
					{
						' ',
						'\t',
						'\r',
						'\n'
					});
				}
				flag = (textTagToken.type == TokenType.Clear || textTagToken.type == TokenType.WaitForInputAndClear);
			}
			return list;
		}

		// Token: 0x040068A2 RID: 26786
		private const string TextTokenRegexString = "\\{.*?\\}";
	}
}
