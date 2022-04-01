using System.ComponentModel;
using Character;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class InputControls : MonoBehaviour
{
    public delegate void HoldForwardStarted();
    public static event HoldForwardStarted OnHoldForwardStarted;
    public delegate void HoldForwardEnded();
    public static event HoldForwardEnded OnHoldForwardEnded;
    public delegate void HoldSide(Vector2 deltaPos);
    public static event HoldSide OnHoldSide;
    public delegate void Tap(Vector2 position);
    public static event Tap OnTap;
    
    private InputPrefs _inputPrefs;
    private void Awake() => _inputPrefs = new InputPrefs();

    private void Start()
    {
        _inputPrefs.Touch.ForwardMoveStarted.performed += x => OnHoldForwardStarted?.Invoke();
        _inputPrefs.Touch.ForwardMoveEnded.performed += x => OnHoldForwardEnded?.Invoke();
        _inputPrefs.Touch.SideMove.started += x => OnHoldSide?.Invoke(x.ReadValue<Vector2>());
        _inputPrefs.Touch.Attack.performed += x => 
            OnTap?.Invoke(_inputPrefs.Touch.TouchPosition.ReadValue<Vector2>());
    }


    private void OnEnable() =>_inputPrefs.Enable();
    private void OnDisable()=>_inputPrefs.Disable();
}
