using System;
using UnityEngine;

namespace Belt
{
    public class ConveyorBelt : MonoBehaviour
    {
        [field:SerializeField] public Vector3 TargetPosition { get; private set; }
    
        public enum Direction
        {
            Up,
            Right,
            Down,
            Left
        }
    
        [Header("Physics")]
        [SerializeField] private Direction direction;
        [SerializeField] private float beltWidth;
    
        [Header("Visual")]
        [SerializeField] private float conveyorSpeed;

        private Material _material;
        private Vector2 _textureMoveDirection;

        private void Awake()
        {
            _material = GetComponent<MeshRenderer>().material;
        
            TargetPosition = CalculateTargetPosition();
            _textureMoveDirection = CalculateTextureDirection();
        }

        private void Update()
        {
            _material.mainTextureOffset += _textureMoveDirection * (conveyorSpeed * Time.deltaTime);
        }

        private Vector2 CalculateTextureDirection()
        {
            return direction switch
            {
                Direction.Up => new Vector2(-1, 0),
                Direction.Right => new Vector2(0, 1),
                Direction.Down => new Vector2(1, 0),
                Direction.Left => new Vector2(0, -1),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    
        private Vector3 CalculateTargetPosition()
        {
            var boxCollider = GetComponent<BoxCollider>();

            Vector3 targetPosition;
            
            switch (direction)
            {
                case Direction.Up:
                    targetPosition = transform.TransformPoint(new Vector3(boxCollider.center.x - boxCollider.size.x / 2f, 0, 0));
                    targetPosition.x -= beltWidth / 2f;
                    
                    break;
                case Direction.Right:
                    targetPosition = transform.TransformPoint(new Vector3(0, 0, boxCollider.center.z + boxCollider.size.z / 2f));
                    targetPosition.z += beltWidth / 2f;
                    
                    break;
                case Direction.Down:
                    targetPosition = transform.TransformPoint(new Vector3(boxCollider.center.x + boxCollider.size.x / 2f, 0, 0));
                    targetPosition.x += beltWidth / 2f;
                    
                    break;
                case Direction.Left:
                    targetPosition = transform.TransformPoint(new Vector3(0, 0, boxCollider.center.z - boxCollider.size.z / 2f));
                    targetPosition.z -= beltWidth / 2f;
                    
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return targetPosition;
        }
        
        public Direction GetDirection()
        {
            return direction;
        }
    }
}