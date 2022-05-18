using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001377 RID: 4983
	[Serializable]
	public struct AudioSourceData
	{
		// Token: 0x060078BF RID: 30911 RVA: 0x00052033 File Offset: 0x00050233
		public static implicit operator AudioSource(AudioSourceData audioSourceData)
		{
			return audioSourceData.Value;
		}

		// Token: 0x060078C0 RID: 30912 RVA: 0x0005203C File Offset: 0x0005023C
		public AudioSourceData(AudioSource v)
		{
			this.audioSourceVal = v;
			this.audioSourceRef = null;
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x060078C1 RID: 30913 RVA: 0x0005204C File Offset: 0x0005024C
		// (set) Token: 0x060078C2 RID: 30914 RVA: 0x0005206E File Offset: 0x0005026E
		public AudioSource Value
		{
			get
			{
				if (!(this.audioSourceRef == null))
				{
					return this.audioSourceRef.Value;
				}
				return this.audioSourceVal;
			}
			set
			{
				if (this.audioSourceRef == null)
				{
					this.audioSourceVal = value;
					return;
				}
				this.audioSourceRef.Value = value;
			}
		}

		// Token: 0x060078C3 RID: 30915 RVA: 0x00052092 File Offset: 0x00050292
		public string GetDescription()
		{
			if (this.audioSourceRef == null)
			{
				return this.audioSourceVal.ToString();
			}
			return this.audioSourceRef.Key;
		}

		// Token: 0x040068D3 RID: 26835
		[SerializeField]
		[VariableProperty("<Value>", new Type[]
		{
			typeof(AudioSourceVariable)
		})]
		public AudioSourceVariable audioSourceRef;

		// Token: 0x040068D4 RID: 26836
		[SerializeField]
		public AudioSource audioSourceVal;
	}
}
