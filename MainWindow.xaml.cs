using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SnakeGame;

namespace Snake2._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Dictionary to map GridValue to corresponding ImageSource
        public readonly Dictionary<GridValue, ImageSource> gridValToImage = new()
        {
            { GridValue.Empty, Images.Empty },
            { GridValue.Snake, Images.Body },
            { GridValue.Food, Images.Food }
        };

        // Dictionary to map Directions to rotation degrees for snake head
        private readonly Dictionary<Directions, int> dirToRotation = new()
        {
            { Directions.Up, 0 },
            { Directions.Right, 90 },
            { Directions.Down, 180 },
            { Directions.Left, 270 }
        };

        public readonly int rows = 25, cols = 25;
        private readonly Image[,] gridImages;
        private GameState gameState;
        private bool gameRunning;

        public MainWindow()
        {
            InitializeComponent();
            gridImages = SetupGrid();
            gameState = new GameState(rows, cols);
        }

        // Method to run the game
        private async Task Rungame()
        {
            Draw();
            await ShowCountDown();
            Overlay.Visibility = Visibility.Hidden;
            await GameLoop();
            await ShowGameOver();
            gameState = new GameState(rows, cols);
        }

        // Event handler for previewing key down events
        private async void Window_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (Overlay.Visibility == Visibility.Visible)
            {
                e.Handled = true;
            }

            if (!gameRunning)
            {
                gameRunning = true;
                await Rungame();
                gameRunning = false;
            }
        }

        // Event handler for key down events
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (gameState.GameOver)
            {
                return;
            }

            // Change the snake's direction based on the pressed key
            switch (e.Key)
            {
                case System.Windows.Input.Key.Left:
                    gameState.ChangeDirection(Directions.Left); break;
                case System.Windows.Input.Key.Right:
                    gameState.ChangeDirection(Directions.Right); break;
                case System.Windows.Input.Key.Up:
                    gameState.ChangeDirection(Directions.Up); break;
                case System.Windows.Input.Key.Down:
                    gameState.ChangeDirection(Directions.Down); break;
            }
        }

        // Method for the main game loop
        private async Task GameLoop()
        {
            while (!gameState.GameOver)
            {
                await Task.Delay(100);
                gameState.Move();
                Draw();
            }
        }

        // Method to set up the game grid with images
        private Image[,] SetupGrid()
        {
            Image[,] images = new Image[rows, cols];
            GameGrid.Rows = rows;
            GameGrid.Columns = cols;
            GameGrid.Width = GameGrid.Height * (cols / (double)rows);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Image image = new Image { Source = Images.Empty, RenderTransformOrigin = new Point(0.5, 0.5) };

                    images[r, c] = image;
                    GameGrid.Children.Add(image);
                }
            }

            return images;
        }

        // Method to update the game grid with current state
        private void Draw()
        {
            DrawGrid();
            DrawSnakeHead();
            ScoreText.Text = $"SCORE {gameState.Score}";
        }

        // Method to draw the grid with corresponding grid values
        private void DrawGrid()
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    GridValue gridVal = gameState.Grid[r, c];
                    gridImages[r, c].Source = gridValToImage[gridVal];
                    gridImages[r, c].RenderTransform = Transform.Identity;
                }
            }
        }

        // Method to draw the snake's head with appropriate rotation
        private void DrawSnakeHead()
        {
            Positions headPos = gameState.HeadPosition();
            Image image = gridImages[headPos.Row, headPos.Col];
            image.Source = Images.Head;

            int rotation = dirToRotation[gameState.Dir];
            image.RenderTransform = new RotateTransform(rotation);
        }

        // Method to draw the snake's body when the game is over
        private async Task DrawDeadSnake()
        {
            List<Positions> positions = new List<Positions>(gameState.SnakePositions());

            for (int i = 0; i < positions.Count; i++)
            {
                Positions pos = positions[i];
                ImageSource source = (i == 0) ? Images.DeadHead : Images.DeadBody;
                gridImages[pos.Row, pos.Col].Source = source;
                await Task.Delay(50);
            }
        }

        // Method to show the countdown before starting the game
        private async Task ShowCountDown()
        {
            for (int i = 3; i >= 1; i--)
            {
                OverlayText.Text = i.ToString();
                await Task.Delay(500);
            }
        }

        // Method to show the "Game Over" overlay and draw the dead snake
        private async Task ShowGameOver()
        {
            await DrawDeadSnake();
            await Task.Delay(1000);
            Overlay.Visibility = Visibility.Visible;
            OverlayText.Text = "PRESS ANY KEY TO START";
        }
    }
}
