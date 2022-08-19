using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EBD RID: 3773
	public class ConversationManager
	{
		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06006A9A RID: 27290 RVA: 0x0029369A File Offset: 0x0029189A
		// (set) Token: 0x06006A9B RID: 27291 RVA: 0x002936A2 File Offset: 0x002918A2
		public bool ClearPrev { get; set; }

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06006A9C RID: 27292 RVA: 0x002936AB File Offset: 0x002918AB
		// (set) Token: 0x06006A9D RID: 27293 RVA: 0x002936B3 File Offset: 0x002918B3
		public bool WaitForInput { get; set; }

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06006A9E RID: 27294 RVA: 0x002936BC File Offset: 0x002918BC
		// (set) Token: 0x06006A9F RID: 27295 RVA: 0x002936C4 File Offset: 0x002918C4
		public bool FadeDone { get; set; }

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06006AA0 RID: 27296 RVA: 0x002936CD File Offset: 0x002918CD
		// (set) Token: 0x06006AA1 RID: 27297 RVA: 0x002936D5 File Offset: 0x002918D5
		public FloatData WaitForSeconds { get; internal set; }

		// Token: 0x06006AA2 RID: 27298 RVA: 0x002936DE File Offset: 0x002918DE
		public ConversationManager()
		{
			this.ClearPrev = true;
			this.FadeDone = true;
			this.WaitForInput = true;
		}

		// Token: 0x06006AA3 RID: 27299 RVA: 0x002936FC File Offset: 0x002918FC
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
					string text = stringBuilder.ToString().Trim(new char[]
					{
						' ',
						'\n',
						'\t',
						'"'
					});
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

		// Token: 0x06006AA4 RID: 27300 RVA: 0x002937BC File Offset: 0x002919BC
		protected virtual SayDialog GetSayDialog(Character character)
		{
			SayDialog sayDialog = null;
			if (character != null && character.SetSayDialog != null)
			{
				sayDialog = character.SetSayDialog;
			}
			if (sayDialog == null)
			{
				sayDialog = SayDialog.GetSayDialog();
			}
			sayDialog.SetActive(true);
			SayDialog.ActiveSayDialog = sayDialog;
			return sayDialog;
		}

		// Token: 0x06006AA5 RID: 27301 RVA: 0x00293808 File Offset: 0x00291A08
		public static void PreParse(string conv, Action<ConversationManager.RawConversationItem> itemAction)
		{
			MatchCollection matchCollection = new Regex("((?<sayParams>[\\w \"><.'-_]*?):)?(?<text>.*)\\r*(\\n|$)").Matches(conv);
			for (int i = 0; i < matchCollection.Count; i++)
			{
				string text = matchCollection[i].Groups["text"].Value.Trim();
				string value = matchCollection[i].Groups["sayParams"].Value;
				if ((text.Length != 0 || value.Length != 0) && !text.StartsWith("--"))
				{
					string[] sayParams;
					if (!string.IsNullOrEmpty(value))
					{
						sayParams = ConversationManager.Split(value);
					}
					else
					{
						sayParams = new string[0];
					}
					ConversationManager.RawConversationItem obj = new ConversationManager.RawConversationItem
					{
						sayParams = sayParams,
						text = text
					};
					itemAction(obj);
				}
			}
		}

		// Token: 0x06006AA6 RID: 27302 RVA: 0x002938DC File Offset: 0x00291ADC
		public static List<ConversationManager.RawConversationItem> PreParse(string conv)
		{
			List<ConversationManager.RawConversationItem> retval = new List<ConversationManager.RawConversationItem>();
			ConversationManager.PreParse(conv, delegate(ConversationManager.RawConversationItem ia)
			{
				retval.Add(ia);
			});
			return retval;
		}

		// Token: 0x06006AA7 RID: 27303 RVA: 0x00293914 File Offset: 0x00291B14
		protected virtual List<ConversationManager.ConversationItem> Parse(string conv)
		{
			List<ConversationManager.ConversationItem> items = new List<ConversationManager.ConversationItem>();
			Character currentCharacter = null;
			ConversationManager.PreParse(conv, delegate(ConversationManager.RawConversationItem ia)
			{
				ConversationManager.ConversationItem item = this.CreateConversationItem(ia.sayParams, ia.text, currentCharacter);
				currentCharacter = item.Character;
				items.Add(item);
			});
			return items;
		}

		// Token: 0x06006AA8 RID: 27304 RVA: 0x00293958 File Offset: 0x00291B58
		protected virtual ConversationManager.ConversationItem CreateConversationItem(string[] sayParams, string text, Character currentCharacter)
		{
			ConversationManager.ConversationItem result = default(ConversationManager.ConversationItem);
			result.ClearPrev = this.ClearPrev;
			result.FadeDone = this.FadeDone;
			result.WaitForInput = this.WaitForInput;
			result.Text = text;
			if (this.WaitForSeconds > 0f)
			{
				result.Text = result.Text + "{w=" + this.WaitForSeconds.ToString() + "}";
			}
			if (sayParams == null || sayParams.Length == 0)
			{
				return result;
			}
			for (int i = 0; i < sayParams.Length; i++)
			{
				if (string.Compare(sayParams[i], "clear", true) == 0)
				{
					result.ClearPrev = true;
				}
				else if (string.Compare(sayParams[i], "noclear", true) == 0)
				{
					result.ClearPrev = false;
				}
				else if (string.Compare(sayParams[i], "fade", true) == 0)
				{
					result.FadeDone = true;
				}
				else if (string.Compare(sayParams[i], "nofade", true) == 0)
				{
					result.FadeDone = false;
				}
				else if (string.Compare(sayParams[i], "wait", true) == 0)
				{
					result.WaitForInput = true;
				}
				else if (string.Compare(sayParams[i], "nowait", true) == 0)
				{
					result.WaitForInput = false;
				}
			}
			int num = -1;
			if (this.characters == null)
			{
				this.PopulateCharacterCache();
			}
			int num2 = 0;
			while (result.Character == null && num2 < sayParams.Length)
			{
				for (int j = 0; j < this.characters.Length; j++)
				{
					if (this.characters[j].NameStartsWith(sayParams[num2]))
					{
						num = num2;
						result.Character = this.characters[j];
						break;
					}
				}
				num2++;
			}
			if (result.Character == null)
			{
				result.Character = currentCharacter;
			}
			int num3 = -1;
			if (result.Character != null)
			{
				for (int k = 0; k < sayParams.Length; k++)
				{
					if (k != num && string.Compare(sayParams[k], "hide", true) == 0)
					{
						num3 = k;
						result.Hide = true;
						break;
					}
				}
			}
			int num4 = -1;
			if (result.Character != null)
			{
				for (int l = 0; l < sayParams.Length; l++)
				{
					if (l != num && l != num3 && (string.Compare(sayParams[l], ">>>", true) == 0 || string.Compare(sayParams[l], "<<<", true) == 0))
					{
						if (string.Compare(sayParams[l], ">>>", true) == 0)
						{
							result.FacingDirection = FacingDirection.Right;
						}
						if (string.Compare(sayParams[l], "<<<", true) == 0)
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
			if (result.Character != null)
			{
				for (int m = 0; m < sayParams.Length; m++)
				{
					if (result.Portrait == null && result.Character != null && m != num && m != num3 && m != num4)
					{
						Sprite portrait = result.Character.GetPortrait(sayParams[m]);
						if (portrait != null)
						{
							num5 = m;
							result.Portrait = portrait;
							break;
						}
					}
				}
			}
			Stage activeStage = Stage.GetActiveStage();
			if (activeStage != null)
			{
				for (int n = 0; n < sayParams.Length; n++)
				{
					if (n != num && n != num5 && n != num3)
					{
						RectTransform position = activeStage.GetPosition(sayParams[n]);
						if (position != null)
						{
							result.Position = position;
							break;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06006AA9 RID: 27305 RVA: 0x00293CD1 File Offset: 0x00291ED1
		public virtual void PopulateCharacterCache()
		{
			this.characters = Object.FindObjectsOfType<Character>();
		}

		// Token: 0x06006AAA RID: 27306 RVA: 0x00293CDE File Offset: 0x00291EDE
		public virtual IEnumerator DoConversation(string conv)
		{
			if (string.IsNullOrEmpty(conv))
			{
				yield break;
			}
			List<ConversationManager.ConversationItem> conversationItems = this.Parse(conv);
			if (conversationItems.Count == 0)
			{
				yield break;
			}
			Character currentCharacter = null;
			Character previousCharacter = null;
			int num;
			for (int i = 0; i < conversationItems.Count; i = num)
			{
				ConversationManager.ConversationItem conversationItem = conversationItems[i];
				if (conversationItem.Character != null)
				{
					currentCharacter = conversationItem.Character;
				}
				Sprite portrait = conversationItem.Portrait;
				RectTransform position = conversationItem.Position;
				SayDialog sayDialog = this.GetSayDialog(currentCharacter);
				if (sayDialog == null)
				{
					yield break;
				}
				if (currentCharacter != null && currentCharacter != previousCharacter)
				{
					sayDialog.SetCharacter(currentCharacter, 0);
				}
				Stage activeStage = Stage.GetActiveStage();
				if (currentCharacter != null && !currentCharacter.State.onScreen && portrait == null)
				{
					conversationItem.Hide = true;
				}
				if (activeStage != null && currentCharacter != null && (portrait != currentCharacter.State.portrait || position != currentCharacter.State.position))
				{
					PortraitOptions portraitOptions = new PortraitOptions(true);
					portraitOptions.display = (conversationItem.Hide ? DisplayType.Hide : DisplayType.Show);
					portraitOptions.character = currentCharacter;
					portraitOptions.fromPosition = currentCharacter.State.position;
					portraitOptions.toPosition = position;
					portraitOptions.portrait = portrait;
					if (conversationItem.Flip)
					{
						portraitOptions.facing = conversationItem.FacingDirection;
					}
					if (currentCharacter.State.onScreen && position != currentCharacter.State.position)
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
				if (activeStage == null && portrait != null)
				{
					sayDialog.SetCharacterImage(portrait, 0);
				}
				previousCharacter = currentCharacter;
				if (!string.IsNullOrEmpty(conversationItem.Text))
				{
					this.exitSayWait = false;
					sayDialog.Say(conversationItem.Text, conversationItem.ClearPrev, conversationItem.WaitForInput, conversationItem.FadeDone, true, false, null, delegate
					{
						this.exitSayWait = true;
					});
					while (!this.exitSayWait)
					{
						yield return null;
					}
					this.exitSayWait = false;
				}
				num = i + 1;
			}
			yield break;
		}

		// Token: 0x040059FA RID: 23034
		private const string ConversationTextBodyRegex = "((?<sayParams>[\\w \"><.'-_]*?):)?(?<text>.*)\\r*(\\n|$)";

		// Token: 0x040059FB RID: 23035
		protected Character[] characters;

		// Token: 0x040059FC RID: 23036
		protected bool exitSayWait;

		// Token: 0x0200170C RID: 5900
		public struct RawConversationItem
		{
			// Token: 0x040074A2 RID: 29858
			public string[] sayParams;

			// Token: 0x040074A3 RID: 29859
			public string text;
		}

		// Token: 0x0200170D RID: 5901
		protected struct ConversationItem
		{
			// Token: 0x17000BB3 RID: 2995
			// (get) Token: 0x060088E3 RID: 35043 RVA: 0x002E9ACC File Offset: 0x002E7CCC
			// (set) Token: 0x060088E4 RID: 35044 RVA: 0x002E9AD4 File Offset: 0x002E7CD4
			public string Text { get; set; }

			// Token: 0x17000BB4 RID: 2996
			// (get) Token: 0x060088E5 RID: 35045 RVA: 0x002E9ADD File Offset: 0x002E7CDD
			// (set) Token: 0x060088E6 RID: 35046 RVA: 0x002E9AE5 File Offset: 0x002E7CE5
			public Character Character { get; set; }

			// Token: 0x17000BB5 RID: 2997
			// (get) Token: 0x060088E7 RID: 35047 RVA: 0x002E9AEE File Offset: 0x002E7CEE
			// (set) Token: 0x060088E8 RID: 35048 RVA: 0x002E9AF6 File Offset: 0x002E7CF6
			public Sprite Portrait { get; set; }

			// Token: 0x17000BB6 RID: 2998
			// (get) Token: 0x060088E9 RID: 35049 RVA: 0x002E9AFF File Offset: 0x002E7CFF
			// (set) Token: 0x060088EA RID: 35050 RVA: 0x002E9B07 File Offset: 0x002E7D07
			public RectTransform Position { get; set; }

			// Token: 0x17000BB7 RID: 2999
			// (get) Token: 0x060088EB RID: 35051 RVA: 0x002E9B10 File Offset: 0x002E7D10
			// (set) Token: 0x060088EC RID: 35052 RVA: 0x002E9B18 File Offset: 0x002E7D18
			public bool Hide { get; set; }

			// Token: 0x17000BB8 RID: 3000
			// (get) Token: 0x060088ED RID: 35053 RVA: 0x002E9B21 File Offset: 0x002E7D21
			// (set) Token: 0x060088EE RID: 35054 RVA: 0x002E9B29 File Offset: 0x002E7D29
			public FacingDirection FacingDirection { get; set; }

			// Token: 0x17000BB9 RID: 3001
			// (get) Token: 0x060088EF RID: 35055 RVA: 0x002E9B32 File Offset: 0x002E7D32
			// (set) Token: 0x060088F0 RID: 35056 RVA: 0x002E9B3A File Offset: 0x002E7D3A
			public bool Flip { get; set; }

			// Token: 0x17000BBA RID: 3002
			// (get) Token: 0x060088F1 RID: 35057 RVA: 0x002E9B43 File Offset: 0x002E7D43
			// (set) Token: 0x060088F2 RID: 35058 RVA: 0x002E9B4B File Offset: 0x002E7D4B
			public bool ClearPrev { get; set; }

			// Token: 0x17000BBB RID: 3003
			// (get) Token: 0x060088F3 RID: 35059 RVA: 0x002E9B54 File Offset: 0x002E7D54
			// (set) Token: 0x060088F4 RID: 35060 RVA: 0x002E9B5C File Offset: 0x002E7D5C
			public bool WaitForInput { get; set; }

			// Token: 0x17000BBC RID: 3004
			// (get) Token: 0x060088F5 RID: 35061 RVA: 0x002E9B65 File Offset: 0x002E7D65
			// (set) Token: 0x060088F6 RID: 35062 RVA: 0x002E9B6D File Offset: 0x002E7D6D
			public bool FadeDone { get; set; }
		}
	}
}
