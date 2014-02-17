using System;

namespace Lette.ProjectEuler.Core
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EulerAttribute : Attribute
    {
        public EulerAttribute(int number, string description) : this(number, description, null) {}

        public EulerAttribute(int number, string description, long answer) : this(number, description, (long?)answer) {}

        private EulerAttribute(int number, string description, long? answer)
        {
            Number = number;
            Description = description;
            Answer = answer;
        }

        public int Number { get; private set; }
        public string Description { get; private set; }
        public long? Answer { get; private set; }
    }
}