using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpaceSim
{
    public partial class Helm : Window
    {
        public string[,] HelmSpatialLocations { get; set; }
        public DataTable BattleFieldLayout { get; set; }
        public string HelmInfoDisplayBox 
        { 
            get 
            {
                return _helmInfoDisplayBox;
            } 
            set 
            {
                if (_helmInfoDisplayBox != null)
                {
                    _helmInfoDisplayBox += Environment.NewLine;
                    _helmInfoDisplayBox += value;

                    HelmInfoDisplayTB.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
                }
                else
                {
                    _helmInfoDisplayBox = value;
                }


            } 
        }
        private string _helmInfoDisplayBox;
        public int PlayerShipMovementRemaining { 
            get
            {
                return _playerShipMovementRemaining;
            } 
            set
            {
                _playerShipMovementRemaining = value;
                PlayerShipMoveRemainingTB.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            } 
        }
        private int _playerShipMovementRemaining;
        public int[] PlayerLocation { get; set; }
        public int[] EnemyLocation { get; set; }
        public int GridRowSize { get; set; }
        public int GridColumnSize { get; set; }

        private int playerSpeed = 1;
        private int cellSize = 100;

        public Helm()
        {
			// Use multi dimensional array to store data for sectors.
			// Entity Type, Owner, Name, Tech Level, Prosperity Level, Modifier1, Modifier2, Modifier3.
            InitializeComponent();
            this.DataContext = this;
            
            DataSetup();
            
            BattleFieldSetup();

            SetupShips();
            
            UpdateBattleField();

            ConvertToBattleField();

            HelmInfoDisplayBox = "Starting Location: " + PlayerLocation[0] + " " + PlayerLocation[1];
        }

        private void DataSetup()
        {
            HelmInfoDisplayBox = "Battle Start!";

            GridRowSize = 8;
            GridColumnSize = 8;

            PlayerShipMovementRemaining = 3;
            UpdateSpeed();
        }

        private void BattleFieldSetup()
        {
            BattleFieldLayout = new DataTable("BattleFieldLayout");
            PlayerLocation = new int[2];
            EnemyLocation = new int[2];

            var tempCellStyle = new Style(typeof(DataGridCell))
            {
                Setters = { 
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center)
                    }
            };

            HelmSpatialLocationGrid.CellStyle = tempCellStyle;
            HelmSpatialLocationGrid.RowHeight = cellSize;

            for (int i = 0; i < GridColumnSize; i++)
            {              
                BattleFieldLayout.Columns.Add(i.ToString());
            }
        }

        private void ConvertToBattleField()
        {
            BattleFieldLayout.Rows.Clear();
            HelmSpatialLocationGrid.Columns.Clear();

            for (int row = 0; row < GridRowSize; row++)
            {
                var tempRow = BattleFieldLayout.NewRow();
                
                for (int column = 0; column < GridColumnSize; column++)
                {
                    tempRow[column.ToString()] = HelmSpatialLocations[row, column];
                }

                BattleFieldLayout.Rows.Add(tempRow);
            }

            if (BattleFieldLayout != null)
            {
                foreach (DataColumn col in BattleFieldLayout.Columns)
                {
                    HelmSpatialLocationGrid.Columns.Add(
                      new DataGridTextColumn
                      {
                          Header = col.ColumnName,
                          MinWidth = cellSize,
                          Binding = new Binding(string.Format("[{0}]", col.ColumnName))
                      });
                }

                HelmSpatialLocationGrid.DataContext = null;
                HelmSpatialLocationGrid.DataContext = BattleFieldLayout;
            }
        }

        private void SetupShips()
        {
            var rnd = new Random();
            var rnd2 = new Random();

            PlayerLocation[0] = 7; // Row
            PlayerLocation[1] = rnd.Next(0,7); // Column

            EnemyLocation[0] = 0; // Row
            EnemyLocation[1] = rnd2.Next(0, 7); // Column
        }

        private void UpdateBattleField()
        {
            HelmSpatialLocations = new string[GridRowSize, GridColumnSize];

            for (int row = 0; row < GridRowSize; row++)
            {
                for (int column = 0; column < GridColumnSize; column++)
                {
                    HelmSpatialLocations[row, column] = "X";
                }
            }

            HelmSpatialLocations[EnemyLocation[0], EnemyLocation[1]] = "EnemyShip";
            HelmSpatialLocations[PlayerLocation[0], PlayerLocation[1]] = "PlayerShip";
        }

        private void UpdateSpeed()
        {
            ToggleSpeed0();
            ToggleSpeed1();
            ToggleSpeed2();
            ToggleSpeed3();
            ToggleSpeed4();
            ToggleSpeed5();
            ToggleSpeed6();
        }
        private void LowerSpeed()
        {
            if (PlayerShipMovementRemaining > 0)
            {
                playerSpeed--;
                PlayerShipMovementRemaining--;
                HelmInfoDisplayBox = "Decreasing Speed!";
                UpdateSpeed();
            }

            else
            {
                HelmInfoDisplayBox = "Not enough movement remaining!";
            }
        }
        private void IncreaseSpeed()
        {
            if (PlayerShipMovementRemaining > 0)
            {
                playerSpeed++;
                PlayerShipMovementRemaining--;
                HelmInfoDisplayBox = "Increasing Speed!";
                UpdateSpeed();
            }

            else
            {
                HelmInfoDisplayBox = "Not enough movement remaining!";
            }
        }
        private void ToggleSpeed0()
        {
            PlayerSpeed0.Background = Brushes.Black;
            PlayerSpeed0.Foreground = Brushes.Black;
        }
        private void ToggleSpeed1()
        {
            if (playerSpeed >= 1)
            {
                PlayerSpeed1.Background = Brushes.Yellow;
            }

            else
            {
                PlayerSpeed1.Background = Brushes.Transparent;
            }
        }
        private void ToggleSpeed2()
        {
            if (playerSpeed >= 2)
            {
                PlayerSpeed2.Background = Brushes.Yellow;
            }

            else
            {
                PlayerSpeed2.Background = Brushes.Transparent;
            }
        }
        private void ToggleSpeed3()
        {
            if (playerSpeed >= 3)
            {
                PlayerSpeed3.Background = Brushes.Orange;
            }

            else
            {
                PlayerSpeed3.Background = Brushes.Transparent;
            }
        }
        private void ToggleSpeed4()
        {
            if (playerSpeed >= 4)
            {
                PlayerSpeed4.Background = Brushes.Orange;
            }

            else
            {
                PlayerSpeed4.Background = Brushes.Transparent;
            }
        }
        private void ToggleSpeed5()
        {
            if (playerSpeed >= 5)
            {
                PlayerSpeed5.Background = Brushes.Red;
            }

            else
            {
                PlayerSpeed5.Background = Brushes.Transparent;
            }
        }
        private void ToggleSpeed6()
        {
            if (playerSpeed >= 6)
            {
                PlayerSpeed6.Background = Brushes.Red;
            }
            
            else
            {
                PlayerSpeed6.Background = Brushes.Transparent;
            }
        }

        private void MovePlayer(string directionToMove)
        {
            if (PlayerShipMovementRemaining > 0 && playerSpeed > 0)
            {
                switch (directionToMove)
                {
                    case "northeast":
                        MoveNorth(PlayerLocation, playerSpeed);
                        MoveEast(PlayerLocation, playerSpeed);
                        break;
                    case "northwest":
                        MoveNorth(PlayerLocation, playerSpeed);
                        MoveWest(PlayerLocation, playerSpeed);
                        break;
                    case "southeast":
                        MoveSouth(PlayerLocation, playerSpeed);
                        MoveEast(PlayerLocation, playerSpeed);
                        break;
                    case "southwest":
                        MoveSouth(PlayerLocation, playerSpeed);
                        MoveWest(PlayerLocation, playerSpeed);
                        break;
                    case "north":
                        MoveNorth(PlayerLocation, playerSpeed);
                        break;
                    case "east":
                        MoveEast(PlayerLocation, playerSpeed);
                        break;
                    case "south":
                        MoveSouth(PlayerLocation, playerSpeed);
                        break;
                    case "west":
                        MoveWest(PlayerLocation, playerSpeed);
                        break;
                }


                HelmInfoDisplayBox = "Moving " + directionToMove;
                PlayerShipMovementRemaining--;
                UpdateBattleField();
                ConvertToBattleField();
            }
            else if (PlayerShipMovementRemaining == 0)
            {
                HelmInfoDisplayBox = "Not enough movement remaining!";
            }

            else if (playerSpeed == 0)
            {
                HelmInfoDisplayBox = "Not enough speed!";
            }
        }
        private void MoveNorth(int[] TargetLocation, int speed)
        {
            TargetLocation[0] = TargetLocation[0] - speed;
        }
        private void MoveSouth(int[] TargetLocation, int speed)
        {
            TargetLocation[0] = TargetLocation[0] + speed;
        }
        private void MoveEast(int[] TargetLocation, int speed)
        {
            TargetLocation[1] = TargetLocation[1] + speed;
        }
        private void MoveWest(int[] TargetLocation, int speed)
        {
            TargetLocation[1] = TargetLocation[1] - speed;
        }

        private void PlayerSpeedUp_Click(object sender, RoutedEventArgs e)
        {
            if (playerSpeed < 6)
            {
                IncreaseSpeed();
            }

            else
            {
                HelmInfoDisplayBox = "Already at max speed!";
            }
        }
        private void PlayerSpeedDown_Click(object sender, RoutedEventArgs e)
        {
            if (playerSpeed > 0)
            {
                LowerSpeed();
            }

            else
            {
                HelmInfoDisplayBox = "Already at a full stop!";
            }
        }

        private void PlayerMoveUp_Click(object sender, RoutedEventArgs e)
        {
            if (PlayerLocation[0] > 0)
            {
                MovePlayer("north");
            }
            else
            {
                HelmInfoDisplayBox = "Cannot Leave Map!";
            }
        }
        private void PlayerMoveDown_Click(object sender, RoutedEventArgs e)
        {
            if (PlayerLocation[0] < GridRowSize - playerSpeed)
            {
                MovePlayer("south");
            }
            else
            {
                HelmInfoDisplayBox = "Cannot Leave Map!";
            }
        }
        private void PlayerMoveRight_Click(object sender, RoutedEventArgs e)
        {
            if (PlayerLocation[1] < GridColumnSize - playerSpeed)
            {
                MovePlayer("east");
            }
            else
            {
                HelmInfoDisplayBox = "Cannot Leave Map!";
            }
        }
        private void PlayerMoveLeft_Click(object sender, RoutedEventArgs e)
        {
            if (PlayerLocation[1] > 0)
            {
                MovePlayer("west");
            }
            else
            {
                HelmInfoDisplayBox = "Cannot Leave Map!";
            }
        }
        private void PlayerMoveUpLeft_Click(object sender, RoutedEventArgs e)
        {
            if (PlayerLocation[0] > 0 && PlayerLocation[1] > 0)
            {
                MovePlayer("northwest");
            }
            else
            {
                HelmInfoDisplayBox = "Cannot Leave Map!";
            }
        }
        private void PlayerMoveUpRight_Click(object sender, RoutedEventArgs e)
        {
            if (PlayerLocation[0] > 0 && PlayerLocation[1] < GridColumnSize - playerSpeed)
            {
                MovePlayer("northeast");
            }
            else
            {
                HelmInfoDisplayBox = "Cannot Leave Map!";
            }
        }
        private void PlayerMoveDownLeft_Click(object sender, RoutedEventArgs e)
        {
            if (PlayerLocation[0] < GridRowSize - playerSpeed && PlayerLocation[1] > 0)
            {
                MovePlayer("southwest");
            }
            else
            {
                HelmInfoDisplayBox = "Cannot Leave Map!";
            }
        }
        private void PlayerMoveDownRight_Click(object sender, RoutedEventArgs e)
        {
            if (PlayerLocation[0] < GridRowSize - playerSpeed && PlayerLocation[1] < GridColumnSize - playerSpeed)
            {
                MovePlayer("southeast");
            }

            else
            {
                HelmInfoDisplayBox = "Cannot Leave Map!";
            }
        }
    }
}
