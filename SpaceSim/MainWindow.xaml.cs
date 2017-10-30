using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	#region Classes
	public class DeployableCraft
	{
		public string Name { get; set; }
		public string Class { get; set; }
		public string Image { get; set; }
		public string Weapon { get; set; }
		public int PilotLevel { get; set; }
		public int Accuracy { get; set; }
		public int Dmg { get; set; }
		public int Speed { get; set; }
		public int HullMax { get; set; }
		public int HullCurrent { get; set; }
		public int ShieldMax { get; set; }
		public int ShieldCurrent { get; set; }
	}
	public class Ship
	{
		public string Name { get; set; }
		public string Class { get; set; }
		public string Image { get; set; }
		public int BattleSpeed { get; set; }
		public int BattleSpeedMaxBonus { get; set; }
		public double TravelSpeed { get; set; }
		public double Evasion { get; set; }
		public double EvasionMaxBonus { get; set; }
		public int HullMax { get; set; }
		public int HullCurrent { get; set; }
		public int HullRegen { get; set; }
		public int HullMaxBonus { get; set; }
		public int ShieldMax { get; set; }
		public int ShieldCurrent { get; set; }
		public int ShieldRegen { get; set; }
		public int ShieldMaxBonus { get; set; }
		public int CrewMax { get; set; }
		public int CrewCurrent { get; set; }
		public double CrewRegen { get; set; }
		public double CrewMaxBonus { get; set; }
		public int AirMax { get; set; }
		public int AirCurrent { get; set; }
		public int AirRegen { get; set; }
		public int AirMaxBonus { get; set; }
		public int OrdnanceMax { get; set; }
		public int OrdnanceCurrent { get; set; }
		public int OrdnanceMaxBonus { get; set; }
		public int OrdnanceRegen { get; set; }
		public int EnergyMax { get; set; }
		public int EnergyCurrent { get; set; }
		public int EnergyPerTurn { get; set; }
		public int CargoHold { get; set; }
		public string CargoMaxSize { get; set; }
		public int MarineMaxCount { get; set; }
		public int MarineCurrent { get; set; }

		public Weapon[] Weapons { get; set; }
		public DeployableCraft[] Deployables { get; set; }
		public string[,] TargetLocations { get; set; }
		public int SectionDamagedCount { get; set; }

		public int bridgeCrewCount { get; set; }
		public int bridgeCrewMax { get; set; }
		public int engineCrewCount { get; set; }
		public int engineCrewMax { get; set; }
		public int weaponCrewCount { get; set; }
		public int weaponCrewMax { get; set; }
		public int flightCrewCount { get; set; }
		public int flightCrewMax { get; set; }
		public int reactorCrewCount { get; set; }
		public int reactorCrewMax { get; set; }
		public int medicalCrewCount { get; set; }
		public int medicalCrewMax { get; set; }
		public int repairCrewCount { get; set; }
		public int repairCrewMax { get; set; }
		public int injuredCrewCount { get; set; }

		public int shieldEnergy { get; set; }
		public int hullEnergy { get; set; }
		public int evasionEnergy { get; set; }
		public int ordnanceEnergy { get; set; }
		public int airEnergy { get; set; }
		public int crewEnergy { get; set; }
		public int battleSpeedEnergy { get; set; }

		public Ship()
		{
			Weapons = new Weapon[0];
			TargetLocations = new string[7,3];

			TargetLocations[0,0] = "Hull";
			TargetLocations[1,0] = "Fore Turrets";
			TargetLocations[1,1] = (this.HullMax / 6).ToString(); // Max
			TargetLocations[1,2] = TargetLocations[1,1]; // Current
			TargetLocations[2,0] = "Aft Turrets";
			TargetLocations[2,1] = (this.HullMax / 6).ToString(); // Max
			TargetLocations[2,2] = TargetLocations[2,1]; // Current
			TargetLocations[3,0] = "Bottom Turrets";
			TargetLocations[3,1] = (this.HullMax / 6).ToString(); // Max
			TargetLocations[3,2] = TargetLocations[3,1]; // Current
			TargetLocations[4,0] = "Landing Bay";
			TargetLocations[4,1] = (this.HullMax / 6).ToString(); // Max
			TargetLocations[4,2] = TargetLocations[4,1]; // Current
			TargetLocations[5,0] = "Engines";
			TargetLocations[5,1] = (this.HullMax / 6).ToString(); // Max
			TargetLocations[5,2] = TargetLocations[5,1]; // Current
			TargetLocations[6,0] = "Bridge";
			TargetLocations[6,1] = (this.HullMax / 6).ToString(); // Max
			TargetLocations[6,2] = TargetLocations[6,1]; // Current

		}
	}
	public class Captain
	{
		public string Race { get; set; }
		public string Age { get; set; }
		public string Hair { get; set; }
		public string Accessory1 { get; set; }
		public string Accessory2 { get; set; }
		public string Accessory3 { get; set; }
		public string Background { get; set; }

		public string Class { get; set; }
		public string Level { get; set; }
		public string AIType { get; set; }

		// Weapon Skills
		public double[] WeaponSkills { get; set; }

		public double KineticSkill { get; set; }
		public double EnergySkill { get; set; }
		public double ProjectileSkill { get; set; }
		public double ExoticSkill { get; set; }

		// Ship System Skills
		public double ReactorSkill { get; set; }
		public double HullSkill { get; set; }
		public double MedicalSkill { get; set; }
		public double EvasionSkill { get; set; }
		public double DeployableSkill { get; set; }
		public double ShieldSkill { get; set; }

		// Merchant Skills	
		public double BarterSkill { get; set; }
		public double LogisticsSkill { get; set; }

		// Communication Skills
		public double BluffSkill { get; set; }
		public double ThreatenSkill { get; set; }
		public double NegotiateSkill { get; set; }

		public Captain()
		{
			WeaponSkills = new double[18];
		}
	}
	public class Weapon
	{
		public string Name { get; set; }
		public string ID { get; set; }
		public string Class { get; set; }
		public string Image { get; set; }
		public string Location { get; set; }
		public int HullMax { get; set; }
		public int HullCurrent { get; set; }
		public double RoF { get; set; }
		public double Accuracy { get; set; }
		public int OrdnanceCost { get; set; }
		public int EnergyCost { get; set; }
		public int Range { get; set; }
		public int HullDmg { get; set; }
		public int ShieldDmg { get; set; }
		public int CrewDmg { get; set; }
		public int CrewDmgChance { get; set; }
		public int O2Dmg { get; set; }
		public int O2DmgChance { get; set; }
		public bool TargetsDeployables { get; set; }
		public int DeployableDamage { get; set; }
		public string Target { get; set; }
	}
	public class CrewModifiers
	{
		public double BridgeCrew;
		public double MedicalCrew;
		public double EngineCrew;
		public double WeaponCrew;
		public double FlightCrew;
		public double ReactorCrew;
	}
	#endregion

	public partial class MainWindow : Window
	{
		#region Declarations
		public static CrewManagement thisBattleCrew { get; private set; }
		public static PowerManagement thisPowerManagement { get; private set; }
		public static BattleArmory thisBattleArmory { get; private set; }
		public static RepairManagement thisRepairManagement { get; private set; }
		public static BattleStatus thisBattleStatus { get; private set; }
		public static BattleNavigation thisBattleNavigation { get; private set; }		
		public static Comms thisComms { get; private set; }
		public static CrewModifiers thisCrewModifiers { get; private set; }

		public static Ship userShip = new Ship();
		public static Captain userCaptain = new Captain();

		public static Ship enemyShip = new Ship();
		public static Captain enemyCaptain = new Captain();

		public static DeployableCraft userMech = new DeployableCraft();
		public static DeployableCraft enemyMech = new DeployableCraft();
		public static DeployableCraft attackCraft = new DeployableCraft();

		public static string[] weaponTypes = new string[18];
		public static string[] aiTypes = new string[7];

		public static int userShipLocation = 0;
		public static int enemyShipLocation = 0;
		public static int currentRange = 0;
		public static int turnBattleSpeed = 0;

		public static Point battleCrewLocation { get; private set; }
		public static Point powerManagementLocation { get; private set; }
		public static Point battleArmoryLocation { get; private set; }
		public static Point repairManagementLocation { get; private set; }
		public static Point battleStatusLocation { get; private set; }
		public static Point battleNavigationLocation { get; private set; }		
		public static Point commsLocation { get; private set; }
		#endregion

		public MainWindow()
		{
			try
			{
				InitializeComponent();
			}
			catch (Exception ex)
			{

			}
			this.Loaded += new RoutedEventHandler(Window_Loaded);
			this.StateChanged += Window_Resize;
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			try
			{
				//mainWindowInitilizations();

				//var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
				//this.Top = desktopWorkingArea.Bottom - this.Height;
			}
			catch (Exception ex)
			{

			}
		}
		private void mainWindowInitilizations()
		{
			thisCrewModifiers = new CrewModifiers();
			crewModifiersSetup();
			weaponTypesSetup();
			aiTypesSetup();

			userShipSetup();
			userCaptainSetup();
			userWeaponsSetup();

			enemyShipSetup();
			enemyCaptainSetup();
			enemyWeaponsSetup();

			deployableSetup();

			thisBattleCrew = new CrewManagement();
			thisBattleArmory = new BattleArmory();
			thisPowerManagement = new PowerManagement();
			thisRepairManagement = new RepairManagement();
			thisBattleStatus = new BattleStatus();
			thisBattleNavigation = new BattleNavigation();
			thisComms = new Comms();

			getWindowLocations();
		}

		private void crewModifiersSetup()
		{
			thisCrewModifiers.BridgeCrew = 10; // Percentage bonus to everything.
			thisCrewModifiers.MedicalCrew = 25; // Flat Percentage to crew recovery per crew.
			thisCrewModifiers.EngineCrew = 10; // Percentage bonus to evasion.
			thisCrewModifiers.WeaponCrew = 5; //  Percentage bonus to accuracy.
			thisCrewModifiers.FlightCrew = 15; // Percentage bonus to deployable actions.
			thisCrewModifiers.ReactorCrew = 10; // Percentage bonus to energy regen.
		}
		private void weaponTypesSetup()
		{
			// Kinetic
			weaponTypes[0] = "Machine Gun";
			weaponTypes[1] = "Gatling Gun";
			weaponTypes[2] = "AutoCannon";
			weaponTypes[3] = "Gauss";
			weaponTypes[4] = "Cannon";

			// Energy
			weaponTypes[5] = "Beam";
			weaponTypes[6] = "Particle";
			weaponTypes[7] = "Plasma";
			weaponTypes[8] = "Radiation";
			weaponTypes[9] = "EMP";

			// Projectile
			weaponTypes[10] = "Rocket";
			weaponTypes[11] = "Missile";
			weaponTypes[12] = "Torpedo";
			weaponTypes[13] = "Swarm";
			weaponTypes[14] = "Drone";

			// Exotic
			weaponTypes[15] = "Computer Virus";
			weaponTypes[16] = "ECM";
			weaponTypes[17] = "Offensive Shield";
		}
		private void aiTypesSetup()
		{
			aiTypes[0] = "Aggressive";
			aiTypes[1] = "Defensive";
			aiTypes[2] = "Cautious";
			aiTypes[3] = "Cowardly";
			aiTypes[4] = "Carrier";
			aiTypes[5] = "Bombardment";
			aiTypes[6] = "Close Range";
		}

		private void userShipSetup()
		{
			userShip.Name = "Ragnarok MKI";
			userShip.Class = "Dreadnaught";
			userShip.Image = "/data\\ships\\images\\friendly_ship.png";
			userShip.BattleSpeed = 1;
			userShip.BattleSpeedMaxBonus = 1;
			userShip.TravelSpeed = 3;
			userShip.Evasion = 5;
			userShip.EvasionMaxBonus = 5;
			userShip.HullMax = 100;
			userShip.HullCurrent = 100;
			userShip.HullRegen = 0;
			userShip.HullMaxBonus = 10;
			userShip.ShieldMax = 100;
			userShip.ShieldCurrent = 100;
			userShip.ShieldRegen = 1;
			userShip.ShieldMaxBonus = 10;
			userShip.CrewMax = 10;
			userShip.CrewCurrent = 10;
			userShip.CrewMaxBonus = 25;
			userShip.AirMax = 10;
			userShip.AirCurrent = 10;
			userShip.AirRegen = 10;
			userShip.AirMaxBonus = 3;
			userShip.OrdnanceMax = 100;
			userShip.OrdnanceCurrent = 100;
			userShip.OrdnanceMaxBonus = 3;
			userShip.EnergyMax = 100;
			userShip.EnergyPerTurn = 10;
			userShip.EnergyCurrent = 100;
			userShip.MarineCurrent = 50;
			userShip.MarineMaxCount = 50;

			userShip.bridgeCrewCount = 0;
			userShip.engineCrewCount = 0;
			userShip.weaponCrewCount = 0;
			userShip.flightCrewCount = 0;
			userShip.reactorCrewCount = 0;
			userShip.medicalCrewCount = 0;
			userShip.repairCrewCount = 0;
			userShip.injuredCrewCount = 0;
		}
		private void userCaptainSetup()
		{
			userCaptain.KineticSkill = 1;
			userCaptain.WeaponSkills[0] = 1;
			userCaptain.WeaponSkills[1] = 1;
			userCaptain.WeaponSkills[2] = 1;
			userCaptain.WeaponSkills[3] = 1;
			userCaptain.WeaponSkills[4] = 1;

			userCaptain.EnergySkill = 1;
			userCaptain.WeaponSkills[5] = 1;
			userCaptain.WeaponSkills[6] = 1;
			userCaptain.WeaponSkills[7] = 1;
			userCaptain.WeaponSkills[8] = 1;
			userCaptain.WeaponSkills[9] = 1;

			userCaptain.ProjectileSkill = 1;
			userCaptain.WeaponSkills[10] = 1;
			userCaptain.WeaponSkills[11] = 1;
			userCaptain.WeaponSkills[12] = 1;
			userCaptain.WeaponSkills[13] = 1;
			userCaptain.WeaponSkills[14] = 1;

			userCaptain.ExoticSkill = 1;
			userCaptain.WeaponSkills[15] = 1;
			userCaptain.WeaponSkills[16] = 1;
			userCaptain.WeaponSkills[17] = 1;

			// Ship System Skills
			userCaptain.ReactorSkill = 1;
			userCaptain.HullSkill = 1;
			userCaptain.MedicalSkill = 1;
			userCaptain.EvasionSkill = 1;
			userCaptain.DeployableSkill = 1;
			userCaptain.ShieldSkill = 1;
		}
		private static void userWeaponsSetup()
		{
			var tempWeapon = new Weapon();
			tempWeapon.Name = "Rail Cannon";
			tempWeapon.ID = "weapon" + (userShip.Weapons.Length);
			tempWeapon.Class = "Gauss";
			tempWeapon.Image = "";
			tempWeapon.Location = "Fore Turrets";
			tempWeapon.HullMax = 4;
			tempWeapon.HullCurrent = 4;
			tempWeapon.RoF = 2;
			tempWeapon.Accuracy = 90;
			tempWeapon.OrdnanceCost = 0;
			tempWeapon.EnergyCost = 1;
			tempWeapon.Range = 2;
			tempWeapon.HullDmg = 5;
			tempWeapon.ShieldDmg = 5;
			tempWeapon.CrewDmg = 1;
			tempWeapon.CrewDmgChance = 10;
			tempWeapon.O2Dmg = 0;
			tempWeapon.O2DmgChance = 0;
			tempWeapon.TargetsDeployables = false;
			tempWeapon.DeployableDamage = 0;

			userShip.Weapons = Mechanics.AddWeapon(userShip, tempWeapon);

			var tempWeapon2 = new Weapon();
			tempWeapon2.Name = "Rail Cannon";
			tempWeapon2.ID = "weapon" + (userShip.Weapons.Length);
			tempWeapon2.Class = "Gauss";
			tempWeapon2.Image = "";
			tempWeapon.Location = "Aft Turrets";
			tempWeapon2.HullMax = 4;
			tempWeapon2.HullCurrent = 4;
			tempWeapon2.RoF = 2;
			tempWeapon2.Accuracy = 90;
			tempWeapon2.OrdnanceCost = 0;
			tempWeapon2.EnergyCost = 1;
			tempWeapon2.Range = 2;
			tempWeapon2.HullDmg = 5;
			tempWeapon2.ShieldDmg = 5;
			tempWeapon2.CrewDmg = 1;
			tempWeapon2.CrewDmgChance = 10;
			tempWeapon2.O2Dmg = 0;
			tempWeapon2.O2DmgChance = 0;
			tempWeapon2.TargetsDeployables = false;
			tempWeapon2.DeployableDamage = 0;

			userShip.Weapons = Mechanics.AddWeapon(userShip, tempWeapon2);

			var tempWeapon3 = new Weapon();
			tempWeapon3.Name = "Plasma Cannon";
			tempWeapon3.ID = "weapon" + (userShip.Weapons.Length);
			tempWeapon3.Class = "Plasma";
			tempWeapon3.Image = "";
			tempWeapon.Location = "Fore Turrets";
			tempWeapon3.HullMax = 5;
			tempWeapon3.HullCurrent = 5;
			tempWeapon3.RoF = 0.5;
			tempWeapon3.Accuracy = 60;
			tempWeapon3.OrdnanceCost = 0;
			tempWeapon3.EnergyCost = 10;
			tempWeapon3.Range = 3;
			tempWeapon3.HullDmg = 50;
			tempWeapon3.ShieldDmg = 20;
			tempWeapon3.CrewDmg = 3;
			tempWeapon3.CrewDmgChance = 30;
			tempWeapon3.O2Dmg = 1;
			tempWeapon3.O2DmgChance = 10;
			tempWeapon3.TargetsDeployables = false;
			tempWeapon3.DeployableDamage = 0;

			userShip.Weapons = Mechanics.AddWeapon(userShip, tempWeapon3);

			var tempWeapon4 = new Weapon();
			tempWeapon4.Name = "Torpedo";
			tempWeapon4.ID = "weapon" + (userShip.Weapons.Length);
			tempWeapon4.Class = "Torpedo";
			tempWeapon4.Image = "";
			tempWeapon.Location = "Fore Turrets";
			tempWeapon4.HullMax = 10;
			tempWeapon4.HullCurrent = 4;
			tempWeapon4.RoF = 0.2;
			tempWeapon4.Accuracy = 90;
			tempWeapon4.OrdnanceCost = 10;
			tempWeapon4.EnergyCost = 0;
			tempWeapon4.Range = 5;
			tempWeapon4.HullDmg = 50;
			tempWeapon4.ShieldDmg = 1;
			tempWeapon4.CrewDmg = 2;
			tempWeapon4.CrewDmgChance = 50;
			tempWeapon4.O2Dmg = 2;
			tempWeapon4.O2DmgChance = 20;
			tempWeapon4.TargetsDeployables = false;
			tempWeapon4.DeployableDamage = 0;

			userShip.Weapons = Mechanics.AddWeapon(userShip, tempWeapon4);
		}

		private void enemyShipSetup()
		{
			enemyShip.Name = "Naglfar";
			enemyShip.Class = "Carrier Cruiser";
			enemyShip.Image = "/data\\ships\\images\\enemy_ship.png";
			enemyShip.BattleSpeed = 1;
			enemyShip.BattleSpeedMaxBonus = 1;
			enemyShip.TravelSpeed = 3;
			enemyShip.Evasion = 5;
			enemyShip.EvasionMaxBonus = 5;
			enemyShip.HullMax = 75;
			enemyShip.HullCurrent = 75;
			enemyShip.HullRegen = 0;
			enemyShip.HullMaxBonus = 7;
			enemyShip.ShieldMax = 80;
			enemyShip.ShieldCurrent = 80;
			enemyShip.ShieldRegen = 1;
			enemyShip.ShieldMaxBonus = 3;
			enemyShip.CrewMax = 5;
			enemyShip.CrewCurrent = 5;
			enemyShip.CrewMaxBonus = 10;
			enemyShip.AirMax = 5;
			enemyShip.AirRegen = 5;
			enemyShip.AirMaxBonus = 2;
			enemyShip.OrdnanceMax = 100;
			enemyShip.OrdnanceCurrent = 100;
			enemyShip.OrdnanceMaxBonus = 1;
			enemyShip.EnergyMax = 100;
			enemyShip.EnergyPerTurn = 10;
			enemyShip.EnergyCurrent = 100;
			enemyShip.MarineCurrent = 10;
			enemyShip.MarineMaxCount = 10;

			enemyShip.bridgeCrewCount = 0;
			enemyShip.engineCrewCount = 0;
			enemyShip.weaponCrewCount = 0;
			enemyShip.flightCrewCount = 0;
			enemyShip.reactorCrewCount = 0;
			enemyShip.medicalCrewCount = 0;
			enemyShip.repairCrewCount = 0;
			enemyShip.injuredCrewCount = 0;
		}
		private void enemyCaptainSetup()
		{
			enemyCaptain.AIType = aiTypes[Utility.GetRandom(0, 6)];
			enemyCaptain.KineticSkill = 1;
			enemyCaptain.WeaponSkills[0] = 1;
			enemyCaptain.WeaponSkills[1] = 1;
			enemyCaptain.WeaponSkills[2] = 1;
			enemyCaptain.WeaponSkills[3] = 1;
			enemyCaptain.WeaponSkills[4] = 1;

			enemyCaptain.EnergySkill = 1;
			enemyCaptain.WeaponSkills[5] = 1;
			enemyCaptain.WeaponSkills[6] = 1;
			enemyCaptain.WeaponSkills[7] = 1;
			enemyCaptain.WeaponSkills[8] = 1;
			enemyCaptain.WeaponSkills[9] = 1;

			enemyCaptain.ProjectileSkill = 1;
			enemyCaptain.WeaponSkills[10] = 1;
			enemyCaptain.WeaponSkills[11] = 1;
			enemyCaptain.WeaponSkills[12] = 1;
			enemyCaptain.WeaponSkills[13] = 1;
			enemyCaptain.WeaponSkills[14] = 1;

			enemyCaptain.ExoticSkill = 1;
			enemyCaptain.WeaponSkills[15] = 1;
			enemyCaptain.WeaponSkills[16] = 1;
			enemyCaptain.WeaponSkills[17] = 1;

			// Ship System Skills
			enemyCaptain.ReactorSkill = 1;
			enemyCaptain.HullSkill = 1;
			enemyCaptain.MedicalSkill = 1;
			enemyCaptain.EvasionSkill = 1;
			enemyCaptain.DeployableSkill = 1;
			enemyCaptain.ShieldSkill = 1;
		}
		private static void enemyWeaponsSetup()
		{
			var tempWeapon = new Weapon();
			tempWeapon.Name = "Beam Cannon";
			tempWeapon.ID = "enemyWeapon" + (enemyShip.Weapons.Length);
			tempWeapon.Class = "Beam";
			tempWeapon.Image = "";
			tempWeapon.Location = "Fore Turrets";
			tempWeapon.HullMax = 20;
			tempWeapon.HullCurrent = 20;
			tempWeapon.RoF = 1;
			tempWeapon.Accuracy = 100;
			tempWeapon.OrdnanceCost = 0;
			tempWeapon.EnergyCost = 20;
			tempWeapon.Range = 2;
			tempWeapon.HullDmg = 5;
			tempWeapon.ShieldDmg = 2;
			tempWeapon.CrewDmg = 1;
			tempWeapon.CrewDmgChance = 2;
			tempWeapon.O2Dmg = 0;
			tempWeapon.O2DmgChance = 0;
			tempWeapon.TargetsDeployables = true;
			tempWeapon.DeployableDamage = 5;

			enemyShip.Weapons = Mechanics.AddWeapon(enemyShip, tempWeapon);

			var tempWeapon2 = new Weapon();
			tempWeapon2.Name = "Beam Cannon";
			tempWeapon2.ID = "enemyWeapon" + (enemyShip.Weapons.Length);
			tempWeapon2.Class = "Beam";
			tempWeapon2.Image = "";
			tempWeapon2.Location = "Aft Turrets";
			tempWeapon2.HullMax = 20;
			tempWeapon2.HullCurrent = 20;
			tempWeapon2.RoF = 1;
			tempWeapon2.Accuracy = 100;
			tempWeapon2.OrdnanceCost = 0;
			tempWeapon2.EnergyCost = 2;
			tempWeapon2.Range = 2;
			tempWeapon2.HullDmg = 5;
			tempWeapon2.ShieldDmg = 2;
			tempWeapon2.CrewDmg = 1;
			tempWeapon2.CrewDmgChance = 2;
			tempWeapon2.O2Dmg = 0;
			tempWeapon2.O2DmgChance = 0;
			tempWeapon2.TargetsDeployables = true;
			tempWeapon2.DeployableDamage = 5;

			enemyShip.Weapons = Mechanics.AddWeapon(enemyShip, tempWeapon2);

			var tempWeapon3 = new Weapon();
			tempWeapon3.Name = "Beam Cannon";
			tempWeapon3.ID = "enemyWeapon" + (enemyShip.Weapons.Length);
			tempWeapon3.Class = "Beam";
			tempWeapon3.Image = "";
			tempWeapon3.Location = "Bottom Turrets";
			tempWeapon3.HullMax = 20;
			tempWeapon3.HullCurrent = 20;
			tempWeapon3.RoF = 1;
			tempWeapon3.Accuracy = 100;
			tempWeapon3.OrdnanceCost = 0;
			tempWeapon3.EnergyCost = 2;
			tempWeapon3.Range = 2;
			tempWeapon3.HullDmg = 5;
			tempWeapon3.ShieldDmg = 2;
			tempWeapon3.CrewDmg = 1;
			tempWeapon3.CrewDmgChance = 2;
			tempWeapon3.O2Dmg = 0;
			tempWeapon3.O2DmgChance = 0;
			tempWeapon3.TargetsDeployables = true;
			tempWeapon3.DeployableDamage = 5;

			enemyShip.Weapons = Mechanics.AddWeapon(enemyShip, tempWeapon3);
		}
		private void deployableSetup()
		{
			userMech.Name = "Würger";
			userMech.Class = "Mech";
			userMech.Weapon = "Gatling Gun";
			userMech.Accuracy = 75;
			userMech.Dmg = 2;
			userMech.Speed = 3;
			userMech.HullMax = 30;
			userMech.HullCurrent = 30;
			userMech.ShieldMax = 30;
			userMech.ShieldCurrent = 30;
			userMech.Image = "/data\\mechs\\images\\gatling_mech.png";

			enemyMech.Name = "Rächer";
			enemyMech.Class = "Mech";
			enemyMech.Weapon = "Missile Launcher";
			enemyMech.Accuracy = 90;
			enemyMech.Dmg = 3;
			enemyMech.Speed = 2;
			enemyMech.HullMax = 50;
			enemyMech.HullCurrent = 50;
			enemyMech.ShieldMax = 10;
			enemyMech.ShieldCurrent = 10;
			enemyMech.Image = "/data\\mechs\\images\\missile_mech.png";

			attackCraft.Name = "Falke";
			attackCraft.Class = "Attack Craft";
			attackCraft.Weapon = "Laser";
			attackCraft.Accuracy = 100;
			attackCraft.Dmg = 1;
			attackCraft.Speed = 5;
			attackCraft.HullMax = 20;
			attackCraft.HullCurrent = 20;
			attackCraft.ShieldMax = 5;
			attackCraft.ShieldCurrent = 5;
			attackCraft.Image = "/data\\jets\\images\\attack_jet.png";
		}

		public static void ShowCommsWindow()
		{
			thisComms.Show();
			commsLocation = thisComms.PointToScreen(new Point(0, 0));
		}
		public static void HideCommsWindow()
		{
			thisComms.Hide();
			//commsLocation = thisComms.PointToScreen(new Point(0, 0));
		}
		private void getWindowLocations()
		{
			thisBattleCrew.Show();
			battleCrewLocation = thisBattleCrew.PointToScreen(new Point(0, 0));

			thisBattleArmory.Show();
			battleArmoryLocation = thisBattleArmory.PointToScreen(new Point(0, 0));

			thisPowerManagement.Show();
			powerManagementLocation = thisPowerManagement.PointToScreen(new Point(0, 0));
	
			thisRepairManagement.Show();
			repairManagementLocation = thisRepairManagement.PointToScreen(new Point(0, 0));

			thisBattleStatus.Show();
			battleStatusLocation = thisBattleStatus.PointToScreen(new Point(0, 0));

			thisBattleNavigation.Show();
			battleNavigationLocation = thisBattleNavigation.PointToScreen(new Point(0, 0));

			//thisComms.Show();
			//commsLocation = thisComms.PointToScreen(new Point(0, 0));
		}

		private void load_Crew_Management(object sender, RoutedEventArgs e)
		{
			thisBattleCrew.Owner = this;

			if (thisBattleCrew.IsLoaded)
			{
				thisBattleCrew.Show();
			}
			else
			{
				thisBattleCrew = new CrewManagement();
				thisBattleCrew.Show();
			}
		}
		private void load_Power_Management(object sender, RoutedEventArgs e)
		{
			thisPowerManagement.Owner = this;

			if (thisPowerManagement.IsLoaded)
			{
				thisPowerManagement.Show();
			}
			else
			{
				thisPowerManagement = new PowerManagement();
				thisPowerManagement.Show();
			}
		}
		private void load_Armory_Window(object sender, RoutedEventArgs e)
		{
			thisBattleArmory.Owner = this;

			if (thisBattleArmory.IsLoaded)
			{
				thisBattleArmory.Show();
			}
			else
			{
				thisBattleArmory = new BattleArmory();
				thisBattleArmory.Show();
			}
		}
		private void load_Repair_Management(object sender, RoutedEventArgs e)
		{
			thisRepairManagement.Owner = this;

			if (thisRepairManagement.IsLoaded)
			{
				thisRepairManagement.Show();
			}
			else
			{
				thisRepairManagement = new RepairManagement();
				thisRepairManagement.Show();
			}
		}
		private void load_Status_Window(object sender, RoutedEventArgs e)
		{
			thisBattleStatus.Owner = this;
			
			if (thisBattleStatus.IsLoaded)
			{
				thisBattleStatus.Show();
			}
			else
			{
				thisBattleStatus = new BattleStatus();
				thisBattleStatus.Show();
			}
		}
		private void load_Navigation_Window(object sender, RoutedEventArgs e)
		{
			thisBattleNavigation.Owner = this;

			if (thisBattleNavigation.IsLoaded)
			{
				thisBattleNavigation.Show();
			}
			else
			{
				thisBattleNavigation = new BattleNavigation();
				thisBattleNavigation.Show();
			}
		}
		private void load_Comms_Management(object sender, RoutedEventArgs e)
		{
			thisComms.Owner = this;

			if (thisComms.IsLoaded)
			{
				thisComms.Show();
			}
			else
			{
				thisComms = new Comms();
				thisComms.Show();
			}
		}
		private void Window_Resize(object sender, EventArgs e)
		{
			if (WindowState == WindowState.Normal || WindowState == WindowState.Maximized)
			{
				thisBattleCrew.Activate();
				thisPowerManagement.Activate();
				thisBattleArmory.Activate();
				thisRepairManagement.Activate();
				thisBattleStatus.Activate();
				thisBattleNavigation.Activate();
				thisComms.Activate();
			}

			this.WindowState = WindowState.Minimized;
		}

		private void startBattle_Click(object sender, RoutedEventArgs e)
		{
			mainWindowInitilizations();
			this.WindowState = WindowState.Minimized;
		}
	}
}
