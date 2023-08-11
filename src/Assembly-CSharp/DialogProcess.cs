using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;

public class DialogProcess : MonoBehaviour
{
	private void Start()
	{
		Event.registerOut("dialog_close", this, "dialog_close");
		Event.registerOut("dialog_setContent", this, "dialog_setContent");
		Event.registerOut("messagelog_setContent", this, "messagelog_setContent");
	}

	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	public void messagelog_setContent(int talkerId, string title, string body, string sayname)
	{
		new List<UnityAction>();
		MapMessageBox.Show(body, title, delegate(DialogResult result)
		{
			if (result == DialogResult.OK)
			{
				MessageBox.Show("You Clicked " + result, "Dialog Result");
			}
		}, MessageBoxButtons.OKCancel);
	}

	public void dialog_setContent(int talkerId, List<uint> dialogIds, List<string> dialogsTitles, string title, string body, string sayname)
	{
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
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
			list2.Add((UnityAction)delegate
			{
				avatar.dialog(talkerId, dialogId);
			});
		}
		if (list.Count > 0)
		{
			MenuBox.Show(list, list2, sayname + ":\n    " + body);
		}
	}

	public void dialog_close()
	{
	}
}
