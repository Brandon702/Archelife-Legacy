Note to self:
Fix errors
Implement commented out code
Add Saving & Loading
Test in unity
Update this file to help set up games universally


Tags are the way to check everything for panels

Instructions:

Controllers - Refers to the given controllers for general use, most are drag and drop, tested for use on
the same object.

	AudioController - Attach to an object in unity and attach audio files from there and define them as needed

	CutsceneController - Specialized Cutscene controller, use to load cutscenes on demand, make sure to connect to an actual video panel

	GameController - Currently used for generic hotkeys and the Save/Load functions

	LevelLoadingController - Used to load levels either Async or normally

	MenuController - Controls the game panels
		Panels - To use the panel system, you need to tag or name them with the correct convention
			That would be keywords such as the following: MainMenu,Resume,Options,Instructions,Credits, & Pause.

Game - Game Specific Scripts, DO NOT use this section for universal and generic features.

	Please put whatever game scripts you plan to use in this folder

Objects - Specific objects in general use, such as Audio, but things like Level, Player, etc. can go here.

	Sound - Sound objects that contain important data and tags to use with my Mixer system

Other - Misc section, anything that does not fit the above belong in this category.

	DontDestroy - Attach to an object and its children into multiple scenes

	UIHelper - N/A