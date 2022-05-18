using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020002EA RID: 746
public class DialogProcess : MonoBehaviour
{
	// Token: 0x060016BE RID: 5822 RVA: 0x00014267 File Offset: 0x00012467
	private void Start()
	{
		Event.registerOut("dialog_close", this, "dialog_close");
		Event.registerOut("dialog_setContent", this, "dialog_setContent");
		Event.registerOut("messagelog_setContent", this, "messagelog_setContent");
	}

	// Token: 0x060016BF RID: 5823 RVA: 0x0001429C File Offset: 0x0001249C
	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	// Token: 0x060016C0 RID: 5824 RVA: 0x000142A5 File Offset: 0x000124A5
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

	// Token: 0x060016C1 RID: 5825 RVA: 0x000CB900 File Offset: 0x000C9B00
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

	// Token: 0x060016C2 RID: 5826 RVA: 0x000042DD File Offset: 0x000024DD
	public void dialog_close()
	{
	}
}
