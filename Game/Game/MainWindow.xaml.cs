using System.Diagnostics;
using System.Numerics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public class Tile
    {
        public int Value;
        public (int x, int y) Coords;

        public Tile(int x, int y, int val)
        {
            this.Value = val;
            Coords = (x, y);
        }

        
    }


    public partial class MainWindow : Window
    {
        private Tile[,] Map { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateGrid()
        {

            int col = Field.ColumnDefinitions.Count;
            int colWidth = (int)Field.ActualWidth / Field.ColumnDefinitions.Count;

            int row = Field.RowDefinitions.Count;
            int rowWidth = (int)Field.ActualHeight / Field.RowDefinitions.Count;

            Map = new Tile[col, row];

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitilazeGame();
        }

        private void InitilazeGame()
        {
            CreateGrid();

            InitilazeTile();

            RenderMap();
        }

        private bool isClicked { get; set; } = false;
        private Point startPos { get; set; }

        private int Score { get; set; } = 0;

        private bool Paused { get; set; } = false;

        private void InitilazeTile()
        {
            Random r = new Random();

            int rX = r.Next(0, Field.RowDefinitions.Count);
            int rY = r.Next(0, Field.ColumnDefinitions.Count);
            int val = (r.Next(0, 2) == 0) ? 2 : 4;

            Tile tile = new Tile(rX, rY, val);



            if (Map[tile.Coords.x, tile.Coords.y] == null)
            {
                //MessageBox.Show(rX.ToString() + ", " + rY.ToString());
                Map[tile.Coords.x, tile.Coords.y] = tile;
                return;
            }
            else
            {
                InitilazeTile();
            }
        }

        private bool EndCheck()
        {
            bool end = true;

            for (int x = 0; x < Map.GetLength(0); x++)
            {
                for (int y = 0; y < Map.GetLength(1); y++)
                {
                    if (Map[x, y] == null)
                    {
                        end = false;
                    }
                }
            }

            return end;

        }

        enum Direction
        {
            Up,
            Down,
            Right,
            Left
        }

        private void ManageMove(Direction direction)
        {
            if (direction == Direction.Up || direction == Direction.Down)
            {

                for (int x = 0; x < Map.GetLength(0); x++)
                {
                    switch (direction)
                    {
                        case Direction.Down:

                            for (int y = Map.GetLength(1) - 2; y >= 0; y--)
                            {
                                Tile tile = Map[x, y];

                                if (tile != null)
                                {
                                    //MessageBox.Show(y.ToString() + "moving tile");
                                    MoveTileDown(tile);


                                }
                            }

                            break;
                        case Direction.Up:
                            for (int y = 1; y <= Map.GetLength(1) - 1; y++)
                            {
                                Tile tile = Map[x, y];

                                if (tile != null)
                                {

                                    //MessageBox.Show(y.ToString() + "moving tile");
                                    MoveTileUp(tile);

                                }
                            }
                            break;

                    }

                }
            }
            else if (direction == Direction.Right || direction == Direction.Left)
            {
                for (int y = 0; y < Map.GetLength(1); y++)
                {
                    switch (direction)
                    {
                        case Direction.Right:
                            for (int x = Map.GetLength(0) - 2; x >= 0; x--)
                            {
                                Tile tile = Map[x, y];

                                if (tile != null)
                                {

                                    //MessageBox.Show(x.ToString() + "moving tile");
                                    MoveTileRight(tile);

                                }
                            }
                            break;
                        case Direction.Left:
                            for (int x = 1; x <= Map.GetLength(0) - 1; x++)
                            {
                                Tile tile = Map[x, y];

                                if (tile != null)
                                {

                                    //MessageBox.Show(x.ToString() + "moving tile left");
                                    MoveTileLeft(tile);

                                }
                            }
                            break;

                    }
                }
            }


            InitilazeTile(); //random tile

            RenderMap();

            Txt_Score.Text = Score.ToString();

            bool end = EndCheck(); //pokud všude v Mapě null tak end

            Paused = end;

            if (Paused)
            {

                Popup.IsHitTestVisible = true;
                Popup.Opacity = 1;

                // Create a StackPanel (Container for Score & Button)
                StackPanel stack = new StackPanel();
                stack.Background = new SolidColorBrush(Colors.White);
                stack.Width = 250;
                stack.Height = 350;
                stack.Orientation = Orientation.Vertical;

                stack.HorizontalAlignment = HorizontalAlignment.Center;
                stack.VerticalAlignment = VerticalAlignment.Center;

                // Create Score TextBlock
                TextBlock score = new TextBlock();
                score.FontSize = 20;
                score.Text = "Score: " + Score.ToString();
                score.Foreground = new SolidColorBrush(Colors.Black);
                score.HorizontalAlignment = HorizontalAlignment.Center;
                score.VerticalAlignment = VerticalAlignment.Center;

                // Create Reset Button
                Button reset = new Button();
                reset.FontSize = 20;
                reset.Foreground = new SolidColorBrush(Colors.White);
                reset.Background = new SolidColorBrush(Colors.Black);
                reset.Content = "Reset";
                reset.HorizontalAlignment = HorizontalAlignment.Center;
                reset.VerticalAlignment = VerticalAlignment.Center;       
      

                reset.Margin = new Thickness(0, 25, 0, 0);

                // Attach event handler
                reset.Click += Reset_Click;

                stack.Children.Add(score);
                stack.Children.Add(reset);

                Popup.VerticalAlignment = VerticalAlignment.Center;
                Popup.HorizontalAlignment = HorizontalAlignment.Center;
                Popup.Margin = new Thickness(0);
                Popup.Children.Add(stack); 

                //MessageBox.Show("End");
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Paused = false;
            Popup.IsHitTestVisible = true;
            Popup.Children.Clear();
            Popup.Opacity = 0;
            Score = 0;
            Txt_Score.Text = Score.ToString();
            InitilazeGame();
        }


        private void MoveTileDown(Tile tile)
        {
            for (int y = tile.Coords.y; y < Map.GetLength(1) - 1; y++)
            {
                //MessageBox.Show(y.ToString());
                int nextY = y + 1;

                if (Map[tile.Coords.x, nextY] == null)
                {
                    // posun
                    //MessageBox.Show("move");
                    //MessageBox.Show(y.ToString());
                    //MessageBox.Show(tile.Value.ToString());
                    tile.Coords.y = nextY;
                    Map[tile.Coords.x, y] = null;
                    Map[tile.Coords.x, nextY] = tile;

                }
                else
                {
                    if (Map[tile.Coords.x, y].Value == Map[tile.Coords.x, nextY].Value)
                    {
                        tile.Value += Map[tile.Coords.x, nextY].Value; //dvoj násobek
                        tile.Coords.y = nextY;
                        Map[tile.Coords.x, y] = null;
                        Map[tile.Coords.x, nextY] = tile;

                        Score += tile.Value;
                    }

                    //zastavení
                    //MessageBox.Show("hit");
                    //MessageBox.Show(y.ToString());
                    break;
                }

            }
        }

        private void MoveTileUp(Tile tile)
        {
            for (int y = tile.Coords.y; y > 0; y--)
            {
                //MessageBox.Show(y.ToString() + "kys");
                int nextY = y - 1;

                if (Map[tile.Coords.x, nextY] == null)
                {
                    //MessageBox.Show("move");
                    //MessageBox.Show(y.ToString());
                    //MessageBox.Show(tile.Value.ToString());
                    tile.Coords.y = nextY;
                    Map[tile.Coords.x, y] = null;
                    Map[tile.Coords.x, nextY] = tile;

                }
                else
                {
                    if (Map[tile.Coords.x, y].Value == Map[tile.Coords.x, nextY].Value)
                    {
                        tile.Value += Map[tile.Coords.x, nextY].Value;
                        tile.Coords.y = nextY;
                        Map[tile.Coords.x, y] = null;
                        Map[tile.Coords.x, nextY] = tile;

                        Score += tile.Value;
                    }


                    break;
                }

            }
        }

        private void MoveTileRight(Tile tile)
        {
            for (int x = tile.Coords.x; x < Map.GetLength(0) - 1; x++)
            {
                //MessageBox.Show(y.ToString() + "kys");
                int nextX = x + 1;

                if (Map[nextX, tile.Coords.y] == null)
                {
                    //MessageBox.Show("move");
                    //MessageBox.Show(y.ToString());
                    //MessageBox.Show(tile.Value.ToString());
                    tile.Coords.x = nextX;
                    Map[x, tile.Coords.y] = null;
                    Map[nextX, tile.Coords.y] = tile;

                }
                else
                {
                    if (Map[x, tile.Coords.y].Value == Map[nextX, tile.Coords.y].Value)
                    {
                        tile.Value += Map[nextX, tile.Coords.y].Value;
                        tile.Coords.x = nextX;
                        Map[x, tile.Coords.y] = null;
                        Map[nextX, tile.Coords.y] = tile;

                        Score += tile.Value;
                    }

                    break;
                }

            }
        }

        private void MoveTileLeft(Tile tile)
        {
            for (int x = tile.Coords.x; x > 0; x--)
            {
                //MessageBox.Show(y.ToString() + "kys");
                int nextX = x - 1;

                if (Map[nextX, tile.Coords.y] == null)
                {
                    //MessageBox.Show("move");
                    //MessageBox.Show(x.ToString());
                    //MessageBox.Show(tile.Value.ToString());
                    tile.Coords.x = nextX;
                    Map[x, tile.Coords.y] = null;
                    Map[nextX, tile.Coords.y] = tile;

                }
                else
                {
                    //MessageBox.Show("hit");
                    //MessageBox.Show(x.ToString());
                    if (Map[x, tile.Coords.y].Value == Map[nextX, tile.Coords.y].Value)
                    {
                        tile.Value += Map[nextX, tile.Coords.y].Value;
                        tile.Coords.x = nextX;
                        Map[x, tile.Coords.y] = null;
                        Map[nextX, tile.Coords.y] = tile;

                        Score += tile.Value;
                    }


                    break;
                }

            }
        }

        private void RenderMap()
        {

            Field.Children.Clear();

            for (int y = 0; y < Map.GetLength(0); y++)
            {
                for (int x = 0; x < Map.GetLength(1); x++)
                {
                    Tile tile = Map[x, y];

                    //MessageBox.Show(x.ToString() + ", " + y.ToString());

                    if (tile != null)
                    {
                        SolidColorBrush color = new SolidColorBrush(Colors.Red);
                        switch (tile.Value)
                        {
                            case 4:
                                color = new SolidColorBrush(Colors.Green);
                                break;
                            case 8:
                                color = new SolidColorBrush(Colors.Yellow);
                                break;
                            case 16:
                                color = new SolidColorBrush(Colors.Orange);
                                break;
                            case 32:
                                color = new SolidColorBrush(Colors.Purple);
                                break;
                        }

                        RenderTile(color, tile.Coords, tile.Value);
                        //MessageBox.Show((x.ToString() + ", " + y.ToString()) + ", tile");
                    }
                    else
                    {
                        RenderTile(new SolidColorBrush(Colors.Bisque), (x, y));
                        //MessageBox.Show((x.ToString() + ", " + y.ToString()) + ", null");
                    }
                }
            }

        }

        private void RenderTile(SolidColorBrush color, (int x, int y) Coords, int Value = 0)
        {
            Canvas tileContainer = new Canvas();

            int col = Field.ColumnDefinitions.Count;
            int colWidth = (int)Field.ActualWidth / Field.ColumnDefinitions.Count;

            int row = Field.RowDefinitions.Count;
            int rowWidth = (int)Field.ActualHeight / Field.RowDefinitions.Count;

            Rectangle tileBox = new Rectangle();
            tileBox.Fill = color;
            tileBox.Width = (color.Color == Colors.Red) ? colWidth * 0.85 : colWidth * 0.8;
            tileBox.Height = (color.Color == Colors.Red) ? rowWidth * 0.85 : rowWidth * 0.8;
            tileBox.RadiusX = 10;
            tileBox.RadiusY = 10;

            TextBlock text = new TextBlock();
            text.Text = (Value != 0) ? Value.ToString() : "";
            text.FontSize = 24;
            text.Foreground = new SolidColorBrush(Colors.Black);
            text.FontWeight = FontWeights.Bold;

            tileBox.Loaded += (s, e) =>
            {
                Canvas.SetLeft(tileBox, (colWidth * 0.15) / 2);
                Canvas.SetTop(tileBox, (rowWidth * 0.15) / 2);

                Canvas.SetLeft(text, (colWidth - text.ActualWidth) / 2);
                Canvas.SetTop(text, (rowWidth - text.ActualHeight) / 2);
            };

            tileContainer.Children.Add(tileBox);
            tileContainer.Children.Add(text);

            Grid.SetColumn(tileContainer, Coords.x); // x
            Grid.SetRow(tileContainer, Coords.y); // y

            Field.Children.Add(tileContainer);
        }

        private void Field_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Paused) return;

            Point mousePosition = e.GetPosition(Field);

            startPos = mousePosition;

            isClicked = true;
            //Debug.Text = ($"{targetCol}, {targetRow}, {col}, {row}");
        }

        private void Field_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isClicked == false) return;

            isClicked = false;

            Point mousePosition = e.GetPosition(Field);

            int dx = (int)(mousePosition.X - startPos.X);
            int dy = (int)(mousePosition.Y - startPos.Y) * -1;

            bool xDir = (Math.Abs(dx) - Math.Abs(dy) > 0);

            //right
            if (xDir && dx > 0)
            {

                ManageMove(Direction.Right);
                //Debug.Text = ($"right");
            }
            //left
            else if (xDir && dx < 0)
            {

                ManageMove(Direction.Left);
                //Debug.Text = ($"left");
            }
            //up
            else if (!xDir && dy > 0)
            {
                ManageMove(Direction.Up);
                //Debug.Text = ($"up");
            }
            //down
            else if (!xDir && dy < 0)
            {

                ManageMove(Direction.Down);
                //Debug.Text = ($"down");
            }

        }

        private void Field_MouseMove(object sender, MouseEventArgs e)
        {
            //if(isClicked == false) return;

            Point mousePosition = e.GetPosition(Field);

            int colWidth = (int)Field.ActualWidth / Field.ColumnDefinitions.Count;
            int col = (int)mousePosition.X / colWidth;
            col = Math.Min(col, Field.ColumnDefinitions.Count - 1);

            int rowWidth = (int)Field.ActualHeight / Field.RowDefinitions.Count;
            int row = (int)mousePosition.Y / rowWidth;
            row = Math.Min(row, Field.RowDefinitions.Count - 1);

            Debug.Text = ($"X: {col}, Y: {row}");

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Paused) return;
            switch (e.Key)
            {
                case Key.Right:
                    ManageMove(Direction.Right);
                    break;
                case Key.Left:
                    ManageMove(Direction.Left);
                    break;
                case Key.Up:
                    ManageMove(Direction.Up);
                    break;
                case Key.Down:
                    ManageMove(Direction.Down);
                    break;
            }
        }
    }
}