using UnityEngine;

namespace SampleGame
{
    [CreateAssetMenu(
        fileName = "CameraConfig",
        menuName = "Gameplay/New CameraConfig"
    )]
    public sealed class CameraConfig : ScriptableObject
    {
        [SerializeField] public Vector3 _cameraOffset = new Vector3(0, 7, -10);
    }
}