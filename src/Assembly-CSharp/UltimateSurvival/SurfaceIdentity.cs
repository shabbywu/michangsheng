using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000901 RID: 2305
	[RequireComponent(typeof(Collider))]
	public class SurfaceIdentity : MonoBehaviour
	{
		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06003B01 RID: 15105 RVA: 0x0002AC9F File Offset: 0x00028E9F
		// (set) Token: 0x06003B02 RID: 15106 RVA: 0x0002ACA7 File Offset: 0x00028EA7
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

		// Token: 0x04003559 RID: 13657
		[SerializeField]
		[Tooltip("The texture for this surface (useful when there's no renderer attached to check for textures).")]
		private Texture m_Texture;
	}
}
