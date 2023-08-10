namespace Fungus;

public enum TokenType
{
	Invalid,
	Words,
	BoldStart,
	BoldEnd,
	ItalicStart,
	ItalicEnd,
	ColorStart,
	ColorEnd,
	SizeStart,
	SizeEnd,
	Wait,
	WaitForInputNoClear,
	WaitForInputAndClear,
	WaitOnPunctuationStart,
	WaitOnPunctuationEnd,
	Clear,
	SpeedStart,
	SpeedEnd,
	Exit,
	Message,
	VerticalPunch,
	HorizontalPunch,
	Punch,
	Flash,
	Audio,
	AudioLoop,
	AudioPause,
	AudioStop,
	WaitForVoiceOver
}
