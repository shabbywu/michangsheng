using System;
using System.Collections.Generic;
using UnityEngine;

namespace YSGame
{
	// Token: 0x02000DA2 RID: 3490
	public class BuffEffectprefab : MonoBehaviour
	{
		// Token: 0x06005447 RID: 21575 RVA: 0x0003C57C File Offset: 0x0003A77C
		private void Start()
		{
			BuffEffectprefab.inst = this;
			Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x06005448 RID: 21576 RVA: 0x002314E0 File Offset: 0x0022F6E0
		public GameObject getBuffObj(string Name)
		{
			skillEffctType skillEffctType = default(skillEffctType);
			bool flag = false;
			foreach (skillEffctType skillEffctType2 in BuffEffectprefab.inst.EffectsList)
			{
				if (skillEffctType2.name == Name)
				{
					flag = true;
					skillEffctType = skillEffctType2;
					break;
				}
			}
			if (flag)
			{
				return skillEffctType.obj;
			}
			return null;
		}

		// Token: 0x04005402 RID: 21506
		[SerializeField]
		public List<skillEffctType> EffectsList = new List<skillEffctType>();

		// Token: 0x04005403 RID: 21507
		public static BuffEffectprefab inst;
	}
}
