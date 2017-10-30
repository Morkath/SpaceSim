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
	/// <summary>
	/// Interaction logic for RepairManagement.xaml
	/// </summary>
	public partial class RepairManagement : Window
	{
		private static Button[] buttonArray = new Button[18];
		private static Image[] repairCrewArray = new Image[4];
		private static string unFilledCrewImgName = "crewUnfilled.png";
		private static string unFilledCrewImg = "/data\\ui\\crew\\" + unFilledCrewImgName;
		private static string filledCrewImgName = "crewFilled.png";
		private static string filledCrewImg = "/data\\ui\\crew\\" + filledCrewImgName;
		private static string injuredCrewImgName = "crewInjured.png";
		private static string injuredCrewImg = "/data\\ui\\crew\\" + injuredCrewImgName;
		private static string fireImg = "/data\\ui\\repair\\fire.png";
		private static string airImg = "/data\\ui\\repair\\O2.png";
		private static string hullImg = "/data\\ui\\repair\\hullBreach.png";
		private static string wrenchImgName = "wrench.png";
		private static string wrenchImg = "/data\\ui\\repair\\" + wrenchImgName;
		private static int repairCrews = 0;

		public RepairManagement()
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
				repairManagementInitilizations();

				var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
				//this.Left = MainWindow.battleCrewLocation.X;
				//this.Top = MainWindow.battleCrewLocation.Y + MainWindow.thisBattleCrew.Height;
				this.Left = MainWindow.powerManagementLocation.X + MainWindow.thisPowerManagement.Width;
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
		private void repairManagementInitilizations()
		{
			setupArrays();
			setInitialImages();
			airBar.Minimum = 0;
			airBar.Maximum = MainWindow.userShip.AirMax;
			airBar.Value = MainWindow.userShip.AirMax;
		}

		private void updateButton(object sender)
		{
			var tempButton = sender as Button;
			updateButtonLogic(tempButton);
		}
		private void updateButton(Button tempButton)
		{
			updateButtonLogic(tempButton);
		}
		private void updateButtonLogic(Button tempButton)
		{
			var tempImage = tempButton.Content as Image;

			if (tempButton.IsEnabled == true && !tempImage.Source.ToString().Contains(filledCrewImgName))
			{
				if(repairCrews > 0)
				{
					Utility.setImage(tempImage, filledCrewImg);
					repairCrews -= 1;
					updateRepairCrewImgs();
				}
			}
			else if (tempButton.IsEnabled == true && tempImage.Source.ToString().Contains(filledCrewImgName))
			{
				if (tempButton.Name.Contains("Fire"))
				{
					Utility.setImage(tempImage, fireImg);
					repairCrews += 1;
					updateRepairCrewImgs();
				}
				else if (tempButton.Name.Contains("Hull"))
				{
					Utility.setImage(tempImage, hullImg);
					repairCrews += 1;
					updateRepairCrewImgs();
				}
				else if (tempButton.Name.Contains("Air"))
				{
					Utility.setImage(tempImage, airImg);
					repairCrews += 1;
					updateRepairCrewImgs();
				}
			}
		}

		private void setupArrays()
		{
			buttonArray[0] = bridgeFire;
			buttonArray[1] = bridgeHull;
			buttonArray[2] = bridgeAir;
			buttonArray[3] = engineFire;
			buttonArray[4] = engineHull;
			buttonArray[5] = engineAir;
			buttonArray[6] = weaponFire;
			buttonArray[7] = weaponHull;
			buttonArray[8] = weaponAir;
			buttonArray[9] = flightFire;
			buttonArray[10] = flightHull;
			buttonArray[11] = flightAir;
			buttonArray[12] = reactorFire;
			buttonArray[13] = reactorHull;
			buttonArray[14] = reactorAir;
			buttonArray[15] = medicalFire;
			buttonArray[16] = medicalHull;
			buttonArray[17] = medicalAir;

			repairCrewArray[0] = repairCrew0img;
			repairCrewArray[1] = repairCrew1img;
			repairCrewArray[2] = repairCrew2img;
			repairCrewArray[3] = repairCrew3img;

			//buttonArray[0].IsEnabled = true;
			//buttonArray[13].IsEnabled = true;
			//buttonArray[14].IsEnabled = true;
		}
		private void setInitialImages()
		{
			Utility.setImage(repairCrew0img, unFilledCrewImg);
			Utility.setImage(repairCrew1img, unFilledCrewImg);
			Utility.setImage(repairCrew2img, unFilledCrewImg);
			Utility.setImage(repairCrew3img, unFilledCrewImg);
			resetRepairChoices();
		}
		public static void UpdateRepairCrewImgs()
		{
			repairCrews = MainWindow.userShip.repairCrewCount;
			updateRepairCrewImgs();
			resetRepairChoices();
		}
		private static void resetRepairChoices()
		{
			Utility.setImage(buttonArray[0].Content as Image, fireImg);
			Utility.setImage(buttonArray[1].Content as Image, hullImg);
			Utility.setImage(buttonArray[2].Content as Image, airImg);
			Utility.setImage(buttonArray[3].Content as Image, fireImg);
			Utility.setImage(buttonArray[4].Content as Image, hullImg);
			Utility.setImage(buttonArray[5].Content as Image, airImg);
			Utility.setImage(buttonArray[6].Content as Image, fireImg);
			Utility.setImage(buttonArray[7].Content as Image, hullImg);
			Utility.setImage(buttonArray[8].Content as Image, airImg);
			Utility.setImage(buttonArray[9].Content as Image, fireImg);
			Utility.setImage(buttonArray[10].Content as Image, hullImg);
			Utility.setImage(buttonArray[11].Content as Image, airImg);
			Utility.setImage(buttonArray[12].Content as Image, fireImg);
			Utility.setImage(buttonArray[13].Content as Image, hullImg);
			Utility.setImage(buttonArray[14].Content as Image, airImg);
			Utility.setImage(buttonArray[15].Content as Image, fireImg);
			Utility.setImage(buttonArray[16].Content as Image, hullImg);
			Utility.setImage(buttonArray[17].Content as Image, airImg);
		}
		private static void updateRepairCrewImgs()
		{
			for (int r = 0; r < repairCrewArray.Length; r++)
			{
				if (r < repairCrews)
				{
					Utility.setImage(repairCrewArray[r], filledCrewImg);
				}
				else
				{
					Utility.setImage(repairCrewArray[r], unFilledCrewImg);
				}
			}
		}

		private void bridgeFire_Click(object sender, RoutedEventArgs e)
		{
			updateButton(sender);
		}
		private void bridgeHull_Click(object sender, RoutedEventArgs e)
		{
			updateButton(sender);
		}
		private void bridgeAir_Click(object sender, RoutedEventArgs e)
		{
			updateButton(sender);
		}
		private void engineAir_Click(object sender, RoutedEventArgs e)
		{
			updateButton(sender);
		}
		private void engineHull_Click(object sender, RoutedEventArgs e)
		{
			updateButton(sender);
		}
		private void engineFire_Click(object sender, RoutedEventArgs e)
		{
			updateButton(sender);
		}
		private void weaponFire_Click(object sender, RoutedEventArgs e)
		{
			updateButton(sender);
		}
		private void weaponHull_Click(object sender, RoutedEventArgs e)
		{
			updateButton(sender);
		}
		private void weaponAir_Click(object sender, RoutedEventArgs e)
		{
			updateButton(sender);
		}
		private void flightFire_Click(object sender, RoutedEventArgs e)
		{
			updateButton(sender);
		}
		private void flightHull_Click(object sender, RoutedEventArgs e)
		{
			updateButton(sender);
		}
		private void flightAir_Click(object sender, RoutedEventArgs e)
		{
			updateButton(sender);
		}
		private void reactorFire_Click(object sender, RoutedEventArgs e)
		{
			updateButton(sender);
		}
		private void reactorHull_Click(object sender, RoutedEventArgs e)
		{
			updateButton(sender);
		}
		private void reactorAir_Click(object sender, RoutedEventArgs e)
		{
			updateButton(sender);
		}
		private void medicalFire_Click(object sender, RoutedEventArgs e)
		{
			updateButton(sender);
		}
		private void medicalHull_Click(object sender, RoutedEventArgs e)
		{
			updateButton(sender);
		}
		private void medicalAir_Click(object sender, RoutedEventArgs e)
		{
			updateButton(sender);
		}
	}
}
