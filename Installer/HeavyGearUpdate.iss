; Script generated by the Inno Setup Script Wizard.
; (Then extensively modified by Caliban Darklock.)
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

; Enter the name of your game here
#define MyAppName "Heavy Gear Tactics"

#define MyAppInstallerName "HeavyGearTacticsUpdate"

; Enter the name of your game and a version number here
#define MyAppVerName "Heavy Gear Tactics BETA 0.6"

; Enter the name of your company, or just your name
#define MyAppPublisher "Justin Raynard"

; Enter the URL of your website
#define MyAppURL ""

; Enter the path to your game project - check Visual Studio properties for the path
#define MyAppLocation "C:\Projects\HeavyGear\Source\HeavyGear"

; Enter the name of your game executable
#define MyAppExeName "HeavyGear.exe"

; Enter the name of your game library
#define MyAppLibraryName "HG.dll"

[Setup]
AppName={#MyAppName}
AppVerName={#MyAppVerName}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DefaultGroupName={#MyAppName}
OutputBaseFilename={#MyAppInstallerName}
Compression=lzma
SolidCompression=yes

[Languages]
Name: english; MessagesFile: compiler:Default.isl

[Tasks]
Name: desktopicon; Description: {cm:CreateDesktopIcon}; GroupDescription: {cm:AdditionalIcons}; Flags: unchecked

[Files]
; The game itself minus content files
Source: {#MyAppLocation}\bin\x86\Release\{#MyAppExeName}; DestDir: {app}; Flags: ignoreversion
Source: {#MyAppLocation}\bin\x86\Release\{#MyAppLibraryName}; DestDir: {app}; Flags: ignoreversion

[Icons]
Name: {group}\{#MyAppName}; Filename: {app}\{#MyAppExeName}
Name: {group}\{cm:UninstallProgram,{#MyAppName}}; Filename: {uninstallexe}
Name: {commondesktop}\{#MyAppName}; Filename: {app}\{#MyAppExeName}; Tasks: desktopicon

[Run]
Filename: {app}\{#MyAppExeName}; Description: {cm:LaunchProgram,{#MyAppName}}; Flags: nowait postinstall skipifsilent

; The code section doesn't like comments for some reason.
; VerifyDotNet2 removes the .NET setup if you already have .NET 2.0 installed.
; VerifyDotNet2sp1 removes the VC redist if you already have .NET 2.0 SP1, -or-
; if you don't have .NET 2.0 at all (it will be installed along with .NET 3.5).
; Using the skipifdoesntexist flag allows the setup to ignore the missing files.

; The editor here on the forums doesn't like the title of this section to be "Code".
; I've changed it to "CODESECTION". Change it back to "Code" before you compile.
[CODE]

