using UnityEngine;

public class ControlHandMoving : MonoBehaviour
{
   [SerializeField] private GameObject hand;
   private InputControls _inputControls;
   private Camera _mainCamera;
   private bool _isMovementGoing;
   
   private void Start()
   {
      _inputControls = FindObjectOfType<InputControls>();
      InputControls.OnHoldForwardStarted += EnableHand;
      InputControls.OnHoldForwardEnded += DisableHand;
      
      _mainCamera = Camera.main;
   }

   private void Update()
   {
      if(_isMovementGoing)
         MoveHand(_inputControls.GetFingerPos());
   }

   private void MoveHand(Vector2 fingerPos)
   {
      hand.transform.position = fingerPos;
   }

   private void EnableHand()
   {
      _isMovementGoing = true;
      hand.SetActive(true);
   }

  
   private void DisableHand()
   {
      _isMovementGoing = false;
      hand.SetActive(false);
   }
}
