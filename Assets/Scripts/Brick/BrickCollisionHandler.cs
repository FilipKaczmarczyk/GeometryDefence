using Belt;
using UnityEngine;

namespace Brick
{
    public class BrickCollisionHandler : MonoBehaviour
    {
        private BrickMovement _brickMovement;
    
        private void Awake()
        {
            _brickMovement = GetComponent<BrickMovement>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<ConveyorBelt>(out var conveyorBelt))
            {
                if (_brickMovement.CurrentBelt != null)
                {
                    if (_brickMovement.NextBelt != null)
                        return;

                    _brickMovement.SetNextBelt(conveyorBelt);
                }
                else
                {
                    _brickMovement.SetCurrentBelt(conveyorBelt);
                }
            }
        }
    }
}
