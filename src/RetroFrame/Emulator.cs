using Microsoft.Xna.Framework.Graphics;

namespace RetroFrame;

public abstract class Emulator
{
    protected RetroFrame RetroFrame { get; }
    protected VirtualLogger Logger { get; }
    
    protected Emulator(RetroFrame retroFrame)
    {
        this.RetroFrame = retroFrame;
        this.Logger = this.RetroFrame.VirtualLogger;
    }
    
    public abstract string Identifier { get; }

    public abstract void Initialize();
    public abstract void LoadContent();
    public abstract void Draw(SpriteBatch spriteBatch);
}