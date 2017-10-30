using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SpaceSim
{
	public class FormattedSlider : Slider
	{
		private ToolTip _autoToolTip;
		private string _autoToolTipFormat;
		private string _autoToolTipMultiplier;

		public string AutoToolTipFormat
		{
			get { return _autoToolTipFormat; }
			set { _autoToolTipFormat = value; }
		}

		public string AutoToolTipMultiplier
		{
			get { return _autoToolTipMultiplier; }
			set { _autoToolTipMultiplier = value; }
		}

		protected override void OnThumbDragStarted(DragStartedEventArgs e)
		{
			base.OnThumbDragStarted(e);
			this.FormatAutoToolTipContent();
		}
		protected override void OnThumbDragDelta(DragDeltaEventArgs e)
		{
			base.OnThumbDragDelta(e);
			this.FormatAutoToolTipContent();
		}
		private void FormatAutoToolTipContent()
		{
			if (!string.IsNullOrEmpty(this.AutoToolTipFormat) && string.IsNullOrEmpty(this.AutoToolTipMultiplier))
			{
				this.AutoToolTip.Content = string.Format(
					this.AutoToolTipFormat,
					this.AutoToolTip.Content);
				this.ToolTip = AutoToolTip;
			}
			else if (!string.IsNullOrEmpty(this.AutoToolTipFormat) && !string.IsNullOrEmpty(this.AutoToolTipMultiplier))
			{
				this.AutoToolTip.Content = string.Format(
					this.AutoToolTipFormat,
					Math.Ceiling((Convert.ToDouble(this.AutoToolTip.Content) * Convert.ToDouble(this.AutoToolTipMultiplier))));
				this.ToolTip = AutoToolTip;
			}
		}
		private ToolTip AutoToolTip
		{
			get
			{
				if (_autoToolTip == null)
				{
					FieldInfo field = typeof(Slider).GetField(
						"_autoToolTip",
						System.Reflection.BindingFlags.NonPublic |
						BindingFlags.Instance);
					_autoToolTip = field.GetValue(this) as ToolTip;
				}
				return _autoToolTip;
			}
		}
	}

	public partial class PowerManagement : Window
	{
		private static bool dragStarted = false;
		private static double dragStartedValue;

		private static double shieldMultiplier = 2;
		public double ShieldMultiplier { get { return shieldMultiplier; } }
		private static double hullMultiplier = 1;
		public double HullMultiplier { get { return hullMultiplier; } }
		private static double evasionMultiplier = 0.5;
		public double EvasionMultiplier { get { return evasionMultiplier; } }
		private static double ordnanceMultiplier = 1;
		public double OrdnanceMultiplier { get { return ordnanceMultiplier; } }
		private static double airMultiplier = 1;
		public double AirMultiplier { get { return airMultiplier; } }
		private static double crewMultiplier = 0.25;
		public double CrewMultiplier { get { return crewMultiplier; } }
		private static double battleSpeedMultiplier = 10;
		public double BattleSpeedMultiplier { get { return battleSpeedMultiplier; } }

		public PowerManagement()
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
				powerManagementInitilizations();

				var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
				//this.Left = MainWindow.battleCrewLocation.X + MainWindow.thisBattleCrew.Width;
				//this.Top = desktopWorkingArea.Top + 10;
				this.Left = desktopWorkingArea.Left + 10;
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
		private void powerManagementInitilizations()
		{
			UpdateEnergyBar();
			UpdateEnergyRegen();

			shieldRecoverySlider.AutoToolTipMultiplier = ShieldMultiplier.ToString();
			hullRecoverySlider.AutoToolTipMultiplier = HullMultiplier.ToString();
			evasionRecoverySlider.AutoToolTipMultiplier = EvasionMultiplier.ToString();
			ordnanceRecoverySlider.AutoToolTipMultiplier = OrdnanceMultiplier.ToString();
			airRecoverySlider.AutoToolTipMultiplier = AirMultiplier.ToString();
			crewRecoverySlider.AutoToolTipMultiplier = CrewMultiplier.ToString();
			speedRecoverySlider.AutoToolTipMultiplier = BattleSpeedMultiplier.ToString();

			shieldBaseTB.Text = MainWindow.userShip.ShieldRegen.ToString();
			shieldRecoverySlider.Maximum = MainWindow.userShip.ShieldMaxBonus;
			shieldRecoverySlider.TickFrequency = 5;

			hullBaseTB.Text = MainWindow.userShip.HullRegen.ToString();
			hullRecoverySlider.Maximum = MainWindow.userShip.HullMaxBonus;
			hullRecoverySlider.TickFrequency = 5;

			evasionBaseTB.Text = MainWindow.userShip.Evasion.ToString();
			evasionRecoverySlider.Maximum = MainWindow.userShip.EvasionMaxBonus;
			evasionRecoverySlider.TickFrequency = 5;

			ordnanceBaseTB.Text = MainWindow.userShip.OrdnanceRegen.ToString();
			ordnanceRecoverySlider.Maximum = MainWindow.userShip.OrdnanceMaxBonus;
			ordnanceRecoverySlider.TickFrequency = 1;

			airBaseTB.Text = MainWindow.userShip.AirRegen.ToString();
			airRecoverySlider.Maximum =  MainWindow.userShip.AirMaxBonus;
			airRecoverySlider.TickFrequency = 1;

			crewBaseTB.Text = MainWindow.userShip.CrewRegen.ToString();
			crewRecoverySlider.Maximum = MainWindow.userShip.CrewMaxBonus;
			crewRecoverySlider.TickFrequency = 5;

			speedBaseTB.Text = MainWindow.userShip.BattleSpeed.ToString();
			speedRecoverySlider.Maximum = MainWindow.userShip.BattleSpeedMaxBonus;
			speedRecoverySlider.TickFrequency = 1;

			UpdateModifiedValues();
			ToggleMedicalSlider();
		}

		public void UpdateEnergyRegen()
		{
			energyRegenTB.Text = Mechanics.ReturnEnergyRegen(MainWindow.userShip, MainWindow.userCaptain).ToString();
		}
		public void UpdateEnergyBar()
		{
			updateEnergyBar();
		}
		private static void updateEnergyBar()
		{
			MainWindow.thisPowerManagement.energyBar.Maximum = MainWindow.userShip.EnergyMax;
			MainWindow.thisPowerManagement.energyBar.Value = MainWindow.userShip.EnergyCurrent;
		}
		public void UpdateModifiedValues()
		{
			shieldModifiedTB.Text = Mechanics.ReturnShieldRegen(MainWindow.userShip, MainWindow.userCaptain).ToString();
			hullModifiedTB.Text = Mechanics.ReturnHullRegen(MainWindow.userShip, MainWindow.userCaptain).ToString();
			evasionModifiedTB.Text = Mechanics.ReturnEvasion(MainWindow.userShip, MainWindow.userCaptain).ToString();
			ordnanceModifiedTB.Text = Mechanics.ReturnOrdnanceRegen(MainWindow.userShip, MainWindow.userCaptain).ToString();
			airModifiedTB.Text = Mechanics.ReturnAirRegen(MainWindow.userShip, MainWindow.userCaptain).ToString();
			crewModifiedTB.Text = Mechanics.ReturnMedicalRegen(MainWindow.userShip, MainWindow.userCaptain).ToString();
			speedModifiedTB.Text = (MainWindow.userShip.BattleSpeed + speedRecoverySlider.Value).ToString();
		}
		public void ResetSliders()
		{
			resetSliders();
		}
		private void resetSliders()
		{
			shieldRecoverySlider.ValueChanged -= slider_ValueChanged;
			hullRecoverySlider.ValueChanged -= slider_ValueChanged;
			evasionRecoverySlider.ValueChanged -= slider_ValueChanged;
			ordnanceRecoverySlider.ValueChanged -= slider_ValueChanged;
			airRecoverySlider.ValueChanged -= slider_ValueChanged;
			crewRecoverySlider.ValueChanged -= slider_ValueChanged;
			speedRecoverySlider.ValueChanged -= slider_ValueChanged;

			shieldRecoverySlider.Value = 0;
			MainWindow.userShip.shieldEnergy = 0;
			hullRecoverySlider.Value = 0;
			MainWindow.userShip.hullEnergy = 0;
			evasionRecoverySlider.Value = 0;
			MainWindow.userShip.evasionEnergy = 0;
			ordnanceRecoverySlider.Value = 0;
			MainWindow.userShip.ordnanceEnergy = 0;
			airRecoverySlider.Value = 0;
			MainWindow.userShip.airEnergy = 0;
			crewRecoverySlider.Value = 0;
			MainWindow.userShip.crewEnergy = 0;
			speedRecoverySlider.Value = 0;
			MainWindow.userShip.battleSpeedEnergy = 0;

			shieldRecoverySlider.ValueChanged += slider_ValueChanged;
			hullRecoverySlider.ValueChanged += slider_ValueChanged;
			evasionRecoverySlider.ValueChanged += slider_ValueChanged;
			ordnanceRecoverySlider.ValueChanged += slider_ValueChanged;
			airRecoverySlider.ValueChanged += slider_ValueChanged;
			crewRecoverySlider.ValueChanged += slider_ValueChanged;
			speedRecoverySlider.ValueChanged += slider_ValueChanged;
		}

		private string shieldModifier()
		{
			return Mechanics.ReturnShieldRegen(MainWindow.userShip, MainWindow.userCaptain).ToString();
		}
		private string hullModifier()
		{
			return Mechanics.ReturnHullRegen(MainWindow.userShip, MainWindow.userCaptain).ToString();
		}
		private string evasionModifier()
		{
			return Mechanics.ReturnEvasion(MainWindow.userShip, MainWindow.userCaptain).ToString();
		}
		private string ordnanceModifier()
		{
			return Mechanics.ReturnOrdnanceRegen(MainWindow.userShip, MainWindow.userCaptain).ToString();
		}
		private string airModifier()
		{
			return Mechanics.ReturnAirRegen(MainWindow.userShip, MainWindow.userCaptain).ToString();
		}
		private string medicalModifier()
		{
			return Mechanics.ReturnMedicalRegen(MainWindow.userShip, MainWindow.userCaptain).ToString();
		}

		public void ToggleMedicalSlider()
		{
			if (MainWindow.userShip.medicalCrewCount > 0)
			{
				crewRecoverySlider.IsEnabled = true;
			}
			else
			{
				crewRecoverySlider.IsEnabled = false;
				crewRecoverySlider.Value = 0;
			}
		}

		private void updateSliderValues(FormattedSlider tempSlider, double newValue, double oldValue)
		{
			var tempEnergy = "";
			switch (tempSlider.Name)
			{
				case "shieldRecoverySlider":
					MainWindow.userShip.shieldEnergy = (int)newValue;
					tempEnergy = shieldModifier();
					shieldModifiedTB.Text = tempEnergy;

					if (!sliderEnergyChange(newValue, oldValue, tempSlider.AutoToolTipMultiplier))
					{
						MainWindow.userShip.shieldEnergy = (int)oldValue;
						shieldModifiedTB.Text = shieldModifier();
					}
					else
					{
						MainWindow.userShip.shieldEnergy = Convert.ToInt16(tempEnergy);
					}
					break;
				case "hullRecoverySlider":
					MainWindow.userShip.hullEnergy = (int)newValue;
					tempEnergy = hullModifier();
					hullModifiedTB.Text = tempEnergy;

					if (!sliderEnergyChange(newValue, oldValue, tempSlider.AutoToolTipMultiplier))
					{
						MainWindow.userShip.hullEnergy = (int)oldValue;
						hullModifiedTB.Text = hullModifier();
					}
					else
					{
						MainWindow.userShip.hullEnergy = Convert.ToInt16(tempEnergy);
					}
					break;
				case "evasionRecoverySlider":
					MainWindow.userShip.evasionEnergy = (int)newValue;
					tempEnergy = evasionModifier();
					evasionModifiedTB.Text = tempEnergy;

					if (!sliderEnergyChange(newValue, oldValue, tempSlider.AutoToolTipMultiplier))
					{
						MainWindow.userShip.evasionEnergy = (int)oldValue;
						evasionModifiedTB.Text = evasionModifier();
					}
					else
					{
						MainWindow.userShip.evasionEnergy = Convert.ToInt16(tempEnergy);
					}
					break;
				case "ordnanceRecoverySlider":
					MainWindow.userShip.ordnanceEnergy = (int)newValue;
					tempEnergy = ordnanceModifier();
					ordnanceModifiedTB.Text = tempEnergy;

					if (!sliderEnergyChange(newValue, oldValue, tempSlider.AutoToolTipMultiplier))
					{
						MainWindow.userShip.ordnanceEnergy = (int)oldValue;
						ordnanceModifiedTB.Text = ordnanceModifier();
					}
					else
					{
						MainWindow.userShip.ordnanceEnergy = Convert.ToInt16(tempEnergy);
					}
					break;
				case "airRecoverySlider":
					MainWindow.userShip.airEnergy = (int)newValue;
					tempEnergy = airModifier();
					airModifiedTB.Text = tempEnergy;

					if (!sliderEnergyChange(newValue, oldValue, tempSlider.AutoToolTipMultiplier))
					{
						MainWindow.userShip.airEnergy = (int)oldValue;
						airModifiedTB.Text = airModifier();
					}
					else
					{
						MainWindow.userShip.airEnergy = Convert.ToInt16(tempEnergy);
					}
					break;
				case "crewRecoverySlider":
					MainWindow.userShip.crewEnergy = (int)newValue;
					tempEnergy = medicalModifier();
					crewModifiedTB.Text = tempEnergy;

					if (!sliderEnergyChange(newValue, oldValue, tempSlider.AutoToolTipMultiplier))
					{
						MainWindow.userShip.crewEnergy = (int)oldValue;
						crewModifiedTB.Text = medicalModifier();
					}
					else
					{
						MainWindow.userShip.crewEnergy = Convert.ToInt16(tempEnergy);
					}
					break;
				case "speedRecoverySlider":
					tempEnergy = (newValue + MainWindow.userShip.BattleSpeed).ToString();
					speedModifiedTB.Text = tempEnergy;

					if (!sliderEnergyChange(newValue, oldValue, tempSlider.AutoToolTipMultiplier))
					{
						speedModifiedTB.Text = (oldValue + MainWindow.userShip.BattleSpeed).ToString();
					}
					else
					{
						MainWindow.userShip.battleSpeedEnergy = Convert.ToInt16(tempEnergy);
					}
					break;
			}
		}
		private bool sliderEnergyChange(double newValue, double oldValue, string multiplier)
		{
			bool success = false;

			if(String.IsNullOrEmpty(multiplier))
			{
				multiplier = "1";
			}

			var tempNewValue = Math.Ceiling((newValue * Convert.ToDouble(multiplier)));
			var tempOldValue = Math.Ceiling(oldValue * Convert.ToDouble(multiplier));

			if (oldValue == 0)
			{
				if (energyBar.Value - tempNewValue >= 0)
				{
					energyBar.Value -= tempNewValue;
					success = true;
				}
				else
				{
					success = false;
				}
			}
			else
			{
				if (energyBar.Value + tempOldValue - tempNewValue >= 0)
				{
					energyBar.Value += tempOldValue;
					energyBar.Value -= tempNewValue;
					success = true;
				}
				else
				{
					success = false;
				}
			}

			MainWindow.userShip.EnergyCurrent = (int)energyBar.Value;
			return success;
		}
		private void Slider_DragCompleted(object sender, DragCompletedEventArgs e)
		{
			updateSliderValues((FormattedSlider)sender, ((FormattedSlider)sender).Value, dragStartedValue);

			dragStarted = false;
			dragStartedValue = 0;
		}
		private void Slider_DragStarted(object sender, DragStartedEventArgs e)
		{
			dragStarted = true;
			dragStartedValue = ((FormattedSlider)sender).Value;
		}
		private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			if (!dragStarted)
			{
				updateSliderValues((FormattedSlider)sender, e.NewValue, e.OldValue);
			}
		}
	}
}
