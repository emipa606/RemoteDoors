# GitHub Copilot Instructions for RimWorld Remote Doors Mod

## Mod Overview and Purpose

The Remote Doors mod enhances the RimWorld experience by introducing remotely operable structures to the game. This mod aims to provide players with increased control over their base’s security and functionality by allowing them to manage doors, embrasures, and power switches remotely. The ability to control these elements from a distance allows for strategic planning and reactive measures to sudden threats, significantly improving game dynamics.

## Key Features and Systems

1. **Remote Operable Doors:**
   - The mod introduces `Building_RemoteDoor`, which can be opened and closed from a distance, providing flexible control over base access and security.

2. **Remote Embrasures:**
   - The mod offers `Building_RemoteEmbrasure`, which can be opened or closed remotely to allow or prevent shooting through them. This feature is crucial during raids or defensive maneuvers.

3. **Remote Power Switches:**
   - The inclusion of `Building_RemotePowerSwitch` allows players to control power distributions in their base remotely, adding a strategic layer to power management.

## Coding Patterns and Conventions

- **Naming Conventions:**
  - Classes are named using PascalCase (e.g., `Building_RemoteController`), indicating their function or role in the mod.

- **Accessibility:**
  - Internal classes (`internal class`) are used where extended access is unnecessary, maintaining encapsulation within the mod components.

- **Method Structure:**
  - Methods are prefixed with appropriate access modifiers (e.g., `private`, `public`) and named to reflect their actions, such as `ActivateRemote`, `OpenEmbrasure`, and `ChangeState`.

## XML Integration

While the provided summary does not explicitly mention XML files, they are typically used in RimWorld modding for defining game objects and properties. The following are common practices:

- **Defining Objects:**
  - XML is used to define new buildings, items, and pawns, and link them with C# backend functionality.
  
- **Property Configuration:**
  - Use XML to specify properties like health, costs, and descriptions for the remote-controllable structures. 

## Harmony Patching

- **Introduction to Harmony:**
  - Harmony is a library used to patch or alter existing game methods in RimWorld. This mod likely uses Harmony to integrate new behaviors without modifying core game files.

- **Common Use Cases:**
  - Patch methods of the base `Building` and `Building_Door` classes to introduce remote control functionalities.
  - Utilize Harmony's `Prefix` and `Postfix` patches to adjust behavior before or after the original method execution.

## Suggestions for Copilot

1. **Class Expansion:**
   - Implement interface methods in `IRemoteTargetable` where needed, as currently, it is an incomplete interface.

2. **Error Handling:**
   - Incorporate error handling for edge cases such as unmanned controllers or targets outside operational range in `Building_RemoteController`.

3. **Code Optimization:**
   - Suggest optimizations for methods dealing with range checking (`InRange`) and user interface updates for remote interactions.

4. **Test Functions:**
   - Write unit tests and in-game tests for each remote action to ensure they work as intended under different scenarios, using RimWorld's debug tools.

5. **Documentation:**
   - Assist in generating XML documentation for C# methods to improve code readability and maintainability.

6. **Code Review:**
   - Identify potential refactoring opportunities to adhere better to the Single Responsibility Principle.

---

This comprehensive guide should assist developers working on the Remote Doors mod to use GitHub Copilot effectively, enhancing their productivity and ensuring mod quality.
