using UnityEngine;

namespace JAM.Scripts.Input
{
    public interface IMoveInput
    {
        Vector2 MoveDirection { get; }
        bool IsMoving { get; }
        bool IsFacingRight { get; }
    }
}
