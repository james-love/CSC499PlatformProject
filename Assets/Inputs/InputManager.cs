using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private void Awake()
    {
        PlayerInput input = GetComponent<PlayerInput>();

        input.actions.actionMaps.First(m => m.name == "NormalMovement").Enable();
    }
}
