using System.Collections.Generic;
using UnityEngine;

namespace YSGame;

public class BuffEffectprefab : MonoBehaviour
{
	[SerializeField]
	public List<skillEffctType> EffectsList = new List<skillEffctType>();

	public static BuffEffectprefab inst;

	private void Start()
	{
		inst = this;
		Object.DontDestroyOnLoad((Object)(object)((Component)this).gameObject);
	}

	public GameObject getBuffObj(string Name)
	{
		skillEffctType skillEffctType2 = default(skillEffctType);
		bool flag = false;
		foreach (skillEffctType effects in inst.EffectsList)
		{
			if (effects.name == Name)
			{
				flag = true;
				skillEffctType2 = effects;
				break;
			}
		}
		if (flag)
		{
			return skillEffctType2.obj;
		}
		return null;
	}
}
