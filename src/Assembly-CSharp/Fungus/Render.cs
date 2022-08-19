using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EA9 RID: 3753
	[EventHandlerInfo("MonoBehaviour", "Render", "The block will execute when the desired Rendering related message for the monobehaviour is received.")]
	[AddComponentMenu("")]
	public class Render : EventHandler
	{
		// Token: 0x06006A2F RID: 27183 RVA: 0x00292B6E File Offset: 0x00290D6E
		private void OnPostRender()
		{
			if ((this.FireOn & Render.RenderMessageFlags.OnPostRender) != (Render.RenderMessageFlags)0)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x06006A30 RID: 27184 RVA: 0x00292B81 File Offset: 0x00290D81
		private void OnPreCull()
		{
			if ((this.FireOn & Render.RenderMessageFlags.OnPreCull) != (Render.RenderMessageFlags)0)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x06006A31 RID: 27185 RVA: 0x00292B94 File Offset: 0x00290D94
		private void OnPreRender()
		{
			if ((this.FireOn & Render.RenderMessageFlags.OnPreRender) != (Render.RenderMessageFlags)0)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x06006A32 RID: 27186 RVA: 0x00004095 File Offset: 0x00002295
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
		}

		// Token: 0x06006A33 RID: 27187 RVA: 0x00292BA7 File Offset: 0x00290DA7
		private void OnRenderObject()
		{
			if ((this.FireOn & Render.RenderMessageFlags.OnRenderObject) != (Render.RenderMessageFlags)0)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x06006A34 RID: 27188 RVA: 0x00292BBB File Offset: 0x00290DBB
		private void OnWillRenderObject()
		{
			if ((this.FireOn & Render.RenderMessageFlags.OnWillRenderObject) != (Render.RenderMessageFlags)0)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x06006A35 RID: 27189 RVA: 0x00292BCF File Offset: 0x00290DCF
		private void OnBecameInvisible()
		{
			if ((this.FireOn & Render.RenderMessageFlags.OnBecameInvisible) != (Render.RenderMessageFlags)0)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x06006A36 RID: 27190 RVA: 0x00292BE3 File Offset: 0x00290DE3
		private void OnBecameVisible()
		{
			if ((this.FireOn & Render.RenderMessageFlags.OnBecameVisible) != (Render.RenderMessageFlags)0)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x040059DF RID: 23007
		[Tooltip("Which of the Rendering messages to trigger on.")]
		[SerializeField]
		[EnumFlag]
		protected Render.RenderMessageFlags FireOn = Render.RenderMessageFlags.OnWillRenderObject;

		// Token: 0x020016F8 RID: 5880
		[Flags]
		public enum RenderMessageFlags
		{
			// Token: 0x0400748E RID: 29838
			OnPostRender = 1,
			// Token: 0x0400748F RID: 29839
			OnPreCull = 2,
			// Token: 0x04007490 RID: 29840
			OnPreRender = 4,
			// Token: 0x04007491 RID: 29841
			OnRenderObject = 16,
			// Token: 0x04007492 RID: 29842
			OnWillRenderObject = 32,
			// Token: 0x04007493 RID: 29843
			OnBecameInvisible = 64,
			// Token: 0x04007494 RID: 29844
			OnBecameVisible = 128
		}
	}
}
