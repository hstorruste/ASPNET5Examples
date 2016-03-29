using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;
using DAL;

namespace BLL
{
    public class BugsBLL : IBugsBLL
    {
        private IDbBugs _bugsRepo;

        public BugsBLL(IDbBugs bugsRepo)
        {
            _bugsRepo = bugsRepo;
        }

        public void AddToScoreboard(string name, int score)
        {
            _bugsRepo.AddToScoreboard(name, score);
        }

        public List<Level> GetAllLevels()
        {
            return _bugsRepo.GetAllLevels();
        }

        public List<Bug> GetBugList()
        {
            return _bugsRepo.GetBugList();
        }

        public List<Bug> GetBugListFromLevel(int levelId)
        {
            return _bugsRepo.GetBugListFromLevel(levelId);
        }

        public Level GetLevel(int id)
        {
            return _bugsRepo.GetLevel(id);
        }

        public Level GetLevelWithBugs(int id)
        {
            return _bugsRepo.GetLevelWithBugs(id);
        }

        public List<Scoreboard> GetScoreBoard()
        {
            return _bugsRepo.GetScoreBoard();
        }

        public bool ReachedTheScoreboard(int score)
        {
            var board = _bugsRepo.GetScoreBoard();
            var sortedBoard = board.OrderByDescending(e => e.score);
            if(sortedBoard.ElementAt(9).score < score) {
                return true;
            }
            return false;
        }
    }
}
