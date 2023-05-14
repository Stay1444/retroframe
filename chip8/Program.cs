using chip8;

RetroFrame.RetroFrame.Create<Chip8Emulator>(rf => new Chip8Emulator(rf), args).Run();