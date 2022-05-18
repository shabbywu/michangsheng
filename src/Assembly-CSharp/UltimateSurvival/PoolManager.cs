using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008B1 RID: 2225
	public class PoolManager : MonoBehaviour
	{
		// Token: 0x06003950 RID: 14672 RVA: 0x0002992E File Offset: 0x00027B2E
		private void Awake()
		{
			this.InitializePools();
		}

		// Token: 0x06003951 RID: 14673 RVA: 0x001A5120 File Offset: 0x001A3320
		private void InitializePools()
		{
			for (int i = 0; i < this._pools.Count; i++)
			{
				Pool pool = this._pools[i];
				if (pool.PoolName == string.Empty)
				{
					Debug.LogError("Pool with index of " + i + " does not contain a pool name.");
					return;
				}
				Transform transform = new GameObject(pool.PoolName + " Pool").transform;
				transform.SetParent(base.transform);
				for (int j = 0; j < pool.Amount; j++)
				{
					if (!pool.Prefab)
					{
						Debug.LogError("There is no prefab assigned for the " + pool.PoolName + " pool");
						return;
					}
					GameObject gameObject = Object.Instantiate<GameObject>(pool.Prefab);
					gameObject.transform.SetParent(transform);
					gameObject.SetActive(false);
					pool.PooledObjects.Add(gameObject);
				}
			}
		}

		// Token: 0x06003952 RID: 14674 RVA: 0x001A5214 File Offset: 0x001A3414
		public Pool GetPool(string name)
		{
			Pool pool = null;
			for (int i = 0; i < this._pools.Count; i++)
			{
				if (this._pools[i].PoolName == name)
				{
					pool = this._pools[i];
				}
			}
			if (pool == null)
			{
				Debug.LogError("Couldn't find pool with name: " + name);
			}
			return pool;
		}

		// Token: 0x04003369 RID: 13161
		[SerializeField]
		private List<Pool> _pools = new List<Pool>();
	}
}
