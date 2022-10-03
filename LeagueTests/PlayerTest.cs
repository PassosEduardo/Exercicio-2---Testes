using FluentAssertions;
using MockingUnitTestsDemoApp.Impl.Models;
using MockingUnitTestsDemoApp.Impl.Repositories;
using MockingUnitTestsDemoApp.Impl.Repositories.Interfaces;
using MockingUnitTestsDemoApp.Impl.Services;
using NSubstitute;
using System;
using System.Collections.Generic;
using Xunit;

namespace LeagueTests
{
    public class PlayerTest
    {
        private readonly PlayerService _subject;
        private readonly ILeagueRepository _mockLeagueRepo;
        private readonly IPlayerRepository _mockplayerRepo;
        private readonly ITeamRepository _mockteamRepo;

        public PlayerTest()
        {
            _mockLeagueRepo = Substitute.For<ILeagueRepository>();
            _mockplayerRepo = Substitute.For<IPlayerRepository>();
            _mockteamRepo = Substitute.For<ITeamRepository>();
            _subject = new PlayerService(_mockplayerRepo, _mockteamRepo, _mockLeagueRepo);
        }


        [Fact]
        public void TestGetForLeague_Return_IsInvalid()
        {
            //Arrange
            _mockLeagueRepo.IsValid(Arg.Any<int>()).Returns(false);

            //Act
            var playerList = _subject.GetForLeague(Arg.Any<int>());

            //Assert
            playerList.Should().Equal(new List<Player>());
        }

        [Fact]
        public void TestGetForLeague_Return_IsValid()
        {
            //Arrange
            _mockLeagueRepo.IsValid(Arg.Any<int>()).Returns(true);
            _mockplayerRepo.GetForTeam(Arg.Any<int>()).Returns(new List<Player>());
            _mockteamRepo.GetForLeague(Arg.Any<int>()).Returns(new List<Team>());

            //Act
            var playerList = _subject.GetForLeague(Arg.Any<int>());

            //Assert

            playerList.Should().Equal(new List<Player>());



        }

    }
}