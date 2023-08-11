using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Fungus;

public class ConversationManager
{
	public struct RawConversationItem
	{
		public string[] sayParams;

		public string text;
	}

	protected struct ConversationItem
	{
		public string Text { get; set; }

		public Character Character { get; set; }

		public Sprite Portrait { get; set; }

		public RectTransform Position { get; set; }

		public bool Hide { get; set; }

		public FacingDirection FacingDirection { get; set; }

		public bool Flip { get; set; }

		public bool ClearPrev { get; set; }

		public bool WaitForInput { get; set; }

		public bool FadeDone { get; set; }
	}

	private const string ConversationTextBodyRegex = "((?<sayParams>[\\w \"><.'-_]*?):)?(?<text>.*)\\r*(\\n|$)";

	protected Character[] characters;

	protected bool exitSayWait;

	public bool ClearPrev { get; set; }

	public bool WaitForInput { get; set; }

	public bool FadeDone { get; set; }

	public FloatData WaitForSeconds { get; internal set; }

	public ConversationManager()
	{
		ClearPrev = true;
		FadeDone = true;
		WaitForInput = true;
	}

	protected static string[] Split(string stringToSplit)
	{
		List<string> list = new List<string>();
		bool flag = false;
		StringBuilder stringBuilder = new StringBuilder();
		foreach (char c in stringToSplit)
		{
			if (c == '"')
			{
				flag = !flag;
			}
			else if (char.IsWhiteSpace(c) && !flag)
			{
				string text = stringBuilder.ToString().Trim(' ', '\n', '\t', '"');
				if (text != "")
				{
					list.Add(text);
				}
				stringBuilder = new StringBuilder();
			}
			else
			{
				stringBuilder.Append(c);
			}
		}
		string text2 = stringBuilder.ToString().Trim();
		if (text2 != "")
		{
			list.Add(text2);
		}
		return list.ToArray();
	}

	protected virtual SayDialog GetSayDialog(Character character)
	{
		SayDialog sayDialog = null;
		if ((Object)(object)character != (Object)null && (Object)(object)character.SetSayDialog != (Object)null)
		{
			sayDialog = character.SetSayDialog;
		}
		if ((Object)(object)sayDialog == (Object)null)
		{
			sayDialog = SayDialog.GetSayDialog();
		}
		sayDialog.SetActive(state: true);
		SayDialog.ActiveSayDialog = sayDialog;
		return sayDialog;
	}

	public static void PreParse(string conv, Action<RawConversationItem> itemAction)
	{
		MatchCollection matchCollection = new Regex("((?<sayParams>[\\w \"><.'-_]*?):)?(?<text>.*)\\r*(\\n|$)").Matches(conv);
		for (int i = 0; i < matchCollection.Count; i++)
		{
			string text = matchCollection[i].Groups["text"].Value.Trim();
			string value = matchCollection[i].Groups["sayParams"].Value;
			if ((text.Length != 0 || value.Length != 0) && !text.StartsWith("--"))
			{
				string[] array = null;
				array = (string.IsNullOrEmpty(value) ? new string[0] : Split(value));
				RawConversationItem rawConversationItem = default(RawConversationItem);
				rawConversationItem.sayParams = array;
				rawConversationItem.text = text;
				RawConversationItem obj = rawConversationItem;
				itemAction(obj);
			}
		}
	}

	public static List<RawConversationItem> PreParse(string conv)
	{
		List<RawConversationItem> retval = new List<RawConversationItem>();
		PreParse(conv, delegate(RawConversationItem ia)
		{
			retval.Add(ia);
		});
		return retval;
	}

	protected virtual List<ConversationItem> Parse(string conv)
	{
		List<ConversationItem> items = new List<ConversationItem>();
		Character currentCharacter = null;
		PreParse(conv, delegate(RawConversationItem ia)
		{
			ConversationItem item = CreateConversationItem(ia.sayParams, ia.text, currentCharacter);
			currentCharacter = item.Character;
			items.Add(item);
		});
		return items;
	}

	protected virtual ConversationItem CreateConversationItem(string[] sayParams, string text, Character currentCharacter)
	{
		ConversationItem result = default(ConversationItem);
		result.ClearPrev = ClearPrev;
		result.FadeDone = FadeDone;
		result.WaitForInput = WaitForInput;
		result.Text = text;
		if ((float)WaitForSeconds > 0f)
		{
			result.Text = result.Text + "{w=" + WaitForSeconds.ToString() + "}";
		}
		if (sayParams == null || sayParams.Length == 0)
		{
			return result;
		}
		for (int i = 0; i < sayParams.Length; i++)
		{
			if (string.Compare(sayParams[i], "clear", ignoreCase: true) == 0)
			{
				result.ClearPrev = true;
			}
			else if (string.Compare(sayParams[i], "noclear", ignoreCase: true) == 0)
			{
				result.ClearPrev = false;
			}
			else if (string.Compare(sayParams[i], "fade", ignoreCase: true) == 0)
			{
				result.FadeDone = true;
			}
			else if (string.Compare(sayParams[i], "nofade", ignoreCase: true) == 0)
			{
				result.FadeDone = false;
			}
			else if (string.Compare(sayParams[i], "wait", ignoreCase: true) == 0)
			{
				result.WaitForInput = true;
			}
			else if (string.Compare(sayParams[i], "nowait", ignoreCase: true) == 0)
			{
				result.WaitForInput = false;
			}
		}
		int num = -1;
		if (characters == null)
		{
			PopulateCharacterCache();
		}
		int num2 = 0;
		while ((Object)(object)result.Character == (Object)null && num2 < sayParams.Length)
		{
			for (int j = 0; j < characters.Length; j++)
			{
				if (characters[j].NameStartsWith(sayParams[num2]))
				{
					num = num2;
					result.Character = characters[j];
					break;
				}
			}
			num2++;
		}
		if ((Object)(object)result.Character == (Object)null)
		{
			result.Character = currentCharacter;
		}
		int num3 = -1;
		if ((Object)(object)result.Character != (Object)null)
		{
			for (int k = 0; k < sayParams.Length; k++)
			{
				if (k != num && string.Compare(sayParams[k], "hide", ignoreCase: true) == 0)
				{
					num3 = k;
					result.Hide = true;
					break;
				}
			}
		}
		int num4 = -1;
		if ((Object)(object)result.Character != (Object)null)
		{
			for (int l = 0; l < sayParams.Length; l++)
			{
				if (l != num && l != num3 && (string.Compare(sayParams[l], ">>>", ignoreCase: true) == 0 || string.Compare(sayParams[l], "<<<", ignoreCase: true) == 0))
				{
					if (string.Compare(sayParams[l], ">>>", ignoreCase: true) == 0)
					{
						result.FacingDirection = FacingDirection.Right;
					}
					if (string.Compare(sayParams[l], "<<<", ignoreCase: true) == 0)
					{
						result.FacingDirection = FacingDirection.Left;
					}
					num4 = l;
					result.Flip = true;
					break;
				}
			}
		}
		int num5 = -1;
		if ((Object)(object)result.Character != (Object)null)
		{
			for (int m = 0; m < sayParams.Length; m++)
			{
				if ((Object)(object)result.Portrait == (Object)null && (Object)(object)result.Character != (Object)null && m != num && m != num3 && m != num4)
				{
					Sprite portrait = result.Character.GetPortrait(sayParams[m]);
					if ((Object)(object)portrait != (Object)null)
					{
						num5 = m;
						result.Portrait = portrait;
						break;
					}
				}
			}
		}
		Stage activeStage = Stage.GetActiveStage();
		if ((Object)(object)activeStage != (Object)null)
		{
			for (int n = 0; n < sayParams.Length; n++)
			{
				if (n != num && n != num5 && n != num3)
				{
					RectTransform position = activeStage.GetPosition(sayParams[n]);
					if ((Object)(object)position != (Object)null)
					{
						result.Position = position;
						break;
					}
				}
			}
		}
		return result;
	}

	public virtual void PopulateCharacterCache()
	{
		characters = Object.FindObjectsOfType<Character>();
	}

	public virtual IEnumerator DoConversation(string conv)
	{
		if (string.IsNullOrEmpty(conv))
		{
			yield break;
		}
		List<ConversationItem> conversationItems = Parse(conv);
		if (conversationItems.Count == 0)
		{
			yield break;
		}
		Character currentCharacter = null;
		Character previousCharacter = null;
		int i = 0;
		while (i < conversationItems.Count)
		{
			ConversationItem conversationItem = conversationItems[i];
			if ((Object)(object)conversationItem.Character != (Object)null)
			{
				currentCharacter = conversationItem.Character;
			}
			Sprite portrait = conversationItem.Portrait;
			RectTransform position = conversationItem.Position;
			SayDialog sayDialog = GetSayDialog(currentCharacter);
			if ((Object)(object)sayDialog == (Object)null)
			{
				break;
			}
			if ((Object)(object)currentCharacter != (Object)null && (Object)(object)currentCharacter != (Object)(object)previousCharacter)
			{
				sayDialog.SetCharacter(currentCharacter);
			}
			Stage activeStage = Stage.GetActiveStage();
			if ((Object)(object)currentCharacter != (Object)null && !currentCharacter.State.onScreen && (Object)(object)portrait == (Object)null)
			{
				conversationItem.Hide = true;
			}
			if ((Object)(object)activeStage != (Object)null && (Object)(object)currentCharacter != (Object)null && ((Object)(object)portrait != (Object)(object)currentCharacter.State.portrait || (Object)(object)position != (Object)(object)currentCharacter.State.position))
			{
				PortraitOptions portraitOptions = new PortraitOptions();
				portraitOptions.display = ((!conversationItem.Hide) ? DisplayType.Show : DisplayType.Hide);
				portraitOptions.character = currentCharacter;
				portraitOptions.fromPosition = currentCharacter.State.position;
				portraitOptions.toPosition = position;
				portraitOptions.portrait = portrait;
				if (conversationItem.Flip)
				{
					portraitOptions.facing = conversationItem.FacingDirection;
				}
				if (currentCharacter.State.onScreen && (Object)(object)position != (Object)(object)currentCharacter.State.position)
				{
					portraitOptions.move = true;
				}
				if (conversationItem.Hide)
				{
					activeStage.Hide(portraitOptions);
				}
				else
				{
					activeStage.Show(portraitOptions);
				}
			}
			if ((Object)(object)activeStage == (Object)null && (Object)(object)portrait != (Object)null)
			{
				sayDialog.SetCharacterImage(portrait);
			}
			previousCharacter = currentCharacter;
			if (!string.IsNullOrEmpty(conversationItem.Text))
			{
				exitSayWait = false;
				sayDialog.Say(conversationItem.Text, conversationItem.ClearPrev, conversationItem.WaitForInput, conversationItem.FadeDone, stopVoiceover: true, waitForVO: false, null, delegate
				{
					exitSayWait = true;
				});
				while (!exitSayWait)
				{
					yield return null;
				}
				exitSayWait = false;
			}
			int num = i + 1;
			i = num;
		}
	}
}
