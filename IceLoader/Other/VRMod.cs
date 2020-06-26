using UnityEngine;

namespace IceModSystem
{
    public class VRmod : MonoBehaviour
    {
        public virtual void OnStart() { }
        public virtual void OnUpdate() { }
        public virtual void OnFixedUpdate() { }
        public virtual void OnLateUpdate() { }
        public virtual void OnGUI() { }
        public virtual void OnQuit() { }
    }
}
