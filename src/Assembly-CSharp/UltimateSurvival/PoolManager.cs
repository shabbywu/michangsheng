using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005E3 RID: 1507
	public class PoolManager : MonoBehaviour
	{
		// Token: 0x06003084 RID: 12420 RVA: 0x0015B73A File Offset: 0x0015993A
		private void Awake()
		{
			this.InitializePools();
		}

		// Token: 0x06003085 RID: 12421 RVA: 0x0015B744 File Offset: 0x00159944
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

		// Token: 0x06003086 RID: 12422 RVA: 0x0015B838 File Offset: 0x00159A38
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

		// Token: 0x04002AAB RID: 10923
		[SerializeField]
		private List<Pool> _pools = new List<Pool>();
	}
}
