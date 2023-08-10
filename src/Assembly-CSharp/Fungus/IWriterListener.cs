using UnityEngine;

namespace Fungus;

public interface IWriterListener
{
	void OnInput();

	void OnStart(AudioClip audioClip);

	void OnPause();

	void OnResume();

	void OnEnd(bool stopAudio);

	void OnGlyph();

	void OnVoiceover(AudioClip voiceOverClip);
}
