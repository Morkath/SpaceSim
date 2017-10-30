using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace SpaceSim
{
	public partial class BattleNavigation : Window
	{
		private static string transparentDistance = "/data\\ui\\distanceBar\\transparentDistanceBox.png";
		private static string distanceBackground = "/data\\ui\\distanceBar\\distanceBarSpaceBackGround.png";
		private static string possibleMove = "/data\\ui\\distanceBar\\possibleMoveBox.png";

		private static string userSil = "/data\\ships\\images\\friendly_sil.png";
		private static string enemySil = "/data\\ships\\images\\enemy_sil.png";

		private static Image[] userLocations = new Image[4];
		private static Image[] enemyLocations = new Image[4];
		private static Button[] userLocationBttns = new Button[4];

		private static int userRemainingMove = 0;

		public BattleNavigation()
		{
			try
			{
				InitializeComponent();
			}
			catch (Exception ex)
			{

			}
			this.Loaded += new RoutedEventHandler(Window_Loaded);
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			try
			{
				BattleNavigationInitilizations();

				var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
				//this.Left = this.Left = MainWindow.battleCrewLocation.X + MainWindow.thisRepairManagement.Width;
				//this.Top = MainWindow.battleStatusLocation.Y + MainWindow.thisBattleStatus.Height;
				this.Left = MainWindow.repairManagementLocation.X + MainWindow.thisRepairManagement.Width;
				this.Top = MainWindow.battleStatusLocation.Y + MainWindow.thisBattleStatus.Height;
				this.Activate();
				this.Topmost = true;
				this.Topmost = false;
				this.Focus();
			}
			catch (Exception ex)
			{

			}
		}
		private void BattleNavigationInitilizations()
		{
			setupDistanceBar();
			MainWindow.turnBattleSpeed = MainWindow.userShip.BattleSpeed;

			UpdateShipLocation(2, true, true);
			UpdateShipLocation(2, false, true);

			MainWindow.currentRange = 3;
			moveDisplayTB.Text = MainWindow.userShip.BattleSpeed.ToString();
			userRemainingMove = MainWindow.userShip.BattleSpeed;
			UpdateRange(0);
		}
		
		public void setupDistanceBar()
		{
			userLocations[0] = userDistanceScaleImg0;
			userLocations[1] = userDistanceScaleImg1;
			userLocations[2] = userDistanceScaleImg2;
			userLocations[3] = userDistanceScaleImg3;

			userLocationBttns[0] = userDistance0Bttn;
			userLocationBttns[1] = userDistance1Bttn;
			userLocationBttns[2] = userDistance2Bttn;
			userLocationBttns[3] = userDistance3Bttn;

			enemyLocations[0] = enemyDistanceScaleImg0;
			enemyLocations[1] = enemyDistanceScaleImg1;
			enemyLocations[2] = enemyDistanceScaleImg2;
			enemyLocations[3] = enemyDistanceScaleImg3;

			Utility.setImage(userDistanceScaleBackground, distanceBackground);
			Utility.setImage(enemyDistanceScaleBackground, distanceBackground);

			ClearUserLocationImage();
			ClearEnemyLocationImage();
		}
		public void UpdateRange()
		{
			MainWindow.currentRange = (MainWindow.userShipLocation + MainWindow.enemyShipLocation);
			rangeDisplayTB.Text = MainWindow.currentRange.ToString() + " AU";
		}
		public void UpdateRange(int moveValue)
		{
			MainWindow.currentRange = (MainWindow.currentRange + moveValue);
			rangeDisplayTB.Text = MainWindow.currentRange.ToString() + " AU";
			MainWindow.thisBattleArmory.ToggleWeaponsByRange();
		}
		public void UpdateMove(bool resetToMax = false)
		{
			if (resetToMax)
			{
				moveDisplayTB.Text = MainWindow.userShip.BattleSpeed.ToString();
				userRemainingMove = MainWindow.userShip.BattleSpeed;
				userDistance3Bttn.Visibility = Visibility.Visible;
				userDistance1Bttn.Visibility = Visibility.Visible;
			}
			else
			{
				updateMove();
			}
		}
		private void updateMove()
		{
			userRemainingMove -= 1;
			moveDisplayTB.Text = userRemainingMove.ToString();
		}
		public void UpdateShipLocation(int location, bool user, bool initial = false)
		{
			if (location >= 0 && location < 4)
			{
				if (user)
				{
					ClearUserLocationImage();
					Utility.setImage(userLocations[location], userSil);
					if (!initial)
					{
						MainWindow.turnBattleSpeed -= 1;

						if (MainWindow.userShipLocation < location)
						{
							Utility.mirrorImage(userLocations[location]);
						}
					}
					MainWindow.userShipLocation = location;
					UpdateMoveLocations(location);
				}
				else
				{
					ClearEnemyLocationImage();
					Utility.setImage(enemyLocations[location], enemySil);
					MainWindow.enemyShipLocation = location;
					Utility.mirrorImage(enemyLocations[location]);
				}

				MainWindow.thisBattleArmory.UpdateAccuracy();
				UpdateRange();
			}
		}
		public void UpdateMoveLocations(int location)
		{
			if (MainWindow.turnBattleSpeed > 0)
			{
				for (int moveCount = 1; moveCount <= MainWindow.turnBattleSpeed; moveCount++)
				{
					var newRightLocation = location - moveCount;
					var newLeftLocation = location + moveCount;

					if (newRightLocation >= 0)
					{
						Utility.setImage(userLocations[newRightLocation], possibleMove);
						userLocationBttns[newRightLocation].IsEnabled = true;
					}
					if (newLeftLocation <= 3)
					{
						Utility.setImage(userLocations[newLeftLocation], possibleMove);
						userLocationBttns[newLeftLocation].IsEnabled = true;
						Utility.mirrorImage(userLocations[newLeftLocation]);
					}
				}
			}
		}
		public void ClearUserLocationImage()
		{
			for (int imgCount = 0; imgCount < userLocations.Length; imgCount++)
			{
				Utility.setImage(userLocations[imgCount], transparentDistance);
				Utility.resetImage(userLocations[imgCount]);
				userLocationBttns[imgCount].IsEnabled = false;
			}
		}
		public void ClearEnemyLocationImage()
		{
			for (int imgCount = 0; imgCount < enemyLocations.Length; imgCount++)
			{
				Utility.setImage(enemyLocations[imgCount], transparentDistance);
				Utility.resetImage(enemyLocations[imgCount]);
			}
		}

		private void userDistance3Bttn_Click(object sender, RoutedEventArgs e)
		{
			//UpdateShipLocation(3, true);
			if (MainWindow.currentRange < 1000)
			{
				if (userRemainingMove > 0)
				{
					UpdateMove();
					UpdateRange(1);

					if (userRemainingMove <= 0)
					{
						userDistance3Bttn.Visibility = Visibility.Hidden;
						userDistance1Bttn.Visibility = Visibility.Hidden;
					}
				}
			}
		}
		private void userDistance2Bttn_Click(object sender, RoutedEventArgs e)
		{
			//UpdateShipLocation(2, true);
		}
		private void userDistance1Bttn_Click(object sender, RoutedEventArgs e)
		{
			//UpdateShipLocation(1, true);
			if (MainWindow.currentRange > 0)
			{
				if (userRemainingMove > 0)
				{
					UpdateMove();
					UpdateRange(-1);

					if (userRemainingMove <= 0)
					{
						userDistance3Bttn.Visibility = Visibility.Hidden;
						userDistance1Bttn.Visibility = Visibility.Hidden;
					}
				}
			}
		}
		private void userDistance0Bttn_Click(object sender, RoutedEventArgs e)
		{
			//UpdateShipLocation(0, true);
		}
	}
}
