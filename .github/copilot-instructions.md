# GitHub Copilot Instructions for RimWorld Modding Project

## Mod Overview and Purpose

This mod is designed to enhance the interaction mechanics of RimWorld by modifying how interaction cells are determined and handled, improving both usability and gameplay dynamics. The purpose is to allow players more control and flexibility in placing objects where interaction cells overlap, reducing frustration in building layouts and improving the overall experience.

## Key Features and Systems

1. **Dynamic Interaction Cell Calculation**: The mod dynamically calculates and updates interaction cells, allowing players to place items more freely without the strict overlap constraints found in the vanilla game.
   
2. **Prevent Interaction Spot Overlap**: It introduces a system to prevent interaction spot overlap, giving players feedback when placements would cause issues and providing alternative solutions.

3. **Blueprint Placement Management**: Adjustments are made to blueprint placements, ensuring compatibility with customized interaction cells.
   
4. **Game Component Tracking**: A new `GameComponent` is included to track and manage interactions and placements across game sessions efficiently.

5. **Enhanced Gizmo Systems**: Custom gizmos are added for better player control over object interactions.

## Coding Patterns and Conventions

- The project primarily uses static classes for utility functions to ensure optimal performance and clarity.
- Classes related to the game components leverage inheritance from RimWorld’s base classes to integrate seamlessly with the game’s lifecycle.
- Methods are designed to be concise, with a focus on single-responsibility principles, enhancing readability and maintainability.
- The naming convention follows PascalCase for class and method names, promoting consistency with C# standards.

## XML Integration

While the core of the mod is built in C#, XML files are typically used in RimWorld mods to define properties and settings. This mod does not directly rely on XML configuration in the provided summary but can easily integrate XML definitions if needed for future features.

## Harmony Patching

Harmony is used extensively to patch the original game methods without modifying them directly, ensuring compatibility with future game updates and other mods. Key areas patched include:

- Methods dealing with interaction cell determination.
- Gizmo and blueprint handling in the vanilla codebase.

## Suggestions for Copilot

- **Pattern Recognition**: Utilize Copilot to identify and suggest repetitive patterns in the codebase that can be abstracted or optimized for better performance.
- **Enhanced Gizmo Features**: Use Copilot to suggest additional user controls for the gizmos, enabling smoother user interaction.
- **Expand XML Configuration**: Although not used currently, Copilot can suggest XML schema setups that align with future game or mod feature extensions.
- **Debugging Assistance**: Provide debugging outputs to assist in pinpointing miscalculations with interaction cells during gameplay.

With these guidelines, GitHub Copilot should help streamline development and provide valuable suggestions for expanding and enhancing mod features.
