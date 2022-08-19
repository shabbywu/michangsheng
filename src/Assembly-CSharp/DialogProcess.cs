using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020001DA RID: 474
public class DialogProcess : MonoBehaviour
{
	// Token: 0x0600141F RID: 5151 RVA: 0x00082689 File Offset: 0x00080889
	private void Start()
	{
		Event.registerOut("dialog_close", this, "dialog_close");
		Event.registerOut("dialog_setContent", this, "dialog_setContent");
		Event.registerOut("messagelog_setContent", this, "messagelog_setContent");
	}

	// Token: 0x06001420 RID: 5152 RVA: 0x000826BE File Offset: 0x000808BE
	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	// Token: 0x06001421 RID: 5153 RVA: 0x000826C7 File Offset: 0x000808C7
	public void messagelog_setContent(int talkerId, string title, string body, string sayname)
	{
		new List<UnityAction>();
		MapMessageBox.Show(body, title, delegate(DialogResult result)
		{
			if (result == DialogResult.OK)
			{
				MessageBox.Show("You Clicked " + result.ToString(), "Dialog Result", null, MessageBoxButtons.OK);
			}
		}, MessageBoxButtons.OKCancel);
	}

	// Token: 0x06001422 RID: 5154 RVA: 0x000826F8 File Offset: 0x000808F8
	public void dialog_setContent(int talkerId, List<uint> dialogIds, List<string> dialogsTitles, string title, string body, string sayname)
	{
		Entity entity = KBEngineApp.app.player();
		Avatar avatar = null;
		if (entity != null && entity.className == "Avatar")
		{
			avatar = (Avatar)entity;
		}
		List<string> list = new List<string>();
		List<UnityAction> list2 = new List<UnityAction>();
		for (int i = 0; i < dialogsTitles.Count; i++)
		{
			list.Add(dialogsTitles[i]);
			uint dialogId = dialogIds[i];
			list2.Add(delegate
			{
				avatar.dialog(talkerId, dialogId);
			});
		}
		if (list.Count > 0)
		{
			MenuBox.Show(list, list2, sayname + ":\n    " + body);
		}
	}

	// Token: 0x06001423 RID: 5155 RVA: 0x00004095 File Offset: 0x00002295
	public void dialog_close()
	{
	}
}
