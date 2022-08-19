using System;
using System.Collections.Generic;

// Token: 0x020001EE RID: 494
public class MessageMag
{
	// Token: 0x1700022F RID: 559
	// (get) Token: 0x0600146A RID: 5226 RVA: 0x000833F4 File Offset: 0x000815F4
	public static MessageMag Instance
	{
		get
		{
			if (MessageMag.instance == null)
			{
				MessageMag.instance = new MessageMag();
			}
			return MessageMag.instance;
		}
	}

	// Token: 0x0600146B RID: 5227 RVA: 0x0008340C File Offset: 0x0008160C
	private MessageMag()
	{
		this.InitData();
	}

	// Token: 0x0600146C RID: 5228 RVA: 0x0008341A File Offset: 0x0008161A
	private void InitData()
	{
		this.dictionaryMessage = new Dictionary<string, Action<MessageData>>();
	}

	// Token: 0x0600146D RID: 5229 RVA: 0x00083428 File Offset: 0x00081628
	public void Register(string key, Action<MessageData> action)
	{
		if (!this.dictionaryMessage.ContainsKey(key))
		{
			this.dictionaryMessage.Add(key, null);
		}
		Dictionary<string, Action<MessageData>> dictionary = this.dictionaryMessage;
		dictionary[key] = (Action<MessageData>)Delegate.Combine(dictionary[key], action);
	}

	// Token: 0x0600146E RID: 5230 RVA: 0x00083474 File Offset: 0x00081674
	public void Remove(string key, Action<MessageData> action)
	{
		if (this.dictionaryMessage.ContainsKey(key) && this.dictionaryMessage[key] != null)
		{
			Dictionary<string, Action<MessageData>> dictionary = this.dictionaryMessage;
			dictionary[key] = (Action<MessageData>)Delegate.Remove(dictionary[key], action);
		}
	}

	// Token: 0x0600146F RID: 5231 RVA: 0x000834BF File Offset: 0x000816BF
	public void Send(string key, MessageData data = null)
	{
		if (this.dictionaryMessage.ContainsKey(key) && this.dictionaryMessage[key] != null)
		{
			this.dictionaryMessage[key](data);
		}
	}

	// Token: 0x06001470 RID: 5232 RVA: 0x000834EF File Offset: 0x000816EF
	public void Clear()
	{
		this.dictionaryMessage.Clear();
	}

	// Token: 0x04000F28 RID: 3880
	private static MessageMag instance;

	// Token: 0x04000F29 RID: 3881
	private Dictionary<string, Action<MessageData>> dictionaryMessage;
}
