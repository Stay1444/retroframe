using ImGuiNET;
using Microsoft.Xna.Framework;
using RetroFrame.Rendering.ImGUI;
using RetroFrame.UI;
using RetroFrame.UI.Panels;
using Vector2 = System.Numerics.Vector2;

namespace RetroFrame.Rendering;

public sealed class UIRenderer
{
    private readonly RetroFrame _retroFrame;
    private ImGUIRenderer _imGuiRenderer;
    
    private List<EditorPanel> _basePanels = new List<EditorPanel>();
    private List<EditorPanel> _externalPanels = new List<EditorPanel>();
    
    public RetroFrame RetroFrame => _retroFrame;
    public ImGUIRenderer ImGUIRenderer => _imGuiRenderer;

    public ImFontPtr Font18 { get; }
    public ImFontPtr Font16 { get; }
    
    public UIRenderer(RetroFrame retroFrame)
    {
        this._retroFrame = retroFrame;
        this._imGuiRenderer = new ImGUIRenderer(retroFrame);
        this._imGuiRenderer.RebuildFontAtlas();

        ImGui.GetIO().ConfigFlags = ImGuiConfigFlags.DockingEnable;
        
        ImGUIUtils.SetupImGuiStyle();
        
        unsafe
        {
            ImFontConfigPtr config = ImGuiNative.ImFontConfig_ImFontConfig();

            config.MergeMode = true;
            config.PixelSnapH = true;
            config.GlyphMinAdvanceX = 16.0f;
            
            try
            {
                Font18 = ImGui.GetIO().Fonts
                    .AddFontFromAssemblyResource("RetroFrame.Resources.Fonts.Ubuntu.ttf", 18);

                Font16 = ImGui.GetIO().Fonts
                    .AddFontFromAssemblyResource("RetroFrame.Resources.Fonts.Ubuntu.ttf", 16);
            }
            finally
            {
                config.Destroy();
            }
        }
        
        _imGuiRenderer.RebuildFontAtlas();
        
        _basePanels.AddRange(new EditorPanel[]
        {
            new ViewportPanel(this._retroFrame),
            new LogsPanel(this._retroFrame),
            new PerformancePanel(this._retroFrame)
        });
    }

    public void AddPanel(EditorPanel panel)
    {
        _externalPanels.Add(panel);
    }
    
    internal void Render(GameTime gameTime)
    {
        _imGuiRenderer.BeforeLayout(gameTime);

        ImGui.PushFont(Font16);
        
        var size = new Vector2(_retroFrame.GraphicsDevice.PresentationParameters.BackBufferWidth,
            _retroFrame.GraphicsDevice.PresentationParameters.BackBufferHeight);
        
        ImGui.SetNextWindowSize(size);
        ImGui.SetNextWindowPos(Vector2.Zero);
        
        ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 0);
        ImGui.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 0);
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0,0));
        
        
        ImGui.Begin("##master", 
            ImGuiWindowFlags.NoResize
            | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoTitleBar
            | ImGuiWindowFlags.NoDocking | ImGuiWindowFlags.MenuBar
            | ImGuiWindowFlags.NoBringToFrontOnFocus | ImGuiWindowFlags.NoNavFocus);
        
        var dockSpaceId = ImGui.GetID("MyDockSpace");
        ImGui.DockSpace(dockSpaceId, new Vector2(0, 0), ImGuiDockNodeFlags.None);

        RenderTopBar();

        ImGui.End();
        
        ImGui.PopStyleVar();
        ImGui.PopStyleVar();
        ImGui.PopStyleVar();

        for (var i = 0; i < _basePanels.Count; i++)
        {
            var p = _basePanels[i];
            p.Draw();
            p.Draw(gameTime);
        }

        for (var i = 0; i < _externalPanels.Count; i++)
        {
            var p = _externalPanels[i];
            p.Draw();
            p.Draw(gameTime);
        }

        ImGui.PopFont();
        
        _imGuiRenderer.AfterLayout();
    }

    private void RenderTopBar()
    {
        if (ImGui.BeginMenuBar())
        {
            if (ImGui.BeginMenu("Edit"))
            {                
                ImGui.Button("Preferences");
                ImGui.SameLine();
                
                ImGui.EndMenu();    
            }
            
            if (ImGui.BeginMenu("View"))
            {
                ImGui.SetCursorPos(ImGui.GetCursorPos() + new Vector2(10, 10));

                if (ImGui.BeginMenu("Base"))
                {
                    ImGui.SetCursorPos(ImGui.GetCursorPos() + new Vector2(10, 10));
                    ImGui.BeginGroup();
                    foreach(var panel in _basePanels)
                    {
                        ImGui.Checkbox(panel.Name, ref panel.Open);
                        ImGui.SameLine();
                        ImGui.SetCursorPos(ImGui.GetCursorPos() + new Vector2(10, 0));
                        ImGui.NewLine();
                    }
                    ImGui.Dummy(new Vector2(10,10));
                    
                    ImGui.EndGroup();
                    ImGui.EndMenu();
                }

                ImGui.SetCursorPos(ImGui.GetCursorPos() + new Vector2(10, 10));

                if (ImGui.BeginMenu(_retroFrame.Emulator.Identifier))
                {
                    ImGui.SetCursorPos(ImGui.GetCursorPos() + new Vector2(10, 10));
                    ImGui.BeginGroup();
                    foreach(var panel in _externalPanels)
                    {
                        ImGui.Checkbox(panel.Name, ref panel.Open);
                        ImGui.SameLine();
                        ImGui.SetCursorPos(ImGui.GetCursorPos() + new Vector2(10, 0));
                        ImGui.NewLine();
                    }
                    ImGui.Dummy(new Vector2(10,10));
                    
                    ImGui.EndGroup();
                    ImGui.EndMenu();
                }

                ImGui.Dummy(new Vector2(10,10));
                
                
                ImGui.EndMenu();
            }
            
            ImGui.EndMenuBar();
        }
    }
}