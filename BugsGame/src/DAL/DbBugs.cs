using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime;
using Model;

namespace DAL
{
    public class DbBugs : IDbBugs
    {
        BugContext db;
        public DbBugs(BugContext context) {
            db = context;
        }
        public void AddToScoreboard(string name, int score)
        {
            db.Scoreboards.Add(new Scoreboards { Name = name, Score = score, Date = DateTime.Now });
            db.SaveChanges();
        }

        //Returns all levels 
        public List<Level> GetAllLevels()
        {
            return db.Levels.Select(l => new Level() {
                id = l.Id,
                title = l.Title,
                seconds = l.Seconds,
                background = l.Background.picture,
                bugs = l.BugList.OrderBy(b => b.SequenceNumber).Select(b => new Bug()
                {
                    id = b.Bug.Id,
                    name = b.Bug.Name,
                    speed = b.Bug.Speed,
                    strength = b.Bug.Strength,
                    value = b.Bug.Value,
                    picture = b.Bug.Picture
                }).ToList()
                
            }).ToList();
        }

        public List<Bug> GetBugList()
        {
            return db.Bugs.Select(b => new Bug()
            {
                id = b.Id,
                name = b.Name,
                speed = b.Speed,
                strength = b.Strength,
                value = b.Value,
                picture = b.Picture
            }).ToList();
        }

        public List<Bug> GetBugListFromLevel(int levelId)
        {
            return db.BugLists.Where(bl => bl.LevelId == levelId).OrderBy(b => b.SequenceNumber).Select(b =>  new Bug()
            {
                id = b.Bug.Id,
                name = b.Bug.Name,
                speed = b.Bug.Speed,
                strength = b.Bug.Strength,
                value = b.Bug.Value,
                picture = b.Bug.Picture
            }).ToList();
        }

        public Level GetLevel(int id)
        {
            var level = db.Levels.SingleOrDefault(l => l.Id == id);
            return new Level()
            {
                id = level.Id,
                title = level.Title,
                background = level.Background.picture
            };
        }

        public Level GetLevelWithBugs(int id)
        {
            var level = db.Levels.SingleOrDefault(l => l.Id == id);
            return new Level()
            {
                id = level.Id,
                title = level.Title,
                background = level.Background.picture,
                seconds = level.Seconds,
                bugs = level.BugList.OrderBy(b => b.SequenceNumber).Select(b => new Bug()
                {
                    id = b.Bug.Id,
                    name = b.Bug.Name,
                    speed = b.Bug.Speed,
                    strength = b.Bug.Strength,
                    value = b.Bug.Value,
                    picture = b.Bug.Picture
                }).ToList()
            };
        }

        //Maby cut to 10 best scores.
        public List<Scoreboard> GetScoreBoard()
        {
            return db.Scoreboards.OrderByDescending(s => s.Score).Select(s => new Scoreboard()
            {
                name = s.Name,
                score = s.Score,
                date = s.Date
            }).ToList();
        }
    }
}
