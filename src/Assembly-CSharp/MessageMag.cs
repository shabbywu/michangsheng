using System;
using System.Collections.Generic;

// Token: 0x02000302 RID: 770
public class MessageMag
{
	// Token: 0x17000277 RID: 631
	// (get) Token: 0x06001714 RID: 5908 RVA: 0x00014624 File Offset: 0x00012824
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

	// Token: 0x06001715 RID: 5909 RVA: 0x0001463C File Offset: 0x0001283C
	private MessageMag()
	{
		this.InitData();
	}

	// Token: 0x06001716 RID: 5910 RVA: 0x0001464A File Offset: 0x0001284A
	private void InitData()
	{
		this.dictionaryMessage = new Dictionary<string, Action<MessageData>>();
	}

	// Token: 0x06001717 RID: 5911 RVA: 0x000CC360 File Offset: 0x000CA560
	public void Register(string key, Action<MessageData> action)
	{
		if (!this.dictionaryMessage.ContainsKey(key))
		{
			this.dictionaryMessage.Add(key, null);
		}
		Dictionary<string, Action<MessageData>> dictionary = this.dictionaryMessage;
		dictionary[key] = (Action<MessageData>)Delegate.Combine(dictionary[key], action);
	}

	// Token: 0x06001718 RID: 5912 RVA: 0x000CC3AC File Offset: 0x000CA5AC
	public void Remove(string key, Action<MessageData> action)
	{
		if (this.dictionaryMessage.ContainsKey(key) && this.dictionaryMessage[key] != null)
		{
			Dictionary<string, Action<MessageData>> dictionary = this.dictionaryMessage;
			dictionary[key] = (Action<MessageData>)Delegate.Remove(dictionary[key], action);
		}
	}

	// Token: 0x06001719 RID: 5913 RVA: 0x00014657 File Offset: 0x00012857
	public void Send(string key, MessageData data = null)
	{
		if (this.dictionaryMessage.ContainsKey(key) && this.dictionaryMessage[key] != null)
		{
			this.dictionaryMessage[key](data);
		}
	}

	// Token: 0x0600171A RID: 5914 RVA: 0x00014687 File Offset: 0x00012887
	public void Clear()
	{
		this.dictionaryMessage.Clear();
	}

	// Token: 0x0400126B RID: 4715
	private static MessageMag instance;

	// Token: 0x0400126C RID: 4716
	private Dictionary<string, Action<MessageData>> dictionaryMessage;
}
