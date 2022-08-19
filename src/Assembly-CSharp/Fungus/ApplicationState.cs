using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EA2 RID: 3746
	[EventHandlerInfo("MonoBehaviour", "Application", "The block will execute when the desired OnApplication message for the monobehaviour is received.")]
	[AddComponentMenu("")]
	public class ApplicationState : EventHandler
	{
		// Token: 0x06006A13 RID: 27155 RVA: 0x002929A8 File Offset: 0x00290BA8
		private void OnApplicationFocus(bool focus)
		{
			if ((focus && (this.FireOn & ApplicationState.ApplicationMessageFlags.OnApplicationGetFocus) != (ApplicationState.ApplicationMessageFlags)0) || (!focus && (this.FireOn & ApplicationState.ApplicationMessageFlags.OnApplicationLoseFocus) != (ApplicationState.ApplicationMessageFlags)0))
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x06006A14 RID: 27156 RVA: 0x002929CB File Offset: 0x00290BCB
		private void OnApplicationPause(bool pause)
		{
			if ((pause && (this.FireOn & ApplicationState.ApplicationMessageFlags.OnApplicationPause) != (ApplicationState.ApplicationMessageFlags)0) || (!pause && (this.FireOn & ApplicationState.ApplicationMessageFlags.OnApplicationResume) != (ApplicationState.ApplicationMessageFlags)0))
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x06006A15 RID: 27157 RVA: 0x002929EE File Offset: 0x00290BEE
		private void OnApplicationQuit()
		{
			if ((this.FireOn & ApplicationState.ApplicationMessageFlags.OnApplicationQuit) != (ApplicationState.ApplicationMessageFlags)0)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x040059DB RID: 23003
		[Tooltip("Which of the Application messages to trigger on.")]
		[SerializeField]
		[EnumFlag]
		protected ApplicationState.ApplicationMessageFlags FireOn = ApplicationState.ApplicationMessageFlags.OnApplicationQuit;

		// Token: 0x020016F4 RID: 5876
		[Flags]
		public enum ApplicationMessageFlags
		{
			// Token: 0x04007479 RID: 29817
			OnApplicationGetFocus = 1,
			// Token: 0x0400747A RID: 29818
			OnApplicationLoseFocus = 2,
			// Token: 0x0400747B RID: 29819
			OnApplicationPause = 4,
			// Token: 0x0400747C RID: 29820
			OnApplicationResume = 8,
			// Token: 0x0400747D RID: 29821
			OnApplicationQuit = 16
		}
	}
}
