using System.Diagnostics;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroFrame.Rendering;
using SDL2;
using Serilog;
using Serilog.Events;

namespace RetroFrame;

public class RetroFrame : Game
{
    public static RetroFrame Create<T>(Func<RetroFrame, T> create, string[] args) where T : Emulator
    {
        Thread.CurrentThread.Name = "MAIN";
        
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .Enrich.FromLogContext()            
            .Enrich.WithThreadId()
            .Enrich.WithThreadName()
            .WriteTo.Console(LogEventLevel.Verbose, "{Timestamp:HH:mm:ss.fff} <{ThreadId}><{ThreadName}> [{Level:u3}] {Message:lj} {NewLine}{Exception}")
            .CreateLogger();

        {
            foreach (var resource in Assembly.GetAssembly(typeof(RetroFrame))!.GetManifestResourceNames()
                         .Where(x => x.StartsWith("RetroFrame.Resources.Lib")))
            {
                var fileName = resource.Replace("RetroFrame.Resources.Lib.", "");
                if (File.Exists(fileName)) continue;
                using var fsr = Assembly.GetAssembly(typeof(RetroFrame))!.GetManifestResourceStream(resource);
                using var fsw = File.OpenWrite(fileName);
                fsr!.CopyTo(fsw);
                Log.Information($"Extracted {resource}");
            }
        }

        FNALoggerEXT.LogError = Log.Error;
        FNALoggerEXT.LogInfo = Log.Information;
        FNALoggerEXT.LogWarn = Log.Warning;
        
        var retroFrame = new RetroFrame();

        var instance = create(retroFrame);

        retroFrame._emulator = instance;
        
        return retroFrame;
    }

    private Emulator _emulator = null!;
    public Emulator Emulator => _emulator;
    private readonly GraphicsDeviceManager _graphicsDeviceManager;
    private UIRenderer _uiRenderer = null!;
    private SpriteBatch _spriteBatch = null!;

    public UIRenderer UiRenderer => _uiRenderer;
    public VirtualLogger VirtualLogger { get; } = new VirtualLogger();
    
    private RetroFrame()
    {
        _graphicsDeviceManager = new GraphicsDeviceManager(this);
    }

    protected override void Initialize()
    {
        Window.Title = $"RetroFrame - {_emulator.Identifier}";
        Log.Information("Initializing RetroFrame");

        IsMouseVisible = true;
        Window.IsBorderlessEXT = false;
        Window.AllowUserResizing = true;
        base.Initialize();
        Log.Information("RetroFrame ready");
        _graphicsDeviceManager.SynchronizeWithVerticalRetrace = true;
        IsFixedTimeStep = true;
        TargetElapsedTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / 60);
        SDL.SDL_MaximizeWindow(Window.Handle);
        
        _emulator.Initialize();
    }

    protected override void LoadContent()
    {
        base.LoadContent();
        _spriteBatch = new SpriteBatch(this.GraphicsDevice);
        _uiRenderer = new UIRenderer(this);
        
        _emulator.LoadContent();
    }

    protected override void OnExiting(object sender, EventArgs args)
    {
        Log.Information("Exiting");
#if DEBUG
        Process.GetCurrentProcess().Kill();
#endif
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        
        GraphicsDevice.Clear(Color.Red);
        
        _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
        
        _uiRenderer.Render(gameTime);
        
        _spriteBatch.End();
    }
}