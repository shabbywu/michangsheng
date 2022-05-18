using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200053B RID: 1339
public static class GameObjectPool
{
	// Token: 0x0600222F RID: 8751 RVA: 0x0011ACE4 File Offset: 0x00118EE4
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

	// Token: 0x06002230 RID: 8752 RVA: 0x0011AD78 File Offset: 0x00118F78
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

	// Token: 0x06002231 RID: 8753 RVA: 0x0011ADE4 File Offset: 0x00118FE4
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

	// Token: 0x06002232 RID: 8754 RVA: 0x0011AE90 File Offset: 0x00119090
	private static void RefreshPoolName(string name)
	{
		if (GameObjectPool.ObjectRootDict.ContainsKey(name) && GameObjectPool.ObjectRootDict[name] != null)
		{
			GameObjectPool.ObjectRootDict[name].name = string.Format("Pool_{0} ({1})", name, GameObjectPool.ObjectRootDict[name].transform.childCount);
		}
	}

	// Token: 0x04001D97 RID: 7575
	private static Vector3 HidePos = new Vector3(10000f, 10000f, 10000f);

	// Token: 0x04001D98 RID: 7576
	private static Dictionary<string, GameObject> ObjectRootDict = new Dictionary<string, GameObject>();

	// Token: 0x04001D99 RID: 7577
	private static Dictionary<string, Stack<GameObject>> RecoveryStack = new Dictionary<string, Stack<GameObject>>();
}
