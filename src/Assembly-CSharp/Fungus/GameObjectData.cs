using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EDE RID: 3806
	[Serializable]
	public struct GameObjectData
	{
		// Token: 0x06006B45 RID: 27461 RVA: 0x002960F5 File Offset: 0x002942F5
		public GameObjectData(GameObject v)
		{
			this.gameObjectVal = v;
			this.gameObjectRef = null;
		}

		// Token: 0x06006B46 RID: 27462 RVA: 0x00296105 File Offset: 0x00294305
		public static implicit operator GameObject(GameObjectData gameObjectData)
		{
			return gameObjectData.Value;
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06006B47 RID: 27463 RVA: 0x0029610E File Offset: 0x0029430E
		// (set) Token: 0x06006B48 RID: 27464 RVA: 0x00296130 File Offset: 0x00294330
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

		// Token: 0x06006B49 RID: 27465 RVA: 0x00296154 File Offset: 0x00294354
		public string GetDescription()
		{
			if (this.gameObjectRef == null)
			{
				return this.gameObjectVal.ToString();
			}
			return this.gameObjectRef.Key;
		}

		// Token: 0x04005A7A RID: 23162
		[SerializeField]
		[VariableProperty("<Value>", new Type[]
		{
			typeof(GameObjectVariable)
		})]
		public GameObjectVariable gameObjectRef;

		// Token: 0x04005A7B RID: 23163
		[SerializeField]
		public GameObject gameObjectVal;
	}
}
