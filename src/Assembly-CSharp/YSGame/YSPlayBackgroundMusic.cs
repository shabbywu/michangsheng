using UnityEngine;

namespace YSGame;

public class YSPlayBackgroundMusic : MonoBehaviour
{
	public int MusicIndex;

	private void Start()
	{
		MusicMag.instance.playMusic(MusicIndex);
	}
}
