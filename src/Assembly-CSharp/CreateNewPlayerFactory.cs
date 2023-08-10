using System;
using System.Collections.Generic;
using System.Threading;
using KBEngine;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateNewPlayerFactory
{
	private Thread createAvatarThead;

	public bool isCreateComplete;

	public void createPlayer(int id, int index, string firstName, string lastName, Avatar avatar)
	{
		isCreateComplete = false;
		Tools.instance.NextSaveTime = DateTime.Now;
		YSNewSaveSystem.Save("SaveAvatar.txt", 1);
		PlayerPrefs.SetInt("NowPlayerFileAvatar", id);
		avatar.name = firstName + lastName;
		avatar.firstName = firstName;
		avatar.lastName = lastName;
		YSNewSaveSystem.Save("FirstSetAvatarRandomJsonData.txt", 1);
		Loom.RunAsync(delegate
		{
			Tools.instance.IsInDF = false;
			jsonData.instance.AvatarBackpackJsonData = null;
			FactoryManager.inst.npcFactory.firstCreateNpcs();
			initAvatarFace(id, index);
			jsonData.instance.AvatarRandomJsonData[string.Concat(1)].SetField("Name", avatar.name);
			initChuanYingFu();
			avatar.seaNodeMag.INITSEA();
			NpcJieSuanManager.inst.NpcJieSuan(1, isCanChanger: false);
			isCreateComplete = true;
		});
		SceneManager.LoadScene("LoadingScreen");
	}

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

	private void initAvatarFace(int id, int index, int startIndex = 1)
	{
		JSONObject avatarJsonData = jsonData.instance.AvatarJsonData;
		JSONObject avatarRandomJsonData = jsonData.instance.AvatarRandomJsonData;
		new JSONObject();
		foreach (JSONObject item in avatarJsonData.list)
		{
			if (item["id"].I != 1 && item["id"].I >= startIndex)
			{
				_ = item["id"].I;
				if (!item.HasField("isImportant") || !item["isImportant"].b)
				{
					JSONObject jSONObject = jsonData.instance.randomAvatarFace(item, avatarRandomJsonData.HasField(string.Concat(item["id"].I)) ? avatarRandomJsonData[item["id"].I.ToString()] : null);
					avatarRandomJsonData.SetField(string.Concat(item["id"].I), jSONObject.Copy());
				}
				else
				{
					avatarRandomJsonData.SetField(string.Concat(item["id"].I), avatarRandomJsonData[item["BindingNpcID"].I.ToString()]);
				}
			}
		}
		if (avatarRandomJsonData.HasField("1"))
		{
			avatarRandomJsonData.SetField("10000", avatarRandomJsonData["1"]);
		}
		randomAvatarBackpack();
		Tools.instance.getPlayer();
		foreach (JSONObject item2 in avatarJsonData.list)
		{
			int i = item2["id"].I;
			if (i >= 20000)
			{
				_ = item2["Type"].I;
				FactoryManager.inst.npcFactory.InitAutoCreateNpcBackpack(jsonData.instance.AvatarBackpackJsonData, i);
			}
		}
	}

	private void randomAvatarBackpack()
	{
		JSONObject jsondata = new JSONObject();
		Avatar avatar = Tools.instance.getPlayer();
		List<JToken> list = Tools.FindAllJTokens((JToken)(object)jsonData.instance.ResetAvatarBackpackBanBen, (JToken aa) => (int)aa[(object)"BanBenID"] > avatar.BanBenHao);
		foreach (JSONObject item in jsonData.instance.BackpackJsonData.list)
		{
			int avatarID = item["AvatrID"].I;
			if (list.Find((JToken aa) => (int)aa[(object)"avatar"] == avatarID) == null && jsonData.instance.AvatarBackpackJsonData != null && jsonData.instance.AvatarBackpackJsonData.HasField(string.Concat(avatarID)))
			{
				jsondata.SetField(string.Concat(avatarID), jsonData.instance.AvatarBackpackJsonData[string.Concat(avatarID)]);
				continue;
			}
			if (!jsondata.HasField(string.Concat(avatarID)))
			{
				jsonData.instance.InitAvatarBackpack(ref jsondata, avatarID);
			}
			jsonData.instance.AvatarAddBackpackByInfo(ref jsondata, item);
		}
		jsonData.instance.AvatarBackpackJsonData = jsondata;
	}
}
