namespace Fungus;

public interface ILocalizable
{
	string GetStandardText();

	void SetStandardText(string standardText);

	string GetDescription();

	string GetStringId();
}
