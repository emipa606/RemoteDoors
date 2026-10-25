# GitHub Copilot Instructions for Remote Doors (Continued) Mod

## Mod Overview and Purpose

Remote Doors (Continued) is a RimWorld mod that builds upon the original TGPArchers mod by adding remote technologies. The primary purpose of this mod is to introduce new interactive building types that can be controlled remotely, enhancing the gameplay experience by allowing players to manage structures from afar.

## Key Features and Systems

### Remote Controller
- **Section**: Misc
- **Research Required**: Electricity
- **Description**: A controller that can access and activate other remote objects.

### Remote Door
- **Section**: Structures
- **Research Required**: Autodoors
- **Description**: A door that can only be opened or closed using the Remote Controller.

### Remote Embrasure
- **Section**: Structures
- **Research Required**: Autodoors
- **Description**: A wall with an embrasure that defaults to a closed state. When activated by the Remote Controller, it opens to allow shooting through.

### Remote Power Switch
- **Description**: A switch that controls the power state of remote objects.

## Coding Patterns and Conventions

### File Structure
- **C# Files**: Located in the `Source\RemoteDoors` directory.
- **XML Files**: Located in the `1.7\Defs\ThingDefs` directory.

### Naming Conventions
- **Classes**: Use PascalCase (e.g., `Building_RemoteDoor`).
- **Methods**: Use camelCase (e.g., `getGizmos`).
- **Variables**: Use camelCase (e.g., `isManned`).

### Code Comments
- Use XML comments for methods and properties to provide documentation.
- Use inline comments for complex logic or temporary code.

## XML Integration

The mod integrates with RimWorld's XML system to define new building types. The following XML files are used:

- `Buildings_RemoteController.xml`
- `Buildings_RemoteDoors.xml`
- `Buildings_RemoteEmbrasure.xml`
- `Buildings_RemotePowerSwitch.xml`

Each XML file defines a `ThingDef` for a specific building type, including properties such as `defName`, `label`, `description`, and `researchPrereq`.

## Harmony Patching

The mod uses Harmony, a popular modding library for RimWorld, to patch existing game functionality. Key Harmony patches include:

- **Building_RemoteDoor**: Patches methods to handle remote activation.
- **Building_RemoteController**: Patches methods to manage remote targets.
- **Building_RemoteEmbrasure**: Patches methods to handle embrasure state changes.
- **Building_RemotePowerSwitch**: Patches methods to control power state.

### Harmony Patch Example

csharp
using HarmonyLib;
using RimWorld;
using Verse;

namespace RemoteDoors.Patches
{
    [HarmonyPatch(typeof(Thing), nameof(Thing.SpawnSetup))]
    public static class Thing_SpawnSetup_Patch
    {
        [HarmonyPostfix]
        public static void Postfix(Thing __instance)
        {
            if (__instance is Building_RemoteDoor remoteDoor)
            {
                remoteDoor.InitializeRemoteControl();
            }
        }
    }
}


## Suggestions for Copilot

### General Suggestions
- **Documentation**: Ensure that all methods and classes are well-documented using XML comments.
- **Testing**: Write unit tests for new features and patches.
- **Code Reviews**: Regularly review code to maintain quality and consistency.

### C# File Suggestions
- **Building_RemoteDoor.cs**: Focus on methods related to door activation and interaction.
- **Building_RemoteController.cs**: Focus on methods related to remote target management.
- **Building_RemoteEmbrasure.cs**: Focus on methods related to embrasure state changes.
- **Building_RemotePowerSwitch.cs**: Focus on methods related to power state control.
- **IRemoteTargetable.cs**: Focus on defining the remote targetable interface.

### XML File Suggestions
- **Buildings_RemoteController.xml**: Focus on defining the Remote Controller's properties and behavior.
- **Buildings_RemoteDoors.xml**: Focus on defining the Remote Door's properties and behavior.
- **Buildings_RemoteEmbrasure.xml**: Focus on defining the Remote Embrasure's properties and behavior.
- **Buildings_RemotePowerSwitch.xml**: Focus on defining the Remote Power Switch's properties and behavior.

## Conclusion

Remote Doors (Continued) is a comprehensive mod that enhances the gameplay experience by introducing new remote-controlled building types. By following the coding patterns and conventions outlined above, and utilizing Harmony for patching, developers can ensure a smooth and efficient development process. Contributions are welcome, so feel free to contribute to the mod's ongoing development!

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

