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
	// Token: 0x020012D1 RID: 4817
	public class Localization : MonoBehaviour, ISubstitutionHandler
	{
		// Token: 0x06007545 RID: 30021 RVA: 0x0004FF99 File Offset: 0x0004E199
		protected virtual void LevelWasLoaded()
		{
			if (SetLanguage.mostRecentLanguage != "")
			{
				this.activeLanguage = SetLanguage.mostRecentLanguage;
			}
		}

		// Token: 0x06007546 RID: 30022 RVA: 0x0004FFB7 File Offset: 0x0004E1B7
		private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
		{
			this.LevelWasLoaded();
		}

		// Token: 0x06007547 RID: 30023 RVA: 0x0004FFBF File Offset: 0x0004E1BF
		protected virtual void OnEnable()
		{
			StringSubstituter.RegisterHandler(this);
			SceneManager.activeSceneChanged += new UnityAction<Scene, Scene>(this.SceneManager_activeSceneChanged);
		}

		// Token: 0x06007548 RID: 30024 RVA: 0x0004FFD8 File Offset: 0x0004E1D8
		protected virtual void OnDisable()
		{
			StringSubstituter.UnregisterHandler(this);
			SceneManager.activeSceneChanged -= new UnityAction<Scene, Scene>(this.SceneManager_activeSceneChanged);
		}

		// Token: 0x06007549 RID: 30025 RVA: 0x0004FFF1 File Offset: 0x0004E1F1
		protected virtual void Start()
		{
			this.Init();
		}

		// Token: 0x0600754A RID: 30026 RVA: 0x002AF900 File Offset: 0x002ADB00
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

		// Token: 0x0600754B RID: 30027 RVA: 0x002AF954 File Offset: 0x002ADB54
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

		// Token: 0x0600754C RID: 30028 RVA: 0x002AF9A0 File Offset: 0x002ADBA0
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

		// Token: 0x0600754D RID: 30029 RVA: 0x002AFAC4 File Offset: 0x002ADCC4
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

		// Token: 0x0600754E RID: 30030 RVA: 0x0004FFF9 File Offset: 0x0004E1F9
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

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x0600754F RID: 30031 RVA: 0x0005001E File Offset: 0x0004E21E
		public virtual string ActiveLanguage
		{
			get
			{
				return this.activeLanguage;
			}
		}

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06007550 RID: 30032 RVA: 0x00050026 File Offset: 0x0004E226
		// (set) Token: 0x06007551 RID: 30033 RVA: 0x0005002E File Offset: 0x0004E22E
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

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06007552 RID: 30034 RVA: 0x00050037 File Offset: 0x0004E237
		// (set) Token: 0x06007553 RID: 30035 RVA: 0x0005003F File Offset: 0x0004E23F
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

		// Token: 0x06007554 RID: 30036 RVA: 0x00050048 File Offset: 0x0004E248
		public virtual void ClearLocalizeableCache()
		{
			this.localizeableObjects.Clear();
		}

		// Token: 0x06007555 RID: 30037 RVA: 0x002AFBEC File Offset: 0x002ADDEC
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

		// Token: 0x06007556 RID: 30038 RVA: 0x002AFE0C File Offset: 0x002AE00C
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

		// Token: 0x06007557 RID: 30039 RVA: 0x002AFF10 File Offset: 0x002AE110
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

		// Token: 0x06007558 RID: 30040 RVA: 0x002AFF50 File Offset: 0x002AE150
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

		// Token: 0x06007559 RID: 30041 RVA: 0x002B0004 File Offset: 0x002AE204
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

		// Token: 0x0600755A RID: 30042 RVA: 0x002B00D4 File Offset: 0x002AE2D4
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

		// Token: 0x04006687 RID: 26247
		[Tooltip("Language to use at startup, usually defined by a two letter language code (e.g DE = German)")]
		[SerializeField]
		protected string activeLanguage = "";

		// Token: 0x04006688 RID: 26248
		[Tooltip("CSV file containing localization data which can be easily edited in a spreadsheet tool")]
		[SerializeField]
		protected TextAsset localizationFile;

		// Token: 0x04006689 RID: 26249
		protected Dictionary<string, ILocalizable> localizeableObjects = new Dictionary<string, ILocalizable>();

		// Token: 0x0400668A RID: 26250
		protected string notificationText = "";

		// Token: 0x0400668B RID: 26251
		protected bool initialized;

		// Token: 0x0400668C RID: 26252
		protected static Dictionary<string, string> localizedStrings = new Dictionary<string, string>();

		// Token: 0x020012D2 RID: 4818
		protected class TextItem
		{
			// Token: 0x0400668D RID: 26253
			public string description = "";

			// Token: 0x0400668E RID: 26254
			public string standardText = "";

			// Token: 0x0400668F RID: 26255
			public Dictionary<string, string> localizedStrings = new Dictionary<string, string>();
		}
	}
}
