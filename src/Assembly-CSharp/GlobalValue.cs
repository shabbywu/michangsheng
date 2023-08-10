using KBEngine;
using UnityEngine;

public static class GlobalValue
{
	public static bool LogSource;

	public static int Get(int id, string source = "unknow")
	{
		Avatar player = PlayerEx.Player;
		if (player == null)
		{
			Debug.LogError((object)("不能获取全局变量:无法获取到玩家。源:" + source));
			return -1;
		}
		if (id < 0 || id >= 2500)
		{
			Debug.LogError((object)$"不能获取全局变量:id {id}超出了边界。源:{source}");
		}
		int num = player.StaticValue.Value[id];
		if (LogSource)
		{
			Debug.Log((object)$"获取全局变量, id:{id} 值:{num}。源:{source}");
		}
		return num;
	}

	public static void Set(int id, int value, string source = "unknow")
	{
		Avatar player = PlayerEx.Player;
		if (player == null)
		{
			Debug.LogError((object)("不能设置全局变量:无法获取到玩家。源:" + source));
			return;
		}
		if (id < 0 || id >= 2500)
		{
			Debug.LogError((object)$"不能设置全局变量:id {id}超出了边界。源:{source}");
		}
		if (LogSource)
		{
			Debug.Log((object)$"设置全局变量, id:{id} 原始值:{player.StaticValue.Value[id]} 新值:{value}。源:{source}");
		}
		player.StaticValue.Value[id] = value;
	}

	public static int GetTalk(int id, string source = "unknow")
	{
		Avatar player = PlayerEx.Player;
		if (player == null)
		{
			Debug.LogError((object)("不能获取talk:无法获取到玩家。源:" + source));
			return -1;
		}
		if (id < 0 || id >= player.StaticValue.talk.Length)
		{
			Debug.LogError((object)$"不能获取talk:id {id}超出了边界。源:{source}");
		}
		int num = player.StaticValue.talk[id];
		if (LogSource)
		{
			Debug.Log((object)$"获取talk, id:{id} 值:{num}。源:{source}");
		}
		return num;
	}

	public static void SetTalk(int id, int value, string source = "unknow")
	{
		Avatar player = PlayerEx.Player;
		if (player == null)
		{
			Debug.LogError((object)("不能设置talk:无法获取到玩家。源:" + source));
			return;
		}
		if (id < 0 || id >= player.StaticValue.talk.Length)
		{
			Debug.LogError((object)$"不能设置talk:id {id}超出了边界。源:{source}");
		}
		if (LogSource)
		{
			Debug.Log((object)$"设置talk, id:{id} 原始值:{player.StaticValue.talk[id]} 新值:{value}。源:{source}");
		}
		player.StaticValue.talk[id] = value;
	}
}
