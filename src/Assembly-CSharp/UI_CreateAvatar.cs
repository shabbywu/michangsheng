using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class UI_CreateAvatar : MonoBehaviour
{
	public InputField if_createAvatarName;

	public Toggle[] tg_profs = (Toggle[])(object)new Toggle[2];

	private void Start()
	{
		Event.registerOut("onCreateAvatarResult", this, "onCreateAvatarResult");
	}

	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	private void Update()
	{
	}

	public void onCreateAvatar()
	{
		Account account = ((KBEngineApp.app == null) ? null : ((Account)KBEngineApp.app.player()));
		if (account == null)
		{
			return;
		}
		byte roleType = 1;
		for (int i = 0; i < tg_profs.Length; i++)
		{
			if (tg_profs[i].isOn)
			{
				roleType = (byte)(i + 1);
			}
		}
		account.reqCreateAvatar(if_createAvatarName.text, roleType);
	}

	public void onCreateAvatarResult(byte retcode, object info, Dictionary<ulong, Dictionary<string, object>> avatarList)
	{
		if (retcode != 0)
		{
			MonoBehaviour.print((object)("创建失败！" + retcode));
		}
		else
		{
			Application.LoadLevel("selectAvatar");
		}
	}

	public void onCancel()
	{
		Application.LoadLevel("selectAvatar");
	}
}
