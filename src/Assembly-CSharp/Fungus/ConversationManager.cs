using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001355 RID: 4949
	public class ConversationManager
	{
		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x06007813 RID: 30739 RVA: 0x00051A70 File Offset: 0x0004FC70
		// (set) Token: 0x06007814 RID: 30740 RVA: 0x00051A78 File Offset: 0x0004FC78
		public bool ClearPrev { get; set; }

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x06007815 RID: 30741 RVA: 0x00051A81 File Offset: 0x0004FC81
		// (set) Token: 0x06007816 RID: 30742 RVA: 0x00051A89 File Offset: 0x0004FC89
		public bool WaitForInput { get; set; }

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06007817 RID: 30743 RVA: 0x00051A92 File Offset: 0x0004FC92
		// (set) Token: 0x06007818 RID: 30744 RVA: 0x00051A9A File Offset: 0x0004FC9A
		public bool FadeDone { get; set; }

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06007819 RID: 30745 RVA: 0x00051AA3 File Offset: 0x0004FCA3
		// (set) Token: 0x0600781A RID: 30746 RVA: 0x00051AAB File Offset: 0x0004FCAB
		public FloatData WaitForSeconds { get; internal set; }

		// Token: 0x0600781B RID: 30747 RVA: 0x00051AB4 File Offset: 0x0004FCB4
		public ConversationManager()
		{
			this.ClearPrev = true;
			this.FadeDone = true;
			this.WaitForInput = true;
		}

		// Token: 0x0600781C RID: 30748 RVA: 0x002B5924 File Offset: 0x002B3B24
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

		// Token: 0x0600781D RID: 30749 RVA: 0x002B59E4 File Offset: 0x002B3BE4
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

		// Token: 0x0600781E RID: 30750 RVA: 0x002B5A30 File Offset: 0x002B3C30
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

		// Token: 0x0600781F RID: 30751 RVA: 0x002B5B04 File Offset: 0x002B3D04
		public static List<ConversationManager.RawConversationItem> PreParse(string conv)
		{
			List<ConversationManager.RawConversationItem> retval = new List<ConversationManager.RawConversationItem>();
			ConversationManager.PreParse(conv, delegate(ConversationManager.RawConversationItem ia)
			{
				retval.Add(ia);
			});
			return retval;
		}

		// Token: 0x06007820 RID: 30752 RVA: 0x002B5B3C File Offset: 0x002B3D3C
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

		// Token: 0x06007821 RID: 30753 RVA: 0x002B5B80 File Offset: 0x002B3D80
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

		// Token: 0x06007822 RID: 30754 RVA: 0x00051AD1 File Offset: 0x0004FCD1
		public virtual void PopulateCharacterCache()
		{
			this.characters = Object.FindObjectsOfType<Character>();
		}

		// Token: 0x06007823 RID: 30755 RVA: 0x00051ADE File Offset: 0x0004FCDE
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

		// Token: 0x04006841 RID: 26689
		private const string ConversationTextBodyRegex = "((?<sayParams>[\\w \"><.'-_]*?):)?(?<text>.*)\\r*(\\n|$)";

		// Token: 0x04006842 RID: 26690
		protected Character[] characters;

		// Token: 0x04006843 RID: 26691
		protected bool exitSayWait;

		// Token: 0x02001356 RID: 4950
		public struct RawConversationItem
		{
			// Token: 0x04006848 RID: 26696
			public string[] sayParams;

			// Token: 0x04006849 RID: 26697
			public string text;
		}

		// Token: 0x02001357 RID: 4951
		protected struct ConversationItem
		{
			// Token: 0x17000B33 RID: 2867
			// (get) Token: 0x06007825 RID: 30757 RVA: 0x00051AFD File Offset: 0x0004FCFD
			// (set) Token: 0x06007826 RID: 30758 RVA: 0x00051B05 File Offset: 0x0004FD05
			public string Text { get; set; }

			// Token: 0x17000B34 RID: 2868
			// (get) Token: 0x06007827 RID: 30759 RVA: 0x00051B0E File Offset: 0x0004FD0E
			// (set) Token: 0x06007828 RID: 30760 RVA: 0x00051B16 File Offset: 0x0004FD16
			public Character Character { get; set; }

			// Token: 0x17000B35 RID: 2869
			// (get) Token: 0x06007829 RID: 30761 RVA: 0x00051B1F File Offset: 0x0004FD1F
			// (set) Token: 0x0600782A RID: 30762 RVA: 0x00051B27 File Offset: 0x0004FD27
			public Sprite Portrait { get; set; }

			// Token: 0x17000B36 RID: 2870
			// (get) Token: 0x0600782B RID: 30763 RVA: 0x00051B30 File Offset: 0x0004FD30
			// (set) Token: 0x0600782C RID: 30764 RVA: 0x00051B38 File Offset: 0x0004FD38
			public RectTransform Position { get; set; }

			// Token: 0x17000B37 RID: 2871
			// (get) Token: 0x0600782D RID: 30765 RVA: 0x00051B41 File Offset: 0x0004FD41
			// (set) Token: 0x0600782E RID: 30766 RVA: 0x00051B49 File Offset: 0x0004FD49
			public bool Hide { get; set; }

			// Token: 0x17000B38 RID: 2872
			// (get) Token: 0x0600782F RID: 30767 RVA: 0x00051B52 File Offset: 0x0004FD52
			// (set) Token: 0x06007830 RID: 30768 RVA: 0x00051B5A File Offset: 0x0004FD5A
			public FacingDirection FacingDirection { get; set; }

			// Token: 0x17000B39 RID: 2873
			// (get) Token: 0x06007831 RID: 30769 RVA: 0x00051B63 File Offset: 0x0004FD63
			// (set) Token: 0x06007832 RID: 30770 RVA: 0x00051B6B File Offset: 0x0004FD6B
			public bool Flip { get; set; }

			// Token: 0x17000B3A RID: 2874
			// (get) Token: 0x06007833 RID: 30771 RVA: 0x00051B74 File Offset: 0x0004FD74
			// (set) Token: 0x06007834 RID: 30772 RVA: 0x00051B7C File Offset: 0x0004FD7C
			public bool ClearPrev { get; set; }

			// Token: 0x17000B3B RID: 2875
			// (get) Token: 0x06007835 RID: 30773 RVA: 0x00051B85 File Offset: 0x0004FD85
			// (set) Token: 0x06007836 RID: 30774 RVA: 0x00051B8D File Offset: 0x0004FD8D
			public bool WaitForInput { get; set; }

			// Token: 0x17000B3C RID: 2876
			// (get) Token: 0x06007837 RID: 30775 RVA: 0x00051B96 File Offset: 0x0004FD96
			// (set) Token: 0x06007838 RID: 30776 RVA: 0x00051B9E File Offset: 0x0004FD9E
			public bool FadeDone { get; set; }
		}
	}
}
