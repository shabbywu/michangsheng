using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x0200061B RID: 1563
	[RequireComponent(typeof(Collider))]
	public class SurfaceIdentity : MonoBehaviour
	{
		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060031D3 RID: 12755 RVA: 0x00161740 File Offset: 0x0015F940
		// (set) Token: 0x060031D4 RID: 12756 RVA: 0x00161748 File Offset: 0x0015F948
		public Texture Texture
		{
			get
			{
				return this.m_Texture;
			}
			set
			{
				this.m_Texture = value;
			}
		}

		// Token: 0x04002C38 RID: 11320
		[SerializeField]
		[Tooltip("The texture for this surface (useful when there's no renderer attached to check for textures).")]
		private Texture m_Texture;
	}
}
