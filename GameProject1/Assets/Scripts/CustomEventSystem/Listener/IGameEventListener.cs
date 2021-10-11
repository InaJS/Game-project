namespace CustomEventSystem.Listener
{
    public interface IGameEventListener<T>
    {
        void OnEventRaised(T item);
    }
}