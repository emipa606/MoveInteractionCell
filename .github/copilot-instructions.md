# GitHub Copilot Instructions for the Move Interaction Cell Mod

## Overview and Purpose

The **Move Interaction Cell** mod for RimWorld is designed to give players more control over the placement of interaction spots for workbenches and other buildable structures. This is particularly useful for efficiently organizing space in congested areas like kitchens or workshops, allowing players to rotate or relocate interaction spots for better workflow and aesthetic arrangements. The mod introduces a simple user interface feature that enables players to rotate the interaction spot of buildings with the click of a button or revert to default positions, enabling greater freedom and customization in base management.

## Key Features and Systems

- **Interaction Spot Movement**: Players can click to rotate the interaction spot of any building, hold Shift to rotate in the opposite direction, and right-click to reset to the default position.
- **UI Integration**: Adds a simple and intuitive user interface to handle interaction spot changes through in-game gizmos.
- **Gameplay Flexibility**: Enhances base-building flexibility by allowing the rearrangement of critical interaction points, optimizing space usage and work efficiency.

## Coding Patterns and Conventions

- **Static Utility Classes**: Many functionalities are implemented using static classes and methods for straightforward utility access across the mod.
- **Naming Conventions**: Classes and methods follow PascalCase naming conventions for clarity and consistency with C# guidelines.
- **GameComponent Utilization**: Core tracking functionality is encapsulated within a custom `GameComponent`, which manages interaction cell data efficiently across game sessions.

## XML Integration

- **Versatility through XML**: Although primarily a C# mod, the integration of XML definitions helps configure various in-game text, default settings, and potential custom object descriptors.
- **Configuration**: Ensure XML files (if any) are laid out clearly and accurately reflect the mod's functional expectations and user interface labels.

## Harmony Patching

- **Harmony Use**: The mod employs Harmony for runtime patching of existing RimWorld methods, ensuring seamless integration and minimal conflict with vanilla or other modded content.
- **Key Entries**:
  - `Building_GetGizmos`: Implements custom gizmo logic for rotating and resetting interaction cells.
  - `GameComponent_InteractionCellTracker`: Extends game components to track and manage interaction spot changes.
  
- **Patch Targets**: Ensure patch targets are precisely defined and avoid unnecessary patches to maintain game performance and reduce potential conflicts.

## Suggestions for Copilot

- **Helper Functions**: Use Copilot to assist in writing utilities or extension methods that can generalize recurring patterns in your code, reducing redundancy and improving readability.
- **Debugging and Logging**: Enhance debugging by suggesting additional logging functionality, especially when tracking interaction spot changes or during gameplay element integration.
- **XML Definition Autofill**: Employ Copilot to automate repetitive XML entry definitions where applicable, ensuring they align with C# configurations and game characteristics.
- **Harmony Patch Definition**: Use Copilot's predictive capabilities to suggest method signatures and patch frameworks, assisting in the quick establishment of new patches or debugging existing ones.

By following these guidelines, collaboration with GitHub Copilot can be greatly enhanced, making the Move Interaction Cell mod not only more efficient to work with but also more robust and user-friendly.
