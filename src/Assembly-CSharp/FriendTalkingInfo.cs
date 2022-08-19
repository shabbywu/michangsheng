using System;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003DB RID: 987
public class FriendTalkingInfo : MonoBehaviour
{
	// Token: 0x06002002 RID: 8194 RVA: 0x000E1BBD File Offset: 0x000DFDBD
	private void Start()
	{
		this.inputInfo = base.transform.Find("Scroll View").Find("InputField").gameObject;
	}

	// Token: 0x06002003 RID: 8195 RVA: 0x000E1BE4 File Offset: 0x000DFDE4
	public void sendMsg()
	{
		Account account = (Account)KBEngineApp.app.player();
		string text = this.inputInfo.transform.Find("Text").GetComponent<Text>().text;
		if (account != null && text != "")
		{
			account.sendMsg(this.friendDbid, text);
		}
	}

	// Token: 0x04001A03 RID: 6659
	public ulong friendDbid;

	// Token: 0x04001A04 RID: 6660
	public string friendName;

	// Token: 0x04001A05 RID: 6661
	public GameObject inputInfo;
}
