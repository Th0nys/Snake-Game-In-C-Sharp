# Snake2.0 - A Classic Snake Game in C# with WPF

Snake2.0 is a classic snake game implemented in C# using Windows Presentation Foundation (WPF). Relive the nostalgia of the iconic Snake game, but with modern visuals and smooth gameplay. Eat the fruits, grow your snake, and try to avoid hitting the walls or yourself!

## Features

- Intuitive controls: Use the arrow keys or W/A/S/D keys to control the snake.
- Customizable colors: Personalize the game's look with a variety of color options.
- Increasing difficulty: As you grow longer, the game gets more challenging.
- High score tracking: Compete with yourself to achieve the highest score.

## Screenshots

![Screenshot 1](https://github.com/Th0nys/Snake-Game-In-C-Sharp/blob/main/Screenshot%202023-08-02%20193942.png?raw=true)
![Screenshot 2](https://github.com/Th0nys/Snake-Game-In-C-Sharp/blob/main/Screenshot%202023-08-02%20194059.png?raw=true)
![Screenshot 3](https://github.com/Th0nys/Snake-Game-In-C-Sharp/blob/main/Screenshot%202023-08-02%20194145.png?raw=true)

## Getting Started

To run the game locally, follow these steps:

1. Clone this repository to your local machine.
2. Open the project in Visual Studio or your favorite C# IDE.
3. Build the project and run the game.

## How to Play

1. Use the arrow keys to control the snake's direction.
2. Guide the snake to eat the fruits to grow longer.
3. Avoid running into the walls or colliding with the snake's body.
4. The game ends when the snake hits a wall or itself.

## Customize Colors

Want to change the game's colors? Modify the `App.xaml` file and experiment with different color values in the resources section:

```xaml
<Application.Resources>
    <SolidColorBrush x:Key="BackgroundColor">#FF000000</SolidColorBrush>
    <!-- Add more color keys here -->
</Application.Resources>
