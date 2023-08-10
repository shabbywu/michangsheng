using JSONClass;
using KBEngine;
using UnityEngine;

public class JianLingManager
{
	private Avatar avatar;

	public JianLingManager(Avatar avatar)
	{
		this.avatar = avatar;
	}

	public int GetJiYiHuiFuDu()
	{
		int num = 0;
		foreach (string key in avatar.JianLingUnlockedXianSuo.keys)
		{
			if (avatar.JianLingUnlockedXianSuo[key].b)
			{
				if (JianLingXianSuo.DataDict.ContainsKey(key))
				{
					JianLingXianSuo jianLingXianSuo = JianLingXianSuo.DataDict[key];
					num += jianLingXianSuo.JiYi;
				}
				else
				{
					Debug.LogError((object)("计算剑灵记忆恢复度出现错误，id为" + key + "的线索不再配表中，已忽略此线索"));
				}
			}
		}
		return num + avatar.JianLingExJiYiHuiFuDu;
	}

	public void AddExJiYiHuiFuDu(int value)
	{
		avatar.JianLingExJiYiHuiFuDu += value;
	}

	public void UnlockXianSuo(string ID)
	{
		if (JianLingXianSuo.DataDict.ContainsKey(ID))
		{
			if (!avatar.JianLingUnlockedXianSuo.HasField(ID) || !avatar.JianLingUnlockedXianSuo[ID].b)
			{
				if (JianLingXianSuo.DataDict[ID].JiYi > 0)
				{
					UIPopTip.Inst.Pop("魏无极似乎想起了什么");
				}
				else
				{
					UIPopTip.Inst.Pop("获得一条新的线索");
				}
				avatar.JianLingUnlockedXianSuo.SetField(ID, val: true);
			}
		}
		else
		{
			Debug.LogError((object)("解锁剑灵线索出现错误，id为" + ID + "的线索不再配表中，已忽略此线索"));
		}
	}

	public bool IsXianSuoUnlocked(string ID)
	{
		if (!JianLingXianSuo.DataDict.ContainsKey(ID))
		{
			Debug.LogError((object)("检查是否解锁剑灵线索出现错误，id为" + ID + "的线索不再配表中，已返回否"));
		}
		if (avatar.JianLingUnlockedXianSuo.HasField(ID))
		{
			return avatar.JianLingUnlockedXianSuo[ID].b;
		}
		return false;
	}

	public void UnlockZhenXiang(string ID)
	{
		if (JianLingZhenXiang.DataDict.ContainsKey(ID))
		{
			avatar.JianLingUnlockedZhenXiang.SetField(ID, val: true);
			UIPopTip.Inst.Pop("【" + ID + "】真相已解锁");
		}
		else
		{
			Debug.LogError((object)("解锁剑灵真相出现错误，id为" + ID + "的线索不再配表中，已忽略此线索"));
		}
	}

	public bool IsZhenXiangUnlocked(string ID)
	{
		if (!JianLingZhenXiang.DataDict.ContainsKey(ID))
		{
			Debug.LogError((object)("检查是否解锁剑灵真相出现错误，id为" + ID + "的线索不再配表中，已返回否"));
		}
		if (avatar.JianLingUnlockedZhenXiang.HasField(ID))
		{
			return avatar.JianLingUnlockedZhenXiang[ID].b;
		}
		return false;
	}
}
