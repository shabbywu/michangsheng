using UnityEngine;

public class PrepareScript : MonoBehaviour
{
	private void Awake()
	{
		if (SystemInfo.systemMemorySize <= 1024 && SystemInfo.processorCount == 1)
		{
			QualitySettings.masterTextureLimit = 1;
		}
		Application.targetFrameRate = 60;
	}

	private void Update()
	{
		if (StagesParser.stagesLoaded)
		{
			Application.LoadLevel(1);
		}
	}
}
