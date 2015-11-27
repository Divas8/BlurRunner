﻿// Created 20.10.2015
// Modified by  27.11.2015 at 12:21

#region References

#endregion

namespace Assets.Scripts.ZenjectConfig {
    #region References

    using System;
    using EndlessEngine.Bonuses;
    using EndlessEngine.Decorations;
    using EndlessEngine.Ground;
    using EndlessEngine.Obstacles;
    using Gameplay;
    using Gameplay.Consts;
    using Gameplay.GameState.Manager;
    using Gameplay.GameState.StateChangedSources;
    using State.Levels;
    using State.Levels.Storage;
    using State.Progress;
    using State.Progress.Score;
    using UnityEngine;
    using Zenject;

    #endregion

    internal class GameplayInstaller : MonoInstaller {
        [SerializeField]
        private Bonuses _bonusGenerator;

        [SerializeField]
        private Game _game;

        [SerializeField]
        private ScoreSettings _scoreSettings;

        public override void InstallBindings() {
            Container.Bind<IGame>().ToInstance(_game);
            Container.Bind<IGameStateManager>().ToSingle<GameStateManager>();
            Container.Bind<Camera>().ToSingleInstance(Camera.main);
            Container.Bind<IWinSource>().ToInstance(_game);
            Container.Bind<ILevelProgress>().ToSingleInstance(new LevelProgress());
            Container.Bind<IScoreSource>().ToInstance(_bonusGenerator);
            Container.Bind<int>(Identifiers.Scores.MinValue).ToInstance(_scoreSettings.ScoreToLose);
            Container.Bind<ILevel>().ToGetter<ILevelStorage>(storage => storage.CurrentLevel);
            Container.Bind<string>(Identifiers.Obstacles.Layer).ToInstance("Obstacle");

            Container.Bind<GroundFactory>().ToTransient();
            Container.Bind<DecorationsFactory>().ToTransient();
            Container.Bind<ObstacleFactory>().ToTransient();
            Container.Bind<BonusFactory>().ToTransient();

            InstallLevelSettings();
        }

        private void InstallLevelSettings() {
            Container.Bind<Decoration[]>().ToGetter<ILevel>(level => level.Decorations);
            Container.Bind<Obstacle[]>().ToGetter<ILevel>(level => level.Obstacles);
        }

        [Serializable]
        public class ScoreSettings {
            public int ScoreToLose;
        }
    }
}