using System;
using System.Collections.Generic;
using UnityEngine;

namespace YSGame
{
	// Token: 0x02000A73 RID: 2675
	public class BuffEffectprefab : MonoBehaviour
	{
		// Token: 0x06004B33 RID: 19251 RVA: 0x001FF94C File Offset: 0x001FDB4C
		private void Start()
		{
			BuffEffectprefab.inst = this;
			Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x06004B34 RID: 19252 RVA: 0x001FF960 File Offset: 0x001FDB60
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

		// Token: 0x04004A5D RID: 19037
		[SerializeField]
		public List<skillEffctType> EffectsList = new List<skillEffctType>();

		// Token: 0x04004A5E RID: 19038
		public static BuffEffectprefab inst;
	}
}
