﻿// Created 09.11.2015 
// Modified by Gorbach Alex 09.11.2015 at 8:43

namespace Assets.Scripts.Gameplay.Bonuses {
    #region References

    using System;
    using EndlessEngine;
    using EndlessEngine.Bonuses;
    using UnityEngine;

    #endregion

    internal abstract class Bonus : HidingItem<Bonus>, IBonus {
        private bool _isCollectedNow = false;

        protected abstract int Direction { get; }
        protected abstract int Force { get; }

        protected override Bonus Instance {
            get {
                return this;
            }
        }

        public event Action<Bonus> Collected;
        public event Action<Bonus> EndCollected;

        public int Points {
            get {
                return Force * Direction;
            }
        }

        public abstract void Apply();

        public void OnTriggerEnter2D(Collider2D collision) {
            if (_isCollectedNow) {
                return;
            }
            var collector = collision.GetComponent<BonusCollector>();
            if (collector != null) {
                CollectAnimation();
                OnCollected();
                _isCollectedNow = true;
            }
        }

        private void OnCollected() {
            var handler = Collected;
            if (handler != null) {
                handler.Invoke(this);
            }
        }

        private void OnCollectEnd() {
            _isCollectedNow = false;
            var handler = EndCollected;
            if (handler != null) {
                handler.Invoke(this);
            }
        }

        protected virtual void CollectAnimation() {
            GetComponent<Animator>().SetTrigger("collect");
        }
    }
}