using System;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000576 RID: 1398
public class FriendTalkingInfo : MonoBehaviour
{
	// Token: 0x06002382 RID: 9090 RVA: 0x0001CB06 File Offset: 0x0001AD06
	private void Start()
	{
		this.inputInfo = base.transform.Find("Scroll View").Find("InputField").gameObject;
	}

	// Token: 0x06002383 RID: 9091 RVA: 0x00124600 File Offset: 0x00122800
	public void sendMsg()
	{
		Account account = (Account)KBEngineApp.app.player();
		string text = this.inputInfo.transform.Find("Text").GetComponent<Text>().text;
		if (account != null && text != "")
		{
			account.sendMsg(this.friendDbid, text);
		}
	}

	// Token: 0x04001E95 RID: 7829
	public ulong friendDbid;

	// Token: 0x04001E96 RID: 7830
	public string friendName;

	// Token: 0x04001E97 RID: 7831
	public GameObject inputInfo;
}
