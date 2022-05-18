using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200137F RID: 4991
	[Serializable]
	public struct GameObjectData
	{
		// Token: 0x060078E4 RID: 30948 RVA: 0x00052382 File Offset: 0x00050582
		public GameObjectData(GameObject v)
		{
			this.gameObjectVal = v;
			this.gameObjectRef = null;
		}

		// Token: 0x060078E5 RID: 30949 RVA: 0x00052392 File Offset: 0x00050592
		public static implicit operator GameObject(GameObjectData gameObjectData)
		{
			return gameObjectData.Value;
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x060078E6 RID: 30950 RVA: 0x0005239B File Offset: 0x0005059B
		// (set) Token: 0x060078E7 RID: 30951 RVA: 0x000523BD File Offset: 0x000505BD
		public GameObject Value
		{
			get
			{
				if (!(this.gameObjectRef == null))
				{
					return this.gameObjectRef.Value;
				}
				return this.gameObjectVal;
			}
			set
			{
				if (this.gameObjectRef == null)
				{
					this.gameObjectVal = value;
					return;
				}
				this.gameObjectRef.Value = value;
			}
		}

		// Token: 0x060078E8 RID: 30952 RVA: 0x000523E1 File Offset: 0x000505E1
		public string GetDescription()
		{
			if (this.gameObjectRef == null)
			{
				return this.gameObjectVal.ToString();
			}
			return this.gameObjectRef.Key;
		}

		// Token: 0x040068E3 RID: 26851
		[SerializeField]
		[VariableProperty("<Value>", new Type[]
		{
			typeof(GameObjectVariable)
		})]
		public GameObjectVariable gameObjectRef;

		// Token: 0x040068E4 RID: 26852
		[SerializeField]
		public GameObject gameObjectVal;
	}
}
