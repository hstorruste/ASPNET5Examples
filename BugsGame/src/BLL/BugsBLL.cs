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
            throw new NotImplementedException();
        }

        public List<Level> GetAllLevels()
        {
            throw new NotImplementedException();
        }

        public List<Bug> GetBugList()
        {
            throw new NotImplementedException();
        }

        public List<Bug> GetBugListFromLevel(int levelId)
        {
            throw new NotImplementedException();
        }

        public Level GetLevel(int id)
        {
            throw new NotImplementedException();
        }

        public Level GetLevelWithBugs(int id)
        {
            throw new NotImplementedException();
        }

        public List<Scoreboard> GetScoreBoard()
        {
            throw new NotImplementedException();
        }

        public bool ReachedTheScoreboard(int score)
        {
            throw new NotImplementedException();
        }
    }
}
