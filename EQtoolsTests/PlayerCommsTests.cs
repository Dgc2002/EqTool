﻿using Autofac;
using EQTool.Models;
using EQTool.Services.Parsing;
using EQTool.ViewModels;
using EQToolShared.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace EQToolTests
{
    [TestClass]
    public class PlayerCommsTests
    {
        private readonly IContainer container;

        PlayerCommsTests()
        {
            container = DI.Init();
        }


        [TestMethod]
        public void TestTell1()
        {
            //You told Qdyil, 'not even sure'
            var commsParser = container.Resolve<PlayerCommsParser>();
            DateTime now = DateTime.Now;
            var message = "You told Qdyil, 'not even sure'";
            var match = commsParser.Match(message, now);

            Assert.IsNotNull(match);
            Assert.AreEqual("Qdyil", match.Receiver);
            Assert.AreEqual("not even sure", match.Content);
            Assert.AreEqual(PlayerCommsEvent.Channel.TELL, match.TheChannel);
            Assert.AreEqual(now, match.TimeStamp);
            Assert.AreEqual(message, match.Line);
        }

        [TestMethod]
        public void TestTell2()
        {
            //Azleep -> Jamori: ok
            var commsParser = container.Resolve<PlayerCommsParser>();
            DateTime now = DateTime.Now;
            var message = "Azleep -> Jamori: ok";
            var match = commsParser.Match(message, now);

            Assert.IsNotNull(match);
            Assert.AreEqual("Jamori", match.Receiver);
            Assert.AreEqual("ok", match.Content);
            Assert.AreEqual(PlayerCommsEvent.Channel.TELL, match.TheChannel);
            Assert.AreEqual(now, match.TimeStamp);
            Assert.AreEqual(message, match.Line);
        }

        [TestMethod]
        public void TestSay()
        {
            //You say, 'Hail, Wenglawks Kkeak'
            var commsParser = container.Resolve<PlayerCommsParser>();
            DateTime now = DateTime.Now;
            var message = "You say, 'Hail, Wenglawks Kkeak'";
            var match = commsParser.Match(message, now);

            Assert.IsNotNull(match);
            Assert.AreEqual("", match.Receiver);
            Assert.AreEqual("Hail, Wenglawks Kkeak", match.Content);
            Assert.AreEqual(PlayerCommsEvent.Channel.SAY, match.TheChannel);
            Assert.AreEqual(now, match.TimeStamp);
            Assert.AreEqual(message, match.Line);
        }

        [TestMethod]
        public void TestGroup()
        {
            //You tell your party, 'oh interesting'
            var commsParser = container.Resolve<PlayerCommsParser>();
            DateTime now = DateTime.Now;
            var message = "You tell your party, 'oh interesting'";
            var match = commsParser.Match(message, now);

            Assert.IsNotNull(match);
            Assert.AreEqual("", match.Receiver);
            Assert.AreEqual("oh interesting", match.Content);
            Assert.AreEqual(PlayerCommsEvent.Channel.GROUP, match.TheChannel);
            Assert.AreEqual(now, match.TimeStamp);
            Assert.AreEqual(message, match.Line);
        }

        [TestMethod]
        public void TestGuild()
        {
            //You say to your guild, 'nice'
            var commsParser = container.Resolve<PlayerCommsParser>();
            DateTime now = DateTime.Now;
            var message = "You say to your guild, 'nice'";
            var match = commsParser.Match(message, now);

            Assert.IsNotNull(match);
            Assert.AreEqual("", match.Receiver);
            Assert.AreEqual("nice", match.Content);
            Assert.AreEqual(PlayerCommsEvent.Channel.GUILD, match.TheChannel);
            Assert.AreEqual(now, match.TimeStamp);
            Assert.AreEqual(message, match.Line);
        }

        [TestMethod]
        public void TestAuction()
        {
            //You auction, 'wtb diamond'
            var commsParser = container.Resolve<PlayerCommsParser>();
            DateTime now = DateTime.Now;
            var message = "You auction, 'wtb diamond'";
            var match = commsParser.Match(message, now);

            Assert.IsNotNull(match);
            Assert.AreEqual("", match.Receiver);
            Assert.AreEqual("wtb diamond", match.Content);
            Assert.AreEqual(PlayerCommsEvent.Channel.AUCTION, match.TheChannel);
            Assert.AreEqual(now, match.TimeStamp);
            Assert.AreEqual(message, match.Line);
        }

        [TestMethod]
        public void TestOOC()
        {
            //You say out of character, 'train to west'
            var commsParser = container.Resolve<PlayerCommsParser>();
            DateTime now = DateTime.Now;
            var message = "You say out of character, 'train to west'";
            var match = commsParser.Match(message, now);

            Assert.IsNotNull(match);
            Assert.AreEqual("", match.Receiver);
            Assert.AreEqual("train to west", match.Content);
            Assert.AreEqual(PlayerCommsEvent.Channel.OOC, match.TheChannel);
            Assert.AreEqual(now, match.TimeStamp);
            Assert.AreEqual(message, match.Line);
        }

        [TestMethod]
        public void TestShout()
        {
            //You shout, 'When it is time - Horse Charmers will be Leffingwell and Ceous'
            var commsParser = container.Resolve<PlayerCommsParser>();
            DateTime now = DateTime.Now;
            var message = "You shout, 'When it is time - Horse Charmers will be Leffingwell and Ceous'";
            var match = commsParser.Match(message, now);

            Assert.IsNotNull(match);
            Assert.AreEqual("", match.Receiver);
            Assert.AreEqual("When it is time - Horse Charmers will be Leffingwell and Ceous", match.Content);
            Assert.AreEqual(PlayerCommsEvent.Channel.SHOUT, match.TheChannel);
            Assert.AreEqual(now, match.TimeStamp);
            Assert.AreEqual(message, match.Line);
        }
    }
}
