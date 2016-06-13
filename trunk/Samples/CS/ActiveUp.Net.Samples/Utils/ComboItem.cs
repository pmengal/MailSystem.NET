using System;
using System.Collections.Generic;
using System.Text;

namespace ActiveUp.Net.Samples.Utils
{
    class ComboItem
    {
        protected string _text;
        protected string _value;

        public ComboItem(string text)
        {
            _text = text;
        }

        public ComboItem(string text, string val)
        {
            _text = text;
            _value = val;
        }

        public string Text
        {
            get
            {
                return _text;
            }

            set
            {
                _text = value;
            }
        }

        public string Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
            }
        }

        public override string ToString()
        {
            return _text;
        }
    }
}
