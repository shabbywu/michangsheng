using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival;

[Serializable]
public class Pool
{
	[SerializeField]
	private string m_PoolName;

	[SerializeField]
	private GameObject m_Prefab;

	[SerializeField]
	private int m_Amount;

	private List<GameObject> m_PooledObjects = new List<GameObject>();

	public string PoolName => m_PoolName;

	public GameObject Prefab => m_Prefab;

	public int Amount => m_Amount;

	public List<GameObject> PooledObjects => m_PooledObjects;

	private GameObject FindFirstActiveObject()
	{
		GameObject val = null;
		for (int i = 0; i < m_PooledObjects.Count; i++)
		{
			if (m_PooledObjects[i].activeInHierarchy)
			{
				val = m_PooledObjects[i];
			}
		}
		if ((Object)(object)val == (Object)null)
		{
			Debug.LogError((object)("There are no available active objects to get from the " + m_PoolName + " pool"));
		}
		return val;
	}

	private GameObject FindFirstInactiveObject()
	{
		GameObject val = null;
		for (int i = 0; i < m_PooledObjects.Count; i++)
		{
			if (!m_PooledObjects[i].activeInHierarchy)
			{
				val = m_PooledObjects[i];
			}
		}
		if ((Object)(object)val == (Object)null)
		{
			Debug.LogError((object)("There are no available inactive objects to get from the " + m_PoolName + " pool"));
		}
		return val;
	}

	public void Spawn(Vector3 position, Quaternion rotation)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		GameObject obj = FindFirstInactiveObject();
		Transform transform = obj.transform;
		transform.position = position;
		transform.rotation = rotation;
		obj.SetActive(true);
		obj.GetComponent<PoolableObject>().OnSpawn();
	}

	public void SpawnAll(Vector3[] positions, Quaternion[] rotations)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < m_PooledObjects.Count; i++)
		{
			Spawn(positions[i], rotations[i]);
		}
	}

	public void DespawnSpecificObject(GameObject toDespawn)
	{
		if (m_PooledObjects.Contains(toDespawn))
		{
			toDespawn.SetActive(false);
		}
		else
		{
			Debug.LogError((object)("Object that entered the trigger is not part of the " + m_PoolName + " pool"));
		}
	}

	public void Despawn()
	{
		GameObject obj = FindFirstActiveObject();
		obj.GetComponent<PoolableObject>().OnDespawn();
		obj.SetActive(false);
	}

	public void DestroyObjects(bool activeOnes)
	{
		for (int i = 0; i < m_PooledObjects.Count; i++)
		{
			if (m_PooledObjects[i].activeInHierarchy == activeOnes)
			{
				m_PooledObjects[i].GetComponent<PoolableObject>().OnPoolableDestroy();
				Object.Destroy((Object)(object)m_PooledObjects[i]);
			}
		}
	}

	public void DestroyAll()
	{
		for (int i = 0; i < m_PooledObjects.Count; i++)
		{
			m_PooledObjects[i].GetComponent<PoolableObject>().OnPoolableDestroy();
			Object.Destroy((Object)(object)m_PooledObjects[i]);
		}
	}

	public void DespawnAll()
	{
		for (int i = 0; i < m_PooledObjects.Count; i++)
		{
			m_PooledObjects[i].GetComponent<PoolableObject>().OnDespawn();
			m_PooledObjects[i].SetActive(false);
		}
	}
}
