namespace DragonBot.Core
{
    public class MicroBus
    {
        private readonly Dictionary<string, (Type, Action<IBusMessage<object>>)> Handlers = [];
        private readonly Dictionary<string, (Type, Func<IBusMessage<object>, Task>)> AsyncHandlers = [];
        public bool Subscribe<T>(string topic, Action<IBusMessage<T>> callback)
        {
            return Handlers.TryAdd(topic, (callback.GetType(), (Action<IBusMessage<object>>)callback));
        }
        public bool AsyncSubscribe<T>(string topic, Func<IBusMessage<T>, Task> callback)
        {
            return AsyncHandlers.TryAdd(topic, (callback.GetType(), (Func<IBusMessage<object>, Task>)callback));
        }
        public bool Unsubscribe(string topic)
        {
            return Handlers.Remove(topic);
        }
        public bool AsyncUnsubscribe(string topic)
        {
            return AsyncHandlers.Remove(topic);
        }
        public async Task<bool> Publish<T>(IBusMessage<T> message)
        {
            if (Handlers.TryGetValue(message.Topic, out (Type, Action<IBusMessage<object>>) value))
            {
                var (type, action) = value;
                if (type == typeof(Action<IBusMessage<T>>))
                {
                    ((Action<IBusMessage<T>>)action)(message);
                    return true;
                }
                return false;
            }
            return false;
        }
        public async Task<bool> PublishAsync<T>(IBusMessage<T> message)
        {
            if (AsyncHandlers.TryGetValue(message.Topic, out (Type, Func<IBusMessage<object>, Task>) value))
            {
                var (type, action) = value;
                if (type == typeof(Func<Task<IBusMessage<T>>>))
                {
                    await ((Func<IBusMessage<T>, Task>)action)(message);
                    return true;
                }
                return false;
            }
            return false;
        }
    }
    internal delegate Task AsyncBusMessageHandler<T>(IBusMessage<T> message);
    public abstract class BusMessage<T>(string topic, T payload) : IBusMessage<T>
    {
        public string Topic { get; } = topic;
        public T Payload { get; } = payload;
    }
    public readonly struct SmallBusMessage<T>(string topic, T payload) : IBusMessage<T>
    {
        public string Topic { get; } = topic;
        public T Payload { get; } = payload;
    }
    public interface IBusMessage<T>
    {
        public string Topic { get; }
        public T Payload { get; }
    }
}
