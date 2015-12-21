﻿// Created 16.12.2015
// Modified by  21.12.2015 at 13:29

namespace Assets.Scripts.State.ScenesInteraction.Loaders {
    #region References

    using Engine;
    using Gameplay.Heroes;
    using UnityEngine;

    #endregion

    internal class LevelWorld : MonoBehaviourBase {
        [SerializeField]
        private Sprite _background;

        [SerializeField]
        private Canvas _backgroundCanvas;

        [SerializeField]
        private Canvas _foregroundCanvas;

        [SerializeField]
        private Hero _heroPrefab;

        [SerializeField]
        private Transform _startPoint;

        public Camera ForegroundCamera {
            set { _foregroundCanvas.worldCamera = value; }
        }

        public Camera BackgroundCamera {
            set { _backgroundCanvas.worldCamera = value; }
        }

        public Sprite Background {
            get { return _background; }
        }

        public Hero HeroPrefab {
            get { return _heroPrefab; }
        }

        public Transform StartPoint {
            get { return _startPoint; }
        }
    }
}