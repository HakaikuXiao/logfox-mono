using Godot;

public partial class Autoload : Node
{
	[Signal]
	public delegate void NewLogEventHandler(string msg);

	public override void _Ready()
	{
		bool isFileLoggingEnabled = (bool)ProjectSettings.GetSetting("debug/file_logging/enable_file_logging");
		if (!isFileLoggingEnabled)
		{
			LogFox.Error("File logging is not enabled. Please enable it in the project settings.");
			ProjectSettings.SetSetting("debug/file_logging/enable_file_logging", true);
			LogFox.Info("File logging enabled(To one run only).");
			LogFox.Warning("Please enable this manually in project settings to work properly.");
		}
		LogFox.LogSystemInfo();
		LogFox.Info("_Ready");
		if (!OS.IsDebugBuild()) { return; }
		LogFox.Info("It is debug build. Loading LogFox ingame viewer.");
		AddChild(new InGameLogView());
	}
}