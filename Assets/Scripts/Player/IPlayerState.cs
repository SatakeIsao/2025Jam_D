using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState
{
    void Enter(GameObject gameObject);
    void Update();
    void Exit();
}
