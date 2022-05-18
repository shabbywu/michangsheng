using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001322 RID: 4898
	[EventHandlerInfo("MonoBehaviour", "Application", "The block will execute when the desired OnApplication message for the monobehaviour is received.")]
	[AddComponentMenu("")]
	public class ApplicationState : EventHandler
	{
		// Token: 0x06007749 RID: 30537 RVA: 0x00051480 File Offset: 0x0004F680
		private void OnApplicationFocus(bool focus)
		{
			if ((focus && (this.FireOn & ApplicationState.ApplicationMessageFlags.OnApplicationGetFocus) != (ApplicationState.ApplicationMessageFlags)0) || (!focus && (this.FireOn & ApplicationState.ApplicationMessageFlags.OnApplicationLoseFocus) != (ApplicationState.ApplicationMessageFlags)0))
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x0600774A RID: 30538 RVA: 0x000514A3 File Offset: 0x0004F6A3
		private void OnApplicationPause(bool pause)
		{
			if ((pause && (this.FireOn & ApplicationState.ApplicationMessageFlags.OnApplicationPause) != (ApplicationState.ApplicationMessageFlags)0) || (!pause && (this.FireOn & ApplicationState.ApplicationMessageFlags.OnApplicationResume) != (ApplicationState.ApplicationMessageFlags)0))
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x0600774B RID: 30539 RVA: 0x000514C6 File Offset: 0x0004F6C6
		private void OnApplicationQuit()
		{
			if ((this.FireOn & ApplicationState.ApplicationMessageFlags.OnApplicationQuit) != (ApplicationState.ApplicationMessageFlags)0)
			{
				this.ExecuteBlock();
			}
		}

		// Token: 0x040067F8 RID: 26616
		[Tooltip("Which of the Application messages to trigger on.")]
		[SerializeField]
		[EnumFlag]
		protected ApplicationState.ApplicationMessageFlags FireOn = ApplicationState.ApplicationMessageFlags.OnApplicationQuit;

		// Token: 0x02001323 RID: 4899
		[Flags]
		public enum ApplicationMessageFlags
		{
			// Token: 0x040067FA RID: 26618
			OnApplicationGetFocus = 1,
			// Token: 0x040067FB RID: 26619
			OnApplicationLoseFocus = 2,
			// Token: 0x040067FC RID: 26620
			OnApplicationPause = 4,
			// Token: 0x040067FD RID: 26621
			OnApplicationResume = 8,
			// Token: 0x040067FE RID: 26622
			OnApplicationQuit = 16
		}
	}
}
