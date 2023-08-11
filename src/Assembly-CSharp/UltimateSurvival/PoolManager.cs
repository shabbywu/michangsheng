using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival;

public class PoolManager : MonoBehaviour
{
	[SerializeField]
	private List<Pool> _pools = new List<Pool>();

	private void Awake()
	{
		InitializePools();
	}

	private void InitializePools()
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < _pools.Count; i++)
		{
			Pool pool = _pools[i];
			if (pool.PoolName == string.Empty)
			{
				Debug.LogError((object)("Pool with index of " + i + " does not contain a pool name."));
				break;
			}
			Transform transform = new GameObject(pool.PoolName + " Pool").transform;
			transform.SetParent(((Component)this).transform);
			for (int j = 0; j < pool.Amount; j++)
			{
				if (!Object.op_Implicit((Object)(object)pool.Prefab))
				{
					Debug.LogError((object)("There is no prefab assigned for the " + pool.PoolName + " pool"));
					return;
				}
				GameObject val = Object.Instantiate<GameObject>(pool.Prefab);
				val.transform.SetParent(transform);
				val.SetActive(false);
				pool.PooledObjects.Add(val);
			}
		}
	}

	public Pool GetPool(string name)
	{
		Pool pool = null;
		for (int i = 0; i < _pools.Count; i++)
		{
			if (_pools[i].PoolName == name)
			{
				pool = _pools[i];
			}
		}
		if (pool == null)
		{
			Debug.LogError((object)("Couldn't find pool with name: " + name));
		}
		return pool;
	}
}
