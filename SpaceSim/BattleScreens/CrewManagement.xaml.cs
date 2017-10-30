using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;
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
	public partial class CrewManagement : Window
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private static string unFilledCrewImgName = "crewUnfilled.png";
		private static string unFilledCrewImg = "/data\\ui\\crew\\" + unFilledCrewImgName;
		private static string filledCrewImgName = "crewFilled.png";
		private static string filledCrewImg = "/data\\ui\\crew\\" + filledCrewImgName;
		private static string injuredCrewImgName = "crewInjured.png";
		private static string injuredCrewImg = "/data\\ui\\crew\\" + injuredCrewImgName;
		private static string plusImage = "/data\\ui\\crew\\plus.png";
		private static string minusImage = "/data\\ui\\crew\\minus.png";
		private static Image[] unassignedCrew = new Image[10];
		private static Image[] bridgeCrew = new Image[2];
		private static Image[] engineCrew = new Image[4];
		private static Image[] weaponCrew = new Image[4];
		private static Image[] flightCrew = new Image[3];
		private static Image[] reactorCrew = new Image[3];
		private static Image[] medicalCrew = new Image[2];
		private static Image[] repairCrew = new Image[4];
		private static string bridgeCrewTooltip;
		private static string engineCrewTooltip;
		private static string weaponCrewTooltip;
		private static string flightCrewTooltip;
		private static string reactorCrewTooltip;
		private static string medicalCrewTooltip;
		private static string repairCrewTooltip = "+- crew to repairs.";

		int availableCrewCount = 0;
		private int _injuredCrewCount = 0;

		public CrewManagement()
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
				CrewManagementInitizliations();

				var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
				this.Left = desktopWorkingArea.Left + 10;
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
		private void CrewManagementInitizliations()
		{
			tooltipSetup();
			crewArraysSetup();
			initialImageSetup();

			updateCrewPlusMinus();
		}

		public void HealCrewMember()
		{
			updateCrewImages(unassignedCrew, false, true);
		}
		private void tooltipSetup()
		{
			bridgeCrewTooltip = "+-" + MainWindow.thisCrewModifiers.BridgeCrew.ToString() + "% percentage bonus to every category.";
			bridgeCrewMinus.ToolTip = bridgeCrewTooltip;
			bridgeCrewPlus.ToolTip = bridgeCrewTooltip;

			engineCrewTooltip = "+-" + MainWindow.thisCrewModifiers.EngineCrew.ToString() + "% percentage bonus to evasion.";
			engineCrewMinus.ToolTip = engineCrewTooltip;
			engineCrewPlus.ToolTip = engineCrewTooltip;

			weaponCrewTooltip = "+-" + MainWindow.thisCrewModifiers.WeaponCrew.ToString() + "% percentage bonus to accuracy.";
			weaponCrewMinus.ToolTip = weaponCrewTooltip;
			weaponCrewPlus.ToolTip = weaponCrewTooltip;

			flightCrewTooltip = "+-" + MainWindow.thisCrewModifiers.FlightCrew.ToString() + "% percentage bonus to deployable units.";
			flightCrewMinus.ToolTip = flightCrewTooltip;
			flightCrewPlus.ToolTip = flightCrewTooltip;

			reactorCrewTooltip = "+-" + MainWindow.thisCrewModifiers.ReactorCrew.ToString() + "% percentage bonus to energy regen.";
			reactorCrewMinus.ToolTip = reactorCrewTooltip;
			reactorCrewPlus.ToolTip = reactorCrewTooltip;

			medicalCrewTooltip = "+-" + MainWindow.thisCrewModifiers.MedicalCrew.ToString() + "% flat percentage bonus to crew recovery.";
			medicalCrewMinus.ToolTip = medicalCrewTooltip;
			medicalCrewPlus.ToolTip = medicalCrewTooltip;

			repairCrewMinus.ToolTip = repairCrewTooltip;
			repairCrewPlus.ToolTip = repairCrewTooltip;
		}
		// Initial crew stations setup.
		private void crewArraysSetup()
		{
			unassignedCrew[0] = freeCrew0img;
			unassignedCrew[1] = freeCrew1img;
			unassignedCrew[2] = freeCrew2img;
			unassignedCrew[3] = freeCrew3img;
			unassignedCrew[4] = freeCrew4img;
			unassignedCrew[5] = freeCrew5img;
			unassignedCrew[6] = freeCrew6img;
			unassignedCrew[7] = freeCrew7img;
			unassignedCrew[8] = freeCrew8img;
			unassignedCrew[9] = freeCrew9img;

			bridgeCrew[0] = bridgeCrew0Img;
			bridgeCrew[1] = bridgeCrew1Img;

			engineCrew[0] = engineCrew0Img;
			engineCrew[1] = engineCrew1Img;
			engineCrew[2] = engineCrew2Img;
			engineCrew[3] = engineCrew3Img;

			weaponCrew[0] = weaponCrew0Img;
			weaponCrew[1] = weaponCrew1Img;
			weaponCrew[2] = weaponCrew2Img;
			weaponCrew[3] = weaponCrew3Img;

			flightCrew[0] = flightCrew0Img;
			flightCrew[1] = flightCrew1Img;
			flightCrew[2] = flightCrew2Img;

			reactorCrew[0] = reactorCrew0Img;
			reactorCrew[1] = reactorCrew1Img;
			reactorCrew[2] = reactorCrew2Img;

			medicalCrew[0] = medicalCrew0Img;
			medicalCrew[1] = medicalCrew1Img;

			repairCrew[0] = repairCrew0Img;
			repairCrew[1] = repairCrew1Img;
			repairCrew[2] = repairCrew2Img;
			repairCrew[3] = repairCrew3Img;
		}
		private void initialImageSetup()
		{
			availableCrewCount = MainWindow.userShip.CrewCurrent;
			int remainingAvailableCrew;
			int c;

			MainWindow.userShip.bridgeCrewCount = setInitialCrewCount(bridgeCrew0Img, bridgeCrew, MainWindow.userShip.bridgeCrewCount);
			MainWindow.userShip.engineCrewCount = setInitialCrewCount(engineCrew0Img, engineCrew, MainWindow.userShip.engineCrewCount);
			MainWindow.userShip.reactorCrewCount = setInitialCrewCount(reactorCrew0Img, reactorCrew, MainWindow.userShip.reactorCrewCount);
			setToUnfilledCrew(weaponCrew);
			setToUnfilledCrew(flightCrew);
			setToUnfilledCrew(medicalCrew);
			setToUnfilledCrew(repairCrew);

			remainingAvailableCrew = availableCrewCount;
			c = 0;
			while (c < unassignedCrew.Length)
			{
				if (remainingAvailableCrew > 0)
				{
					Utility.setImage(unassignedCrew[c], filledCrewImg);
					remainingAvailableCrew -= 1;
					c++;
				}
				else if (_injuredCrewCount < MainWindow.userShip.injuredCrewCount)
				{
					Utility.setImage(unassignedCrew[c], injuredCrewImg);
					c++;
					_injuredCrewCount++;
				}
				else
				{
					Utility.setImage(unassignedCrew[c], unFilledCrewImg);
					c++;
				}
			}
			Utility.setImage(bridgeCrewPlusImage, plusImage);
			Utility.setImage(bridgeCrewMinusImage, minusImage);
			Utility.setImage(engineCrewPlusImage, plusImage);
			Utility.setImage(engineCrewMinusImage, minusImage);
			Utility.setImage(reactorCrewPlusImage, plusImage);
			Utility.setImage(reactorCrewMinusImage, minusImage);
			Utility.setImage(weaponCrewPlusImage, plusImage);
			Utility.setImage(weaponCrewMinusImage, minusImage);
			Utility.setImage(flightCrewPlusImage, plusImage);
			Utility.setImage(flightCrewMinusImage, minusImage);
			Utility.setImage(medicalCrewPlusImage, plusImage);
			Utility.setImage(medicalCrewMinusImage, minusImage);
			Utility.setImage(repairCrewPlusImage, plusImage);
			Utility.setImage(repairCrewMinusImage, minusImage);
		}
		private int setInitialCrewCount(Image tempImage, Image[] tempArray, int tempCrewCount)
		{
			int c = 0;

			if (availableCrewCount > 0)
			{
				Utility.setImage(tempImage, filledCrewImg);
				availableCrewCount -= 1;
				tempCrewCount += 1;
				c = 1;
			}

			while (c < tempArray.Length)
			{
				Utility.setImage(tempArray[c], unFilledCrewImg);
				c++;
			}

			return tempCrewCount;
		}
		private void setToUnfilledCrew(Image[] tempArray)
		{
			for (int c = 0; c < tempArray.Length; c++)
			{
				Utility.setImage(tempArray[c], unFilledCrewImg);
			}
		}

		// Logic for adding or removing crew to stations.
		private void updateCrewCount(object sender)
		{
			var tempButton = sender as Button;

			switch (tempButton.Name)
			{
				case "bridgeCrewMinus":
					updateCrewImages(bridgeCrew);
					MainWindow.userShip.bridgeCrewCount -= 1;
					availableCrewCount += 1;
					updateCrewImages(unassignedCrew, true);
					MainWindow.thisPowerManagement.UpdateEnergyRegen();
					MainWindow.thisBattleArmory.UpdateAccuracy();
					break;
				case "bridgeCrewPlus":
					updateCrewImages(bridgeCrew, true);
					MainWindow.userShip.bridgeCrewCount += 1;
					availableCrewCount -= 1;
					updateCrewImages(unassignedCrew);
					MainWindow.thisPowerManagement.UpdateEnergyRegen();
					MainWindow.thisBattleArmory.UpdateAccuracy();
					break;
				case "engineCrewMinus":
					updateCrewImages(engineCrew);
					MainWindow.userShip.engineCrewCount -= 1;
					availableCrewCount += 1;
					updateCrewImages(unassignedCrew, true);
					break;
				case "engineCrewPlus":
					updateCrewImages(engineCrew, true);
					MainWindow.userShip.engineCrewCount += 1;
					availableCrewCount -= 1;
					updateCrewImages(unassignedCrew);
					break;
				case "reactorCrewMinus":
					updateCrewImages(reactorCrew);
					MainWindow.userShip.reactorCrewCount -= 1;
					availableCrewCount += 1;
					updateCrewImages(unassignedCrew, true);
					MainWindow.thisPowerManagement.UpdateEnergyRegen();
					break;
				case "reactorCrewPlus":
					updateCrewImages(reactorCrew, true);
					MainWindow.userShip.reactorCrewCount += 1;
					availableCrewCount -= 1;
					updateCrewImages(unassignedCrew);
					MainWindow.thisPowerManagement.UpdateEnergyRegen();
					break;
				case "weaponCrewMinus":
					updateCrewImages(weaponCrew);
					MainWindow.userShip.weaponCrewCount -= 1;
					availableCrewCount += 1;
					updateCrewImages(unassignedCrew, true);
					MainWindow.thisBattleArmory.UpdateAccuracy();
					break;
				case "weaponCrewPlus":
					updateCrewImages(weaponCrew, true);
					MainWindow.userShip.weaponCrewCount += 1;
					availableCrewCount -= 1;
					updateCrewImages(unassignedCrew);
					MainWindow.thisBattleArmory.UpdateAccuracy();
					break;
				case "flightCrewMinus":
					updateCrewImages(flightCrew);
					MainWindow.userShip.flightCrewCount -= 1;
					availableCrewCount += 1;
					updateCrewImages(unassignedCrew, true);
					break;
				case "flightCrewPlus":
					updateCrewImages(flightCrew, true);
					MainWindow.userShip.flightCrewCount += 1;
					availableCrewCount -= 1;
					updateCrewImages(unassignedCrew);
					break;
				case "medicalCrewMinus":
					updateCrewImages(medicalCrew);
					MainWindow.userShip.medicalCrewCount -= 1;
					availableCrewCount += 1;
					updateCrewImages(unassignedCrew, true);
					MainWindow.thisPowerManagement.ToggleMedicalSlider();
					break;
				case "medicalCrewPlus":
					updateCrewImages(medicalCrew, true);
					MainWindow.userShip.medicalCrewCount += 1;
					availableCrewCount -= 1;
					updateCrewImages(unassignedCrew);
					MainWindow.thisPowerManagement.ToggleMedicalSlider();
					break;
				case "repairCrewMinus":
					updateCrewImages(repairCrew);
					MainWindow.userShip.repairCrewCount -= 1;
					availableCrewCount += 1;
					updateCrewImages(unassignedCrew, true);
					RepairManagement.UpdateRepairCrewImgs();
					break;
				case "repairCrewPlus":
					updateCrewImages(repairCrew, true);
					MainWindow.userShip.repairCrewCount += 1;
					availableCrewCount -= 1;
					updateCrewImages(unassignedCrew);
					RepairManagement.UpdateRepairCrewImgs();
					break;
			}

			updateCrewPlusMinus();
			MainWindow.thisPowerManagement.UpdateModifiedValues();
		}

		private void updateCrewPlusMinus()
		{
			updateCrewButtons(bridgeCrewPlus, bridgeCrewMinus, bridgeCrew, MainWindow.userShip.bridgeCrewCount);
			updateCrewButtons(engineCrewPlus, engineCrewMinus, engineCrew, MainWindow.userShip.engineCrewCount);
			updateCrewButtons(reactorCrewPlus, reactorCrewMinus, reactorCrew, MainWindow.userShip.reactorCrewCount);
			updateCrewButtons(weaponCrewPlus, weaponCrewMinus, weaponCrew, MainWindow.userShip.weaponCrewCount);
			updateCrewButtons(flightCrewPlus, flightCrewMinus, flightCrew, MainWindow.userShip.flightCrewCount);
			updateCrewButtons(medicalCrewPlus, medicalCrewMinus, medicalCrew, MainWindow.userShip.medicalCrewCount);
			updateCrewButtons(repairCrewPlus, repairCrewMinus, repairCrew, MainWindow.userShip.repairCrewCount);
		}
		private void updateCrewImages(Image[] tempArray, bool add = false, bool injured = false)
		{
			for (int c = 0; c < tempArray.Length; c++)
			{
				int nextC = c + 1;

				if (add)
				{
					if (tempArray[c].Source.ToString().Contains(unFilledCrewImgName))
					{
						if (!injured)
						{
							Utility.setImage(tempArray[c], filledCrewImg);
							break;
						}
					}
				}

				else if (injured)
				{
					if (tempArray[c].Source.ToString().Contains(injuredCrewImg))
					{
						if (nextC < tempArray.Length)
						{
							if (tempArray[nextC].Source.ToString().Contains(unFilledCrewImgName) || tempArray[nextC] == null)
							{
								Utility.setImage(tempArray[c], filledCrewImg);
								break;
							}
						}
					}
				}

				else
				{
					if (tempArray[c].Source.ToString().Contains(filledCrewImgName))
					{
						if (nextC < tempArray.Length)
						{
							if (tempArray[nextC].Source.ToString().Contains(unFilledCrewImgName) || tempArray[nextC].Source.ToString().Contains(injuredCrewImgName))
							{
								Utility.setImage(tempArray[c], unFilledCrewImg);
								break;
							}
						}
						else
						{
							if (!tempArray[c].Source.ToString().Contains(injuredCrewImgName))
							{
								Utility.setImage(tempArray[c], unFilledCrewImg);
								break;
							}
						}
					}
				}
			}
		}
		private void updateCrewButtons(Button tempButtonPlus, Button tempButtonMinus, Image[] tempArray, int tempCount)
		{
			if (tempCount == tempArray.Length)
			{
				tempButtonPlus.IsEnabled = false;
				tempButtonMinus.IsEnabled = true;
			}

			else if (tempCount < tempArray.Length && tempCount != 0)
			{
				if (availableCrewCount > 0)
				{
					tempButtonPlus.IsEnabled = true;
				}
				else
				{
					tempButtonPlus.IsEnabled = false;
				}

				tempButtonMinus.IsEnabled = true;
			}

			if (tempCount == 0)
			{
				if (availableCrewCount > 0)
				{
					tempButtonPlus.IsEnabled = true;
				}
				else
				{
					tempButtonPlus.IsEnabled = false;

				}
				tempButtonMinus.IsEnabled = false;
			}
		}

		private void crewMinus_Click(object sender, RoutedEventArgs e)
		{
			updateCrewCount(sender);
		}
		private void crewPlus_Click(object sender, RoutedEventArgs e)
		{
			updateCrewCount(sender);
		}
	}
}
