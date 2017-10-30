using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceSim
{
	class Mechanics
	{
		//TRAP TODO I should add second bars onto the power and ordnance usages, to show how much is planned to be used during that turn for weapons and power manamgenet.  Opacity the current turns values.
		public static void CommitTurn()
		{
			MainWindow.thisBattleArmory.weaponList.SelectedItem = null; //TRAP TODO Temporary.  Move this somewhere.

			Utility.AddPageToLog(); // End the current log page.
			Utility.AddBlockToLog();

			Utility.WriteToLog("Combat:");
			FireWeapons(MainWindow.userShip, MainWindow.enemyShip, MainWindow.userCaptain, MainWindow.enemyCaptain);
			Utility.AddNewLineToLog();
			// Deployable Combat
			// Marine Combat
			// Repair Systems
			// Send communications

			MainWindow.thisBattleArmory.UpdateWeapons(MainWindow.userShip);
			
			Battle_AI.CombatTurn();

			if (CheckForDestroyed(MainWindow.userShip))
			{
				//TRAP TODO End combat
			}
			else
			{
				if (CheckForDestroyed(MainWindow.enemyShip))
				{
					//TRAP TODO End combat if deployables also dead.
				}
				else
				{
					Utility.AddBlockToLog();
					Utility.WriteToLog("Repairs and Rearms:");
					RegenerateHull(MainWindow.userShip, MainWindow.userCaptain);
					RegenerateShield(MainWindow.userShip, MainWindow.userCaptain);
					RegenerateEnergy(MainWindow.userShip, MainWindow.userCaptain);
					RegenerateOrdnance(MainWindow.userShip, MainWindow.userCaptain);
					ConsumeAir(MainWindow.userShip);
					RegenerateAir(MainWindow.userShip, MainWindow.userCaptain);
					if (CheckForDeadCrew(MainWindow.userShip))
					{
						Utility.WriteToLog(MainWindow.userShip.Name + " has lost all of its crew!");
						//TRAP TODO End combat
					}

					Utility.AddNewLineToLog();
					RegenerateHull(MainWindow.enemyShip, MainWindow.enemyCaptain);
					RegenerateShield(MainWindow.enemyShip, MainWindow.enemyCaptain);
					RegenerateEnergy(MainWindow.enemyShip, MainWindow.enemyCaptain);
					RegenerateOrdnance(MainWindow.enemyShip, MainWindow.enemyCaptain);
					ConsumeAir(MainWindow.enemyShip);
					RegenerateAir(MainWindow.enemyShip, MainWindow.enemyCaptain);
					if (CheckForDeadCrew(MainWindow.userShip))
					{
						Utility.WriteToLog(MainWindow.userShip.Name + " has lost all of its crew!");
						//TRAP TODO End combat if deployables also dead.
					}

					MainWindow.thisPowerManagement.ResetSliders();
					MainWindow.thisPowerManagement.UpdateModifiedValues();
					MainWindow.thisBattleNavigation.UpdateMove(true);
				}
			}
		}

		// Misc

		public static void SkillAdjustment(Captain targetCaptain)
		{
			//TRAP TODO
		}
		public static void SendCommunication(Ship tempShip)
		{
			//TRAP TODO
		}
		public static Weapon[] AddWeapon(Ship tempShip, Weapon tempWeapon)
		{
			var tempArray = new Weapon[tempShip.Weapons.Length + 1];

			Array.Copy(tempShip.Weapons, tempArray, tempShip.Weapons.Length);

			tempArray[tempArray.Length - 1] = tempWeapon;

			return tempArray;
		}
		public static Weapon[] RemoveWeapon(Ship tempShip, Weapon tempWeapon)
		{
			var tempArray = new Weapon[tempShip.Weapons.Length - 1];

			for (int weaponCount = 0; weaponCount < tempShip.Weapons.Length; weaponCount++)
			{
				if(tempShip.Weapons[weaponCount].ID != tempWeapon.ID)
				{
					tempArray[weaponCount] = tempShip.Weapons[weaponCount];
				}
			}

			return tempArray;
		}
		public static int[] GetWeaponRanges(Ship tempShip)
		{
			int[] range = new int[2];
			range[0] = tempShip.Weapons[0].Range;
			range[1] = tempShip.Weapons[0].Range;

			foreach (Weapon tempWeapon in tempShip.Weapons)
			{
				// Shortest Range
				if (tempWeapon.Range < range[0])
				{
					range[0] = tempWeapon.Range;
				}
				// Longest Range
				if (tempWeapon.Range > range[1])
				{
					range[1] = tempWeapon.Range;
				}
			}

			return range;
		}
		public static void ConsumeAir(Ship tempShip)
		{
			var airConsumed = tempShip.CrewCurrent;

			if (airConsumed <= tempShip.AirCurrent)
			{
				tempShip.AirCurrent -= airConsumed;
				Utility.WriteToLog(tempShip.Name + " consumed " + airConsumed + " air.");
			}
			else
			{
				var injuredCrew = tempShip.AirCurrent - airConsumed;
				tempShip.AirCurrent = 0;

				for (int i = 0; i < injuredCrew; i++)
				{
					InjureCrew(tempShip);
				}
				Utility.WriteToLog(tempShip.Name + " has had " + injuredCrew + " injured crew members due to lack of atmosphere.");
			}
		}
	
		// Combat
		public static void FireWeapons(Ship firingShip, Ship targetShip, Captain attackingCaptain, Captain targetCaptain)
		{
			for (int weaponCount = 0; weaponCount < firingShip.Weapons.Length; weaponCount++)
			{
				var firingWeapon = firingShip.Weapons[weaponCount];

				// Make sure we want to fire this weapon.
				if (firingWeapon.Target != null)
				{
					// Make sure the weapon isn't destroyed.
					if (firingWeapon.HullCurrent > 0)
					{
						// Make sure we have enough ordnance and energy left to fire it.
						if (firingShip.OrdnanceCurrent >= firingWeapon.OrdnanceCost && firingShip.EnergyCurrent >= firingWeapon.EnergyCost)
						{
							// Subtract the used ordnance and energy.
							firingShip.OrdnanceCurrent -= firingWeapon.OrdnanceCost;
							firingShip.EnergyCurrent -= firingWeapon.EnergyCost;
							
							// Gets aggressors hit chance.
							var accuracy = CalculateAccuracy(firingShip, attackingCaptain, firingWeapon);

							// Check if we hit.
							if (Utility.GetRandom(100) <= accuracy)
							{
								// Get the targets evasion chance.
								var targetEvasion = ReturnEvasion(targetShip, targetCaptain);

								// Check if the target evades.
								if (Utility.GetRandom(100) <= targetEvasion)
								{
									Utility.WriteToLog(firingShip.Name + " fired a " + firingWeapon.Name + " at the " + targetShip.Name + ", but they managed to evade!");
								}
								else
								{
									// Hit the shields if they exist.
									if (targetShip.ShieldCurrent > 0)
									{
										if (targetShip.ShieldCurrent - firingWeapon.ShieldDmg >= 0)
										{
											targetShip.ShieldCurrent -= firingWeapon.ShieldDmg;
										}
										else
										{
											targetShip.ShieldCurrent = 0;
										}

										Utility.WriteToLog(firingShip.Name + " fired a " + firingWeapon.Name + " at the " + targetShip.Name + ", it impacted their shields. (" + firingWeapon.ShieldDmg + ")");
										
										if (targetShip.ShieldCurrent == 0)
										{
											Utility.WriteToLog(targetShip.Name + "'s shield is down!");
										}
									}
									// Otherwise we hit what we are targetting.
									else
									{
										int targetIndex;
										for (targetIndex = 0; targetIndex < targetShip.TargetLocations.Length; targetIndex++)
										{
											if(targetShip.TargetLocations[targetIndex,0] == firingWeapon.Target)
											{
												break;
											}
										}
										switch(targetIndex)
										{
											case 0: // Hull
												targetShip.HullCurrent -= firingWeapon.HullDmg;
												Utility.WriteToLog(firingShip.Name + " fired a " + firingWeapon.Name + " at the " + targetShip.Name + ", and have damaged their hull! (" + firingWeapon.HullDmg + ")");
												break;
											case 1: // Fore Turrets
												//targetShip.TargetLocations[targetIndex, 2] -= firingWeapon.HullDmg;
												break;
											case 2: // Aft Turrets
												//targetShip.TargetLocations[targetIndex, 2] -= firingWeapon.HullDmg;
												break;
											case 3: // Bottom Turrets
												//targetShip.TargetLocations[targetIndex, 2] -= firingWeapon.HullDmg;
												break;
											case 4: // Landing Bay
												//targetShip.TargetLocations[targetIndex, 2] -= firingWeapon.HullDmg;
												break;
											case 5: // Engines
												//targetShip.TargetLocations[targetIndex, 2] -= firingWeapon.HullDmg;
												break;
											case 6: // Bridge
												//targetShip.TargetLocations[targetIndex, 2] -= firingWeapon.HullDmg;
												break;
										}

										// Anything else.
										//TRAP TODO Higher chance to damage section for other areas.
									}
								}
							}
							else
							{
								//TRAP TODO Miss
								Utility.WriteToLog(firingShip.Name + " fired a " + firingWeapon.Name + " at the " + targetShip.Name + ", but missed completly!");
							}
						}
					}
				}

				// Remove the weapon's target.
				firingWeapon.Target = null;
			}

			MainWindow.thisBattleStatus.UpdateHulls();
			MainWindow.thisBattleStatus.UpdateShields();
			MainWindow.thisBattleArmory.UpdateOrdnance();
			MainWindow.thisPowerManagement.UpdateEnergyBar();
		}
		public static double CalculateAccuracy(Ship attackingShip, Captain attackingCaptain, Weapon firingWeapon, string subTarget = null)
		{
			double accuracy = firingWeapon.Accuracy;

			accuracy *= 1 + (attackingCaptain.WeaponSkills[(Array.IndexOf(MainWindow.weaponTypes, firingWeapon.Class))] / 100) +
				((MainWindow.thisCrewModifiers.WeaponCrew * MainWindow.userShip.weaponCrewCount) / 100) +
				(ReturnBridgeCrewModifier(attackingShip));
			
			if (firingWeapon.Range < MainWindow.currentRange)
			{
				accuracy -= (MainWindow.currentRange - firingWeapon.Range) * 10;
			}

			if (subTarget != null)
			{
				switch (subTarget)
				{
					case "Bridge":
						accuracy -= 20;
						break;
					case "Fore Turrets":
						accuracy -= 10;
						break;
					case "Aft Turrets":
						accuracy -= 10;
						break;
					case "Bottom Turrets":
						accuracy -= 10;
						break;
					case "Landing Bay":
						accuracy -= 5;
						break;
					case "Engine":
						accuracy -= 5;
						break;
				}
			}

			return Math.Round(accuracy, 2);
		}
		public static void MarineCombat(Ship tempShip)
		{
			//TRAP TODO
		}
		public static void DeployableCombat(Ship attackingShip, Ship targetShip)
		{
			//TRAP TODO
		}
		public static bool CheckForDestroyed(Ship tempShip)
		{
			bool destroyed = false;

			if (tempShip.HullCurrent <= 0)
			{
				destroyed = true;
				Utility.WriteToLog(tempShip.Name + " has been destroyed!");
			}

			return destroyed;
		}
		public static bool CheckForDestroyed(DeployableCraft tempDeployable)
		{
			bool destroyed = false;

			if (tempDeployable.HullCurrent <= 0)
			{
				destroyed = true;
			}

			return destroyed;
		}
		public static bool CheckForDeadCrew(Ship tempShip)
		{
			bool crewDead = false;

			if (tempShip.CrewCurrent == 0)
			{
				crewDead = true;
			}

			return crewDead;
		}
		public static void InjureCrew(Ship tempShip)
		{
			if (tempShip.CrewCurrent > tempShip.injuredCrewCount)
			{
				tempShip.injuredCrewCount++;
			}
			else
			{
				KillCrew(tempShip);
			}
		}
		public static void KillCrew(Ship tempShip)
		{
			if (tempShip.CrewCurrent > 0)
			{
				tempShip.CrewCurrent -= 1;
				Utility.WriteToLog(tempShip.Name + " has lost a crew member!");
			}
		}
		// Healing
		public static void RegenerateEnergy(Ship tempShip, Captain tempCaptain)
		{
			double energyRegen = ReturnEnergyRegen(tempShip, tempCaptain);

			if (energyRegen != 0)
			{
				if (tempShip.EnergyMax >= tempShip.EnergyCurrent + energyRegen)
				{
					tempShip.EnergyCurrent += Convert.ToInt16(energyRegen);
					Utility.WriteToLog(tempShip.Name + " generated " + energyRegen + " energy.");
				}
				else
				{
					tempShip.EnergyCurrent = tempShip.EnergyMax;
					Utility.WriteToLog(tempShip.Name + " generated " + (tempShip.EnergyMax - tempShip.EnergyCurrent) + " energy.");
				}
			}

			MainWindow.thisPowerManagement.UpdateEnergyBar();
		}
		public static void RegenerateHull(Ship tempShip, Captain tempCaptain)
		{
			double hullRegen = ReturnHullRegen(tempShip, tempCaptain);

			if (hullRegen != 0)
			{
				if (tempShip.HullMax <= tempShip.HullCurrent + hullRegen)
				{
					tempShip.HullCurrent += Convert.ToInt16(hullRegen);
					Utility.WriteToLog(tempShip.Name + " patched " + hullRegen + " hull.");
				}
				else
				{
					tempShip.HullCurrent = tempShip.HullMax;
					Utility.WriteToLog(tempShip.Name + " patched " + (tempShip.HullMax - tempShip.HullCurrent) + " hull.");
				}
			}
		}
		public static void RegenerateShield(Ship tempShip, Captain tempCaptain)
		{
			double shieldRegen = ReturnShieldRegen(tempShip, tempCaptain);

			if (shieldRegen != 0)
			{
				if (tempShip.ShieldMax >= tempShip.ShieldCurrent + shieldRegen)
				{
					tempShip.ShieldCurrent += Convert.ToInt16(shieldRegen);
					Utility.WriteToLog(tempShip.Name + " stabilized " + shieldRegen + " shields.");
				}
				else
				{
					tempShip.ShieldCurrent = tempShip.ShieldMax;
					Utility.WriteToLog(tempShip.Name + " stabilized " + (tempShip.ShieldMax - tempShip.ShieldCurrent) + " shields.");
				}
			}
		}
		public static void RegenerateAir(Ship tempShip, Captain tempCaptain)
		{
			double airRegen = ReturnAirRegen(tempShip, tempCaptain);

			if (airRegen != 0)
			{
				if (tempShip.AirMax >= tempShip.AirCurrent + airRegen)
				{
					tempShip.AirCurrent += Convert.ToInt16(airRegen);
					Utility.WriteToLog(tempShip.Name + " produced " + airRegen + " air.");
				}
				else
				{
					tempShip.AirCurrent = tempShip.AirMax;
					Utility.WriteToLog(tempShip.Name + " produced " + (tempShip.AirMax - tempShip.AirCurrent) + " air.");
				}
			}
		}
		public static void RegenerateOrdnance(Ship tempShip, Captain tempCaptain)
		{
			double ordnanceRegen = ReturnOrdnanceRegen(tempShip, tempCaptain);

			if (ordnanceRegen != 0)
			{
				if (tempShip.OrdnanceMax >= tempShip.OrdnanceCurrent + ordnanceRegen)
				{
					tempShip.OrdnanceCurrent += Convert.ToInt16(ordnanceRegen);
					Utility.WriteToLog(tempShip.Name + " manufactured " + ordnanceRegen + " ordnance.");
				}
				else
				{
					tempShip.OrdnanceCurrent = tempShip.OrdnanceMax;
					Utility.WriteToLog(tempShip.Name + " manufactured " + (tempShip.OrdnanceMax - tempShip.OrdnanceCurrent) + " ordnance.");
				}
			}
		}
		public static void ShipDamageSection(Ship tempShip)
		{
			//TRAP TODO
			tempShip.SectionDamagedCount++;
		}
		public static void ShipRepairSection(Ship tempShip, Captain tempCaptain)
		{
			//TRAP TODO
			// If every section is repaired;
			tempShip.SectionDamagedCount--;
		}
		public static void CrewRecoveryCalculation(Ship tempShip, Captain tempCaptain)
		{
			for (int recoveryChanceCount = 0; recoveryChanceCount < MainWindow.userShip.medicalCrewCount; recoveryChanceCount++)
			{
				if (MainWindow.userShip.injuredCrewCount == 0)
				{
					var recoveryChance = ReturnMedicalRegen(tempShip, tempCaptain);

					var temp = Utility.GetRandom(100);

					if (temp <= recoveryChance)
					{
						MainWindow.userShip.injuredCrewCount -= 1;
						MainWindow.thisBattleCrew.HealCrewMember();
						Utility.WriteToLog(tempShip.Name + " has healed a crew member.");
					}
				}
			}
		}
		
		// Modifiers
		public static double ReturnBridgeCrewModifier(Ship tempShip)
		{
			double bridgeModifier = 0;
			bridgeModifier = ((tempShip.bridgeCrewCount * MainWindow.thisCrewModifiers.BridgeCrew) / 100);

			return bridgeModifier;
		}
		public static double ReturnEnergyRegen(Ship tempShip, Captain tempCaptain)
		{
			double energyRegen = tempShip.EnergyPerTurn;
			energyRegen *= 1 + ((tempCaptain.EnergySkill / 100) + (((tempShip.reactorCrewCount * MainWindow.thisCrewModifiers.ReactorCrew)) / 100) + 
				ReturnBridgeCrewModifier(tempShip));

			return Math.Floor(energyRegen);
		}
		public static double ReturnShieldRegen(Ship tempShip, Captain tempCaptain)
		{
			double shieldRegen = tempShip.ShieldRegen;
			var tempRegen = 1 + (((tempCaptain.ShieldSkill / 100)) + ReturnBridgeCrewModifier(tempShip));

			shieldRegen += tempShip.shieldEnergy;

			shieldRegen *= tempRegen;

			return Math.Floor(shieldRegen);
		}
		public static double ReturnHullRegen(Ship tempShip, Captain tempCaptain)
		{
			double hullRegen = tempShip.HullRegen;
			var tempRegen = 1 + ((tempCaptain.HullSkill / 100) + ReturnBridgeCrewModifier(tempShip));

			hullRegen += tempShip.hullEnergy;

			hullRegen *= tempRegen;

			return Math.Floor(hullRegen);
		}
		public static double ReturnEvasion(Ship tempShip, Captain tempCaptain)
		{
			double evasion = tempShip.Evasion;

			var tempEvasion = ((tempCaptain.EvasionSkill + (MainWindow.thisCrewModifiers.EngineCrew * MainWindow.userShip.engineCrewCount) / 100) + 
				ReturnBridgeCrewModifier(tempShip));

			evasion += tempShip.evasionEnergy;

			evasion *= tempEvasion;

			return Math.Floor(evasion);
		}
		public static double ReturnOrdnanceRegen(Ship tempShip, Captain tempCaptain)
		{
			double ordnance = tempShip.OrdnanceRegen;

			var tempOrdnance = 1 + (ReturnBridgeCrewModifier(tempShip));

			ordnance += tempShip.ordnanceEnergy;

			ordnance *= tempOrdnance;

			return Math.Floor(ordnance);
		}
		public static double ReturnAirRegen(Ship tempShip, Captain tempCaptain)
		{
			double air = tempShip.AirRegen;

			var tempAir = 1 + (ReturnBridgeCrewModifier(tempShip));
		
			air += tempShip.airEnergy;

			air *= tempAir;

			return Math.Floor(air);
		}
		public static double ReturnMedicalRegen(Ship tempShip, Captain tempCaptain)
		{
			double medical = tempShip.CrewRegen;
			var tempMedical = ((MainWindow.thisCrewModifiers.MedicalCrew * MainWindow.userShip.medicalCrewCount) * (1 + ((MainWindow.userCaptain.MedicalSkill) / 100) + 
				ReturnBridgeCrewModifier(tempShip)));

			medical += tempShip.crewEnergy;

			medical += tempMedical;

			return Math.Floor(medical);
		}
	}
}
