using UnityEngine;

public class ControlHandMoving : MonoBehaviour
{
   [SerializeField] private GameObject hand;
   [SerializeField] private GameObject handOnTap;
   private InputControls _inputControls;
   private Transform _currentHandTransform;
   private bool _isMovementGoing;
   
   private void Start()
   {
      _inputControls = FindObjectOfType<InputControls>();
      InputControls.OnHoldForwardStarted += EnableTapHand;
      InputControls.OnHoldForwardEnded += DisableTapHand;
   }

   private void Update()
   {
      if(_isMovementGoing)
         MoveHand(_inputControls.GetFingerPos());
   }

   public void DisableHands()
   {
      hand.SetActive(false);
      handOnTap.SetActive(false);
   }

   private void MoveHand(Vector2 fingerPos)
   {
      _currentHandTransform.position = fingerPos;
   }

   private void EnableTapHand()
   {
      _isMovementGoing = true;
      hand.SetActive(false);
      handOnTap.SetActive(true);
      _currentHandTransform = handOnTap.transform;
   }
   
   private void DisableTapHand()
   {
      _isMovementGoing = false;
      hand.SetActive(true);
      handOnTap.SetActive(false);
      _currentHandTransform = hand.transform;
   }
}
