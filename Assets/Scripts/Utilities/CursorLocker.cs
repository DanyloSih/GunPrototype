using System;
using UnityEngine;

namespace GunPrototype.Utilities
{
    public class CursorLocker : MonoBehaviour
    {
        [Serializable]
        public struct CursorState
        {
            public CursorLockMode LockState;
            public bool Visibility;
        }

        [SerializeField] private KeyCode _toggleCursorStateKey = KeyCode.LeftAlt;
        [SerializeField] private CursorState _onEnableCursorState;
        [SerializeField] private CursorState _onDisableCursorState;

        protected void OnEnable()
        {
            SetCursorState(_onEnableCursorState);
        }

        protected void OnDisable()
        {
            SetCursorState(_onDisableCursorState);
        }

        protected void Update()
        {
            if (Input.GetKeyDown(_toggleCursorStateKey))
            {
                SetCursorState(_onDisableCursorState);
            }
            else if (Input.GetKeyUp(_toggleCursorStateKey))
            {
                SetCursorState(_onEnableCursorState);
            }
        }

        private void SetCursorState(CursorState cursorState)
        {
            Cursor.lockState = cursorState.LockState;
            Cursor.visible = cursorState.Visibility;
        }
    }
}