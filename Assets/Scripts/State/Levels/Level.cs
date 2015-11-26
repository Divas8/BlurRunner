﻿// Created 26.11.2015
// Modified by Александр 26.11.2015 at 20:38

namespace Assets.Scripts.State.Levels {
    #region References

    using Data;
    using EndlessEngine.Decorations;
    using EndlessEngine.Ground;
    using EndlessEngine.Obstacles;
    using Gameplay.Heroes;
    using UnityEngine;

    #endregion

    #region References

    #endregion

    internal class Level : ILevel {
        private readonly LevelData _data;

        public Level(LevelData data) {
            _data = data;
        }

        public DecorationItem[] Decorations {
            get { return _data.Decorations; }
        }

        public Obstacle[] Obstacles {
            get { return _data.Obstacles; }
        }

        public GroundBlock[] Ground {
            get { return _data.Ground; }
        }

        public int Number {
            get { return _data.Number; }
        }

        public Sprite Background {
            get { return _data.Background; }
        }

        public Hero Hero {
            get { return _data.Hero; }
        }

        public Sprite Startpoint {
            get { return _data.Startpoint; }
        }

        public float Length {
            get { return _data.Length; }
        }
    }
}