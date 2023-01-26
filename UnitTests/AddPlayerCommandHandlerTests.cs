using Xunit.Sdk;
using Application;
using Domain.Games;
using Moq;
using Application.Abstract;
using Application.Games.Base.Commands;
using Domain.Games.Elements;
using Domain.Enums;

namespace UnitTests
{
    [TestClass]
    public class AddPlayerCommandHandlerTests
    {
        private Mock<IGameManager> _gameManager;
        private AddPlayerCommandHandler _handler;
        private AddPlayerCommand _validCommand;
        private AddPlayerCommand _invalidCommand;

        [TestInitialize]
        public void Initialize()
        {
            _gameManager = new Mock<IGameManager>();
            _handler = new AddPlayerCommandHandler(_gameManager.Object);

            _validCommand = new AddPlayerCommand
            {
                GameId = "asayaastasta",
                Nickname = "Gusky",
                Avatar = Avatar.LULE,
                ConnectionId = "asas66a6as6"
            };

            _invalidCommand = new AddPlayerCommand
            {
                Nickname = null,
                ConnectionId = "",
                GameId = ""
            };
        }

        [TestMethod]
        public void Handle_ValidCommand_AddsPlayerToGame()
        {
            _gameManager.Setup(x => x.AddPlayerToGame(It.IsAny<Player>(), It.IsAny<string>())).Returns(PlayerType.guest);

            var result = _handler.Handle(_validCommand, CancellationToken.None).Result;

            _gameManager.Verify(x => x.AddPlayerToGame(It.IsAny<Player>(), _validCommand.GameId), Times.Once());

            Assert.AreEqual(PlayerType.guest, result);
        }

        [TestMethod]
        public void Handle_CommandWithAvatar_AddsPlayerWithAvatarToGame()
        {
            _gameManager.Setup(x => x.AddPlayerToGame(It.IsAny<Player>(), It.IsAny<string>())).Returns(PlayerType.guest);

            var result = _handler.Handle(_validCommand, CancellationToken.None).Result;

            _gameManager.Verify(x => x.AddPlayerToGame(It.Is<Player>(p => p.Avatar == Avatar.LULE), _validCommand.GameId), Times.Once());
            Assert.AreEqual(PlayerType.guest, result);
        }
    }
}