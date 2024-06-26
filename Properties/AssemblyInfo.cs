﻿using System.Reflection;
using MelonLoader;

[assembly: AssemblyTitle(InventoryPersistence.Main.Description)]
[assembly: AssemblyDescription(InventoryPersistence.Main.Description)]
[assembly: AssemblyCompany(InventoryPersistence.Main.Company)]
[assembly: AssemblyProduct(InventoryPersistence.Main.Name)]
[assembly: AssemblyCopyright("Developed by " + InventoryPersistence.Main.Author)]
[assembly: AssemblyTrademark(InventoryPersistence.Main.Company)]
[assembly: AssemblyVersion(InventoryPersistence.Main.Version)]
[assembly: AssemblyFileVersion(InventoryPersistence.Main.Version)]
[assembly: MelonInfo(typeof(InventoryPersistence.Main), InventoryPersistence.Main.Name, InventoryPersistence.Main.Version, InventoryPersistence.Main.Author, InventoryPersistence.Main.DownloadLink)]
[assembly: MelonColor(System.ConsoleColor.White)]

// Create and Setup a MelonGame Attribute to mark a Melon as Universal or Compatible with specific Games.
// If no MelonGame Attribute is found or any of the Values for any MelonGame Attribute on the Melon is null or empty it will be assumed the Melon is Universal.
// Values for MelonGame Attribute can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame("Stress Level Zero", "BONELAB")]
[assembly: MelonOptionalDependencies("LabFusion")]
[assembly: MelonPriority(-1000)]