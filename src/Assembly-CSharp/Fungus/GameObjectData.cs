using System;
using UnityEngine;

namespace Fungus;

[Serializable]
public struct GameObjectData
{
	[SerializeField]
	[VariableProperty("<Value>", new Type[] { typeof(GameObjectVariable) })]
	public GameObjectVariable gameObjectRef;

	[SerializeField]
	public GameObject gameObjectVal;

	public GameObject Value
	{
		get
		{
			if (!((Object)(object)gameObjectRef == (Object)null))
			{
				return gameObjectRef.Value;
			}
			return gameObjectVal;
		}
		set
		{
			if ((Object)(object)gameObjectRef == (Object)null)
			{
				gameObjectVal = value;
			}
			else
			{
				gameObjectRef.Value = value;
			}
		}
	}

	public GameObjectData(GameObject v)
	{
		gameObjectVal = v;
		gameObjectRef = null;
	}

	public static implicit operator GameObject(GameObjectData gameObjectData)
	{
		return gameObjectData.Value;
	}

	public string GetDescription()
	{
		if ((Object)(object)gameObjectRef == (Object)null)
		{
			return ((object)gameObjectVal).ToString();
		}
		return gameObjectRef.Key;
	}
}
