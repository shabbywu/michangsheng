using System.Collections.Generic;
using UnityEngine;

public static class GameObjectPool
{
	private static Vector3 HidePos = new Vector3(10000f, 10000f, 10000f);

	private static Dictionary<string, GameObject> ObjectRootDict = new Dictionary<string, GameObject>();

	private static Dictionary<string, Stack<GameObject>> RecoveryStack = new Dictionary<string, Stack<GameObject>>();

	public static GameObject Get(GameObject prefab)
	{
		InitPool(prefab);
		GameObject val = null;
		while (RecoveryStack[((Object)prefab).name].Count > 0)
		{
			val = RecoveryStack[((Object)prefab).name].Pop();
			if ((Object)(object)val != (Object)null)
			{
				break;
			}
		}
		if ((Object)(object)val == (Object)null)
		{
			val = Object.Instantiate<GameObject>(prefab, ObjectRootDict[((Object)prefab).name].transform);
			((Object)val).name = ((Object)prefab).name;
		}
		val.SetActive(true);
		RefreshPoolName(((Object)prefab).name);
		return val;
	}

	public static void Recovery(GameObject obj)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		if (!RecoveryStack.ContainsKey(((Object)obj).name))
		{
			RecoveryStack.Add(((Object)obj).name, new Stack<GameObject>());
		}
		obj.transform.position = HidePos;
		obj.SetActive(false);
		RecoveryStack[((Object)obj).name].Push(obj);
		RefreshPoolName(((Object)obj).name);
	}

	private static void InitPool(GameObject prefab)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Expected O, but got Unknown
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		if (!RecoveryStack.ContainsKey(((Object)prefab).name))
		{
			RecoveryStack.Add(((Object)prefab).name, new Stack<GameObject>());
		}
		if (!ObjectRootDict.ContainsKey(((Object)prefab).name))
		{
			GameObject val = new GameObject();
			val.transform.position = HidePos;
			ObjectRootDict.Add(((Object)prefab).name, val);
		}
		if ((Object)(object)ObjectRootDict[((Object)prefab).name] == (Object)null)
		{
			GameObject val2 = new GameObject();
			val2.transform.position = HidePos;
			ObjectRootDict[((Object)prefab).name] = val2;
		}
	}

	private static void RefreshPoolName(string name)
	{
		if (ObjectRootDict.ContainsKey(name) && (Object)(object)ObjectRootDict[name] != (Object)null)
		{
			((Object)ObjectRootDict[name]).name = $"Pool_{name} ({ObjectRootDict[name].transform.childCount})";
		}
	}
}
