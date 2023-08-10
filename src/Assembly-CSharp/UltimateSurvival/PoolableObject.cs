using UnityEngine;

namespace UltimateSurvival;

public class PoolableObject : MonoBehaviour
{
	public virtual void OnSpawn()
	{
	}

	public virtual void OnDespawn()
	{
	}

	public virtual void OnPoolableDestroy()
	{
	}
}
