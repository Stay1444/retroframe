using ImGuiNET;
using RetroFrame.UI;

namespace chip8.UI;

public class PreferencesPanel : EditorPanel
{
    private Chip8Emulator _chip8Emulator;

    private enum Presets
    {
        Chip8,
        SuperChip
    }
    
    public PreferencesPanel(RetroFrame.RetroFrame retroFrame, Chip8Emulator chip8Emulator) : base(retroFrame)
    {
        _chip8Emulator = chip8Emulator;
        SetDirectory(Environment.CurrentDirectory, "roms");
    }

    public override string Name { get; } = "Preferences";

    private string _directory = Path.Combine(Environment.CurrentDirectory, "roms");
    private List<string> _currentDirectoryFiles = new List<string>();
    
    private string? _selectedRomPath;
    private int _loadMemoryStartLocation = 200;

    private Presets _selectedPreset = Presets.Chip8;
    
    public void SetDirectory(params string[] parts)
    {
        var path = Path.Combine(parts);

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        _directory = path;
        _currentDirectoryFiles.Clear();
        
        foreach (var file in Directory.GetFiles(_directory))
        {
            _currentDirectoryFiles.Add(file);
        }

        _selectedRomPath = null;
    }

    public override void Draw()
    {
        if (Open && ImGui.Begin(Name, ref Open))
        {
            DrawAudioOptions();
            DrawDisplayOptions();
            DrawMemoryOptions();
            DrawStackOptions();
            DrawCpuOptions();
            DrawRomOptions();
            DrawPresetsOptions();

            ImGui.End();
        }
    }

    private void DrawDisplayOptions()
    {
        if (!ImGui.CollapsingHeader("Display")) return;
        
        ImGui.Text("Foreground");
        ImGui.SameLine();
        ImGui.ColorEdit3("###display_foreground", ref _chip8Emulator.Preferences.DisplayForegroundColor);
            
        ImGui.Text("Background");
        ImGui.SameLine();
        ImGui.ColorEdit3("###display_background", ref _chip8Emulator.Preferences.DisplayBackgroundColor);
            
        ImGui.Text("Width");
        ImGui.SameLine();
        ImGui.InputInt("###width", ref _chip8Emulator.Preferences.DisplayWidth);

        ImGui.Text("Height");
        ImGui.SameLine();
        ImGui.InputInt("###height", ref _chip8Emulator.Preferences.DisplayHeight);
    }

    private void DrawAudioOptions()
    {
        if (!ImGui.CollapsingHeader("Audio")) return;
    }
    
    private void DrawStackOptions()
    {
        if (!ImGui.CollapsingHeader("Stack")) return;
    }

    private void DrawMemoryOptions()
    {
        if (!ImGui.CollapsingHeader("Memory")) return;
    }

    private void DrawCpuOptions()
    {
        if (!ImGui.CollapsingHeader("CPU")) return;
    }

    private void DrawRomOptions()
    {
        if (!ImGui.CollapsingHeader("ROM")) return;
        
        ImGui.Text("ROM Directory: ");
        
        ImGui.SameLine();
        
        var dir = _directory;
        if (ImGui.InputText("###dir", ref dir, 300, ImGuiInputTextFlags.EnterReturnsTrue))
        {
            SetDirectory(dir);
        }

        ImGui.Text("Selected ROM");
        ImGui.SameLine();

        if (ImGui.BeginCombo("###rom", _selectedRomPath == null ? "<none>" : Path.GetFileName(_selectedRomPath)))
        {
            foreach (var file in _currentDirectoryFiles)
            {
                if (ImGui.Selectable(Path.GetFileName(file), _selectedRomPath == file))
                {
                    _selectedRomPath = file;
                }
            }
                
            ImGui.EndCombo();
        }
                
        ImGui.SameLine();
                
        if (ImGui.Button("Refresh"))
        {
            SetDirectory(_directory);
        }

        ImGui.Text("Load Location");

        ImGui.SameLine();
        
        ImGui.InputInt("###mlc", ref _loadMemoryStartLocation, 1, 32);
    }

    private void DrawPresetsOptions()
    {
        if (!ImGui.CollapsingHeader("Presets")) return;
        
        ImGui.Text("Preset");

        ImGui.SameLine();
        
        if (ImGui.BeginCombo("###presets", _selectedPreset.ToString()))
        {
            if (ImGui.Selectable(Presets.Chip8.ToString(), _selectedPreset == Presets.Chip8))
            {
                _selectedPreset = Presets.Chip8;
            }
                
            if (ImGui.Selectable(Presets.SuperChip.ToString(), _selectedPreset == Presets.SuperChip))
            {
                _selectedPreset = Presets.SuperChip;
            }
            
            ImGui.EndCombo();
        }
        

        if (ImGui.Button("Apply"))
        {
            RetroFrame.VirtualLogger.Info("Applying preset " + _selectedPreset);
            switch (_selectedPreset)
            {
                case Presets.Chip8:
                    break;
                case Presets.SuperChip:
                    break;
                default: throw new NotSupportedException($"Preset {_selectedPreset} not supported");
            }
        }
    }
}