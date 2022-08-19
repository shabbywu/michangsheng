using System;
using System.Collections.Generic;
using System.Threading;
using KBEngine;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020002B1 RID: 689
public class CreateNewPlayerFactory
{
	// Token: 0x06001850 RID: 6224 RVA: 0x000A9A54 File Offset: 0x000A7C54
	public void createPlayer(int id, int index, string firstName, string lastName, Avatar avatar)
	{
		this.isCreateComplete = false;
		Tools.instance.NextSaveTime = DateTime.Now;
		YSNewSaveSystem.Save("SaveAvatar.txt", 1, true);
		PlayerPrefs.SetInt("NowPlayerFileAvatar", id);
		avatar.name = firstName + lastName;
		avatar.firstName = firstName;
		avatar.lastName = lastName;
		YSNewSaveSystem.Save("FirstSetAvatarRandomJsonData.txt", 1, true);
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

	// Token: 0x06001851 RID: 6225 RVA: 0x000A9B0C File Offset: 0x000A7D0C
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

	// Token: 0x06001852 RID: 6226 RVA: 0x000A9C04 File Offset: 0x000A7E04
	private void initAvatarFace(int id, int index, int startIndex = 1)
	{
		JSONObject avatarJsonData = jsonData.instance.AvatarJsonData;
		JSONObject avatarRandomJsonData = jsonData.instance.AvatarRandomJsonData;
		new JSONObject();
		foreach (JSONObject jsonobject in avatarJsonData.list)
		{
			if (jsonobject["id"].I != 1 && jsonobject["id"].I >= startIndex)
			{
				int i = jsonobject["id"].I;
				if (!jsonobject.HasField("isImportant") || !jsonobject["isImportant"].b)
				{
					JSONObject jsonobject2 = jsonData.instance.randomAvatarFace(jsonobject, avatarRandomJsonData.HasField(string.Concat(jsonobject["id"].I)) ? avatarRandomJsonData[jsonobject["id"].I.ToString()] : null);
					avatarRandomJsonData.SetField(string.Concat(jsonobject["id"].I), jsonobject2.Copy());
				}
				else
				{
					avatarRandomJsonData.SetField(string.Concat(jsonobject["id"].I), avatarRandomJsonData[jsonobject["BindingNpcID"].I.ToString()]);
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

	// Token: 0x06001853 RID: 6227 RVA: 0x000A9E5C File Offset: 0x000A805C
	private void randomAvatarBackpack()
	{
		JSONObject jsonobject = new JSONObject();
		Avatar avatar = Tools.instance.getPlayer();
		List<JToken> list = Tools.FindAllJTokens(jsonData.instance.ResetAvatarBackpackBanBen, (JToken aa) => (int)aa["BanBenID"] > avatar.BanBenHao);
		foreach (JSONObject jsonobject2 in jsonData.instance.BackpackJsonData.list)
		{
			int avatarID = jsonobject2["AvatrID"].I;
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

	// Token: 0x04001365 RID: 4965
	private Thread createAvatarThead;

	// Token: 0x04001366 RID: 4966
	public bool isCreateComplete;
}
