using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200040A RID: 1034
public class UI_CreateAvatar : MonoBehaviour
{
	// Token: 0x06002149 RID: 8521 RVA: 0x000E834B File Offset: 0x000E654B
	private void Start()
	{
		Event.registerOut("onCreateAvatarResult", this, "onCreateAvatarResult");
	}

	// Token: 0x0600214A RID: 8522 RVA: 0x000826BE File Offset: 0x000808BE
	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	// Token: 0x0600214B RID: 8523 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x0600214C RID: 8524 RVA: 0x000E8360 File Offset: 0x000E6560
	public void onCreateAvatar()
	{
		Account account = (KBEngineApp.app == null) ? null : ((Account)KBEngineApp.app.player());
		if (account != null)
		{
			byte roleType = 1;
			for (int i = 0; i < this.tg_profs.Length; i++)
			{
				if (this.tg_profs[i].isOn)
				{
					roleType = (byte)(i + 1);
				}
			}
			account.reqCreateAvatar(this.if_createAvatarName.text, roleType);
		}
	}

	// Token: 0x0600214D RID: 8525 RVA: 0x000E83C5 File Offset: 0x000E65C5
	public void onCreateAvatarResult(byte retcode, object info, Dictionary<ulong, Dictionary<string, object>> avatarList)
	{
		if (retcode != 0)
		{
			MonoBehaviour.print("创建失败！" + retcode);
			return;
		}
		Application.LoadLevel("selectAvatar");
	}

	// Token: 0x0600214E RID: 8526 RVA: 0x000E83EA File Offset: 0x000E65EA
	public void onCancel()
	{
		Application.LoadLevel("selectAvatar");
	}

	// Token: 0x04001ADE RID: 6878
	public InputField if_createAvatarName;

	// Token: 0x04001ADF RID: 6879
	public Toggle[] tg_profs = new Toggle[2];
}
