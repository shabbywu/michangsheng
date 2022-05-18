using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008AF RID: 2223
	[Serializable]
	public class Pool
	{
		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x0600393E RID: 14654 RVA: 0x00029884 File Offset: 0x00027A84
		public string PoolName
		{
			get
			{
				return this.m_PoolName;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x0600393F RID: 14655 RVA: 0x0002988C File Offset: 0x00027A8C
		public GameObject Prefab
		{
			get
			{
				return this.m_Prefab;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06003940 RID: 14656 RVA: 0x00029894 File Offset: 0x00027A94
		public int Amount
		{
			get
			{
				return this.m_Amount;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06003941 RID: 14657 RVA: 0x0002989C File Offset: 0x00027A9C
		public List<GameObject> PooledObjects
		{
			get
			{
				return this.m_PooledObjects;
			}
		}

		// Token: 0x06003942 RID: 14658 RVA: 0x001A4F18 File Offset: 0x001A3118
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

		// Token: 0x06003943 RID: 14659 RVA: 0x001A4F84 File Offset: 0x001A3184
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

		// Token: 0x06003944 RID: 14660 RVA: 0x000298A4 File Offset: 0x00027AA4
		public void Spawn(Vector3 position, Quaternion rotation)
		{
			GameObject gameObject = this.FindFirstInactiveObject();
			Transform transform = gameObject.transform;
			transform.position = position;
			transform.rotation = rotation;
			gameObject.SetActive(true);
			gameObject.GetComponent<PoolableObject>().OnSpawn();
		}

		// Token: 0x06003945 RID: 14661 RVA: 0x001A4FF0 File Offset: 0x001A31F0
		public void SpawnAll(Vector3[] positions, Quaternion[] rotations)
		{
			for (int i = 0; i < this.m_PooledObjects.Count; i++)
			{
				this.Spawn(positions[i], rotations[i]);
			}
		}

		// Token: 0x06003946 RID: 14662 RVA: 0x000298D0 File Offset: 0x00027AD0
		public void DespawnSpecificObject(GameObject toDespawn)
		{
			if (this.m_PooledObjects.Contains(toDespawn))
			{
				toDespawn.SetActive(false);
				return;
			}
			Debug.LogError("Object that entered the trigger is not part of the " + this.m_PoolName + " pool");
		}

		// Token: 0x06003947 RID: 14663 RVA: 0x00029902 File Offset: 0x00027B02
		public void Despawn()
		{
			GameObject gameObject = this.FindFirstActiveObject();
			gameObject.GetComponent<PoolableObject>().OnDespawn();
			gameObject.SetActive(false);
		}

		// Token: 0x06003948 RID: 14664 RVA: 0x001A5028 File Offset: 0x001A3228
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

		// Token: 0x06003949 RID: 14665 RVA: 0x001A5088 File Offset: 0x001A3288
		public void DestroyAll()
		{
			for (int i = 0; i < this.m_PooledObjects.Count; i++)
			{
				this.m_PooledObjects[i].GetComponent<PoolableObject>().OnPoolableDestroy();
				Object.Destroy(this.m_PooledObjects[i]);
			}
		}

		// Token: 0x0600394A RID: 14666 RVA: 0x001A50D4 File Offset: 0x001A32D4
		public void DespawnAll()
		{
			for (int i = 0; i < this.m_PooledObjects.Count; i++)
			{
				this.m_PooledObjects[i].GetComponent<PoolableObject>().OnDespawn();
				this.m_PooledObjects[i].SetActive(false);
			}
		}

		// Token: 0x04003365 RID: 13157
		[SerializeField]
		private string m_PoolName;

		// Token: 0x04003366 RID: 13158
		[SerializeField]
		private GameObject m_Prefab;

		// Token: 0x04003367 RID: 13159
		[SerializeField]
		private int m_Amount;

		// Token: 0x04003368 RID: 13160
		private List<GameObject> m_PooledObjects = new List<GameObject>();
	}
}
