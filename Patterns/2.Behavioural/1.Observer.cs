using System;

namespace Behavioural
{
    public class ObserverSample
    {
        public delegate void UpdatedEventHandler(string message);
        public event UpdatedEventHandler Update;

        public void GenerateUpdate(string message)
        {
            Update?.Invoke(message);
        }
    }

    public class Observer
    {
        private readonly int _myNumber;

        public Observer(int number)
        {
            _myNumber = number;
        }

        public void GetUpdate(string message)
        {
            Console.WriteLine($"{_myNumber}:Update={message}");
        }
    }
}