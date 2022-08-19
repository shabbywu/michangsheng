using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000ED6 RID: 3798
	[Serializable]
	public struct AudioSourceData
	{
		// Token: 0x06006B20 RID: 27424 RVA: 0x00295ADA File Offset: 0x00293CDA
		public static implicit operator AudioSource(AudioSourceData audioSourceData)
		{
			return audioSourceData.Value;
		}

		// Token: 0x06006B21 RID: 27425 RVA: 0x00295AE3 File Offset: 0x00293CE3
		public AudioSourceData(AudioSource v)
		{
			this.audioSourceVal = v;
			this.audioSourceRef = null;
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06006B22 RID: 27426 RVA: 0x00295AF3 File Offset: 0x00293CF3
		// (set) Token: 0x06006B23 RID: 27427 RVA: 0x00295B15 File Offset: 0x00293D15
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

		// Token: 0x06006B24 RID: 27428 RVA: 0x00295B39 File Offset: 0x00293D39
		public string GetDescription()
		{
			if (this.audioSourceRef == null)
			{
				return this.audioSourceVal.ToString();
			}
			return this.audioSourceRef.Key;
		}

		// Token: 0x04005A6A RID: 23146
		[SerializeField]
		[VariableProperty("<Value>", new Type[]
		{
			typeof(AudioSourceVariable)
		})]
		public AudioSourceVariable audioSourceRef;

		// Token: 0x04005A6B RID: 23147
		[SerializeField]
		public AudioSource audioSourceVal;
	}
}
