using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200132D RID: 4909
	[EventHandlerInfo("MonoBehaviour", "Render", "The block will execute when the desired Rendering related message for the monobehaviour is received.")]
	[AddComponentMenu("")]
	public class Render : EventHandler
	{
		// Token: 0x06007765 RID: 30565 RVA: 0x00051646 File Offset: 0x0004F846
		private void OnPostRender()
		{
			if ((this.FireOn & Render.RenderMessageFlags.OnPostRender) != (Render.RenderMessageFlags)0)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x06007766 RID: 30566 RVA: 0x00051659 File Offset: 0x0004F859
		private void OnPreCull()
		{
			if ((this.FireOn & Render.RenderMessageFlags.OnPreCull) != (Render.RenderMessageFlags)0)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x06007767 RID: 30567 RVA: 0x0005166C File Offset: 0x0004F86C
		private void OnPreRender()
		{
			if ((this.FireOn & Render.RenderMessageFlags.OnPreRender) != (Render.RenderMessageFlags)0)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x06007768 RID: 30568 RVA: 0x000042DD File Offset: 0x000024DD
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
		}

		// Token: 0x06007769 RID: 30569 RVA: 0x0005167F File Offset: 0x0004F87F
		private void OnRenderObject()
		{
			if ((this.FireOn & Render.RenderMessageFlags.OnRenderObject) != (Render.RenderMessageFlags)0)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x0600776A RID: 30570 RVA: 0x00051693 File Offset: 0x0004F893
		private void OnWillRenderObject()
		{
			if ((this.FireOn & Render.RenderMessageFlags.OnWillRenderObject) != (Render.RenderMessageFlags)0)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x0600776B RID: 30571 RVA: 0x000516A7 File Offset: 0x0004F8A7
		private void OnBecameInvisible()
		{
			if ((this.FireOn & Render.RenderMessageFlags.OnBecameInvisible) != (Render.RenderMessageFlags)0)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x0600776C RID: 30572 RVA: 0x000516BB File Offset: 0x0004F8BB
		private void OnBecameVisible()
		{
			if ((this.FireOn & Render.RenderMessageFlags.OnBecameVisible) != (Render.RenderMessageFlags)0)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x04006811 RID: 26641
		[Tooltip("Which of the Rendering messages to trigger on.")]
		[SerializeField]
		[EnumFlag]
		protected Render.RenderMessageFlags FireOn = Render.RenderMessageFlags.OnWillRenderObject;

		// Token: 0x0200132E RID: 4910
		[Flags]
		public enum RenderMessageFlags
		{
			// Token: 0x04006813 RID: 26643
			OnPostRender = 1,
			// Token: 0x04006814 RID: 26644
			OnPreCull = 2,
			// Token: 0x04006815 RID: 26645
			OnPreRender = 4,
			// Token: 0x04006816 RID: 26646
			OnRenderObject = 16,
			// Token: 0x04006817 RID: 26647
			OnWillRenderObject = 32,
			// Token: 0x04006818 RID: 26648
			OnBecameInvisible = 64,
			// Token: 0x04006819 RID: 26649
			OnBecameVisible = 128
		}
	}
}
