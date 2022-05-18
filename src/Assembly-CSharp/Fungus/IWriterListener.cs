using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200133E RID: 4926
	public interface IWriterListener
	{
		// Token: 0x0600779A RID: 30618
		void OnInput();

		// Token: 0x0600779B RID: 30619
		void OnStart(AudioClip audioClip);

		// Token: 0x0600779C RID: 30620
		void OnPause();

		// Token: 0x0600779D RID: 30621
		void OnResume();

		// Token: 0x0600779E RID: 30622
		void OnEnd(bool stopAudio);

		// Token: 0x0600779F RID: 30623
		void OnGlyph();

		// Token: 0x060077A0 RID: 30624
		void OnVoiceover(AudioClip voiceOverClip);
	}
}
