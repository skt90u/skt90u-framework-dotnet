namespace JUtil.Service
{
    public interface IService
    {
        void Start();
        void Stop();
        void Suspend();
        void Continue();
    }
}
