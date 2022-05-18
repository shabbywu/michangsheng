using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005BA RID: 1466
public class UI_CreateAvatar : MonoBehaviour
{
	// Token: 0x060024FB RID: 9467 RVA: 0x0001DB24 File Offset: 0x0001BD24
	private void Start()
	{
		Event.registerOut("onCreateAvatarResult", this, "onCreateAvatarResult");
	}

	// Token: 0x060024FC RID: 9468 RVA: 0x0001429C File Offset: 0x0001249C
	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	// Token: 0x060024FD RID: 9469 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x060024FE RID: 9470 RVA: 0x00129E7C File Offset: 0x0012807C
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

	// Token: 0x060024FF RID: 9471 RVA: 0x0001DB37 File Offset: 0x0001BD37
	public void onCreateAvatarResult(byte retcode, object info, Dictionary<ulong, Dictionary<string, object>> avatarList)
	{
		if (retcode != 0)
		{
			MonoBehaviour.print("创建失败！" + retcode);
			return;
		}
		Application.LoadLevel("selectAvatar");
	}

	// Token: 0x06002500 RID: 9472 RVA: 0x0001DB5C File Offset: 0x0001BD5C
	public void onCancel()
	{
		Application.LoadLevel("selectAvatar");
	}

	// Token: 0x04001F9A RID: 8090
	public InputField if_createAvatarName;

	// Token: 0x04001F9B RID: 8091
	public Toggle[] tg_profs = new Toggle[2];
}
