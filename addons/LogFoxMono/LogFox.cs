using Godot;
using System.Diagnostics;
using System.Runtime.CompilerServices;


/// <summary>
/// A class used to log messages.<br/>
/// <example>
/// LogFox.Info("This is an info message."); <br/>
/// LogFox.Warning("This is a warning message."); <br/>
/// LogFox.Error("This is an error message."); <br/>
/// LogFox.Fatal("This is a fatal error message.");
/// </example>
/// </summary>


public partial class LogFox : Node
{
	/// <summary>
	/// Logs an info message.
	/// </summary>
	/// <param name="message">Error message.</param>
	/// <param name="memberName">Name of caller member. Dont set this. Is automatic.</param>
	/// <param name="lineNumber">Line number of caller member. Dont set this. Is automatic.</param>
	public static void Info(string message,
			[CallerMemberName] string memberName = "",
			[CallerLineNumber] int lineNumber = 0
			)
	{
		Log(message, "[color=#A6E3A1]INFO", memberName, lineNumber);
	}


	/// <summary>
	/// Logs a warning message.
	/// </summary>
	/// <param name="message">Error message.</param>
	/// <param name="memberName">Name of caller member. Dont set this. Is automatic.</param>
	/// <param name="lineNumber">Line number of caller member. Dont set this. Is automatic.</param>
	public static void Warning(string message,
			[CallerMemberName] string memberName = "",
			[CallerLineNumber] int lineNumber = 0
			)
	{
		Log(message, "[color=#F9E2AF]WARNING", memberName, lineNumber);
	}


	/// <summary>
	/// Logs an error message.
	/// </summary>
	/// <param name="message">Error message.</param>
	/// <param name="memberName">Name of caller member. Dont set this. Is automatic.</param>
	/// <param name="lineNumber">Line number of caller member. Dont set this. Is automatic.</param>
	public static void Error(string message,
			[CallerMemberName] string memberName = "",
			[CallerLineNumber] int lineNumber = 0
			)
	{
		Log(message, "[color=#F38BA8]ERROR", memberName, lineNumber);
		GD.Print(GetStackTrace());
	}


	/// <summary>
	/// Logs a fatal error message and quits the game.
	/// </summary>
	/// <param name="message">Error message.</param>
	/// <param name="memberName">Name of caller member. Dont set this. Is automatic.</param>
	/// <param name="lineNumber">Line number of caller member. Dont set this. Is automatic.</param>
	public static void Fatal(string message,
			[CallerMemberName] string memberName = "",
			[CallerLineNumber] int lineNumber = 0
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


	/// <summary>
	/// Internal func that logs a message.
	/// </summary>
	private static void Log(string message, string logLevel,
			string memberName, int lineNumber
			)
	{
		string msg = (
			"[" + Time.GetTimeStringFromSystem() + "] " +
		 	"(N: " + GetSourceName() + " F: " + memberName + " L: " + lineNumber + ") " +
		  	"[" + logLevel + "[/color]] " +
		  	message
		);
		GD.PrintRich(msg);
		if (!OS.IsDebugBuild()) { return; }
		if (Engine.GetMainLoop() is SceneTree sceneTree)
		{
			sceneTree.Root.GetNode<Autoload>("LogFoxAutoload").EmitSignal("NewLog", msg);
		}
	}


	/// <summary>
	/// Gets the name of the source file.
	/// </summary>
	private static string GetSourceName()
	{
		StackTrace stackTrace = new();
		StackFrame[] frames = stackTrace.GetFrames();
		if (frames.Length < 3)
		{
			throw new System.InvalidOperationException("Cannot get source name.");
		}
		return frames[3].GetMethod().ReflectedType.Name;
	}


	/// <summary>
	/// Gets the stack trace.
	/// </summary>
	private static string GetStackTrace()
	{
		StackTrace stackTrace = new();
		return stackTrace.ToString();
	}


	/// <summary>
	/// Logs system information. <br/>
	/// <para><b>lineNumber</b> The line number of the calling member. Dont set this. Is automatic.</para>
	/// </summary>
	public static void LogSystemInfo([CallerLineNumber] int lineNumber = 0)
	{
		if (Engine.GetMainLoop() is not SceneTree)
		{
			throw new System.InvalidOperationException("Cannot log system info before the main loop is created.");
		}
		string message = $"\nGodot Version: {Engine.GetVersionInfo()["string"]}\n" +
						 $"OS: {OS.GetName()} {OS.GetModelName()}\n" +
						 $"CPU: {OS.GetProcessorName()}\n" +
						 $"RAM: {System.GC.GetGCMemoryInfo().TotalAvailableMemoryBytes / 1024 / 1024 / 1024} GB available\n";
		Log(message, "[color=#94E2D5]SYSTEM INFO[/color]", "LogSystemInfo", lineNumber);
	}
}

