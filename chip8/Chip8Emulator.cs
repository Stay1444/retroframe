using chip8.UI;
using Microsoft.Xna.Framework.Graphics;
using RetroFrame;

namespace chip8;

public class Chip8Emulator : Emulator
{
    public override string Identifier { get; } = "Chip8";

    public Preferences Preferences { get; } = new Preferences();
    
    public override void Initialize()
    {
        RetroFrame.UiRenderer.AddPanel(new PreferencesPanel(RetroFrame, this));
        RetroFrame.UiRenderer.AddPanel(new ControlPanel(RetroFrame));
        RetroFrame.UiRenderer.AddPanel(new TimersPanel(RetroFrame));
        RetroFrame.UiRenderer.AddPanel(new RegistersPanel(RetroFrame));
        RetroFrame.UiRenderer.AddPanel(new KeyboardPanel(RetroFrame));
        RetroFrame.UiRenderer.AddPanel(new CpuPanel(RetroFrame));
        RetroFrame.UiRenderer.AddPanel(new MemoryPanel(RetroFrame));
        RetroFrame.UiRenderer.AddPanel(new StackPanel(RetroFrame));
    }

    public override void LoadContent()
    {
        Logger.Info("Chip8 Emulator Loaded");
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        
    }

    public Chip8Emulator(RetroFrame.RetroFrame retroFrame) : base(retroFrame)
    {
        
    }
}