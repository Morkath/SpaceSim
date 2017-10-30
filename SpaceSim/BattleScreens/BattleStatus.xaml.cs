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
	public partial class BattleStatus : Window
	{
		#region Declarations
		private static string shipSpaceBackground = "/data\\ui\\shipSpaceBackGround.png";
		private static string transparentDeployableOverlay = "/data\\ui\\deployables\\transparentDeployable.png";
		private static string spaceBackDeployable = "/data\\ui\\deployables\\spaceDeployables.png";
		private static string attackOverlay = "/data\\ui\\deployables\\attackOverlay.png";
		private static string interceptOverlay = "/data\\ui\\deployables\\interceptOverlay.png";
		private static string defendOverlay = "/data\\ui\\deployables\\defendOverlay.png";
		private static string marineBackground = "/data\\ui\\deployables\\marineBackground.png";
		private static string marine = "/data\\ui\\deployables\\marine.png";
		
		private bool userDeployedUnit0 = false;
		private int userDeployedUnit0Action = -1;
		private bool userDeployedUnit1 = false;
		private int userDeployedUnit1Action = -1;
		private bool userDeployedUnit2 = false;
		private int userDeployedUnit2Action = -1;

		private int deployingUnitSlot = -1;

		#endregion

		public BattleStatus()
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
				BattleStatusInitilizations();

				var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
				//this.Left = MainWindow.battleCrewLocation.X + MainWindow.thisRepairManagement.Width;
				//this.Top = MainWindow.battleCrewLocation.Y + MainWindow.thisBattleCrew.Height;
				this.Left = MainWindow.repairManagementLocation.X + MainWindow.thisRepairManagement.Width;
				this.Top = MainWindow.battleCrewLocation.Y + MainWindow.thisBattleCrew.Height;
				this.Activate();
				this.Topmost = true;
				this.Topmost = false;
				this.Focus();
			}
			catch (Exception ex)
			{

			}
		}
		private void BattleStatusInitilizations()
		{
			setupDeployables();
			hideOfflineElements();

			debugStuff();
		}

		public void setupDeployables()
		{
			Utility.setImage(userDeployedBackImg0, spaceBackDeployable);
			Utility.setImage(userDeployedBackImg1, spaceBackDeployable);
			Utility.setImage(userDeployedBackImg2, spaceBackDeployable);

			Utility.setImage(userDeployedImg0, transparentDeployableOverlay);
			Utility.setImage(userDeployedImg1, transparentDeployableOverlay);
			Utility.setImage(userDeployedImg2, transparentDeployableOverlay);

			Utility.setImage(userDeployedActionImg0, transparentDeployableOverlay);
			Utility.setImage(userDeployedActionImg1, transparentDeployableOverlay);
			Utility.setImage(userDeployedActionImg2, transparentDeployableOverlay);

			Utility.setImage(enemyDeployedBackImg0, spaceBackDeployable);
			Utility.setImage(enemyDeployedBackImg1, spaceBackDeployable);
			Utility.setImage(enemyDeployedBackImg2, spaceBackDeployable);

			Utility.setImage(enemyDeployedActionImg0, transparentDeployableOverlay);
			Utility.setImage(enemyDeployedActionImg1, transparentDeployableOverlay);
			Utility.setImage(enemyDeployedActionImg2, transparentDeployableOverlay);

			Utility.setImage(userMarineFriendlyBackground, marineBackground);
			Utility.setImage(enemyMarineFriendlyBackground, marineBackground);
			Utility.mirrorImage(enemyMarineFriendlyBackground);
			Utility.setImage(userMarinesEnemyBackground, marineBackground);
			Utility.setImage(enemyMarinesEnemyBackground, marineBackground);
			Utility.mirrorImage(enemyMarinesEnemyBackground);

			Utility.setImage(enemyMarinesFriendly0, marine);
			Utility.mirrorImage(enemyMarinesFriendly0);
			Utility.setImage(enemyMarinesFriendly1, marine);
			Utility.mirrorImage(enemyMarinesFriendly1);
			Utility.setImage(enemyMarinesFriendly2, marine);
			Utility.mirrorImage(enemyMarinesFriendly2);
			Utility.setImage(enemyMarinesFriendly3, marine);
			Utility.mirrorImage(enemyMarinesFriendly3);
        }
		public void hideOfflineElements()
		{
			userDeployableMenu0.Visibility = Visibility.Hidden;
			userDeployableMenu1.Visibility = Visibility.Hidden;
			userDeployableMenu2.Visibility = Visibility.Hidden;
			userDeployedHealthBar0.Visibility = Visibility.Hidden;
			userDeployedShieldBar0.Visibility = Visibility.Hidden;
			userDeployedHealthBar1.Visibility = Visibility.Hidden;
			userDeployedShieldBar1.Visibility = Visibility.Hidden;
			userDeployedHealthBar2.Visibility = Visibility.Hidden;
			userDeployedShieldBar2.Visibility = Visibility.Hidden;
			enemyDeployedHealthBar0.Visibility = Visibility.Hidden;
			enemyDeployedShieldBar0.Visibility = Visibility.Hidden;
			deployUnitPanel.Visibility = Visibility.Hidden;
		}

		public void debugStuff()
		{
			Utility.setImage(userShipImg, MainWindow.userShip.Image);
			Utility.setImage(userShipImgBackground, shipSpaceBackground);
			Utility.setProgressBarInitial(userHealthBar, MainWindow.userShip.HullMax);
			Utility.setProgressBarInitial(userShieldBar, MainWindow.userShip.HullCurrent);

			Utility.setImage(enemyShipImg, MainWindow.enemyShip.Image);
			Utility.setImage(enemyShipImgBackground, shipSpaceBackground);
			Utility.mirrorImage(enemyShipImg);
			Utility.setProgressBarInitial(enemyHealthBar, MainWindow.enemyShip.HullMax);
			Utility.setProgressBarInitial(enemyShieldBar, MainWindow.enemyShip.ShieldMax);

			Utility.setImage(enemyDeployedImg0, MainWindow.enemyMech.Image);
			Utility.mirrorImage(enemyDeployedImg0);
			Utility.setImage(enemyDeployedActionImg0, attackOverlay);
			Utility.setProgressBarInitial(enemyDeployedHealthBar0, MainWindow.enemyMech.HullMax);
			Utility.setProgressBarInitial(enemyDeployedShieldBar0, MainWindow.enemyMech.ShieldMax);
			enemyDeployedHealthBar0.Visibility = Visibility.Visible;
			enemyDeployedShieldBar0.Visibility = Visibility.Visible;

			Utility.setImage(enemyDeployedImg1, MainWindow.enemyMech.Image);
			Utility.mirrorImage(enemyDeployedImg1);
			Utility.setImage(enemyDeployedActionImg1, interceptOverlay);
			Utility.setProgressBarInitial(enemyDeployedHealthBar1, MainWindow.enemyMech.HullMax);
			Utility.setProgressBarInitial(enemyDeployedShieldBar1, MainWindow.enemyMech.ShieldMax);
			enemyDeployedHealthBar1.Visibility = Visibility.Visible;
			enemyDeployedShieldBar1.Visibility = Visibility.Visible;

			Utility.setImage(enemyDeployedImg2, MainWindow.enemyMech.Image);
			Utility.mirrorImage(enemyDeployedImg2);
			Utility.setImage(enemyDeployedActionImg2, defendOverlay);
			Utility.setProgressBarInitial(enemyDeployedHealthBar2, MainWindow.enemyMech.HullMax);
			Utility.setProgressBarInitial(enemyDeployedShieldBar2, MainWindow.enemyMech.ShieldMax);
			enemyDeployedHealthBar2.Visibility = Visibility.Visible;
			enemyDeployedShieldBar2.Visibility = Visibility.Visible;

			deployUnitList.Items.Add(new ListViewItem { Content = MainWindow.userMech });
			deployUnitList.Items.Add(new ListViewItem { Content = MainWindow.enemyMech });
			deployUnitList.Items.Add(new ListViewItem { Content = MainWindow.attackCraft });
		}

		// Utility
		private void setDeployedAction(int unitSlot, string action)
		{
			Image deployedUnitImg = null;

			switch (unitSlot)
			{
				case 0:
					deployedUnitImg = userDeployedActionImg0;
					break;
				case 1:
					deployedUnitImg = userDeployedActionImg1;
					break;
				case 2:
					deployedUnitImg = userDeployedActionImg2;
					break;
			}

			switch (action)
			{
				case "attack":
					Utility.setImage(deployedUnitImg, attackOverlay);
					break;
				case "intercept":
					Utility.setImage(deployedUnitImg, interceptOverlay);
					break;
				case "defend":
					Utility.setImage(deployedUnitImg, defendOverlay);
					break;
			}
						
		}
		private void deployUnit()
		{
			Image unitImg = null;
			Image userDeployedActionImg = null;
			ProgressBar healthBar = null;
			ProgressBar shieldBar = null;
			ListViewItem tempUnitItem = deployUnitList.SelectedItem as ListViewItem;
			DeployableCraft tempMech = tempUnitItem.Content as DeployableCraft;

			switch (deployingUnitSlot)
			{
				case -1:
					break;
				case 0:
					unitImg = userDeployedImg0;
					healthBar = userDeployedHealthBar0;
					shieldBar = userDeployedShieldBar0;
					userDeployedUnit0 = true;
					userDeployedUnit0Action = -1;
					userDeployedActionImg = userDeployedActionImg0;
					break;
				case 1:
					unitImg = userDeployedImg1;
					healthBar = userDeployedHealthBar1;
					shieldBar = userDeployedShieldBar1;
					userDeployedUnit1 = true;
					userDeployedUnit1Action = -1;
					userDeployedActionImg = userDeployedActionImg1;
					break;
				case 2:
					unitImg = userDeployedImg2;
					healthBar = userDeployedHealthBar2;
					shieldBar = userDeployedShieldBar2;
					userDeployedUnit2 = true;
					userDeployedUnit2Action = -1;
					userDeployedActionImg = userDeployedActionImg2;
					break;
			}

			Utility.setImage(unitImg, tempMech.Image);
			Utility.setProgressBarInitial(healthBar, tempMech.HullMax, tempMech.HullCurrent);
			Utility.setProgressBarInitial(shieldBar, tempMech.ShieldMax, tempMech.ShieldCurrent);
			Utility.toggleProgressBarVisibility(healthBar);
			Utility.toggleProgressBarVisibility(shieldBar);
			Utility.setImage(userDeployedActionImg, transparentDeployableOverlay);
		}
		private void recallUnit()
		{
			Image unitImg = null;
			Image userDeployedActionImg = null;
			ProgressBar healthBar = null;
			ProgressBar shieldBar = null;
			ListViewItem tempUnitItem = deployUnitList.SelectedItem as ListViewItem;
			DeployableCraft tempMech = tempUnitItem.Content as DeployableCraft;

			switch (deployingUnitSlot)
			{
				case -1:
					break;
				case 0:
					unitImg = userDeployedImg0;
					healthBar = userDeployedHealthBar0;
					shieldBar = userDeployedShieldBar0;
					userDeployedUnit0 = false;
					userDeployedUnit0Action = -1;
					userDeployedActionImg = userDeployedActionImg0;
					break;
				case 1:
					unitImg = userDeployedImg1;
					healthBar = userDeployedHealthBar1;
					shieldBar = userDeployedShieldBar1;
					userDeployedUnit1 = false;
					userDeployedUnit1Action = -1;
					userDeployedActionImg = userDeployedActionImg1;
					break;
				case 2:
					unitImg = userDeployedImg2;
					healthBar = userDeployedHealthBar2;
					shieldBar = userDeployedShieldBar2;
					userDeployedUnit2 = false;
					userDeployedUnit2Action = -1;
					userDeployedActionImg = userDeployedActionImg2;
					break;
			}

			Utility.setImage(unitImg, transparentDeployableOverlay);
			Utility.setProgressBarInitial(healthBar, 0, 0);
			Utility.setProgressBarInitial(shieldBar, 0, 0);
			Utility.toggleProgressBarVisibility(healthBar, true);
			Utility.toggleProgressBarVisibility(shieldBar, true);
			Utility.setImage(userDeployedActionImg, transparentDeployableOverlay);
		}
		public void UpdateShields()
		{
			Utility.setProgressBarValue(enemyShieldBar, MainWindow.enemyShip.ShieldCurrent);
			Utility.setProgressBarValue(userShieldBar, MainWindow.userShip.ShieldCurrent);
		}
		public void UpdateHulls()
		{
			Utility.setProgressBarValue(enemyHealthBar, MainWindow.enemyShip.HullCurrent);
			Utility.setProgressBarValue(userHealthBar, MainWindow.userShip.HullCurrent);
		}

		// Commands
		private void userDeployedBttn0_Click(object sender, RoutedEventArgs e)
		{
			if (userDeployedUnit0 == true)
			{
				Utility.setMenuVisiblity(userDeployableMenu0);
			}

			else
			{
				deployingUnitSlot = 0;
				userDeployedBttn0.ToolTip = "Set Unit Mode";
				deployUnitPanel.Visibility = Visibility.Visible;
			}
		}
		private void userDeployed0Attack_Click(object sender, RoutedEventArgs e)
		{
			Utility.setMenuVisiblity(userDeployableMenu0);
			setDeployedAction(0, "attack");
		}
		private void userDeployed0Intercept_Click(object sender, RoutedEventArgs e)
		{
			Utility.setMenuVisiblity(userDeployableMenu0);
			setDeployedAction(0, "intercept");
		}
		private void userDeployed0Defend_Click(object sender, RoutedEventArgs e)
		{
			Utility.setMenuVisiblity(userDeployableMenu0);
			setDeployedAction(0, "defend");
		}

		private void userDeployedBttn1_Click(object sender, RoutedEventArgs e)
		{
			if (userDeployedUnit1 == true)
			{
				Utility.setMenuVisiblity(userDeployableMenu1);
			}

			else
			{
				deployingUnitSlot = 1;
				userDeployedBttn1.ToolTip = "Set Unit Mode";
				deployUnitPanel.Visibility = Visibility.Visible;
			}
		}
		private void userDeployed1Attack_Click(object sender, RoutedEventArgs e)
		{
			Utility.setMenuVisiblity(userDeployableMenu1);
			setDeployedAction(1, "attack");
		}
		private void userDeployed1Intercept_Click(object sender, RoutedEventArgs e)
		{
			Utility.setMenuVisiblity(userDeployableMenu1);
			setDeployedAction(1, "intercept");
		}
		private void userDeployed1Defend_Click(object sender, RoutedEventArgs e)
		{
			Utility.setMenuVisiblity(userDeployableMenu1);
			setDeployedAction(1, "defend");
		}

		private void userDeployedBttn2_Click(object sender, RoutedEventArgs e)
		{
			if (userDeployedUnit2 == true)
			{
				Utility.setMenuVisiblity(userDeployableMenu2);
			}

			else
			{
				deployingUnitSlot = 2;
				userDeployedBttn2.ToolTip = "Set Unit Mode";
				deployUnitPanel.Visibility = Visibility.Visible;
			}
		}
		private void userDeployed2Attack_Click(object sender, RoutedEventArgs e)
		{
			Utility.setMenuVisiblity(userDeployableMenu2);
			setDeployedAction(2, "attack");
		}
		private void userDeployed2Intercept_Click(object sender, RoutedEventArgs e)
		{
			Utility.setMenuVisiblity(userDeployableMenu2);
			setDeployedAction(2, "intercept");
		}
		private void userDeployed2Defend_Click(object sender, RoutedEventArgs e)
		{
			Utility.setMenuVisiblity(userDeployableMenu2);
			setDeployedAction(2, "defend");
		}

		private void deployUnit_Click(object sender, RoutedEventArgs e)
		{
			deployUnit();
			deployUnitPanel.Visibility = Visibility.Hidden;
		}
		private void cancelDeployUnit_Click(object sender, RoutedEventArgs e)
		{
			deployingUnitSlot = -1;
			deployUnitPanel.Visibility = Visibility.Hidden;
		}

		private void engageBttn_Click(object sender, RoutedEventArgs e)
		{
			Mechanics.CommitTurn();
		}
		private void retreatBttn_Click(object sender, RoutedEventArgs e)
		{

		}

		private void userRecallBttn0_Click(object sender, RoutedEventArgs e)
		{
			if (userDeployedUnit0 == true)
			{
				Utility.setMenuVisiblity(userDeployableMenu0, true);
				deployingUnitSlot = 0;
				userDeployedBttn0.ToolTip = "Deploy Unit";
				deployUnitPanel.Visibility = Visibility.Hidden;
				recallUnit();
			}
		}
		private void userRecallBttn1_Click(object sender, RoutedEventArgs e)
		{
			if (userDeployedUnit1 == true)
			{
				Utility.setMenuVisiblity(userDeployableMenu1, true);
				deployingUnitSlot = 1;
				userDeployedBttn1.ToolTip = "Deploy Unit";
				deployUnitPanel.Visibility = Visibility.Hidden;
				recallUnit();
			}
		}
		private void userRecallBttn2_Click(object sender, RoutedEventArgs e)
		{
			if (userDeployedUnit2 == true)
			{
				Utility.setMenuVisiblity(userDeployableMenu2, true);
				deployingUnitSlot = 2;
				userDeployedBttn2.ToolTip = "Deploy Unit";
				deployUnitPanel.Visibility = Visibility.Hidden;
				recallUnit();
			}
		}
	}
}
