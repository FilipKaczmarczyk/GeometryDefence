using System;
using Belt;
using UnityEngine;

namespace Brick
{
    public class BrickMovement : MonoBehaviour
    {
        public ConveyorBelt CurrentBelt { get; private set; }
        public ConveyorBelt NextBelt { get; private set; }
    
        [SerializeField] private float moveSpeed;

        private Vector3 _currentPosition;
        private Vector3 _newPosition;
    
        private void FixedUpdate()
        {
            if(CurrentBelt == null)
                return;
        
            CalculateNewPosition();

            if (!IsTargetPositionReached()) 
                return;
        
            if (NextBelt)
            {
                CurrentBelt = NextBelt;
                NextBelt = null;
            }
        }
    
        private void CalculateNewPosition()
        {
            _currentPosition = transform.position;
        
            switch (CurrentBelt.GetDirection())
            {
                case ConveyorBelt.Direction.Up:
            
                    var newPositionXPlus = _currentPosition.x - moveSpeed * Time.fixedDeltaTime;
                    _newPosition = new Vector3(newPositionXPlus, _currentPosition.y, _currentPosition.z);
                    break;
            
                case ConveyorBelt.Direction.Right:
            
                    var newPositionZPlus = _currentPosition.z + moveSpeed * Time.fixedDeltaTime;
                    _newPosition = new Vector3(_currentPosition.x, _currentPosition.y, newPositionZPlus);
                    break;
            
                case ConveyorBelt.Direction.Down:
                    var newPositionXMinus = _currentPosition.x + moveSpeed * Time.fixedDeltaTime;
                    _newPosition = new Vector3(newPositionXMinus, _currentPosition.y, _currentPosition.z);
                
                    break;
                case ConveyorBelt.Direction.Left:
                    var newPositionZMinus = _currentPosition.z - moveSpeed * Time.fixedDeltaTime;
                    _newPosition = new Vector3(_currentPosition.x, _currentPosition.y, newPositionZMinus);
                
                    break;
            
                default:
                    throw new ArgumentOutOfRangeException();
            }

            transform.position = _newPosition;
        }
    
        private bool IsTargetPositionReached()
        {
            switch (CurrentBelt.GetDirection())
            {
                case ConveyorBelt.Direction.Up:
                    if (transform.position.x <= CurrentBelt.TargetPosition.x)
                        CurrentBelt = null;

                    break;
                case ConveyorBelt.Direction.Right:
                    if (transform.position.z >= CurrentBelt.TargetPosition.z)
                        CurrentBelt = null;
                
                    break;
                case ConveyorBelt.Direction.Down:
                    if (transform.position.x >= CurrentBelt.TargetPosition.x)
                        CurrentBelt = null;
                
                    break;
                case ConveyorBelt.Direction.Left:
                    if (transform.position.z <= CurrentBelt.TargetPosition.z)
                        CurrentBelt = null;
                
                    break;
            
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return CurrentBelt == null;
        }

        public void SetCurrentBelt(ConveyorBelt conveyorBelt)
        {
            CurrentBelt = conveyorBelt;
        }
    
        public void SetNextBelt(ConveyorBelt conveyorBelt)
        {
            NextBelt = conveyorBelt;
        }
    }
}
