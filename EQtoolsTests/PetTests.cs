﻿using Autofac;
using EQTool.Models;
using EQToolShared.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQtoolsTests
{
    [TestClass]
    public class PetTests : BaseTestClass
    {
        private readonly PlayerPet playerPet;

        // ctor
        public PetTests()
        {
            playerPet = container.Resolve<PlayerPet>();
        }

        [TestMethod]
        public void TestLoad()
        {
            Pets pets = playerPet.Pets;

            var petSpells = pets.PetSpellDictionary;
            PetSpell spell = petSpells["Emissary of Thule"];

            // 6x pet ranks: 5x min to max, 1x max+focus
            Assert.AreEqual(6, spell.PetRankList.Count);

            // 2x bone chips, 1x peridot
            Assert.AreEqual(2, spell.PetReagents.Count);
            
            Tuple<PetReagent, int> reagent0 = spell.PetReagents[0];
            Assert.AreEqual(PetReagent.BoneChip, reagent0.Item1);
            Assert.AreEqual(2, reagent0.Item2);

            Tuple<PetReagent, int> reagent1 = spell.PetReagents[1];
            Assert.AreEqual(PetReagent.Peridot, reagent1.Item1);
            Assert.AreEqual(1, reagent1.Item2);
        }


    }
}
