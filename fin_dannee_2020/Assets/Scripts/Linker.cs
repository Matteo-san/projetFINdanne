using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linker : MonoBehaviour
{
    #region singleton
    static Linker _instance;
    public static Linker instance { get { return _instance; } }
    Linker()
    {
        _instance = this;
    }
    #endregion

    public CharacterLook characterLook;
    public CharacterManager characterManager;
}
