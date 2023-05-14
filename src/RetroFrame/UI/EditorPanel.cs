using Microsoft.Xna.Framework;

namespace RetroFrame.UI;

public abstract class EditorPanel
{
    public RetroFrame RetroFrame { get; }

    public bool Open = true;
    
    public abstract string Name { get; }
    
    protected EditorPanel(RetroFrame retroFrame)
    {
        this.RetroFrame = retroFrame;
    }
    
    public virtual void Draw() { }
    public virtual void Draw(GameTime gameTime) { }
}