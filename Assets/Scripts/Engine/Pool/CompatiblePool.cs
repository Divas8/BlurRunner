﻿// Created 01.11.2015
// Modified by Александр 01.11.2015 at 20:59

namespace Assets.Scripts.Engine.Pool {
    #region References

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Factory.Strategy;
    using UnityEngine;
    using Zenject;

    #endregion

    internal abstract class CompatiblePool<T> : MonoBehaviourBase where T : MonoBehaviour, ICompatible<T> {
        public const string INITIAL_SIZE_KEY = "initialSize";
        public const string CAN_GROW_KEY = "canGrow";

        private bool _canGrow;
        private Factory.IFactory<T> _factory;
        private List<Item> _pool;
        private IGettingStrategy _strategy;

        public T Get(T origin) {
            Item obj;
            try {
                obj = _strategy.Get(_pool.Where(item => item.Free));
            }
            catch (ArgumentOutOfRangeException) {
                if (!_canGrow || (obj = AddNew()) == null) {
                    throw new PoolBusyException<T>();
                }
            }

            obj.Free = false;
            return obj.Object;
        }

        public virtual void Release(T obj) {
            var item = _pool.First(x => x.Object.Equals(obj));
            item.Free = true;
            obj.transform.SetParent(transform);
            obj.gameObject.SetActive(false);
        }

        protected override void Awake() {
            base.Awake();
            _pool = new List<Item>();
        }

        [PostInject]
        protected void Init(
            [Inject(CAN_GROW_KEY)] bool canGrow,
            [Inject(INITIAL_SIZE_KEY)] int initialSize,
            Factory.IFactory<T> factory,
            IGettingStrategy strategy) {
            _canGrow = canGrow;
            _factory = factory;
            _strategy = strategy;
            AddInitialItems(initialSize);
        }

        private void AddInitialItems(int count) {
            for (var i = 0; i < count; i++) {
                AddNew();
            }
        }

        private Item AddNew() {
            var obj = GetNew();
            if (obj == null) {
                return null;
            }
            obj.transform.SetParent(transform);
            obj.gameObject.SetActive(false);
            var item = new Item {Object = obj, Free = true};
            _pool.Add(item);
            return item;
        }

        private T GetNew() {
            return _factory.Create();
        }

        protected class Item {
            public T Object { get; set; }
            public bool Free { get; set; }
        }
    }
}