//using AutoMapper;
//using BattleshipsApi.Hubs;
//using Microsoft.AspNetCore.SignalR;
//using Microsoft.VisualStudio.TestPlatform.Utilities;

//namespace BattleshipApiTests.Hubs
//{
//    public class BattleshipHub_Tests
//    {
//        private readonly BattleshipsApi.Handlers.QueueHandler _queueHandler = new();
//        private readonly BattleshipsApi.Handlers.GameLogicHandler _gameLogicHandler = new();
//        private IHubContext<BattleshipHub> _context;
//        private BattleshipsApi.Handlers.GameDataSender _gameDataSender;
//        private BattleshipsApi.Helpers.GameDataAdapter _gameDataAdapter;
//        private BattleshipsApi.Facades.BattleshipsFacade _battleshipFacade;
//        private BattleshipsApi.Hubs.BattleshipHub _battleshipHub;

//        public BattleshipHub_Tests(IHubContext<BattleshipHub> context, Mapper mapper)
//        {
//            _context = context;
//            _gameDataSender = new(_context);
//            _gameDataAdapter = new(_context, mapper);
//            _battleshipFacade = new(_queueHandler, _gameLogicHandler, mapper, _context, _gameDataSender);
//            _battleshipHub = new(_battleshipFacade);
//        }

//        [Test]
//        public void PlaceBattleshipToBoard()
//        {
//            // Arrange
//            var name1 = "Marius";
//            var nation1 = "German";
//            _battleshipHub.JoinQueue(name1, nation1);

//            var name2 = "Vinkas";
//            var nation2 = "Russian";
//            _battleshipHub.JoinQueue(name2, nation2);

//            // Act & Assert
//        }
//    }
//}
