using Annex;
using BounceBack.Scenes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BounceBack.Models
{
    class ScoreSingleton
    {
        private static ScoreSingleton instance = null;

        public static ScoreSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ScoreSingleton();
                }
                return instance;
            }
        }
        
        private int difficultyLevel = 1;

        private ScoreSingleton()
        {
        }

        public void IncreaseDifficulty()
        {
            difficultyLevel++;
        }

        public void DecreaseDifficulty()
        {
            difficultyLevel--;
        }

        public int GetDifficultyLevel()
        {
            return difficultyLevel;
        }

        public void ResetValues()
        {
            difficultyLevel = 1;
        }
    }
}
