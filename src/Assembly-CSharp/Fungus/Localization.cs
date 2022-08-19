using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Ideafixxxer.CsvParser;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Fungus
{
	// Token: 0x02000E73 RID: 3699
	public class Localization : MonoBehaviour, ISubstitutionHandler
	{
		// Token: 0x0600688C RID: 26764 RVA: 0x0028D34F File Offset: 0x0028B54F
		protected virtual void LevelWasLoaded()
		{
			if (SetLanguage.mostRecentLanguage != "")
			{
				this.activeLanguage = SetLanguage.mostRecentLanguage;
			}
		}

		// Token: 0x0600688D RID: 26765 RVA: 0x0028D36D File Offset: 0x0028B56D
		private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
		{
			this.LevelWasLoaded();
		}

		// Token: 0x0600688E RID: 26766 RVA: 0x0028D375 File Offset: 0x0028B575
		protected virtual void OnEnable()
		{
			StringSubstituter.RegisterHandler(this);
			SceneManager.activeSceneChanged += new UnityAction<Scene, Scene>(this.SceneManager_activeSceneChanged);
		}

		// Token: 0x0600688F RID: 26767 RVA: 0x0028D38E File Offset: 0x0028B58E
		protected virtual void OnDisable()
		{
			StringSubstituter.UnregisterHandler(this);
			SceneManager.activeSceneChanged -= new UnityAction<Scene, Scene>(this.SceneManager_activeSceneChanged);
		}

		// Token: 0x06006890 RID: 26768 RVA: 0x0028D3A7 File Offset: 0x0028B5A7
		protected virtual void Start()
		{
			this.Init();
		}

		// Token: 0x06006891 RID: 26769 RVA: 0x0028D3B0 File Offset: 0x0028B5B0
		protected virtual void Init()
		{
			if (this.initialized)
			{
				return;
			}
			this.CacheLocalizeableObjects();
			if (this.localizationFile != null && this.localizationFile.text.Length > 0)
			{
				this.SetActiveLanguage(this.activeLanguage, false);
			}
			this.initialized = true;
		}

		// Token: 0x06006892 RID: 26770 RVA: 0x0028D404 File Offset: 0x0028B604
		protected virtual void CacheLocalizeableObjects()
		{
			Object[] array = Resources.FindObjectsOfTypeAll(typeof(Component));
			for (int i = 0; i < array.Length; i++)
			{
				ILocalizable localizable = array[i] as ILocalizable;
				if (localizable != null)
				{
					this.localizeableObjects[localizable.GetStringId()] = localizable;
				}
			}
		}

		// Token: 0x06006893 RID: 26771 RVA: 0x0028D450 File Offset: 0x0028B650
		protected Dictionary<string, Localization.TextItem> FindTextItems()
		{
			Dictionary<string, Localization.TextItem> dictionary = new Dictionary<string, Localization.TextItem>();
			Flowchart[] array = Object.FindObjectsOfType<Flowchart>();
			for (int i = 0; i < array.Length; i++)
			{
				Block[] components = array[i].GetComponents<Block>();
				for (int j = 0; j < components.Length; j++)
				{
					List<Command> commandList = components[j].CommandList;
					for (int k = 0; k < commandList.Count; k++)
					{
						ILocalizable localizable = commandList[k] as ILocalizable;
						if (localizable != null)
						{
							Localization.TextItem textItem = new Localization.TextItem();
							textItem.standardText = localizable.GetStandardText();
							textItem.description = localizable.GetDescription();
							dictionary[localizable.GetStringId()] = textItem;
						}
					}
				}
			}
			Object[] array2 = Resources.FindObjectsOfTypeAll(typeof(Component));
			for (int l = 0; l < array2.Length; l++)
			{
				ILocalizable localizable2 = array2[l] as ILocalizable;
				if (localizable2 != null)
				{
					string stringId = localizable2.GetStringId();
					if (!dictionary.ContainsKey(stringId))
					{
						dictionary[stringId] = new Localization.TextItem
						{
							standardText = localizable2.GetStandardText(),
							description = localizable2.GetDescription()
						};
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06006894 RID: 26772 RVA: 0x0028D574 File Offset: 0x0028B774
		protected virtual void AddCSVDataItems(Dictionary<string, Localization.TextItem> textItems, string csvData)
		{
			string[][] array = new CsvParser().Parse(csvData);
			if (array.Length <= 1)
			{
				return;
			}
			string[] array2 = array[0];
			for (int i = 1; i < array.Length; i++)
			{
				string[] array3 = array[i];
				if (array3.Length >= 3)
				{
					string text = array3[0];
					if (!textItems.ContainsKey(text))
					{
						if (text.StartsWith("CHARACTER.") || text.StartsWith("SAY.") || text.StartsWith("MENU.") || text.StartsWith("WRITE.") || text.StartsWith("SETTEXT."))
						{
							goto IL_10C;
						}
						textItems[text] = new Localization.TextItem
						{
							description = CSVSupport.Unescape(array3[1]),
							standardText = CSVSupport.Unescape(array3[2])
						};
					}
					Localization.TextItem textItem = textItems[text];
					for (int j = 3; j < array3.Length; j++)
					{
						if (j < array2.Length)
						{
							string key = array2[j];
							string text2 = CSVSupport.Unescape(array3[j]);
							if (text2.Length > 0)
							{
								textItem.localizedStrings[key] = text2;
							}
						}
					}
				}
				IL_10C:;
			}
		}

		// Token: 0x06006895 RID: 26773 RVA: 0x0028D69A File Offset: 0x0028B89A
		public static string GetLocalizedString(string stringId)
		{
			if (Localization.localizedStrings == null)
			{
				return null;
			}
			if (Localization.localizedStrings.ContainsKey(stringId))
			{
				return Localization.localizedStrings[stringId];
			}
			return null;
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06006896 RID: 26774 RVA: 0x0028D6BF File Offset: 0x0028B8BF
		public virtual string ActiveLanguage
		{
			get
			{
				return this.activeLanguage;
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06006897 RID: 26775 RVA: 0x0028D6C7 File Offset: 0x0028B8C7
		// (set) Token: 0x06006898 RID: 26776 RVA: 0x0028D6CF File Offset: 0x0028B8CF
		public virtual TextAsset LocalizationFile
		{
			get
			{
				return this.localizationFile;
			}
			set
			{
				this.localizationFile = value;
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06006899 RID: 26777 RVA: 0x0028D6D8 File Offset: 0x0028B8D8
		// (set) Token: 0x0600689A RID: 26778 RVA: 0x0028D6E0 File Offset: 0x0028B8E0
		public virtual string NotificationText
		{
			get
			{
				return this.notificationText;
			}
			set
			{
				this.notificationText = value;
			}
		}

		// Token: 0x0600689B RID: 26779 RVA: 0x0028D6E9 File Offset: 0x0028B8E9
		public virtual void ClearLocalizeableCache()
		{
			this.localizeableObjects.Clear();
		}

		// Token: 0x0600689C RID: 26780 RVA: 0x0028D6F8 File Offset: 0x0028B8F8
		public virtual string GetCSVData()
		{
			Dictionary<string, Localization.TextItem> dictionary = this.FindTextItems();
			if (this.localizationFile != null && this.localizationFile.text.Length > 0)
			{
				this.AddCSVDataItems(dictionary, this.localizationFile.text);
			}
			string str = "Key,Description,Standard";
			List<string> list = new List<string>();
			foreach (Localization.TextItem textItem in dictionary.Values)
			{
				foreach (string text in textItem.localizedStrings.Keys)
				{
					if (!list.Contains(text))
					{
						list.Add(text);
						str = str + "," + text;
					}
				}
			}
			int num = 0;
			string text2 = str + "\n";
			foreach (string text3 in dictionary.Keys)
			{
				Localization.TextItem textItem2 = dictionary[text3];
				string text4 = CSVSupport.Escape(text3);
				text4 = text4 + "," + CSVSupport.Escape(textItem2.description);
				text4 = text4 + "," + CSVSupport.Escape(textItem2.standardText);
				for (int i = 0; i < list.Count; i++)
				{
					string key = list[i];
					if (textItem2.localizedStrings.ContainsKey(key))
					{
						text4 = text4 + "," + CSVSupport.Escape(textItem2.localizedStrings[key]);
					}
					else
					{
						text4 += ",";
					}
				}
				text2 = text2 + text4 + "\n";
				num++;
			}
			this.notificationText = "Exported " + num + " localization text items.";
			return text2;
		}

		// Token: 0x0600689D RID: 26781 RVA: 0x0028D918 File Offset: 0x0028BB18
		public virtual void SetActiveLanguage(string languageCode, bool forceUpdateSceneText = false)
		{
			if (!Application.isPlaying)
			{
				return;
			}
			if (this.localizationFile == null)
			{
				return;
			}
			Localization.localizedStrings.Clear();
			string[][] array = new CsvParser().Parse(this.localizationFile.text);
			if (array.Length <= 1)
			{
				return;
			}
			string[] array2 = array[0];
			if (array2.Length < 3)
			{
				return;
			}
			int num = 2;
			for (int i = 3; i < array2.Length; i++)
			{
				if (array2[i] == languageCode)
				{
					num = i;
					break;
				}
			}
			if (num == 2)
			{
				for (int j = 1; j < array.Length; j++)
				{
					string[] array3 = array[j];
					if (array3.Length >= 3)
					{
						Localization.localizedStrings[array3[0]] = array3[num];
					}
				}
				if (!forceUpdateSceneText)
				{
					return;
				}
			}
			for (int k = 1; k < array.Length; k++)
			{
				string[] array4 = array[k];
				if (array4.Length >= num + 1)
				{
					string text = array4[0];
					string text2 = CSVSupport.Unescape(array4[num]);
					if (text2.Length > 0)
					{
						Localization.localizedStrings[text] = text2;
						this.PopulateTextProperty(text, text2);
					}
				}
			}
		}

		// Token: 0x0600689E RID: 26782 RVA: 0x0028DA1C File Offset: 0x0028BC1C
		public virtual bool PopulateTextProperty(string stringId, string newText)
		{
			if (this.localizeableObjects.Count == 0)
			{
				this.CacheLocalizeableObjects();
			}
			ILocalizable localizable = null;
			this.localizeableObjects.TryGetValue(stringId, out localizable);
			if (localizable != null)
			{
				localizable.SetStandardText(newText);
				return true;
			}
			return false;
		}

		// Token: 0x0600689F RID: 26783 RVA: 0x0028DA5C File Offset: 0x0028BC5C
		public virtual string GetStandardText()
		{
			Dictionary<string, Localization.TextItem> dictionary = this.FindTextItems();
			string text = "";
			int num = 0;
			foreach (string text2 in dictionary.Keys)
			{
				Localization.TextItem textItem = dictionary[text2];
				text = text + "#" + text2 + "\n";
				text = text + textItem.standardText.Trim() + "\n\n";
				num++;
			}
			this.notificationText = "Exported " + num + " standard text items.";
			return text;
		}

		// Token: 0x060068A0 RID: 26784 RVA: 0x0028DB10 File Offset: 0x0028BD10
		public virtual void SetStandardText(string textData)
		{
			string[] array = textData.Split(new char[]
			{
				'\n'
			});
			int num = 0;
			string text = "";
			string text2 = "";
			foreach (string text3 in array)
			{
				if (text3.StartsWith("#"))
				{
					if (text.Length > 0 && this.PopulateTextProperty(text, text2.Trim()))
					{
						num++;
					}
					text = text3.Substring(1, text3.Length - 1);
					text2 = "";
				}
				else
				{
					text2 = text2 + text3 + "\n";
				}
			}
			if (text.Length > 0 && this.PopulateTextProperty(text, text2.Trim()))
			{
				num++;
			}
			this.notificationText = "Updated " + num + " standard text items.";
		}

		// Token: 0x060068A1 RID: 26785 RVA: 0x0028DBE0 File Offset: 0x0028BDE0
		public virtual bool SubstituteStrings(StringBuilder input)
		{
			this.Init();
			Regex regex = new Regex("{\\$.*?}");
			bool result = false;
			MatchCollection matchCollection = regex.Matches(input.ToString());
			for (int i = 0; i < matchCollection.Count; i++)
			{
				Match match = matchCollection[i];
				string localizedString = Localization.GetLocalizedString(match.Value.Substring(2, match.Value.Length - 3));
				if (localizedString != null)
				{
					input.Replace(match.Value, localizedString);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x040058E9 RID: 22761
		[Tooltip("Language to use at startup, usually defined by a two letter language code (e.g DE = German)")]
		[SerializeField]
		protected string activeLanguage = "";

		// Token: 0x040058EA RID: 22762
		[Tooltip("CSV file containing localization data which can be easily edited in a spreadsheet tool")]
		[SerializeField]
		protected TextAsset localizationFile;

		// Token: 0x040058EB RID: 22763
		protected Dictionary<string, ILocalizable> localizeableObjects = new Dictionary<string, ILocalizable>();

		// Token: 0x040058EC RID: 22764
		protected string notificationText = "";

		// Token: 0x040058ED RID: 22765
		protected bool initialized;

		// Token: 0x040058EE RID: 22766
		protected static Dictionary<string, string> localizedStrings = new Dictionary<string, string>();

		// Token: 0x020016D2 RID: 5842
		protected class TextItem
		{
			// Token: 0x040073F9 RID: 29689
			public string description = "";

			// Token: 0x040073FA RID: 29690
			public string standardText = "";

			// Token: 0x040073FB RID: 29691
			public Dictionary<string, string> localizedStrings = new Dictionary<string, string>();
		}
	}
}
