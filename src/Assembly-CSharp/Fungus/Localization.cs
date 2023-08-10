using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Ideafixxxer.CsvParser;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fungus;

public class Localization : MonoBehaviour, ISubstitutionHandler
{
	protected class TextItem
	{
		public string description = "";

		public string standardText = "";

		public Dictionary<string, string> localizedStrings = new Dictionary<string, string>();
	}

	[Tooltip("Language to use at startup, usually defined by a two letter language code (e.g DE = German)")]
	[SerializeField]
	protected string activeLanguage = "";

	[Tooltip("CSV file containing localization data which can be easily edited in a spreadsheet tool")]
	[SerializeField]
	protected TextAsset localizationFile;

	protected Dictionary<string, ILocalizable> localizeableObjects = new Dictionary<string, ILocalizable>();

	protected string notificationText = "";

	protected bool initialized;

	protected static Dictionary<string, string> localizedStrings = new Dictionary<string, string>();

	public virtual string ActiveLanguage => activeLanguage;

	public virtual TextAsset LocalizationFile
	{
		get
		{
			return localizationFile;
		}
		set
		{
			localizationFile = value;
		}
	}

	public virtual string NotificationText
	{
		get
		{
			return notificationText;
		}
		set
		{
			notificationText = value;
		}
	}

	protected virtual void LevelWasLoaded()
	{
		if (SetLanguage.mostRecentLanguage != "")
		{
			activeLanguage = SetLanguage.mostRecentLanguage;
		}
	}

	private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
	{
		LevelWasLoaded();
	}

	protected virtual void OnEnable()
	{
		StringSubstituter.RegisterHandler(this);
		SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
	}

	protected virtual void OnDisable()
	{
		StringSubstituter.UnregisterHandler(this);
		SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
	}

	protected virtual void Start()
	{
		Init();
	}

	protected virtual void Init()
	{
		if (!initialized)
		{
			CacheLocalizeableObjects();
			if ((Object)(object)localizationFile != (Object)null && localizationFile.text.Length > 0)
			{
				SetActiveLanguage(activeLanguage);
			}
			initialized = true;
		}
	}

	protected virtual void CacheLocalizeableObjects()
	{
		Object[] array = Resources.FindObjectsOfTypeAll(typeof(Component));
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] is ILocalizable localizable)
			{
				localizeableObjects[localizable.GetStringId()] = localizable;
			}
		}
	}

	protected Dictionary<string, TextItem> FindTextItems()
	{
		Dictionary<string, TextItem> dictionary = new Dictionary<string, TextItem>();
		Flowchart[] array = Object.FindObjectsOfType<Flowchart>();
		for (int i = 0; i < array.Length; i++)
		{
			Block[] components = ((Component)array[i]).GetComponents<Block>();
			for (int j = 0; j < components.Length; j++)
			{
				List<Command> commandList = components[j].CommandList;
				for (int k = 0; k < commandList.Count; k++)
				{
					if (commandList[k] is ILocalizable localizable)
					{
						TextItem textItem = new TextItem();
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
			if (array2[l] is ILocalizable localizable2)
			{
				string stringId = localizable2.GetStringId();
				if (!dictionary.ContainsKey(stringId))
				{
					TextItem textItem2 = new TextItem();
					textItem2.standardText = localizable2.GetStandardText();
					textItem2.description = localizable2.GetDescription();
					dictionary[stringId] = textItem2;
				}
			}
		}
		return dictionary;
	}

	protected virtual void AddCSVDataItems(Dictionary<string, TextItem> textItems, string csvData)
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
			if (array3.Length < 3)
			{
				continue;
			}
			string text = array3[0];
			if (!textItems.ContainsKey(text))
			{
				if (text.StartsWith("CHARACTER.") || text.StartsWith("SAY.") || text.StartsWith("MENU.") || text.StartsWith("WRITE.") || text.StartsWith("SETTEXT."))
				{
					continue;
				}
				TextItem textItem = new TextItem();
				textItem.description = CSVSupport.Unescape(array3[1]);
				textItem.standardText = CSVSupport.Unescape(array3[2]);
				textItems[text] = textItem;
			}
			TextItem textItem2 = textItems[text];
			for (int j = 3; j < array3.Length; j++)
			{
				if (j < array2.Length)
				{
					string key = array2[j];
					string text2 = CSVSupport.Unescape(array3[j]);
					if (text2.Length > 0)
					{
						textItem2.localizedStrings[key] = text2;
					}
				}
			}
		}
	}

	public static string GetLocalizedString(string stringId)
	{
		if (localizedStrings == null)
		{
			return null;
		}
		if (localizedStrings.ContainsKey(stringId))
		{
			return localizedStrings[stringId];
		}
		return null;
	}

	public virtual void ClearLocalizeableCache()
	{
		localizeableObjects.Clear();
	}

	public virtual string GetCSVData()
	{
		Dictionary<string, TextItem> dictionary = FindTextItems();
		if ((Object)(object)localizationFile != (Object)null && localizationFile.text.Length > 0)
		{
			AddCSVDataItems(dictionary, localizationFile.text);
		}
		string text = "Key,Description,Standard";
		List<string> list = new List<string>();
		foreach (TextItem value in dictionary.Values)
		{
			foreach (string key2 in value.localizedStrings.Keys)
			{
				if (!list.Contains(key2))
				{
					list.Add(key2);
					text = text + "," + key2;
				}
			}
		}
		int num = 0;
		string text2 = text + "\n";
		foreach (string key3 in dictionary.Keys)
		{
			TextItem textItem = dictionary[key3];
			string text3 = CSVSupport.Escape(key3);
			text3 = text3 + "," + CSVSupport.Escape(textItem.description);
			text3 = text3 + "," + CSVSupport.Escape(textItem.standardText);
			for (int i = 0; i < list.Count; i++)
			{
				string key = list[i];
				text3 = ((!textItem.localizedStrings.ContainsKey(key)) ? (text3 + ",") : (text3 + "," + CSVSupport.Escape(textItem.localizedStrings[key])));
			}
			text2 = text2 + text3 + "\n";
			num++;
		}
		notificationText = "Exported " + num + " localization text items.";
		return text2;
	}

	public virtual void SetActiveLanguage(string languageCode, bool forceUpdateSceneText = false)
	{
		if (!Application.isPlaying || (Object)(object)localizationFile == (Object)null)
		{
			return;
		}
		localizedStrings.Clear();
		string[][] array = new CsvParser().Parse(localizationFile.text);
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
					localizedStrings[array3[0]] = array3[num];
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
					localizedStrings[text] = text2;
					PopulateTextProperty(text, text2);
				}
			}
		}
	}

	public virtual bool PopulateTextProperty(string stringId, string newText)
	{
		if (localizeableObjects.Count == 0)
		{
			CacheLocalizeableObjects();
		}
		ILocalizable value = null;
		localizeableObjects.TryGetValue(stringId, out value);
		if (value != null)
		{
			value.SetStandardText(newText);
			return true;
		}
		return false;
	}

	public virtual string GetStandardText()
	{
		Dictionary<string, TextItem> dictionary = FindTextItems();
		string text = "";
		int num = 0;
		foreach (string key in dictionary.Keys)
		{
			TextItem textItem = dictionary[key];
			text = text + "#" + key + "\n";
			text = text + textItem.standardText.Trim() + "\n\n";
			num++;
		}
		notificationText = "Exported " + num + " standard text items.";
		return text;
	}

	public virtual void SetStandardText(string textData)
	{
		string[] array = textData.Split(new char[1] { '\n' });
		int num = 0;
		string text = "";
		string text2 = "";
		foreach (string text3 in array)
		{
			if (text3.StartsWith("#"))
			{
				if (text.Length > 0 && PopulateTextProperty(text, text2.Trim()))
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
		if (text.Length > 0 && PopulateTextProperty(text, text2.Trim()))
		{
			num++;
		}
		notificationText = "Updated " + num + " standard text items.";
	}

	public virtual bool SubstituteStrings(StringBuilder input)
	{
		Init();
		Regex regex = new Regex("{\\$.*?}");
		bool result = false;
		MatchCollection matchCollection = regex.Matches(input.ToString());
		for (int i = 0; i < matchCollection.Count; i++)
		{
			Match match = matchCollection[i];
			string localizedString = GetLocalizedString(match.Value.Substring(2, match.Value.Length - 3));
			if (localizedString != null)
			{
				input.Replace(match.Value, localizedString);
				result = true;
			}
		}
		return result;
	}
}
