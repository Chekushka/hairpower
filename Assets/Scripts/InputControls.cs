using UnityEngine;

[DefaultExecutionOrder(-1)]
public class InputControls : MonoBehaviour
{
    public delegate void HoldForwardStarted();
    public static event HoldForwardStarted OnHoldForwardStarted;
    public delegate void HoldForwardEnded();
    public static event HoldForwardEnded OnHoldForwardEnded;
    public delegate void Tap();
    public static event Tap OnTap;

    private InputPrefs _inputPrefs;

    public static InputControls Instance { get; private set; }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
        
        _inputPrefs = new InputPrefs();
    }

    private void Start()
    {
        _inputPrefs.Touch.ForwardMoveStarted.performed += x => OnHoldForwardStarted?.Invoke();
        _inputPrefs.Touch.ForwardMoveEnded.performed += x => OnHoldForwardEnded?.Invoke();
        _inputPrefs.Touch.Tap.performed += x => OnTap?.Invoke();
    }

    public Vector2 GetFingerPos() => _inputPrefs.Touch.TouchPosition.ReadValue<Vector2>();

    private void OnEnable() => _inputPrefs.Enable();

    private void OnDisable() => _inputPrefs.Disable();
}
