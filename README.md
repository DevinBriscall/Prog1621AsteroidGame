Invasion Game Documentation
Project Description & Scope Specification
Project Name: Invasion
Purpose:
Invasion is an engaging and challenging game designed for users to navigate a spaceship through various levels, collecting stars while avoiding asteroids. The game consists of eight levels, each increasing in difficulty and complexity. Players must successfully collect all stars on each level and navigate to Earth to progress to the next level. The game aims to provide entertainment and improve hand-eye coordination and reaction time.
Scope:
The game is developed using Universal Windows Platform (UWP) for Windows, ensuring compatibility with various Windows devices. The project includes a user-friendly interface with minimal menu options to enhance gameplay experience. Upon starting the game, players will receive essential information about game mechanics and controls.
________________________________________
Class Descriptions and Diagrams
Class Descriptions
1.	GamePiece
o	Represents a basic game piece, containing properties for its position and image. It allows for the management of game piece attributes.
2.	LevelData
o	Manages the configuration of game levels, including the locations and attributes of stars and asteroids for each level. It stores level-specific data in a structured format.
3.	Player
o	Extends the GamePiece class to represent the player-controlled spaceship. It handles player movement, including vertical and horizontal adjustments, and manages the effects of gravity.
4.	ScoreKeeping
o	Responsible for tracking and saving the player's score and time. It includes methods to start, stop, reset the game timer, and manage high scores.
5.	Star
o	Represents collectible stars in the game. It tracks whether each star has been collected.
Class Diagram
Create a UML Class Diagram here to visually represent the relationships and attributes of the classes.
________________________________________
User Manual / Guide
Controls:
•	Spacebar: Press to move the spaceship upward.
•	Left Arrow Key: Move the spaceship left.
•	Right Arrow Key: Move the spaceship right.
Gameplay:
•	The objective is to collect all stars while avoiding asteroids on each level.
•	After collecting all stars, the player must fly to Earth to progress to the next level.
•	The game consists of eight levels, each with unique challenges.
Game Display:
•	The top-left corner of the screen displays:
o	Current level
o	Stars collected / Total stars
o	Time elapsed
o	Fastest time / High score
