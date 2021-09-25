using System;

namespace Incapsulation.Weights
{
    public class Indexer
    {
        private readonly double[] _data;
        private readonly int _start;

        public Indexer(double[] data, int start, int length)
        {
            if (start < 0 || length < 0)
                throw new ArgumentException();

            if (start + length > data.Length)
                throw new ArgumentException();

            _data = data;
            _start = start;
            Length = length;
        }

        public int Length { get; }

        public double this[int index]
        {
            get
            {
                var realIndex = GetRealIndex(index);

                if (IsIndexValid(realIndex))
                    return _data[realIndex];

                throw new IndexOutOfRangeException();
            }

            set
            {
                var realIndex = GetRealIndex(index);

                if (IsIndexValid(realIndex))
                {
                    _data[realIndex] = value;
                }
                else throw new IndexOutOfRangeException();
            }
        }

        private int GetRealIndex(int index)
        {
            return index + _start;
        }

        private bool IsIndexValid(int index)
        {
            return !(index > Length || index < _start);
        }
    }
}