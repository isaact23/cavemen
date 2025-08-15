using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MouseLocker
{
    public static void LockMouse()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public static void UnlockMouse()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public static void ToggleMouse()
    {
        if (Cursor.visible) {
            LockMouse();
        }
        else {
            UnlockMouse();
        }
    }
}