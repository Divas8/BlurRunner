﻿// Created 21.10.2015
// Modified by Александр 01.11.2015 at 17:30

namespace Assets.Scripts.State.Progress.Storage {
    #region References

    using System.Collections.Generic;
    using System.Linq;

    #endregion

    internal class ProgressStorage : IProgressStorage {
        private readonly IList<ILevelProgress> _levelsProgress;

        public ProgressStorage() {
            _levelsProgress = new List<ILevelProgress>();
            CurrentLevel = 0;
        }

        public ILevelProgress this[int levelNumber] {
            get { return _levelsProgress.FirstOrDefault(x => x.Number == levelNumber); }
        }

        public int CurrentLevel { get; private set; }

        public void SetCurrentLevel(int number) {
            CurrentLevel = number;
        }
    }
}