using System;
using System.Collections.Generic;
using System.Threading;
using KBEngine;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using YSGame;

// Token: 0x020003E9 RID: 1001
public class CreateNewPlayerFactory
{
	// Token: 0x06001B42 RID: 6978 RVA: 0x000F0810 File Offset: 0x000EEA10
	public void createPlayer(int id, int index, string firstName, string lastName, Avatar avatar)
	{
		this.isCreateComplete = false;
		Tools.instance.NextSaveTime = DateTime.Now;
		YSSaveGame.save("SaveAvatar" + id, 1, "-1");
		PlayerPrefs.SetInt("NowPlayerFileAvatar", id);
		avatar.name = firstName + lastName;
		avatar.firstName = firstName;
		avatar.lastName = lastName;
		YSSaveGame.save("PlayerAvatarName" + id, avatar.name, "-1");
		YSSaveGame.save("FirstSetAvatarRandomJsonData" + Tools.instance.getSaveID(id, index), 1, "-1");
		Loom.RunAsync(delegate
		{
			Tools.instance.IsInDF = false;
			jsonData.instance.AvatarBackpackJsonData = null;
			FactoryManager.inst.npcFactory.firstCreateNpcs();
			this.initAvatarFace(id, index, 1);
			jsonData.instance.AvatarRandomJsonData[string.Concat(1)].SetField("Name", avatar.name);
			this.initChuanYingFu();
			avatar.seaNodeMag.INITSEA();
			NpcJieSuanManager.inst.NpcJieSuan(1, false);
			this.isCreateComplete = true;
		});
		SceneManager.LoadScene("LoadingScreen");
	}

	// Token: 0x06001B43 RID: 6979 RVA: 0x000F0924 File Offset: 0x000EEB24
	private void initChuanYingFu()
	{
		Avatar player = Tools.instance.getPlayer();
		player.ToalChuanYingFuList = new JSONObject();
		List<JSONObject> list = jsonData.instance.ChuanYingFuBiao.list;
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i]["Type"].I != 3)
			{
				player.ToalChuanYingFuList.SetField(list[i]["id"].I.ToString(), list[i]);
			}
		}
		JSONObject hasSendChuanYingFuList = player.HasSendChuanYingFuList;
		GlobalValue.Get(63, "CreateNewPlayerFactory.initChuanYingFu");
		for (int j = 0; j < hasSendChuanYingFuList.Count; j++)
		{
			try
			{
				player.ToalChuanYingFuList.RemoveField(hasSendChuanYingFuList[j]["id"].I.ToString());
			}
			catch (Exception)
			{
			}
		}
	}

	// Token: 0x06001B44 RID: 6980 RVA: 0x000F0A1C File Offset: 0x000EEC1C
	private void initAvatarFace(int id, int index, int startIndex = 1)
	{
		JSONObject avatarJsonData = jsonData.instance.AvatarJsonData;
		JSONObject avatarRandomJsonData = jsonData.instance.AvatarRandomJsonData;
		new JSONObject();
		foreach (JSONObject jsonobject in avatarJsonData.list)
		{
			if ((int)jsonobject["id"].n != 1 && (int)jsonobject["id"].n >= startIndex)
			{
				int i = jsonobject["id"].I;
				if (!jsonobject.HasField("isImportant") || !jsonobject["isImportant"].b)
				{
					JSONObject jsonobject2 = jsonData.instance.randomAvatarFace(jsonobject, avatarRandomJsonData.HasField(string.Concat((int)jsonobject["id"].n)) ? avatarRandomJsonData[((int)jsonobject["id"].n).ToString()] : null);
					avatarRandomJsonData.SetField(string.Concat((int)jsonobject["id"].n), jsonobject2.Clone());
				}
				else
				{
					avatarRandomJsonData.SetField(string.Concat((int)jsonobject["id"].n), avatarRandomJsonData[jsonobject["BindingNpcID"].I.ToString()]);
				}
			}
		}
		if (avatarRandomJsonData.HasField("1"))
		{
			avatarRandomJsonData.SetField("10000", avatarRandomJsonData["1"]);
		}
		this.randomAvatarBackpack();
		Tools.instance.getPlayer();
		foreach (JSONObject jsonobject3 in avatarJsonData.list)
		{
			int i2 = jsonobject3["id"].I;
			if (i2 >= 20000)
			{
				int i3 = jsonobject3["Type"].I;
				FactoryManager.inst.npcFactory.InitAutoCreateNpcBackpack(jsonData.instance.AvatarBackpackJsonData, i2, null);
			}
		}
	}

	// Token: 0x06001B45 RID: 6981 RVA: 0x000F0C78 File Offset: 0x000EEE78
	private void randomAvatarBackpack()
	{
		JSONObject jsonobject = new JSONObject();
		Avatar avatar = Tools.instance.getPlayer();
		List<JToken> list = Tools.FindAllJTokens(jsonData.instance.ResetAvatarBackpackBanBen, (JToken aa) => (int)aa["BanBenID"] > avatar.BanBenHao);
		foreach (JSONObject jsonobject2 in jsonData.instance.BackpackJsonData.list)
		{
			int avatarID = (int)jsonobject2["AvatrID"].n;
			if (list.Find((JToken aa) => (int)aa["avatar"] == avatarID) == null && jsonData.instance.AvatarBackpackJsonData != null && jsonData.instance.AvatarBackpackJsonData.HasField(string.Concat(avatarID)))
			{
				jsonobject.SetField(string.Concat(avatarID), jsonData.instance.AvatarBackpackJsonData[string.Concat(avatarID)]);
			}
			else
			{
				if (!jsonobject.HasField(string.Concat(avatarID)))
				{
					jsonData.instance.InitAvatarBackpack(ref jsonobject, avatarID);
				}
				jsonData.instance.AvatarAddBackpackByInfo(ref jsonobject, jsonobject2);
			}
		}
		jsonData.instance.AvatarBackpackJsonData = jsonobject;
	}

	// Token: 0x04001702 RID: 5890
	private Thread createAvatarThead;

	// Token: 0x04001703 RID: 5891
	public bool isCreateComplete;
}
