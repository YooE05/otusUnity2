using UnityEngine;
using Zenject;

namespace SampleGame
{
    public sealed class CameraFollower : ILateTickable
    {
        private readonly ICharacter _character;
        private readonly Camera _camera;
        private readonly Vector3 _cameraOffset;

        public CameraFollower(ICharacter character, Camera camera, Vector3 cameraOffset)
        {
            _character = character;
            _camera = camera;
            _cameraOffset = cameraOffset;
        }

        void ILateTickable.LateTick()
        {
            var cameraPosition = _character.GetPosition() + _cameraOffset;
            _camera.transform.position = cameraPosition;
        }
    }
}