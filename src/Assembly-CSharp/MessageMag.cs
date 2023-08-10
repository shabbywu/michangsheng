using System;
using System.Collections.Generic;

public class MessageMag
{
	private static MessageMag instance;

	private Dictionary<string, Action<MessageData>> dictionaryMessage;

	public static MessageMag Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new MessageMag();
			}
			return instance;
		}
	}

	private MessageMag()
	{
		InitData();
	}

	private void InitData()
	{
		dictionaryMessage = new Dictionary<string, Action<MessageData>>();
	}

	public void Register(string key, Action<MessageData> action)
	{
		if (!dictionaryMessage.ContainsKey(key))
		{
			dictionaryMessage.Add(key, null);
		}
		Dictionary<string, Action<MessageData>> dictionary = dictionaryMessage;
		dictionary[key] = (Action<MessageData>)Delegate.Combine(dictionary[key], action);
	}

	public void Remove(string key, Action<MessageData> action)
	{
		if (dictionaryMessage.ContainsKey(key) && dictionaryMessage[key] != null)
		{
			Dictionary<string, Action<MessageData>> dictionary = dictionaryMessage;
			dictionary[key] = (Action<MessageData>)Delegate.Remove(dictionary[key], action);
		}
	}

	public void Send(string key, MessageData data = null)
	{
		if (dictionaryMessage.ContainsKey(key) && dictionaryMessage[key] != null)
		{
			dictionaryMessage[key](data);
		}
	}

	public void Clear()
	{
		dictionaryMessage.Clear();
	}
}
