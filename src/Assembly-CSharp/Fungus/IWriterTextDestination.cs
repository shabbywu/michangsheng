using UnityEngine;

namespace Fungus;

public interface IWriterTextDestination
{
	string Text { get; set; }

	void ForceRichText();

	void SetTextColor(Color textColor);

	void SetTextAlpha(float textAlpha);

	bool HasTextObject();

	bool SupportsRichText();
}
