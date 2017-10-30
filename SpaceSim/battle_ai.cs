using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSim
{
	class Battle_AI
	{
		private static string[] targetLocations { get; set; }
		Captain enemyCaptain = MainWindow.enemyCaptain;
		int distance = 0;

		public Battle_AI()
		{
			// Values for reference.
			//aiTypes[0] = "Aggressive";
			//aiTypes[1] = "Defensive";
			//aiTypes[2] = "Cautious";
			//aiTypes[3] = "Cowardly";
			//aiTypes[4] = "Carrier";
			//aiTypes[5] = "Bombardment";
			//aiTypes[6] = "Close Range";
		}
		public static void CombatTurn()
		{
			//TRAP TODO

			// Check hull/shield, try to escape range if low and type supports.
			// Check weapon ranges, change to distance based on type.
			switch (Array.IndexOf(MainWindow.aiTypes, MainWindow.enemyCaptain.AIType))
			{
				case 0:	// Aggressive
					allocateCrew();
					targetWeapons(0.5);
					Mechanics.FireWeapons(MainWindow.enemyShip, MainWindow.userShip, MainWindow.enemyCaptain, MainWindow.userCaptain);
					allocateEnergy();
					// Deployables to attack.
					aiMovement(aiCalculateRetreat(false, 0.25), true, determineSpeed(1));
					break;
				case 1: // Defensive
					allocateCrew();
					targetWeapons(0.8);
					Mechanics.FireWeapons(MainWindow.enemyShip, MainWindow.userShip, MainWindow.enemyCaptain, MainWindow.userCaptain);
					allocateEnergy();
					// Deployables to defend/intercept.
					aiMovement(aiCalculateRetreat(true, 0.5), false, determineSpeed(0.5));
					break;
				case 2: // Cautious
					allocateCrew();
					targetWeapons(0.75);
					Mechanics.FireWeapons(MainWindow.enemyShip, MainWindow.userShip, MainWindow.enemyCaptain, MainWindow.userCaptain);
					allocateEnergy();
					// Deployables to defend/intercept.
					aiMovement(aiCalculateRetreat(true, 0.75), false, determineSpeed(0.5));
					break;
				case 3: // Cowardly
					allocateCrew();
					targetWeapons(0.75);
					Mechanics.FireWeapons(MainWindow.enemyShip, MainWindow.userShip, MainWindow.enemyCaptain, MainWindow.userCaptain);
					allocateEnergy();
					// Attempt to flee / surrender as soon as the fight goes badly.
					aiMovement(aiCalculateRetreat(true, 0.9), false, determineSpeed(2));
					break;
				case 4: // Carrier
					allocateCrew();
					targetWeapons(0.5);
					Mechanics.FireWeapons(MainWindow.enemyShip, MainWindow.userShip, MainWindow.enemyCaptain, MainWindow.userCaptain);
					allocateEnergy(); 
					// Micromanage deployables more.
					aiMovement(aiCalculateRetreat(true, 0.5), false, determineSpeed(1));
					break;
				case 5: // Bombardment
					allocateCrew();
					targetWeapons(0.8);
					Mechanics.FireWeapons(MainWindow.enemyShip, MainWindow.userShip, MainWindow.enemyCaptain, MainWindow.userCaptain);
					allocateEnergy();
					// Try to stay at long range.
					aiMovement(aiCalculateRetreat(true, 0.8), true, determineSpeed(1));
					break;
				case 6: // Close Range
					allocateCrew();
					targetWeapons(0.5);
					Mechanics.FireWeapons(MainWindow.enemyShip, MainWindow.userShip, MainWindow.enemyCaptain, MainWindow.userCaptain);
					allocateEnergy();
					// Try to stay at close range.
					aiMovement(aiCalculateRetreat(false, 0.25), true, determineSpeed(1));
					break;
			}
		}

		// Crew Management
		private static void allocateCrew()
		{
			var availableCrewCount = MainWindow.enemyShip.CrewCurrent - MainWindow.enemyShip.injuredCrewCount;
			
			// Default crew stations.
			if (availableCrewCount > 0)
			{
				MainWindow.enemyShip.bridgeCrewCount++;
				availableCrewCount--;
			}
			if (availableCrewCount > 0)
			{
				MainWindow.enemyShip.weaponCrewCount++;
				availableCrewCount--;
			}
			if (availableCrewCount > 0)
			{
				MainWindow.enemyShip.reactorCrewCount++;
				availableCrewCount--;
			}
			if (availableCrewCount > 0)
			{
				MainWindow.enemyShip.engineCrewCount++;
				availableCrewCount--;
			}

			if (availableCrewCount > 0)
			{
				switch (Array.IndexOf(MainWindow.aiTypes, MainWindow.enemyCaptain.AIType))
				{
					case 0: // Aggressive
						setAggressiveCrew(availableCrewCount);
						break;
					case 1: // Defensive
						setDefensiveCrew(availableCrewCount);
						break;
					case 2: // Cautious
						setCautiousCrew(availableCrewCount);
						break;
					case 3: // Cowardly
						setCowardlyCrew(availableCrewCount);
						break;
					case 4: // Carrier
						setCarrierCrew(availableCrewCount);
						break;
					case 5: // Bombardment
						setBombardmentCrew(availableCrewCount);
						break;
					case 6: // Close Range
						setCloseRangeCrew(availableCrewCount);
						break;
				}
			}
		}
		private static void setAggressiveCrew(int availableCrewCount)
		{
			setWeaponCrew(availableCrewCount);
			setBridgeCrew(availableCrewCount);
			setReactorCrew(availableCrewCount);
			setFlightCrew(availableCrewCount);
			setRepairCrew(availableCrewCount);
			setMedicalCrew(availableCrewCount);
			setEngineCrew(availableCrewCount);
		}
		private static void setDefensiveCrew(int availableCrewCount)
		{
			setReactorCrew(availableCrewCount);
			setRepairCrew(availableCrewCount);
			setMedicalCrew(availableCrewCount);
			setBridgeCrew(availableCrewCount);
			setWeaponCrew(availableCrewCount);
			setFlightCrew(availableCrewCount);
			setEngineCrew(availableCrewCount);
		}
		private static void setCautiousCrew(int availableCrewCount)
		{
			setReactorCrew(availableCrewCount);
			setRepairCrew(availableCrewCount);
			setMedicalCrew(availableCrewCount);
			setEngineCrew(availableCrewCount);
			setBridgeCrew(availableCrewCount);
			setWeaponCrew(availableCrewCount);
			setFlightCrew(availableCrewCount);
		}
		private static void setCowardlyCrew(int availableCrewCount)
		{
			setEngineCrew(availableCrewCount);
			setReactorCrew(availableCrewCount);
			setRepairCrew(availableCrewCount);
			setMedicalCrew(availableCrewCount);
			setBridgeCrew(availableCrewCount);
			setWeaponCrew(availableCrewCount);
			setFlightCrew(availableCrewCount);
		}
		private static void setCarrierCrew(int availableCrewCount)
		{
			setFlightCrew(availableCrewCount);
			setEngineCrew(availableCrewCount);
			setBridgeCrew(availableCrewCount);
			setReactorCrew(availableCrewCount);
			setRepairCrew(availableCrewCount);
			setMedicalCrew(availableCrewCount);
			setWeaponCrew(availableCrewCount);
		}
		private static void setBombardmentCrew(int availableCrewCount)
		{
			setWeaponCrew(availableCrewCount);
			setReactorCrew(availableCrewCount);
			setEngineCrew(availableCrewCount);
			setFlightCrew(availableCrewCount);
			setBridgeCrew(availableCrewCount);
			setRepairCrew(availableCrewCount);
			setMedicalCrew(availableCrewCount);
		}
		private static void setCloseRangeCrew(int availableCrewCount)
		{
			setEngineCrew(availableCrewCount);
			setWeaponCrew(availableCrewCount);
			setReactorCrew(availableCrewCount);
			setBridgeCrew(availableCrewCount);
			setRepairCrew(availableCrewCount);
			setMedicalCrew(availableCrewCount);
			setFlightCrew(availableCrewCount);
		}

		private static void setReactorCrew(int availableCrewCount)
		{
			for (int i = 0; i < MainWindow.enemyShip.reactorCrewMax - 1; i++)
			{
				if (availableCrewCount > 0)
				{
					MainWindow.enemyShip.reactorCrewCount++;
					availableCrewCount--;
				}
			}
		}
		private static void setRepairCrew(int availableCrewCount)
		{
			if (MainWindow.enemyShip.SectionDamagedCount > 0)
			{
				for (int i = 0; i < MainWindow.enemyShip.repairCrewMax; i++)
				{
					if (availableCrewCount > 0 && MainWindow.enemyShip.repairCrewCount < MainWindow.enemyShip.SectionDamagedCount)
					{
						MainWindow.enemyShip.repairCrewCount++;
						availableCrewCount--;
					}
				}
			}
		}
		private static void setMedicalCrew(int availableCrewCount)
		{
			if (MainWindow.enemyShip.injuredCrewCount > 0)
			{
				for (int i = 0; i < MainWindow.enemyShip.medicalCrewMax; i++)
				{
					if (availableCrewCount > 0)
					{
						MainWindow.enemyShip.medicalCrewCount++;
						availableCrewCount--;
					}
				}
			}
		}
		private static void setEngineCrew(int availableCrewCount)
		{
			for (int i = 0; i < MainWindow.enemyShip.engineCrewMax - 1; i++)
			{
				if (availableCrewCount > 0)
				{
					MainWindow.enemyShip.engineCrewCount++;
					availableCrewCount--;
				}
			}
		}
		private static void setBridgeCrew(int availableCrewCount)
		{
			for (int i = 0; i < MainWindow.enemyShip.bridgeCrewMax - 1; i++)
			{
				if (availableCrewCount > 0)
				{
					MainWindow.enemyShip.bridgeCrewCount++;
					availableCrewCount--;
				}
			}
		}
		private static void setWeaponCrew(int availableCrewCount)
		{
			for (int i = 0; i < MainWindow.enemyShip.weaponCrewMax - 1; i++)
			{
				if (availableCrewCount > 0)
				{
					MainWindow.enemyShip.weaponCrewCount++;
					availableCrewCount--;
				}
			}
		}
		private static void setFlightCrew(int availableCrewCount)
		{
			for (int i = 0; i < MainWindow.enemyShip.flightCrewMax; i++)
			{
				if (availableCrewCount > 0)
				{
					MainWindow.enemyShip.flightCrewCount++;
					availableCrewCount--;
				}
			}
		}

		// Energy Management
		private static void allocateEnergy()
		{
			//TRAP TODO Fix these so they manage energy better.
			switch (Array.IndexOf(MainWindow.aiTypes, MainWindow.enemyCaptain.AIType))
			{
				case 0: // Aggressive
					setAggressiveEnergy();
					break;
				case 1: // Defensive
					setDefensiveEnergy();
					break;
				case 2: // Cautious
					setCautiousEnergy();
					break;
				case 3: // Cowardly
					setCowardlyEnergy();
					break;
				case 4: // Carrier
					setCarrierEnergy();
					break;
				case 5: // Bombardment
					setBombardmentEnergy();
					break;
				case 6: // Close Range
					setCloseRangeEnergy();
					break;
			}
		}
		private static bool hasAvailableEnergy()
		{
			bool hasEnergy = false;

			if (MainWindow.enemyShip.EnergyCurrent > 0)
			{
				hasEnergy = true;
			}

			return hasEnergy;
		}
		private static bool hasAvailableEnergy(int value)
		{
			bool hasEnergy = false;

			if (MainWindow.enemyShip.EnergyCurrent > value)
			{
				hasEnergy = true;
			}

			return hasEnergy;
		}

		private static void setAggressiveEnergy()
		{
			if (Mechanics.GetWeaponRanges(MainWindow.enemyShip)[1] > (MainWindow.currentRange + MainWindow.enemyShip.BattleSpeed) && hasAvailableEnergy())
			{
				setSpeedEnergy();
			}
			if (MainWindow.enemyShip.OrdnanceCurrent <= (MainWindow.enemyShip.OrdnanceMax / 4) && hasAvailableEnergy())
			{
				setOrdnanceEnergy();
			}
			if (MainWindow.enemyShip.AirCurrent < MainWindow.enemyShip.AirMax && hasAvailableEnergy())
			{
				setAirEnergy();
			}
			if (MainWindow.enemyShip.injuredCrewCount > 0 && hasAvailableEnergy()) 
			{
				setCrewEnergy();
			}
			//if (MainWindow.enemyShip.HullCurrent <= (MainWindow.enemyShip.HullMax / 4) && hasAvailableEnergy())
			//{
			//	setHullEnergy();
			//}
			//if (MainWindow.enemyShip.ShieldCurrent < (MainWindow.enemyShip.ShieldMax / 4) && hasAvailableEnergy())
			//{
			//	setShieldEnergy();
			//}
			//if (hasAvailableEnergy())
			//{
			//	setEvasionEnergy();
			//}
		}
		private static void setDefensiveEnergy()
		{
			if (MainWindow.enemyShip.HullCurrent <= (MainWindow.enemyShip.HullMax / 1.33) && hasAvailableEnergy())
			{
				setHullEnergy();
			}
			if (MainWindow.enemyShip.ShieldCurrent < (MainWindow.enemyShip.ShieldMax / 1.33) && hasAvailableEnergy())
			{
				setShieldEnergy();
			}
			if (hasAvailableEnergy())
			{
				setEvasionEnergy();
			}
			if (MainWindow.enemyShip.AirCurrent < MainWindow.enemyShip.AirMax && hasAvailableEnergy())
			{
				setAirEnergy();
			}
			if (MainWindow.enemyShip.injuredCrewCount > 0 && hasAvailableEnergy())
			{
				setCrewEnergy();
			}
			//if (MainWindow.enemyShip.OrdnanceCurrent <= (MainWindow.enemyShip.OrdnanceMax / 6) && hasAvailableEnergy())
			//{
			//	setOrdnanceEnergy();
			//}
			//if (Mechanics.GetWeaponRanges(MainWindow.enemyShip)[1] > (MainWindow.currentRange + MainWindow.enemyShip.BattleSpeed) && hasAvailableEnergy())
			//{
			//	setSpeedEnergy();
			//}
		}
		private static void setCautiousEnergy()
		{
			if (MainWindow.enemyShip.HullCurrent <= (MainWindow.enemyShip.HullMax / 1.33) && hasAvailableEnergy())
			{
				setHullEnergy();
			}
			if (MainWindow.enemyShip.ShieldCurrent < (MainWindow.enemyShip.ShieldMax / 1.33) && hasAvailableEnergy())
			{
				setShieldEnergy();
			}
			if (MainWindow.enemyShip.AirCurrent < MainWindow.enemyShip.AirMax && hasAvailableEnergy())
			{
				setAirEnergy();
			}
			if (MainWindow.enemyShip.injuredCrewCount > 0 && hasAvailableEnergy())
			{
				setCrewEnergy();
			}
			if (Mechanics.GetWeaponRanges(MainWindow.enemyShip)[1] > (MainWindow.currentRange + MainWindow.enemyShip.BattleSpeed) && hasAvailableEnergy())
			{
				setSpeedEnergy();
			}
			//if (MainWindow.enemyShip.OrdnanceCurrent <= (MainWindow.enemyShip.OrdnanceMax / 6) && hasAvailableEnergy())
			//{
			//	setOrdnanceEnergy();
			//}
			//if (hasAvailableEnergy())
			//{
			//	setEvasionEnergy();
			//}
		}
		private static void setCowardlyEnergy()
		{
			if (hasAvailableEnergy())
			{
				setEvasionEnergy();
			}
			if (Mechanics.GetWeaponRanges(MainWindow.enemyShip)[1] > (MainWindow.currentRange + MainWindow.enemyShip.BattleSpeed) && hasAvailableEnergy())
			{
				setSpeedEnergy();
			}
			if (MainWindow.enemyShip.HullCurrent <= (MainWindow.enemyShip.HullMax / 1.2) && hasAvailableEnergy())
			{
				setHullEnergy();
			}
			if (MainWindow.enemyShip.ShieldCurrent < (MainWindow.enemyShip.ShieldMax / 1.2) && hasAvailableEnergy())
			{
				setShieldEnergy();
			}
			//if (MainWindow.enemyShip.AirCurrent < MainWindow.enemyShip.AirMax && hasAvailableEnergy())
			//{
			//	setAirEnergy();
			//}
			//if (MainWindow.enemyShip.injuredCrewCount > 0 && hasAvailableEnergy())
			//{
			//	setCrewEnergy();
			//}
			//if (MainWindow.enemyShip.OrdnanceCurrent <= (MainWindow.enemyShip.OrdnanceMax / 6) && hasAvailableEnergy())
			//{
			//	setOrdnanceEnergy();
			//}

		}
		private static void setCarrierEnergy()
		{
			if (Mechanics.GetWeaponRanges(MainWindow.enemyShip)[1] > (MainWindow.currentRange + MainWindow.enemyShip.BattleSpeed) && hasAvailableEnergy())
			{
				setSpeedEnergy();
			}
			if (MainWindow.enemyShip.OrdnanceCurrent <= (MainWindow.enemyShip.OrdnanceMax / 4) && hasAvailableEnergy())
			{
				setOrdnanceEnergy();
			}
			if (MainWindow.enemyShip.HullCurrent <= (MainWindow.enemyShip.HullMax / 2) && hasAvailableEnergy())
			{
				setHullEnergy();
			}
			if (MainWindow.enemyShip.ShieldCurrent < (MainWindow.enemyShip.ShieldMax / 2) && hasAvailableEnergy())
			{
				setShieldEnergy();
			}
			if (MainWindow.enemyShip.AirCurrent < MainWindow.enemyShip.AirMax && hasAvailableEnergy())
			{
				setAirEnergy();
			}
			if (MainWindow.enemyShip.injuredCrewCount > 0 && hasAvailableEnergy())
			{
				setCrewEnergy();
			}
			//if (hasAvailableEnergy())
			//{
			//	setEvasionEnergy();
			//}
		}
		private static void setBombardmentEnergy()
		{
			if (Mechanics.GetWeaponRanges(MainWindow.enemyShip)[1] > (MainWindow.currentRange + MainWindow.enemyShip.BattleSpeed) && hasAvailableEnergy())
			{
				setSpeedEnergy();
			}
			if (MainWindow.enemyShip.OrdnanceCurrent <= (MainWindow.enemyShip.OrdnanceMax / 2) && hasAvailableEnergy())
			{
				setOrdnanceEnergy();
			}
			if (MainWindow.enemyShip.HullCurrent <= (MainWindow.enemyShip.HullMax / 2) && hasAvailableEnergy())
			{
				setHullEnergy();
			}
			if (MainWindow.enemyShip.ShieldCurrent < (MainWindow.enemyShip.ShieldMax / 2) && hasAvailableEnergy())
			{
				setShieldEnergy();
			}
			if (MainWindow.enemyShip.AirCurrent < MainWindow.enemyShip.AirMax && hasAvailableEnergy())
			{
				setAirEnergy();
			}
			if (MainWindow.enemyShip.injuredCrewCount > 0 && hasAvailableEnergy())
			{
				setCrewEnergy();
			}
			//if (hasAvailableEnergy())
			//{
			//	setEvasionEnergy();
			//}
		}
		private static void setCloseRangeEnergy()
		{
			if (Mechanics.GetWeaponRanges(MainWindow.enemyShip)[0] > (MainWindow.currentRange + MainWindow.enemyShip.BattleSpeed) && hasAvailableEnergy())
			{
				setSpeedEnergy();
			}
			if (hasAvailableEnergy())
			{
				setEvasionEnergy();
			}
			if (MainWindow.enemyShip.OrdnanceCurrent <= (MainWindow.enemyShip.OrdnanceMax / 2) && hasAvailableEnergy())
			{
				setOrdnanceEnergy();
			}
			if (MainWindow.enemyShip.HullCurrent <= (MainWindow.enemyShip.HullMax / 2) && hasAvailableEnergy())
			{
				setHullEnergy();
			}
			if (MainWindow.enemyShip.ShieldCurrent < (MainWindow.enemyShip.ShieldMax / 2) && hasAvailableEnergy())
			{
				setShieldEnergy();
			}
			if (MainWindow.enemyShip.AirCurrent < MainWindow.enemyShip.AirMax && hasAvailableEnergy())
			{
				setAirEnergy();
			}
			if (MainWindow.enemyShip.injuredCrewCount > 0 && hasAvailableEnergy())
			{
				setCrewEnergy();
			}
		}

		private static void setShieldEnergy()
		{
			var missingValue = MainWindow.enemyShip.ShieldMax - MainWindow.enemyShip.ShieldCurrent;
			var modifiedEnergy = false;

			for (int i = MainWindow.enemyShip.ShieldMaxBonus; i > 0; i--)
			{
				var tempEnergy = (i * MainWindow.thisPowerManagement.ShieldMultiplier);

				if (MainWindow.enemyShip.EnergyCurrent >= tempEnergy)
				{
					MainWindow.enemyShip.shieldEnergy = i;

					if (Mechanics.ReturnShieldRegen(MainWindow.enemyShip, MainWindow.enemyCaptain) <= missingValue)
					{
						modifiedEnergy = true;
						MainWindow.enemyShip.EnergyCurrent -= (int)Math.Floor(tempEnergy);
						break;
					}
				}
			}
			if (!modifiedEnergy)
			{
				MainWindow.enemyShip.shieldEnergy = 0;
			}
		}
		private static void setHullEnergy()
		{
			var missingValue = MainWindow.enemyShip.HullMax - MainWindow.enemyShip.HullCurrent;
			var modifiedEnergy = false;

			for (int i = MainWindow.enemyShip.HullMaxBonus; i > 0; i--)
			{
				var tempEnergy = (i * MainWindow.thisPowerManagement.HullMultiplier);

				if (MainWindow.enemyShip.EnergyCurrent >= tempEnergy)
				{
					MainWindow.enemyShip.hullEnergy = i;

					if (Mechanics.ReturnHullRegen(MainWindow.enemyShip, MainWindow.enemyCaptain) <= missingValue)
					{
						modifiedEnergy = true;
						MainWindow.enemyShip.EnergyCurrent -= (int)Math.Floor(tempEnergy);
						break;
					}
				}
			}
			if (!modifiedEnergy)
			{
				MainWindow.enemyShip.hullEnergy = 0;
			}
		}
		private static void setEvasionEnergy()
		{
			var modifiedEnergy = false;

			for (int i = (int)Math.Floor(MainWindow.enemyShip.EvasionMaxBonus); i > 0; i--)
			{
				var tempEnergy = (i * MainWindow.thisPowerManagement.EvasionMultiplier);

				if (MainWindow.enemyShip.EnergyCurrent >= tempEnergy)
				{
					MainWindow.enemyShip.evasionEnergy = i;

					modifiedEnergy = true;
					MainWindow.enemyShip.EnergyCurrent -= (int)Math.Floor(tempEnergy);
					break;
				}
			}
			if (!modifiedEnergy)
			{
				MainWindow.enemyShip.evasionEnergy = 0;
			}
		}
		private static void setOrdnanceEnergy()
		{
			var missingValue = MainWindow.enemyShip.OrdnanceMax - MainWindow.enemyShip.OrdnanceCurrent;
			var modifiedEnergy = false;

			for (int i = MainWindow.enemyShip.OrdnanceMaxBonus; i > 0; i--)
			{
				var tempEnergy = (i * MainWindow.thisPowerManagement.OrdnanceMultiplier);

				if (MainWindow.enemyShip.EnergyCurrent >= tempEnergy)
				{
					MainWindow.enemyShip.ordnanceEnergy = i;

					if (Mechanics.ReturnOrdnanceRegen(MainWindow.enemyShip, MainWindow.enemyCaptain) <= missingValue)
					{
						modifiedEnergy = true;
						MainWindow.enemyShip.EnergyCurrent -= (int)Math.Floor(tempEnergy);
						break;
					}
				}
			}
			if (!modifiedEnergy)
			{
				MainWindow.enemyShip.ordnanceEnergy = 0;
			}
		}
		private static void setAirEnergy()
		{
			var missingValue = MainWindow.enemyShip.AirMax - MainWindow.enemyShip.AirCurrent;
			var modifiedEnergy = false;

			for (int i = MainWindow.enemyShip.AirMaxBonus; i > 0; i--)
			{
				var tempEnergy = (i * MainWindow.thisPowerManagement.AirMultiplier);

				if (MainWindow.enemyShip.EnergyCurrent >= tempEnergy)
				{
					MainWindow.enemyShip.airEnergy = i;

					if (Mechanics.ReturnAirRegen(MainWindow.enemyShip, MainWindow.enemyCaptain) <= missingValue)
					{
						modifiedEnergy = true;
						MainWindow.enemyShip.EnergyCurrent -= (int)Math.Floor(tempEnergy);
						break;
					}
				}
			}
			if (!modifiedEnergy)
			{
				MainWindow.enemyShip.airEnergy = 0;
			}
		}
		private static void setCrewEnergy()
		{
			var modifiedEnergy = false;

			for (int i = (int)Math.Floor(MainWindow.enemyShip.CrewMaxBonus); i > 0; i--)
			{
				var tempEnergy = (i * MainWindow.thisPowerManagement.CrewMultiplier);

				if (MainWindow.enemyShip.EnergyCurrent >= tempEnergy)
				{
					MainWindow.enemyShip.crewEnergy = i;

					modifiedEnergy = true;
					MainWindow.enemyShip.EnergyCurrent -= (int)Math.Floor(tempEnergy);
					break;
				}
			}
			if (!modifiedEnergy)
			{
				MainWindow.enemyShip.airEnergy = 0;
			}
		}
		private static void setSpeedEnergy()
		{
			var modifiedEnergy = false;

			for (int i = MainWindow.enemyShip.BattleSpeedMaxBonus; i > 0; i--)
			{
				var tempEnergy = (i * MainWindow.thisPowerManagement.BattleSpeedMultiplier);

				if (MainWindow.enemyShip.EnergyCurrent >= tempEnergy)
				{
					MainWindow.enemyShip.battleSpeedEnergy = i;

					modifiedEnergy = true;
					MainWindow.enemyShip.EnergyCurrent -= (int)Math.Floor(tempEnergy);
					break;
				}
			}
			if (!modifiedEnergy)
			{
				MainWindow.enemyShip.battleSpeedEnergy = 0;
			}
		}

		// Weapon Management
		private static void targetWeapons(double accuracyThreshold)
		{
			foreach (Weapon tempWeapon in MainWindow.enemyShip.Weapons)
			{
				var target = Utility.GetRandom(100);
				string targetString;

				if (target < 50) // Hull
					targetString = MainWindow.userShip.TargetLocations[0,0];
				else if (target >= 50 && target < 70)
				{
					var subTarget = Utility.GetRandom(100);

					if (subTarget < 50 && MainWindow.userShip.TargetLocations[1, 2] != "0") // Fore Turrets
						targetString = MainWindow.userShip.TargetLocations[1, 0];
					else if (subTarget >= 50 && subTarget < 75 && MainWindow.userShip.TargetLocations[2, 2] != "0") // Aft Turrets
						targetString = MainWindow.userShip.TargetLocations[2, 0];
					else if (subTarget >= 75 && subTarget <= 100 && MainWindow.userShip.TargetLocations[3, 2] != "0") // Bottom Turrets
						targetString = MainWindow.userShip.TargetLocations[3, 0]; 
					else
						targetString = MainWindow.userShip.TargetLocations[0, 0]; // Hull
				}
				else if (target >= 70 && target < 80 && MainWindow.userShip.TargetLocations[4, 2] != "0") // Landing Bay
					targetString = MainWindow.userShip.TargetLocations[4, 0];
				else if (target >= 80 && target < 95 && MainWindow.userShip.TargetLocations[5, 2] != "0") // Engines
					targetString = MainWindow.userShip.TargetLocations[5, 0];
				else if (target >= 95 && target <= 100 && MainWindow.userShip.TargetLocations[6, 2] != "0") // Bridge
					targetString = MainWindow.userShip.TargetLocations[6, 0];
				else
					targetString = MainWindow.userShip.TargetLocations[0, 0]; // Hull

				if (Mechanics.CalculateAccuracy(MainWindow.enemyShip, MainWindow.enemyCaptain, tempWeapon, targetString) >= accuracyThreshold)
					tempWeapon.Target = targetString;
			}
		}
		private static bool checkAmmoCount()
		{
			bool canFire = false;
			var ordnance = MainWindow.enemyShip.OrdnanceCurrent;

			foreach (Weapon tempWeapon in MainWindow.enemyShip.Weapons)
			{
				if (tempWeapon.OrdnanceCost <= ordnance)
				{
					canFire = true;
					break;
				}
			}

			return canFire;
		}
		private static bool checkDeployableStatus()
		{
			bool hasDeployables = false;

			if (MainWindow.enemyShip.Deployables != null)
			{
				foreach (DeployableCraft tempCraft in MainWindow.enemyShip.Deployables)
				{
					if (tempCraft.HullCurrent > 0)
					{
						hasDeployables = true;
						break;
					}
				}
			}

			return hasDeployables;
		}

		// Movement Management
		private static void aiMovement(bool retreat, bool stayInRange, int move)
		{
			//TRAP TODO Update this for the new range stuff
			var location = MainWindow.enemyShipLocation;
			var weaponRanges = Mechanics.GetWeaponRanges(MainWindow.enemyShip);

			if (retreat)
			{
				if (stayInRange)
				{
					if ((location + move) > weaponRanges[0])
					{
						MainWindow.thisBattleNavigation.UpdateShipLocation(location + move, false);
					}
				}
				else if (!stayInRange)
				{
					MainWindow.thisBattleNavigation.UpdateShipLocation(location + move, false);
				}			
			}
			else if (!retreat)
			{
				if (stayInRange)
				{
					if ((location - move) > weaponRanges[0])
					{
						MainWindow.thisBattleNavigation.UpdateShipLocation(location - move, false);
					}
				}
				else if (!stayInRange)
				{
					MainWindow.thisBattleNavigation.UpdateShipLocation(location - move, false);
				}	
			}
		}
		private static bool aiCalculateRetreat(bool cowardly, double healthThreshold)
		{
			//TRAP TODO
			bool retreat = false;

			if ((MainWindow.enemyShip.HullCurrent / MainWindow.enemyShip.HullMax) <= healthThreshold)
			{
				// We might want to escape range to heal.
				if (cowardly)
				{
					retreat = true;
				}

				else if (Utility.GetRandom(100) > (50 * (MainWindow.enemyShip.HullCurrent / MainWindow.enemyShip.HullMax)))
				{
					retreat = true;
				}
			}
			if ((MainWindow.enemyShip.ShieldMax / 2) >= MainWindow.enemyShip.ShieldCurrent)
			{
				// We might want to escape range to heal.
				if (cowardly)
				{
					retreat = true;
				}

				else if (Utility.GetRandom(100) > (50 * (MainWindow.enemyShip.ShieldCurrent / MainWindow.enemyShip.ShieldMax)))
				{
					retreat = true;
				}
			}
			if (MainWindow.enemyShip.SectionDamagedCount > 0)
			{
				// We might want to escape range to heal.
				if (cowardly)
				{
					retreat = true;
				}

				else if (Utility.GetRandom(7) < MainWindow.enemyShip.SectionDamagedCount)
				{
					retreat = true;
				}
			}
			if (!checkAmmoCount())
			{
				// We might want to escape range to heal.
				if (cowardly)
				{
					retreat = true;
				}

				else if (Utility.GetRandom(100) > (50 * (MainWindow.enemyShip.OrdnanceCurrent / MainWindow.enemyShip.OrdnanceMax)))
				{
					retreat = true;
				}
			}
			if (!checkDeployableStatus())
			{
				if (cowardly)
				{
					retreat = true;
				}

				else if (Utility.GetRandom(100) < 50)
				{
					retreat = true;
				}
			}
			return retreat;
		}
		private static int determineSpeed(double modifier)
		{
			//TRAP TODO Fix this to not be stupid.
			var speed = MainWindow.enemyShip.BattleSpeed;
			int move = 0;
			int modifiedSpeed = 0;

			switch (speed)
			{
				case 0:
					modifiedSpeed = 0;
					break;
				case 1:
					modifiedSpeed = (int)Math.Floor(1 * modifier);
					break;
				case 2:
					modifiedSpeed = (int)Math.Floor(2 * modifier);
					break;
				case 3:
					modifiedSpeed = (int)Math.Floor(3 * modifier);
					break;
				case 4:
					modifiedSpeed = (int)Math.Floor(4 * modifier);
					break;
			}

			if (modifiedSpeed <= speed)
			{
				move = modifiedSpeed;
			}
			else
			{
				move = speed;
			}
			return move;
		}
		private void AttemptRetreat()
		{
			//TRAP TODO Finish this.
			// Check ship speed values.  If speed is faster, can attempt to flee.
			// If not faster, attempt to damage enemy ship engines, then flee.
		}
	}
}
