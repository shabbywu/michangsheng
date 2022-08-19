using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005E1 RID: 1505
	[Serializable]
	public class Pool
	{
		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06003072 RID: 12402 RVA: 0x0015B488 File Offset: 0x00159688
		public string PoolName
		{
			get
			{
				return this.m_PoolName;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06003073 RID: 12403 RVA: 0x0015B490 File Offset: 0x00159690
		public GameObject Prefab
		{
			get
			{
				return this.m_Prefab;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06003074 RID: 12404 RVA: 0x0015B498 File Offset: 0x00159698
		public int Amount
		{
			get
			{
				return this.m_Amount;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06003075 RID: 12405 RVA: 0x0015B4A0 File Offset: 0x001596A0
		public List<GameObject> PooledObjects
		{
			get
			{
				return this.m_PooledObjects;
			}
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x0015B4A8 File Offset: 0x001596A8
		private GameObject FindFirstActiveObject()
		{
			GameObject gameObject = null;
			for (int i = 0; i < this.m_PooledObjects.Count; i++)
			{
				if (this.m_PooledObjects[i].activeInHierarchy)
				{
					gameObject = this.m_PooledObjects[i];
				}
			}
			if (gameObject == null)
			{
				Debug.LogError("There are no available active objects to get from the " + this.m_PoolName + " pool");
			}
			return gameObject;
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x0015B514 File Offset: 0x00159714
		private GameObject FindFirstInactiveObject()
		{
			GameObject gameObject = null;
			for (int i = 0; i < this.m_PooledObjects.Count; i++)
			{
				if (!this.m_PooledObjects[i].activeInHierarchy)
				{
					gameObject = this.m_PooledObjects[i];
				}
			}
			if (gameObject == null)
			{
				Debug.LogError("There are no available inactive objects to get from the " + this.m_PoolName + " pool");
			}
			return gameObject;
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x0015B57D File Offset: 0x0015977D
		public void Spawn(Vector3 position, Quaternion rotation)
		{
			GameObject gameObject = this.FindFirstInactiveObject();
			Transform transform = gameObject.transform;
			transform.position = position;
			transform.rotation = rotation;
			gameObject.SetActive(true);
			gameObject.GetComponent<PoolableObject>().OnSpawn();
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x0015B5AC File Offset: 0x001597AC
		public void SpawnAll(Vector3[] positions, Quaternion[] rotations)
		{
			for (int i = 0; i < this.m_PooledObjects.Count; i++)
			{
				this.Spawn(positions[i], rotations[i]);
			}
		}

		// Token: 0x0600307A RID: 12410 RVA: 0x0015B5E3 File Offset: 0x001597E3
		public void DespawnSpecificObject(GameObject toDespawn)
		{
			if (this.m_PooledObjects.Contains(toDespawn))
			{
				toDespawn.SetActive(false);
				return;
			}
			Debug.LogError("Object that entered the trigger is not part of the " + this.m_PoolName + " pool");
		}

		// Token: 0x0600307B RID: 12411 RVA: 0x0015B615 File Offset: 0x00159815
		public void Despawn()
		{
			GameObject gameObject = this.FindFirstActiveObject();
			gameObject.GetComponent<PoolableObject>().OnDespawn();
			gameObject.SetActive(false);
		}

		// Token: 0x0600307C RID: 12412 RVA: 0x0015B630 File Offset: 0x00159830
		public void DestroyObjects(bool activeOnes)
		{
			for (int i = 0; i < this.m_PooledObjects.Count; i++)
			{
				if (this.m_PooledObjects[i].activeInHierarchy == activeOnes)
				{
					this.m_PooledObjects[i].GetComponent<PoolableObject>().OnPoolableDestroy();
					Object.Destroy(this.m_PooledObjects[i]);
				}
			}
		}

		// Token: 0x0600307D RID: 12413 RVA: 0x0015B690 File Offset: 0x00159890
		public void DestroyAll()
		{
			for (int i = 0; i < this.m_PooledObjects.Count; i++)
			{
				this.m_PooledObjects[i].GetComponent<PoolableObject>().OnPoolableDestroy();
				Object.Destroy(this.m_PooledObjects[i]);
			}
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x0015B6DC File Offset: 0x001598DC
		public void DespawnAll()
		{
			for (int i = 0; i < this.m_PooledObjects.Count; i++)
			{
				this.m_PooledObjects[i].GetComponent<PoolableObject>().OnDespawn();
				this.m_PooledObjects[i].SetActive(false);
			}
		}

		// Token: 0x04002AA7 RID: 10919
		[SerializeField]
		private string m_PoolName;

		// Token: 0x04002AA8 RID: 10920
		[SerializeField]
		private GameObject m_Prefab;

		// Token: 0x04002AA9 RID: 10921
		[SerializeField]
		private int m_Amount;

		// Token: 0x04002AAA RID: 10922
		private List<GameObject> m_PooledObjects = new List<GameObject>();
	}
}
