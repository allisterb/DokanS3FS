namespace DokanS3FS.Control;

using System;
using System.Windows.Forms;

using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;

using System.Xml.Serialization;

using CommandLine;
using CommandLine.Text;

internal class Program : Runtime
{
    public enum ExitResult
    {
        SUCCESS = 0,
        UNHANDLED_EXCEPTION = 1,
        INVALID_OPTIONS = 2,
        ERROR_IN_RESULTS = 3,
        UNKNOWN_ERROR = 4
    }

    static Program()
    {
        AppDomain.CurrentDomain.UnhandledException += Program_UnhandledException;
        InteractiveConsole = true;
        Console.CancelKeyPress += Console_CancelKeyPress;
        Console.OutputEncoding = Encoding.UTF8;
        foreach (var t in optionTypes)
        {
            optionTypesMap.Add(t.Name, t);
        }
    }

    #region Entry-point
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
        if (args.Contains("--debug") || args.Contains("-d"))
        {
            DebugEnabled = true;
            SetLogger(new SerilogLogger(console: true, debug: true));
            Info("Debug mode set.");
        }
        else
        {
            SetLogger(new SerilogLogger(console: true, debug: false));
        }

        PrintLogo();

        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        //ApplicationConfiguration.Initialize();
        //Application.Run(new Form1());
        //S3.Connect();
        XmlSerializer serializer = new XmlSerializer(typeof(S3Config));
        S3Config? d = null;
        using Stream reader = new FileStream("s3.xml", FileMode.Open);
        
            // Call the Deserialize method to restore the object's state.
        S3Config? i = (S3Config) serializer.Deserialize(reader);
        
    }
    #endregion
    #region Methods
    static void PrintLogo()
    {
        Con.Write(new FigletText(font, "S3fs").LeftAligned().Color(Spectre.Console.Color.Cyan1));
        Con.Write(new Text($"v{AssemblyVersion.ToString(3)}\n").LeftAligned());
    }

    static void RestoreOriginalConsoleColors()
    {
        Console.ForegroundColor = originalConsoleForegroundColor;
        Console.BackgroundColor = originalConsoleBackgroundColor;
    }

    public static void Exit(ExitResult result)
    {
        if (Cts != null && !Cts.Token.CanBeCanceled)
        {
            Cts.Cancel();
            Cts.Dispose();
        }
        RestoreOriginalConsoleColors();
        Serilog.Log.CloseAndFlush();
        Environment.Exit((int)result);
    }

    #endregion

    #region Event Handlers
    private static void Program_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        Serilog.Log.CloseAndFlush();
        Error("Unhandled runtime error occurred. Silver CLI will now shutdown.");
        Con.WriteException((Exception)e.ExceptionObject);
        Exit(ExitResult.UNHANDLED_EXCEPTION);
    }

    private static void Console_CancelKeyPress(object? sender, ConsoleCancelEventArgs e)
    {
        Info("Ctrl-C pressed. Exiting.");
        Cts.Cancel();
        Exit(ExitResult.SUCCESS);
    }
    #endregion


    #region Fields
    static object uilock = new object();
    static Type[] optionTypes =
    {
        typeof(Options), typeof(StartOptions)

    };
    static FigletFont font = FigletFont.Load(Path.Combine(AssemblyLocation, "chunky.flf"));
    static Dictionary<string, Type> optionTypesMap = new Dictionary<string, Type>();

    static ConsoleColor originalConsoleForegroundColor = Console.ForegroundColor;
    static ConsoleColor originalConsoleBackgroundColor = Console.BackgroundColor;
    #endregion
}
