using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EB5 RID: 3765
	public interface IWriterListener
	{
		// Token: 0x06006A5D RID: 27229
		void OnInput();

		// Token: 0x06006A5E RID: 27230
		void OnStart(AudioClip audioClip);

		// Token: 0x06006A5F RID: 27231
		void OnPause();

		// Token: 0x06006A60 RID: 27232
		void OnResume();

		// Token: 0x06006A61 RID: 27233
		void OnEnd(bool stopAudio);

		// Token: 0x06006A62 RID: 27234
		void OnGlyph();

		// Token: 0x06006A63 RID: 27235
		void OnVoiceover(AudioClip voiceOverClip);
	}
}
