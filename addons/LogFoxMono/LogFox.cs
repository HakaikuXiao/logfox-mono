using Godot;
using System.Diagnostics;

public partial class LogFox : Node
{
	public static void Info(string message,
			[System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
			[System.Runtime.CompilerServices.CallerLineNumber] int lineNumber = 0
			)
	{
		Log(message, "[color=#A6E3A1]INFO", memberName, lineNumber);
	}
	public static void Warning(string message,
			[System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
			[System.Runtime.CompilerServices.CallerLineNumber] int lineNumber = 0
			)
	{
		Log(message, "[color=#F9E2AF]WARNING", memberName, lineNumber);
	}
	public static void Error(string message,
			[System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
			[System.Runtime.CompilerServices.CallerLineNumber] int lineNumber = 0
			)
	{
		Log(message, "[color=#F38BA8]ERROR", memberName, lineNumber);
		GD.Print(GetStackTrace());
	}
	public static void Fatal(string message,
			[System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
			[System.Runtime.CompilerServices.CallerLineNumber] int lineNumber = 0
			)
	{
		Log(message, "[color=#F38BA8]FATAL", memberName, lineNumber);
		GD.Print(GetStackTrace());
		if (Engine.GetMainLoop() is SceneTree sceneTree)
		{
			sceneTree.Root.PrintTreePretty();
			Info("Quitting...");
			OS.Alert("Fatal error", message);
			sceneTree.Quit();
		}
	}
	private static void Log(string message, string logLevel,
			string memberName, int lineNumber
			)
	{
		GD.PrintRich("[", Time.GetTimeStringFromSystem(), "] ",
		 	"(N: ", GetSourceName(), " F: ", memberName, " L: ", lineNumber, ") ",
		  	"[", logLevel, "[/color]] ",
		  	message
		  	);
	}
	private static string GetSourceName()
	{
		StackTrace stackTrace = new();
		StackFrame[] frames = stackTrace.GetFrames();
		return frames[3].GetMethod().ReflectedType.Name;
	}
	private static string GetStackTrace()
	{
		StackTrace stackTrace = new();
		return stackTrace.ToString();
	}
	public static void LogSystemInfo([System.Runtime.CompilerServices.CallerLineNumber] int lineNumber = 0)
	{
		string message = $"\nGodot Version: {Engine.GetVersionInfo()["string"]}\n" +
						 $"OS: {OS.GetName()} {OS.GetModelName()}\n" +
						 $"CPU: {OS.GetProcessorName()}\n" +
						 $"RAM: {System.GC.GetGCMemoryInfo().TotalAvailableMemoryBytes / 1024 / 1024 / 1024} GB available\n";
		Log(message, "[color=#94E2D5]SYSTEM INFO[/color]", "LogSystemInfo", lineNumber);
	}
}