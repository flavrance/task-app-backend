using System;
using System.Collections.Generic;
using System.Text;

namespace TaskApp.Domain.ValueObjects
{
    public sealed class Date
    {
        private DateTime _value;

        public Date(DateTime value)
        {
            _value = value;
        }

        public override string ToString()
        {
            return _value.ToString("dd/MM/yyyy");
        }

        public static implicit operator DateTime(Date value)
        {
            return value._value;
        }

        public static implicit operator Date(DateTime value)
        {
            return new Date(value);
        }

        public static bool operator >(Date date1, Date date2)
        {
            return date1._value > date2._value;
        }

        public static bool operator <=(Date date1, Date date2)
        {
            return date1._value <= date2._value;
        }

        public static bool operator >=(Date date1, Date date2)
        {
            return date1._value > date2._value;
        }

        public static bool operator <(Date date1, Date date2)
        {
            return date1._value < date2._value;
        }

        public static bool operator ==(Date date1, Date date2)
        {
            return date1._value == date2._value;
        }

        public static bool operator !=(Date date1, Date date2)
        {
            return date1._value != date2._value;
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

            if (obj is DateTime)
            {
                return (DateTime)obj == _value;
            }

            return ((Date)obj)._value == _value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}
