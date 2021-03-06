﻿// Created 23.10.2015 
// Modified by Gorbach Alex 11.11.2015 at 14:54

namespace Assets.Scripts.UI.Popups {
    #region References

    using System;
    using Engine;
    using UnityEngine.EventSystems;

    #endregion

    internal abstract class Popup : MonoBehaviourBase, IPopup {
        private EventTrigger _eventTrigger;

        public event Action<IPopup> Closed;

        protected override void Awake() {
            base.Awake();
            _eventTrigger = gameObject.AddComponent<EventTrigger>();
            var trigger = new EventTrigger.TriggerEvent();
            trigger.AddListener(OnClick);
            _eventTrigger.triggers.Add(
                new EventTrigger.Entry { eventID = EventTriggerType.PointerClick, callback = trigger });
        }

        private void OnClick(BaseEventData args) {
            Close();
        }

        protected virtual void OnClose() {
        }

        private void Close() {
            OnClose();
            var handler = Closed;
            if (handler != null) {
                handler.Invoke(this);
            }
        }
    }
}