namespace IceModSystem
{
    public class VRmod
    {
        public virtual int LoadOrder => 9999;
        public virtual void OnEarlierStart() { }
        public virtual void OnStart() { }
        public virtual void OnUpdate() { }
        public virtual void OnFixedUpdate() { }
        public virtual void OnLateUpdate() { }
        public virtual void OnGUI() { }
        public virtual void OnQuit() { }
    }
}
