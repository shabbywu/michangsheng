using System;
using KBEngine;
using UnityEngine;

// Token: 0x020002BC RID: 700
public static class GlobalValue
{
	// Token: 0x06001525 RID: 5413 RVA: 0x000BE390 File Offset: 0x000BC590
	public static int Get(int id, string source = "unknow")
	{
		Avatar player = PlayerEx.Player;
		if (player == null)
		{
			Debug.LogError("不能获取全局变量:无法获取到玩家。源:" + source);
			return -1;
		}
		if (id < 0 || id >= 2500)
		{
			Debug.LogError(string.Format("不能获取全局变量:id {0}超出了边界。源:{1}", id, source));
		}
		int num = player.StaticValue.Value[id];
		if (GlobalValue.LogSource)
		{
			Debug.Log(string.Format("获取全局变量, id:{0} 值:{1}。源:{2}", id, num, source));
		}
		return num;
	}

	// Token: 0x06001526 RID: 5414 RVA: 0x000BE40C File Offset: 0x000BC60C
	public static void Set(int id, int value, string source = "unknow")
	{
		Avatar player = PlayerEx.Player;
		if (player == null)
		{
			Debug.LogError("不能设置全局变量:无法获取到玩家。源:" + source);
			return;
		}
		if (id < 0 || id >= 2500)
		{
			Debug.LogError(string.Format("不能设置全局变量:id {0}超出了边界。源:{1}", id, source));
		}
		if (GlobalValue.LogSource)
		{
			Debug.Log(string.Format("设置全局变量, id:{0} 原始值:{1} 新值:{2}。源:{3}", new object[]
			{
				id,
				player.StaticValue.Value[id],
				value,
				source
			}));
		}
		player.StaticValue.Value[id] = value;
	}

	// Token: 0x06001527 RID: 5415 RVA: 0x000BE4AC File Offset: 0x000BC6AC
	public static int GetTalk(int id, string source = "unknow")
	{
		Avatar player = PlayerEx.Player;
		if (player == null)
		{
			Debug.LogError("不能获取talk:无法获取到玩家。源:" + source);
			return -1;
		}
		if (id < 0 || id >= player.StaticValue.talk.Length)
		{
			Debug.LogError(string.Format("不能获取talk:id {0}超出了边界。源:{1}", id, source));
		}
		int num = player.StaticValue.talk[id];
		if (GlobalValue.LogSource)
		{
			Debug.Log(string.Format("获取talk, id:{0} 值:{1}。源:{2}", id, num, source));
		}
		return num;
	}

	// Token: 0x06001528 RID: 5416 RVA: 0x000BE530 File Offset: 0x000BC730
	public static void SetTalk(int id, int value, string source = "unknow")
	{
		Avatar player = PlayerEx.Player;
		if (player == null)
		{
			Debug.LogError("不能设置talk:无法获取到玩家。源:" + source);
			return;
		}
		if (id < 0 || id >= player.StaticValue.talk.Length)
		{
			Debug.LogError(string.Format("不能设置talk:id {0}超出了边界。源:{1}", id, source));
		}
		if (GlobalValue.LogSource)
		{
			Debug.Log(string.Format("设置talk, id:{0} 原始值:{1} 新值:{2}。源:{3}", new object[]
			{
				id,
				player.StaticValue.talk[id],
				value,
				source
			}));
		}
		player.StaticValue.talk[id] = value;
	}

	// Token: 0x04001033 RID: 4147
	public static bool LogSource;
}
