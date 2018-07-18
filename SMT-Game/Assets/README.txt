Folder structure:

Animations:
Contains the animations for the visual variant of the game

Audio:
Contains the original audio composed by Veldhuis & Troost

Audio_redesigned:
Contains the redesigned audio, composed by van Andel & He

Entities:
Contains all active entities, a folder for each one.
Each entity folder can consist of a set of prefabs and scripts unique to the entity.

Fonts:
Contains the fonts used in the game (mostly variants of Lato)

Prefabs:
All prefabs that cannot be attributed to specific entities

Scenes:
Contains all the scenes of this game (1 scene for main gameplay, other scenes for experiment dialogue, menu, and questionnaire).

Scripts:
All scripts that cannot be attributed to specific entities, this includes the input system, logging system, sound system and level loader.
It also includes scripts that work on multiple entities.
Levels will go in the specific Levels sub-folder.

Sprites:
All sprites in the game (considering there shouldn't be too many, just dump them all in this folder)

TextMesh Pro:
Library that was used for clearer and crisper text in the game

 ==== ==== ==== ==== ==== ==== ====

Main game architecture:
Scenes will provide an object-pool for levels to make use of, this means we don't need to instantiate new objects
in run time.
Levels will be defined with C# scripts.
Input will go through a special input handling script for logging purposes.
