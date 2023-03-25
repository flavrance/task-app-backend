using System;

namespace TaskApp.Domain.ValueObjects
{
    public sealed class Description
    {
        private string _value;

        public Description(string value)
        {
            _value = value;
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public static implicit operator string(Description value)
        {
            return value._value;
        }        

        public static implicit operator Description(string value)
        {
            return new Description(value);
        }

        

        public static bool operator ==(Description description1, Description description2)
        {
            return description1._value == description2._value;
        }

        public static bool operator !=(Description description1, Description description2)
        {
            return description1._value != description2._value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is string)
            {
                return (string)obj == _value;
            }

            return ((Description)obj)._value == _value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}
