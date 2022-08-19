using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003B0 RID: 944
public static class GameObjectPool
{
	// Token: 0x06001EAC RID: 7852 RVA: 0x000D77FC File Offset: 0x000D59FC
	public static GameObject Get(GameObject prefab)
	{
		GameObjectPool.InitPool(prefab);
		GameObject gameObject = null;
		while (GameObjectPool.RecoveryStack[prefab.name].Count > 0)
		{
			gameObject = GameObjectPool.RecoveryStack[prefab.name].Pop();
			if (gameObject != null)
			{
				break;
			}
		}
		if (gameObject == null)
		{
			gameObject = Object.Instantiate<GameObject>(prefab, GameObjectPool.ObjectRootDict[prefab.name].transform);
			gameObject.name = prefab.name;
		}
		gameObject.SetActive(true);
		GameObjectPool.RefreshPoolName(prefab.name);
		return gameObject;
	}

	// Token: 0x06001EAD RID: 7853 RVA: 0x000D7890 File Offset: 0x000D5A90
	public static void Recovery(GameObject obj)
	{
		if (!GameObjectPool.RecoveryStack.ContainsKey(obj.name))
		{
			GameObjectPool.RecoveryStack.Add(obj.name, new Stack<GameObject>());
		}
		obj.transform.position = GameObjectPool.HidePos;
		obj.SetActive(false);
		GameObjectPool.RecoveryStack[obj.name].Push(obj);
		GameObjectPool.RefreshPoolName(obj.name);
	}

	// Token: 0x06001EAE RID: 7854 RVA: 0x000D78FC File Offset: 0x000D5AFC
	private static void InitPool(GameObject prefab)
	{
		if (!GameObjectPool.RecoveryStack.ContainsKey(prefab.name))
		{
			GameObjectPool.RecoveryStack.Add(prefab.name, new Stack<GameObject>());
		}
		if (!GameObjectPool.ObjectRootDict.ContainsKey(prefab.name))
		{
			GameObject gameObject = new GameObject();
			gameObject.transform.position = GameObjectPool.HidePos;
			GameObjectPool.ObjectRootDict.Add(prefab.name, gameObject);
		}
		if (GameObjectPool.ObjectRootDict[prefab.name] == null)
		{
			GameObject gameObject2 = new GameObject();
			gameObject2.transform.position = GameObjectPool.HidePos;
			GameObjectPool.ObjectRootDict[prefab.name] = gameObject2;
		}
	}

	// Token: 0x06001EAF RID: 7855 RVA: 0x000D79A8 File Offset: 0x000D5BA8
	private static void RefreshPoolName(string name)
	{
		if (GameObjectPool.ObjectRootDict.ContainsKey(name) && GameObjectPool.ObjectRootDict[name] != null)
		{
			GameObjectPool.ObjectRootDict[name].name = string.Format("Pool_{0} ({1})", name, GameObjectPool.ObjectRootDict[name].transform.childCount);
		}
	}

	// Token: 0x04001923 RID: 6435
	private static Vector3 HidePos = new Vector3(10000f, 10000f, 10000f);

	// Token: 0x04001924 RID: 6436
	private static Dictionary<string, GameObject> ObjectRootDict = new Dictionary<string, GameObject>();

	// Token: 0x04001925 RID: 6437
	private static Dictionary<string, Stack<GameObject>> RecoveryStack = new Dictionary<string, Stack<GameObject>>();
}
