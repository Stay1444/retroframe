﻿using ImGuiNET;
using RetroFrame.UI;

namespace chip8.UI;

public class ControlPanel : EditorPanel
{
    public ControlPanel(RetroFrame.RetroFrame retroFrame) : base(retroFrame)
    {
    }

    public override string Name { get; } = "Control";
    
    public override void Draw()
    {
        if (Open && ImGui.Begin(Name, ref Open))
        {
            ImGui.End();
        }
    }
}