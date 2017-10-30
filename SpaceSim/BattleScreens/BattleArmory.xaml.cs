using System;
using System.Collections.Generic;
using System.ComponentModel;
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
	public partial class BattleArmory : Window
	{
		public BattleArmory()
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
				BattleArmoryInitilizations();

				var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
				//this.Left = MainWindow.powerManagementLocation.X + MainWindow.thisPowerManagement.Width;
				//this.Top = desktopWorkingArea.Top + 10;
				this.Left = MainWindow.battleCrewLocation.X + MainWindow.thisBattleCrew.Width;
				this.Top = desktopWorkingArea.Top + 10;
				this.Activate();
				this.Topmost = true;
				this.Topmost = false;
				this.Focus();
			}
			catch (Exception ex)
			{

			}
		}
		private void BattleArmoryInitilizations()
		{
			weaponList.ItemsSource = MainWindow.userShip.Weapons;
			target_setup();
			UpdateOrdnance();
		}

		private void target_setup()
		{
			Utility.setImage(armoryTargetBackgroundImg, "/data\\ui\\armory\\armoryTargeterBackground.png");
			Utility.setImage(armoryTargetShipImg, "/data\\ui\\armory\\armoryTargeterBaseShip.png");
			Utility.setImage(armoryTargetBridgeImg, "/data\\ui\\armory\\armoryTargeterBridge.png");
			Utility.setImage(armoryTargetEngineImg, "/data\\ui\\armory\\armoryTargeterEngine.png");
			Utility.setImage(armoryTargetLandingBayImg, "/data\\ui\\armory\\armoryTargeterLandingBay.png");
			Utility.setImage(armoryTargetAftTurretsImg, "/data\\ui\\armory\\armoryTargeterAftTurrets.png");
			Utility.setImage(armoryTargetForeTurretsImg, "/data\\ui\\armory\\armoryTargeterForeTurrets.png");
			Utility.setImage(armoryTargetBottomTurretsImg, "/data\\ui\\armory\\armoryTargeterBottomTurrets.png");
		}
		private void showTarget(Weapon tempWeapon)
		{
			hideTargetting();

			switch (tempWeapon.Target)
			{
				case "Bridge":
					targetBridge();
					break;
				case "Engine":
					targetEngine();
					break;
				case "Fore Turrets":
					targetForeTurrets();
					break;
				case "Aft Turrets":
					targetAftTurrets();
					break;
				case "Bottom Turrets":
					targetBottomTurrets();
					break;
				case "Landing Bay":
					targetLandingBay();
					break;
				case "Hull":
					targetHull();
					break;
			}
		}

		private void hideTargetting()
		{
			bridgeBttn.Content = "";
			engineBttn.Content = "";
			foreTurretsBttn.Content = "";
			aftTurretsBttn.Content = "";
			bottomTurretsBttn.Content = "";
			landingBayBttn.Content = "";
			hullBttn.Content = "";
		}
		private void targetBridge()
		{
			bridgeBttn.Content = this.Resources["TargettingImage"];

			targettingLbl.Content = ((Weapon)weaponList.SelectedItem).Target = "Bridge";
			MainWindow.userShip.Weapons[(weaponList.SelectedIndex)].Target = "Bridge";
			refresh_WeaponsList();
		}
		private void targetEngine()
		{
			engineBttn.Content = this.Resources["TargettingImage"];

			targettingLbl.Content = ((Weapon)weaponList.SelectedItem).Target = "Engine";
			MainWindow.userShip.Weapons[(weaponList.SelectedIndex)].Target = "Engine";
			refresh_WeaponsList();
		}
		private void targetForeTurrets()
		{
			foreTurretsBttn.Content = this.Resources["TargettingImage"];

			targettingLbl.Content = ((Weapon)weaponList.SelectedItem).Target = "Fore Turrets";
			MainWindow.userShip.Weapons[(weaponList.SelectedIndex)].Target = "Fore Turrets";
			refresh_WeaponsList();
		}
		private void targetAftTurrets()
		{
			aftTurretsBttn.Content = this.Resources["TargettingImage"];

			targettingLbl.Content = ((Weapon)weaponList.SelectedItem).Target = "Aft Turrets";
			MainWindow.userShip.Weapons[(weaponList.SelectedIndex)].Target = "Aft Turrets";
			refresh_WeaponsList();
		}
		private void targetBottomTurrets()
		{
			bottomTurretsBttn.Content = this.Resources["TargettingImage"];

			targettingLbl.Content = ((Weapon)weaponList.SelectedItem).Target = "Bottom Turrets";
			MainWindow.userShip.Weapons[(weaponList.SelectedIndex)].Target = "Bottom Turrets";
			refresh_WeaponsList();
		}
		private void targetLandingBay()
		{
			landingBayBttn.Content = this.Resources["TargettingImage"];

			targettingLbl.Content = ((Weapon)weaponList.SelectedItem).Target = "Landing Bay";
			MainWindow.userShip.Weapons[(weaponList.SelectedIndex)].Target = "Landing Bay";
			refresh_WeaponsList();
		}
		private void targetHull()
		{
			hullBttn.Content = this.Resources["TargettingImage"];

			targettingLbl.Content = ((Weapon)weaponList.SelectedItem).Target = "Hull";
			MainWindow.userShip.Weapons[(weaponList.SelectedIndex)].Target = "Hull";
			refresh_WeaponsList();
		}
		private void refresh_WeaponsList()
		{
			ICollectionView view = CollectionViewSource.GetDefaultView(weaponList.ItemsSource);
			view.Refresh();
		}
		private void clear_Target()
		{
			accuracyLbl.Content = "";
			hideTargetting();
			targettingLbl.Content = ((Weapon)weaponList.SelectedItem).Target = "";
			MainWindow.userShip.Weapons[(weaponList.SelectedIndex)].Target = "";
			refresh_WeaponsList();
		}
		public void UpdateAccuracy(string subTarget = null)
		{
			if ((Weapon)weaponList.SelectedItem != null)
			{
				if (((Weapon)weaponList.SelectedItem).Target != null && ((Weapon)weaponList.SelectedItem).Target != "")
				{
					accuracyLbl.Content = Mechanics.CalculateAccuracy(MainWindow.userShip, MainWindow.userCaptain, (Weapon)weaponList.SelectedItem, subTarget);
				}
			}
		}
		public void UpdateOrdnance()
		{
			ordnanceBar.Maximum = MainWindow.userShip.OrdnanceMax;
			ordnanceBar.Value = MainWindow.userShip.OrdnanceCurrent;

			UpdateWeapons(MainWindow.userShip);
		}
		public void UpdateWeapons(Ship targetShip)
		{
			foreach (Weapon tempWeapon in targetShip.Weapons)
			{
				if (tempWeapon.OrdnanceCost > targetShip.OrdnanceCurrent)
				{
					ToggleWeapon(tempWeapon, false);
				}
				else
				{
					ToggleWeapon(tempWeapon, true);
				}
			}
		}
		public void ToggleWeapon(Weapon targetWeapon, bool enabledState, bool clearTarget = false)
		{
			for (int i = 0; i < weaponList.Items.Count; i++) 
			{
				var tempWeapon = weaponList.Items[i] as Weapon;
				if (tempWeapon == targetWeapon) 
				{
					weaponList.UpdateLayout();
					weaponList.ScrollIntoView(weaponList.Items[i]);

					ListViewItem item = (ListViewItem)weaponList.ItemContainerGenerator.ContainerFromIndex(i);

					if (item != null)
					{
						if (clearTarget == true)
						{
							item.IsSelected = true;
							clear_Target();
						}

						item.IsSelected = false;
						item.IsEnabled = enabledState;
						item.IsHitTestVisible = enabledState;
						//TRAP $TODO$ This needs to actually disable the entries that are out of range.
					}
				} 
			} 
		}
		public void ToggleWeaponsByRange()
		{
			for (int i = 0; i < weaponList.Items.Count; i++) 
			{
				var tempWeapon = weaponList.Items[i] as Weapon;

				if (tempWeapon.Range < MainWindow.currentRange)
				{
					ToggleWeapon(tempWeapon, false, true);
				}
			}

			refresh_WeaponsList();
			weaponList.UpdateLayout();
		}

		private void target_bridge_Click(object sender, RoutedEventArgs e)
		{
			if (((Weapon)weaponList.SelectedItem) != null)
			{
				if (((Weapon)weaponList.SelectedItem).Target == "Bridge")
				{
					clear_Target();
				}

				else
				{
					if (((Weapon)weaponList.SelectedItem).Range >= MainWindow.currentRange)
					{
						hideTargetting();
						targetBridge();
						UpdateAccuracy("Bridge");
					}
				}
			}
		}
		private void target_engine_Click(object sender, RoutedEventArgs e)
		{
			if (((Weapon)weaponList.SelectedItem) != null)
			{
				if (((Weapon)weaponList.SelectedItem).Target == "Engine")
				{
					clear_Target();
				}

				else
				{
					if (((Weapon)weaponList.SelectedItem).Range >= MainWindow.currentRange)
					{
						hideTargetting();
						targetEngine();
						UpdateAccuracy("Engine");
					}
				}
			}
		}
		private void target_foreTurrets_Click(object sender, RoutedEventArgs e)
		{
			if (((Weapon)weaponList.SelectedItem) != null)
			{
				if (((Weapon)weaponList.SelectedItem).Target == "Fore Turrets")
				{
					clear_Target();
				}

				else
				{
					if (((Weapon)weaponList.SelectedItem).Range >= MainWindow.currentRange)
					{
						hideTargetting();
						targetForeTurrets();
						UpdateAccuracy("Fore Turrets");
					}
				}
			}
		}
		private void target_aftTurrets_Click(object sender, RoutedEventArgs e)
		{
			if (((Weapon)weaponList.SelectedItem) != null)
			{
				if (((Weapon)weaponList.SelectedItem).Target == "Aft Turrets")
				{
					clear_Target();
				}

				else
				{
					if (((Weapon)weaponList.SelectedItem).Range >= MainWindow.currentRange)
					{
						hideTargetting();
						targetAftTurrets();
						UpdateAccuracy("Aft Turrets");
					}
				}
			}
		}
		private void target_bottomTurrets_Click(object sender, RoutedEventArgs e)
		{
			if (((Weapon)weaponList.SelectedItem) != null)
			{
				if (((Weapon)weaponList.SelectedItem).Target == "Bottom Turrets")
				{
					clear_Target();
				}

				else
				{
					if (((Weapon)weaponList.SelectedItem).Range >= MainWindow.currentRange)
					{
						hideTargetting();
						targetBottomTurrets();
						UpdateAccuracy("Bottom Turrets");
					}
				}
			}
		}
		private void target_landingBay_Click(object sender, RoutedEventArgs e)
		{
			if (((Weapon)weaponList.SelectedItem) != null)
			{
				if (((Weapon)weaponList.SelectedItem).Target == "Landing Bay")
				{
					clear_Target();
				}

				else
				{
					if (((Weapon)weaponList.SelectedItem).Range >= MainWindow.currentRange)
					{
						hideTargetting();
						targetLandingBay();
						UpdateAccuracy("Landing Bay");
					}
				}
			}
		}
		private void target_hull_Click(object sender, RoutedEventArgs e)
		{
			if(((Weapon)weaponList.SelectedItem) != null)
			{
				if (((Weapon)weaponList.SelectedItem).Target == "Hull")
				{
					clear_Target();
				}

				else
				{
					if (((Weapon)weaponList.SelectedItem).Range >= MainWindow.currentRange)
					{
						hideTargetting();
						targetHull();
						UpdateAccuracy();
					}
				}
			}
		}
		private void info_Click(object sender, RoutedEventArgs e)
		{
			//TRAP TODO
		}
		private void comms_Click(object sender, RoutedEventArgs e)
		{
			if (MainWindow.thisComms.Visibility == Visibility.Hidden || MainWindow.thisComms.Visibility == Visibility.Collapsed)
			{
				MainWindow.ShowCommsWindow();
			}
			else if (MainWindow.thisComms.Visibility == Visibility.Visible)
			{
				MainWindow.HideCommsWindow();
			}
		}
		private void weaponList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (weaponList.SelectedItem != null)
			{
				var tempWeapon = weaponList.SelectedItem as Weapon;
				targettingLbl.Content = tempWeapon.Target;
				showTarget(tempWeapon);
				UpdateAccuracy(tempWeapon.Target);
			}
			else
			{
				hideTargetting();
			}
		}
	}
}
